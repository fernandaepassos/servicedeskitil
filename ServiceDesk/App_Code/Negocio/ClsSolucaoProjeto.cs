/*
- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
  • Classe de acesso aos dados da tabela SolucaoProjeto.
  
  	Data: 19/12/2005
  	Autor: Fernanda Passos
  	Descrição: Esta classe visa apresentar funcionalidades que permite o perfeito acessoa aos dados
    da tabela que armazena os dados da unificação das soluções de projeto para os diversos processos
    que existe dentro do sistema (Problema, Incidente, Chamada e outros).
  
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
/// Classe de acesso a dados da tabela ProblemaTipo.
/// </summary>
namespace ServiceDesk.Negocio
{
    /// <summary>
    /// Construtor da classe
    /// </summary>
    public class ClsSolucaoProjeto
    {

        #region Construtores da classe
        public ClsSolucaoProjeto()
        {
            alimentaColecaoCampos();
        }

        /// <summary>
        /// Construtor da classe com passagem de parametro
        /// </summary>
        public ClsSolucaoProjeto(int intCodigo)
        {
            this.alimentaColecaoCampos();
            this.objCodigo.Valor = intCodigo.ToString();
            ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
            objBanco.alimentaColecao(this.objAtributos);
            objBanco = null;
        }

        #endregion

        #region Declarações

        //Coleção de objetos.
        private ServiceDesk.Banco.ClsAtributos objAtributos = new ServiceDesk.Banco.ClsAtributos();
        //Atributos.
        private ServiceDesk.Banco.ClsAtributo objCodigo = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objDescricao = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objCodigoSolucaoTipo = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objTabela = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objCodigoIdentificadorTabela = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objFlagImplementacao = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objDescricaoNaoImplementacao = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objDataInclusao = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objPessoaCodigoInclusao = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objDataAlteracao = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objPessoaCodigoAlteracao = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objCodigoProjeto = new ServiceDesk.Banco.ClsAtributo();
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
        /// Codigo do registro da tabela.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo Codigo
        {
            get { return objCodigo; }
            set { this.objCodigo = value; }
        }

        /// <summary>
        /// Descrição prévia sobre a solução.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo Descricao
        {
            get { return objDescricao; }
            set { this.objDescricao = value; }
        }

        /// <summary>
        /// Código do tipo da solução.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo CodigoSolucaoTipo
        {
            get { return objCodigoSolucaoTipo; }
            set { this.objCodigoSolucaoTipo = value; }
        }

        /// <summary>
        /// Nome da tabela que armazena os dados do processo que tem a solução.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo Tabela
        {
            get { return objTabela; }
            set { this.objTabela = value; }
        }

        /// <summary>
        /// Código do registro identificador na tabela.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo CodigoIdentificadorTabela
        {
            get { return objCodigoIdentificadorTabela; }
            set { this.objCodigoIdentificadorTabela = value; }
        }

        /// <summary>
        /// Flag que informa se a solução será ou não implementada.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo FlagImplementacao
        {
            get { return objFlagImplementacao; }
            set { this.objFlagImplementacao = value; }
        }

        /// <summary>
        /// Descrição da não implementação da solução.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo DescricaoNaoImplementacao
        {
            get { return objDescricaoNaoImplementacao; }
            set { this.objDescricaoNaoImplementacao = value; }
        }

        /// <summary>
        /// Data a inclusão do registro no banco de dados.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo DataInclusao
        {
            get { return objDataInclusao; }
            set { this.objDataInclusao = value; }
        }

        /// <summary>
        /// Código da pessoa que cadastrou o registro no banco de dados.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo PessoaCodigoInclusao
        {
            get { return objPessoaCodigoInclusao; }
            set { this.objPessoaCodigoInclusao = value; }
        }

        /// <summary>
        /// Data da ultima alteracao do registro no banco de dados.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo DataAlteracao
        {
            get { return objDataAlteracao; }
            set { this.objDataAlteracao = value; }
        }

        /// <summary>
        /// Código da pessoa que fez a ultima alteração do registro no banco de dados.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo PessoaCodigoAlteracao
        {
            get { return objPessoaCodigoAlteracao; }
            set { this.objPessoaCodigoAlteracao = value; }
        }

        /// <summary>
        /// Código do projeto que esta para a solução.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo CodigoProjeto
        {
            get { return objCodigoProjeto; }
            set { this.objCodigoProjeto = value; }
        }

        #endregion

        #region Metodos

        #region Verifica ser existe projeto para a solução
        /// <summary>
        /// Verifica se existe projeto para a solução.
        /// </summary>
        /// <param name="intCodigoSolucaoProjeto">Código do registro da solução</param>
        /// <returns>Retorna true ou false. Se existe ou não.</returns>
        public bool VerificaSeExisteProjetoParaSolucao(int intCodigoSolucaoProjeto, out int intCodigoProjetoRaiz)
        {
            try
            {
                //if (intCodigoSolucaoProjeto <= 0) return false ;

                intCodigoProjetoRaiz = 0;
                string strSql = string.Empty;

                strSql = "select projeto_codigo";
                strSql += " from SolucaoProjeto";
                strSql += " where solucao_projeto_codigo = " + intCodigoSolucaoProjeto + "";

                System.Data.SqlClient.SqlDataReader objReader = ServiceDesk.Banco.ClsBanco.geraDataReader(strSql);

                if (objReader.Read() && objReader["projeto_codigo"].ToString() != string.Empty)
                {
                    intCodigoProjetoRaiz = Convert.ToInt32(objReader["projeto_codigo"].ToString());
                    objReader.Dispose();
                    return true;
                }
                else
                {
                    objReader.Dispose();
                    intCodigoProjetoRaiz = 0;
                    return false;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion

        #region Validacao dos dados
        /// <summary>
        /// Validação da integridade dos registros.
        /// </summary>
        /// <returns>Retorna true ou false. Se for validado ou não.</returns>
        public bool ValidacaoDados(out String strMsg)
        {
            try
            {
                strMsg = String.Empty;

                if (objCodigo.Valor.Trim() == string.Empty)
                {
                    strMsg = "Informe o código identificador do registro.";
                    return false;
                }
                else if (objTabela.Valor.Trim() == string.Empty)
                {
                    strMsg = "Informe o nome da tabela que representa o processo.";
                    return false;
                }
                else if (objCodigoIdentificadorTabela.Valor.Trim() == string.Empty)
                {
                    strMsg = "Informe o código identificador do registro na tabela que representa o processo.";
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

        #region Valida exclusão
        /// <summary>
        /// Valida exclusão
        /// </summary>
        /// <param name="strMsg"></param>
        /// <returns>Retorna true ou false. Se a exclusão é válida ou não.</returns>
        public bool ValidaExclusao(out String strMsg)
        {
            strMsg = string.Empty;
            try
            {
                ///>>> Valida exclusão dos projetos da solução.
                ServiceDesk.Projeto.ClsProjeto objProjeto = new ServiceDesk.Projeto.ClsProjeto();
                if (objProjeto.ValidaExclusao(out strMsg) == false)
                {
                    objProjeto = null;
                    return false;
                }

                objProjeto = null;
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Verifica se já esta cadastrado
        /// <summary>
        /// Verifica se já esta cadastrado
        /// </summary>
        /// <returns>Retorna false ou true. Se esta cadastrado ou se não esta cadastrado<returns>
        public bool VerificaSeJaEstaCadastrado(out String strMsg)
        {
            try
            {
                strMsg = String.Empty;

                ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
                string strValorCampo = string.Empty;

                //>> Verifica se já existe solução com o nome informado.
                strMsg = " solucao_projeto_codigo <> " + Convert.ToInt32(objCodigo.Valor.Trim()) + "";
                strMsg += " and tabela = '" + objTabela.Valor.Trim() + "'";
                strMsg += " and tabela_identificador = " + Convert.ToInt32(objCodigoIdentificadorTabela.Valor.Trim()) + "";
                if (objCodigoProjeto.Valor != string.Empty) strMsg += " and projeto_codigo = " + Convert.ToInt32(objCodigoProjeto.Valor.Trim()) + "";

                strValorCampo = objBanco.retornaValorCampo("SolucaoProjeto", "tabela", strMsg);
                objBanco = null;
                if (strValorCampo != string.Empty)
                {
                    strMsg = "Esta solução já esta para o processo informado.";
                    return true;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                strMsg = ex.Message;
                throw ex;
            }

        }
        #endregion

        #region Pega dados de solução por código identificador.
        /// <summary>
        /// Pega dados da solucao por código identificador do registro.
        /// </summary>
        /// <param name="intCodigoSolucaoProjeto">Código do registro que deseja localizar</param>
        /// <returns>Retorna um data reader</returns>
        public System.Data.SqlClient.SqlDataReader GetDataSolucaoProjeto(int intCodigoSolucaoProjeto)
        {
            try
            {

                string strSql = string.Empty;

                strSql = " select solucao_projeto_codigo, descricao, solucao_projeto_tipo_codigo, flag_implementacao, descricao_nao_implementacao";
                strSql += " from SolucaoProjeto";
                strSql += " where solucao_projeto_codigo = " + intCodigoSolucaoProjeto + "";

                System.Data.SqlClient.SqlDataReader objReader = ServiceDesk.Banco.ClsBanco.geraDataReader(strSql);
                return objReader;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        #endregion

        #region Pega dados de solução por tabela e código identificador.
        /// <summary>
        /// Pega dados da solucao por tabela e código identificador.
        /// </summary>
        /// <param name="intCodigoIdentificador" >Código identificador da tabela</param>
        /// <param name="strTabela">Nome da tabela que representa o processo</param> 
        /// <returns>Retorna um data reader</returns>
        public System.Data.SqlClient.SqlDataReader GetDataSolucaoProjeto(int intCodigoIdentificador, string strTabela)
        {
            try
            {
                string strSql = string.Empty;

                strSql = " select solucao_projeto_codigo, descricao, solucao_projeto_tipo_codigo, flag_implementacao, descricao_nao_implementacao";
                strSql += " from SolucaoProjeto";
                strSql += " where tabela = '" + strTabela + "'";
                strSql += " and tabela_identificador = " + intCodigoIdentificador + "";

                System.Data.SqlClient.SqlDataReader objReader = ServiceDesk.Banco.ClsBanco.geraDataReader(strSql);
                return objReader;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        #endregion

        #region Pega dados do processo para qual a solução foi cadastrada.
        /// <summary>
        /// Pega dados do processo para qual a solução foi cadastrada.
        /// </summary>
        /// <param name="intCodigoIdentificador" >Código identificador da tabela</param>
        /// <param name="strTabela">Nome da tabela que representa o processo</param> 
        /// <returns>Retorna um data reader</returns>
        public System.Data.SqlClient.SqlDataReader GetDadosProcessoOrigemSolucao(int intCodigoIdentificador, string strTabela)
        {
            try
            {
                string strSql = string.Empty;
                System.Data.SqlClient.SqlDataReader objReader;

                if (strTabela == "Problema")
                {
                    strSql = " select P.nome, P.descricao, (select descricao from Equipe where equipe_codigo = P.equipe_codigo_alocacao)descricao,";
                    strSql += " (select descricao from Status where status_codigo = P.status_codigo) status, ";
                    strSql += " (select nome from Pessoa where pessoa_codigo = P.pessoa_codigo_alocacao)pessoa_alocada, ";
                    strSql += " (select descricao from Equipe where equipe_codigo = P.equipe_codigo_alocacao) equipe_alocada";
                    strSql += " from Problema P";
                    strSql += " where P.problema_codigo = " + intCodigoIdentificador + "";

                    objReader = ServiceDesk.Banco.ClsBanco.geraDataReader(strSql);
                    return objReader;
                }
                else if (strTabela == "Chamado")
                {
                    strSql = "select C.escalado, C.vip, C.chamado_codigo, (select descricao from Status where status_codigo = C.status) status, ";
                    strSql += " (select descricao from Impacto where impacto_codigo = C.impacto_codigo)impacto,";
                    strSql += " (select nome from Pessoa where pessoa_codigo = C.pessoa_codigo_solicitante)solicitante,";
                    strSql += " (select descricao from Equipe where equipe_codigo = C.equipe_codigo_alocacao) equipe,";
                    strSql += " (select descricao from Urgencia where urgencia_codigo = C.urgencia_codigo)urgencia,";
                    strSql += " (Select nome from Pessoa where pessoa_codigo = C.pessoa_codigo_proprietario)proprietario,";
                    strSql += " (select nome from Pessoa where pessoa_codigo = C.pessoa_codigo_alocacao)tecnico_pessoa_alocada,";
                    strSql += " (select descricao from Prioridade where prioridade_codigo = C.prioridade_codigo)prioridade,";
                    strSql += " (select descricao from ChamadoOrigem where chamado_origem_codigo = C.origem_chamado_tipo_codigo)origem_chamado,";
                    strSql += "(select descricao from ChamadoTipo where chamado_tipo_codigo = C.chamado_tipo_codigo)tipo_chamado";
                    strSql += " from Chamado C ";
                    strSql += " where C.chamado_codigo = " + intCodigoIdentificador + "";

                    objReader = ServiceDesk.Banco.ClsBanco.geraDataReader(strSql);
                    return objReader;
                }
                else if (strTabela == "Incidente")
                {
                    strSql = "select I.descricao, I.incidente_codigo,";
                    strSql += " (select nome from Pessoa where pessoa_codigo = I.pessoa_codigo_solicitante)solicitante,";
                    strSql += " (select descricao from Impacto where impacto_codigo = I.impacto_codigo)impacto,";
                    strSql += " (select descricao from Equipe where equipe_codigo = I.equipe_codigo_alocacao)equipe,";
                    strSql += " (select descricao from Urgencia where urgencia_codigo = I.urgencia_codigo)urgencia,";
                    strSql += " (select nome from Pessoa where pessoa_codigo = I.pessoa_codigo_alocacao)pessoa_alocacao,";
                    strSql += " (select descricao from Prioridade where prioridade_codigo = I.prioridade_codigo)prioridade,";
                    strSql += " (select nome from Pessoa where pessoa_codigo = I.pessoa_codigo_proprietario)proprietario,";
                    strSql += " (select descricao from ChamadoTipo where chamado_tipo_codigo = I.incidente_origem_codigo) origem,";
                    strSql += " (select descricao from Status where status_codigo = I.status)status";
                    strSql += " from Incidente I";
                    strSql += " where incidente_codigo = " + intCodigoIdentificador + "";

                    objReader = ServiceDesk.Banco.ClsBanco.geraDataReader(strSql);
                    return objReader;
                }
                return null;
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
                string strSql = "select max(solucao_projeto_codigo) as maximo from SolucaoProjeto";
                System.Data.SqlClient.SqlDataReader objDtReader = ServiceDesk.Banco.ClsBanco.geraDataReader(strSql);

                if (objDtReader.Read())
                {
                    string strMax = objDtReader["maximo"].ToString();
                    objDtReader.Dispose();
                    if (strMax == string.Empty)
                        return 1;
                    else
                        return Convert.ToInt32(strMax) + 1;
                }
                else
                    return 1;
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

                if (ValidacaoDados(out strMensagem) == false)
                {
                    return false;
                }
                else
                {
                    ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
                    //Verifica se já esta cadastrado no banco.
                    if (VerificaSeJaEstaCadastrado(out strMensagem) == true)
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

                if (ValidacaoDados(out strMensagem) == false)
                {
                    bolRetorno = false;
                }
                else
                {
                    ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
                    //if (VerificaSeJaEstaCadastrado(out strMensagem) == true )
                    //{
                    //    bolRetorno = false;
                    //}
                    //else 
                    if (objBanco.alteraColecao(this.objAtributos))
                    {
                        strMensagem = "Registro alterado com sucesso.";
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

                //Valida exclusão da Solução Projeto
                //if (ValidaExclusao(out strMsg) == false) return false;

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

        #region Alimenta campos coleção
        /// <summary>
        /// Alimenta campos coleção
        /// </summary>
        private void alimentaColecaoCampos()
        {
            try
            {
                objAtributos.NomeTabela = "SolucaoProjeto";
                objAtributos.DescricaoTabela = "Tabela que armazena as soluções gerenciadas no projeto com os processos.";

                objCodigo.Campo = "solucao_projeto_codigo";
                objCodigo.Descricao = "Código identificador do registro na tabela.";
                objCodigo.CampoIdentificador = true;
                objCodigo.CampoObrigatorio = true;
                objCodigo.Tipo = System.Data.DbType.Int32;
                objAtributos.Add(objCodigo);

                objDescricao.Campo = "descricao";
                objDescricao.Descricao = "Descrição prévia da solução.";
                objDescricao.CampoIdentificador = false;
                objDescricao.CampoObrigatorio = false;
                objDescricao.Tipo = System.Data.DbType.String;
                objDescricao.Tamanho = 100;
                objAtributos.Add(objDescricao);

                objCodigoSolucaoTipo.Campo = "solucao_projeto_tipo_codigo";
                objCodigoSolucaoTipo.Descricao = "Código do tipo de solução.";
                objCodigoSolucaoTipo.CampoIdentificador = false;
                objCodigoSolucaoTipo.CampoObrigatorio = false;
                objCodigoSolucaoTipo.Tipo = System.Data.DbType.Int32;
                objAtributos.Add(objCodigoSolucaoTipo);

                objTabela.Campo = "tabela";
                objTabela.Descricao = "Nome da tabela que representa o processo que necessitou da solução.";
                objTabela.CampoIdentificador = false;
                objTabela.CampoObrigatorio = true;
                objTabela.Tipo = System.Data.DbType.String;
                objTabela.Tamanho = 100;
                objAtributos.Add(objTabela);

                objCodigoIdentificadorTabela.Campo = "tabela_identificador";
                objCodigoIdentificadorTabela.Descricao = "Código do registro identificador da tabela.";
                objCodigoIdentificadorTabela.CampoIdentificador = false;
                objCodigoIdentificadorTabela.CampoObrigatorio = true;
                objCodigoIdentificadorTabela.Tipo = System.Data.DbType.Int32;
                objAtributos.Add(objCodigoIdentificadorTabela);

                objFlagImplementacao.Campo = "flag_implementacao";
                objFlagImplementacao.Descricao = "Flag indicador da implementação ou não da solução.";
                objFlagImplementacao.CampoIdentificador = false;
                objFlagImplementacao.CampoObrigatorio = false;
                objFlagImplementacao.Tipo = System.Data.DbType.String;
                objFlagImplementacao.Tamanho = 1;
                objAtributos.Add(objFlagImplementacao);

                objDescricaoNaoImplementacao.Campo = "descricao_nao_implementacao";
                objDescricaoNaoImplementacao.Descricao = "Descrição da não implementação da solução.";
                objDescricaoNaoImplementacao.CampoIdentificador = false;
                objDescricaoNaoImplementacao.CampoObrigatorio = false;
                objDescricaoNaoImplementacao.Tipo = System.Data.DbType.String;
                objDescricaoNaoImplementacao.Tamanho = 200;
                objAtributos.Add(objDescricaoNaoImplementacao);

                objDataInclusao.Campo = "data_inclusao";
                objDataInclusao.Descricao = "Data da inclusão do registro no banco de dados.";
                objDataInclusao.CampoIdentificador = false;
                objDataInclusao.CampoObrigatorio = false;
                objDataInclusao.Tipo = System.Data.DbType.DateTime;
                objAtributos.Add(objDataInclusao);

                objPessoaCodigoInclusao.Campo = "pessoa_codigo_inclusao";
                objPessoaCodigoInclusao.Descricao = "Código da pessoa que incluiu o registro no banco de dados.";
                objPessoaCodigoInclusao.CampoIdentificador = false;
                objPessoaCodigoInclusao.CampoObrigatorio = false;
                objPessoaCodigoInclusao.Tipo = System.Data.DbType.Int32;
                objAtributos.Add(objPessoaCodigoInclusao);

                objDataAlteracao.Campo = "data_alteracao";
                objDataAlteracao.Descricao = "Data da última alteração do registro no banco de dados.";
                objDataAlteracao.CampoIdentificador = false;
                objDataAlteracao.CampoObrigatorio = false;
                objDataAlteracao.Tipo = System.Data.DbType.DateTime;
                objAtributos.Add(objDataAlteracao);

                objPessoaCodigoAlteracao.Campo = "pessoa_codigo_alteracao";
                objPessoaCodigoAlteracao.Descricao = "Código da pessoa que realizou a ultima alteração.";
                objPessoaCodigoAlteracao.CampoIdentificador = false;
                objPessoaCodigoAlteracao.CampoObrigatorio = false;
                objPessoaCodigoAlteracao.Tipo = System.Data.DbType.Int32;
                objAtributos.Add(objPessoaCodigoAlteracao);

                objCodigoProjeto.Campo = "projeto_codigo";
                objCodigoProjeto.Descricao = "Código do projeto que esta para a solução.";
                objCodigoProjeto.CampoIdentificador = false;
                objCodigoProjeto.CampoObrigatorio = false;
                objCodigoProjeto.Tipo = System.Data.DbType.Int32;
                objAtributos.Add(objCodigoProjeto);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region GeraDropDownList
        /// <summary>
        /// Gera um novo DropDownList de acordo com a coleção de atributos.
        /// </summary>
        /// <param name="objDropDownList">DropDownList</param>
        public static void geraDropDownList(System.Web.UI.WebControls.DropDownList objDropDownList, string strItemDefault)
        {
            try
            {
                ClsSolucaoProjeto objSolucaoProjeto = new ClsSolucaoProjeto();
                objDropDownList.DataTextField = objSolucaoProjeto.objDescricao.Campo;
                objDropDownList.DataValueField = objSolucaoProjeto.objCodigo.Campo;
                ServiceDesk.Controle.ClsDropDownList.geraDropDownList(objDropDownList, objSolucaoProjeto.objAtributos);
                objSolucaoProjeto = null;

                //Adiciona a opção default no dropdownlist.
                ListItem itemDefault = new ListItem();
                itemDefault.Text = strItemDefault.Trim();
                itemDefault.Value = "";
                itemDefault.Selected = true;
                objDropDownList.Items.Insert(0, itemDefault);
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
                strSql += " from SolucaoProjeto";
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
                ClsSolucaoProjeto objSolucaoProjeto = new ClsSolucaoProjeto();
                ServiceDesk.Controle.ClsGridView.geraGridView(objGridView, objSolucaoProjeto.objAtributos);
                objSolucaoProjeto = null;
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
        /// <param name="objGridView">objeto Grid View</param>
        /// <param name="bolCondicao">Condição para verificar se será utilizado a cláusula Where</param>
        public static void geraGridView(System.Web.UI.WebControls.GridView objGridView, ClsSolucaoProjeto objSolucaoProblema, bool bolCondicao)
        {
            try
            {
                objGridView.AutoGenerateColumns = false;
                ServiceDesk.Controle.ClsGridView.geraGridView(objGridView, objSolucaoProblema.objAtributos, bolCondicao);
                objSolucaoProblema = null;
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
        /// <param name="dtDataFinal" >DateTime  - Data final do cadastrado da solução</param>
        /// <param name="dtDataIncial">DateTime - Data inicial do cadastro da solução</</param> 
        /// <param name="strDescricaoSolucao">String - Descrição parcial ou total da solução</param> 
        /// <param name="strTabela">String - Nome da tabela que representa o processo</param> 
        public static void geraGridView(System.Web.UI.WebControls.GridView objGridView, string strTabela, DateTime dtDataIncial, DateTime dtDataFinal, string strDescricaoSolucao)
        {
            try
            {
                objGridView.AutoGenerateColumns = false;
                string strSql = " select SP.* , SPT.descricao as tipo_solucao, SPT.solucao_projeto_tipo_codigo";
                strSql += " from SolucaoProjeto SP, SolucaoProjetoTipo SPT";
                strSql += " where SP.solucao_projeto_tipo_codigo = SPT.solucao_projeto_tipo_codigo";
                if (strDescricaoSolucao != string.Empty) strSql += " and SP.descricao like '%" + strDescricaoSolucao + "%' ";
                if (strTabela != string.Empty) strSql += " and SP.tabela = '" + strTabela + "'";
                if (dtDataIncial.ToString() != "1/1/0001 00:00:00" && dtDataFinal.ToString() != "1/1/0001 00:00:00") strSql += " and SP.data_inclusao between '" + dtDataIncial.ToString(ClsParametro.DataInclusao) + "' and '" + dtDataFinal.ToString(ClsParametro.DataInclusao) + "'";
                strSql += " order by SP.descricao";

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
        /// GeraGridView por nome da tabela e código identificador da tabela.
        /// </summary>
        /// <param name="objGridView">objeto Grid View</param>
        /// <param name="strTabela">String - Nome da tabela que representa o processo</param> 
        /// <param name="intTabelaIdentificador">Inteiro - Código identificador da tabela</param>
        public static void geraGridView(System.Web.UI.WebControls.GridView objGridView, string strTabela, int intTabelaIdentificador)
        {
            try
            {
                objGridView.AutoGenerateColumns = false;

                string strSql = " select SP.*, P.nome,";
                strSql += "(select descricao from SolucaoProjetoTipo where solucao_projeto_tipo_codigo = SP.solucao_projeto_tipo_codigo)tipo_solucao,";
                strSql += "(select solucao_projeto_tipo_codigo from SolucaoProjetoTipo where solucao_projeto_tipo_codigo = SP.solucao_projeto_tipo_codigo)solucao_projeto_tipo_codigo";
                strSql += " from SolucaoProjeto SP, Projeto P";
                strSql += " where SP.projeto_codigo = P.projeto_codigo";
                if (strTabela != string.Empty) strSql += " and SP.tabela = '" + strTabela + "'";
                if (intTabelaIdentificador != 0) strSql += " and SP.tabela_identificador = " + intTabelaIdentificador + "";
                strSql += " order by P.nome";

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
        /// GeraGridView por nome da tabela e código identificador da tabela.
        /// </summary>
        /// <param name="objGridView">objeto Grid View</param>
        /// <param name="strTabela">String - Nome da tabela que representa o processo</param> 
        /// <param name="intTabelaIdentificador">Inteiro - Código identificador da tabela</param>
        public static void geraGridView(System.Web.UI.WebControls.GridView objGridView, string strCriterioBusca, string strTabela, int intTabelaIdentificador)
        {
            try
            {
                objGridView.AutoGenerateColumns = false;
                string strSql = " SELECT P.projeto_codigo, P.nome ";
                strSql += " FROM Projeto P ";
                strSql += " WHERE P.projeto_codigo_superior = 0";

                if (strCriterioBusca != string.Empty)
                    strSql += " AND P.nome LIKE '%" + strCriterioBusca + "%'";

                strSql += " AND P.projeto_codigo NOT IN ";
                strSql += " (SELECT projeto_codigo FROM solucaoprojeto WHERE tabela = '" + strTabela + "' ";
                strSql += " AND tabela_identificador = 0" + intTabelaIdentificador.ToString() + ") ";
                strSql += " order by P.nome";

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

        #region Associa as soluções dos processos (INC, RM e RS) ao chamado que originou os processos.
        /// <summary>
        /// Associa as soluções dos processos (INC, RM e RS) á tabela\registro que originou o processo.
        /// </summary>
        /// <param name="intIdentificadorChamado">Número inteiro identificador do chamado</param>
        /// <param name="strTabelaProcessoDestino">Nome físico da tabela\processo que surgiu a partir da tabale a de origem</param>
        /// <returns>Retorna true ou false. Se foi vinculado ou não.</returns>
        public static void VinculaSolucaoExistente(int intIdentificadorChamado, string strTabelaProcessoDestino, out string strMensagem)
        {
            try
            {
                strMensagem = string.Empty;

                //Declaração das variáveis
                string strSql = string.Empty;
                System.Data.SqlClient.SqlDataReader objDataReader;
                ClsSolucaoProjeto objSolucaoProjeto = new ClsSolucaoProjeto();
                

                //Seleciona os projetos de solução do do processo originado do chamado
                strSql = " select projeto_codigo";
                strSql += " from solucaoprojeto ";
                if (strTabelaProcessoDestino == "Incidente")
                {
                    strSql += " where tabela = 'Incidente'";
                    strSql += " and tabela_identificador in (select incidente_codigo from incidentechamado where chamado_codigo in (select chamado_codigo from chamado where chamado_codigo = " + intIdentificadorChamado + "))";
                }
                else if (strTabelaProcessoDestino == "RequisicaoServico")
                {
                    strSql += " where tabela = 'RequisicaoServico'";
                    strSql += " and tabela_identificador in (select requisicaoservico_codigo from RequisicaoServicoChamado where chamado_codigo in (select chamado_codigo from chamado where chamado_codigo = "+ intIdentificadorChamado +"))";
                }
                if (strTabelaProcessoDestino == "RequisicaoMudanca")
                {
                    strSql += " where tabela = 'RequisicaoMudanca'";
                    strSql += " and tabela_identificador in (select requisicaomudanca_codigo from RequisicaoMudancaChamado where chamado_codigo in (select chamado_codigo from chamado where chamado_codigo = "+ intIdentificadorChamado +"))";
                }
                
                objDataReader = ServiceDesk.Banco.ClsBanco.geraDataReader(strSql);

                //Apaga associação anterior.
                ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
                if(objBanco.executaSQL("delete SolucaoProjeto where tabela = 'chamado' and tabela_identificador = " + intIdentificadorChamado + "") == false)
                {
                    strMensagem = "Impossível visualizar soluções associadas ao processo do chamado.";
                    objBanco = null;
                    return;
                }
                objBanco = null;

                //Associa a solução ao chamado.
                while (objDataReader.Read())
                {
                    objSolucaoProjeto.Codigo.Valor = objSolucaoProjeto.GetMaxId().ToString();
                    objSolucaoProjeto.Tabela.Valor = "Chamado";
                    objSolucaoProjeto.CodigoIdentificadorTabela.Valor = intIdentificadorChamado.ToString();
                    objSolucaoProjeto.CodigoProjeto.Valor = objDataReader["projeto_codigo"].ToString().Trim();
                    objSolucaoProjeto.DataInclusao.Valor = DateTime.Now.ToString(ClsParametro.DataInclusao);
                    objSolucaoProjeto.PessoaCodigoInclusao.Valor = ClsUsuario.getCodigoUsuario().ToString();

                    objSolucaoProjeto.insere(out strMensagem);
                }


                //Limpar as variáveis
                objDataReader.Dispose();
                objSolucaoProjeto = null;
                

            }
            catch (Exception ex)
            {
                strMensagem = ex.Message;
                throw ex;
            }
        }
        #endregion
        #endregion
    }
}
