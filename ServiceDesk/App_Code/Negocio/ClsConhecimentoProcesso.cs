/*
- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
  � Classe para manipula��o dos registros da tabela ConhecimentoProcesso.
  
  	Data: 28/12/2005
  	Autor: Fernanda Passos
  	Descri��o: Classe que permite a minipula��o dos registros da tabela de ConhecimentoProcesso 
    a qual tem por objetivo associar os processos (Incidente, Chamado, Problema e outros) que
    podem esta para pesquisa na base de conhecimento.
  
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
/// Classe de acesso a dados da tabela ConhecimentoProcesso.
/// </summary>
namespace ServiceDesk.Negocio
{
    /// <summary>
    /// Construtor da classe
    /// </summary>
    public class ClsConhecimentoProcesso
    {
        #region Declara��es
        public ClsConhecimentoProcesso()
        {
            alimentaColecaoCampos();
        }
        private ServiceDesk.Banco.ClsAtributos objAtributos = new ServiceDesk.Banco.ClsAtributos();
        private ServiceDesk.Banco.ClsAtributo objCodigo = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objTabela = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objTabelaIdentificador = new ServiceDesk.Banco.ClsAtributo();
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
        /// Codigo do registro de processo no conhecimento.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo Codigo
        {
            get { return objCodigo; }
            set { this.objCodigo = value; }
        }

        /// <summary>
        /// Nome da tabela que armazena dados do processo.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo Tabela
        {
            get { return objTabela; }
            set { this.objTabela = value; }
        }

        /// <summary>
        /// C�digo do registro identificador na tabela que representa o processo.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo TabelaIdentificador
        {
            get { return objTabelaIdentificador ; }
            set { this.objTabelaIdentificador = value; }
        }
        #endregion

        #region Metodos

        #region GeraGridView
        /// <summary>
        /// Gera grid view com campos especificos
        /// </summary>
        /// <param name="objGridView">Nome do GridView</param>
        public static void geraGridView(System.Web.UI.WebControls.GridView objGridView, string strTabela, int intTabelaIdentificador)
        {
            try
            {
                objGridView.AutoGenerateColumns = false;

                string strSql = "select * ";
                strSql += " from ConhecimentoProcesso";
                strSql += " where conhecimento_processo_codigo is not null";
                if (strTabela != string.Empty && strTabela != "Todos") strSql += " and tabela = '" + strTabela.Trim() + "'";
                if (intTabelaIdentificador > 0) strSql += " and tabela_identificador = " + intTabelaIdentificador  + "";

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

        #region Valida dados.
        /// <summary>
        /// Valida dados.
        /// </summary>
        /// <returns>Retorna true ou false. Se os dados foram aprovados ou n�o.</returns>
        public bool ValidaDados(out String strMsg)
        {
            try
            {
                strMsg = string.Empty;

                if (objCodigo.Valor.Trim() == null)
                {
                    strMsg = "Informe o processo.";
                    return false;
                }
                else if (objTabela.Valor.Trim() == null)
                {
                    strMsg = "A tabela que representa o processo n�o foi informada. Informe ao administrador do sistema.";
                    return false;
                }
                else if (objTabelaIdentificador.Valor.Trim() == null)
                {
                    strMsg = "O c�digo identificador da tabela n�o foi informado. Informe ao administrador do sistema.";
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

        #region Verifica se dados j� existem
        /// <summary>
        /// Verifica se j� existe.
        /// </summary>
        /// <returns>Retorna true ou false. Se existe ou n�o.</returns>
        public bool VerificaSeJaExisteNoBanco(out String strMensagem)
        {
            strMensagem = string.Empty;
            try
            {

                string strSql = string.Empty;

                strSql = " tabela = '"+ objTabela.Valor.Trim() +"'";
                strSql += " and tabela_identificador = "+ Convert.ToInt32(objTabelaIdentificador.Valor.Trim()) +"";

                ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();

                string strValor = objBanco.retornaValorCampo("ConhecimentoProcesso", "tabela", strSql);

                if (strValor.Trim() != string.Empty)
                {
                    objBanco = null;
                    strMensagem = "Este registro j� encontra-se na base de conhecimento.";
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

        #region Inserir
        /// <summary>
        /// Inserir
        /// </summary>
        /// <param name="strMensagem">Mensagem com informa��o da execu��o do m�todo.</param>
        /// <returns>Retorna true ou false. Se o registro foi inserido ou n�o.</returns>
        public bool insere(out String strMensagem)
        {
            try
            {
                strMensagem = String.Empty;

                //Verifica se os dados a ser inseridos s�o v�lidos.
                if (ValidaDados(out strMensagem) == false) return false;

                //Verifica se os dados a se inseridos j� existem na base de dados.
                if (VerificaSeJaExisteNoBanco(out strMensagem) == true) return false;

                ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();

                if (objBanco.insereColecao(this.objAtributos))
                {
                    strMensagem = "Registro inserido com sucesso.";
                    objBanco = null;
                    return true;
                }
                else
                {
                    strMensagem = "Imposs�vel inserir registro.";
                    objBanco = null;
                    return true;
                }
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
        /// Gera uma nova geraGridView de acordo com a cole��o de atributos.
        /// </summary>
        /// <param name="objGridView">objeto gridview</param>
        public static void geraGridView(System.Web.UI.WebControls.GridView objGridView)
        {
            try
            {
                objGridView.AutoGenerateColumns = false;
                ClsConhecimentoProcesso objConhecimentoProcesso = new ClsConhecimentoProcesso();
                ServiceDesk.Controle.ClsGridView.geraGridView(objGridView, objConhecimentoProcesso.Atributos);
                objConhecimentoProcesso = null;
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
        /// <param name="objGridView">Nome do Grid View</param>
        /// <param name="bolCondicao">Condi��o para verificar se ser� utilizado a cl�usula Where</param>
        public static void geraGridView(System.Web.UI.WebControls.GridView objGridView, ClsConhecimentoProcesso objConhecimentoProcesso, bool bolCondicao)
        {
            try
            {
                objGridView.AutoGenerateColumns = false;
                ServiceDesk.Controle.ClsGridView.geraGridView(objGridView, objConhecimentoProcesso.objAtributos, bolCondicao);
                objConhecimentoProcesso = null;
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

                //Verifica se os s�o a serem alterados s�o v�lidos ou n�o.
                if (ValidaDados(out strMensagem) == false) return false;

                //Verifica se os registro a ser alterado j� existe no banco de dados.
                if (VerificaSeJaExisteNoBanco(out strMensagem) == true) return false;

                ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
                if (objBanco.alteraColecao(this.objAtributos))
                {
                    strMensagem = "Registro alterado com sucesso";
                    objBanco = null;
                    return true;
                }
                else
                {
                    strMensagem = "Registro n�o alterado.";
                    objBanco = null;
                    return true;
                }
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
                //if (ValidaExclusao(out strMsg) == false)return false;

                //Valida a exclus�o.
                if (ServiceDesk.Negocio.ClsIdentificador.ValidaExclusao(objCodigo.Campo, objCodigo.Valor.Trim(), out strMsg, true, false, objAtributos.NomeTabela.Trim()) == false) return false;

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

        #region Validacao exclus�o
        /// <summary>
        /// Valida exclus�o
        /// </summary>
        /// <returns>Retorna true ou false. Se for validado ou n�o.</returns>
        public bool ValidaExclusao(out String strMsg)
        {
            try
            {
                strMsg = String.Empty;

                return true;
            }
            catch (System.Exception ex)
            {
                strMsg = ex.Message;
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
                objAtributos.NomeTabela = "ConhecimentoProcesso ";
                objAtributos.DescricaoTabela = "Tabela que armazena as informa��es do processo que est�o na base de conhecimento para consultas.";

                objCodigo.Campo = "conhecimento_processo_codigo";
                objCodigo.Descricao = "C�digo do registro na tabela ConhecimentoProcesso";
                objCodigo.CampoIdentificador = true;
                objCodigo.CampoObrigatorio = true;
                objCodigo.Tipo = System.Data.DbType.Int32;
                objAtributos.Add(objCodigo);

                objTabela.Campo = "tabela";
                objTabela.Descricao = "Nome da tabela que representa o processo.";
                objTabela.CampoIdentificador = false;
                objTabela.CampoObrigatorio = true;
                objTabela.Tipo = System.Data.DbType.String;
                objTabela.Tamanho = 100;
                objAtributos.Add(objTabela);

                objTabelaIdentificador.Campo = "tabela_identificador";
                objTabelaIdentificador.Descricao = "C�digo identificador do registro na tabela que representa o processo.";
                objTabelaIdentificador.CampoIdentificador = false;
                objTabelaIdentificador.CampoObrigatorio = true;
                objTabelaIdentificador.Tipo = System.Data.DbType.Int32;
                objTabelaIdentificador.Tamanho = 7500;
                objAtributos.Add(objTabelaIdentificador);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion

        #region GeraDropDownList
        /// <summary>
        /// Gera um novo DropDownList com os nomes das tabela que representam os processos.
        /// </summary>
        /// <param name="objDropDownList">DropDownList</param>
        public static void geraDropDownList(System.Web.UI.WebControls.DropDownList objDropDownList)
        {
            try
            {
                string strSql = "select distinct(Tabela) as tabela ";
                strSql += " from ConhecimentoProcesso";
                strSql += " order by tabela";

                System.Data.SqlClient.SqlDataReader objReader = ServiceDesk.Banco.ClsBanco.geraDataReader(strSql);

                objDropDownList.Items.Clear();
                if (objReader.FieldCount > 0)
                {
                    objDropDownList.Items.Add("Todos");
                }

                while (objReader.Read())
                {
                    objDropDownList.Items.Add(objReader["tabela"].ToString());
                }
                objReader.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Notifica��o de envio para base de conhecimento
        /// <summary>
        /// Notifica��o de envia para base de conhecimento.
        /// </summary>
        /// <param name="strTabela">Nome da tabela que representa o processo.</param>
        /// <param name="strTabelaIdentificador">N�mero do registro identificador na tabela.</param>
        /// <param name="strCodigoUsuarioEnvia">C�digo do usu�rio emissor.</param>
        /// <param name="strCodigoUsuarioRecebe">C�digo do usu�rio receptor.</param>
        /// <returns>Retorna true ou false. Se foi enviado ou n�o.</returns> 
        public static bool EnviaParaBaseConhecimento(string strTabela, string strTabelaIdentificador, string strCodigoUsuarioEnvia, string strCodigoUsuarioRecebe, out string strMensagem)
        {
            try
            {
                strMensagem = string.Empty;
                ServiceDesk.Negocio.ClsNotificacao objNotificacao = new ClsNotificacao();

                //Por Sylvio 14/02/2006 - troquei para usar clsIdentificador
                //nao atualizava contador depois da inser��o
                ClsIdentificador objIdentificador = new ClsIdentificador();
                objIdentificador.Tabela.Valor = "Notificacao";
                
                objNotificacao.Tabela.Valor = strTabela.Trim();
                objNotificacao.IdentificadorTabela.Valor = strTabelaIdentificador.Trim();
                objNotificacao.Descricao.Valor = "KB: " + ClsUsuario.getNomeUsuario(strCodigoUsuarioEnvia) + " informa que foi encaminhado para base de conhecimento um registro do processo para sua aprova��o.";
                objNotificacao.CodigoUsuarioEmissor.Valor = strCodigoUsuarioEnvia;
                objNotificacao.CodigoUsuarioReceptor.Valor = strCodigoUsuarioRecebe;
                objNotificacao.DtInclusao.Valor = DateTime.Now.ToString(ClsParametro.DataInclusao);
                //objNotificacao.Codigo.Valor = objNotificacao.GetMaxId().ToString();
                objNotificacao.Codigo.Valor = objIdentificador.getProximoValor().ToString();
                
                if (objNotificacao.enviar(out strMensagem) == false)
                {                 
                  objNotificacao = null;
                  objIdentificador = null;
                  return false;
                }
                else
                {
                  objIdentificador.atualizaValor();                  
                  objNotificacao = null;
                  objIdentificador = null;
                  return true;
                }
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
                string strSql = "select max(conhecimento_processo_codigo) as maximo from ConhecimentoProcesso";

                System.Data.SqlClient.SqlDataReader objDtReader = ServiceDesk.Banco.ClsBanco.geraDataReader(strSql);

                if (objDtReader.Read())
                {
                    string strMax = objDtReader["maximo"].ToString();
                    if (strMax == string.Empty)
                    {
                        objDtReader.Dispose();
                        return 1;
                    }
                    else
                    {
                        objDtReader.Dispose();
                        return Convert.ToInt32(strMax) + 1;
                    }
                }
                else
                {
                    objDtReader.Dispose();
                    return 1;
                }

            }
            catch
            {
                return 0;
            }

        }
        #endregion
        #endregion
    }
}