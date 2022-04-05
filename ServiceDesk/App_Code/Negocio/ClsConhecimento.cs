/*
- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
  • Classe para manipulação dos registros da tabela Conhecimento.
  
  	Data: 27/12/2005
  	Autor: Fernanda Passos
  	Descrição: Classe que permite a minipulação dos registros da tabela de Conhecimento a qual tem por 
    objetivo inicial armazenar as informações de soluções para processos novos.
  
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
/// Classe de acesso a dados da tabela Conhecimento.
/// </summary>
namespace ServiceDesk.Negocio
{
    /// <summary>
    /// Construtor da classe
    /// </summary>
    public class ClsConhecimento
    {
        #region Declarações

        public ClsConhecimento()
        {
            alimentaColecaoCampos();
        }
        private ServiceDesk.Banco.ClsAtributos objAtributos = new ServiceDesk.Banco.ClsAtributos();

        private ServiceDesk.Banco.ClsAtributo objCodigo = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objNome = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objTexto = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objStatus = new ServiceDesk.Banco.ClsAtributo();
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
        /// Codigo do conhecimento.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo Codigo
        {
            get { return objCodigo; }
            set { this.objCodigo = value; }
        }

        /// <summary>
        /// Nome do conhecimento.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo Nome
        {
            get { return objNome; }
            set { this.objNome = value; }
        }

        /// <summary>
        /// Texto com descrição do conhecimento.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo Texto
        {
            get { return objTexto; }
            set { this.objTexto = value; }
        }

        /// <summary>
        /// Status do conhecimento.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo Status
        {
            get { return objStatus; }
            set { this.objStatus = value; }
        }
        #endregion

        #region Metodos

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
                    strMsg = "Não foi lacalizado o código do conhecimento. Informe o conhecimento.";
                    return false;
                }
                else if (objNome.Valor.Trim() == null)
                {
                    strMsg = "É necessário selecionar o ";
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
        /// <returns>Retorna true ou false. Se existe ou não.</returns>
        public bool VerificaSeJaExisteNoBanco(out String  strMensagem)
        {
            strMensagem = string.Empty;
            try
            {
                
                string strSql = string.Empty;

                strSql = " nome = '"+ objNome.Valor.Trim()+"' ";
                strSql += " and texto = '"+ objTexto.Valor.Trim() +"' ";
                strSql += " and conhecimento_codigo <> "+ Convert.ToInt32(objCodigo.Valor.Trim ()) +"";

                ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();

                string strValor = objBanco.retornaValorCampo("Conhecimento", "conhecimento_codigo", strSql);

                if (strValor.Trim() != string.Empty)
                {
                    strMensagem = "Já existe registro com o nome e a descrição descrita na tela. Por favor, verifique as informações.";
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

                if (ValidaDados(out strMensagem) == false)
                {
                    return false;
                }
                else
                {
                    ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
                    //Verifica se já esta cadastrado no banco.
                    if (VerificaSeJaExisteNoBanco(out strMensagem) == true)
                    {
                        objBanco = null;
                        return false;
                    }

                    else if (objBanco.insereColecao(this.objAtributos))
                    {
                        objBanco = null;
                        strMensagem = "Registro inserido com sucesso.";
                        return true;
                    }
                    objBanco = null;
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
                ClsConhecimento objConhecimento = new ClsConhecimento();
                ServiceDesk.Controle.ClsGridView.geraGridView(objGridView, objConhecimento.Atributos);
                objConhecimento = null;
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
        public static void geraGridView(System.Web.UI.WebControls.GridView objGridView, ClsConhecimento objConhecimento, bool bolCondicao)
        {
            try
            {
                objGridView.AutoGenerateColumns = false;
                ServiceDesk.Controle.ClsGridView.geraGridView(objGridView, objConhecimento.objAtributos, bolCondicao);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region GeraGridView
        /// <summary>
        /// Gera grid view com campos especificos
        /// </summary>
        /// <param name="objGridView">Nome do GridView</param>
        /// <param name="intCodigo">Código do registro do qual deseja buscar as informações.</param>
        public static void geraGridView(System.Web.UI.WebControls.GridView objGridView, int intCodigo)
        {
            try
            {
                objGridView.AutoGenerateColumns = false;

                string strSql = "select * ";
                strSql += " from Conhecimento";
                strSql += " where conhecimento_codigo = " + intCodigo + "";

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

        #region GeraGridView
        /// <summary>
        /// GeraGridView por parametro.
        /// </summary>
        /// <param name="objGridView">objeto Grid View</param>
        /// <param name="strTabela">String - Nome da tabela que representa o processo</param> 
        /// <param name="intTabelaIdentificador">Código identificador da tabela que representa o processo</param> 
        public static void geraGridView(System.Web.UI.WebControls.GridView objGridView, string strTabela, string strItemConfiguracaoCodigo)
        {
            string strSql = string.Empty;
            try
            {
                objGridView.AutoGenerateColumns = false;

                switch (strTabela)
                {
                    case "CONHECIMENTO":
                        {
                            strSql = "SELECT conhecimento.conhecimento_codigo, nome, status.descricao status ";
                            strSql += " FROM conhecimento, status, ConhecimentoIC";
                            strSql += " WHERE Conhecimento.status_codigo = status.status_codigo ";
                            strSql += " AND Conhecimento.conhecimento_codigo = ConhecimentoIC.conhecimento_codigo ";
                            strSql += " AND ConhecimentoIC.ic_codigo = " + Convert.ToInt32( strItemConfiguracaoCodigo) +"";
                            break;
                        }

                    case "CHAMADO":
                        {
                            strSql = "SELECT chamado.chamado_codigo, chamado.descricao ";
                            strSql += "FROM chamado, conhecimentoprocesso, ChamadoIC ";
                            strSql += " WHERE conhecimentoprocesso.tabela = 'CHAMADO'";
                            strSql += " AND chamado.chamado_codigo = conhecimentoprocesso.tabela_identificador ";
                            strSql += " AND chamado.chamado_codigo = ChamadoIC.chamado_codigo ";
                            strSql += " AND ChamadoIC.ic_codigo = " + Convert.ToInt32( strItemConfiguracaoCodigo) +"";
                            break;
                        }

                    case "INCIDENTE":
                        {
                            strSql = "SELECT Incidente.incidente_codigo, descricao ";
                            strSql += "FROM Incidente, conhecimentoprocesso, IncidenteIC ";
                            strSql += " WHERE conhecimentoprocesso.tabela = 'INCIDENTE'";
                            strSql += " AND Incidente.incidente_codigo = conhecimentoprocesso.tabela_identificador ";
                            strSql += " AND Incidente.incidente_codigo = IncidenteIC.incidente_codigo ";
                            strSql += " AND IncidenteIC.ic_codigo = " + Convert.ToInt32( strItemConfiguracaoCodigo) +"";
                            break;
                        }
                    case "REQUISICAOSERVICO":
                        {
                            strSql = " SELECT RequisicaoServico.requisicaoservico_codigo, RequisicaoServico.descricao";
                            strSql += " FROM RequisicaoServico, conhecimentoprocesso, RequisicaoServicoIC";
                            strSql += " WHERE conhecimentoprocesso.tabela = 'RequisicaoServico'";
                            strSql += " AND RequisicaoServico.requisicaoservico_codigo = conhecimentoprocesso.tabela_identificador ";
                            strSql += " AND RequisicaoServico.requisicaoservico_codigo = RequisicaoServicoIC.requisicaoservico_codigo";
                            strSql += " AND RequisicaoServicoIC.ic_codigo = " + Convert.ToInt32(strItemConfiguracaoCodigo) + "";
                            break;
                        }
                }

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
                        bolRetorno = false;
                    }
                    else if (objBanco.alteraColecao(this.objAtributos))
                    {
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
                strMsg = string.Empty;

                //Valida a exclusão.
                if (ValidaExclusao(out strMsg) == false)
                    return false;

                ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
                if (objBanco.apagaColecao(this.objAtributos))
                {
                    //Delete todas as associações em ConhecimentoConhecimento
                    ServiceDesk.Negocio.ClsConhecimentoConhecimento objConhecimentoConhecimento = new ClsConhecimentoConhecimento();
                    objConhecimentoConhecimento.CodigoConhecimentoOrigem.Valor = Codigo.Valor;

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
            catch (Exception ex)
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
                objAtributos.NomeTabela = "Conhecimento";
                objAtributos.DescricaoTabela = "Tabela que armazena os dados de conhecimentos que podem ser pesquisados.";

                objCodigo.Campo = "conhecimento_codigo";
                objCodigo.Descricao = "Código do conhecimento";
                objCodigo.CampoIdentificador = true;
                objCodigo.CampoObrigatorio = true;
                objCodigo.Tipo = System.Data.DbType.Int32;
                objAtributos.Add(objCodigo);

                objNome.Campo = "nome";
                objNome.Descricao = "Nome do conhecimento";
                objNome.CampoIdentificador = false;
                objNome.CampoObrigatorio = true;
                objNome.Tipo = System.Data.DbType.String;
                objNome.Tamanho = 100;
                objAtributos.Add(objNome);

                objTexto.Campo = "texto";
                objTexto.Descricao = "Texto com a descrição da solução do conhecimento.";
                objTexto.CampoIdentificador = false;
                objTexto.CampoObrigatorio = false;
                objTexto.Tipo = System.Data.DbType.String;
                objTexto.Tamanho = 7500;
                objAtributos.Add(objTexto);

                objStatus.Campo = "status_codigo";
                objStatus.Descricao = "Status do conhecimento, se ativo ou inativo.";
                objStatus.CampoIdentificador = false;
                objStatus.CampoObrigatorio = false;
                objStatus.Tipo = System.Data.DbType.Int32;
                objAtributos.Add(objStatus);
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
                string strSql = "select max(conhecimento_codigo) as maximo from Conhecimento";

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

        #region metodo populaNoRaiz
        /// <summary>
        /// Método que popula os nós que não possuem pai
        /// </summary>
        public static void populaNoRaiz(int intCodigoItem, TreeView trvTreeView)
        {
            try
            {
                String strSql = String.Empty;

                strSql = "select conhecimento_codigo, nome";
                strSql += " from Conhecimento";
                if (intCodigoItem <= 0) strSql += " where conhecimento_codigo is not null";
                if (intCodigoItem > 0) strSql += " where conhecimento_codigo = " + intCodigoItem + "";
                strSql += " order by nome";

                System.Data.SqlClient.SqlDataReader objDataReader = ServiceDesk.Banco.ClsBanco.geraDataReader(strSql);

                trvTreeView.Nodes.Clear();

                populaNos(objDataReader, trvTreeView.Nodes, trvTreeView);

                objDataReader.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Popula Nós
        /// <summary>
        /// Popula Nós
        /// </summary>
        public static void populaNos(System.Data.SqlClient.SqlDataReader objDataReader, TreeNodeCollection objTreeNodeCollection, TreeView trvTreeView)
        {
            try
            {
                while (objDataReader.Read())
                {
                    TreeNode objTreeNode = new TreeNode();
                    objTreeNode.Text = objDataReader["nome"].ToString();
                    objTreeNode.Value = objDataReader["conhecimento_codigo"].ToString();
                    objTreeNodeCollection.Add(objTreeNode);
                }
                objDataReader.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Pega dados do conhecimento por parametro
        /// <summary>
        /// Pega dados do conhecimento por parametro
        /// </summary>
        /// <param name="intCodigoConhecimento">Código do conhecimento</param>
        public static System.Data .SqlClient.SqlDataReader GetDadosConhecimento(int intCodigoConhecimento)
        {
            try
            {
                string strSql = "select * from Conhecimento where conhecimento_codigo = " + intCodigoConhecimento + "";
                System.Data.SqlClient.SqlDataReader objReader = ServiceDesk.Banco.ClsBanco.geraDataReader(strSql);
                return objReader;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        public static int ContadorRelacionadosKB(string strTabela, string strItemConfiguracaoCodigo)
        {
            try
            {
                string strSQL = string.Empty;
                switch (strTabela)
                {
                    case "CONHECIMENTO":
                        {
                            strTabela = "ConhecimentoIC";
                            strSQL = " ic_codigo = " + Convert.ToInt32(strItemConfiguracaoCodigo) +"";
                            break;
                        }

                    case "CHAMADO":
                        {
                            strSQL = " ConhecimentoProcesso.tabela_identificador = ChamadoIC.chamado_codigo ";
                            strSQL += " AND ConhecimentoProcesso.tabela = '" + strTabela + "'";
                            strSQL += " AND ic_codigo = " + Convert.ToInt32(strItemConfiguracaoCodigo) +"";
                            strTabela = "ChamadoIC, ConhecimentoProcesso";
                            break;
                        }

                    case "INCIDENTE":
                        {
                            strSQL = " ConhecimentoProcesso.tabela_identificador = IncidenteIC.incidente_codigo ";
                            strSQL += " AND ConhecimentoProcesso.tabela = '" + strTabela + "'";
                            strSQL += " AND ic_codigo = " + Convert.ToInt32(strItemConfiguracaoCodigo) +"";
                            strTabela = "IncidenteIC, ConhecimentoProcesso";
                            break;
                        }
                    case "REQUISICAOSERVICO":
                        {
                            strSQL = " ConhecimentoProcesso.tabela_identificador = RequisicaoServicoIC.requisicaoservico_codigo";
                            strSQL += " AND ConhecimentoProcesso.tabela = '" + strTabela + "'";
                            strSQL += " AND ic_codigo = " + Convert.ToInt32(strItemConfiguracaoCodigo) + "";
                            strTabela = "RequisicaoServicoIC, ConhecimentoProcesso";
                            break;
                        }
                }

                ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
                return Convert.ToInt32(objBanco.retornaValorCampo(strTabela, "COUNT(*)", strSQL));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}