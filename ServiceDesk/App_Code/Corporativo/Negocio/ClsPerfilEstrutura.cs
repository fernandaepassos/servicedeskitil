using System;

namespace SServiceDesk.Negocio
{
  /// <summary>
  /// Summary description for PerfilEstrutura
  /// </summary>
  public class ClsPerfilEstrutura
  {
    //Colecao de atributos de PerfilEstrutura
    private ServiceDesk.Banco.ClsAtributos objAtributos = new ServiceDesk.Banco.ClsAtributos();

    //Atributos de uma PerfilEstrutura
    private ServiceDesk.Banco.ClsAtributo objCodigo = new ServiceDesk.Banco.ClsAtributo();
    private ServiceDesk.Banco.ClsAtributo objPerfilCodigo = new ServiceDesk.Banco.ClsAtributo();
    private ServiceDesk.Banco.ClsAtributo objEstruturaCodigo = new ServiceDesk.Banco.ClsAtributo();

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

    public ServiceDesk.Banco.ClsAtributo PerfilCodigo
    {
      get
      {
        return objPerfilCodigo;
      }
    }

    public ServiceDesk.Banco.ClsAtributo EstruturaCodigo
    {
      get
      {
        return objEstruturaCodigo;
      }
    }

    #endregion

    #region Construtor
    public ClsPerfilEstrutura()
    {
      this.alimentaColecaoCampos();
    }
    #endregion

    #region metodo alimentaColecaoCampos
    /// <summary>
    /// Adiciona todos os atributos de uma Estrutura a coleção de atributos.
    /// </summary>
    private void alimentaColecaoCampos()
    {
      objAtributos.DescricaoTabela = "PerfilEstrutura";
      objAtributos.NomeTabela = "PerfilEstrutura";

      objCodigo.Campo = "perfil_estrutura_codigo";
      objCodigo.Descricao = "Código";
      objCodigo.CampoIdentificador = true;
      objCodigo.CampoObrigatorio = true;
      objCodigo.Tipo = System.Data.DbType.Int32;
      objAtributos.Add(objCodigo);

      objPerfilCodigo.Campo = "perfil_codigo";
      objPerfilCodigo.Descricao = "perfil codigo";
      objPerfilCodigo.CampoObrigatorio = true;
      objPerfilCodigo.Tipo = System.Data.DbType.Int32;
      objAtributos.Add(objPerfilCodigo);

      objEstruturaCodigo.Campo = "Estrutura_codigo";
      objEstruturaCodigo.Descricao = "Estrutura_codigo";
      objEstruturaCodigo.Tipo = System.Data.DbType.Int32;
      objAtributos.Add(objEstruturaCodigo);
    }
    #endregion

    #region metodo geraDropDownList
    /// <summary>
    /// Gera um novo DropDownList de acordo com a coleção de atributos.
    /// </summary>
    /// <param name="objDropDownList">DropDownList</param>
    public static void geraDropDownList(System.Web.UI.WebControls.DropDownList objDropDownList)
    {
      ClsPerfilEstrutura objPerfilEstrutura = new ClsPerfilEstrutura();
      objDropDownList.DataTextField = objPerfilEstrutura.objCodigo.Campo;
      objDropDownList.DataValueField = objPerfilEstrutura.objCodigo.Campo;
      ServiceDesk.Controle.ClsDropDownList.geraDropDownList(objDropDownList, objPerfilEstrutura.objAtributos);
      objPerfilEstrutura = null;
    }
    #endregion

    #region alimentaColecao
    /// <summary>
    /// Alimenta a coleção de atributos de um Perdil Estrutura
    /// </summary>
    /// <param name="intCodigo">Código da PerfilEstrutura a ser alimentado</param>
    public void alimentaPerfilEstrutura(int intCodigo)
    {
      this.objCodigo.Valor = intCodigo.ToString();
      ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
      objBanco.alimentaColecao(this.objAtributos);
      objBanco = null;
    }
    #endregion

    #region existeRegistro

