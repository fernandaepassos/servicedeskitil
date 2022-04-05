using System;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.WebControls;

namespace SServiceDesk.Negocio
{
  /// <summary>
  /// Classe Estrutura Organizacional
  /// </summary>
  public class ClsEstruturaOrganizacional
  {
    #region Declarações
    private ServiceDesk.Banco.ClsAtributos objAtributos = new ServiceDesk.Banco.ClsAtributos();
    #endregion

    #region Atributos
    private ServiceDesk.Banco.ClsAtributo objCodigo = new ServiceDesk.Banco.ClsAtributo();
    private ServiceDesk.Banco.ClsAtributo objTipoEstruturaCodigo = new ServiceDesk.Banco.ClsAtributo();
    private ServiceDesk.Banco.ClsAtributo objCodigoEstrutura = new ServiceDesk.Banco.ClsAtributo();
    private ServiceDesk.Banco.ClsAtributo objDescricao = new ServiceDesk.Banco.ClsAtributo();
    private ServiceDesk.Banco.ClsAtributo objResponsavel = new ServiceDesk.Banco.ClsAtributo();
    private ServiceDesk.Banco.ClsAtributo objEstruturaSuperior = new ServiceDesk.Banco.ClsAtributo();
    private ServiceDesk.Banco.ClsAtributo objChave = new ServiceDesk.Banco.ClsAtributo();
    private ServiceDesk.Banco.ClsAtributo objStatus = new ServiceDesk.Banco.ClsAtributo();
    private ServiceDesk.Banco.ClsAtributo objSigla = new ServiceDesk.Banco.ClsAtributo();
    private ServiceDesk.Banco.ClsAtributo objEndereco = new ServiceDesk.Banco.ClsAtributo();
    private ServiceDesk.Banco.ClsAtributo objTelefone = new ServiceDesk.Banco.ClsAtributo();
    private ServiceDesk.Banco.ClsAtributo objFax = new ServiceDesk.Banco.ClsAtributo();
    private ServiceDesk.Banco.ClsAtributo objCnpj = new ServiceDesk.Banco.ClsAtributo();
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

    public ServiceDesk.Banco.ClsAtributo TipoEstruturaCodigo
    {
      get
      {
        return objTipoEstruturaCodigo;
      }
      set
      {
        this.objTipoEstruturaCodigo = value;
      }
    }

