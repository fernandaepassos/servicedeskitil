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

public partial class Minhas_Aplicacoes : BasePage
{
    public string strLinkPagina = string.Empty;
    public string strFimLinkPagina = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            CheckAcesso(42);

            //Esconde a mensagem de erro
            lblMensagem.Visible = false;
            divMensagem.Visible = false;

            if (!Page.IsPostBack)
            {
                SServiceDesk.Negocio.ClsAplicacao.geraGridView(this.gvAplicacao);
            }
            strFimLinkPagina = ")";
            strLinkPagina = "Javascript:VisualizaAplicacao(";
        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, user.IDusuario.ToString(), this.Request.Path, "0", ex.ToString());
        }
    }

    protected void btnFiltra_Click(object sender, EventArgs e)
    {
        try
        {
            bool bPrimeiroParametro = true;
            String strMensagem = string.Empty;
            String strSql = "SELECT * FROM Aplicacao ";

            if (txtCodigo.Text.Trim() != String.Empty)
            {
                strSql += "WHERE ";
                strSql += "aplicacao_codigo = '" + ServiceDesk.Generica.ClsTexto.trocaAspaPorHtml(txtCodigo.Text.Trim()) + "' ";
                bPrimeiroParametro = false;
            }

            if (txtDescricao.Text.Trim() != String.Empty)
            {
                if (!bPrimeiroParametro)
                {
                    strSql += "AND ";
                }
                else
                {
                    strSql += "WHERE ";
                    bPrimeiroParametro = false;
                }
                strSql += " descricao LIKE '%" + ServiceDesk.Generica.ClsTexto.trocaAspaPorHtml(txtDescricao.Text.Trim()) + "%' ";
            }

            if (txtSigla.Text != String.Empty)
            {
                if (!bPrimeiroParametro)
                {
                    strSql += "AND ";
                }
                else
                {
                    strSql += "WHERE ";
                    bPrimeiroParametro = false;
                }
                strSql += "sigla = '" + ServiceDesk.Generica.ClsTexto.trocaAspaPorHtml(txtSigla.Text.Trim()) + "' ";
            }

            if (txtVersao.Text != String.Empty)
            {
                if (!bPrimeiroParametro)
                {
                    strSql += "AND ";
                }
                else
                {
                    strSql += "WHERE ";
                    bPrimeiroParametro = false;
                }
                strSql += "versao = '" + ServiceDesk.Generica.ClsTexto.trocaAspaPorHtml(txtVersao.Text.Trim()) + "'";
            }

            if (!SServiceDesk.Negocio.ClsAplicacao.geraGridViewQuery(gvAplicacao, strSql))
            {
                lblMensagem.Text = "Sua consulta naõ retornou resultados.";
                imgIcone.ImageUrl = "images/icones/aviso.gif";
                lblMensagem.Visible = true;
                divMensagem.Visible = true;
            }
        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, user.IDusuario.ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    protected void gvAplicacao_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Excluir")
            {
                GridViewRow objRow = gvAplicacao.Rows[Convert.ToInt32(e.CommandArgument)];
                if (objRow != null)
                {
                    // Seleciona os códigos que serão excluidos
                    Label lblCodigo = (Label)objRow.FindControl("lblCodigo");

                    SServiceDesk.Negocio.ClsAplicacao objAplicacao = new SServiceDesk.Negocio.ClsAplicacao(Convert.ToInt32(lblCodigo.Text));
                    try
                    {
                        if (objAplicacao.exclui())
                        {
                            lblMensagem.Text = "Item excluido com sucesso.";
                            imgIcone.ImageUrl = "images/icones/aviso.gif";
                            divMensagem.Visible = true;
                            lblMensagem.Visible = true;

                            txtCodigo.Text = String.Empty;
                            txtDescricao.Text = String.Empty;
                            txtSigla.Text = String.Empty;
                            txtVersao.Text = String.Empty;
                        }
                    }
                    catch
                    {
                        lblMensagem.Text = "Não foi possível excluir a Aplicação.<br>";
                        imgIcone.ImageUrl = "images/icones/erro.gif";
                        divMensagem.Visible = true;
                    }

                    objRow = null;
                    objAplicacao = null;
                }
                SServiceDesk.Negocio.ClsAplicacao.geraGridView(gvAplicacao);
            }
        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, user.IDusuario.ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    protected void gvAplicacao_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Condicao IF que exibe os dados no GridView (estado: não-editável)
                if ((e.Row.RowState.ToString() == "Normal") || (e.Row.RowState.ToString() == "Alternate"))
                {
                    ImageButton btnExcluir = (ImageButton)e.Row.Cells[4].Controls[0];
                    btnExcluir.Attributes.Add("onclick", "return verifica();");
                }
            }

        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, user.IDusuario.ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
}
