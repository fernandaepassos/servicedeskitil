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

public partial class WUCStatus : System.Web.UI.UserControl
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
                //Gerando o GridView Status
                ServiceDesk.Negocio.ClsStatus.geraGridView(gvStatus);

                ddlContaTempo.Items.Insert(0, "Sim");
                ddlContaTempo.Items[0].Value = "1";
                ddlContaTempo.Items.Insert(1, "Não");
                ddlContaTempo.Items[1].Value = "NULL";
            }

        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }

    #region Salva Status
    /// <summary>
    /// Método que salva um Status
    /// </summary>
    protected void btnSalvar_Click(object sender, EventArgs e)
    {
        try
        {
            String strMensagem = String.Empty;

            ServiceDesk.Negocio.ClsStatus objStatus = new ServiceDesk.Negocio.ClsStatus();
            ServiceDesk.Negocio.ClsIdentificador objIdentificador = new ServiceDesk.Negocio.ClsIdentificador();

            objIdentificador.Tabela.Valor = objStatus.Atributos.NomeTabela;
            objStatus.Codigo.Valor = objIdentificador.getProximoValor().ToString();
            objStatus.Sigla.Valor = ServiceDesk.Generica.ClsTexto.trocaAspaPorHtml(txtSigla.Text);
            objStatus.Descricao.Valor = ServiceDesk.Generica.ClsTexto.trocaAspaPorHtml(txtDescricao.Text);
            objStatus.ContaTempoSla.Valor = ServiceDesk.Generica.ClsTexto.trocaAspaPorHtml(ddlContaTempo.SelectedValue);

            if (objStatus.insere(out strMensagem))
            {
                //Atualizando o valor na tabela identificador
                objIdentificador.atualizaValor();

                //Informando a mensagem do status da operação
                lblMensagem.Text = strMensagem;
                imgIcone.ImageUrl = "images/icones/aviso.gif";
                divMensagem.Visible = true;

                //Gerando o GridView Status
                ServiceDesk.Negocio.ClsStatus.geraGridView(gvStatus);

            }
            else
            {
                lblMensagem.Text = "Não foi possivel realizar a operação.<br />" + strMensagem;
                imgIcone.ImageUrl = "images/icones/aviso.gif";
                divMensagem.Visible = true;
            }

            objStatus = null;
            objIdentificador = null;

        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region LimparCampos Status
    /// <summary>
    /// Método que limpa os campos de Inserção de Status 
    /// </summary>
    protected void btnLimpar_Click(object sender, EventArgs e)
    {
        txtSigla.Text = String.Empty;
        txtDescricao.Text = String.Empty;
    }
    #endregion

    #region Evento gvStatus_OnRowCommand
    /// <summary>
    /// Evento que ocorre quando algum comando do GridView é executado(Alterar/Excluir/Editar)
    /// </summary>
    protected void gvStatus_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Excluir")
            {
                GridViewRow objRow = gvStatus.Rows[Convert.ToInt32(e.CommandArgument)];
                if (objRow != null)
                {
                    // Seleciona os códigos que serão excluidos
                    Label lblCodigoStatus = (Label)objRow.FindControl("lblCodigo");

                    ServiceDesk.Negocio.ClsStatus objStatus = new ServiceDesk.Negocio.ClsStatus(Convert.ToInt32(lblCodigoStatus.Text));
                    try
                    {
                        if (objStatus.exclui())
                        {
                            lblMensagem.Text = "Item excluido com sucesso.";
                            imgIcone.ImageUrl = "images/icones/aviso.gif";
                            divMensagem.Visible = true;

                            txtSigla.Text = String.Empty;
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
                    objStatus = null;
                }
                ServiceDesk.Negocio.ClsStatus.geraGridView(gvStatus);
            }

        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region Evento gvStatus_RowEditing
    /// <summary>
    /// Método que é disparado quando é selecionada a opção editar
    /// </summary>
    protected void gvStatus_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            gvStatus.EditIndex = e.NewEditIndex;
            ServiceDesk.Negocio.ClsStatus.geraGridView(gvStatus);
        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region Evento gvStatus_RowDataBound
    /// <summary>
    /// Carrega os dados na GridView
    /// </summary>
    protected void gvStatus_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Condicao IF que exibe os dados no GridView (estado: não-editável)
                if ((e.Row.RowState.ToString() == "Normal") || (e.Row.RowState.ToString() == "Alternate"))
                {
                    // Adiciona um evento javascript no botão Excluir
                    ImageButton btnExcluir = (ImageButton)e.Row.Cells[5].Controls[0];
                    btnExcluir.Attributes.Add("onclick", "return verifica();");

                    Label lblContaTempoSla = (Label)e.Row.FindControl("lblContaTempoSla");
                    if(lblContaTempoSla.Text.Trim() == "1")
                        lblContaTempoSla.Text = "Sim";
                    else
                        lblContaTempoSla.Text = "Não";
                }

                // Condicao IF que exibe os controles e dados destes durante edição de uma linha (estado: editável)
                if ((e.Row.RowState == DataControlRowState.Edit) || (e.Row.RowState.ToString() == "Alternate, Edit"))
                {
                    Label lblSiglaStatus = (Label)e.Row.FindControl("lblSiglaStatus");
                    TextBox txtSiglaStatus = (TextBox)e.Row.FindControl("txtSiglaStatus");
                    if (lblSiglaStatus.Text != String.Empty)
                    {
                        txtSiglaStatus.Text = lblSiglaStatus.Text;
                    }

                    Label lblDescricaoStatus = (Label)e.Row.FindControl("lblDescricaoStatus");
                    TextBox txtDescricaoStatus = (TextBox)e.Row.FindControl("txtDescricaoStatus");
                    if (lblDescricaoStatus.Text != String.Empty)
                    {
                        txtDescricaoStatus.Text = lblDescricaoStatus.Text;
                    }

                    Label lblContaTempoSla = (Label)e.Row.FindControl("lblContaTempoSla");
                    DropDownList ddlContaTempoSla = (DropDownList)e.Row.FindControl("ddlContaTempoSla");
                    ddlContaTempoSla.Items.Insert(0, "Sim");
                    ddlContaTempoSla.Items[0].Value = "1";
                    ddlContaTempoSla.Items.Insert(1, "Não");
                    ddlContaTempoSla.Items[1].Value = "NULL";

                    if (lblContaTempoSla.Text.Trim() == "1")
                        ddlContaTempoSla.SelectedIndex = 0;
                    else
                        ddlContaTempoSla.SelectedIndex = 1;
                }
            }

        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region Evento gvStatus_RowCancelingEdit
    protected void gvStatus_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        try
        {
            gvStatus.EditIndex = -1;
            ServiceDesk.Negocio.ClsStatus.geraGridView(gvStatus);

        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region Evento gvStatus_RowUpdating
    /// <summary>
    /// Método que atualiza um item do DataGrid
    /// </summary>
    protected void gvStatus_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            String strMensagem = String.Empty;

            GridViewRow objRow = gvStatus.Rows[e.RowIndex];
            if (objRow != null)
            {
                try
                {
                    Label lblCodigo = (Label)objRow.FindControl("lblCodigo");
                    TextBox txtSiglaStatus = (TextBox)objRow.FindControl("txtSiglaStatus");
                    TextBox txtDescricaoStatus = (TextBox)objRow.FindControl("txtDescricaoStatus");
                    DropDownList ddlContaTempoSla = (DropDownList)objRow.FindControl("ddlContaTempoSla");

                    ServiceDesk.Negocio.ClsStatus objStatus = new ServiceDesk.Negocio.ClsStatus();
                    objStatus.Codigo.Valor = lblCodigo.Text;
                    objStatus.Sigla.Valor = ServiceDesk.Generica.ClsTexto.trocaAspaPorHtml(txtSiglaStatus.Text);
                    objStatus.Descricao.Valor = ServiceDesk.Generica.ClsTexto.trocaAspaPorHtml(txtDescricaoStatus.Text);
                    objStatus.ContaTempoSla.Valor = ServiceDesk.Generica.ClsTexto.trocaAspaPorHtml(ddlContaTempoSla.SelectedValue);

                    if (objStatus.altera(out strMensagem))
                    {
                        lblMensagem.Text = strMensagem;
                        imgIcone.ImageUrl = "images/icones/info.gif";
                        divMensagem.Visible = true;
                        gvStatus.EditIndex = -1;
                        ServiceDesk.Negocio.ClsStatus.geraGridView(gvStatus);
                    }
                    else
                    {
                        lblMensagem.Text = "Não foi possível realizar a operação.<br>" + strMensagem;
                        imgIcone.ImageUrl = "images/icones/aviso.gif";
                        divMensagem.Visible = true;
                    }
                }
                catch
                {
                    lblMensagem.Text = "Não foi possível realizar a operação.";
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
