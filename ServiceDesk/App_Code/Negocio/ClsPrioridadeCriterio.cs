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
/// Classe PrioridadeCritério
/// </summary>
namespace ServiceDesk.Negocio
{
    public class ClsPrioridadeCriterio
    {
        //Colecao de atributos de PrioridadeCriterio
        private ServiceDesk.Banco.ClsAtributos objAtributos = new ServiceDesk.Banco.ClsAtributos();

        //Atributos de PrioridadeCriterio
        private ServiceDesk.Banco.ClsAtributo objPrioridadeCodigo = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objImpactoCodigo = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objUrgenciaCodigo = new ServiceDesk.Banco.ClsAtributo();

        #region Propriedades
        public ServiceDesk.Banco.ClsAtributos Atributos
        {
            get { return this.objAtributos; }
        }

        /// <summary>
        /// Código da Prioridade
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo PrioridadeCodigo
        {
            get { return objPrioridadeCodigo; }
            set { this.objPrioridadeCodigo = value; }
        }

        /// <summary>
        /// Código do Impacto
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo ImpactoCodigo
        {
            get { return objImpactoCodigo; }
            set { this.objImpactoCodigo = value; }
        }

        /// <summary>
        /// Código da urgência
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo UrgenciaCodigo
        {
            get { return objUrgenciaCodigo; }
            set { this.objUrgenciaCodigo = value; }
        }
        #endregion

        #region metodos

        #region Construtor da classe
        /// <summary>
        /// Construtor da Classe
        /// </summary>
        public ClsPrioridadeCriterio()
        {
            this.alimentaColecaoCampos();
        }
        #endregion

        #region alimentaColecaoCampos
        /// <summary>
        /// Método que alimenta a coleção de atributos
        /// </summary>
        private void alimentaColecaoCampos()
        {
            objAtributos.NomeTabela = "PrioridadeCriterio";
            objAtributos.DescricaoTabela = "Critério da prioridade";

            objPrioridadeCodigo.Campo = "prioridade_codigo";
            objPrioridadeCodigo.Descricao = "Código da Prioridade";
            objPrioridadeCodigo.CampoIdentificador = true;
            objPrioridadeCodigo.CampoObrigatorio = true;
            objPrioridadeCodigo.Tipo = System.Data.DbType.Int32;
            objAtributos.Add(objPrioridadeCodigo);

            objImpactoCodigo.Campo = "impacto_codigo";
            objImpactoCodigo.Descricao = "Código do Impacto";
            objImpactoCodigo.CampoIdentificador = true;
            objImpactoCodigo.CampoObrigatorio = true;
            objImpactoCodigo.Tipo = System.Data.DbType.Int32;
            objAtributos.Add(objImpactoCodigo);

            objUrgenciaCodigo.Campo = "urgencia_codigo";
            objUrgenciaCodigo.Descricao = "Código da Urgencia";
            objUrgenciaCodigo.CampoIdentificador = true;
            objUrgenciaCodigo.CampoObrigatorio = true;
            objUrgenciaCodigo.Tipo = System.Data.DbType.Int32;
            objAtributos.Add(objUrgenciaCodigo);
        }
        #endregion

