using System;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ServiceDesk.Negocio;

public partial class AuditoriaLista : BasePage
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
            //Esconde a mensagem de erro
            lblMensagem.Visible = false;
            divMensagem.Visible = false;

            if (!Page.IsPostBack)
            {

                //Gerando o GridView com as Auditorias cadastradas
                ClsAuditoria.geraGridView(gvAuditoria);
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
        catch (Exception)
        {
            //Excepções são tratadas agora no método page_error do BaseClass
            throw;
        }
    }
    #endregion

    #region Evento gvAuditoria_OnRowCommand
    /// <summary>
    /// Evento que ocorre quando um comando da GridView é solicitado
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvAuditoria_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            GridView objGridView = (GridView)sender;
            String strMensagem = String.Empty;

            switch (e.CommandName)
            {
                case "Editar":
                    {
                        GridViewRow objRow = objGridView.Rows[Convert.ToInt32(e.CommandArgument)];
                        if (objRow != null)
                        {
                            Label lblCodigo = (Label)objRow.FindControl("lblCodigo");
                            Response.Redirect("auditoria.aspx?codigo=" + lblCodigo.Text);
                        }
                        objRow = null;
                        break;
                    }
                case "Excluir":
                    {
                        GridViewRow objRow = objGridView.Rows[Convert.ToInt32(e.CommandArgument)];
                        if (objRow != null)
                        {
                            Label lblCodigo = (Label)objRow.FindControl("lblCodigo");
                            ServiceDesk.Negocio.ClsAuditoria objAuditoria = new ServiceDesk.Negocio.ClsAuditoria(Convert.ToInt32(lblCodigo.Text));
                            try
                            {
                                objAuditoria.exclui();
                                strMensagem = "Auditoria excluída com sucesso.";
                            }
                            catch
                            {
                                strMensagem = "Não foi possível excluir a Auditoria.";
                            }
                            objAuditoria = null;
                        }
                        objRow = null;
                        lblMensagem.Text = strMensagem;
                        imgIcone.ImageUrl = "images/icones/aviso.gif";
                        lblMensagem.Visible = true;
                        divMensagem.Visible = true;
                        break;
                    }
            }

            ServiceDesk.Negocio.ClsAuditoria.geraGridView(objGridView);
            objGridView = null;
        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, user.IDusuario.ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region Evento gvAuditoria_PageIndexChanging
    /// <summary>
    /// Evento que ocorre quando é mudada a página da GridView
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvAuditoria_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView objGridView = (GridView)sender;
        objGridView.PageIndex = e.NewPageIndex;
        objGridView = null;
    }
    #endregion

    #region Evento gvAuditoria_RowDataBound
    /// <summary>
    /// Evento que ocorre na montagem da GridView
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvAuditoria_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                try
                {
                    Label lblStatus = (Label)e.Row.FindControl("lblStatus");

                    if (lblStatus.Text != String.Empty)
                    {
                        ServiceDesk.Negocio.ClsStatus objStatus = new ServiceDesk.Negocio.ClsStatus(Convert.ToInt32(lblStatus.Text));
                        lblStatus.Text = objStatus.Descricao.Valor;
                        objStatus = null;
                    }

                    //adiciona um evento javascript no botão Excluir
                    ImageButton btnExcluir = (ImageButton)e.Row.Cells[5].Controls[0];
                    btnExcluir.Attributes.Add("onclick", "return verifica();");

                }
                catch (Exception ex)
                {
                    Response.Write(ex.ToString());
                    if (Session["CodigoUsuarioLogado"] != null)
                    {
                        //Grava Log de Erro
                        ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, user.IDusuario.ToString(), this.Request.Path, "0", ex.ToString());
                    }
                    else
                    {
                        Response.Redirect("Default.aspx", true);
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

    #region Evento gvAuditoria_Load
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvAuditoria_Load(object sender, EventArgs e)
    {
        try
        {
            GridView objGridView = (GridView)sender;
            if (objGridView.Rows.Count == 0)
            {
                novaAuditoria(sender, e);
            }
            objGridView = null;
        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, user.IDusuario.ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region evento novaAuditoria
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void novaAuditoria(object sender, EventArgs e)
    {
        Response.Redirect("auditoria.aspx");
    }
    #endregion

}