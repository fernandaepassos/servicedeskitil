using System;

namespace SServiceDesk.Negocio
{
  /// <summary>
  /// Classe ClsTipoEstruturaOrganizacional.
  /// </summary>
  public class ClsTipoEstruturaOrganizacional
  {
    //Colecao de atributos do Tipo de Estrutura Organizacional
    private ServiceDesk.Banco.ClsAtributos objAtributos = new ServiceDesk.Banco.ClsAtributos();

    //Atributos do Tipo de Estrutura Organizacional
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
    }

    #endregion

    #region Construtor
    public ClsTipoEstruturaOrganizacional()
    {
      this.alimentaColecaoCampos();
    }
    #endregion

    #region metodo Construtor da classe com passagem de parametro
    /// <summary>
    /// Construtor da classe
    /// </summary>
    public ClsTipoEstruturaOrganizacional(int intCodigo)
    {
      this.alimentaColecaoCampos();
      this.objCodigo.Valor = intCodigo.ToString();
      ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
      objBanco.alimentaColecao(this.objAtributos);
      objBanco = null;
    }
    #endregion

    #region metodo alimentaColecaoCampos
    /// <summary>
    /// Adiciona todos os atributos de um Tipo de Estrutura Organizacional, a cole��o de atributos.
    /// </summary>
    private void alimentaColecaoCampos()
    {
      objAtributos.DescricaoTabela = "TipoEstruturaOrganizacional";
      objAtributos.NomeTabela = "tipoestruturaorganizacional";

      objCodigo.Campo = "tipo_estrutura_codigo";
      objCodigo.Descricao = "C�digo";
      objCodigo.CampoIdentificador = true;
      objCodigo.CampoObrigatorio = true;
      objCodigo.Tipo = System.Data.DbType.Int32;
      objAtributos.Add(objCodigo);

      objDescricao.Campo = "descricao";
      objDescricao.Descricao = "descricao";
      objDescricao.CampoObrigatorio = true;
      objDescricao.Tipo = System.Data.DbType.String;
      objDescricao.Tamanho = 50;
      objAtributos.Add(objDescricao);
    }
    #endregion

    #region metodo geraDropDownList
    /// <summary>
    /// Gera um novo DropDownList de acordo com a cole��o de atributos.
    /// </summary>
    /// <param name="objDropDownList">DropDownList</param>
    public static void geraDropDownList(System.Web.UI.WebControls.DropDownList objDropDownList)
    {
        try
        {
            ClsTipoEstruturaOrganizacional objTipoEstrutura = new ClsTipoEstruturaOrganizacional();
            objDropDownList.DataTextField = objTipoEstrutura.objDescricao.Campo;
            objDropDownList.DataValueField = objTipoEstrutura.objCodigo.Campo;
            ServiceDesk.Controle.ClsDropDownList.geraDropDownList(objDropDownList, objTipoEstrutura.objAtributos);
            objTipoEstrutura = null;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region metodo geraDropDownListExcecao
    /// <summary>
    /// Gera um novo DropDownList de acordo com a cole��o de atributos com exe��o de uma �rea.
    /// </summary>
    /// <param name="objDropDownList">DropDownList</param>
    /// <param name="intCodigoArea">Codigop da area que nao sera retornada</param>
    public static void geraDropDownListExcecao(System.Web.UI.WebControls.DropDownList objDropDownList, int intCodigoTipoEstrutura)
    {
        try
        {
            string strSql = string.Empty;
            strSql = "SELECT tipo_estrutura_codigo, descricao ";
            strSql = strSql + " FROM TipoEstruturaOrganizacional";
            strSql = strSql + " WHERE tipo_estrutura_codigo <> 0" + intCodigoTipoEstrutura.ToString();

            ClsTipoEstruturaOrganizacional objTipoEstrutura = new ClsTipoEstruturaOrganizacional();

            objDropDownList.DataTextField = objTipoEstrutura.objDescricao.Campo;
            objDropDownList.DataValueField = objTipoEstrutura.objCodigo.Campo;
            System.Data.SqlClient.SqlDataReader objDataReader = ServiceDesk.Banco.ClsBanco.geraDataReader(strSql);
            objDropDownList.DataSource = objDataReader;
            objDropDownList.DataBind();

            objDataReader.Close();
            objDataReader = null;
            objTipoEstrutura = null;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region metodo geraDataGrid
    /// <summary>
    /// Gera um novo DataGrid de acordo com a cole��o de atributos.
    /// </summary>
    /// <param name="objDataGrid">DataGrid</param>
    public static void geraDataGrid(System.Web.UI.WebControls.DataGrid objDataGrid)
    {
        try
        {
            objDataGrid.AutoGenerateColumns = false;
            ClsTipoEstruturaOrganizacional objTipoEstrutura = new ClsTipoEstruturaOrganizacional();
            ServiceDesk.Controle.ClsDataGrid.geraDataGrid(objDataGrid, objTipoEstrutura.objAtributos);
            objTipoEstrutura = null;
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
    public static void geraGridView(System.Web.UI.WebControls.GridView objGridView)
    {
        try
        {
            objGridView.AutoGenerateColumns = false;
            ClsTipoEstruturaOrganizacional objTipoEstrutura = new ClsTipoEstruturaOrganizacional();
            ServiceDesk.Controle.ClsGridView.geraGridView(objGridView, objTipoEstrutura.objAtributos);
            objTipoEstrutura = null;
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
    public static void geraGridView(System.Web.UI.WebControls.GridView objGridView, ClsTipoEstruturaOrganizacional objTipoEstrutura, bool bolCondicao)
    {
        try
        {
            objGridView.AutoGenerateColumns = false;
            ServiceDesk.Controle.ClsGridView.geraGridView(objGridView, objTipoEstrutura.objAtributos, bolCondicao);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region metodo insere
    /// <summary>
    /// M�todo que insere.
    /// </summary>
    /// <returns>Retorna true ou false. Se o registro foi inserido ou n�o.</returns>
    public bool insere(out String strMensagem)
    {
        try
        {
            strMensagem = String.Empty;
            bool bolRetorno = false;

            if (this.objDescricao.Valor.Trim() == String.Empty)
            {
                strMensagem = strMensagem + "Favor informar a descri��o do Tipo de Estrutura Organizacional.<br>";
            }
            if (strMensagem == String.Empty)
            {
                ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
                if (objBanco.insereColecao(this.objAtributos))
                {
                    strMensagem = "Tipo Organizacional inserido com sucesso.";
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
    /// M�todo que altera.
    /// </summary>
    /// <returns>Retorna true ou false. Se o registro foi alterado ou n�o.</returns>
    public bool altera(out String strMensagem)
    {
        try
        {
            strMensagem = String.Empty;
            bool bolRetorno = false;

            if (this.objDescricao.Valor.Trim() == String.Empty)
            {
                strMensagem = "Favor informar a descri��o do Tipo de Estrutura Organizacional.<br>";
            }
            if (strMensagem == String.Empty)
            {
                ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
                if (objBanco.alteraColecao(this.objAtributos))
                {
                    strMensagem = "Tipo de Estrutura Organizacional atualizado com sucesso.";
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
    /// M�todo que exclui.
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

  }
}