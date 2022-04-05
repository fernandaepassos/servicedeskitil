using System;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Classe ClsStatusLog
/// </summary>
/// 

namespace ServiceDesk.Negocio
{
    public class ClsStatusLog
    {

        //Colecao de atributos do Log de Status
        private ServiceDesk.Banco.ClsAtributos objAtributos = new ServiceDesk.Banco.ClsAtributos();

        //Atributos de um Log de Status
        private ServiceDesk.Banco.ClsAtributo objCodigo = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objPessoa = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objStatusOrigem = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objStatusDestino = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objData = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objTabela = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objTabelaIdentificador = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objTempo = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objWorkflowCodigo = new ServiceDesk.Banco.ClsAtributo();
        

        #region Propriedades

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
        /// Pessoa 
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo Pessoa
        {
            get { return objPessoa; }
            set { this.objPessoa = value; }
        }

        /// <summary>
        /// StatusOrigem
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo StatusOrigem
        {
            get { return objStatusOrigem; }
            set { this.objStatusOrigem = value; }
        }

        /// <summary>
        /// StatusDestino
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo StatusDestino
        {
            get { return objStatusDestino; }
            set { this.objStatusDestino = value; }
        }

        /// <summary>
        /// Data
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo Data
        {
            get { return objData; }
            set { this.objData = value; }
        }

        /// <summary>
        /// Tabela
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo Tabela
        {
            get { return objTabela; }
            set { this.objTabela = value; }
        }

        /// <summary>
        /// TabelaIdentificador
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo TabelaIdentificador
        {
            get { return objTabelaIdentificador; }
            set { this.objTabelaIdentificador = value; }
        }

        /// <summary>
        /// Tempo
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo Tempo
        {
            get { return objTempo; }
            set { this.objTempo = value; }
        }

        /// <summary>
        /// Workflow Codigo
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo WorkflowCodigo
        {
            get { return objWorkflowCodigo; }
            set { this.objWorkflowCodigo = value; }
        }

        #endregion

        #region Métodos

        #region metodo Construtor da classe
        /// <summary>
        /// Construtor da classe
        /// </summary>
        public ClsStatusLog()
        {
            this.alimentaColecaoCampos();
        }
        #endregion

