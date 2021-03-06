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

public partial class Meus_Incidentes : BasePage
{
    public string strLinkPagina = string.Empty;
    public string strFimLinkPagina = string.Empty;
    public int intCodigoFuncao = 3;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            CheckAcesso(intCodigoFuncao);

            //Esconde a mensagem de erro
            lblMensagem.Visible = false;
            divMensagem.Visible = false;

            if (!Page.IsPostBack)
            {
                //Gera as Drop Down list              
                ClsStatusTabela.geraDropDownList(ddlStatus, "Incidente");

                ClsIncidente objIncidente = new ClsIncidente();
                objIncidente.Codigo.CampoIdentificador = false;

                objIncidente.MatriculaInclusor.Valor = user.IDusuario.ToString(); ;
                objIncidente.MatriculaInclusor.CampoIdentificador = true;
                PesquisarIncidenteDoUsuario();
            }

            if (CheckPermissao(intCodigoFuncao))
            {
                strLinkPagina = "Javascript:VisualizaIncidente(";
                strFimLinkPagina = ")";
            }
            else
            {
                strLinkPagina = "#";
                strFimLinkPagina = "";
            }

        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, user.IDusuario.ToString(), this.Request.Path, "0", ex.ToString());
        }
    }

    protected void gvIncidente_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //condicao IF que exibe os dados no GridView (estado: n?o-edit?vel)
                if ((e.Row.RowState == DataControlRowState.Normal) || (e.Row.RowState == DataControlRowState.Alternate))
                {
                    Label lblIncidenteCodigo = (Label)e.Row.FindControl("lblIncidenteCodigo");

                    string strFormatoDataExibicao = ClsParametro.DataCompletaExibicao;
                    Label lblDataInclusao = (Label)e.Row.FindControl("lblDataInclusao");
                    lblDataInclusao.Text = Convert.ToDateTime(lblDataInclusao.Text.Trim()).ToString(strFormatoDataExibicao);

                    Label lblCodigoStatus = (Label)e.Row.FindControl("lblCodigoStatus");
                    Label lblDescricaoStatus = (Label)e.Row.FindControl("lblDescricaoStatus");
                    if (lblCodigoStatus.Text.Trim() != string.Empty)
                    {
                        lblDescricaoStatus.Text = ServiceDesk.Negocio.ClsStatus.getDescricaoStatus(lblCodigoStatus.Text);
                    }

                    Label lblCodigoProprietario = (Label)e.Row.FindControl("lblCodigoProprietario");
                    Label lblProprietario = (Label)e.Row.FindControl("lblProprietario");
                    if (lblCodigoProprietario.Text.Trim() != string.Empty)
                    {
                        lblProprietario.Text = ClsUsuario.getNomeUsuario(lblCodigoProprietario.Text.Trim());
                    }

                    Label lblCodigoPrioridade = (Label)e.Row.FindControl("lblCodigoPrioridade");
                    Label lblPrioridade = (Label)e.Row.FindControl("lblPrioridade");
                    if (lblCodigoPrioridade.Text.Trim() != string.Empty)
                    {
                        lblPrioridade.Text = ClsPrioridade.getPrioridadeDescricao(lblCodigoPrioridade.Text.Trim());
                    }

                    Label lblDataAlteracao = (Label)e.Row.FindControl("lblDataAlteracao");
                    if (lblDataAlteracao.Text.Trim() != string.Empty)
                    {
                        lblDataAlteracao.Text = Convert.ToDateTime(lblDataAlteracao.Text.Trim()).ToString(strFormatoDataExibicao);
                    }

                    Label lblCodigoAlterador = (Label)e.Row.FindControl("lblCodigoAlterador");
                    Label lblAlterador = (Label)e.Row.FindControl("lblAlterador");
                    if (lblCodigoAlterador.Text.Trim() != string.Empty)
                    {
                        lblAlterador.Text = ClsUsuario.getNomeUsuario(lblCodigoAlterador.Text.Trim());
                    }

                    Label lblCodigoSolicitante = (Label)e.Row.FindControl("lblCodigoSolicitante");
                    Label lblSolicitante = (Label)e.Row.FindControl("lblSolicitante");
                    if (lblCodigoSolicitante.Text.Trim() != string.Empty)
                    {
                        lblSolicitante.Text = ClsUsuario.getNomeUsuario(lblCodigoSolicitante.Text.Trim());
                    }

                    Label lblDataFinalizacao = (Label)e.Row.FindControl("lblDataFinalizacao");
                    if (lblDataFinalizacao.Text.Trim() != string.Empty)
                    {
                        lblDataFinalizacao.Text = Convert.ToDateTime(lblDataFinalizacao.Text.Trim()).ToString(strFormatoDataExibicao);
                    }

                    Label lblCodigoFinalizador = (Label)e.Row.FindControl("lblCodigoFinalizador");
                    Label lblFinalizador = (Label)e.Row.FindControl("lblFinalizador");
                    if (lblCodigoFinalizador.Text.Trim() != string.Empty)
                    {
                        lblFinalizador.Text = ClsUsuario.getNomeUsuario(lblCodigoFinalizador.Text.Trim());
                    }

                    Label lblDataAvaliacao = (Label)e.Row.FindControl("lblDataAvaliacao");
                    if (lblDataAvaliacao.Text.Trim() != string.Empty)
                    {
                        lblDataAvaliacao.Text = Convert.ToDateTime(lblDataAvaliacao.Text.Trim()).ToString(strFormatoDataExibicao);
                    }
                    //Label lblDataAgendamento = (Label)e.Row.FindControl("lblDataAgendamento");
                    //if (lblDataAgendamento.Text.Trim() != string.Empty)
                    //{
                    //  lblDataAgendamento.Text = Convert.ToDateTime(lblDataAgendamento.Text.Trim()).ToString(strFormatoDataExibicao);
                    //}

                    Label lblVip = (Label)e.Row.FindControl("lblVip");
                    Label lblTempoDeVida = (Label)e.Row.FindControl("lblTempoDeVida");
                    Label lblSlaInicio = (Label)e.Row.FindControl("lblSlaInicio");
                    Label lblSlaFim = (Label)e.Row.FindControl("lblSlaFim");

                    Image imgDetalhe1 = (Image)e.Row.FindControl("imgDetalhes1");
                    Image imgDetalhe2 = (Image)e.Row.FindControl("imgDetalhes2");

                    if (CheckPermissao(intCodigoFuncao))
                    {
                        imgDetalhe1.Visible = true;
                        imgDetalhe2.Visible = true;
                    }
                    else
                    {
                        imgDetalhe1.Visible = false;
                        imgDetalhe2.Visible = false;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, user.IDusuario.ToString(), this.Request.Path, "0", ex.ToString());
        }
    }

    protected void btnFiltra_Click(object sender, EventArgs e)
    {
        PesquisarIncidenteDoUsuario();
    }

    private void PesquisarIncidenteDoUsuario()
    {
        try
        {
            string strMensagem = string.Empty;

            string strSql = "SELECT * FROM incidente WHERE ";
            strSql += " ( ";
            strSql += " pessoa_codigo_solicitante = " + user.IDusuario.ToString();
            strSql += " OR ";
            strSql += " pessoa_codigo_proprietario = " + user.IDusuario.ToString();
            strSql += " ) ";

            if (txtCodigo.Text.Trim() != string.Empty)
                strSql += " AND incidente_codigo = '" + ServiceDesk.Generica.ClsTexto.trocaAspaPorHtml(txtCodigo.Text.Trim()) + "' ";

            if (txtDescricao.Text.Trim() != string.Empty)
                strSql += " AND descricao LIKE '%" + ServiceDesk.Generica.ClsTexto.trocaAspaPorHtml(txtDescricao.Text.Trim()) + "%' ";

            if (ddlStatus.SelectedValue != string.Empty)
                strSql += " AND status_codigo = '" + ddlStatus.SelectedValue + "' ";

            strSql += "ORDER BY prioridade_codigo, incidente_codigo";

            if (!ClsChamado.geraGridViewQuery(gvIncidentes, strSql, out strMensagem))
            {
                lblMensagem.Text = strMensagem;
                imgIcone.ImageUrl = "images/icones/aviso.gif";
                lblMensagem.Visible = true;
                divMensagem.Visible = true;
            }

            litCountChamados.Text = gvIncidentes.Rows.Count.ToString();
        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ClsLog.enumTipoLog.ERRO, user.IDusuario.ToString(), Request.Path, "0", ex.ToString());
        }
    }
}