using System;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ServiceDesk.Negocio;

public partial class WorkFlow : BasePage
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
            CheckAcesso(27);

            int intCodigoAtributo = 0;

            //Esconde a mensagem de erro
            lblMensagem.Visible = false;
            divMensagem.Visible = false;

            pnNovoWorkFlowTipo.Visible = false;
            pnNovoWorkFlow.Visible = false;

            if (!Page.IsPostBack)
            {
                //Populando a Grid View dos Tipos de WorkFlow
                SServiceDesk.Negocio.ClsWorkFlowTipo.geraGridView(gvWorkFlowTipo);

                if (Request.QueryString["codigo"] != null)
                {
                    intCodigoAtributo = Convert.ToInt32(Request.QueryString["codigo"].ToString().Trim());
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
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, user.IDusuario.ToString(), this.Request.Path, "0", ex.ToString());
        }

    }
    #endregion

    #region Evento gvWorkFlowTipo_OnRowCommand
    /// <summary>
    /// Evento que ocorre quando um comando da GridView é solicitado
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvWorkFlowTipo_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            GridView objGridView = (GridView)sender;
            String strMensagem = String.Empty;

            if (e.CommandName == "Fluxo")
            {

                GridViewRow objRow = objGridView.Rows[Convert.ToInt32(e.CommandArgument)];

                if (objRow != null)
                {

                    Label lblCodigo = (Label)objRow.FindControl("lblCodigo");
                    Label lblTabela = (Label)objRow.FindControl("lblTabela");

                    objGridView.SelectedIndex = Convert.ToInt32(e.CommandArgument);
                    pnNovoWorkFlow.Visible = true;
                    ddlWorkFlowStatus.Enabled = true;
                    ddlWorkFlowSuperior.Enabled = true;

                    //Montando o dropdownlist dos status
                    ServiceDesk.Negocio.ClsStatus.geraDropDownList(ddlWorkFlowStatus);

                    //Montando os dados de Repercusao
                    SServiceDesk.Negocio.ClsWorkFlowTipo.geraDropDownListTabelaItem(ddlRepercusaoTabela, lblTabela.Text, "Informe a Tabela");
                    ServiceDesk.Negocio.ClsStatus.geraDropDownList(ddlRepercusaoStatus);

                    //Montando o dropdownlist dos workflows Superiores
                    SServiceDesk.Negocio.ClsWorkFlow.geraDropDownListPorTipo(ddlWorkFlowSuperior, Convert.ToInt32(lblCodigo.Text));
                    ddlWorkFlowSuperior.Items.Insert(0, "");
                    ddlWorkFlowSuperior.Items[0].Value = "";
                    ddlWorkFlowSuperior.Items[0].Text = "Informe o Superior";

                    //Montando a TreeView dos workflows
                    SServiceDesk.Negocio.ClsWorkFlow.populaNoRaiz(0, trvWorkFlow, null, String.Empty, Convert.ToInt32(lblCodigo.Text));
                    trvWorkFlow.ExpandAll();

                    exibeGridViewRepercusao(lblCodigo.Text);
                }

                objRow = null;

            }
            else if (e.CommandName == "Excluir")
            {

                GridViewRow objRow = objGridView.Rows[Convert.ToInt32(e.CommandArgument)];

                if (objRow != null)
                {

                    Label lblCodigo = (Label)objRow.FindControl("lblCodigo");
                    Label lblTabela = (Label)objRow.FindControl("lblTabela");

                    try
                    {
                        //Excluindo todos os workflow com o tipo
                        SServiceDesk.Negocio.ClsWorkFlow.exclui(Convert.ToInt32(lblCodigo.Text));

                        SServiceDesk.Negocio.ClsWorkFlowTipo objWorkFlowTipo = new SServiceDesk.Negocio.ClsWorkFlowTipo(Convert.ToInt32(lblCodigo.Text));
                        objWorkFlowTipo.exclui();
                        objWorkFlowTipo = null;

                        //Populando a Grid View dos Tipos de WorkFlow
                        SServiceDesk.Negocio.ClsWorkFlowTipo.geraGridView(gvWorkFlowTipo);
                    }
                    catch
                    {
                        strMensagem = "Não foi possível excluir o workflow. Existem repercusão associado ao estado do workflow.";
                        lblMensagem.Text = strMensagem;
                        imgIcone.ImageUrl = "images/icones/aviso.gif";
                        lblMensagem.Visible = true;
                        divMensagem.Visible = true;
                    }
                }

                objRow = null;

            }
            objGridView = null;
        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, user.IDusuario.ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region Evento gvWorkFlowTipo_PageIndexChanging
    /// <summary>
    /// Evento que ocorre quando é mudada a página da GridView
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvWorkFlowTipo_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView objGridView = (GridView)sender;
        objGridView.PageIndex = e.NewPageIndex;
        SServiceDesk.Negocio.ClsWorkFlowTipo.geraGridView(objGridView);
        objGridView = null;
    }
    #endregion

    #region Evento gvWorkFlowTipo_RowDataBound
    /// <summary>
    /// Evento que ocorre na montagem da GridView
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvWorkFlowTipo_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            int intCodigoAtributo = 0;

            if (Request.QueryString["codigo"] != null)
            {
                intCodigoAtributo = Convert.ToInt32(Request.QueryString["codigo"].ToString().Trim());
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                try
                {
                    Label lblCodigo = (Label)e.Row.FindControl("lblCodigo");

                    //adiciona um evento javascript no botão Excluir
                    ImageButton btnExcluir = (ImageButton)e.Row.Cells[5].Controls[0];
                    btnExcluir.Attributes.Add("onclick", "return verifica();");

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

    #region Evento gvWorkFlowTipo_OnRowEditing
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvWorkFlowTipo_OnRowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView objGridView = (GridView)sender;
        objGridView.EditIndex = e.NewEditIndex;
        SServiceDesk.Negocio.ClsWorkFlowTipo.geraGridView(objGridView);
    }
    #endregion

    #region Evento gvWorkFlowTipo_OnRowCancelingEdit
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvWorkFlowTipo_OnRowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView objGridView = (GridView)sender;
        objGridView.EditIndex = -1;
        SServiceDesk.Negocio.ClsWorkFlowTipo.geraGridView(objGridView);
    }
    #endregion

    #region Evento gvWorkFlowTipo_OnRowUpdating
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvWorkFlowTipo_OnRowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            GridView objGridView = (GridView)sender;
            String strMensagem = String.Empty;

            GridViewRow objRow = objGridView.Rows[e.RowIndex];

            if (objRow != null)
            {
                Label lblCodigo = (Label)objRow.FindControl("lblCodigo");
                TextBox txtDescricao = (TextBox)objRow.FindControl("txtDescricao");
                TextBox txtTabela = (TextBox)objRow.FindControl("txtTabela");

                SServiceDesk.Negocio.ClsWorkFlowTipo objWorkFlowTipo = new SServiceDesk.Negocio.ClsWorkFlowTipo(Convert.ToInt32(lblCodigo.Text));
                objWorkFlowTipo.Descricao.Valor = ServiceDesk.Generica.ClsTexto.trocaAspaPorHtml(txtDescricao.Text.Trim());
                objWorkFlowTipo.Tabela.Valor = ServiceDesk.Generica.ClsTexto.trocaAspaPorHtml(txtTabela.Text.Trim());

                if (objWorkFlowTipo.existeDescricao())
                {
                    strMensagem = "Não foi possível inserir o Workflow. A descrição informada já existe.";
                }
                else if (objWorkFlowTipo.existeTabela())
                {
                    strMensagem = "Não foi possível inserir o Workflow. A tabela informada já existe.";
                }
                else
                {
                    objWorkFlowTipo.altera(out strMensagem);
                }

                lblMensagem.Text = strMensagem;
                imgIcone.ImageUrl = "images/icones/aviso.gif";
                lblMensagem.Visible = true;
                divMensagem.Visible = true;

                objWorkFlowTipo = null;

                objGridView.EditIndex = -1;
                SServiceDesk.Negocio.ClsWorkFlowTipo.geraGridView(objGridView);

            }

            objGridView = null;
        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, user.IDusuario.ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region Evento gvWorkFlowTipo_Load
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvWorkFlowTipo_Load(object sender, EventArgs e)
    {
        try
        {
            GridView objGridView = (GridView)sender;
            if (objGridView.Rows.Count == 0)
            {
                novoWorkFlowTipo(sender, e);
            }
            objGridView = null;
        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, user.IDusuario.ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region Evento novoWorkFlowTipo
    /// <summary>
    /// Disponibiliza a inserção de um novo Tipo de WorkFlow
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void novoWorkFlowTipo(object sender, EventArgs e)
    {
        pnNovoWorkFlowTipo.Visible = true;
    }
    #endregion

    #region Evento insereWorkFlowTipo
    /// <summary>
    /// Salva um Tipo de WorkFlow
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void insereWorkFlowTipo(object sender, EventArgs e)
    {
        try
        {
            String strMensagem = String.Empty;

            SServiceDesk.Negocio.ClsWorkFlowTipo objWorkFlowTipo = new SServiceDesk.Negocio.ClsWorkFlowTipo();
            ServiceDesk.Negocio.ClsIdentificador objIdentificador = new ServiceDesk.Negocio.ClsIdentificador();

            objIdentificador.Tabela.Valor = objWorkFlowTipo.Atributos.NomeTabela;
            objWorkFlowTipo.Codigo.Valor = objIdentificador.getProximoValor().ToString();
            objWorkFlowTipo.Descricao.Valor = ServiceDesk.Generica.ClsTexto.trocaAspaPorHtml(txtDescricao.Text.Trim());
            objWorkFlowTipo.Tabela.Valor = txtTabela.Text.Trim();
            objWorkFlowTipo.FlagPadrao.Valor = "N";

            if (SServiceDesk.Negocio.ClsWorkFlowTipo.existeDescricao(objWorkFlowTipo.Descricao.Valor))
            {
                lblMensagem.Text = "Não foi possível inserir o Workflow. A descrição informada já existe.";
                imgIcone.ImageUrl = "images/icones/aviso.gif";
                lblMensagem.Visible = true;
                divMensagem.Visible = true;
            }
            else if (SServiceDesk.Negocio.ClsWorkFlowTipo.existeTabela(objWorkFlowTipo.Tabela.Valor))
            {
                lblMensagem.Text = "Não foi possível inserir o Workflow. A tabela informada já existe.";
                imgIcone.ImageUrl = "images/icones/aviso.gif";
                lblMensagem.Visible = true;
                divMensagem.Visible = true;
            }
            else if (objWorkFlowTipo.insere(out strMensagem))
            {
                //Atualizando o valor na tabela identificador
                objIdentificador.atualizaValor();

                //Informando a mensagem do status da operação
                Session["strStatus"] = strMensagem;

                Response.Redirect("workflow.aspx");
            }
            else
            {
                lblMensagem.Text = strMensagem;
                imgIcone.ImageUrl = "images/icones/aviso.gif";
                lblMensagem.Visible = true;
                divMensagem.Visible = true;

                //Mantem visivel o painel de insercao do tipo do workflow
                pnNovoWorkFlowTipo.Visible = true;

            }

            objIdentificador = null;
            objWorkFlowTipo = null;
        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, user.IDusuario.ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region Evento salvaWorkFlow
    /// <summary>
    /// Evento que salva um WorkFlow(insere/atualiza)
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void salvaWorkFlow(object sender, EventArgs e)
    {
        try
        {
            Label lblCodigo = (Label)gvWorkFlowTipo.Rows[gvWorkFlowTipo.SelectedIndex].FindControl("lblCodigo");

            if (lblCodigoWorkFlow.Text == String.Empty)
            {
                insereWorkFlow();
            }
            else
            {
                alteraWorkFlow(Convert.ToInt32(lblCodigoWorkFlow.Text));
            }
            pnNovoWorkFlow.Visible = true;
            //Montando a TreeView dos workflows
            SServiceDesk.Negocio.ClsWorkFlow.populaNoRaiz(0, trvWorkFlow, null, String.Empty, Convert.ToInt32(lblCodigo.Text));
            trvWorkFlow.ExpandAll();
        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, user.IDusuario.ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region Metodo insereWorkFlow
    /// <summary>
    /// Salva um WorkFlow
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void insereWorkFlow()
    {
        try
        {
            String strMensagem = String.Empty;
            String strMensagemRepercusao = String.Empty;

            //=================================================================================//
            // - O que: Validas os dados a serem inseridos no banco de dados.
            // - Quem: Fernanda Passos
            // - Quando: 15/03/2006 ás14:41hs
            //=================================================================================//
            if (ValidaDados(out strMensagem) == false)
            {
                Mensagem(true, strMensagem.Trim(), false);
                return;
            }
            //=================================================================================//

            SServiceDesk.Negocio.ClsWorkFlow objWorkFlow = new SServiceDesk.Negocio.ClsWorkFlow();
            ServiceDesk.Negocio.ClsIdentificador objIdentificador = new ServiceDesk.Negocio.ClsIdentificador();

            Label lblCodigoTipo = (Label)gvWorkFlowTipo.Rows[gvWorkFlowTipo.SelectedIndex].FindControl("lblCodigo");
            Label lblTabela = (Label)gvWorkFlowTipo.Rows[gvWorkFlowTipo.SelectedIndex].FindControl("lblTabela");

            objIdentificador.Tabela.Valor = objWorkFlow.Atributos.NomeTabela;
            objWorkFlow.Codigo.Valor = objIdentificador.getProximoValor().ToString();
            objWorkFlow.Tipo.Valor = ((Label)gvWorkFlowTipo.Rows[gvWorkFlowTipo.SelectedIndex].FindControl("lblCodigo")).Text.Trim();
            objWorkFlow.Descricao.Valor = ServiceDesk.Generica.ClsTexto.trocaAspaPorHtml(txtWorkFlowDescricao.Text.Trim());
            objWorkFlow.PreCondicao.Valor = ServiceDesk.Generica.ClsTexto.trocaAspaPorHtml(txtWorkFlowPreCondicao.Text.Trim()).Trim();
            objWorkFlow.Superior.Valor = ddlWorkFlowSuperior.SelectedValue;
            objWorkFlow.Status.Valor = ddlWorkFlowStatus.SelectedValue;
            objWorkFlow.FlagSemiAutomatico.Valor = ServiceDesk.Generica.ClsTexto.trocaAspaPorHtml(ddlSemiAutomatico.SelectedValue.Trim()).Trim();

            if (objWorkFlow.Descricao.Valor == String.Empty)
            {
                objWorkFlow.Descricao.Valor = ServiceDesk.Negocio.ClsStatus.getDescricaoStatus(objWorkFlow.Status.Valor);
            }

            if (objWorkFlow.insere(out strMensagem))
            {
                //Atualizando o valor na tabela identificador
                objIdentificador.atualizaValor();

                objWorkFlow.atualizaChave();
                objWorkFlow.altera(out strMensagem);

                //Verificando se deve ser inserido a Repercusao do WorkFlow
                if ((ddlRepercusaoTabela.SelectedValue != String.Empty) && (ddlRepercusaoStatus.SelectedValue != String.Empty))
                {
                    //Inserindo a repercusao
                    SServiceDesk.Negocio.ClsWorkFlowRepercusao objWorkFlowRepercusao = new SServiceDesk.Negocio.ClsWorkFlowRepercusao();
                    objIdentificador.Tabela.Valor = objWorkFlowRepercusao.Atributos.NomeTabela;
                    objWorkFlowRepercusao.Codigo.Valor = objIdentificador.getProximoValor().ToString();
                    objWorkFlowRepercusao.WorkFlow.Valor = objWorkFlow.Codigo.Valor;
                    objWorkFlowRepercusao.TabelaOrigem.Valor = lblTabela.Text;
                    objWorkFlowRepercusao.StatusOrigem.Valor = objWorkFlow.Status.Valor;
                    objWorkFlowRepercusao.TabelaDestino.Valor = ddlRepercusaoTabela.SelectedValue;
                    objWorkFlowRepercusao.StatusDestino.Valor = ddlRepercusaoStatus.SelectedValue;
                    if (objWorkFlowRepercusao.insere(out strMensagemRepercusao))
                    {
                        objIdentificador.atualizaValor();
                    }

                    exibeGridViewRepercusao(objWorkFlow.Codigo.Valor);

                }

                //Informando a mensagem do status da operação
                Session["strStatus"] = strMensagem;

                //pnNovoWorkFlow.Visible = false;

            }
            else
            {
                lblMensagem.Text = strMensagem;
                imgIcone.ImageUrl = "images/icones/aviso.gif";
                lblMensagem.Visible = true;
                divMensagem.Visible = true;
            }

            //Montando o dropdownlist dos workflows Superiores
            SServiceDesk.Negocio.ClsWorkFlow.geraDropDownListPorTipo(ddlWorkFlowSuperior, Convert.ToInt32(lblCodigoTipo.Text));
            ddlWorkFlowSuperior.Items.Insert(0, "");
            ddlWorkFlowSuperior.Items[0].Value = "";
            ddlWorkFlowSuperior.Items[0].Text = "Informe o Superior";

            objIdentificador = null;
            objWorkFlow = null;
        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, user.IDusuario.ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region Metodo alteraWorkFlow
    /// <summary>
    /// Altera os dados de um WorkFlow
    /// </summary>
    /// <param name="intCodigoWorkFlow"></param>
    private void alteraWorkFlow(int intCodigoWorkFlow)
    {
        try
        {
            String strMensagem = String.Empty;
            String strMensagemRepercusao = String.Empty;

            //=================================================================================//
            // - O que: Validas os dados a serem inseridos no banco de dados.
            // - Quem: Fernanda Passos
            // - Quando: 15/03/2006 ás14:41hs
            //=================================================================================//
            if (ValidaDados(out strMensagem) == false)
            {
                Mensagem(true, strMensagem.Trim(), false);
                return;
            }
            //=================================================================================//

            Label lblTabela = (Label)gvWorkFlowTipo.Rows[gvWorkFlowTipo.SelectedIndex].FindControl("lblTabela");

            SServiceDesk.Negocio.ClsWorkFlow objWorkFlow = new SServiceDesk.Negocio.ClsWorkFlow(intCodigoWorkFlow);
            objWorkFlow.Descricao.Valor = ServiceDesk.Generica.ClsTexto.trocaAspaPorHtml(txtWorkFlowDescricao.Text.Trim());
            objWorkFlow.PreCondicao.Valor = ServiceDesk.Generica.ClsTexto.trocaAspaPorHtml(txtWorkFlowPreCondicao.Text.Trim()).Trim();
            objWorkFlow.FlagSemiAutomatico.Valor = ServiceDesk.Generica.ClsTexto.trocaAspaPorHtml(ddlSemiAutomatico.SelectedValue.Trim()).Trim();

            if (objWorkFlow.Descricao.Valor == String.Empty)
            {
                objWorkFlow.Descricao.Valor = ServiceDesk.Negocio.ClsStatus.getDescricaoStatus(objWorkFlow.Status.Valor);
            }

            if (objWorkFlow.altera(out strMensagem))
            {
                //Verificando se deve ser inserido a Repercusao do WorkFlow
                if ((ddlRepercusaoTabela.SelectedValue != String.Empty) && (ddlRepercusaoStatus.SelectedValue != String.Empty))
                {
                    //Inserindo a repercusao
                    SServiceDesk.Negocio.ClsWorkFlowRepercusao objWorkFlowRepercusao = new SServiceDesk.Negocio.ClsWorkFlowRepercusao();
                    ServiceDesk.Negocio.ClsIdentificador objIdentificador = new ServiceDesk.Negocio.ClsIdentificador();

                    objIdentificador.Tabela.Valor = objWorkFlowRepercusao.Atributos.NomeTabela;
                    objWorkFlowRepercusao.Codigo.Valor = objIdentificador.getProximoValor().ToString();
                    objWorkFlowRepercusao.WorkFlow.Valor = objWorkFlow.Codigo.Valor;
                    objWorkFlowRepercusao.TabelaOrigem.Valor = lblTabela.Text;
                    objWorkFlowRepercusao.StatusOrigem.Valor = objWorkFlow.Status.Valor;
                    objWorkFlowRepercusao.TabelaDestino.Valor = ddlRepercusaoTabela.SelectedValue;
                    objWorkFlowRepercusao.StatusDestino.Valor = ddlRepercusaoStatus.SelectedValue;
                    if (objWorkFlowRepercusao.insere(out strMensagemRepercusao))
                    {
                        objIdentificador.atualizaValor();
                    }

                    exibeGridViewRepercusao(objWorkFlow.Codigo.Valor);

                }
            }

            lblMensagem.Text = strMensagem;
            imgIcone.ImageUrl = "images/icones/aviso.gif";
            lblMensagem.Visible = true;
            divMensagem.Visible = true;

            objWorkFlow = null;
        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, user.IDusuario.ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region Evento novoWorkFlow
    /// <summary>
    /// Deixa disponível a opção de inserir um novo WorkFlow
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void novoWorkFlow(object sender, EventArgs e)
    {
        pnNovoWorkFlow.Visible = true;
        lblCodigoWorkFlow.Text = String.Empty;
        ddlWorkFlowStatus.Enabled = true;
        ddlWorkFlowStatus.SelectedIndex = -1;
        ddlWorkFlowSuperior.Enabled = true;
        ddlWorkFlowSuperior.SelectedIndex = -1;
        txtWorkFlowDescricao.Text = String.Empty;
        txtWorkFlowPreCondicao.Text = String.Empty;
        ddlRepercusaoStatus.SelectedIndex = -1;
        ddlRepercusaoTabela.SelectedIndex = -1;
    }
    #endregion

    #region Evento populaNos
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void populaNos(object sender, TreeNodeEventArgs e)
    {
        try
        {
            Label lblCodigo = (Label)gvWorkFlowTipo.Rows[gvWorkFlowTipo.SelectedIndex].FindControl("lblCodigo");
            SServiceDesk.Negocio.ClsWorkFlow.populaNoRaiz(Convert.ToInt32(e.Node.Value), null, e.Node, String.Empty, Convert.ToInt32(lblCodigo.Text));
        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, user.IDusuario.ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region Evento selecionaNo
    /// <summary>
    /// Evento que ocorre quando se clica em um determinado no da TreeView
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void selecionaNo(object sender, EventArgs e)
    {
        try
        {
            TreeView objTreeView = (TreeView)sender;

            if (objTreeView != null)
            {
                if ((lblCodigoWorkFlow != null) && (objTreeView.SelectedNode != null))
                {
                    lblCodigoWorkFlow.Text = objTreeView.SelectedNode.Value;
                }

                pnNovoWorkFlow.Visible = true;
                ddlWorkFlowStatus.Enabled = false;
                ddlWorkFlowSuperior.Enabled = false;

                if (lblCodigoWorkFlow.Text != String.Empty)
                {

                    exibeGridViewRepercusao(lblCodigoWorkFlow.Text);

                    SServiceDesk.Negocio.ClsWorkFlow objWorkFlow = new SServiceDesk.Negocio.ClsWorkFlow(Convert.ToInt32(lblCodigoWorkFlow.Text));

                    txtWorkFlowDescricao.Text = objWorkFlow.Descricao.Valor;
                    txtWorkFlowPreCondicao.Text = objWorkFlow.PreCondicao.Valor;

                    if (objWorkFlow.FlagSemiAutomatico.Valor.Trim() == string.Empty || objWorkFlow.FlagSemiAutomatico.Valor.Trim() == "N") ddlSemiAutomatico.SelectedValue = "N"; else ddlSemiAutomatico.SelectedValue = "S";

                    if (objWorkFlow.Status.Valor != String.Empty)
                    {
                        ddlWorkFlowStatus.SelectedValue = objWorkFlow.Status.Valor;
                    }
                    if (objWorkFlow.Superior.Valor != String.Empty)
                    {
                        ddlWorkFlowSuperior.SelectedValue = objWorkFlow.Superior.Valor;
                    }

                    objWorkFlow = null;
                }

            }
            objTreeView = null;
        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, user.IDusuario.ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region Evento excluiWorkFlow
    /// <summary>
    /// Exclui os WorkFlows selecionados na TreeView
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void excluiWorkFlow(object sender, EventArgs e)
    {
        try
        {
            int intI = 0;
            TreeNodeCollection objTreeNodeCollection = trvWorkFlow.CheckedNodes;
            Label lblCodigo = (Label)gvWorkFlowTipo.Rows[gvWorkFlowTipo.SelectedIndex].FindControl("lblCodigo");

            for (intI = (objTreeNodeCollection.Count - 1); intI > -1; intI--)
            {
                try
                {
                    ///TODO: Não pode permitir a exclusão caso o workflow esteja sendo utilizado
                    SServiceDesk.Negocio.ClsWorkFlow objWorkFlow = new SServiceDesk.Negocio.ClsWorkFlow(Convert.ToInt32(objTreeNodeCollection[intI].Value));
                    objWorkFlow.exclui();
                    objWorkFlow = null;
                }
                catch
                {
                    lblMensagem.Text = "Não é possível excluir o fluxo do WorkFlow. Existe repercursão associada ao mesmo.";
                    imgIcone.ImageUrl = "images/icones/aviso.gif";
                    lblMensagem.Visible = true;
                    divMensagem.Visible = true;
                }
            }
            objTreeNodeCollection = null;

            pnNovoWorkFlow.Visible = true;

            //Montando a TreeView dos workflows
            SServiceDesk.Negocio.ClsWorkFlow.populaNoRaiz(0, trvWorkFlow, null, String.Empty, Convert.ToInt32(lblCodigo.Text));
            trvWorkFlow.ExpandAll();
        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, user.IDusuario.ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region Evento marcaNo
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void marcaNo(object sender, TreeNodeEventArgs e)
    {
        try
        {
            Label lblCodigo = (Label)gvWorkFlowTipo.Rows[gvWorkFlowTipo.SelectedIndex].FindControl("lblCodigo");

            marcaFilhos(e.Node, e.Node.Checked);

            pnNovoWorkFlow.Visible = true;
        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, user.IDusuario.ToString(), this.Request.Path, "0", ex.ToString());
        }

    }
    #endregion

    #region metodo marcaFilhos
    /// <summary>
    /// 
    /// </summary>
    /// <param name="e"></param>
    /// <param name="check"></param>
    private void marcaFilhos(TreeNode objTreeNode, bool bolSelecionado)
    {
        try
        {
            objTreeNode.Checked = bolSelecionado;
            foreach (TreeNode objNode in objTreeNode.ChildNodes)
            {
                marcaFilhos(objNode, bolSelecionado);
            }
        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, user.IDusuario.ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region evento gvWorkFlowRepercusao_OnRowCommand
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvWorkFlowRepercusao_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            GridView objGridView = (GridView)sender;
            if (e.CommandName == "Excluir")
            {
                GridViewRow objRow = (GridViewRow)objGridView.Rows[Convert.ToInt32(e.CommandArgument)];
                if (objRow != null)
                {
                    Label lblCodigo = (Label)objRow.FindControl("lblCodigo");
                    SServiceDesk.Negocio.ClsWorkFlowRepercusao objWorkFlowRepercusao = new SServiceDesk.Negocio.ClsWorkFlowRepercusao(Convert.ToInt32(lblCodigo.Text));
                    objWorkFlowRepercusao.exclui();
                    objWorkFlowRepercusao = null;

                    lblMensagem.Text = "Repercursão do workflow excluída com sucesso.";
                    imgIcone.ImageUrl = "images/icones/aviso.gif";
                    lblMensagem.Visible = true;
                    divMensagem.Visible = true;

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

    #region evento gvWorkFlowRepercusao_RowDataBound
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvWorkFlowRepercusao_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblStatusCodigo = (Label)e.Row.FindControl("lblStatusCodigo");
                Label lblStatus = (Label)e.Row.FindControl("lblStatus");

                ServiceDesk.Negocio.ClsStatus objStatus = new ServiceDesk.Negocio.ClsStatus(Convert.ToInt32(lblStatusCodigo.Text));
                lblStatus.Text = objStatus.Descricao.Valor;
                objStatus = null;

                //adiciona um evento javascript no botão Excluir
                ImageButton btnExcluir = (ImageButton)e.Row.Cells[3].Controls[0];
                btnExcluir.Attributes.Add("onclick", "return verifica();");

            }
        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, user.IDusuario.ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region metodo exibeGridViewRepercusao
    /// <summary>
    /// 
    /// </summary>
    /// <param name="strWorkFlowCodigo"></param>
    private void exibeGridViewRepercusao(String strWorkFlowCodigo)
    {
        try
        {
            if (strWorkFlowCodigo != String.Empty)
            {
                //Monta a GridView com as Repercusoes
                SServiceDesk.Negocio.ClsWorkFlowRepercusao objWorkFlowRepercusao = new SServiceDesk.Negocio.ClsWorkFlowRepercusao();
                objWorkFlowRepercusao.Codigo.CampoIdentificador = false;
                objWorkFlowRepercusao.WorkFlow.Valor = strWorkFlowCodigo;
                objWorkFlowRepercusao.WorkFlow.CampoIdentificador = true;
                SServiceDesk.Negocio.ClsWorkFlowRepercusao.geraGridView(gvWorkFlowRepercusao, objWorkFlowRepercusao, true);
                objWorkFlowRepercusao = null;
            }
        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, user.IDusuario.ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region Valida as informações
    /// <summary>
    /// Validação dos dados da tela.
    /// </summary>
    /// <param name="strMensagem"></param>
    /// <returns></returns>
    public bool ValidaDados(out string strMensagem)
    {
        bool bolRetorno = true;
        strMensagem = string.Empty;

        try
        {
            //=======================================================================================================
            // - O que: Verifica se foi selecionado semi automatico para status sem pré-condição, pois
            // somente pode ser semi automático com houver pré-condição.
            // - Que: Fernanda Passos
            // - Quando: 15/03/2006 ás 14:28hs
            //=======================================================================================================
            if (txtWorkFlowPreCondicao.Text.Trim() == string.Empty && ddlSemiAutomatico.SelectedValue.Trim() == "S")
            {
                strMensagem = "Um status somente poderá ser semi-automático quando houver pré-condição.";
                bolRetorno = false;
            }
            //=======================================================================================================

            return bolRetorno;
        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, user.IDusuario.ToString(), this.Request.Path, "0", ex.ToString());
            return bolRetorno;
        }
    }
    #endregion

    #region Mensagem
    /// <summary>
    /// Mensagem
    /// </summary>
    public void Mensagem(bool bolExibeMensagem, string strMensagem, bool bolMensagemCritica)
    {
        try
        {
            if (strMensagem.Trim() != string.Empty && bolExibeMensagem == true)
            {
                lblMensagem.Text = strMensagem.Trim();
                if (bolMensagemCritica == true) imgIcone.ImageUrl = "images/icones/aviso.gif"; else imgIcone.ImageUrl = "images/icones/info.gif";
                lblMensagem.Visible = true;
                divMensagem.Visible = true;
            }
            else if (bolExibeMensagem == false)
            {
                divMensagem.Visible = false;
                lblMensagem.Visible = false;
            }
        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, user.IDusuario.ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion
}