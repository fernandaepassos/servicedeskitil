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

public partial class WUCLog : System.Web.UI.UserControl
{
    #region Declarações Publicas


    #endregion

    #region Declarações Privadas

    private int intCodigoIdentificador;
    private string strOrigemRelacionada;
    private System.Web.UI.WebControls.Unit strAltura;
    private System.Web.UI.WebControls.Unit strLargura;
    private bool bExibeLogStatus = false;

    #endregion

    #region Propriedades

    /// <summary>
    /// Codigo identificador do registro da tabela ralacionada.
    /// </summary>
    public int CodigoIdentificador
    {
        get
        { return intCodigoIdentificador; }
        set
        { intCodigoIdentificador = value; }
    }

    /// <summary>
    /// Tabela Relacionada
    /// </summary>
    public string OrigemRelacionada
    {
        get
        { return strOrigemRelacionada; }
        set
        { strOrigemRelacionada = value; }
    }
    
    /// <summary>
    /// Exibe Log de Status do registro, usando as mesmas informações de origem e identificador.
    /// </summary>
    public bool ExibirLogStatus
    {
        get
        { return bExibeLogStatus; }
        set
        { bExibeLogStatus = value; }
    }

    #endregion

    #region métodos

    public void MontaDados(int CodigoIdentificador)
    {
        try
        {
            if (CodigoIdentificador != 0)
            {
                //gera a drid de log
                ClsLog objLog = new ClsLog();
                objLog.Codigo.CampoIdentificador = false;
                objLog.Origem.Valor = OrigemRelacionada;
                objLog.Origem.CampoIdentificador = true;
                objLog.Identificador.Valor = CodigoIdentificador.ToString();
                objLog.Identificador.CampoIdentificador = true;
                ClsLog.geraGridView(gvLog, objLog, true);
                objLog = null;

                //gera a grid de status caso seja necessário
                if (ExibirLogStatus)
                {
                    pnlGrupoLogStatus.Visible = true;
                    pnlLogStatus.Visible = true;
                    ClsStatusLog objStatusLog = new ClsStatusLog();
                    objStatusLog.Codigo.CampoIdentificador = false;
                    objStatusLog.Tabela.Valor = OrigemRelacionada;
                    objStatusLog.Tabela.CampoIdentificador = true;
                    objStatusLog.TabelaIdentificador.Valor = CodigoIdentificador.ToString();
                    objStatusLog.TabelaIdentificador.CampoIdentificador = true;
                    ServiceDesk.Negocio.ClsStatusLog.geraGridView(gvLogStatus, objStatusLog, true);
                    objStatusLog = null;
                }

                //Se as grid estão vazias nao exibe os painéis
                if (gvLog.Rows.Count <= 0)
                {
                    pnlGrupoLog.GroupingText = "Log de Operações (Sem dados para exibir)";
                }
                if (gvLogStatus.Rows.Count <= 0)
                {
                    pnlGrupoLogStatus.GroupingText = "Log de Status (Sem dados para exibir)";
                }

            }
        }
        catch (Exception ex)
        {
            ExibeMensagem("Ocorreu um erro. Verifique o parâmetro informado.", "images/icones/aviso.gif", true);

            //Grava Log de Erro
            ClsLog.insereLog(ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), Request.Path, "0", ex.ToString());
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
            Label lblMensagem = (Label)Parent.FindControl("lblMensagem");
            Image imgIcone = (Image)Parent.FindControl("imgIcone");
            HtmlControl divMensagem = (HtmlControl)Parent.FindControl("divMensagem");

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
            //Grava Log de Erro
            ClsLog.insereLog(ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), Request.Path, "0", ex.ToString());
        }
    }

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void gvLog_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            try
            {
                Label lblData = (Label)e.Row.FindControl("lblData");
                Label lblCodigoPessoa = (Label)e.Row.FindControl("lblCodigoPessoa");
                Label lblNomePessoa = (Label)e.Row.FindControl("lblNomePessoa");

                string strFormatoDataExibicao = ClsParametro.DataCompletaExibicao;
                lblData.Text = Convert.ToDateTime(lblData.Text.Trim()).ToString(strFormatoDataExibicao);

                if (lblCodigoPessoa.Text.Trim() != string.Empty)
                {
                    try
                    {
                        lblNomePessoa.Text = ClsUsuario.getNomeUsuario(lblCodigoPessoa.Text.Trim());
                    }
                    catch { }
                }
            }
            catch (Exception ex)
            {
                //Grava Log de Erro
                ClsLog.insereLog(ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), Request.Path, "0", ex.ToString());
            }
        }
    }

    protected void gvLogStatus_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            try
            {
                Label lblData = (Label)e.Row.FindControl("lblDataStatus");
                Label lblCodigoPessoa = (Label)e.Row.FindControl("lblCodigoPessoaStatus");
                Label lblNomePessoa = (Label)e.Row.FindControl("lblNomePessoaStatus");
                Label lblStatusOrigem = (Label)e.Row.FindControl("lblStatusOrigem");
                Label lblStatusDestino = (Label)e.Row.FindControl("lblStatusDestino");

                string strFormatoDataExibicao = ClsParametro.DataCompletaExibicao;
                lblData.Text = Convert.ToDateTime(lblData.Text.Trim()).ToString(strFormatoDataExibicao);

                if (lblCodigoPessoa.Text.Trim() != string.Empty)
                {
                    try
                    {
                        lblNomePessoa.Text = ClsUsuario.getNomeUsuario(lblCodigoPessoa.Text.Trim());
                    }
                    catch { }
                }

                if (lblStatusOrigem.Text.Trim() != string.Empty)
                {
                    try
                    {
                        lblStatusOrigem.Text = ClsStatus.getDescricaoStatus(lblStatusOrigem.Text.Trim());
                    }
                    catch { }
                }

                if (lblStatusDestino.Text.Trim() != string.Empty)
                {
                    try
                    {
                        lblStatusDestino.Text = ClsStatus.getDescricaoStatus(lblStatusDestino.Text.Trim());
                    }
                    catch { }
                }
            }
            catch (Exception ex)
            {
                //Grava Log de Erro
                ClsLog.insereLog(ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), Request.Path, "0", ex.ToString());
            }
        }
    }
}
