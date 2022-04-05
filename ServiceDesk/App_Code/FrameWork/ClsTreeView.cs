using System;
using System.Web.UI.WebControls;

namespace ServiceDesk.FrameWork
{
    /// <summary>
    /// Classe ClsTreeView.
    /// </summary>
    public class ClsTreeView
    {
        public int intContador = 0;

        #region Propriedades
        #endregion

        #region Construtor

        public ClsTreeView()
        {

        }
        #endregion

        #region Metodos

        public void gravaItensSelecionados(System.Web.UI.WebControls.TreeView trv, string strTabelaRelacionada, string strCodigoIdentificador)
        {
          try
          {
            TreeNodeCollection objTreeNodeCollection = trv.Nodes;
            for (int intI = 0; intI < objTreeNodeCollection.Count; intI++)
            {
              VerificaNos(objTreeNodeCollection[intI], strTabelaRelacionada, strCodigoIdentificador);
            }
          }
          catch (Exception ex)
          {
            throw ex;          
          }          
        }


      #region metodo VerificaNos

      private void VerificaNos(TreeNode objTreeNode, string strTabelaRelacionada, string strCodigoIdentificador)
      {
        try
        {
          if (objTreeNode.Checked)
          {            
            ServiceDesk.Negocio.ClsItemConfiguracao.AdicionaItemConfiguracao(strCodigoIdentificador.Trim(), strTabelaRelacionada, objTreeNode.Value.ToString());
          }
          foreach (TreeNode objNode in objTreeNode.ChildNodes)
          {
              VerificaNos(objNode, strTabelaRelacionada, strCodigoIdentificador);
          }
        }
        catch (Exception ex)
        {
          throw ex;
        }

      }
      #endregion



        public TreeNode insereNoArvore(string strTexto, string strValor, TreeNodeCollection objTreeNodePai)
        {
          try
          {
              TreeNode objTreeNode = new TreeNode();
              objTreeNode.Text = strTexto;
              objTreeNode.Value = strValor;
              objTreeNodePai.Add(objTreeNode);
              return objTreeNode;
          }
          catch (Exception ex)
          {
              throw ex;
          }
        }


      /// <summary>
      /// Retorna a quantidade de itens selecionados na árvore (checked = true)
      /// </summary>
      /// <param name="trv">TreeView</param>      
      public int quantidadeItensSelecionados(System.Web.UI.WebControls.TreeView trv)
      {
        try
        {
          intContador = 0;

          TreeNodeCollection objTreeNodeCollection = trv.Nodes;
          for (int intI = 0; intI < objTreeNodeCollection.Count; intI++)
          {
            VerificaNoselecionados(objTreeNodeCollection[intI]);
          }
          objTreeNodeCollection = null;
         
          return intContador;          
        }
        catch (Exception ex)
        {
          throw ex;
        }
      }


      #region metodo VerificaNoselecionados

      private void VerificaNoselecionados(TreeNode objTreeNode)
      {
        try
        {
          if (objTreeNode.Checked)
          {
            intContador++;
          }
          foreach (TreeNode objNode in objTreeNode.ChildNodes)
          {
            VerificaNoselecionados(objNode);
          }
        }
        catch (Exception ex)
        {
          throw ex;
        }

      }
      #endregion
        #endregion
    }
}