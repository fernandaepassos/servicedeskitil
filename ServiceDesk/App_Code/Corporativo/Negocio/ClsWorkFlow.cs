/*
- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
  • Classe do WorkFlow. Work Flow permite automatização dos status dos processos do sistema.
  
  	Data: 27/12/2005
  	Autor: Diego
  	Descrição: Classe que apresenta métodos, propriedades e atributos do WorkFlow. Work Flow
    permite automatização dos status dos processos do sistema.
      
  • Alterações
  	Data: 15/03/2006
  	Autor: Fernanda Passos
  	Descrição: Inclusão do atributo flag_semi_automatico.
    
- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
*/
using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ServiceDesk.Negocio;

namespace SServiceDesk.Negocio
{
    /// <summary>
    /// Classe ClsWorkFlow.
    /// </summary>
    public class ClsWorkFlow
    {

        #region Declarações
        //Colecao de atributos do WorkFlow
        private ServiceDesk.Banco.ClsAtributos objAtributos = new ServiceDesk.Banco.ClsAtributos();

        //Atributos do WorkFlow
        private ServiceDesk.Banco.ClsAtributo objCodigo = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objTipo = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objSuperior = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objStatus = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objPreCondicao = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objDescricao = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objOrdenacao = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objChave = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objFlagSemiAutomatico = new ServiceDesk.Banco.ClsAtributo();
        #endregion

        #region Métodos

        #region metodo Construtor da classe com passagem de parametro
        /// <summary>
        /// Construtor da classe
        /// </summary>
        public ClsWorkFlow(int intCodigo)
        {
            this.alimentaColecaoCampos();
            this.objCodigo.Valor = intCodigo.ToString();
            ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
            objBanco.alimentaColecao(this.objAtributos);
            objBanco = null;
        }
        #endregion

        #region metodo alimentaColecaoCampos
        /// <summary>
        /// Adiciona todos os atributos de um anexo a coleção de atributos.
        /// </summary>
        private void alimentaColecaoCampos()
        {
            objAtributos.DescricaoTabela = "WorkFlow";
            objAtributos.NomeTabela = "WorkFlow";

            objCodigo.Campo = "workflow_codigo";
            objCodigo.Descricao = "Código";
            objCodigo.CampoIdentificador = true;
            objCodigo.CampoObrigatorio = true;
            objCodigo.Tipo = System.Data.DbType.Int32;
            objAtributos.Add(objCodigo);

            objTipo.Campo = "workflow_tipo_codigo";
            objTipo.Descricao = "Código do Tipo do WorkFlow";
            objTipo.CampoObrigatorio = true;
            objTipo.Tipo = System.Data.DbType.Int32;
            objAtributos.Add(objTipo);

            objSuperior.Campo = "workflow_codigo_superior";
            objSuperior.Descricao = "Superior";
            objSuperior.Tipo = System.Data.DbType.Int32;
            objAtributos.Add(objSuperior);

            objStatus.Campo = "status_codigo";
            objStatus.Descricao = "Código do Status";
            objStatus.CampoObrigatorio = true;
            objStatus.Tipo = System.Data.DbType.Int32;
            objAtributos.Add(objStatus);

            objDescricao.Campo = "descricao";
            objDescricao.Descricao = "Descrição";
            objDescricao.CampoObrigatorio = true;
            objDescricao.Tipo = System.Data.DbType.String;
            objDescricao.Tamanho = 50;
            objAtributos.Add(objDescricao);

            objPreCondicao.Campo = "pre_condicao";
            objPreCondicao.Descricao = "Pré Condição";
            objPreCondicao.Tipo = System.Data.DbType.String;
            objPreCondicao.Tamanho = 3000;
            objAtributos.Add(objPreCondicao);

            objOrdenacao.Campo = "ordenacao";
            objOrdenacao.Descricao = "Ordem";
            objOrdenacao.Tipo = System.Data.DbType.Int32;
            objAtributos.Add(objOrdenacao);

            objChave.Campo = "chave";
            objChave.Descricao = "Chave";
            objChave.Tipo = System.Data.DbType.String;
            objChave.Tamanho = 100;
            objAtributos.Add(objChave);

            objFlagSemiAutomatico.Campo = "flag_semi_automatico";
            objFlagSemiAutomatico.Descricao = "Flag que indica se a seleção pelo usuário ficará disponível se a condição atender";
            objFlagSemiAutomatico.CampoIdentificador = false;
            objFlagSemiAutomatico.CampoObrigatorio = false;
            objFlagSemiAutomatico.Tipo = System.Data.DbType.String;
            objFlagSemiAutomatico.Tamanho = 1;
            objAtributos.Add(objFlagSemiAutomatico);
        }
        #endregion

