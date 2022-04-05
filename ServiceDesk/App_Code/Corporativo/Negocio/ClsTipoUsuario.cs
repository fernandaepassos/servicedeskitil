using System;

namespace SServiceDesk.Negocio
{
  /// <summary>
  /// Classe TipoUsuario.
  /// </summary>
  public class ClsTipoUsuario
  {

    //Colecao de atributos do tipo de usuario
    private ServiceDesk.Banco.ClsAtributos objAtributos = new ServiceDesk.Banco.ClsAtributos();

    //Atributos de um cargo
    private ServiceDesk.Banco.ClsAtributo objCodigo = new ServiceDesk.Banco.ClsAtributo();
    private ServiceDesk.Banco.ClsAtributo objSigla = new ServiceDesk.Banco.ClsAtributo();
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

    public ServiceDesk.Banco.ClsAtributo Sigla
    {
      get
      {
        return objSigla;
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
    public ClsTipoUsuario()
    {
      this.alimentaColecaoCampos();
    }
    #endregion

    #region metodo alimentaColecaoCampos
    /// <summary>
    /// Adiciona todos os atributos de um tipo de usuario a coleção de atributos.
    /// </summary>
    private void alimentaColecaoCampos()
    {
      objAtributos.DescricaoTabela = "TipoUsuario";
      objAtributos.NomeTabela = "TipoUsuario";

      objCodigo.Campo = "tipo_usuario_codigo";
      objCodigo.Descricao = "Código";
      objCodigo.CampoIdentificador = true;
      objCodigo.CampoObrigatorio = true;
      objCodigo.Tipo = System.Data.DbType.Int32;
      objAtributos.Add(objCodigo);

      objSigla.Campo = "sigla";
      objSigla.Descricao = "Sigla";
      objSigla.CampoObrigatorio = true;
      objSigla.Tipo = System.Data.DbType.String;
      objSigla.Tamanho = 3;
      objAtributos.Add(objSigla);

      objDescricao.Campo = "descricao";
      objDescricao.Descricao = "Descrição";
      objDescricao.CampoObrigatorio = true;
      objDescricao.Tipo = System.Data.DbType.String;
      objDescricao.Tamanho = 30;
      objAtributos.Add(objDescricao);

    }
    #endregion

    #region metodo alimentaColecao
    /// <summary>
    /// Alimenta a coleção de atributos de uma unidade
    /// </summary>
    /// <param name="intCodigo">Código da unidade a ser alimentada</param>
    public void alimentaTipoUsuario(int intCodigo)
    {
      this.objCodigo.Valor = intCodigo.ToString();
      ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
      objBanco.alimentaColecao(this.objAtributos);
      objBanco = null;
    }
    #endregion

    #region metodo geraDropDownList
    /// <summary>
    /// Gera um novo DropDownList de acordo com a coleção de atributos.
    /// </summary>
    /// <param name="objDropDownList">DropDownList</param>
    public static void geraDropDownList(System.Web.UI.WebControls.DropDownList objDropDownList)
    {
      ClsTipoUsuario objTipoUsuario = new ClsTipoUsuario();
      objDropDownList.DataTextField = objTipoUsuario.objDescricao.Campo;
      objDropDownList.DataValueField = objTipoUsuario.objCodigo.Campo;
      ServiceDesk.Controle.ClsDropDownList.geraDropDownList(objDropDownList, objTipoUsuario.objAtributos);
      objTipoUsuario = null;
    }
    #endregion

    #region metodo geraCheckBoxList
    /// <summary>
    /// Gera um novo CheckBoxList de acordo com a coleção de atributos.
    /// </summary>
    /// <param name="objDropDownList">CheckBoxList</param>
    public static void geraCheckBoxList(System.Web.UI.WebControls.CheckBoxList objCheckBoxList)
    {
      ClsTipoUsuario objTipoUsuario = new ClsTipoUsuario();
      objCheckBoxList.DataTextField = objTipoUsuario.objDescricao.Campo;
      objCheckBoxList.DataValueField = objTipoUsuario.objCodigo.Campo;
      ServiceDesk.Controle.ClsCheckBoxList.geraCheckBoxList(objCheckBoxList, objTipoUsuario.objAtributos);
      objTipoUsuario = null;
    }
    #endregion

    #region metodo geraGridView
    /// <summary>
    /// Gera uma nova geraGridView de acordo com a coleção de atributos.
    /// </summary>
    /// <param name="objGridView">geraGridView</param>
    public static void geraGridView(System.Web.UI.WebControls.GridView objGridView)
    {
      String strSql = String.Empty;
      objGridView.AutoGenerateColumns = false;
      ClsTipoUsuario objTipoUsuario = new ClsTipoUsuario();
      //ServiceDesk.Controle.ClsGridView.geraGridView(objGridView, objTipoUsuario.objAtributos);
      ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
      strSql = objBanco.montaQuery(objTipoUsuario.objAtributos, false);
      strSql += " ORDER BY descricao";
      System.Data.DataSet objDataSet = ServiceDesk.Banco.ClsBanco.geraDataSet(strSql);
      objGridView.DataSource = objDataSet;
      objGridView.DataBind();
      objDataSet.Dispose();
      objDataSet = null;
      objBanco = null;
      objTipoUsuario = null;
    }
    #endregion

    #region metodo geraGridView
    /// <summary>
    /// Gera uma nova geraGridView de acordo com a coleção de atributos.
    /// </summary>
    /// <param name="objGridView">geraGridView</param>
    public static void geraGridView(System.Web.UI.WebControls.GridView objGridView, ClsTipoUsuario objTipoUsuario, bool bolCondicao)
    {
      objGridView.AutoGenerateColumns = false;
      ServiceDesk.Controle.ClsGridView.geraGridView(objGridView, objTipoUsuario.Atributos, bolCondicao);
    }
    #endregion

    #region metodo insere
    /// <summary>
    /// Método que insere um novo Tipo de Usuário.
    /// </summary>
    /// <param name="strMensagem">Mensagem com informação da execução do método.</param>
    /// <returns>Retorna true ou false. Se o registro foi inserido ou não.</returns>
    public bool insere(out String strMensagem)
    {
      strMensagem = String.Empty;
      bool bolRetorno = false;

      if (this.Sigla.Valor.Trim() == String.Empty)
      {
        strMensagem = "Favor informar a Sigla do Tipo de Usuário.";
      }
      else if (this.Descricao.Valor.Trim() == String.Empty)
      {
        strMensagem = "Favor informar a Descrição do Tipo de Usuário.";
      }
      else
      {
        ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
        if (objBanco.insereColecao(this.objAtributos))
        {
          strMensagem = "Tipo de Usuário inserido com sucesso.";
          bolRetorno = true;
        }
        objBanco = null;
      }

      return bolRetorno;
    }
    #endregion

    #region metodo altera
    /// <summary>
    /// Método que altera um Tipo de Usuário
    /// </summary>
    /// <returns>Retorna true ou false. Se o registro foi alterado ou não.</returns>
    public bool altera(out String strMensagem)
    {

      strMensagem = String.Empty;
      bool bolRetorno = false;

      if (this.Sigla.Valor.Trim() == String.Empty)
      {
        strMensagem = "Favor informar a Sigla do Tipo de Usuário.";
      }
      else if (this.Descricao.Valor.Trim() == String.Empty)
      {
        strMensagem = "Favor informar a Descrição do Tipo de Usuário.";
      }
      else
      {
        ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
        if (objBanco.alteraColecao(this.objAtributos))
        {
          strMensagem = "Tipo de Usuário atualizado com sucesso.";
          bolRetorno = true;
        }
        objBanco = null;
      }

      return bolRetorno;
    }
    #endregion

    #region metodo exclui
    /// <summary>
    /// Método que exclui um Tipo de Usuário
    /// </summary>
    /// <returns>Retorna true ou false. Se o registro foi excluido ou não.</returns>
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

  }
}