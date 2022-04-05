/*
- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
  � Classe para manipula��o dos registros da tabela ConhecimentoConhecimento.cs.
  
  	Data: 28/12/2005
  	Autor: Fernanda Passos
  	Descri��o: Classe que apresenta v�rias funcionalidades para maniplu��o dos registro da tabela 
    ConhecimentoConhecimento a qual ter por objetivos informa a um conhecimento que o mesmo faz parte
    de outro conhecimento.
  
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
/// Classe de acesso a dados da tabela ConhecimentoConhecimento.
/// </summary>
namespace ServiceDesk.Negocio
{
    /// <summary>
    /// Construtor da classe
    /// </summary>
    public class ClsConhecimentoConhecimento
    {
        #region Declara��es

        public ClsConhecimentoConhecimento()
        {
            alimentaColecaoCampos();
        }

        private ServiceDesk.Banco.ClsAtributos objAtributos = new ServiceDesk.Banco.ClsAtributos();

        private ServiceDesk.Banco.ClsAtributo objCodigoConhecimentoOrigem = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objCodigoConhecimentoDestino = new ServiceDesk.Banco.ClsAtributo();
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
        /// Codigo do conhecimento de origem.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo CodigoConhecimentoOrigem
        {
            get { return objCodigoConhecimentoOrigem ; }
            set { this.objCodigoConhecimentoOrigem = value; }
        }

        /// <summary>
        /// C�digo do conhecimento de destino.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo CodigoConhecimentoDestino
        {
            get { return objCodigoConhecimentoDestino ; }
            set { this.objCodigoConhecimentoDestino = value; }
        }


        #endregion

        #region Metodos

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

                if (objCodigoConhecimentoOrigem.Valor.Trim() == null)
                {
                    strMsg = "Informe o c�digo do conhecimento de origem.";
                    return false;
                }
                else if (objCodigoConhecimentoDestino.Valor.Trim() == null)
                {
                    strMsg = "Informe o c�digo do conhecimento de destino.";
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
        public bool VerificaSeJaExisteNoBanco(out String strMensagem)
        {
            strMensagem = string.Empty;

            try
            {   //Busca no banco de dados pelo nome.
                ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
                string strSql = " conhecimento_codigo_origem = " + Convert.ToInt32(objCodigoConhecimentoOrigem.Valor.Trim()) + " ";
                strSql += " and conhecimento_codigo_destino = " + Convert.ToInt32(objCodigoConhecimentoDestino.Valor.Trim()) + "";

                string strValor = objBanco.retornaValorCampo("ConhecimentoConhecimento", "conhecimento_codigo_origem", strSql);
                objBanco = null;

                //Verifica se o nome foi encontrado.
                if (strValor.Trim() != string.Empty)
                {
                    strMensagem = "Associa��o j� existente no banco de dados.";
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
        public bool insere(out String strMsg)
        {
            try
            {
                bool bolRetorno = false;
                strMsg = string.Empty;

                if (ValidaDados(out strMsg) == false)
                {
                    return false;
                }
                else if (VerificaSeJaExisteNoBanco(out strMsg) == true)
                {
                    return false;
                }
                else
                {
                    //Insere no banco de dados
                    ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
                    if (objBanco.insereColecao(this.objAtributos))
                    {
                        strMsg = "Registro inserido com sucesso.";
                        objBanco = null;
                        return true;
                    }
                    else
                    {
                        strMsg = "N�o foi poss�vel realizar a inser��o.";
                        objBanco = null;
                        bolRetorno = false;
                    }
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
                ClsConhecimentoConhecimento objConhecimentoConhecimento = new ClsConhecimentoConhecimento();
                ServiceDesk.Controle.ClsGridView.geraGridView(objGridView, objConhecimentoConhecimento.objAtributos);
                objConhecimentoConhecimento = null;
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
        public static void geraGridView(System.Web.UI.WebControls.GridView objGridView, ClsConhecimentoConhecimento objConhecimentoConhecimento, bool bolCondicao)
        {
            try
            {
                objGridView.AutoGenerateColumns = false;
                ServiceDesk.Controle.ClsGridView.geraGridView(objGridView, objConhecimentoConhecimento.objAtributos, bolCondicao);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region GeraGridView
        /// <summary>
        /// Gera uma nova grid de acordo com o c�digo do conhecimento de origem.
        /// </summary>
        /// <param name="objGridView">geraGridView</param>
        /// <param name="intCodigo">C�digo do conhecimento de origem</param>
        public static void geraGridView(System.Web.UI.WebControls.GridView objGridView, int intCodigo)
        {
            try
            {
                objGridView.AutoGenerateColumns = false;

                string strSql = "select C.*";
                strSql += " from Conhecimento C, ConhecimentoConhecimento CC";
                strSql += " where CC.conhecimento_codigo_origem = C.conhecimento_codigo";
                strSql += " and C.conhecimento_codigo =" + intCodigo + "";

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
                    if (VerificaSeJaExisteNoBanco(out strMensagem) == true)
                    {
                        objBanco = null;
                        bolRetorno = false;
                    }
                    else if (objBanco.alteraColecao(this.objAtributos))
                    {
                        objBanco = null;
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
        /// <returns>Retorna true ou false. Se o registro foi excluido ou n�o.</returns>
        public bool exclui(out String strMsg)
        {
            try
            {
                if (ValidaDados(out strMsg) == false) return false;

                strMsg = string.Empty;

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

        #region Exclui todos as associa��es de incidente subordinados ao cod_origem.
        /// <summary>
        /// Exclui todos os incidentes associados ao problema.
        /// </summary>
        /// <param name="intCodigo" > C�digo do conhecimento origem </param>
        /// <returns>Retorna true ou false. Se o registro foi excluido ou n�o.</returns>
        public bool exclui(out String strMensagem, int intCodigo)
        {
            strMensagem = string.Empty;
            try
            {
                ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();

                string strSql = "delete from ConhecimentoConhecimento where conhecimento_codigo_destino = " + intCodigo + " ";

                if (objBanco.executaSQL(strSql))
                {
                    strMensagem = "Registro exclu�do com sucesso.";
                    objBanco = null;
                    return true;
                }
                else
                {
                    strMensagem = "Imposs�vel exclu�r com sucesso.";
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
                objAtributos.NomeTabela = "ConhecimentoConhecimento";
                objAtributos.DescricaoTabela = "Tabela que relaciona um conhecimento a outro conhecimento";

                objCodigoConhecimentoOrigem.Campo = "conhecimento_codigo_origem";
                objCodigoConhecimentoOrigem.Descricao = "C�digo do conhecimento de origem";
                objCodigoConhecimentoOrigem.CampoIdentificador = true;
                objCodigoConhecimentoOrigem.CampoObrigatorio = true;
                objCodigoConhecimentoOrigem.Tipo = System.Data.DbType.Int32;
                objAtributos.Add(objCodigoConhecimentoOrigem);

                objCodigoConhecimentoDestino.Campo = "conhecimento_codigo_destino";
                objCodigoConhecimentoDestino.Descricao = "C�digo do de destino";
                objCodigoConhecimentoDestino.CampoIdentificador = true;
                objCodigoConhecimentoDestino.CampoObrigatorio = true;
                objCodigoConhecimentoDestino.Tipo = System.Data.DbType.Int32;
                objAtributos.Add(objCodigoConhecimentoDestino);

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion

        #region Associa cole��o de conhecimentos ao conhecimento informado no parametro.
        /// <summary>
        /// Adiciana conhecimento a conhecimentos.
        /// </summary>
        /// <param name="strCodigoConhecimento">Codigo do conhecimento</param>
        static public void AdicionaConhecimentoAoConhecimento(String strCodigoConhecimento, string strCodigoItem)
        {
            try
            {
                string strSql = string.Empty;
                if (strCodigoConhecimento != string.Empty)
                {
                    strSql = "INSERT INTO ConhecimentoConhecimento";
                    strSql += " (conhecimento_codigo_destino, conhecimento_codigo_origem)";
                    strSql += " SELECT " + strCodigoConhecimento + ", ic_codigo";
                    strSql += " FROM IC WHERE ic_codigo IN (" + strCodigoItem + ")";

                    ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
                    objBanco.executaSQL(strSql);
                    objBanco = null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region M�todo Popula TreeView Com N�s
        /// <summary>
        /// M�todo Popula TreeView Com N�s
        /// </summary>
        /// <param name="intCodigoConhecimento">C�digo do conhecimento</param>
        /// <param name="objTreeView">Nome da treeview</param>
        public static void PopulaN�s(int intCodigoConhecimento, TreeView objTreeView)
        {
            try
            {
                String strSql = String.Empty;

                strSql = "select C.conhecimento_codigo, C.nome";
                if (intCodigoConhecimento > 0) strSql += " , (select distinct(conhecimento_codigo_origem) from ConhecimentoConhecimento where conhecimento_codigo_destino = " + intCodigoConhecimento + " and conhecimento_codigo_origem = C.conhecimento_codigo) selecionado ";
                strSql += " from Conhecimento C ";
                if (intCodigoConhecimento > 0) strSql += " where C.conhecimento_codigo <> " + intCodigoConhecimento + " ";
                strSql += " order by C.nome";

                System.Data.SqlClient.SqlDataReader objDataReader = ServiceDesk.Banco.ClsBanco.geraDataReader(strSql);

                objTreeView.Nodes.Clear();
                while (objDataReader.Read())
                {
                    TreeNode objTreeNode = new TreeNode();
                    objTreeNode.Text = objDataReader["nome"].ToString();
                    objTreeNode.Value = objDataReader["conhecimento_codigo"].ToString().Trim();

                    if (intCodigoConhecimento > 0)
                    {
                        if (objDataReader["selecionado"].ToString() != string.Empty)
                            objTreeNode.Checked = true;
                        else
                            objTreeNode.Checked = false;
                    }
                    objTreeView.Nodes.Add(objTreeNode);
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