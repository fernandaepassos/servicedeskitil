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

public partial class WUCSegurancaDireitoPapel : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //Verificar acessibilidade
        /*int intCodigoFuncao = 45;
        if (!ServiceDesk.Negocio.ClsUsuario.verificaAcessoUsuarioFuncao(ClsUsuario.getCodigoUsuario(ClsUsuario.getCodigoRede()), intCodigoFuncao.ToString(), ClsUsuario.getTipoUsuarioCodigo(ClsUsuario.getCodigoRede()).ToString()))
        {
            Response.Redirect("AcessoNegado.aspx", false);
            return;
        }*/

        try
        {
            //Mantém a posição do Scroll após o PostBack
            Page.MaintainScrollPositionOnPostBack = true;

            //Esconde a mensagem de erro
            divMensagem.Visible = false;

            if (!Page.IsPostBack)
            {
                SServiceDesk.Negocio.ClsSegurancaPapel.geraDropDownList(ddlPapel);
                ddlPapel.Items.Insert(0, "--");
                ddlPapel.Items[0].Value = "";

                SServiceDesk.Negocio.ClsSegurancaDireitoPapel.geraDropDownTabelas(ddlTabela);
                ddlTabela.Items.Insert(0, "--");
                ddlTabela.Items[0].Value = "";

                if (ddlPapel.SelectedValue != String.Empty && ddlTabela.SelectedValue != String.Empty)
                    SServiceDesk.Negocio.ClsSegurancaDireito.geraGridView(gvSegurancaDireitoPapel, Convert.ToInt32(ddlPapel.SelectedValue), ddlTabela.SelectedValue);
            }
        }

        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }

    #region Salva Direitos
    /// <summary>
    /// Método que salva uma Equipe
    /// </summary>
    protected void btnSalvar_Click(object sender, EventArgs e)
    {
        try
        {
            int intContador = 0;
            int intContInclusos = 0;
            string strMensagem = string.Empty;
            bool bolSelecionado = false;

            ServiceDesk.Negocio.ClsIdentificador objIdentificador = new ServiceDesk.Negocio.ClsIdentificador();
            SServiceDesk.Negocio.ClsSegurancaDireitoPapel objSegurancaDireitoPapel = new SServiceDesk.Negocio.ClsSegurancaDireitoPapel();

            if (ddlTabela.SelectedValue != String.Empty && ddlPapel.SelectedValue != String.Empty)
            {
                if (SServiceDesk.Negocio.ClsSegurancaDireitoPapel.exclui(ddlTabela.SelectedValue, Convert.ToInt32(ddlPapel.SelectedValue)))
                {
                    for (intContador = 0; intContador < gvSegurancaDireitoPapel.Rows.Count; intContador++)
                    {
                        GridViewRow objRow = (GridViewRow)gvSegurancaDireitoPapel.Rows[intContador];
                        if (objRow != null)
                        {
                            CheckBox ckAtribuirDireito = (CheckBox)objRow.FindControl("ckAtribuirDireito");
                            Label lblCodigoDireitoSeguranca = (Label)objRow.FindControl("lblCodigoDireitoSeguranca");

                            if (ckAtribuirDireito.Checked == true)
                            {
                                bolSelecionado = true;
                                objIdentificador.Tabela.Valor = objSegurancaDireitoPapel.Atributos.NomeTabela;
                                objSegurancaDireitoPapel.Codigo.Valor = objIdentificador.getProximoValor().ToString();
                                objSegurancaDireitoPapel.SegurancaPapelCodigo.Valor = ServiceDesk.Generica.ClsTexto.trocaAspaPorHtml(ddlPapel.SelectedValue);
                                objSegurancaDireitoPapel.SegurancaDireitoCodigo.Valor = ServiceDesk.Generica.ClsTexto.trocaAspaPorHtml(lblCodigoDireitoSeguranca.Text);
                                objSegurancaDireitoPapel.Tabela.Valor = ServiceDesk.Generica.ClsTexto.trocaAspaPorHtml(ddlTabela.SelectedValue);

                                if (objSegurancaDireitoPapel.insere(out strMensagem))
                                {
                                    intContInclusos += 1;
                                    objIdentificador.atualizaValor();

                                    if (intContInclusos == 1)
                                        strMensagem = "1 direito atribuido ao papel.";
                                    else if (intContInclusos > 1)
                                        strMensagem = intContInclusos.ToString() + " direitos atribuidos ao papél.";
                                }
                            }
                        } //fim if Row
                        objRow = null;
                    }

                    // Só apresenta caso tenha inserido algum colaborador
                    if (intContInclusos > 0)
                    {
                        lblMensagem.Text = strMensagem;
                        imgIcone.ImageUrl = "images/icones/aviso.gif";
                        divMensagem.Visible = true;
                    }
                    else
                    {
                        if (!bolSelecionado)
                        {
                            lblMensagem.Text = "O papel e tabela selecionados não existe direito atribuído.";
                            imgIcone.ImageUrl = "images/icones/aviso.gif";
                            divMensagem.Visible = true;
                        }
                    }

                    if (ddlPapel.SelectedValue != String.Empty && ddlTabela.SelectedValue != String.Empty)
                        SServiceDesk.Negocio.ClsSegurancaDireito.geraGridView(gvSegurancaDireitoPapel, Convert.ToInt32(ddlPapel.SelectedValue), ddlTabela.SelectedValue);
                    else
                    {
                        lblMensagem.Text = "Selecione os campos Papel e Tabela.";
                        divMensagem.Visible = true;
                        imgIcone.ImageUrl = "images/icones/aviso.gif";
                    }
                }
                else
                {
                    lblMensagem.Text = "Selecione o Processo e o Papel";
                    divMensagem.Visible = true;
                    imgIcone.ImageUrl = "images/icones/aviso.gif";
                }
                objSegurancaDireitoPapel = null;
                objIdentificador = null;
            }
            else
            {
                lblMensagem.Text = "Selecione o Papel e a Tabela";
                divMensagem.Visible = true;
                imgIcone.ImageUrl = "images/icones/aviso.gif";
            }
        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region LimparCampos
    /// <summary>
    /// Método que limpa os campos de Inserção de Equipe 
    /// </summary>
    protected void btnLimpar_Click(object sender, EventArgs e)
    {
        ddlTabela.ClearSelection();
        ddlPapel.ClearSelection();
    }
    #endregion

    #region Evento gvSegurancaDireitoPapel_RowDataBound
    /// <summary>
    /// Carrega os dados na GridView
    /// </summary>
    protected void gvSegurancaDireitoPapel_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Condicao IF que exibe os dados no GridView (estado: não-editável)
                if ((e.Row.RowState.ToString() == "Normal") || (e.Row.RowState.ToString() == "Alternate"))
                {
                    // Carrega o campo Aplicação com a descrição da Aplicação e não o código
                    Label lblCodigoDireitoSeguranca = (Label)e.Row.FindControl("lblCodigoDireitoSeguranca");
                    Label lblDescricaoDireito = (Label)e.Row.FindControl("lblDescricaoDireito");

                    if (lblCodigoDireitoSeguranca.Text != String.Empty)
                    {
                        SServiceDesk.Negocio.ClsSegurancaDireito objSegurancaDireito = new SServiceDesk.Negocio.ClsSegurancaDireito(Convert.ToInt32(lblCodigoDireitoSeguranca.Text));
                        lblDescricaoDireito.Text = objSegurancaDireito.Descricao.Valor;
                    }

                    Label lblAtribuirDireito = (Label)e.Row.FindControl("lblAtribuirDireito");
                    CheckBox ckAtribuirDireito = (CheckBox)e.Row.FindControl("ckAtribuirDireito");
                    if (lblAtribuirDireito.Text != String.Empty && lblAtribuirDireito.Text != null)
                    {
                        ckAtribuirDireito.Checked = true;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    protected void btnLimpar_Click1(object sender, EventArgs e)
    {
        try
        {
            if (ddlPapel.SelectedValue != String.Empty && ddlTabela.SelectedValue != String.Empty)
                SServiceDesk.Negocio.ClsSegurancaDireito.geraGridView(gvSegurancaDireitoPapel, Convert.ToInt32(ddlPapel.SelectedValue), ddlTabela.SelectedValue);
            else
            {
                lblMensagem.Text = "Selecione os campos Papél e Tabela.";
                divMensagem.Visible = true;
                imgIcone.ImageUrl = "images/icones/aviso.gif";
            }
        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
}