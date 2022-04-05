/*
- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
  • Tela de cadastramento e atualização das funções que os perfis da empresa pode ter.
  
  	Data: 27/01/2006
  	Autor: Fernanda Passos
  	Descrição: 
  
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

public partial class DireitoPerfil : BasePage
{
    #region Declarações
    public string strCodigoFuncaoMarcada = string.Empty;
    public int intRowIndex = -1;
    #endregion

    #region Eventos
    #region Evento Page_Load
    /// <summary>
    /// Evento Page_Load
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            CheckAcesso(38);

            if (!Page.IsPostBack)
            {
                SServiceDesk.Negocio.ClsAplicacao.geraDropDownListPorEmpresa(ddlAplicacao, SServiceDesk.Negocio.ClsEstruturaOrganizacional.GetCodigoEstrutura(user.IDusuario.ToString()), "descricao", "aplicacao_codigo");
            }
        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, user.IDusuario.ToString(), this.Request.Path, "0", ex.ToString());

        }
    }
    #endregion

    #region Evento ddlAplicacao_SelectedIndexChanged
    /// <summary>
    /// Evento ddlAplicacao_SelectedIndexChanged
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlAplicacao_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            SServiceDesk.Negocio.ClsPerfil.geraDropDownListPorEstruturaAplicacao(ddlTipoUsuario, SServiceDesk.Negocio.ClsEstruturaOrganizacional.GetCodigoEstrutura(user.IDusuario.ToString()), ddlAplicacao.SelectedValue, "descricao", "perfil_codigo");
        }
        catch
        { }
    }
    #endregion

    #region Valida os campos
    /// <summary>
    /// Valida os campos 
    /// </summary>
    /// <returns></returns>
    public bool ValidaCampos()
    {
        if (ddlAplicacao.SelectedValue == string.Empty)
        {
            lblMensagem.Text = "Por favor, selecione a aplicação.";
            lblMensagem.Visible = true;
            imgIcone.ImageUrl = "images/icones/info.gif";
            divMensagem.Visible = true;
            return false;
        }
        else if (ddlTipoUsuario.SelectedValue == string.Empty)
        {
            lblMensagem.Text = "Por favor, selecione o perfil.";
            lblMensagem.Visible = true;
            imgIcone.ImageUrl = "images/icones/info.gif";
            divMensagem.Visible = true;
            return false;
        }
        else if (tvFuncao.Nodes.Count <= 0)
        {
            lblMensagem.Text = "Por favor, informe as funções que estão para o perfil.";
            lblMensagem.Visible = true;
            imgIcone.ImageUrl = "images/icones/info.gif";
            divMensagem.Visible = true;
            return false;
        }
        return true;
    }
    #endregion

    #region Evento btnGravar_Click
    /// <summary>
    /// Evento btnGravar_Click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnGravar_Click(object sender, EventArgs e)
    {
        try
        {   //Verifica se os campos necessários foram preenchidos.
            if (ValidaCampos() == false) return;

            divMensagem.Visible = false;

            string strMensagem = string.Empty;

            int i;
            for (i = 0; i < tvFuncao.Nodes.Count; i++)
            {
                SServiceDesk.Negocio.ClsDireitoPerfil objDireitoPerfil = new SServiceDesk.Negocio.ClsDireitoPerfil();
                objDireitoPerfil.Codigo.Valor = objDireitoPerfil.GetCodigo(Convert.ToInt32(tvFuncao.Nodes[i].Value.Trim()), Convert.ToInt32(ddlTipoUsuario.SelectedValue.ToString().Trim())).ToString().Trim();
                if (tvFuncao.Nodes[i].Checked == true)
                {
                    if (objDireitoPerfil.Codigo.Valor == "0")
                    {
                        ServiceDesk.Negocio.ClsIdentificador objIdentificador = new ServiceDesk.Negocio.ClsIdentificador();
                        objIdentificador.Tabela.Valor = objDireitoPerfil.Atributos.NomeTabela;
                        objDireitoPerfil.Codigo.Valor = objIdentificador.getProximoValor().ToString();
                        objDireitoPerfil.CodigoFuncao.Valor = tvFuncao.Nodes[i].Value.Trim();
                        objDireitoPerfil.CodigoPerfil.Valor = ddlTipoUsuario.SelectedValue.ToString();
                        objDireitoPerfil.insere(out strMensagem);
                        objIdentificador.atualizaValor();
                        objIdentificador = null;
                    }
                }
                else
                {
                    objDireitoPerfil = new SServiceDesk.Negocio.ClsDireitoPerfil(Convert.ToInt32(objDireitoPerfil.Codigo.Valor));
                    objDireitoPerfil.exclui();
                }
                objDireitoPerfil = null;
            }

            SServiceDesk.Negocio.ClsFuncaoAplicacao.AtualizaCheckBoxTreeview(tvFuncao, Convert.ToInt32(ddlTipoUsuario.SelectedValue.ToString().Trim()));

            lblMensagem.Text = "Operação realizada com sucesso.";
            lblMensagem.Visible = true;
            imgIcone.ImageUrl = "images/icones/info.gif";
            divMensagem.Visible = true;
        }
        catch
        { }
    }
    #endregion

    #region Evento btnBusca_Click
    /// <summary>
    /// Evento btnBusca_Click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnBusca_Click(object sender, EventArgs e)
    {
        divMensagem.Visible = false;
        plTreview.Visible = true;

        if (ddlAplicacao.SelectedValue == "")
        {
            lblMensagem.Text = "Por favor, para executar o filtro é necessário selecionar a aplicação.";
            imgIcone.ImageUrl = "images/icones/info.gif";
            divMensagem.Visible = true;
            return;
        }
        else if (ddlTipoUsuario.SelectedValue == "")
        {
            lblMensagem.Text = "Por favor, para executar o filtro é necessário selecionar o perfil.";
            imgIcone.ImageUrl = "images/icones/info.gif";
            divMensagem.Visible = true;
            return;
        }
        SServiceDesk.Negocio.ClsFuncaoAplicacao.PopulaNoRaiz(0, tvFuncao);
        SServiceDesk.Negocio.ClsFuncaoAplicacao.AtualizaCheckBoxTreeview(tvFuncao, Convert.ToInt32(ddlTipoUsuario.SelectedValue.Trim()));
    }
    #endregion

    #endregion

    #region Métodos
    #region Métodos MarcaPais
    /// <summary>
    /// Método MarcaPais
    /// </summary>
    /// <param name="objGridView"></param>
    /// <param name="strChaveFuncaoMarcada"></param>
    /// <param name="strChecked"></param>
    /// <param name="intRowIndex"></param>
    protected void MarcaPais(GridView objGridView, String strChaveFuncaoMarcada, bool strChecked, int intRowIndex)
    {
        String strCodigoFuncaoLinhaAtual = String.Empty;

        for (int i = intRowIndex; i >= 0; i--)
        {
            GridViewRow row = objGridView.Rows[i];
            CheckBox cb = (CheckBox)row.Cells[0].Controls[5];
            Label lb = (Label)row.FindControl("lblCodigo");

            string[] split = lb.Text.Split(new Char[] { ',' });
            if (strChaveFuncaoMarcada.IndexOf(lb.Text.Trim()) != -1)
            {
                if (strChecked == false)
                {
                    if (VerificaFilho(objGridView, lb.Text, intRowIndex) == false)
                        cb.Checked = strChecked;
                }
                else
                {
                    cb.Checked = strChecked;
                }
            }
        }
    }
    #endregion

    #region Método VerificaFilho
    /// <summary>
    /// Métodos VerificaFilho
    /// </summary>
    /// <param name="objGridView"></param>
    /// <param name="strChaveFuncaoLinha"></param>
    /// <param name="intRowIndex"></param>
    /// <returns></returns>
    protected bool VerificaFilho(GridView objGridView, string strChaveFuncaoLinha, int intRowIndex)
    {
        bool bTemFilhos = false;
        for (int i = intRowIndex + 1; i <= objGridView.Rows.Count - 1; i++)
        {
            GridViewRow row = objGridView.Rows[i];
            CheckBox cb = (CheckBox)row.Cells[0].Controls[5];
            Label lb = (Label)row.FindControl("lblCodigo");

            if (lb.Text.Trim().IndexOf(strChaveFuncaoLinha) != -1) //é filho
            {
                if (cb.Checked)
                {
                    bTemFilhos = true;
                    break;
                }
            }
        }
        return bTemFilhos;
    }
    #endregion

    #region Método MarcaFilhos
    /// <summary>
    /// Método MarcaFilhos
    /// </summary>
    /// <param name="objGridView"></param>
    /// <param name="CodigoFuncaoMarcada"></param>
    /// <param name="strChecked"></param>
    protected void MarcaFilhos(GridView objGridView, String CodigoFuncaoMarcada, bool strChecked)
    {
        foreach (GridViewRow row in objGridView.Rows)
        {
            CheckBox cb = (CheckBox)row.Cells[0].Controls[5];
            Label lb = (Label)row.FindControl("lblCodigo");

            //Verifica se o item deve ser marcado/desmarcado
            string[] split = lb.Text.Split(new Char[] { ',' });
            if (split.Length > 0)
            {
                foreach (string s in split)
                {
                    if (s.Trim() == CodigoFuncaoMarcada)
                    {
                        cb.Checked = strChecked;
                        break;
                    }
                }
            }
        }
    }
    #endregion

    #region Método MarcaItem
    /// <summary>
    /// Métodos MarcaItem
    /// </summary>
    /// <param name="strMarcado"></param>
    /// <returns></returns>
    public bool MarcaItem(String strMarcado)
    {
        if (strMarcado.ToLower() == "true")
        { return true; }
        else
        { return false; }
    }
    #endregion
    #endregion


}