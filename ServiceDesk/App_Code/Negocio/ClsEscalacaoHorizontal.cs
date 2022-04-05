using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ServiceDesk.Negocio;

/// <summary>
/// Summary description for ClsEscalacaoHorizontal
/// </summary>
public class ClsEscalacaoHorizontal
{

    //Colecao de atributos
    private ServiceDesk.Banco.ClsAtributos objAtributos = new ServiceDesk.Banco.ClsAtributos();

    //Atributos
    private ServiceDesk.Banco.ClsAtributo objCodigo = new ServiceDesk.Banco.ClsAtributo();
    private ServiceDesk.Banco.ClsAtributo objTabela = new ServiceDesk.Banco.ClsAtributo();
    private ServiceDesk.Banco.ClsAtributo objTecnicoAtendimento = new ServiceDesk.Banco.ClsAtributo();
    private ServiceDesk.Banco.ClsAtributo objNivelAtendimento = new ServiceDesk.Banco.ClsAtributo();
    private ServiceDesk.Banco.ClsAtributo objDataInclusao = new ServiceDesk.Banco.ClsAtributo();
    private ServiceDesk.Banco.ClsAtributo objEquipeAtendimento = new ServiceDesk.Banco.ClsAtributo();
    private ServiceDesk.Banco.ClsAtributo objCodigoIdentificador = new ServiceDesk.Banco.ClsAtributo();
    private ServiceDesk.Banco.ClsAtributo objPessoaCodigoInclusor = new ServiceDesk.Banco.ClsAtributo();

    #region Propriedades

    public enum enumTipoLog : int
    {
        UPDATE = 0,
        INSERT = 1,
        DELETE = 2,
        SELECT = 3,
        ACESSO = 4,
        EDIT = 5
    }

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
    /// Tabela relacionada
    /// </summary>
    public ServiceDesk.Banco.ClsAtributo Tabela
    {
        get { return objTabela; }
        set { this.objTabela = value; }
    }

    /// <summary>
    /// Codigo do Tecnico
    /// </summary>
    public ServiceDesk.Banco.ClsAtributo TecnicoAtendimento
    {
        get { return objTecnicoAtendimento; }
        set { this.objTecnicoAtendimento = value; }
    }

    /// <summary>
    /// Codigo do Nivel de Atendimento
    /// </summary>
    public ServiceDesk.Banco.ClsAtributo NivelAtendimento
    {
        get { return objNivelAtendimento; }
        set { this.objNivelAtendimento = value; }
    }

    /// <summary>
    /// Data de Inclusao
    /// </summary>
    public ServiceDesk.Banco.ClsAtributo DataInclusao
    {
        get { return objDataInclusao; }
        set { this.objDataInclusao = value; }
    }

    /// <summary>
    /// Codigo da Equipe
    /// </summary>
    public ServiceDesk.Banco.ClsAtributo EquipeAtendimento
    {
        get { return objEquipeAtendimento; }
        set { this.objEquipeAtendimento = value; }
    }

    /// <summary>
    /// Identificador do registro
    /// </summary>
    public ServiceDesk.Banco.ClsAtributo Identificador
    {
        get { return objCodigoIdentificador; }
        set { this.objCodigoIdentificador = value; }
    }

    /// <summary>
    /// Codigo da Pessoa 
    /// </summary>
    public ServiceDesk.Banco.ClsAtributo PessoaCodigoInclusor
    {
        get { return objPessoaCodigoInclusor; }
        set { this.objPessoaCodigoInclusor = value; }
    }

    #endregion


    #region Métodos

    #region Construtores da classe
    /// <summary>
    /// Construtor da Classe
    /// </summary>
    public ClsEscalacaoHorizontal()
    {
        this.alimentaColecaoCampos();
    }

    /// <summary>
    /// Construtor da classe
    /// </summary>
    public ClsEscalacaoHorizontal(int intCodigo)
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
        objAtributos.NomeTabela = "EscalacaoHorizontal";
        objAtributos.DescricaoTabela = "Escalação Horizontal";

