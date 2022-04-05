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

public partial class WUCMinhasRequisicoesServico : System.Web.UI.UserControl
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

                ClsLog.insereLog(ClsLog.enumTipoLog.ACESSO, strCodigoUsuario, Request.Path, "0", "");

                string strCodigoRede = ClsUsuario.getCodigoRede();

                //Gera as Drop Down list
                ListItem itemDefault = new ListItem("Selecione o solicitante", "");
                ClsUsuario.geraDropDownList(ddlSolicitante, itemDefault);
                ClsStatusTabela.geraDropDownList(ddlStatus, "RequisicaoServico");
                ClsRequisicaoServico objRequisicaoServico = new ClsRequisicaoServico();
                objRequisicaoServico.Codigo.CampoIdentificador = false;
                objRequisicaoServico.PessoaCodigoInclusor.Valor = strCodigoUsuario;
                objRequisicaoServico.PessoaCodigoInclusor.CampoIdentificador = true;
                ClsRequisicaoServico.geraGridView(this.gvRequisicaoServico, objRequisicaoServico);

            }
            strFimLinkPagina = ")";
            strLinkPagina = "Javascript:VisualizaRequisicaoServico(";
        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region Evento gvRequisicaoServico_RowDataBound
    /// <summary>
    /// Evento gvRequisicaoServico_RowDataBound
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvRequisicaoServico_RowDataBound(object sender, GridViewRowEventArgs e)
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
                ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
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
            strSql += "FROM RequisicaoServico ";

            if (txtCodigo.Text.Trim() != string.Empty)
            {
                strSql += "WHERE ";

                strSql += "requisicaoservico_codigo = '" + ServiceDesk.Generica.ClsTexto.trocaAspaPorHtml(txtCodigo.Text.Trim()) + "' ";
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

            strSql += "ORDER BY prioridade_codigo, requisicaoservico_codigo";


            if (!ServiceDesk.Negocio.ClsChamado.geraGridViewQuery(gvRequisicaoServico, strSql, out strMensagem))
            {
                lblMensagem.Text = strMensagem;
                imgIcone.ImageUrl = "images/icones/aviso.gif";
                lblMensagem.Visible = true;
                divMensagem.Visible = true;
            }
        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion
}
