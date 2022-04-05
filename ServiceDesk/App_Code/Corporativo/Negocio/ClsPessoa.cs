using System;
using System.Collections;

namespace SServiceDesk.Negocio
{
  /// <summary>
  /// Classe ClsPessoa.
  /// </summary>
  public class ClsPessoa
  {

    //Colecao de atributos da pessoa
    private ServiceDesk.Banco.ClsAtributos objAtributos = new ServiceDesk.Banco.ClsAtributos();

    //Atributos de uma empresa
    private ServiceDesk.Banco.ClsAtributo objCodigo = new ServiceDesk.Banco.ClsAtributo();
    private ServiceDesk.Banco.ClsAtributo objEstruturaCodigo = new ServiceDesk.Banco.ClsAtributo();
    private ServiceDesk.Banco.ClsAtributo objMatricula = new ServiceDesk.Banco.ClsAtributo();
    private ServiceDesk.Banco.ClsAtributo objNome = new ServiceDesk.Banco.ClsAtributo();
    private ServiceDesk.Banco.ClsAtributo objEmpresaCodigo = new ServiceDesk.Banco.ClsAtributo();
    private ServiceDesk.Banco.ClsAtributo objAreaCodigo = new ServiceDesk.Banco.ClsAtributo();
    private ServiceDesk.Banco.ClsAtributo objUnidadeCodigo = new ServiceDesk.Banco.ClsAtributo();
    private ServiceDesk.Banco.ClsAtributo objCentroCustoCodigo = new ServiceDesk.Banco.ClsAtributo();
    private ServiceDesk.Banco.ClsAtributo objCargoCodigo = new ServiceDesk.Banco.ClsAtributo();
    private ServiceDesk.Banco.ClsAtributo objLinhaOnibus = new ServiceDesk.Banco.ClsAtributo();
    private ServiceDesk.Banco.ClsAtributo objTipoUsuarioCodigo = new ServiceDesk.Banco.ClsAtributo();
    private ServiceDesk.Banco.ClsAtributo objDataInicioTrabalho = new ServiceDesk.Banco.ClsAtributo();
    private ServiceDesk.Banco.ClsAtributo objDataFimTrabalho = new ServiceDesk.Banco.ClsAtributo();
    private ServiceDesk.Banco.ClsAtributo objTipoColaborador = new ServiceDesk.Banco.ClsAtributo();
    private ServiceDesk.Banco.ClsAtributo objRamal = new ServiceDesk.Banco.ClsAtributo();
    private ServiceDesk.Banco.ClsAtributo objEmail = new ServiceDesk.Banco.ClsAtributo();
    private ServiceDesk.Banco.ClsAtributo objDataNascimento = new ServiceDesk.Banco.ClsAtributo();
    private ServiceDesk.Banco.ClsAtributo objSexo = new ServiceDesk.Banco.ClsAtributo();
    private ServiceDesk.Banco.ClsAtributo objCpf = new ServiceDesk.Banco.ClsAtributo();
    private ServiceDesk.Banco.ClsAtributo objLogradouro = new ServiceDesk.Banco.ClsAtributo();
    private ServiceDesk.Banco.ClsAtributo objBairro = new ServiceDesk.Banco.ClsAtributo();
    private ServiceDesk.Banco.ClsAtributo objCidade = new ServiceDesk.Banco.ClsAtributo();
    private ServiceDesk.Banco.ClsAtributo objUf = new ServiceDesk.Banco.ClsAtributo();
    private ServiceDesk.Banco.ClsAtributo objCep = new ServiceDesk.Banco.ClsAtributo();
    private ServiceDesk.Banco.ClsAtributo objTelefone = new ServiceDesk.Banco.ClsAtributo();
    private ServiceDesk.Banco.ClsAtributo objPontoOnibus = new ServiceDesk.Banco.ClsAtributo();
    private ServiceDesk.Banco.ClsAtributo objCodigoRede = new ServiceDesk.Banco.ClsAtributo();
    private ServiceDesk.Banco.ClsAtributo objValorHora = new ServiceDesk.Banco.ClsAtributo();
    private ServiceDesk.Banco.ClsAtributo objFlagVip = new ServiceDesk.Banco.ClsAtributo();
    private ServiceDesk.Banco.ClsAtributo objLocalizacaoFisica = new ServiceDesk.Banco.ClsAtributo();
    private ServiceDesk.Banco.ClsAtributo objSenha = new ServiceDesk.Banco.ClsAtributo();
    private ServiceDesk.Banco.ClsAtributo objStatus = new ServiceDesk.Banco.ClsAtributo();
    private ServiceDesk.Banco.ClsAtributo objTipoSangue = new ServiceDesk.Banco.ClsAtributo();
    private ServiceDesk.Banco.ClsAtributo objNomeGuerra = new ServiceDesk.Banco.ClsAtributo();
    private ServiceDesk.Banco.ClsAtributo objFoto = new ServiceDesk.Banco.ClsAtributo();
    private ServiceDesk.Banco.ClsAtributo objCnh = new ServiceDesk.Banco.ClsAtributo();
    private ServiceDesk.Banco.ClsAtributo objDataExpedicaoCnh = new ServiceDesk.Banco.ClsAtributo();
    private ServiceDesk.Banco.ClsAtributo objDataValidadeCnh = new ServiceDesk.Banco.ClsAtributo();

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

