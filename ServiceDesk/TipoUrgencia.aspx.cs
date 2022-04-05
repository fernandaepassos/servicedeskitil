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

public partial class TipoUrgencia : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            CheckAcesso(26);

            int intCodigoUrgencia = 0;

            //Mantém a posição do Scroll após o PostBack
            Page.MaintainScrollPositionOnPostBack = true;

            //Esconde a mensagem de erro
            divMensagem.Visible = false;

            if (!Page.IsPostBack)
            {
                //Gerando o GridView com as Origens dos Chamados
                ClsTipoUrgencia.geraGridView(gvUrgencia);

                if (Request.QueryString["codigo"] != null)
                {
                    intCodigoUrgencia = Convert.ToInt32(Request.QueryString["codigo"].ToString().Trim());

                    //Monta os dados na tela
                    CarregaDescricaoUrgencia(intCodigoUrgencia);
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

    #region metodo CarregaDescricaoUrgencia
    /// <summary>
    /// Método que popula o campo descrição da Origem do Chamado
    /// </summary>
    /// <param name="intCodigoItem">Código da Origem do Chamado</param>
    private void CarregaDescricaoUrgencia(int intCodigoUrgencia)
    {
        try
        {
            ServiceDesk.Negocio.ClsTipoUrgencia objTipoUrgencia = new ServiceDesk.Negocio.ClsTipoUrgencia(intCodigoUrgencia);
            txtDescricao.Text = objTipoUrgencia.Descricao.Valor;
            objTipoUrgencia = null;
        }
        catch (Exception ex)
        {
                ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, user.IDusuario.ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region Salva o Impacto
    /// <summary>
    /// Método que salva Urgência (inserir/alterar)
    /// </summary>
    protected void btnSalvar_Click(object sender, EventArgs e)
    {
        try
        {
            if (Request.QueryString["codigo"] == null)
            {
                insereUrgencia();
            }
            else
            {
                if (Request.QueryString["codigo"] != null)
                {
                    alteraUrgencia(Convert.ToInt32(Request.QueryString["codigo"].ToString().Trim()));
                }
            }
        }
        catch (Exception ex)
        {
                ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, user.IDusuario.ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region metodo insereUrgencia
    /// <summary>
    /// Método que insere uma nova Origem do Chamado
    /// </summary>
    private void insereUrgencia()
    {
        try
        {
            String strMensagem = String.Empty;

            ServiceDesk.Negocio.ClsTipoUrgencia objTipoUrgencia = new ServiceDesk.Negocio.ClsTipoUrgencia();
            ServiceDesk.Negocio.ClsIdentificador objIdentificador = new ServiceDesk.Negocio.ClsIdentificador();

            objIdentificador.Tabela.Valor = objTipoUrgencia.Atributos.NomeTabela;
            objTipoUrgencia.Codigo.Valor = objIdentificador.getProximoValor().ToString();
            objTipoUrgencia.Descricao.Valor = ServiceDesk.Generica.ClsTexto.trocaAspaPorHtml(txtDescricao.Text);

            if (objTipoUrgencia.insere(out strMensagem))
            {
                //Atualizando o valor na tabela identificador
                objIdentificador.atualizaValor();

                //Informando a mensagem do status da operação
                Session["strStatus"] = "Item inserido com sucesso.";

                Response.Redirect("TipoUrgencia.aspx?codigo=" + objTipoUrgencia.Codigo.Valor);
            }
            else
            {
                lblMensagem.Text = "Não foi possivel realizar a operação.<br />" + strMensagem;
                imgIcone.ImageUrl = "images/icones/aviso.gif";
                divMensagem.Visible = true;
            }

            objTipoUrgencia = null;
            objIdentificador = null;
        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, user.IDusuario.ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region metodo alteraUrgencia
    /// <summary>
    /// Método que altera um Impacto
    /// </summary>
    /// <param name="intCodigo">Código do Impácto</param>
    private void alteraUrgencia(int intCodigoUrgencia)
    {
        try
        {
            String strMensagem = String.Empty;

            ServiceDesk.Negocio.ClsTipoUrgencia objTipoUrgencia = new ServiceDesk.Negocio.ClsTipoUrgencia(intCodigoUrgencia);
            objTipoUrgencia.Descricao.Valor = ServiceDesk.Generica.ClsTexto.trocaAspaPorHtml(txtDescricao.Text);

            if (objTipoUrgencia.altera(out strMensagem))
            {
                Session["strStatus"] = "Impacto alterado com sucesso.";
            }
            else
            {
                lblMensagem.Text = strMensagem;
                imgIcone.ImageUrl = "images/icones/aviso.gif";
                divMensagem.Visible = true;
            }
            objTipoUrgencia = null;

            //Gerando o GridView com as Origens dos Chamados
            ServiceDesk.Negocio.ClsTipoUrgencia.geraGridView(gvUrgencia);
        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, user.IDusuario.ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region Evento gvUrgencia_OnRowCommand
    /// <summary>
    /// Evento que ocorre quando um comando da GridView é solicitado
    /// </summary>
    protected void gvUrgencia_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Editar")
            {
                GridViewRow objRow = gvUrgencia.Rows[Convert.ToInt32(e.CommandArgument)];
                if (objRow != null)
                {
                    Label lblCodigo = (Label)objRow.FindControl("lblCodigo");
                    Response.Redirect("TipoUrgencia.aspx?codigo=" + lblCodigo.Text,false);
                }
                objRow = null;
                ServiceDesk.Negocio.ClsTipoUrgencia.geraGridView(gvUrgencia);
            }

            if (e.CommandName == "Excluir")
            {
                GridViewRow objRow = gvUrgencia.Rows[Convert.ToInt32(e.CommandArgument)];
                if (objRow != null)
                {
                    Label lblCodigo = (Label)objRow.FindControl("lblCodigo");

                    ServiceDesk.Negocio.ClsTipoUrgencia objTipoUrgencia = new ServiceDesk.Negocio.ClsTipoUrgencia(Convert.ToInt32(lblCodigo.Text));
                    try
                    {
                        if (objTipoUrgencia.exclui())
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
                    objTipoUrgencia = null;
                }
                ServiceDesk.Negocio.ClsTipoUrgencia.geraGridView(gvUrgencia);
            }
        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, user.IDusuario.ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region Evento gvUrgencia_PageIndexChanging
    /// <summary>
    /// Evento que ocorre quando é mudada a página da GridView
    /// </summary>
    protected void gvUrgencia_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView objGridView = (GridView)sender;
        objGridView.PageIndex = e.NewPageIndex;
        objGridView = null;
    }
    #endregion

    #region Evento gvUrgencia_RowDataBound
    /// <summary>
    /// Evento que ocorre na montagem da GridView
    /// </summary>
    protected void gvUrgencia_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            int intCodigoUrgencia = 0;

            if (Request.QueryString["codigo"] != null)
            {
                intCodigoUrgencia = Convert.ToInt32(Request.QueryString["codigo"].ToString().Trim());
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ImageButton btnExcluir = (ImageButton)e.Row.Cells[3].Controls[0];
                btnExcluir.Attributes.Add("onclick", "return verifica();");

                Label lblCodigo = (Label)e.Row.Cells[0].Controls[1];
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
        Response.Redirect("TipoUrgencia.aspx");
    }
    #endregion

}
