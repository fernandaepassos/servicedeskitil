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

public partial class ChamadoOperador : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        CheckAcesso(0);

        Page.MaintainScrollPositionOnPostBack = true;
        string strAbaSelecionada = string.Empty;
        string strCodigoUsuario = string.Empty;
        string strCodigoChamado = string.Empty;

        if (!Page.IsPostBack)
        {
            strCodigoChamado = Request.QueryString["chamado"];
        }

        if (strCodigoChamado != string.Empty)
        {
            WUCChamado1.EditaChamado(Convert.ToInt32(strCodigoChamado));
        }
    }
}