        #region VerificaExisteCriterio
        /// <summary>
        /// Método que verifica se existe um determinado registro já cadastrado na 
        /// tabela.
        /// </summary>
        /// <returns>Retorna um valor Bool</returns>
        public bool VerificaExisteCriterio(out String strMensagem, String strPrioridadeCodigo, String strImpactoCodigo, String strUrgenciaCodigo)
        {
            try
            {
                strMensagem = String.Empty;
                String strSQL = String.Empty;
                bool bolRetorno = false;
                try
                {
                    if (strImpactoCodigo != String.Empty && strUrgenciaCodigo != String.Empty)
                    {

                        strSQL = "SELECT prioridade_codigo FROM PrioridadeCriterio WHERE";
                        strSQL += " prioridade_codigo = " + strPrioridadeCodigo.Trim();
                        strSQL += " AND impacto_codigo = " + strImpactoCodigo.Trim();
                        strSQL += " AND urgencia_codigo = " + strUrgenciaCodigo.Trim();

                        if (ServiceDesk.Banco.ClsBanco.geraDataReader(strSQL).Read())
                        {
                            bolRetorno = true;
                            strMensagem = "Este critério já está definido para o item selecionado.";
                        }
                        else
                        {
                            bolRetorno = false;

                        }

                        return bolRetorno;
                    }
                    else
                    {
                        strMensagem = "Selecione o Impacto e a Urgência para inserir um novo critério.";
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region metodo insere
        /// <summary>
        /// Método que insere um novo Critério da prioridade
        /// </summary>
        /// <param name="strMensagem">Mensagem com informação da execução do método.</param>
        /// <returns>Retorna true ou false. Se o registro foi inserido ou não.</returns>
        public bool insere(out String strMensagem)
        {
            try
            {
                strMensagem = String.Empty;
                String strMensagemErro = String.Empty;
                bool bolRetorno = false;

                if (this.objPrioridadeCodigo.Valor == String.Empty)
                    strMensagemErro = "Favor selecionar a prioridade.<br />";

                if (this.objImpactoCodigo.Valor == String.Empty)
                    strMensagemErro += "Favor informar a descrição do Impacto.<br />";

                if (this.objUrgenciaCodigo.Valor == String.Empty)
                    strMensagemErro += "Favor informar a descrição da Urgência.";

                if (strMensagemErro == String.Empty)
                {
                    ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
                    if (objBanco.insereColecao(this.objAtributos))
                    {
                        strMensagem = "Item inserido com sucesso.";
                        bolRetorno = true;
                    }
                    objBanco = null;
                }
                else
                {
                    strMensagem = strMensagemErro;
                    bolRetorno = false;
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
        /// Método que altera um Critério da prioridade
        /// </summary>
        /// <param name="intPrioridadeCodigoAtual">Código da Prioridade atual</param>
        /// <param name="intImpactoCodigoAtual">Código do Impacto atual</param>
        /// <param name="intUrgenciaCodigoAtual">Código da urgência atual</param>
        /// <param name="intPrioridadeCodigoAlterado">Código da prioridade alterado</param>
        /// <param name="intImpactoCodigoAlterado">Código do impacto alterado</param>
        /// <param name="intUrgenciaCodigoAlterado">Código da urgência alterado</param>
        /// <returns>Retorna True ou False de acordo com a operação.</returns>
        public bool altera(int intPrioridadeCodigoAtual, int intImpactoCodigoAtual, int intUrgenciaCodigoAtual,
            int intPrioridadeCodigoAlterado, int intImpactoCodigoAlterado, int intUrgenciaCodigoAlterado)
        {
            try
            {
                String strSQL = String.Empty;
                bool bolRetorno = false;
                try
                {
                    strSQL = "UPDATE PrioridadeCriterio SET prioridade_codigo = " + intPrioridadeCodigoAlterado + ",";
                    strSQL += " impacto_codigo = " + intImpactoCodigoAlterado + ", urgencia_codigo = " + intUrgenciaCodigoAlterado;
                    strSQL += " WHERE prioridade_codigo = " + intPrioridadeCodigoAtual;
                    strSQL += " AND impacto_codigo = " + intImpactoCodigoAtual;
                    strSQL += " AND urgencia_codigo = " + intUrgenciaCodigoAtual;

                    ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();

                    if (objBanco.executaSQL(strSQL))
                        bolRetorno = true;
                    else
                        bolRetorno = false;

                    objBanco = null;
                    return bolRetorno;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region metodo exclui
        /// <summary>
        /// Método que exclui um Critério da prioridade onde os três campos formam uma chave.
        /// </summary>
        /// <param name="intPrioridadeCodigo">Código da prioridade a ser excluido.</param>
        /// <param name="intImpactoCodigo">Código do Impacto a ser excluido.</param>
        /// <param name="intUrgenciaCodigo">Código da Urgência a ser excluido.</param>
        /// <returns>Retorna true ou false. Se o registro foi excluido ou não.</returns>
        public bool exclui(int intPrioridadeCodigo, int intImpactoCodigo, int intUrgenciaCodigo)
        {
            String strSQL = String.Empty;
            bool bolRetorno = false;
            try
            {
                strSQL = "DELETE FROM PrioridadeCriterio WHERE ";
                strSQL += " prioridade_codigo = " + intPrioridadeCodigo;
                strSQL += " AND impacto_codigo = " + intImpactoCodigo;
                strSQL += " AND urgencia_codigo = " + intUrgenciaCodigo;

                ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();

                if (objBanco.executaSQL(strSQL))
                    bolRetorno = true;
                else
                    bolRetorno = false;

                objBanco = null;
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
        /// Gera uma nova geraGridView de acordo com o código da prioridade.
        /// </summary>
        /// <param name="objGridView">geraGridView</param>
        /// <param name="bolCondicao">Condição para verificar se será utilizado a cláusula Where</param>
        public static void geraGridView(System.Web.UI.WebControls.GridView objGridView, int intCodigoPrioridade)
        {
            try
            {
                objGridView.AutoGenerateColumns = false;

                String strSQL = "SELECT * FROM PrioridadeCriterio WHERE prioridade_codigo = " + intCodigoPrioridade;
                objGridView.DataSource = ServiceDesk.Banco.ClsBanco.geraDataReader(strSQL);
                objGridView.DataBind();
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

