/*
- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
  � Classe de acesso a dados da tabela SolucaoProjetoTipo que armazena os tipos de solu��es.
  
  	Data: 16/12/2005
  	Autor: Fernanda Passos
  	Descri��o: Esta classe apresentar� m�todos e propriedades para manipula��o de dados.
  
  
  � Altera��es
  	Data: 14/12/2005
  	Autor: Fernanda Passos
  	Descri��o: Inclus�o do atributo pessoa_codigo_alocacao na declara��o, cole��o de atributos e
    inclus�o altera��o do nome equipe_codigo para equipe_codigo_alocacao.
 
   � Altera��es
  	Data: 26/12/2005
  	Autor: Sylvio Neto
  	Descri��o: Inclus�o do atributo flag_padrao na declara��o, cole��o de atributos.
    Este campo indica se um tipo � padr�o para o sistema.
 * 
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
/// Classe de acesso a dados da tabela SolucaoProjetoTipo
/// </summary>
namespace ServiceDesk.Negocio
{
    /// <summary>
    /// Construtor da classe
    /// </summary>
    public class ClsSolucaoProjetoTipo
    {
        #region Construtor da classe
        public ClsSolucaoProjetoTipo()
        {
            alimentaColecaoCampos();
        }

        #region metodo Construtor da classe com passagem de parametro
        /// <summary>
        /// Construtor da classe
        /// </summary>
        public ClsSolucaoProjetoTipo(int intCodigo)
        {
          this.alimentaColecaoCampos();
          this.objCodigo.Valor = intCodigo.ToString();
          ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
          objBanco.alimentaColecao(this.objAtributos);
          objBanco = null;
        }
        #endregion

        #endregion

        #region Declara��es
        //Cole��o de objetos.
        private ServiceDesk.Banco.ClsAtributos objAtributos = new ServiceDesk.Banco.ClsAtributos();
        //Atributos de um problema.
        private ServiceDesk.Banco.ClsAtributo objCodigo = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objNome = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objTipoPadrao = new ServiceDesk.Banco.ClsAtributo();
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
        /// Codigo do problema.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo Codigo
        {
            get { return objCodigo; }
            set { this.objCodigo = value; }
        }

        /// <summary>
        /// Nome do tipo.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo Nome
        {
            get { return objNome; }
            set { this.objNome = value; }
        }

        /// <summary>
        /// Flag de Tipo padr�o
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo FlagTipoPadrao
        {
          get { return objTipoPadrao; }
          set { this.objTipoPadrao = value; }
        }

      
        #endregion

        #region Metodos

        #region Validacao dos dados
        /// <summary>
        /// Valida��o dos campos de preenchimento obrigat�rio.
        /// </summary>
        /// <returns>Retorna true ou false. Se foi validado ou n�o.</returns>
        public bool ValidacaoDados(out String strMsg)
        {
            try
            {
                strMsg = String.Empty;

                if (objCodigo.Valor.Trim() == string.Empty)
                {
                    strMsg = "Informe o c�digo identificador.";
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
     
        #region Valida a exclus�o
        /// <summary>
        /// Valida a exclus�o
        /// </summary>
        /// <returns>Retorna true ou false. Se foi validado ou n�o.</returns>
        public bool ValidaExclusao(out String strMsg)
        {
            try
            {
                strMsg = String.Empty;

                string strSqlWhere = " solucao_projeto_tipo_codigo = " + Convert.ToInt32(objCodigo.Valor.Trim()) + "";

                ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
                string strValor = objBanco.retornaValorCampo("SolucaoProjeto", "descricao", strSqlWhere);

                if (strValor == string.Empty)
                    return true; //Validado � exclus�o.
                else
                {
                    strMsg = "A solu��o que deseja excluir esta associada � solu��o existente. A��o cancelada.";
                    return false;//N�o validado � exclus�o.
                }
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

                //Verifica se todos os campos foram informados.
                if (ValidacaoDados(out strMensagem) == false)
                {
                    bolRetorno = false;
                }
                //Verifica se o nome do problema j� existe no banco para outro registro.
                else if (VerificaSeJaExisteNoBanco() == true)
                {
                    strMensagem = "J� existe um tipo de solu��o com o nome informado na tela.";
                    bolRetorno = false;
                }
                else
                {
                    ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
                    //Altera no banco de dados.
                    if (objBanco.alteraColecao(this.objAtributos))
                    {
                        strMensagem = "Altera��o realizada com sucesso.";
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

                //Valida exclus�o do problema
                if (ValidaExclusao(out strMsg) == false)
                    return false;

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
                objAtributos.NomeTabela = "SolucaoProjetoTipo";
                objAtributos.DescricaoTabela = "Tabela que armazena os tipos de solu��es";

                objCodigo.Campo = "solucao_projeto_tipo_codigo";
                objCodigo.Descricao = "C�digo do registro.";
                objCodigo.CampoIdentificador = true;
                objCodigo.CampoObrigatorio = true;
                objCodigo.Tipo = System.Data.DbType.Int32;
                objAtributos.Add(objCodigo);

                objNome.Campo = "descricao";
                objNome.Descricao = "Nome do tipo de solu��o.";
                objNome.CampoIdentificador = false;
                objNome.CampoObrigatorio = true;
                objNome.Tipo = System.Data.DbType.String;
                objAtributos.Add(objNome);

                objTipoPadrao.Campo = "flag_padrao";
                objTipoPadrao.Descricao = "Flag tipo padr�o de solu��o.";
                objTipoPadrao.CampoIdentificador = false;
                objTipoPadrao.CampoObrigatorio = false;
                objTipoPadrao.Tipo = System.Data.DbType.String;
                objAtributos.Add(objTipoPadrao);
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
                ClsSolucaoProjetoTipo objSolucaoProjetoTipo = new ClsSolucaoProjetoTipo();
                objDropDownList.DataTextField = objSolucaoProjetoTipo.objNome.Campo;
                objDropDownList.DataValueField = objSolucaoProjetoTipo.objCodigo.Campo;
                ServiceDesk.Controle.ClsDropDownList.geraDropDownList(objDropDownList, objSolucaoProjetoTipo.objAtributos);
                objSolucaoProjetoTipo = null;

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
        public static void geraGridView(System.Web.UI.WebControls.GridView objGridView, ClsSolucaoProjetoTipo objSolucaoProblemaTipo, bool bolCondicao)
        {
            try
            {
                objGridView.AutoGenerateColumns = false;
                ServiceDesk.Controle.ClsGridView.geraGridView(objGridView, objSolucaoProblemaTipo.objAtributos, bolCondicao);
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

                string strSql = "select solucao_projeto_tipo_codigo, descricao";
                strSql += " from SolucaoProjetoTipo";

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
                string strSql = "select max(solucao_projeto_tipo_codigo) as maximo from SolucaoProjetoTipo";

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

        #region Verifica se dados j� existem
        /// <summary>
        /// Verifica se j� existe com o mesmo nome para registro diferente.
        /// </summary>
        /// <param name="strNome"></param>
        /// <returns>Retorna true ou false. Se existe ou n�o.</returns>
        public bool VerificaSeJaExisteNoBanco()
        {
            try
            {   //Busca no banco de dados pelo nome.
                ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
                string strSqlCriterio = "descricao = '" + objNome.Valor.Trim() + "' and solucao_projeto_tipo_codigo <> " + objCodigo.Valor.Trim() + "";
                string strValor = objBanco.retornaValorCampo("SolucaoProjetoTipo", "descricao", strSqlCriterio);

                //Verifica se o nome foi encontrado.
                if (strValor.Trim() == string.Empty)
                    return false; //N�o existe
                else
                    return true; //Existe
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Busca o codigo do Tipo marcado como padr�o
        /// <summary>
        /// Busca o codigo do Tipo marcado como padr�o
        /// </summary>
        /// <returns>Retorna true ou false. Se existe ou n�o. 
        /// Retorna (out) o codigo do tipo padrao caso exista. Caso contrario retorna zero no codigo</returns>
        public static bool getTipoPadrao(out int intCodigoTipoPadrao)
        {
          intCodigoTipoPadrao = 0;

          try
          {   //Busca no banco de dados pelo nome.
            ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
            string strSqlCriterio = " flag_padrao = 'S' ";
            string strValor = objBanco.retornaValorCampo("SolucaoProjetoTipo", "solucao_projeto_tipo_codigo", strSqlCriterio);
            
            //Verifica se o nome foi encontrado.
            if (strValor.Trim() == string.Empty)
            { 
              return false; //N�o existe
            }  
            else
            {
              intCodigoTipoPadrao = Convert.ToInt32(strValor);
              return true; //Existe
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        }
        #endregion

        #region Pega o nome do tipo pelo c�digo
        /// <summary>
        /// Pega o nome do tipo pelo c�digo
        /// </summary>
        /// <param name="intCodigoSolucaoTipo">C�digo do tipo</param>
        /// <returns>Retorna string com nome do tipo</returns>
        public static string GetNameTipoSolucao(int intCodigoSolucaoTipo)
        {
            try
            {
                if (intCodigoSolucaoTipo <= 0) return string.Empty;

                string strSql = " solucao_projeto_tipo_codigo = " + intCodigoSolucaoTipo + " ";

                ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
                string strValor = objBanco.retornaValorCampo("SolucaoProjetoTipo", "descricao", strSql);
                return strValor;
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