        #region Metodo retornaCodigoWorkFlow
        public static int retornaCodigoWorkFlow(string strTabela, int intWorkFlowSuperiorCodigo, int intStatusCodigo)
        {
            int intRetorno = 0;
            string strRetorno = string.Empty;
            string strCondicao = string.Empty;
            string strSql = string.Empty;
            bool bolWorkflowSuperior = false;
            System.Data.SqlClient.SqlDataReader objDataReader = null;

            strCondicao = "workflow.workflow_tipo_codigo = workflowtipo.workflow_tipo_codigo";
            strCondicao += " AND lower(tabela) = '" + strTabela.ToLower() + "'";
            if (intWorkFlowSuperiorCodigo > 0)
            {
                strCondicao += " AND workflow_codigo_superior = " + intWorkFlowSuperiorCodigo.ToString();
                strCondicao += " AND status_codigo = " + intStatusCodigo.ToString();
            }
            else
            {
                strCondicao += " AND workflow_codigo_superior is null ";
            }

            ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
            strRetorno = objBanco.retornaValorCampo("workflow, workflowtipo", "workflow_codigo", strCondicao);
            if (strRetorno != string.Empty)
            {
                intRetorno = Convert.ToInt32(strRetorno);
            }

            if (intStatusCodigo > 0)
            {
                while (!bolWorkflowSuperior)
                {
                    //Verificando se existe algum workflow com o mesmo status anterior ao atual
                    strSql = "SELECT workflow_codigo_superior, status_codigo FROM workflow";
                    strSql += " WHERE workflow_codigo = " + intWorkFlowSuperiorCodigo.ToString();
                    objDataReader = ServiceDesk.Banco.ClsBanco.geraDataReader(strSql);
                    if (objDataReader.Read())
                    {
                        if (objDataReader["workflow_codigo_superior"].ToString() != string.Empty)
                        {
                            intWorkFlowSuperiorCodigo = Convert.ToInt32(objDataReader["workflow_codigo_superior"].ToString());
                        }
                        else
                        {
                            bolWorkflowSuperior = true;
                        }
                        if (intStatusCodigo.ToString() == objDataReader["status_codigo"].ToString())
                        {
                            intRetorno = intWorkFlowSuperiorCodigo;
                            bolWorkflowSuperior = true;
                        }
                    }
                    else
                    {
                        bolWorkflowSuperior = true;
                    }
                    objDataReader.Dispose();
                }

            }

            objDataReader = null;
            objBanco = null;
            return intRetorno;

        }
        #endregion