    public ServiceDesk.Banco.ClsAtributo EstruturaCodigo
    {
      get
      {
        return objEstruturaCodigo;
      }
    }

    public ServiceDesk.Banco.ClsAtributo Matricula
    {
      get
      {
        return objMatricula;
      }
    }

    public ServiceDesk.Banco.ClsAtributo Nome
    {
      get
      {
        return objNome;
      }
    }
    public ServiceDesk.Banco.ClsAtributo EmpresaCodigo
    {
      get
      {
        return objEmpresaCodigo;
      }
    }
    public ServiceDesk.Banco.ClsAtributo AreaCodigo
    {
      get
      {
        return objAreaCodigo;
      }
    }
    public ServiceDesk.Banco.ClsAtributo UnidadeCodigo
    {
      get
      {
        return objUnidadeCodigo;
      }
    }
    public ServiceDesk.Banco.ClsAtributo CentroCustoCodigo
    {
      get
      {
        return objCentroCustoCodigo;
      }
    }
    public ServiceDesk.Banco.ClsAtributo CargoCodigo
    {
      get
      {
        return objCargoCodigo;
      }
    }
    public ServiceDesk.Banco.ClsAtributo LinhaOnibus
    {
      get
      {
        return objLinhaOnibus;
      }
    }
    public ServiceDesk.Banco.ClsAtributo TipoUsuarioCodigo
    {
      get
      {
        return objTipoUsuarioCodigo;
      }
    }
    public ServiceDesk.Banco.ClsAtributo DataInicioTrabalho
    {
      get
      {
        return objDataInicioTrabalho;
      }
    }
    public ServiceDesk.Banco.ClsAtributo DataFimTrabalho
    {
      get
      {
        return objDataFimTrabalho;
      }
    }
    public ServiceDesk.Banco.ClsAtributo TipoColaborador
    {
      get
      {
        return objTipoColaborador;
      }
    }
    public ServiceDesk.Banco.ClsAtributo Ramal
    {
      get
      {
        return objRamal;
      }
    }
    public ServiceDesk.Banco.ClsAtributo Email
    {
      get
      {
        return objEmail;
      }
    }
    public ServiceDesk.Banco.ClsAtributo DataNascimento
    {
      get
      {
        return objDataNascimento;
      }
    }
    public ServiceDesk.Banco.ClsAtributo Sexo
    {
      get
      {
        return objSexo;
      }
    }
    public ServiceDesk.Banco.ClsAtributo Cpf
    {
      get
      {
        return objCpf;
      }
    }
    public ServiceDesk.Banco.ClsAtributo Logradouro
    {
      get
      {
        return objLogradouro;
      }
    }
    public ServiceDesk.Banco.ClsAtributo Bairro
    {
      get
      {
        return objBairro;
      }
    }
    public ServiceDesk.Banco.ClsAtributo Cidade
    {
      get
      {
        return objCidade;
      }
    }
    public ServiceDesk.Banco.ClsAtributo Uf
    {
      get
      {
        return objUf;
      }
    }
    public ServiceDesk.Banco.ClsAtributo Cep
    {
      get
      {
        return objCep;
      }
    }
    public ServiceDesk.Banco.ClsAtributo Telefone
    {
      get
      {
        return objTelefone;
      }
    }
    public ServiceDesk.Banco.ClsAtributo PontoOnibus
    {
      get
      {
        return objPontoOnibus;
      }
    }
    public ServiceDesk.Banco.ClsAtributo CodigoRede
    {
      get
      {
        return objCodigoRede;
      }
    }
    public ServiceDesk.Banco.ClsAtributo ValorHora
    {
      get
      {
        return objValorHora;
      }
    }
    public ServiceDesk.Banco.ClsAtributo FlagVip
    {
      get
      {
        return objFlagVip;
      }
    }
    public ServiceDesk.Banco.ClsAtributo LocalizacaoFisica
    {
      get
      {
        return objLocalizacaoFisica;
      }
    }
    public ServiceDesk.Banco.ClsAtributo Senha
    {
      get
      {
        return objSenha;
      }
    }
    public ServiceDesk.Banco.ClsAtributo Status
    {
      get
      {
        return objStatus;
      }
    }
    public ServiceDesk.Banco.ClsAtributo TipoSangue
    {
      get
      {
        return objTipoSangue;
      }
    }
    public ServiceDesk.Banco.ClsAtributo NomeGuerra
    {
      get
      {
        return objNomeGuerra;
      }
    }
    public ServiceDesk.Banco.ClsAtributo Foto
    {
      get
      {
        return objFoto;
      }
    }
    public ServiceDesk.Banco.ClsAtributo Cnh
    {
      get
      {
        return objCnh;
      }
    }
    public ServiceDesk.Banco.ClsAtributo DataExpedicaoCnh
    {
      get
      {
        return objDataExpedicaoCnh;
      }
    }
    public ServiceDesk.Banco.ClsAtributo DataValidadeCnh
    {
      get
      {
        return objDataValidadeCnh;
      }
    }


