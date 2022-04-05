using System;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Classe ClsStatus
/// </summary>
/// 

namespace ServiceDesk.Negocio
{
    public class ClsStatus
    {
        #region Declarações
        //Colecao de atributos de Status
        private ServiceDesk.Banco.ClsAtributos objAtributos = new ServiceDesk.Banco.ClsAtributos();
        //Atributos de uma Status
        private ServiceDesk.Banco.ClsAtributo objCodigo = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objSigla = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objDescricao = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objContaTempoSla = new ServiceDesk.Banco.ClsAtributo();
        #endregion

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

        /// <summary>
        /// Sigla
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo ContaTempoSla
        {
            get { return objContaTempoSla; }
            set { this.objContaTempoSla = value; }
        }
        #endregion

        #region Métodos

        #region metodo Construtor da classe
        /// <summary>
        /// Construtor da classe
        /// </summary>
        public ClsStatus()
        {
            this.alimentaColecaoCampos();
        }
        #endregion

        #region metodo Construtor da classe com passagem de parametro
        /// <summary>
        /// Construtor da classe
        /// </summary>
        public ClsStatus(int intCodigo)
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
            objAtributos.NomeTabela = "Status";
            objAtributos.DescricaoTabela = "Status";

            objCodigo.Campo = "status_codigo";
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

            objContaTempoSla.Campo = "conta_tempo_sla";
            objContaTempoSla.Descricao = "Conta Tempo";
            objContaTempoSla.CampoIdentificador = false;
            objContaTempoSla.CampoObrigatorio = false;
            objContaTempoSla.Tipo = System.Data.DbType.Int32;
            objAtributos.Add(objContaTempoSla);



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
                ClsStatus objStatus = new ClsStatus();
                ServiceDesk.Controle.ClsGridView.geraGridView(objGridView, objStatus.objAtributos);
                objStatus = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion

