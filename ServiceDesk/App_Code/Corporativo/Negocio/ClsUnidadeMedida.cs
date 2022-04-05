using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace SServiceDesk.Negocio
{
  /// <summary>
  /// Classe ClsUnidadeMedida.
  /// </summary>
  public class ClsUnidadeMedida
  {

    //Colecao de atributos de UnidadeMedida
    private ServiceDesk.Banco.ClsAtributos objAtributos = new ServiceDesk.Banco.ClsAtributos();

    //Atributos da Unidade de Medida
    private ServiceDesk.Banco.ClsAtributo objCodigo = new ServiceDesk.Banco.ClsAtributo();
    private ServiceDesk.Banco.ClsAtributo objSiglaMedida = new ServiceDesk.Banco.ClsAtributo();
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

    public ServiceDesk.Banco.ClsAtributo SiglaMedida
    {
      get
      {
        return objSiglaMedida;
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
    public ClsUnidadeMedida()
    {
      this.alimentaColecaoCampos();
    }
    #endregion

    #region metodo Construtor da classe com passagem de parametro
    /// <summary>
    /// Construtor da classe
    /// </summary>
    public ClsUnidadeMedida(int intCodigo)
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
    /// Adiciona todos os atributos de um feriado a cole��o de atributos.
    /// </summary>
    private void alimentaColecaoCampos()
    {
      objAtributos.DescricaoTabela = "UnidadeMedida";
      objAtributos.NomeTabela = "UnidadeMedida";

      objCodigo.Campo = "medida_codigo";
      objCodigo.Descricao = "C�digo";
      objCodigo.CampoIdentificador = true;
      objCodigo.CampoObrigatorio = true;
      objCodigo.Tipo = System.Data.DbType.Int32;
      objAtributos.Add(objCodigo);

      objSiglaMedida.Campo = "sigla_medida";
      objSiglaMedida.Descricao = "sigla medida";
      objSiglaMedida.CampoObrigatorio = true;
      objSiglaMedida.Tipo = System.Data.DbType.String;
      objSiglaMedida.Tamanho = 50;
      objAtributos.Add(objSiglaMedida);

      objDescricao.Campo = "descricao";
      objDescricao.Descricao = "descricao";
      objDescricao.CampoObrigatorio = true;
      objDescricao.Tipo = System.Data.DbType.String;
      objDescricao.Tamanho = 255;
      objAtributos.Add(objDescricao);
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
            ClsUnidadeMedida objUnidadeMedida = new ClsUnidadeMedida();
            ServiceDesk.Controle.ClsDataGrid.geraDataGrid(objDataGrid, objUnidadeMedida.objAtributos);
            objUnidadeMedida = null;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region metodo geraGridView
    /// <summary>
    /// Gera uma nova GridView de acordo com a cole��o de atributos.
    /// </summary>
    /// <param name="objGridView">geraGridView</param>
    public static void geraGridView(System.Web.UI.WebControls.GridView objGridView)
    {
        try
        {
            objGridView.AutoGenerateColumns = false;
            ClsUnidadeMedida objUnidadeMedida = new ClsUnidadeMedida();
            ServiceDesk.Controle.ClsGridView.geraGridView(objGridView, objUnidadeMedida.objAtributos);
            objUnidadeMedida = null;
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
      public static void geraGridView(System.Web.UI.WebControls.GridView objGridView, ClsUnidadeMedida objUnidadeMedida, bool bolCondicao)
      {
          try
          {
              objGridView.AutoGenerateColumns = false;
              ServiceDesk.Controle.ClsGridView.geraGridView(objGridView, objUnidadeMedida.objAtributos, bolCondicao);
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
    public static void geraDropDownList(System.Web.UI.WebControls.DropDownList objDropDownList)
    {
        try
        {
            ClsUnidadeMedida objUnidadeMedida = new ClsUnidadeMedida();
            objDropDownList.DataTextField = objUnidadeMedida.objDescricao.Campo;
            objDropDownList.DataValueField = objUnidadeMedida.objCodigo.Campo;
            ServiceDesk.Controle.ClsDropDownList.geraDropDownList(objDropDownList, objUnidadeMedida.objAtributos);
            objUnidadeMedida = null;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region metodo geraDropDownListItem
    /// <summary>
    /// Gera o DropDownList dos itens de configura��o com excess�o do item que tem o c�digo igual ao passado pelo parametro
    /// </summary>
    /// <param name="objDropDownList">Objeto DropDownList</param>
    /// <param name="intCodigoItem">C�digo do Item que n�o ser� listado</param>
    public static void geraDropDownListItem(DropDownList objDropDownList, int intCodigoItem)
    {
        try
        {
            ClsUnidadeMedida objUnidadeMedida = new ClsUnidadeMedida();
            objDropDownList.DataTextField = objUnidadeMedida.Descricao.Campo;
            objDropDownList.DataValueField = objUnidadeMedida.Codigo.Campo;
            ServiceDesk.Controle.ClsDropDownList.geraDropDownList(objDropDownList, objUnidadeMedida.Atributos);
            objDropDownList.Items.FindByValue(intCodigoItem.ToString()).Enabled = false;
            objUnidadeMedida = null;
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
                strMensagem = "Favor informar a descri��o da unidade de medida.<br>";
            }
            if (this.objSiglaMedida.Valor.Trim() == String.Empty)
            {
                strMensagem = strMensagem + "Favor informar a sigla da unidade de medida.";
            }
            if (strMensagem == String.Empty)
            {
                ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
                if (objBanco.insereColecao(this.objAtributos))
                {
                    strMensagem = "Unidade de Medida inserida com sucesso.";
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
                strMensagem = "Favor informar a descri��o da unidade de medida.<br>";
            }
            if (this.objSiglaMedida.Valor.Trim() == String.Empty)
            {
                strMensagem = strMensagem + "Favor informar a sigla da unidade de medida.";
            }
            if (strMensagem == String.Empty)
            {
                ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
                if (objBanco.alteraColecao(this.objAtributos))
                {
                    strMensagem = "Unidade de Medida atualizada com sucesso.";
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