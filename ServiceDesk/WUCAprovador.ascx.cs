/*
- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
  • User Control para seleção dos aprovadores.
  
  	Data: 06/03/2006
  	Autor: Fernanda Passos
  	Descrição: Este possibilita solicitar aprovação.
  
  • Alterações
  	Data: 
  	Autor: 
  	Descrição: 
- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
*/
using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ServiceDesk.Negocio;  

public partial class WUCAprovador : System.Web.UI.UserControl
{
    #region Métodos

    #region Marca e Desmarca todos da grid
    /// <summary>
    /// Marca e Desmarca todos da grid
    /// </summary>
    /// <param name="bolMarcarTodos">Marca ou Desmarca Todos da Grid</param>
    public void SelecaoPessoaGrid(bool bolMarcarTodos)
    {
        try
        {
            int intCount = 0;
            while (intCount < this.gvPessoa.Rows.Count)
            {
                GridViewRow objRow = (GridViewRow)this.gvPessoa.Rows[intCount];
                if (objRow != null)
                {
                    CheckBox cbxPessoa = (CheckBox)objRow.FindControl("cbxPessoa");
                    cbxPessoa.Checked = bolMarcarTodos;
                }
                intCount ++;
            }
        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region Salvar\Enviar Notificação
    /// <summary>
    /// Salvar - Enviar Notificação
    /// </summary>
    /// <param name="intCodigoReceptor">código do usuário receptor</param> 
    /// <param name="intTabelaIdentificador">Código do identificador do registro do processo</param> 
    /// <param name="strDescricao">Descrição da Notificação</param> 
    /// <param name="strMensagem">Mensagem que retorna o resultado do evento Notificar</param> 
    /// <param name="strTabela">Nome da tabela que representa o processo que envia a notificação</param> 
    /// <param name="strTipo">Tipo de Notificação</param> 
    /// <returns>Retorna true ou false. Se foi salvo com sucesso ou não.</returns>
    public static bool Salvar(string strTabela, int intTabelaIdentificador, string strDescricao, int intCodigoReceptor, out string strMensagem, string strTipo)
    {
        try
        {
            strMensagem = string.Empty;
            bool bolValorRetorno = false;
            ServiceDesk.Negocio.ClsNotificacao objNotificacao = new ClsNotificacao();

            ClsIdentificador objIdentificador = new ClsIdentificador();
            objIdentificador.Tabela.Valor = objNotificacao.Atributos.NomeTabela;
            objNotificacao.Codigo.Valor = objIdentificador.getProximoValor().ToString();

            objNotificacao.Descricao.Valor = strDescricao.Trim();
            objNotificacao.Tabela.Valor = strTabela.Trim();
            objNotificacao.IdentificadorTabela.Valor = intTabelaIdentificador.ToString();
            objNotificacao.CodigoUsuarioEmissor.Valor = ClsUsuario.getCodigoUsuario().ToString();
            objNotificacao.CodigoUsuarioReceptor.Valor = intCodigoReceptor.ToString();
            if (strTipo.Trim() != string.Empty) objNotificacao.Tipo.Valor = strTipo.Trim();       
            objNotificacao.DtInclusao.Valor = DateTime.Now.ToString(ClsParametro.DataInclusao);

            if (objNotificacao.enviar(out strMensagem) == true)
            {
                objIdentificador.atualizaValor();
                bolValorRetorno = true;
            }
            else bolValorRetorno = false;

            objNotificacao = null;
            objIdentificador = null;

            return bolValorRetorno;
        }
        catch (Exception ex)
        {
            //ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
            strMensagem = ex.Message.Trim(); 
            return false;
        }
    }
    #endregion

    #region Preenche Histórico
    /// <summary>
    /// Preenche Histórico de Notificações do Registro Atual
    /// </summary>
    /// <param name="strTabela">Nome da tabeça física no banco de dados</param>
    /// <param name="intTabelaIdentificador">Código do registro identificador</param>
    public void PreencheHistorico(string strTabela, int intTabelaIdentificador)
    {
        try
        {
            ClsNotificacao.geraGridView(gvHistorico, strTabela.Trim(), intTabelaIdentificador,ClsUsuario.getCodigoUsuario());        
        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region Preenche Campos
    /// <summary>
    /// Preenche campos.
    /// </summary>
    /// <param name="strTabela"></param>
    /// <param name="intTabelaIdentificador"></param>
    public void PreencheCampos(string strTabela, int intTabelaIdentificador)
    {
        txtTabela.Text = strTabela;
        txtTabelaIdentificador.Text = intTabelaIdentificador.ToString();
        PreencheHistorico(strTabela.Trim(), intTabelaIdentificador);
        SServiceDesk.Negocio.ClsTipo.geraDropDownList(ddlTipoNotificacao, "Selecione");      
    }
    #endregion

    #region Validação dos registros
    /// <summary>
    /// Validação dos dados
    /// </summary>
    /// <param name="strMensagem">Mensagem de retorna da validação</param>
    /// <returns>Retorna true ou false. Se foi validado ou não</returns>
    public bool ValidaDados(out String strMensagem)
    {
        strMensagem = string.Empty;
        bool bolRetorno = true;
        try
        {
            //=================================================================================//
            // - O que: Valida as informações antes de enviar a notificação
            // - Quem: Fernanda Passos.
            // - Quando: 06/03/2006 ás 15:25hs.
            //=================================================================================//
            if (gvPessoa.Rows.Count <= 0)
            {
                bolRetorno = false;
                strMensagem = "Por favor, selecione uma pessoa para enviar uma mensagem de notificação.";
            }
            else if (txtTabela.Text == string.Empty && txtTabelaIdentificador.Text == string.Empty)
            {
                bolRetorno = false;
                strMensagem = "Por favor, salve o registro do processo antes de notificar.";
            }
            else if ( ddlTipoNotificacao.SelectedValue == string.Empty)
            {
                bolRetorno = false;
                strMensagem = "Por favor, selecione o tipo de notificação que deseja enviar.";
            }
            //=================================================================================//

            divMensagem.Visible = false;
            return bolRetorno;
            
        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
            return bolRetorno;
        }
    }
    #endregion

    #region Mensagem
    /// <summary>
    /// Mensagem
    /// </summary>
    /// <param name="bolExibeMensagem">Indica se é para exibir ou ocular a mensagem</param>
    /// <param name="strMensagem">Conteúdo da mensagem</param>
    /// <param name="bolMensagemCritica">Indica o tipo de gif a ser usado, para mensagem critica ou não</param>
    public void Mensagem(bool bolExibeMensagem, string strMensagem, bool bolMensagemCritica)
    {
        try
        {
            if (bolExibeMensagem == true && strMensagem.Trim() != string.Empty)
            {
                lblMensagem.Text = strMensagem.Trim();
                if (bolMensagemCritica == false) imgIcone.ImageUrl = "images/icones/info.gif"; else imgIcone.ImageUrl = "images/icones/aviso.gif";
                divMensagem.Visible = true;
            }
            else divMensagem.Visible = false;
        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #endregion

    #region Eventos

    #region Evento Page Load
    /// <summary>
    /// Page Load
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            SServiceDesk.Negocio.ClsEstruturaOrganizacional.geraDropDownListPorEmpresa(ddlEmpresa, Convert.ToInt32(ClsParametro.CodigoTipoEmpresa));
            this.ddlEmpresa.Items.Insert(0, "Selecione");
            this.ddlEmpresa.Items[0].Value = "";

            if (ClsParametro.CodigoEmpresa != null)
            {
                ListItem objItem = (ListItem)ddlEmpresa.Items.FindByValue(ClsParametro.CodigoEmpresa.ToString());
                if (objItem != null)
                {
                    objItem.Selected = true;
                }
                objItem = null;

                if (ddlEmpresa.SelectedIndex > 0)
                {
                    gvPessoa.DataSource = null;
                    SServiceDesk.Negocio.ClsPessoa.geraGridViewBusca(gvPessoa, "", "", Convert.ToInt32(ddlEmpresa.SelectedValue), 0);
                }
            }
        }
    }
    #endregion

    #region Evento Salvar
    /// <summary>
    /// Evento Salvar
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSalvar_Click(object sender, EventArgs e)
    {
        try
        {

            string strMensagem = string.Empty;

            //=======================================================================//
            // - O que: Verifica se os dados foram informandos corretamente.
            // - Quem: Fernanda Passos
            // - Quando: 06/03/2006 ás 15:24hs
            //=======================================================================//
            if (ValidaDados(out strMensagem) == false)
            {
                Mensagem(true, strMensagem, false);
                return;
            }
            //=======================================================================//

            string strMensagemNotificacaoPadrao = string.Empty;
            strMensagemNotificacaoPadrao = "SOLICITAÇÃO: " + ClsUsuario.getNomeUsuario() + " vem solicitar á aprovação\aceitação do(a) " + txtTabela.Text.Trim().ToUpper() ;    

            int intCount = 0;

            while (intCount < gvPessoa.Rows.Count)
            {
                GridViewRow objRow = gvPessoa.Rows[intCount];
                if (objRow != null)
                {
                    CheckBox cbxPessoa = (CheckBox)objRow.FindControl("cbxPessoa");
                    Label lblCodigoPessoa = (Label)objRow.FindControl("lblCodigoPessoa");

                    if (cbxPessoa.Checked == true && lblCodigoPessoa.Text != string.Empty)
                    {
                        //=================================================================================//
                        // - O que: Notifica cada pessoa selecionada na grid de pessoas.
                        // - Quem: Fernanda Passos.
                        // - Quando: 06/03/2006 ás 15:25hs.
                        //=================================================================================//
                        Salvar(txtTabela.Text.Trim(), Convert.ToInt32(txtTabelaIdentificador.Text.Trim()), strMensagemNotificacaoPadrao, Convert.ToInt32(lblCodigoPessoa.Text.Trim()), out strMensagem, ddlTipoNotificacao.SelectedValue.Trim());
                        //=================================================================================//
                    }

                }
                intCount ++;
            }

            Mensagem(true, strMensagem, false);
        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region Evento ddlEmpresa_SelectedIndexChanged
    /// <summary>
    /// Evento ddlEmpresa_SelectedIndexChanged
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlEmpresa_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlEmpresa.SelectedIndex != -1) SServiceDesk.Negocio.ClsPessoa.geraGridViewBusca(gvPessoa, "", "", Convert.ToInt32(ddlEmpresa.SelectedValue.Trim()), 0);
        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region Evento Selecionar Todos
    /// <summary>
    /// Evento Selecionar Todos
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void MarcarTodas(object sender, EventArgs e)
    {
        SelecaoPessoaGrid(true);
    }
    #endregion

    #region Evento Selecionar Todos
    /// <summary>
    /// Evento Des Selecionar Todos
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void DesmarcarTodas(object sender, EventArgs e)
    {
        SelecaoPessoaGrid(false);
    }
    #endregion

    #region Evento gvHistorico_RowDataBound
    /// <summary>
    /// Evento gvHistorico_RowDataBound
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvHistorico_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if ((e.Row.RowState == DataControlRowState.Normal) || (e.Row.RowState == DataControlRowState.Alternate))
            {
                Label lblStatus = (Label)e.Row.FindControl("lblStatus");
                Label txtStatus = (Label)e.Row.FindControl("txtStatus");

                if (lblStatus != null)
                {
                    if (lblStatus.Text == string.Empty) lblStatus.Text = "Pendente aprovação";
                    else if (lblStatus.Text == "N") lblStatus.Text = "Não aprovado";
                    else if (lblStatus.Text == "S") lblStatus.Text = "Aprovado";
                }
            }
        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion
    
    #region Evento btnFiltrar_Click
    /// <summary>
    /// Evento btnFiltrar_Click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnFiltrar_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlEmpresa.SelectedIndex != -1)
            {
                gvPessoa.DataSource = null;  
                SServiceDesk.Negocio.ClsPessoa.geraGridViewBusca(gvPessoa, txtPessoa.Text.Trim() , "", Convert.ToInt32(ddlEmpresa.SelectedValue.Trim()), 0);
            }
        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion 
    #endregion
}