        #region metodo geraDataGrid
        /// <summary>
        /// Gera um novo DataGrid de acordo com a coleção de atributos.
        /// </summary>
        /// <param name="objDataGrid">DataGrid</param>
        public static void geraDataGrid(System.Web.UI.WebControls.DataGrid objDataGrid)
        {
            try
            {
                objDataGrid.AutoGenerateColumns = false;
                ClsWorkFlow objWorkFlow = new ClsWorkFlow();
                ServiceDesk.Controle.ClsDataGrid.geraDataGrid(objDataGrid, objWorkFlow.objAtributos);
                objWorkFlow = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region metodo geraGridView
        /// <summary>
        /// Gera uma nova GridView de acordo com a coleção de atributos.
        /// </summary>
        /// <param name="objGridView">geraGridView</param>
        public static void geraGridView(System.Web.UI.WebControls.GridView objGridView)
        {
            try
            {
                objGridView.AutoGenerateColumns = false;
                ClsWorkFlow objWorkFlow = new ClsWorkFlow();
                ServiceDesk.Controle.ClsGridView.geraGridView(objGridView, objWorkFlow.objAtributos);
                objWorkFlow = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region metodo geraGridView
        /// <summary>
        /// Gera uma nova geraGridView de acordo com a coleção de atributos.
        /// </summary>
        /// <param name="objGridView">geraGridView</param>
        /// <param name="bolCondicao">Condição para verificar se será utilizado a cláusula Where</param>
        public static void geraGridView(System.Web.UI.WebControls.GridView objGridView, ClsWorkFlow objWorkFlow, bool bolCondicao)
        {
            try
            {
                objGridView.AutoGenerateColumns = false;
                ServiceDesk.Controle.ClsGridView.geraGridView(objGridView, objWorkFlow.objAtributos, bolCondicao);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region metodo geraDropDownList
        /// <summary>
        /// Gera um novo DropDownList de acordo com a coleção de atributos.
        /// </summary>
        /// <param name="objDropDownList">DropDownList</param>
        public static void geraDropDownList(System.Web.UI.WebControls.DropDownList objDropDownList)
        {
            try
            {
                ClsWorkFlow objWorkFlow = new ClsWorkFlow();
                objDropDownList.DataTextField = objWorkFlow.objDescricao.Campo;
                objDropDownList.DataValueField = objWorkFlow.objCodigo.Campo;
                ServiceDesk.Controle.ClsDropDownList.geraDropDownList(objDropDownList, objWorkFlow.objAtributos);
                objWorkFlow = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region metodo geraDropDownListItem
        /// <summary>
        /// Gera o DropDownList dos itens de configuração com excessão do item que tem o código igual ao passado pelo parametro
        /// </summary>
        /// <param name="objDropDownList">Objeto DropDownList</param>
        /// <param name="intCodigoItem">Código do Item que não será listado</param>
        public static void geraDropDownListItem(DropDownList objDropDownList, int intCodigoItem)
        {
            try
            {
                ClsWorkFlow objWorkFlow = new ClsWorkFlow();
                objDropDownList.DataTextField = objWorkFlow.objDescricao.Campo;
                objDropDownList.DataValueField = objWorkFlow.objCodigo.Campo;
                ServiceDesk.Controle.ClsDropDownList.geraDropDownList(objDropDownList, objWorkFlow.Atributos);
                try
                {
                    objDropDownList.Items.FindByValue(intCodigoItem.ToString()).Enabled = false;
                }
                catch
                {
                }

                objWorkFlow = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region metodo geraDropDownListPorTipo
        /// <summary>
        /// Gera o DropDownList dos itens de configuração de acordo com o Tipo do WorkFlow
        /// </summary>
        /// <param name="objDropDownList">Objeto DropDownList</param>
        /// <param name="intCodigoItem">Código do Item que não será listado</param>
        public static void geraDropDownListPorTipo(DropDownList objDropDownList, int intCodigoTipo)
        {
            try
            {
                ClsWorkFlow objWorkFlow = new ClsWorkFlow();
                objDropDownList.DataTextField = objWorkFlow.objDescricao.Campo;
                objDropDownList.DataValueField = objWorkFlow.objCodigo.Campo;
                objWorkFlow.Codigo.CampoIdentificador = false;
                objWorkFlow.Tipo.Valor = intCodigoTipo.ToString();
                objWorkFlow.Tipo.CampoIdentificador = true;
                ServiceDesk.Controle.ClsDropDownList.geraDropDownList(objDropDownList, objWorkFlow.Atributos, true);

                objWorkFlow = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region metodo geraDropDownListProximo
        /// <summary>
        /// Gera os proximos workflows possiveis a partir da tabela e do status atual
        /// </summary>
        /// <param name="ddlWorkflow"></param>
        /// <param name="strTabela"></param>
        /// <param name="intCodigoStatus"></param>
        public static void geraDropDownListProximo(DropDownList ddlWorkflow, string strTabela, int intCodigoStatus)
        {
            string strSql = string.Empty;

            strSql = "SELECT DISTINCT(workflow.status_codigo), status.descricao";
            strSql += " FROM workflow, status";
            strSql += " WHERE workflow_codigo_superior IN (";
            strSql += "SELECT workflow_codigo FROM workflow,workflowtipo";
            strSql += " WHERE workflowtipo.workflow_tipo_codigo = workflow.workflow_tipo_codigo";
            strSql += " AND LOWER(tabela) = '" + strTabela.ToLower() + "'";
            strSql += " AND status_codigo = " + intCodigoStatus.ToString() + ")";
            strSql += " AND status.status_codigo = workflow.status_codigo";
            strSql += " AND workflow.pre_condicao IS NULL";

            System.Data.SqlClient.SqlDataReader objDataReader = ServiceDesk.Banco.ClsBanco.geraDataReader(strSql);
            ddlWorkflow.DataValueField = "status_codigo";
            ddlWorkflow.DataTextField = "descricao";
            ddlWorkflow.DataSource = objDataReader;
            ddlWorkflow.DataBind();
            objDataReader.Dispose();
            objDataReader = null;
        }
        #endregion

        #region metodo geraDropDownListProximo
        /// <summary>
        /// Gera os proximos workflows possiveis a partir da tabela do codigo do item e do status atual
        /// </summary>
        /// <param name="ddlWorkflow"></param>
        /// <param name="strTabela"></param>
        /// <param name="intCodigoItem"></param>
        /// <param name="intCodigoStatus"></param>
        public static void geraDropDownListProximo(DropDownList ddlWorkflow, string strTabela, int intCodigoItem, int intCodigoStatus)
        {
            string strSql = string.Empty;
            int intWorkFlowCodigo = 0;

            //===========================================================================//
            // -  O que: Pega código do work flow na tabela de statuslog no momento em que 
            // o chamado mudou para o status que apresenta na variável  intCodigoStatus
            // O objetivo é pegar o código do status pai do atual para depois defin
            // os possíveis status filhos do atual.
            // - Quem: ?????
            // - Quando: ????
            //===========================================================================//
            strSql = "SELECT workflow_codigo FROM statuslog";
            strSql += " WHERE LOWER(tabela) = '" + strTabela.ToLower() + "'";
            strSql += " AND tabela_identificador = " + intCodigoItem;
            strSql += " AND status_codigo_destino = " + intCodigoStatus;
            strSql += " ORDER BY status_log_codigo DESC";
            System.Data.SqlClient.SqlDataReader objDataReader = ServiceDesk.Banco.ClsBanco.geraDataReader(strSql);
            if (objDataReader.Read())
            {
                if (objDataReader["workflow_codigo"].ToString() != string.Empty)
                {
                    intWorkFlowCodigo = Convert.ToInt32(objDataReader["workflow_codigo"].ToString());
                }
            }
            objDataReader = null;
            //===========================================================================//


            //===========================================================================//
            // - O que: Pega os status filhos do status acima encontrado.
            // - Quem: ???
            // - Quando: ?????
            //===========================================================================//
            strSql = " SELECT DISTINCT(workflow.status_codigo), status.descricao, flag_semi_automatico, pre_condicao";
            strSql += " FROM workflow, status";
            strSql += " WHERE status.status_codigo = workflow.status_codigo";
            strSql += " AND workflow_codigo_superior = " + intWorkFlowCodigo.ToString();
            objDataReader = ServiceDesk.Banco.ClsBanco.geraDataReader(strSql);
            //===========================================================================//

            AlimentaDropDropProximoStatus(ddlWorkflow, objDataReader, intCodigoItem);

            objDataReader.Dispose();
            objDataReader = null;
        }
        #endregion

        #region Alimenta o drop down list dos próximos status possíveis
        /// <summary>
        /// Alimenta o drop down list dos próximos status possíveis
        /// </summary>
        /// <param name="ddlWorkflow">Nome do DropDownList</param> 
        /// <param name="objDataReader">Objeto DataReader com os próximos status possíveis</param> 
        public static void AlimentaDropDropProximoStatus(DropDownList ddlWorkflow, System.Data.SqlClient.SqlDataReader objDataReader, int intIdentificador)
        {
            ddlWorkflow.Items.Clear();
            while (objDataReader.Read())
            {
                if (objDataReader["pre_condicao"].ToString().Trim() == string.Empty) ddlWorkflow.Items.Add(new ListItem(objDataReader["descricao"].ToString(), objDataReader["status_codigo"].ToString()));
                else if (objDataReader["pre_condicao"].ToString().Trim() != string.Empty && objDataReader["flag_semi_automatico"].ToString().Trim() == "S")
                {
                    if (VerificaSeCondicaoAtendida(objDataReader["pre_condicao"].ToString().Trim(), intIdentificador) == true) ddlWorkflow.Items.Add(new ListItem(objDataReader["descricao"].ToString(), objDataReader["status_codigo"].ToString()));
                }
            }
            objDataReader.Dispose();
            objDataReader = null;
        }
        #endregion

        #region Verifica se a condição atual atende
        /// <summary>
        /// Verifica se a condição atual atende
        /// </summary>
        /// <param name="strCondicao">Descrição da condição</param>
        /// <returns>Retorna true ou false. Se foi atendida ou não</returns>
        public static bool VerificaSeCondicaoAtendida(string strCondicao, int intIdentificador)
        {
            bool bolRetorno = false;

            if (strCondicao.Trim() != string.Empty)
            {
                strCondicao = strCondicao.Replace("%IDENTIFICADOR%", intIdentificador.ToString().Trim());
                strCondicao = strCondicao.Replace("$INCIDENTE$", "'INCIDENTE'");
                strCondicao = strCondicao.Replace("$REQUISICAOSERVICO$", "'REQUISICAOSERVICO'");
                strCondicao = strCondicao.Replace("$REQUISICAOMUDANCA$", "'REQUISICAOMUDANCA'");
                strCondicao = strCondicao.Replace("%CODIGOTIPOACEITACAO%", ClsParametro.CodigoTipoAceitacao.Trim());
                strCondicao = strCondicao.Replace("%CODIGOTIPOAPROVACAO%", ClsParametro.CodigoTipoAprovacao.Trim());
                strCondicao = strCondicao.Replace("$N$", "'N'");
                strCondicao = strCondicao.Replace("$S$", "'S'");

                System.Data.SqlClient.SqlDataReader objDataReader;

                objDataReader = ServiceDesk.Banco.ClsBanco.geraDataReader(strCondicao.Trim());
                objDataReader.Read();
                if (!objDataReader.HasRows) bolRetorno = false;
                else bolRetorno = true;

                objDataReader.Dispose();
                objDataReader = null;
            }
            return bolRetorno;
        }
        #endregion

        #region metodo primeiroStatus
        /// <summary>
        /// Retorna o primeiro status do WorkFlow
        /// </summary>
        /// <param name="strTabela"></param>
        public static int primeiroStatus(string strTabela)
        {
            try
            {
                string strSql = string.Empty;
                int intStatusCodigo = 0;

                strSql = " SELECT workflow.status_codigo ";
                strSql += " FROM workflow, workflowtipo ";
                strSql += " WHERE workflowtipo.workflow_tipo_codigo = workflow.workflow_tipo_codigo ";
                strSql += " AND tabela = '" + strTabela + "' ORDER BY workflow.status_codigo ";

                System.Data.SqlClient.SqlDataReader objDataReader = ServiceDesk.Banco.ClsBanco.geraDataReader(strSql);
                objDataReader.Read();
                if (objDataReader.HasRows)
                    intStatusCodigo = Convert.ToInt32(objDataReader["status_codigo"]);
                objDataReader.Dispose();
                objDataReader = null;
                return intStatusCodigo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region metodo proximoStatusAutomatico
        ///<summary>
        /// Retorna o próximo status automático do WorkFlow
        ///</summary>
        ///<param name="strTabela">Nome físico no banco de dados da tabela que representa o processo</param>
        ///<param name="strStatusCodigoAtual">Código do status atual</param> 
        public static int proximoStatusAutomatico(string strTabela, string strStatusCodigoAtual, int intCodigoIdentificador)
        {
            string strSql = string.Empty;
            string strCodigoWorkFlow = string.Empty;
            int intStatusCodigo = 0;
            ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();

            try
            {
                strSql = " workflowtipo.workflow_tipo_codigo = workflow.workflow_tipo_codigo ";
                strSql += " AND tabela = '" + strTabela + "' ";
                strSql += " AND workflow.status_codigo = 0" + strStatusCodigoAtual;

                strCodigoWorkFlow = objBanco.retornaValorCampo("workflow,workflowtipo", "workflow.workflow_codigo", strSql);

                strSql = " SELECT workflow.status_codigo, workflow.pre_condicao, workflow.flag_semi_automatico";
                strSql += " FROM workflow, workflowtipo ";
                strSql += " WHERE workflowtipo.workflow_tipo_codigo = workflow.workflow_tipo_codigo ";
                strSql += " AND tabela = '" + strTabela + "' ";
                strSql += " AND workflow_codigo_superior = 0" + strCodigoWorkFlow;
                strSql += " AND workflow.pre_condicao IS NOT NULL";

                System.Data.SqlClient.SqlDataReader objDataReaderCondicoes = ServiceDesk.Banco.ClsBanco.geraDataReader(strSql);

                strSql = string.Empty;

                while (objDataReaderCondicoes.Read())
                {
                    if (objDataReaderCondicoes.HasRows && objDataReaderCondicoes["flag_semi_automatico"].ToString().Trim() != "S")
                    {
                        strSql = objDataReaderCondicoes["pre_condicao"].ToString();
                        intStatusCodigo = Convert.ToInt32(objDataReaderCondicoes["status_codigo"]);


                        if (strSql != string.Empty)
                        {
                            strSql = strSql.Replace("%IDENTIFICADOR%", intCodigoIdentificador.ToString().Trim());
                            strSql = strSql.Replace("$INCIDENTE$", "'INCIDENTE'");
                            strSql = strSql.Replace("$REQUISICAOSERVICO$", "'REQUISICAOSERVICO'");
                            strSql = strSql.Replace("$REQUISICAOMUDANCA$", "'REQUISICAOMUDANCA'");
                            strSql = strSql.Replace("%CODIGOTIPOACEITACAO%", ClsParametro.CodigoTipoAceitacao.Trim());
                            strSql = strSql.Replace("%CODIGOTIPOAPROVACAO%", ClsParametro.CodigoTipoAprovacao.Trim());
                            strSql = strSql.Replace("$N$", "'N'");
                            strSql = strSql.Replace("$S$", "'S'");

                            System.Data.SqlClient.SqlDataReader objDataReader = ServiceDesk.Banco.ClsBanco.geraDataReader(strSql);

                            objDataReader.Read();

                            if (!objDataReader.HasRows)
                            {
                                intStatusCodigo = 0;
                                objDataReader.Dispose();
                                objDataReader = null;
                            }
                            else
                            {
                                objDataReader.Dispose();
                                objDataReader = null;
                                objDataReaderCondicoes.Dispose();
                                objDataReaderCondicoes = null;
                                return intStatusCodigo;
                            }
                        }
                    }
                }
                objDataReaderCondicoes.Dispose();
                objDataReaderCondicoes = null;

                return intStatusCodigo;
            }
            catch
            {
                return 0;
            }

        }
        #endregion

        #region metodo insere
        /// <summary>
        /// Método que insere.
        /// </summary>
        /// <returns>Retorna true ou false. Se o registro foi inserido ou não.</returns>
        public bool insere(out string strMensagem)
        {
            try
            {
                strMensagem = string.Empty;
                bool bolRetorno = false;

                if (this.objDescricao.Valor.Trim() == string.Empty)
                {
                    strMensagem = "Favor informar a descrição do WorkFlow.<br>";
                }
                else
                {
                    ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
                    if (objBanco.insereColecao(this.objAtributos))
                    {
                        strMensagem = "Item inserido com sucesso.";
                        bolRetorno = true;
                    }
                    objBanco = null;
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
        /// Método que altera.
        /// </summary>
        /// <returns>Retorna true ou false. Se o registro foi alterado ou não.</returns>
        public bool altera(out string strMensagem)
        {
            try
            {
                strMensagem = string.Empty;
                bool bolRetorno = false;

                if (this.objDescricao.Valor.Trim() == string.Empty)
                {
                    strMensagem = "Favor informar a descrição do WorkFlow.<br>";
                }
                else
                {
                    ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
                    if (objBanco.alteraColecao(this.objAtributos))
                    {
                        strMensagem = "Item atualizado com sucesso.";
                        bolRetorno = true;
                    }
                    objBanco = null;
                }

                return bolRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region metodo exclui
        /// <summary>
        /// Método que exclui.
        /// </summary>
        /// <returns>Retorna true ou false. Se o registro foi excluido ou não.</returns>
        public bool exclui()
        {
            try
            {
                string strMsg = string.Empty;

                //Valida a exclusão.
                if (ServiceDesk.Negocio.ClsIdentificador.ValidaExclusao(objCodigo.Campo, objCodigo.Valor.Trim(), out strMsg, true, false, objAtributos.NomeTabela.Trim()) == false) return false;

                ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
                if (objBanco.apagaColecao(this.objAtributos))
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

        #region metodo exclui
        /// <summary>
        /// Método que exclui todos os workflows de um determinado tipo.
        /// </summary>
        /// <returns>Retorna true ou false. Se o registro foi excluido ou não.</returns>
        public static void exclui(int intCodigoTipo)
        {
            try
            {

                string strSql = string.Empty;
                strSql = "SELECT workflow_codigo";
                strSql += " FROM workflow";
                strSql += " WHERE workflow_tipo_codigo = " + intCodigoTipo.ToString();
                strSql += " ORDER BY chave DESC";
                System.Data.SqlClient.SqlDataReader objDataReader = ServiceDesk.Banco.ClsBanco.geraDataReader(strSql);
                while (objDataReader.Read())
                {
                    ClsWorkFlow objWorkFlow = new ClsWorkFlow(Convert.ToInt32(objDataReader["workflow_codigo"].ToString()));
                    objWorkFlow.exclui();
                    objWorkFlow = null;
                }
                objDataReader.Dispose();
                objDataReader = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region metodo atualizaChave
        /// <summary>
        /// Método responsavel por montar a chave do WorkFlow
        /// </summary>
        public void atualizaChave()
        {
            try
            {
                this.objChave.Valor = this.objCodigo.Valor;
                if ((this.objCodigo.Valor != null) && (this.objCodigo.Valor != string.Empty))
                {
                    if ((this.objSuperior.Valor != null) && (this.objSuperior.Valor != string.Empty))
                    {
                        string strSql = string.Empty;
                        strSql = "SELECT chave FROM workflow";
                        strSql += " WHERE workflow_codigo = " + this.objSuperior.Valor;
                        System.Data.SqlClient.SqlDataReader objDateReader = ServiceDesk.Banco.ClsBanco.geraDataReader(strSql);
                        if (objDateReader.Read())
                        {
                            if (objDateReader[0] != null)
                            {
                                this.objChave.Valor = objDateReader[0].ToString() + "," + this.objCodigo.Valor.ToString();
                            }
                        }
                        objDateReader = null;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region metodo atualizaChaveFilhos
        /// <summary>
        /// Método que atualiza a chave dos itens filhos de um determinado item
        /// </summary>
        /// <param name="intCodigoPai">Código do Pai dos itens que serão atualizados</param>
        public static void atualizaChaveFilhos(int intCodigoPai)
        {
            try
            {
                string strSql = string.Empty;

                strSql = "SELECT workflow_codigo,workflow_codigo_superior FROM workflow";
                strSql += " WHERE workflow_codigo_superior = " + intCodigoPai.ToString();
                System.Data.SqlClient.SqlDataReader objDateReader = ServiceDesk.Banco.ClsBanco.geraDataReader(strSql);

                while (objDateReader.Read())
                {

                    strSql = "UPDATE workflow";
                    strSql += " SET chave = ";
                    strSql += " (SELECT chave FROM workflow WHERE workflow_codigo = " + objDateReader["workflow_codigo_superior"].ToString() + ") + ',' + LTRIM(STR(ic_codigo))";
                    strSql += " WHERE workflow_codigo = " + objDateReader["workflow_codigo"].ToString();
                    ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
                    objBanco.executaSQL(strSql);
                    objBanco = null;

                    atualizaChaveFilhos(Convert.ToInt32(objDateReader["workflow_codigo"].ToString()));

                }

                objDateReader = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Metodo existeDescricao
        /// <summary>
        /// 
        /// </summary>
        public bool existeDescricao()
        {
            bool bolRetorno = false;
            string strRetorno = string.Empty;

            ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
            strRetorno = objBanco.retornaValorCampo("workflow", "workflow_codigo", " LOWER(descricao) = '" + this.objDescricao.Valor.ToLower() + "' AND workflow_codigo <> " + this.objCodigo.Valor);
            if (strRetorno != string.Empty)
            {
                bolRetorno = true;
            }
            objBanco = null;

            return bolRetorno;
        }
        #endregion

        #region Metodo existeDescricao
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strDescricao"></param>
        /// <returns></returns>
        public static bool existeDescricao(string strDescricao)
        {
            bool bolRetorno = false;
            string strRetorno = string.Empty;

            ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
            strRetorno = objBanco.retornaValorCampo("workflow", "workflow_codigo", " LOWER(descricao) = '" + strDescricao.ToLower() + "'");
            if (strRetorno != string.Empty)
            {
                bolRetorno = true;
            }
            objBanco = null;
            return bolRetorno;
        }
        #endregion

        #region metodo populaNoRaiz
        /// <summary>
        /// Método que popula os nós que não possuem pai
        /// </summary>
        public static void populaNoRaiz(int intCodigoPai, TreeView objTreeView, TreeNode objTreeNode, string strUrl, int intCodigoTipo)
        {
            try
            {
                string strSql = string.Empty;

                strSql = "SELECT workflow_codigo, workflow_codigo_superior, descricao";
                strSql += ", (SELECT count(*) FROM workflow WHERE workflow_codigo_superior = item.workflow_codigo) pai";
                strSql += " FROM workflow item";
                if (intCodigoPai > 0)
                {
                    strSql += " WHERE workflow_codigo_superior = " + intCodigoPai.ToString();
                }
                else
                {
                    strSql += " WHERE workflow_codigo_superior is null";
                }
                if (intCodigoTipo > 0)
                {
                    strSql += " AND workflow_tipo_codigo = " + intCodigoTipo.ToString();
                }
                strSql += " ORDER BY descricao";

                System.Data.SqlClient.SqlDataReader objDataReader = ServiceDesk.Banco.ClsBanco.geraDataReader(strSql);

                if (objTreeView != null)
                {
                    objTreeView.Nodes.Clear();
                    ClsWorkFlow.populaNos(objDataReader, objTreeView.Nodes, strUrl);
                }
                else if (objTreeNode != null)
                {
                    ClsWorkFlow.populaNos(objDataReader, objTreeNode.ChildNodes, strUrl);
                }

                objDataReader = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region metodo populaNos
        /// <summary>
        /// Método que popula os nós
        /// </summary>
        public static void populaNos(System.Data.SqlClient.SqlDataReader objDataReader, TreeNodeCollection objTreeNodeCollection, string strUrl)
        {
            try
            {
                while (objDataReader.Read())
                {
                    TreeNode objTreeNode = new TreeNode();
                    objTreeNode.Text = objDataReader["descricao"].ToString();
                    objTreeNode.Value = objDataReader["workflow_codigo"].ToString();
                    if (strUrl != string.Empty)
                    {
                        objTreeNode.NavigateUrl = strUrl + "codigo=" + objTreeNode.Value;
                    }
                    if (Convert.ToInt32(objDataReader["pai"]) > 0)
                    {
                        objTreeNode.PopulateOnDemand = true;
                    }
                    objTreeNodeCollection.Add(objTreeNode);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Metodo salvaStatus
        /// <summary>
        /// Salva o novo Status
        /// </summary>
        public static bool salvaStatus(string strTabela, int intCodigoItem, int intStatusAnterior, int intStatusAtual, int intProximoStatus)
        {
            string strSql = string.Empty;

            if ((strTabela.Trim() != string.Empty) && (intCodigoItem > 0))
            {
                if (intProximoStatus <= 0)
                {
                    intProximoStatus = SServiceDesk.Negocio.ClsWorkFlow.proximoStatusAutomatico(strTabela.Trim(), intStatusAtual.ToString(), intCodigoItem);
                }

                if (intProximoStatus > 0)
                {
                    strSql = "UPDATE " + strTabela.Trim();
                    strSql += " SET status_codigo = " + intProximoStatus.ToString();
                    strSql += " WHERE " + strTabela.Trim() + "_codigo = " + intCodigoItem.ToString();
                    ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
                    objBanco.executaSQL(strSql);

                    //=========================================================================================//
                    // - O que: Escalação Horizontal Não Livre - Atualiza o código do nivel para um nível 
                    // inferior ao atual se foi reprovado em algum nível e se a escalação não for livre.
                    // - Quem: Fernanda Passos
                    // - Quando: 03/2006
                    //=========================================================================================//
                    if (ClsParametro.CodigoStatusReprovado1Nivel == intProximoStatus.ToString() || ClsParametro.CodigoStatusReprovado2Nivel == intProximoStatus.ToString() || ClsParametro.CodigoStatusReprovado3Nivel == intProximoStatus.ToString()) ClsEquipeNivel.AtualizaProcessoNivelInferiorAoAtual(intCodigoItem, strTabela.Trim());
                    //=========================================================================================//
                    objBanco = null;

                    salvaRepercusao(intCodigoItem, strTabela.Trim().ToLower(), intProximoStatus.ToString());

                    if (intStatusAnterior != 0) gravaLog(intCodigoItem, strTabela.Trim().ToLower(), intStatusAnterior.ToString(), intProximoStatus.ToString()); else gravaLog(intCodigoItem, strTabela.Trim().ToLower(), intStatusAtual.ToString(), intProximoStatus.ToString());
                }
            }
            return true;

        }
        #endregion

        #region metodo gravaLog
        /// <summary>
        /// 
        /// </summary>
        /// <param name="intCodigo"></param>
        /// <param name="strTabelaOrigem"></param>
        /// <param name="strStatusOrigem"></param>
        public static void gravaLog(int intCodigo, string strTabelaOrigem, string strStatusOrigem, string strStatusDestino)
        {
            string strFormatoDataInclusao = ClsParametro.DataInclusao;
            DateTime dataAnterior = DateTime.MinValue;
            DateTime dataAtual = DateTime.Now;
            int iTempo = 0;
            string strMensagem = string.Empty;
            string strStatusOrigemLogAnterior = string.Empty;
            string strStatusDestinoLogAnterior = string.Empty;
            string strWorkFlowCodigo = string.Empty;

            //Busca o registro de log anterior se hover
            string strCodigoStatusLogAnterior = ServiceDesk.Negocio.ClsStatusLog.buscaCodigoPenultimoStatus(strTabelaOrigem, intCodigo.ToString());
            if (strCodigoStatusLogAnterior != string.Empty)
            {
                //instancia um objeto com o log anterior
                ClsStatusLog objStatusLogAnterior = new ClsStatusLog(Convert.ToInt32(strCodigoStatusLogAnterior));
                dataAnterior = Convert.ToDateTime(objStatusLogAnterior.Data.Valor);
                strStatusOrigemLogAnterior = objStatusLogAnterior.StatusOrigem.Valor.Trim();
                strStatusDestinoLogAnterior = objStatusLogAnterior.StatusDestino.Valor.Trim();

                strWorkFlowCodigo = retornaCodigoWorkFlow(strTabelaOrigem, Convert.ToInt32(objStatusLogAnterior.WorkflowCodigo.Valor), Convert.ToInt32(strStatusDestino)).ToString();

                objStatusLogAnterior = null;
            }

            if (strWorkFlowCodigo == string.Empty)
            {
                strWorkFlowCodigo = retornaCodigoWorkFlow(strTabelaOrigem, 0, 0).ToString();
            }

            ClsStatusLog objStatusLog = new ClsStatusLog();
            objStatusLog.Data.Valor = dataAtual.ToString(strFormatoDataInclusao);
            objStatusLog.Pessoa.Valor = ClsUsuario.getCodigoUsuario().ToString();
            objStatusLog.StatusOrigem.Valor = strStatusOrigem;
            objStatusLog.StatusDestino.Valor = strStatusDestino;
            objStatusLog.Tabela.Valor = strTabelaOrigem;
            objStatusLog.TabelaIdentificador.Valor = intCodigo.ToString();

            if ((strWorkFlowCodigo != string.Empty) && (strWorkFlowCodigo != "0"))
            {
                objStatusLog.WorkflowCodigo.Valor = strWorkFlowCodigo;
            }

            if (dataAnterior.Date != DateTime.MinValue)
            {
                iTempo = ServiceDesk.Generica.ClsData.getDiferencaData(dataAnterior, dataAtual, "min");
            }
            objStatusLog.Tempo.Valor = iTempo.ToString();

            ServiceDesk.Negocio.ClsIdentificador objIdentificador = new ServiceDesk.Negocio.ClsIdentificador();
            objIdentificador.Tabela.Valor = objStatusLog.Atributos.NomeTabela;
            objStatusLog.Codigo.Valor = objIdentificador.getProximoValor().ToString();

            if (objStatusLog.StatusDestino.Valor.Trim() != strStatusDestinoLogAnterior)
            {
                if (objStatusLog.insere(out strMensagem))
                {
                    objIdentificador.atualizaValor();
                }
            }
            objIdentificador = null;
            objStatusLog = null;

        }

        #endregion

        #region metodo salvaRepercusao
        /// <summary>
        /// 
        /// </summary>
        /// <param name="intCodigo"></param>
        /// <param name="strTabelaOrigem"></param>
        /// <param name="strStatusOrigem"></param>
        public static void salvaRepercusao(int intCodigo, string strTabelaOrigem, string strStatusOrigem)
        {
            string strSql = string.Empty;
            string strSqlCondicao = string.Empty;
            string strTabelaDestino = string.Empty;
            string strStatusDestino = string.Empty;

            strTabelaOrigem = strTabelaOrigem.ToLower();

            strSql = "SELECT workflowrepercusao.*";
            strSql += " FROM workflowrepercusao, workflow, workflowtipo";
            strSql += " WHERE workflowrepercusao.workflow_codigo = workflow.workflow_codigo";
            strSql += " AND workflow.workflow_tipo_codigo = workflowtipo.workflow_tipo_codigo";
            strSql += " AND tabela = '" + strTabelaOrigem + "'";
            strSql += " AND tabela_origem = '" + strTabelaOrigem + "'";
            strSql += " AND status_codigo_origem = " + strStatusOrigem;

            System.Data.SqlClient.SqlDataReader objDataReader = ServiceDesk.Banco.ClsBanco.geraDataReader(strSql);

            while (objDataReader.Read())
            {
                strTabelaDestino = objDataReader["tabela_destino"].ToString().ToLower();
                strStatusDestino = objDataReader["status_codigo_destino"].ToString().ToLower();

                strSql = "UPDATE " + strTabelaDestino;
                strSql += " SET status_codigo = " + strStatusDestino;
                strSql += " WHERE " + strTabelaDestino + "_codigo IN ";

                if (((strTabelaOrigem == "incidente") && (strTabelaDestino == "chamado")) || ((strTabelaDestino == "incidente") && (strTabelaOrigem == "chamado")))
                {
                    strSqlCondicao = " (SELECT " + strTabelaDestino + "_codigo FROM incidentechamado WHERE";
                    strSqlCondicao += " " + strTabelaOrigem + "_codigo = " + intCodigo.ToString() + ")";
                }
                else if (((strTabelaDestino == "problema") && (strTabelaOrigem == "incidente")) || ((strTabelaDestino == "problema") && (strTabelaOrigem == "incidente")))
                {
                    strSqlCondicao = " (SELECT " + strTabelaDestino + "_codigo FROM problemaincidente WHERE";
                    strSqlCondicao += " " + strTabelaOrigem + "_codigo = " + intCodigo.ToString() + ")";
                }
                else if (((strTabelaDestino == "chamado") && (strTabelaOrigem == "requisicaoservico")) || ((strTabelaDestino == "requisicaoservico") && (strTabelaOrigem == "chamado")))
                {
                    strSqlCondicao = " (SELECT " + strTabelaDestino + "_codigo FROM requisicaoservicochamado WHERE";
                    strSqlCondicao += " " + strTabelaOrigem + "_codigo = " + intCodigo.ToString() + ")";
                }

                if (strSqlCondicao != string.Empty)
                {
                    ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
                    objBanco.executaSQL(strSql + strSqlCondicao);
                    objBanco = null;

                    strSql = "SELECT " + strTabelaDestino + "_codigo";
                    strSql += " FROM " + strTabelaDestino;
                    strSql += " WHERE " + strTabelaDestino + "_codigo IN ";
                    strSql += strSqlCondicao;
                    System.Data.SqlClient.SqlDataReader objDataReader2 = ServiceDesk.Banco.ClsBanco.geraDataReader(strSql);
                    while (objDataReader2.Read())
                    {
                        salvaRepercusao(Convert.ToInt32(objDataReader2[0].ToString()), strTabelaDestino, strStatusDestino);
                    }
                    objDataReader2.Dispose();
                    objDataReader2 = null;

                }

            }

            objDataReader.Dispose();
            objDataReader = null;

        }
        #endregion

        #endregion

        #region Propriedades
        public ServiceDesk.Banco.ClsAtributos Atributos
        {
            get { return this.objAtributos; }
        }

        /// <summary>
        /// Código do Anexo
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo Codigo
        {
            get { return objCodigo; }
        }

        /// <summary>
        /// Tipo do WorkFlow
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo Tipo
        {
            get { return objTipo; }
            set { this.objTipo = value; }
        }

        /// <summary>
        /// Código do WorkFlow Superior (Pai)
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo Superior
        {
            get { return objSuperior; }
            set { this.objSuperior = value; }
        }

        /// <summary>
        /// Código do Status
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo Status
        {
            get { return objStatus; }
            set { this.objStatus = value; }
        }

        /// <summary>
        /// Descrição
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo Descricao
        {
            get { return objDescricao; }
            set { this.objDescricao = value; }
        }

        /// <summary>
        /// Pré Condição
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo PreCondicao
        {
            get { return objPreCondicao; }
            set { this.objPreCondicao = value; }
        }

        /// <summary>
        /// Ordenação
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo Ordenacao
        {
            get { return objOrdenacao; }
            set { this.objOrdenacao = value; }
        }

        /// <summary>
        /// Chave
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo Chave
        {
            get { return objChave; }
            set { this.objChave = value; }
        }

        /// <summary>
        /// Flag Semi-automático
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo FlagSemiAutomatico
        {
            get { return objFlagSemiAutomatico; }
            set { this.objFlagSemiAutomatico = value; }
        }
        #endregion

        #region Construtor
        public ClsWorkFlow()
        {
            this.alimentaColecaoCampos();
        }
        #endregion

    }
}