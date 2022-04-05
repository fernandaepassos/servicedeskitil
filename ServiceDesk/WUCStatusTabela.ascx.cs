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

public partial class WUCStatusTabela : System.Web.UI.UserControl
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
                divStatus.Visible = false;

                //Gerando o GridView Equipe
                ServiceDesk.Negocio.ClsStatusTabela.geraGridViewTabelas(gvTabela);
            }
        }
        catch (Exception ex)
        {
            lblMensagem.Text = "Não foi possivel realizar a operação.";
            imgIcone.ImageUrl = "images/icones/aviso.gif";
            divMensagem.Visible = true;

            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO,ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }

    #region Evento gvTabela_OnRowCommand
    /// <summary>
    /// Evento que ocorre quando algum comando do GridView é executado(Alterar/Excluir/Editar)
    /// </summary>
    protected void gvTabela_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Selecionar")
            {
                GridViewRow objRow = gvTabela.Rows[Convert.ToInt32(e.CommandArgument)];
                if (objRow != null)
                {
                    gvTabela.SelectedIndex = Convert.ToInt32(e.CommandArgument);
                    Label lblNomeTabela = (Label)objRow.FindControl("lblStatusTabelaCodigo");
                    ViewState["tabela_selecionada"] = lblNomeTabela.Text;

                    ServiceDesk.Negocio.ClsStatus objStatus = new ServiceDesk.Negocio.ClsStatus();
                    objStatus.GeraGridViewporTabela(gvStatusTabela);
                    objStatus = null;

                    divStatus.Visible = true;
                }
                objRow = null;
            }

        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO,ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    /*
    - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
    • Métodos relacionados a GridView gvStatusTabela
    - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
    */

    protected void gvStatusTabela_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if ((e.Row.RowState.ToString() == "Normal") || (e.Row.RowState.ToString() == "Alternate"))
                {
                    Label lblStatusCodigo = (Label)e.Row.FindControl("lblStatusCodigo");
                    CheckBox ckStatus = (CheckBox)e.Row.FindControl("ckStatus");

                    if (ServiceDesk.Negocio.ClsStatusTabela.GetStatusporTabela(lblStatusCodigo.Text, ViewState["tabela_selecionada"].ToString()) == "S")
                        ckStatus.Checked = true;

                }
            }

        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO,ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }

    protected void btnSalvarStatus_Click(object sender, EventArgs e)
    {
        try
        {
            int intContador = 0;
            int intContInclusos = 0;
            String strMensagem = String.Empty;
            
            ServiceDesk.Negocio.ClsStatusTabela objStatusTabela = new ServiceDesk.Negocio.ClsStatusTabela();

            if (objStatusTabela.exclui(ViewState["tabela_selecionada"].ToString()))
            {
                for (intContador = 0; intContador < gvStatusTabela.Rows.Count; intContador++)
                {
                    GridViewRow objRow = (GridViewRow)gvStatusTabela.Rows[intContador];
                    if (objRow != null)
                    {
                        CheckBox ckStatus = (CheckBox)objRow.FindControl("ckStatus");
                        Label lblStatusCodigo = (Label)objRow.FindControl("lblStatusCodigo");

                        if (ckStatus.Checked == true)
                        {
                            ServiceDesk.Negocio.ClsIdentificador objIdentificador = new ServiceDesk.Negocio.ClsIdentificador();

                            objIdentificador.Tabela.Valor = objStatusTabela.Atributos.NomeTabela;
                            objStatusTabela.Codigo.Valor = objIdentificador.getProximoValor().ToString();
                            objStatusTabela.StatusCodigo.Valor = ServiceDesk.Generica.ClsTexto.trocaAspaPorHtml(lblStatusCodigo.Text);
                            objStatusTabela.Tabela.Valor = ServiceDesk.Generica.ClsTexto.trocaAspaPorHtml(ViewState["tabela_selecionada"].ToString());

                            if (objStatusTabela.insere(out strMensagem))
                            {
                                intContInclusos += 1;
                                objIdentificador.atualizaValor();

                                if (intContInclusos == 1)
                                    strMensagem = "1 item inserido com sucesso.";
                                else if (intContInclusos > 1)
                                    strMensagem = intContInclusos.ToString() + " itens inseridos com sucesso.";

                                lblMensagem.Text = strMensagem.Trim();
                                imgIcone.ImageUrl = "images/icones/info.gif";
                                divMensagem.Visible = true;
                            }
                            else
                            {
                                lblMensagem.Text = "Não foi possivel realizar a operação.<br />";
                                imgIcone.ImageUrl = "images/icones/aviso.gif";
                                divMensagem.Visible = true;
                            }
                            objIdentificador = null;
                        }
                    }
                    objRow.Dispose();
                }
                
                if (strMensagem == String.Empty)
                {
                    lblMensagem.Text = "É necessário selecionar o status desejado para associar a tabela.";
                    imgIcone.ImageUrl = "images/icones/aviso.gif";
                    divMensagem.Visible = true;
                }
                ServiceDesk.Negocio.ClsStatus objStatus = new ServiceDesk.Negocio.ClsStatus();
                objStatus.GeraGridViewporTabela(gvStatusTabela);
                objStatus = null;
            }
            objStatusTabela = null;
        }
        catch (Exception ex)
        {
                ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO,ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
}
