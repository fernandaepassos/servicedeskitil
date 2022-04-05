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

public partial class Projeto : BasePage
{

    #region Page_Load
    /// <summary>
    /// Page_Load
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        CheckAcesso(0);

        int intProjetoCodigo = 0;
        if (!Page.IsPostBack)
        {
            WUCSolucao1.PreencheDropDownList();

            if (Request.QueryString["projeto_codigo"] != null)
            { 
                intProjetoCodigo = Convert.ToInt32(Request.QueryString["projeto_codigo"]);
                ServiceDesk.Projeto.ClsProjeto objProjeto = new ServiceDesk.Projeto.ClsProjeto();
                ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
                WUCSolucao1.PreecheCampos(objProjeto.GetDadosProjetoPorParamatro(intProjetoCodigo), true,string .Empty ,string .Empty );
                WUCSolucao1.PreencheTreeView(intProjetoCodigo);
                objProjeto = null;
                objBanco = null;
            }
            else if (Request.QueryString["tabela"] != null && Request.QueryString["tabela_identificador"] != null)
            {
              WUCSolucao1.PreecheCampos(null, false, Request.QueryString["tabela"].ToString().Trim(), Request.QueryString["tabela_identificador"].ToString().Trim());
            }
          }
    }
    #endregion
}
