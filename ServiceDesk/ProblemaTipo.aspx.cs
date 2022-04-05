/*
- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
  • Formulário de cadastramento dos tipos de problemas que podem existir.
  
  	Data: 29/11/2005
  	Autor: Fernanda Passos
  	Descrição: Este WebForm permite incluir, excluir e alterar os tipos de problemas que podem
    haver. De acordo com o conjunto de boas práticas UTIL define-se que problema é algo distinto
    de erro e erro conhecido, más o Help Desk definido pela Ghenus na pessoa do Flavio da Purificação
    consensamos que Erro e Erro conhecido classifica o problema.
  
  
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

public partial class ProblemaTipo : BasePage
{
    #region Declarações
    ServiceDesk.Negocio.ClsProblemaTipo objProblemaTipo = new ServiceDesk.Negocio.ClsProblemaTipo(); 
    #endregion

    #region Page_Load
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            CheckAcesso(23);

            if (!Page.IsPostBack)
            {
                ClsProblemaTipo.geraGridView(gvProblemaTipo);
            }
        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, user.IDusuario.ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region Eventos

    /// <summary>
    /// Evento PageIndexChanging do grid Tipo de problema.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvProblemaTipo_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        divMensagem.Visible = false;
        gvProblemaTipo.PageIndex = e.NewPageIndex;
        ServiceDesk .Negocio .ClsProblemaTipo .geraGridView(gvProblemaTipo);
    }

    /// <summary>
    ///  Evento RowCancelingEdit do grid Tipo de problema.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvProblemaTipo_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        divMensagem.Visible = false;
        gvProblemaTipo.EditIndex = -1;
        ServiceDesk.Negocio.ClsProblemaTipo.geraGridView(gvProblemaTipo);
    }
    
    /// <summary>
    /// Evento RowCommand do Grid Tipo de problema.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvProblemaTipo_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            GridViewRow objRow = gvProblemaTipo.Rows[Convert.ToInt32(e.CommandArgument)];

            if (e.CommandName == "Excluir")
            {
                if (objRow != null)
                {
                    Label lblCodigo = (Label)objRow.FindControl("lblCodigo");
                    objProblemaTipo.Codigo.Valor = lblCodigo.Text.Trim(); ;

                    string strMensagem = string.Empty;

                    if (objProblemaTipo.exclui(out strMensagem) == false)
                    {
                        lblMensagem.Text = strMensagem;
                        imgIcone.ImageUrl = "images/icones/info.gif";
                        divMensagem.Visible = true;
                    }
                    else
                        divMensagem.Visible = false;

                    objRow = null;
                    objProblemaTipo = null;
                }
                ServiceDesk.Negocio.ClsProblemaTipo.geraGridView(gvProblemaTipo);
            }
            else if (e.CommandName == "Novo")
            {
                gvProblemaTipo.EditIndex = 0;
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
    /// Evento RowEditing do Grid tipo de problema.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvProblemaTipo_RowEditing(object sender, GridViewEditEventArgs e)
    {
        divMensagem.Visible = false;
        gvProblemaTipo.EditIndex = e.NewEditIndex;//ok
        ServiceDesk.Negocio.ClsProblemaTipo.geraGridView(gvProblemaTipo); 
    }
    
    /// <summary>
    /// Evento RowUpdating do Grid Tipo de problema.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvProblemaTipo_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            String strMensagem = String.Empty;

            GridViewRow objRow = gvProblemaTipo.Rows[e.RowIndex];
            if (objRow != null)
            {
                TextBox txtCodigo = (TextBox)objRow.FindControl("txtCodigo");
                TextBox txtDescricao = (TextBox)objRow.FindControl("txtDescricao");

                objProblemaTipo.Codigo.Valor = txtCodigo.Text.Trim();
                objProblemaTipo.Nome.Valor = txtDescricao.Text.Trim();

                if (objProblemaTipo.altera(out strMensagem) == false)
                {
                    lblMensagem.Text = strMensagem;
                    imgIcone.ImageUrl = "images/icones/aviso.gif";
                    divMensagem.Visible = true;
                }
                else
                    divMensagem.Visible = false;

                objRow = null;
                objProblemaTipo = null;
            }
        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, user.IDusuario.ToString(), this.Request.Path, "0", ex.ToString());
        }
    }

    
    /// <summary>
    /// Evento que chama a inserção de um novo tipo no banco de dados.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSalvar_Click(object sender, EventArgs e)
    {
        try
        {
            //Verifica se caracteres da descrição execede o limite.
            if (txtDescricaoTipo.Text.Length > 50)
            {
                objProblemaTipo.Nome.Valor = txtDescricaoTipo.Text.Trim();
                objProblemaTipo.Nome.Valor = ServiceDesk.Generica.ClsTexto.trocaAspaPorHtml(txtDescricaoTipo.Text.Substring(0, 50));
                txtDescricaoTipo.Text = ServiceDesk.Generica.ClsTexto.trocaAspaPorHtml(txtDescricaoTipo.Text.Substring(0, 50));
            }
            else objProblemaTipo.Nome.Valor = ServiceDesk.Generica.ClsTexto.trocaAspaPorHtml(txtDescricaoTipo.Text);
            
            
            string strMensagem = string.Empty;

            ServiceDesk.Negocio.ClsIdentificador objIdentificador = new ServiceDesk.Negocio.ClsIdentificador();
            objIdentificador.Tabela.Valor = objProblemaTipo.Atributos.NomeTabela;
            objProblemaTipo.Codigo.Valor = objIdentificador.getProximoValor().ToString();

            if (objProblemaTipo.insere(out strMensagem) == false)
            {
                lblMensagem.Text = strMensagem;
                imgIcone.ImageUrl = "images/icones/aviso.gif";
                divMensagem.Visible = true;
            }
            else
            {
                objIdentificador.atualizaValor(); 
                ServiceDesk.Negocio.ClsProblemaTipo.geraGridView(gvProblemaTipo);
                divMensagem.Visible = false;
            }
            objIdentificador = null;
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
    /// Evento que torna visivel os objetos para inserção do novo tipo
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void imgNovoTipo_Click(object sender, ImageClickEventArgs e)
    {
        this.pnlNovoTipo.Visible = true;
        txtDescricaoTipo.Text = string.Empty;
    }
    #endregion
}
