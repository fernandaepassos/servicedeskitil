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
/// Summary description for ClsTipoChamado
/// </summary>
/// 

namespace ServiceDesk.Negocio
{

    public class ClsTipoChamado
    {
        public ClsTipoChamado()
        {
            this.alimentaColecaoCampos();
        }

        #region metodo Construtor da classe com passagem de parametro
        /// <summary>
        /// Construtor da classe
        /// </summary>
        public ClsTipoChamado(int intCodigo)
        {
            this.alimentaColecaoCampos();
            this.objCodigo.Valor = intCodigo.ToString();
            ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
            objBanco.alimentaColecao(this.objAtributos);
            objBanco = null;
        }
        #endregion

        //Colecao de atributos de Tipo de Solicitacao
        private ServiceDesk.Banco.ClsAtributos objAtributos = new ServiceDesk.Banco.ClsAtributos();

        //Atributos de uma Tipo de Solicitacao
        private ServiceDesk.Banco.ClsAtributo objCodigo = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objDescricao = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objFlagGeraProcesso = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objPrefixo = new ServiceDesk.Banco.ClsAtributo();


        #region Propriedades

        public ServiceDesk.Banco.ClsAtributos Atributos
        {
            get
            {
                return this.objAtributos;
            }
        }

        /// <summary>
        /// Código do Tipo.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo Codigo
        {
            get { return objCodigo; }
            set { this.objCodigo = value; }
        }

        /// <summary>
        /// Descrição do Tipo.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo Descricao
        {
            get { return objDescricao; }
            set { this.objDescricao = value; }
        }

        /// <summary>
        /// Descrição do Tipo.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo FlagGeraProcesso
        {
            get { return objFlagGeraProcesso; }
            set { this.objFlagGeraProcesso = value; }
        }

                /// <summary>
        /// Descrição do Tipo.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo Prefixo
        {
            get { return objPrefixo; }
            set { this.objPrefixo = value; }
        }
        #endregion

        #region Métodos

        #region alimentaColecaoCampos

        /// <summary>
        /// Método que alimenta a coleção de atributos
        /// </summary>
        private void alimentaColecaoCampos()
        {
            objAtributos.NomeTabela = "ChamadoTipo";
            objAtributos.DescricaoTabela = "Tipo de Chamado";

            objCodigo.Campo = "chamado_tipo_codigo";
            objCodigo.Descricao = "Código";
            objCodigo.CampoIdentificador = true;
            objCodigo.CampoObrigatorio = true;
            objCodigo.Tipo = System.Data.DbType.Int32;
            objAtributos.Add(objCodigo);

            objDescricao.Campo = "descricao";
            objDescricao.Descricao = "Descrição";
            objDescricao.CampoIdentificador = false;
            objDescricao.CampoObrigatorio = true;
            objDescricao.Tipo = System.Data.DbType.String;
            objAtributos.Add(objDescricao);

            objFlagGeraProcesso.Campo = "flag_gera_processo";
            objFlagGeraProcesso.Descricao = "Flag que informa se é gerado um processo ou não";
            objFlagGeraProcesso.CampoIdentificador = false;
            objFlagGeraProcesso.CampoObrigatorio = true;
            objFlagGeraProcesso.Tipo = System.Data.DbType.String;
            objFlagGeraProcesso.Tamanho = 1;
            objAtributos.Add(objFlagGeraProcesso);

            objPrefixo.Campo = "prefixo";
            objPrefixo.Descricao = "Prefixo";
            objPrefixo.CampoIdentificador = false;
            objPrefixo.CampoObrigatorio = true;
            objPrefixo.Tipo = System.Data.DbType.String;
            objPrefixo.Tamanho = 10;
            objAtributos.Add(objPrefixo);
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
            strExiste = banco.retornaValorCampo("ChamadoTipo", "chamado_tipo_codigo", "(descricao ='" + this.objDescricao.Valor.Trim() + "' OR prefixo = '"+ this.objPrefixo.Valor.Trim() +"') AND chamado_tipo_codigo <> " + this.objCodigo.Valor.Trim());
            if (strExiste != String.Empty)
            {
                bolRetorno = true;
            }
            return bolRetorno;
        }
        #endregion

