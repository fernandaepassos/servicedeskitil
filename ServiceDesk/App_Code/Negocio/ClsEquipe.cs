using System;
using System.Web.UI.WebControls;
using System.Collections;

/// <summary>
/// Summary description for ClsEquipe
/// </summary>
/// 
namespace ServiceDesk.Negocio
{
    public class ClsEquipe
    {
        #region Declarações

        //Colecao de atributos de Equipe
        private ServiceDesk.Banco.ClsAtributos objAtributos = new ServiceDesk.Banco.ClsAtributos();

        //Atributos de uma Equipe
        private ServiceDesk.Banco.ClsAtributo objCodigo = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objDescricao = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objCodigoAplicacao = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objCodigoNivelEquipe = new ServiceDesk.Banco.ClsAtributo();

        #endregion
       
        #region Propriedades

        public ServiceDesk.Banco.ClsAtributos Atributos
        {
            get
            {
                return this.objAtributos;
            }
        }

        /// <summary>
        /// Código da Equipe.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo Codigo
        {
            get { return objCodigo; }
            set { this.objCodigo = value; }
        }

        /// <summary>
        /// Descrição da Equipe.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo Descricao
        {
            get { return objDescricao; }
            set { this.objDescricao = value; }
        }

        /// <summary>
        /// Código da aplicação.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo CodigoAplicacao
        {
            get { return objCodigoAplicacao; }
            set { this.objCodigoAplicacao = value; }
        }

        /// <summary>
        /// Código do Nivel da Equipe.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo CodigoNivelEquipe
        {
            get { return objCodigoNivelEquipe; }
            set { this.objCodigoNivelEquipe = value; }
        }
        #endregion

        #region Métodos

        #region alimentaColecaoCampos

        /// <summary>
        /// Método que alimenta a coleção de atributos
        /// </summary>
        private void alimentaColecaoCampos()
        {
            objAtributos.NomeTabela = "Equipe";
            objAtributos.DescricaoTabela = "Equipe";

            objCodigo.Campo = "equipe_codigo";
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

            objCodigoAplicacao.Campo = "aplicacao_codigo";
            objCodigoAplicacao.Descricao = "Código da aplicação";
            objCodigoAplicacao.CampoIdentificador = false;
            objCodigoAplicacao.CampoObrigatorio = false;
            objCodigoAplicacao.Tipo = System.Data.DbType.Int32;
            objAtributos.Add(objCodigoAplicacao);

            objCodigoNivelEquipe.Campo = "equipe_nivel_codigo";
            objCodigoNivelEquipe.Descricao = "Código do Nível da Equipe";
            objCodigoNivelEquipe.CampoIdentificador = false;
            objCodigoNivelEquipe.CampoObrigatorio = false;
            objCodigoNivelEquipe.Tipo = System.Data.DbType.Int32;
            objAtributos.Add(objCodigoNivelEquipe);

        }
        #endregion

        #region Contrutor da Classe
        /// <summary>
        /// Construtor da classe Equipe
        /// </summary>
        public ClsEquipe()
        {
            this.alimentaColecaoCampos();
        }
        #endregion

        #region metodo Construtor da classe com passagem de parametro
        /// <summary>
        /// Construtor da classe
        /// </summary>
        public ClsEquipe(int intCodigo)
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
            strExiste = banco.retornaValorCampo("Equipe", "equipe_codigo", "descricao ='" + this.objDescricao.Valor.Trim() + "' AND equipe_codigo <> " + this.objCodigo.Valor.Trim());
            if (strExiste != String.Empty)
            {
                bolRetorno = true;
            }
            return bolRetorno;
        }
        #endregion

