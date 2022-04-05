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

public partial class WUCSemaforo : System.Web.UI.UserControl
{

    #region evento Page_Load
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        double dblTempoAtualizacao = 0;
        try
        {
            SServiceDesk.Negocio.ClsPrioridade.geraRepeater(rptSemaforo);
            if (ClsParametro.TempoAtualizacaoSemaforo != null)
            {
                if (ClsParametro.TempoAtualizacaoSemaforo != String.Empty)
                {
                    ClsParametro.TempoAtualizacaoSemaforo = ClsParametro.TempoAtualizacaoSemaforo.Replace(".", ",");
                    dblTempoAtualizacao = Convert.ToDouble(ClsParametro.TempoAtualizacaoSemaforo);
                    dblTempoAtualizacao = dblTempoAtualizacao * 60000;
                    //registraScript(dblTempoAtualizacao);
                }
            }
        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region Metodo registraScript
    /// <summary>
    /// 
    /// </summary>
    private void registraScript(double dblTempo)
    {
        String strScript = String.Empty;
        strScript = "<script language=\"javascript\">";
        strScript += "setInterval(\"atualizaPagina()\", " + dblTempo.ToString() + ");";
        strScript += "</script>";
        Page.ClientScript.RegisterClientScriptBlock(Page.ClientScript.GetType(), "atualizaPagina", strScript);
    }
    #endregion

    #region rptSemaforo_OnItemDataBound
    protected void rptSemaforo_OnItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == ListItemType.Header)
            {
                Label lblMes = (Label)e.Item.FindControl("lblMes");
                lblMes.Text = DateTime.Now.ToString("MMMM");
                if (lblMes.Text.Length > 0)
                {
                    lblMes.Text = lblMes.Text.Substring(0, 1).ToUpper() + lblMes.Text.Substring(1, lblMes.Text.Length - 1);
                }
            }
            else if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
            {
                Label lblPrioridadeCodigo = (Label)e.Item.FindControl("lblPrioridadeCodigo");
                Label lblPrioridadeDescricao = (Label)e.Item.FindControl("lblPrioridadeDescricao");
                Label lblAtendido = (Label)e.Item.FindControl("lblAtendido");
                Label lblTMA = (Label)e.Item.FindControl("lblTMA");
                Label lblFila = (Label)e.Item.FindControl("lblFila");
                Label lblTMF = (Label)e.Item.FindControl("lblTMF");
                GridView gvAtendimentoTecnico = (GridView)e.Item.FindControl("gvAtendimentoTecnico");

                HtmlControl tdPrioridade = (HtmlControl)e.Item.FindControl("tdPrioridade");
                HtmlControl tdAtendido = (HtmlControl)e.Item.FindControl("tdAtendido");
                HtmlControl tdTMA = (HtmlControl)e.Item.FindControl("tdTMA");
                HtmlControl tdFila = (HtmlControl)e.Item.FindControl("tdFila");
                HtmlControl tdTMF = (HtmlControl)e.Item.FindControl("tdTMF");

                //Total de Atendimentos
                lblAtendido.Text = ClsChamado.retornaTotalPorPrioridade("chamado", lblPrioridadeCodigo.Text, "=", ClsStatus.getCodigoStatusFinalizado(), "data_finalizacao");
                lblTotalAtendido.Text = Convert.ToString(Convert.ToInt32(lblTotalAtendido.Text) + Convert.ToInt32(lblAtendido.Text));

                //Total de Horas do Atendimento
                lblTMA.Text = ClsChamado.retornaTotalHorasAtendimentoPorPrioridade("chamado", lblPrioridadeCodigo.Text, ClsStatus.getCodigoStatusFinalizado());
                lblTotalTMA.Text = Convert.ToString(Convert.ToDouble(lblTotalTMA.Text) + Convert.ToDouble(lblTMA.Text));

                //Total de Atendimentos na Fila
                lblFila.Text = ClsChamado.retornaTotalPorPrioridade("chamado", lblPrioridadeCodigo.Text, "<>",ClsStatus.getCodigoStatusFinalizado(),"data_inclusao");
                lblTotalFila.Text = Convert.ToString(Convert.ToInt32(lblTotalFila.Text) + Convert.ToInt32(lblFila.Text));

                //Total de Horas na Fila
                lblTMF.Text = ClsChamado.retornaTotalHorasFilaPorPrioridade("chamado", lblPrioridadeCodigo.Text, ClsStatus.getCodigoStatusFinalizado());
                lblTotalTMF.Text = Convert.ToString(Convert.ToDouble(lblTotalTMF.Text) + Convert.ToDouble(lblTMF.Text));

                //Montando o GridView dos Tecnicos
                ServiceDesk.Negocio.ClsChamado.geraGridViewNaoFinalizadosPorPrioridade(gvAtendimentoTecnico, Convert.ToInt32(lblPrioridadeCodigo.Text.Trim()));

                //Codigo provisorio para tirar acento
                string classe = lblPrioridadeDescricao.Text.Replace("é", "e").Replace("í", "i").ToLower();

                tdPrioridade.Attributes.Add("class", "aba " + classe);
                tdAtendido.Attributes.Add("class", "fonte " + classe);
                tdTMA.Attributes.Add("class", "fonte " + classe);
                tdFila.Attributes.Add("class", "fonte " + classe);
                tdTMF.Attributes.Add("class", "fonte " + classe);
            }
            else if (e.Item.ItemType == ListItemType.Footer)
            {
                Label lblAtendido = (Label)e.Item.FindControl("lblAtendido");
                Label lblTMA = (Label)e.Item.FindControl("lblTMA");
                Label lblFila = (Label)e.Item.FindControl("lblFila");
                Label lblTMF = (Label)e.Item.FindControl("lblTMF");
                Label lblTotal = (Label)e.Item.FindControl("lblTotal");

                lblAtendido.Text = lblTotalAtendido.Text;
                lblTMA.Text = lblTotalTMA.Text;
                lblFila.Text = lblTotalFila.Text;
                lblTMF.Text = lblTotalTMF.Text;
                lblTotal.Text = Convert.ToString(Convert.ToInt32(lblAtendido.Text) + Convert.ToInt32(lblFila.Text));
            }
        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());

        }
    }
    #endregion

    #region evento gvAtendimentoTecnico_RowDataBound
    protected void gvAtendimentoTecnico_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblTempoVida = (Label)e.Row.FindControl("lblTempoVida");
                Label lblTempoSla = (Label)e.Row.FindControl("lblTempoSla");
                HyperLink hlTempoRestante = (HyperLink)e.Row.FindControl("hlTempoRestante");
                Label lblTecnicoCodigo = (Label)e.Row.FindControl("lblTecnicoCodigo");
                HyperLink hlTecnico = (HyperLink)e.Row.FindControl("hlTecnico");
                HyperLink hlCodigo = (HyperLink)e.Row.FindControl("hlCodigo");
                int intTempo = 0;
                TimeSpan tsTempoRestante = new TimeSpan();

                try
                {
                    if ((lblTempoVida.Text != String.Empty) || (lblTempoSla.Text != String.Empty))
                    {
                        intTempo = Convert.ToInt32(lblTempoVida.Text) - Convert.ToInt32(lblTempoSla.Text);
                        if (intTempo > 0)
                        {
                            tsTempoRestante = new TimeSpan(0, intTempo, 0);
                        }
                    }
                    hlTempoRestante.Text = tsTempoRestante.Hours.ToString() + ":" + tsTempoRestante.Minutes.ToString();
                }
                catch (Exception ex)
                {
                    ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
                }

                try
                {
                    SServiceDesk.Negocio.ClsPessoa objPessoa = new SServiceDesk.Negocio.ClsPessoa(Convert.ToInt32(lblTecnicoCodigo.Text));
                    hlTecnico.Text = objPessoa.Nome.Valor;
                    objPessoa = null;
                }
                catch
                {
                }

                hlTempoRestante.NavigateUrl = "#";
                hlTecnico.NavigateUrl = hlTempoRestante.NavigateUrl;
                hlCodigo.NavigateUrl = hlTempoRestante.NavigateUrl;
                hlTempoRestante.Attributes.Add("onclick", "VisualizaChamado(" + hlCodigo.Text + ")");
                hlTecnico.Attributes.Add("onclick", "VisualizaChamado(" + hlCodigo.Text + ")");
                hlCodigo.Attributes.Add("onclick", "VisualizaChamado(" + hlCodigo.Text + ")");
            }
        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion
}