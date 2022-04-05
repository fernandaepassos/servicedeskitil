using System;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ServiceDesk.Negocio;

public partial class Auditoria : BasePage
{
    #region Evento Page_Load
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            CheckAcesso(9);

            int intCodigoAuditoria = 0;

            //Esconde a mensagem de erro
            lblMensagem.Visible = false;
            divMensagem.Visible = false;

            if (!Page.IsPostBack)
            {
                //Populando os status da Auditoria
                ServiceDesk.Negocio.ClsAuditoria.geraDropDownListStatus(ddlStatus);

                if (Request.QueryString["codigo"] != null)
                {
                    intCodigoAuditoria = Convert.ToInt32(Request.QueryString["codigo"].ToString().Trim());

                    //Monta os dados na tela
                    montaDadosAuditoria(intCodigoAuditoria);

                    //Monta a TreeView dos Tipos de Atributos
                    ServiceDesk.Negocio.ClsItemConfiguracaoTipo.populaNoRaiz(0, trvItemConfiguracaoTipo, null, "auditoria.aspx?codigo=" + intCodigoAuditoria.ToString() + "&");

                    //Monta a GridView
                    if (Request.QueryString["tipo"] != null)
                    {
                        ServiceDesk.Negocio.ClsItemConfiguracao.geraGridViewAuditado(gvItemConfiguracao, Convert.ToInt32(Request.QueryString["tipo"].ToString()));
                    }
                }

            }

            if (Session["strStatus"] != null)
            {
                //Exibindo mensagem com o status da operação
                lblMensagem.Text = Session["strStatus"].ToString();
                imgIcone.ImageUrl = "images/icones/aviso.gif";
                lblMensagem.Visible = true;
                divMensagem.Visible = true;
            }

