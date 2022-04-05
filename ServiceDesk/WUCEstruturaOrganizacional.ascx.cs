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

public partial class WUCEstruturaOrganizacional : System.Web.UI.UserControl
{
    #region Page_Load
    /// <summary>
    /// Evento que ocorre quando carregada a página
    /// </summary>
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                divMensagem.Visible = false;

                if (Request.QueryString["codestrutura"] != null)
                    ViewState["codigo-estrutura"] = Request.QueryString["codestrutura"].ToString();

                if (ViewState["codigo-estrutura"] != null)
                {
                    SServiceDesk.Negocio.ClsEstruturaOrganizacional.geraDropDownListExcecao(ddlEstruturaSuperior, Convert.ToInt32(ViewState["codigo-estrutura"]));
                }
                else
                {
                    SServiceDesk.Negocio.ClsEstruturaOrganizacional.geraDropDownList(ddlEstruturaSuperior);
                }

                ddlEstruturaSuperior.Items.Insert(0, "--");
                ddlEstruturaSuperior.Items[0].Value = String.Empty;

                SServiceDesk.Negocio.ClsTipoEstruturaOrganizacional.geraDropDownList(ddlTipoEstrutura);
                ddlTipoEstrutura.Items.Insert(0, "--");
                ddlTipoEstrutura.Items[0].Value = String.Empty;

                SServiceDesk.Negocio.ClsPessoa.geraDropDownList(ddlResponsavel);
                ddlResponsavel.Items.Insert(0, "--");
                ddlResponsavel.Items[0].Value = String.Empty;

                ServiceDesk.Negocio.ClsStatusTabela.geraDropDownList(ddlStatus, "EstruturaOrganizacional");


                //ddlStatus.Items.Insert(1, "Inativo");
                //ddlStatus.Items[1].Value = "INA";    
                //ddlStatus.Items.Insert(1, "Inativo");
                //ddlStatus.Items[1].Value = "INA";

