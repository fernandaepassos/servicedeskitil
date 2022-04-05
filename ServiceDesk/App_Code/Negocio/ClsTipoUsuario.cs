using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for ClsTipoUsuario
/// </summary>
namespace ServiceDesk.Negocio
{
    public class ClsTipoUsuario
    {

        //Colecao de atributos de Tipo de Usuário
        private ServiceDesk.Banco.ClsAtributos objAtributos = new ServiceDesk.Banco.ClsAtributos();

        //Atributos de um Tipo de Usuário
        private ServiceDesk.Banco.ClsAtributo objCodigo = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objSigla = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objDescricao = new ServiceDesk.Banco.ClsAtributo();

        #region Propriedades

        public ServiceDesk.Banco.ClsAtributos Atributos
        {
            get { return this.objAtributos; }
        }

        /// <summary>
        /// Código .
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo Codigo
        {
            get { return objCodigo; }
            set { this.objCodigo = value; }
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
        /// Sigla
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo Sigla
        {
            get { return objSigla; }
            set { this.objSigla = value; }
        }


        #endregion

        #region Métodos

        #region metodo Construtor da classe
        /// <summary>
        /// Construtor da classe
        /// </summary>
        public ClsTipoUsuario()
        {
            this.alimentaColecaoCampos();
        }
        #endregion

        #region metodo Construtor da classe com passagem de parametro
        /// <summary>
        /// Construtor da classe
        /// </summary>
        public ClsTipoUsuario(int intCodigo)
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

        #region alimentaColecaoCampos

        /// <summary>
        /// Método que alimenta a coleção de atributos
        /// </summary>
        private void alimentaColecaoCampos()
        {
            objAtributos.NomeTabela = "TipoUsuario";
            objAtributos.DescricaoTabela = "TipoUsuario";

            objCodigo.Campo = "tipo_usuario_codigo";
            objCodigo.Descricao = "Código";
            objCodigo.CampoIdentificador = true;
            objCodigo.CampoObrigatorio = false;
            objCodigo.Tipo = System.Data.DbType.Int32;
            objAtributos.Add(objCodigo);

            objDescricao.Campo = "descricao";
            objDescricao.Descricao = "Descrição";
            objDescricao.CampoIdentificador = false;
            objDescricao.CampoObrigatorio = false;
            objDescricao.Tipo = System.Data.DbType.String;
            objAtributos.Add(objDescricao);

            objSigla.Campo = "sigla";
            objSigla.Descricao = "Sigla";
            objSigla.CampoIdentificador = false;
            objSigla.CampoObrigatorio = false;
            objSigla.Tipo = System.Data.DbType.String;
            objAtributos.Add(objSigla);

        }
        #endregion

        #region metodo geraGridView
        /// <summary>
        /// Gera uma nova geraGridView de acordo com a coleção de atributos.
        /// </summary>
        /// <param name="objGridView">geraGridView</param>
        public static void geraGridView(System.Web.UI.WebControls.GridView objGridView)
        {
            try
            {
                objGridView.AutoGenerateColumns = false;
                ClsTipoUsuario objTipoUsuario = new ClsTipoUsuario();
                ServiceDesk.Controle.ClsGridView.geraGridView(objGridView, objTipoUsuario.objAtributos);
                objTipoUsuario = null;
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
        public static void geraGridView(System.Web.UI.WebControls.GridView objGridView, ClsTipoUsuario objTipoUsuario, bool bolCondicao)
        {
            try
            {
                objGridView.AutoGenerateColumns = false;
                ServiceDesk.Controle.ClsGridView.geraGridView(objGridView, objTipoUsuario.objAtributos, bolCondicao);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region metodo insere
        /// <summary>
        /// Método que insere um Tipo de Usuário
        /// </summary>
        /// <param name="strMensagem">Mensagem com informação da execução do método.</param>
        /// <returns>Retorna true ou false. Se o registro foi inserido ou não.</returns>
        public bool insere(out String strMensagem)
        {
            try
            {
                strMensagem = String.Empty;
                bool bolRetorno = false;

                if (this.objSigla.Valor.Trim() == String.Empty)
                    strMensagem = "Favor informar a Sigla do Tipo de Usuário.<br />";

                if (this.objDescricao.Valor.Trim() == String.Empty)
                    strMensagem = "Favor informar a Descrição do Tipo de Usuário.";

                if (strMensagem == String.Empty)
                {
                    if (VerificaExisteSiglaDesc() == false)
                    {
                        ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
                        if (objBanco.insereColecao(this.objAtributos))
                        {
                            strMensagem = "Item inserido com sucesso.";
                            bolRetorno = true;
                        }
                        objBanco = null;
                    }
                    else
                    {
                        strMensagem = "Já existe um item cadastrado com esta Sigla ou Descrição.";
                    }
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
        /// Método que altera um Tipo de usuario
        /// </summary>
        /// <returns>Retorna true ou false. Se o registro foi alterado ou não.</returns>
        public bool altera(out String strMensagem)
        {
            try
            {
                strMensagem = String.Empty;
                bool bolRetorno = false;

                if (this.objSigla.Valor.Trim() == String.Empty)
                    strMensagem = "Favor informar a Sigla do Tipo de Usuário.<br />";

                if (this.objDescricao.Valor.Trim() == String.Empty)
                    strMensagem = "Favor informar a Descrição do Tipo de Usuário.";

                if (strMensagem == String.Empty)
                {
                    if (VerificaExisteSiglaDesc() == false)
                    {
                        ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
                        if (objBanco.alteraColecao(this.objAtributos))
                        {
                            strMensagem = "Item alterado com sucesso.";
                            bolRetorno = true;
                        }
                        objBanco = null;
                    }
                    else
                    {
                        strMensagem = "Já existe um item cadastrado com esta Sigla ou Descrição.";
                    }
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
        /// Método que exclui uma Aplicação
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

        #region metodo geraDropDownList
        /// <summary>
        /// Gera um novo DropDownList de acordo com a coleção de atributos.
        /// </summary>
        /// <param name="objDropDownList">DropDownList</param>
        public static void geraDropDownList(System.Web.UI.WebControls.DropDownList objDropDownList)
        {
            try
            {
                ClsTipoUsuario objTipoUsuario = new ClsTipoUsuario();
                objDropDownList.DataTextField = objTipoUsuario.objDescricao.Campo;
                objDropDownList.DataValueField = objTipoUsuario.objCodigo.Campo;
                ServiceDesk.Controle.ClsDropDownList.geraDropDownList(objDropDownList, objTipoUsuario.objAtributos);
                objTipoUsuario = null;

                //Adiciona a opção default no dropdownlist.
                ListItem itemDefault = new ListItem();
                itemDefault.Text = "--";
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

        #region VerificaExisteSiglaDesc
        /// <summary>
        /// Método que retorna um registro caso exista uma sigla ou descrição no banco.
        /// </summary>
        /// <param name="strSigla">Descrição da sigla</param>
        /// <param name="strDescricao">Descrição do Tipo de Usuário</param>
        /// <returns>Retorna True ou False</returns>
        public bool VerificaExisteSiglaDesc()
        {
            String strSql = String.Empty;
            bool bolRetorno = false;
            try
            {
                strSql = "SELECT sigla, descricao FROM tipousuario WHERE (sigla = '" + objSigla.Valor.Trim() + "' OR descricao = '" + objDescricao.Valor.Trim() + "') AND tipo_usuario_codigo <> "+ objCodigo.Valor.Trim();
                SqlDataReader objSqlDataReader = ServiceDesk.Banco.ClsBanco.geraDataReader(strSql);

                if (objSqlDataReader.Read())
                    bolRetorno = true;

                objSqlDataReader.Close();
                objSqlDataReader = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bolRetorno;
        }
        #endregion

        #region getDescricaoTipoUsuario
        /// <summary>
        /// Pega a descrição do tipo de usuário
        /// </summary>
        /// <param name="strCodigoTipoUsuario"></param>
        /// <returns></returns>
        static public string getDescricaoTipoUsuario(String strCodigoTipoUsuario)
        {
            String strSql = String.Empty;
            String strDescricaoTipoUsuario = String.Empty;
            try
            {
                //Query que busca os TipoUsuario disponíveis para um determinado usuário
                strSql = "SELECT descricao FROM tipousuario WHERE tipo_usuario_codigo = " + strCodigoTipoUsuario;
                SqlDataReader objSqlDataReader = ServiceDesk.Banco.ClsBanco.geraDataReader(strSql);

                if (objSqlDataReader.Read())
                {
                    strDescricaoTipoUsuario = objSqlDataReader["descricao"].ToString();

                }
                objSqlDataReader.Close();
                objSqlDataReader = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return strDescricaoTipoUsuario;
        }
        #endregion

        #endregion
    }
}
