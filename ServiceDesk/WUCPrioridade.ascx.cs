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

public partial class WUCPrioridade : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            int intCodigoPrioridade = 0;

            //Mantém a posição do Scroll após o PostBack
            Page.MaintainScrollPositionOnPostBack = true;

            //Esconde a mensagem de erro
            divMensagem.Visible = false;

            if (!Page.IsPostBack)
            {
                divPrioridadeCriterio.Visible = false;

                //Gerando o GridView de Prioridade e PrioridadeCritério
                ServiceDesk.Negocio.ClsPrioridade.geraGridView(gvPrioridade);

                ServiceDesk.Negocio.ClsTipoImpacto.geraDropDownList(ddlImpacto);
                ddlImpacto.Items.Insert(0, "--");
                ddlImpacto.Items[0].Value = "";

                ServiceDesk.Negocio.ClsTipoUrgencia.geraDropDownList(ddlUrgencia);
                ddlUrgencia.Items.Insert(0, "--");
                ddlUrgencia.Items[0].Value = "";

                if (Request.QueryString["codigo"] != null)
                {
                    intCodigoPrioridade = Convert.ToInt32(Request.QueryString["codigo"].ToString().Trim());

                    //Monta os dados na tela
                    CarregaDescricaoPrioridade(intCodigoPrioridade);
                }
            }

            if (Session["strStatus"] != null)
            {
                //Exibindo mensagem com o status da operação
                lblMensagem.Text = Session["strStatus"].ToString();
                imgIcone.ImageUrl = "images/icones/aviso.gif";
                divMensagem.Visible = true;
            }

            Session["strStatus"] = null;

        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }

    #region metodo CarregaDescricaoPrioridade
    /// <summary>
    /// Método que popula os campos (Descrição, Tempo_Inicio_Atendimento, Tempo_Solução)
    /// </summary>
    /// <param name="intCodigoItem">Código da Prioridade</param>
    private void CarregaDescricaoPrioridade(int intCodigoPrioridade)
    {
        try
        {
            ServiceDesk.Negocio.ClsPrioridade objPrioridade = new ServiceDesk.Negocio.ClsPrioridade(intCodigoPrioridade);
            txtDescricao.Text = objPrioridade.Descricao.Valor;
            txtTempoInicioAtendimento.Text = objPrioridade.TempoInicioAtendimento.Valor;
            txtTempoSolucao.Text = objPrioridade.TempoSolucao.Valor;
            objPrioridade = null;

        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region Salva o Prioridade
    /// <summary>
    /// Método que salva Prioridade (inserir/alterar)
    /// </summary>
    protected void btnSalvar_Click(object sender, EventArgs e)
    {
        try
        {
            if (Request.QueryString["codigo"] == null)
            {
                inserePrioridade();
            }
            else
            {
                if (Request.QueryString["codigo"] != null)
                {
                    alteraPrioridade(Convert.ToInt32(Request.QueryString["codigo"].ToString().Trim()));
                }
            }

        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region metodo inserePrioridade
    /// <summary>
    /// Método que insere uma nova Prioridade
    /// </summary>
    private void inserePrioridade()
    {
        try
        {
            String strMensagem = String.Empty;

            ServiceDesk.Negocio.ClsPrioridade objPrioridade = new ServiceDesk.Negocio.ClsPrioridade();
            ServiceDesk.Negocio.ClsIdentificador objIdentificador = new ServiceDesk.Negocio.ClsIdentificador();

            objIdentificador.Tabela.Valor = objPrioridade.Atributos.NomeTabela;
            objPrioridade.Codigo.Valor = objIdentificador.getProximoValor().ToString();
            objPrioridade.Descricao.Valor = ServiceDesk.Generica.ClsTexto.trocaAspaPorHtml(txtDescricao.Text);
            objPrioridade.TempoInicioAtendimento.Valor = ServiceDesk.Generica.ClsTexto.trocaAspaPorHtml(txtTempoInicioAtendimento.Text);
            objPrioridade.TempoSolucao.Valor = ServiceDesk.Generica.ClsTexto.trocaAspaPorHtml(txtTempoSolucao.Text);

            if (objPrioridade.insere(out strMensagem))
            {
                //Atualizando o valor na tabela identificador
                objIdentificador.atualizaValor();

                //Informando a mensagem do status da operação
                Session["strStatus"] = "Prioridade inserida com sucesso.";
                Response.Redirect("Prioridade.aspx?codigo=" + objPrioridade.Codigo.Valor);
            }
            else
            {
                lblMensagem.Text = "Não foi possivel realizar a operação.<br />" + strMensagem;
                imgIcone.ImageUrl = "images/icones/aviso.gif";
                divMensagem.Visible = true;
            }

            objPrioridade = null;
            objIdentificador = null;

        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region metodo alteraPrioridade
    /// <summary>
    /// Método que altera uma Prioridade
    /// </summary>
    /// <param name="intCodigo">Código da Prioridade</param>
    private void alteraPrioridade(int intCodigoPrioridade)
    {
        try
        {
            String strMensagem = String.Empty;

            ServiceDesk.Negocio.ClsPrioridade objPrioridade = new ServiceDesk.Negocio.ClsPrioridade(intCodigoPrioridade);
            objPrioridade.Descricao.Valor = ServiceDesk.Generica.ClsTexto.trocaAspaPorHtml(txtDescricao.Text);
            objPrioridade.TempoInicioAtendimento.Valor = ServiceDesk.Generica.ClsTexto.trocaAspaPorHtml(txtTempoInicioAtendimento.Text);
            objPrioridade.TempoSolucao.Valor = ServiceDesk.Generica.ClsTexto.trocaAspaPorHtml(txtTempoSolucao.Text);

            if (objPrioridade.altera(out strMensagem))
            {
                Session["strStatus"] = "Prioridade alterada com sucesso.";
            }
            else
            {
                lblMensagem.Text = strMensagem;
                imgIcone.ImageUrl = "images/icones/aviso.gif";
                divMensagem.Visible = true;
            }
            objPrioridade = null;

            //Gerando o GridView com as Origens dos Chamados
            ServiceDesk.Negocio.ClsPrioridade.geraGridView(gvPrioridade);

        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region Evento gvImpacto_OnRowCommand
    /// <summary>
    /// Evento que ocorre quando um comando da GridView é solicitado
    /// </summary>
    protected void gvPrioridade_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Selecionar")
            {
                GridViewRow objRow = gvPrioridade.Rows[Convert.ToInt32(e.CommandArgument)];
                if (objRow != null)
                {
                    Label lblCodigo = (Label)objRow.FindControl("lblCodigo");
                    ServiceDesk.Negocio.ClsPrioridadeCriterio.geraGridView(gvPrioridadeCriterio, Convert.ToInt32(lblCodigo.Text));
                    ViewState["codigo_selecionado"] = lblCodigo.Text;
                    divPrioridadeCriterio.Visible = true;
                    divMensagem.Visible = false;
                }
                objRow = null;

            }

            if (e.CommandName == "Editar")
            {
                GridViewRow objRow = gvPrioridade.Rows[Convert.ToInt32(e.CommandArgument)];
                if (objRow != null)
                {
                    Label lblCodigo = (Label)objRow.FindControl("lblCodigo");
                    Response.Redirect("Prioridade.aspx?codigo=" + lblCodigo.Text);
                }
                objRow = null;
                ServiceDesk.Negocio.ClsPrioridade.geraGridView(gvPrioridade);
            }

            if (e.CommandName == "Excluir")
            {
                GridViewRow objRow = gvPrioridade.Rows[Convert.ToInt32(e.CommandArgument)];
                if (objRow != null)
                {
                    Label lblCodigo = (Label)objRow.FindControl("lblCodigo");

                    ServiceDesk.Negocio.ClsPrioridade objPrioridade = new ServiceDesk.Negocio.ClsPrioridade(Convert.ToInt32(lblCodigo.Text));
                    try
                    {
                        if (objPrioridade.exclui())
                        {
                            lblMensagem.Text = "Item excluido com sucesso.";
                            imgIcone.ImageUrl = "images/icones/aviso.gif";
                            divMensagem.Visible = true;

                            txtDescricao.Text = String.Empty;
                        }
                    }
                    catch (Exception ex)
                    {
                        lblMensagem.Text = "Não foi possível excluir a Origem do Chamado.<br>" + ex.Message;
                        imgIcone.ImageUrl = "images/icones/erro.gif";
                        divMensagem.Visible = true;
                    }

                    objRow = null;
                    objPrioridade = null;
                }
                ServiceDesk.Negocio.ClsPrioridade.geraGridView(gvPrioridade);
            }

        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region Evento gvPrioridade_PageIndexChanging
    /// <summary>
    /// Evento que ocorre quando é mudada a página da GridView
    /// </summary>
    protected void gvPrioridade_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            GridView objGridView = (GridView)sender;
            objGridView.PageIndex = e.NewPageIndex;
            objGridView = null;

        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region Evento gvPrioridade_RowDataBound
    /// <summary>
    /// Evento que ocorre na montagem da GridView Prioridade
    /// </summary>
    protected void gvPrioridade_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            int intCodigoPrioridade = 0;

            if (Request.QueryString["codigo"] != null)
            {
                intCodigoPrioridade = Convert.ToInt32(Request.QueryString["codigo"].ToString().Trim());
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                try
                {
                    ImageButton btnExcluir = (ImageButton)e.Row.Cells[6].Controls[0];
                    btnExcluir.Attributes.Add("onclick", "return verifica();");

                    Label lblCodigo = (Label)e.Row.Cells[0].Controls[1];
                }
                catch
                {
                }
            }

        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion
    
    #region LimparCampos gvPrioridade
    /// <summary>
    /// Método que limpa o campo descrição 
    /// </summary>
    protected void btnLimpar_Click(object sender, EventArgs e)
    {
        txtDescricao.Text = String.Empty;
        txtTempoInicioAtendimento.Text = String.Empty;
        txtTempoSolucao.Text = String.Empty;
        Response.Redirect("Prioridade.aspx");
    }
    #endregion

    /*
    - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
    • Métodos relacionados a GridView gvPrioridadeCriterio
    - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
    */

    #region Salva PrioridadeCriterio
    /// <summary>
    /// Método que salva PrioridadeCriterio
    /// </summary>
    protected void btnSalva_Criterio_Click(object sender, EventArgs e)
    {
        try
        {
            String strMensagem = String.Empty;

            ServiceDesk.Negocio.ClsPrioridadeCriterio objPrioridadeCriterio = new ServiceDesk.Negocio.ClsPrioridadeCriterio();

            if (ViewState["codigo_selecionado"] != null)
            {
                objPrioridadeCriterio.PrioridadeCodigo.Valor = ViewState["codigo_selecionado"].ToString().Trim();
                objPrioridadeCriterio.ImpactoCodigo.Valor = ServiceDesk.Generica.ClsTexto.trocaAspaPorHtml(ddlImpacto.SelectedValue.Trim());
                objPrioridadeCriterio.UrgenciaCodigo.Valor = ServiceDesk.Generica.ClsTexto.trocaAspaPorHtml(ddlUrgencia.SelectedValue.Trim());

                if (objPrioridadeCriterio.VerificaExisteCriterio(out strMensagem, ViewState["codigo_selecionado"].ToString(),
                    ddlImpacto.SelectedValue, ddlUrgencia.SelectedValue) == false)
                {
                    if (objPrioridadeCriterio.insere(out strMensagem))
                    {
                        //Informando a mensagem do status da operação
                        Session["strStatus"] = "Critério de prioridade inserido com sucesso.";
                        ServiceDesk.Negocio.ClsPrioridadeCriterio.geraGridView(gvPrioridadeCriterio, Convert.ToInt32(ViewState["codigo_selecionado"]));
                    }
                    else
                    {
                        lblMensagem.Text = "Não foi possivel realizar a operação.<br />" + strMensagem;
                        imgIcone.ImageUrl = "images/icones/aviso.gif";
                        divMensagem.Visible = true;
                    }
                }
                else
                {
                    lblMensagem.Text = strMensagem;
                    imgIcone.ImageUrl = "images/icones/aviso.gif";
                    divMensagem.Visible = true;
                }
            }
            else
            {
                lblMensagem.Text = "Selecione uma prioridade.";
                imgIcone.ImageUrl = "images/icones/aviso.gif";
                divMensagem.Visible = true;
            }

            objPrioridadeCriterio = null;

        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region Evento gvPrioridadeCriterio_OnRowCommand
    /// <summary>
    /// Evento que ocorre quando um comando da GridView é solicitado
    /// </summary>
    protected void gvPrioridadeCriterio_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Excluir")
            {
                GridViewRow objRow = gvPrioridadeCriterio.Rows[Convert.ToInt32(e.CommandArgument)];
                if (objRow != null)
                {
                    // Seleciona os códigos que serão excluidos
                    Label lblCodigoPrioridadeCriterio = (Label)objRow.FindControl("lblCodigo");
                    Label lblImpactoCodigo = (Label)objRow.FindControl("lblImpactoCodigoValor");
                    Label lblUrgenciaCodigo = (Label)objRow.FindControl("lblUrgenciaCodigoValor");

                    ServiceDesk.Negocio.ClsPrioridadeCriterio objPrioridadeCriterio = new ServiceDesk.Negocio.ClsPrioridadeCriterio();
                    objPrioridadeCriterio.PrioridadeCodigo.Valor = lblCodigoPrioridadeCriterio.Text.Trim();
                    objPrioridadeCriterio.ImpactoCodigo.Valor = lblImpactoCodigo.Text.Trim();
                    objPrioridadeCriterio.UrgenciaCodigo.Valor = lblUrgenciaCodigo.Text.Trim();

                    try
                    {
                        if (objPrioridadeCriterio.exclui(Convert.ToInt32(lblCodigoPrioridadeCriterio.Text),
                            Convert.ToInt32(lblImpactoCodigo.Text), Convert.ToInt32(lblUrgenciaCodigo.Text)))
                        {
                            lblMensagem.Text = "Item excluido com sucesso.";
                            imgIcone.ImageUrl = "images/icones/aviso.gif";
                            divMensagem.Visible = true;

                            txtDescricao.Text = String.Empty;
                        }
                    }
                    catch (Exception ex)
                    {
                        lblMensagem.Text = "Não foi possível excluir a Origem do Chamado.<br>" + ex.Message;
                        imgIcone.ImageUrl = "images/icones/erro.gif";
                        divMensagem.Visible = true;
                    }

                    objRow = null;
                    objPrioridadeCriterio = null;
                }
                ServiceDesk.Negocio.ClsPrioridadeCriterio.geraGridView(gvPrioridadeCriterio, Convert.ToInt32(ViewState["codigo_selecionado"]));
            }

        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region Evento gvPrioridadeCriterio_RowEditing
    /// <summary>
    /// Método que é disparado quando é selecionada a opção editar
    /// </summary>
    protected void gvPrioridadeCriterio_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            gvPrioridadeCriterio.EditIndex = e.NewEditIndex;
            ServiceDesk.Negocio.ClsPrioridadeCriterio.geraGridView(gvPrioridadeCriterio, Convert.ToInt32(ViewState["codigo_selecionado"]));

        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region Evento gvPrioridadeCriterio_RowDataBound
    /// <summary>
    /// Carrega os dados na GridView
    /// </summary>
    protected void gvPrioridadeCriterio_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Condicao IF que exibe os dados no GridView (estado: não-editável)
                if ((e.Row.RowState.ToString() == "Normal") || (e.Row.RowState.ToString() == "Alternate"))
                {
                    // Adiciona um evento javascript no botão Excluir
                    ImageButton btnExcluir = (ImageButton)e.Row.Cells[4].Controls[0];
                    btnExcluir.Attributes.Add("onclick", "return verifica();");

                    // Carrega o campo Impacto com a descrição do Impacto e não o código
                    Label lblImpactoCodigo = (Label)e.Row.FindControl("lblImpactoCodigo");
                    if (lblImpactoCodigo.Text != String.Empty)
                    {
                        ServiceDesk.Negocio.ClsTipoImpacto objTipoImpacto = new ServiceDesk.Negocio.ClsTipoImpacto(Convert.ToInt32(lblImpactoCodigo.Text));
                        lblImpactoCodigo.Text = objTipoImpacto.Descricao.Valor;
                    }

                    // Carrega o campo Urgência com a descrição de Urgência e não o código
                    Label lblUrgenciaCodigo = (Label)e.Row.FindControl("lblUrgenciaCodigo");
                    if (lblUrgenciaCodigo.Text != String.Empty)
                    {
                        ServiceDesk.Negocio.ClsTipoUrgencia objTipoUrgencia = new ServiceDesk.Negocio.ClsTipoUrgencia(Convert.ToInt32(lblUrgenciaCodigo.Text));
                        lblUrgenciaCodigo.Text = objTipoUrgencia.Descricao.Valor;
                    }
                }

                // Condicao IF que exibe os controles e dados destes durante edição de uma linha (estado: editável)
                if ((e.Row.RowState == DataControlRowState.Edit) || (e.Row.RowState.ToString() == "Alternate, Edit"))
                {
                    Label lblImpactoCodigo = (Label)e.Row.FindControl("lblImpactoCodigo");
                    DropDownList ddlImpactoCodigo = (DropDownList)e.Row.FindControl("ddlImpactoCodigo");
                    ServiceDesk.Negocio.ClsTipoImpacto.geraDropDownList(ddlImpactoCodigo);
                    ddlImpactoCodigo.SelectedValue = lblImpactoCodigo.Text;

                    Label lblUrgenciaCodigo = (Label)e.Row.FindControl("lblUrgenciaCodigo");
                    DropDownList ddlUrgenciaCodigo = (DropDownList)e.Row.FindControl("ddlUrgenciaCodigo");
                    ServiceDesk.Negocio.ClsTipoUrgencia.geraDropDownList(ddlUrgenciaCodigo);
                    ddlUrgenciaCodigo.SelectedValue = lblUrgenciaCodigo.Text;
                }
            }

        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region Evento gvPrioridadeCriterio_RowCancelingEdit
    protected void gvPrioridadeCriterio_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        try
        {
            gvPrioridadeCriterio.EditIndex = -1;
            ServiceDesk.Negocio.ClsPrioridadeCriterio.geraGridView(gvPrioridadeCriterio, Convert.ToInt32(ViewState["codigo_selecionado"]));

        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region Evento gvPrioridadeCriterio_RowUpdating
    /// <summary>
    /// Método que atualiza um item do DataGrid
    /// </summary>
    protected void gvPrioridadeCriterio_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            String strMensagem = String.Empty;

            GridViewRow objRow = gvPrioridadeCriterio.Rows[e.RowIndex];
            if (objRow != null)
            {
                try
                {
                    Label lblCodigoPrioridadeCriterio = (Label)objRow.FindControl("lblCodigo");

                    Label lblImpactoCodigo = (Label)objRow.FindControl("lblImpactoCodigo");
                    DropDownList ddlImpactoCodigo = (DropDownList)objRow.FindControl("ddlImpactoCodigo");

                    Label lblUrgenciaCodigo = (Label)objRow.FindControl("lblUrgenciaCodigo");
                    DropDownList ddlUrgenciaCodigo = (DropDownList)objRow.FindControl("ddlUrgenciaCodigo");

                    ServiceDesk.Negocio.ClsPrioridadeCriterio objPrioridadeCriterio = new ServiceDesk.Negocio.ClsPrioridadeCriterio();


                    if (objPrioridadeCriterio.VerificaExisteCriterio(out strMensagem, lblCodigoPrioridadeCriterio.Text,
                        ddlImpactoCodigo.SelectedValue, ddlUrgenciaCodigo.SelectedValue) == false)
                    {
                        if (objPrioridadeCriterio.altera(Convert.ToInt32(lblCodigoPrioridadeCriterio.Text),
                            Convert.ToInt32(lblImpactoCodigo.Text), Convert.ToInt32(lblUrgenciaCodigo.Text),
                            Convert.ToInt32(lblCodigoPrioridadeCriterio.Text),
                            Convert.ToInt32(ddlImpactoCodigo.SelectedValue),
                            Convert.ToInt32(ddlUrgenciaCodigo.SelectedValue)))
                        {
                            lblMensagem.Text = "Item alterado com sucesso.";
                            imgIcone.ImageUrl = "images/icones/info.gif";
                            divMensagem.Visible = true;
                            gvPrioridadeCriterio.EditIndex = -1;
                            ServiceDesk.Negocio.ClsPrioridadeCriterio.geraGridView(gvPrioridadeCriterio, Convert.ToInt32(ViewState["codigo_selecionado"]));
                        }
                        else
                        {
                            lblMensagem.Text = "Não foi possível realizar a operação.<br>" + strMensagem;
                            imgIcone.ImageUrl = "images/icones/aviso.gif";
                            divMensagem.Visible = true;
                        }
                    }
                    else
                    {
                        lblMensagem.Text = strMensagem;
                        imgIcone.ImageUrl = "images/icones/aviso.gif";
                        divMensagem.Visible = true;
                    }
                }
                catch (Exception ex)
                {
                    lblMensagem.Text = "Não foi possível realizar a operação.<br>" + ex.ToString();
                    imgIcone.ImageUrl = "images/icones/erro.gif";
                    divMensagem.Visible = true;
                }

                objRow = null;
            }

        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region LimparCampos gvPrioridadeCriterio
    /// <summary>
    /// Método que limpa o campo descrição 
    /// </summary>
    protected void btnLimpa_Criterio_Click(object sender, EventArgs e)
    {
        ddlImpacto.SelectedValue = String.Empty;
        ddlUrgencia.SelectedValue = String.Empty;
    }
    #endregion
    
}
