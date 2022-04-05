/*
- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
  • Classe que permite associar as funcoes que as pessoas podem acessar.
  
  	Data: 16/01/2006
  	Autor: Fernanda Passos
  	Descrição: Criação da classe.
  
  
  • Alterações
  	Data:
  	Autor: 
  	Descrição:  
- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
*/

using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Classe de acesso aos dados da tabela FuncaoPessoa
/// </summary>
namespace ServiceDesk.Negocio
{
  /// <summary>
  /// Construtor da classe
  /// </summary>
  public class ClsFuncaoPessoa
  {
    #region Construtor da classe
    public ClsFuncaoPessoa()
    {
      alimentaColecaoCampos();
    }
    #endregion

    #region Construtor da classe por parametro
    /// <summary>
    /// Construtor da classe por parametro
    /// </summary>
    /// <param name="intCodigoPessoa">Código da pessoa</param> 
    public ClsFuncaoPessoa(int intCodigoPessoa)
    {
      this.alimentaColecaoCampos();
      this.objCodigoPessoa.Valor = intCodigoPessoa.ToString();
      ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
      objBanco.alimentaColecao(this.objAtributos);
      objBanco = null;
    }
    #endregion

    #region Declarações

    //Coleção de objetos.
    private ServiceDesk.Banco.ClsAtributos objAtributos = new ServiceDesk.Banco.ClsAtributos();

    //Atributos de um problema.
    private ServiceDesk.Banco.ClsAtributo objCodigoPessoa = new ServiceDesk.Banco.ClsAtributo();
    private ServiceDesk.Banco.ClsAtributo objCodigoFuncao = new ServiceDesk.Banco.ClsAtributo();
    #endregion

    #region Propriedades
    /// <summary>
    /// Coleção de atributos
    /// </summary>
    public ServiceDesk.Banco.ClsAtributos Atributos
    {
      get
      {
        return this.objAtributos;
      }
    }

    /// <summary>
    /// Codigo da função
    /// </summary>
    public ServiceDesk.Banco.ClsAtributo CodigoFuncao
    {
      get { return objCodigoFuncao; }
      set { this.objCodigoFuncao = value; }
    }

    /// <summary>
    /// Código da pessoa.
    /// </summary>
    public ServiceDesk.Banco.ClsAtributo CodigoPessoa
    {
      get { return objCodigoPessoa ; }
      set { this.objCodigoPessoa = value; }
    }

    #endregion

    #region Metodos

    #region Validacao dos dados
    /// <summary>
    /// Verifica se os campos obrigatórios foram informados
    /// </summary>
    /// <returns>Retorna true ou false. Se for validado ou não.</returns>
    public bool ValidacaoDados(out String strMsg)
    {
        try
        {
            strMsg = String.Empty;

            if (objCodigoFuncao.Valor.Trim() == string.Empty)
            {
                strMsg = "Por favor, informe o função.";
                return false;
            }
            else if (objCodigoPessoa.Valor.Trim() == string.Empty)
            {
                strMsg = "Por favor, informe a pessoa.";
                return false;
            }

            return true;
        }
        catch (Exception ex)
        {
            strMsg = ex.Message;
            throw ex;
        }


    }
    #endregion

    #region Validacao exclusão
    /// <summary>
    /// Valida exclusão
    /// </summary>
    /// <returns>Retorna true ou false. Se for validado ou não.</returns>
    public bool ValidaExclusao(out String strMsg)
    {
      strMsg = string.Empty;
      try
      {
          return true;
      }
      catch (Exception ex)
      {
          strMsg = ex.Message;
          throw ex;
      }
    }
    #endregion

    #region Inserir
    /// <summary>
    /// Inserir
    /// </summary>
    /// <param name="strMensagem">Mensagem com informação da execução do método.</param>
    /// <returns>Retorna true ou false. Se o registro foi inserido ou não.</returns>
    public bool insere(out String strMensagem)
    {
        try
        {
            strMensagem = String.Empty;
            bool bolRetorno = false;

            //Verifica se todos os campos foram preenchidos.
            if (ValidacaoDados(out strMensagem) == false) return false;
            else if (VerificaSeExisteNoBanco(out strMensagem) == true) return false;
            else
            {
                ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
                //Insere no banco de dados.
                if (objBanco.insereColecao(this.objAtributos))
                {
                    strMensagem = "Registro inserido com sucesso.";
                    bolRetorno = true;
                }
                objBanco = null;
            }
            return bolRetorno;
        }
        catch (Exception ex)
        {
            strMensagem = ex.Message;
            throw ex;
        }
    }
    #endregion

    #region Alterar
    /// <summary>
    /// Alterar
    /// </summary>
    /// <returns>Retorna true ou false. Se o registro foi alterado ou não.</returns>
    public bool altera(out String strMensagem)
    {
        try
        {
            strMensagem = String.Empty;
            bool bolRetorno = false;

            if (ValidacaoDados(out strMensagem) == false) return false;
            else
            {
                ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
                //Altera no banco de dados.
                if (objBanco.alteraColecao(this.objAtributos))
                {
                    strMensagem = "Registro alterado com sucesso.";
                    bolRetorno = true;
                }
                objBanco = null;
            }

            return bolRetorno;
        }
        catch (Exception ex)
        {
            strMensagem = ex.Message;
            throw ex;
        }
    }
    #endregion