    #endregion

    #region Construtor
    public ClsPessoa()
    {
      this.alimentaColecaoCampos();
    }
    #endregion

    #region metodo Construtor da classe com passagem de parametro
    /// <summary>
    /// Construtor da classe
    /// </summary>
    public ClsPessoa(int intCodigo)
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
    /// Adiciona todos os atributos de uma empresa a coleção de atributos.
    /// </summary>
    public void alimentaColecaoCampos()
    {
      objAtributos.DescricaoTabela = "pessoa";
      objAtributos.NomeTabela = "pessoa";

      objCodigo.Campo = "pessoa_codigo";
      objCodigo.Descricao = "Código";
      objCodigo.CampoIdentificador = true;
      objCodigo.CampoObrigatorio = true;
      objCodigo.Tipo = System.Data.DbType.Int32;
      objAtributos.Add(objCodigo);

      objEstruturaCodigo.Campo = "estrutura_codigo";
      objEstruturaCodigo.Descricao = "Estrutura";
      objEstruturaCodigo.Tipo = System.Data.DbType.Int32;
      objAtributos.Add(objEstruturaCodigo);

      objMatricula.Campo = "matricula";
      objMatricula.Descricao = "Matricula";
      objMatricula.CampoObrigatorio = true;
      objMatricula.Tipo = System.Data.DbType.String;
      objMatricula.Tamanho = 15;
      objAtributos.Add(objMatricula);

      objNome.Campo = "nome";
      objNome.Descricao = "Nome";
      objNome.CampoObrigatorio = true;
      objNome.Tipo = System.Data.DbType.String;
      objNome.Tamanho = 120;
      objAtributos.Add(objNome);

      objEmpresaCodigo.Campo = "estrutura_codigo";
      objEmpresaCodigo.Descricao = "Código da Estrutura Organizacional";
      objEmpresaCodigo.CampoObrigatorio = true;
      objEmpresaCodigo.Tipo = System.Data.DbType.Int32;
      objAtributos.Add(objEmpresaCodigo);

      objAreaCodigo.Campo = "area_codigo";
      objAreaCodigo.Descricao = "Código da área";
      objAreaCodigo.Tipo = System.Data.DbType.String;
      objAreaCodigo.Tamanho = 20;
      objAtributos.Add(objAreaCodigo);

      objUnidadeCodigo.Campo = "unidade_codigo";
      objUnidadeCodigo.Descricao = "Código da unidade";
      objUnidadeCodigo.Tipo = System.Data.DbType.String;
      objUnidadeCodigo.Tamanho = 2;
      objAtributos.Add(objUnidadeCodigo);

      objCentroCustoCodigo.Campo = "centro_custo_codigo";
      objCentroCustoCodigo.Descricao = "Código do centro de custo";
      objCentroCustoCodigo.Tipo = System.Data.DbType.String;
      objCentroCustoCodigo.Tamanho = 20;
      objAtributos.Add(objCentroCustoCodigo);

      objCargoCodigo.Campo = "cargo_codigo";
      objCargoCodigo.Descricao = "Código do cargo";
      objCargoCodigo.Tipo = System.Data.DbType.String;
      objCargoCodigo.Tamanho = 20;
      objAtributos.Add(objCargoCodigo);

      objLinhaOnibus.Campo = "linha_onibus";
      objLinhaOnibus.Descricao = "Linha onibus";
      objLinhaOnibus.Tipo = System.Data.DbType.Int32;
      objAtributos.Add(objLinhaOnibus);

      objTipoUsuarioCodigo.Campo = "tipo_usuario_codigo";
      objTipoUsuarioCodigo.Descricao = "tipo_usuario_codigo";
      objTipoUsuarioCodigo.Tipo = System.Data.DbType.Int32;
      objAtributos.Add(objTipoUsuarioCodigo);

      objDataInicioTrabalho.Campo = "data_inicio_trabalho";
      objDataInicioTrabalho.Descricao = "Data do início do trabalho";
      objDataInicioTrabalho.Tipo = System.Data.DbType.DateTime;
      objAtributos.Add(objDataInicioTrabalho);

      objDataFimTrabalho.Campo = "data_fim_trabalho";
      objDataFimTrabalho.Descricao = "Data do fim do trabalho";
      objDataFimTrabalho.Tipo = System.Data.DbType.DateTime;
      objAtributos.Add(objDataFimTrabalho);

      objTipoColaborador.Campo = "tipo_colaborador";
      objTipoColaborador.Descricao = "Tipo de colaborador";
      objTipoColaborador.Tipo = System.Data.DbType.String;
      objTipoColaborador.Tamanho = 1;
      objAtributos.Add(objTipoColaborador);

      objRamal.Campo = "ramal";
      objRamal.Descricao = "Ramal";
      objRamal.Tipo = System.Data.DbType.String;
      objRamal.Tamanho = 50;
      objAtributos.Add(objRamal);

      objEmail.Campo = "email";
      objEmail.Descricao = "Email";
      objEmail.Tipo = System.Data.DbType.String;
      objEmail.Tamanho = 300;
      objAtributos.Add(objEmail);

      objDataNascimento.Campo = "data_nascimento";
      objDataNascimento.Descricao = "Data Nascimento";
      objDataNascimento.Tipo = System.Data.DbType.DateTime;
      objAtributos.Add(objDataNascimento);

      objSexo.Campo = "sexo";
      objSexo.Descricao = "Sexo";
      objSexo.Tipo = System.Data.DbType.String;
      objSexo.Tamanho = 1;
      objAtributos.Add(objSexo);

      objCpf.Campo = "cpf";
      objCpf.Descricao = "cpf";
      objCpf.Tipo = System.Data.DbType.String;
      objCpf.Tamanho = 20;
      objAtributos.Add(objCpf);

      objLogradouro.Campo = "logradouro";
      objLogradouro.Descricao = "Logradouro";
      objLogradouro.Tipo = System.Data.DbType.String;
      objLogradouro.Tamanho = 255;
      objAtributos.Add(objLogradouro);

      objBairro.Campo = "bairro";
      objBairro.Descricao = "Bairro";
      objBairro.Tipo = System.Data.DbType.String;
      objBairro.Tamanho = 50;
      objAtributos.Add(objBairro);

      objCidade.Campo = "cidade";
      objCidade.Descricao = "cidade";
      objCidade.Tipo = System.Data.DbType.String;
      objCidade.Tamanho = 50;
      objAtributos.Add(objCidade);

      objUf.Campo = "uf";
      objUf.Descricao = "Estado";
      objUf.Tipo = System.Data.DbType.String;
      objUf.Tamanho = 2;
      objAtributos.Add(objUf);

      objCep.Campo = "cep";
      objCep.Descricao = "cep";
      objCep.Tipo = System.Data.DbType.String;
      objCep.Tamanho = 10;
      objAtributos.Add(objCep);

      objTelefone.Campo = "telefone";
      objTelefone.Descricao = "Telefone";
      objTelefone.Tipo = System.Data.DbType.String;
      objTelefone.Tamanho = 50;
      objAtributos.Add(objTelefone);

      objPontoOnibus.Campo = "ponto_onibus";
      objPontoOnibus.Descricao = "ponto de onibus";
      objPontoOnibus.Tipo = System.Data.DbType.Int32;
      objAtributos.Add(objPontoOnibus);

      objCodigoRede.Campo = "codigo_rede";
      objCodigoRede.Descricao = "Codigo da rede";
      objCodigoRede.Tipo = System.Data.DbType.String;
      objCodigoRede.Tamanho = 30;
      objAtributos.Add(objCodigoRede);

      objValorHora.Campo = "valor_hora";
      objValorHora.Descricao = "Hora";
      objValorHora.Tipo = System.Data.DbType.Decimal;
      objAtributos.Add(objValorHora);

      objFlagVip.Campo = "flag_vip";
      objFlagVip.Descricao = "flag_vip";
      objFlagVip.Tipo = System.Data.DbType.String;
      objFlagVip.Tamanho = 1;
      objAtributos.Add(objFlagVip);

      objLocalizacaoFisica.Campo = "localizacao_fisica";
      objLocalizacaoFisica.Descricao = "localizacao_fisica";
      objLocalizacaoFisica.Tipo = System.Data.DbType.String;
      objLocalizacaoFisica.Tamanho = 255;
      objAtributos.Add(objLocalizacaoFisica);

      objSenha.Campo = "senha";
      objSenha.Descricao = "senha";
      objSenha.Tipo = System.Data.DbType.VarNumeric;
      objSenha.Tamanho = 255;
      objAtributos.Add(objSenha);

      objStatus.Campo = "status_codigo";
      objStatus.Descricao = "status";
      objStatus.Tipo = System.Data.DbType.Int32;
      objAtributos.Add(objStatus);

      objTipoSangue.Campo = "tipo_sangue";
      objTipoSangue.Descricao = "tipo_sangue";
      objTipoSangue.Tipo = System.Data.DbType.String;
      objTipoSangue.Tamanho = 3;
      objAtributos.Add(objTipoSangue);

      objNomeGuerra.Campo = "nome_guerra";
      objNomeGuerra.Descricao = "nome de guerra";
      objNomeGuerra.Tipo = System.Data.DbType.String;
      objNomeGuerra.Tamanho = 15;
      objAtributos.Add(objNomeGuerra);

      objFoto.Campo = "foto";
      objFoto.Descricao = "foto";
      objFoto.Tipo = System.Data.DbType.String;
      objFoto.Tamanho = 100;
      objAtributos.Add(objFoto);

      objCnh.Campo = "cnh";
      objCnh.Descricao = "cnh";
      objCnh.Tipo = System.Data.DbType.String;
      objCnh.Tamanho = 20;
      objAtributos.Add(objCnh);

      objDataExpedicaoCnh.Campo = "data_expedicao_cnh";
      objDataExpedicaoCnh.Descricao = "data_expedicao_cnh";
      objDataExpedicaoCnh.Tipo = System.Data.DbType.DateTime;
      objAtributos.Add(objDataExpedicaoCnh);

      objDataValidadeCnh.Campo = "data_validade_cnh";
      objDataValidadeCnh.Descricao = "data_validade_cnh";
      objDataValidadeCnh.Tipo = System.Data.DbType.DateTime;
      objAtributos.Add(objDataValidadeCnh);
    }
    #endregion

