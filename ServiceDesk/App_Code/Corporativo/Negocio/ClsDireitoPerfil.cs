using System;
using System.Data;
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
  /// Summary description for ClsDireitoPerfil
  /// </summary>
  public class ClsDireitoPerfil
  {

    //Colecao de atributos de uma aplicação.
    private ServiceDesk.Banco.ClsAtributos objAtributos = new ServiceDesk.Banco.ClsAtributos();

    #region Atributos
    private ServiceDesk.Banco.ClsAtributo objCodigo = new ServiceDesk.Banco.ClsAtributo();
    private ServiceDesk.Banco.ClsAtributo objPerfilCodigo = new ServiceDesk.Banco.ClsAtributo();
    private ServiceDesk.Banco.ClsAtributo objFuncaoCodigo = new ServiceDesk.Banco.ClsAtributo();
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
      set
      {
        this.objCodigo = value;
      }
    }

    public ServiceDesk.Banco.ClsAtributo CodigoPerfil
    {
      get
      {
        return objPerfilCodigo;
      }
      set
      {
        this.objPerfilCodigo = value;
      }
    }

    public ServiceDesk.Banco.ClsAtributo CodigoFuncao
    {
      get
      {
        return objFuncaoCodigo;
      }
      set
      {
        this.objFuncaoCodigo = value;
      }
    }

    #endregion

    #region Construtor
    public ClsDireitoPerfil()
    {
      this.alimentaColecaoCampos();
    }
    #endregion

    #region metodo Construtor da classe com passagem de parametro
    /// <summary>
    /// Construtor da classe
    /// </summary>
    public ClsDireitoPerfil(int intCodigo)
    {
      this.alimentaColecaoCampos();
      this.objCodigo.Valor = intCodigo.ToString();
      ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
      objBanco.alimentaColecao(this.objAtributos);
      objBanco = null;
    }
    #endregion

    #region alimentaColecaoCampos
    /// <summary>
    /// Método que alimenta a coleção de atributos
    /// </summary>
    private void alimentaColecaoCampos()
    {
      objAtributos.NomeTabela = "DireitoPerfil";
      objAtributos.DescricaoTabela = "Direitos de perfil à funcoes e subfuncoes.";

      objCodigo.Campo = "direito_codigo";
      objCodigo.Descricao = "Código";
      objCodigo.CampoIdentificador = true;
      objCodigo.CampoObrigatorio = true;
      objCodigo.Tipo = System.Data.DbType.Int32;
      objAtributos.Add(objCodigo);

      objPerfilCodigo.Campo = "perfil_codigo";
      objPerfilCodigo.Descricao = "Perfil";
      objPerfilCodigo.CampoObrigatorio = true;
      objPerfilCodigo.Tipo = System.Data.DbType.Int32;
      objPerfilCodigo.Tamanho = 255;
      objAtributos.Add(objPerfilCodigo);

      objFuncaoCodigo.Campo = "funcao_codigo";
      objFuncaoCodigo.Descricao = "Função";
      objFuncaoCodigo.CampoObrigatorio = true;
      objFuncaoCodigo.Tipo = System.Data.DbType.Int32;
      objAtributos.Add(objFuncaoCodigo);


    }
    #endregion

    #region metodo insere
    /// <summary>
    /// Método que insere um novo direito.
    /// </summary>
    /// <param name="strMensagem">Mensagem com informação da execução do método.</param>
    /// <returns>Retorna true ou false. Se o registro foi inserido ou não.</returns>
    public bool insere(out String strMensagem)
    {
      strMensagem = String.Empty;
      bool bolRetorno = false;

      ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
      if (objBanco.insereColecao(this.objAtributos))
      {
        strMensagem = "Gravação efetuada com sucesso.";
        bolRetorno = true;
      }
      objBanco = null;

      return bolRetorno;
    }
    #endregion

    #region metodo exclui
    /// <summary>
    /// Método que exclui uma direito
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

    #region metodo apagadireitos
    /// <summary>
    /// 
    /// </summary>
    /// <param name="strCodigoEmpresa"></param>
    /// <param name="strCodigoAplicacao"></param>
    /// <param name="strCodigoPerfil"></param>
    public static void apagaDireitosPerfil(string strCodigoEmpresa, string strCodigoAplicacao, string strCodigoPerfil)
    {
      string strSQL = string.Empty;

        strSQL = "DELETE FROM DireitoPerfil ";
        strSQL += " WHERE perfil_codigo IN ";
        strSQL += " (SELECT P.perfil_codigo ";
        strSQL += " FROM aplicacao A, FuncaoAplicacao FA, perfil P, TipoUsuario TU, PerfilEstrutura PE, EstruturaOrganizacional E, DireitoPerfil D ";
        strSQL += " WHERE";
        strSQL += " A.aplicacao_codigo = '" + strCodigoAplicacao + "'";
        strSQL += " AND A.aplicacao_codigo = FA.aplicacao_codigo";
        strSQL += " AND A.aplicacao_codigo = P.aplicacao_codigo";
        strSQL += " AND P.tipo_usuario_codigo = TU.tipo_usuario_codigo";
        strSQL += " AND P.perfil_codigo = '" + strCodigoPerfil + "'";
        strSQL += " AND P.perfil_codigo = PE.perfil_codigo";
        strSQL += " AND PE.estrutura_codigo = E.estrutura_codigo";
        strSQL += " AND E.estrutura_codigo = '" + strCodigoEmpresa + "'";
        strSQL += " AND D.funcao_codigo = FA.funcao_codigo";
        strSQL += " AND D.perfil_codigo = PE.perfil_codigo )";

      ServiceDesk.Banco.ClsBanco Banco = new ServiceDesk.Banco.ClsBanco();
      try
      {
        Banco.executaSQL(strSQL);
        //	return true;
      }
      catch
      {
        //return false;
      }

    }
    #endregion

    #region Retorna Código do Direito Perfil
      /// <summary>
      /// Retorna Código
      /// </summary>
      /// <param name="intCodigoFuncao">Código da Função</param>
      /// <param name="intCodigoPerfil">Código da Aplicação</param>
      /// <returns>Retorna código do dereito perfil</returns>
      public int GetCodigo(int intCodigoFuncao, int intCodigoPerfil)
      {
          ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
          string strValor = objBanco.retornaValorCampo("DireitoPerfil", "direito_codigo", "funcao_codigo = " + intCodigoFuncao + " and perfil_codigo = " + intCodigoPerfil + "");
          if (strValor != string.Empty) return Convert.ToInt32(strValor.Trim());else return 0;
      }
    #endregion
}
}