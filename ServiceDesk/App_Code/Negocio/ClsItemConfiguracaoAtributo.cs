using System;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Classe ClsItemConfiguracaoAtributo
/// </summary>

namespace ServiceDesk.Negocio
{
  public class ClsItemConfiguracaoAtributo
  {

    //Colecao de atributos de Status
    private ServiceDesk.Banco.ClsAtributos objAtributos = new ServiceDesk.Banco.ClsAtributos();

    //Atributos de um Atributo do Item de Configuração
    private ServiceDesk.Banco.ClsAtributo objCodigo = new ServiceDesk.Banco.ClsAtributo();
    private ServiceDesk.Banco.ClsAtributo objCodigoUnidadeMedida = new ServiceDesk.Banco.ClsAtributo();
    private ServiceDesk.Banco.ClsAtributo objDescricao = new ServiceDesk.Banco.ClsAtributo();
    private ServiceDesk.Banco.ClsAtributo objTipo = new ServiceDesk.Banco.ClsAtributo();

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
    /// Código da Unidade de Medida
    /// </summary>
    public ServiceDesk.Banco.ClsAtributo CodigoUnidadeMedida
    {
      get { return objCodigoUnidadeMedida; }
      set { this.objCodigoUnidadeMedida = value; }
    }

    /// <summary>
    /// Descrição
    /// </summary>
    public ServiceDesk.Banco.ClsAtributo Descricao
    {
      get { return objDescricao; }
      set { this.objDescricao = value; }
    }

    /// <summary>
    /// Tipo
    /// </summary>
    public ServiceDesk.Banco.ClsAtributo Tipo
    {
      get { return objTipo; }
      set { this.objTipo = value; }
    }

    #endregion

    #region metodos

    #region Construtor da classe
    /// <summary>
    /// Construtor da Classe
    /// </summary>
    public ClsItemConfiguracaoAtributo()
    {
      this.alimentaColecaoCampos();
    }
    #endregion

    #region metodo Construtor da classe com passagem de parametro
    /// <summary>
    /// Construtor da classe
    /// </summary>
    public ClsItemConfiguracaoAtributo(int intCodigo)
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

    #region alimentaColecaoCampos
    /// <summary>
    /// Método que alimenta a coleção de atributos
    /// </summary>
    private void alimentaColecaoCampos()
    {
      objAtributos.NomeTabela = "ICAtributo";
      objAtributos.DescricaoTabela = "Atributo do Item de Configuração";

      objCodigo.Campo = "ic_atributo_codigo";
      objCodigo.Descricao = "Código";
      objCodigo.CampoIdentificador = true;
      objCodigo.CampoObrigatorio = false;
      objCodigo.Tipo = System.Data.DbType.Int32;
      objAtributos.Add(objCodigo);

      objCodigoUnidadeMedida.Campo = "medida_codigo";
      objCodigoUnidadeMedida.Descricao = "Código da Unidade de Medida";
      objCodigoUnidadeMedida.CampoIdentificador = false;
      objCodigoUnidadeMedida.CampoObrigatorio = false;
      objCodigoUnidadeMedida.Tipo = System.Data.DbType.Int32;
      objAtributos.Add(objCodigoUnidadeMedida);

      objDescricao.Campo = "descricao";
      objDescricao.Descricao = "Descrição";
      objDescricao.CampoIdentificador = false;
      objDescricao.CampoObrigatorio = false;
      objDescricao.Tipo = System.Data.DbType.String;
      objAtributos.Add(objDescricao);

      objTipo.Campo = "tipo";
      objTipo.Descricao = "Tipo";
      objTipo.CampoIdentificador = false;
      objTipo.CampoObrigatorio = false;
      objTipo.Tipo = System.Data.DbType.String;
      objAtributos.Add(objTipo);

    }
    #endregion

