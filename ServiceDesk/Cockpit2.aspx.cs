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

public partial class Cockpit2 : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.MaintainScrollPositionOnPostBack = true;

        CheckAcesso(18);
    }

    #region Eventos
    #region GridView
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvChamados_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //condicao IF que exibe os dados no GridView (estado: não-editável)
                if ((e.Row.RowState == DataControlRowState.Normal) || (e.Row.RowState == DataControlRowState.Alternate))
                {
                    Label lblCodigo = (Label)e.Row.FindControl("lblCodigo");
                    Label lblCodigoProprietario = (Label)e.Row.FindControl("lblCodigoProprietario");
                    Label lblDescricaoProprietario = (Label)e.Row.FindControl("lblDescricaoProprietario");
                    Label lblCodigoStatus = (Label)e.Row.FindControl("lblCodigoStatus");
                    Label lblDescricaoStatus = (Label)e.Row.FindControl("lblDescricaoStatus");
                    Label lblCodigoPrioridade = (Label)e.Row.FindControl("lblCodigoPrioridade");
                    Label lblDescricaoPrioridade = (Label)e.Row.FindControl("lblDescricaoPrioridade");
                    Label lblPrefixo = (Label)e.Row.FindControl("lblPrefixo");
                    HyperLink hplLink = (HyperLink)e.Row.FindControl("hplLink");
                    HyperLink hplLink1 = (HyperLink)e.Row.FindControl("hplLink1");

                    lblDescricaoStatus.Text = ClsStatus.getDescricaoStatus(lblCodigoStatus.Text);

                    try
                    {
                        SServiceDesk.Negocio.ClsPessoa objPessoa = new SServiceDesk.Negocio.ClsPessoa(Convert.ToInt32(lblCodigoProprietario.Text.Trim()));
                        lblDescricaoProprietario.Text = objPessoa.Nome.Valor;
                        objPessoa = null;
                    }
                    catch
                    {
                    }
                    try
                    {
                        SServiceDesk.Negocio.ClsPrioridade objPrioridade = new SServiceDesk.Negocio.ClsPrioridade(Convert.ToInt32(lblCodigoPrioridade.Text.Trim()));
                        lblDescricaoPrioridade.Text = objPrioridade.Descricao.Valor;
                        objPrioridade = null;
                    }
                    catch
                    {
                    }

                    switch (hfLabel.Value)
                    {
                        case "chamado":
                            {
                                lblPrefixo.Text = ClsParametro.ChamadoPrefixo;
                                hplLink.NavigateUrl = "#";
                                hplLink.Attributes.Add("onclick", "VisualizaChamado(" + lblCodigo.Text + ")");
                                hplLink1.NavigateUrl = "#";
                                hplLink1.Attributes.Add("onclick", "VisualizaChamado(" + lblCodigo.Text + ")");
                                break;
                            }
                        case "incidente":
                            {
                                lblPrefixo.Text = ClsTipoChamado.getPrefixoTipoChamado(ClsParametro.CodigoTipoChamadoIncidente);
                                hplLink.Attributes.Add("onclick", "VisualizaIncidente(" + lblCodigo.Text + ")");
                                hplLink.NavigateUrl = "#";
                                hplLink1.Attributes.Add("onclick", "VisualizaIncidente(" + lblCodigo.Text + ")");
                                hplLink1.NavigateUrl = "#";
                                break;
                            }
                        case "problema":
                            {
                                hplLink.NavigateUrl = "#";
                                hplLink.Attributes.Add("onclick", "NovaJanela('problema.aspx?CodProblema=" + lblCodigo.Text + "')");
                                hplLink1.NavigateUrl = "#";
                                hplLink1.Attributes.Add("onclick", "NovaJanela('problema.aspx?CodProblema=" + lblCodigo.Text + "')");
                                break;
                            }
                        case "requisicaoservico":
                            {
                                hplLink.NavigateUrl = "#";
                                hplLink.Attributes.Add("onclick", "VisualizaRequisicaoServico(" + lblCodigo.Text + ")");
                                hplLink1.NavigateUrl = "#";
                                hplLink1.Attributes.Add("onclick", "VisualizaRequisicaoServico(" + lblCodigo.Text + ")");
                                break;
                            }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), Request.Path, "0", ex.ToString());
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvChamados_Load(object sender, EventArgs e)
    {
        GridView objGridView = (GridView)sender;
        if (objGridView.Rows.Count > 0)
        {
            if (objGridView.Rows[0].Cells[0].Text == String.Empty)
            {
                objGridView.Visible = false;
            }
        }
        objGridView = null;
    }
    #endregion

    #region Pesquisar_Click
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Pesquisar_Click(object sender, EventArgs e)
    {
        try
        {
            ClsPainelProcesso objPainelProcesso = new ClsPainelProcesso();
            ClsPainelProcesso.TipoTabelaTreeView objTipoTabela = ClsPainelProcesso.TipoTabelaTreeView.CHAMADO;
            int intCodigo = 0;
            int intCodigoProprietario = 0;

            switch (ddlChamado.SelectedValue)
            {
                case "chamado":
                    {
                        objTipoTabela = ClsPainelProcesso.TipoTabelaTreeView.CHAMADO;
                        break;
                    }
                case "incidente":
                    {
                        objTipoTabela = ClsPainelProcesso.TipoTabelaTreeView.INCIDENTE;
                        break;
                    }
                case "problema":
                    {
                        objTipoTabela = ClsPainelProcesso.TipoTabelaTreeView.PROBLEMA;
                        break;
                    }
                case "requisicaoservico":
                    {
                        objTipoTabela = ClsPainelProcesso.TipoTabelaTreeView.REQUISICAOSERVICO;
                        break;
                    }
            }

            lblChamado.Text = ddlChamado.SelectedItem.Text;
            hfLabel.Value = ddlChamado.SelectedItem.Value;

            gvChamados.Dispose();
            gvChamados = null;

            try
            {
                intCodigo = Convert.ToInt32(txtCodigo.Text.Trim());
            }
            catch
            {
            }
            try
            {
                intCodigoProprietario = Convert.ToInt32(ddlProprietario.SelectedValue);
            }
            catch
            {
            }

            //pnlGridResultado.Visible = false;
            objPainelProcesso.contadorAgrupamentoTreeView(objTipoTabela, ClsPainelProcesso.TipoAgrupamentoTreeView.STATUS, trv_tabelas, intCodigo, txtDescricao.Text.Trim(), intCodigoProprietario);

            //mvLateral.ActiveViewIndex = 1;

            objPainelProcesso = null;
        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region Menu Lateral


    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void trv_tabelas_SelectedNodeChanged(object sender, EventArgs e)
    {
        try
        {
            TreeView objTreeView = (TreeView)sender;
            int intCodigo = 0;
            int intCodigoProprietario = 0;

            gvChamados.Visible = true;

            //mvLateral.ActiveViewIndex = 1;
            try
            {
                intCodigo = Convert.ToInt32(txtCodigo.Text.Trim());
            }
            catch
            {
            }
            try
            {
                intCodigoProprietario = Convert.ToInt32(ddlProprietario.SelectedValue);
            }
            catch
            {
            }
            //pnlGridResultado.Visible = true;
            //pnlSemaforo.Visible = false;

            ClsPainelProcesso.geraGridView(gvChamados, hfLabel.Value, objTreeView.SelectedNode.Value, intCodigo, txtDescricao.Text.Trim(), intCodigoProprietario);
            if (gvChamados.Rows.Count > 0)
            {
                lblMensagemGrid.Visible = false;
            }

            litCountChamados.Text = gvChamados.Rows.Count.ToString();

            objTreeView = null;
        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #endregion
}
