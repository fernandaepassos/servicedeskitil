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

public partial class EstruturaOrganizacional : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            CheckAcesso(34);

            Page.MaintainScrollPositionOnPostBack = true;

            string strCodigoChamado = string.Empty;
            string strCodigoIncidente = string.Empty;
        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, user.IDusuario.ToString(), this.Request.Path, "0", ex.ToString());

        }
    }
}
