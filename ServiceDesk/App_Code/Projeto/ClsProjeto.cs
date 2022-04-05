/*
- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
  • Classe para manipulação dos registros da tabela de Projeto.
  
  	Data: 29/11/2005
  	Autor: Fernanda Passos
  	Descrição: Esta classe apresenta várias funcionalidades que permite manipular os dados
    da tabela Projeto.
  
  • Alterações
  	Data: 20/02/2006
  	Autor: Fernanda Passos
  	Descrição: Substituição dos métodos, CopiaProjeto, CopiaFilho, CopiaNeto e CopiaBisNeto pelo 
    método único copía projeto.
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
using ServiceDesk.Negocio;

/// <summary>
/// Classe de acesso aos dados da tabela Projeto.
/// </summary>
namespace ServiceDesk.Projeto
{
    /// <summary>
    /// Construtor da classe
    /// </summary>
    public class ClsProjeto
    {
        #region Construtor da classe
        public ClsProjeto()
        {
            alimentaColecaoCampos();
        }
        #endregion

        #region Construtor da classe por parametro
        /// <summary>
        /// Construtor da classe por parametro
        /// </summary>
        /// <param name="intCodigo">Código do Projeto</param> 
         public ClsProjeto(int intCodigo)
        {
          try
          {
            this.alimentaColecaoCampos();
            this.objCodigo.Valor = intCodigo.ToString();
            ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
            objBanco.alimentaColecao(this.objAtributos);
            objBanco = null;
          }
          catch (Exception ex)
          {
            throw ex;
          }
        }
        #endregion

        #region Declarações

        //Coleção de objetos.
        private ServiceDesk.Banco.ClsAtributos objAtributos = new ServiceDesk.Banco.ClsAtributos();
        //Atributos.
        private ServiceDesk.Banco.ClsAtributo objCodigo = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objCodigoSuperior = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objNome = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objDataInicioPrevista = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objDataFimPrevista = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objDataInicioReal = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objDataFimReal = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objDataInclusao = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objDataAlteracao = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objCodigoPessoaInclusor = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objCodigoPessoaAlterador = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objObservacao = new ServiceDesk.Banco.ClsAtributo();
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
        /// Código do a atividade mãe da atividade atual.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo CodigoSuperior
        {
            get { return objCodigoSuperior; }
            set { this.objCodigoSuperior = value; }
        }

        /// <summary>
        /// Nome
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo Nome
        {
            get { return objNome; }
            set { this.objNome = value; }
        }

        /// <summary>
        /// Data de inicio prevista da atividade.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo DataInicioPrevista
        {
            get { return objDataInicioPrevista; }
            set { this.objDataInicioPrevista = value; }
        }

        /// <summary>
        /// Data final prevista da atividade.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo DataFimPrevista
        {
            get { return objDataFimPrevista; }
            set { this.objDataFimPrevista = value; }
        }

        /// <summary>
        /// Data de inicio realizada da atividade.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo DataInicioReal
        {
            get { return objDataInicioReal; }
            set { this.objDataInicioReal = value; }
        }

        /// <summary>
        /// Data fim realizada da atividade.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo DataFimReal
        {
            get { return objDataFimReal; }
            set { this.objDataFimReal = value; }
        }

        /// <summary>
        /// Data da inclusão do registro no banco de dados.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo DataInclusao
        {
            get { return objDataInclusao; }
            set { this.objDataInclusao = value; }
        }

        /// <summary>
        /// Data de alteração do registro no banco de dados.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo DataAlteracao
        {
            get { return objDataAlteracao; }
            set { this.objDataAlteracao = value; }
        }

        /// <summary>
        /// Código da pessoa que incluiu o registro no banco de dados.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo CodigoPessoaInclusor
        {
            get { return objCodigoPessoaInclusor; }
            set { this.objCodigoPessoaInclusor = value; }
        }

        /// <summary>
        /// Código da pessoa que realizou a ultima alteração do registro.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo CodigoPessoaAlterador
        {
            get { return objCodigoPessoaAlterador; }
            set { this.objCodigoPessoaAlterador = value; }
        }

        /// <summary>
        /// Observação
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo Observacao
        {
            get { return objObservacao; }
            set { this.objObservacao = value; }
        }
        #endregion

        #region Metodos

        #region Verifica se projeto esta associado com processo
        /// <summary>
        /// Verifica se projeto esta associado com processo
        /// </summary>
        /// <param name="intTabelaIdentificador">Número inteiro com código do identificador da tabela</param>
        /// <param name="strTabela">String com nome da tabela que representa o processo</param>
        /// <returns>Retorna true ou false. Se esta associado ou não</returns> 
        /// <param name="intProjetoCodigo">Número inteiro com código do projeto</param> 
        public static bool VerificaSeProjetoAssociadoProcesso(string strTabela, int intTabelaIdentificador, int intProjetoCodigo)
        {
            try
            {
                if (strTabela.Trim() == string.Empty || intTabelaIdentificador <= 0 || intProjetoCodigo <= 0) return false;
                string strSql = " tabela = '" + strTabela + "'";
                strSql += " and tabela_identificador = " + intTabelaIdentificador + "";
                strSql += " and projeto_codigo = " + intProjetoCodigo + "";

                ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
                if (objBanco.retornaValorCampo("solucaoprojeto", "solucao_projeto_codigo", strSql) != string.Empty)
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

        #region Copia um projeto
        /// <summary>
        /// Copia um projeto
        /// </summary>
        /// <returns>Retorna true ou false. Se o projeto foi copiado ou não.</returns>
        public int CopiaProjeto(int intCodigoPai, ClsProjeto objProjetoOriginal, out String strMensagem)
        {
            try
            {
                int intRetorno = 0;
                String strMensagemAltera = String.Empty;
                String strSql = String.Empty;
                System.Data.SqlClient.SqlDataReader objDataReader = null;

                strMensagem = String.Empty;

                //Verificando a premissa básica para realização da cópia
                if (objProjetoOriginal.Codigo.Valor != String.Empty)
                {
                    //##################### -- >>> Preenche com dados do projeto a ser inserido  como pai.
                    ClsProjeto objProjeto = new ClsProjeto(Convert.ToInt32(objProjetoOriginal.Codigo.Valor.Trim()));
                    ServiceDesk.Negocio.ClsIdentificador objIdentificador = new ServiceDesk.Negocio.ClsIdentificador();
                    objIdentificador.Tabela.Valor = objProjeto.Atributos.NomeTabela;

                    //-------------->>> Dados essenciais
                    objProjeto.Codigo.Valor = objIdentificador.getProximoValor().ToString();

                    if (intCodigoPai != 0)objProjeto.CodigoSuperior.Valor = intCodigoPai.ToString();else objProjeto.CodigoSuperior.Valor = "0";

                    objProjeto.Nome.Valor = "Cópia de " + objProjetoOriginal.Nome.Valor.Trim() +" realizada no dia " + DateTime.Now  +".";
                    objProjeto.CodigoPessoaInclusor.Valor = ClsUsuario.getCodigoUsuario().ToString();
                    objProjeto.DataInclusao.Valor = DateTime.Now.ToString(ClsParametro.DataInclusao);
                    
                    //Trata as data para para que as mesmas sejam inseridas dentro do padrão.
                    if(objProjetoOriginal.DataInicioPrevista.Valor !=  string.Empty ) objProjeto.DataInicioPrevista.Valor = Convert.ToDateTime(objProjetoOriginal.DataInicioPrevista.Valor).ToString(ClsParametro .DataInclusao);
                    if(objProjetoOriginal.DataInicioReal.Valor != string.Empty ) objProjeto.DataInicioReal.Valor = Convert.ToDateTime(objProjetoOriginal.DataInicioReal.Valor).ToString(ClsParametro.DataInclusao) ;
                    if(objProjetoOriginal.DataFimPrevista.Valor != string.Empty ) objProjeto.DataFimPrevista.Valor = Convert.ToDateTime(objProjetoOriginal.DataFimPrevista.Valor).ToString(ClsParametro.DataInclusao) ;
                    if(objProjetoOriginal.DataFimReal.Valor != string.Empty) objProjeto.DataFimReal.Valor = Convert.ToDateTime(objProjetoOriginal.DataFimReal.Valor).ToString(ClsParametro.DataInclusao);
                    objProjeto.DataAlteracao.Valor = string.Empty ;
                    objProjeto.CodigoPessoaAlterador.Valor = string.Empty;  
  
                    if (objProjeto.insere(out strMensagem))
                    {
                        //Atualizando o valor na tabela identificador
                        objIdentificador.atualizaValor();

                        intRetorno = Convert.ToInt32(objProjeto.Codigo.Valor.Trim());

                        ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
                        strSql = "SELECT projeto_codigo";
                        strSql += " FROM Projeto";
                        strSql += " WHERE projeto_codigo_superior = " + objProjetoOriginal.Codigo.Valor.Trim() + "";

                        objIdentificador = null;
                        objProjeto = null;

                        objDataReader = ServiceDesk.Banco.ClsBanco.geraDataReader(strSql);
                        while (objDataReader.Read())
                        {
                            ClsProjeto objProjetoNovo = new ClsProjeto(Convert.ToInt32(objDataReader["projeto_codigo"].ToString()));
                            CopiaProjeto(intRetorno, objProjetoNovo, out strMensagem);
                            objProjetoNovo = null;
                        }
                    }

                }

                objDataReader.Dispose();
                objDataReader = null;

                return intRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Atualiza código projeto em SolucaoProjeto
        /// <summary>
        /// Atualiza código do projeto da solução.
        /// </summary>
        /// <param name="intCodigoProjeto">Código do projeto que será atualizado</param>
        /// <param name="intCodigoSolucaoProjeto">Código da solução que será atualizada</param>
        /// <returns>Retorna true ou false. Se foi atualizado ou não</returns>
        public bool UpdateCodigoProjetoSolucao(int intCodigoProjeto, int intCodigoSolucaoProjeto)
        {
            try
            {

                string strSql = "UPDATE SolucaoProjeto";
                if (intCodigoProjeto != 0) strSql += " SET projeto_codigo = " + intCodigoProjeto + "";
                if (intCodigoProjeto == 0) strSql += " SET projeto_codigo = Null";
                strSql += " WHERE solucao_projeto_codigo = " + intCodigoSolucaoProjeto + "";

                ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
                if (objBanco.executaSQL(strSql) == true)
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

        #region GeraDropDownList
        /// <summary>
        /// Gera um novo DropDownList de acordo com a coleção de atributos.
        /// </summary>
        /// <param name="objDropDownList">Nome do objeto DropDownList</param>
        /// <param name="strItemDefault">Nome do item que será default ao carregar o DropDownList</param>
        public static void geraDropDownList(System.Web.UI.WebControls.DropDownList objDropDownList, string strItemDefault)
        {
            try
            {
                string strSql = string.Empty;

                strSql = " select nome, projeto_codigo";
                strSql += " from Projeto";
                strSql += " where projeto_codigo_superior is null ";
                strSql += " or projeto_codigo_superior = 0";

                System.Data.SqlClient.SqlDataReader objReader = ServiceDesk.Banco.ClsBanco.geraDataReader(strSql);
                objDropDownList.Items.Clear();
                objDropDownList.Items.Add(strItemDefault);
                while (objReader.Read()) objDropDownList.Items.Add(new ListItem(objReader["nome"].ToString(), objReader["projeto_codigo"].ToString()));
                objReader = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Verifica e retorno o código superior da atividade.

        /// <summary>
        /// Verifica se a ativida é filha de outra atividade.
        /// </summary>
        /// <param name="intCodigoProjeto">Código do projeto ou atividade a ser verificada.</param>
        /// <param name="intCodigoPai">Variavel que receberá o código da atividade pai caso exista.</param>
        /// <returns>Retorna true ou false. Se é filho ou não.</returns>
        public bool VerificaSeFilho(int intCodigoProjeto, out int intCodigoPai)
        {
            try
            {
                intCodigoPai = 0;
                ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
                string strSql = objBanco.retornaValorCampo("Projeto", "projeto_codigo_superior", "projeto_codigo = " + intCodigoProjeto + "");

                if (strSql != string.Empty)
                {
                    if (Convert.ToInt32(strSql) == 0)
                        return false;
                    else
                    {
                        intCodigoPai = Convert.ToInt32(strSql);
                        return true;
                    }
                }
                else
                {
                    intCodigoPai = 0;
                    return false;

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Valida a exclusão
        /// <summary>
        /// Valida exclusão.
        /// </summary>
        /// <param name="strMensagem">Mensagem com informação do motivo da não exclusão.</param>
        /// <returns>Retorna true ou false. Se validado ou não.</returns>
        public bool ValidaExclusao(out String strMensagem)
        {
            try
            {
                strMensagem = string.Empty;

                return true;
            }
            catch (Exception ex)
            {
                strMensagem = "Processo de validação da exclusão abortado. Exclusão cancelada.";
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
                objAtributos.NomeTabela = "Projeto";
                objAtributos.DescricaoTabela = "Tabela que armazena os projetos e suas respectivas atividades.";

                objCodigo.Campo = "projeto_codigo";
                objCodigo.Descricao = "Código identificador do registro na tabela.";
                objCodigo.CampoIdentificador = true;
                objCodigo.CampoObrigatorio = true;
                objCodigo.Tipo = System.Data.DbType.Int32;
                objAtributos.Add(objCodigo);

                objCodigoSuperior.Campo = "projeto_codigo_superior";
                objCodigoSuperior.Descricao = "Código do projeto superior.";
                objCodigoSuperior.CampoIdentificador = false;
                objCodigoSuperior.CampoObrigatorio = true;
                objCodigoSuperior.Tipo = System.Data.DbType.Int32;
                objAtributos.Add(objCodigoSuperior);

                objNome.Campo = "nome";
                objNome.Descricao = "Nome.";
                objNome.CampoIdentificador = false;
                objNome.CampoObrigatorio = true;
                objNome.Tipo = System.Data.DbType.String;
                objNome.Tamanho = 500;
                objAtributos.Add(objNome);

                objDataInicioPrevista.Campo = "data_inicio_prevista";
                objDataInicioPrevista.Descricao = "Data de inicio prevista.";
                objDataInicioPrevista.CampoIdentificador = false;
                objDataInicioPrevista.CampoObrigatorio = false;
                objDataInicioPrevista.Tipo = System.Data.DbType.DateTime;
                objAtributos.Add(objDataInicioPrevista);

                objDataFimPrevista.Campo = "data_fim_prevista";
                objDataFimPrevista.Descricao = "Data fim prevista.";
                objDataFimPrevista.CampoIdentificador = false;
                objDataFimPrevista.CampoObrigatorio = false;
                objDataFimPrevista.Tipo = System.Data.DbType.DateTime;
                objAtributos.Add(objDataFimPrevista);

                objDataInicioReal.Campo = "data_inicio_real";
                objDataInicioReal.Descricao = "Data de início realizada.";
                objDataInicioReal.CampoIdentificador = false;
                objDataInicioReal.CampoObrigatorio = false;
                objDataInicioReal.Tipo = System.Data.DbType.DateTime;
                objAtributos.Add(objDataInicioReal);

                objDataFimReal.Campo = "data_fim_real";
                objDataFimReal.Descricao = "Data fim realizada.";
                objDataFimReal.CampoIdentificador = false;
                objDataFimReal.CampoObrigatorio = false;
                objDataFimReal.Tipo = System.Data.DbType.DateTime;
                objAtributos.Add(objDataFimReal);

                objDataInclusao.Campo = "data_inclusao";
                objDataInclusao.Descricao = "Data da inclusão do registro no banco de dados.";
                objDataInclusao.CampoIdentificador = false;
                objDataInclusao.CampoObrigatorio = true;
                objDataInclusao.Tipo = System.Data.DbType.DateTime;
                objAtributos.Add(objDataInclusao);

                objDataAlteracao.Campo = "data_alteracao";
                objDataAlteracao.Descricao = "Data da última alteração do registro no banco de dados.";
                objDataAlteracao.CampoIdentificador = false;
                objDataAlteracao.CampoObrigatorio = false;
                objDataAlteracao.Tipo = System.Data.DbType.DateTime;
                objAtributos.Add(objDataAlteracao);

                objCodigoPessoaInclusor.Campo = "pessoa_codigo_inclusao";
                objCodigoPessoaInclusor.Descricao = "Código da pessoa que incluiu o registro na tabela.";
                objCodigoPessoaInclusor.CampoIdentificador = false;
                objCodigoPessoaInclusor.CampoObrigatorio = true;
                objCodigoPessoaInclusor.Tipo = System.Data.DbType.Int32;
                objAtributos.Add(objCodigoPessoaInclusor);

                objCodigoPessoaAlterador.Campo = "pessoa_codigo_alteracao";
                objCodigoPessoaAlterador.Descricao = "Código da pessoa que fez a ultima alteração no registro.";
                objCodigoPessoaAlterador.CampoIdentificador = false;
                objCodigoPessoaAlterador.CampoObrigatorio = false;
                objCodigoPessoaAlterador.Tipo = System.Data.DbType.DateTime;
                objAtributos.Add(objCodigoPessoaAlterador);

                objObservacao.Campo = "observacao";
                objObservacao.Descricao = "Observação.";
                objObservacao.CampoIdentificador = false;
                objObservacao.CampoObrigatorio = false;
                objObservacao.Tipo = System.Data.DbType.String;
                objObservacao.Tamanho = 200;
                objAtributos.Add(objObservacao);
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
                    strMsg = "Informe o código.";
                    return false;
                }
                else if (objCodigoSuperior.Valor.Trim() == string.Empty)
                {
                    strMsg = "Informe o código superior.";
                    return false;
                }
                else if (objNome.Valor.Trim() == string.Empty)
                {
                    strMsg = "Informe o nome.";
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

        #region Verifica se já existe o registro no banco de dados.
        /// <summary>
        /// Verifica se já existe o registro no banco de dados.
        /// </summary>
        /// <param name="strMsg">Mensagem que o método retorna com status da operação.</param> 
        /// <returns>Retorna true ou false. Se existe ou não.</returns>
        public bool VerificaSeJaExisteNoBanco(out String strMsg)
        {
            strMsg = string.Empty;
            try
            {
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
        /// <param name="strMensagem">Mensagem com status da execução do método.</param>
        /// <returns>Retorna true ou false. Se o registro foi inserido ou não.</returns>
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
                        strMensagem = "Registro inserido com sucesso.";
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
        /// <returns>Retorna true ou false. Se o registro foi alterado ou não.</returns>
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

                //Valida a exclusão.
                if (ServiceDesk.Negocio.ClsIdentificador.ValidaExclusao(objCodigo.Campo, objCodigo.Valor.Trim(), out strMsg, true, false, objAtributos.NomeTabela.Trim()) == false) return false;


                //Excluir associação das pessoas as atividades.
                ServiceDesk.Projeto.ClsProjetoPessoa objProjetoPessoa = new ClsProjetoPessoa();
                objProjetoPessoa.CodigoProjeto.CampoIdentificador = true;
                objProjetoPessoa.CodigoProjeto.Valor = Codigo.Valor;
                objProjetoPessoa.exclui();

                //Excluir filhos
                if (VerificaSeExisteFilhos(Convert.ToInt32(objCodigo.Valor.Trim())) == true)
                {
                    ExcluiFilhos(Convert.ToInt32(objCodigo.Valor.Trim()));
                }

                ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
                if (objBanco.apagaColecao(objAtributos))
                {
                    strMsg = "Registro excluído com sucesso.";
                    objBanco = null;
                    return true;
                }
                else
                {
                    strMsg = "Registro não excluído.";
                    objBanco = null;
                    return true;
                }
            }
            catch (Exception ex)
            {
                strMsg = ex.Message;
                throw ex;
            }
        }
        #endregion

        #region Excluir filhos
        /// <summary>
        /// Excluir filhos
        /// </summary>
        /// <param name="intCodigoPai">Número inteiro com código do pai</param>
        /// <returns>Retorna true ou false. Se foi excluido ou não.</returns>
        public bool ExcluiFilhos(int intCodigoPai)
        {
            try
            {
                string strSql = "delete from Projeto where projeto_codigo_superior = " + intCodigoPai + "";
                ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
                if (objBanco.executaSQL(strSql) == true)
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

        #region Verifica se tem filhos e retorna a quantidade de filhos
        /// <summary>
        /// Verifica se tem filhos e retorna a quantidade de filhos
        /// </summary>
        /// <param name="intCodigoProjeto">Código do projeto que será verificado os filhos</param>
        /// <param name="intQuantidadeFilhos">Quantidade de filhos que irá apresentar</param>
        /// <returns>Retorna true ou false. Se tem filhos ou não</returns>
        public bool VerificaSeExisteFilhos(int intCodigoProjeto)
        {
            try
            {
                if (intCodigoProjeto <= 0) return false;

                string strSql = "select count(*) ";
                strSql += " from Projeto ";
                strSql += " where projeto_codigo_superior = (select projeto_codigo from Projeto where projeto_codigo = " + intCodigoProjeto + ")";

                System.Data.SqlClient.SqlDataReader objDataReader = ServiceDesk.Banco.ClsBanco.geraDataReader(strSql);
                if (objDataReader.Read())
                {
                    if (Convert.ToInt32(objDataReader[0].ToString()) > 0)
                    {
                        return true;
                    }
                    else
                        return false;
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

        #region GeraGridView
        /// <summary>
        /// Gera uma nova geraGridView de acordo com a coleção de atributos.
        /// </summary>
        /// <param name="objGridView">objeto Grid View</param>
        /// <param name="bolCondicao">Condição para verificar se será utilizado a cláusula Where</param>
        public static void geraGridView(System.Web.UI.WebControls.GridView objGridView, ClsProjeto objProjeto, bool bolCondicao)
        {
            try
            {
                objGridView.AutoGenerateColumns = false;
                ServiceDesk.Controle.ClsGridView.geraGridView(objGridView, objProjeto.Atributos, bolCondicao);
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
                string strSql = "select max(projeto_codigo) as maximo from Projeto";

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
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region GeraGridView
        /// <summary>
        /// Gera gridview com campos especificos.
        /// </summary>
        /// <param name="objGridView">Nome do GridView que receberá os dados.</param>
        public static void geraGridView(System.Web.UI.WebControls.GridView objGridView)
        {
            try
            {
                objGridView.AutoGenerateColumns = false;

                string strSql = "select P.projeto_codigo, P.projeto_codigo_superior, P.nome, ";
                strSql += " P.data_inicio_prevista, P.data_fim_prevista, P.data_inicio_real, ";
                strSql += " P.data_fim_real, P.observacao, PE.nome as responsavel";
                strSql += " from Projeto P, ProjetoPessoa PP, Pessoa PE";
                strSql += " where PP.projeto_codigo = P.projeto_codigo";
                strSql += " and PE.pessoa_codigo = PP.pessoa_codigo";

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

        #region metodo populaNoRaiz
        /// <summary>
        /// Método que popula os nós que não possuem pai
        /// </summary>
        public static void populaNoRaiz(int intCodigoItem, TreeView trvTreeView)
        {
            try
            {
                String strSql = String.Empty;

                strSql = "SELECT projeto_codigo, projeto_codigo_superior, nome, (projeto_codigo + projeto_codigo_superior) ordem";
                strSql += " , (SELECT count(*) FROM Projeto WHERE projeto_codigo_superior = (select projeto_codigo from Projeto where projeto_codigo = " + intCodigoItem + ")) qtdPai";
                strSql += " FROM Projeto";
                strSql += " WHERE projeto_codigo = " + intCodigoItem + "";
                strSql += " ORDER BY ordem";

                System.Data.SqlClient.SqlDataReader objDataReader = ServiceDesk.Banco.ClsBanco.geraDataReader(strSql);

                trvTreeView.Nodes.Clear();

                populaNos(objDataReader, trvTreeView.Nodes, trvTreeView);

                objDataReader = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Popula Nó Raiz
        /// <summary>
        /// Popula Nó Raiz
        /// </summary>
        /// <param name="intCodigoPai" >Número inteiro do código do pai do item.</param>
        /// <param name="objTreeNode"></param> 
        /// <param name="objTreeView">Nome da treeview</param> 
        public static void PopulaNoz(int intCodigoPai, TreeView objTreeView, TreeNode objTreeNode, bool bolIsNoRaiz)
        {
            try
            {
                String strSql = String.Empty;

                strSql = "select P.projeto_codigo, P.projeto_codigo_superior, P.nome, (select count(*) from Projeto where projeto_codigo_superior = P.projeto_codigo) qtdPai";
                strSql += " from Projeto P";

                if (intCodigoPai > 0)
                {
                    strSql += " where projeto_codigo_superior = " + intCodigoPai + "";
                }
                else
                {
                    strSql += " WHERE projeto_codigo_superior is null or projeto_codigo_superior = 0";
                }

                System.Data.SqlClient.SqlDataReader objDataReader = ServiceDesk.Banco.ClsBanco.geraDataReader(strSql);

                if (bolIsNoRaiz == true)
                {
                    if (objTreeView != null)
                    {
                        objTreeView.Nodes.Clear();
                        ClsProjeto.populaNos(objDataReader, objTreeView.Nodes, null);
                    }
                    else if (objTreeNode != null)
                    {
                        ClsProjeto.populaNos(objDataReader, objTreeNode.ChildNodes, null);
                    }
                }
                else
                {
                    if (objTreeNode != null)
                    {
                        ClsProjeto.populaNos(objDataReader, objTreeNode.ChildNodes, null);
                    }

                }
                objDataReader = null;
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
                    //objTreeNode.Text = "<a href=\"SolucaoProjeto.aspx?PostBack= true &&codigo_item_projeto=" + objDataReader["projeto_codigo"].ToString() + "\">" + objDataReader["nome"].ToString() + "</a>";
                    objTreeNode.Text = objDataReader["nome"].ToString();
                    objTreeNode.Value = objDataReader["projeto_codigo"].ToString();
                    objTreeNode.NavigateUrl = "";
                    if (Convert.ToInt32(objDataReader["qtdPai"].ToString()) > 0)
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

        #region Retorna DataReader com dados do projeto.
        /// <summary>
        /// Retorna dados do projeto.
        /// </summary>
        /// <param name="intCodigo">Código do registro na tabela de projeto</param>
        /// <returns>Retorna um data reader.</returns>
        public System.Data.SqlClient.SqlDataReader GetDadosProjetoPorParamatro(int intCodigo)
        {
            try
            {
                string strSql = string.Empty;

                strSql = " select P.*,";
                strSql += " (select pessoa_codigo from ProjetoPessoa where projeto_codigo = P.projeto_codigo)pessoa_codigo";
                //strSql += " (select tabela from SolucaoProjeto where projeto_codigo = P.projeto_codigo)tabela, ";
                //strSql += " (select tabela_identificador from SolucaoProjeto where projeto_codigo = P.projeto_codigo) tabela_identificador, ";
                //strSql += " (select solucao_projeto_codigo from SolucaoProjeto where projeto_codigo = P.projeto_codigo)solucao_projeto_codigo ";
                strSql += " from Projeto P";
                strSql += " where P.projeto_codigo = " + intCodigo + "";

                System.Data.SqlClient.SqlDataReader objReader = ServiceDesk.Banco.ClsBanco.geraDataReader(strSql);

                return objReader;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region VerificaSeFinalizado
        /// <summary>
        /// Verifica se esta finalizaod
        /// </summary>
        /// <param name="strCodigoProjeto">Código do projeto</param>
        /// <returns>Retorna true ou false. Se finalizado ou não</returns>
        public static bool VerificaSeFinalizado(string strCodigoProjeto)
        {
            if (strCodigoProjeto == string.Empty) return false;
            ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
            string strValor = objBanco.retornaValorCampo("Projeto", "data_fim_real", "projeto_codigo = "+ Convert.ToInt32(strCodigoProjeto.Trim()) +"");
            if (strValor == string.Empty) return false;
            else return true;
        }
        #endregion

        #region Finaliza projeto
        /// <summary>
        /// Finaliza Projeto
        /// </summary>
        /// <param name="strCodigoProjeto">Código do projeto</param>
        /// <returns>Retorna true ou false. Se finalizado ou não</returns>
        public static bool FinalizaProjeto(string strCodigoProjeto)
        {
            if (strCodigoProjeto == string.Empty) return false;

            ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();

            string strFormatoDataInclusao = ClsParametro.DataInclusao;
            if (objBanco.executaSQL("UPDATE Projeto SET data_fim_real = '" + DateTime.Now.ToString(strFormatoDataInclusao) + "' WHERE projeto_codigo = " + Convert.ToInt32(strCodigoProjeto.Trim()) + "") == true) return true;
            else return false;

        }
        #endregion
        
       #endregion
    }
}