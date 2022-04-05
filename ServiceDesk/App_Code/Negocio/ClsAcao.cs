using System;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Classe ClsAcao
/// </summary>

namespace ServiceDesk.Negocio
{
  public class ClsAcao
  {
    #region Atributos
    //Colecao de atributos da Acao
    private ServiceDesk.Banco.ClsAtributos objAtributos = new ServiceDesk.Banco.ClsAtributos();

    //Atributos de uma Acao
    private ServiceDesk.Banco.ClsAtributo objCodigo = new ServiceDesk.Banco.ClsAtributo();
    private ServiceDesk.Banco.ClsAtributo objDescricao = new ServiceDesk.Banco.ClsAtributo();
    #endregion

    #region Propriedades

    public ServiceDesk.Banco.ClsAtributos Atributos
    {
      get { return this.objAtributos; }
    }

    /// <summary>
    /// Código
    /// </summary>
    public ServiceDesk.Banco.ClsAtributo Codigo
    {
      get { return objCodigo; }
      set { this.objCodigo = value; }
    }

    /// <summary>
    /// Descrição
    /// </summary>
    public ServiceDesk.Banco.ClsAtributo Descricao
    {
      get { return objDescricao; }
      set { this.objDescricao = value; }
    }

    #endregion

    #region Métodos

    #region Construtor da classe
    /// <summary>
    /// Construtor da Classe
    /// </summary>
    public ClsAcao()
    {
      this.alimentaColecaoCampos();
    }
    #endregion

    #region Construtor da classe por parametro
    /// <summary>
    /// Construtor da classe
    /// </summary>
      public ClsAcao(int intCodigo)
      {
          this.alimentaColecaoCampos();
          this.objCodigo.Valor = intCodigo.ToString();
          ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
          objBanco.alimentaColecao(this.objAtributos);
          objBanco = null;
      }
    #endregion

    #region Alimenta campos coleção

    /// <summary>
    /// Método que alimenta a coleção de atributos
    /// </summary>
    private void alimentaColecaoCampos()
    {
      objAtributos.NomeTabela = "Acao";
      objAtributos.DescricaoTabela = "Acao";

      objCodigo.Campo = "acao_codigo";
      objCodigo.Descricao = "Acao";
      objCodigo.CampoIdentificador = true;
      objCodigo.CampoObrigatorio = false;
      objCodigo.Tipo = System.Data.DbType.Int32;
      objAtributos.Add(objCodigo);

      objDescricao.Campo = "descricao";
      objDescricao.Descricao = "Descricao";
      objDescricao.CampoIdentificador = false;
      objDescricao.CampoObrigatorio = false;
      objDescricao.Tipo = System.Data.DbType.String;
      objAtributos.Add(objDescricao);

    }
    #endregion

    #region Inclui
    /// <summary>
    /// Método que insere uma Ação.
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
                strMensagem = "Favor informar a descrição da ação.";
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

    #region Altera
    /// <summary>
    /// Método que altera uma ação
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
                strMensagem = "Favor informar a Descrição da Ação.";
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

