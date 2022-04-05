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

public partial class WUCAplicacao : System.Web.UI.UserControl
{

    #region Page_Load
    /// <summary>
    /// Evento que ocorre quando carregada a página
    /// </summary>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            try
            {
                divMensagem.Visible = false;

                if (Request.QueryString["codaplicacao"] != null)
                    ViewState["codigo-aplicacao"] = Request.QueryString["codaplicacao"].ToString();

                PopulaAplicacaoSuperior();

                if (ViewState["codigo-aplicacao"] != null)
                {
                    SServiceDesk.Negocio.ClsAplicacao objAplicacao = new SServiceDesk.Negocio.ClsAplicacao(Convert.ToInt32(ViewState["codigo-aplicacao"]));

                    lblIDAplicacao.Text = ServiceDesk.Generica.ClsTexto.trocaHtmlPorAspa(objAplicacao.Codigo.Valor);
                    txtDescricao.Text = ServiceDesk.Generica.ClsTexto.trocaHtmlPorAspa(objAplicacao.Descricao.Valor);
                    ddlAplicacaoSuperior.SelectedValue = ServiceDesk.Generica.ClsTexto.trocaHtmlPorAspa(objAplicacao.AplicacaoSuperior.Valor);
                    txtPrecoReal.Text = ServiceDesk.Generica.ClsTexto.trocaHtmlPorAspa(objAplicacao.PrecoReal.Valor);
                    txtPrecoDolar.Text = ServiceDesk.Generica.ClsTexto.trocaHtmlPorAspa(objAplicacao.PrecoDolar.Valor);

                    if (objAplicacao.TipoLicensa.Valor != String.Empty)
                        rdbTipoLicenca.SelectedValue = objAplicacao.TipoLicensa.Valor;

                    txtNumeroLicencas.Text = ServiceDesk.Generica.ClsTexto.trocaHtmlPorAspa(objAplicacao.QuantidadeLicenca.Valor);
                    txtSigla.Text = ServiceDesk.Generica.ClsTexto.trocaHtmlPorAspa(objAplicacao.Sigla.Valor);

                    if (objAplicacao.FlagIstalacaoPadrao.Valor.ToUpper().Trim() == "S")
                        ckInstalacaoPadrao.Checked = true;
                    else
                        ckInstalacaoPadrao.Checked = false;

                    if (objAplicacao.FlagPermissaoAcesso.Valor.ToUpper().Trim() == "S")
                        ckPermissaoAcesso.Checked = true;
                    else
                        ckPermissaoAcesso.Checked = false;

                    if (objAplicacao.FlagAgendar.Valor.ToUpper().Trim() == "S")
                        ckAgendar.Checked = true;
                    else
                        ckAgendar.Checked = false;

                    if (objAplicacao.FlagVisibilidade.Valor.ToUpper().Trim() == "S")
                        ckVisibilidade.Checked = true;
                    else
                        ckVisibilidade.Checked = false;

                    if (objAplicacao.FlagAprovacao.Valor.ToUpper().Trim() == "S")
                        ckAprovacao.Checked = true;
                    else
                        ckAprovacao.Checked = false;

                    txtLocalizacaoInterna.Text = ServiceDesk.Generica.ClsTexto.trocaHtmlPorAspa(objAplicacao.LocalizacaoInterna.Valor);
                    txtLocalizacaoExterna.Text = ServiceDesk.Generica.ClsTexto.trocaHtmlPorAspa(objAplicacao.LocalizacaoExterna.Valor);
                    txtVersao.Text = ServiceDesk.Generica.ClsTexto.trocaHtmlPorAspa(objAplicacao.Versao.Valor);

                    btnExcluir.Visible = true;

                    // Adiciona um evento javascript no botão Excluir
                    btnExcluir.Attributes.Add("onclick", "return verifica();");

                    objAplicacao = null;
                }
                else
                {
                    ServiceDesk.Negocio.ClsIdentificador objIdentificador = new ServiceDesk.Negocio.ClsIdentificador();
                    objIdentificador.Tabela.Valor = "Aplicacao";
                    lblIDAplicacao.Text = objIdentificador.getProximoValor().ToString();
                    objIdentificador = null;
                }
            }
            catch (Exception ex)
            {
                ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
            }
        }
    }
    #endregion

    protected void PopulaAplicacaoSuperior()
    {
        if (ViewState["codigo-aplicacao"] != null)
        {
            SServiceDesk.Negocio.ClsAplicacao.geraDropDownListExcecao(ddlAplicacaoSuperior, Convert.ToInt32(ViewState["codigo-aplicacao"]));
        }
        else
        {
            SServiceDesk.Negocio.ClsAplicacao.geraDropDownList(ddlAplicacaoSuperior);
        }
        ddlAplicacaoSuperior.Items.Insert(0, "--");
        ddlAplicacaoSuperior.Items[0].Value = String.Empty;
    }

    #region btnSalvar_Click
    protected void btnSalvar_Click(object sender, EventArgs e)
    {
        try
        {
            String strMensagem = String.Empty;

            ServiceDesk.Negocio.ClsIdentificador objIdentificador = new ServiceDesk.Negocio.ClsIdentificador();
            SServiceDesk.Negocio.ClsAplicacao objAplicacao = new SServiceDesk.Negocio.ClsAplicacao();
            objIdentificador.Tabela.Valor = objAplicacao.Atributos.NomeTabela;

            objAplicacao.Descricao.Valor = ServiceDesk.Generica.ClsTexto.trocaHtmlPorAspa(txtDescricao.Text);
            objAplicacao.AplicacaoSuperior.Valor = ServiceDesk.Generica.ClsTexto.trocaHtmlPorAspa(ddlAplicacaoSuperior.SelectedValue);

            if (txtPrecoReal.Text != String.Empty)
                objAplicacao.PrecoReal.Valor = ServiceDesk.Generica.ClsTexto.trocaHtmlPorAspa(Convert.ToDouble(txtPrecoReal.Text).ToString());
            else
                objAplicacao.PrecoReal.Valor = "0";

            if (txtPrecoDolar.Text != String.Empty)
                objAplicacao.PrecoDolar.Valor = ServiceDesk.Generica.ClsTexto.trocaHtmlPorAspa(Convert.ToDouble(txtPrecoDolar.Text).ToString());
            else
                objAplicacao.PrecoDolar.Valor = "0";

            objAplicacao.TipoLicensa.Valor = rdbTipoLicenca.SelectedValue;
            objAplicacao.QuantidadeLicenca.Valor = ServiceDesk.Generica.ClsTexto.trocaHtmlPorAspa(txtNumeroLicencas.Text);
            objAplicacao.Sigla.Valor = ServiceDesk.Generica.ClsTexto.trocaHtmlPorAspa(txtSigla.Text);

            if (ckInstalacaoPadrao.Checked)
                objAplicacao.FlagIstalacaoPadrao.Valor = "S";
            else
                objAplicacao.FlagIstalacaoPadrao.Valor = "N";

            if (ckPermissaoAcesso.Checked)
                objAplicacao.FlagPermissaoAcesso.Valor = "S";
            else
                objAplicacao.FlagPermissaoAcesso.Valor = "N";

            if (ckAgendar.Checked)
                objAplicacao.FlagAgendar.Valor = "S";
            else
                objAplicacao.FlagAgendar.Valor = "N";

            if (ckVisibilidade.Checked)
                objAplicacao.FlagVisibilidade.Valor = "S";
            else
                objAplicacao.FlagVisibilidade.Valor = "N";

            if (ckAprovacao.Checked)
                objAplicacao.FlagAprovacao.Valor = "S";
            else
                objAplicacao.FlagAprovacao.Valor = "N";

            objAplicacao.LocalizacaoInterna.Valor = ServiceDesk.Generica.ClsTexto.trocaHtmlPorAspa(txtLocalizacaoInterna.Text);
            objAplicacao.LocalizacaoExterna.Valor = ServiceDesk.Generica.ClsTexto.trocaHtmlPorAspa(txtLocalizacaoExterna.Text);
            objAplicacao.Versao.Valor = ServiceDesk.Generica.ClsTexto.trocaHtmlPorAspa(txtVersao.Text);

            // Altera
            if (ViewState["codigo-aplicacao"] != null)
            {
                objAplicacao.Codigo.Valor = ViewState["codigo-aplicacao"].ToString();
                if (objAplicacao.altera(out strMensagem))
                {
                    lblMensagem.Text = strMensagem;
                    imgIcone.ImageUrl = "images/icones/aviso.gif";
                    divMensagem.Visible = true;
                    btnExcluir.Visible = true;
                }
                else
                {
                    lblMensagem.Text = "Não foi possivel realizar a operação";
                    imgIcone.ImageUrl = "images/icones/aviso.gif";
                    divMensagem.Visible = true;
                }
            }
            else // Insere
            {
                objAplicacao.Codigo.Valor = objIdentificador.getProximoValor().ToString();

                if (objAplicacao.insere(out strMensagem))
                {
                    objIdentificador.atualizaValor();

                    lblMensagem.Text = strMensagem;
                    imgIcone.ImageUrl = "images/icones/aviso.gif";
                    divMensagem.Visible = true;
                    btnExcluir.Visible = true;

                    ViewState["codigo-aplicacao"] = objAplicacao.Codigo.Valor;
                }
                else
                {
                    lblMensagem.Text = strMensagem;
                    imgIcone.ImageUrl = "images/icones/aviso.gif";
                    divMensagem.Visible = true;
                }
            }

            PopulaAplicacaoSuperior();

            ddlAplicacaoSuperior.SelectedValue = objAplicacao.AplicacaoSuperior.Valor;

            objIdentificador = null;
            objAplicacao = null;
        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region
    protected void LimpaCampos()
    {
        try
        {
            PopulaAplicacaoSuperior();

            txtDescricao.Text = String.Empty;
            ddlAplicacaoSuperior.ClearSelection();
            txtPrecoReal.Text = String.Empty;
            txtPrecoDolar.Text = String.Empty;
            rdbTipoLicenca.ClearSelection();
            txtNumeroLicencas.Text = String.Empty;
            txtSigla.Text = String.Empty;
            ckInstalacaoPadrao.Checked = false;
            ckPermissaoAcesso.Checked = false;
            ckAgendar.Checked = false;
            ckVisibilidade.Checked = false;
            ckAprovacao.Checked = false;
            txtLocalizacaoInterna.Text = String.Empty;
            txtLocalizacaoExterna.Text = String.Empty;
            txtVersao.Text = String.Empty;

        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion
    protected void btnNovo_Click(object sender, EventArgs e)
    {
        try
        {
            LimpaCampos();
            ViewState["codigo-aplicacao"] = null;

            ServiceDesk.Negocio.ClsIdentificador objIdentificador = new ServiceDesk.Negocio.ClsIdentificador();
            objIdentificador.Tabela.Valor = "Aplicacao";
            lblIDAplicacao.Text = objIdentificador.getProximoValor().ToString();
            objIdentificador = null;
            btnExcluir.Visible = false;
        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }

    protected void btnExcluir_Click(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["codigo-aplicacao"] != null)
            {
                SServiceDesk.Negocio.ClsAplicacao objAplicacao = new SServiceDesk.Negocio.ClsAplicacao(Convert.ToInt32(ViewState["codigo-aplicacao"]));

                if (objAplicacao.exclui())
                {
                    ViewState["codigo-aplicacao"] = null;

                    lblMensagem.Text = "Item excluido com sucesso.";
                    imgIcone.ImageUrl = "images/icones/aviso.gif";
                    divMensagem.Visible = true;

                    ServiceDesk.Negocio.ClsIdentificador objIdentificador = new ServiceDesk.Negocio.ClsIdentificador();
                    objIdentificador.Tabela.Valor = "Aplicacao";
                    lblIDAplicacao.Text = objIdentificador.getProximoValor().ToString();
                    objIdentificador = null;

                    btnExcluir.Visible = false;
                    LimpaCampos();

                    objIdentificador = null;
                }
                objAplicacao = null;
            }
        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
}