/*
- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
  � Classe para manipula��o dos registro da tabela Fun��oAplicacao
  
  	Data: 26/01/2006
  	Autor: Fernanda Passos
  	Descri��o: Esta classe apresenta v�rias funcionalidades que permite manipular os dados
    da tabela Fun��oAplica��o.
  
  � Altera��es
  	Data:
  	Autor:
  	Descri��o: 
- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
*/

using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace SServiceDesk.Negocio
{
  /// <summary>
  /// Classe Fun��o Aplica��o
  /// </summary>
  public class ClsFuncaoAplicacao
  {
    #region Declara��es
    //Colecao de atributos de FuncaoAplicacao
    private ServiceDesk.Banco.ClsAtributos objAtributos = new ServiceDesk.Banco.ClsAtributos();

    //Atributos de Funcao
    private ServiceDesk.Banco.ClsAtributo objCodigo = new ServiceDesk.Banco.ClsAtributo();
    private ServiceDesk.Banco.ClsAtributo objAplicacaoCodigo = new ServiceDesk.Banco.ClsAtributo();
    private ServiceDesk.Banco.ClsAtributo objDescricao = new ServiceDesk.Banco.ClsAtributo();
    private ServiceDesk.Banco.ClsAtributo objFuncaoSuperior = new ServiceDesk.Banco.ClsAtributo();
    private ServiceDesk.Banco.ClsAtributo objChave = new ServiceDesk.Banco.ClsAtributo();
    private ServiceDesk.Banco.ClsAtributo objUrl = new ServiceDesk.Banco.ClsAtributo();
    #endregion

    #region Propriedades
    public ServiceDesk.Banco.ClsAtributos Atributos
    {
      get
      {
        return this.objAtributos;
      }
    }

    public ServiceDesk.Banco.ClsAtributo Codigo
    {
      get
      {
        return objCodigo;
      }
    }

    public ServiceDesk.Banco.ClsAtributo AplicacaoCodigo
    {
      get
      {
        return objAplicacaoCodigo;
      }
    }

    public ServiceDesk.Banco.ClsAtributo Descricao
    {
      get
      {
        return objDescricao;
      }
    }

    public ServiceDesk.Banco.ClsAtributo FuncaoSuperior
    {
      get
      {
        return objFuncaoSuperior;
      }
    }

    public ServiceDesk.Banco.ClsAtributo Chave
    {
      get
      {
        return objChave;
      }
    }

    public ServiceDesk.Banco.ClsAtributo Url
    {
      get
      {
        return objUrl;
      }
    }

    #endregion

    #region Construtor
    public ClsFuncaoAplicacao()
    {
      this.alimentaColecaoCampos();
    }
    #endregion

    #region M�todos
    #region metodo alimentaColecaoCampos
    /// <summary>
    /// Adiciona todos os atributos de uma FuncaoAplicacao, a cole��o de atributos.
    /// </summary>
    private void alimentaColecaoCampos()
    {
      objAtributos.DescricaoTabela = "FuncaoAplicacao";
      objAtributos.NomeTabela = "FuncaoAplicacao";

      objCodigo.Campo = "funcao_codigo";
      objCodigo.Descricao = "C�digo";
      objCodigo.CampoIdentificador = true;
      objCodigo.CampoObrigatorio = true;
      objCodigo.Tipo = System.Data.DbType.Int32;
      objAtributos.Add(objCodigo);

      objAplicacaoCodigo.Campo = "aplicacao_codigo";
      objAplicacaoCodigo.Descricao = "C�digo da Aplica��o";
      objAplicacaoCodigo.CampoObrigatorio = true;
      objAplicacaoCodigo.Tipo = System.Data.DbType.Int32;
      objAtributos.Add(objAplicacaoCodigo);

      objDescricao.Campo = "descricao";
      objDescricao.Descricao = "descricao";
      objDescricao.CampoObrigatorio = true;
      objDescricao.Tipo = System.Data.DbType.String;
      objDescricao.Tamanho = 255;
      objAtributos.Add(objDescricao);

      objFuncaoSuperior.Campo = "funcao_superior_codigo";
      objFuncaoSuperior.Descricao = "C�digo da Fun��o Superior";
      objFuncaoSuperior.CampoObrigatorio = false;
      objFuncaoSuperior.Tipo = System.Data.DbType.Int32;
      objAtributos.Add(objFuncaoSuperior);

      objChave.Campo = "chave";
      objChave.Descricao = "Chave";
      objChave.CampoObrigatorio = false;
      objChave.Tipo = System.Data.DbType.String;
      objChave.Tamanho = 255;
      objAtributos.Add(objChave);

      objUrl.Campo = "url";
      objUrl.Descricao = "Url";
      objUrl.CampoObrigatorio = false;
      objUrl.Tipo = System.Data.DbType.String;
      objUrl.Tamanho = 255;
      objAtributos.Add(objUrl);
    }
    #endregion

    #region metodo geraDropDownList
    /// <summary>
    /// Gera um novo DropDownList de acordo com a cole��o de atributos.
    /// </summary>
    /// <param name="objDropDownList">DropDownList</param>
    public static void geraDropDownList(System.Web.UI.WebControls.DropDownList objDropDownList)
    {
      ClsFuncaoAplicacao objFuncaoAplicacao = new ClsFuncaoAplicacao();
      objDropDownList.DataTextField = objFuncaoAplicacao.objDescricao.Campo;
      objDropDownList.DataValueField = objFuncaoAplicacao.objCodigo.Campo;
      ServiceDesk.Controle.ClsDropDownList.geraDropDownList(objDropDownList, objFuncaoAplicacao.objAtributos);
      objFuncaoAplicacao = null;
    }
    #endregion

    #region metodo geraDropDownListExcecao
    /// <summary>
    /// Gera um novo DropDownList de acordo com a cole��o de atributos exceto a funcao passada no parametro.
    /// </summary>
    /// <param name="objDropDownList">DropDownList</param>
    public static void geraDropDownListExcecao(System.Web.UI.WebControls.DropDownList objDropDownList, int intCodigoFuncaoAplicacao)
    {

      string strSql = string.Empty;
      strSql = "SELECT funcao_codigo, descricao ";
      strSql = strSql + " FROM funcaoaplicacao";
      strSql = strSql + " WHERE funcao_codigo <> 0" + intCodigoFuncaoAplicacao.ToString();

      ClsFuncaoAplicacao objFuncaoAplicacao = new ClsFuncaoAplicacao();
      objDropDownList.DataTextField = objFuncaoAplicacao.objDescricao.Campo;
      objDropDownList.DataValueField = objFuncaoAplicacao.objCodigo.Campo;
      objDropDownList.DataSource = ServiceDesk.Banco.ClsBanco.geraDataReader(strSql);
      objDropDownList.DataBind();

      objFuncaoAplicacao = null;

    }
    #endregion

    #region metodo geraDataGrid
    /// <summary>
    /// Gera um novo DataGrid de acordo com a cole��o de atributos.
    /// </summary>
    /// <param name="objDataGrid">DataGrid</param>
    public static void geraDataGrid(System.Web.UI.WebControls.DataGrid objDataGrid)
    {
      objDataGrid.AutoGenerateColumns = false;
      ClsFuncaoAplicacao objFuncaoAplicacao = new ClsFuncaoAplicacao();
      ServiceDesk.Controle.ClsDataGrid.geraDataGrid(objDataGrid, objFuncaoAplicacao.objAtributos);
      objFuncaoAplicacao = null;
    }
    #endregion

    #region metodo geraGridView
    /// <summary>
    /// Gera uma nova geraGridView de acordo com a cole��o de atributos.
    /// </summary>
    /// <param name="objGridView">geraGridView</param>
    public static void geraGridView(System.Web.UI.WebControls.GridView objGridView)
    {
      String strSql = String.Empty;
      objGridView.AutoGenerateColumns = false;
      ClsFuncaoAplicacao objFuncaoAplicacao = new ClsFuncaoAplicacao();
      //ServiceDesk.Controle.ClsGridView.geraGridView(objGridView, objFuncaoAplicacao.objAtributos);
      ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
      strSql = objBanco.montaQuery(objFuncaoAplicacao.objAtributos, false);
      strSql += " ORDER BY descricao";
      System.Data.DataSet objDataSet = ServiceDesk.Banco.ClsBanco.geraDataSet(strSql);
      objGridView.DataSource = objDataSet;
      objGridView.DataBind();
      objDataSet.Dispose();
      objDataSet = null;
      objBanco = null;
      objFuncaoAplicacao = null;
    }
    #endregion

    #region metodo geraGridView
    /// <summary>
    /// Gera uma nova geraGridView de acordo com a cole��o de atributos.
    /// </summary>
    /// <param name="objGridView">geraGridView</param>
    /// <param name="bolCondicao">Condi��o para verificar se ser� utilizado a cl�usula Where</param>
    public static void geraGridView(System.Web.UI.WebControls.GridView objGridView, ClsFuncaoAplicacao objFuncaoAplicacao, bool bolCondicao)
    {
      String strSql = String.Empty;
      objGridView.AutoGenerateColumns = false;
      //ServiceDesk.Controle.ClsGridView.geraGridView(objGridView, objFuncaoAplicacao.objAtributos, bolCondicao);
      ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
      strSql = objBanco.montaQuery(objFuncaoAplicacao.objAtributos, bolCondicao);
      strSql += " ORDER BY descricao";
      System.Data.DataSet objDataSet = ServiceDesk.Banco.ClsBanco.geraDataSet(strSql);
      objGridView.DataSource = objDataSet;
      objGridView.DataBind();
      objDataSet.Dispose();
      objDataSet = null;
      objBanco = null;
    }
    #endregion

    #region metodo insere
    /// <summary>
    /// M�todo que insere.
    /// </summary>
    /// <returns>Retorna true ou false. Se o registro foi inserido ou n�o.</returns>
    public bool insere(out String strMensagem)
    {
      strMensagem = String.Empty;
      bool bolRetorno = false;

      if (this.objDescricao.Valor.Trim() == String.Empty)
      {
        strMensagem = "Favor informar a descri��o da Fun��o.<br>";
      }

      if (this.objAplicacaoCodigo.Valor.Trim() == String.Empty)
      {
        strMensagem = strMensagem + "Favor informar a Aplica��o.<br>";
      }

      if (strMensagem == String.Empty)
      {
        ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
        if (objBanco.insereColecao(this.objAtributos))
        {
          strMensagem = "Fun��o inserida com sucesso.";
          bolRetorno = true;
        }
        objBanco = null;
      }

      return bolRetorno;
    }
    #endregion

    #region metodo altera
    /// <summary>
    /// M�todo que altera.
    /// </summary>
    /// <returns>Retorna true ou false. Se o registro foi alterado ou n�o.</returns>
    public bool altera(out String strMensagem)
    {
      strMensagem = String.Empty;
      bool bolRetorno = false;

      if (this.objDescricao.Valor.Trim() == String.Empty)
      {
        strMensagem = "Favor informar a descri��o da Fun��o.<br>";
      }

      if (this.objAplicacaoCodigo.Valor.Trim() == String.Empty)
      {
        strMensagem = strMensagem + "Favor informar a Aplica��o.<br>";
      }

      if (this.objCodigo.Valor == this.objFuncaoSuperior.Valor)
      {
        strMensagem = strMensagem + "N�o � permitido essa fun��o ser superior a ela mesma.<br>";
      }

      if (strMensagem == String.Empty)
      {
        ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
        if (objBanco.alteraColecao(this.objAtributos))
        {
          strMensagem = "Fun��o atualizada com sucesso.";
          bolRetorno = true;
        }
        objBanco = null;
      }

      return bolRetorno;
    }
    #endregion

    #region metodo exclui
    /// <summary>
    /// M�todo que exclui.
    /// </summary>
    /// <returns>Retorna true ou false. Se o registro foi excluido ou n�o.</returns>
    public bool exclui()
    {
      ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
      if (objBanco.apagaColecao(this.objAtributos))
      {
        objBanco = null;
        return true;
      }
      else
      {
        objBanco = null;
        return false;
      }
    }
    #endregion

    #region alimentaColecao
    /// <summary>
    /// Alimenta a cole��o de atributos de uma FuncaoAplicacao
    /// </summary>
    /// <param name="intCodigo">C�digo da funcao a ser alimentada</param>
    public void alimentaFuncaoAplicacao(int intCodigo)
    {
      this.objCodigo.Valor = intCodigo.ToString();
      ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
      objBanco.alimentaColecao(this.objAtributos);
      objBanco = null;
    }
    #endregion

    #region atualizaChaveFuncao
    /// <summary>
    /// Fun��o que recupera a chave do pai e chama a funcao que atualiza a chave do(s) filho(s).
    /// </summary>
    public void atualizaChaveFuncao()
    {
      String strChavePai = String.Empty;

      if (this.objFuncaoSuperior.Valor != String.Empty)
      {
        ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
        strChavePai = objBanco.retornaValorCampo("FuncaoAplicacao", "chave", "funcao_codigo = '" + this.objFuncaoSuperior.Valor + "'");
      }

      atualizaChave(Convert.ToInt32(this.Codigo.Valor), strChavePai);
    }

    #endregion

    #region atualizaChave
    /// <summary>
    /// <para>Fun��o que atualiza as chaves dos subitens para um determinado item.</para>
    /// <para>Data: 12/05/2005.</para>
    /// <para>Autor: Sylvio Neto (Adapta��o de rotina existente no projetos).</para>
    /// </summary>
    /// <param name="intCodigo"><para>(int) C�digo identificador do item (funcao).</para></param>
    /// <param name="strChavePai"><para>(string) Chave completa do item pai.</para></param>

    private static void atualizaChave(int intCodigo, string strChavePai)
    {
      //Atualizando a chave do item
      string strSql = "UPDATE FuncaoAplicacao SET ";
      if (strChavePai != string.Empty)
      {
        strSql = strSql + " chave = '" + strChavePai + "," + intCodigo.ToString() + "'";
      }
      else
      {
        strSql = strSql + " chave ='" + intCodigo.ToString() + "'";
      }
      strSql = strSql + " WHERE funcao_codigo = " + intCodigo.ToString();


      try
      {
        ServiceDesk.Banco.ClsBanco banco = new ServiceDesk.Banco.ClsBanco();
        banco.executaSQL(strSql);

      }
      catch (Exception ex)
      {
        throw ex;
      }

      if (strChavePai == string.Empty)
      {
        strChavePai = intCodigo.ToString();
      }
      else
      {
        strChavePai = strChavePai + "," + intCodigo;
      }

      //Verificando se o item possui filhos
      strSql = "SELECT funcao_codigo FROM FuncaoAplicacao";
      strSql = strSql + " WHERE funcao_superior_codigo = 0" + intCodigo.ToString();
      System.Data.SqlClient.SqlDataReader objReaderAtualizaChave = ServiceDesk.Banco.ClsBanco.geraDataReader(strSql); //SqlHelper.ExecuteReader(strConn, CommandType.Text, strSql);
      while (objReaderAtualizaChave.Read())
      {
        atualizaChave(Convert.ToInt32(objReaderAtualizaChave["funcao_codigo"]), strChavePai);
      }
      objReaderAtualizaChave.Close();

    }
    #endregion

    #region Popula N� Raiz Por C�digo do Perfil
    /// <summary>
    /// Popola n�s raiz, os que n�o possuem pai.
    /// </summary>
    /// <param name="intCodigoPerfil">C�digo do perfil</param> 
    /// <param name="objTreeView">Nome da TreeView</param> 
    public static void PopulaNoRaizPorPerfil(int intCodigoPerfil, TreeView objTreeView)
    {
        try
        {
            if (intCodigoPerfil <= 0) return;

            String strSql = String.Empty;

            strSql = " SELECT FA.funcao_codigo, FA.funcao_superior_codigo, descricao";
            strSql += " , (SELECT count(*) FROM FuncaoAplicacao WHERE funcao_superior_codigo = FA.funcao_codigo) pai";
            strSql += " FROM FuncaoAplicacao FA, DireitoPerfil DP, Perfil P";
            strSql += " where FA.funcao_codigo = DP.funcao_codigo";
            strSql += " and DP.perfil_codigo = P.perfil_codigo";
            strSql += " and P.perfil_codigo = "+ intCodigoPerfil +"";

            System.Data.SqlClient.SqlDataReader objDataReader = ServiceDesk.Banco.ClsBanco.geraDataReader(strSql);

            objTreeView.Nodes.Clear();

            PopulaNosNos(objDataReader, objTreeView.Nodes,objTreeView);

            objDataReader.Dispose();
            objDataReader = null;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region Popula N� Raiz
        /// <summary>
        /// Popola n�s raiz, os que n�o possuem pai.
        /// </summary>
        /// <param name="intCodigoItem">C�digo do item</param> 
        /// <param name="objTreeView">Nome da TreeView</param> 
        public static void PopulaNoRaiz(int intCodigoItem, TreeView objTreeView)
        {
       try
        {
            String strSql = String.Empty;

            strSql = "SELECT funcao_codigo, funcao_superior_codigo, descricao";
            strSql += " , (SELECT count(*) FROM FuncaoAplicacao WHERE funcao_superior_codigo = FA.funcao_codigo) pai";
            strSql += " FROM FuncaoAplicacao FA";

            if (intCodigoItem > 0)
            {
                strSql += " WHERE funcao_codigo = " + intCodigoItem +"" ;
            }
            strSql += " ORDER BY descricao";

            System.Data.SqlClient.SqlDataReader objDataReader = ServiceDesk.Banco.ClsBanco.geraDataReader(strSql);

            objTreeView.Nodes.Clear();

            PopulaNosNos(objDataReader, objTreeView.Nodes, objTreeView);

            objDataReader.Dispose();
            objDataReader = null;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region Popula N�s
    /// <summary>
    /// Popula N�s
    /// </summary>
    /// <param name="objDataReader">Data Reader com dados que ser�o apresentados no treeview</param> 
    /// <param name="objTreeNodeCollection">Nome da Treeview</param> 
      public static void PopulaNosNos(System.Data.SqlClient.SqlDataReader objDataReader, TreeNodeCollection objTreeNodeCollection, TreeView trvTreeView)
    {
        try
        {
            while (objDataReader.Read())
            {
                TreeNode objTreeNode = new TreeNode();
                objTreeNode.Text = objDataReader["descricao"].ToString();
                objTreeNode.Value = objDataReader["funcao_codigo"].ToString().Trim();
                objTreeNode.NavigateUrl = "";
                //if (Convert.ToInt32(objDataReader["pai"]) > 0)
                //{
                //    objTreeNode.PopulateOnDemand = true;
                //}
                objTreeNodeCollection.Add(objTreeNode);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    #endregion

    #region Popula N� Raiz
    /// <summary>
    /// Popula N� Raiz
    /// </summary>
    /// <param name="intCodigoPai" >N�mero inteiro do c�digo do pai do item.</param>
    /// <param name="objTreeNode"></param> 
    /// <param name="objTreeView">Nome da treeview</param> 
      public static void PopulaNos(int intCodigoPai, TreeView objTreeView, TreeNode objTreeNode, bool bolIsNoRaiz)
    {
        try
        {
            String strSql = String.Empty;

            strSql = "select FA.funcao_codigo, FA.funcao_superior_codigo, FA.descricao, (select count(*) from FuncaoAplicacao where funcao_superior_codigo = FA.funcao_codigo) qtdPai";
            strSql += " from FuncaoAplicacao FA";

            if (intCodigoPai > 0)
            {
                strSql += " where funcao_superior_codigo = " + intCodigoPai + "";
            }
            else
            {
                strSql += " WHERE funcao_superior_codigo is null or funcao_superior_codigo = 0";
            }

            System.Data.SqlClient.SqlDataReader objDataReader = ServiceDesk.Banco.ClsBanco.geraDataReader(strSql);

            if (bolIsNoRaiz == true)
            {
                if (objTreeView != null)
                {
                    objTreeView.Nodes.Clear();
                    PopulaNosNos(objDataReader, objTreeView.Nodes, null);
                }
                else if (objTreeNode != null)
                {
                    PopulaNosNos(objDataReader, objTreeNode.ChildNodes, null);
                }
            }
            else
            {
                if (objTreeNode != null)
                {
                    PopulaNosNos(objDataReader, objTreeNode.ChildNodes, null);
                }

            }
            objDataReader = null;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region Verifica se a funca��o esta para o perfil
      /// <summary>
      /// Verifica se a fun��o esta para o perfil
      /// </summary>
      /// <returns>Retorna true ou false. Se associa��o ao perfil ou n�o.</returns>
      /// <param name="intCodigoFuncao">C�digo da fun��o</param> 
      /// <param name="intCodigoPerfil">C�digo do perfil</param> 
    public static bool VerificaSeFuncaoEstaProPerfil(int intCodigoPerfil, int intCodigoFuncao)
    {
            if (intCodigoPerfil <= 0 || intCodigoFuncao <= 0) return false ;

            ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
            string strValor = objBanco.retornaValorCampo("DireitoPerfil", "funcao_codigo", "funcao_codigo = " + intCodigoFuncao + "  and perfil_codigo = " + intCodigoPerfil + ""); 
            objBanco = null;

            if (strValor != string.Empty) 
                return true; 
            else 
                return false;
    }
    #endregion

    #region Atualiza CheckBox da Treeview
    /// <summary>
    /// Marcar no CheckBox as fun��es que est�o para o perfil
    /// </summary>
    /// <param name="objTreeNode">Nome da TreeNode</param>
    public static void AtualizaCheckBoxTreeview(TreeView objTreeView, int intCodigoPerfil)
    {
        int i;
        for (i = 0; i < objTreeView.Nodes.Count; i++)
        {
            if (VerificaSeFuncaoEstaProPerfil(intCodigoPerfil, Convert.ToInt32(objTreeView.Nodes[i].Value.Trim())) == true)
                objTreeView.Nodes[i].Checked = true;
            else
                objTreeView.Nodes[i].Checked = false;
       }
    }
    #endregion

    #endregion
}
}