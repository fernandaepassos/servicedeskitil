using System;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Classe ClsStatusTabela
/// </summary>

namespace ServiceDesk.Negocio
{
  public class ClsStatusTabela
  {
    public ClsStatusTabela()
    {
      this.alimentaColecaoCampos();
    }

    #region metodo Construtor da classe com passagem de parametro
    /// <summary>
    /// Construtor da classe
    /// </summary>
    public ClsStatusTabela(int intCodigo)
    {
      this.alimentaColecaoCampos();
      this.objCodigo.Valor = intCodigo.ToString();
      ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
      objBanco.alimentaColecao(this.objAtributos);
      objBanco = null;
    }
    #endregion

    //Colecao de atributos de Status
    private ServiceDesk.Banco.ClsAtributos objAtributos = new ServiceDesk.Banco.ClsAtributos();

    //Atributos de uma Status
    private ServiceDesk.Banco.ClsAtributo objCodigo = new ServiceDesk.Banco.ClsAtributo();
    private ServiceDesk.Banco.ClsAtributo objStatusCodigo = new ServiceDesk.Banco.ClsAtributo();
    private ServiceDesk.Banco.ClsAtributo objTabela = new ServiceDesk.Banco.ClsAtributo();
    private ServiceDesk.Banco.ClsAtributo objCor = new ServiceDesk.Banco.ClsAtributo();

    #region Propriedades

    public ServiceDesk.Banco.ClsAtributos Atributos
    {
      get { return this.objAtributos; }
    }

    /// <summary>
    /// C�digo
    /// </summary>
    public ServiceDesk.Banco.ClsAtributo Codigo
    {
      get { return objCodigo; }
      set { this.objCodigo = value; }
    }

    /// <summary>
    /// C�digo do Status
    /// </summary>
    public ServiceDesk.Banco.ClsAtributo StatusCodigo
    {
      get { return objStatusCodigo; }
      set { this.objStatusCodigo = value; }
    }

    /// <summary>
    /// Tabela
    /// </summary>
    public ServiceDesk.Banco.ClsAtributo Tabela
    {
      get { return objTabela; }
      set { this.objTabela = value; }
    }

    /// <summary>
    /// Cor
    /// </summary>
    public ServiceDesk.Banco.ClsAtributo Cor
    {
      get { return objCor; }
      set { this.objCor = value; }
    }

    #endregion

    #region M�todos

    #region alimentaColecaoCampos

    /// <summary>
    /// M�todo que alimenta a cole��o de atributos
    /// </summary>
    private void alimentaColecaoCampos()
    {
      objAtributos.NomeTabela = "StatusTabela";
      objAtributos.DescricaoTabela = "StatusTabela";

      objCodigo.Campo = "status_tabela_codigo";
      objCodigo.Descricao = "C�digo";
      objCodigo.CampoIdentificador = true;
      objCodigo.CampoObrigatorio = false;
      objCodigo.Tipo = System.Data.DbType.Int32;
      objAtributos.Add(objCodigo);

      objStatusCodigo.Campo = "status_codigo";
      objStatusCodigo.Descricao = "C�digo do Status";
      objStatusCodigo.CampoIdentificador = false;
      objStatusCodigo.CampoObrigatorio = false;
      objStatusCodigo.Tipo = System.Data.DbType.Int32;
      objAtributos.Add(objStatusCodigo);

      objTabela.Campo = "tabela";
      objTabela.Descricao = "Tabela";
      objTabela.CampoIdentificador = false;
      objTabela.CampoObrigatorio = false;
      objTabela.Tipo = System.Data.DbType.String;
      objAtributos.Add(objTabela);

      objCor.Campo = "cor";
      objCor.Descricao = "Cor";
      objCor.CampoIdentificador = false;
      objCor.CampoObrigatorio = false;
      objCor.Tipo = System.Data.DbType.String;
      objAtributos.Add(objCor);

    }
    #endregion