    /// <summary>
    /// Método que verifica se existe um determinado registro na tabela PerfilEstrutura.
    /// </summary>
    /// <returns>Retorna uma string com o código PerfilEstrutura.</returns>
    public string existeRegistro()
    {
      //Verifica se já existe o item selecionado
      ServiceDesk.Banco.ClsBanco banco = new ServiceDesk.Banco.ClsBanco();
      string strExiste = string.Empty;
      strExiste = banco.retornaValorCampo("PerfilEstrutura", "perfil_estrutura_codigo", "perfil_codigo ='" + this.objPerfilCodigo.Valor.ToString() + "' and Estrutura_codigo ='" + this.objEstruturaCodigo.Valor.ToString() + "'");

      banco = null;
      return strExiste;
    }
    #endregion

    #region getNaoSelecionados
    /// <summary>
    /// Método que captura os perfis não selecionados através de uma string de perfis selecionados.
    /// </summary>
    /// <param name="strSelecionados">strSelecionados é o parâmetro do método.</param>
    /// <returns>Retorna uma string de perfis não selecionados.</returns>
    public string getNaoSelecionados(string strSelecionados)
    {
      System.Data.SqlClient.SqlDataReader objReader;
      String strNaoSelecionados = String.Empty;

      if (strSelecionados == String.Empty)
      {
        strSelecionados = "''";
      }

      String strSql = "SELECT perfil_codigo FROM PerfilEstrutura ";
      strSql += "where Estrutura_codigo = '" + objEstruturaCodigo.Valor + "' ";
      strSql += "and perfil_codigo NOT IN (" + strSelecionados + ")";

      objReader = ServiceDesk.Banco.ClsBanco.geraDataReader(strSql);
      if (objReader.HasRows)
      {
        while (objReader.Read())
        {
          strNaoSelecionados = strNaoSelecionados + objReader["perfil_codigo"].ToString() + ",";
        }

        strNaoSelecionados = strNaoSelecionados.Substring(0, strNaoSelecionados.Length - 1);
      }

      objReader.Close();
      objReader.Dispose();

      return strNaoSelecionados;
    }

    #endregion

    #region gravaSelecionados
    /// <summary>
    /// Método que grava um novo PerfilEstrutura.
    /// </summary>
    /// <returns>Retorna true ou false. Se o registro foi inserido ou não.</returns>
    /// 
    public bool gravaSelecionados(out string strMensagem)
    {
      strMensagem = string.Empty;

      ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();

      if (objBanco.insereColecao(this.objAtributos))
      {
        objBanco = null;
        //strMensagem = "Gravação efetuada com sucesso.";
        return true;
      }
      else
      {
        objBanco = null;
        //strMensagem = "Erro ao gravar o registro.";
        return false;
      }
    }
    #endregion

    #region metodo exclui não-selecionados
    /// <summary>
    /// Método que exclui os não-selecionados.
    /// </summary>
    /// <returns>Retorna true ou false. Se o registro foi excluido ou não.</returns>
    public bool excluiNaoSelecionados()
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

    #region geraArvore
    /// <summary>
    /// Método que gera a árvore de perfis.
    /// </summary>
    public static void geraArvore(System.Web.UI.WebControls.TreeView objArvore, ClsPerfilEstrutura objPerfilEstrutura)
    {
      criaRaizArvore(objArvore, objPerfilEstrutura);
    }

      private static void criaRaizArvore(System.Web.UI.WebControls.TreeView objTreeView, ClsPerfilEstrutura objPerfilEstrutura)
      {
          //Cria arvore e adiciona o nó raiz
          objTreeView.Nodes.Clear();  // Limpando Treeview 

          //Nó raiz
          System.Web.UI.WebControls.TreeNode rootNode = new System.Web.UI.WebControls.TreeNode();
          System.Web.UI.WebControls.TreeNode Node = new System.Web.UI.WebControls.TreeNode();
          rootNode.Value = "-1";
          rootNode.Target = "";
          rootNode.Text = "Perfis";
          objTreeView.Nodes.Add(rootNode);

          //restante da árvore
          criaArvore(objTreeView, objPerfilEstrutura);

          objTreeView.ExpandAll();

          rootNode = null;
          Node = null;
      }
    #endregion

