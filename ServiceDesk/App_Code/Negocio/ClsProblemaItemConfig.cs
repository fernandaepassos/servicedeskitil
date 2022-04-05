/*
- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
  • Classe para manipulação dos registros da tabela ProblemaItemConfiguracao.
  
  	Data: 30/11/2005
  	Autor: Fernanda Passos
  	Descrição: Classe que apresenta métodos e propriedades para o objeto ProblemaItemConfiguracao.   
  
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
/// Classe de acesso a dados dos itens de configuração do problema.
/// </summary>
namespace ServiceDesk.Negocio
{
    /// <summary>
    /// Construtor da classe
    /// </summary>
    public class ClsProblemaItemConfig
    {
        #region Declarações

        public ClsProblemaItemConfig()
        {
            alimentaColecaoCampos();
        }

        //Coleção de objetos.
        private ServiceDesk.Banco.ClsAtributos objAtributos = new ServiceDesk.Banco.ClsAtributos();

        //Atributos.
        private ServiceDesk.Banco.ClsAtributo objCodigoProblema = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objCodigoItemConfig = new ServiceDesk.Banco.ClsAtributo();
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
        /// Código do item de configuração.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo CodigoItemConfig
        {
            get { return objCodigoItemConfig; }
            set { this.objCodigoItemConfig = value; }
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
                if (objCodigoItemConfig.Valor.Trim() == null)
                {
                    strMsg = "Informe o item de configuração.";
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
        /// <param name="strNome"></param>
        /// <returns>bool Retorna true ou false. Se existe ou não.</returns>
        public bool VerificaSeJaExisteNoBanco()
        {
            try
            {   //Busca no banco de dados pelo nome.
                ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
                string strSqlCriterio = " problema_codigo = " + Convert.ToInt32(objCodigoProblema.Valor.Trim()) + " ";
                strSqlCriterio += " and item_configuracao_codigo = " + Convert.ToInt32(objCodigoItemConfig.Valor.Trim()) + "";

                string strValor = objBanco.retornaValorCampo("ProblemaItemConfiguracao", "item_configuracao_codigo", strSqlCriterio);
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
                        strMensagem = "Associação já existente no banco de dados.";
                        return false;
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
                ClsProblemaItemConfig objProblemaItemConfig = new ClsProblemaItemConfig();
                ServiceDesk.Controle.ClsGridView.geraGridView(objGridView, objProblemaItemConfig.objAtributos);
                objProblemaItemConfig = null;
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
        public static void geraGridView(System.Web.UI.WebControls.GridView objGridView, ClsProblemaItemConfig objProblemaItemConfig, bool bolCondicao)
        {
            try
            {
                objGridView.AutoGenerateColumns = false;
                ServiceDesk.Controle.ClsGridView.geraGridView(objGridView, objProblemaItemConfig.objAtributos, bolCondicao);
                objProblemaItemConfig = null;
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

                string strSql = " select ItemConfiguracao.item_configuracao_codigo,  ItemConfiguracao.nome  ";
                strSql += " from ItemConfiguracao, ProblemaItemConfiguracao ";
                strSql += " where ProblemaItemConfiguracao.item_configuracao_codigo = ItemConfiguracao.item_configuracao_codigo";
                strSql += " and ProblemaItemConfiguracao.problema_codigo = " + CodigoProblema + "";

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
                        objBanco = null;
                        strMensagem = "Associação já existente no banco de dados.";
                        return false;
                    }

                    else if (objBanco.alteraColecao(this.objAtributos))
                    {
                        objBanco = null;
                        strMensagem = "Registro alterado com sucesso";
                        return true;
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

                //Valida a exclusão.
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
                    objBanco = null;
                    strMsg = "Registro não excluído.";
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

        #region Excluir relação de item de configuração
        /// <summary>
        /// Excluir relação de itens de configuração associado ao problema
        /// </summary>
        /// <param name="intCodigo" > Código do problema </param>
        /// <returns>Retorna true ou false. Se o registro foi excluido ou não.</returns>
        public bool excluiAssociacaoItemConfigDoProblema(int intCodigo)
        {
            try
            {
                String strSql = String.Empty;
                ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();

                strSql = "delete from ProblemaItemConfiguracao where problema_codigo = " + intCodigo + "";

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
                objAtributos.NomeTabela = "ProblemaItemConfiguracao";
                objAtributos.DescricaoTabela = "Tabela que relaciona o item de configuração ao problema.";

                objCodigoProblema.Campo = "problema_codigo";
                objCodigoProblema.Descricao = "Código do problema";
                objCodigoProblema.CampoIdentificador = true;
                objCodigoProblema.CampoObrigatorio = true;
                objCodigoProblema.Tipo = System.Data.DbType.Int32;
                objAtributos.Add(objCodigoProblema);

                objCodigoItemConfig.Campo = "item_configuracao_codigo";
                objCodigoItemConfig.Descricao = "Código do item de configuração";
                objCodigoItemConfig.CampoIdentificador = true;
                objCodigoItemConfig.CampoObrigatorio = true;
                objCodigoItemConfig.Tipo = System.Data.DbType.Int32;
                objAtributos.Add(objCodigoItemConfig);
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