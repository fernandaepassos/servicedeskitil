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

public partial class teste : System.Web.UI.UserControl
{
    #region Eventos

    #region Page Load
    /// <summary>
    /// Page load
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //Verificar acessibilidade

            /*int intCodigoFuncao = 5;
            if (!ServiceDesk.Negocio.ClsUsuario.verificaAcessoUsuarioFuncao(ClsUsuario.getCodigoUsuario(ClsUsuario.getCodigoRede()), intCodigoFuncao.ToString(), ClsUsuario.getTipoUsuarioCodigo(ClsUsuario.getCodigoRede()).ToString()))
            {
              Response.Redirect("AcessoNegado.aspx", false);
              return;

            }*/

            if (!IsPostBack)
            {
                TipoItemConfigPopulaNoRaiz(0, tvTipoIC, null);
                tvTipoIC.ExpandAll();
                hplNovaConhecimento.NavigateUrl = "Javascript:VisualizaConhecimento('" + string.Empty + "');"; ;
            }
            else
                Page.MaintainScrollPositionOnPostBack = true;

        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());

        }

    }
    #endregion

    #region Item de configura��o - TreeNodePopulate
    /// <summary>
    /// Item de configura��o - TreeNodePopulate
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void tvIC_TreeNodePopulate(object sender, TreeNodeEventArgs e)
    {
        try
        {
            ItemConfiguracaoPopulaSubNivel(Convert.ToInt32(e.Node.Value), e.Node);
            tvIC.ExpandAll();

        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());

        }
    }
    #endregion

    #region Item de configura��o - SelectedNodeChanged
    /// <summary>
    /// Item de configura��o - SelectedNodeChanged
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void tvIC_SelectedNodeChanged(object sender, EventArgs e)
    {
        try
        {
            tvConhecimento.Nodes.Clear();
            TreeNode objNode = new TreeNode();
            objNode.Text = "Itens da KB relacionados";
            objNode.Value = "raiz";
            tvConhecimento.Nodes.Add(objNode);
            TreeNode objNodeFilho1 = new TreeNode();
            objNodeFilho1.Value = "CONHECIMENTO";
            objNodeFilho1.Text = "Conhecimentos (" + ServiceDesk.Negocio.ClsConhecimento.ContadorRelacionadosKB(objNodeFilho1.Value, tvIC.SelectedValue) + ")";
            objNode.ChildNodes.Add(objNodeFilho1);
            TreeNode objNodeFilho2 = new TreeNode();
            objNodeFilho2.Value = "CHAMADO";
            objNodeFilho2.Text = "Chamados (" + ServiceDesk.Negocio.ClsConhecimento.ContadorRelacionadosKB(objNodeFilho2.Value, tvIC.SelectedValue) + ")";
            objNode.ChildNodes.Add(objNodeFilho2);
            TreeNode objNodeFilho3 = new TreeNode();
            objNodeFilho3.Value = "INCIDENTE";
            objNodeFilho3.Text = "Incidentes (" + ServiceDesk.Negocio.ClsConhecimento.ContadorRelacionadosKB(objNodeFilho3.Value, tvIC.SelectedValue) + ")";
            objNode.ChildNodes.Add(objNodeFilho3);
            TreeNode objNodeFilho4 = new TreeNode();
            objNodeFilho4.Value = "REQUISICAOSERVICO";
            objNodeFilho4.Text = "Requisi��o de Servi�o(" + ServiceDesk.Negocio.ClsConhecimento.ContadorRelacionadosKB(objNodeFilho4.Value, tvIC.SelectedValue) + ")";
            objNode.ChildNodes.Add(objNodeFilho4);

            tvConhecimento.ExpandAll();

            tvIC.SelectedNodeStyle.BackColor = System.Drawing.Color.LightGray;

        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region Item configura��o - TreeNodePopulate
    /// <summary>
    /// Item de configura��o - TreeNodePopulate
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void tvItemConfigConhecimento_TreeNodePopulate(object sender, TreeNodeEventArgs e)
    {
        try
        {
            ItemConfiguracaoPopulaSubNivel(Convert.ToInt32(e.Node.Value), e.Node);

        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region Tipo de item de configura��o - TreeNodePopulate
    /// <summary>
    /// Tipo de item de configura��o
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void tvTipoIC_TreeNodePopulate(object sender, TreeNodeEventArgs e)
    {
        try
        {
            TipoItemConfigPopulaNoRaiz(Convert.ToInt32(e.Node.Value), null, e.Node);
            tvTipoIC.ExpandAll();

        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region Tipo de item de configura��o - SelectedNodeChanged
    /// <summary>
    /// Tipo de item de configura��o - SelectedNodeChanged
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void tvTipoIC_SelectedNodeChanged(object sender, EventArgs e)
    {
        try
        {
            if (tvTipoIC.SelectedValue.ToString() == "0") return;
            PopulaNoRaizPorTipoDeIC(Convert.ToInt32(tvTipoIC.SelectedValue.ToString()), tvIC);
            tvTipoIC.SelectedNodeStyle.BackColor = System.Drawing.Color.LightGray;
            tvIC.ExpandAll();
            tvConhecimento.Nodes.Clear();

        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region Treeview Conhecimento - SelectedNodeChanged
    /// <summary>
    /// Treeview Conhecimento - SelectedNodeChanged
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void tvConhecimento_SelectedNodeChanged(object sender, EventArgs e)
    {
        try
        {
            GridView objGridView = new GridView();

            if (tvConhecimento.SelectedValue != null)
            {
                tvConhecimento.SelectedNodeStyle.BackColor = System.Drawing.Color.LightGray;

                switch (tvConhecimento.SelectedValue.ToUpper().Trim())
                {
                    case "CONHECIMENTO":
                        {
                            ServiceDesk.Negocio.ClsConhecimento.geraGridView(gvConhecimento, tvConhecimento.SelectedValue.Trim().ToUpper(), tvIC.SelectedValue);
                            pnlGridConhecimento.Visible = true;
                            pnlGridChamado.Visible = false;
                            pnlGridIncidente.Visible = false;
                            pnlGridSolicitacaoServico.Visible = false;
                            break;
                        }

                    case "CHAMADO":
                        {
                            ServiceDesk.Negocio.ClsConhecimento.geraGridView(gvChamado, tvConhecimento.SelectedValue.Trim().ToUpper(), tvIC.SelectedValue);
                            pnlGridConhecimento.Visible = false;
                            pnlGridChamado.Visible = true;
                            pnlGridIncidente.Visible = false;
                            pnlGridSolicitacaoServico.Visible = false;
                            break;
                        }

                    case "INCIDENTE":
                        {
                            ServiceDesk.Negocio.ClsConhecimento.geraGridView(gvIncidente, tvConhecimento.SelectedValue.Trim().ToUpper(), tvIC.SelectedValue);
                            pnlGridConhecimento.Visible = false;
                            pnlGridChamado.Visible = false;
                            pnlGridIncidente.Visible = true;
                            pnlGridSolicitacaoServico.Visible = false;
                            break;
                        }
                    case "REQUISICAOSERVICO":
                        {
                            ServiceDesk.Negocio.ClsConhecimento.geraGridView(gvSolicitacaoServico, tvConhecimento.SelectedValue.Trim().ToUpper(), tvIC.SelectedValue);
                            pnlGridConhecimento.Visible = false;
                            pnlGridChamado.Visible = false;
                            pnlGridIncidente.Visible = false;
                            pnlGridSolicitacaoServico.Visible = true;
                            break;
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

    #region Novo Conhecimento
    /// <summary>
    /// Novo Conhecimento
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnNovoConhecimento_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("Conhecimento.aspx", false);
        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #endregion

    #region M�todos
    #region Tipo de item de configura��o - Popula N� Raiz
    /// <summary>
    /// M�todo que popula os n�s que n�o possuem pai
    /// </summary>
    /// <param name="tvTreeView">Nome da treeview</param> 
    /// <param name="intCodigoTipo">C�digo do tipo</param>
    public void TipoItemConfigPopulaNoRaiz(int intCodigoTipo, TreeView tvTreeView, TreeNode objTreeNode)
    {
        try
        {
            String strSql = String.Empty;

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
                if (objDataReader != null) { tvTreeView.Nodes.Add(new TreeNode("Tipo - Item de Configura��o", "0")); }
                TipoItemConfiguracaoPopulaNos(objDataReader, tvTreeView.Nodes[0].ChildNodes);
            }
            else if (objTreeNode != null)
            {
                TipoItemConfiguracaoPopulaNos(objDataReader, objTreeNode.ChildNodes);
            }

            objDataReader.Dispose();

        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }

    }
    #endregion

    #region Tipo de item de configura��o - Popula n�s
    /// <summary>
    /// Tipo de item de configura��o - Popula n�s
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
            objDataReader.Dispose();

        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region Item de configura��o - Popula Sub N�vel
    /// <summary>
    /// M�todo que popula o sub n�vel do item de configura��o.
    /// </summary>
    /// <param name="intCodigoPai">C�digo do item pai</param>
    /// <param name="objTreeNode">Nome da tree node</param>
    public void ItemConfiguracaoPopulaSubNivel(int intCodigoPai, TreeNode objTreeNodePai)
    {
        try
        {
            String strSql = String.Empty;

            strSql = "SELECT ic_codigo, ic_codigo_superior, nome";
            strSql += ", (SELECT count(*) FROM IC WHERE ic_codigo_superior = item.ic_codigo) pai";
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

    #region Item configura��o - Popula n�s
    /// <summary>
    /// Item de configura��o - Popula N�s
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

                if (Convert.ToInt32(objDataReader["pai"]) > 0)
                {
                    objTreeNode.PopulateOnDemand = true;
                }
                objTreeNodeCollection.Add(objTreeNode);
            }
            objDataReader.Dispose();

        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region Item de configura��o - Popula n�s raiz
    /// <summary>
    /// Item de configura��o - Popula n�s raiz
    /// </summary>
    public void ItemConfiguracaoPopuloNosRaiz(int intCodigoItem, TreeView trvItemConfiguracao)
    {
        try
        {
            String strSql = String.Empty;

            strSql = "SELECT ic_codigo, ic_codigo_superior, nome";
            strSql += ", (SELECT count(*) FROM IC WHERE ic_codigo_superior = item.ic_codigo) pai";
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

    #region Item de configura��o - Popula no raiz por tipo de item de configura��o
    /// <summary>
    /// M�todo que popula n�s raiz do item de configura��o por tipo
    /// </summary>
    /// <param name="intCodigoTipoIC">C�digo do tipo de IC</param>
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

            if (intCodigoTipoIC > 0)
            {
                strSql += " AND item.ic_tipo_codigo = " + intCodigoTipoIC.ToString();
                strSql += " AND ic_codigo_superior IS null";
            }
            else
                strSql += " and item.ic_tipo_codigo is not null";

            strSql += " ORDER BY nome";

            System.Data.SqlClient.SqlDataReader objDataReader = ServiceDesk.Banco.ClsBanco.geraDataReader(strSql);

            objTreeView.Nodes.Clear();

            if (objDataReader.HasRows == true)
            {
                objTreeView.Nodes.Add(new TreeNode("Item de Configura��o", "0"));
                ItemConfiguracaoPopulaNos(objDataReader, objTreeView.Nodes[0].ChildNodes);
            }

            objDataReader.Dispose();

        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #endregion


}