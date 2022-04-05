/*
- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
  • Classe para manipulação dos registros da tabela de ProblemaMudanca.
  1
  	Data: 30/11/2005
  	Autor: Fernanda Passos
  	Descrição: Classe que apresenta métodos e propriedades para o objeto ProblemaMudanca.   
  
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
/// Classe de acesso a dados da tabela ProblemaMudanca
/// </summary>
namespace ServiceDesk.Negocio
{
    /// <summary>
    /// Construtor da classe
    /// </summary>
    public class ClsProblemaMudanca
    {
        #region Declarações

        public ClsProblemaMudanca()
        {
            alimentaColecaoCampos();
        }

        //Coleção de objetos.
        private ServiceDesk.Banco.ClsAtributos objAtributos = new ServiceDesk.Banco.ClsAtributos();

        //Atributos.
        private ServiceDesk.Banco.ClsAtributo objCodigoProblema = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objCodigoMudanca = new ServiceDesk.Banco.ClsAtributo();
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
        /// Codigo do problema.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo CodigoProblema
        {
            get { return objCodigoProblema; }
            set { this.objCodigoProblema = value; }
        }

        /// <summary>
        /// Código da mudanca.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo CodigoMudanca
        {
            get { return objCodigoMudanca ; }
            set { this.objCodigoMudanca = value; }
        }
        #endregion

        #region Metodos

        #region Valida dados.
        /// <summary>
        /// Valida dados.
        /// </summary>
        /// <returns>Retorna o valor true se os dados for aprovado, retorna false se não for aprovado</returns>
        public bool ValidaDados(out String strMsg)
        {
            try
            {
            strMsg = string.Empty;

            if (objCodigoProblema.Valor.Trim() == null)
            {
                strMsg = "Informe o problema.";
                return false;
            }
            if (objCodigoMudanca.Valor.Trim() == null)
            {
                strMsg = "Informe a mudança.";
                return false;
            }

            return true;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        }
        #endregion

        #region Verifica se dados já existem
        /// <summary>
        /// Verifica se já existe.
        /// </summary>
        /// <param name="strNome"></param>
        /// <returns>Retorna true ou false. Se existe ou não.</returns>
        public bool VerificaSeJaExisteNoBanco()
        {
            try
            {   //Busca no banco de dados pelo nome.
                ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
                string strSqlCriterio = " problema_codigo = " + Convert.ToInt32(objCodigoProblema.Valor.Trim()) + " ";
                strSqlCriterio += " and mudanca_codigo = " + Convert.ToInt32(objCodigoMudanca.Valor.Trim()) + "";

                string strValor = objBanco.retornaValorCampo("ProblemaMudanca", "mudanca_codigo", strSqlCriterio);
                objBanco = null;
                //Verifica se o nome foi encontrado.
                if (strValor.Trim() != string.Empty)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
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

                if (ValidaDados(out strMensagem) == false)
                {
                    return false;
                }
                else
                {
                    ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
                    if (VerificaSeJaExisteNoBanco() == true)
                    {
                        strMensagem = "Associação já existe no banco.";
                        bolRetorno = false;
                    }
                    else if (objBanco.insereColecao(this.objAtributos))
                    {
                        strMensagem = "Associação realizada com sucesso.";
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

        #region GeraGridView
        /// <summary>
        /// Gera uma nova geraGridView de acordo com a coleção de atributos.
        /// </summary>
        /// <param name="objGridView">objeto gridview</param>
        public static void geraGridView(System.Web.UI.WebControls.GridView objGridView)
        {
            try
            {
                objGridView.AutoGenerateColumns = false;
                ClsProblemaMudanca objProblemaMudanca = new ClsProblemaMudanca();
                ServiceDesk.Controle.ClsGridView.geraGridView(objGridView, objProblemaMudanca.objAtributos);
                objProblemaMudanca = null;
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
        /// <param name="objGridView">objjeto Grid View</param>
        /// <param name="bolCondicao">Condição para verificar se será utilizado valores dos campos identificadores</param>
        public static void geraGridView(System.Web.UI.WebControls.GridView objGridView, ClsProblemaMudanca objProblemaMudanca, bool bolCondicao)
        {
            try
            {
                objGridView.AutoGenerateColumns = false;
                ServiceDesk.Controle.ClsGridView.geraGridView(objGridView, objProblemaMudanca.objAtributos, bolCondicao);
                objProblemaMudanca = null;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region GeraGridView
        /// <summary>
        /// Gera uma nova geraGridView de acordo com o código do problema 
        /// para listar os itens de configuração associados ao problema.
        /// </summary>
        /// <param name="objGridView">geraGridView</param>
        /// <param name="CodigoProblema" >Código do problema no qual esta associado o item de configuração</param>
        public static void geraGridView(System.Web.UI.WebControls.GridView objGridView, int CodigoProblema)
        {
            try
            {
                objGridView.AutoGenerateColumns = false;


                string strSql = "select Mudanca.mudanca_codigo, Mudanca.descricao";
                strSql += " from Problema, Mudanca, ProblemaMudanca";
                strSql += " where ProblemaMudanca.problema_codigo = Problema.problema_codigo";
                strSql += " and ProblemaMudanca.mudanca_codigo = Mudanca.mudanca_codigo";
                strSql += " and ProblemaMudanca.problema_codigo = " + CodigoProblema + "";


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

                if (ValidaDados(out strMensagem) == false)
                {
                    return false;
                }
                else
                {
                    ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
                    if (VerificaSeJaExisteNoBanco() == true)
                    {
                        strMensagem = "Associação já existe.";
                        bolRetorno = false;
                    }
                    else if (objBanco.alteraColecao(this.objAtributos))
                    {
                        strMensagem = "Registro alterado com sucesso";
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

                ////Valida a exclusão.
                //if (ServiceDesk.Negocio.ClsIdentificador.ValidaExclusao(objCodigo.Campo, objCodigo.Valor.Trim(), out strMsg, true, false, objAtributos.NomeTabela.Trim()) == false) return false;

                ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
                if (objBanco.apagaColecao(this.objAtributos))
                {
                    strMsg = "Registro excluído com sucesso.";
                    objBanco = null;
                    return true;
                }
                else
                {
                    strMsg = "Registro não excluído.";
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

        #region Excluir relação de mudanças associadas ao problema
        /// <summary>
        /// Excluir relação de mudanças associadas ao problema
        /// </summary>
        /// <param name="intCodigo" > Código do problema </param>
        /// <returns>Retorna true ou false. Se o registro foi excluido ou não.</returns>
        public bool excluiAssociacaoMudanca(int intCodigo)
        {
            try
            {
                String strSql = String.Empty;

                ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();

                strSql = "delete from ProblemaMudanca where problema_codigo = " + intCodigo + "";

                if (objBanco.executaSQL(strSql))
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

        #region alimentaColecaoCampos
        /// <summary>
        /// Alimentar coleção de atributos
        /// </summary>
        private void alimentaColecaoCampos()
        {
            try
            {
                objAtributos.NomeTabela = "ProblemaMudanca";
                objAtributos.DescricaoTabela = "Tabela que informa as mudanças para o problema.";

                objCodigoProblema.Campo = "problema_codigo";
                objCodigoProblema.Descricao = "Código do problema";
                objCodigoProblema.CampoIdentificador = true;
                objCodigoProblema.CampoObrigatorio = true;
                objCodigoProblema.Tipo = System.Data.DbType.Int32;
                objAtributos.Add(objCodigoProblema);

                objCodigoMudanca.Campo = "mudanca_codigo";
                objCodigoMudanca.Descricao = "Código da mudança";
                objCodigoMudanca.CampoIdentificador = true;
                objCodigoMudanca.CampoObrigatorio = true;
                objCodigoMudanca.Tipo = System.Data.DbType.Int32;
                objAtributos.Add(objCodigoMudanca);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #endregion
    }
}