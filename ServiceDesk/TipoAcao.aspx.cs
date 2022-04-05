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

public partial class Acao : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            int intCodigoAcao = 0;

            //Mant�m a posi��o do Scroll ap�s o PostBack
            Page.MaintainScrollPositionOnPostBack = true;

            //Esconde a mensagem de erro
            divMensagem.Visible = false;

            CheckAcesso(11);

            if (!Page.IsPostBack)
            {
                //Gerando o GridView com as Origens dos Chamados
                ServiceDesk.Negocio.ClsAcao.geraGridView(gvAcao);

                if (Request.QueryString["codigo"] != null)
                {
                    intCodigoAcao = Convert.ToInt32(Request.QueryString["codigo"].ToString().Trim());

                    //Monta os dados na tela
                    CarregaDescricaoAcao(intCodigoAcao);
                }
            }

            if (Session["strStatus"] != null)
            {
                //Exibindo mensagem com o status da opera��o
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

    #region metodo CarregaDescricaoAcao
    /// <summary>
    /// M�todo que popula o campo descri��o da Origem do Chamado
    /// </summary>
    /// <param name="intCodigoItem">C�digo da Origem do Chamado</param>
    private void CarregaDescricaoAcao(int intCodigoAcao)
    {
        try
        {
            ServiceDesk.Negocio.ClsAcao objTipoAcao = new ServiceDesk.Negocio.ClsAcao(intCodigoAcao);
            txtDescricao.Text = objTipoAcao.Descricao.Valor;
            objTipoAcao = null;
        }
        catch (Exception ex)
        {
                ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, user.IDusuario.ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region Salva o Acao
    /// <summary>
    /// M�todo que salva o Acao (inserir/alterar)
    /// </summary>
    protected void btnSalvar_Click(object sender, EventArgs e)
    {
        try
        {
            if (Request.QueryString["codigo"] == null)
            {
                insereAcao();
            }
            else
            {
                if (Request.QueryString["codigo"] != null)
                {
                    alteraAcao(Convert.ToInt32(Request.QueryString["codigo"].ToString().Trim()));
                }
            }
        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, user.IDusuario.ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region metodo insereAcao
    /// <summary>
    /// M�todo que insere uma nova Origem do Chamado
    /// </summary>
    private void insereAcao()
    {
        try
        {

            String strMensagem = String.Empty;

            ServiceDesk.Negocio.ClsAcao objTipoAcao = new ServiceDesk.Negocio.ClsAcao();
            ServiceDesk.Negocio.ClsIdentificador objIdentificador = new ServiceDesk.Negocio.ClsIdentificador();

            objIdentificador.Tabela.Valor = objTipoAcao.Atributos.NomeTabela;
            objTipoAcao.Codigo.Valor = objIdentificador.getProximoValor().ToString();
            objTipoAcao.Descricao.Valor = ServiceDesk.Generica.ClsTexto.trocaAspaPorHtml(txtDescricao.Text);

            if (objTipoAcao.insere(out strMensagem))
            {
                //Atualizando o valor na tabela identificador
                objIdentificador.atualizaValor();

                //Informando a mensagem do status da opera��o
                Session["strStatus"] = "Acao inserido com sucesso.";

                Response.Redirect("TipoAcao.aspx?codigo=" + objTipoAcao.Codigo.Valor);
            }
            else
            {
                lblMensagem.Text = "N�o foi possivel realizar a opera��o.<br />" + strMensagem;
                imgIcone.ImageUrl = "images/icones/aviso.gif";
                divMensagem.Visible = true;
            }

            objTipoAcao = null;
            objIdentificador = null;
        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, user.IDusuario.ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region metodo alteraAcao
    /// <summary>
    /// M�todo que altera um Acao
    /// </summary>
    /// <param name="intCodigo">C�digo do Imp�cto</param>
    private void alteraAcao(int intCodigoAcao)
    {
        try
        {
            String strMensagem = String.Empty;

            ServiceDesk.Negocio.ClsAcao objTipoAcao = new ServiceDesk.Negocio.ClsAcao(intCodigoAcao);
            objTipoAcao.Descricao.Valor = ServiceDesk.Generica.ClsTexto.trocaAspaPorHtml(txtDescricao.Text);

            if (objTipoAcao.altera(out strMensagem))
            {
                Session["strStatus"] = "Acao alterado com sucesso.";
            }
            else
            {
                lblMensagem.Text = strMensagem;
                imgIcone.ImageUrl = "images/icones/aviso.gif";
                divMensagem.Visible = true;
            }
            objTipoAcao = null;

            //Gerando o GridView com as Origens dos Chamados
            ServiceDesk.Negocio.ClsAcao.geraGridView(gvAcao);
        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, user.IDusuario.ToString(), this.Request.Path, "0", ex.ToString());
          }
    }
    #endregion

    #region Evento gvAcao_OnRowCommand
    /// <summary>
    /// Evento que ocorre quando um comando da GridView � solicitado
    /// </summary>
    protected void gvAcao_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Editar")
            {
                GridViewRow objRow = gvAcao.Rows[Convert.ToInt32(e.CommandArgument)];
                if (objRow != null)
                {
                    Label lblCodigo = (Label)objRow.FindControl("lblCodigo");
                    Response.Redirect("TipoAcao.aspx?codigo=" + lblCodigo.Text,false);
                }
                objRow = null;
                ServiceDesk.Negocio.ClsAcao.geraGridView(gvAcao);
            }

            if (e.CommandName == "Excluir")
            {
                GridViewRow objRow = gvAcao.Rows[Convert.ToInt32(e.CommandArgument)];
                if (objRow != null)
                {
                    Label lblCodigo = (Label)objRow.FindControl("lblCodigo");

                    ServiceDesk.Negocio.ClsAcao objTipoAcao = new ServiceDesk.Negocio.ClsAcao(Convert.ToInt32(lblCodigo.Text));
                    try
                    {
                        if (objTipoAcao.exclui())
                        {
                            lblMensagem.Text = "Item excluido com sucesso.";
                            imgIcone.ImageUrl = "images/icones/aviso.gif";
                            divMensagem.Visible = true;

                            txtDescricao.Text = String.Empty;
                        }
                    }
                    catch (Exception ex)
                    {
                        lblMensagem.Text = "N�o foi poss�vel excluir a Origem do Chamado.<br>" + ex.Message;
                        imgIcone.ImageUrl = "images/icones/erro.gif";
                        divMensagem.Visible = true;
                    }

                    objRow = null;
                    objTipoAcao = null;
                }
                ServiceDesk.Negocio.ClsAcao.geraGridView(gvAcao);
            }
        }
        catch (Exception ex)
        {
                ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, user.IDusuario.ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region Evento gvAcao_PageIndexChanging
    /// <summary>
    /// Evento que ocorre quando � mudada a p�gina da GridView
    /// </summary>
    protected void gvAcao_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView objGridView = (GridView)sender;
        objGridView.PageIndex = e.NewPageIndex;
        objGridView = null;
    }
    #endregion

    #region Evento gvAcao_RowDataBound
    /// <summary>
    /// Evento que ocorre na montagem da GridView
    /// </summary>
    protected void gvAcao_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            int intCodigoAcao = 0;

            if (Request.QueryString["codigo"] != null)
            {
                intCodigoAcao = Convert.ToInt32(Request.QueryString["codigo"].ToString().Trim());
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

    #region LimparCampo Descri��o
    /// <summary>
    /// M�todo que limpa o campo descri��o 
    /// </summary>
    protected void btnLimpar_Click(object sender, EventArgs e)
    {
        txtDescricao.Text = String.Empty;
        Response.Redirect("TipoAcao.aspx");
    }
    #endregion

}