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


public partial class TipoSolicitacao : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        CheckAcesso(0);
    }

    protected void imgNovoTipoDia_Click(object sender, ImageClickEventArgs e)
    {
        this.pnlNovoTipo.Visible = true;
        txtDescricaoTipo.Text = string.Empty;
    }
}
