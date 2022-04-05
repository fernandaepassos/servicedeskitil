/*
- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
  � Classe para manipula��o dos registros da tabela Problema.
  
  	Data: 29/11/2005
  	Autor: Fernanda Passos
  	Descri��o: Esta classe disp�e de v�rios m�todos e atributos que permitem v�rias a��es quanto 
    aos registros da tabela Problema. Esta voltado para o gerenciamento de problemas.
  
  
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
/// Classe de acesso a dados da tabela Problema.
/// </summary>
namespace ServiceDesk.Negocio
{
    /// <summary>
    /// Construtor da classe
    /// </summary>
    public class ClsRecorrenciaLog
    {
        #region Construtor da classe
        public ClsRecorrenciaLog()
        {
            alimentaColecaoCampos();
        }
        #endregion

        #region Declara��es

        //Cole��o de objetos.
        private ServiceDesk.Banco.ClsAtributos objAtributos = new ServiceDesk.Banco.ClsAtributos();

        //Atributos.
        private ServiceDesk.Banco.ClsAtributo objCodigo = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objData = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objTabela = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objIdentificadorOrigem = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objIdentificadorRegistro = new ServiceDesk.Banco.ClsAtributo();
        #endregion

        #region Propriedades
        /// <summary>
        /// Cole��o de atributos
        /// </summary>
        public ServiceDesk.Banco.ClsAtributos Atributos
        {
            get
            {
                return this.objAtributos;
            }
        }

        /// <summary>
        /// Codigo da recorr�ncia.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo Codigo
        {
            get { return objCodigo; }
            set { this.objCodigo = value; }
        }

        /// <summary>
        /// Data da recorr�ncia
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo Data
        {
            get { return objData ; }
            set { this.objData = value; }
        }

        /// <summary>
        /// Tabela, objeto ou entidade que apresentou recorr�ncia (Ex: Problema, Incidente, Item Configura��o e etc..).
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo Tabela
        {
            get { return objTabela; }
            set { this.objTabela = value; }
        }

        /// <summary>
        /// Identificador do campo na tabela.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo IdentificadorOrigem
        {
            get { return objIdentificadorOrigem; }
            set { this.objIdentificadorOrigem = value; }
        }

        /// <summary>
        /// Identificador do registro na tabela.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo IdentificadorRegistro 
        {
            get { return objIdentificadorRegistro; }
            set { this.objIdentificadorRegistro = value; }
        }

        #endregion

        #region Metodos

        #region Validacao dos dados
        /// <summary>
        /// Valida��o da integridade dos registros.
        /// </summary>
        /// <returns>Retorna true ou false. Se for validado ou n�o.</returns>
        public bool ValidacaoDados(out String strMsg)
        {
            try
            {
                strMsg = String.Empty;

                if (objCodigo.Valor.Trim() == string.Empty)
                {
                    strMsg = "Informe o c�digo identificador da recorr�ncia.";
                    return false;
                }
                else if (objData.Valor.Trim() == string.Empty)
                {
                    strMsg = "Informe a data da recorr�ncia.";
                    return false;
                }
                else if (objTabela.Valor.Trim() == string.Empty)
                {
                    strMsg = "Informe a tabela.";
                    return false;
                }
                else if (objIdentificadorOrigem.Valor.Trim() == string.Empty)
                {
                    strMsg = "Informe o registro identificador da origem.";
                    return false;
                }
                else if (objIdentificadorRegistro.Valor.Trim() == string.Empty)
                {
                    strMsg = "Informe o identificador do registro da tabela.";
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

        #region Verifica se j� existe o registro no banco de dados.
        /// <summary>
        /// Verifica se j� existe o registro no banco de dados.
        /// </summary>
        /// <param name="intCodRegistro">C�digo do registro atual</param> 
        /// <param name="strMsg">Mensagem que o m�todo retorna com status da opera��o.</param> 
        /// <returns>Retorna true ou false. Se existe ou n�o.</returns>
        public bool VerificaSeJaExisteNoBanco(out String strMsg)
        {
            strMsg = string.Empty;
            try
            {
                ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();

                string strSql = " data = "+ objData .Valor.Trim () +"";
                strSql += " and tabela = '"+ objTabela .Valor .Trim () +"'";
                strSql += " and identificador_registro = "+ Convert.ToInt32 (objIdentificadorRegistro.Valor .Trim ()) +"";
                strSql += " and identificador_origem = "+ Convert.ToInt32 (objIdentificadorOrigem.Valor .Trim ()) +"";

                string strValor = objBanco.retornaValorCampo("RecorrenciaLog", "tabela", strSql);

                //Verifica se foi encontrado registro dentro do crit�rio.
                if (strValor.Trim() != string.Empty)
                {
                    strMsg = "Registro de recorr�ncia j� existe no banco.";
                    return true;
                }
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
        /// <param name="strMensagem">Mensagem com status da execu��o do m�todo.</param>
        /// <returns>Retorna true ou false. Se o registro foi inserido ou n�o.</returns>
        public bool insere(out String strMensagem)
        {
            try
            {

                strMensagem = String.Empty;
                bool bolRetorno = false;

                if (ValidacaoDados(out strMensagem) == false)
                {
                    bolRetorno = false;
                }
                else
                {
                    ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
                    if (VerificaSeJaExisteNoBanco(out strMensagem) == true)
                    {
                        bolRetorno = false;
                    }
                    else if (objBanco.insereColecao(this.objAtributos))
                    {
                        strMensagem = "Recorr�ncia inserida com sucesso.";
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
        /// <returns>Retorna true ou false. Se o registro foi alterado ou n�o.</returns>
        public bool altera(out String strMensagem)
        {
            try
            {
                strMensagem = String.Empty;
                bool bolRetorno = false;

                if (ValidacaoDados(out strMensagem) == false) bolRetorno = false;
                else
                {
                    ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
                    if (VerificaSeJaExisteNoBanco(out strMensagem) == true) bolRetorno = false;
                    else if (objBanco.alteraColecao(this.objAtributos))
                    {
                        strMensagem = "Recorr�ncia alterada com sucesso.";
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

                //Valida a exclus�o.
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
            catch (Exception ex)
            {
                strMsg = ex.Message;
                throw ex;
            }
        }
        #endregion

        #region Alimenta campos cole��o
        /// <summary>
        /// Alimenta campos cole��o
        /// </summary>
        private void alimentaColecaoCampos()
        {
            try
            {
                objAtributos.NomeTabela = "RecorrenciaLog";
                objAtributos.DescricaoTabela = "Tabela que armazena as recorr�ncias.";

                objCodigo.Campo = "recorrencia_log_codigo";
                objCodigo.Descricao = "C�digo do registro da recorr�ncia.";
                objCodigo.CampoIdentificador = true;
                objCodigo.CampoObrigatorio = true;
                objCodigo.Tipo = System.Data.DbType.Int32;
                objAtributos.Add(objCodigo);

                objData.Campo = "data";
                objData.Descricao = "Data em que ocorreu a recorr�ncia.";
                objData.CampoIdentificador = false;
                objData.CampoObrigatorio = true;
                objData.Tipo = System.Data.DbType.DateTime;
                objAtributos.Add(objData);

                objTabela.Campo = "tabela";
                objTabela.Descricao = "Tabela onde surgiu a recorr�ncia.";
                objTabela.CampoIdentificador = false;
                objTabela.CampoObrigatorio = true;
                objTabela.Tipo = System.Data.DbType.String;
                objAtributos.Add(objTabela);

                objIdentificadorOrigem.Campo = "identificador_origem";
                objIdentificadorOrigem.Descricao = "N�mero do registro identificador da origem.";
                objIdentificadorOrigem.CampoIdentificador = false;
                objIdentificadorOrigem.CampoObrigatorio = true;
                objIdentificadorOrigem.Tipo = System.Data.DbType.Int32;
                objAtributos.Add(objIdentificadorOrigem);

                objIdentificadorRegistro.Campo = "identificador_registro";
                objIdentificadorRegistro.Descricao = "N�mero do registro na tabela de onde surgiu a recorr�ncia.";
                objIdentificadorRegistro.CampoIdentificador = false;
                objIdentificadorRegistro.CampoObrigatorio = true;
                objIdentificadorRegistro.Tipo = System.Data.DbType.Int32;
                objAtributos.Add(objIdentificadorRegistro);
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
        /// <param name="objGridView">objeto Grid View</param>
        /// <param name="bolCondicao">Condi��o para verificar se ser� utilizado a cl�usula Where</param>
        public static void geraGridView(System.Web.UI.WebControls.GridView objGridView, ClsRecorrenciaLog objRecorrenciaLog, bool bolCondicao)
        {
            try
            {
                objGridView.AutoGenerateColumns = false;
                ServiceDesk.Controle.ClsGridView.geraGridView(objGridView, objRecorrenciaLog.Atributos, bolCondicao);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Verifica se � uma recorr�ncia do IC
        /// <summary>
        /// Verifica se esta ocorrendo uma recorr�ncia do IC.
        /// </summary>
        /// <param name="intCodigoIC">C�digo do item de configura��o.</param>
        /// <param name="strTabela">Nome do processo no banco de dados que esta se relacionando com o item de configura��o.</param>
        /// <param name="intCodigoRegistroTabela" >N�mero do registro do processo que esta associando o IC.</param>
        /// <returns>Retorna true ou false. Se � uma recorr�ncia ou n�o.</returns> 
        public static bool VerificaSeRecorrencia(int intCodigoIC, String  strTabela, int intCodigoRegistroTabela)
        {
            try
            {
                if (strTabela == "Problema")
                {
                    //Verifica se j� existe o item de configura��o informado para 
                    //algum problema existente no banco de dados diferente do problema atual.
                    string strSql = " select problema_codigo ";
                    strSql += " from ProblemaItemConfiguracao";
                    strSql += " where item_configuracao_codigo = " + intCodigoIC + "";
                    strSql += " and problema_codigo <> " + intCodigoRegistroTabela + "";

                    //Executa a consulta.
                    System.Data.SqlClient.SqlDataReader dtReader = ServiceDesk.Banco.ClsBanco.geraDataReader(strSql);
                    //Se retorna registro � recorr�ncia.
                    if (dtReader.Read())
                        return true;
                    else//N�o � recorr�ncia.
                        return false;
                }
                else if (strTabela == "Incidente")
                {
                    return true;
                }
                else if (strTabela == "ItemConfiguracao")
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Pega o pr�ximo c�digo identificador da tabela
        /// <summary>
        /// Pega o pr�ximo registro identificador da tabela.
        /// </summary>
        /// <returns>Retorna n�mero inteiro do pr�ximo identificador ou zero se houver erro.</returns>
        public int GetMaxId()
        {
            try
            {
                string strSql = "select max(recorrencia_log_codigo) as maximo from RecorrenciaLog";

                System.Data.SqlClient.SqlDataReader objDtReader = ServiceDesk.Banco.ClsBanco.geraDataReader(strSql);

                if (objDtReader.Read())
                {
                    string strMax = objDtReader["maximo"].ToString();
                    if (strMax == string.Empty)
                        return 1;
                    else
                        return Convert.ToInt32(objDtReader["maximo"].ToString()) + 1;
                }
                else
                    return 1;

            }
            catch
            {
                return 0;
            }

        }
        #endregion

        #region GeraGridView
        /// <summary>
        /// Gera gridview com campos especificos.
        /// </summary>
        /// <param name="objGridView">Nome do GridView que receber� os dados.</param>
        /// <param name="strTabela">Nome da tabela que esta relacionado IC.</param>
        public static void geraGridView(System.Web.UI.WebControls.GridView objGridView, string strTabela)
        {
            try
            {
                objGridView.AutoGenerateColumns = false;

                string strSql = "select R.*, IC.nome";
                strSql += " from RecorrenciaLog R, ItemConfiguracao IC";
                strSql += " where IC.item_configuracao_codigo = R.identificador_origem";
                strSql += " and R.tabela = '" + strTabela.Trim() + "'";

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

        #endregion
    }
}