    #region Metodo alimentaColecaoPessoa
    /// <summary>
    /// Alimenta a coleção de atributos de uma pessoa
    /// </summary>
    /// <param name="intCodigo">Código da pessoa a ser alimentada</param>
    public void alimentaPessoa(int intCodigo)
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
      String strSql = String.Empty;
      try
      {
        strSql = "SELECT * FROM pessoa";
        strSql += " ORDER BY nome";
        ClsPessoa objPessoa = new ClsPessoa();
        objDropDownList.DataTextField = objPessoa.objNome.Campo;
        objDropDownList.DataValueField = objPessoa.objCodigo.Campo;
        System.Data.SqlClient.SqlDataReader objDataReader = ServiceDesk.Banco.ClsBanco.geraDataReader(strSql);
        objDropDownList.DataSource = objDataReader;
        objDropDownList.DataBind();
        objDataReader.Dispose();
        objDataReader = null;
        objPessoa = null;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }
    #endregion

    #region metodo geraDropDownList
    /// <summary>
    /// Gera uma nova dropDonwlist com as pessoas da empresa passada como parâmetro..
    /// </summary>
    /// <param name="objDropDownlist">Objeto do tipo dropdownlist</param>
    /// <param name="strEstrutura">Codigo da estrutura</param>
    public static void geraDropDownList(System.Web.UI.WebControls.DropDownList objDropDownlist, String strEstrutura)
    {
      try
      {
        string strSql = "Select pessoa_codigo, matricula, nome ";
        strSql += "from pessoa P ";
        strSql += "Where P.estrutura_codigo = '" + strEstrutura + "'";
        strSql += " ORDER BY P.nome";

        ClsPessoa objPessoa = new ClsPessoa();
        ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
        objDropDownlist.DataTextField = objPessoa.objNome.Campo;
        objDropDownlist.DataValueField = objPessoa.objCodigo.Campo;
        System.Data.DataSet objDataSet = ServiceDesk.Banco.ClsBanco.geraDataSet(strSql);
        objDropDownlist.DataSource = objDataSet;
        objDropDownlist.DataBind();

        objDataSet.Dispose();
        objDataSet = null;
        objBanco = null;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }
    #endregion

    #region metodo geraDropDownList
    /// <summary>
    /// Método que retorna a empresa que o usuário trabalha
    /// </summary>
    /// <param name="strCodigoPessoa">Código da pessoa</param>
    /// <returns>String</returns>
      public String GetEmpresaUsuario(String strCodigoPessoa)
      {
          try
          {
              System.Data.SqlClient.SqlDataReader dr;

              String strSql = "SELECT E.estrutura_codigo FROM EstruturaOrganizacional E, Pessoa P ";
              strSql += "WHERE P.pessoa_codigo = " + strCodigoPessoa.Trim() + " AND E.estrutura_codigo = P.estrutura_codigo ";
              strSql += " ORDER BY P.nome";

              dr = ServiceDesk.Banco.ClsBanco.geraDataReader(strSql);
              if (dr.Read())
              {
                  String strCodigoEmpresa = dr["estrutura_codigo"].ToString();
                  dr.Dispose();
                  return strCodigoEmpresa;
              }
              else
              {
                  dr.Dispose();
                  return String.Empty;
              }
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

        strSql = "SELECT * FROM pessoa";
        strSql += " ORDER BY nome";
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

    #region Metodo geraGridViewBusca
    /// <summary>
    /// 
    /// </summary>
    /// <param name="objGridView"></param>
    /// <param name="strNome"></param>
    /// <param name="strMatricula"></param>
    /// <param name="intEstrutura"></param>
    /// <param name="intUsuarioTipo"></param>
    public static void geraGridViewBusca(System.Web.UI.WebControls.GridView objGridView, String strNome, String strMatricula, int intEstrutura, int intUsuarioTipo)
    {
      String strSql = String.Empty;
      
      if (strNome != String.Empty)
      {
        strSql += " WHERE nome LIKE '%" + strNome + "%'";
      }
      if (strMatricula != String.Empty)
      {
        if (strSql == String.Empty)
        {
          strSql = " WHERE";
        }
        else
        {
          strSql += " AND";
        }
        strSql += " matricula = '" + strMatricula + "'";
      }
      if (intUsuarioTipo > 0)
      {
        if (strSql == String.Empty)
        {
          strSql = " WHERE";
        }
        else
        {
          strSql += " AND";
        }
        strSql += " tipo_usuario_codigo = " + intUsuarioTipo.ToString();
      }
      if (intEstrutura > 0)
      {
        if (strSql == String.Empty)
        {
          strSql = " WHERE";
        }
        else
        {
          strSql += " AND";
        }
        strSql += " estrutura_codigo = " + intEstrutura.ToString();
      }
      strSql += " ORDER BY nome";
      strSql = "SELECT * FROM pessoa " + strSql;
      System.Data.DataSet objDataSet = ServiceDesk.Banco.ClsBanco.geraDataSet(strSql);
      objGridView.DataSource = objDataSet;
      objGridView.DataBind();
      objDataSet.Dispose();
      objDataSet = null;

    }
    #endregion

    #region metodo geraDetailsView
    /// <summary>
    /// Gera uma novo DetailsView de acordo com a coleção de atributos.
    /// </summary>
    /// <param name="objDetailsView">geraDetailsView</param>
    public static void geraDetailsView(System.Web.UI.WebControls.DetailsView objDetailsView, int intCodigo)
    {
      try
      {
        objDetailsView.AutoGenerateRows = false;
        ClsPessoa objPessoa = new ClsPessoa();
        objPessoa.Codigo.Valor = intCodigo.ToString();
        ServiceDesk.Controle.ClsDetailsView.geraDetailsView(objDetailsView, objPessoa.objAtributos, true);
        objPessoa = null;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }
    #endregion

    #region metodo insere
    /// <summary>
    /// Método que insere uma nova empresa.
    /// </summary>
    /// <param name="strMensagem">Mensagem com informação da execução do método.</param>
    /// <returns>Retorna true ou false. Se o registro foi inserido ou não.</returns>
    public bool insere(out String strMensagem)
    {
      try
      {
        strMensagem = String.Empty;
        bool bolRetorno = false;

        if (this.objMatricula.Valor.Trim() == String.Empty)
        {
          strMensagem = "Favor informar a Matrícula da Pessoa.<br>";
        }
        if (this.objNome.Valor.Trim() == String.Empty)
        {
          strMensagem = "Favor informar a Nome da Pessoa.<br>";
        }

        if (strMensagem == String.Empty)
        {
          ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
          if (objBanco.insereColecao(this.objAtributos))
          {
            strMensagem = "Pessoa inserida com sucesso.";
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
    /// Método que altera uma pessoa
    /// </summary>
    /// <returns>Retorna true ou false. Se o registro foi alterado ou não.</returns>
    public bool altera(out String strMensagem)
    {
      try
      {
        strMensagem = String.Empty;
        bool bolRetorno = false;

        if (this.objMatricula.Valor.Trim() == String.Empty)
        {
          strMensagem = "Favor informar a Matrícula da Pessoa.<br>";
        }
        if (this.objNome.Valor.Trim() == String.Empty)
        {
          strMensagem = "Favor informar a Nome da Pessoa.<br>";
        }
        if (strMensagem == String.Empty)
        {
          ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
          if (objBanco.alteraColecao(this.objAtributos))
          {
            excluiRelacaoPerfilEstrutura();
            strMensagem = "Pessoa atualizada com sucesso.";
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
    /// Método que exclui uma pessoa
    /// </summary>
    /// <returns>Retorna true ou false. Se o registro foi excluido ou não.</returns>
    public bool exclui(out String strMensagem)
    {
      try
      {

        //Valida a exclusão.
        if (ServiceDesk.Negocio.ClsIdentificador.ValidaExclusao(objCodigo.Campo, objCodigo.Valor.Trim(), out strMensagem, true, false, objAtributos.NomeTabela.Trim()) == false) return false;

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

    #region Metodo excluiRelacaoPerfilEstrutura
    /// <summary>
    /// 
    /// </summary>
    private void excluiRelacaoPerfilEstrutura()
    {
      String strSql = String.Empty;

      strSql = "DELETE FROM pessoaperfilestrutura";
      strSql += " WHERE pessoaperfilestrutura.pessoa_codigo = 0" + this.objCodigo.Valor.Trim();
      strSql += " AND pessoa_perfil_estrutura_codigo NOT IN (";
      strSql += " SELECT pessoa_perfil_estrutura_codigo";
      strSql += " FROM pessoaperfilestrutura, perfilestrutura";
      strSql += " WHERE pessoaperfilestrutura.perfil_estrutura_codigo = perfilestrutura.perfil_estrutura_codigo";
      strSql += " AND pessoaperfilestrutura.pessoa_codigo = 0" + this.objCodigo.Valor.Trim();
      strSql += " AND perfilestrutura.estrutura_codigo = 0" + this.objEstruturaCodigo.Valor.Trim() +")";

      ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
      objBanco.executaSQL(strSql);
      objBanco = null;

    }
    #endregion

    #region Metodo existeMatricula
    /// <summary>
    /// 
    /// </summary>
    public bool existeMatricula()
    {
      bool bolRetorno = false;
      String strRetorno = String.Empty;

      ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
      strRetorno = objBanco.retornaValorCampo("pessoa", "pessoa_codigo", " LOWER(matricula) = '" + this.objMatricula.Valor.ToLower() + "'AND pessoa_codigo <> " + this.Codigo.Valor);
      if (strRetorno != String.Empty)
      {
        bolRetorno = true;
      }
      objBanco = null;

      return bolRetorno;
    }
    #endregion

    #region Metodo existeMatricula
    /// <summary>
    /// 
    /// </summary>
    /// <param name="strMatricula"></param>
    /// <returns></returns>
    public static bool existeMatricula(String strMatricula)
    {
      bool bolRetorno = false;
      String strRetorno = String.Empty;

      ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
      strRetorno = objBanco.retornaValorCampo("pessoa", "pessoa_codigo", " LOWER(matricula) = '" + strMatricula.ToLower() + "'");
      if (strRetorno != String.Empty)
      {
        bolRetorno = true;
      }
      objBanco = null;
      return bolRetorno;
    }
    #endregion

    #region metodo retornaListaPorItem
    /// <summary>
    /// Retorna um Array de objetos Prioridade
    /// </summary>
    /// <returns></returns>
    public static ArrayList retornaListaPorItem(String strTabela)
    {
      try
      {
        String strSql = String.Empty;
        ArrayList arlPessoa = new ArrayList();

        strSql = "SELECT DISTINCT(pessoa_codigo_proprietario) FROM " + strTabela;
        strSql += " WHERE pessoa_codigo_proprietario is not null";

        System.Data.SqlClient.SqlDataReader objReader = ServiceDesk.Banco.ClsBanco.geraDataReader(strSql);
        while (objReader.Read())
        {
          ClsPessoa objPessoa = new ClsPessoa(Convert.ToInt32(objReader["pessoa_codigo_proprietario"].ToString()));
          arlPessoa.Add(objPessoa);
          objPessoa = null;
        }

        objReader.Dispose();
        objReader = null;

        return arlPessoa;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }
    #endregion

  }
}