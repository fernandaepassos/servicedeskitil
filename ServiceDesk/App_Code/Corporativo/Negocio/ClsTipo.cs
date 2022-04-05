/*
- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
  � Classe de todos os tipos que podem haver no sistema.
  
  	Data: 09/03/2006
  	Autor: Fernanda Passos
  	Descri��o: Classe de todos os tipos que podem haver no sistema.
  
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
using ServiceDesk.Negocio;

/// <summary>
/// Classe de todos os tipos que podem haver no sistema.
/// </summary>
namespace SServiceDesk.Negocio
{
    /// <summary>
    /// Construtor da classe
    /// </summary>
    public class ClsTipo
    {
        #region Construtor da classe
        public ClsTipo()
        {
            alimentaColecaoCampos();
        }
        #endregion

        #region Construtor da classe por parametro
        /// <summary>
        /// Construtor da classe por parametro
        /// </summary>
        /// <param name="intCodigo">C�digo do Projeto</param> 
        public ClsTipo(int intCodigo)
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

        #region Declara��es

        //Cole��o de objetos.
        private ServiceDesk.Banco.ClsAtributos objAtributos = new ServiceDesk.Banco.ClsAtributos();
        //Atributos.
        private ServiceDesk.Banco.ClsAtributo objCodigo = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objDescricao= new ServiceDesk.Banco.ClsAtributo();
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
        /// Codigo do registro da tabela.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo Codigo
        {
            get { return objCodigo; }
            set { this.objCodigo = value; }
        }

        /// <summary>
        /// Descri��o\Nome do tipo
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo Descricao
        {
            get { return objDescricao; }
            set { this.objDescricao = value; }
        }

        #endregion

        #region Metodos

        #region Verifica se projeto esta associado com processo
        /// <summary>
        /// Verifica se projeto esta associado com processo
        /// </summary>
        /// <param name="intTabelaIdentificador">N�mero inteiro com c�digo do identificador da tabela</param>
        /// <param name="strTabela">String com nome da tabela que representa o processo</param>
        /// <returns>Retorna true ou false. Se esta associado ou n�o</returns> 
        /// <param name="intProjetoCodigo">N�mero inteiro com c�digo do projeto</param> 
        public static bool VerificaSeProjetoAssociadoProcesso(string strTabela, int intTabelaIdentificador, int intProjetoCodigo)
        {
            try
            {
                //if (strTabela.Trim() == string.Empty || intTabelaIdentificador <= 0 || intProjetoCodigo <= 0) return false;
                //string strSql = " tabela = '" + strTabela + "'";
                //strSql += " and tabela_identificador = " + intTabelaIdentificador + "";
                //strSql += " and projeto_codigo = " + intProjetoCodigo + "";

                //ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
                //if (objBanco.retornaValorCampo("solucaoprojeto", "solucao_projeto_codigo", strSql) != string.Empty)
                //{
                //    objBanco = null;
                //    return true;
                //}
                //else
                //{
                //    objBanco = null;
                    return false;
                //}
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion

        #region Valida a exclus�o
        /// <summary>
        /// Valida exclus�o.
        /// </summary>
        /// <param name="strMensagem">Mensagem com informa��o do motivo da n�o exclus�o.</param>
        /// <returns>Retorna true ou false. Se validado ou n�o.</returns>
        public bool ValidaExclusao(out String strMensagem)
        {
            try
            {
                strMensagem = string.Empty;

                return true;
            }
            catch (Exception ex)
            {
                strMensagem = "Processo de valida��o da exclus�o abortado. Exclus�o cancelada.";
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
                objAtributos.NomeTabela = "Tipo";
                objAtributos.DescricaoTabela = "Tabela que armazena todos os tipos que podem haver no sistema.";

                objCodigo.Campo = "tipo_codigo";
                objCodigo.Descricao = "C�digo identificador do registro na tabela.";
                objCodigo.CampoIdentificador = true;
                objCodigo.CampoObrigatorio = true;
                objCodigo.Tipo = System.Data.DbType.Int32;
                objAtributos.Add(objCodigo);

                objDescricao.Campo = "descricao";
                objDescricao.Descricao = "Descri��o, Nome do tipo.";
                objDescricao.CampoIdentificador = false;
                objDescricao.CampoObrigatorio = true;
                objDescricao.Tipo = System.Data.DbType.String;
                objDescricao.Tamanho = 100; 
                objAtributos.Add(objDescricao);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Validacao dos dados
        /// <summary>
        /// Valida��o da integridade dos registros.
        /// </summary>
        /// <returns>Retorna true ou false. Se for validado ou n�o.</returns>
        public bool ValidacaoDados(out String strMsg)
        {
            try
            {
                strMsg = String.Empty;

                if (objCodigo.Valor.Trim() == string.Empty)
                {
                    strMsg = "Informe o c�digo.";
                    return false;
                }
                else if (objDescricao.Valor.Trim() == string.Empty)
                {
                    strMsg = "Informe a descri��o.";
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

        #region Verifica se j� existe o registro no banco de dados.
        /// <summary>
        /// Verifica se j� existe o registro no banco de dados.
        /// </summary>
        /// <param name="strMsg">Mensagem que o m�todo retorna com status da opera��o.</param> 
        /// <returns>Retorna true ou false. Se existe ou n�o.</returns>
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
        /// <param name="strMensagem">Mensagem com status da execu��o do m�todo.</param>
        /// <returns>Retorna true ou false. Se o registro foi inserido ou n�o.</returns>
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
        /// <returns>Retorna true ou false. Se o registro foi alterado ou n�o.</returns>
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
        /// <returns>Retorna true ou false. Se o registro foi excluido ou n�o.</returns>
        public bool exclui(out String strMsg)
        {
            try
            {
                strMsg = string.Empty;

                //Valida a exclus�o.
                if (ServiceDesk.Negocio.ClsIdentificador.ValidaExclusao(objCodigo.Campo, objCodigo.Valor.Trim(), out strMsg, true, false, objAtributos.NomeTabela.Trim()) == false) return false;

                ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
                if (objBanco.apagaColecao(objAtributos))
                {
                    strMsg = "Registro exclu�do com sucesso.";
                    objBanco = null;
                    return true;
                }
                else
                {
                    strMsg = "Registro n�o exclu�do.";
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

        #region GeraDropDownList
        /// <summary>
        /// Gera um novo DropDownList de acordo com a cole��o de atributos.
        /// </summary>
        /// <param name="objDropDownList">DropDownList</param>
        public static void geraDropDownList(System.Web.UI.WebControls.DropDownList objDropDownList, string strItemDefault)
        {
            try
            {
                String strSql = String.Empty;
                ClsTipo objTipo = new ClsTipo();
  
                objDropDownList.DataTextField = objTipo.objDescricao.Campo;
                objDropDownList.DataValueField = objTipo.objCodigo.Campo;

                ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
                strSql = objBanco.montaQuery(objTipo.Atributos, false);
                strSql += " ORDER BY descricao";
                System.Data.SqlClient.SqlDataReader objDataReader = ServiceDesk.Banco.ClsBanco.geraDataReader(strSql);
                objDropDownList.DataSource = objDataReader;
                objDropDownList.DataBind();
                objDataReader.Dispose();
                objDataReader = null;
                objBanco = null;
                objTipo = null;

                //Adiciona a op��o default no dropdownlist.
                ListItem itemDefault = new ListItem();
                itemDefault.Text = strItemDefault;
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


        #endregion
    }
}