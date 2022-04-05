/*
- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
  � Classe que permite a manipula��o dos registro da tabela ConhecimentoPerfil.
  
  	Data: 28/12/2005
  	Autor: Fernanda Passos
  	Descri��o: Atrav�s de m�todos e fun��es a classe oferece recursos diversos para manipula��o dos
    registros e intera��o com controles e objetos voltados para interface com usu�rio.
  
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
/// Classe de acesso a dados da tabela ConhecimentoPerfil.
/// </summary>
namespace ServiceDesk.Negocio
{
    /// <summary>
    /// Construtor da classe
    /// </summary>
    public class ClsConhecimentoPerfil
    {
        #region Declara��es

        public ClsConhecimentoPerfil()
        {
            alimentaColecaoCampos();
        }

        //Cole��o de objetos.
        private ServiceDesk.Banco.ClsAtributos objAtributos = new ServiceDesk.Banco.ClsAtributos();

        //Atributos.
        private ServiceDesk.Banco.ClsAtributo objCodigoConhecimento = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objCodigoPerfil = new ServiceDesk.Banco.ClsAtributo();
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
        /// Codigo do conhecimento.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo CodigoConhecimento
        {
            get { return objCodigoConhecimento; }
            set { this.objCodigoConhecimento = value; }
        }

        /// <summary>
        /// C�digo do perfil.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo CodigoPerfil
        {
            get { return objCodigoPerfil; }
            set { this.objCodigoPerfil = value; }
        }
        #endregion

        #region Metodos

        #region Valida a exclus�o
        /// <summary>
        /// Valida��o da exclus�o.
        /// </summary>
        /// <param name="strMensagem">Mensagem com status da opera��o.</param>
        /// <returns>Retorna true ou false. Se foi validado ou n�o.</returns>
        public bool ValidaExclusao(out String strMensagem)
        {
            try
            {
                strMensagem = string.Empty;

                if (objCodigoConhecimento.Valor.Trim() == string.Empty)
                {
                    strMensagem = "Por favor, selecione o perfil que deseja dessassociar do conhecimento.";
                    return false;
                }
                else if (objCodigoPerfil.Valor.Trim() == string.Empty)
                {
                    strMensagem = "Por favor, selecione o perfil que deseja dessassociar do conhecimento.";
                    return false;
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

                if (objCodigoConhecimento.Valor.Trim() == null)
                {
                    strMsg = "Selecione o conhecimento.";
                    return false;
                }
                if (objCodigoPerfil.Valor.Trim() == null)
                {
                    strMsg = "Selecione o perfil.";
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
        /// <returns>bool Retorna true ou false. Se existe ou n�o.</returns>
        public bool VerificaSeJaExisteNoBanco(out String strMensagem)
        {
            strMensagem = string.Empty;
            try
            {
                ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();

                string strSql = " perfil_codigo = " + Convert.ToInt32(objCodigoPerfil.Valor.Trim()) + " ";
                strSql += " and conhecimento_codigo = "+ Convert.ToInt32(objCodigoConhecimento.Valor.Trim()) +"";

                string strValor = objBanco.retornaValorCampo("ConhecimentoPerfil", "conhecimento_codigo", strSql);

                if (strValor.Trim() != string.Empty)
                {
                    objBanco = null;
                    strMensagem = "O perfil selecionado j� esta para o conhecimento no qual deseja associar o perfil.";
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
        /// <param name="strMensagem">Mensagem com informa��o da execu��o do m�todo.</param>
        /// <returns>Retorna true ou false. Se o registro foi inserido ou n�o.</returns>
        public bool insere(out String strMensagem)
        {
            try
            {
                strMensagem = String.Empty;

                //Verifica se todos os campos foram informandos corretamente.
                if (ValidaDados(out strMensagem) == false) return false;

                //Verifica se os registros a serem inseridos j� existem na tabela.
                if (VerificaSeJaExisteNoBanco(out strMensagem) == true) return false;

                ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
                if (objBanco.insereColecao(this.objAtributos))
                {
                    strMensagem = "Registro inserido com sucesso.";
                    objBanco = null;
                    return true;
                }
                else
                {
                    strMensagem = "Imposs�vel inserir registro.";
                    objBanco = null;
                    return false;
                }
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
        /// Gera uma nova geraGridView de acordo com a cole��o de atributos.
        /// </summary>
        /// <param name="objGridView">objeto gridview</param>
        public static void geraGridView(System.Web.UI.WebControls.GridView objGridView)
        {
            try
            {
                objGridView.AutoGenerateColumns = false;
                ClsConhecimentoPerfil objConhecimentoPerfil = new ClsConhecimentoPerfil();
                ServiceDesk.Controle.ClsGridView.geraGridView(objGridView, objConhecimentoPerfil.objAtributos);
                objConhecimentoPerfil = null;
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
        /// <param name="objGridView">Nome do Grid View</param>
        /// <param name="bolCondicao">Condi��o para verificar se ser� utilizado valores dos campos identificadores</param>
        public static void geraGridView(System.Web.UI.WebControls.GridView objGridView, ClsConhecimentoPerfil objConhecimentoPerfil, bool bolCondicao)
        {
            try
            {
                objGridView.AutoGenerateColumns = false;
                ServiceDesk.Controle.ClsGridView.geraGridView(objGridView, objConhecimentoPerfil.objAtributos, bolCondicao);
                objConhecimentoPerfil = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region GeraGridView
        /// <summary>
        /// Gera grid view com dados do perfil pelo c�digo do conhecimento.
        /// </summary>
        /// <param name="objGridView">Nome do Grid View</param>
        /// <param name="intCodigo">C�digo do conhecimento</param>
        public static void geraGridView(System.Web.UI.WebControls.GridView objGridView, int intCodigo)
        {
            try
            {
                objGridView.AutoGenerateColumns = false;

                string strSql = "select CP.conhecimento_codigo, P.perfil_codigo, TU.tipo_usuario_codigo, TU.sigla, TU.descricao, A.descricao as aplicacao";
                strSql += " from ConhecimentoPerfil CP, Perfil P, TipoUsuario TU, Aplicacao A ";
                strSql += " where CP.perfil_codigo = P.perfil_codigo ";
                strSql += " and P.aplicacao_codigo = A.aplicacao_codigo";
                strSql += " and P.tipo_usuario_codigo = TU.tipo_usuario_codigo ";
                strSql += " and CP.conhecimento_codigo = "+ intCodigo +"";

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

        #region GeraDropDownListPerfil
        /// <summary>
        /// Gera um novo DropDownList de acordo com a cole��o de atributos.
        /// </summary>
        /// <param name="objDropDownList">Nome do objeto DropDownList</param>
        /// <param name="strItemDefault">Nome do item que ser� default ao carregar o DropDownList</param>
        public static void geraDropDownListPerfil(System.Web.UI.WebControls.DropDownList objDropDownList, string strItemDefault)
        {
            try
            {
                string strSql = string.Empty;

                strSql = " select P.perfil_codigo, (TU.descricao)+' | '+(A.descricao) descricao ";
                strSql += " from TipoUsuario TU, Perfil P, Aplicacao A ";
                strSql += " where P.tipo_usuario_codigo = TU.tipo_usuario_codigo ";
                strSql += " and A.aplicacao_codigo = P.aplicacao_codigo";
                strSql += " order by descricao";

                objDropDownList.Items.Clear();

                System.Data.SqlClient.SqlDataReader objReader = ServiceDesk.Banco.ClsBanco.geraDataReader(strSql);
                while (objReader.Read()) objDropDownList.Items.Add(new ListItem(objReader["descricao"].ToString(), objReader["perfil_codigo"].ToString()));
                objReader.Dispose();
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

                if (ValidaDados(out strMensagem) == false) return false;

                if (VerificaSeJaExisteNoBanco(out strMensagem) == true) return false;

                ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();

                if (objBanco.alteraColecao(this.objAtributos))
                {
                    strMensagem = "Registro alterado com sucesso.";
                    objBanco = null;
                    return true;
                }
                else
                {
                    strMensagem = "Imposs�vel alterar registro.";
                    objBanco = null;
                    return false;
                }
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
                if (ValidaExclusao(out strMsg) == false) return false;


                ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
                if (objBanco.apagaColecao(this.objAtributos))
                {
                    strMsg = "Registro exclu�do com sucesso.";
                    objBanco = null;
                    return true;
                }
                else
                {
                    objBanco = null;
                    strMsg = "Imposs�vel excluir registro.";
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

        #region Excluir
        /// <summary>
        /// Excluir associa��o do perfil associado ao conhecimento por c�digo do conhecimento.
        /// </summary>
        /// <param name="intCodigo">C�digo do conhecimento.</param> 
        /// <returns>Retorna true ou false. Se o registro foi excluido ou n�o.</returns>
        public bool exclui(out String strMsg, int intCodigo)
        {
            try
            {
                strMsg = string.Empty;
                string strSql = "delete from ConhecimentoPerfil where conhecimento_codigo = " + intCodigo + "";

                ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
                if (objBanco.executaSQL(strSql) == true)
                {
                    strMsg = "Registro exclu�do com sucesso.";
                    objBanco = null;
                    return true;
                }
                else
                {
                    objBanco = null;
                    strMsg = "Imposs�vel excluir registro.";
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

        #region alimentaColecaoCampos
        /// <summary>
        /// Alimentar cole��o de atributos
        /// </summary>
        private void alimentaColecaoCampos()
        {
            try
            {
                objAtributos.NomeTabela = "ConhecimentoPerfil";
                objAtributos.DescricaoTabela = "Tabela que armazena as informa��es do perfil que poder� ver o conhecimento.";

                objCodigoConhecimento.Campo = "conhecimento_codigo";
                objCodigoConhecimento.Descricao = "C�digo do conhecimento.";
                objCodigoConhecimento.CampoIdentificador = true;
                objCodigoConhecimento.CampoObrigatorio = true;
                objCodigoConhecimento.Tipo = System.Data.DbType.Int32;
                objAtributos.Add(objCodigoConhecimento);

                objCodigoPerfil.Campo = "perfil_codigo";
                objCodigoPerfil.Descricao = "C�digo do perfil.";
                objCodigoPerfil.CampoIdentificador = true;
                objCodigoPerfil.CampoObrigatorio = true;
                objCodigoPerfil.Tipo = System.Data.DbType.Int32;
                objAtributos.Add(objCodigoPerfil);
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