using System;

namespace SServiceDesk.Negocio
{
  /// <summary>
  /// Classe PessoaPerfilEstrutura.
  /// </summary>
  public class ClsPessoaPerfilEstrutura
  {
    //Colecao de atributos de PessoaPerfilEstrutura
    private ServiceDesk.Banco.ClsAtributos objAtributos = new ServiceDesk.Banco.ClsAtributos();

    //Atributos de PessoaPerfilEstrutura
    private ServiceDesk.Banco.ClsAtributo objCodigo = new ServiceDesk.Banco.ClsAtributo();
    private ServiceDesk.Banco.ClsAtributo objPessoaCodigo = new ServiceDesk.Banco.ClsAtributo();
    private ServiceDesk.Banco.ClsAtributo objPerfilEstruturaCodigo = new ServiceDesk.Banco.ClsAtributo();

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

    public ServiceDesk.Banco.ClsAtributo PessoaCodigo
    {
      get
      {
        return objPessoaCodigo;
      }
    }

    public ServiceDesk.Banco.ClsAtributo PerfilEmpresaCodigo
    {
      get
      {
        return objPerfilEstruturaCodigo;
      }
    }

    #endregion

    #region Construtor
    public ClsPessoaPerfilEstrutura()
    {
      this.alimentaColecaoCampos();
    }
    #endregion

    #region metodo alimentaColecaoCampos
    /// <summary>
    /// Adiciona todos os atributos de uma ClsPessoaPerfilEstrutura, a coleção de atributos.
    /// </summary>
    private void alimentaColecaoCampos()
    {
      objAtributos.DescricaoTabela = "PessoaPerfilEstrutura";
      objAtributos.NomeTabela = "PessoaPerfilEstrutura";

      objCodigo.Campo = "pessoa_perfil_estrutura_codigo";
      objCodigo.Descricao = "Código";
      objCodigo.CampoIdentificador = true;
      objCodigo.CampoObrigatorio = true;
      objCodigo.Tipo = System.Data.DbType.Int32;
      objAtributos.Add(objCodigo);

      objPessoaCodigo.Campo = "pessoa_codigo";
      objPessoaCodigo.Descricao = "Código da Pessoa";
      objPessoaCodigo.CampoObrigatorio = true;
      objPessoaCodigo.Tipo = System.Data.DbType.Int32;
      objAtributos.Add(objPessoaCodigo);

      objPerfilEstruturaCodigo.Campo = "perfil_estrutura_codigo";
      objPerfilEstruturaCodigo.Descricao = "Código do Perfil da Estrutura";
      objPerfilEstruturaCodigo.CampoObrigatorio = true;
      objPerfilEstruturaCodigo.Tipo = System.Data.DbType.Int32;
      objAtributos.Add(objPerfilEstruturaCodigo);
    }
    #endregion

    #region metodo geraGridView
    /// <summary>
    /// Gera uma nova geraGridView de acordo com a coleção de atributos.
    /// </summary>
    /// <param name="objGridView">geraGridView</param>
    /// <param name="strEstrutura">Codigo da estrutura</param>
    public static void geraGridView(System.Web.UI.WebControls.GridView objGridView, String strEstrutura)
    {
      objGridView.AutoGenerateColumns = false;
      string strSql = "Select pessoa_perfil_estrutura_codigo, PPE.pessoa_codigo, P.aplicacao_codigo, P.tipo_usuario_codigo ";
      strSql += "from PessoaPerfilEstrutura PPE, PerfilEstrutura PE, Perfil P, aplicacao A, pessoa ";
      strSql += "Where PPE.perfil_estrutura_codigo = PE.perfil_estrutura_codigo ";
      strSql += "and PE.perfil_codigo = P.perfil_codigo ";
      strSql += "and PE.estrutura_codigo = 0" + strEstrutura;
      strSql += " AND P.aplicacao_codigo = A.aplicacao_codigo";
      strSql += " AND PPE.pessoa_codigo = pessoa.pessoa_codigo";
      strSql += " ORDER BY A.descricao, pessoa.nome";

      ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
      System.Data.DataSet objDataSet = ServiceDesk.Banco.ClsBanco.geraDataSet(strSql);
      objGridView.DataSource = objDataSet;
      objGridView.DataBind();
      objDataSet.Dispose();
      objDataSet = null;
      objBanco = null;
    }
    #endregion

