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

public partial class WUCTipoUsuario : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //Verificar acessibilidade
            /*int intCodigoFuncao = 41;
            if (!ServiceDesk.Negocio.ClsUsuario.verificaAcessoUsuarioFuncao(ClsUsuario.getCodigoUsuario(ClsUsuario.getCodigoRede()), intCodigoFuncao.ToString(), ClsUsuario.getTipoUsuarioCodigo(ClsUsuario.getCodigoRede()).ToString()))
            {
                Response.Redirect("AcessoNegado.aspx", false);
                return;
            }*/

            //Mant�m a posi��o do Scroll ap�s o PostBack
            Page.MaintainScrollPositionOnPostBack = true;

            //Esconde a mensagem de erro
            divMensagem.Visible = false;

            if (!Page.IsPostBack)
            {
                //Gerando o GridView TipoUsuario
                ServiceDesk.Negocio.ClsTipoUsuario.geraGridView(gvTipoUsuario);
            }

        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }

    #region Salva TipoUsuario
    /// <summary>
    /// M�todo que salva um TipoUsuario
    /// </summary>
    protected void btnSalvar_Click(object sender, EventArgs e)
    {
        try
        {
            String strMensagem = String.Empty;

            ServiceDesk.Negocio.ClsTipoUsuario objTipoUsuario = new ServiceDesk.Negocio.ClsTipoUsuario();
            ServiceDesk.Negocio.ClsIdentificador objIdentificador = new ServiceDesk.Negocio.ClsIdentificador();

            objIdentificador.Tabela.Valor = objTipoUsuario.Atributos.NomeTabela;
            objTipoUsuario.Codigo.Valor = objIdentificador.getProximoValor().ToString();
            objTipoUsuario.Sigla.Valor = ServiceDesk.Generica.ClsTexto.trocaAspaPorHtml(txtSigla.Text);
            objTipoUsuario.Descricao.Valor = ServiceDesk.Generica.ClsTexto.trocaAspaPorHtml(txtDescricao.Text);

            if (objTipoUsuario.insere(out strMensagem))
            {
                //Atualizando o valor na tabela identificador
                objIdentificador.atualizaValor();

                //Informando a mensagem do TipoUsuario da opera��o
                lblMensagem.Text = strMensagem;
                imgIcone.ImageUrl = "images/icones/aviso.gif";
                divMensagem.Visible = true;

                //Gerando o GridView TipoUsuario
                ServiceDesk.Negocio.ClsTipoUsuario.geraGridView(gvTipoUsuario);

            }
            else
            {
                lblMensagem.Text = "N�o foi possivel realizar a opera��o.<br />" + strMensagem;
                imgIcone.ImageUrl = "images/icones/aviso.gif";
                divMensagem.Visible = true;
            }

            objTipoUsuario = null;
            objIdentificador = null;

        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region LimparCampos TipoUsuario
    /// <summary>
    /// M�todo que limpa os campos de Inser��o de TipoUsuario 
    /// </summary>
    protected void btnLimpar_Click(object sender, EventArgs e)
    {
        txtSigla.Text = String.Empty;
        txtDescricao.Text = String.Empty;
    }
    #endregion

    #region Evento gvTipoUsuario_OnRowCommand
    /// <summary>
    /// Evento que ocorre quando algum comando do GridView � executado(Alterar/Excluir/Editar)
    /// </summary>
    protected void gvTipoUsuario_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Excluir")
            {
                GridViewRow objRow = gvTipoUsuario.Rows[Convert.ToInt32(e.CommandArgument)];
                if (objRow != null)
                {
                    // Seleciona os c�digos que ser�o excluidos
                    Label lblCodigoTipoUsuario = (Label)objRow.FindControl("lblCodigo");

                    ServiceDesk.Negocio.ClsTipoUsuario objTipoUsuario = new ServiceDesk.Negocio.ClsTipoUsuario(Convert.ToInt32(lblCodigoTipoUsuario.Text));
                    try
                    {
                        if (objTipoUsuario.exclui())
                        {
                            lblMensagem.Text = "Item excluido com sucesso.";
                            imgIcone.ImageUrl = "images/icones/aviso.gif";
                            divMensagem.Visible = true;

                            txtSigla.Text = String.Empty;
                            txtDescricao.Text = String.Empty;
                        }
                        else
                        {
                            lblMensagem.Text = "N�o foi poss�vel realizar a oper��o.<br />Este item possui relacionamento com outra tabela.";
                            imgIcone.ImageUrl = "images/icones/aviso.gif";
                            divMensagem.Visible = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        lblMensagem.Text = "N�o foi poss�vel excluir a Origem do Chamado.<br>" + ex.Message;
                        imgIcone.ImageUrl = "images/icones/erro.gif";
                        divMensagem.Visible = true;
                    }

                    objRow = null;
                    objTipoUsuario = null;
                }
                ServiceDesk.Negocio.ClsTipoUsuario.geraGridView(gvTipoUsuario);
            }

        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region Evento gvTipoUsuario_RowEditing
    /// <summary>
    /// M�todo que � disparado quando � selecionada a op��o editar
    /// </summary>
    protected void gvTipoUsuario_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            gvTipoUsuario.EditIndex = e.NewEditIndex;
            ServiceDesk.Negocio.ClsTipoUsuario.geraGridView(gvTipoUsuario);
        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region Evento gvTipoUsuario_RowDataBound
    /// <summary>
    /// Carrega os dados na GridView
    /// </summary>
    protected void gvTipoUsuario_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Condicao IF que exibe os dados no GridView (estado: n�o-edit�vel)
                if ((e.Row.RowState.ToString() == "Normal") || (e.Row.RowState.ToString() == "Alternate"))
                {
                    // Adiciona um evento javascript no bot�o Excluir
                    ImageButton btnExcluir = (ImageButton)e.Row.Cells[4].Controls[0];
                    btnExcluir.Attributes.Add("onclick", "return verifica();");
                }

                // Condicao IF que exibe os controles e dados destes durante edi��o de uma linha (estado: edit�vel)
                if ((e.Row.RowState == DataControlRowState.Edit) || (e.Row.RowState.ToString() == "Alternate, Edit"))
                {
                    Label lblSiglaTipoUsuario = (Label)e.Row.FindControl("lblSiglaTipoUsuario");
                    TextBox txtSiglaTipoUsuario = (TextBox)e.Row.FindControl("txtSiglaTipoUsuario");
                    if (lblSiglaTipoUsuario.Text != String.Empty)
                    {
                        txtSiglaTipoUsuario.Text = lblSiglaTipoUsuario.Text;
                    }

                    Label lblDescricaoTipoUsuario = (Label)e.Row.FindControl("lblDescricaoTipoUsuario");
                    TextBox txtDescricaoTipoUsuario = (TextBox)e.Row.FindControl("txtDescricaoTipoUsuario");
                    if (lblDescricaoTipoUsuario.Text != String.Empty)
                    {
                        txtDescricaoTipoUsuario.Text = lblDescricaoTipoUsuario.Text;
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

    #region Evento gvTipoUsuario_RowCancelingEdit
    protected void gvTipoUsuario_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        try
        {
            gvTipoUsuario.EditIndex = -1;
            ServiceDesk.Negocio.ClsTipoUsuario.geraGridView(gvTipoUsuario);

        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region Evento gvTipoUsuario_RowUpdating
    /// <summary>
    /// M�todo que atualiza um item do DataGrid
    /// </summary>
    protected void gvTipoUsuario_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            String strMensagem = String.Empty;

            GridViewRow objRow = gvTipoUsuario.Rows[e.RowIndex];
            if (objRow != null)
            {
                try
                {
                    Label lblCodigo = (Label)objRow.FindControl("lblCodigo");
                    TextBox txtSiglaTipoUsuario = (TextBox)objRow.FindControl("txtSiglaTipoUsuario");
                    TextBox txtDescricaoTipoUsuario = (TextBox)objRow.FindControl("txtDescricaoTipoUsuario");

                    ServiceDesk.Negocio.ClsTipoUsuario objTipoUsuario = new ServiceDesk.Negocio.ClsTipoUsuario();
                    objTipoUsuario.Codigo.Valor = lblCodigo.Text;
                    objTipoUsuario.Sigla.Valor = ServiceDesk.Generica.ClsTexto.trocaAspaPorHtml(txtSiglaTipoUsuario.Text);
                    objTipoUsuario.Descricao.Valor = ServiceDesk.Generica.ClsTexto.trocaAspaPorHtml(txtDescricaoTipoUsuario.Text);

                    if (objTipoUsuario.altera(out strMensagem))
                    {
                        lblMensagem.Text = "Item alterado com sucesso.";
                        imgIcone.ImageUrl = "images/icones/info.gif";
                        divMensagem.Visible = true;
                        gvTipoUsuario.EditIndex = -1;
                        ServiceDesk.Negocio.ClsTipoUsuario.geraGridView(gvTipoUsuario);
                    }
                    else
                    {
                        lblMensagem.Text = "N�o foi poss�vel realizar a opera��o.<br>" + strMensagem;
                        imgIcone.ImageUrl = "images/icones/aviso.gif";
                        divMensagem.Visible = true;
                    }
                }
                catch (Exception ex)
                {
                    lblMensagem.Text = "N�o foi poss�vel realizar a opera��o." + ex.ToString();
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