/*
- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
  • Classe para manipulação dos registros da tabela ConhecimentoConhecimento.cs.
  
  	Data: 28/12/2005
  	Autor: Fernanda Passos
  	Descrição: Classe que apresenta várias funcionalidades para maniplução dos registro da tabela 
    ConhecimentoConhecimento a qual ter por objetivos informa a um conhecimento que o mesmo faz parte
    de outro conhecimento.
  
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
/// Classe de acesso a dados da tabela ConhecimentoConhecimento.
/// </summary>
namespace ServiceDesk.Negocio
{
    /// <summary>
    /// Construtor da classe
    /// </summary>
    public class ClsConhecimentoConhecimento
    {
        #region Declarações

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
        /// Codigo do conhecimento de origem.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo CodigoConhecimentoOrigem
        {
            get { return objCodigoConhecimentoOrigem ; }
            set { this.objCodigoConhecimentoOrigem = value; }
        }

        /// <summary>
        /// Código do conhecimento de destino.
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
        /// <returns>Retorna true ou false. Se foi validado ou não.</returns>
        public bool ValidaDados(out String strMsg)
        {
            try
            {
                strMsg = string.Empty;

                if (objCodigoConhecimentoOrigem.Valor.Trim() == null)
                {
                    strMsg = "Informe o código do conhecimento de origem.";
                    return false;
                }
                else if (objCodigoConhecimentoDestino.Valor.Trim() == null)
                {
                    strMsg = "Informe o código do conhecimento de destino.";
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

        #region Verifica se dados já existem
        /// <summary>
        /// Verifica se já existe.
        /// </summary>
        /// <param name="strNome"></param>
        /// <returns>Retorna true ou false. Se existe ou não.</returns>
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
                    strMensagem = "Associação já existente no banco de dados.";
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
        /// <param name="strMensagem">Mensagem com informação da execução do método.</param>
        /// <returns>Retorna true ou false. Se o registro foi inserido ou não.</returns>
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
                        strMsg = "Não foi possível realizar a inserção.";
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
        /// Gera uma nova geraGridView de acordo com a coleção de atributos.
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
        /// Gera uma nova geraGridView de acordo com a coleção de atributos.
        /// </summary>
        /// <param name="objGridView">objjeto Grid View</param>
        /// <param name="bolCondicao">Condição para verificar se será utilizado a cláusula Where</param>
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
        /// Gera uma nova grid de acordo com o código do conhecimento de origem.
        /// </summary>
        /// <param name="objGridView">geraGridView</param>
        /// <param name="intCodigo">Código do conhecimento de origem</param>
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
        /// <returns>Retorna true ou false. Se o registro foi excluido ou não.</returns>
        public bool exclui(out String strMsg)
        {
            try
            {
                if (ValidaDados(out strMsg) == false) return false;

                strMsg = string.Empty;

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
            catch (Exception ex)
            {
                strMsg = ex.Message;
                throw ex;
            }
        }
        #endregion

        #region Exclui todos as associações de incidente subordinados ao cod_origem.
        /// <summary>
        /// Exclui todos os incidentes associados ao problema.
        /// </summary>
        /// <param name="intCodigo" > Código do conhecimento origem </param>
        /// <returns>Retorna true ou false. Se o registro foi excluido ou não.</returns>
        public bool exclui(out String strMensagem, int intCodigo)
        {
            strMensagem = string.Empty;
            try
            {
                ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();

                string strSql = "delete from ConhecimentoConhecimento where conhecimento_codigo_destino = " + intCodigo + " ";

                if (objBanco.executaSQL(strSql))
                {
                    strMensagem = "Registro excluído com sucesso.";
                    objBanco = null;
                    return true;
                }
                else
                {
                    strMensagem = "Impossível excluír com sucesso.";
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
                objAtributos.NomeTabela = "ConhecimentoConhecimento";
                objAtributos.DescricaoTabela = "Tabela que relaciona um conhecimento a outro conhecimento";

                objCodigoConhecimentoOrigem.Campo = "conhecimento_codigo_origem";
                objCodigoConhecimentoOrigem.Descricao = "Código do conhecimento de origem";
                objCodigoConhecimentoOrigem.CampoIdentificador = true;
                objCodigoConhecimentoOrigem.CampoObrigatorio = true;
                objCodigoConhecimentoOrigem.Tipo = System.Data.DbType.Int32;
                objAtributos.Add(objCodigoConhecimentoOrigem);

                objCodigoConhecimentoDestino.Campo = "conhecimento_codigo_destino";
                objCodigoConhecimentoDestino.Descricao = "Código do de destino";
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

        #region Associa coleção de conhecimentos ao conhecimento informado no parametro.
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

        #region Método Popula TreeView Com Nós
        /// <summary>
        /// Método Popula TreeView Com Nós
        /// </summary>
        /// <param name="intCodigoConhecimento">Código do conhecimento</param>
        /// <param name="objTreeView">Nome da treeview</param>
        public static void PopulaNós(int intCodigoConhecimento, TreeView objTreeView)
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