/*
- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
  • Formulário que apresenta todos os problemas cadastrados no sistema.
  
  	Data: 29/11/2005
  	Autor: Fernanda Passos
  	Descrição: ...
  
  
  • Alterações
  	Data:
  	Autor:
  	Descrição:
- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
*/
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

public partial class Meus_Problemas : BasePage
{
    public string strLinkPagina = string.Empty;

    #region Page Loag
    /// <summary>
    /// Page Loag
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            CheckAcesso(28);

            divMensagem.Visible = false;
            if (!Page.IsPostBack)
            {
                strLinkPagina = "Problema.aspx?codigo_problema = ";
                ClsProblema.geraGridView(gvProblema);
            }
        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, user.IDusuario.ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region Row Command do grid
    /// <summary>
    /// Row Command do grid
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

            //Grava Log de Erro
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, user.IDusuario.ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region DataBound do grid
    /// <summary>
    /// DataBound do grid
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
            //Grava Log de Erro
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, user.IDusuario.ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion
}