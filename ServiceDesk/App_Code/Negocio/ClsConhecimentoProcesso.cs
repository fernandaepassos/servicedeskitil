/*
- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
  • Classe para manipulação dos registros da tabela ConhecimentoProcesso.
  
  	Data: 28/12/2005
  	Autor: Fernanda Passos
  	Descrição: Classe que permite a minipulação dos registros da tabela de ConhecimentoProcesso 
    a qual tem por objetivo associar os processos (Incidente, Chamado, Problema e outros) que
    podem esta para pesquisa na base de conhecimento.
  
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
/// Classe de acesso a dados da tabela ConhecimentoProcesso.
/// </summary>
namespace ServiceDesk.Negocio
{
    /// <summary>
    /// Construtor da classe
    /// </summary>
    public class ClsConhecimentoProcesso
    {
        #region Declarações
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
        /// Coleção de atributos
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
        /// Código do registro identificador na tabela que representa o processo.
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
        /// <returns>Retorna true ou false. Se os dados foram aprovados ou não.</returns>
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
                    strMsg = "A tabela que representa o processo não foi informada. Informe ao administrador do sistema.";
                    return false;
                }
                else if (objTabelaIdentificador.Valor.Trim() == null)
                {
                    strMsg = "O código identificador da tabela não foi informado. Informe ao administrador do sistema.";
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

        #region Verifica se dados já existem
        /// <summary>
        /// Verifica se já existe.
        /// </summary>
        /// <returns>Retorna true ou false. Se existe ou não.</returns>
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
                    strMensagem = "Este registro já encontra-se na base de conhecimento.";
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
        /// <param name="strMensagem">Mensagem com informação da execução do método.</param>
        /// <returns>Retorna true ou false. Se o registro foi inserido ou não.</returns>
        public bool insere(out String strMensagem)
        {
            try
            {
                strMensagem = String.Empty;

                //Verifica se os dados a ser inseridos são válidos.
                if (ValidaDados(out strMensagem) == false) return false;

                //Verifica se os dados a se inseridos já existem na base de dados.
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
                    strMensagem = "Impossível inserir registro.";
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
        /// Gera uma nova geraGridView de acordo com a coleção de atributos.
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
        /// Gera uma nova geraGridView de acordo com a coleção de atributos.
        /// </summary>
        /// <param name="objGridView">Nome do Grid View</param>
        /// <param name="bolCondicao">Condição para verificar se será utilizado a cláusula Where</param>
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
        /// <returns>Retorna true ou false. Se o registro foi alterado ou não.</returns>
        public bool altera(out String strMensagem)
        {
            try
            {
                strMensagem = String.Empty;

                //Verifica se os são a serem alterados são válidos ou não.
                if (ValidaDados(out strMensagem) == false) return false;

                //Verifica se os registro a ser alterado já existe no banco de dados.
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
                    strMensagem = "Registro não alterado.";
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
        /// <returns>Retorna true ou false. Se o registro foi excluido ou não.</returns>
        public bool exclui(out String strMsg)
        {
            try
            {
                strMsg = string.Empty;

                //Valida a exclusão.
                //if (ValidaExclusao(out strMsg) == false)return false;

                //Valida a exclusão.
                if (ServiceDesk.Negocio.ClsIdentificador.ValidaExclusao(objCodigo.Campo, objCodigo.Valor.Trim(), out strMsg, true, false, objAtributos.NomeTabela.Trim()) == false) return false;

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
            catch (System.Exception ex)
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
        /// Alimentar coleção de atributos
        /// </summary>
        private void alimentaColecaoCampos()
        {
            try
            {
                objAtributos.NomeTabela = "ConhecimentoProcesso ";
                objAtributos.DescricaoTabela = "Tabela que armazena as informações do processo que estão na base de conhecimento para consultas.";

                objCodigo.Campo = "conhecimento_processo_codigo";
                objCodigo.Descricao = "Código do registro na tabela ConhecimentoProcesso";
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
                objTabelaIdentificador.Descricao = "Código identificador do registro na tabela que representa o processo.";
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

        #region Notificação de envio para base de conhecimento
        /// <summary>
        /// Notificação de envia para base de conhecimento.
        /// </summary>
        /// <param name="strTabela">Nome da tabela que representa o processo.</param>
        /// <param name="strTabelaIdentificador">Número do registro identificador na tabela.</param>
        /// <param name="strCodigoUsuarioEnvia">Código do usuário emissor.</param>
        /// <param name="strCodigoUsuarioRecebe">Código do usuário receptor.</param>
        /// <returns>Retorna true ou false. Se foi enviado ou não.</returns> 
        public static bool EnviaParaBaseConhecimento(string strTabela, string strTabelaIdentificador, string strCodigoUsuarioEnvia, string strCodigoUsuarioRecebe, out string strMensagem)
        {
            try
            {
                strMensagem = string.Empty;
                ServiceDesk.Negocio.ClsNotificacao objNotificacao = new ClsNotificacao();

                //Por Sylvio 14/02/2006 - troquei para usar clsIdentificador
                //nao atualizava contador depois da inserção
                ClsIdentificador objIdentificador = new ClsIdentificador();
                objIdentificador.Tabela.Valor = "Notificacao";
                
                objNotificacao.Tabela.Valor = strTabela.Trim();
                objNotificacao.IdentificadorTabela.Valor = strTabelaIdentificador.Trim();
                objNotificacao.Descricao.Valor = "KB: " + ClsUsuario.getNomeUsuario(strCodigoUsuarioEnvia) + " informa que foi encaminhado para base de conhecimento um registro do processo para sua aprovação.";
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

        #region Pega o próximo código identificador da tabela
        /// <summary>
        /// Pega o próximo registro identificador da tabela.
        /// </summary>
        /// <returns>Retorna número inteiro do próximo identificador ou zero se houver erro.</returns>
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