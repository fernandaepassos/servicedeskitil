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

public partial class Parametro2 : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        CheckAcesso(14);
    }
}