        objCodigo.Campo = "escalacao_horizontal_codigo";
        objCodigo.Descricao = "Código";
        objCodigo.CampoIdentificador = true;
        objCodigo.CampoObrigatorio = true;
        objCodigo.Tipo = System.Data.DbType.Int32;
        objAtributos.Add(objCodigo);

        objTabela.Campo = "Tabela";
        objTabela.Descricao = "Tabela Relacionada";
        objTabela.CampoIdentificador = false;
        objTabela.CampoObrigatorio = true;
        objTabela.Tipo = System.Data.DbType.String;
        objAtributos.Add(objTabela);

        objDataInclusao.Campo = "data_inclusao";
        objDataInclusao.Descricao = "Data";
        objDataInclusao.CampoIdentificador = false;
        objDataInclusao.CampoObrigatorio = true;
        objDataInclusao.Tipo = System.Data.DbType.DateTime;
        objAtributos.Add(objDataInclusao);

        PessoaCodigoInclusor.Campo = "pessoa_codigo_inclusao";
        PessoaCodigoInclusor.Descricao = "Pessoa";
        PessoaCodigoInclusor.CampoIdentificador = false;
        PessoaCodigoInclusor.CampoObrigatorio = true;
        PessoaCodigoInclusor.Tipo = System.Data.DbType.Int32;
        objAtributos.Add(PessoaCodigoInclusor);

        objTecnicoAtendimento.Campo = "tecnico_codigo";
        objTecnicoAtendimento.Descricao = "Técnico";
        objTecnicoAtendimento.CampoIdentificador = false;
        objTecnicoAtendimento.CampoObrigatorio = true;
        objTecnicoAtendimento.Tipo = System.Data.DbType.Int32;
        objAtributos.Add(objTecnicoAtendimento);

        objNivelAtendimento.Campo = "nivel_codigo";
        objNivelAtendimento.Descricao = "Nível de Atendimento";
        objNivelAtendimento.CampoIdentificador = false;
        objNivelAtendimento.CampoObrigatorio = true;
        objNivelAtendimento.Tipo = System.Data.DbType.Int32;
        objAtributos.Add(objNivelAtendimento);

        objEquipeAtendimento.Campo = "equipe_codigo";
        objEquipeAtendimento.Descricao = "Equipe";
        objEquipeAtendimento.CampoIdentificador = false;
        objEquipeAtendimento.CampoObrigatorio = true;
        objEquipeAtendimento.Tipo = System.Data.DbType.Int32;
        objAtributos.Add(objEquipeAtendimento);

