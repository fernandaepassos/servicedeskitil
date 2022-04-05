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
/// Classe PrioridadeCrit�rio
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
        /// C�digo da Prioridade
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo PrioridadeCodigo
        {
            get { return objPrioridadeCodigo; }
            set { this.objPrioridadeCodigo = value; }
        }

        /// <summary>
        /// C�digo do Impacto
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo ImpactoCodigo
        {
            get { return objImpactoCodigo; }
            set { this.objImpactoCodigo = value; }
        }

        /// <summary>
        /// C�digo da urg�ncia
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
        /// M�todo que alimenta a cole��o de atributos
        /// </summary>
        private void alimentaColecaoCampos()
        {
            objAtributos.NomeTabela = "PrioridadeCriterio";
            objAtributos.DescricaoTabela = "Crit�rio da prioridade";

            objPrioridadeCodigo.Campo = "prioridade_codigo";
            objPrioridadeCodigo.Descricao = "C�digo da Prioridade";
            objPrioridadeCodigo.CampoIdentificador = true;
            objPrioridadeCodigo.CampoObrigatorio = true;
            objPrioridadeCodigo.Tipo = System.Data.DbType.Int32;
            objAtributos.Add(objPrioridadeCodigo);

            objImpactoCodigo.Campo = "impacto_codigo";
            objImpactoCodigo.Descricao = "C�digo do Impacto";
            objImpactoCodigo.CampoIdentificador = true;
            objImpactoCodigo.CampoObrigatorio = true;
            objImpactoCodigo.Tipo = System.Data.DbType.Int32;
            objAtributos.Add(objImpactoCodigo);

            objUrgenciaCodigo.Campo = "urgencia_codigo";
            objUrgenciaCodigo.Descricao = "C�digo da Urgencia";
            objUrgenciaCodigo.CampoIdentificador = true;
            objUrgenciaCodigo.CampoObrigatorio = true;
            objUrgenciaCodigo.Tipo = System.Data.DbType.Int32;
            objAtributos.Add(objUrgenciaCodigo);
        }
        #endregion

        #region VerificaExisteCriterio
        /// <summary>
        /// M�todo que verifica se existe um determinado registro j� cadastrado na 
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
                            strMensagem = "Este crit�rio j� est� definido para o item selecionado.";
                        }
                        else
                        {
                            bolRetorno = false;

                        }

                        return bolRetorno;
                    }
                    else
                    {
                        strMensagem = "Selecione o Impacto e a Urg�ncia para inserir um novo crit�rio.";
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
        /// M�todo que insere um novo Crit�rio da prioridade
        /// </summary>
        /// <param name="strMensagem">Mensagem com informa��o da execu��o do m�todo.</param>
        /// <returns>Retorna true ou false. Se o registro foi inserido ou n�o.</returns>
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
                    strMensagemErro += "Favor informar a descri��o do Impacto.<br />";

                if (this.objUrgenciaCodigo.Valor == String.Empty)
                    strMensagemErro += "Favor informar a descri��o da Urg�ncia.";

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
        /// M�todo que altera um Crit�rio da prioridade
        /// </summary>
        /// <param name="intPrioridadeCodigoAtual">C�digo da Prioridade atual</param>
        /// <param name="intImpactoCodigoAtual">C�digo do Impacto atual</param>
        /// <param name="intUrgenciaCodigoAtual">C�digo da urg�ncia atual</param>
        /// <param name="intPrioridadeCodigoAlterado">C�digo da prioridade alterado</param>
        /// <param name="intImpactoCodigoAlterado">C�digo do impacto alterado</param>
        /// <param name="intUrgenciaCodigoAlterado">C�digo da urg�ncia alterado</param>
        /// <returns>Retorna True ou False de acordo com a opera��o.</returns>
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
        /// M�todo que exclui um Crit�rio da prioridade onde os tr�s campos formam uma chave.
        /// </summary>
        /// <param name="intPrioridadeCodigo">C�digo da prioridade a ser excluido.</param>
        /// <param name="intImpactoCodigo">C�digo do Impacto a ser excluido.</param>
        /// <param name="intUrgenciaCodigo">C�digo da Urg�ncia a ser excluido.</param>
        /// <returns>Retorna true ou false. Se o registro foi excluido ou n�o.</returns>
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
        /// Gera uma nova geraGridView de acordo com o c�digo da prioridade.
        /// </summary>
        /// <param name="objGridView">geraGridView</param>
        /// <param name="bolCondicao">Condi��o para verificar se ser� utilizado a cl�usula Where</param>
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

