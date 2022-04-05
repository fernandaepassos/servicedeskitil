/*
- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
  • Classe de todos os tipos que podem haver no sistema.
  
  	Data: 09/03/2006
  	Autor: Fernanda Passos
  	Descrição: Classe de todos os tipos que podem haver no sistema.
  
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
        /// <param name="intCodigo">Código do Projeto</param> 
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

        #region Declarações

        //Coleção de objetos.
        private ServiceDesk.Banco.ClsAtributos objAtributos = new ServiceDesk.Banco.ClsAtributos();
        //Atributos.
        private ServiceDesk.Banco.ClsAtributo objCodigo = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objDescricao= new ServiceDesk.Banco.ClsAtributo();
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
        /// Descrição\Nome do tipo
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
        /// <param name="intTabelaIdentificador">Número inteiro com código do identificador da tabela</param>
        /// <param name="strTabela">String com nome da tabela que representa o processo</param>
        /// <returns>Retorna true ou false. Se esta associado ou não</returns> 
        /// <param name="intProjetoCodigo">Número inteiro com código do projeto</param> 
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
                objAtributos.NomeTabela = "Tipo";
                objAtributos.DescricaoTabela = "Tabela que armazena todos os tipos que podem haver no sistema.";

                objCodigo.Campo = "tipo_codigo";
                objCodigo.Descricao = "Código identificador do registro na tabela.";
                objCodigo.CampoIdentificador = true;
                objCodigo.CampoObrigatorio = true;
                objCodigo.Tipo = System.Data.DbType.Int32;
                objAtributos.Add(objCodigo);

                objDescricao.Campo = "descricao";
                objDescricao.Descricao = "Descrição, Nome do tipo.";
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
                else if (objDescricao.Valor.Trim() == string.Empty)
                {
                    strMsg = "Informe a descrição.";
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

        #region GeraDropDownList
        /// <summary>
        /// Gera um novo DropDownList de acordo com a coleção de atributos.
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

                //Adiciona a opção default no dropdownlist.
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