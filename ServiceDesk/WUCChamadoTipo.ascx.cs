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

public partial class WUCChamadoTipo : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //Mantém a posição do Scroll após o PostBack
            Page.MaintainScrollPositionOnPostBack = true;

            //Esconde a mensagem de erro
            divMensagem.Visible = false;

            if (!Page.IsPostBack)
            {
                ddlGeraProcesso.Items.Insert(0, "Sim");
                ddlGeraProcesso.Items[0].Value = "S";
                ddlGeraProcesso.Items.Insert(1, "Não");
                ddlGeraProcesso.Items[1].Value = "N";

                //Gerando o GridView TipoChamado
                ClsTipoChamado.geraGridView(gvTipoChamado);
            }
        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }

    #region Salva TipoChamado
    /// <summary>
    /// Método que salva um TipoChamado
    /// </summary>
    protected void btnSalvar_Click(object sender, EventArgs e)
    {
        try
        {
            String strMensagem = String.Empty;

            ServiceDesk.Negocio.ClsTipoChamado objTipoChamado = new ServiceDesk.Negocio.ClsTipoChamado();
            ServiceDesk.Negocio.ClsIdentificador objIdentificador = new ServiceDesk.Negocio.ClsIdentificador();

            objIdentificador.Tabela.Valor = objTipoChamado.Atributos.NomeTabela;
            objTipoChamado.Codigo.Valor = objIdentificador.getProximoValor().ToString();
            objTipoChamado.Descricao.Valor = ServiceDesk.Generica.ClsTexto.trocaAspaPorHtml(txtDescricao.Text);
            objTipoChamado.FlagGeraProcesso.Valor = ServiceDesk.Generica.ClsTexto.trocaAspaPorHtml(ddlGeraProcesso.SelectedValue);
            objTipoChamado.Prefixo.Valor = ServiceDesk.Generica.ClsTexto.trocaAspaPorHtml(txtPrefixo.Text);

            if (objTipoChamado.insere(out strMensagem))
            {
                //Atualizando o valor na tabela identificador
                objIdentificador.atualizaValor();

                //Informando a mensagem do TipoChamado da operação
                lblMensagem.Text = strMensagem;
                imgIcone.ImageUrl = "images/icones/aviso.gif";
                divMensagem.Visible = true;

                //Gerando o GridView TipoChamado
                ServiceDesk.Negocio.ClsTipoChamado.geraGridView(gvTipoChamado);

            }
            else
            {
                lblMensagem.Text = "Não foi possivel realizar a operação.<br />" + strMensagem;
                imgIcone.ImageUrl = "images/icones/aviso.gif";
                divMensagem.Visible = true;
            }

            objTipoChamado = null;
            objIdentificador = null;

        }
        catch (Exception ex)
        {

            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region LimparCampos TipoChamado
    /// <summary>
    /// Método que limpa os campos de Inserção de TipoChamado 
    /// </summary>
    protected void btnNovo_Click(object sender, EventArgs e)
    {
        txtDescricao.Text = String.Empty;
        ddlGeraProcesso.ClearSelection();
        txtPrefixo.Text = String.Empty;
    }
    #endregion

    #region Evento gvTipoChamado_OnRowCommand
    /// <summary>
    /// Evento que ocorre quando algum comando do GridView é executado(Alterar/Excluir/Editar)
    /// </summary>
    protected void gvTipoChamado_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Excluir")
            {
                GridViewRow objRow = gvTipoChamado.Rows[Convert.ToInt32(e.CommandArgument)];
                if (objRow != null)
                {
                    // Seleciona os códigos que serão excluidos
                    Label lblCodigoTipoChamado = (Label)objRow.FindControl("lblCodigo");

                    ServiceDesk.Negocio.ClsTipoChamado objTipoChamado = new ServiceDesk.Negocio.ClsTipoChamado(Convert.ToInt32(lblCodigoTipoChamado.Text));
                    try
                    {
                        if (objTipoChamado.exclui())
                        {
                            lblMensagem.Text = "Item excluido com sucesso.";
                            imgIcone.ImageUrl = "images/icones/aviso.gif";
                            divMensagem.Visible = true;

                            txtPrefixo.Text = String.Empty;
                            txtDescricao.Text = String.Empty;
                            ddlGeraProcesso.ClearSelection();
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
                    objTipoChamado = null;
                }
                ServiceDesk.Negocio.ClsTipoChamado.geraGridView(gvTipoChamado);
            }

        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region Evento gvTipoChamado_RowEditing
    /// <summary>
    /// Método que é disparado quando é selecionada a opção editar
    /// </summary>
    protected void gvTipoChamado_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            gvTipoChamado.EditIndex = e.NewEditIndex;
            ServiceDesk.Negocio.ClsTipoChamado.geraGridView(gvTipoChamado);
        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region Evento gvTipoChamado_RowDataBound
    /// <summary>
    /// Carrega os dados na GridView
    /// </summary>
    protected void gvTipoChamado_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Condicao IF que exibe os dados no GridView (estado: não-editável)
                if ((e.Row.RowState.ToString() == "Normal") || (e.Row.RowState.ToString() == "Alternate"))
                {
                    ImageButton btnExcluir = (ImageButton)e.Row.Cells[5].Controls[0];
                    btnExcluir.Attributes.Add("onclick", "return verifica();");

                    Label lblGeraProcesso = (Label)e.Row.FindControl("lblGeraProcesso");
                    if (lblGeraProcesso.Text.Trim() == "S")
                        lblGeraProcesso.Text = "Sim";
                    else
                        lblGeraProcesso.Text = "Não";
                }

                // Condicao IF que exibe os controles e dados destes durante edição de uma linha (estado: editável)
                if ((e.Row.RowState == DataControlRowState.Edit) || (e.Row.RowState.ToString() == "Alternate, Edit"))
                {
                    Label lblGeraProcesso = (Label)e.Row.FindControl("lblGeraProcesso");
                    DropDownList ddlGeraProcesso = (DropDownList)e.Row.FindControl("ddlGeraProcesso");

                    ddlGeraProcesso.Items.Insert(0, "Sim");
                    ddlGeraProcesso.Items[0].Value = "S";
                    ddlGeraProcesso.Items.Insert(1, "Não");
                    ddlGeraProcesso.Items[1].Value = "N";

                    if (lblGeraProcesso.Text != String.Empty)
                        ddlGeraProcesso.SelectedValue = lblGeraProcesso.Text.Trim();
                    else
                        ddlGeraProcesso.SelectedValue = "N";
                }
            }

        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region Evento gvTipoChamado_RowCancelingEdit
    protected void gvTipoChamado_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        try
        {
            gvTipoChamado.EditIndex = -1;
            ServiceDesk.Negocio.ClsTipoChamado.geraGridView(gvTipoChamado);

        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region Evento gvTipoChamado_RowUpdating
    /// <summary>
    /// Método que atualiza um item do DataGrid
    /// </summary>
    protected void gvTipoChamado_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            String strMensagem = String.Empty;

            GridViewRow objRow = gvTipoChamado.Rows[e.RowIndex];
            if (objRow != null)
            {
                try
                {
                    ServiceDesk.Negocio.ClsTipoChamado objTipoChamado = new ServiceDesk.Negocio.ClsTipoChamado();

                    Label lblCodigo = (Label)objRow.FindControl("lblCodigo");
                    TextBox txtDescricao = (TextBox)objRow.FindControl("txtDescricao");
                    TextBox txtPrefixo = (TextBox)objRow.FindControl("txtPrefixo");
                    DropDownList ddlGeraProcesso = (DropDownList)objRow.FindControl("ddlGeraProcesso");

                    objTipoChamado.Codigo.Valor = lblCodigo.Text;
                    objTipoChamado.Descricao.Valor = ServiceDesk.Generica.ClsTexto.trocaAspaPorHtml(txtDescricao.Text);
                    objTipoChamado.FlagGeraProcesso.Valor = ServiceDesk.Generica.ClsTexto.trocaAspaPorHtml(ddlGeraProcesso.SelectedValue);
                    objTipoChamado.Prefixo.Valor = ServiceDesk.Generica.ClsTexto.trocaAspaPorHtml(txtPrefixo.Text);

                    if (objTipoChamado.altera(out strMensagem))
                    {
                        lblMensagem.Text = "Item alterado com sucesso.";
                        imgIcone.ImageUrl = "images/icones/info.gif";
                        divMensagem.Visible = true;
                        gvTipoChamado.EditIndex = -1;
                        ServiceDesk.Negocio.ClsTipoChamado.geraGridView(gvTipoChamado);
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