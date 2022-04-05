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

public partial class WUCItemConfiguracaoRelatorio : System.Web.UI.UserControl
{
  #region Evento Page_Load
  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  protected void Page_Load(object sender, EventArgs e)
  {
    if (!Page.IsPostBack)
    {
      ServiceDesk.Negocio.ClsStatus objStatus = new ServiceDesk.Negocio.ClsStatus();
      ServiceDesk.Negocio.ClsStatus.geraRepeater(rptStatus, objStatus);
      objStatus = null;
    }
  }
  #endregion

  #region Evento rptStatus_DataBound
  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  protected void rptStatus_DataBound(object sender, RepeaterItemEventArgs e)
  {
    Repeater objRepeater = (Repeater)sender;
    if ( (e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem) )
    {
      Label lblCodigo = (Label)e.Item.FindControl("lblCodigo");
      Label lblStatusQuantidade = (Label)e.Item.FindControl("lblStatusQuantidade");

      lblStatusQuantidade.Text = ServiceDesk.Negocio.ClsItemConfiguracao.retornaTotalPorStatus(Convert.ToInt32(lblCodigo.Text)).ToString();

    }
    objRepeater = null;
  }
  #endregion

}