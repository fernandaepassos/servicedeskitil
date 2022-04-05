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

public partial class Minha_Estrutura_Organizacional : BasePage
{
    public string strLinkPagina = string.Empty;
    public string strFimLinkPagina = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            CheckAcesso(34);

            //Esconde a mensagem de erro
            lblMensagem.Visible = false;
            divMensagem.Visible = false;

            if (!Page.IsPostBack)
            {
                ClsStatusTabela.geraDropDownList(ddlStatus, "EstruturaOrganizacional");

                //ddlStatus.Items.Insert(0, "--");
                //ddlStatus.Items[0].Value = "";
                //ddlStatus.Items.Insert(1, "Ativo");
                //ddlStatus.Items[1].Value = "16";
                //ddlStatus.Items.Insert(2, "Inativo");
                //ddlStatus.Items[2].Value = "20";

                SServiceDesk.Negocio.ClsEstruturaOrganizacional.geraGridView(this.gvEstruturaOrganizacional);
            }
            strFimLinkPagina = ")";
            strLinkPagina = "Javascript:VisualizaEstruturaOrganizacional(";
        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, user.IDusuario.ToString(), this.Request.Path, "0", ex.ToString());
        }
    }

    #region gvEstruturaOrganizacional_RowDataBound
    /// <summary>
    /// Evento que ocorre na montagem da GridView
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvEstruturaOrganizacional_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            try
            {
                Label lblEstruturaSuperiorCodigo = (Label)e.Row.FindControl("lblEstruturaSuperiorCodigo");
                Label lblEstruturaSuperior = (Label)e.Row.FindControl("lblEstruturaSuperior");
                if (lblEstruturaSuperiorCodigo.Text != String.Empty)
                {
                    SServiceDesk.Negocio.ClsEstruturaOrganizacional objEstruturaOrganizacional = new SServiceDesk.Negocio.ClsEstruturaOrganizacional(Convert.ToInt32(lblEstruturaSuperiorCodigo.Text));
                    lblEstruturaSuperior.Text = objEstruturaOrganizacional.Descricao.Valor;
                    objEstruturaOrganizacional = null;
                }

                Label lblTipoEstruturaCodigo = (Label)e.Row.FindControl("lblTipoEstruturaCodigo");
                Label lblTipoEstrutura = (Label)e.Row.FindControl("lblTipoEstrutura");
                if (lblTipoEstruturaCodigo.Text != String.Empty)
                {
                    SServiceDesk.Negocio.ClsTipoEstruturaOrganizacional objTipoEstruturaOrganizacional = new SServiceDesk.Negocio.ClsTipoEstruturaOrganizacional(Convert.ToInt32(lblTipoEstruturaCodigo.Text));
                    lblTipoEstrutura.Text = objTipoEstruturaOrganizacional.Descricao.Valor;
                    objTipoEstruturaOrganizacional = null;
                }

            }
            catch
            {
            }
        }
    }
    #endregion

    protected void btnFiltra_Click(object sender, EventArgs e)
    {
        try
        {
            bool bPrimeiroParametro = true;
            String strMensagem = string.Empty;
            String strSql = "SELECT * FROM estruturaorganizacional ";

            if (txtCodigo.Text.Trim() != String.Empty)
            {
                strSql += "WHERE ";
                strSql += "estrutura_codigo = '" + ServiceDesk.Generica.ClsTexto.trocaAspaPorHtml(txtCodigo.Text.Trim()) + "' ";
                bPrimeiroParametro = false;
            }

            if (txtDescricao.Text.Trim() != String.Empty)
            {
                if (!bPrimeiroParametro)
                {
                    strSql += "AND ";
                }
                else
                {
                    strSql += "WHERE ";
                    bPrimeiroParametro = false;
                }
                strSql += " descricao LIKE '%" + ServiceDesk.Generica.ClsTexto.trocaAspaPorHtml(txtDescricao.Text.Trim()) + "%' ";
            }

            if (ddlStatus.SelectedValue != String.Empty)
            {
                if (!bPrimeiroParametro)
                {
                    strSql += "AND ";
                }
                else
                {
                    strSql += "WHERE ";
                    bPrimeiroParametro = false;
                }
                strSql += "status = '" + ServiceDesk.Generica.ClsTexto.trocaAspaPorHtml(ddlStatus.SelectedValue.Trim()) + "' ";
            }

            if (txtSigla.Text != String.Empty)
            {
                if (!bPrimeiroParametro)
                {
                    strSql += "AND ";
                }
                else
                {
                    strSql += "WHERE ";
                    bPrimeiroParametro = false;
                }
                strSql += "sigla = '" + ServiceDesk.Generica.ClsTexto.trocaAspaPorHtml(txtSigla.Text.Trim()) + "'";
            }

            strSql += " ORDER BY descricao ";

            if (!SServiceDesk.Negocio.ClsEstruturaOrganizacional.geraGridViewQuery(gvEstruturaOrganizacional, strSql))
            {
                lblMensagem.Text = "Sua consulta naõ retornou resultados.";
                imgIcone.ImageUrl = "images/icones/aviso.gif";
                lblMensagem.Visible = true;
                divMensagem.Visible = true;
            }
        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, user.IDusuario.ToString(), this.Request.Path, "0", ex.ToString());
        }
    }

    protected void btnNovo_Click(object sender, EventArgs e)
    {
        Response.Redirect("EstruturaOrganizacional.aspx", false);
    }
}