    #region Exclui
    /// <summary>
    /// Método que exclui um Auditado
    /// </summary>
    /// <returns>Retorna true ou false. Se o registro foi excluido ou não.</returns>
    public bool exclui()
    {
        try
        {
            string strMsg = string.Empty;
            
            //Valida a exclusão.
            //if (ServiceDesk.Negocio.ClsIdentificador.ValidaExclusao(objCodigo.Campo, objCodigo.Valor.Trim(), out strMsg, true, false, objAtributos.NomeTabela.Trim()) == false) return false;

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

    #region GeraGridView
    /// <summary>
    /// Gera uma nova geraGridView de acordo com a coleção de atributos.
    /// </summary>
    /// <param name="objGridView">objeto gridview</param>
    public static void geraGridView(System.Web.UI.WebControls.GridView objGridView)
    {
        try
        {
            String strSql = String.Empty;
            objGridView.AutoGenerateColumns = false;
            ClsAcao objAcao = new ClsAcao();
            //ServiceDesk.Controle.ClsGridView.geraGridView(objGridView, objAcao.objAtributos);
            ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
            strSql = objBanco.montaQuery(objAcao.objAtributos, false);
            strSql += " ORDER BY descricao";
            System.Data.DataSet objDataSet = ServiceDesk.Banco.ClsBanco.geraDataSet(strSql);
            objGridView.DataSource = objDataSet;
            objGridView.DataBind();
            objDataSet.Dispose();
            objDataSet = null;
            objBanco = null;
            objAcao = null;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region GeraGridView
    /// <summary>
    /// Gera uma nova geraGridView de acordo com a coleção de atributos.
    /// </summary>
    /// <param name="objGridView">objjeto Grid View</param>
    /// <param name="bolCondicao">Condição para verificar se será utilizado a cláusula Where</param>
    public static void geraGridView(System.Web.UI.WebControls.GridView objGridView, ClsAcao objAcao, bool bolCondicao)
    {
        try
        {
            objGridView.AutoGenerateColumns = false;
            ServiceDesk.Controle.ClsGridView.geraGridView(objGridView, objAcao.objAtributos, bolCondicao);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region GeraGridView
    /// <summary>
    /// Método que gera um Grid View de Auditados
    /// </summary>
    /// <param name="objGridView">ObjetoGridView</param>
    /// <param name="intCodigoAuditoria">Código da auditoria</param>
    public static void geraGridViewItemConfiguracao(GridView objGridView, int intCodigoItemConfiguracao)
    {
        try
        {
            String strSql = String.Empty;
            strSql = "SELECT *";
            strSql += " FROM ic_codigo";
            if (intCodigoItemConfiguracao > 0)
            {
                strSql = " WHERE ic_codigo = " + intCodigoItemConfiguracao.ToString();
            }

            System.Data.DataSet objDataSet = ServiceDesk.Banco.ClsBanco.geraDataSet(strSql);

            objGridView.DataSource = objDataSet;
            objGridView.DataBind();

            objDataSet = null;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region metodo retornaValorItemAcao
    /// <summary>
    /// Retorna o o valor da acao relacionada com o Item de Configuração
    /// </summary>
    /// <param name="intCodigoItem">Código do Item de Configuração</param>
    /// <param name="intCodigoAtributo">Código da Acao</param>
    /// <returns>String com o valor do Atributo</returns>
    public static String retornaValorItemAcao(int intCodigoItem, int intCodigoAcao)
    {
        try
        {
            String strRetorno = String.Empty;

            ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
            strRetorno = objBanco.retornaValorCampo("ICAcao", "valor", "ic_codigo = " + intCodigoItem.ToString() + " AND acao_codigo = " + intCodigoAcao.ToString());
            objBanco = null;

            return strRetorno;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region metodo insereValorItemAcao
    /// <summary>
    /// Insere ou atualiza um determinado Item de Configuração Ação Valor
    /// </summary>
    /// <param name="intCodigoItem">Código do Item de Configuração</param>
    /// <param name="intCodigoAtributo">Código da Acao</param>
    /// <param name="strValor">Valor do Atributo</param>
    /// <returns>Verdadeiro ou Falso (true/false)</returns>
    public static void insereValorItemAcao(int intCodigoItem, int intCodigoAcao, String strValor)
    {
        try
        {
            ClsAcao objAcao = new ClsAcao(intCodigoItem);

            ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
            if (objBanco.retornaValorCampo("ICAcao", "ic_codigo", "ic_codigo = " + intCodigoItem.ToString() + " AND acao_codigo = " + intCodigoAcao.ToString()) == String.Empty)
            {
                objAcao.insereValorItemAcao(intCodigoAcao, strValor);
            }
            else
            {
                objAcao.alteraValorItemAcao(intCodigoAcao, strValor);
            }

            objBanco = null;
            objAcao = null;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region metodo insereValorItemAcao
    /// <summary>
    /// Insere um novo relacionamento entre Item de Configuração e Ação
    /// </summary>
    /// <param name="intCodigoAtributo">Código da Acao</param>
    /// <param name="strValor">Valor do Atributo</param>
    /// <returns>Verdadeiro ou Falso (true/false)</returns>
    private bool insereValorItemAcao(int intCodigoAcao, String strValor)
    {
        try
        {
            String strSql = String.Empty;
            bool bolRetorno = false;

            strSql = "INSERT INTO ICAcao";
            strSql += " VALUES";
            strSql += " (" + this.Codigo.Valor + ", " + intCodigoAcao.ToString();
            strSql += ", '" + ServiceDesk.Generica.ClsTexto.trocaAspaPorHtml(strValor) + "')";

            ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
            if (objBanco.executaSQL(strSql))
            {
                bolRetorno = true;
            }
            objBanco = null;

            return bolRetorno;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region metodo alteraValorItemAcao
    /// <summary>
    /// Altera um relacionamento entre Item de Configuração e Ação
    /// </summary>
    /// <param name="intCodigoItem">Código do Item de Configuração</param>
    /// <param name="intCodigoAtributo">Código da Ação</param>
    /// <param name="strValor">Valor do Atributo</param>
    /// <returns>Verdadeiro ou Falso (true/false)</returns>
    private bool alteraValorItemAcao(int intCodigoAcao, String strValor)
    {
        try
        {
            String strSql = String.Empty;
            bool bolRetorno = false;

            strSql = "UPDATE ICAcao SET ";
            strSql += " valor = '" + ServiceDesk.Generica.ClsTexto.trocaAspaPorHtml(strValor) + "'";
            strSql += " WHERE ic_codigo = " + this.Codigo.Valor;
            strSql += " AND acao_codigo = " + intCodigoAcao.ToString();

            ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
            if (objBanco.executaSQL(strSql))
            {
                bolRetorno = true;
            }
            objBanco = null;

            return bolRetorno;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region GeraDropDownList
    /// <summary>
    /// Gera um novo DropDownList de acordo com a coleção de atributos.
    /// </summary>
    /// <param name="objDropDownList">DropDownList</param>
    public static void geraDropDownList(System.Web.UI.WebControls.DropDownList objDropDownList)
    {
      try
      {
        ClsAcao objAcao = new ClsAcao();
        objDropDownList.DataTextField = objAcao.Descricao.Campo;
        objDropDownList.DataValueField = objAcao.Codigo.Campo;
        ServiceDesk.Controle.ClsDropDownList.geraDropDownList(objDropDownList, objAcao.objAtributos);
        objAcao = null;

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
    
    #region GeraDropDownList
    /// <summary>
    /// Gera um novo DropDownList de ações ralacionadas a um deteminado item
    /// </summary>
    /// <param name="objDropDownList">DropDownList</param>
    public static void geraDropDownList(System.Web.UI.WebControls.DropDownList objDropDownList, string TabelaDestino, string IdentificadorTabelaDestino)
    {
      try
      {
        string strSql = string.Empty;

        strSql = "SELECT Acao.acao_codigo, acao.descricao FROM vinculo, acao ";
        strSql += "WHERE tabela_origem = 'acao' ";
        strSql += "AND tabela_destino = '" + TabelaDestino.Trim() + "' ";
        strSql += "AND identificador_destino = "+ IdentificadorTabelaDestino.Trim() +" ";
        strSql += "AND acao.acao_codigo = identificador_origem ";
        strSql += "ORDER by descricao ";

        //Executa a query e preenche a gridview.
        DataSet ds = ServiceDesk.Banco.ClsBanco.geraDataSet(strSql);
        objDropDownList.DataTextField = "descricao";
        objDropDownList.DataValueField = "acao_codigo";
        objDropDownList.DataSource = ds;
        objDropDownList.DataBind();
        ds.Dispose();
        ds = null;

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

    #region Pega descrição da ação

    public static string getDescricaoAcao(string CodigoAcao)
    {
      string strDescricaoAcao = string.Empty;

      string strSql = "Select descricao from acao where acao_codigo = " + CodigoAcao.Trim();

      SqlDataReader objReader = ServiceDesk.Banco.ClsBanco.geraDataReader(strSql);
      if (objReader.Read())
      {
        strDescricaoAcao = objReader["descricao"].ToString();
      }

      return strDescricaoAcao;
    }

    #endregion

    #endregion
}
}