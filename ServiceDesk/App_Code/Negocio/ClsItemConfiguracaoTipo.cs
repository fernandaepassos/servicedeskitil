using System;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Classe ClsItemConfiguracaoTipo
/// </summary>
/// 

namespace ServiceDesk.Negocio
{
  public class ClsItemConfiguracaoTipo
  {

    #region Declarações
    //Colecao de atributos de um Tipo de Item de Configuracao
    private ServiceDesk.Banco.ClsAtributos objAtributos = new ServiceDesk.Banco.ClsAtributos();

    //Atributos de um Tipo de Item de Configuracao
    private ServiceDesk.Banco.ClsAtributo objCodigo = new ServiceDesk.Banco.ClsAtributo();
    private ServiceDesk.Banco.ClsAtributo objSuperior = new ServiceDesk.Banco.ClsAtributo();
    private ServiceDesk.Banco.ClsAtributo objDescricao = new ServiceDesk.Banco.ClsAtributo();
    private ServiceDesk.Banco.ClsAtributo objPrefixo = new ServiceDesk.Banco.ClsAtributo();
    private ServiceDesk.Banco.ClsAtributo objStatus = new ServiceDesk.Banco.ClsAtributo();
    private ServiceDesk.Banco.ClsAtributo objChave = new ServiceDesk.Banco.ClsAtributo();
    private ArrayList objAtributoLista = new ArrayList();
    #endregion

    #region Propriedades

    /// <summary>
    /// Coleção de atributos.
    /// </summary>
    public ServiceDesk.Banco.ClsAtributos Atributos
    {
      get { return this.objAtributos; }
    }

    /// <summary>
    /// Código do Tipo de Item de Configuração.
    /// </summary>
    public ServiceDesk.Banco.ClsAtributo Codigo
    {
      get { return objCodigo; }
      set { this.objCodigo = value; }
    }

    /// <summary>
    /// Código do Tipo do Item de Configuração Superior.
    /// </summary>
    public ServiceDesk.Banco.ClsAtributo Superior
    {
      get { return objSuperior; }
      set { this.objSuperior = value; }
    }

    /// <summary>
    /// Descrição do Tipo do Item de Configuração.
    /// </summary>
    public ServiceDesk.Banco.ClsAtributo Descricao
    {
      get { return objDescricao; }
      set { this.objDescricao = value; }
    }

    /// <summary>
    /// Prefixo do Tipo do Item de Configuração.
    /// </summary>
    public ServiceDesk.Banco.ClsAtributo Prefixo
    {
      get { return objPrefixo; }
      set { this.objPrefixo = value; }
    }

    /// <summary>
    /// Status do Tipo do Item de Configuração.
    /// </summary>
    public ServiceDesk.Banco.ClsAtributo Status
    {
      get { return objStatus; }
      set { this.objStatus = value; }
    }

    /// <summary>
    /// Chave do Tipo do Item de Configuração.
    /// </summary>
    public ServiceDesk.Banco.ClsAtributo Chave
    {
      get { return objChave; }
      set { this.objChave = value; }
    }

    /// <summary>
    /// Lista de Atributos
    /// </summary>
    public String AtributoLista
    {
      get { return objAtributoLista.ToString(); }
      set { this.objAtributoLista.Add(value); }
    }

    #endregion

    #region Métodos

    #region metodo Construtor da classe
    /// <summary>
    /// Construtor da classe
    /// </summary>
    public ClsItemConfiguracaoTipo()
    {
      this.alimentaColecaoCampos();
    }
    #endregion