        #region getDescricaoTipo
        /// <summary>
        /// Busca a descricao do tipo do chamado pelo codigo.
        /// Criação - Autor: Thiago Oliviera 30/06/2017
        /// </summary>
        /// <param name="strTipoCodigo">Codigo do ChamadoTipo.</param>
        /// <returns>A descrição do Tipo.</returns>
        static public string getDescricaoTipo(string strTipoCodigo)
        {
            try
            {
                String strSql = String.Empty;
                String strDescricaoTipo = String.Empty;

                if (strTipoCodigo == string.Empty) return string.Empty;

                strSql = "SELECT descricao FROM chamadoTipo";
                strSql += " WHERE chamado_tipo_codigo = " + Convert.ToInt32(strTipoCodigo) + " ";

                SqlDataReader objReader = ServiceDesk.Banco.ClsBanco.geraDataReader(strSql);
                if (objReader.Read())
                {
                    strDescricaoTipo = objReader["descricao"].ToString();
                }
                objReader.Close();
                objReader.Dispose();
                objReader = null;

                return strDescricaoTipo;
            }
            catch (SqlException e)
            {
                throw e;
            }
            catch (Exception ex)
            {
                throw  ex;
            }
            
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

                if (this.objDescricao.Valor.Trim() == String.Empty)
                    strMensagem = "Favor informar a Descrição do Item.";

                if (this.objPrefixo.Valor.Trim() == String.Empty)
                    strMensagem += "Favor informar o prefixo do Item.";

                if (strMensagem == String.Empty)
                {
                    if (existeDescricao() == false)
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
                        strMensagem = "Já existe um item cadastrado com este prefixo ou esta descrição.";
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
                ClsTipoChamado objTipoSolicitacao = new ClsTipoChamado();
                //ServiceDesk.Controle.ClsGridView.geraGridView(objGridView, objTipoSolicitacao.objAtributos);
                ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
                strSql = objBanco.montaQuery(objTipoSolicitacao.objAtributos, false);
                strSql += " ORDER BY descricao";
                System.Data.DataSet objDataSet = ServiceDesk.Banco.ClsBanco.geraDataSet(strSql);
                objGridView.DataSource = objDataSet;
                objGridView.DataBind();
                objDataSet.Dispose();
                objDataSet = null;
                objBanco = null;
                objTipoSolicitacao = null;
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
        public static void geraGridView(System.Web.UI.WebControls.GridView objGridView, ClsTipoChamado objTipoSolicitacao, bool bolCondicao)
        {
            try
            {
                objGridView.AutoGenerateColumns = false;
                ServiceDesk.Controle.ClsGridView.geraGridView(objGridView, objTipoSolicitacao.objAtributos, bolCondicao);
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

                if (this.objDescricao.Valor.Trim() == String.Empty)
                    strMensagem = "Favor informar a Descrição do Item.";

                if (this.objPrefixo.Valor.Trim() == String.Empty)
                    strMensagem += "Favor informar o prefixo do Item.";

                if (strMensagem == String.Empty)
                {
                    if (existeDescricao() == false)
                    {
                        ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
                        if (objBanco.alteraColecao(this.objAtributos))
                        {
                            strMensagem = "Item atualizado com sucesso.";
                            bolRetorno = true;
                        }
                        objBanco = null;
                    }
                    else
                        strMensagem = "Já existe um item cadastrado com este prefixo ou esta descrição.";
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
                ClsTipoChamado objTipoChamado = new ClsTipoChamado();
                objDropDownList.DataTextField = objTipoChamado.objDescricao.Campo;
                objDropDownList.DataValueField = objTipoChamado.objCodigo.Campo;
                ServiceDesk.Controle.ClsDropDownList.geraDropDownList(objDropDownList, objTipoChamado.objAtributos);
                objTipoChamado = null;

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

        #region getCodigoTipoChamadoIncidente
        /// <summary>
        /// Retorna o codigo do tipo de log
        /// </summary>
        /// <returns></returns>
        static public string getCodigoTipoChamadoIncidente()
        {
            String strCodigoTipoChamadoIncidente = String.Empty;
            try
            {
                
                strCodigoTipoChamadoIncidente = ClsParametro.CodigoTipoChamadoIncidente;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return strCodigoTipoChamadoIncidente;
        }
        #endregion

        #region getCodigoTipoChamadoRequisicaoMudanca
        /// <summary>
        /// Retorna o codigo do tipo de log
        /// </summary>
        /// <returns></returns>
        static public string getCodigoTipoChamadoRequisicaoMudanca()
        {
            String strCodigoTipoChamadoRequisicaoMudanca = String.Empty;
            try
            {
                strCodigoTipoChamadoRequisicaoMudanca = ClsParametro.CodigoTipoChamadoRequisicaoMudanca;
            }
            catch (Exception ex)
            {
                throw ex;
            }


            return strCodigoTipoChamadoRequisicaoMudanca;
        }
        #endregion

        #region getCodigoTipoChamadoRequisicaoServico
        /// <summary>
        /// Retorna o codigo do tipo de log
        /// </summary>
        /// <returns></returns>
        static public string getCodigoTipoChamadoRequisicaoServico()
        {
            String strCodigoTipoChamadoRequisicaoServico = String.Empty;
            try
            {
                strCodigoTipoChamadoRequisicaoServico = ClsParametro.CodigoTipoChamadoServico;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return strCodigoTipoChamadoRequisicaoServico;
        }
        #endregion
      
        #region getPrefixoTipoChamado(string strCodigoTipo)
        /// <summary>
        /// Retorna o prefixo do tipo informado
        /// </summary>
        /// <returns></returns>
        static public string getPrefixoTipoChamado(string strCodigoTipo)
        {
          String strPrefixo = String.Empty;
          try
          {
            string strSql = "SELECT prefixo from ChamadoTipo WHERE chamado_tipo_codigo = " + strCodigoTipo;
            SqlDataReader objReader = ServiceDesk.Banco.ClsBanco.geraDataReader(strSql);
            if (objReader.Read())
            {
              strPrefixo = objReader["prefixo"].ToString().Trim();
            }
            objReader.Close();
            objReader.Dispose();
          }
          catch (Exception ex)
          {
            throw ex;
          }
          return strPrefixo;
        }
        #endregion

        #region geraProcesso(string strCodigoTipo)
        /// <summary>
        /// Para um tipo informado, retorna se o mesmo gera ou não um novo processo.
        /// </summary>
        /// <returns>true/false</returns>
        static public bool geraProcesso(string strCodigoTipo)
        {
          bool bGeraProcesso = false;

          try
          {
            if (strCodigoTipo.Trim() != string.Empty)
            {
              string strSql = "SELECT flag_gera_processo from ChamadoTipo WHERE chamado_tipo_codigo = " + strCodigoTipo + " AND flag_gera_processo = 'S' ";
              SqlDataReader objReader = ServiceDesk.Banco.ClsBanco.geraDataReader(strSql);
              if (objReader.Read())
              {
                bGeraProcesso = true;
              }
              objReader.Close();
              objReader.Dispose();
            }
          }
          catch (Exception ex)
          {
            throw ex;
          }
          return bGeraProcesso;
        }
        #endregion

        #endregion
    }
}