/*
- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
  • Formulário de cadastramento dos tipos de soluções do projeto.
  
  	Data: 20/11/2005
  	Autor: Fernanda Passos
  	Descrição: Este WebForm permite incluir, excluir e alterar os tipos de soluções do projeto que podem
    haver.   
  
  • Alterações
  	Data:
  	Autor:
  	Descrição:
- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
*/

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

public partial class SolucaoProjetoTipo : BasePage
{

    #region Declarações
    private ServiceDesk.Negocio.ClsSolucaoProjetoTipo objSolucaoProjetoTipo = new ServiceDesk.Negocio.ClsSolucaoProjetoTipo();
    #endregion

    #region Page_Load
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            CheckAcesso(24);

            if (!Page.IsPostBack)
            {
                ClsSolucaoProjetoTipo.geraGridView(gvSolucaoProjetoTipo);
            }
        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ClsLog.enumTipoLog.ERRO, string.Empty, Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region Eventos
    /// <summary>
    /// Evento PageIndexChanging do Grid Solucao Projeto Tipo
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvSolucaoProjetoTipo_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvSolucaoProjetoTipo.PageIndex = e.NewPageIndex;
        ServiceDesk.Negocio.ClsSolucaoProjetoTipo.geraGridView(gvSolucaoProjetoTipo);
    }

    /// <summary>
    /// Evento RowCancelingEdit do Grid Solucao Projeto Tipo
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvSolucaoProjetoTipo_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvSolucaoProjetoTipo.EditIndex = -1;
        ServiceDesk.Negocio.ClsSolucaoProjetoTipo.geraGridView(gvSolucaoProjetoTipo);
    }

    /// <summary>
    /// Evento RowCommand do Grid Solução Projeto Tipo
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvSolucaoProjetoTipo_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            GridViewRow objRow = gvSolucaoProjetoTipo.Rows[Convert.ToInt32(e.CommandArgument)];

            if (e.CommandName == "Excluir")
            {
                if (objRow != null)
                {
                    Label lblCodigo = (Label)objRow.FindControl("lblCodigo");
                    objSolucaoProjetoTipo.Codigo.Valor = lblCodigo.Text.Trim(); ;

                    string strMensagem = string.Empty;

                    if (objSolucaoProjetoTipo.exclui(out strMensagem) == false)
                    {
                        lblMensagem.Text = strMensagem;
                        imgIcone.ImageUrl = "images/icones/aviso.gif";
                        divMensagem.Visible = true;
                    }
                    else
                        divMensagem.Visible = false;

                    objRow = null;
                    objSolucaoProjetoTipo = null;
                }
                ServiceDesk.Negocio.ClsSolucaoProjetoTipo.geraGridView(gvSolucaoProjetoTipo);
            }
            else if (e.CommandName == "Novo")
            {
                gvSolucaoProjetoTipo.EditIndex = 0;
            }
        }
        catch (Exception ex)
        {
            lblMensagem.Text = ex.Message;
            imgIcone.ImageUrl = "images/icones/aviso.gif";
            divMensagem.Visible = true;

            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, user.IDusuario.ToString(), this.Request.Path, "0", ex.ToString());
        }
    }

    /// <summary>
    /// Evento RowEditing do Grid SolucaoProjetoTipo
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvSolucaoProjetoTipo_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gvSolucaoProjetoTipo.EditIndex = e.NewEditIndex;//ok
        ServiceDesk.Negocio.ClsSolucaoProjetoTipo.geraGridView(gvSolucaoProjetoTipo);
    }

    /// <summary>
    /// Evento RowUpdating do Grid SolucaoProjetoTipo
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvSolucaoProjetoTipo_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            String strMensagem = String.Empty;

            GridViewRow objRow = gvSolucaoProjetoTipo.Rows[e.RowIndex];
            if (objRow != null)
            {
                TextBox txtCodigo = (TextBox)objRow.FindControl("txtCodigo");
                TextBox txtDescricao = (TextBox)objRow.FindControl("txtDescricao");

                objSolucaoProjetoTipo.Codigo.Valor = txtCodigo.Text.Trim();
                objSolucaoProjetoTipo.Nome.Valor = txtDescricao.Text.Trim();

                if (objSolucaoProjetoTipo.altera(out strMensagem) == false)
                {
                    lblMensagem.Text = strMensagem;
                    imgIcone.ImageUrl = "images/icones/aviso.gif";
                    divMensagem.Visible = true;
                }
                else
                    divMensagem.Visible = false;

                objRow = null;
                objSolucaoProjetoTipo = null;
            }
        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, user.IDusuario.ToString(), this.Request.Path, "0", ex.ToString());
        }
    }

    /// <summary>
    /// Evento que salva um novo tipo de solução.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSalvar_Click(object sender, EventArgs e)
    {
        try
        {
            string strMensagem = string.Empty;

            objSolucaoProjetoTipo.Codigo.Valor = objSolucaoProjetoTipo.GetMaxId().ToString();

            //Verifica se caracteres da descrição execede o limite.
            if (txtDescricaoTipo.Text.Length > 100)
            {
                objSolucaoProjetoTipo.Nome.Valor = txtDescricaoTipo.Text.Trim();
                objSolucaoProjetoTipo.Nome.Valor = ServiceDesk.Generica.ClsTexto.trocaAspaPorHtml(txtDescricaoTipo.Text.Substring(0, 100));
                txtDescricaoTipo.Text = ServiceDesk.Generica.ClsTexto.trocaAspaPorHtml(txtDescricaoTipo.Text.Substring(0, 100));
            }
            else objSolucaoProjetoTipo.Nome.Valor = ServiceDesk.Generica.ClsTexto.trocaAspaPorHtml(txtDescricaoTipo.Text);

            if (ckPadrao.Checked)
            { objSolucaoProjetoTipo.FlagTipoPadrao.Valor = "S"; }
            else
            { objSolucaoProjetoTipo.FlagTipoPadrao.Valor = "N"; }

            if (objSolucaoProjetoTipo.insere(out strMensagem) == false)
            {
                lblMensagem.Text = strMensagem;
                imgIcone.ImageUrl = "images/icones/aviso.gif";
                divMensagem.Visible = true;
            }
            else
            {
                divMensagem.Visible = false;
                ServiceDesk.Negocio.ClsSolucaoProjetoTipo.geraGridView(gvSolucaoProjetoTipo);
            }
        }
        catch (Exception ex)
        {
            lblMensagem.Text = ex.Message;
            imgIcone.ImageUrl = "images/icones/aviso.gif";
            divMensagem.Visible = true;

            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, user.IDusuario.ToString(), this.Request.Path, "0", ex.ToString());
        }
    }

    /// <summary>
    /// Evento que torna visivel objetos para cadastro de novo registro.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void imgNovoTipoDia_Click(object sender, ImageClickEventArgs e)
    {
        this.pnlNovoTipo.Visible = true;
        txtDescricaoTipo.Text = string.Empty;

    }
    #endregion
}