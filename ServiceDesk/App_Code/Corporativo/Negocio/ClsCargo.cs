using System;

namespace SServiceDesk.Negocio
{
    /// <summary>
    /// Classe ClsCargo.
    /// </summary>
    public class ClsCargo
    {

        //Colecao de atributos do cargo
        private ServiceDesk.Banco.ClsAtributos objAtributos = new ServiceDesk.Banco.ClsAtributos();

        //Atributos de um cargo
        private ServiceDesk.Banco.ClsAtributo objCodigo = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objCodigoCargo = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objDescricao = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objStatus = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objUnidadeCodigo = new ServiceDesk.Banco.ClsAtributo();


        #region Propriedades
        public ServiceDesk.Banco.ClsAtributos Atributos
        {
            get
            {
                return this.objAtributos;
            }
        }

        public ServiceDesk.Banco.ClsAtributo Codigo
        {
            get
            {
                return objCodigo;
            }
        }

        public ServiceDesk.Banco.ClsAtributo CodigoCargo
        {
            get
            {
                return objCodigoCargo;
            }
        }


        public ServiceDesk.Banco.ClsAtributo Descricao
        {
            get
            {
                return objDescricao;
            }
        }

        public ServiceDesk.Banco.ClsAtributo Status
        {
            get
            {
                return objStatus;
            }
        }

        public ServiceDesk.Banco.ClsAtributo UnidadeCodigo
        {
            get
            {
                return objUnidadeCodigo;
            }
        }


        #endregion

        #region Construtor
        public ClsCargo()
        {
            this.alimentaColecaoCampos();
        }
        #endregion

        #region metodo Construtor da classe com passagem de parametro
        /// <summary>
        /// Construtor da classe
        /// </summary>
        public ClsCargo(int intCodigo)
        {
            this.alimentaColecaoCampos();
            this.objCodigo.Valor = intCodigo.ToString();
            ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
            objBanco.alimentaColecao(this.objAtributos);
            objBanco = null;
        }
        #endregion

        #region metodo alimentaColecaoCampos
        /// <summary>
        /// Adiciona todos os atributos de um centro de custo a coleção de atributos.
        /// </summary>
        private void alimentaColecaoCampos()
        {
            objAtributos.DescricaoTabela = "Cargo";
            objAtributos.NomeTabela = "Cargo";

            objCodigo.Campo = "cargo_codigo";
            objCodigo.Descricao = "Código";
            objCodigo.CampoIdentificador = true;
            objCodigo.CampoObrigatorio = true;
            objCodigo.Tipo = System.Data.DbType.Int32;
            objAtributos.Add(objCodigo);

            objCodigoCargo.Campo = "codigo";
            objCodigoCargo.Descricao = "Código do Cargo";
            objCodigoCargo.CampoObrigatorio = true;
            objCodigoCargo.Tipo = System.Data.DbType.String;
            objCodigoCargo.Tamanho = 20;
            objAtributos.Add(objCodigoCargo);

            objDescricao.Campo = "descricao";
            objDescricao.Descricao = "Descrição";
            objDescricao.CampoObrigatorio = true;
            objDescricao.Tipo = System.Data.DbType.String;
            objDescricao.Tamanho = 120;
            objAtributos.Add(objDescricao);

            objStatus.Campo = "status_codigo";
            objStatus.Descricao = "Status";
            objStatus.Tipo = System.Data.DbType.Int32;
            objAtributos.Add(objStatus);

            objUnidadeCodigo.Campo = "unidade_codigo";
            objUnidadeCodigo.Descricao = "Unidade Código";
            objUnidadeCodigo.Tipo = System.Data.DbType.Int32;
            objAtributos.Add(objUnidadeCodigo);

        }
        #endregion

        #region metodo geraGridView
        /// <summary>
        /// Gera uma nova geraGridView de acordo com a coleção de atributos.
        /// </summary>
        /// <param name="objGridView">geraGridView</param>
        public static void geraGridView(System.Web.UI.WebControls.GridView objGridView)
        {
            objGridView.AutoGenerateColumns = false;
            ClsCargo objCargo = new ClsCargo();
            ServiceDesk.Controle.ClsGridView.geraGridView(objGridView, objCargo.objAtributos);
            objCargo = null;
        }
        #endregion

