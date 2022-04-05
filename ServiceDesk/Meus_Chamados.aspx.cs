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

public partial class Meus_Chamados : BasePage
{
    public string strLinkPagina = string.Empty;
    public string strFimLinkPagina = string.Empty;
    public int intCodigoFuncao = 1;

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
                //Gera as Drop Down list Status
                ClsStatusTabela.geraDropDownList(ddlStatus, "Chamado");

                //Gera as Drop Down list Prioridade
                ClsPrioridade.geraDropDownList(ddlPrioridade);

                //Gera as Drop Down list Tipo
                ClsTipoChamado.geraDropDownList(ddlTipo);

                PesquisarChamadoDoUsuario();
            }

            if (CheckPermissao(intCodigoFuncao))
            {
                strLinkPagina = "Javascript:VisualizaChamado(";
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
            //ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, user.IDusuario.ToString(), this.Request.Path, "0", ex.ToString());
        }
    }

    /// <summary>
    /// Fornece um meio de acesso ao painel de mensagem
    /// </summary>
    /// <param name="Mensagem">Mensagem a ser exibida na tela</param>
    /// <param name="Imagem">Nome da imagem do ícone do painel</param>
    /// <param name="Ativo">true para Exibir, false para Ocultar</param>
    /// <example>ExibeMensagem("teste","images/icones/aviso.gif",true)</example>
    private void ExibeMensagem(String Mensagem, String Imagem, bool Ativo)
    {
        try
        {
            Label lblMensagem = (Label)Page.FindControl("lblMensagem");
            Image imgIcone = (Image)Page.FindControl("imgIcone");
            HtmlControl divMensagem = (HtmlControl)Page.FindControl("divMensagem");

            lblMensagem.Text = Mensagem;
            imgIcone.ImageUrl = Imagem;

            if (Ativo == true)
            {
                lblMensagem.Visible = true;
                divMensagem.Visible = true;
            }
            else if (Ativo == false)
            {
                lblMensagem.Visible = false;
                divMensagem.Visible = false;
            }
            else //nao foi especificado, assume true
            {
                lblMensagem.Visible = true;
                divMensagem.Visible = true;
            }
        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, user.IDusuario.ToString(), this.Request.Path, "0", ex.ToString());
        }

    }

    /// <summary>
    /// >>Alterado por Thiago Oliveira
    /// >>Foi retirada o campo proprietário e adicionado prioridade e tipo
    /// >>Data alteração: 30/06/2017
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

                    Label lblChamadoCodigo = (Label)e.Row.FindControl("lblChamadoCodigo");
                    //Image imgDetalhes1 = (Image)e.Row.FindControl("imgDetalhes1");
                    //Image imgDetalhes2 = (Image)e.Row.FindControl("imgDetalhes2");

                    ////Verifica a permissao do papel do usuario no chamado
                    //imgDetalhes1.Visible = ClsUsuario.verificaAcessoPapel(Convert.ToInt32(ClsUsuario.getCodigoUsuario()), Convert.ToInt32(lblChamadoCodigo.Text), 4, "chamado");
                    //imgDetalhes2.Visible = ClsUsuario.verificaAcessoPapel(Convert.ToInt32(ClsUsuario.getCodigoUsuario()), Convert.ToInt32(lblChamadoCodigo.Text), 4, "chamado");

                    string strFormatoDataExibicao = ClsParametro.DataCompletaExibicao;
                    Label lblDataInclusao = (Label)e.Row.FindControl("lblDataInclusao");
                    lblDataInclusao.Text = Convert.ToDateTime(lblDataInclusao.Text.Trim()).ToString(strFormatoDataExibicao);

                    Label lblCodigoStatus = (Label)e.Row.FindControl("lblCodigoStatus");
                    Label lblDescricaoStatus = (Label)e.Row.FindControl("lblDescricaoStatus");
                    if (lblCodigoStatus.Text.Trim() != string.Empty)
                    {
                        lblDescricaoStatus.Text = ServiceDesk.Negocio.ClsStatus.getDescricaoStatus(lblCodigoStatus.Text);
                    }

                    Label lblTipoCodigo = (Label)e.Row.FindControl("lblTipoCodigo");
                    Label lblTipo = (Label)e.Row.FindControl("lblTipo");
                    if (lblTipoCodigo.Text.Trim() != string.Empty)
                    {
                        lblTipo.Text = ClsTipoChamado.getDescricaoTipo(lblTipoCodigo.Text.Trim());
                    }
                    //Removido a pedido do chamado 930
                    //Label lblCodigoProprietario = (Label)e.Row.FindControl("lblCodigoProprietario");
                    //Label lblProprietario = (Label)e.Row.FindControl("lblProprietario");
                    //if (lblCodigoProprietario.Text.Trim() != string.Empty)
                    //{
                    //    lblProprietario.Text = ClsUsuario.getNomeUsuario(lblCodigoProprietario.Text.Trim());
                    //}

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
                    Label lblDataAgendamento = (Label)e.Row.FindControl("lblDataAgendamento");
                    if (lblDataAgendamento.Text.Trim() != string.Empty)
                    {
                        lblDataAgendamento.Text = Convert.ToDateTime(lblDataAgendamento.Text.Trim()).ToString(strFormatoDataExibicao);
                    }

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
        PesquisarChamadoDoUsuario();
    }

    /// <summary>
    /// >>Adicionado dois filtros (prioridade e tipo) e restringindo a pesquisa pelas primeiros 30 registros
    /// >>Alterador por : Thiago Oliveira
    /// >>Alterado dia 30/06/2017
    /// </summary>
    private void PesquisarChamadoDoUsuario()
    {
        try
        {
            string strMensagem = string.Empty;

            //seleciona apenas 30 registros
            string strSql = " SELECT top 30 * FROM chamado WHERE 1 = 1";

            //Comentar para listar sempre resultados independentemente do usuário (debug)
            strSql += " AND pessoa_codigo_alocacao = " + user.IDusuario.ToString();

            if (txtCodigo.Text.Trim() != string.Empty)
                strSql += " AND chamado_codigo = " + ServiceDesk.Generica.ClsTexto.trocaAspaPorHtml(txtCodigo.Text.Trim());

            if (txtDescricao.Text.Trim() != string.Empty)
                strSql += " AND descricao LIKE '%" + ServiceDesk.Generica.ClsTexto.trocaAspaPorHtml(txtDescricao.Text.Trim()) + "%' ";

            if (ddlStatus.SelectedValue != string.Empty)
                strSql += " AND status_codigo = " + ddlStatus.SelectedValue;

            if (ddlPrioridade.SelectedValue != string.Empty)
                strSql += " AND prioridade_codigo = " + ddlPrioridade.SelectedValue;

            if (ddlTipo.SelectedValue != string.Empty)
                strSql += " AND chamado_tipo_codigo = " + ddlTipo.SelectedValue;

            strSql += " ORDER BY chamado_codigo DESC";

            ClsChamado.geraGridViewQuery(gvChamados, strSql, out strMensagem);
            litCountChamados.Text = gvChamados.Rows.Count.ToString();

            if (strMensagem.Trim() != string.Empty)
            {
                ExibeMensagem(strMensagem, "images/icones/aviso.gif", true);
            }
        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, user.IDusuario.ToString(), this.Request.Path, "0", ex.ToString());
        }

    }

}