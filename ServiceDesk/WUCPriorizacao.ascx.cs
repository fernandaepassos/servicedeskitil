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

public partial class WUCPriorizacao : System.Web.UI.UserControl
{
    protected string strPrioridadeCodigo = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
      if (!Page.IsPostBack)
      {
        //Grava Log de Acesso
        if (Session["CodigoUsuarioLogado"] != null)
        {
          ServiceDesk.Negocio.ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ACESSO, ClsUsuario.getCodigoUsuario().ToString(),Request.Path, "0", "");
        }
      }  
    }

    public void geraDropDownListImpactoUrgencia()
    {
        try
        {
            ServiceDesk.Negocio.ClsTipoUrgencia.geraDropDownList(ddlUrgencia);
            ddlUrgencia.Items.Insert(0, "0");
            ddlUrgencia.Items[0].Text = "";
            ServiceDesk.Negocio.ClsTipoImpacto.geraDropDownList(ddlImpacto);
            ddlImpacto.Items.Insert(0, "0");
            ddlImpacto.Items[0].Text = "";

        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }

    public void setImpacto(string strImpactoCodigo)
    {
        try
        {
            if (strImpactoCodigo != string.Empty)
            {
                this.ddlImpacto.ClearSelection();
                this.ddlImpacto.SelectedValue = strImpactoCodigo;
                getPrioridade();
            }

        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }

    public void setUrgencia(string strUrgenciaCodigo)
    {
        try
        {
            if (strUrgenciaCodigo != string.Empty)
            {
                this.ddlUrgencia.ClearSelection();
                this.ddlUrgencia.SelectedValue = strUrgenciaCodigo;
                getPrioridade();
            }

        }
        catch (Exception ex)
        {
                ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }

    public string getImpacto()
    {
      if (this.ddlImpacto.SelectedIndex > 0)
      {
        return this.ddlImpacto.SelectedValue;
      }
      return string.Empty;
    }

    public string getUrgencia()
    {
      if (this.ddlUrgencia.SelectedIndex > 0)
      {
        return this.ddlUrgencia.SelectedValue;
      }
      return string.Empty;
    }

    public string getPrioridade()
    {
        try
        {
            if (this.ddlImpacto.SelectedIndex > 0 && this.ddlUrgencia.SelectedIndex > 0)
            {
                strPrioridadeCodigo = ServiceDesk.Negocio.ClsPrioridade.getPrioridade(getImpacto(), getUrgencia());
                txtPrioridade.Text = ServiceDesk.Negocio.ClsPrioridade.getPrioridadeDescricao(strPrioridadeCodigo);
                return strPrioridadeCodigo;
            }
            else
            {
                return string.Empty;
            }

        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());

            return string.Empty;
        }
    }
  
    #region Bloqueia Campos
    /// <summary>
    /// Bloqueia campos
    /// </summary>
    /// <param name="bolBloqueiaPrioridade">Se é para bloquear a prioridade</param> 
    /// <param name="bolBloqueiaUrgencia">Se é para bloquear a urgência</param> 
    /// <param name="bolBloquiaImpacto">Se é para bloquear o impacto</param> 
    public void BloqueiaCampos(bool bolBloquiaImpacto, bool bolBloqueiaUrgencia, bool bolBloqueiaPrioridade)
    {
        //===============================================================================================//
        // - O que: Bloqueia os campos da escalação de acordo com o parametro "BLOQUEARESCALACAOCHAMADO"
        // que indica se para a empresa atual deseja-se bloquear os campos de escalação.
        // - Quem: Fernanda Passos.
        // - Quando: 06/03/2006 ás 10:15hs.
        //===============================================================================================//
        if (bolBloqueiaPrioridade == true) this.txtPrioridade.Enabled = false; else this.txtPrioridade.Enabled = true;

        if (bolBloquiaImpacto == true) this.ddlImpacto.Enabled = false; else this.ddlImpacto.Enabled = true;

        if (bolBloqueiaUrgencia == true) this.ddlUrgencia.Enabled = false; else this.ddlUrgencia.Enabled = true;

    }
    #endregion
}
