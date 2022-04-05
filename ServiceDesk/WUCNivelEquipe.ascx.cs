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

public partial class WUCNivelEquipe : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            int intCodigoNivelEquipe = 0;

            //Mantém a posição do Scroll após o PostBack
            Page.MaintainScrollPositionOnPostBack = true;

            //Esconde a mensagem de erro
            divMensagem.Visible = false;

            if (!Page.IsPostBack)
            {
                //Gerando o GridView de NivelEquipe
                ServiceDesk.Negocio.ClsEquipeNivel.geraGridView(gvNivelEquipe);

                ServiceDesk.Negocio.ClsEquipePessoa objEquipPessoa = new ServiceDesk.Negocio.ClsEquipePessoa();
                ddlEmpresa.DataSource = objEquipPessoa.geraDropDownEmpresa();
                ddlEmpresa.DataTextField = "descricao";
                ddlEmpresa.DataValueField = "estrutura_codigo";
                ddlEmpresa.DataBind();
                ddlEmpresa.Items.Insert(0, "--");
                ddlEmpresa.Items[0].Value = "";

                if (Request.QueryString["codigo"] != null)
                {
                    intCodigoNivelEquipe = Convert.ToInt32(Request.QueryString["codigo"].ToString().Trim());
                    //Monta os dados na tela
                    CarregaNivelEquipe(intCodigoNivelEquipe);
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
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO,ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());

        }
    }
   
    protected void ddlEmpresa_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlPessoa.Items.Clear();
        SServiceDesk.Negocio.ClsPessoa.geraDropDownList(ddlPessoa, ddlEmpresa.SelectedValue);
        ddlPessoa.Items.Insert(0, "--");
        ddlPessoa.Items[0].Value = String.Empty;
    }

    #region metodo CarregaNivelEquipe
    /// <summary>
    /// Método que popula os campos (Descrição, Empresa e Pessoa)
    /// </summary>
    /// <param name="intCodigoNivelEquipe">Código do Nível da Equipe</param>
    private void CarregaNivelEquipe(int intCodigoNivelEquipe)
    {
        try
        {
            SServiceDesk.Negocio.ClsPessoa objPessoa = new SServiceDesk.Negocio.ClsPessoa();

            ServiceDesk.Negocio.ClsEquipeNivel objNivelEquipe = new ServiceDesk.Negocio.ClsEquipeNivel(intCodigoNivelEquipe);
            txtDescricao.Text = objNivelEquipe.Descricao.Valor;
            ddlEmpresa.SelectedValue = objPessoa.GetEmpresaUsuario(objNivelEquipe.PessoaCodigoGerente.Valor);

            ddlPessoa.Items.Clear();
            SServiceDesk.Negocio.ClsPessoa.geraDropDownList(ddlPessoa, ddlEmpresa.SelectedValue);
            ddlPessoa.Items.Insert(0, "--");
            ddlPessoa.Items[0].Value = String.Empty;

            ddlPessoa.SelectedValue = objNivelEquipe.PessoaCodigoGerente.Valor;

            objNivelEquipe = null;
            objPessoa = null;
        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO,ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region Salva o Nivel da Equipe
    /// <summary>
    /// Método que salva o Nível da Equipe (inserir/alterar)
    /// </summary>
    protected void btnSalvar_Click(object sender, EventArgs e)
    {
        try
        {
            if (Request.QueryString["codigo"] == null)
            {
                insereNivelEquipe();
            }
            else
            {
                if (Request.QueryString["codigo"] != null)
                {
                    alteraNivelEquipe(Convert.ToInt32(Request.QueryString["codigo"].ToString().Trim()));
                }
            }

        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO,ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region metodo insereNivelEquipe
    /// <summary>
    /// Método que insere um novo Nível
    /// </summary>
    private void insereNivelEquipe()
    {
        try
        {
            String strMensagem = String.Empty;

            ServiceDesk.Negocio.ClsEquipeNivel objEquipeNivel = new ServiceDesk.Negocio.ClsEquipeNivel();
            ServiceDesk.Negocio.ClsIdentificador objIdentificador = new ServiceDesk.Negocio.ClsIdentificador();

            objIdentificador.Tabela.Valor = "EquipeNivel";
            objEquipeNivel.Codigo.Valor = objIdentificador.getProximoValor().ToString();
            objEquipeNivel.Descricao.Valor = ServiceDesk.Generica.ClsTexto.trocaAspaPorHtml(txtDescricao.Text);
            objEquipeNivel.PessoaCodigoGerente.Valor = ServiceDesk.Generica.ClsTexto.trocaAspaPorHtml(ddlPessoa.SelectedValue);

            if (objEquipeNivel.insere(out strMensagem))
            {
                //Atualizando o valor na tabela identificador
                objIdentificador.atualizaValor();

                //Informando a mensagem do status da operação
                Session["strStatus"] = "Nivel Equipe inserido com sucesso.";
                Response.Redirect("NivelEquipe.aspx?codigo=" + objEquipeNivel.Codigo.Valor,false);
            }
            else
            {
                lblMensagem.Text = "Não foi possivel realizar a operação.<br />" + strMensagem;
                imgIcone.ImageUrl = "images/icones/aviso.gif";
                divMensagem.Visible = true;
            }

            objEquipeNivel = null;
            objIdentificador = null;

        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO,ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region metodo alteraNivelEquipe
    /// <summary>
    /// Método que altera um Nivel Equipe
    /// </summary>
    /// <param name="intCodigoNivelEquipe">Código do Nível da Equipe</param>
    private void alteraNivelEquipe(int intCodigoNivelEquipe)
    {
        try
        {
            String strMensagem = String.Empty;

            ServiceDesk.Negocio.ClsEquipeNivel objEquipeNivel = new ServiceDesk.Negocio.ClsEquipeNivel(intCodigoNivelEquipe);
            objEquipeNivel.Descricao.Valor = ServiceDesk.Generica.ClsTexto.trocaAspaPorHtml(txtDescricao.Text);
            objEquipeNivel.PessoaCodigoGerente.Valor = ServiceDesk.Generica.ClsTexto.trocaAspaPorHtml(ddlPessoa.SelectedValue);

            if (objEquipeNivel.altera(out strMensagem))
            {
                lblMensagem.Text = "Item atualizado com sucesso";
                imgIcone.ImageUrl = "images/icones/aviso.gif";
                divMensagem.Visible = true;
            }
            else
            {
                lblMensagem.Text = strMensagem;
                imgIcone.ImageUrl = "images/icones/aviso.gif";
                divMensagem.Visible = true;
            }
            objEquipeNivel = null;

            //Gerando o GridView com as Origens dos Chamados
            ServiceDesk.Negocio.ClsEquipeNivel.geraGridView(gvNivelEquipe);

        }
        catch (Exception ex)
        {
                ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO,ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());

        }
    }
    #endregion

    #region Evento gvNivelEquipe_OnRowCommand
    /// <summary>
    /// Evento que ocorre quando um comando da GridView é solicitado
    /// </summary>
    protected void gvNivelEquipe_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Editar")
            {
                GridViewRow objRow = gvNivelEquipe.Rows[Convert.ToInt32(e.CommandArgument)];
                if (objRow != null)
                {
                    Label lblCodigo = (Label)objRow.FindControl("lblCodigo");
                    Response.Redirect("NivelEquipe.aspx?codigo=" + lblCodigo.Text,false);
                }
                objRow = null;
                ServiceDesk.Negocio.ClsEquipeNivel.geraGridView(gvNivelEquipe);
            }

            if (e.CommandName == "Excluir")
            {
                GridViewRow objRow = gvNivelEquipe.Rows[Convert.ToInt32(e.CommandArgument)];
                if (objRow != null)
                {
                    Label lblCodigo = (Label)objRow.FindControl("lblCodigo");
                    ServiceDesk.Negocio.ClsEquipeNivel objNivelEquipe = new ServiceDesk.Negocio.ClsEquipeNivel(Convert.ToInt32(lblCodigo.Text));

                    try
                    {
                        if (objNivelEquipe.exclui())
                        {
                            lblMensagem.Text = "Item excluido com sucesso.";
                            imgIcone.ImageUrl = "images/icones/aviso.gif";
                            divMensagem.Visible = true;

                            txtDescricao.Text = String.Empty;
                        }
                        else
                        {
                            lblMensagem.Text = "Não foi possível realizar a exclusão deste item.<br /> Existem itens realicionados a ele.";
                            imgIcone.ImageUrl = "images/icones/aviso.gif";
                            divMensagem.Visible = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        lblMensagem.Text = "Não foi possível excluir a Origem do Chamado.<br>" + ex.Message;
                        imgIcone.ImageUrl = "images/icones/erro.gif";
                        divMensagem.Visible = true;
                    }

                    objRow = null;
                    objNivelEquipe = null;
                }
                ServiceDesk.Negocio.ClsEquipeNivel.geraGridView(gvNivelEquipe);
            }
        }
        catch (Exception ex)
        {
           ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO,ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region Evento gvNivelEquipe_PageIndexChanging
    /// <summary>
    /// Evento que ocorre quando é mudada a página da GridView
    /// </summary>
    protected void gvNivelEquipe_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            GridView objGridView = (GridView)sender;
            objGridView.PageIndex = e.NewPageIndex;
            objGridView = null;

        }
        catch (Exception ex)
        {
               ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO,ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region Evento gvNivelEquipe_RowDataBound
    /// <summary>
    /// Evento que ocorre na montagem da GridView
    /// </summary>
    protected void gvNivelEquipe_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            int intCodigoNivelEquipe = 0;

            if (Request.QueryString["codigo"] != null)
            {
                intCodigoNivelEquipe = Convert.ToInt32(Request.QueryString["codigo"].ToString().Trim());
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Condicao IF que exibe os dados no GridView (estado: não-editável)
                if ((e.Row.RowState.ToString() == "Normal") || (e.Row.RowState.ToString() == "Alternate"))
                {
                    // Carrega o campo Impacto com a descrição do Impacto e não o código
                    Label lblPessoaCodigoGerente = (Label)e.Row.FindControl("lblPessoaCodigoGerente");
                    if (lblPessoaCodigoGerente.Text != String.Empty)
                    {
                        SServiceDesk.Negocio.ClsPessoa objPessoa = new SServiceDesk.Negocio.ClsPessoa(Convert.ToInt32(lblPessoaCodigoGerente.Text));
                        lblPessoaCodigoGerente.Text = objPessoa.Nome.Valor;
                    }
                }

                ImageButton btnExcluir = (ImageButton)e.Row.Cells[4].Controls[0];
                btnExcluir.Attributes.Add("onclick", "return verifica();");

                Label lblCodigo = (Label)e.Row.Cells[0].Controls[1];
            }
        }
        catch (Exception ex)
        {
             ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO,ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region LimparCampos gvNivelEquipe
    /// <summary>
    /// Método que limpa o campo descrição 
    /// </summary>
    protected void btnLimpar_Click(object sender, EventArgs e)
    {
        txtDescricao.Text = String.Empty;
        ddlEmpresa.SelectedValue = String.Empty;
        ddlPessoa.SelectedValue = String.Empty;
        Response.Redirect("NivelEquipe.aspx");
    }
    #endregion



}