    public ServiceDesk.Banco.ClsAtributo CodigoEstrutura
    {
      get
      {
        return objCodigoEstrutura;
      }
      set
      {
        this.objCodigoEstrutura = value;
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

    public ServiceDesk.Banco.ClsAtributo Responsavel
    {
      get
      {
        return objResponsavel;
      }
      set
      {
        this.objResponsavel = value;
      }
    }

    public ServiceDesk.Banco.ClsAtributo EstruturaSuperior
    {
      get
      {
        return objEstruturaSuperior;
      }
      set
      {
        this.objEstruturaSuperior = value;
      }
    }

    public ServiceDesk.Banco.ClsAtributo Chave
    {
      get
      {
        return objChave;
      }
      set
      {
        this.objChave = value;
      }
    }

    public ServiceDesk.Banco.ClsAtributo Status
    {
      get
      {
        return objStatus;
      }
      set
      {
        this.objStatus = value;
      }
    }

    public ServiceDesk.Banco.ClsAtributo Sigla
    {
      get
      {
        return objSigla;
      }
      set
      {
        this.objSigla = value;
      }
    }

    public ServiceDesk.Banco.ClsAtributo Endereco
    {
      get
      {
        return objEndereco;
      }
    }

    public ServiceDesk.Banco.ClsAtributo Telefone
    {
      get
      {
        return objTelefone;
      }
    }

    public ServiceDesk.Banco.ClsAtributo Fax
    {
      get
      {
        return objFax;
      }
    }

    public ServiceDesk.Banco.ClsAtributo Cnpj
    {
      get
      {
        return objCnpj;
      }
    }

    #endregion

    #region Contrutor
    /// <summary>
    /// Contrutor da Classe
    /// </summary>
    public ClsEstruturaOrganizacional()
    {
      this.alimentaColecaoCampos();
    }
    #endregion

    #region metodo Construtor da classe com passagem de parametro
    /// <summary>
    /// Construtor da classe
    /// </summary>
    public ClsEstruturaOrganizacional(int intCodigo)
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
      objAtributos.NomeTabela = "EstruturaOrganizacional";
      objAtributos.DescricaoTabela = "EstruturaOrganizacional";

      objCodigo.Campo = "estrutura_codigo";
      objCodigo.Descricao = "Código";
      objCodigo.CampoIdentificador = true;
      objCodigo.CampoObrigatorio = true;
      objCodigo.Tipo = System.Data.DbType.Int32;
      objAtributos.Add(objCodigo);

      objTipoEstruturaCodigo.Campo = "tipo_estrutura_codigo";
      objTipoEstruturaCodigo.Descricao = "Código do Tipo de Estrutura";
      objTipoEstruturaCodigo.CampoObrigatorio = true;
      objTipoEstruturaCodigo.Tipo = System.Data.DbType.Int32;
      objAtributos.Add(objTipoEstruturaCodigo);

      objCodigoEstrutura.Campo = "codigo";
      objCodigoEstrutura.Descricao = "Código";
      objCodigoEstrutura.Tipo = System.Data.DbType.String;
      objAtributos.Add(objCodigoEstrutura);

      objDescricao.Campo = "descricao";
      objDescricao.Descricao = "Descrição";
      objDescricao.CampoObrigatorio = true;
      objDescricao.Tipo = System.Data.DbType.String;
      objDescricao.Tamanho = 255;
      objAtributos.Add(objDescricao);

      objEstruturaSuperior.Campo = "estrutura_superior_codigo";
      objEstruturaSuperior.Descricao = "Estrutura Superior";
      objEstruturaSuperior.CampoObrigatorio = true;
      objEstruturaSuperior.Tipo = System.Data.DbType.Int32;
      objAtributos.Add(objEstruturaSuperior);

      objChave.Campo = "chave";
      objChave.Descricao = "Chave de Tratamento";
      objChave.CampoObrigatorio = true;
      objChave.Tipo = System.Data.DbType.String;
      objChave.Tamanho = 100;
      objAtributos.Add(objChave);

      objStatus.Campo = "status_codigo";
      objStatus.Descricao = "Código do Status";
      objStatus.CampoObrigatorio = true;
      objStatus.Tipo = System.Data.DbType.Int32;
      objAtributos.Add(objStatus);

      objSigla.Campo = "sigla";
      objSigla.Descricao = "Sigla";
      objSigla.CampoObrigatorio = true;
      objSigla.Tipo = System.Data.DbType.String;
      objSigla.Tamanho = 100;
      objAtributos.Add(objSigla);

      objEndereco.Campo = "endereco";
      objEndereco.Descricao = "Endereço";
      objEndereco.Tipo = System.Data.DbType.String;
      objEndereco.Tamanho = 200;
      objAtributos.Add(objEndereco);

      objTelefone.Campo = "telefone";
      objTelefone.Descricao = "Telefone";
      objTelefone.Tipo = System.Data.DbType.String;
      objTelefone.Tamanho = 12;
      objAtributos.Add(objTelefone);

      objFax.Campo = "fax";
      objFax.Descricao = "Fax";
      objFax.Tipo = System.Data.DbType.String;
      objFax.Tamanho = 12;
      objAtributos.Add(objFax);

      objCnpj.Campo = "cnpj";
      objCnpj.Descricao = "Cnpj";
      objCnpj.Tipo = System.Data.DbType.String;
      objCnpj.Tamanho = 14;
      objAtributos.Add(objCnpj);

      objResponsavel.Campo = "responsavel_codigo";
      objResponsavel.Descricao = "Responsável pelo nível da estrutura";
      objResponsavel.Tipo = System.Data.DbType.Int32;
      objAtributos.Add(objResponsavel);

    }
    #endregion

    #region metodo insere
    /// <summary>
    /// Método que insere uma nova estrutura organizacional.
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
          strMensagem = "Favor informar a Descrição da Estrutura Organizacional.<br />";

        if (this.objTipoEstruturaCodigo.Valor.Trim() == String.Empty)
          strMensagem += "Favor informar o tipo da Estrutura Organizacional.<br />";

        if (validaCNPJ(this.objCnpj.Valor) == false)
          strMensagem += "O CNPJ informado é inválido.";

        if (strMensagem == String.Empty)
        {
          if (VerificaExisteDescricao() == false)
          {
            ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
            if (objBanco.insereColecao(this.objAtributos))
            {
              strMensagem = "Estrutura Organizacional inserida com sucesso.";
              bolRetorno = true;
            }
            objBanco = null;
          }
          else
            strMensagem = "Já existe um item cadastrado com esta descrição.";
        }

        return bolRetorno;
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
      String strSql = String.Empty;
      try
      {
        strSql = "SELECT * FROM estruturaorganizacional";
        strSql += " ORDER BY descricao";
        System.Data.DataSet objDataSet = ServiceDesk.Banco.ClsBanco.geraDataSet(strSql);
        objGridView.DataSource = objDataSet;
        objGridView.DataBind();
        objDataSet.Dispose();
        objDataSet = null;
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
    /// <param name="bolCondicao">Condição para verificar se será utilizado a cláusula Where</param>
    public static void geraGridView(System.Web.UI.WebControls.GridView objGridView, ClsEstruturaOrganizacional objEstruturaOrganizacional, bool bolCondicao)
    {
      try
      {
        String strSql = String.Empty;
        objGridView.AutoGenerateColumns = false;
        ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
        strSql = objBanco.montaQuery(objEstruturaOrganizacional.objAtributos, bolCondicao);
        strSql += " ORDER BY descricao";
        System.Data.DataSet objDataSet = ServiceDesk.Banco.ClsBanco.geraDataSet(strSql);
        objGridView.DataSource = objDataSet;
        objGridView.DataBind();
        objDataSet.Dispose();
        objDataSet = null;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }
    #endregion

    #region metodo geraDetailsView
    /// <summary>
    /// Gera uma novo DetailsView de acordo com a coleção de atributos.
    /// </summary>
    /// <param name="objGridView">geraDetailsView</param>
    public static void geraDetailsView(System.Web.UI.WebControls.DetailsView objDetailsView, int intCodigo)
    {
      try
      {
        objDetailsView.AutoGenerateRows = false;
        ClsEstruturaOrganizacional objEstruturaOrganizacional = new ClsEstruturaOrganizacional();
        objEstruturaOrganizacional.Codigo.Valor = intCodigo.ToString();
        ServiceDesk.Controle.ClsDetailsView.geraDetailsView(objDetailsView, objEstruturaOrganizacional.objAtributos, true);
        objEstruturaOrganizacional = null;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }
    #endregion

    #region metodo geraGridViewQuery
    /// <summary>
    /// Gera o GridView de acordo com a Query passada
    /// </summary>
    /// <param name="objGridView">nome do GridView</param>
    /// <param name="strSQL">Sript SQL</param>
    public static bool geraGridViewQuery(System.Web.UI.WebControls.GridView objGridView, String strSQL)
    {
      try
      {
        System.Data.SqlClient.SqlDataReader dr;

        objGridView.AutoGenerateColumns = false;

        dr = ServiceDesk.Banco.ClsBanco.geraDataReader(strSQL);
        if (dr.Read())
        {
          objGridView.DataSource = ServiceDesk.Banco.ClsBanco.geraDataReader(strSQL);
          objGridView.DataBind();
          return true;
        }
        else
        {
          return false;
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }
    #endregion

    #region metodo altera
    /// <summary>
    /// Método que altera uma estrutura organizacional
    /// </summary>
    /// <returns>Retorna true ou false. Se o registro foi alterado ou não.</returns>
    public bool altera(out String strMensagem)
    {
      try
      {
        strMensagem = String.Empty;
        bool bolRetorno = false;

        if (this.objDescricao.Valor.Trim() == String.Empty)
          strMensagem = "Favor informar a Descrição da Estrutura Organizacional.<br />";

        if (this.objTipoEstruturaCodigo.Valor.Trim() == String.Empty)
          strMensagem += "Favor informar o tipo da Estrutura Organizacional.<br />";

        if (validaCNPJ(this.objCnpj.Valor) == false)
          strMensagem += "O CNPJ informado é inválido.";

        if (strMensagem == String.Empty)
        {
          if (VerificaExisteDescricao() == false)
          {
            ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
            if (objBanco.alteraColecao(this.objAtributos))
            {
              strMensagem = "Estrutura Organizacional atualizada com sucesso.";
              bolRetorno = true;
            }
            objBanco = null;
          }
          else
            strMensagem = "Já existe um item cadastrado com esta descrição.";
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
    /// Método que exclui uma estrutura organizacional
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

    #region metodo geraDropDownList
    /// <summary>
    /// Gera um novo DropDownList de acordo com a coleção de atributos.
    /// </summary>
    /// <param name="objDropDownList">DropDownList</param>
    public static void geraDropDownList(System.Web.UI.WebControls.DropDownList objDropDownList)
    {
      String strSql = String.Empty;
      try
      {
        strSql = "SELECT * FROM estruturaorganizacional";
        strSql += " ORDER BY descricao";
        ClsEstruturaOrganizacional objEstruturaOrganizacional = new ClsEstruturaOrganizacional();
        objDropDownList.DataTextField = objEstruturaOrganizacional.objDescricao.Campo;
        objDropDownList.DataValueField = objEstruturaOrganizacional.objCodigo.Campo;

        System.Data.SqlClient.SqlDataReader objDataReader = ServiceDesk.Banco.ClsBanco.geraDataReader(strSql);
        objDropDownList.DataSource = objDataReader;
        objDropDownList.DataBind();
        objDataReader.Dispose();
        objDataReader = null;
        objEstruturaOrganizacional = null;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }
    #endregion

    #region metodo geraDropDownList
    /// <summary>
    /// Gera um novo DropDownList de acordo com a coleção de atributos.
    /// </summary>
    /// <param name="objDropDownList">DropDownList</param>
    public static void geraDropDownListPorEmpresa(System.Web.UI.WebControls.DropDownList objDropDownList, int intEstruturaTipoCodigo)
    {
      String strSql = String.Empty;
      try
      {
        strSql = "SELECT * FROM estruturaorganizacional";
        strSql += " WHERE tipo_estrutura_codigo = " + intEstruturaTipoCodigo.ToString();
        strSql += " ORDER BY descricao";
        ClsEstruturaOrganizacional objEstruturaOrganizacional = new ClsEstruturaOrganizacional();
        objDropDownList.DataTextField = objEstruturaOrganizacional.objDescricao.Campo;
        objDropDownList.DataValueField = objEstruturaOrganizacional.objCodigo.Campo;

        System.Data.SqlClient.SqlDataReader objDataReader = ServiceDesk.Banco.ClsBanco.geraDataReader(strSql);
        objDropDownList.DataSource = objDataReader;
        objDropDownList.DataBind();
        objDataReader.Dispose();
        objDataReader = null;
        objEstruturaOrganizacional = null;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }
    #endregion

    #region metodo geraDropDownList
    /// <summary>
    /// Método que gera a dropdownlist de estrutura organizacionais pai do formulario.
    /// </summary>
    /// <returns>.</returns>
    /// 
    public static void geraDropDownList(System.Web.UI.WebControls.DropDownList objDropDownList, string campoChave, string campoExibicao)
    {
      try
      {
        ClsEstruturaOrganizacional objEstruturaOrganizacional = new ClsEstruturaOrganizacional();
        objEstruturaOrganizacional.alimentaColecaoCampos();
        objDropDownList.DataTextField = campoExibicao;
        objDropDownList.DataValueField = campoChave;
        ServiceDesk.Controle.ClsDropDownList.geraDropDownList(objDropDownList, objEstruturaOrganizacional.objAtributos);

        //Adiciona a opção default no dropdownlist.
        ListItem itemDefault = new ListItem();
        itemDefault.Text = "- Nenhum item selecionado -";
        itemDefault.Value = "";
        objDropDownList.Items.Insert(0, itemDefault);
        itemDefault = null;

        objEstruturaOrganizacional = null;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }
    #endregion

    #region metodo geraDropDownListExcecao
    /// <summary>
    /// Gera um novo DropDownList de acordo com a coleção de atributos exceto a estrutura organizacional passada no parametro.
    /// </summary>
    /// <param name="objDropDownList">DropDownList</param>
    /// <param name="intCodigoAplicacao">Código da Aplicação</param>
    public static void geraDropDownListExcecao(System.Web.UI.WebControls.DropDownList objDropDownList, int intCodigo)
    {
      try
      {
        string strSql = string.Empty;
        strSql = "SELECT estrutura_codigo, descricao ";
        strSql = strSql + " FROM estruturaorganizacional";
        strSql = strSql + " WHERE estrutura_codigo <> 0" + intCodigo.ToString();

        ClsEstruturaOrganizacional objEstruturaOrganizacional = new ClsEstruturaOrganizacional();

        objDropDownList.DataTextField = objEstruturaOrganizacional.objDescricao.Campo;
        objDropDownList.DataValueField = objEstruturaOrganizacional.objCodigo.Campo;

        System.Data.SqlClient.SqlDataReader objDataReader = ServiceDesk.Banco.ClsBanco.geraDataReader(strSql);

        objDropDownList.DataSource = objDataReader;
        objDropDownList.DataBind();

        objDataReader.Dispose();
        objDataReader = null;

        objEstruturaOrganizacional = null;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }
    #endregion

    #region atualizaChave
    /// <summary>
    /// <para>Função que atualiza as chaves dos subitens para um determinado item.</para>
    /// <para>Data: 12/05/2005.</para>
    /// <para>Autor: Sylvio Neto (Adaptação de rotina existente no projetos).</para>
    /// </summary>
    /// <param name="numitm"><para>(int) Número identificador do item.</para></param>
    /// <param name="chave"><para>(string) Chave do item pai.</para></param>
    private static void atualizaChave(int numitm, string chave)
    {
      try
      {
        //Atualizando a chave do item
        string strSql = "UPDATE estruturaorganizacional SET ";
        if (chave != string.Empty)
        {
          strSql = strSql + " chave = '" + chave + "," + numitm.ToString() + "'";
        }
        else
        {
          strSql = strSql + " chave ='" + numitm.ToString() + "'";
        }
        strSql = strSql + " WHERE estrutura_codigo = " + numitm.ToString();


        ServiceDesk.Banco.ClsBanco banco = new ServiceDesk.Banco.ClsBanco();
        banco.executaSQL(strSql);
        banco = null;

        if (chave == string.Empty)
        {
          chave = numitm.ToString();
        }
        else
        {
          chave = chave + "," + numitm;
        }

        //Verificando se o item possui filhos
        strSql = "SELECT estrutura_codigo FROM estruturaorganizacional";
        strSql = strSql + " WHERE estrutura_superior_codigo = 0" + numitm.ToString();
        System.Data.SqlClient.SqlDataReader objReader = ServiceDesk.Banco.ClsBanco.geraDataReader(strSql);
        while (objReader.Read())
        {
          atualizaChave(Convert.ToInt32(objReader["estrutura_codigo"]), chave);
        }
        objReader.Dispose();
        objReader = null;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }
    #endregion

    #region alimentaColecao
    /// <summary>
    /// Alimenta a coleção de atributos de uma Aplicacao
    /// </summary>
    /// <param name="intCodigo">Código da aplicacao a ser alimentada</param>
    public void alimentaAplicacao(int intCodigo)
    {
      try
      {
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

    #region getDescricaoSituacao
    /// <summary>
    /// Retorna a descrição de uma Situacao
    /// </summary>
    /// <param name="strAbreviacaoSituacao"></param>
    /// <returns></returns>
    public static String getDescricaoSituacao(String strAbreviacaoSituacao)
    {
      try
      {
        String strRetorno = String.Empty;

        switch (strAbreviacaoSituacao.Trim())
        {
          case "A":
            {
              strRetorno = "Ativa";
              break;
            }
          case "I":
            {
              strRetorno = "Inativa";
              break;
            }
          default:
            {
              strRetorno = "";
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

    #region Retorna o código da estrutura
    /// <summary>
    /// Retorna o código da estrutura por parâmentro do código de rede
    /// </summary>
    /// <param name="strCodigoUsuario">Código do usuário</param> 
    /// <returns>Retorna o código da estrutura</returns>
    public static string GetCodigoEstrutura(string strCodigoUsuario)
    {
      ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
      string strValor = objBanco.retornaValorCampo("EstruturaOrganizacional EO, Pessoa P", "EO.estrutura_codigo", "EO.estrutura_codigo = P.estrutura_codigo and P.pessoa_codigo = " + Convert.ToInt32(strCodigoUsuario.Trim()) + "");
      objBanco = null;
      if (strValor != string.Empty) return strValor;
      else return string.Empty;
    }
    #endregion

    #region VerificaExisteDescricao
    /// <summary>
    /// Método que retorna um registro caso exista uma descrição de estrutura organizacional
    /// já cadastrada no banco.
    /// </summary>
    /// <param name="strSigla">Descrição da estrutura</param>
    /// <returns>Retorna True ou False</returns>
    public bool VerificaExisteDescricao()
    {
      String strSql = String.Empty;
      bool bolRetorno = false;
      try
      {
        strSql = "select estrutura_codigo from estruturaorganizacional where descricao = '" + this.objDescricao.Valor.Trim() + "' AND estrutura_codigo <> " + this.objCodigo.Valor.Trim();
        System.Data.SqlClient.SqlDataReader objSqlDataReader = ServiceDesk.Banco.ClsBanco.geraDataReader(strSql);

        if (objSqlDataReader.Read())
          bolRetorno = true;

        objSqlDataReader.Close();
        objSqlDataReader = null;
      }
      catch (Exception ex)
      {
        throw ex;
      }
      return bolRetorno;
    }
    #endregion

    #region validaCNPJ
    /// <summary>
    /// Método de validação de CNPJ
    /// </summary>
    /// <param name="CNPJ">Número do CNPJ à validar, sem a máscara</param>
    /// <returns>Retorna true se o CNPJ for válido</returns>
    public bool validaCNPJ(string CNPJ)
    {
      if (CNPJ.Trim().Length != 14)
        return false;

      int sum = 0;
      int mult = 5;

      for (int i = 0; i < 12; i++)
      {
        sum += (Convert.ToInt32(CNPJ.Substring(i, 1)) * mult--);
        if (mult == 1)
          mult = 9;
      }
      int auxRest = 11 - (sum % 11);
      if (auxRest >= 10)
        auxRest = 0;
      int rest1 = auxRest;
      sum = 0;
      mult = 6;

      for (int i = 0; i < 13; i++)
      {
        sum += (Convert.ToInt32(CNPJ.Substring(i, 1)) * mult--);
        if (mult == 1)
          mult = 9;
      }
      auxRest = 11 - (sum % 11);
      if (auxRest >= 10)
        auxRest = 0;

      int rest2 = auxRest;
      int verificaDigitoUm = Convert.ToInt32(CNPJ.Substring(12, 1));
      bool digitoUmOK = (rest1 == verificaDigitoUm);
      int verificaDigitoDois = Convert.ToInt32(CNPJ.Substring(13, 1));
      bool digitoDoisOK = (rest2 == verificaDigitoDois);

      if (digitoUmOK && digitoDoisOK)
        return true;
      else
        return false;
    }
    #endregion
  }
}