/*
- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
  • Classe para manipulação dos registros da tabela Conhecimento.
  
  	Data: 29/12/2005
  	Autor: Fernanda Passos
  	Descrição: Esta classe apresenta várias funcionalidades que permite manipular os dados
    da tabela Conhecimento.
  
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

public partial class BaseConhecimento : System.Web.UI.UserControl
{
    #region Declarações
    ServiceDesk.Negocio.ClsConhecimento objConhecimento = new ServiceDesk.Negocio.ClsConhecimento();

    #endregion

    #region Métodos

    #region Preenche Drop Down
    /// <summary>
    /// Preenche Drop Down
    /// </summary>
    public void PreencheDroDown(string strProcesso)
    {
        try
        {
            if (strProcesso == "Perfil")
            {
                ServiceDesk.Negocio.ClsConhecimentoPerfil.geraDropDownListPerfil(dlPerfil, "Selecione o perfil");
            }
            else if (strProcesso == "Conhecimento")
            {
                dlStatusConhecimento.Items.Clear();
                ServiceDesk.Negocio.ClsStatusTabela.geraDropDownList(dlStatusConhecimento, "Conhecimento");
            }
        }
        catch (Exception ex)
        {
            imgIcone.ImageUrl = "images/icones/aviso.gif";
            lblMensagem.Text = ex.Message;
            divMensagem.Visible = true;

            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());

        }
    }
    #endregion

    #region Conhecimento novo
    /// <summary>
    /// Conhecimento novo
    /// </summary>
    public void ConhecimentoNovo()
    {
        try
        {
            txtCodConhecimento.Text = string.Empty;
            txtDescricaoConhecimento.Text = string.Empty;
            txtNomeAnexo.Text = string.Empty;
            txtNomeConhecimento.Text = string.Empty;
            dlPerfil.SelectedIndex = -1;
            dlStatusConhecimento.SelectedIndex = -1;
        }
        catch (Exception ex)
        {
            imgIcone.ImageUrl = "images/icones/aviso.gif";
            lblMensagem.Text = ex.Message;
            divMensagem.Visible = true;

            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region Preenche Grid
    /// <summary>
    /// Preenche dados da grid
    /// </summary>
    /// <param name="strProcesso"></param>
    public void PrencheGrid(string strProcesso)
    {
        try
        {
            if (strProcesso == "Anexo")
            {
                if (this.txtCodConhecimento.Text.Trim() != string.Empty)
                {
                    SServiceDesk.Negocio.ClsAnexo objAnexo = new SServiceDesk.Negocio.ClsAnexo();

                    objAnexo.Codigo.CampoIdentificador = false;
                    objAnexo.Tabela.Valor = "Conhecimento";
                    objAnexo.Tabela.CampoIdentificador = true;
                    objAnexo.TabelaIdentificador.Valor = txtCodConhecimento.Text.Trim();
                    objAnexo.TabelaIdentificador.CampoIdentificador = true;
                    SServiceDesk.Negocio.ClsAnexo.geraGridView(this.gvAnexo, objAnexo, true);
                    objAnexo = null;
                }
            }
            else if (strProcesso == "Solucao")
            {
                //ServiceDesk.Negocio.ClsConhecimentoProjeto.geraGridView(gvSolucao, Convert.ToInt32(txtCodConhecimento.Text.Trim()));
            }
            else if (strProcesso == "Perfil")
            {
                ServiceDesk.Negocio.ClsConhecimentoPerfil.geraGridView(gvPerfil, Convert.ToInt32(txtCodConhecimento.Text.Trim()));
            }
        }
        catch (Exception ex)
        {
            imgIcone.ImageUrl = "images/icones/aviso.gif";
            lblMensagem.Text = ex.Message;
            divMensagem.Visible = true;

            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());

        }
    }
    #endregion

    #region PopulaNos
    /// <summary>
    /// PopulaNos
    /// </summary>
    /// <param name="objDataReader">Nome da DataReader</param>
    /// <param name="objTreeNodeCollection">Nome da TreeNodeCollection</param>
    /// <param name="strProcesso">Nome do processso, se Perfil, Item de configuração e outros.</param>
    public void PopulaNos(System.Data.SqlClient.SqlDataReader objDataReader, TreeNodeCollection objTreeNodeCollection, string strProcesso)
    {
        try
        {
            while (objDataReader.Read())
            {
                TreeNode objTreeNode = new TreeNode();

                objTreeNode.Text = objDataReader["descricao"].ToString();
                objTreeNode.Value = objDataReader["codigo"].ToString();

                if (strProcesso == "ICTipo" || strProcesso == "IC")
                {
                    if (Convert.ToInt32(objDataReader["pai"]) > 0)
                    {
                        objTreeNode.PopulateOnDemand = true;
                    }
                }
                if (strProcesso == "IC")
                {
                    if (txtCodConhecimento.Text.Trim() != string.Empty)
                    {
                        if (objDataReader["selecionado"].ToString() != string.Empty)
                            objTreeNode.Checked = true;
                        else
                            objTreeNode.Checked = false;
                    }
                }

                objTreeNodeCollection.Add(objTreeNode);
            }
            objDataReader.Dispose();
        }
        catch (Exception ex)
        {
            imgIcone.ImageUrl = "images/icones/aviso.gif";
            lblMensagem.Text = ex.Message;
            divMensagem.Visible = true;

            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());

        }
    }
    #endregion

    #region PopulaNoRaiz
    /// <summary>
    /// PopulaNoRaiz
    /// </summary>
    /// <param name="objTreeView">Nome da TreeView</param>
    /// <param name="objTreeNode">Nome do TreeNode</param>
    /// <param name="intCodigoTipoItemConfiguracao">Número inteiro do código do tipo de item de configuração</param>
    /// <param name="intCodigoItemConfiguracao">Número inteiro do código do item de configuração</param>
    /// <param name="bolPopulaPorTipoDeIC">Treu ou false. Se os IC serão populados pelo tipo ou não</param>
    /// <param name="strProcesso">Nome do processo</param>
    public void PopulaNoRaiz(TreeView objTreeView, TreeNode objTreeNode, int intCodigoItemConfiguracaoTipo, int intCodigoItemConfiguracao, bool bolPopulaPorTipoDeIC, string strProcesso, bool bolPopulaSubNivel)
    {
        try
        {
            string strSql = string.Empty;
            divMensagem.Visible = false;

            if (strProcesso == "ICTipo")
            {
                strSql = "SELECT ic_tipo_codigo as codigo, ic_tipo_codigo_superior, descricao";
                strSql += ", (SELECT count(*) FROM ICTipo WHERE ic_tipo_codigo_superior = itemTipo.ic_tipo_codigo) pai";
                strSql += " FROM ICTipo itemTipo";
                if (intCodigoItemConfiguracaoTipo <= 0) strSql += " WHERE ic_tipo_codigo_superior is null";
                if (intCodigoItemConfiguracaoTipo > 0) strSql += " WHERE ic_tipo_codigo_superior = " + intCodigoItemConfiguracaoTipo + "";
                strSql += " ORDER BY descricao";
            }
            else if (strProcesso == "IC" && bolPopulaPorTipoDeIC == false && bolPopulaSubNivel == false)
            {
                strSql = "SELECT ic_codigo as codigo, ic_codigo_superior, nome as descricao";
                strSql += ", (SELECT count(*) FROM IC WHERE ic_codigo_superior = item.ic_codigo) pai";
                if (txtCodConhecimento.Text.Trim() != string.Empty) strSql += " ,(select distinct(ic_codigo) from ConhecimentoIC where conhecimento_codigo = " + Convert.ToInt32(txtCodConhecimento.Text.Trim()) + " and ic_codigo = item.ic_codigo) selecionado ";
                strSql += " FROM IC item";
                if (intCodigoItemConfiguracao > 0) strSql += " WHERE ic_codigo = " + intCodigoItemConfiguracao + "";
                if (intCodigoItemConfiguracao <= 0) strSql += " WHERE ic_codigo_superior is null";
                strSql += " ORDER BY nome";
            }
            else if (strProcesso == "IC" && bolPopulaPorTipoDeIC == true)
            {
                strSql = "SELECT ic_codigo, ic_codigo_superior, nome as descricao";
                strSql += " , (SELECT count(*) FROM IC WHERE ic_codigo_superior = item.ic_codigo) pai";
                strSql += " FROM IC item, ICTipo ICT";
                strSql += " where item.ic_tipo_codigo = ICT.ic_tipo_codigo";
                if (intCodigoItemConfiguracaoTipo > 0) strSql += " and item.ic_tipo_codigo = " + intCodigoItemConfiguracaoTipo + "";
                if (intCodigoItemConfiguracaoTipo <= 0) strSql += " and item.ic_tipo_codigo is not null";
                strSql += " ORDER BY nome";
            }
            else if (strProcesso == "IC" && bolPopulaSubNivel == true)
            {
                strSql = "SELECT ic_codigo as codigo, ic_codigo_superior, nome as descricao";
                strSql += ", (SELECT count(*) FROM IC WHERE ic_codigo_superior = item.ic_codigo) pai";
                if (txtCodConhecimento.Text.Trim() != string.Empty) strSql += " ,(select distinct(ic_codigo) from ConhecimentoIC where conhecimento_codigo = " + Convert.ToInt32(txtCodConhecimento.Text.Trim()) + " and ic_codigo = item.ic_codigo) selecionado ";
                strSql += " FROM IC item";
                strSql += " WHERE ic_codigo_superior = " + intCodigoItemConfiguracao + "";
                strSql += " ORDER BY nome";
            }

            if (strProcesso != string.Empty)
            {
                System.Data.SqlClient.SqlDataReader objDataReader = ServiceDesk.Banco.ClsBanco.geraDataReader(strSql);

                //Limpa os objetos.
                if (objTreeView != null) objTreeView.Nodes.Clear();
                if (objTreeNode != null) objTreeNode.ChildNodes.Clear();

                //Alimenta os nós com os dados.
                if (objTreeView != null) PopulaNos(objDataReader, objTreeView.Nodes, strProcesso);
                if (objTreeNode != null) PopulaNos(objDataReader, objTreeNode.ChildNodes, strProcesso);

                objDataReader.Dispose();
            }
        }
        catch (Exception ex)
        {
            imgIcone.ImageUrl = "images/icones/aviso.gif";
            lblMensagem.Text = ex.Message;
            divMensagem.Visible = true;

            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());

        }
    }
    #endregion

    #region Tipo de item de configuração - Popula Nó Raiz
    /// <summary>
    /// Método que popula os nós que não possuem pai
    /// </summary>
    /// <param name="tvTreeView">Nome da treeview</param> 
    /// <param name="intCodigoTipo">Código do tipo</param>
    public void TipoItemConfigPopulaNoRaiz(int intCodigoTipo, TreeView tvTreeView, TreeNode objTreeNode)
    {
        try
        {
            String strSql = String.Empty;
            divMensagem.Visible = false;

            strSql = "SELECT ic_tipo_codigo, ic_tipo_codigo_superior, descricao";
            strSql += ", (SELECT count(*) FROM ICTipo WHERE ic_tipo_codigo_superior = itemTipo.ic_tipo_codigo) pai";
            strSql += " FROM ICTipo itemTipo";
            if (intCodigoTipo <= 0) strSql += " WHERE ic_tipo_codigo_superior is null";
            if (intCodigoTipo > 0) strSql += " WHERE ic_tipo_codigo_superior = " + intCodigoTipo + "";
            strSql += " ORDER BY descricao";

            System.Data.SqlClient.SqlDataReader objDataReader = ServiceDesk.Banco.ClsBanco.geraDataReader(strSql);

            if (tvTreeView != null)
            {
                tvTreeView.Nodes.Clear();
                TipoItemConfiguracaoPopulaNos(objDataReader, tvTreeView.Nodes);
            }
            else if (objTreeNode != null)
            {
                TipoItemConfiguracaoPopulaNos(objDataReader, objTreeNode.ChildNodes);
            }

            objDataReader.Dispose();
        }
        catch (Exception ex)
        {
            imgIcone.ImageUrl = "images/icones/aviso.gif";
            lblMensagem.Text = ex.Message;
            divMensagem.Visible = true;

            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }

    }
    #endregion

    #region Tipo de item de configuração - Popula nós
    /// <summary>
    /// Tipo de item de configuração - Popula nós
    /// </summary>
    public void TipoItemConfiguracaoPopulaNos(System.Data.SqlClient.SqlDataReader objDataReader, TreeNodeCollection objTreeNodeCollection)
    {
        try
        {
            while (objDataReader.Read())
            {
                TreeNode objTreeNode = new TreeNode();

                objTreeNode.Text = objDataReader["descricao"].ToString();
                objTreeNode.Value = objDataReader["ic_tipo_codigo"].ToString();

                if (Convert.ToInt32(objDataReader["pai"]) > 0)
                {
                    objTreeNode.PopulateOnDemand = true;
                }
                objTreeNodeCollection.Add(objTreeNode);
            }
        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());

        }
    }
    #endregion

    #region Item de configuração - Popula Sub Nível
    /// <summary>
    /// Método que popula o sub nível do item de configuração.
    /// </summary>
    /// <param name="intCodigoPai">Código do item pai</param>
    /// <param name="objTreeNode">Nome da tree node</param>
    public void ItemConfiguracaoPopulaSubNivel(int intCodigoPai, TreeNode objTreeNodePai)
    {
        try
        {
            String strSql = String.Empty;

            strSql = "SELECT ic_codigo, ic_codigo_superior, nome";
            strSql += ", (SELECT count(*) FROM IC WHERE ic_codigo_superior = item.ic_codigo) pai";
            if (txtCodConhecimento.Text.Trim() != string.Empty) strSql += " ,(select distinct(ic_codigo) from ConhecimentoIC where conhecimento_codigo = " + Convert.ToInt32(txtCodConhecimento.Text.Trim()) + " and ic_codigo = item.ic_codigo) selecionado ";
            strSql += " FROM IC item";
            strSql += " WHERE ic_codigo_superior = " + intCodigoPai.ToString();
            strSql += " ORDER BY nome";

            System.Data.SqlClient.SqlDataReader objDataReader = ServiceDesk.Banco.ClsBanco.geraDataReader(strSql);

            ItemConfiguracaoPopulaNos(objDataReader, objTreeNodePai.ChildNodes);

            objDataReader.Dispose();
        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region Item configuração - Popula nós
    /// <summary>
    /// Item de configuração - Popula Nós
    /// </summary>
    public void ItemConfiguracaoPopulaNos(System.Data.SqlClient.SqlDataReader objDataReader, TreeNodeCollection objTreeNodeCollection)
    {
        try
        {
            while (objDataReader.Read())
            {
                TreeNode objTreeNode = new TreeNode();
                objTreeNode.Text = objDataReader["nome"].ToString();
                objTreeNode.Value = objDataReader["ic_codigo"].ToString().Trim();

                if (txtCodConhecimento.Text.Trim() != string.Empty)
                {
                    if (objDataReader["selecionado"].ToString() != string.Empty)
                        objTreeNode.Checked = true;
                    else
                        objTreeNode.Checked = false;
                }

                if (Convert.ToInt32(objDataReader["pai"]) > 0)
                {
                    objTreeNode.PopulateOnDemand = true;
                }
                objTreeNodeCollection.Add(objTreeNode);
            }
        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region Item de configuração - Popula nós raiz
    /// <summary>
    /// Item de configuração - Popula nós raiz
    /// </summary>
    public void ItemConfiguracaoPopuloNosRaiz(int intCodigoItem, TreeView trvItemConfiguracao)
    {
        try
        {
            String strSql = String.Empty;

            strSql = "SELECT ic_codigo, ic_codigo_superior, nome";
            strSql += ", (SELECT count(*) FROM IC WHERE ic_codigo_superior = item.ic_codigo) pai";
            if (txtCodConhecimento.Text.Trim() != string.Empty) strSql += " ,(select distinct(ic_codigo) from ConhecimentoIC where conhecimento_codigo = " + Convert.ToInt32(txtCodConhecimento.Text.Trim()) + " and ic_codigo = item.ic_codigo) selecionado ";
            strSql += " FROM IC item";


            if (intCodigoItem > 0)
            {
                strSql += " WHERE ic_codigo = " + intCodigoItem.ToString();
            }
            else
            {
                strSql += " WHERE ic_codigo_superior is null";
            }
            strSql += " ORDER BY nome";

            System.Data.SqlClient.SqlDataReader objDataReader = ServiceDesk.Banco.ClsBanco.geraDataReader(strSql);

            trvItemConfiguracao.Nodes.Clear();

            ItemConfiguracaoPopulaNos(objDataReader, trvItemConfiguracao.Nodes);

            objDataReader.Dispose();
        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region Item de configuração - Popula no raiz por tipo de item de configuração
    /// <summary>
    /// Método que popula nós raiz do item de configuração por tipo
    /// </summary>
    /// <param name="intCodigoTipoIC">Código do tipo de IC</param>
    /// <param name="objTreeView">Nome da treeview</param>
    public void PopulaNoRaizPorTipoDeIC(int intCodigoTipoIC, TreeView objTreeView)
    {
        try
        {
            String strSql = String.Empty;

            strSql = "SELECT ic_codigo, ic_codigo_superior, nome";
            strSql += " , (SELECT count(*) FROM IC WHERE ic_codigo_superior = item.ic_codigo) pai";
            strSql += " FROM IC item, ICTipo ICT";
            strSql += " where item.ic_tipo_codigo = ICT.ic_tipo_codigo";
            if (intCodigoTipoIC > 0) strSql += " and item.ic_tipo_codigo = " + intCodigoTipoIC + "";
            if (intCodigoTipoIC <= 0) strSql += " and item.ic_tipo_codigo is not null";
            strSql += " ORDER BY nome";

            System.Data.SqlClient.SqlDataReader objDataReader = ServiceDesk.Banco.ClsBanco.geraDataReader(strSql);

            objTreeView.Nodes.Clear();

            ItemConfiguracaoPopulaNos(objDataReader, objTreeView.Nodes);

            objDataReader.Dispose();
        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region Preenche campos
    /// <summary>
    /// Preenche campos com dados do conhecimento
    /// </summary>
    /// <param name="intCodigoConhecimento">Código do identificador do conhecimento</param>
    public void PreencheCampos(string strConhecimentoCodigo)
    {
        try
        {
            string strSql = "SELECT * FROM conhecimento ";
            strSql += " WHERE conhecimento_codigo = " + strConhecimentoCodigo;

            ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
            System.Data.SqlClient.SqlDataReader objDataReader;
            objDataReader = ServiceDesk.Banco.ClsBanco.geraDataReader(strSql);

            if (objDataReader.Read())
            {
                ServiceDesk.Negocio.ClsStatusTabela.geraDropDownList(dlStatusConhecimento, "conhecimento");
                txtNomeConhecimento.Text = objDataReader["nome"].ToString();
                dlStatusConhecimento.SelectedValue = objDataReader["status_codigo"].ToString();
                txtCodConhecimento.Text = objDataReader["conhecimento_codigo"].ToString();
                txtDescricaoConhecimento.Text = objDataReader["texto"].ToString();
                mvAbas.ActiveViewIndex = 0;
                WUCSolucaoFiltro1.PreencheCampo("CONHECIMENTO", Convert.ToInt32(txtCodConhecimento.Text.Trim()));
            }
            objDataReader.Dispose();
        }
        catch (Exception ex)
        {
            imgIcone.ImageUrl = "images/icones/aviso.gif";
            lblMensagem.Text = ex.Message;
            divMensagem.Visible = true;

            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region Pega a prioridade do incidente.
    /// <summary>
    /// Pega a prioridade do incidente.
    /// </summary>
    /// <param name="strTabela">Nome da tabela que representa o processo</param>
    /// <param name="strTabeleIdentificador">Número identificador do registro na tabela</param>
    /// <returns>Retorna a descrição da prioridade</returns>
    private string GetPrioridadeProcesso(string strTabela, string strTabeleIdentificador)
    {
        try
        {
            if (strTabela == string.Empty || strTabeleIdentificador == string.Empty) return string.Empty;

            string strPrioridade = string.Empty;
            ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
            System.Data.SqlClient.SqlDataReader objDataReader;

            if (strTabela == "Incidente")
            {
                objDataReader = ServiceDesk.Negocio.ClsIncidente.getIncidente(strTabeleIdentificador);
                if (objDataReader.Read() && objDataReader["prioridade_codigo"] != null)
                {
                    strPrioridade = objBanco.retornaValorCampo("Prioridade", "descricao", "prioridade_codigo = " + Convert.ToInt32(objDataReader["prioridade_codigo"]) + "");
                }
                objDataReader.Dispose();
            }
            else if (strTabela == "Chamado")
            {
                objDataReader = ServiceDesk.Negocio.ClsChamado.getChamado(strTabeleIdentificador);
                if (objDataReader.Read() && objDataReader["prioridade_codigo"].ToString() != string.Empty)
                {
                    strPrioridade = objBanco.retornaValorCampo("Prioridade", "descricao", "prioridade_codigo = " + Convert.ToInt32(objDataReader["prioridade_codigo"]) + "");
                }
                objDataReader.Dispose();
            }
            objBanco = null;


            if (strPrioridade != string.Empty)
                return strPrioridade;
            else
                return string.Empty;
        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());

            return String.Empty;
        }
    }
    #endregion

    #endregion

    #region Eventos

    #region Page Load
    /// <summary>
    /// Page Load
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                if (txtCodConhecimento.Text.Trim() != string.Empty)
                {
                    WUCSolucaoFiltro1.PreencheCampo("CONHECIMENTO", Convert.ToInt32(txtCodConhecimento.Text.Trim()));
                    WUCSolucaoFiltro1.Filtrar();
                }
            }
        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region Anexo - lkbAnexo_Click
    /// <summary>
    /// Anexo - Aba anexo
    /// Anexo - Alimente grid
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lkbAnexo_Click(object sender, EventArgs e)
    {
        try
        {
            divMensagem.Visible = false;
            PrencheGrid("Anexo");
            mvAbas.ActiveViewIndex = 1;
        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region Perfil - lkbPerfil_Click
    /// <summary>
    /// lkbPerfil_Click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lkbPerfil_Click(object sender, EventArgs e)
    {
        try
        {
            divMensagem.Visible = false;
            if (txtCodConhecimento.Text.Trim() != string.Empty) PrencheGrid("Perfil");
            PreencheDroDown("Perfil");
            mvAbas.ActiveViewIndex = 2;
        }
        catch (Exception ex)
        {
            imgIcone.ImageUrl = "images/icones/aviso.gif";
            lblMensagem.Text = ex.Message;
            divMensagem.Visible = true;

            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region Conhecimento - lkbConhecimento_Click
    /// <summary>
    /// Conhecimento - lkbConhecimento_Click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lkbConhecimento_Click(object sender, EventArgs e)
    {
        try
        {
            divMensagem.Visible = false;
            PopulaNoRaiz(tvTipoIC, null, 0, 0, false, "ICTipo", false);
            tvTipoIC.ExpandAll();

            PopulaNoRaiz(tvIC, null, 0, 0, false, "IC", false);
            tvIC.ExpandAll();

            if (txtCodConhecimento.Text.Trim() != string.Empty)
                ServiceDesk.Negocio.ClsConhecimentoConhecimento.PopulaNós(Convert.ToInt32(txtCodConhecimento.Text.Trim()), tvConhecimento);
            else
                ServiceDesk.Negocio.ClsConhecimentoConhecimento.PopulaNós(0, tvConhecimento);
            mvAbas.ActiveViewIndex = 3;
        }
        catch (Exception ex)
        {
            imgIcone.ImageUrl = "images/icones/aviso.gif";
            lblMensagem.Text = ex.Message;
            divMensagem.Visible = true;

            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region Conhecimento - Evento Salvar
    /// <summary>
    /// Conhecimento - Evento Salvar
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSalvar_Click(object sender, EventArgs e)
    {
        try
        {

            divMensagem.Visible = false;

            if (txtNomeConhecimento.Text == string.Empty)
            {
                lblMensagem.Text = "Por favor, informe o nome.";
                imgIcone.ImageUrl = "images/icones/info.gif";
                divMensagem.Visible = true;
                return;
            }
            else if (dlStatusConhecimento.SelectedValue == string.Empty)
            {
                lblMensagem.Text = "Por favor, informe o status do conhecimento.";
                imgIcone.ImageUrl = "images/icones/info.gif";
                divMensagem.Visible = true;
                return;
            }


            string strMensagem = string.Empty;

            //Verifica o tamanho do nome.
            if (txtNomeConhecimento.Text.Length > 50)
            {
                objConhecimento.Nome.Valor = txtNomeConhecimento.Text.Trim();
                objConhecimento.Nome.Valor = ServiceDesk.Generica.ClsTexto.trocaAspaPorHtml(txtNomeConhecimento.Text.Substring(0, 100));
                txtNomeConhecimento.Text = ServiceDesk.Generica.ClsTexto.trocaAspaPorHtml(txtNomeConhecimento.Text.Substring(0, 100));
            }
            else objConhecimento.Nome.Valor = ServiceDesk.Generica.ClsTexto.trocaAspaPorHtml(txtNomeConhecimento.Text);

            //Verifica o tamanho da descrição.
            if (txtDescricaoConhecimento.Text.Length > 7500)
            {
                objConhecimento.Texto.Valor = txtDescricaoConhecimento.Text.Trim();
                objConhecimento.Texto.Valor = ServiceDesk.Generica.ClsTexto.trocaAspaPorHtml(txtDescricaoConhecimento.Text.Substring(0, 7500));
                txtDescricaoConhecimento.Text = ServiceDesk.Generica.ClsTexto.trocaAspaPorHtml(txtDescricaoConhecimento.Text.Substring(0, 7500));
            }
            else objConhecimento.Texto.Valor = ServiceDesk.Generica.ClsTexto.trocaAspaPorHtml(txtDescricaoConhecimento.Text.Trim());

            if (dlStatusConhecimento.SelectedValue != null) objConhecimento.Status.Valor = dlStatusConhecimento.SelectedValue.ToString();
            else objConhecimento.Status.Valor = string.Empty;

            //Insere novo Conhecimento
            if (txtCodConhecimento.Text.Trim() == string.Empty)
            {
                objConhecimento.Codigo.Valor = objConhecimento.GetMaxId().ToString();
                objConhecimento.insere(out strMensagem);
                lblMensagem.Text = strMensagem;
                imgIcone.ImageUrl = "images/icones/info.gif";
                divMensagem.Visible = true;
                txtCodConhecimento.Text = objConhecimento.Codigo.Valor.Trim();
            }
            //Altera conhecimento
            else
            {
                objConhecimento.Codigo.Valor = txtCodConhecimento.Text.Trim();
                objConhecimento.altera(out strMensagem);
                lblMensagem.Text = strMensagem;
                imgIcone.ImageUrl = "images/icones/info.gif";
                divMensagem.Visible = true;
                txtCodConhecimento.Text = objConhecimento.Codigo.Valor.Trim();
            }
        }
        catch (Exception ex)
        {
            imgIcone.ImageUrl = "images/icones/aviso.gif";
            lblMensagem.Text = ex.Message;
            divMensagem.Visible = true;

            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region Conhecimento - Evento excluir
    /// <summary>
    /// Conhecimento - Evento excluir
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnExcluir_Click(object sender, EventArgs e)
    {
        try
        {
            divMensagem.Visible = false;
            #region Verifica se foi selecionado algum conhecimento para ser excluído.
            if (txtCodConhecimento.Text == string.Empty)
            {
                lblMensagem.Text = "Informe o conhecimento que deseja excluir.";
                imgIcone.ImageUrl = "images/icones/info.gif";
                divMensagem.Visible = true;
                return;
            }
            else
                divMensagem.Visible = false;
            #endregion

            #region Exclui
            string strMensagem = string.Empty;

            objConhecimento.Codigo.Valor = txtCodConhecimento.Text.Trim();
            if (objConhecimento.exclui(out strMensagem) == false)
            {
                lblMensagem.Text = strMensagem;
                imgIcone.ImageUrl = "images/icones/info.gif";
                divMensagem.Visible = true;
            }
            else
                divMensagem.Visible = false;
            #endregion
        }
        catch (Exception ex)
        {
            imgIcone.ImageUrl = "images/icones/aviso.gif";
            lblMensagem.Text = ex.Message;
            divMensagem.Visible = true;

            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region Conhecimento - Novo
    /// <summary>
    /// Conhecimento - Novo - Limpa os objetos da tela para um novo registro
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnNovo_Click(object sender, EventArgs e)
    {
        divMensagem.Visible = false;
        ConhecimentoNovo();
    }
    #endregion

    #region Perfil - Inserir
    /// <summary>
    /// Perfil - Salvar
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSalvarPerfil_Click(object sender, EventArgs e)
    {
        try
        {
            divMensagem.Visible = false;
            #region Verifica se os dados necessários foram informados.
            if (txtCodConhecimento.Text.Trim() == string.Empty)
            {
                imgIcone.ImageUrl = "images/icones/info.gif";
                lblMensagem.Text = "Por favor, selecione o conhecimento para o qual deseja atribuir o perfil.";
                divMensagem.Visible = true;
                return;
            }
            else if (dlPerfil.SelectedValue == null)
            {
                imgIcone.ImageUrl = "images/icones/info.gif";
                lblMensagem.Text = "Por favor, selecione um perfil na listagem.";
                divMensagem.Visible = true;
                return;
            }
            else
                divMensagem.Visible = false;
            #endregion

            #region Insere no banco
            string strMensagem = string.Empty;

            ServiceDesk.Negocio.ClsConhecimentoPerfil objConhecimentoPerfil = new ServiceDesk.Negocio.ClsConhecimentoPerfil();

            objConhecimentoPerfil.CodigoConhecimento.Valor = txtCodConhecimento.Text.Trim();
            objConhecimentoPerfil.CodigoPerfil.Valor = dlPerfil.SelectedValue.ToString();

            if (objConhecimentoPerfil.insere(out strMensagem) == false)
            {
                imgIcone.ImageUrl = "images/icones/info.gif";
                lblMensagem.Text = strMensagem;
                divMensagem.Visible = true;

                ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.INSERT, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, objConhecimentoPerfil.CodigoConhecimento.Valor, String.Empty);
            }
            else
            {
                divMensagem.Visible = false;
                if (txtCodConhecimento.Text.Trim() != string.Empty) ServiceDesk.Negocio.ClsConhecimentoPerfil.geraGridView(gvPerfil, Convert.ToInt32(txtCodConhecimento.Text.Trim()));
            }
            #endregion
        }
        catch (Exception ex)
        {
            imgIcone.ImageUrl = "images/icones/aviso.gif";
            lblMensagem.Text = ex.Message;
            divMensagem.Visible = true;
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());

        }
    }
    #endregion

    #region Anexo - Inserir
    /// <summary>
    /// Anexo - Insere
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSalvarAnexo_Click(object sender, EventArgs e)
    {
        try
        {
            divMensagem.Visible = false;
            #region Verifica se os dados necessários foram informados.
            //if (flDocumento.Value == string.Empty)
            if (!flDocumento.HasFile)
            {
                imgIcone.ImageUrl = "images/icones/info.gif";
                lblMensagem.Text = "Por favor, selecione o arquivo.";
                divMensagem.Visible = true;
                return;
            }
            else if (txtNomeAnexo.Text.Trim() == null)
            {
                imgIcone.ImageUrl = "images/icones/info.gif";
                lblMensagem.Text = "Por favor, informe o nome do anexo.";
                divMensagem.Visible = true;
                return;
            }
            else if (txtCodConhecimento.Text.Trim() == string.Empty)
            {
                imgIcone.ImageUrl = "images/icones/info.gif";
                lblMensagem.Text = "Por favor, selecione o conhecimento.";
                divMensagem.Visible = true;
                return;
            }
            divMensagem.Visible = false;
            #endregion

            #region Transferência
            SServiceDesk.Negocio.ClsAnexo objAnexo = new SServiceDesk.Negocio.ClsAnexo();
            ClsIdentificador objIdentificador = new ClsIdentificador();
            objIdentificador.Tabela.Valor = objAnexo.Atributos.NomeTabela;
            objAnexo.Codigo.Valor = objIdentificador.getProximoValor().ToString();
            objAnexo.Nome.Valor = txtNomeAnexo.Text.Trim();
            objAnexo.DataInclusao.Valor = DateTime.Now.ToString(ClsParametro.DataInclusao);
            objAnexo.PessoaInclusor.Valor = ClsUsuario.getCodigoUsuario().ToString();
            objAnexo.Tabela.Valor = "Conhecimento";
            objAnexo.TabelaIdentificador.Valor = txtCodConhecimento.Text.Trim();
            objAnexo.Caminho.Valor = Server.MapPath(".") + "\\docs";

            string strMensagem = string.Empty;

            string result = ServiceDesk.Generica.ClsArquivo.uploadArquivo(flDocumento, objAnexo.Caminho.Valor);
            if (string.IsNullOrEmpty(result))
            {
                objAnexo.Caminho.Valor += "\\" + ServiceDesk.Generica.ClsArquivo.getNomeArquivo(flDocumento);

                if (objAnexo.insere(out strMensagem) == true)
                {
                    objIdentificador.atualizaValor();
                    txtNomeAnexo.Text = string.Empty;
                }
                else
                {
                    imgIcone.ImageUrl = "images/icones/info.gif";
                    lblMensagem.Text = strMensagem;
                    divMensagem.Visible = true;
                    return;
                }
            }
            else
            {
                imgIcone.ImageUrl = "images/icones/info.gif";
                lblMensagem.Text = result;
                divMensagem.Visible = true;
            }
            #endregion

            PrencheGrid("Anexo");
        }
        catch (Exception ex)
        {
            imgIcone.ImageUrl = "images/icones/aviso.gif";
            lblMensagem.Text = ex.Message;
            divMensagem.Visible = true;

            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());

        }
    }
    #endregion

    #region Item Configuração associado - Salvar
    /// <summary>
    /// Conhecimento com conhecimento - Salva
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSalvarConhecimentoAssociado_Click(object sender, EventArgs e)
    {
        try
        {
            divMensagem.Visible = false;
            #region Verifica se os dados necessários foram informados.
            if (txtCodConhecimento.Text.Trim() == string.Empty)
            {
                imgIcone.ImageUrl = "images/icones/info.gif";
                lblMensagem.Text = "Por favor, selecione o conhecimento.";
                divMensagem.Visible = true;
                return;
            }
            else
                divMensagem.Visible = false;
            #endregion

            string strMensagem = string.Empty;

            if (txtCodConhecimento.Text.Trim() != string.Empty)
            {
                TreeNodeCollection objTreeNodeCollection = tvIC.Nodes;
                for (int i = 0; i < objTreeNodeCollection.Count; i++)
                {
                    VerificaNos(objTreeNodeCollection[i]);
                }
            }

            imgIcone.ImageUrl = "images/icones/info.gif";
            lblMensagem.Text = "Associação realizada com sucesso.";
            divMensagem.Visible = true;
        }
        catch (Exception ex)
        {
            imgIcone.ImageUrl = "images/icones/aviso.gif";
            lblMensagem.Text = ex.Message;
            divMensagem.Visible = true;

            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region Associa conhecimento aos itens de configuração
    /// <summary>
    /// Verifica nós
    /// </summary>
    /// <param name="objTreeNode"></param>
    private void VerificaNos(TreeNode objTreeNode)
    {
        try
        {
            string strMensagem = string.Empty;
            ServiceDesk.Negocio.ClsConhecimentoItemConfiguracao objConhecimentoItemConfiguracao = new ServiceDesk.Negocio.ClsConhecimentoItemConfiguracao();

            if (objTreeNode.Checked)
            {
                objConhecimentoItemConfiguracao.CodigoConhecimento.Valor = txtCodConhecimento.Text.Trim();
                objConhecimentoItemConfiguracao.CodigoItemConfig.Valor = objTreeNode.Value.ToString();
                objConhecimentoItemConfiguracao.insere(out strMensagem);
            }
            else
            {
                objConhecimentoItemConfiguracao.CodigoConhecimento.CampoIdentificador = true;
                objConhecimentoItemConfiguracao.CodigoConhecimento.Valor = txtCodConhecimento.Text.Trim();
                objConhecimentoItemConfiguracao.CodigoItemConfig.CampoIdentificador = true;
                objConhecimentoItemConfiguracao.CodigoItemConfig.Valor = objTreeNode.Value.ToString();
                objConhecimentoItemConfiguracao.exclui(out strMensagem);
            }

            foreach (TreeNode objNode in objTreeNode.ChildNodes)
            {
                VerificaNos(objNode);
            }
            objConhecimentoItemConfiguracao = null;
        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());

        }
    }
    #endregion

    #region Anexo - Row Command
    /// <summary>
    /// Anexo - Row Command
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvAnexo_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            divMensagem.Visible = false;
            if (e.CommandName == "Excluir")
            {
                string strMsg = string.Empty;

                GridViewRow objRow = gvAnexo.Rows[Convert.ToInt32(e.CommandArgument)];

                Label lblCodigoAnexo = (Label)objRow.FindControl("lblCodigoAnexo");
                //Verifica se foi selecionado o anexo.
                if (lblCodigoAnexo.Text.Trim() == string.Empty)
                {
                    this.imgIcone.ImageUrl = "images/icones/aviso.gif";
                    this.lblMensagem.Text = "Por favor, selecione o anexo que deseja excluir.";
                    this.divMensagem.Visible = true;
                    return;
                }

                SServiceDesk.Negocio.ClsAnexo objAnexo = new SServiceDesk.Negocio.ClsAnexo();
                objAnexo.Codigo.Valor = lblCodigoAnexo.Text.Trim();
                objAnexo.exclui();

                PrencheGrid("Anexo");
            }
        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());

        }
    }
    #endregion

    #region Anexo - Row DataBound
    /// <summary>
    /// Anexo - Row Data Bound 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvAnexo_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblCodigoAnexo = (Label)e.Row.FindControl("lblCodigoAnexo");
                Label lblCaminho = (Label)e.Row.FindControl("lblCaminho");
                Label lblArquivo = (Label)e.Row.FindControl("lblArquivo");
                Label lblNome = (Label)e.Row.FindControl("lblNome");

                String strArquivo = String.Empty;

                strArquivo = ServiceDesk.Generica.ClsTexto.getParteFinalPorChave(lblCaminho.Text, "\\");

                lblArquivo.Text = "<a href=\"docs\\" + strArquivo + "\" target=\"_blank\">" + lblNome.Text + "</a>";
            }
        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region Solução - Row Data Bound
    /// <summary>
    /// Solucao - Row Data Bound
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvSolucao_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if ((e.Row.RowState.ToString() == "Normal") || (e.Row.RowState.ToString() == "Alternate"))
            {

                string strFormatoDataSimples = ClsParametro.DataCompletaExibicao;

                Label lblDataCadastroProjeto = (Label)e.Row.FindControl("lblDataCadastroProjeto");

                if (lblDataCadastroProjeto.Text.Trim() != string.Empty)
                {
                    DateTime dataInclusao = Convert.ToDateTime(lblDataCadastroProjeto.Text.Trim());
                    e.Row.Cells[6].Text = dataInclusao.Date.ToString(strFormatoDataSimples);
                }
            }
        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region Perfil - Row Command
    /// <summary>
    /// Perfil - Row Command
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvPerfil_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Excluir")
            {
                GridViewRow objRow = gvPerfil.Rows[Convert.ToInt32(e.CommandArgument)];
                Label lblCodigoConhecimento = (Label)objRow.FindControl("lblCodigoConhecimento");
                Label lblCodigoPerfil = (Label)objRow.FindControl("lblCodigoPerfil");
                Label lblDescricao = (Label)objRow.FindControl("lblDescricao");

                if (lblCodigoPerfil.Text != string.Empty && lblCodigoConhecimento.Text != string.Empty)
                {
                    ServiceDesk.Negocio.ClsConhecimentoPerfil objConhecimentoPerfil = new ServiceDesk.Negocio.ClsConhecimentoPerfil();
                    objConhecimentoPerfil.CodigoConhecimento.CampoIdentificador = true;
                    objConhecimentoPerfil.CodigoConhecimento.Valor = lblCodigoConhecimento.Text.Trim();
                    objConhecimentoPerfil.CodigoPerfil.CampoObrigatorio = true;
                    objConhecimentoPerfil.CodigoPerfil.Valor = lblCodigoPerfil.Text.Trim();
                    string strMensagem;
                    if (objConhecimentoPerfil.exclui(out strMensagem) == false)
                    {
                        this.imgIcone.ImageUrl = "images/icones/info.gif";
                        this.lblMensagem.Text = strMensagem;
                        this.divMensagem.Visible = true;
                        return;
                    }
                    else
                    {
                        if (txtCodConhecimento.Text.Trim() != string.Empty) PrencheGrid("Perfil");
                        divMensagem.Visible = false;
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

    #region Item de configuração - TreeNodePopulate
    /// <summary>
    /// Item de configuração - TreeNodePopulate
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void tvIC_TreeNodePopulate(object sender, TreeNodeEventArgs e)
    {
        try
        {
            PopulaNoRaiz(null, e.Node, 0, Convert.ToInt32(e.Node.Value), false, "IC", true);
            tvIC.ExpandAll();
        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());

        }
    }
    #endregion

    #region Item de configuração - SelectedNodeChanged
    /// <summary>
    /// Item de configuração - SelectedNodeChanged
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void tvIC_SelectedNodeChanged(object sender, EventArgs e)
    {
        try
        {
            ServiceDesk.Negocio.ClsConhecimentoItemConfiguracao.PopulaNós(Convert.ToInt32(tvIC.SelectedValue.ToString()), tvConhecimento);
            tvIC.ExpandAll();
        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region Tipo de item de configuração - TreeNodePopulate
    /// <summary>
    /// Tipo de item de configuração
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void tvTipoIC_TreeNodePopulate(object sender, TreeNodeEventArgs e)
    {
        try
        {
            PopulaNoRaiz(null, e.Node, Convert.ToInt32(e.Node.Value), 0, false, "ICTipo", false);
            tvTipoIC.ExpandAll();
        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region Tipo de item de configuração - SelectedNodeChanged
    /// <summary>
    /// Tipo de item de configuração - SelectedNodeChanged
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void tvTipoIC_SelectedNodeChanged(object sender, EventArgs e)
    {
        try
        {
            PopulaNoRaiz(tvIC, null, Convert.ToInt32(tvTipoIC.SelectedValue.ToString()), 0, true, "ICTipo", false);
            tvIC.ExpandAll();
        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());

        }
    }
    #endregion

    #region Conhecimento - Associa conhecimento ao conhecimento
    /// <summary>
    /// Conhecimento - Associa conhecimento ao conhecimento 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAssociarConhecimento_Click(object sender, EventArgs e)
    {
        try
        {
            divMensagem.Visible = false;

            #region Verifica se os dados necessários foram informados.
            if (txtCodConhecimento.Text.Trim() == string.Empty)
            {
                imgIcone.ImageUrl = "images/icones/info.gif";
                lblMensagem.Text = "Por favor, selecione o conhecimento.";
                divMensagem.Visible = true;
                return;
            }
            else
                divMensagem.Visible = false;
            #endregion

            ServiceDesk.Negocio.ClsConhecimentoConhecimento objConhecimentoConhecimento = new ServiceDesk.Negocio.ClsConhecimentoConhecimento();
            string strMensagem = string.Empty;
            if (txtCodConhecimento.Text.Trim() != string.Empty)
            {
                for (int i = 0; i < this.tvConhecimento.Nodes.Count; i++)
                {
                    if (this.tvConhecimento.Nodes[i].Checked)
                    {
                        objConhecimentoConhecimento.CodigoConhecimentoDestino.Valor = txtCodConhecimento.Text.Trim();
                        objConhecimentoConhecimento.CodigoConhecimentoOrigem.Valor = this.tvConhecimento.Nodes[i].Value.ToString();
                        objConhecimentoConhecimento.insere(out strMensagem);
                    }
                    else //Remove
                    {
                        objConhecimentoConhecimento.CodigoConhecimentoDestino.CampoIdentificador = true;
                        objConhecimentoConhecimento.CodigoConhecimentoDestino.Valor = txtCodConhecimento.Text.Trim();
                        objConhecimentoConhecimento.CodigoConhecimentoOrigem.CampoIdentificador = true;
                        objConhecimentoConhecimento.CodigoConhecimentoOrigem.Valor = this.tvConhecimento.Nodes[i].Value.ToString();
                        objConhecimentoConhecimento.exclui(out strMensagem);
                    }
                }
            }

            objConhecimentoConhecimento = null;
            imgIcone.ImageUrl = "images/icones/info.gif";
            lblMensagem.Text = "Associação realizada com sucesso.";
            divMensagem.Visible = true;
        }
        catch (Exception ex)
        {
            imgIcone.ImageUrl = "images/icones/aviso.gif";
            lblMensagem.Text = ex.Message;
            divMensagem.Visible = true;


            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region Solução - Nova solução
    /// <summary>
    /// Solução - Nova solução
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSolucaoNovo_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtCodConhecimento.Text.Trim() != string.Empty)
            {
                Response.Redirect("solucaoprojeto.aspx?tabela=Conhecimento&identificador=" + txtCodConhecimento.Text.Trim(), false);
            }
        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());

        }
    }
    #endregion

    #region lkbProjeto_Click
    /// <summary>
    /// lkbProjeto_Click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lkbProjeto_Click(object sender, EventArgs e)
    {
        try
        {
            divMensagem.Visible = false;
            mvAbas.ActiveViewIndex = 0;
            if (txtCodConhecimento.Text.Trim() != string.Empty)
            {
                WUCSolucaoFiltro1.PreencheCampo("Conhecimento", Convert.ToInt32(txtCodConhecimento.Text.Trim()));
                WUCSolucaoFiltro1.Filtrar();
            }



        }
        catch (Exception ex)
        {
            imgIcone.ImageUrl = "images/icones/aviso.gif";
            lblMensagem.Text = ex.Message;
            divMensagem.Visible = true;

            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #endregion
}