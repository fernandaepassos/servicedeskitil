/*
- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
  • DashBoard - Apresenta várias funcionalidades do sistema em apenas uma tela.
  
  	Data: 
  	Autor: Marcos Paulo \ Fernanda Passos
  	Descrição: Esta tela permite á utilização de todas as funcionalidades do sistema service desk que estão
    dentro de um User Control.
  
  
  • Alterações
  	Data:
  	Autor: 
  	Descrição: 
- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
*/
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
using InfoSupport.Demos.WebParts;

public partial class teste_webparts : BasePage
{
    #region Métodos
    void Page_PreInit()
    {

    }
    #endregion

    #region Evento
    #region Evento Page Load
    /// <summary>
    /// Evento Page Load
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        // Declarações
        WUCItemConfiguracaoAtributo wuc1 = new WUCItemConfiguracaoAtributo();

        try
        {
            CheckAcesso(20);

            if (!IsPostBack)
            {
                wpm1.DisplayMode = WebPartManager.CatalogDisplayMode;
            }
            else
            {
                //Mantém a posição do Scroll após o PostBack
                Page.MaintainScrollPositionOnPostBack = true;
            }
        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, user.IDusuario.ToString(), this.Request.Path, "0", ex.ToString());
        }
    }

    #endregion
    #endregion

}
