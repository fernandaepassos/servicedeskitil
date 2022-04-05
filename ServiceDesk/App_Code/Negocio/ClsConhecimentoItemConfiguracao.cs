/*
- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
  � Classe que permite a manipula��o atrav�s de recursos dos registros da tabela ConhecimentoItemConfiguracao
    a qual tem por objetivo associar os conhecimentos de solu��es da tabela de Conhecimento aos itens
    de configura��o da empresa.
  
  	Data: 28/12/2005
  	Autor: Fernanda Passos
  	Descri��o: Atrav�s de m�todos e fun��es a classe oferece recursos diversos para manipula��o dos
    registros e intera��o com controles e objetos voltados para interface com usu�rio.
  
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
/// Classe de acesso a dados da tabela ConhecimentoItemConfiguracao.
/// </summary>
namespace ServiceDesk.Negocio
{
    /// <summary>
    /// Construtor da classe
    /// </summary>
    public class ClsConhecimentoItemConfiguracao
    {
        #region Declara��es

        public ClsConhecimentoItemConfiguracao()
        {
            alimentaColecaoCampos();
        }

        //Cole��o de objetos.
        private ServiceDesk.Banco.ClsAtributos objAtributos = new ServiceDesk.Banco.ClsAtributos();

        //Atributos.
        private ServiceDesk.Banco.ClsAtributo objCodigoConhecimento = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objCodigoItemConfig = new ServiceDesk.Banco.ClsAtributo();
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
        /// Codigo do registro do conhecimento.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo CodigoConhecimento
        {
            get { return objCodigoConhecimento ; }
            set { this.objCodigoConhecimento = value; }
        }

        /// <summary>
        /// C�digo do item de configura��o.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo CodigoItemConfig
        {
            get { return objCodigoItemConfig; }
            set { this.objCodigoItemConfig = value; }
        }
        #endregion

        #region Metodos

        #region Valida a exclus�o
        /// <summary>
        /// Valida a exclus�o do conhecimento associado ao item de configura��o
        /// </summary>
        /// <param name="strMensagem">Mensagem com status da opera��o.</param>
        /// <returns>Retorna true ou false. Se foi validado ou n�o.</returns>
        public bool ValidaExclusao(out String strMensagem)
        {
            try
            {
                strMensagem = string.Empty;

                if (objCodigoConhecimento.Valor.Trim() == string.Empty)
                {
                    strMensagem = "Para exclu�r � necess�rio selecionar um conhecimento. Selecione um conhecimento associado ao item de configura��o.";
                    return false;
                }
                else if (objCodigoItemConfig.Valor.Trim() == string.Empty)
                {
                    strMensagem = "Para exclu�r � necess�rio selecionar um conhecimento. Selecione um conhecimento associado ao item de configura��o.";
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                strMensagem = ex.Message;
                throw ex;
            }
        }
        #endregion

        #region Valida dados.
        /// <summary>
        /// Valida dados.
        /// </summary>
        /// <returns>Retorna true ou false. Se foi validado ou n�o.</returns>
        public bool ValidaDados(out String strMsg)
        {
            try
            {
                strMsg = string.Empty;

                if (objCodigoConhecimento.Valor.Trim() == null)
                {
                    strMsg = "O c�digo do conhecimento n�o foi informado. Entre em contato com o Administrador do sistema.";
                    return false;
                }
                if (objCodigoItemConfig.Valor.Trim() == null)
                {
                    strMsg = "O c�digo do item de configura��o n�o foi informado. Entre em contato com o Administrador do sistema.";
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
        /// <param name="strNome"></param>
        /// <returns>bool Retorna true ou false. Se existe ou n�o.</returns>
        public bool VerificaSeJaExisteNoBanco(out String strMensagem)
        {
            strMensagem = string.Empty;
            try
            {   
                ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
                string strSql = " conhecimento_codigo = "+ Convert.ToInt32(objCodigoConhecimento.Valor.Trim ()) +" and ic_codigo = "+ Convert.ToInt32(objCodigoItemConfig.Valor.Trim ()) +"";
                string strValor = objBanco.retornaValorCampo("ConhecimentoIC", "conhecimento_codigo", strSql);

                if (strValor.Trim() != string.Empty)
                {
                    strMensagem = "Este conhecimento j� existe para o item de configura��o selecionado.";
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
        /// <param name="strMensagem">Mensagem com informa��o da execu��o do m�todo.</param>
        /// <returns>Retorna true ou false. Se o registro foi inserido ou n�o.</returns>
        public bool insere(out String strMensagem)
        {
            try
            {
                strMensagem = String.Empty;

                //Verifica se todos os campos foram informandos corretamente.
                if (ValidaDados(out strMensagem) == false) return false;

                //Verifica se os registros a serem inseridos j� existem na tabela.
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
                    return false;
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
                ClsConhecimentoItemConfiguracao objConhecimentoItemConfiguracao = new ClsConhecimentoItemConfiguracao();
                ServiceDesk.Controle.ClsGridView.geraGridView(objGridView, objConhecimentoItemConfiguracao.objAtributos);
                objConhecimentoItemConfiguracao = null;
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
        /// <param name="bolCondicao">Condi��o para verificar se ser� utilizado valores dos campos identificadores</param>
        public static void geraGridView(System.Web.UI.WebControls.GridView objGridView, ClsConhecimentoItemConfiguracao objConhecimentoItemConfiguracao, bool bolCondicao)
        {
            try
            {
                objGridView.AutoGenerateColumns = false;
                ServiceDesk.Controle.ClsGridView.geraGridView(objGridView, objConhecimentoItemConfiguracao.objAtributos, bolCondicao);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region GeraGridView
        /// <summary>
        /// Gera uma nova geraGridView de acordo com o c�digo do item de configura��o. 
        /// </summary>
        /// <param name="objGridView">geraGridView</param>
        /// <param name="Codigo">C�digo do item de configura��o</param>
        public static void geraGridView(System.Web.UI.WebControls.GridView objGridView, int Codigo)
        {
            try
            {
                objGridView.AutoGenerateColumns = false;

                string strSql = "select * ";
                strSql += " from Conhecimento C, ConhecimentoIC CIC";
                strSql += " where CIC.conhecimento_codigo = C.conhecimento_codigo";

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

                if (ValidaDados(out strMensagem) == false) return false;

                if (VerificaSeJaExisteNoBanco(out strMensagem) == true) return false;

                ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();

                if (objBanco.alteraColecao(this.objAtributos))
                {
                    strMensagem = "Registro alterado com sucesso.";
                    objBanco = null;
                    return true;
                }
                else
                {
                    strMensagem = "Imposs�vel alterar registro.";
                    objBanco = null;
                    return false;
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
                if (ValidaExclusao(out strMsg) == false) return false;

                ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
                if (objBanco.apagaColecao(this.objAtributos))
                {
                    strMsg = "Registro exclu�do com sucesso.";
                    objBanco = null;
                    return true;
                }
                else
                {
                    objBanco = null;
                    strMsg = "Imposs�vel excluir registro.";
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

        #region Excluir
        /// <summary>
        /// Exclui todos as associa��es de conhecimento por item de configura��o.
        /// </summary>
        /// <param name="intCodigo" > C�digo do item de configura��o.</param>
        /// <param name="strMensagem">Mensagem de retorno com status da opera��o.</param> 
        /// <returns>Retorna true ou false. Se o registro foi excluido ou n�o.</returns>
        public bool exclui(out String strMensagem, int intCodigo)
        {
            strMensagem = string.Empty;
            try
            {
                

                ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();

                string strSql = "delete from ConhecimentoIC";
                strSql += " where ic_codigo = " + intCodigo + "";

                if (objBanco.executaSQL(strSql))
                {
                    strMensagem = "Registro exclu�do com sucesso.";
                    objBanco = null;
                    return true;
                }
                else
                {
                    strMensagem = "Imposs�vel excluir registro.";
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
                objAtributos.NomeTabela = "ConhecimentoIC";
                objAtributos.DescricaoTabela = "Tabela que associa conhecimentos de solu��es aos itens de configura��o.";

                objCodigoConhecimento.Campo = "conhecimento_codigo";
                objCodigoConhecimento.Descricao = "C�digo do conhecimento.";
                objCodigoConhecimento.CampoIdentificador = true;
                objCodigoConhecimento.CampoObrigatorio = true;
                objCodigoConhecimento.Tipo = System.Data.DbType.Int32;
                objAtributos.Add(objCodigoConhecimento);

                objCodigoItemConfig.Campo = "ic_codigo";
                objCodigoItemConfig.Descricao = "C�digo do item de configura��o.";
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

        #region Popula n�s com conhecimento associados ao item de configura��o informado.
        /// <summary>
        /// Popula n�s com conhecimento associados ao item de configura��o informado.
        /// </summary>
        /// <param name="intCodigoItemConfiguracao">C�digo do item de configura��o</param>
        /// <param name="objTreeView">Nome da treeview</param>
        public static void PopulaN�s(int intCodigoItemConfiguracao, TreeView objTreeView)
        {
            try
            {
                String strSql = String.Empty;

                strSql = "select C.conhecimento_codigo, C.nome from ConhecimentoIC CIC, Conhecimento C";
                strSql += " where C.conhecimento_codigo = CIC.conhecimento_codigo";
                if (intCodigoItemConfiguracao > 0) strSql += " and CIC.ic_codigo = " + intCodigoItemConfiguracao + "";
                if (intCodigoItemConfiguracao <= 0) strSql += " and CIC.ic_codigo is not null";

                System.Data.SqlClient.SqlDataReader objDataReader = ServiceDesk.Banco.ClsBanco.geraDataReader(strSql);
                objTreeView.Nodes.Clear();
                while (objDataReader.Read())
                {
                    //objTreeView.Nodes.Add(new TreeNode("<a href=\"BaseConhecimento.ascx?codigo=" + objDataReader["conhecimento_codigo"].ToString() + "\">" + objDataReader["nome"].ToString() + "</a>", objDataReader["conhecimento_codigo"].ToString().Trim()));
                    objTreeView.Nodes.Add(new TreeNode(objDataReader["nome"].ToString(), objDataReader["conhecimento_codigo"].ToString()));
                }
                objDataReader.Dispose();
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