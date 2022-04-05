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

public partial class RequisicaoChamado : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            CheckAcesso(17);

            Page.MaintainScrollPositionOnPostBack = true;
            string strAbaSelecionada = string.Empty;
            string strCodigoUsuario = string.Empty;
            string strCodigo = string.Empty;

            if (!Page.IsPostBack)
            {
                strCodigo = Request.QueryString["RequisicaoServico"];
            }

            if (strCodigo != string.Empty)
            {
                WUCRequisicaoServico1.EditaRequisicaoServico(Convert.ToInt32(strCodigo));
            }
        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, user.IDusuario.ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
}
