/*
- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
  • Formulário que permite apresentar as notificações emitidas para o usuário 
    que esta usando o sistema.
  
  	Data: 29/11/2005
  	Autor: Fernanda Passos
  	Descrição: Este WebForm apresenta as notificações do usuário que esta logado na máquina\que 
    que acessou o sistema e de nenhum outro usuário.
  
  
  • Alterações
  	Data:26/12/2005
  	Autor: Sylvio Neto
  	Descrição: Condicional para desalocar equipe/tecnico de um incidente caso a notificacao esteja
    relacionada a um incidente no rowcommand "Reprovar".
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

public partial class Notificacao : BasePage
{
    #region Declarações
    private ClsNotificacao objNotificacao = new ClsNotificacao();
    #endregion

    #region Page Load
    /// <summary>
    /// Evento Page Load
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            CheckAcesso(49);

            if (!Page.IsPostBack)
            {
                //Alimenta grid
                CarregaGridNotificacao();
            }
        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, user.IDusuario.ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region Carrega o grid de notificações
    /// <summary>
    /// Carrega o grid de notificações
    /// </summary>
    private void CarregaGridNotificacao()
    {
        try
        {
            objNotificacao.geraGridView(gvNotificacao, user.IDusuario);
        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, user.IDusuario.ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region RowCommand do Grid Notificação
    /// <summary>
    /// Aprovar e Reprovar notificação
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvNotificacao_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            string strMsg = string.Empty;

            GridViewRow objRow = gvNotificacao.Rows[Convert.ToInt32(e.CommandArgument)];
            if (objRow == null) return;

            //Preenche atributos da coleção
            objNotificacao.DtResposta.Valor = System.DateTime.Now.ToString(ClsParametro.DataInclusao);

            Label lblCodigo = (Label)objRow.FindControl("lblCodigo");
            objNotificacao.Codigo.Valor = lblCodigo.Text.Trim();

            TextBox txtJustificativa = (TextBox)objRow.FindControl("txtJustificativa");

            string strTipo = string.Empty;
            if (e.CommandName == "Reprovar") strTipo = "reprovado(a)";
            if (e.CommandName == "Aprovar") strTipo = "aprovado(a)";

            if (e.CommandName == "Reprovar")
            {
                //Verifica se foi informado a justificativa, pois a mesma é obrigatória
                //quando se reprova.
                if (txtJustificativa.Text == string.Empty)
                {
                    Mensagem(true, "Descreva a justificativa.", false);
                    return;
                }
                Mensagem(false, string.Empty, false);

                //Atualiza a base de dados
                objNotificacao.Justificativa.Valor = txtJustificativa.Text.Trim();
                objNotificacao.FlagAprovado.Valor = "N";
            }
            else if (e.CommandName == "Aprovar")
            {
                objNotificacao.Justificativa.Valor = txtJustificativa.Text.Trim();
                objNotificacao.FlagAprovado.Valor = "S";
            }

            if (objNotificacao.altera(out strMsg) == false)
            {
                Mensagem(true, strMsg.Trim(), true);
            }
            else
            {
                try
                {
                    ServiceDesk.Negocio.ClsNotificacao objNotificacaoAtual = new ServiceDesk.Negocio.ClsNotificacao(Convert.ToInt32(lblCodigo.Text.Trim()));
                    //==================================================================================//
                    // - O que: Se o tipo de notificação for de aprovação e se é notificação de processo
                    // se sim o sistema irá rodar o WorkFlow e notificar ao proprietário.
                    // - Quem: Fernanda Passos
                    // - Quando: 09/03/2006 ás 16:22hs
                    //==================================================================================//
                    if (objNotificacaoAtual.Tipo.Valor != string.Empty)
                    {
                        SServiceDesk.Negocio.ClsWorkFlow.salvaStatus(objNotificacaoAtual.Tabela.Valor.Trim(), Convert.ToInt32(objNotificacaoAtual.IdentificadorTabela.Valor.Trim()), 0, ServiceDesk.Negocio.ClsStatus.GetCodigoStatusProcesso(objNotificacaoAtual.Tabela.Valor.Trim(), Convert.ToInt32(objNotificacaoAtual.IdentificadorTabela.Valor.Trim())), 0);

                        ClsIdentificador objIdentificador = new ClsIdentificador();
                        objIdentificador.Tabela.Valor = objNotificacao.Atributos.NomeTabela;
                        objNotificacao.Codigo.Valor = objIdentificador.getProximoValor().ToString();

                        objNotificacao.Tabela.Valor = objNotificacaoAtual.Tabela.Valor;
                        objNotificacao.CodigoUsuarioEmissor.Valor = user.IDusuario.ToString();
                        objNotificacao.CodigoUsuarioReceptor.Valor = ClsUsuario.GetCodigoProprietario(objNotificacaoAtual.Tabela.Valor.Trim(), Convert.ToInt32(objNotificacaoAtual.IdentificadorTabela.Valor.Trim()));
                        objNotificacao.Descricao.Valor = "INFORMATIVO: " + user.Nome.ToUpper() + " informa que o(a) " + objNotificacaoAtual.Tabela.Valor.ToUpper() + " de código " + objNotificacaoAtual.IdentificadorTabela.Valor + " foi " + strTipo;
                        objNotificacao.DtInclusao.Valor = DateTime.Now.ToString(ClsParametro.DataInclusao);
                        objNotificacao.IdentificadorTabela.Valor = objNotificacaoAtual.IdentificadorTabela.Valor;
                        objNotificacao.FlagAprovado.Valor = string.Empty;
                        objNotificacao.Justificativa.Valor = string.Empty;
                        objNotificacao.DtResposta.Valor = string.Empty;
                        if (objNotificacao.enviar(out strMsg) == true) objIdentificador.atualizaValor();

                    }
                    //==================================================================================//

                    if (e.CommandName == "Reprovar")
                    {
                        //Volta a Escalação Horizontal 
                        if (objNotificacaoAtual.Tabela.Valor.ToLower() == "escalacaohorizontal")
                        {
                            //busca a escalacao anterior e volta.
                            ClsEscalacaoHorizontal objEscalacaoHorizontal = new ClsEscalacaoHorizontal(Convert.ToInt32(objNotificacaoAtual.IdentificadorTabela.Valor));
                            try
                            {
                                ClsEscalacaoHorizontal.voltaEscalacaoHorizontal(objEscalacaoHorizontal.Tabela.Valor, objEscalacaoHorizontal.Identificador.Valor, objEscalacaoHorizontal.Codigo.Valor);
                            }
                            catch { }
                            objEscalacaoHorizontal = null;
                        }
                    }

                    if ((objNotificacaoAtual.Descricao.Valor.ToLower().Substring(0, objNotificacaoAtual.Descricao.Valor.ToLower().Length - (objNotificacaoAtual.Descricao.Valor.ToLower().Length - 2)) == "kb") && (objNotificacao.FlagAprovado.Valor == "S"))
                    {
                        ServiceDesk.Negocio.ClsConhecimentoProcesso objConhecimentoProcesso = new ServiceDesk.Negocio.ClsConhecimentoProcesso();
                        objConhecimentoProcesso.Codigo.Valor = objConhecimentoProcesso.GetMaxId().ToString().Trim();
                        objConhecimentoProcesso.Tabela.Valor = objNotificacaoAtual.Tabela.Valor.Trim();
                        objConhecimentoProcesso.TabelaIdentificador.Valor = objNotificacaoAtual.IdentificadorTabela.Valor.Trim();
                        objConhecimentoProcesso.insere(out strMsg);
                    }

                    objNotificacaoAtual = null;
                }
                catch { }

                Mensagem(false, string.Empty, false);
            }
            //Atualiza grid
            CarregaGridNotificacao();
        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, user.IDusuario.ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region gvNotificacao_RowDataBound
    /// <summary>
    /// RowDataBound
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvNotificacao_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if ((e.Row.RowState.ToString() == "Normal") || (e.Row.RowState.ToString() == "Alternate"))
            {
                TextBox txtStatus = (TextBox)e.Row.FindControl("txtFlgAprov");
                TextBox txtDescricao = (TextBox)e.Row.FindControl("txtDescricao");
                txtDescricao.Text = ServiceDesk.Generica.ClsTexto.trocaHtmlPorAspa(txtDescricao.Text);

                if (txtStatus.Text == "S")
                {
                    e.Row.Cells[6].Visible = true;//Aprova
                    e.Row.Cells[5].Enabled = false;
                    e.Row.Cells[7].Visible = false;//Reprova
                    e.Row.Cells[6].Enabled = false;
                }
                else if (txtStatus.Text == "N")
                {
                    e.Row.Cells[6].Visible = false;
                    e.Row.Cells[5].Enabled = false;
                    e.Row.Cells[7].Visible = true;//Reprova
                    e.Row.Cells[7].Enabled = false;//Reprova
                }
            }

            #region Coloca um link para visualizar o registro que originou a notificao no tipo

            Label lblTipo = (Label)e.Row.FindControl("lblTipo");
            Label lblIdentificadorTabela = (Label)e.Row.FindControl("lblIdentificadorTabela");
            string strCaminho = System.Web.HttpContext.Current.Server.MapPath(".");
            string strLinkRegistro = string.Empty;

            switch (lblTipo.Text.Trim().ToLower())
            {
                case "chamado":
                    {
                        strLinkRegistro = "Javascript:VisualizaChamado(" + lblIdentificadorTabela.Text.Trim() + ");";
                        break;
                    }
                case "incidente":
                    {
                        strLinkRegistro = "Javascript:VisualizaIncidente(" + lblIdentificadorTabela.Text.Trim() + ");";
                        break;
                    }
                case "requisicaoservico":
                    {
                        strLinkRegistro = "Javascript:VisualizaRequisicaoServico(" + lblIdentificadorTabela.Text.Trim() + ");";
                        break;
                    }
                case "requisicaomudanca":
                    {
                        strLinkRegistro = "Javascript:VisualizaRequisicaoMudanca(" + lblIdentificadorTabela.Text.Trim() + ");";
                        break;
                    }
                case "IC":
                    {
                        strLinkRegistro = "Javascript:VisualizarItemConfiguracao(" + lblIdentificadorTabela.Text.Trim() + ");";
                        break;
                    }
                case "escalacaohorizontal":
                    {
                        //Quando uma notificacao é do tipo escalacaohorizontal, o identificador aponta para um
                        //registro dessa tabela. Esse registro guarda a informacao se essa escalação foi para
                        //chamado, RS, RM ou incidente.
                        if (lblIdentificadorTabela.Text.Trim() != string.Empty)
                        {
                            ClsEscalacaoHorizontal objEscalacaoHorizontal = new ClsEscalacaoHorizontal(Convert.ToInt32(lblIdentificadorTabela.Text.Trim()));

                            if (objEscalacaoHorizontal.Tabela.Valor.ToLower() == "chamado")
                                strLinkRegistro = "Javascript:VisualizaChamado(" + objEscalacaoHorizontal.Identificador.Valor + ");";

                            if (objEscalacaoHorizontal.Tabela.Valor.ToLower() == "incidente")
                                strLinkRegistro = "Javascript:VisualizaIncidente(" + objEscalacaoHorizontal.Identificador.Valor + ");";

                            if (objEscalacaoHorizontal.Tabela.Valor.ToLower() == "requisicaoservico")
                                strLinkRegistro = "Javascript:VisualizaRequisicaoServico(" + objEscalacaoHorizontal.Identificador.Valor + ");";

                            if (objEscalacaoHorizontal.Tabela.Valor.ToLower() == "requisicaomudanca")
                                strLinkRegistro = "Javascript:VisualizaRequisicaoMudanca(" + objEscalacaoHorizontal.Identificador.Valor + ");";

                            if (objEscalacaoHorizontal.Tabela.Valor.ToLower() == "IC")
                                strLinkRegistro = "Javascript:VisualizarItemConfiguracao(" + objEscalacaoHorizontal.Identificador.Valor + ");";

                            objEscalacaoHorizontal = null;
                        }
                        break;
                    }
            }

            lblTipo.Text = "<a href='" + strLinkRegistro + "' >" + lblTipo.Text + "</a>";

            #endregion

        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, user.IDusuario.ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region Mensagem
    /// <summary>
    /// Mensagem
    /// </summary>
    /// <param name="bolExibe">True ou false. Se para exibir ou não.</param>
    /// <param name="strMesangem">Descrição da mensagem</param>
    public void Mensagem(bool bolExibe, string strMesangem, bool bolMsgCritica)
    {
        try
        {
            if (bolExibe == true && strMesangem != string.Empty)
            {
                lblMensagem.Text = strMesangem.Trim();
                if (bolMsgCritica == true) imgIcone.ImageUrl = "images/icones/aviso.gif"; else imgIcone.ImageUrl = "images/icones/info.gif";
                lblMensagem.Visible = true;
                divMensagem.Visible = true;
            }
            else
            {
                lblMensagem.Visible = false;
                divMensagem.Visible = false;
            }
        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, user.IDusuario.ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion
}