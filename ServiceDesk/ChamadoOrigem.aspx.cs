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

public partial class ChamadoOrigem : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            CheckAcesso(15);

            int intCodigoChamadoOrigem = 0;

            //Esconde a mensagem de erro
            divMensagem.Visible = false;

            //Mant�m a posi��o do Scroll ap�s o PostBack
            Page.MaintainScrollPositionOnPostBack = true;

            if (!Page.IsPostBack)
            {
                //Gerando o GridView com as Origens dos Chamados
                ServiceDesk.Negocio.ClsChamadoOrigem.geraGridView(gvChamadoOrigem);

                if (Request.QueryString["codigo"] != null)
                {
                    intCodigoChamadoOrigem = Convert.ToInt32(Request.QueryString["codigo"].ToString().Trim());

                    //Monta os dados na tela
                    CarregaDescricaoOrigem(intCodigoChamadoOrigem);
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

    #region metodo CarregaDescricaoOrigem
    /// <summary>
    /// M�todo que popula o campo descri��o da Origem do Chamado
    /// </summary>
    /// <param name="intCodigoItem">C�digo da Origem do Chamado</param>
    private void CarregaDescricaoOrigem(int intCodigoChamadoOrigem)
    {
        try
        {
            ServiceDesk.Negocio.ClsChamadoOrigem objChamadoOrigem = new ServiceDesk.Negocio.ClsChamadoOrigem(intCodigoChamadoOrigem);
            txtDescricao.Text = objChamadoOrigem.Descricao.Valor;
            objChamadoOrigem = null;
        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, user.IDusuario.ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region Salva Origem do Chamado
    /// <summary>
    /// M�todo que salva a Origem do Chamado (inserir/alterar)
    /// </summary>
    protected void btnSalvar_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["codigo"] == null)
        {
            insereChamadoOrigem();
        }
        else
        {
            if (Request.QueryString["codigo"] != null)
            {
                alteraChamadoOrigem(Convert.ToInt32(Request.QueryString["codigo"].ToString().Trim()));
            }
        }
    }
    #endregion

    #region metodo insereChamadoOrigem
    /// <summary>
    /// M�todo que insere uma nova Origem do Chamado
    /// </summary>
    private void insereChamadoOrigem()
    {
        try
        {
            String strMensagem = String.Empty;

            ServiceDesk.Negocio.ClsChamadoOrigem objChamadoOrigem = new ServiceDesk.Negocio.ClsChamadoOrigem();
            ServiceDesk.Negocio.ClsIdentificador objIdentificador = new ServiceDesk.Negocio.ClsIdentificador();

            objIdentificador.Tabela.Valor = objChamadoOrigem.Atributos.NomeTabela;
            objChamadoOrigem.Codigo.Valor = objIdentificador.getProximoValor().ToString();
            objChamadoOrigem.Descricao.Valor = ServiceDesk.Generica.ClsTexto.trocaAspaPorHtml(txtDescricao.Text);

            if (objChamadoOrigem.insere(out strMensagem))
            {
                //Atualizando o valor na tabela identificador
                objIdentificador.atualizaValor();

                //Informando a mensagem do status da opera��o
                Session["strStatus"] = "Origem do Chamado inserida com sucesso.";

                Response.Redirect("ChamadoOrigem.aspx?codigo=" + objChamadoOrigem.Codigo.Valor);
            }
            else
            {
                lblMensagem.Text = "N�o foi possivel realizar a opera��o.<br />" + strMensagem;
                imgIcone.ImageUrl = "images/icones/aviso.gif";
                divMensagem.Visible = true;
            }

            objChamadoOrigem = null;
            objIdentificador = null;
        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, user.IDusuario.ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region metodo alteraChamadoOrigem
    /// <summary>
    /// M�todo que altera uma Origem do Chamado
    /// </summary>
    /// <param name="intCodigo"></param>
    private void alteraChamadoOrigem(int intCodigoChamadoOrigem)
    {
        try
        {
            String strMensagem = String.Empty;

            ServiceDesk.Negocio.ClsChamadoOrigem objChamadoOrigem = new ServiceDesk.Negocio.ClsChamadoOrigem(intCodigoChamadoOrigem);
            objChamadoOrigem.Descricao.Valor = ServiceDesk.Generica.ClsTexto.trocaAspaPorHtml(txtDescricao.Text);

            if (objChamadoOrigem.altera(out strMensagem))
            {
                lblMensagem.Text = strMensagem;
                imgIcone.ImageUrl = "images/icones/aviso.gif";
                divMensagem.Visible = true;
            }
            else
            {
                lblMensagem.Text = strMensagem;
                imgIcone.ImageUrl = "images/icones/aviso.gif";
                divMensagem.Visible = true;
            }
            objChamadoOrigem = null;

            //Gerando o GridView com as Origens dos Chamados
            ServiceDesk.Negocio.ClsChamadoOrigem.geraGridView(gvChamadoOrigem);
        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, user.IDusuario.ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region Evento gvChamadoOrigem_OnRowCommand
    /// <summary>
    /// Evento que ocorre quando um comando da GridView � solicitado
    /// </summary>
    protected void gvChamadoOrigem_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Editar")
            {
                GridViewRow objRow = gvChamadoOrigem.Rows[Convert.ToInt32(e.CommandArgument)];
                if (objRow != null)
                {
                    Label lblCodigo = (Label)objRow.FindControl("lblCodigo");
                    Response.Redirect("ChamadoOrigem.aspx?codigo=" + lblCodigo.Text);
                }
                objRow = null;
                ServiceDesk.Negocio.ClsItemConfiguracaoAtributo.geraGridView(gvChamadoOrigem);
            }

            if (e.CommandName == "Excluir")
            {
                GridViewRow objRow = gvChamadoOrigem.Rows[Convert.ToInt32(e.CommandArgument)];
                if (objRow != null)
                {
                    Label lblCodigo = (Label)objRow.FindControl("lblCodigo");

                    ServiceDesk.Negocio.ClsChamadoOrigem objChamadoOrigem = new ServiceDesk.Negocio.ClsChamadoOrigem(Convert.ToInt32(lblCodigo.Text));
                    try
                    {
                        if (objChamadoOrigem.exclui())
                        {
                            Session["strStatus"] = "Origem do Chamado excluido com sucesso.";
                        }
                    }
                    catch (Exception ex)
                    {
                        lblMensagem.Text = "N�o foi poss�vel excluir a Origem do Chamado.<br>" + ex.Message;
                        imgIcone.ImageUrl = "images/icones/erro.gif";
                        divMensagem.Visible = true;
                    }

                    objRow = null;
                    objChamadoOrigem = null;
                }
                Response.Redirect("ChamadoOrigem.aspx", false);
                //ServiceDesk.Negocio.ClsChamadoOrigem.geraGridView(gvChamadoOrigem);
            }
        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, user.IDusuario.ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region Evento gvChamadoOrigem_PageIndexChanging
    /// <summary>
    /// Evento que ocorre quando � mudada a p�gina da GridView
    /// </summary>
    protected void gvChamadoOrigem_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView objGridView = (GridView)sender;
        objGridView.PageIndex = e.NewPageIndex;
        objGridView = null;
    }
    #endregion

    #region Evento gvChamadoOrigem_RowDataBound
    /// <summary>
    /// Evento que ocorre na montagem da GridView
    /// </summary>
    protected void gvChamadoOrigem_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            int intCodigoChamadoOrigem = 0;

            if (Request.QueryString["codigo"] != null)
            {
                intCodigoChamadoOrigem = Convert.ToInt32(Request.QueryString["codigo"].ToString().Trim());
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
        Response.Redirect("ChamadoOrigem.aspx");
    }
    #endregion

}