    #region metodo insere
    /// <summary>
    /// Método que insere um novo Atributo do Item de Configuração.
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
          strMensagem = "Favor informar a Descrição do Atributo do Item de Configuração.";
        }
        else if (this.objCodigoUnidadeMedida.Valor.Trim() == String.Empty)
        {
          strMensagem = "Favor informar a Unidade de Medida do Atributo do Item de Configuração.";
        }
        else if (this.objTipo.Valor.Trim() == String.Empty)
        {
          strMensagem = "Favor informar o Tipo do Atributo do Item de Configuração.";
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
    /// Método que altera um Atributo do Item de Configuração
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
          strMensagem = "Favor informar a Descrição do Atributo do Item de Configuração.";
        }
        else if (this.objCodigoUnidadeMedida.Valor.Trim() == String.Empty)
        {
          strMensagem = "Favor informar a Unidade de Medida do Atributo do Item de Configuração.";
        }
        else if (this.objTipo.Valor.Trim() == String.Empty)
        {
          strMensagem = "Favor informar o Tipo do Atributo do Item de Configuração.";
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
    /// Método que exclui um Atributo do Item de Configuração
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

    #region metodo geraCheckBoxList
    /// <summary>
    /// Método que gera um CheckBox
    /// </summary>
    /// <param name="objCheckBoxList">Objeto CheckBoxList</param>
    public static void geraCheckBoxList(CheckBoxList objCheckBoxList)
    {
      try
      {
        ClsItemConfiguracaoAtributo objItemConfiguracaoAtributo = new ClsItemConfiguracaoAtributo();
        objCheckBoxList.DataTextField = objItemConfiguracaoAtributo.Descricao.Campo;
        objCheckBoxList.DataValueField = objItemConfiguracaoAtributo.Codigo.Campo;
        ServiceDesk.Controle.ClsCheckBoxList.geraCheckBoxList(objCheckBoxList, objItemConfiguracaoAtributo.Atributos);
        objItemConfiguracaoAtributo = null;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }
    #endregion

    #region metodo geraGridView
    /// <summary>
    /// Método que gera um Grid View de Atributo do Item de Configuração
    /// </summary>
    /// <param name="objGridView">ObjetoGridView</param>
    public static void geraGridView(GridView objGridView)
    {
      try
      {
        String strSql = String.Empty;
        strSql = "SELECT *";
        strSql += " FROM ICAtributo atributo";

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

    #region metodo geraGridViewPorTipo
    /// <summary>
    /// Método que gera um Grid View de acordo com o Tipo do Atributo do Item de Configuração
    /// </summary>
    /// <param name="objGridView">ObjetoGridView</param>
    /// <param name="intCodigoItemTipo">Código do Tipo do Item</param>
    public static void geraGridViewPorTipo(GridView objGridView, int intCodigoItemTipo)
    {
      try
      {
        String strSql = String.Empty;
        strSql = "SELECT atributo.ic_atributo_codigo,descricao, medida_codigo";
        strSql += " FROM ICatributo atributo, ICtipoatributo tipo";
        strSql += " WHERE atributo.ic_atributo_codigo = tipo.ic_atributo_codigo";
        strSql += " AND tipo.ic_tipo_codigo = " + intCodigoItemTipo.ToString();

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

    #region metodo geraDropDownListTipo
    /// <summary>
    /// Gera o Drop Down List com os Tipos possíveis do Atributo do Item de Configuração
    /// </summary>
    /// <param name="objDropDownList"></param>
    public static void geraDropDownListTipo(DropDownList objDropDownList)
    {
      try
      {
        objDropDownList.Items.Add(new ListItem("Texto", "T"));
        objDropDownList.Items.Add(new ListItem("Númerico", "N"));
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }
    #endregion

    #region metodo retornaDescricaoTipo
    /// <summary>
    /// Retorna a descrição do Tipo do Atributo do Item de Configuração
    /// </summary>
    /// <param name="chrTipo">Tipo do Atributo</param>
    /// <returns>String com a descrição</returns>
    public static String retornaDescricaoTipo(char chrTipo)
    {
      try
      {
        String strRetorno = String.Empty;

        switch (char.ToUpper(chrTipo))
        {
          case 'T':
            {
              strRetorno = "Texto";
              break;
            }
          case 'N':
            {
              strRetorno = "Númerico";
              break;
            }
        }

        return strRetorno;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }
    #endregion

    #region Metodo existeDescricao
    /// <summary>
    /// 
    /// </summary>
    public bool existeDescricao()
    {
      bool bolRetorno = false;
      String strRetorno = String.Empty;

      ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
      strRetorno = objBanco.retornaValorCampo("ICatributo", "ic_atributo_codigo", " LOWER(descricao) = '" + this.objDescricao.Valor.ToLower() + "' AND ic_atributo_codigo <> " + this.objCodigo.Valor);
      if (strRetorno != String.Empty)
      {
        bolRetorno = true;
      }
      objBanco = null;

      return bolRetorno;
    }
    #endregion

    #region Metodo existeDescricao
    /// <summary>
    /// 
    /// </summary>
    /// <param name="strDescricao"></param>
    /// <returns></returns>
    public static bool existeDescricao(String strDescricao)
    {
      bool bolRetorno = false;
      String strRetorno = String.Empty;

      ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
      strRetorno = objBanco.retornaValorCampo("ICatributo", "ic_atributo_codigo", " LOWER(descricao) = '" + strDescricao.ToLower() + "'");
      if (strRetorno != String.Empty)
      {
        bolRetorno = true;
      }
      objBanco = null;
      return bolRetorno;
    }
    #endregion

    #endregion

  }
}