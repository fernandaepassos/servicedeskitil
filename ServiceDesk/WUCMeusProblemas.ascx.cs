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

public partial class WUCMeusProblemas : System.Web.UI.UserControl
{
    #region Declaração
    public string strLinkPagina = string.Empty;
    #endregion

    #region Evento Page_Load
    /// <summary>
    /// Evento Page_Load
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            divMensagem.Visible = false;
            if (!Page.IsPostBack)
            {
                ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ACESSO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", "");
  

                strLinkPagina = "Problema.aspx?codigo_problema = ";
                ServiceDesk.Negocio.ClsProblema.geraGridView(gvProblema);
            }
        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region Evento gvProblema_RowCommand
    /// <summary>
    /// Evento gvProblema_RowCommand
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvProblema_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Exibir")
            {
                GridViewRow objRow = gvProblema.Rows[Convert.ToInt32(e.CommandArgument)];
                if (objRow != null)
                {
                    Label lblCodigo = (Label)objRow.FindControl("lblCodigo");
                    Response.Redirect("Problema.aspx?CodProblema=" + lblCodigo.Text.Trim(), false);
                }
                objRow = null;
            }
            else if (e.CommandName == "Excluir")
            {
                GridViewRow objRow = gvProblema.Rows[Convert.ToInt32(e.CommandArgument)];
                if (objRow != null)
                {

                    string strMensagem = string.Empty;
                    Label lblCodigo = (Label)objRow.FindControl("lblCodigo");
                    if (lblCodigo == null) return;
                    ServiceDesk.Negocio.ClsProblema bjProblema = new ServiceDesk.Negocio.ClsProblema();
                    bjProblema.Codigo.Valor = lblCodigo.Text.Trim();
                    if (bjProblema.exclui(out strMensagem) == true) ServiceDesk.Negocio.ClsProblema.geraGridView(gvProblema);
                    bjProblema = null;
                }
            }
        }
        catch (Exception ex)
        {
            lblMensagem.Text = ex.Message;
            imgIcone.ImageUrl = "images/icones/aviso.gif";
            lblMensagem.Visible = true;
            divMensagem.Visible = true;

            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region Evento gvProblema_RowDataBound
    /// <summary>
    /// Evento gvProblema_RowDataBound
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvProblema_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if ((e.Row.RowState.ToString() == "Normal") || (e.Row.RowState.ToString() == "Alternate"))
            {
                string strFormatoDataSimples = ClsParametro.DataCompletaExibicao;
                Label lblDtInclusao = (Label)e.Row.FindControl("lblDataInclusao");
                if (lblDtInclusao.Text.Trim() != string.Empty)
                {
                    DateTime dataInclusao = Convert.ToDateTime(lblDtInclusao.Text.Trim());
                    e.Row.Cells[4].Text = dataInclusao.Date.ToString(strFormatoDataSimples);
                }
            }
        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion
}
