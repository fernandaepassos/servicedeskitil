/*
- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
  � Classe de acesso a dados da tabela de diagn�stico.
  
  	Data: 19/12/2005
  	Autor: Fernanda Passos
  	Descri��o: Classe que apresenta funcionalidades que permite os acessos necess�rios a tabela de
    dign�stico.
  
  
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

namespace ServiceDesk.Negocio
{
    /// <summary>
    /// Class de manipula��o de dados do objeto Diagnostico
    /// </summary>
    public class ClsDiagnostico
    {
        #region Construtor da classe
        public ClsDiagnostico()
        {
            alimentaColecaoCampos();
        }
        #endregion

        #region Declara��es
        //Cole��o de objetos.
        private ServiceDesk.Banco.ClsAtributos objAtributos = new ServiceDesk.Banco.ClsAtributos();

        //Atributos de um problema.
        private ServiceDesk.Banco.ClsAtributo objCodigo = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objCodigoProjeto = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objTabela = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objTabelaIdentificador = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objDataInclusao = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objDataAlteracao = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objCodigoPessoaInclusor = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objCodigoPessoaAlterador = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objNome = new ServiceDesk.Banco.ClsAtributo();
    
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
        /// C�digo do diagn�stico
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo Codigo
        {
            get { return objCodigo ; }
            set { this.objCodigo = value; }
        }

        /// <summary>
        /// C�digo do projeto.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo CodigoProjeto
        {
            get { return objCodigoProjeto; }
            set { this.objCodigoProjeto = value; }
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
        /// C�digo do registro identificador da tabela.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo TabelaIdentificador
        {
            get { return objTabelaIdentificador; }
            set { this.objTabelaIdentificador = value; }
        }

        /// <summary>
        /// Data da inclus�o do registro.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo DataInclusao
        {
            get { return objDataInclusao; }
            set { this.objDataInclusao = value; }
        }

        /// <summary>
        /// Data de altera��o.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo DataAlteracao
        {
            get { return objDataAlteracao; }
            set { this.objDataAlteracao = value; }
        }

        /// <summary>
        /// C�digo do usu�rio que cadastrou.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo CodigoPessoaInclusor
        {
            get { return objCodigoPessoaInclusor; }
            set { this.objCodigoPessoaInclusor = value; }
        }

        /// <summary>
        /// C�digo do usu�rio que alterou.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo CodigoPessoaAlterador
        {
            get { return objCodigoPessoaAlterador; }
            set { this.objCodigoPessoaAlterador = value; }
        }

        /// <summary>
        /// Nome do diagn�stico.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo Nome
        {
            get { return objNome; }
            set { this.objNome = value; }
        }
        #endregion

        #region Metodos

        #region Validacao dos dados
      
        /// <summary>
        /// Valida��o dos dados.
        /// </summary>
        /// <returns>Retorna true ou false. Se for validado ou n�o.</returns>
        public bool ValidacaoDados(out String strMsg)
        {
            try
            {
                strMsg = String.Empty;

                if (objCodigo.Valor.Trim() == string.Empty)
                {
                    strMsg = "Informe o c�digo do diagn�stico.";
                    return false;
                }
                else if (objCodigoProjeto.Valor.Trim() == string.Empty)
                {
                    strMsg = "Informe o c�digo do projeto\atividade superior.";
                    return false;
                }
                else if (objTabela.Valor.Trim() == string.Empty)
                {
                    strMsg = "Informe o nome da tabela que representa o processo.";
                    return false;
                }
                else if (objTabelaIdentificador.Valor.Trim() == string.Empty)
                {
                    strMsg = "Informe o c�digo do registro identificador dentro da tabela que representa o processo.";
                    return false;
                }
                else if (objNome.Valor.Trim() == string.Empty)
                {
                    strMsg = "Informe o nome do diagn�stico.";
                    return false;
                }

                return true;            
            }
            catch(System .Exception ex)
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

                ///>>Verifica se pode excluir o projeto
                ServiceDesk.Projeto.ClsProjeto objProjeto = new ServiceDesk.Projeto.ClsProjeto();
                if (objProjeto.ValidaExclusao(out strMsg) == false)
                    return false;

                return true;
            }
            catch (Exception ex)
            {
                strMsg = ex.Message;
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
                bool bolRetorno = false;

                //Verifica se todos os campos foram preenchidos.
                if (ValidacaoDados(out strMensagem) == false)
                {
                    bolRetorno = false;
                }
                else
                {
                    ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
                    //Insere no banco de dados.
                    if (objBanco.insereColecao(this.objAtributos))
                    {
                        strMensagem = "Diagn�stico inserido com sucesso";
                        bolRetorno = true;
                    }
                    objBanco = null;
                }

                return bolRetorno;            
            }
            catch(System .Exception ex)
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
        /// <returns>Retorna true ou false. Se o registro foi alterado ou n�o.</returns>
        public bool altera(out String strMensagem)
        {
            try
            {
                strMensagem = String.Empty;
                bool bolRetorno = false;

                //Verifica se todos os campos foram informados.
                if (ValidacaoDados(out strMensagem) == false)
                {
                    bolRetorno = false;
                }
                //Verifica se o nome do problema j� existe no banco para outro registro.
                else if (VerificaSeJaExisteNoBanco(out strMensagem) == true)
                {
                    bolRetorno = false;
                }
                else
                {
                    ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
                    //Altera no banco de dados.
                    if (objBanco.alteraColecao(this.objAtributos))
                    {
                        strMensagem = "Altera��o realizada com sucesso";
                        bolRetorno = true;
                    }
                    objBanco = null;
                }

                return bolRetorno;            
            }
            catch(System .Exception ex)
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
                //if (ServiceDesk.Negocio.ClsIdentificador.ValidaExclusao(objCodigo.Campo, objCodigo.Valor.Trim(), out strMsg, true, false, objAtributos.NomeTabela.Trim()) == false) return false;

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
            catch(System .Exception ex)
            {
                strMsg = ex.Message;
                throw ex;
            }
        }
        #endregion
        
        #region Alimenta campos cole��o
        /// <summary>
        /// Alimenta campos cole��o
        /// </summary>
        private void alimentaColecaoCampos()
        {
            try
            {
                objAtributos.NomeTabela = "Diagnostico";
                objAtributos.DescricaoTabela = "Tabela de diagn�stico.";

                objCodigo.Campo = "diagnostico_codigo";
                objCodigo.Descricao = "C�digo do registro identificador do diagn�stico.";
                objCodigo.CampoIdentificador = true;
                objCodigo.CampoObrigatorio = true;
                objCodigo.Tipo = System.Data.DbType.Int32;
                objAtributos.Add(objCodigo);

                objNome.Campo = "nome";
                objNome.Descricao = "Nome do diagn�stico";
                objNome.CampoIdentificador = false;
                objNome.CampoObrigatorio = true;
                objNome.Tipo = System.Data.DbType.String;
                objNome.Tamanho = 500;
                objAtributos.Add(objNome);

                objCodigoProjeto.Campo = "projeto_codigo";
                objCodigoProjeto.Descricao = "C�digo do projeto.";
                objCodigoProjeto.CampoIdentificador = false;
                objCodigoProjeto.CampoObrigatorio = true;
                objCodigoProjeto.Tipo = System.Data.DbType.Int32;
                objAtributos.Add(objCodigoProjeto);

                objTabela.Campo = "tabela";
                objTabela.Descricao = "Nome da tabela que armazena os dados do processo que diagn�stica.";
                objTabela.CampoIdentificador = false;
                objTabela.CampoObrigatorio = true;
                objTabela.Tipo = System.Data.DbType.String;
                objTabela.Tamanho = 100;
                objAtributos.Add(objTabela);

                objTabelaIdentificador.Campo = "tabela_identificador";
                objTabelaIdentificador.Descricao = "C�digo identificador do registro da tabela no banco de dados.";
                objTabelaIdentificador.CampoIdentificador = false;
                objTabelaIdentificador.CampoObrigatorio = true;
                objTabelaIdentificador.Tipo = System.Data.DbType.Int32;
                objAtributos.Add(objTabelaIdentificador);

                objDataInclusao.Campo = "data_inclusao";
                objDataInclusao.Descricao = "Data da inclus�o do registro no banco de dados.";
                objDataInclusao.CampoIdentificador = false;
                objDataInclusao.CampoObrigatorio = true;
                objDataInclusao.Tipo = System.Data.DbType.DateTime;
                objAtributos.Add(objDataInclusao);

                objDataAlteracao.Campo = "data_alteracao";
                objDataAlteracao.Descricao = "Data da �ltima altera��o do registro no banco de dados.";
                objDataAlteracao.CampoIdentificador = false;
                objDataAlteracao.CampoObrigatorio = false;
                objDataAlteracao.Tipo = System.Data.DbType.DateTime;
                objAtributos.Add(objDataAlteracao);

                objCodigoPessoaInclusor.Campo = "pessoa_codigo_inclusao";
                objCodigoPessoaInclusor.Descricao = "C�digo da pessoa que incluiu o registro no banco de dados.";
                objCodigoPessoaInclusor.CampoIdentificador = false;
                objCodigoPessoaInclusor.CampoObrigatorio = true;
                objCodigoPessoaInclusor.Tipo = System.Data.DbType.Int32;
                objAtributos.Add(objCodigoPessoaInclusor);

                objCodigoPessoaAlterador.Campo = "pessoa_codigo_alteracao";
                objCodigoPessoaAlterador.Descricao = "C�digo da pessoa que realizou a �ltima altera��o.";
                objCodigoPessoaAlterador.CampoIdentificador = false;
                objCodigoPessoaAlterador.CampoObrigatorio = false;
                objCodigoPessoaAlterador.Tipo = System.Data.DbType.Int32;
                objAtributos.Add(objCodigoPessoaAlterador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region GeraDropDownList
        /// <summary>
        /// Gera um novo DropDownList de acordo com a cole��o de atributos.
        /// </summary>
        /// <param name="objDropDownList">DropDownList</param>
        public static void geraDropDownList(System.Web.UI.WebControls.DropDownList objDropDownList, string strItemDefault)
        {
            try
            {
                ClsDiagnostico objDiagnostico = new ClsDiagnostico();
                objDropDownList.DataTextField = objDiagnostico.objNome.Campo;
                objDropDownList.DataValueField = objDiagnostico.objCodigo.Campo;
                ServiceDesk.Controle.ClsDropDownList.geraDropDownList(objDropDownList, objDiagnostico.objAtributos);
                objDiagnostico = null;

                //Adiciona a op��o default no dropdownlist.
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

        #region GeraGridView
        /// <summary>
        /// Gera uma nova geraGridView de acordo com a cole��o de atributos.
        /// </summary>
        /// <param name="objGridView">objeto Grid View</param>
        /// <param name="bolCondicao">Condi��o para verificar se ser� utilizado a cl�usula Where</param>
        public static void geraGridView(System.Web.UI.WebControls.GridView objGridView, ClsDiagnostico objDiagnostico, bool bolCondicao)
        {
            try
            {
                objGridView.AutoGenerateColumns = false;
                ServiceDesk.Controle.ClsGridView.geraGridView(objGridView, objDiagnostico.objAtributos, bolCondicao);
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
        /// <param name="objGridView">geraGridView</param>
        public static void geraGridView(System.Web.UI.WebControls.GridView objGridView)
        {
            try
            {
                objGridView.AutoGenerateColumns = false;

                string strSql = "select D.diagnostico_codigo, D.nome, P.nome as NomeProjeto, P.data_inicio_prevista, P.data_fim_prevista, P.data_inicio_real, P.data_fim_real";
                strSql += " from Diagnostico D, Projeto P, ProjetoPessoa PP";
                strSql += " where D.projeto_codigo = P.projeto_codigo";
                strSql += " and PP.projeto_codigo = P.projeto_codigo";
                ServiceDesk.Negocio.ClsDiagnostico objDiagnostico = new ClsDiagnostico();
                strSql += " and D.diagnostico_codigo = " + Convert.ToInt32(objDiagnostico.objCodigo.Valor.Trim()) + "";

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

        #region Pega o pr�ximo c�digo identificador da tabela
        /// <summary>
        /// Pega o pr�ximo registro identificador da tabela.
        /// </summary>
        /// <returns>Retorna n�mero inteiro do pr�ximo identificador ou zero se houver erro.</returns>
        public int GetMaxId()
        {
            try
            {
                string strSql = "select max(diagnostico_codigo) as maximo from Diagnostico";

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
            catch
            {
                return 0;
            }

        }
        #endregion

        #region Verifica se dados j� existem
        /// <summary>
        /// Verifica se j� existe com o mesmo nome para registro diferente.
        /// </summary>
        /// <param name="strNome"></param>
        /// <returns>Retorna true ou false. Se existe ou n�o.</returns>
        public bool VerificaSeJaExisteNoBanco(out String strMensagem)
        {
            try
            {
                strMensagem = String.Empty;

                ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
                string strValorCampo = string.Empty;

                strValorCampo = objBanco.retornaValorCampo("Diagnostico", "nome", " diagnostico_codigo <> " + Convert.ToInt32(objCodigo.Valor.Trim()) + " and nome = '" + objNome.Valor.Trim() + "'");

                if (strValorCampo != string.Empty)
                {
                    strMensagem = "J� existe um diagn�stico com o nome informado.";
                    return true;
                }

                return false;

            }
            catch (System.Exception ex)
            {
                strMensagem = ex.Message;
                throw ex;
            }
        }
        #endregion
        #endregion
    }
}