        objCodigoIdentificador.Campo = "tabela_identificador";
        objCodigoIdentificador.Descricao = "Identificador";
        objCodigoIdentificador.CampoIdentificador = false;
        objCodigoIdentificador.CampoObrigatorio = true;
        objCodigoIdentificador.Tipo = System.Data.DbType.Int32;
        objAtributos.Add(objCodigoIdentificador);

    }

    #endregion

    #region metodo insereEscalacao

    public string insereEscalacao(string strTabela, string strTabelaIdentificador, string strNivelCodigo, string strEquipeCodigo, string strTecnicoCodigo, string strPessoaCodigo)
    {
        try
        {
            string CodigoEscalacao = string.Empty;
            string strMensagem = string.Empty;
            string strTipoLog = string.Empty;
            //bool bolRetorno = false;
            ServiceDesk.Negocio.ClsIdentificador objIdentificador = new ServiceDesk.Negocio.ClsIdentificador();

            objIdentificador.Tabela.Valor = objAtributos.NomeTabela;
            objCodigo.Valor = objIdentificador.getProximoValor().ToString();
            objNivelAtendimento.Valor = strNivelCodigo;
            objTecnicoAtendimento.Valor = strTecnicoCodigo;

            objEquipeAtendimento.Valor = strEquipeCodigo;
            objPessoaCodigoInclusor.Valor = strPessoaCodigo;
            objDataInclusao.Valor = DateTime.Now.ToString(ClsParametro.DataInclusao);
            objTabela.Valor = strTabela;
            objCodigoIdentificador.Valor = strTabelaIdentificador.Trim();

            if (insere(out strMensagem))
            {
                //Atualizando o valor na tabela identificador
                objIdentificador.atualizaValor();
                CodigoEscalacao = objCodigo.Valor;
            }

            return CodigoEscalacao;
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    #endregion insereEscalacao

    #region metodo alteraEscalacao

    public bool alteraEscalacao(string strTabela, string strTabelaIdentificador, string strNivelCodigo, string strEquipeCodigo, string strTecnicoCodigo)
    {
        try
        {
            string strMensagem = string.Empty;
            string strTipoLog = string.Empty;
            string strSql = string.Empty;
            bool bolRetorno = false;
            bool bPrimeiroArgumento = true;

            strSql = "UPDATE " + strTabela;

            if (strNivelCodigo != string.Empty)
            {
                if (bPrimeiroArgumento == true)
                {
                    strSql += " SET nivel_atendimento_codigo = " + strNivelCodigo + " ";
                    bPrimeiroArgumento = false;
                }
                else
                {
                    strSql += " nivel_atendimento_codigo = " + strNivelCodigo + " ";
                }
            }

            if (strEquipeCodigo != string.Empty)
            {
                if (bPrimeiroArgumento == true)
                {
                    strSql = " SET equipe_codigo_alocacao = " + strEquipeCodigo + " ";
                    bPrimeiroArgumento = false;
                }
                else
                {
                    strSql = " , equipe_codigo_alocacao = " + strEquipeCodigo + " ";
                }
            }

            if (strTecnicoCodigo != string.Empty)
            {
                if (bPrimeiroArgumento == true)
                {
                    strSql = " SET pessoa_codigo_alocacao = " + strTecnicoCodigo + " ";
                    bPrimeiroArgumento = false;
                }
                else
                {
                    strSql = " , pessoa_codigo_alocacao = " + strTecnicoCodigo + " ";
                }
            }

            if (bPrimeiroArgumento == false)
            {
                strSql = " WHERE " + strTabela + "_codigo = " + strTabelaIdentificador + " ";

            }

            ServiceDesk.Banco.ClsBanco banco = new ServiceDesk.Banco.ClsBanco();
            if (banco.executaSQL(strSql))
            {
                bolRetorno = true;
            }
            else
            {
                bolRetorno = false;
            }

            return bolRetorno;
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    #endregion alteraEscalacao

    #region metodo voltaEscalacaoHorizontal

    public static bool voltaEscalacaoHorizontal(string strTabela, string strTabelaIdentificador, string strCodigoEscalacao)
    {
      try
      {
        bool bSucesso = false;
        string strSql = string.Empty;
        string strMensagem = string.Empty;

        strSql = "SELECT * from escalacaohorizontal ";
        strSql += "WHERE  ";
        strSql += "escalacao_horizontal_codigo IN ";
        strSql += "( ";
        strSql += "  Select top 1 escalacao_horizontal_codigo  ";
        strSql += "  from escalacaohorizontal ";
        strSql += "  WHERE escalacao_horizontal_codigo NOT IN ('" + strCodigoEscalacao + "') ";
        strSql += "  and tabela = '" + strTabela + "' and tabela_identificador = '" + strTabelaIdentificador + "' ";
        strSql += "  order by data_inclusao desc  ";
        strSql += ") ";

        SqlDataReader objReader = ServiceDesk.Banco.ClsBanco.geraDataReader(strSql);
        if (objReader.Read())
        {
          ClsEscalacaoHorizontal objEscalacaoHorizontalAnterior = new ClsEscalacaoHorizontal(Convert.ToInt32(objReader["escalacao_horizontal_codigo"].ToString()));
          //propaga a escalacao
          ClsEscalacaoHorizontal.propagaEscalacaoHorizontal(objEscalacaoHorizontalAnterior);
        }
        objReader.Close();
        objReader.Dispose();
        return bSucesso;
      }
      catch (Exception ex)
      {
          throw ex;
      }

    }

    #endregion buscaEscalacaoAnterior

    #region metodo insere
    /// <summary>
    /// Método que insere um novo registro de Log.
    /// </summary>
    /// <param name="strMensagem">Mensagem com informação da execução do método.</param>
    /// <returns>Retorna true ou false. Se o registro foi inserido ou não.</returns>
    public bool insere(out String strMensagem)
    {
        try
        {
            strMensagem = String.Empty;
            bool bolRetorno = false;

            ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
            if (objBanco.insereColecao(this.objAtributos))
            {
                strMensagem = "Item inserido com sucesso.";
                bolRetorno = true;
            }
            objBanco = null;

            return bolRetorno;
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
    #endregion

    #region metodo existeEscalacao
    /// <summary>
    /// Verifica se uma notificacao ja existe
    /// </summary>
    /// <param name="strTabela">tabela relacionada</param>
    /// <param name="strTabelaIdentificador">codigo identificador </param>
    /// <param name="strNivelCodigo">codigo nivel</param>
    /// <param name="strEquipeCodigo">codigo equipe</param>
    /// <param name="strTecnicoCodigo">codigo tecnico</param>
    /// <returns>true se existe, false se nao existe</returns>
    public static bool existeEscalacao(string strTabela, string strTabelaIdentificador, string strNivelCodigo, string strEquipeCodigo, string strTecnicoCodigo)
    {
        try
        {
            bool bExiste = false;
            string strSql = string.Empty;

            strSql = "Select escalacao_horizontal_codigo from escalacaohorizontal ";
            strSql += "where tabela =  '" + strTabela + "' ";

            strSql += "AND tabela_identificador = '" + strTabelaIdentificador + "' ";
            if (strNivelCodigo != string.Empty)
            {
                strSql += "AND nivel_codigo ='" + strNivelCodigo + "' ";
            }
            if (strEquipeCodigo != string.Empty)
            {
                strSql += "AND equipe_codigo = '" + strEquipeCodigo + "' ";
            }
            if (strTecnicoCodigo != string.Empty)
            {
                strSql += "AND tecnico_codigo = '" + strTecnicoCodigo + "' ";
            }

            strSql += "AND escalacao_horizontal_codigo IN ";
            strSql += "( ";
            strSql += "  Select top 1 escalacao_horizontal_codigo from escalacaohorizontal ";
            strSql += "  where tabela ='" + strTabela + "'  ";
            strSql += "  AND tabela_identificador = '" + strTabelaIdentificador + "' ";
            strSql += "  ORDER by data_inclusao desc ";
            strSql += ") ";

            SqlDataReader objReader = ServiceDesk.Banco.ClsBanco.geraDataReader(strSql);
            if (objReader.Read())
            {
                bExiste = true;
            }
            else
            {
                bExiste = true;
            }
            objReader.Dispose();

            return bExiste;
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    #endregion

    #region metodo propagaEscalacaoHorizontal
    /// <summary>
    /// Seta os valores de escalação horizontal do registro e propaga para os registros
    /// das tabela relacionadas de acordo com os atributos do objeto.
    /// </summary>
    /// <param name="objEscalacaoHorizontal">objeto do tipo escalacao horizontal</param>
    /// <returns>true se sucesso. false se nao</returns>
    public static bool propagaEscalacaoHorizontal(ClsEscalacaoHorizontal objEscalacaoHorizontal)
    {
        try
        {
            bool bSucesso = false;
            string strSql = string.Empty;
            string strMensagem = string.Empty;
            string strDataMinimaSistema = ClsParametro.DataMinimaSistema;

            if (objEscalacaoHorizontal.Tabela.Valor.ToLower() == "chamado")
            {
              //Seta a escalacao em chamado e propaga para incidente e requisicao de serviço
              //chamado
              strSql = "UPDATE chamado set nivel_atendimento_codigo = '" + objEscalacaoHorizontal.NivelAtendimento.Valor + "',";
              strSql += " equipe_codigo_alocacao = '" + objEscalacaoHorizontal.EquipeAtendimento.Valor + "',";
              strSql += " pessoa_codigo_alocacao = '" + objEscalacaoHorizontal.objTecnicoAtendimento.Valor + "'";
              strSql += " WHERE chamado_codigo = '" + objEscalacaoHorizontal.Identificador.Valor + "'";

              ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
              if (objBanco.executaSQL(strSql))
              {
                #region grava a nova escalação retornada
                ClsEscalacaoHorizontal objEscalacao = new ClsEscalacaoHorizontal();
                objEscalacao.insereEscalacao(objEscalacaoHorizontal.Tabela.Valor, objEscalacaoHorizontal.Identificador.Valor, objEscalacaoHorizontal.NivelAtendimento.Valor, objEscalacaoHorizontal.EquipeAtendimento.Valor, objEscalacaoHorizontal.objTecnicoAtendimento.Valor, objEscalacaoHorizontal.objPessoaCodigoInclusor.Valor);
                objEscalacao = null;
                #endregion

                #region Propaga para os incidentes relacionados
                if (ClsChamado.PossuiIncidenteVinculado(objEscalacaoHorizontal.Identificador.Valor))
                {
                    strSql = "SELECT incidente_codigo FROM IncidenteChamado ";
                    strSql += "WHERE chamado_codigo = '" + objEscalacaoHorizontal.Identificador.Valor + "'";
                    SqlDataReader objReader = ServiceDesk.Banco.ClsBanco.geraDataReader(strSql);
                    while (objReader.Read())
                    {
                        strSql = "UPDATE incidente set nivel_atendimento_codigo = '" + objEscalacaoHorizontal.NivelAtendimento.Valor + "',";
                        strSql += " equipe_codigo_alocacao = '" + objEscalacaoHorizontal.EquipeAtendimento.Valor + "',";
                        strSql += " pessoa_codigo_alocacao = '" + objEscalacaoHorizontal.objTecnicoAtendimento.Valor + "'";
                        strSql += " WHERE incidente_codigo = '" + objReader["incidente_codigo"].ToString() + "'";

                        try
                        {
                            objBanco.executaSQL(strSql);
                        }
                        catch
                        { }
                    }
                    objReader.Dispose();
                    objReader = null;
                }
                #endregion

                #region Propaga para requisicao de serviço
                strSql = "SELECT requisicaoservico_codigo FROM RequisicaoServicoChamado ";
                strSql += "WHERE chamado_codigo = '" + objEscalacaoHorizontal.Identificador.Valor + "'";
                SqlDataReader objReaderServico = ServiceDesk.Banco.ClsBanco.geraDataReader(strSql);
                while (objReaderServico.Read())
                {
                    strSql = "UPDATE requisicaoservico set nivel_atendimento_codigo = '" + objEscalacaoHorizontal.NivelAtendimento.Valor + "',";
                    strSql += " equipe_codigo_alocacao = '" + objEscalacaoHorizontal.EquipeAtendimento.Valor + "',";
                    strSql += " pessoa_codigo_alocacao = '" + objEscalacaoHorizontal.objTecnicoAtendimento.Valor + "'";
                    strSql += " WHERE incidente_codigo = '" + objReaderServico["requisicaoservico_codigo"].ToString() + "'";

                    try
                    {
                        objBanco.executaSQL(strSql);
                    }
                    catch 
                    { }
                }
                objReaderServico.Dispose();
                objReaderServico = null;
                #endregion
              }
            }

            if (objEscalacaoHorizontal.Tabela.Valor.ToLower() == "incidente")
            {
              strSql = "UPDATE incidente set nivel_atendimento_codigo = '" + objEscalacaoHorizontal.NivelAtendimento.Valor + "',";
              strSql += " equipe_codigo_alocacao = '" + objEscalacaoHorizontal.EquipeAtendimento.Valor + "',";
              strSql += " pessoa_codigo_alocacao = '" + objEscalacaoHorizontal.objTecnicoAtendimento.Valor + "'";
              strSql += " WHERE incidente_codigo = '" + objEscalacaoHorizontal.Identificador.Valor + "'";

              ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
              if (objBanco.executaSQL(strSql))
              {
                #region grava a nova escalação retornada
                //ClsEscalacaoHorizontal objEscalacao = new ClsEscalacaoHorizontal();
                //objEscalacao.insereEscalacao(objEscalacaoHorizontal.Tabela.Valor, objEscalacaoHorizontal.Identificador.Valor, objEscalacaoHorizontal.NivelAtendimento.Valor, objEscalacaoHorizontal.EquipeAtendimento.Valor, objEscalacaoHorizontal.objTecnicoAtendimento.Valor, objEscalacaoHorizontal.objPessoaCodigoInclusor.Valor);
                //objEscalacao = null;
                #endregion

                #region Propaga para os chamados relacionados
                if (ClsIncidente.PossuiChamadoVinculado(objEscalacaoHorizontal.Identificador.Valor))
                {
                    strSql = "SELECT chamado_codigo FROM IncidenteChamado ";
                    strSql += "WHERE incidente_codigo = '" + objEscalacaoHorizontal.Identificador.Valor + "'";
                    SqlDataReader objReader = ServiceDesk.Banco.ClsBanco.geraDataReader(strSql);
                    while (objReader.Read())
                    {
                        strSql = "UPDATE chamado set nivel_atendimento_codigo = '" + objEscalacaoHorizontal.NivelAtendimento.Valor + "',";
                        strSql += " equipe_codigo_alocacao = '" + objEscalacaoHorizontal.EquipeAtendimento.Valor + "',";
                        strSql += " pessoa_codigo_alocacao = '" + objEscalacaoHorizontal.objTecnicoAtendimento.Valor + "'";
                        strSql += " WHERE chamado_codigo = '" + objReader["chamado_codigo"].ToString() + "'";

                        try
                        {
                            objBanco.executaSQL(strSql);
                        }
                        catch 
                        { }

                    }
                    objReader.Dispose();
                }
                #endregion
              }
            }

            if (objEscalacaoHorizontal.Tabela.Valor.ToLower() == "requisicaoservico")
            {
              strSql = "UPDATE requisicaoservico set nivel_atendimento_codigo = '" + objEscalacaoHorizontal.NivelAtendimento.Valor + "',";
              strSql += " equipe_codigo_alocacao = '" + objEscalacaoHorizontal.EquipeAtendimento.Valor + "',";
              strSql += " pessoa_codigo_alocacao = '" + objEscalacaoHorizontal.objTecnicoAtendimento.Valor + "'";
              strSql += " WHERE requisicaoservico_codigo = '" + objEscalacaoHorizontal.Identificador.Valor + "'";

              ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
              if (objBanco.executaSQL(strSql))
              {
                #region grava a nova escalação retornada
                //ClsEscalacaoHorizontal objEscalacao = new ClsEscalacaoHorizontal();
                //objEscalacao.insereEscalacao(objEscalacaoHorizontal.Tabela.Valor, objEscalacaoHorizontal.Identificador.Valor, objEscalacaoHorizontal.NivelAtendimento.Valor, objEscalacaoHorizontal.EquipeAtendimento.Valor, objEscalacaoHorizontal.objTecnicoAtendimento.Valor, objEscalacaoHorizontal.objPessoaCodigoInclusor.Valor);
                //objEscalacao = null;
                #endregion

                #region Propaga para os chamados relacionados
                if (ClsIncidente.PossuiChamadoVinculado(objEscalacaoHorizontal.Identificador.Valor))
                {
                  strSql = "SELECT chamado_codigo FROM RequisicaoServicoChamado ";
                  strSql += "WHERE requisicaoservico_codigo = '" + objEscalacaoHorizontal.Identificador.Valor + "'";
                  SqlDataReader objReader = ServiceDesk.Banco.ClsBanco.geraDataReader(strSql);
                  while (objReader.Read())
                  {
                    strSql = "UPDATE chamado set nivel_atendimento_codigo = '" + objEscalacaoHorizontal.NivelAtendimento.Valor + "',";
                    strSql += " equipe_codigo_alocacao = '" + objEscalacaoHorizontal.EquipeAtendimento.Valor + "',";
                    strSql += " pessoa_codigo_alocacao = '" + objEscalacaoHorizontal.objTecnicoAtendimento.Valor + "'";
                    strSql += " WHERE chamado_codigo = '" + objReader["chamado_codigo"].ToString() + "'";

                    try
                    {
                      objBanco.executaSQL(strSql);
                    }
                    catch 
                    { }
                  }
                  objReader.Dispose();
                }
                #endregion
              }
            }

            return bSucesso;
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    #endregion

    #endregion metodos
}
