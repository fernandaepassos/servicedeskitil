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

public partial class WUCSegurancaDireito : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //Verificar acessibilidade
        /*int intCodigoFuncao = 44;
        if (!ServiceDesk.Negocio.ClsUsuario.verificaAcessoUsuarioFuncao(ClsUsuario.getCodigoUsuario(ClsUsuario.getCodigoRede()), intCodigoFuncao.ToString(), ClsUsuario.getTipoUsuarioCodigo(ClsUsuario.getCodigoRede()).ToString()))
        {
            Response.Redirect("AcessoNegado.aspx", false);
            return;
        }*/

        //Desabilitando o botao salvar. Nao deve permitir inserir direitos.
        btnSalvar.Visible = false;
        btnLimpar.Visible = false;
        txtDescricao.Visible = false;
        lblTitulo.Visible = false;

        try
        {
            //Mantém a posição do Scroll após o PostBack
            Page.MaintainScrollPositionOnPostBack = true;

            //Esconde a mensagem de erro
            divMensagem.Visible = false;

            if (!Page.IsPostBack)
            {
                //Gerando o GridView SegurancaDireito
                SServiceDesk.Negocio.ClsSegurancaDireito.geraGridView(gvSegurancaDireito);
            }

        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }

    #region Salva SegurancaDireito
    /// <summary>
    /// Método que salva um SegurancaDireito
    /// </summary>
    protected void btnSalvar_Click(object sender, EventArgs e)
    {
        try
        {
            String strMensagem = String.Empty;

            SServiceDesk.Negocio.ClsSegurancaDireito objSegurancaDireito = new SServiceDesk.Negocio.ClsSegurancaDireito();
            ServiceDesk.Negocio.ClsIdentificador objIdentificador = new ServiceDesk.Negocio.ClsIdentificador();

            objIdentificador.Tabela.Valor = objSegurancaDireito.Atributos.NomeTabela;
            objSegurancaDireito.Codigo.Valor = objIdentificador.getProximoValor().ToString();
            objSegurancaDireito.Descricao.Valor = ServiceDesk.Generica.ClsTexto.trocaAspaPorHtml(txtDescricao.Text);

            if (objSegurancaDireito.insere(out strMensagem))
            {
                //Atualizando o valor na tabela identificador
                objIdentificador.atualizaValor();

                //Informando a mensagem de status da operação
                lblMensagem.Text = strMensagem;
                imgIcone.ImageUrl = "images/icones/aviso.gif";
                divMensagem.Visible = true;

                //Gerando o GridView SegurancaDireito
                SServiceDesk.Negocio.ClsSegurancaDireito.geraGridView(gvSegurancaDireito);

            }
            else
            {
                lblMensagem.Text = "Não foi possivel realizar a operação.<br />" + strMensagem;
                imgIcone.ImageUrl = "images/icones/aviso.gif";
                divMensagem.Visible = true;
            }

            objSegurancaDireito = null;
            objIdentificador = null;

        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region LimparCampos SegurancaDireito
    /// <summary>
    /// Método que limpa os campos de Inserção de SegurancaDireito 
    /// </summary>
    protected void btnLimpar_Click(object sender, EventArgs e)
    {
        txtDescricao.Text = String.Empty;
    }
    #endregion

    #region Evento gvSegurancaDireito_OnRowCommand
    /// <summary>
    /// Evento que ocorre quando algum comando do GridView é executado(Alterar/Excluir/Editar)
    /// </summary>
    protected void gvSegurancaDireito_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Excluir")
            {
                GridViewRow objRow = gvSegurancaDireito.Rows[Convert.ToInt32(e.CommandArgument)];
                if (objRow != null)
                {
                    // Seleciona os códigos que serão excluidos
                    Label lblCodigoSegurancaDireito = (Label)objRow.FindControl("lblCodigo");

                    SServiceDesk.Negocio.ClsSegurancaDireito objSegurancaDireito = new SServiceDesk.Negocio.ClsSegurancaDireito(Convert.ToInt32(lblCodigoSegurancaDireito.Text));
                    try
                    {
                        String strMensagem;

                        if (objSegurancaDireito.exclui(out strMensagem))
                        {
                            lblMensagem.Text = strMensagem;
                            imgIcone.ImageUrl = "images/icones/aviso.gif";
                            divMensagem.Visible = true;

                            txtDescricao.Text = String.Empty;
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
                    objSegurancaDireito = null;
                }
                SServiceDesk.Negocio.ClsSegurancaDireito.geraGridView(gvSegurancaDireito);
            }

        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region Evento gvSegurancaDireito_RowEditing
    /// <summary>
    /// Método que é disparado quando é selecionada a opção editar
    /// </summary>
    protected void gvSegurancaDireito_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            gvSegurancaDireito.EditIndex = e.NewEditIndex;
            SServiceDesk.Negocio.ClsSegurancaDireito.geraGridView(gvSegurancaDireito);
        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region Evento gvSegurancaDireito_RowDataBound
    /// <summary>
    /// Carrega os dados na GridView
    /// </summary>
    protected void gvSegurancaDireito_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Condicao IF que exibe os dados no GridView (estado: não-editável)
                if ((e.Row.RowState.ToString() == "Normal") || (e.Row.RowState.ToString() == "Alternate"))
                {
                    // Adiciona um evento javascript no botão Excluir
                    ImageButton btnExcluir = (ImageButton)e.Row.Cells[3].Controls[0];
                    btnExcluir.Attributes.Add("onclick", "return verifica();");
                }

                // Condicao IF que exibe os controles e dados destes durante edição de uma linha (estado: editável)
                if ((e.Row.RowState == DataControlRowState.Edit) || (e.Row.RowState.ToString() == "Alternate, Edit"))
                {
                    Label lblDescricaoSegurancaDireito = (Label)e.Row.FindControl("lblDescricaoSegurancaDireito");
                    TextBox txtDescricaoSegurancaDireito = (TextBox)e.Row.FindControl("txtDescricaoSegurancaDireito");
                    if (lblDescricaoSegurancaDireito.Text != String.Empty)
                    {
                        txtDescricaoSegurancaDireito.Text = lblDescricaoSegurancaDireito.Text;
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

    #region Evento gvSegurancaDireito_RowCancelingEdit
    protected void gvSegurancaDireito_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        try
        {
            gvSegurancaDireito.EditIndex = -1;
            SServiceDesk.Negocio.ClsSegurancaDireito.geraGridView(gvSegurancaDireito);

        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region Evento gvSegurancaDireito_RowUpdating
    /// <summary>
    /// Método que atualiza um item do DataGrid
    /// </summary>
    protected void gvSegurancaDireito_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            String strMensagem = String.Empty;

            GridViewRow objRow = gvSegurancaDireito.Rows[e.RowIndex];
            if (objRow != null)
            {
                try
                {
                    Label lblCodigo = (Label)objRow.FindControl("lblCodigo");
                    TextBox txtDescricaoSegurancaDireito = (TextBox)objRow.FindControl("txtDescricaoSegurancaDireito");

                    SServiceDesk.Negocio.ClsSegurancaDireito objSegurancaDireito = new SServiceDesk.Negocio.ClsSegurancaDireito();
                    objSegurancaDireito.Codigo.Valor = lblCodigo.Text;
                    objSegurancaDireito.Descricao.Valor = ServiceDesk.Generica.ClsTexto.trocaAspaPorHtml(txtDescricaoSegurancaDireito.Text);

                    if (objSegurancaDireito.altera(out strMensagem))
                    {
                        lblMensagem.Text = "Item alterado com sucesso.";
                        imgIcone.ImageUrl = "images/icones/info.gif";
                        divMensagem.Visible = true;
                        gvSegurancaDireito.EditIndex = -1;
                        SServiceDesk.Negocio.ClsSegurancaDireito.geraGridView(gvSegurancaDireito);
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