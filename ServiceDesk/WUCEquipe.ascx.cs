using System;
using System.Data;
using System.Data.Common;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ServiceDesk.Negocio;

public partial class WUCEquipe : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //Mantém a posição do Scroll após o PostBack
            Page.MaintainScrollPositionOnPostBack = true;

            //Esconde a mensagem de erro
            divMensagem.Visible = false;

            if (!Page.IsPostBack)
            {
                divEquipePessoa.Visible = false;

                //Gerando o GridView Equipe
                ServiceDesk.Negocio.ClsEquipe.geraGridView(gvEquipe);

                SServiceDesk.Negocio.ClsAplicacao.geraDropDownList(ddlAplicacao);
                ddlAplicacao.Items.Insert(0, "--");
                ddlAplicacao.Items[0].Value = "";

                ServiceDesk.Negocio.ClsEquipeNivel.geraDropDownList(ddlNivelEquipe);

                ServiceDesk.Negocio.ClsEquipePessoa objEquipPessoa = new ServiceDesk.Negocio.ClsEquipePessoa();
                ddlEmpresa.DataSource = objEquipPessoa.geraDropDownEmpresa();
                ddlEmpresa.DataTextField = "descricao";
                ddlEmpresa.DataValueField = "estrutura_codigo";
                ddlEmpresa.DataBind();
                ddlEmpresa.Items.Insert(0, "--");
                ddlEmpresa.Items[0].Value = "";
                objEquipPessoa = null;
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
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO,ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }

    #region Salva Equipe
    /// <summary>
    /// Método que salva uma Equipe
    /// </summary>
    protected void btnSalvar_Click(object sender, EventArgs e)
    {
        try
        {
            String strMensagem = String.Empty;

            ServiceDesk.Negocio.ClsEquipe objEquipe = new ServiceDesk.Negocio.ClsEquipe();
            ServiceDesk.Negocio.ClsIdentificador objIdentificador = new ServiceDesk.Negocio.ClsIdentificador();

            objIdentificador.Tabela.Valor = objEquipe.Atributos.NomeTabela;
            objEquipe.Codigo.Valor = objIdentificador.getProximoValor().ToString();
            objEquipe.Descricao.Valor = ServiceDesk.Generica.ClsTexto.trocaAspaPorHtml(txtDescricao.Text);
            objEquipe.CodigoAplicacao.Valor = ServiceDesk.Generica.ClsTexto.trocaAspaPorHtml(ddlAplicacao.SelectedValue);
            objEquipe.CodigoNivelEquipe.Valor = ServiceDesk.Generica.ClsTexto.trocaAspaPorHtml(ddlNivelEquipe.SelectedValue);

            if (objEquipe.insere(out strMensagem))
            {
                //Atualizando o valor na tabela identificador
                objIdentificador.atualizaValor();

                ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.INSERT,ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, objEquipe.Codigo.Valor, String.Empty);


                //Informando a mensagem do status da operação
                lblMensagem.Text = strMensagem;
                imgIcone.ImageUrl = "images/icones/aviso.gif";
                divMensagem.Visible = true;
            }
            else
            {
                lblMensagem.Text = "Não foi possivel realizar a operação.<br />" + strMensagem;
                imgIcone.ImageUrl = "images/icones/aviso.gif";
                divMensagem.Visible = true;
            }

            ServiceDesk.Negocio.ClsEquipe.geraGridView(gvEquipe);
            objEquipe = null;
            objIdentificador = null;
        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO,ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region LimparCampos Equipe
    /// <summary>
    /// Método que limpa os campos de Inserção de Equipe 
    /// </summary>
    protected void btnLimpar_Click(object sender, EventArgs e)
    {
        txtDescricao.Text = String.Empty;
        ddlAplicacao.SelectedValue = String.Empty;
        ddlNivelEquipe.SelectedValue = String.Empty;
        Response.Redirect("Equipe.aspx");
    }
    #endregion

    #region Evento gvEquipe_OnRowCommand
    /// <summary>
    /// Evento que ocorre quando algum comando do GridView é executado(Alterar/Excluir/Editar)
    /// </summary>
    protected void gvEquipe_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Selecionar")
            {
                GridViewRow objRow = gvEquipe.Rows[Convert.ToInt32(e.CommandArgument)];
                if (objRow != null)
                {
                    DataSet ds;
                    Label lblCodigo = (Label)objRow.FindControl("lblCodigo");
                    ServiceDesk.Negocio.ClsEquipePessoa objEquipePessoa = new ServiceDesk.Negocio.ClsEquipePessoa();

                    ds = objEquipePessoa.GetEquipe(lblCodigo.Text, String.Empty);

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        gvEquipePessoa.DataSource = ds;
                        gvEquipePessoa.DataBind();
                        divIntegrantesEquipe.Visible = true;
                        divMensagem.Visible = false;
                    }
                    else
                    {
                        lblMensagem.Text = "Esta equipe não possui integrantes.";
                        imgIcone.ImageUrl = "images/icones/aviso.gif";
                        divMensagem.Visible = true;
                        divIntegrantesEquipe.Visible = false;
                    }
                    ViewState["codigo_selecionado"] = lblCodigo.Text;
                    divEquipePessoa.Visible = true;

                    ds.Dispose();
                }
                objRow = null;
            }

            if (e.CommandName == "Excluir")
            {
                GridViewRow objRow = gvEquipe.Rows[Convert.ToInt32(e.CommandArgument)];
                if (objRow != null)
                {
                    // Seleciona os códigos que serão excluidos
                    Label lblCodigoEquipe = (Label)objRow.FindControl("lblCodigo");

                    ServiceDesk.Negocio.ClsEquipe objEquipe = new ServiceDesk.Negocio.ClsEquipe(Convert.ToInt32(lblCodigoEquipe.Text));
                    try
                    {
                        if (objEquipe.exclui())
                        {
                            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.DELETE,ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, objEquipe.Codigo.Valor, String.Empty);


                            lblMensagem.Text = "Item excluido com sucesso.";
                            imgIcone.ImageUrl = "images/icones/aviso.gif";
                            divMensagem.Visible = true;

                            ddlAplicacao.SelectedValue = String.Empty;
                            ddlNivelEquipe.SelectedValue = String.Empty;
                            txtDescricao.Text = String.Empty;
                        }
                    }
                    catch
                    {
                        lblMensagem.Text = "Não foi possível excluir o item.<br>";
                        imgIcone.ImageUrl = "images/icones/erro.gif";
                        divMensagem.Visible = true;
                    }

                    objRow = null;
                    objEquipe = null;
                }
                ServiceDesk.Negocio.ClsEquipe.geraGridView(gvEquipe);
            }

        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO,ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region Evento gvEquipe_RowEditing
    /// <summary>
    /// Método que é disparado quando é selecionada a opção editar
    /// </summary>
    protected void gvEquipe_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            gvEquipe.EditIndex = e.NewEditIndex;
            ServiceDesk.Negocio.ClsEquipe.geraGridView(gvEquipe);
        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO,ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());

        }
    }
    #endregion

    #region Evento gvEquipe_RowDataBound
    /// <summary>
    /// Carrega os dados na GridView
    /// </summary>
    protected void gvEquipe_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Condicao IF que exibe os dados no GridView (estado: não-editável)
                if ((e.Row.RowState.ToString() == "Normal") || (e.Row.RowState.ToString() == "Alternate"))
                {
                    // Adiciona um evento javascript no botão Excluir
                    ImageButton btnExcluir = (ImageButton)e.Row.Cells[6].Controls[0];
                    btnExcluir.Attributes.Add("onclick", "return verifica();");

                    // Carrega o campo Aplicação com a descrição da Aplicação e não o código
                    Label lblAplicacaoCodigo = (Label)e.Row.FindControl("lblAplicacaoCodigo");
                    if (lblAplicacaoCodigo.Text != String.Empty)
                    {
                        SServiceDesk.Negocio.ClsAplicacao objAplicacao = new SServiceDesk.Negocio.ClsAplicacao(Convert.ToInt32(lblAplicacaoCodigo.Text));
                        lblAplicacaoCodigo.Text = objAplicacao.Descricao.Valor;
                        objAplicacao = null;
                    }

                    // Carrega o campo NivelEquipe com a descrição de NivelEquipe e não o código
                    Label lblNivelEquipeCodigo = (Label)e.Row.FindControl("lblNivelEquipeCodigo");
                    if (lblNivelEquipeCodigo.Text != String.Empty)
                    {
                        ServiceDesk.Negocio.ClsEquipeNivel objEquipeNivel = new ServiceDesk.Negocio.ClsEquipeNivel(Convert.ToInt32(lblNivelEquipeCodigo.Text));
                        lblNivelEquipeCodigo.Text = objEquipeNivel.Descricao.Valor;
                        objEquipeNivel = null;
                    }
                }

                // Condicao IF que exibe os controles e dados destes durante edição de uma linha (estado: editável)
                if ((e.Row.RowState == DataControlRowState.Edit) || (e.Row.RowState.ToString() == "Alternate, Edit"))
                {
                    Label lblAplicacaoCodigo = (Label)e.Row.FindControl("lblAplicacaoCodigo");
                    DropDownList ddlAplicacaoCodigo = (DropDownList)e.Row.FindControl("ddlAplicacaoCodigo");
                    SServiceDesk.Negocio.ClsAplicacao.geraDropDownList(ddlAplicacaoCodigo);
                    ddlAplicacaoCodigo.SelectedValue = lblAplicacaoCodigo.Text;

                    Label lblNivelEquipeCodigo = (Label)e.Row.FindControl("lblNivelEquipeCodigo");
                    DropDownList ddlNivelEquipeCodigo = (DropDownList)e.Row.FindControl("ddlNivelEquipeCodigo");
                    ServiceDesk.Negocio.ClsEquipeNivel.geraDropDownList(ddlNivelEquipeCodigo, "");
                    ddlNivelEquipeCodigo.SelectedValue = lblNivelEquipeCodigo.Text;

                    // Carrega o campo Descrição com a descrição da Equipe
                    Label lblDescricao = (Label)e.Row.FindControl("lblDescricao");
                    TextBox txtDescricaoEquipe = (TextBox)e.Row.FindControl("txtDescricaoEquipe");
                    if (lblDescricao.Text != String.Empty)
                    {
                        txtDescricaoEquipe.Text = lblDescricao.Text;
                    }
                }
            }

        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO,ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region Evento gvEquipe_RowCancelingEdit
    protected void gvEquipe_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvEquipe.EditIndex = -1;
        ServiceDesk.Negocio.ClsEquipe.geraGridView(gvEquipe);
    }
    #endregion

    #region Evento gvEquipe_RowUpdating
    /// <summary>
    /// Método que atualiza um item do DataGrid
    /// </summary>
    protected void gvEquipe_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            String strMensagem = String.Empty;

            GridViewRow objRow = gvEquipe.Rows[e.RowIndex];
            if (objRow != null)
            {
                try
                {
                    Label lblCodigo = (Label)objRow.FindControl("lblCodigo");
                    DropDownList ddlAplicacaoCodigo = (DropDownList)objRow.FindControl("ddlAplicacaoCodigo");
                    DropDownList ddlNivelEquipeCodigo = (DropDownList)objRow.FindControl("ddlNivelEquipeCodigo");
                    TextBox txtDescricaoEquipe = (TextBox)objRow.FindControl("txtDescricaoEquipe");

                    ServiceDesk.Negocio.ClsEquipe objEquipe = new ServiceDesk.Negocio.ClsEquipe();
                    objEquipe.Codigo.Valor = lblCodigo.Text;
                    objEquipe.CodigoAplicacao.Valor = ddlAplicacaoCodigo.SelectedValue;
                    objEquipe.CodigoNivelEquipe.Valor = ddlNivelEquipeCodigo.SelectedValue;
                    objEquipe.Descricao.Valor = txtDescricaoEquipe.Text;

                    ServiceDesk.Negocio.ClsEquipe objEquipeAntigo = new ServiceDesk.Negocio.ClsEquipe(Convert.ToInt32(objEquipe.Codigo.Valor));

                    if (objEquipe.altera(out strMensagem))
                    {
                        ServiceDesk.Negocio.ClsEquipe objEquipeAtualizado = new ServiceDesk.Negocio.ClsEquipe(Convert.ToInt32(objEquipe.Codigo.Valor));

                        ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.UPDATE,ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, objEquipe.Codigo.Valor, objEquipeAtualizado.Atributos, objEquipeAntigo.Atributos);

                        lblMensagem.Text = "Item alterado com sucesso.";
                        imgIcone.ImageUrl = "images/icones/info.gif";
                        divMensagem.Visible = true;
                        gvEquipe.EditIndex = -1;
                        ServiceDesk.Negocio.ClsEquipe.geraGridView(gvEquipe);
                        objEquipeAtualizado = null;
                    }
                    else
                    {
                        lblMensagem.Text = "Não foi possível realizar a operação.<br>" + strMensagem;
                        imgIcone.ImageUrl = "images/icones/aviso.gif";
                        divMensagem.Visible = true;
                    }
                    objEquipe = null;
                    objEquipeAntigo = null;

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
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO,ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region metodo CarregaEquipe
    /// <summary>
    /// Método que popula os campos (Aplicação, NivelEquipe e Descrição)
    /// </summary>
    /// <param name="intCodigoItem">Código da Prioridade</param>
    private void CarregaEquipe(int intCodigoEquipe)
    {
        try
        {
            ServiceDesk.Negocio.ClsEquipe objEquipe = new ServiceDesk.Negocio.ClsEquipe(intCodigoEquipe);
            ddlAplicacao.SelectedValue = objEquipe.CodigoAplicacao.Valor;
            ddlNivelEquipe.SelectedValue = objEquipe.CodigoNivelEquipe.Valor;
            txtDescricao.Text = objEquipe.Descricao.Valor;
            objEquipe = null;

        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO,ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    /*
      - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
      • Métodos relacionados a GridView gvEquipePessoa
      - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
      */

    protected void btnFiltrar_Click(object sender, EventArgs e)
    {
        try
        {
            DataSet ds;
            ServiceDesk.Negocio.ClsEquipePessoa objEquipePessoa = new ServiceDesk.Negocio.ClsEquipePessoa();

            if (ViewState["codigo_selecionado"] != null)
            {
                ds = objEquipePessoa.GetEquipe(ViewState["codigo_selecionado"].ToString(), ddlEmpresa.SelectedValue);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gvEquipePessoa.DataSource = ds;
                    gvEquipePessoa.DataBind();
                    divIntegrantesEquipe.Visible = true;
                }
                else
                {
                    lblMensagem.Text = "Sua consulta não retornou resultados.";
                    imgIcone.ImageUrl = "images/icones/aviso.gif";
                    divMensagem.Visible = true;
                    divIntegrantesEquipe.Visible = false;
                }
                ds.Dispose();
            }
            else
            {
                lblMensagem.Text = "Selecione uma equipe.";
                imgIcone.ImageUrl = "images/icones/aviso.gif";
                divMensagem.Visible = true;
                divIntegrantesEquipe.Visible = false;
            }
            objEquipePessoa = null;


        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO,ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }

    protected void btnSalvarPessoaEquipe_Click(object sender, EventArgs e)
    {
        try
        {
            int intContador = 0;
            int intContInclusos = 0;
            String strMensagem = String.Empty;

            try
            {

                ServiceDesk.Negocio.ClsEquipePessoa objEquipePessoa = new ServiceDesk.Negocio.ClsEquipePessoa();
                ServiceDesk.Negocio.ClsIdentificador objIdentificador = new ServiceDesk.Negocio.ClsIdentificador();

                if (gvEquipePessoa.EditIndex != -1)
                {
                    gvEquipePessoa.EditIndex = -1;
                    gvEquipePessoa.DataSource = objEquipePessoa.GetEquipe(ViewState["codigo_selecionado"].ToString(), ddlEmpresa.SelectedValue);
                    gvEquipePessoa.DataBind();
                }

                if (ServiceDesk.Negocio.ClsEquipePessoa.excluiEquipe(Convert.ToInt32(ViewState["codigo_selecionado"])))
                {
                    for (intContador = 0; intContador < gvEquipePessoa.Rows.Count; intContador++)
                    {
                        GridViewRow objRow = (GridViewRow)gvEquipePessoa.Rows[intContador];
                        if (objRow != null)
                        {
                            CheckBox ckEquipePessoa = (CheckBox)objRow.FindControl("ckEquipePessoa");
                            Label lblCodigo = (Label)objRow.FindControl("lblPessoaCodigo");
                            Label lblFlagLider = (Label)objRow.FindControl("lblCodigoFlagLider");

                            if (ckEquipePessoa.Checked == true)
                            {
                                objIdentificador.Tabela.Valor = objEquipePessoa.Atributos.NomeTabela;
                                objEquipePessoa.EquipePessoaCodigo.Valor = objIdentificador.getProximoValor().ToString();
                                objEquipePessoa.EquipeCodigo.Valor = ServiceDesk.Generica.ClsTexto.trocaAspaPorHtml(ViewState["codigo_selecionado"].ToString());
                                objEquipePessoa.PessoaCodigo.Valor = ServiceDesk.Generica.ClsTexto.trocaAspaPorHtml(lblCodigo.Text);
                                objEquipePessoa.FlagLider.Valor = ServiceDesk.Generica.ClsTexto.trocaAspaPorHtml(lblFlagLider.Text);

                                if (objEquipePessoa.insere(out strMensagem))
                                {
                                    intContInclusos += 1;
                                    objIdentificador.atualizaValor();

                                    if (intContInclusos == 1)
                                        strMensagem = "1 colaborador incluso com sucesso na Equipe.";
                                    else if (intContInclusos > 1)
                                        strMensagem = intContInclusos.ToString() + " colaboradores inclusos com sucesso na Equipe.";
                                }
                            }
                        } //fim if Row
                        objRow = null;
                    }

                    // Só apresenta caso tenha inserido algum colaborador
                    if (intContInclusos > 0)
                    {
                        lblMensagem.Text = strMensagem;
                        imgIcone.ImageUrl = "images/icones/aviso.gif";
                        divMensagem.Visible = true;
                    }

                    gvEquipePessoa.DataSource = objEquipePessoa.GetEquipe(ViewState["codigo_selecionado"].ToString(), ddlEmpresa.SelectedValue);
                    gvEquipePessoa.DataBind();

                    objEquipePessoa = null;
                    objIdentificador = null;
                    ViewState["intContInclusos"] = null;
                }
            }
            catch (Exception ex)
            {
                lblMensagem.Text = "Não foi possivel realizar a operação.<br />" + ex.ToString();
                imgIcone.ImageUrl = "images/icones/aviso.gif";
                divMensagem.Visible = true;
            }

        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO,ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }

    protected void gvEquipePessoa_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        try
        {
            ServiceDesk.Negocio.ClsEquipePessoa objEquipePessoa = new ServiceDesk.Negocio.ClsEquipePessoa();
            gvEquipePessoa.EditIndex = -1;
            gvEquipePessoa.DataSource = objEquipePessoa.GetEquipe(ViewState["codigo_selecionado"].ToString(), ddlEmpresa.SelectedValue);
            gvEquipePessoa.DataBind();
            objEquipePessoa = null;

        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO,ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }

    protected void gvEquipePessoa_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            GridViewRow objRow = gvEquipePessoa.Rows[e.NewEditIndex];
            if (objRow != null)
            {
                Label lblCodigoPessoa = (Label)objRow.FindControl("lblPessoaCodigo");
                ServiceDesk.Negocio.ClsEquipePessoa objEquipePessoa = new ServiceDesk.Negocio.ClsEquipePessoa();

                if (objEquipePessoa.VerificaExisteEquipePessoa(Convert.ToInt32(lblCodigoPessoa.Text), Convert.ToInt32(ViewState["codigo_selecionado"])))
                {
                    gvEquipePessoa.EditIndex = e.NewEditIndex;
                }
                else
                {
                    lblMensagem.Text = "Esta colaborador não está incluso nesta equipe, por isso você não pode definilo como lider.<br />";
                    imgIcone.ImageUrl = "images/icones/aviso.gif";
                    divMensagem.Visible = true;
                }

                gvEquipePessoa.DataSource = objEquipePessoa.GetEquipe(ViewState["codigo_selecionado"].ToString(), ddlEmpresa.SelectedValue);
                gvEquipePessoa.DataBind();
                objEquipePessoa = null;
            }
            objRow.Dispose();
        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO,ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }

    protected void gvEquipePessoa_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            String strMensagem = String.Empty;

            GridViewRow objRow = gvEquipePessoa.Rows[e.RowIndex];
            if (objRow != null)
            {
                try
                {
                    Label lblCodigoPessoa = (Label)objRow.FindControl("lblPessoaCodigo");
                    DropDownList ddlFlagLider = (DropDownList)objRow.FindControl("ddlFlagLider");
                    ServiceDesk.Negocio.ClsEquipePessoa objEquipePessoa = new ServiceDesk.Negocio.ClsEquipePessoa();

                    if (ddlFlagLider.SelectedValue == "S")
                    {
                        if (objEquipePessoa.GetLiderEquipe(Convert.ToInt32(ViewState["codigo_selecionado"]), Convert.ToInt32(lblCodigoPessoa.Text)) == true)
                        {
                            //Existe um lider
                            lblMensagem.Text = "Esta equipe já possui um lider.";
                            imgIcone.ImageUrl = "images/icones/aviso.gif";
                            divMensagem.Visible = true;
                        }
                        else
                        {
                            if (objEquipePessoa.altera(out strMensagem, Convert.ToInt32(ViewState["codigo_selecionado"]), Convert.ToInt32(lblCodigoPessoa.Text), "S"))
                            {
                                lblMensagem.Text = "Lider definido com sucesso.";
                                imgIcone.ImageUrl = "images/icones/aviso.gif";
                                divMensagem.Visible = true;
                            }
                        }
                    }
                    else
                    {
                        if (objEquipePessoa.altera(out strMensagem, Convert.ToInt32(ViewState["codigo_selecionado"]), Convert.ToInt32(lblCodigoPessoa.Text), "N"))
                        { }
                    }

                    gvEquipePessoa.EditIndex = -1;
                    gvEquipePessoa.DataSource = objEquipePessoa.GetEquipe(ViewState["codigo_selecionado"].ToString(), ddlEmpresa.SelectedValue);
                    gvEquipePessoa.DataBind();
                    objEquipePessoa = null;
                }
                catch (Exception ex)
                {
                    lblMensagem.Text = "Não foi possível realizar a operação.<br>" + ex.ToString();
                    imgIcone.ImageUrl = "images/icones/erro.gif";
                    divMensagem.Visible = true;
                }
            }
            objRow.Dispose();
        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO,ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }

    protected void gvEquipePessoa_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Condicao IF que exibe os dados no GridView (estado: não-editável)
                if ((e.Row.RowState.ToString() == "Normal") || (e.Row.RowState.ToString() == "Alternate"))
                {
                    Label lblIntegranteEquipe = (Label)e.Row.FindControl("lblIntegranteEquipe");
                    CheckBox ckEquipePessoa = (CheckBox)e.Row.FindControl("ckEquipePessoa");
                    if (lblIntegranteEquipe.Text == "S")
                        ckEquipePessoa.Checked = true;

                    Label lblFlagLider = (Label)e.Row.FindControl("lblFlagLider");
                    if (lblFlagLider.Text == "S")
                        lblFlagLider.Text = "Sim";
                    else
                        lblFlagLider.Text = "Não";
                }

                // Condicao IF que exibe os controles e dados destes durante edição de uma linha (estado: editável)
                if ((e.Row.RowState == DataControlRowState.Edit) || (e.Row.RowState.ToString() == "Alternate, Edit"))
                {
                    Label lblIntegranteEquipe = (Label)e.Row.FindControl("lblIntegranteEquipe2");
                    CheckBox ckEquipePessoa = (CheckBox)e.Row.FindControl("ckEquipePessoa2");
                    if (lblIntegranteEquipe.Text == "S")
                        ckEquipePessoa.Checked = true;

                    Label lblFlagLider = (Label)e.Row.FindControl("lblFlagLider");
                    DropDownList ddlFlagLider = (DropDownList)e.Row.FindControl("ddlFlagLider");
                    ddlFlagLider.Items.Insert(0, "Sim");
                    ddlFlagLider.Items[0].Value = "S";

                    ddlFlagLider.Items.Insert(1, "Não");
                    ddlFlagLider.Items[1].Value = "N";
                    ddlFlagLider.SelectedValue = lblFlagLider.Text;
                }
            }

        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO,ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
}