    #region metodo Construtor da classe com passagem de parametro
    /// <summary>
    /// Construtor da classe
    /// </summary>
    public ClsItemConfiguracaoTipo(int intCodigo)
    {
      try
      {
        this.alimentaColecaoCampos();
        this.objCodigo.Valor = intCodigo.ToString();
        ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
        objBanco.alimentaColecao(this.objAtributos);
        objBanco = null;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }
    #endregion

    #region metodo alimentaColecaoCampos
    /// <summary>
    /// Método que alimenta a coleção de atributos
    /// </summary>
    private void alimentaColecaoCampos()
    {
      objAtributos.NomeTabela = "ICTipo";
      objAtributos.DescricaoTabela = "Tipo de Item de Configuração";

      objCodigo.Campo = "ic_tipo_codigo";
      objCodigo.Descricao = "Código";
      objCodigo.CampoIdentificador = true;
      objCodigo.CampoObrigatorio = true;
      objCodigo.Tipo = System.Data.DbType.Int32;
      objAtributos.Add(objCodigo);

      objSuperior.Campo = "ic_tipo_codigo_superior";
      objSuperior.Descricao = "Código Superior";
      objSuperior.CampoIdentificador = false;
      objSuperior.CampoObrigatorio = true;
      objSuperior.Tipo = System.Data.DbType.Int32;
      objAtributos.Add(objSuperior);

      objDescricao.Campo = "descricao";
      objDescricao.Descricao = "Descrição";
      objDescricao.CampoIdentificador = false;
      objDescricao.CampoObrigatorio = true;
      objDescricao.Tipo = System.Data.DbType.String;
      objAtributos.Add(objDescricao);

      objPrefixo.Campo = "prefixo";
      objPrefixo.Descricao = "Prefixo";
      objPrefixo.CampoIdentificador = false;
      objPrefixo.CampoObrigatorio = true;
      objPrefixo.Tipo = System.Data.DbType.String;
      objAtributos.Add(objPrefixo);

      objStatus.Campo = "status_codigo";
      objStatus.Descricao = "Status";
      objStatus.CampoIdentificador = false;
      objStatus.CampoObrigatorio = true;
      objStatus.Tipo = System.Data.DbType.Int32;
      objAtributos.Add(objStatus);

      objChave.Campo = "chave";
      objChave.Descricao = "Chave";
      objChave.CampoIdentificador = false;
      objChave.CampoObrigatorio = false;
      objPrefixo.Tipo = System.Data.DbType.String;
      objAtributos.Add(objChave);

    }
    #endregion

    #region metodo insere
    /// <summary>
    /// Método que insere um novo Tipo de Item de Configuração.
    /// </summary>
    /// <param name="strMensagem">Mensagem com informação da execução do método.</param>
    /// <returns>Retorna true ou false. Se o registro foi inserido ou não.</returns>
    public bool insere(out String strMensagem)
    {
      try
      {
        strMensagem = String.Empty;
        bool bolRetorno = false;

        if (this.objDescricao.Valor.Trim() == String.Empty)
        {
          strMensagem = "Favor informar a Descrição do Tipo do Item de Configuração.";
        }
        else if (this.objPrefixo.Valor.Trim() == String.Empty)
        {
          strMensagem = "Favor informar o Prefixo do Tipo do Item de Configuração.";
        }
        else
        {
          ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
          if (objBanco.insereColecao(this.objAtributos))
          {
            strMensagem = "Item inserido com sucesso.";
            bolRetorno = true;
          }
          objBanco = null;
        }

        return bolRetorno;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }
    #endregion

    #region metodo altera
    /// <summary>
    /// Método que altera um Tipo de Item de Configuração
    /// </summary>
    /// <returns>Retorna true ou false. Se o registro foi alterado ou não.</returns>
    public bool altera(out String strMensagem)
    {
      try
      {
        strMensagem = String.Empty;
        bool bolRetorno = false;

        if (this.objDescricao.Valor.Trim() == String.Empty)
        {
          strMensagem = "Favor informar a Descrição do Tipo do Item de Configuração.";
        }
        else if (this.objPrefixo.Valor.Trim() == String.Empty)
        {
          strMensagem = "Favor informar o Prefixo do Tipo do Item de Configuração.";
        }
        else
        {
          ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
          if (objBanco.alteraColecao(this.objAtributos))
          {
            strMensagem = "Item atualizado com sucesso.";
            bolRetorno = true;
          }
          objBanco = null;
        }

        return bolRetorno;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }
    #endregion

    #region metodo exclui
    /// <summary>
    /// Método que exclui um Tipo de Item de Configuração
    /// </summary>
    /// <returns>Retorna true ou false. Se o registro foi excluido ou não.</returns>
    public bool exclui(out String strMensagem)
    {
      strMensagem = string.Empty;
      try
      {
          if (ServiceDesk.Negocio.ClsIdentificador.ValidaExclusao(objCodigo.Campo, objCodigo.Valor.Trim(), out strMensagem, true, false, objAtributos.NomeTabela.Trim()) == false) return false;

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
      catch (Exception ex)
      {
        throw ex;
      }
    }
    #endregion

    #region metodo excluiRelacaoAtributo
    /// <summary>
    /// Método que exclui um Tipo de Item de Configuração
    /// </summary>
    /// <returns>Retorna true ou false. Se o registro foi excluido ou não.</returns>
    public bool excluiRelacaoAtributo(int intCodigo)
    {
      try
      {
        String strSql = String.Empty;
        ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();

        strSql = "DELETE FROM ICTipoAtributo";
        strSql += " WHERE ic_tipo_codigo = " + intCodigo.ToString();

        if (objBanco.executaSQL(strSql))
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
      catch (Exception ex)
      {
        throw ex;
      }
    }
    #endregion

    #region metodo geraGridView
    /// <summary>
    /// Gera uma nova geraGridView de acordo com a coleção de atributos.
    /// </summary>
    /// <param name="objGridView">objeto gridview</param>
    public static void geraGridView(System.Web.UI.WebControls.GridView objGridView)
    {
      try
      {
        objGridView.AutoGenerateColumns = false;
        ClsItemConfiguracaoTipo objItemConfiguracaoTipo = new ClsItemConfiguracaoTipo();
        ServiceDesk.Controle.ClsGridView.geraGridView(objGridView, objItemConfiguracaoTipo.objAtributos);
        objItemConfiguracaoTipo = null;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }
    #endregion

    #region metodo geraGridView
    /// <summary>
    /// Gera uma nova geraGridView de acordo com a coleção de atributos.
    /// </summary>
    /// <param name="objGridView">objjeto Grid View</param>
    /// <param name="bolCondicao">Condição para verificar se será utilizado a cláusula Where</param>
    public static void geraGridView(System.Web.UI.WebControls.GridView objGridView, ClsItemConfiguracaoTipo objItemConfiguracaoTipo, bool bolCondicao)
    {
      try
      {
        objGridView.AutoGenerateColumns = false;
        ServiceDesk.Controle.ClsGridView.geraGridView(objGridView, objItemConfiguracaoTipo.objAtributos, bolCondicao);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }
    #endregion

    #region metodo geraDropDownList
    /// <summary>
    /// Gera um novo DropDownList de acordo com a coleção de atributos.
    /// </summary>
    /// <param name="objDropDownList">DropDownList</param>
    public static void geraDropDownList(System.Web.UI.WebControls.DropDownList objDropDownList)
    {
      try
      {
        String strSql = String.Empty;

        ClsItemConfiguracaoTipo objItemConfiguracaoTipo = new ClsItemConfiguracaoTipo();
        objDropDownList.DataTextField = objItemConfiguracaoTipo.objDescricao.Campo;
        objDropDownList.DataValueField = objItemConfiguracaoTipo.objCodigo.Campo;

        ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
        strSql = objBanco.montaQuery(objItemConfiguracaoTipo.objAtributos, false);

        strSql += " ORDER BY descricao";
        System.Data.DataSet objDataSet = ServiceDesk.Banco.ClsBanco.geraDataSet(strSql);
        objDropDownList.DataSource = objDataSet;
        objDropDownList.DataBind();
        objDataSet.Dispose();
        objDataSet = null;

        objItemConfiguracaoTipo = null;

        //Adiciona a opção default no dropdownlist.
        ListItem itemDefault = new ListItem();
        itemDefault.Text = "--";
        itemDefault.Value = "";
        itemDefault.Selected = true;
        objDropDownList.Items.Insert(0, itemDefault);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }
    #endregion

    #region metodo atualizaChave
    /// <summary>
    /// Método responsavel por montar a chave do Tipo do Item de Configuração
    /// </summary>
    /// <returns>Retorna uma String com a chave do Tipo do Item de Configuração</returns>
    public void atualizaChave()
    {
      try
      {

        this.objChave.Valor = this.objCodigo.Valor;
        if ((this.objCodigo.Valor != null) && (this.objCodigo.Valor != ""))
        {
          if ((this.objSuperior.Valor != null) && (this.objSuperior.Valor != String.Empty))
          {
            String strSql = String.Empty;
            strSql = "SELECT chave FROM ICtipo";
            strSql += " WHERE ic_tipo_codigo = " + this.objSuperior.Valor;
            System.Data.SqlClient.SqlDataReader objDateReader = ServiceDesk.Banco.ClsBanco.geraDataReader(strSql);
            if (objDateReader.Read())
            {
              if (objDateReader[0] != null)
              {
                this.objChave.Valor = objDateReader[0].ToString() + "," + this.objCodigo.Valor.ToString();
              }
            }
            objDateReader = null;
          }
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }
    #endregion

    #region atualizaChaveFilhos
    /// <summary>
    /// Método que atualiza a chave dos itens filhos de um determinado item
    /// </summary>
    /// <param name="intCodigoPai">Código do Pai dos itens que serão atualizados</param>
    public static void atualizaChaveFilhos(int intCodigoPai)
    {
      try
      {
        String strSql = String.Empty;

        strSql = "SELECT ic_tipo_codigo,ic_tipo_codigo_superior FROM ICtipo";
        strSql += " WHERE ic_tipo_codigo_superior = " + intCodigoPai.ToString();
        System.Data.SqlClient.SqlDataReader objDateReader = ServiceDesk.Banco.ClsBanco.geraDataReader(strSql);

        while (objDateReader.Read())
        {

          strSql = "UPDATE ICtipo";
          strSql += " SET chave = ";
          strSql += " (SELECT chave FROM ICtipo WHERE ic_tipo_codigo = " + objDateReader["ic_tipo_codigo_superior"].ToString() + ") + ',' + LTRIM(STR(ic_tipo_codigo))";
          strSql += " WHERE ic_tipo_codigo = " + objDateReader["ic_tipo_codigo"].ToString();
          ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
          objBanco.executaSQL(strSql);
          objBanco = null;

          atualizaChaveFilhos(Convert.ToInt32(objDateReader["ic_tipo_codigo"].ToString()));

        }

        objDateReader = null;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }
    #endregion

    #region metodo insereRelacaoAtributo
    /// <summary>
    /// Falta implementar esse metodo
    /// </summary>
    /// <returns></returns>
    public int insereRelacaoAtributo()
    {
      try
      {
        int i = 0;
        String strSql = String.Empty;

        for (i = 0; i < this.objAtributoLista.Count; i++)
        {
          strSql = "INSERT INTO ICTipoAtributo";
          strSql += " (ic_tipo_codigo, ic_atributo_codigo)";
          strSql += " VALUES";
          strSql += " (" + this.objCodigo.Valor.ToString() + "," + this.objAtributoLista[i].ToString() + ")";
          ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
          objBanco.executaSQL(strSql);
          objBanco = null;
        }
        return i;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }
    #endregion

    #region metodo marcaTipoAtributo
    /// <summary>
    /// Método que marcas todos atributos de um Tipo do Item de Configuração
    /// </summary>
    /// <param name="intCodigoItem">Código do Tipo do Item de Configuração</param>
    /// <param name="objCheckBoxList">CheckBoxList a ser marcado</param>
    public void marcaTipoAtributo(int intCodigoItem, CheckBoxList objCheckBoxList)
    {
      try
      {
        String strSql = String.Empty;
        System.Data.SqlClient.SqlDataReader objDataReader = null;

        //Marcando os atributos do item de configuração
        strSql = "SELECT ic_atributo_codigo";
        strSql += " FROM ICTipoAtributo";
        strSql += " WHERE ic_tipo_codigo = " + intCodigoItem.ToString();

        objDataReader = ServiceDesk.Banco.ClsBanco.geraDataReader(strSql);

        while (objDataReader.Read())
        {
          objCheckBoxList.Items.FindByValue(objDataReader["ic_atributo_codigo"].ToString()).Selected = true;
        }

        objDataReader = null;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }
    #endregion

    #region metodo geraTreeView
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public TreeView geraTreeView()
    {
      try
      {
        TreeView objTreeView = new TreeView();
        return objTreeView;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }
    #endregion

    #region metodo populaNoRaiz
    /// <summary>
    /// Método que popula os nós que não possuem pai
    /// </summary>
    public static void populaNoRaiz(int intCodigoPai, TreeView objTreeView, TreeNode objTreeNode, String strUrl)
    {
      try
      {
        String strSql = String.Empty;

        strSql = "SELECT ic_tipo_codigo, ic_tipo_codigo_superior, descricao";
        strSql += ", (SELECT count(*) FROM ICTipo WHERE ic_tipo_codigo_superior = itemTipo.ic_tipo_codigo) pai";
        strSql += " FROM ICTipo itemTipo";
        if (intCodigoPai > 0)
        {
          strSql += " WHERE ic_tipo_codigo_superior = " + intCodigoPai.ToString();
        }
        else
        {
          strSql += " WHERE ic_tipo_codigo_superior is null";
        }
        strSql += " ORDER BY descricao";

        System.Data.SqlClient.SqlDataReader objDataReader = ServiceDesk.Banco.ClsBanco.geraDataReader(strSql);

        if (objTreeView != null)
        {
          objTreeView.Nodes.Clear();
          ClsItemConfiguracaoTipo.populaNos(objDataReader, objTreeView.Nodes, strUrl);
        }
        else if (objTreeNode != null)
        {
          ClsItemConfiguracaoTipo.populaNos(objDataReader, objTreeNode.ChildNodes, strUrl);
        }

        objDataReader.Dispose();
        objDataReader = null;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }
    #endregion

    #region metodo populaNos
    /// <summary>
    /// Método que popula os nós
    /// </summary>
    public static void populaNos(System.Data.SqlClient.SqlDataReader objDataReader, TreeNodeCollection objTreeNodeCollection, String strUrl)
    {
      try
      {
        while (objDataReader.Read())
        {
          TreeNode objTreeNode = new TreeNode();

          objTreeNode.Text = objDataReader["descricao"].ToString();
          objTreeNode.Value = objDataReader["ic_tipo_codigo"].ToString();
          if (strUrl != String.Empty)
          {
            objTreeNode.Text = "<a class=\"menu\" href=\"" + strUrl + "tipo=" + objTreeNode.Value + "\">" + objDataReader["descricao"].ToString() + "</a>";
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
        throw ex;
      }
    }
    #endregion

    #endregion

  }
}