/*
- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
  • Classe para manipulação dos registros da tabela mudança.
  
  	Data: 30/11/2005
  	Autor: Fernanda Passos
  	Descrição: Apresenta métodos e propriedades do objeto mudança.
  
  
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

/// <summary>
/// Classe de acesso a dados da tabela Problema.
/// </summary>
namespace ServiceDesk.Negocio
{
    /// <summary>
    /// Construtor da classe
    /// </summary>
    public class ClsMudanca
    {
        #region Declarações

        public ClsMudanca()
        {
            alimentaColecaoCampos();
        }

        //Coleção de objetos.
        private ServiceDesk.Banco.ClsAtributos objAtributos = new ServiceDesk.Banco.ClsAtributos();

        //Atributos.
        private ServiceDesk.Banco.ClsAtributo objCodigo = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objChamadoCodigo = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objDescricao = new ServiceDesk.Banco.ClsAtributo();
        #endregion

        #region Propriedades
        /// <summary>
        /// Coleção de atributos
        /// </summary>
        public ServiceDesk.Banco.ClsAtributos Atributos
        {
            get
            {
                return this.Atributos;
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
        /// Código do chamado.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo ChamadoCodigo
        {
            get { return objChamadoCodigo; }
            set { this.objChamadoCodigo = value; }
        }


        /// <summary>
        /// Descrição.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo Descricao
        {
            get { return objDescricao; }
            set { this.objDescricao = value; }
        }
        #endregion

        #region Metodos

        #region Método de exclusão
        /// <summary>
        /// Método de exclusão da mudança.
        /// </summary>
        /// <returns>Retorna true ou false. Se o registro foi excluido ou não.</returns>
        public bool exclui(out String strMsg)
        {
            strMsg = string.Empty;

            try
            {
                //Valida a exclusão.
                //if (ServiceDesk.Negocio.ClsIdentificador.ValidaExclusao(objCodigo.Campo, objCodigo.Valor.Trim(), out strMsg, true, false, objAtributos.NomeTabela.Trim()) == false) return false;

            }
            catch (System.Exception ex)
            {
                strMsg = ex.Message;
                return false;
            }

            return true;
        }
        #endregion

        #region Método alimenta coleção de campos.
        /// <summary>
        /// Método que alimenta a coleção de atributos
        /// </summary>
        private void alimentaColecaoCampos()
        {
            try
            {
                objAtributos.NomeTabela = "Mudanca";
                objAtributos.DescricaoTabela = "Tabela de mudanca";

                objCodigo.Campo = "mudanca_codigo";
                objCodigo.Descricao = "Código da mudança";
                objCodigo.CampoIdentificador = true;
                objCodigo.CampoObrigatorio = true;
                objCodigo.Tipo = System.Data.DbType.Int32;
                objAtributos.Add(objCodigo);

                objChamadoCodigo.Campo = "chamado_codigo";
                objChamadoCodigo.Descricao = "Código do chamado";
                objChamadoCodigo.CampoIdentificador = false;
                objChamadoCodigo.CampoObrigatorio = false;
                objChamadoCodigo.Tipo = System.Data.DbType.Int32;
                objAtributos.Add(objChamadoCodigo);

                objDescricao.Campo = "descricao";
                objDescricao.Descricao = "Descrição da mudança";
                objDescricao.CampoIdentificador = false;
                objDescricao.CampoObrigatorio = true;
                objDescricao.Tipo = System.Data.DbType.String;
                objAtributos.Add(objDescricao);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Gera DropDownList
        /// <summary>
        /// Gera um novo DropDownList de acordo com a coleção de atributos.
        /// </summary>
        /// <param name="objDropDownList">DropDownList</param>
        public static void geraDropDownList(System.Web.UI.WebControls.DropDownList objDropDownList)
        {
            try
            {
                ClsMudanca objMudanca = new ClsMudanca();
                objDropDownList.DataTextField = objMudanca.objDescricao.Campo;
                objDropDownList.DataValueField = objMudanca.objCodigo.Campo;
                ServiceDesk.Controle.ClsDropDownList.geraDropDownList(objDropDownList, objMudanca.objAtributos);
                objMudanca = null;

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

        #region GeraGridView
        /// <summary>
        /// Gera uma nova geraGridView de acordo com a coleção de atributos.
        /// </summary>
        /// <param name="objGridView">objeto gridview</param>
        public static void geraGridView(System.Web.UI.WebControls.GridView objGridView)
        {
            try
            {
                objGridView.AutoGenerateColumns = false;
                ClsMudanca objMudanca = new ClsMudanca();
                ServiceDesk.Controle.ClsGridView.geraGridView(objGridView, objMudanca.objAtributos);
                objMudanca = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region GeraGridView
        /// <summary>
        /// Gera uma nova geraGridView de acordo com a coleção de atributos.
        /// </summary>
        /// <param name="objGridView">objjeto Grid View</param>
        /// <param name="bolCondicao">Condição para verificar se será utilizado a cláusula Where</param>
        public static void geraGridView(System.Web.UI.WebControls.GridView objGridView, ClsMudanca objMudanca, bool bolCondicao)
        {
            try
            {
                objGridView.AutoGenerateColumns = false;
                ServiceDesk.Controle.ClsGridView.geraGridView(objGridView, objMudanca.objAtributos, bolCondicao);
                objMudanca = null;
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