        #region metodo Construtor da classe com passagem de parametro
        /// <summary>
        /// Construtor da classe
        /// </summary>
        public ClsStatusLog(int intCodigo)
        {
            try
            {
                this.alimentaColecaoCampos();
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

        #region metodo insere
        /// <summary>
        /// Método que insere um novo Log do Status.
        /// </summary>
        /// <param name="strMensagem">Mensagem com informação da execução do método.</param>
        /// <returns>Retorna true ou false. Se o registro foi inserido ou não.</returns>
        public bool insere(out String strMensagem)
        {
            try
            {
                strMensagem = String.Empty;
                bool bolRetorno = false;

                if (this.objPessoa.Valor.Trim() == String.Empty)
                {
                    strMensagem = "Pessoa que está realizando a alteração não informada.";
                }
                else if (this.objStatusOrigem.Valor.Trim() == String.Empty)
                {
                    strMensagem = "Status Anterior não informado.";
                }
                else if (this.objStatusDestino.Valor.Trim() == String.Empty)
                {
                    strMensagem = "Status Novo não informado.";
                }
                else if (this.objTabela.Valor.Trim() == String.Empty)
                {
                    strMensagem = "Tabela não informada.";
                }
                else if (this.objTabelaIdentificador.Valor.Trim() == String.Empty)
                {
                    strMensagem = "Identificador da tabela não informado.";
                }
                else
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
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region buscaCodigoPenultimoStatus
        /// <summary>
        /// Busca o status de acordo com os parametros informados
        /// </summary>
        /// <param name="Tabela">Nome da tabela</param>
        /// <param name="IdentificadorTabela">Código Identificador</param>
        /// <returns>codigo do registro de status</returns>
        public static string buscaCodigoPenultimoStatus(string strTabela, string strIdentificadorTabela)
        {
            try
            {
                string strSql = string.Empty;
                string strCodigo = string.Empty;

                //penultimo status registrado	 
                strSql = "  SELECT top 1 status_log_codigo FROM STATUSLOG ";
                strSql += "  WHERE ";
                strSql += "  tabela_identificador =  " + strIdentificadorTabela + " ";
                strSql += "  AND tabela = '" + strTabela + "' ";
                strSql += "  ORDER BY data desc ";

                ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
                SqlDataReader objReader = ServiceDesk.Banco.ClsBanco.geraDataReader(strSql);
                if (objReader.Read())
                {
                    strCodigo = objReader["status_log_codigo"].ToString();
                }
                objBanco = null;
                objReader.Dispose();

                return strCodigo;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        #endregion

        #region alimentaColecaoCampos

        /// <summary>
        /// Método que alimenta a coleção de atributos
        /// </summary>
        private void alimentaColecaoCampos()
        {
            objAtributos.NomeTabela = "StatusLog";
            objAtributos.DescricaoTabela = "Log de Status";

            objCodigo.Campo = "status_log_codigo";
            objCodigo.Descricao = "Código";
            objCodigo.CampoIdentificador = true;
            objCodigo.CampoObrigatorio = false;
            objCodigo.Tipo = System.Data.DbType.Int32;
            objAtributos.Add(objCodigo);

            objPessoa.Campo = "pessoa_codigo";
            objPessoa.Descricao = "Pessoa que realizou a alteração";
            objPessoa.CampoIdentificador = false;
            objPessoa.CampoObrigatorio = false;
            objPessoa.Tipo = System.Data.DbType.Int32;
            objAtributos.Add(objPessoa);

            objStatusOrigem.Campo = "status_codigo_origem";
            objStatusOrigem.Descricao = "Status Anterior";
            objStatusOrigem.CampoIdentificador = false;
            objStatusOrigem.CampoObrigatorio = false;
            objStatusOrigem.Tipo = System.Data.DbType.Int32;
            objAtributos.Add(objStatusOrigem);

            objStatusDestino.Campo = "status_codigo_destino";
            objStatusDestino.Descricao = "Status Novo";
            objStatusDestino.CampoIdentificador = false;
            objStatusDestino.CampoObrigatorio = false;
            objStatusDestino.Tipo = System.Data.DbType.Int32;
            objAtributos.Add(objStatusDestino);

            objData.Campo = "data";
            objData.Descricao = "Data";
            objData.CampoIdentificador = false;
            objData.CampoObrigatorio = false;
            objData.Tipo = System.Data.DbType.DateTime;
            objAtributos.Add(objData);

            objTabela.Campo = "tabela";
            objTabela.Descricao = "Tabela";
            objTabela.CampoIdentificador = false;
            objTabela.CampoObrigatorio = false;
            objTabela.Tipo = System.Data.DbType.String;
            objAtributos.Add(objTabela);

            objTabelaIdentificador.Campo = "tabela_identificador";
            objTabelaIdentificador.Descricao = "Identificador da Tabela";
            objTabelaIdentificador.CampoIdentificador = false;
            objTabelaIdentificador.CampoObrigatorio = false;
            objTabelaIdentificador.Tipo = System.Data.DbType.Int32;
            objAtributos.Add(objTabelaIdentificador);

            objTempo.Campo = "tempo";
            objTempo.Descricao = "Tempo de Permanência do Status";
            objTempo.CampoIdentificador = false;
            objTempo.CampoObrigatorio = false;
            objTempo.Tipo = System.Data.DbType.Int32;
            objAtributos.Add(objTempo);

            objWorkflowCodigo.Campo = "workflow_codigo";
            objWorkflowCodigo.Descricao = "Work Flow Item";
            objWorkflowCodigo.CampoIdentificador = false;
            objWorkflowCodigo.CampoObrigatorio = false;
            objWorkflowCodigo.Tipo = System.Data.DbType.Int32;
            objAtributos.Add(objWorkflowCodigo);

        }
        #endregion

        #region metodo geraGridView
        /// <summary>
        /// Gera uma nova geraGridView de acordo com a coleção de atributos.
        /// </summary>
        /// <param name="objGridView">geraGridView</param>
        public static void geraGridView(System.Web.UI.WebControls.GridView objGridView)
        {
            try
            {
                objGridView.AutoGenerateColumns = false;
                ClsStatusLog objStatusLog = new ClsStatusLog();
                ServiceDesk.Controle.ClsGridView.geraGridView(objGridView, objStatusLog.objAtributos);
                objStatusLog = null;
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
        public static void geraGridView(System.Web.UI.WebControls.GridView objGridView, ClsStatusLog objStatusLog, bool bolCondicao)
        {
            try
            {
                objGridView.AutoGenerateColumns = false;
                ServiceDesk.Controle.ClsGridView.geraGridView(objGridView, objStatusLog.objAtributos, bolCondicao);
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