        #region GeraGridViewporTabela
        /// <summary>
        /// Método que gera uma GridView ordernada pela tabela que possui um status..
        /// </summary>
        /// <param name="objGridView">Nome da GridView.</param>
        public void GeraGridViewporTabela(System.Web.UI.WebControls.GridView objGridView)
        {
            try
            {
                String strSQL = String.Empty;
                strSQL = "SELECT S.status_codigo,S.sigla, S.descricao FROM Status S, StatusTabela ST ";
                strSQL += "GROUP BY S.status_codigo,S.sigla, S.descricao ORDER BY S.status_codigo,S.sigla, S.descricao ";

                objGridView.AutoGenerateColumns = false;
                objGridView.DataSource = ServiceDesk.Banco.ClsBanco.geraDataReader(strSQL);
                objGridView.DataBind();
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
        public static void geraGridView(System.Web.UI.WebControls.GridView objGridView, ClsStatus objStatus, bool bolCondicao)
        {
            try
            {
                objGridView.AutoGenerateColumns = false;
                ServiceDesk.Controle.ClsGridView.geraGridView(objGridView, objStatus.objAtributos, bolCondicao);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region metodo existeDescricao
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool existeDescricao()
        {
            //Verifica se já existe um determinado papel pela descrição
            bool bolRetorno = false;
            ServiceDesk.Banco.ClsBanco banco = new ServiceDesk.Banco.ClsBanco();
            string strExiste = string.Empty;
            strExiste = banco.retornaValorCampo("Status", "status_codigo", "(descricao ='" + this.objDescricao.Valor.Trim() + "' OR sigla = '" + this.objSigla.Valor.Trim() + "') AND status_codigo <> " + this.objCodigo.Valor.Trim());
            if (strExiste != String.Empty)
            {
                bolRetorno = true;
            }
            return bolRetorno;
        }
        #endregion

        #region metodo insere
        /// <summary>
        /// Método que insere um Status
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
                    strMensagem = "Favor informar a Sigla do Status.<br />";

                if (this.objDescricao.Valor.Trim() == String.Empty)
                    strMensagem = "Favor informar a Descrição do Status.";

                if (strMensagem == String.Empty)
                {
                    if (existeDescricao() == false)
                    {
                        ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
                        if (objBanco.insereColecao(this.objAtributos))
                        {
                            strMensagem = "Status inserido com sucesso.";
                            bolRetorno = true;
                        }
                        objBanco = null;
                    }
                    else
                        strMensagem = "Já existe um item cadastrado com esta sigla ou descrição.";
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
        /// Método que altera um Status
        /// </summary>
        /// <returns>Retorna true ou false. Se o registro foi alterado ou não.</returns>
        public bool altera(out String strMensagem)
        {
            try
            {
                strMensagem = String.Empty;
                bool bolRetorno = false;

                if (this.objSigla.Valor.Trim() == String.Empty)
                    strMensagem = "Favor informar a Sigla do Status.<br />";

                if (this.objDescricao.Valor.Trim() == String.Empty)
                    strMensagem = "Favor informar a Descrição do Status.";

                if (strMensagem == String.Empty)
                {
                    if (existeDescricao() == false)
                    {
                        ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
                        if (objBanco.alteraColecao(this.objAtributos))
                        {
                            strMensagem = "Staus alterado com sucesso.";
                            bolRetorno = true;
                        }
                        objBanco = null;
                    }
                    else
                        strMensagem = "Já existe um item cadastrado com esta sigla ou descrição.";
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
                String strSql = String.Empty;
                ClsStatus objStatus = new ClsStatus();
                objDropDownList.DataTextField = objStatus.objDescricao.Campo;
                objDropDownList.DataValueField = objStatus.objCodigo.Campo;
                //ServiceDesk.Controle.ClsDropDownList.geraDropDownList(objDropDownList, objStatus.objAtributos);

                ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
                strSql = objBanco.montaQuery(objStatus.Atributos, false);
                strSql += " ORDER BY descricao";
                SqlDataReader objDataReader = ServiceDesk.Banco.ClsBanco.geraDataReader(strSql);
                objDropDownList.DataSource = objDataReader;
                objDropDownList.DataBind();
                objDataReader.Dispose();
                objDataReader = null;
                objBanco = null;
                objStatus = null;

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

        #region metodo geraRepeater
        /// <summary>
        /// Gera um novo Repeater de acordo com a coleção de atributos.
        /// </summary>
        /// <param name="objDropDownList">Repeater</param>
        public static void geraRepeater(System.Web.UI.WebControls.Repeater objRepeater, ClsStatus objStatus)
        {
            try
            {
                ServiceDesk.Controle.ClsRepeater.geraRepeater(objRepeater, objStatus.objAtributos);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion

        #region getStatusPermitidosUsuario
        /// <summary>
        /// Gera dropdownlist com os tipos de status permitidos para o tipo de usuario logado na aplicacao
        /// </summary>
        /// <param name="strCodigoUsuario"></param>
        /// <returns></returns>
        static public void geraDropDownStatusPermitidosUsuario(System.Web.UI.WebControls.DropDownList objDropDownList, String strCodigoUsuario)
        {
            String strSql = String.Empty;
            try
            {
                strSql = " SELECT Distinct S.status_codigo, S.sigla, S.descricao";
                strSql += " FROM Status S, StatusAplicacao SA, PerfilEstrutura PE, Perfil P, TipoUsuario TU, ";
                strSql += " PessoaPerfilEstrutura PPE, Pessoa";
                strSql += " WHERE";
                strSql += " Pessoa.pessoa_codigo = '" + strCodigoUsuario + "'";
                strSql += " and Pessoa.pessoa_codigo = PPE.pessoa_codigo";
                strSql += " and PPE.perfil_estrutura_codigo = PE.perfil_estrutura_codigo";
                strSql += " and PE.perfil_estrutura_codigo = SA.perfil_estrutura_codigo ";
                strSql += " and SA.status_codigo = S.status_codigo ORDER BY S.descricao";

                SqlDataReader objSqlDataReader = ServiceDesk.Banco.ClsBanco.geraDataReader(strSql);

                if (objSqlDataReader.Read())
                {
                    objDropDownList.DataSource = objSqlDataReader;
                    objDropDownList.DataTextField = "descricao";
                    objDropDownList.DataValueField = "status_codigo";
                    objDropDownList.DataBind();
                    //Adiciona a opção default no dropdownlist.
                    ListItem itemDefault = new ListItem();
                    itemDefault.Text = "--";
                    itemDefault.Value = "";
                    itemDefault.Selected = true;
                    objDropDownList.Items.Insert(0, itemDefault);

                }
                objSqlDataReader.Close();
                objSqlDataReader = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion

        #region getDescricaoStatus
        /// <summary>
        /// Gera dropdownlist com os tipos de status permitidos para o tipo de usuario logado na aplicacao
        /// </summary>
        /// <param name="strCodigoStatus"></param>
        /// <returns></returns>
        static public string getDescricaoStatus(String strCodigoStatus)
        {
            String strSql = String.Empty;
            String strDescricaoStatus = String.Empty;
            try
            {
                //Query que busca os status disponíveis para um determinado usuário
                strSql = "SELECT S.descricao ";
                strSql += "FROM status S  ";
                strSql += "WHERE  ";
                strSql += "S.status_codigo = " + strCodigoStatus + " ORDER BY S.descricao";
                SqlDataReader objSqlDataReader = ServiceDesk.Banco.ClsBanco.geraDataReader(strSql);

                if (objSqlDataReader.Read())
                {
                    strDescricaoStatus = objSqlDataReader["descricao"].ToString();

                }
                objSqlDataReader.Close();
                objSqlDataReader = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }


            return strDescricaoStatus;
        }
        #endregion

        #region getCodigoStatusSolicitado
        /// <summary>
        /// Retorna o codigo do status
        /// </summary>
        /// <returns></returns>
        static public string getCodigoStatusSolicitado()
        {
            String strCodigoStatus = String.Empty;
            try
            {
                strCodigoStatus = ClsParametro.CodigoStatusSolicitado;
            }
            catch (Exception ex)
            {
                throw ex;
            }


            return strCodigoStatus;
        }
        #endregion

        #region getCodigoStatusIniciado
        /// <summary>
        /// Retorna o codigo do status
        /// </summary>
        /// <returns></returns>
        static public string getCodigoStatusIniciado()
        {
            String strCodigoStatus = String.Empty;
            try
            {
                strCodigoStatus = ClsParametro.CodigoStatusIniciado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return strCodigoStatus;
        }
        #endregion

        #region getCodigoStatusCancelado
        /// <summary>
        /// Retorna o codigo do status
        /// </summary>
        /// <returns></returns>
        static public string getCodigoStatusCancelado()
        {
            String strCodigoStatus = String.Empty;
            try
            {
                strCodigoStatus = ClsParametro.CodigoStatusCancelado;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return strCodigoStatus;
        }
        #endregion

        #region getCodigoStatusExecutado
        /// <summary>
        /// Retorna o codigo do status
        /// </summary>
        /// <returns></returns>
        static public string getCodigoStatusExecutado()
        {
            String strCodigoStatus = String.Empty;
            try
            {
                strCodigoStatus = ClsParametro.CodigoStatusExecutado;
            }
            catch (Exception ex)
            {
                throw ex;
            }


            return strCodigoStatus;
        }
        #endregion

        #region getCodigoStatusAprovado
        /// <summary>
        /// Retorna o codigo do status
        /// </summary>
        /// <returns></returns>
        static public string getCodigoStatusAprovado()
        {
            String strCodigoStatus = String.Empty;
            try
            {
                strCodigoStatus = ClsParametro.CodigoStatusAprovado;
            }
            catch (Exception ex)
            {
                throw ex;
            }


            return strCodigoStatus;
        }
        #endregion

        #region getCodigoStatusFinalizado
        /// <summary>
        /// Retorna o codigo do status
        /// </summary>
        /// <returns></returns>
        static public string getCodigoStatusFinalizado()
        {
            String strCodigoStatus = String.Empty;
            try
            {
                strCodigoStatus = ClsParametro.CodigoStatusFinalizado;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return strCodigoStatus;
        }
        #endregion

        #region Pega status atual do processo
        /// <summary>
        /// Pega o código do status atual
        /// </summary>
        /// <param name="strTabela">Nome da tabela que representa o processo</param>
        /// <param name="intTabelaIdentificador">Número identificador</param>
        /// <returns>Retorna número inteiro com o código do status</returns>
        public static int GetCodigoStatusProcesso(string strTabela, int intTabelaIdentificador)
        {
            try
            {
                string strValorRetorno = string.Empty;

                ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
                strValorRetorno = objBanco.retornaValorCampo(strTabela, "status_codigo", strTabela + "_codigo =" + intTabelaIdentificador);
                objBanco = null;

                if (strValorRetorno != string.Empty) return Convert.ToInt32(strValorRetorno); else return 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Preencher drop down de status
        public static void geraDropDownListStatus(DropDownList ddlStatusFuturo)
        {
            try
            {
                ddlStatusFuturo.DataTextField = "descricao";
                ddlStatusFuturo.DataValueField = "status_codigo";


                string strSql = "SELECT status_codigo , descricao FROM Status WHERE status_codigo IN (SELECT status_codigo FROM StatusTabela WHERE tabela = 'chamado')";
                SqlDataReader objDataReader = Banco.ClsBanco.geraDataReader(strSql);
                ddlStatusFuturo.DataSource = objDataReader;
                ddlStatusFuturo.DataBind();
                objDataReader.Dispose();
                objDataReader = null;

                ddlStatusFuturo.Items.Insert(0, new ListItem("* Selecione *", "", true));

            }
            catch (Exception)
            {

            }
        }
        #endregion

        #endregion
    }
}