                if (ViewState["codigo-estrutura"] != null)
                {
                    SServiceDesk.Negocio.ClsEstruturaOrganizacional objEstrutura = new SServiceDesk.Negocio.ClsEstruturaOrganizacional(Convert.ToInt32(ViewState["codigo-estrutura"]));

                    lblIDEstrutura.Text = ServiceDesk.Generica.ClsTexto.trocaHtmlPorAspa(objEstrutura.Codigo.Valor);
                    txtEstruturaOrganizacional.Text = ServiceDesk.Generica.ClsTexto.trocaHtmlPorAspa(objEstrutura.Descricao.Valor);
                    ddlEstruturaSuperior.SelectedValue = ServiceDesk.Generica.ClsTexto.trocaHtmlPorAspa(objEstrutura.EstruturaSuperior.Valor);
                    ddlTipoEstrutura.SelectedValue = ServiceDesk.Generica.ClsTexto.trocaHtmlPorAspa(objEstrutura.TipoEstruturaCodigo.Valor);
                    txtSigla.Text = ServiceDesk.Generica.ClsTexto.trocaHtmlPorAspa(objEstrutura.Sigla.Valor);

                    if (ServiceDesk.Generica.ClsTexto.trocaHtmlPorAspa(objEstrutura.Status.Valor) == String.Empty)
                    {
                        ddlStatus.SelectedIndex = 2;
                    }
                    else
                    {
                        ddlStatus.SelectedValue = ServiceDesk.Generica.ClsTexto.trocaHtmlPorAspa(objEstrutura.Status.Valor);
                    }

                    txtEndereco.Text = ServiceDesk.Generica.ClsTexto.trocaHtmlPorAspa(objEstrutura.Endereco.Valor);
                    txtTelefone.Text = ServiceDesk.Generica.ClsTexto.trocaHtmlPorAspa(objEstrutura.Telefone.Valor);
                    txtFax.Text = ServiceDesk.Generica.ClsTexto.trocaHtmlPorAspa(objEstrutura.Fax.Valor);
                    txtCnpj.Text = ServiceDesk.Generica.ClsTexto.trocaHtmlPorAspa(objEstrutura.Cnpj.Valor);
                    ddlResponsavel.SelectedValue = ServiceDesk.Generica.ClsTexto.trocaHtmlPorAspa(objEstrutura.Responsavel.Valor);

                    btnExcluir.Visible = true;

                    // Adiciona um evento javascript no botão Excluir
                    btnExcluir.Attributes.Add("onclick", "return verifica();");

                    objEstrutura = null;
                }
                else
                {
                    ServiceDesk.Negocio.ClsIdentificador objIdentificador = new ServiceDesk.Negocio.ClsIdentificador();
                    objIdentificador.Tabela.Valor = "EstruturaOrganizacional";
                    lblIDEstrutura.Text = objIdentificador.getProximoValor().ToString();
                    objIdentificador = null;
                }

            }
        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region btnSalvar_Click
    protected void btnSalvar_Click(object sender, EventArgs e)
    {
        try
        {
            String strMensagem = String.Empty;

            ServiceDesk.Negocio.ClsIdentificador objIdentificador = new ServiceDesk.Negocio.ClsIdentificador();
            SServiceDesk.Negocio.ClsEstruturaOrganizacional objEstrutura = new SServiceDesk.Negocio.ClsEstruturaOrganizacional();
            objIdentificador.Tabela.Valor = objEstrutura.Atributos.NomeTabela;

            objEstrutura.Descricao.Valor = ServiceDesk.Generica.ClsTexto.trocaAspaPorHtml(txtEstruturaOrganizacional.Text);
            objEstrutura.EstruturaSuperior.Valor = ServiceDesk.Generica.ClsTexto.trocaAspaPorHtml(ddlEstruturaSuperior.SelectedValue);
            objEstrutura.TipoEstruturaCodigo.Valor = ServiceDesk.Generica.ClsTexto.trocaAspaPorHtml(ddlTipoEstrutura.SelectedValue);
            objEstrutura.Sigla.Valor = ServiceDesk.Generica.ClsTexto.trocaAspaPorHtml(txtSigla.Text);
            objEstrutura.Status.Valor = ServiceDesk.Generica.ClsTexto.trocaAspaPorHtml(ddlStatus.SelectedValue);
            objEstrutura.Endereco.Valor = ServiceDesk.Generica.ClsTexto.trocaAspaPorHtml(txtEndereco.Text);
            objEstrutura.Telefone.Valor = ServiceDesk.Generica.ClsTexto.trocaAspaPorHtml(txtTelefone.Text);
            objEstrutura.Fax.Valor = ServiceDesk.Generica.ClsTexto.trocaAspaPorHtml(txtFax.Text);
            objEstrutura.Cnpj.Valor = ServiceDesk.Generica.ClsTexto.trocaAspaPorHtml(txtCnpj.Text);
            objEstrutura.Responsavel.Valor = ServiceDesk.Generica.ClsTexto.trocaAspaPorHtml(ddlResponsavel.SelectedValue);

            // Altera
            if (ViewState["codigo-estrutura"] != null)
            {
                objEstrutura.Codigo.Valor = ViewState["codigo-estrutura"].ToString();
                if (objEstrutura.altera(out strMensagem))
                {
                    lblMensagem.Text = strMensagem;
                    imgIcone.ImageUrl = "images/icones/aviso.gif";
                    divMensagem.Visible = true;
                    btnExcluir.Visible = true;
                }
                else
                {
                    lblMensagem.Text = strMensagem;
                    imgIcone.ImageUrl = "images/icones/aviso.gif";
                    divMensagem.Visible = true;
                }
            }
            else // Insere
            {
                objEstrutura.Codigo.Valor = objIdentificador.getProximoValor().ToString();

                if (objEstrutura.insere(out strMensagem))
                {
                    objIdentificador.atualizaValor();

                    lblMensagem.Text = strMensagem;
                    imgIcone.ImageUrl = "images/icones/aviso.gif";
                    divMensagem.Visible = true;
                    btnExcluir.Visible = true;

                    ViewState["codigo-estrutura"] = objEstrutura.Codigo.Valor;
                }
                else
                {
                    lblMensagem.Text = strMensagem;
                    imgIcone.ImageUrl = "images/icones/aviso.gif";
                    divMensagem.Visible = true;
                }
            }

            objIdentificador = null;
            objEstrutura = null;
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
            txtEstruturaOrganizacional.Text = String.Empty;
            ddlEstruturaSuperior.ClearSelection();
            ddlTipoEstrutura.ClearSelection();
            txtSigla.Text = String.Empty;
            ddlStatus.ClearSelection();
            txtEndereco.Text = String.Empty;
            txtTelefone.Text = String.Empty;
            txtFax.Text = String.Empty;
            txtCnpj.Text = String.Empty;
            ddlResponsavel.SelectedValue = String.Empty;
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
            ViewState["codigo-estrutura"] = null;

            ServiceDesk.Negocio.ClsIdentificador objIdentificador = new ServiceDesk.Negocio.ClsIdentificador();
            objIdentificador.Tabela.Valor = "EstruturaOrganizacional";
            lblIDEstrutura.Text = objIdentificador.getProximoValor().ToString();
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
            if (ViewState["codigo-estrutura"] != null)
            {
                SServiceDesk.Negocio.ClsEstruturaOrganizacional objEstrutura = new SServiceDesk.Negocio.ClsEstruturaOrganizacional(Convert.ToInt32(ViewState["codigo-estrutura"]));

                if (objEstrutura.exclui())
                {
                    ViewState["codigo-estrutura"] = null;

                    lblMensagem.Text = "Item excluido com sucesso.";
                    imgIcone.ImageUrl = "images/icones/aviso.gif";
                    divMensagem.Visible = true;

                    ServiceDesk.Negocio.ClsIdentificador objIdentificador = new ServiceDesk.Negocio.ClsIdentificador();
                    objIdentificador.Tabela.Valor = objEstrutura.Atributos.NomeTabela;
                    lblIDEstrutura.Text = objIdentificador.getProximoValor().ToString();
                    objIdentificador = null;


                    btnExcluir.Visible = false;
                    LimpaCampos();

                    objIdentificador = null;
                }
                objEstrutura = null;
            }
        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
}