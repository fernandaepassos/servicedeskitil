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

public partial class Conhecimento : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        CheckAcesso(0);

        Page.MaintainScrollPositionOnPostBack = true;

        if (!Page.IsPostBack)
        {
            string strConhecimentoCodigo = string.Empty;

            if (Request.QueryString["conhecimento"] != string.Empty)
            {
                strConhecimentoCodigo = Request.QueryString["conhecimento"];
                WUCBaseConhecimento1.PreencheCampos(strConhecimentoCodigo);
            }
            else
                WUCBaseConhecimento1.PreencheDroDown("Conhecimento");
        }
    }
}