            Session["strStatus"] = null;
        }
        catch (Exception ex)
        {
            //Grava Log de Erro
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, user.IDusuario.ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region metodo montaDadosAuditoria
    /// <summary>
    /// Método que popula os dados da tela de Auditoria
    /// </summary>
    /// <param name="intCodigoItem">Código da Auditoria</param>
    private void montaDadosAuditoria(int intCodigoAuditoria)
    {
        try
        {
            ServiceDesk.Negocio.ClsAuditoria objAuditoria = new ServiceDesk.Negocio.ClsAuditoria(intCodigoAuditoria);
            txtNome.Text = objAuditoria.Nome.Valor;
            txtComentario.Text = objAuditoria.Comentario.Valor;
            if (objAuditoria.DataInicialPrevista.Valor != String.Empty)
            {
                clpDataInicialPrevista.SelectedDate = Convert.ToDateTime(objAuditoria.DataInicialPrevista.Valor);
            }
            if (objAuditoria.DataInicialReal.Valor != String.Empty)
            {
                clpDataInicialReal.SelectedDate = Convert.ToDateTime(objAuditoria.DataInicialReal.Valor);
            }
            if (objAuditoria.DataFinalPrevista.Valor != String.Empty)
            {
                clpDataFinalPrevista.SelectedDate = Convert.ToDateTime(objAuditoria.DataFinalPrevista.Valor);
            }
            if (objAuditoria.DataFinalReal.Valor != String.Empty)
            {
                clpDataFinalReal.SelectedDate = Convert.ToDateTime(objAuditoria.DataFinalReal.Valor);
            }
            ddlStatus.SelectedValue = null;
            try
            {
                ddlStatus.Items.FindByValue(objAuditoria.Status.Valor).Selected = true;
            }
            catch
            {
            }
            objAuditoria = null;
        }
        catch (Exception ex)
        {
            //Grava Log de Erro
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, user.IDusuario.ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region Evento salvaAuditoria
    /// <summary>
    /// Método que salva a Auditoria (inserir/alterar)
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void salvaAuditoria(object sender, EventArgs e)
    {
        try
        {
            if (Request.QueryString["codigo"] == null)
            {
                insereAuditoria();
            }
            else
            {
                if (Request.QueryString["codigo"] != null)
                {
                    alteraAuditoria(Convert.ToInt32(Request.QueryString["codigo"].ToString().Trim()));
                }
            }
        }
        catch (Exception ex)
        {
            //Grava Log de Erro
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, user.IDusuario.ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region metodo insereAuditoria
    /// <summary>
    /// Método que insere uma nova Auditoria
    /// </summary>
    private void insereAuditoria()
    {
        try
        {
            String strMensagem = String.Empty;

            ServiceDesk.Negocio.ClsAuditoria objAuditoria = new ServiceDesk.Negocio.ClsAuditoria();
            ServiceDesk.Negocio.ClsIdentificador objIdentificador = new ServiceDesk.Negocio.ClsIdentificador();

            objIdentificador.Tabela.Valor = objAuditoria.Atributos.NomeTabela;
            objAuditoria.Codigo.Valor = objIdentificador.getProximoValor().ToString();
            objAuditoria.Nome.Valor = ServiceDesk.Generica.ClsTexto.trocaAspaPorHtml(txtNome.Text);
            objAuditoria.Status.Valor = ddlStatus.SelectedValue;
            objAuditoria.Comentario.Valor = ServiceDesk.Generica.ClsTexto.trocaAspaPorHtml(txtComentario.Text);
            if (clpDataInicialPrevista.SelectedDate != DateTime.MinValue)
            {
                objAuditoria.DataInicialPrevista.Valor = clpDataInicialPrevista.SelectedDate.ToString(ClsParametro.DataInclusao);
            }
            if (clpDataInicialReal.SelectedDate != DateTime.MinValue)
            {
                objAuditoria.DataInicialReal.Valor = clpDataInicialReal.SelectedDate.ToString(ClsParametro.DataInclusao);
            }
            if (clpDataFinalPrevista.SelectedDate != DateTime.MinValue)
            {
                objAuditoria.DataFinalPrevista.Valor = clpDataFinalPrevista.SelectedDate.ToString(ClsParametro.DataInclusao);
            }
            if (clpDataFinalReal.SelectedDate != DateTime.MinValue)
            {
                objAuditoria.DataFinalReal.Valor = clpDataFinalReal.SelectedDate.ToString(ClsParametro.DataInclusao);
            }
            objAuditoria.DataInclusao.Valor = DateTime.Now.ToString(ClsParametro.DataInclusao);
            objAuditoria.PessoaInclusao.Valor = user.IDusuario.ToString();

            if (objAuditoria.insere(out strMensagem))
            {
                //Atualizando o valor na tabela identificador
                objIdentificador.atualizaValor();

                //Informando a mensagem do status da operação
                Session["strStatus"] = "Auditoria inserida com sucesso.";

                Response.Redirect("auditoria.aspx?codigo=" + objAuditoria.Codigo.Valor);

            }
            else
            {
                lblMensagem.Text = strMensagem;
                imgIcone.ImageUrl = "images/icones/aviso.gif";
                lblMensagem.Visible = true;
                divMensagem.Visible = true;
            }

            objAuditoria = null;
            objIdentificador = null;
        }
        catch (Exception ex)
        {
            //Grava Log de Erro
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, user.IDusuario.ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region metodo alteraAuditoria
    /// <summary>
    /// Método que altera a Auditoria
    /// </summary>
    /// <param name="intCodigo"></param>
    private void alteraAuditoria(int intCodigoAuditoria)
    {
        try
        {
            String strMensagem = String.Empty;

            ServiceDesk.Negocio.ClsAuditoria objAuditoria = new ServiceDesk.Negocio.ClsAuditoria(intCodigoAuditoria);

            objAuditoria.Nome.Valor = ServiceDesk.Generica.ClsTexto.trocaAspaPorHtml(txtNome.Text);
            objAuditoria.Status.Valor = ddlStatus.SelectedValue;
            objAuditoria.Comentario.Valor = ServiceDesk.Generica.ClsTexto.trocaAspaPorHtml(txtComentario.Text);
            if (clpDataInicialPrevista.SelectedDate != DateTime.MinValue)
            {
                objAuditoria.DataInicialPrevista.Valor = clpDataInicialPrevista.SelectedDate.ToString(ClsParametro.DataInclusao);
            }
            if (clpDataInicialReal.SelectedDate != DateTime.MinValue)
            {
                objAuditoria.DataInicialReal.Valor = clpDataInicialReal.SelectedDate.ToString(ClsParametro.DataInclusao);
            }
            if (clpDataFinalPrevista.SelectedDate != DateTime.MinValue)
            {
                objAuditoria.DataFinalPrevista.Valor = clpDataFinalPrevista.SelectedDate.ToString(ClsParametro.DataInclusao);
            }
            if (clpDataFinalReal.SelectedDate != DateTime.MinValue)
            {
                objAuditoria.DataFinalReal.Valor = clpDataFinalReal.SelectedDate.ToString(ClsParametro.DataInclusao);
            }
            if (objAuditoria.DataInclusao.Valor != String.Empty)
            {
                objAuditoria.DataInclusao.Valor = Convert.ToDateTime(objAuditoria.DataInclusao.Valor).ToString(ClsParametro.DataInclusao);
            }

            objAuditoria.altera(out strMensagem);

            lblMensagem.Text = strMensagem;
            imgIcone.ImageUrl = "images/icones/aviso.gif";
            lblMensagem.Visible = true;
            divMensagem.Visible = true;

            objAuditoria = null;
        }
        catch (Exception ex)
        {
            //Grava Log de Erro
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, user.IDusuario.ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region Evento mudaAba
    /// <summary>
    /// Evento do clique do Botao da aba atributo
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void mudaAba(object sender, EventArgs e)
    {
        try
        {
            int intAbaSelecionada = 0;
            int intCodigoAuditoria = 0;

            if (Request.QueryString["codigo"] != null)
            {
                intCodigoAuditoria = Convert.ToInt32(Request.QueryString["codigo"].ToString().Trim());
            }

            switch (sender.GetType().ToString())
            {
                case "System.Web.UI.WebControls.Button":
                    {
                        Button btnAba = (Button)sender;
                        intAbaSelecionada = Convert.ToInt32(btnAba.CommandArgument);
                        mtvAuditoria.ActiveViewIndex = intAbaSelecionada;
                        btnAba = null;
                        break;
                    }
                case "System.Web.UI.WebControls.LinkButton":
                    {
                        LinkButton lkbAba = (LinkButton)sender;
                        intAbaSelecionada = Convert.ToInt32(lkbAba.CommandArgument);
                        mtvAuditoria.ActiveViewIndex = intAbaSelecionada;
                        lkbAba = null;
                        break;
                    }
            }

            //Verificando qual aba foi escolhida
            switch (intAbaSelecionada)
            {
                case 0:
                    {
                        break;
                    }
                case 1:
                    {
                        pesquisarPessoaPorNome(sender, e);
                        break;
                    }
                case 2:
                    {
                        SServiceDesk.Negocio.ClsAnexo objAnexo = new SServiceDesk.Negocio.ClsAnexo();
                        objAnexo.Codigo.CampoIdentificador = false;
                        objAnexo.Tabela.Valor = "auditoria";
                        objAnexo.Tabela.CampoIdentificador = true;
                        objAnexo.TabelaIdentificador.Valor = intCodigoAuditoria.ToString();
                        objAnexo.TabelaIdentificador.CampoIdentificador = true;
                        SServiceDesk.Negocio.ClsAnexo.geraGridView(gvDocumento, objAnexo, true);
                        objAnexo = null;
                        break;
                    }
            }
        }
        catch (Exception ex)
        {
            //Grava Log de Erro
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, user.IDusuario.ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region Evento populaSubNivel
    /// <summary>
    /// Evento que ocorre quando um no da tree view é criado
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void populaSubNivel(object sender, TreeNodeEventArgs e)
    {
        try
        {
            ServiceDesk.Negocio.ClsItemConfiguracaoTipo.populaNoRaiz(Convert.ToInt32(e.Node.Value), null, e.Node, "auditoria.aspx?codigo=" + Request.QueryString["codigo"].ToString().Trim() + "&");
        }
        catch (Exception ex)
        {
            //Grava Log de Erro
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, user.IDusuario.ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region Evento gvItemConfiguracao_OnRowCommand
    /// <summary>
    /// Evento que ocorre quando um comando da GridView é solicitado
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvItemConfiguracao_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {

        switch (e.CommandName)
        {
            case "Qualquer":
                {
                    break;
                }
        }
    }
    #endregion

    #region Evento editarGrid
    /// <summary>
    /// Evento que ocorre quando clicado no botão editar
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void editarGrid(object sender, GridViewEditEventArgs e)
    {
        try
        {
            GridView objGridView = (GridView)sender;
            objGridView.EditIndex = e.NewEditIndex;
            ServiceDesk.Negocio.ClsItemConfiguracao.geraGridViewAuditado(objGridView, Convert.ToInt32(Request.QueryString["tipo"].ToString()));
            objGridView = null;
        }
        catch (Exception ex)
        {
            //Grava Log de Erro
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, user.IDusuario.ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region Evento cancelarEdicaoGrid
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void cancelarEdicaoGrid(object sender, GridViewCancelEditEventArgs e)
    {
        try
        {
            GridView objGridView = (GridView)sender;
            objGridView.EditIndex = -1;
            ServiceDesk.Negocio.ClsItemConfiguracao.geraGridViewAuditado(objGridView, Convert.ToInt32(Request.QueryString["tipo"].ToString()));
            objGridView = null;
        }
        catch (Exception ex)
        {
            //Grava Log de Erro
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, user.IDusuario.ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region Evento atualizaGrid
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void atualizaGrid(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            String strMensagem = String.Empty;
            GridView objGridView = (GridView)sender;
            GridViewRow objRow = objGridView.Rows[e.RowIndex];

            if (objRow != null)
            {
                Label lblCodigo = (Label)objRow.FindControl("lblCodigo");
                String strCodigoAuditoria = String.Empty;
                if (Request.QueryString["codigo"] != null)
                {
                    strCodigoAuditoria = Request.QueryString["codigo"].ToString().Trim();
                }
                Label lblCodigoItem = (Label)objRow.FindControl("lblCodigoItem");
                DropDownList ddlAuditor = (DropDownList)objRow.FindControl("ddlAuditor");
                DropDownList ddlStatus = (DropDownList)objRow.FindControl("ddlStatus");
                TextBox txtComentario = (TextBox)objRow.FindControl("txtComentario");

                ServiceDesk.Negocio.ClsAuditado objAuditado = new ServiceDesk.Negocio.ClsAuditado();

                if (lblCodigo.Text != String.Empty)
                {
                    //Altera o Auditado
                    objAuditado = new ServiceDesk.Negocio.ClsAuditado(Convert.ToInt32(lblCodigo.Text));
                    objAuditado.Auditor.Valor = ddlAuditor.SelectedValue;
                    objAuditado.Status.Valor = ddlStatus.SelectedValue;
                    objAuditado.Comentario.Valor = ServiceDesk.Generica.ClsTexto.trocaAspaPorHtml(txtComentario.Text);
                    if (objAuditado.DataInclusao.Valor != String.Empty)
                    {
                        objAuditado.DataInclusao.Valor = Convert.ToDateTime(objAuditado.DataInclusao.Valor).ToString(ClsParametro.DataInclusao);
                    }

                    objAuditado.altera(out strMensagem);

                }
                else
                {
                    //Insere novo auditado
                    ServiceDesk.Negocio.ClsIdentificador objIdentificador = new ServiceDesk.Negocio.ClsIdentificador();

                    objIdentificador.Tabela.Valor = objAuditado.Atributos.NomeTabela;
                    objAuditado.Codigo.Valor = objIdentificador.getProximoValor().ToString();
                    objAuditado.Auditoria.Valor = strCodigoAuditoria;
                    objAuditado.TabelaIdentificador.Valor = lblCodigoItem.Text;
                    objAuditado.Auditor.Valor = ddlAuditor.SelectedValue;
                    objAuditado.Status.Valor = ddlStatus.SelectedValue;
                    objAuditado.Comentario.Valor = ServiceDesk.Generica.ClsTexto.trocaAspaPorHtml(txtComentario.Text);
                    objAuditado.PessoaInclusao.Valor = user.IDusuario.ToString();
                    objAuditado.DataInclusao.Valor = DateTime.Now.ToString(ClsParametro.DataInclusao);

                    if (objAuditado.insere(out strMensagem))
                    {
                        objIdentificador.atualizaValor();
                    }

                }

                lblMensagem.Text = strMensagem;
                imgIcone.ImageUrl = "images/icones/aviso.gif";
                lblMensagem.Visible = true;
                divMensagem.Visible = true;

                objAuditado = null;

            }

            objRow = null;
            objGridView = null;
        }
        catch (Exception ex)
        {
            //Grava Log de Erro
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, user.IDusuario.ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region Evento gvItemConfiguracao_PageIndexChanging
    /// <summary>
    /// Evento que ocorre quando é mudada a página da GridView
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvItemConfiguracao_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvItemConfiguracao.PageIndex = e.NewPageIndex;
            if (Request.QueryString["tipo"] != null)
            {
                ServiceDesk.Negocio.ClsItemConfiguracao.geraGridViewAuditado(gvItemConfiguracao, Convert.ToInt32(Request.QueryString["tipo"].ToString()));
            }
        }
        catch (Exception ex)
        {
            //Grava Log de Erro
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, user.IDusuario.ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region Evento gvItemConfiguracao_RowDataBound
    /// <summary>
    /// Evento que ocorre na montagem da GridView
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvItemConfiguracao_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            String strCodigoAuditoria = String.Empty;

            if ((e.Row.RowState == DataControlRowState.Normal) || (e.Row.RowState == DataControlRowState.Alternate))
            {
                try
                {
                    Label lblCodigo = (Label)e.Row.FindControl("lblCodigo");
                    Label lblCodigoItem = (Label)e.Row.FindControl("lblCodigoItem");
                    Label lblCodigoTipo = (Label)e.Row.FindControl("lblCodigoTipo");
                    Label lblPrefixoNumero = (Label)e.Row.FindControl("lblPrefixoNumero");
                    Label lblStatus = (Label)e.Row.FindControl("lblStatus");
                    Label lblAuditor = (Label)e.Row.FindControl("lblAuditor");

                    ServiceDesk.Negocio.ClsItemConfiguracao objItemConfiguracao = new ServiceDesk.Negocio.ClsItemConfiguracao(Convert.ToInt32(lblCodigoItem.Text));

                    ServiceDesk.Negocio.ClsItemConfiguracaoTipo objItemConfiguracaoTipo = new ServiceDesk.Negocio.ClsItemConfiguracaoTipo(Convert.ToInt32(lblCodigoTipo.Text));
                    lblPrefixoNumero.Text = objItemConfiguracaoTipo.Prefixo.Valor + objItemConfiguracao.Numero.Valor;
                    objItemConfiguracaoTipo = null;

                    //Mostrando a Descricao do Status
                    if (lblStatus.Text != String.Empty)
                    {
                        ServiceDesk.Negocio.ClsStatus objStatus = new ServiceDesk.Negocio.ClsStatus(Convert.ToInt32(lblStatus.Text));
                        lblStatus.Text = objStatus.Descricao.Valor;
                        objStatus = null;
                    }

                    //Mostrando o Nome do Auditor
                    if (lblAuditor.Text != String.Empty)
                    {
                        SServiceDesk.Negocio.ClsPessoa objPessoa = new SServiceDesk.Negocio.ClsPessoa(Convert.ToInt32(lblAuditor.Text));
                        lblAuditor.Text = objPessoa.Nome.Valor;
                        objPessoa = null;
                    }

                }
                catch
                {
                }
            }
            else
            {
                Label lblCodigo = (Label)e.Row.FindControl("lblCodigo");
                Label lblCodigoItem = (Label)e.Row.FindControl("lblCodigoItem");
                Label lblCodigoTipo = (Label)e.Row.FindControl("lblCodigoTipo");
                Label lblPrefixoNumero = (Label)e.Row.FindControl("lblPrefixoNumero");
                Label lblStatus = (Label)e.Row.FindControl("lblStatus");
                DropDownList ddlStatus = (DropDownList)e.Row.FindControl("ddlStatus");
                Label lblAuditor = (Label)e.Row.FindControl("lblAuditor");
                DropDownList ddlAuditor = (DropDownList)e.Row.FindControl("ddlAuditor");

                if (Request.QueryString["codigo"] != null)
                {
                    strCodigoAuditoria = Request.QueryString["codigo"].ToString().Trim();
                }

                ServiceDesk.Negocio.ClsItemConfiguracao objItemConfiguracao = new ServiceDesk.Negocio.ClsItemConfiguracao(Convert.ToInt32(lblCodigoItem.Text));

                ServiceDesk.Negocio.ClsItemConfiguracaoTipo objItemConfiguracaoTipo = new ServiceDesk.Negocio.ClsItemConfiguracaoTipo(Convert.ToInt32(lblCodigoTipo.Text));
                lblPrefixoNumero.Text = objItemConfiguracaoTipo.Prefixo.Valor + objItemConfiguracao.Numero.Valor;
                objItemConfiguracaoTipo = null;

                objItemConfiguracao = null;

                ServiceDesk.Negocio.ClsStatusTabela.geraDropDownList(ddlStatus, "auditado");
                if (lblStatus.Text != String.Empty)
                {
                    ddlStatus.SelectedValue = lblStatus.Text.Trim();
                }

                if (strCodigoAuditoria != String.Empty)
                {
                    ServiceDesk.Negocio.ClsAuditoria.geraDropDownListAuditor(ddlAuditor, Convert.ToInt32(strCodigoAuditoria));
                    if (lblAuditor.Text != String.Empty)
                    {
                        ddlAuditor.SelectedValue = lblAuditor.Text.Trim();
                    }
                }

            }
        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, user.IDusuario.ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region Evento pesquisarPessoaPorNome
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void pesquisarPessoaPorNome(object sender, EventArgs e)
    {
        SServiceDesk.Negocio.ClsPessoa.geraGridViewBusca(gvAuditor, ServiceDesk.Generica.ClsTexto.trocaAspaPorHtml(txtPessoaNomeBusca.Text), String.Empty, 0, 0);
    }
    #endregion

    #region Evento gvAuditor_OnRowCommand
    /// <summary>
    /// Evento que ocorre quando um comando da GridView é solicitado
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvAuditor_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
    }
    #endregion

    #region Evento gvAuditor_PageIndexChanging
    /// <summary>
    /// Evento que ocorre quando é mudada a página da GridView
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvAuditor_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView objGridView = (GridView)sender;
        objGridView.PageIndex = e.NewPageIndex;
        pesquisarPessoaPorNome(sender, e);
        objGridView = null;
    }
    #endregion

    #region Evento gvAuditor_RowDataBound
    /// <summary>
    /// Evento que ocorre na montagem da GridView
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvAuditor_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            int intCodigoAuditoria = 0;
            int intCodigoAuditor = 0;

            if (Request.QueryString["codigo"] != null)
            {
                intCodigoAuditoria = Convert.ToInt32(Request.QueryString["codigo"].ToString().Trim());
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                try
                {
                    CheckBox chbCodigo = (CheckBox)e.Row.FindControl("chbCodigo");
                    Label lblCodigo = (Label)e.Row.FindControl("lblCodigo");

                    if (lblCodigo.Text != String.Empty)
                    {
                        intCodigoAuditor = ServiceDesk.Negocio.ClsAuditoria.retornaItemPessoaTipo(intCodigoAuditoria, Convert.ToInt32(lblCodigo.Text));
                    }

                    if (intCodigoAuditor > 0)
                    {
                        chbCodigo.Checked = true;
                    }

                }
                catch
                {
                }
            }
        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, user.IDusuario.ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region Evento gvDocumento_OnRowCommand
    /// <summary>
    /// Evento que ocorre quando um comando da GridView é solicitado
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvDocumento_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            GridView objGridView = (GridView)sender;
            String strMensagem = String.Empty;

            if (e.CommandName == "Excluir")
            {
                Label lblCodigoAnexo = (Label)objGridView.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("lblCodigo");
                SServiceDesk.Negocio.ClsAnexo objAnexo = new SServiceDesk.Negocio.ClsAnexo(Convert.ToInt32(lblCodigoAnexo.Text));
                if (ServiceDesk.Generica.ClsArquivo.apagaArquivo(objAnexo.Caminho.Valor))
                {
                    if (objAnexo.exclui())
                    {
                        strMensagem = "Arquivo excluído com sucesso.";
                        mudaAba(lkbDocumento, e);
                    }
                }
                else
                {
                    strMensagem = "Não foi possível excluir o documento. Entre em contato com o suporte.";
                }

                lblMensagem.Text = strMensagem;
                imgIcone.ImageUrl = "images/icones/aviso.gif";
                lblMensagem.Visible = true;
                divMensagem.Visible = true;

                objAnexo = null;
                lblCodigoAnexo = null;
            }

            objGridView = null;
        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, user.IDusuario.ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region Evento gvDocumento_PageIndexChanging
    /// <summary>
    /// Evento que ocorre quando é mudada a página da GridView
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvDocumento_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView objGridView = (GridView)sender;
        objGridView.PageIndex = e.NewPageIndex;
        objGridView = null;
    }
    #endregion

    #region Evento gvDocumento_RowDataBound
    /// <summary>
    /// Evento que ocorre na montagem da GridView
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvDocumento_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            int intCodigoAuditoria = 0;

            if (Request.QueryString["codigo"] != null)
            {
                intCodigoAuditoria = Convert.ToInt32(Request.QueryString["codigo"].ToString().Trim());
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                try
                {
                    Label lblCodigo = (Label)e.Row.FindControl("lblCodigo");
                    Label lblCaminho = (Label)e.Row.FindControl("lblCaminho");
                    Label lblArquivo = (Label)e.Row.FindControl("lblArquivo");
                    TextBox txtNome = (TextBox)e.Row.FindControl("txtNome");

                    String strArquivo = String.Empty;

                    strArquivo = ServiceDesk.Generica.ClsTexto.getParteFinalPorChave(lblCaminho.Text, "\\");

                    lblArquivo.Text = "<a href=\"docs\\" + strArquivo + "\" target=\"_blank\">" + strArquivo + "</a>";

                    ImageButton btnExcluir = (ImageButton)e.Row.Cells[3].Controls[0];
                    btnExcluir.Attributes.Add("onclick", "return verifica('o Documento');");

                }
                catch
                {
                }
            }
        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, user.IDusuario.ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region metodo salvaAuditor
    /// <summary>
    /// Evento que salva o relacionamento entre pessoa x auditoria (auditor)
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void salvaAuditor(object sender, EventArgs e)
    {
        try
        {
            int intCodigoAuditoria = 0;
            int intContador = 0;

            if (Request.QueryString["codigo"] != null)
            {
                intCodigoAuditoria = Convert.ToInt32(Request.QueryString["codigo"].ToString().Trim());
            }

            ServiceDesk.Negocio.ClsAuditoria.excluiAuditor(intCodigoAuditoria);

            for (intContador = 0; intContador < gvAuditor.Rows.Count; intContador++)
            {
                GridViewRow objRow = (GridViewRow)gvAuditor.Rows[intContador];
                if (objRow != null)
                {
                    CheckBox chbCodigo = (CheckBox)objRow.FindControl("chbCodigo");
                    Label lblCodigo = (Label)objRow.FindControl("lblCodigo");
                    if (chbCodigo.Checked)
                    {
                        //Insere uma nova relacao entre item auditoria x pessoa (auditor)
                        ServiceDesk.Negocio.ClsAuditoria.insereAuditor(intCodigoAuditoria, Convert.ToInt32(lblCodigo.Text));
                    }
                }
                objRow = null;
            }
        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, user.IDusuario.ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region Evento salvaDocumento
    /// <summary>
    /// Grava um documento associado a auditoria
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void salvaDocumento(object sender, EventArgs e)
    {
        try
        {
            int intCodigoAuditoria = 0;
            String strMensagem = String.Empty;

            if (Request.QueryString["codigo"] != null)
            {
                intCodigoAuditoria = Convert.ToInt32(Request.QueryString["codigo"].ToString().Trim());
            }

            SServiceDesk.Negocio.ClsAnexo objAnexo = new SServiceDesk.Negocio.ClsAnexo();
            ClsIdentificador objIdentificador = new ClsIdentificador();

            objIdentificador.Tabela.Valor = objAnexo.Atributos.NomeTabela;

            objAnexo.Codigo.Valor = objIdentificador.getProximoValor().ToString();
            objAnexo.PessoaInclusor.Valor = user.IDusuario.ToString();
            objAnexo.Tabela.Valor = "auditoria";
            objAnexo.TabelaIdentificador.Valor = intCodigoAuditoria.ToString();
            objAnexo.Nome.Valor = txtDocumentoNome.Text;
            objAnexo.DataInclusao.Valor = DateTime.Now.ToString(ClsParametro.DataInclusao);
            objAnexo.Caminho.Valor = Server.MapPath(".") + "\\docs";

            string result = ServiceDesk.Generica.ClsArquivo.uploadArquivo(flDocumento, objAnexo.Caminho.Valor);
            if (string.IsNullOrEmpty(result))
            {
                objAnexo.Caminho.Valor += "\\" + ServiceDesk.Generica.ClsArquivo.getNomeArquivo(flDocumento);
                txtDocumentoNome.Text = string.Empty;

                if (objAnexo.insere(out strMensagem))
                {
                    objIdentificador.atualizaValor();
                }
            }
            else
            {
                strMensagem = result;
            }

            lblMensagem.Text = strMensagem;
            imgIcone.ImageUrl = "images/icones/aviso.gif";
            lblMensagem.Visible = true;
            divMensagem.Visible = true;

            mudaAba(lkbDocumento, e);

            objIdentificador = null;
            objAnexo = null;
        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, user.IDusuario.ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

}