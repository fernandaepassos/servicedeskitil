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
using System.Linq;

public partial class WUCAnexo : System.Web.UI.UserControl
{
    #region Declarações Publicas


    #endregion

    #region Declarações Privadas

    private int intCodigoIdentificador;
    private string strTabelaRelacionada;
    private System.Web.UI.WebControls.Unit strAltura;
    private System.Web.UI.WebControls.Unit strLargura;

    #endregion

    #region Propriedades

    /// <summary>
    /// Codigo identificador do registro da tabela ralacionada.
    /// </summary>
    public int CodigoIdentificador
    {
        get
        { return intCodigoIdentificador; }
        set
        { intCodigoIdentificador = value; }
    }

    /// <summary>
    /// Tabela Relacionada
    /// </summary>
    public string TabelaRelacionada
    {
        get
        { return strTabelaRelacionada; }
        set
        { strTabelaRelacionada = value; }
    }

    /// <summary>
    /// Altura do Controle
    /// </summary>
    public System.Web.UI.WebControls.Unit Altura
    {
        get
        { return strAltura; }
        set
        { strAltura = value; }
    }

    /// <summary>
    /// Altura do Controle
    /// </summary>
    public System.Web.UI.WebControls.Unit Largura
    {
        get
        { return strLargura; }
        set
        { strLargura = value; }
    }


    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                //Seta a largura/altura do painel de anexo
                if (!this.Largura.IsEmpty)
                { pnlAnexo.Width = Largura; }
                if (!this.Altura.IsEmpty)
                { pnlAnexo.Height = Altura; }
            }
        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }

    #region métodos
    public void CarregaAnexos(string CodigoIdentificador, string TabelaRelacionada)
    {
        try
        {
            lblCodigoIdentificador.Text = CodigoIdentificador;
            lblTabela.Text = TabelaRelacionada;
            SServiceDesk.Negocio.ClsAnexo objAnexo = new SServiceDesk.Negocio.ClsAnexo();
            objAnexo.Codigo.CampoIdentificador = false;
            objAnexo.Tabela.Valor = TabelaRelacionada;
            objAnexo.Tabela.CampoIdentificador = true;
            objAnexo.TabelaIdentificador.Valor = CodigoIdentificador;
            objAnexo.TabelaIdentificador.CampoIdentificador = true;
            SServiceDesk.Negocio.ClsAnexo.geraGridView(gvDocumento, objAnexo, true);
            objAnexo = null;

            //Se as grid esta vazia
            if (gvDocumento.Rows.Count <= 0)
            {
                pnlAnexo.GroupingText = "Arquivos Anexos (Nenhum arquivo anexado)";
            }

        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }

    public void DesabilitarBotaoSalvar()
    {
        btnSalvaDocumento.Visible = false;
    }
    /// <summary>
    /// Fornece um meio de acesso ao painel de mensagem
    /// </summary>
    /// <param name="Mensagem">Mensagem a ser exibida na tela</param>
    /// <param name="Imagem">Nome da imagem do ícone do painel</param>
    /// <param name="Ativo">true para Exibir, false para Ocultar</param>
    /// <example>ExibeMensagem("teste","images/icones/aviso.gif",true)</example>
    private void ExibeMensagem(String Mensagem, String Imagem, bool Ativo)
    {
        try
        {
            Label lblMensagem = (Label)Parent.FindControl("lblMensagem");
            Image imgIcone = (Image)Parent.FindControl("imgIcone");
            HtmlControl divMensagem = (HtmlControl)Parent.FindControl("divMensagem");

            lblMensagem.Text = Mensagem;
            imgIcone.ImageUrl = Imagem;

            if (Ativo == true)
            {
                lblMensagem.Visible = true;
                divMensagem.Visible = true;
            }
            else if (Ativo == false)
            {
                lblMensagem.Visible = false;
                divMensagem.Visible = false;
            }
            else //nao foi especificado, assume true
            {
                lblMensagem.Visible = true;
                divMensagem.Visible = true;
            }
        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }

    }

    #endregion

    #region Evento salvaDocumento
    /// <summary>
    /// Grava um documento associado ao item
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void salvaDocumento(object sender, EventArgs e)
    {
        try
        {
            salvaDocumento(false, lblCodigoIdentificador.Text, lblTabela.Text);
        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region Método salvaDocumento
    /// <summary>
    /// Grava um documento associado ao item
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void salvaDocumento(bool bolModoSilencioso, string CodigoIdentificador, string TabelaRelacionada)
    {
        try
        {
            bool updateArquivo = false;
            string anexoCodigo = string.Empty;

            foreach (GridViewRow row in gvDocumento.Rows)
            {
                string nomeArquivo = (row.FindControl("lblCaminho") as Label).Text.Split('\\').Last(); 
                string descricao = (row.FindControl("txtNome") as Label).Text;

                if(flDocumento.FileName == nomeArquivo)
                {
                    if (txtDocumentoNome.Text == descricao)
                    {
                        ExibeMensagem("Este arquivo já foi anexado", "images/icones/aviso.gif", true);
                        return;
                    }
                    else
                    {
                        updateArquivo = true;
                        anexoCodigo = (row.FindControl("lblCodigo") as Label).Text;
                    }

                    break;
                }
            }

            string strMensagem = string.Empty;
            string strImagem = string.Empty;
            string strFileName = string.Empty;

            lblTabela.Text = TabelaRelacionada;

            SServiceDesk.Negocio.ClsAnexo objAnexo = new SServiceDesk.Negocio.ClsAnexo();
            ClsIdentificador objIdentificador = new ClsIdentificador();

            objAnexo.Caminho.Valor = string.Format("{0}\\docs\\{2}_{1}\\", Server.MapPath("."),CodigoIdentificador,TabelaRelacionada.ToLower());
            string result = ServiceDesk.Generica.ClsArquivo.uploadArquivo(flDocumento, objAnexo.Caminho.Valor);
            if (string.IsNullOrEmpty(result))
            {
                objIdentificador.Tabela.Valor = objAnexo.Atributos.NomeTabela;
                objAnexo.Codigo.Valor = updateArquivo ? anexoCodigo : objIdentificador.getProximoValor().ToString();
                objAnexo.PessoaInclusor.Valor = ClsUsuario.getCodigoUsuario().ToString();
                objAnexo.Tabela.Valor = lblTabela.Text.Trim();
                objAnexo.TabelaIdentificador.Valor = CodigoIdentificador;
                if (txtDocumentoNome.Text != string.Empty)
                    objAnexo.Nome.Valor = txtDocumentoNome.Text;
                else
                    objAnexo.Nome.Valor = strFileName;

                objAnexo.DataInclusao.Valor = DateTime.Now.ToString(ClsParametro.DataInclusao);
                objAnexo.Caminho.Valor += ServiceDesk.Generica.ClsArquivo.getNomeArquivo(flDocumento);
                txtDocumentoNome.Text = string.Empty;

                bool arqStatus = updateArquivo ? objAnexo.altera(out strMensagem) : objAnexo.insere(out strMensagem);
                if (arqStatus)
                {
                    if (!updateArquivo) objIdentificador.atualizaValor();
                    strImagem = "images/icones/aviso.gif";
                }
            }
            else
            {
                strMensagem = result;
                strImagem = "images/icones/aviso.gif";
            }

            if (!bolModoSilencioso)
                ExibeMensagem(strMensagem, strImagem, true);

            objIdentificador = null;
            objAnexo = null;

            SServiceDesk.Negocio.ClsAnexo objAnexo2 = new SServiceDesk.Negocio.ClsAnexo();
            objAnexo2.Codigo.CampoIdentificador = false;
            objAnexo2.Tabela.Valor = lblTabela.Text.Trim();
            objAnexo2.Tabela.CampoIdentificador = true;
            objAnexo2.TabelaIdentificador.Valor = CodigoIdentificador;
            objAnexo2.TabelaIdentificador.CampoIdentificador = true;
            SServiceDesk.Negocio.ClsAnexo.geraGridView(gvDocumento, objAnexo2, true);
            objAnexo2 = null;
        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region Evento gvDocumento_PageIndexChanging
    /// <summary>
    /// Evento que ocorre quando é mudada a página da GridView
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvDocumento_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView objGridView = (GridView)sender;
        objGridView.PageIndex = e.NewPageIndex;
        objGridView = null;
    }
    #endregion

    #region Evento gvDocumento_RowDataBound
    /// <summary>
    /// Evento que ocorre na montagem da GridView
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvDocumento_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            try
            {
                // Adiciona um evento javascript no botão Excluir
                ImageButton btnExcluir = (ImageButton)e.Row.Cells[3].Controls[0];
                btnExcluir.Attributes.Add("onclick", "if (!verifica()) return false;");

                Label lblCodigo = (Label)e.Row.FindControl("lblCodigo");
                Label lblCaminho = (Label)e.Row.FindControl("lblCaminho");
                Label lblArquivo = (Label)e.Row.FindControl("lblArquivo");
                Label txtNome = (Label)e.Row.FindControl("txtNome");
                HyperLink hplArquivo = (HyperLink)e.Row.FindControl("hplArquivo");

                string strArquivo = ServiceDesk.Generica.ClsTexto.getParteFinalPorChave(lblCaminho.Text, "\\docs\\");

                hplArquivo.NavigateUrl = "docs\\" + strArquivo;
                hplArquivo.Target = "_blank";
            }
            catch (Exception ex)
            {
                ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
            }
        }
    }
    #endregion

    #region Evento gvDocumento_OnRowCommand
    /// <summary>
    /// Evento que ocorre quando um comando da GridView é solicitado
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvDocumento_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            string strMensagem = string.Empty;
            string strImagem = string.Empty;

            if (e.CommandName == "Excluir")
            {
                GridViewRow objRow = gvDocumento.Rows[Convert.ToInt32(e.CommandArgument)];
                if (objRow != null)
                {
                    // Seleciona os códigos que serão excluidos
                    Label lblCodigoAnexo = (Label)objRow.FindControl("lblCodigo");
                    SServiceDesk.Negocio.ClsAnexo objAnexo = new SServiceDesk.Negocio.ClsAnexo(Convert.ToInt32(lblCodigoAnexo.Text.Trim()));

                    try
                    {
                        if (objAnexo.exclui())
                        {
                            strMensagem = "Item excluido com sucesso.";
                            strImagem = "images/icones/info.gif";
                        }
                    }
                    catch
                    {
                        strMensagem = "Não foi possível excluir o anexo.";
                        strImagem = "images/icones/aviso.gif";
                    }

                    objRow = null;
                    objAnexo = null;

                }
                //Remonta a grid de anexos
                CarregaAnexos(lblCodigoIdentificador.Text, "Chamado");

                ExibeMensagem(strMensagem, strImagem, true);
            }
        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }

    }

    #endregion
}
