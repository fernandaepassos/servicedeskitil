using System;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace SServiceDesk.Negocio
{
  /// <summary>
  /// Summary description for Perfil
  /// </summary>
  public class ClsPerfil
  {

    //Colecao de atributos de uma perfil.
    private ServiceDesk.Banco.ClsAtributos objAtributos = new ServiceDesk.Banco.ClsAtributos();

    # region Atributos.

    private ServiceDesk.Banco.ClsAtributo objCodigo = new ServiceDesk.Banco.ClsAtributo();
    private ServiceDesk.Banco.ClsAtributo objTipoUsuarioCodigo = new ServiceDesk.Banco.ClsAtributo();
    private ServiceDesk.Banco.ClsAtributo objAplicacaoCodigo = new ServiceDesk.Banco.ClsAtributo();
    private ServiceDesk.Banco.ClsAtributo objFlagPadrao = new ServiceDesk.Banco.ClsAtributo();

    #endregion

    #region Construtor

    public ClsPerfil()
    {
      this.alimentaColecaoCampos();
    }

    #endregion

    #region Construtor(int intTipoUsuarioCodigo, int intAplicacaoCodigo)
    /// <summary>
    /// Construtor da classe
    /// </summary>
    /// <param name="intAplicacaoCodigo">Código da Aplicação</param>
    /// <param name="intTipoUsuarioCodigo">Código do Tipo de Usuário</param>
    public ClsPerfil(int intAplicacaoCodigo, int intTipoUsuarioCodigo)
    {
      if ((intTipoUsuarioCodigo != 0) && (intAplicacaoCodigo != 0))
      {
        String strSql = "SELECT perfil_codigo, flag_padrao";
        strSql += " FROM perfil";
        strSql += " WHERE tipo_usuario_codigo = " + intTipoUsuarioCodigo.ToString();
        strSql += " AND aplicacao_codigo = " + intAplicacaoCodigo.ToString();
        SqlDataReader objDataReader = ServiceDesk.Banco.ClsBanco.geraDataReader(strSql);
        if (objDataReader.Read())
        {
          this.alimentaColecaoCampos();
          this.AplicacaoCodigo.Valor = intAplicacaoCodigo.ToString();
          this.TipoUsuarioCodigo.Valor = intTipoUsuarioCodigo.ToString();
          this.Codigo.Valor = objDataReader["perfil_codigo"].ToString();
          this.FlagPadrao.Valor = objDataReader["flag_padrao"].ToString();
        }
        objDataReader = null;
      }
    }
    #endregion

    #region metodo alimentaColecaoCampos
    /// <summary>
    /// Adiciona todos os atributos de um Perfil, a coleção de atributos.
    /// </summary>
    private void alimentaColecaoCampos()
    {
      objAtributos.DescricaoTabela = "Perfil";
      objAtributos.NomeTabela = "Perfil";

      objCodigo.Campo = "perfil_codigo";
      objCodigo.Descricao = "Código";
      objCodigo.CampoIdentificador = true;
      objCodigo.CampoObrigatorio = true;
      objCodigo.Tipo = System.Data.DbType.Int32;
      objAtributos.Add(objCodigo);

      objTipoUsuarioCodigo.Campo = "tipo_usuario_codigo";
      objTipoUsuarioCodigo.Descricao = "Tipo de Usuário";
      objTipoUsuarioCodigo.CampoObrigatorio = true;
      objTipoUsuarioCodigo.Tipo = System.Data.DbType.Int32;
      objAtributos.Add(objTipoUsuarioCodigo);

      objAplicacaoCodigo.Campo = "aplicacao_codigo";
      objAplicacaoCodigo.Descricao = "Aplicação";
      objAplicacaoCodigo.CampoObrigatorio = true;
      objAplicacaoCodigo.Tipo = System.Data.DbType.Int32;
      objAtributos.Add(objAplicacaoCodigo);

      objFlagPadrao.Campo = "flag_padrao";
      objFlagPadrao.Descricao = "Tipo de Usuário Padrão";
      objFlagPadrao.CampoObrigatorio = true;
      objFlagPadrao.Tipo = System.Data.DbType.String;
      objFlagPadrao.Tamanho = 1;
      objAtributos.Add(objFlagPadrao);
    }
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

    public ServiceDesk.Banco.ClsAtributo TipoUsuarioCodigo
    {
      get
      {
        return objTipoUsuarioCodigo;
      }
    }

    public ServiceDesk.Banco.ClsAtributo AplicacaoCodigo
    {
      get
      {
        return objAplicacaoCodigo;
      }
    }

    public ServiceDesk.Banco.ClsAtributo FlagPadrao
    {
      get
      {
        return objFlagPadrao;
      }
    }
    #endregion

    #region gravaSelecionados
    /// <summary>
    /// Método que grava um novo perfil.
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
        return true;
      }
      else
      {
        objBanco = null;
        return false;
      }
    }
    #endregion

    #region existeRegistro
    public string existeRegistro()
    {
      //Verifica se já existe o item selecionado
      ServiceDesk.Banco.ClsBanco banco = new ServiceDesk.Banco.ClsBanco();
      string strExiste = string.Empty;
      strExiste = banco.retornaValorCampo("Perfil", "perfil_codigo", "aplicacao_codigo ='" + this.objAplicacaoCodigo.Valor.ToString() + "' and tipo_usuario_codigo ='" + this.objTipoUsuarioCodigo.Valor.ToString() + "'");

      return strExiste;
    }
    #endregion

    #region apagaNaoSelecionados
    public bool apagaNaoSelecionados(out string strMensagem)
    {
      strMensagem = string.Empty;

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

    #region geraGridView
    /// <summary>
    /// Gera uma nova geraGridView de acordo com a coleção de atributos.
    /// </summary>
    /// <param name="objGridView">geraGridView</param>
    public static void geraGridView(System.Web.UI.WebControls.GridView objGridView, String strEstrutura)
    {
      objGridView.AutoGenerateColumns = false;
      string strSql = "SELECT P.perfil_codigo, A.aplicacao_codigo, A.descricao, ";
      strSql += "TU.tipo_usuario_codigo, TU.descricao, PE.perfil_estrutura_codigo ";
      strSql += "FROM Perfil P, TipoUsuario TU, Aplicacao A, PerfilEstrutura PE ";
      strSql += "WHERE PE.perfil_codigo = P.perfil_codigo and P.aplicacao_codigo = A.aplicacao_codigo ";
      strSql += "and P.tipo_usuario_codigo = TU.tipo_usuario_codigo ";
      strSql += "and PE.estrutura_codigo = " + strEstrutura;
      strSql += "order by A.descricao, TU.descricao";

      ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
      DataSet objDataSet = ServiceDesk.Banco.ClsBanco.geraDataSet(strSql);
      objGridView.DataSource = objDataSet;
      objGridView.DataBind();
      objDataSet.Dispose();
      objDataSet = null;
      objBanco = null;
    }
    #endregion

    #region altera
    /// <summary>
    /// Método que altera um tipo de perfil
    /// </summary>
    /// <returns>Retorna true ou false. Se o registro foi alterado ou não.</returns>
    public bool altera(out string strMensagem)
    {
      strMensagem = string.Empty;
      string strExiste = string.Empty;
      bool bolRetorno = false;

      ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
      if (objBanco.alteraColecao(this.objAtributos))
      {
        objBanco = null;

        strMensagem = "Alteração efetuada com sucesso.";
        bolRetorno = true;
      }
      else
      {
        objBanco = null;
        strMensagem = "Erro ao gravar o registro.";
      }

      return bolRetorno;
    }
    #endregion

    #region zeraFlagPadrao
    /// <summary>
    /// Coloca o valor da flag padrao para '0' nos perfis de uma determinada aplicacao
    /// </summary>
    /// <param name="intAplicacaoCodigo"></param>
    public void zeraFlagPadrao()
    {
      String strSql = "UPDATE perfil set flag_padrao = '0'";
      strSql += " WHERE aplicacao_codigo = 0" + this.AplicacaoCodigo.Valor;
      ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
      objBanco.executaSQL(strSql);
      objBanco = null;
    }
    #endregion

    #region geraDropDownListPorEstruturaEAplicacao
    /// <summary>
    /// Gera uma nova geraGridView de acordo com a coleção de atributos.
    /// </summary>
    /// <param name="objDropDownList">objeto DropDownList que receberá os valores.</param>
    /// <param name="strAplicacao">Código da aplicação</param>
    /// <param name="strEstrutura">Código da estrutura.</param>
    /// <param name="campoTextoExibicao">Nome do campo cujo valor será exibido no texto do item da combo.</param>
    /// <param name="campoValor">Nome do campo cujo valor será atribuido ao item da combo.</param>
    public static void geraDropDownListPorEstruturaAplicacao(System.Web.UI.WebControls.DropDownList objDropDownList, String strEstrutura, String strAplicacao, String campoTextoExibicao, String campoValor)
    {
      string strSql = "SELECT P.perfil_codigo,  ";
      strSql += "TU.tipo_usuario_codigo, TU.descricao, PE.perfil_estrutura_codigo  ";
      strSql += "FROM Perfil P, TipoUsuario TU, Aplicacao A, PerfilEstrutura PE  ";
      strSql += "WHERE PE.perfil_codigo = P.perfil_codigo and P.aplicacao_codigo = A.aplicacao_codigo  ";
      strSql += "and P.tipo_usuario_codigo = TU.tipo_usuario_codigo  ";
      strSql += "and PE.estrutura_codigo = '" + strEstrutura + "' ";
      strSql += "and A.aplicacao_codigo = '" + strAplicacao + "' ";
      strSql += "order by TU.descricao ";

      ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
      objDropDownList.DataTextField = campoTextoExibicao;
      objDropDownList.DataValueField = campoValor;
      objDropDownList.DataSource = ServiceDesk.Banco.ClsBanco.geraDataSet(strSql);
      objDropDownList.DataBind();
      objBanco = null;

      //Adiciona a opção default no dropdownlist.
      ListItem itemDefault = new ListItem();
      itemDefault.Text = "- Nenhum item selecionado -";
      itemDefault.Value = "";
      objDropDownList.Items.Insert(0, itemDefault);
    }
    #endregion
  }
}