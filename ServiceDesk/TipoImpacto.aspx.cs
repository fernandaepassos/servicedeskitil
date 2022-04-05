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

public partial class TipoImpacto : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            CheckAcesso(11);

            int intCodigoImpacto = 0;

            //Mantém a posição do Scroll após o PostBack
            Page.MaintainScrollPositionOnPostBack = true;

            //Esconde a mensagem de erro
            divMensagem.Visible = false;

            if (!Page.IsPostBack)
            {
                //Gerando o GridView com as Origens dos Chamados
                ClsTipoImpacto.geraGridView(gvImpacto);

                if (Request.QueryString["codigo"] != null)
                {
                    intCodigoImpacto = Convert.ToInt32(Request.QueryString["codigo"].ToString().Trim());

                    //Monta os dados na tela
                    CarregaDescricaoImpacto(intCodigoImpacto);
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
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, user.IDusuario.ToString(), this.Request.Path, "0", ex.ToString());
        }
    }

    #region metodo CarregaDescricaoImpacto
    /// <summary>
    /// Método que popula o campo descrição da Origem do Chamado
    /// </summary>
    /// <param name="intCodigoItem">Código da Origem do Chamado</param>
    private void CarregaDescricaoImpacto(int intCodigoImpacto)
    {
        try
        {
            ServiceDesk.Negocio.ClsTipoImpacto objTipoImpacto = new ServiceDesk.Negocio.ClsTipoImpacto(intCodigoImpacto);
            txtDescricao.Text = objTipoImpacto.Descricao.Valor;
            objTipoImpacto = null;
        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, user.IDusuario.ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region Salva o Impacto
    /// <summary>
    /// Método que salva o Impacto (inserir/alterar)
    /// </summary>
    protected void btnSalvar_Click(object sender, EventArgs e)
    {
        try
        {
            if (Request.QueryString["codigo"] == null)
            {
                insereImpacto();
            }
            else
            {
                if (Request.QueryString["codigo"] != null)
                {
                    alteraImpacto(Convert.ToInt32(Request.QueryString["codigo"].ToString().Trim()));
                }
            }
        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, user.IDusuario.ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region metodo insereImpacto
    /// <summary>
    /// Método que insere uma nova Origem do Chamado
    /// </summary>
    private void insereImpacto()
    {
        try
        {

            String strMensagem = String.Empty;

            ServiceDesk.Negocio.ClsTipoImpacto objTipoImpacto = new ServiceDesk.Negocio.ClsTipoImpacto();
            ServiceDesk.Negocio.ClsIdentificador objIdentificador = new ServiceDesk.Negocio.ClsIdentificador();

            objIdentificador.Tabela.Valor = objTipoImpacto.Atributos.NomeTabela;
            objTipoImpacto.Codigo.Valor = objIdentificador.getProximoValor().ToString();
            objTipoImpacto.Descricao.Valor = ServiceDesk.Generica.ClsTexto.trocaAspaPorHtml(txtDescricao.Text);

            if (objTipoImpacto.insere(out strMensagem))
            {
                //Atualizando o valor na tabela identificador
                objIdentificador.atualizaValor();

                //Informando a mensagem do status da operação
                Session["strStatus"] = "Impacto inserido com sucesso.";

                Response.Redirect("TipoImpacto.aspx?codigo=" + objTipoImpacto.Codigo.Valor);
            }
            else
            {
                lblMensagem.Text = "Não foi possivel realizar a operação.<br />" + strMensagem;
                imgIcone.ImageUrl = "images/icones/aviso.gif";
                divMensagem.Visible = true;
            }

            objTipoImpacto = null;
            objIdentificador = null;
        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, user.IDusuario.ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region metodo alteraImpacto
    /// <summary>
    /// Método que altera um Impacto
    /// </summary>
    /// <param name="intCodigo">Código do Impácto</param>
    private void alteraImpacto(int intCodigoImpacto)
    {
        try
        {
            String strMensagem = String.Empty;

            ServiceDesk.Negocio.ClsTipoImpacto objTipoImpacto = new ServiceDesk.Negocio.ClsTipoImpacto(intCodigoImpacto);
            objTipoImpacto.Descricao.Valor = ServiceDesk.Generica.ClsTexto.trocaAspaPorHtml(txtDescricao.Text);

            if (objTipoImpacto.altera(out strMensagem))
            {
                Session["strStatus"] = "Impacto alterado com sucesso.";
            }
            else
            {
                lblMensagem.Text = strMensagem;
                imgIcone.ImageUrl = "images/icones/aviso.gif";
                divMensagem.Visible = true;
            }
            objTipoImpacto = null;

            //Gerando o GridView com as Origens dos Chamados
            ServiceDesk.Negocio.ClsTipoImpacto.geraGridView(gvImpacto);
        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, user.IDusuario.ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region Evento gvImpacto_OnRowCommand
    /// <summary>
    /// Evento que ocorre quando um comando da GridView é solicitado
    /// </summary>
    protected void gvImpacto_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Editar")
            {
                GridViewRow objRow = gvImpacto.Rows[Convert.ToInt32(e.CommandArgument)];
                if (objRow != null)
                {
                    Label lblCodigo = (Label)objRow.FindControl("lblCodigo");
                    Response.Redirect("TipoImpacto.aspx?codigo=" + lblCodigo.Text, false);
                }
                objRow = null;
                ServiceDesk.Negocio.ClsTipoImpacto.geraGridView(gvImpacto);
            }

            if (e.CommandName == "Excluir")
            {
                GridViewRow objRow = gvImpacto.Rows[Convert.ToInt32(e.CommandArgument)];
                if (objRow != null)
                {
                    Label lblCodigo = (Label)objRow.FindControl("lblCodigo");

                    ServiceDesk.Negocio.ClsTipoImpacto objTipoImpacto = new ServiceDesk.Negocio.ClsTipoImpacto(Convert.ToInt32(lblCodigo.Text));
                    try
                    {
                        if (objTipoImpacto.exclui())
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
                    objTipoImpacto = null;
                }
                ServiceDesk.Negocio.ClsTipoImpacto.geraGridView(gvImpacto);
            }
        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, user.IDusuario.ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region Evento gvImpacto_PageIndexChanging
    /// <summary>
    /// Evento que ocorre quando é mudada a página da GridView
    /// </summary>
    protected void gvImpacto_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView objGridView = (GridView)sender;
        objGridView.PageIndex = e.NewPageIndex;
        objGridView = null;
    }
    #endregion

    #region Evento gvImpacto_RowDataBound
    /// <summary>
    /// Evento que ocorre na montagem da GridView
    /// </summary>
    protected void gvImpacto_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            int intCodigoImpacto = 0;

            if (Request.QueryString["codigo"] != null)
            {
                intCodigoImpacto = Convert.ToInt32(Request.QueryString["codigo"].ToString().Trim());
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                try
                {
                    ImageButton btnExcluir = (ImageButton)e.Row.Cells[3].Controls[0];
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
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, user.IDusuario.ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region LimparCampo Descrição
    /// <summary>
    /// Método que limpa o campo descrição 
    /// </summary>
    protected void btnLimpar_Click(object sender, EventArgs e)
    {
        txtDescricao.Text = String.Empty;
        Response.Redirect("TipoImpacto.aspx");
    }
    #endregion

}