    #region Excluir
    /// <summary>
    /// Excluir
    /// </summary>
    /// <returns>Retorna true ou false. Se o registro foi excluido ou não.</returns>
    public bool exclui(out String strMsg)
    {
        try
        {
            strMsg = string.Empty;

            //Valida exclusão 
            if (ValidaExclusao(out strMsg) == false) return false;
            ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
            if (objBanco.apagaColecao(this.objAtributos))
            {
                strMsg = "Registro excluído com sucesso.";
                objBanco = null;
                return true;
            }
            else
            {
                strMsg = "Impossível excluir registro.";
                objBanco = null;
                return false;
            }
        }
        catch (Exception ex)
        {
            strMsg = ex.Message;
            throw ex;
        }
    }
    #endregion

    #region Alimenta campos coleção
    /// <summary>
    /// Alimenta campos coleção
    /// </summary>
    private void alimentaColecaoCampos()
    {
        try
        {
            objAtributos.NomeTabela = "FuncaoPessoa";
            objAtributos.DescricaoTabela = "Tabela que associa as funções que estão para a pessoa";

            objCodigoFuncao.Campo = "funcao_codigo";
            objCodigoFuncao.Descricao = "Código da fução";
            objCodigoFuncao.CampoIdentificador = false;
            objCodigoFuncao.CampoObrigatorio = true;
            objCodigoFuncao.Tipo = System.Data.DbType.Int32;
            objAtributos.Add(objCodigoFuncao);

            objCodigoPessoa.Campo = "pessoa_codigo";
            objCodigoPessoa.Descricao = "Código da pessoa.";
            objCodigoPessoa.CampoIdentificador = false;
            objCodigoPessoa.CampoObrigatorio = true;
            objCodigoPessoa.Tipo = System.Data.DbType.UInt32;
            objAtributos.Add(objCodigoPessoa);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region GeraGridView
    /// <summary>
    /// Gera uma nova geraGridView de acordo com a coleção de atributos.
    /// </summary>
    /// <param name="objGridView">objeto Grid View</param>
    /// <param name="bolCondicao">Condição para verificar se será utilizado a cláusula Where</param>
    public static void geraGridView(System.Web.UI.WebControls.GridView objGridView, ClsFuncaoPessoa objFuncaoPessoa, bool bolCondicao)
    {
        try
        {
            objGridView.AutoGenerateColumns = false;
            ServiceDesk.Controle.ClsGridView.geraGridView(objGridView, objFuncaoPessoa.objAtributos, bolCondicao);
            objFuncaoPessoa = null;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    #endregion

    #region GeraGridView
    /// <summary>
    /// Gera gridview com campos especificos.
    /// </summary>
    /// <param name="objGridView">geraGridView</param>
    public static void geraGridView(System.Web.UI.WebControls.GridView objGridView)
    {
        try
        {
            objGridView.AutoGenerateColumns = false;

            string strSql = "select FP.*, (select nome from Pessoa where pessoa_codigo = FP.pessoa_codigo)nome_pessoa,";
            strSql += "(select nome from Funcao F where F.funcao_codigo = FP.funcao_codigo)nome_funcao";
            strSql += " from FuncaoPessoa FP";

            ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
            objGridView.DataSource = ServiceDesk.Banco.ClsBanco.geraDataSet(strSql);
            objGridView.DataBind();
            objBanco = null;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region Verifica se dados já existem
    /// <summary>
    /// Verifica se já existe no banco de dados.
    /// </summary>
    /// <param name="strNome"></param>
    /// <returns>Retorna true ou false. Se existe ou não.</returns>
    /// <param name="strMensagem">Mensagem com resultado do método</param> 
    public bool VerificaSeExisteNoBanco(out String strMensagem)
    {
      strMensagem = string.Empty;
      try
      {


          ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();

          string strSql = "pessoa_codigo = " + Convert.ToInt32(objCodigoPessoa.Valor.Trim()) + " and funcao_codigo = " + Convert.ToInt32(objCodigoFuncao.Valor.Trim()) + "";

          string strValor = objBanco.retornaValorCampo("FuncaoPessoa", "pessoa_codigo", strSql);

          objBanco = null;

          //Verifica se o nome foi encontrado.
          if (strValor.Trim() != string.Empty)//Já existe no banco de dados.
          {
              strMensagem = "Registro já existente no banco de dados.";
              return true;
          }
          else return false;//Não existe no banco de dados.
      }
      catch (Exception ex)
      {
          throw ex;
      }
    }
    #endregion

    #region Pega funções por parametro
    /// <summary>
    /// Pega das funções por parametro
    /// </summary>
    /// <param name="strNomeFuncao">Nome da função que esta cadastrada no banco de dados</param>
    /// <param name="strCodigoPessoa">Código da pessoa</param>
    /// <returns>Retorna DataReader com nome das funções</returns>
    public static System.Data.SqlClient.SqlDataReader GetFuncaoPorParamentro(string strNomeFuncao, string strCodigoPessoa)
    {

            string strSql = "select F.* ";
            strSql += " from FuncaoPessoa FP, Funcao F ";
            strSql += " where F.funcao_codigo is not null";
            if (strCodigoPessoa != string.Empty) strSql += " and FP.pessoa_codigo = " + Convert.ToInt32(strCodigoPessoa.Trim()) + "";
            if (strNomeFuncao != string.Empty) strSql += " and F.funcao_codigo_superior in (select funcao_codigo from Funcao where nome = '" + strNomeFuncao.Trim() + "')";

            return ServiceDesk.Banco.ClsBanco.geraDataReader(strSql);

    }
    #endregion
    #endregion
  }
}