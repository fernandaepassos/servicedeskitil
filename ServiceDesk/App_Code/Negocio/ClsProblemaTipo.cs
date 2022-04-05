/*
- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
  � Classe de acesso a dados da tabela Tipo de Problema.
  
  	Data: 05/12/2005
  	Autor: Fernanda Passos
  	Descri��o: Esta  classe apresenta m�todos e propriedades que permitem acessar dados na
    tabela tipo de problema a qual classifica o problema inicialmente em erro, erro conhecido
    ou problema.
  
  
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
/// Classe de acesso a dados da tabela ProblemaTipo.
/// </summary>
namespace ServiceDesk.Negocio
{
    /// <summary>
    /// Construtor da classe
    /// </summary>
    public class ClsProblemaTipo
    {
        #region Construtor da classe
        public ClsProblemaTipo()
        {
            alimentaColecaoCampos();
        }
        #endregion

        #region Declara��es

        //Cole��o de objetos.
        private ServiceDesk.Banco.ClsAtributos objAtributos = new ServiceDesk.Banco.ClsAtributos();

        //Atributos.
        private ServiceDesk.Banco.ClsAtributo objCodigo = new ServiceDesk.Banco.ClsAtributo();
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
        /// Codigo do tipo de problema.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo Codigo
        {
            get { return objCodigo; }
            set { this.objCodigo = value; }
        }

        /// <summary>
        /// Nome do tipo de problema.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo Nome
        {
            get { return objNome; }
            set { this.objNome = value; }
        }


        #endregion

        #region Metodos
        #region Pega o pr�ximo c�digo identificador da tabela
        /// <summary>
        /// Pega o pr�ximo registro identificador da tabela.
        /// </summary>
        /// <returns>Retorna n�mero inteiro do pr�ximo identificador ou zero se houver erro.</returns>
        public int GetMaxId()
        {
            try
            {
                string strSql = "select max(problema_tipo_codigo) as maximo from ProblemaTipo";

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
                        return Convert.ToInt32(objDtReader["maximo"].ToString()) + 1;
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

        #region Validacao dos dados
        /// <summary>
        /// Valida��o da integridade dos registros.
        /// </summary>
        /// <returns>Retorna true ou false. Se for validado ou n�o.</returns>
        public bool ValidacaoDadosProblemaTipo(out String strMsg)
        {
            try
            {
                strMsg = String.Empty;
                // bool bolValidacao = true;

                if (objNome.Valor.Trim() == string.Empty)
                {

                    strMsg = "Informe o nome do tipo do problema.";
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

        #region Verifica se o tipo esta associado ao problema.
        /// <summary>
        /// Verifica se o tipo esta associado ao problema.
        /// </summary>
        /// <returns>Retorna true ou false. Se associado ou n�o.<returns>
        public bool VerificaSeAssociadoAoProblema(out String strMsg)
        {
            try
            {
                strMsg = String.Empty;

                ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
                string strValorCampo = string.Empty;
                strValorCampo = objBanco.retornaValorCampo("Problema", "nome", "problema_tipo_codigo = " + Convert.ToInt32(objCodigo.Valor.Trim()) + "");
                objBanco = null;
                if (strValorCampo != string.Empty)
                {
                    strMsg = "Existem problemas cadastrados com o tipo que deseja excluir. A��o cancelada.";
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                strMsg = ex.Message;
                throw ex;
            }
        }
        #endregion

        #region Verifica se j� existe tipo de problema com o nome informado.
        /// <summary>
        /// Verifica se j� existe tipo de problema com o nome informado.
        /// </summary>
        /// <param name="strNome"></param>
        /// <returns>Retorna true ou false. Se associado ou n�o.</returns>
        public bool VerificaSeJaExisteNoBanco(out String strMensagem)
        {
            try
            {
                strMensagem = string.Empty;

                //Busca no banco de dados pelo nome.
                ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
                string strSqlCriterio = " nome = '" + objNome.Valor.Trim() + "' and problema_tipo_codigo <> " + Convert.ToInt32(objCodigo.Valor.Trim()) + "";
                string strValor = objBanco.retornaValorCampo("ProblemaTipo", "nome", strSqlCriterio);
                objBanco = null;
                //Verifica se o nome foi encontrado.
                if (strValor.Trim() == string.Empty)
                    return false; //N�o existe
                else
                {
                    strMensagem = "J� existe um tipo de problema com o nome informado. A��o cancelada.";
                    return true; //Existe
                }
            }
            catch (Exception ex)
            {
                strMensagem = ex.Message;
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

                if (ValidacaoDadosProblemaTipo(out strMensagem) == false)
                {
                    return false;
                }
                else
                {
                    ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
                    //Verifica se j� esta cadastrado no banco.
                    if (VerificaSeJaExisteNoBanco(out strMensagem) == true)
                    {
                        objBanco = null;
                        return false;
                    }
                    else if (objBanco.insereColecao(this.objAtributos))
                    {
                        objBanco = null;
                        strMensagem = "Tipo de Problema inserido com sucesso";
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
        /// <returns>Retorna true ou false. Se o registro foi alterado ou n�o.</returns>
        public bool altera(out String strMensagem)
        {

            try
            {
                strMensagem = String.Empty;
                bool bolRetorno = false;

                if (ValidacaoDadosProblemaTipo(out strMensagem) == false)
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
                    else if (objBanco.alteraColecao(this.objAtributos))
                    {
                        strMensagem = "Tipo de problema alterado com sucesso.";
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
                if (ServiceDesk.Negocio.ClsIdentificador.ValidaExclusao(objCodigo.Campo, objCodigo.Valor.Trim(), out strMsg, true, false, objAtributos.NomeTabela.Trim() ) == false) return false;

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

        #region Alimenta campos cole��o
        /// <summary>
        /// Alimenta campos cole��o
        /// </summary>
        private void alimentaColecaoCampos()
        {
            try
            {
                objAtributos.NomeTabela = "ProblemaTipo";
                objAtributos.DescricaoTabela = "Tabela de tipos de problema";

                objCodigo.Campo = "problema_tipo_codigo";
                objCodigo.Descricao = "C�digo do tipo do problema";
                objCodigo.CampoIdentificador = true;
                objCodigo.CampoObrigatorio = true;
                objCodigo.Tipo = System.Data.DbType.Int32;
                objAtributos.Add(objCodigo);

                objNome.Campo = "nome";
                objNome.Descricao = "Nome do tipo do problema";
                objNome.CampoIdentificador = false;
                objNome.CampoObrigatorio = true;
                objNome.Tamanho = 50; 
                objNome.Tipo = System.Data.DbType.String;
                objAtributos.Add(objNome);
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
                ClsProblemaTipo objProblemaTipo = new ClsProblemaTipo();
                objDropDownList.DataTextField = objProblemaTipo.objNome.Campo;
                objDropDownList.DataValueField = objProblemaTipo.objCodigo.Campo;
                ServiceDesk.Controle.ClsDropDownList.geraDropDownList(objDropDownList, objProblemaTipo.objAtributos);
                objProblemaTipo = null;

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
        /// <param name="objGridView">objeto gridview</param>
        public static void geraGridView(System.Web.UI.WebControls.GridView objGridView)
        {
            try
            {
                objGridView.AutoGenerateColumns = false;
                ClsProblemaTipo objProblemaTipo = new ClsProblemaTipo();
                ServiceDesk.Controle.ClsGridView.geraGridView(objGridView, objProblemaTipo.objAtributos);
                objProblemaTipo = null;
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
        public static void geraGridView(System.Web.UI.WebControls.GridView objGridView, ClsProblemaTipo objProblemaTipo, bool bolCondicao)
        {
            try
            {
                objGridView.AutoGenerateColumns = false;
                ServiceDesk.Controle.ClsGridView.geraGridView(objGridView, objProblemaTipo.objAtributos, bolCondicao);
                objProblemaTipo = null;
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
