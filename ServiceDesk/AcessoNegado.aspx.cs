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


public partial class AcessoNegado : System.Web.UI.Page
{
    //protected System.Web.UI.WebControls.Label lblerro;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["erro"] != null)
        {
            lblerro.Text = Request.QueryString["erro"].ToString();
        }
    }
}