        #region metodo geraGridView
        /// <summary>
        /// Gera uma nova geraGridView de acordo com a coleção de atributos.
        /// </summary>
        /// <param name="objGridView">geraGridView</param>
        /// <param name="bolCondicao">Condição para verificar se será utilizado a cláusula Where</param>
        public static void geraGridView(System.Web.UI.WebControls.GridView objGridView, ClsCargo objCargo, bool bolCondicao)
        {
            objGridView.AutoGenerateColumns = false;
            ServiceDesk.Controle.ClsGridView.geraGridView(objGridView, objCargo.objAtributos, bolCondicao);

        }
        #endregion

        #region metodo geraDropDownList
        /// <summary>
        /// Gera um novo DropDownList de acordo com a coleção de atributos.
        /// </summary>
        /// <param name="objDropDownList">DropDownList</param>
        public static void geraDropDownList(System.Web.UI.WebControls.DropDownList objDropDownList)
        {
            String strSql = String.Empty;
            ClsCargo objCargo = new ClsCargo();
            ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();

            strSql = objBanco.montaQuery(objCargo.objAtributos, false);
            strSql += " ORDER BY descricao";
            objDropDownList.DataSource = ServiceDesk.Banco.ClsBanco.geraDataReader(strSql);
            objDropDownList.DataTextField = objCargo.objDescricao.Campo;
            objDropDownList.DataValueField = objCargo.objCodigo.Campo;
            objDropDownList.DataBind();
            objCargo = null;
            objBanco = null;
        }
        #endregion

        #region metodo insere
        /// <summary>
        /// Método que insere um novo cargo.
        /// </summary>
        /// <param name="strMensagem">Mensagem com informação da execução do método.</param>
        /// <returns>Retorna true ou false. Se o registro foi inserido ou não.</returns>
        public bool insere(out String strMensagem)
        {
            strMensagem = String.Empty;
            bool bolRetorno = false;

            if (this.objCodigoCargo.Valor.Trim() == String.Empty)
            {
                strMensagem = "Favor informar o código do Cargo.<br>";
            }
            if (this.objDescricao.Valor.Trim() == String.Empty)
            {
                strMensagem = strMensagem + "Favor informar a descrição do Cargo.<br>";
            }
            if (this.objStatus.Valor.Trim() == String.Empty)
            {
                strMensagem = strMensagem + "Favor informar o status do Cargo.";
            }
            if (strMensagem == String.Empty)
            {
                ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
                if (objBanco.insereColecao(this.objAtributos))
                {
                    strMensagem = "Cargo inserido com sucesso.";
                    bolRetorno = true;
                }
                objBanco = null;
            }

            return bolRetorno;
        }
        #endregion

        #region metodo altera
        /// <summary>
        /// Método que altera um cargo
        /// </summary>
        /// <returns>Retorna true ou false. Se o registro foi alterado ou não.</returns>
        public bool altera(out String strMensagem)
        {
            strMensagem = String.Empty;
            bool bolRetorno = false;

            if (this.objCodigoCargo.Valor.Trim() == String.Empty)
            {
                strMensagem = "Favor informar o código do Cargo.<br>";
            }
            if (this.objDescricao.Valor.Trim() == String.Empty)
            {
                strMensagem = "Favor informar a descrição do Cargo.<br>";
            }
            if (this.objStatus.Valor.Trim() == String.Empty)
            {
                strMensagem = strMensagem + "Favor informar o status do Cargo.<br>";
            }
            if (strMensagem == String.Empty)
            {
                ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
                if (objBanco.alteraColecao(this.objAtributos))
                {
                    strMensagem = "Cargo atualizado com sucesso.";
                    bolRetorno = true;
                }
                objBanco = null;
            }

            return bolRetorno;
        }
        #endregion

        #region metodo exclui
        /// <summary>
        /// Método que exclui um cargo
        /// </summary>
        /// <returns>Retorna true ou false. Se o registro foi excluido ou não.</returns>
        public bool exclui()
        {
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
        #endregion

        #region alimentaColecao
        /// <summary>
        /// Alimenta a coleção de atributos de uma unidade
        /// </summary>
        /// <param name="intCodigo">Código da unidade a ser alimentada</param>
        public void alimentaCargo(int intCodigo)
        {
            this.objCodigo.Valor = intCodigo.ToString();
            ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
            objBanco.alimentaColecao(this.objAtributos);
            objBanco = null;
        }
        #endregion

    }
}