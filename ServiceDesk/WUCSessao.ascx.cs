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

public partial class WUCSessao : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
      //teste de SESSAO      
      // int count = Session.Contents.Count;
      //fim teste
      if (Session.Contents.Count == 0)
      {
        HtmlControl tagBody = (HtmlControl)Page.FindControl("body");
        tagBody.Attributes.Add("onload", "SessaoEncerrada()");
      }
    }
}
