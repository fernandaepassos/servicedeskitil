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

public partial class Incidente : BasePage
{
    #region Page Load
    /// <summary>
    /// Page Load
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            CheckAcesso(47);

            Page.MaintainScrollPositionOnPostBack = true;

            string strCodigoChamado = string.Empty;
            string strCodigoIncidente = string.Empty;

            if (!Page.IsPostBack)
            {
                string strMensagem = string.Empty;
                string strTipoExibicaoTela = string.Empty;
                string strFormatoDataInclusao = ClsParametro.DataInclusao;

                if (Request.QueryString["chamado"] != null)
                {
                    strCodigoChamado = Request.QueryString["chamado"];
                }

                if (Request.QueryString["incidente"] != null)
                {
                    strCodigoIncidente = Request.QueryString["incidente"];
                }

                if (Request.QueryString["view"] != null)
                { strTipoExibicaoTela = Request.QueryString["view"]; }
            }

            if (strCodigoIncidente != string.Empty)
            {
                WUCIncidente1.EditaIncidente(Convert.ToInt32(strCodigoIncidente));
            }

            if (strCodigoChamado != string.Empty)
            {
                WUCIncidente1.CriaIncidenteHerdandoChamado(Convert.ToInt32(strCodigoChamado));
            }

        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, user.IDusuario.ToString(), this.Request.Path, "0", ex.ToString());

        }
    }
    #endregion

    protected void WUCIncidente1_Load(object sender, EventArgs e)
    {

    }
}