        #region metodo insere
        /// <summary>
        /// Método que insere um novo tipo de urgência.
        /// </summary>
        /// <param name="strMensagem">Mensagem com informação da execução do método.</param>
        /// <returns>Retorna true ou false. Se o registro foi inserido ou não.</returns>
        public bool insere(out String strMensagem)
        {
            try
            {
                strMensagem = String.Empty;
                bool bolRetorno = false;

                if (this.objCodigoAplicacao.Valor.Trim() == String.Empty)
                    strMensagem = "Favor informar a aplicação.<br />";

                if (this.objCodigoNivelEquipe.Valor.Trim() == String.Empty)
                    strMensagem += "Favor informar o nível da equipe.<br />";

                if (this.objDescricao.Valor.Trim() == String.Empty)
                    strMensagem += "Favor informar a descrição da equipe.";

                if (strMensagem == String.Empty)
                {
                    if (existeDescricao() == false)
                    {
                        ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
                        if (objBanco.insereColecao(this.objAtributos))
                        {
                            strMensagem = "Equipe inserida com sucesso.";
                            bolRetorno = true;
                        }
                        objBanco = null;
                    }
                    else
                        strMensagem = "Já existe um item cadastrado com esta descrição.";
                }

                return bolRetorno;
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
        public static void geraGridView(System.Web.UI.WebControls.GridView objGridView)
        {
            try
            {
                String strSql = String.Empty;
                objGridView.AutoGenerateColumns = false;
                ClsEquipe objEquipe = new ClsEquipe();
                //ServiceDesk.Controle.ClsGridView.geraGridView(objGridView, objEquipe.objAtributos);
                ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
                strSql = objBanco.montaQuery(objEquipe.objAtributos, false);
                strSql += " ORDER BY descricao";
                System.Data.DataSet objDataSet = ServiceDesk.Banco.ClsBanco.geraDataSet(strSql);
                objGridView.DataSource = objDataSet;
                objGridView.DataBind();
                objDataSet.Dispose();
                objDataSet = null;
                objBanco = null;
                objEquipe = null;
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
        public static void geraGridView(System.Web.UI.WebControls.GridView objGridView, ClsEquipe objEquipe, bool bolCondicao)
        {
            try
            {
                objGridView.AutoGenerateColumns = false;
                ServiceDesk.Controle.ClsGridView.geraGridView(objGridView, objEquipe.objAtributos, bolCondicao);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region metodo altera
        /// <summary>
        /// Método que altera um tipo de Urgência
        /// </summary>
        /// <returns>Retorna true ou false. Se o registro foi alterado ou não.</returns>
        public bool altera(out String strMensagem)
        {
            try
            {
                strMensagem = String.Empty;
                bool bolRetorno = false;

                if (this.objCodigoAplicacao.Valor.Trim() == String.Empty)
                    strMensagem = "Favor informar a aplicação.<br />";

                if (this.objCodigoNivelEquipe.Valor.Trim() == String.Empty)
                    strMensagem += "Favor informar o nível da equipe.<br />";

                if (this.objDescricao.Valor.Trim() == String.Empty)
                    strMensagem += "Favor informar a descrição da equipe.";

                if (strMensagem == String.Empty)
                {
                    if (existeDescricao() == false)
                    {
                        ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
                        if (objBanco.alteraColecao(this.objAtributos))
                        {
                            strMensagem = "Equipe altera com sucesso.";
                            bolRetorno = true;
                        }
                        objBanco = null;
                    }
                    else
                        strMensagem = "Já existe um item cadastrado com esta descrição.";
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
        /// Método que exclui um tipo de Urgência
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
                ClsEquipe objEquipe = new ClsEquipe();
                objDropDownList.DataTextField = objEquipe.objDescricao.Campo;
                objDropDownList.DataValueField = objEquipe.objCodigo.Campo;
                ServiceDesk.Controle.ClsDropDownList.geraDropDownList(objDropDownList, objEquipe.objAtributos);
                objEquipe = null;

                //Adiciona a opção default no dropdownlist.
                ListItem itemDefault = new ListItem();
                itemDefault.Text = "--";
                itemDefault.Value = "";
                itemDefault.Selected = true;
                objDropDownList.Items.Insert(0, itemDefault);
                itemDefault = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region  Busca os dados dos usuários lideres da equipe.

        /// <summary>
        /// Busca os dados dos usuários lideres da equipe.
        /// </summary>
        /// <param name="intCodigoEquipe">Código da equipe.</param>
        /// <returns>Retorna um data reader com coleção de atributos.</returns>
        public System.Data.SqlClient.SqlDataReader GetDadosLiderEquipe(int intCodigoEquipe)
        {
            try
            {
                System.Data.SqlClient.SqlDataReader dtReader;

                string strSql = string.Empty;
                strSql = " select P.* ";
                strSql += " from Pessoa P, EquipePessoa EP ";
                strSql += " where P.pessoa_codigo = EP.pessoa_codigo ";
                strSql += " and EP.equipe_codigo = " + intCodigoEquipe + " ";
                strSql += " and EP.flag_lider = 'S' ";
                strSql += " ORDER BY P.nome ";

                dtReader = ServiceDesk.Banco.ClsBanco.geraDataReader(strSql);

                return dtReader;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region metodo getTecnicosEquipe
        /// <summary>
        /// Busca os tecnicos de uma equipe 
        /// </summary>
        /// 
        public static System.Data.SqlClient.SqlDataReader getTecnicosEquipe(String strCodigoEquipe)
        {
            try
            {
                string strSql = string.Empty;
                System.Data.SqlClient.SqlDataReader objReader = null;

                strSql = "SELECT P.pessoa_codigo, P.Nome, P.email ";
                strSql += "FROM pessoa P, equipepessoa EP ";
                strSql += "WHERE EP.equipe_codigo = " + strCodigoEquipe;
                strSql += " AND P.pessoa_codigo = EP.pessoa_codigo ";
                strSql += " ORDER BY P.Nome ";
                try
                {
                    objReader = ServiceDesk.Banco.ClsBanco.geraDataReader(strSql);
                }
                catch
                {
                    objReader = null;
                }

                return objReader;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region metodo getlLiderEquipe
        /// <summary>
        /// Busca lider/gerente de uma equipe
        /// </summary>
        /// 
        public static System.Data.SqlClient.SqlDataReader getlLiderEquipe(String strCodigoEquipe)
        {
            try
            {
                string strSql = string.Empty;
                System.Data.SqlClient.SqlDataReader objReader = null;

                strSql = "SELECT P.pessoa_codigo, P.Nome, P.email ";
                strSql += "FROM pessoa P, equipepessoa EP ";
                strSql += "WHERE EP.equipe_codigo = " + strCodigoEquipe;
                strSql += " AND P.pessoa_codigo = EP.pessoa_codigo and EP.flag_lider = 'S'";

                try
                {
                    objReader = ServiceDesk.Banco.ClsBanco.geraDataReader(strSql);
                }
                catch
                {
                    objReader = null;
                }

                return objReader;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion


        #region metodo getCodigoLiderEquipe
        /// <summary>
        /// Busca lider/gerente de uma equipe
        /// </summary>
        /// 
        public static string getCodigoLiderEquipe(String strCodigoEquipe)
        {
          try
          {
            string strSql = string.Empty;
            string strCodigoLider = string.Empty;
            System.Data.SqlClient.SqlDataReader objReader = null;

            strSql = "SELECT P.pessoa_codigo, P.Nome, P.email ";
            strSql += "FROM pessoa P, equipepessoa EP ";
            strSql += "WHERE EP.equipe_codigo = " + strCodigoEquipe;
            strSql += " AND P.pessoa_codigo = EP.pessoa_codigo and EP.flag_lider = 'S'";

            try
            {
              objReader = ServiceDesk.Banco.ClsBanco.geraDataReader(strSql);
              if (objReader.Read())
              {
                strCodigoLider = objReader["pessoa_codigo"].ToString();
              }

            }
            catch
            {
              objReader = null;
            }

            return strCodigoLider;
          }
          catch (Exception ex)
          {
            throw ex;
          }
        }
        #endregion

        #region metodo geraDropDownListNivel
        /// <summary>
        /// Gera um novo DropDownList de acordo com a coleção de atributos.
        /// Filtrando pelo nível especificado.
        /// </summary>
        /// <param name="objDropDownList">DropDownList</param>
        public static void geraDropDownListNivel(System.Web.UI.WebControls.DropDownList objDropDownList, int CodigoNivel)
        {
            try
            {
                ClsEquipe objEquipe = new ClsEquipe();
                objEquipe.Codigo.CampoIdentificador = false;
                objEquipe.CodigoNivelEquipe.Valor = CodigoNivel.ToString();
                objEquipe.CodigoNivelEquipe.CampoIdentificador = true;
                objDropDownList.DataTextField = objEquipe.objDescricao.Campo;
                objDropDownList.DataValueField = objEquipe.objCodigo.Campo;

                ServiceDesk.Controle.ClsDropDownList.geraDropDownList(objDropDownList, objEquipe.objAtributos, true);
                objEquipe = null;

                //Adiciona a opção default no dropdownlist.
                ListItem itemDefault = new ListItem();
                itemDefault.Text = "";
                itemDefault.Value = "";
                objDropDownList.Items.Insert(0, itemDefault);
                itemDefault = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region metodo retornaLista
        /// <summary>
        /// Retorna um Array de objetos Equipe
        /// </summary>
        /// <returns></returns>
        public static ArrayList retornaLista(string strEquipeCodigo)
        {
            String strSql = String.Empty;
            ArrayList arlEquipe = new ArrayList();

            strSql = "SELECT equipe_codigo FROM Equipe ";
            
            if (strEquipeCodigo != string.Empty)
                strSql += " WHERE equipe_nivel_codigo = 0" + strEquipeCodigo;

            strSql += " ORDER BY descricao";

            System.Data.SqlClient.SqlDataReader objReader = ServiceDesk.Banco.ClsBanco.geraDataReader(strSql);
            while (objReader.Read())
            {
                ClsEquipe objEquipe = new ClsEquipe(Convert.ToInt32(objReader["equipe_codigo"].ToString()));
                arlEquipe.Add(objEquipe);
                objEquipe = null;
            }
            objReader = null;

            return arlEquipe;
        }
        #endregion

       #endregion

    }

}