    #region criaArvore
    /// <summary>
    /// Metodo que cria a arvore
    /// </summary>
    /// <param name="objTreeView"></param>
      private static void criaArvore(System.Web.UI.WebControls.TreeView objTreeView, ClsPerfilEstrutura objPerfilEstrutura)
      {
          //Cria a árvore
          try
          {
              System.Data.SqlClient.SqlDataReader objReader;
              string strSql = "select A.descricao as 'Aplicacao', A.aplicacao_codigo ";
              strSql += "from Perfil P, Aplicacao A ";
              strSql += "where A.aplicacao_codigo = P.aplicacao_codigo ";
              strSql += "Group by A.aplicacao_codigo, A.descricao ";
              strSql += "Order by A.descricao ";

              objReader = ServiceDesk.Banco.ClsBanco.geraDataReader(strSql);
              if (objReader.HasRows)
              {
                  while (objReader.Read())
                  {
                      //bool strTemFilhos = true;
                      System.Web.UI.WebControls.TreeNode Node = new System.Web.UI.WebControls.TreeNode();
                      Node.Value = objReader["aplicacao_codigo"].ToString();
                      Node.Text = objReader["Aplicacao"].ToString();

                      montaArvore(Node, objPerfilEstrutura);

                      //adiciona nós na arvore
                      objTreeView.Nodes[0].ChildNodes.Add(Node);
                  }
              }
              objReader.Close();
              objReader.Dispose();
          }
          catch (Exception ex)
          {
              throw ex;
          }
          //Fim da Criação da árvore.
      }
    #endregion

    #region montaArvore
    /// <summary>
    /// Método que monta a arvore
    /// </summary>
    /// <param name="Node"></param>
      private static void montaArvore(System.Web.UI.WebControls.TreeNode Node, ClsPerfilEstrutura objPerfilEstrutura)
      {
          System.Data.SqlClient.SqlDataReader objReaderArvore;
          string strSql = "select P.perfil_codigo, TU.descricao as 'TipoUsuario', ";
          strSql += "A.descricao as 'Aplicacao', A.aplicacao_codigo, TU.tipo_usuario_codigo ";
          strSql += "from Perfil P, TipoUsuario TU, Aplicacao A ";
          strSql += "where TU.tipo_usuario_codigo = P.tipo_usuario_codigo ";
          strSql += "and A.aplicacao_codigo = P.aplicacao_codigo and A.aplicacao_codigo = '" + Node.Value.ToString() + "' ";
          strSql += "Group by A.aplicacao_codigo, TU.tipo_usuario_codigo, ";
          strSql += "A.descricao, TU.descricao, P.perfil_codigo ";
          strSql += "Order by A.descricao, TU.descricao";

          try
          {
              objReaderArvore = ServiceDesk.Banco.ClsBanco.geraDataReader(strSql);
              System.Web.UI.WebControls.TreeNode newNode;
              if (objReaderArvore.HasRows)
              {
                  while (objReaderArvore.Read())
                  {
                      newNode = new System.Web.UI.WebControls.TreeNode(); // Inicializando Node 
                      newNode.Value = objReaderArvore["perfil_codigo"].ToString();
                      newNode.Text = objReaderArvore["TipoUsuario"].ToString();
                      newNode.ShowCheckBox = true;

                      //chama funcao que verifica se existe registro na tabela PerfilEstrutura
                      String strExiste = String.Empty;
                      objPerfilEstrutura.objPerfilCodigo.Valor = objReaderArvore["perfil_codigo"].ToString();
                      strExiste = objPerfilEstrutura.existeRegistro();
                      if (strExiste != String.Empty)
                      {
                          newNode.Checked = true;
                      }

                      Node.ChildNodes.Add(newNode);
                  }
              }
              objReaderArvore.Close();
              objReaderArvore.Dispose();
          }
          catch (Exception ex)
          {
              throw ex;
          }
      }
    #endregion

  }
}