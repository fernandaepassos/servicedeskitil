/*
- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
  • Web User Control que possibilidade pesquisa das soluções existentes.
  
  	Data: 06/12/2005
  	Autor: Fernanda Passos
  	Descrição: Tela de pesquisa das soluções.
  
  • Alterações
  	Data: 20/02/2006
  	Autor: Fernanda Passos
  	Descrição: Reecriação do evento atualizar registro de solução pois o mesmo havia sido removido.
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

public partial class WUCSolucaoFiltro : System.Web.UI.UserControl
{
    #region Declarações
    ServiceDesk.Negocio.ClsSolucaoProjeto objSolucaoProjeto = new ServiceDesk.Negocio.ClsSolucaoProjeto();
    ServiceDesk.Projeto.ClsProjeto objProjeto = new ServiceDesk.Projeto.ClsProjeto();
    public String strTabela;
    public int intTabelaIdentificador;
    #endregion

    #region Propriedades

    /// <summary>
    /// Nome da tabela que presenta o processo.
    /// </summary>
    public string Tabela
    {
        get { return strTabela; }
        set { this.strTabela  = value; }
    }

    /// <summary>
    /// String identificador na tabela que representa o processo.
    /// </summary>
    public int TabelaIdentificador
    {
        get { return intTabelaIdentificador; }
        set { this.intTabelaIdentificador = value; }
    }
    #endregion

    #region Eventos

    #region VerificaSeTemProcesso
    protected void VerificaSeTemProcesso(object sender, EventArgs e)
    { 
        divMensagem.Visible = false;
        if(txtTabela.Text.Trim () == string .Empty  || txtTabelaIdentificador.Text .Trim () == string.Empty) 
        {
            lblMensagem.Text = "Por favor, informe o processo para o qual deseja atribuir solução.";
            imgIcone.ImageUrl = "images/icones/info.gif";
            divMensagem.Visible = true;
            return;
        }        
    }
    #endregion

    #region Page_Load
    /// <summary>
    /// Page_Load
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }
    #endregion

    #region Solucao_RowCommand
    /// <summary>
    /// Solucao_RowCommand
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvSolucao_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            divMensagem.Visible = false;
            string strMensagem = string.Empty;
            switch (e.CommandName)
            {
                case "Update":
                { 

                    GridViewRow objRow = gvSolucao.Rows[Convert.ToInt32(e.CommandArgument)];

                    if (objRow != null)
                    {
                        //Captura os valores das colunas da grid.
                        Label lblSolucaoProjetoCodigo = (Label)objRow.FindControl("lblSolucaoProjetoCodigo");
                        CheckBox ckbSeraImplementado = (CheckBox)objRow.FindControl("ckbSeraImplementado");
                        DropDownList dlTipo = (DropDownList)objRow.FindControl("dlTipo");
                        TextBox txtMotivoNaoImplemento = (TextBox)objRow.FindControl("txtMotivoNaoImplemento");

                        ///Verifica se foi informado o motivo da não implementação da solução
                        ///Quando a solução não for implementada.
                        if (ckbSeraImplementado.Checked == false && txtMotivoNaoImplemento.Text.Trim() == string.Empty)
                        {
                            lblMensagem.Text = "Por favor, informe o motivo da não implementação da solução.";
                            imgIcone.ImageUrl = "images/icones/info.gif";
                            divMensagem.Visible = true;
                            return;
                        }

                        //Verifica se existem valores retornados da grid.
                        if (lblSolucaoProjetoCodigo != null)
                        {
                            //Preenche a coleção a atributos da entidade.
                            ServiceDesk.Negocio.ClsSolucaoProjeto objSolucaoProjeto = new ClsSolucaoProjeto();
                            objSolucaoProjeto.Codigo.Valor = lblSolucaoProjetoCodigo.Text.Trim();
                            objSolucaoProjeto.Tabela.Valor = txtTabela.Text.Trim();
                            objSolucaoProjeto.CodigoIdentificadorTabela.Valor = txtTabelaIdentificador.Text.Trim();  
                            objSolucaoProjeto.CodigoSolucaoTipo.Valor = dlTipo.SelectedValue.Trim();
                            objSolucaoProjeto.DataAlteracao.Valor = DateTime.Now.ToString(ClsParametro.DataInclusao);
                            objSolucaoProjeto.PessoaCodigoAlteracao.Valor = ClsUsuario.getCodigoUsuario().ToString();
                            if (ckbSeraImplementado.Checked == true) { objSolucaoProjeto.FlagImplementacao.Valor = "S"; } else if (ckbSeraImplementado.Checked == false) { objSolucaoProjeto.FlagImplementacao.Valor = "N";}
                            objSolucaoProjeto.DescricaoNaoImplementacao.Valor = txtMotivoNaoImplemento.Text.Trim();
                            
                            //Realiza a alteração na base de dados.
                            objSolucaoProjeto.altera(out strMensagem);
                            
                            //Apresenta mensagem para o usuário.
                            lblMensagem.Text = strMensagem;
                            imgIcone.ImageUrl = "images/icones/info.gif";
                            divMensagem.Visible = true;

                            objSolucaoProjeto = null;
                                
                            //Atualiza a grid com os dados alterados.
                            gvSolucao.EditIndex = -1;
                            ServiceDesk.Negocio.ClsSolucaoProjeto.geraGridView(gvSolucao, txtTabela.Text.Trim(), Convert.ToInt32(txtTabelaIdentificador.Text.Trim()));
                        }
                    }
                    break;
                }
                case "Exibir":
                    {
                        GridViewRow objRow = this.gvSolucao.Rows[Convert.ToInt32(e.CommandArgument)];

                        if (objRow != null)
                        {
                            Label lblCodigo = (Label)objRow.FindControl("lblCodigo");
                        }
                        objRow = null;

                        break;
                    }
                case "Excluir":
                    {
                        GridViewRow objRow = gvSolucao.Rows[Convert.ToInt32(e.CommandArgument)];

                        if (objRow != null)
                        {
                            Label lblSolucaoProjetoCodigo = (Label)objRow.FindControl("lblSolucaoProjetoCodigo");
                            Label lblCodigo = (Label)objRow.FindControl("lblCodigo");

                            if (lblSolucaoProjetoCodigo != null && lblCodigo != null)
                            {
                                objSolucaoProjeto.Codigo.Valor = lblSolucaoProjetoCodigo.Text.Trim();
                                objSolucaoProjeto.exclui(out strMensagem);
                                objSolucaoProjeto = null;

                                objProjeto.Codigo.Valor = lblCodigo.Text.Trim();
                                objProjeto.exclui(out strMensagem);
                                objProjeto = null;

                                ServiceDesk.Negocio.ClsSolucaoProjeto.geraGridView(gvSolucao, txtTabela.Text.Trim(), Convert.ToInt32(txtTabelaIdentificador.Text.Trim()));
                                Filtrar();

                            }
                        }
                        objRow = null;
                        break;
                    }
            }
        }
        catch (Exception ex)
        {
                ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());

        }
    }
    #endregion

    #region Filtrar
    /// <summary>
    /// Filtrar
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnFiltrar_Click(object sender, EventArgs e)
    {
        try
        {
            Filtrar();
        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }

    #endregion

    #region Preenche campo
    /// <summary>
    /// PreencheCampo
    /// </summary>
    /// <param name="strTabela"></param>
    /// <param name="intTabelaIdentificador"></param>
    public void PreencheCampo(string strTabela, int intTabelaIdentificador)
    {
        try
        {
            if (strTabela == "Chamado")
            {
                string strMensagem = string.Empty;
                if (ClsChamado.PossuiIncidenteVinculado(intTabelaIdentificador.ToString()) == true) ClsSolucaoProjeto.VinculaSolucaoExistente(intTabelaIdentificador, "Incidente", out strMensagem);
                if (ClsChamado.PossuiRequisicaoMudancaVinculado(intTabelaIdentificador.ToString()) == true) ClsSolucaoProjeto.VinculaSolucaoExistente(intTabelaIdentificador, "RequisicaoMudanca", out strMensagem);
                if (ClsChamado.PossuiRequisicaoServicoVinculado(intTabelaIdentificador.ToString()) == true) ClsSolucaoProjeto.VinculaSolucaoExistente(intTabelaIdentificador, "RequisicaoServico", out strMensagem);
                
                this.txtFiltroDescricao.Visible = false;
                this.btnAssociar.Visible = false;
                this.btnFiltrar.Visible = false;
                this.btnNovaSolucao.Visible = false;
                this.lblFiltroPorDescricao.Visible = false;
            }

            txtTabela.Text = strTabela;
            txtTabelaIdentificador.Text = intTabelaIdentificador.ToString();
            btnNovaSolucao.NavigateUrl = "Javascript:VisualizaProjetoNovo('" + txtTabela.Text.Trim() + "','" + txtTabelaIdentificador.Text.Trim() + "');"; ;
        }
        catch (Exception ex)
        {
                ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region Salvar - Associar
    /// <summary>
    /// Salvar - Associar
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAssociar_Click(object sender, EventArgs e)
    {
        try
        {
            divMensagem.Visible = false;
            if(txtTabela.Text .Trim () == string .Empty  || txtTabelaIdentificador.Text .Trim () == string.Empty) 
            {
                lblMensagem.Text = "Por favor, informe o processo para o qual deseja atribuir solução.";
                imgIcone.ImageUrl = "images/icones/info.gif";
                divMensagem.Visible = true;
                return;
            }

            divMensagem.Visible = false;
            Salvar();
            Filtrar();
        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region Solucao - RowDataBound
    /// <summary>
    /// Solucao - RowDataBound
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvSolucao_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            String strCodigoAuditoria = String.Empty;
            if ((e.Row.RowState == DataControlRowState.Normal) || (e.Row.RowState == DataControlRowState.Alternate))
            {
                try
                {
                    // Adiciona um evento javascript no botão Excluir
                    ImageButton btnExcluir = (ImageButton)e.Row.Cells[7].Controls[0];
                    btnExcluir.Attributes.Add("onclick", "return verifica();");

                    Label lblTipo = (Label)e.Row.FindControl("lblTipo");
                    Label lblCodigo = (Label)e.Row.FindControl("lblCodigo");
                    Label lblSeraImplementado = (Label)e.Row.FindControl("lblSeraImplementado");
                    CheckBox ckbSeraImplementado = (CheckBox)e.Row.FindControl("ckbSeraImplementado");

                    //Preenche o nome do tipo de solução.
                    if (lblTipo != null) lblTipo.Text = ServiceDesk.Negocio.ClsSolucaoProjetoTipo.GetNameTipoSolucao(Convert.ToInt32(lblTipo.Text));

                    //Preenche se será implementado ou não.
                    if (lblSeraImplementado != null && lblSeraImplementado.Text.Trim() != string.Empty)
                    {
                        if (lblSeraImplementado.Text.Trim() == "S") ckbSeraImplementado.Checked = true; else ckbSeraImplementado.Checked = false;
                    }
                }
                catch
                {
                }
            }
            else
            {
                Label lblTipo = (Label)e.Row.FindControl("lblTipo");
                DropDownList dlTipo = (DropDownList)e.Row.FindControl("dlTipo");
                if (dlTipo != null)
                {
                    ServiceDesk.Negocio.ClsSolucaoProjetoTipo.geraDropDownList(dlTipo, "Selecione o tipo");
                    dlTipo.ForeColor = System.Drawing.Color.Black;
                    dlTipo.Font.Bold = false;
                    if (lblTipo != null && lblTipo.Text != string.Empty) { dlTipo.SelectedValue = lblTipo.Text.Trim();}
                }
            }

        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region Solucao - RowCancelingEdit
    /// <summary>
    /// Solucao - RowCancelingEdit
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvSolucao_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        try
        {
            divMensagem.Visible = false;

            GridView objGridView = (GridView)sender;
            objGridView.EditIndex = -1;
            ServiceDesk.Negocio.ClsSolucaoProjeto.geraGridView(objGridView, txtTabela.Text.Trim(), Convert.ToInt32(txtTabelaIdentificador.Text.Trim()));
            objGridView = null;
        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region Solução - RowEditing
    /// <summary>
    /// Solução - RowEditing
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvSolucao_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            divMensagem.Visible = false;
            GridView objGridView = (GridView)sender;
            objGridView.EditIndex = e.NewEditIndex;
            //Filtrar();
            ServiceDesk.Negocio.ClsSolucaoProjeto.geraGridView(objGridView, txtTabela.Text.Trim(), Convert.ToInt32(txtTabelaIdentificador.Text.Trim()));
            objGridView = null;
        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region gvSolucao_RowUpdating
    /// <summary>
    /// gvSolucao_RowUpdating
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvSolucao_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

    }
    #endregion
    #endregion

    #region Métodos

    #region Salvar
    /// <summary>
    /// Salvar
    /// </summary>
    public void Salvar()
    {
        try
        {
            int intContador = 0;

            for (intContador = 0; intContador < gvSolucaoNaoSelecionadas.Rows.Count; intContador++)
            {
                string strMensagem = string.Empty;
                GridViewRow objRow = (GridViewRow)gvSolucaoNaoSelecionadas.Rows[intContador];

                if (objRow != null)
                {
                    CheckBox ckbAssociaNao = (CheckBox)objRow.FindControl("ckbAssociaNao");
                    Label lblCodigo = (Label)objRow.FindControl("lblCodigo");

                    if (ckbAssociaNao.Checked == true)
                    {
                        objSolucaoProjeto.Codigo.Valor = objSolucaoProjeto.GetMaxId().ToString();
                        objSolucaoProjeto.Tabela.Valor = txtTabela.Text.Trim();
                        objSolucaoProjeto.CodigoIdentificadorTabela.Valor = Convert.ToInt32(txtTabelaIdentificador.Text.Trim()).ToString();
                        
                        //Copia to projeto
                        ServiceDesk.Projeto.ClsProjeto objProjeto = new ServiceDesk.Projeto.ClsProjeto(Convert.ToInt32(lblCodigo.Text.Trim()));
                        objSolucaoProjeto.CodigoProjeto.Valor = objProjeto.CopiaProjeto(0, objProjeto, out strMensagem).ToString().Trim();
                        objProjeto = null;

                        objSolucaoProjeto.insere(out strMensagem);
                    }
                }
                objRow = null;
            }
        }
        catch (Exception ex)
        {
            lblMensagem.Text = ex.Message;
            imgIcone.ImageUrl = "images/icones/aviso.gif";
            divMensagem.Visible = true;

            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region Excluir
    /// <summary>
    /// Excluir
    /// </summary>
    public void Excluir()
    {
        try
        { }
        catch (System.Exception ex)
        {
            lblMensagem.Text = ex.Message;
            imgIcone.ImageUrl = "images/icones/aviso.gif";
            divMensagem.Visible = true;
        }
    }
    #endregion

    #region Filtrar
    /// <summary>
    /// Filtrar
    /// </summary>
    public void Filtrar()
    {
        try
        {
            if (txtTabelaIdentificador.Text.Trim() != string.Empty && txtTabela.Text.Trim() != string.Empty) { PreencheGrid(Convert.ToInt32(txtTabelaIdentificador.Text.Trim()), txtTabela.Text.Trim()); }
        }
        catch (Exception ex)
        {
            lblMensagem.Text = ex.Message;
            imgIcone.ImageUrl = "images/icones/aviso.gif";
            divMensagem.Visible = true;

            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region PreencheGrid
    /// <summary>
    /// PreencheGrid
    /// </summary>
    public void PreencheGrid(int intTabelaIdentificador, string strTabela)
    {
        try
        {
            divMensagem.Visible = false;
            ServiceDesk.Negocio.ClsSolucaoProjeto.geraGridView(gvSolucao, strTabela, intTabelaIdentificador);
            if (txtFiltroDescricao.Text.Trim() != string.Empty)
                ServiceDesk.Negocio.ClsSolucaoProjeto.geraGridView(gvSolucaoNaoSelecionadas, txtFiltroDescricao.Text, strTabela, intTabelaIdentificador);
        }
        catch (Exception ex)
        {
            lblMensagem.Text = ex.Message;
            imgIcone.ImageUrl = "images/icones/aviso.gif";
            divMensagem.Visible = true;

            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #endregion
}
