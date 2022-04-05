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

public partial class Header : BasePage
{
    protected static string strCodigoRede;
    protected static string strCodigoUsuarioLogado;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            CheckAcesso(0);

            UsuarioLogado user = (UsuarioLogado)Session["USUARIOLOGADO"];
            lblData.Text = DateTime.Now.ToString();
            lblNomUsu.Text = user.Nome;
        }
        catch (Exception ex)
        {
            //Grava Log de Erro
            ClsLog.insereLog(ClsLog.enumTipoLog.ERRO, strCodigoUsuarioLogado, Request.Path, "0", ex.ToString());
        }
    }

    protected void btnLogout_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, typeof(Page),
        "RedirectScript", "window.parent.location = 'logout.aspx'", true);
    }
}