    #region metodo geraGridView
    /// <summary>
    /// Gera uma nova geraGridView de acordo com a cole��o de atributos.
    /// </summary>
    /// <param name="objGridView">geraGridView</param>
    public static void geraGridView(System.Web.UI.WebControls.GridView objGridView)
    {
      try
      {
        objGridView.AutoGenerateColumns = false;
        ClsStatusTabela objStatusTabela = new ClsStatusTabela();
        ServiceDesk.Controle.ClsGridView.geraGridView(objGridView, objStatusTabela.objAtributos);
        objStatusTabela = null;
      }
      catch (Exception ex)
      {
        throw ex;
      }

    }
    #endregion

    #region metodo geraGridView
    /// <summary>
    /// Gera uma nova geraGridView de acordo com a cole��o de atributos.
    /// </summary>
    /// <param name="objGridView">geraGridView</param>
    /// <param name="bolCondicao">Condi��o para verificar se ser� utilizado a cl�usula Where</param>
    public static void geraGridView(System.Web.UI.WebControls.GridView objGridView, ClsStatusTabela objStatusTabela, bool bolCondicao)
    {
      try
      {
        objGridView.AutoGenerateColumns = false;
        ServiceDesk.Controle.ClsGridView.geraGridView(objGridView, objStatusTabela.objAtributos, bolCondicao);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }
    #endregion

    #region metodo geraGridViewTabelas
    /// <summary>
    /// Gera uma nova geraGridView com a descri��o de todas as tabelas do banco
    /// </summary>
    /// <param name="objGridView">Nome da GridView</param>
    public static void geraGridViewTabelas(System.Web.UI.WebControls.GridView objGridView)
    {
      try
      {
        String strSQL;
        strSQL = "SELECT name as NomeTabela FROM SysObjects WHERE xtype = 'U' ORDER BY name";
        objGridView.DataSource = ServiceDesk.Banco.ClsBanco.geraDataReader(strSQL);
        objGridView.DataBind();
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }
    #endregion

    #region metodo geraGridViewStatusTabela
    /// <summary>
    /// Gera uma nova geraGridView com a descri��o de todas as tabelas do banco
    /// </summary>
    /// <param name="objGridView">Nome da GridView.</param>
    /// <param name="strNomeTabela">Nome da Tabela.</param>
    public SqlDataReader geraGridViewStatusTabela(String strNomeTabela)
    {
      try
      {
        String strSQL;

        strSQL = "SELECT s.status_codigo,s.sigla, s.descricao FROM status s, StatusTabela st WHERE ";
        strSQL += "st.tabela = '" + strNomeTabela.Trim() + "'AND s.status_codigo = st.status_codigo ";
        strSQL += "Group By s.status_codigo,s.sigla, s.descricao";

        return ServiceDesk.Banco.ClsBanco.geraDataReader(strSQL);
      }
      catch (Exception ex)
      {
        throw ex;
      }

    }
    #endregion

    #region metodo insere
    /// <summary>
    /// M�todo que insere um StatusTabela
    /// </summary>
    /// <param name="strMensagem">Mensagem com informa��o da execu��o do m�todo.</param>
    /// <returns>Retorna true ou false. Se o registro foi inserido ou n�o.</returns>
    public bool insere(out String strMensagem)
    {
      try
      {
        strMensagem = String.Empty;
        bool bolRetorno = false;

        if (this.objTabela.Valor.Trim() == String.Empty)
          strMensagem = "Favor informar o nome da Tabela.<br />";

        if (this.objStatusCodigo.Valor.Trim() == String.Empty)
          strMensagem = "Favor informar o Status.";

        if (strMensagem == String.Empty)
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

    #region metodo exclui
    /// <summary>
    /// M�todo que exclui
    /// </summary>
    /// <returns>Retorna true ou false. Se o registro foi excluido ou n�o.</returns>
    public bool exclui()
    {
      try
      {
        string strMsg = string.Empty;

        //Valida a exclus�o.
        if (ServiceDesk.Negocio.ClsIdentificador.ValidaExclusao(objCodigo.Campo, objCodigo.Valor.Trim(), out strMsg, true, false, objAtributos.NomeTabela.Trim()) == false) return false;

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

    #region metodo exclui
    /// <summary>
    /// M�todo que exclui um registro de acordo com o c�digo do status
    /// </summary>
    /// <param name="intCodigoStatus">C�digo do Status.</param>
    /// <param name="strNomeTabela">Nome da Tabela.</param>
    /// <returns>Retorna true ou false. Se o registro foi excluido ou n�o.</returns>
    public bool exclui(int intCodigoStatus, String strNomeTabela)
    {
      try
      {
        String strSQL;
        strSQL = "DELETE FROM StatusTabela WHERE status_codigo = " + intCodigoStatus + "AND tabela = '" + strNomeTabela.Trim() + "'";
        ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
        if (objBanco.executaSQL(strSQL))
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

    #region metodo exclui
    /// <summary>
    /// M�todo que exclui um registro de acordo com o nome da tabela.
    /// </summary>
    /// <param name="intCodigoStatus">C�digo do Status.</param>
    /// <param name="strNomeTabela">Nome da Tabela.</param>
    /// <returns>Retorna true ou false. Se o registro foi excluido ou n�o.</returns>
    public bool exclui(String strNomeTabela)
    {
      try
      {
        String strSQL;
        strSQL = "DELETE FROM StatusTabela WHERE tabela = '" + strNomeTabela.Trim() + "'";
        ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
        if (objBanco.executaSQL(strSQL))
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

    #region GetStatusporTabela
    /// <summary>
    /// M�todo que pega o status de uma determinada tabela.
    /// </summary>
    /// <param name="intCodigoStatus">C�digo do Status.</param>
    /// <param name="strNomeTabela">Nome da Tabela.</param>
    /// <returns>Retorna uma String S para sim ou N para n�o</returns>
    public static String GetStatusporTabela(String intCodigoStatus, String strNomeTabela)
    {
      try
      {
        SqlDataReader dr;
        String strSQL = "SELECT status_tabela_codigo FROM StatusTabela WHERE status_codigo = " + intCodigoStatus + "AND tabela = '" + strNomeTabela.Trim() + "'";
        dr = ServiceDesk.Banco.ClsBanco.geraDataReader(strSQL);
        if (dr.Read())
        {
          dr.Dispose();
          return "S";
        }
        else
        {
          dr.Dispose();
          return "N";
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }
    #endregion

    #region metodo geraDropDownList
    /// <summary>
    /// Gera um novo DropDownList de acordo com a cole��o de atributos.
    /// </summary>
    /// <param name="objDropDownList">DropDownList</param>
    /// <param name="strTabela">Tabela a qual ser� filtrado para a busca dos status</param>
    public static void geraDropDownList(System.Web.UI.WebControls.DropDownList objDropDownList, String strTabela)
    {
      try
      {
        String strSql = String.Empty;

        strSql = "SELECT statustabela.status_codigo, status.descricao";
        strSql += " FROM status,statustabela";
        strSql += " WHERE status.status_codigo = statustabela.status_codigo";
        if ((strTabela != null) && (strTabela != ""))
        {
          strSql += " AND statustabela.tabela = '" + strTabela.Trim() + "'";
        }

        System.Data.DataSet objDataSet = ServiceDesk.Banco.ClsBanco.geraDataSet(strSql);

        objDropDownList.Items.Clear();

        objDropDownList.DataTextField = "descricao";
        objDropDownList.DataValueField = "status_codigo";
        objDropDownList.DataSource = objDataSet;
        objDropDownList.DataBind();

        //Adiciona a op��o default no dropdownlist.
        ListItem itemDefault = new ListItem();
        itemDefault.Text = "Selecione";
        itemDefault.Value = "";
        itemDefault.Selected = true;
        objDropDownList.Items.Insert(0, itemDefault);

        objDataSet.Dispose();
        objDataSet = null;
      }
      catch (Exception ex)
      {

        throw ex;
      }

    }
    #endregion

    #region metodo geraDropDownListPrioridade
    /// <summary>
    /// Gera um novo DropDownList de acordo com a cole��o de atributos de prioridade.
    /// </summary>
    /// <param name="objDropDownList">DropDownList</param>
    /// <param name="strTabela">Tabela a qual ser� filtrado para a busca dos status</param>
    public static void geraDropDownListPrioridade(System.Web.UI.WebControls.DropDownList objDropDownList, String strTabela)
    {
        try
        {
            String strSql = String.Empty;

            strSql = "SELECT statustabela.status_codigo, status.descricao";
            strSql += " FROM status,statustabela";
            strSql += " WHERE status.status_codigo = statustabela.status_codigo";
            if ((strTabela != null) && (strTabela != ""))
            {
                strSql += " AND statustabela.tabela = '" + strTabela.Trim() + "'";
            }

            System.Data.DataSet objDataSet = ServiceDesk.Banco.ClsBanco.geraDataSet(strSql);

            objDropDownList.Items.Clear();

            objDropDownList.DataTextField = "descricao";
            objDropDownList.DataValueField = "status_codigo";
            objDropDownList.DataSource = objDataSet;
            objDropDownList.DataBind();

            //Adiciona a op��o default no dropdownlist.
            ListItem itemDefault = new ListItem();
            itemDefault.Text = "Selecione";
            itemDefault.Value = "";
            itemDefault.Selected = true;
            objDropDownList.Items.Insert(0, itemDefault);

            objDataSet.Dispose();
            objDataSet = null;
        }
        catch (Exception ex)
        {

            throw ex;
        }

    }
    #endregion

    #region metodo retornaLista
    /// <summary>
    /// Retorna um ArrayList de objetos Status
    /// </summary>
    /// <param name="strTabela">Tabela a qual ser� filtrado para a busca dos status</param>
    public static System.Collections.ArrayList retornaLista(String strTabela)
    {
      try
      {
        String strSql = String.Empty;
        System.Collections.ArrayList arlStatus = new System.Collections.ArrayList();

        strSql = "SELECT statustabela.status_codigo, status.descricao";
        strSql += " FROM status,statustabela";
        strSql += " WHERE status.status_codigo = statustabela.status_codigo";
        if ((strTabela != null) && (strTabela != ""))
        {
          strSql += " AND statustabela.tabela = '" + strTabela.Trim() + "'";
        }

        System.Data.SqlClient.SqlDataReader objReader = ServiceDesk.Banco.ClsBanco.geraDataReader(strSql);
        while (objReader.Read())
        {
          ClsStatus objStatus = new ClsStatus(Convert.ToInt32(objReader["status_codigo"].ToString()));
          arlStatus.Add(objStatus);
          objStatus = null;
        }
        objReader.Dispose();
        objReader = null;

        return arlStatus;
      }
      catch (Exception ex)
      {
        throw ex;
      }

    }
    #endregion

    #region VerificaExiteRelacao
    /// <summary>
    /// Verifica se existe uma rela��o de Status com o a Tabela indicada.
    /// </summary>
    /// <param name="intCodigoStatus">C�digo do Status.</param>
    /// <param name="strNomeTabela">C�digo do par�metro.</param>
    /// <returns>Retorna um DataReader</returns>
    public static bool VerificaExiteRelacao(int intCodigoStatus, String strNomeTabela)
    {
      try
      {
        bool bolRetorno = false;
        SqlDataReader dr;
        String strSQL = "SELECT * FROM statustabela WHERE status_codigo = " + intCodigoStatus;
        strSQL += " AND tabela = '" + strNomeTabela.Trim() + "'";

        dr = ServiceDesk.Banco.ClsBanco.geraDataReader(strSQL);
        if (dr.Read())
        {
          bolRetorno = true;
        }

        dr.Dispose();
        dr = null;

        return bolRetorno;

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