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

public partial class WUCMeusIncidentes : System.Web.UI.UserControl
{
    #region Declarações
    public string strLinkPagina = string.Empty;
    public string strFimLinkPagina = string.Empty;
    #endregion

    #region Evento Page_Load
    /// <summary>
    /// Evento Page_Load
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //Esconde a mensagem de erro
            lblMensagem.Visible = false;
            divMensagem.Visible = false;

            if (!Page.IsPostBack)
            {
                string strCodigoUsuario = ClsUsuario.getCodigoUsuario().ToString();
                ClsLog.insereLog(ClsLog.enumTipoLog.ACESSO, strCodigoUsuario, this.Request.Path, "0", "");

                //Gera as Drop Down list
                ListItem itemDefault = new ListItem("Selecione o solicitante", "");
                ClsUsuario.geraDropDownList(ddlSolicitante, itemDefault);
                ClsStatusTabela.geraDropDownList(ddlStatus, "Incidente");

                ClsIncidente objIncidente = new ClsIncidente();
                objIncidente.Codigo.CampoIdentificador = false;

                objIncidente.MatriculaInclusor.Valor = strCodigoUsuario;
                objIncidente.MatriculaInclusor.CampoIdentificador = true;
                ClsIncidente.geraGridView(this.gvIncidentes, objIncidente);

            }
            strFimLinkPagina = ")";
            strLinkPagina = "Javascript:VisualizaIncidente(";
        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region Evento btnFiltra_Click
    /// <summary>
    /// Evento btnFiltra_Click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnFiltra_Click(object sender, EventArgs e)
    {
        try
        {
            String strMensagem = string.Empty;
            bool bPrimeiroParametro = true;



            String strSql = "SELECT * ";
            strSql += "FROM incidente ";

            if (txtCodigo.Text.Trim() != string.Empty)
            {
                strSql += "WHERE ";

                strSql += "incidente_codigo = '" + ServiceDesk.Generica.ClsTexto.trocaAspaPorHtml(txtCodigo.Text.Trim()) + "' ";
                bPrimeiroParametro = false;
            }

            if (txtDescricao.Text.Trim() != string.Empty)
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

            if (ddlSolicitante.SelectedValue != string.Empty)
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
                strSql += " pessoa_codigo_solicitante = '" + ddlSolicitante.SelectedValue + "' ";
            }

            if (ddlStatus.SelectedValue != string.Empty)
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
                strSql += " status_codigo = '" + ddlStatus.SelectedValue + "' ";
            }

            strSql += "ORDER BY prioridade_codigo, incidente_codigo";


            if (!ClsChamado.geraGridViewQuery(gvIncidentes, strSql, out strMensagem))
            {
                lblMensagem.Text = strMensagem;
                imgIcone.ImageUrl = "images/icones/aviso.gif";
                lblMensagem.Visible = true;
                divMensagem.Visible = true;
            }
        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region gvIncidente_RowDataBound
    /// <summary>
    /// Evento gvIncidente_RowDataBound
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvIncidente_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //condicao IF que exibe os dados no GridView (estado: não-editável)
                if ((e.Row.RowState == DataControlRowState.Normal) || (e.Row.RowState == DataControlRowState.Alternate))
                {
                    Label lblCodigoStatus = (Label)e.Row.FindControl("lblCodigoStatus");
                    Label lblDescricaoStatus = (Label)e.Row.FindControl("lblDescricaoStatus");
                    lblDescricaoStatus.Text = ServiceDesk.Negocio.ClsStatus.getDescricaoStatus(lblCodigoStatus.Text);
                }
            }
        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion
}
