using System;
using System.Collections;

namespace SServiceDesk.Negocio
{
  /// <summary>
  /// Classe ClsPrioridade
  /// </summary>
  public class ClsPrioridade
  {

    //Colecao de atributos do Status
    private ServiceDesk.Banco.ClsAtributos objAtributos = new ServiceDesk.Banco.ClsAtributos();

    //Atributos de um status
    private ServiceDesk.Banco.ClsAtributo objCodigo = new ServiceDesk.Banco.ClsAtributo();
    private ServiceDesk.Banco.ClsAtributo objDescricao = new ServiceDesk.Banco.ClsAtributo();

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

    public ServiceDesk.Banco.ClsAtributo Descricao
    {
      get
      {
        return objDescricao;
      }
      set
      {
        this.objDescricao = value;
      }
    }

    #endregion

    #region Construtor
    public ClsPrioridade()
    {
      this.alimentaColecaoCampos();
    }
    #endregion

    #region metodo Construtor da classe com passagem de parametro
    /// <summary>
    /// Construtor da classe
    /// </summary>
    public ClsPrioridade(int intCodigo)
    {
      this.alimentaColecaoCampos();
      this.objCodigo.Valor = intCodigo.ToString();
      ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
      objBanco.alimentaColecao(this.objAtributos);
      objBanco = null;
    }
    #endregion

    #region ClsPrioridade(String strSigla, int intCodigo)
    /// <summary>
    /// Construtor da Classe
    /// </summary>
    /// <param name="strSigla"></param>
    /// <param name="intCodigo"></param>
    public ClsPrioridade(String strSigla, int intCodigo)
    {
        try
        {
            if (strSigla != String.Empty)
            {
                String strSql = "SELECT *";
                strSql += " FROM prioridade";
                strSql += " WHERE sigla = '" + strSigla.Trim() + "'";
                if (intCodigo != 0)
                {
                    strSql += " AND prioridade_codigo <> 0" + intCodigo.ToString();
                }
                System.Data.SqlClient.SqlDataReader objDataReader = ServiceDesk.Banco.ClsBanco.geraDataReader(strSql);
                if (objDataReader.Read())
                {
                    this.alimentaColecaoCampos();
                    this.Codigo.Valor = objDataReader["prioridade_codigo"].ToString();
                    this.Descricao.Valor = objDataReader["descricao"].ToString();
                }
                objDataReader = null;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region metodo alimentaColecaoCampos
    /// <summary>
    /// Adiciona todos os atributos de um status a coleção de atributos.
    /// </summary>
    private void alimentaColecaoCampos()
    {
      objAtributos.DescricaoTabela = "Prioridade";
      objAtributos.NomeTabela = "prioridade";

      objCodigo.Campo = "prioridade_codigo";
      objCodigo.Descricao = "Código";
      objCodigo.CampoIdentificador = true;
      objCodigo.CampoObrigatorio = true;
      objCodigo.Tipo = System.Data.DbType.Int32;
      objAtributos.Add(objCodigo);

      objDescricao.Campo = "descricao";
      objDescricao.Descricao = "Descrição";
      objDescricao.Tipo = System.Data.DbType.String;
      objDescricao.Tamanho = 30;
      objAtributos.Add(objDescricao);

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
            ClsPrioridade objPrioridade = new ClsPrioridade();
            objDropDownList.DataTextField = objPrioridade.objDescricao.Campo;
            objDropDownList.DataValueField = objPrioridade.objCodigo.Campo;
            ServiceDesk.Controle.ClsDropDownList.geraDropDownList(objDropDownList, objPrioridade.objAtributos);
            objPrioridade = null;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region metodo geraCheckBoxList
    /// <summary>
    /// Gera um novo CheckBoxList de acordo com a coleção de atributos.
    /// </summary>
    /// <param name="objDropDownList">CheckBoxList</param>
    public static void geraCheckBoxList(System.Web.UI.WebControls.CheckBoxList objCheckBoxList)
    {
        try
        {
            ClsPrioridade objPrioridade = new ClsPrioridade();
            objCheckBoxList.DataTextField = objPrioridade.objDescricao.Campo;
            objCheckBoxList.DataValueField = objPrioridade.objCodigo.Campo;
            ServiceDesk.Controle.ClsCheckBoxList.geraCheckBoxList(objCheckBoxList, objPrioridade.objAtributos);
            objPrioridade = null;
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
    /// <param name="objGridView">geraGridView</param>
    public static void geraGridView(System.Web.UI.WebControls.GridView objGridView)
    {
        try
        {
            objGridView.AutoGenerateColumns = false;
            ClsPrioridade objPrioridade = new ClsPrioridade();
            ServiceDesk.Controle.ClsGridView.geraGridView(objGridView, objPrioridade.objAtributos);
            objPrioridade = null;
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
    /// <param name="objGridView">geraGridView</param>
    public static void geraGridView(System.Web.UI.WebControls.GridView objGridView, ClsPrioridade objPrioridade, bool bolCondicao)
    {
        try
        {
            objGridView.AutoGenerateColumns = false;
            ServiceDesk.Controle.ClsGridView.geraGridView(objGridView, objPrioridade.Atributos, bolCondicao);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region metodo geraRepeater
    /// <summary>
    /// Gera um novo Repeater de acordo com a coleção de atributos.
    /// </summary>
    /// <param name="objGridView">geraGridView</param>
    public static void geraRepeater(System.Web.UI.WebControls.Repeater objRepeater)
    {
        try
        {
            ClsPrioridade objPrioridade = new ClsPrioridade();
            ServiceDesk.Controle.ClsRepeater.geraRepeater(objRepeater, objPrioridade.objAtributos);
            objPrioridade = null;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region metodo insere
    /// <summary>
    /// Método que insere uma nova Prioridade.
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
                strMensagem = "Favor informar a Descrição da Prioridade.";
            }
            else
            {
                ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
                if (objBanco.insereColecao(this.objAtributos))
                {
                    strMensagem = "Prioridade inserida com sucesso.";
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
    /// Método que altera uma Prioridade
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
                strMensagem = "Favor informar a Descrição da Prioridade.";
            }
            else
            {
                ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
                if (objBanco.alteraColecao(this.objAtributos))
                {
                    strMensagem = "Prioridade atualizada com sucesso.";
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
    /// Método que exclui uma Prioridade
    /// </summary>
    /// <returns>Retorna true ou false. Se o registro foi excluido ou não.</returns>
    public bool exclui()
    {
        try
        {
            string strMsg = string.Empty;
            
            //Valida a exclusão.
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

    #region metodo retornaLista
    /// <summary>
    /// Retorna um Array de objetos Prioridade
    /// </summary>
    /// <returns></returns>
    public static ArrayList retornaLista()
    {
        try
        {
            String strSql = String.Empty;
            ArrayList arlPrioridade = new ArrayList();

            strSql = "SELECT prioridade_codigo FROM prioridade";

            System.Data.SqlClient.SqlDataReader objReader = ServiceDesk.Banco.ClsBanco.geraDataReader(strSql);
            while (objReader.Read())
            {
                ClsPrioridade objPrioridade = new ClsPrioridade(Convert.ToInt32(objReader["prioridade_codigo"].ToString()));
                arlPrioridade.Add(objPrioridade);
                objPrioridade = null;
            }

            objReader.Dispose();  
          objReader = null;

            return arlPrioridade;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

  }
}