/*
- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
  � Classe para manipula��o dos registros da tabela ProblemaIncidente.
  
  	Data: 30/11/2005
  	Autor: Fernanda Passos
  	Descri��o: Classe que apresenta m�todos e propriedades para o objeto ProblemaIncidente. Um
    problema poder� ter ou n�o incidentes.
  
  
  � Altera��es
  	Data:
  	Autor:
  	Descri��o:
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
/// Classe de acesso a dados da tabela ProblemaIncidente.
/// </summary>
namespace ServiceDesk.Negocio
{
    /// <summary>
    /// Construtor da classe
    /// </summary>
    public class ClsProblemaIncidente
    {
        #region Declara��es

        public ClsProblemaIncidente()
        {
            alimentaColecaoCampos();
        }

        //Cole��o de objetos.
        private ServiceDesk.Banco.ClsAtributos objAtributos = new ServiceDesk.Banco.ClsAtributos();

        //Atributos.
        private ServiceDesk.Banco.ClsAtributo objCodigoProblema = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objCodigoIncidente = new ServiceDesk.Banco.ClsAtributo();
        #endregion

        #region Propriedades
        /// <summary>
        /// Cole��o de atributos
        /// </summary>
        public ServiceDesk.Banco.ClsAtributos Atributos
        {
            get
            {
                return this.Atributos;
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
        /// Nome do Incidente.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo CodigoIncidente
        {
            get { return objCodigoIncidente; }
            set { this.objCodigoIncidente = value; }
        }


        #endregion

        #region Metodos

        #region Valida dados.
        /// <summary>
        /// Valida dados.
        /// </summary>
        /// <returns>Retorna o valor true se a inser��o for aprovada, retorna false se a inser��o n�o for aprovada</returns>
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
                else if (objCodigoIncidente.Valor.Trim() == null)
                {
                    strMsg = "Informe o incidente.";
                    return false;
                }

                return true;
            }
            catch (System.Exception ex)
            {
                strMsg = ex.Message;
                throw ex;
            }
        }
        #endregion

        #region Verifica se dados j� existem
        /// <summary>
        /// Verifica se j� existe.
        /// </summary>
        /// <param name="strNome"></param>
        /// <returns>Retorna true ou false. Se existe ou n�o.</returns>
        public bool VerificaSeJaExisteNoBanco()
        {
            try
            {   //Busca no banco de dados pelo nome.
                ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
                string strSqlCriterio = " problema_codigo = " + Convert.ToInt32(objCodigoProblema.Valor.Trim()) + " ";
                strSqlCriterio += " and incidente_codigo = " + Convert.ToInt32(objCodigoIncidente.Valor.Trim()) + "";

                string strValor = objBanco.retornaValorCampo("ProblemaIncidente", "incidente_codigo", strSqlCriterio);
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
        /// Inserir (Insert into)
        /// </summary>
        /// <param name="strMensagem">Mensagem com informa��o da execu��o do m�todo.</param>
        /// <returns>Retorna true ou false. Se o registro foi inserido ou n�o.</returns>
        public bool insere(out String strMsg)
        {
            try
            {
                bool bolRetorno = false;
                strMsg = string.Empty;

                //Valida os dados.
                if (ValidaDados(out strMsg) == false)
                {
                    return false;
                }
                //Verifica se os dados j� existe no banco de dados.
                else if (VerificaSeJaExisteNoBanco() == true)
                {
                    strMsg = "Associa��o j� existente no banco.";
                    return false;
                }
                else
                {
                    //Insere no banco de dados
                    ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();

                    string strSql = "INSERT INTO ProblemaIncidente";
                    strSql += " (problema_codigo, incidente_codigo)";
                    strSql += " VALUES(" + Convert.ToInt32(objCodigoProblema.Valor.Trim()) + "";
                    strSql += " , " + Convert.ToInt32(this.objCodigoIncidente.Valor.Trim()) + ")";

                    if (objBanco.executaSQL(strSql) == true)
                    {
                        strMsg = "Registro inserido com sucesso.";
                        bolRetorno = true;
                    }
                    else
                    {
                        strMsg = "N�o foi poss�vel realizar a inser��o.";
                        bolRetorno = false;
                    }
                    objBanco = null;
                }

                return bolRetorno;
            }
            catch (Exception ex)
            {
                strMsg = ex.Message;
                throw ex;
            }

        }
        #endregion

        #region GeraGridView
        /// <summary>
        /// Gera uma nova geraGridView de acordo com a cole��o de atributos.
        /// </summary>
        /// <param name="objGridView">objeto gridview</param>
        public static void geraGridView(System.Web.UI.WebControls.GridView objGridView)
        {
            try
            {
                objGridView.AutoGenerateColumns = false;
                ClsProblemaIncidente objProblemaIncidente = new ClsProblemaIncidente();
                ServiceDesk.Controle.ClsGridView.geraGridView(objGridView, objProblemaIncidente.objAtributos);
                objProblemaIncidente = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region GeraGridView
        /// <summary>
        /// Gera uma nova geraGridView de acordo com a cole��o de atributos.
        /// </summary>
        /// <param name="objGridView">objjeto Grid View</param>
        /// <param name="bolCondicao">Condi��o para verificar se ser� utilizado a cl�usula Where</param>
        public static void geraGridView(System.Web.UI.WebControls.GridView objGridView, ClsProblemaIncidente objProblemaIncidente, bool bolCondicao)
        {
            try
            {
                objGridView.AutoGenerateColumns = false;
                ServiceDesk.Controle.ClsGridView.geraGridView(objGridView, objProblemaIncidente.objAtributos, bolCondicao);
                objProblemaIncidente = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region GeraGridView
        /// <summary>
        /// Gera uma nova geraGridView de acordo com o c�digo do problema para listar os incidentes do problema.
        /// </summary>
        /// <param name="objGridView">geraGridView</param>
        /// <param name="CodigoProblema" > C�digo do problema para listar os incidentes associados ao problema</param>
        public static void geraGridView(System.Web.UI.WebControls.GridView objGridView, int CodigoProblema)
        {
            try
            {
                objGridView.AutoGenerateColumns = false;

                string strSql = "SELECT ProblemaIncidente.incidente_codigo, Incidente.descricao, Impacto.descricao AS impacto, Prioridade.descricao AS PrioridadeDescricao, ";
                strSql += " Urgencia.descricao AS UrgenciaDescricao";
                strSql += " FROM ProblemaIncidente INNER JOIN";
                strSql += " Incidente ON ProblemaIncidente.incidente_codigo = Incidente.incidente_codigo INNER JOIN";
                strSql += " Impacto ON Incidente.impacto_codigo = Impacto.impacto_codigo INNER JOIN";
                strSql += " Prioridade ON Incidente.prioridade_codigo = Prioridade.prioridade_codigo INNER JOIN";
                strSql += " Urgencia ON Incidente.urgencia_codigo = Urgencia.urgencia_codigo";
                strSql += " WHERE ProblemaIncidente.problema_codigo = " + CodigoProblema + "";

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
        /// <returns>Retorna true ou false. Se o registro foi alterado ou n�o.</returns>
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
                        strMensagem = "Associa��o j� existe.";
                        bolRetorno = false;
                    }
                    else if (objBanco.alteraColecao(this.objAtributos))
                    {
                        strMensagem = "Problema alterado com sucesso";
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
        /// <returns>Retorna true ou false. Se o registro foi excluido ou n�o.</returns>
        public bool exclui(out String strMsg)
        {
            try
            {
                strMsg = string.Empty;

                ////Valida a exclus�o.
                //if (ServiceDesk.Negocio.ClsIdentificador.ValidaExclusao(objCodigo.Campo, objCodigo.Valor.Trim(), out strMsg, true, false, objAtributos.NomeTabela.Trim()) == false) return false;

                ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
                if (objBanco.apagaColecao(this.objAtributos))
                {
                    strMsg = "Registro exclu�do com sucesso.";
                    objBanco = null;
                    return true;
                }
                else
                {
                    strMsg = "Registro n�o exclu�do.";
                    objBanco = null;
                    return false;
                }

            }
            catch (System.Exception ex)
            {
                strMsg = ex.Message;
                throw ex;
            }

        }
        #endregion

        #region Exclui todos os incidentes associados ao problema.
        /// <summary>
        /// Exclui todos os incidentes associados ao problema.
        /// </summary>
        /// <param name="intCodigo" > C�digo do problema </param>
        /// <returns>Retorna true ou false. Se o registro foi excluido ou n�o.</returns>
        public bool excluiAssociacaoIncidenteDoProblema(int intCodigo)
        {
            try
            {
                String strSql = String.Empty;
                ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();

                strSql = "delete from ProblemaIncidente where problema_codigo = " + intCodigo + " ";

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
        /// Alimentar cole��o de atributos
        /// </summary>
        private void alimentaColecaoCampos()
        {
            try
            {
                objAtributos.NomeTabela = "ProblemaIncidente";
                objAtributos.DescricaoTabela = "Tabela que relaciona incidente a problema";

                objCodigoProblema.Campo = "problema_codigo";
                objCodigoProblema.Descricao = "C�digo do problema";
                objCodigoProblema.CampoIdentificador = true;
                objCodigoProblema.CampoObrigatorio = true;
                objCodigoProblema.Tipo = System.Data.DbType.Int32;
                objAtributos.Add(objCodigoProblema);

                objCodigoIncidente.Campo = "incidente_codigo";
                objCodigoIncidente.Descricao = "C�digo do incidente";
                objCodigoIncidente.CampoIdentificador = true;
                objCodigoIncidente.CampoObrigatorio = true;
                objCodigoIncidente.Tipo = System.Data.DbType.Int32;
                objAtributos.Add(objCodigoIncidente);
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