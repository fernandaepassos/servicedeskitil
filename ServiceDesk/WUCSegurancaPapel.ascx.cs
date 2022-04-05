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

public partial class WUCSegurancaPapel : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //Verificar acessibilidade
        /*int intCodigoFuncao = 43;

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
                //Gerando o GridView SegurancaPapel
                SServiceDesk.Negocio.ClsSegurancaPapel.geraGridView(gvSegurancaPapel);
            }

        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }

    #region Salva SegurancaPapel
    /// <summary>
    /// Método que salva um SegurancaPapel
    /// </summary>
    protected void btnSalvar_Click(object sender, EventArgs e)
    {
        try
        {
            String strMensagem = String.Empty;

            SServiceDesk.Negocio.ClsSegurancaPapel objSegurancaPapel = new SServiceDesk.Negocio.ClsSegurancaPapel();
            ServiceDesk.Negocio.ClsIdentificador objIdentificador = new ServiceDesk.Negocio.ClsIdentificador();

            objIdentificador.Tabela.Valor = objSegurancaPapel.Atributos.NomeTabela;
            objSegurancaPapel.Codigo.Valor = objIdentificador.getProximoValor().ToString();
            objSegurancaPapel.Descricao.Valor = ServiceDesk.Generica.ClsTexto.trocaAspaPorHtml(txtDescricao.Text);
            objSegurancaPapel.CampoTabela.Valor = ServiceDesk.Generica.ClsTexto.trocaAspaPorHtml(txtCampoTabela.Text);

            if (objSegurancaPapel.insere(out strMensagem))
            {
                //Atualizando o valor na tabela identificador
                objIdentificador.atualizaValor();

                //Informando a mensagem de status da operação
                lblMensagem.Text = strMensagem;
                imgIcone.ImageUrl = "images/icones/aviso.gif";
                divMensagem.Visible = true;
                txtDescricao.Text = String.Empty;
                txtCampoTabela.Text = String.Empty;

            }
            else
            {
                lblMensagem.Text = "Não foi possivel realizar a operação.<br />" + strMensagem;
                imgIcone.ImageUrl = "images/icones/aviso.gif";
                divMensagem.Visible = true;
            }

            //Gerando o GridView SegurancaPapel
            SServiceDesk.Negocio.ClsSegurancaPapel.geraGridView(gvSegurancaPapel);

            objSegurancaPapel = null;
            objIdentificador = null;
        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region LimparCampos SegurancaPapel
    /// <summary>
    /// Método que limpa os campos de Inserção de SegurancaPapel 
    /// </summary>
    protected void btnLimpar_Click(object sender, EventArgs e)
    {
        txtDescricao.Text = String.Empty;
        txtCampoTabela.Text = String.Empty;
    }
    #endregion

    #region Evento gvSegurancaPapel_OnRowCommand
    /// <summary>
    /// Evento que ocorre quando algum comando do GridView é executado(Alterar/Excluir/Editar)
    /// </summary>
    protected void gvSegurancaPapel_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Excluir")
            {
                GridViewRow objRow = gvSegurancaPapel.Rows[Convert.ToInt32(e.CommandArgument)];
                if (objRow != null)
                {
                    // Seleciona os códigos que serão excluidos
                    Label lblCodigoSegurancaPapel = (Label)objRow.FindControl("lblCodigo");

                    SServiceDesk.Negocio.ClsSegurancaPapel objSegurancaPapel = new SServiceDesk.Negocio.ClsSegurancaPapel(Convert.ToInt32(lblCodigoSegurancaPapel.Text));
                    try
                    {
                        String strMensagem;

                        if (objSegurancaPapel.exclui(out strMensagem))
                        {
                            lblMensagem.Text = strMensagem;
                            imgIcone.ImageUrl = "images/icones/aviso.gif";
                            divMensagem.Visible = true;

                            txtDescricao.Text = String.Empty;
                            txtCampoTabela.Text = String.Empty;
                        }
                        else
                        {
                            lblMensagem.Text = "Não foi possível realizar a operção.<br />Este item possui relacionamento com outra tabela.";
                            imgIcone.ImageUrl = "images/icones/aviso.gif";
                            divMensagem.Visible = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        lblMensagem.Text = "Não foi possível excluir a Origem do Chamado.<br>" + ex.Message;
                        imgIcone.ImageUrl = "images/icones/erro.gif";
                        divMensagem.Visible = true;
                    }

                    objRow = null;
                    objSegurancaPapel = null;
                }
                SServiceDesk.Negocio.ClsSegurancaPapel.geraGridView(gvSegurancaPapel);
            }

        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region Evento gvSegurancaPapel_RowEditing
    /// <summary>
    /// Método que é disparado quando é selecionada a opção editar
    /// </summary>
    protected void gvSegurancaPapel_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            gvSegurancaPapel.EditIndex = e.NewEditIndex;
            SServiceDesk.Negocio.ClsSegurancaPapel.geraGridView(gvSegurancaPapel);
        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region Evento gvSegurancaPapel_RowDataBound
    /// <summary>
    /// Carrega os dados na GridView
    /// </summary>
    protected void gvSegurancaPapel_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Condicao IF que exibe os dados no GridView (estado: não-editável)
                if ((e.Row.RowState.ToString() == "Normal") || (e.Row.RowState.ToString() == "Alternate"))
                {
                    // Adiciona um evento javascript no botão Excluir
                    ImageButton btnExcluir = (ImageButton)e.Row.Cells[4].Controls[0];
                    btnExcluir.Attributes.Add("onclick", "return verifica();");
                }

                // Condicao IF que exibe os controles e dados destes durante edição de uma linha (estado: editável)
                if ((e.Row.RowState == DataControlRowState.Edit) || (e.Row.RowState.ToString() == "Alternate, Edit"))
                {
                    Label lblDescricaoSegurancaPapel = (Label)e.Row.FindControl("lblDescricaoSegurancaPapel");
                    TextBox txtDescricaoSegurancaPapel = (TextBox)e.Row.FindControl("txtDescricaoSegurancaPapel");
                    if (lblDescricaoSegurancaPapel.Text != String.Empty)
                    {
                        txtDescricaoSegurancaPapel.Text = lblDescricaoSegurancaPapel.Text;
                    }

                    Label lblCampoTabela = (Label)e.Row.FindControl("lblCampoTabela");
                    TextBox txtCampoTabela = (TextBox)e.Row.FindControl("txtCampoTabela");
                    if (lblCampoTabela.Text != String.Empty)
                    {
                        txtCampoTabela.Text = lblCampoTabela.Text;
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

    #region Evento gvSegurancaPapel_RowCancelingEdit
    protected void gvSegurancaPapel_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        try
        {
            gvSegurancaPapel.EditIndex = -1;
            SServiceDesk.Negocio.ClsSegurancaPapel.geraGridView(gvSegurancaPapel);

        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region Evento gvSegurancaPapel_RowUpdating
    /// <summary>
    /// Método que atualiza um item do DataGrid
    /// </summary>
    protected void gvSegurancaPapel_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            String strMensagem = String.Empty;

            GridViewRow objRow = gvSegurancaPapel.Rows[e.RowIndex];
            if (objRow != null)
            {
                try
                {
                    Label lblCodigo = (Label)objRow.FindControl("lblCodigo");
                    TextBox txtDescricaoSegurancaPapel = (TextBox)objRow.FindControl("txtDescricaoSegurancaPapel");
                    TextBox txtCampoTabela = (TextBox)objRow.FindControl("txtCampoTabela");

                    SServiceDesk.Negocio.ClsSegurancaPapel objSegurancaPapel = new SServiceDesk.Negocio.ClsSegurancaPapel();
                    objSegurancaPapel.Codigo.Valor = lblCodigo.Text;
                    objSegurancaPapel.Descricao.Valor = ServiceDesk.Generica.ClsTexto.trocaAspaPorHtml(txtDescricaoSegurancaPapel.Text);
                    objSegurancaPapel.CampoTabela.Valor = ServiceDesk.Generica.ClsTexto.trocaAspaPorHtml(txtCampoTabela.Text);

                    if (objSegurancaPapel.altera(out strMensagem))
                    {
                        lblMensagem.Text = "Item alterado com sucesso.";
                        imgIcone.ImageUrl = "images/icones/info.gif";
                        divMensagem.Visible = true;
                        gvSegurancaPapel.EditIndex = -1;
                        SServiceDesk.Negocio.ClsSegurancaPapel.geraGridView(gvSegurancaPapel);
                    }
                    else
                    {
                        lblMensagem.Text = "Não foi possível realizar a operação.<br>" + strMensagem;
                        imgIcone.ImageUrl = "images/icones/aviso.gif";
                        divMensagem.Visible = true;
                    }
                }
                catch (Exception ex)
                {
                    lblMensagem.Text = "Não foi possível realizar a operação." + ex.ToString();
                    imgIcone.ImageUrl = "images/icones/erro.gif";
                    divMensagem.Visible = true;
                }

                objRow = null;
            }

        }
        catch (Exception ex)
        {

            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion
}