    #region metodo apagaPerfis
    /// <summary>
    /// Gera uma nova geraGridView de acordo com a coleção de atributos.
    /// </summary>
    /// <param name="strPessoa">String que representa o código da pessoa.</param>
    /// <param name="strEstrutura">String que representa o código da estrutura.</param>		
    public static void apagaPerfis(String strPessoa, String strEstrutura)
    {
      string strSql = "DELETE FROM pessoaperfilestrutura where pessoa_perfil_estrutura_codigo IN ";
      strSql += "(";
      strSql += "SELECT distinct pessoa_perfil_estrutura_codigo from PerfilEstrutura,pessoaPerfilEstrutura ";
      strSql += "where ";
      strSql += "pessoaPerfilEstrutura.pessoa_codigo = " + strPessoa;
      strSql += " and pessoaPerfilEstrutura.perfil_estrutura_codigo  =  PerfilEstrutura.perfil_estrutura_codigo ";
      strSql += " and PerfilEstrutura.estrutura_codigo = " + strEstrutura;
      strSql += ")";

      ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
      objBanco.executaSQL(strSql);
      objBanco = null;
    }
    #endregion

    #region metodo verificaAtribuicaoPerfil
    /// <summary>
    /// Verifica se um determinado perfil está atribuido a um determinado usuário para uma aplicação.
    /// </summary>
    /// <param name="strPessoa">String que representa o código da pessoa.</param>
    /// <param name="strEstrutura">String que representa o código da estrutura.</param>
    /// <param name="strAplicacao">String que representa o código da aplicacao.</param> 
    /// <param name="strTipoUsuario">String que representa o código do tipo de usuario atribuido.</param> 
    /// <returns>Retorna true se a o perfil está atribuido ao usuario. False se não.</returns>
    public static bool verificaAtribuicaoPerfil(String strPessoa, String strEstrutura, String strAplicacao, String strTipoUsuario)
    {
      bool bolRetorno = false;
      
      string strSql = "SELECT pessoa_perfil_estrutura_codigo FROM ";
      strSql += "pessoaPerfilEstrutura PPE, PerfilEstrutura PE, Perfil P ";
      strSql += " WHERE PPE.perfil_estrutura_codigo = PE.perfil_estrutura_codigo ";
      strSql += " AND PE.perfil_codigo = P.perfil_codigo ";
      strSql += " AND PPE.pessoa_codigo = " + strPessoa;
      strSql += " AND PE.estrutura_codigo = " + strEstrutura;
      strSql += " AND P.tipo_usuario_codigo = " + strTipoUsuario;
      strSql += " AND P.aplicacao_codigo = " + strAplicacao;
      System.Data.SqlClient.SqlDataReader dr = ServiceDesk.Banco.ClsBanco.geraDataReader(strSql);

      if (dr.Read())
      {
        bolRetorno = true;
      }

      dr.Dispose();
      dr = null;

      return bolRetorno;

    }
    #endregion

    #region metodo insere
    /// <summary>
    /// Método que insere.
    /// </summary>
    /// <returns>Retorna true ou false. Se o registro foi inserido ou não.</returns>
    public bool insere(out String strMensagem, String strEmpresa)
    {
      strMensagem = String.Empty;
      bool bolRetorno = false;

      if (this.objPessoaCodigo.Valor.Trim() == String.Empty)
      {
        strMensagem = "Favor informar a Pessoa.<br>";
      }

      if (strEmpresa.Trim() == String.Empty)
      {
        strMensagem = strMensagem + "Favor informar a Empresa.<br>";
      }

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
    #endregion

  }
}