/*
- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
  � Classe de acesso a dados da tabela de notifica��o.
  
  	Data: 05/12/2005
  	Autor: Fernanda Passos
  	Descri��o: Esta  classe apresenta m�todos e propriedades que permitem acessar dados na
    tabela de notifica��o a qual registra todas as notifica��es.
  
  
  � Altera��es
  	Data:26/12/2005
  	Autor: Sylvio Neto
  	Descri��o: Inclusao de um construtor que recebe o codigo da notificacao desejada.
 
  � Altera��es
  	Data:09/03/2006
  	Autor: Fernanda Passos
  	Descri��o: Inclus�o do atributo tipo_codigo - int - null.
- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
*/

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
using System.Text;

/// <summary>
/// Classe de acesso a dados da tabela Notifica��o.
/// </summary>
namespace ServiceDesk.Negocio
{
    /// <summary>
    /// Construtor da classe
    /// </summary>
    public class ClsNotificacao
    {
        #region Construtores

        #region Construtor
        /// <summary>
        /// Construtor
        /// </summary>
        public ClsNotificacao()
        {
          alimentaColecaoCampos();
        }
        #endregion

        #region Construtor por parametro
      /// <summary>
        /// Construtor da classe
        /// </summary>
        public ClsNotificacao(int intCodigo)
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

        //Atributos.
        private ServiceDesk.Banco.ClsAtributo objCodigo = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objTabela = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objIdentificadorTabela = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objCodigoUsuarioEmissor = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objCodigoUsuarioReceptor = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objDtInclusao = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objDtResposta = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objFlagAprovado = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objJustificativa = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objDescricao = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objTipo = new ServiceDesk.Banco.ClsAtributo();

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
        /// Codigo da notifica��o.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo Codigo
        {
          get { return objCodigo; }
          set { this.objCodigo = value; }
        }

        /// <summary>
        /// Nome da tabela.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo Tabela
        {
          get { return objTabela; }
          set { this.objTabela = value; }
        }

        /// <summary>
        /// C�digo do identificador da tabela (n�mero inteiro do registro da tabela que esta associada).
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo IdentificadorTabela
        {
          get { return objIdentificadorTabela; }
          set { this.objIdentificadorTabela = value; }
        }

        /// <summary>
        /// C�digo do usu�rio emissor.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo CodigoUsuarioEmissor
        {
          get { return objCodigoUsuarioEmissor; }
          set { this.objCodigoUsuarioEmissor = value; }
        }

        /// <summary>
        /// C�digo do usu�rio receptor.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo CodigoUsuarioReceptor
        {
          get { return objCodigoUsuarioReceptor; }
          set { this.objCodigoUsuarioReceptor = value; }
        }

        /// <summary>
        /// Data e hora da inclus�o do registro.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo DtInclusao
        {
          get { return objDtInclusao; }
          set { this.objDtInclusao = value; }
        }

        /// <summary>
        /// Data e hora da resposta do receptor.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo DtResposta
        {
          get { return objDtResposta; }
          set { this.objDtResposta = value; }
        }

        /// <summary>
        /// Flag indicador de aprovado\n�o aprovado.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo FlagAprovado
        {
          get { return objFlagAprovado; }
          set { this.objFlagAprovado = value; }
        }

        /// <summary>
        /// Descri��o da justificativa.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo Justificativa
        {
          get { return objJustificativa; }
          set { this.objJustificativa = value; }
        }

        /// <summary>
        /// Descri��o da notifica��o.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo Descricao
        {
          get { return objDescricao; }
          set { this.objDescricao = value; }
        }

        /// <summary>
        /// Descri��o da notifica��o.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo Tipo
        {
            get { return objTipo ; }
            set { this.objTipo = value; }
        }
        
        #endregion

        #region Metodos
        
        #region Validacao dos dados
        /// <summary>
        /// Valida��o da integridade dos registros.
        /// </summary>
        /// <returns>Retorna true ou false. Se for validado ou n�o.</returns>
        public bool ValidacaoDados(out String strMsg)
        {
          try
          {
            strMsg = String.Empty;

            if (objCodigoUsuarioEmissor.Valor.Trim() == string.Empty)
            {
              strMsg = "Informe o emissor.";
              return false;
            }
            else if (objCodigoUsuarioReceptor.Valor.Trim() == string.Empty)
            {
              strMsg = "Informe o receptor.";
              return false;
            }
            else if (objDtInclusao.Valor.Trim() == string.Empty)
            {
              strMsg = "Informe a data e hora do envio.";
              return false;
            }
            else if (objTabela.Valor.Trim() == string.Empty)
            {
              strMsg = "Informe a tabela.";
              return false;
            }
            else if (objIdentificadorTabela.Valor.Trim() == string.Empty)
            {
              strMsg = "Informe o c�digo do identificador do registro na tabela.";
              return false;
            }
            else if (objIdentificadorTabela.Descricao.Trim() == string.Empty)
            {
              strMsg = "Informe a descri��o da notifica��o.";
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

        #region Enviar
        /// <summary>
        /// Inserir
        /// </summary>
        /// <param name="strMensagem">Mensagem com informa��o da execu��o do m�todo.</param>
        /// <returns>Retorna true ou false. Se o registro foi inserido ou n�o.</returns>
        public bool enviar(out String strMensagem)
        {
          try
          {
            strMensagem = String.Empty;

            if (ValidacaoDados(out strMensagem) == false)
            {
              return false;
            }
            else
            {
              ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
              if (objBanco.insereColecao(this.objAtributos))
              {
                strMensagem = "Notifica��o processada com sucesso.";
                return true;
              }
              else
                strMensagem = "N�o foi poss�vel notificar uma pessoa da equipe: " + this.objCodigoUsuarioReceptor.Valor + ".";
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

            ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
            if (objBanco.alteraColecao(this.objAtributos))
            {
              strMensagem = "Notifica��o alterada com sucesso.";
              bolRetorno = true;
            }
            objBanco = null;

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
            if (ServiceDesk.Negocio.ClsIdentificador.ValidaExclusao(objCodigo.Campo, objCodigo.Valor.Trim(), out strMsg, true, false, objAtributos.NomeTabela.Trim()) == false) return false;
            
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
            objAtributos.NomeTabela = "Notificacao";
            objAtributos.DescricaoTabela = "Tabela de notifica��es geral do sistema.";
           
            objCodigo.Campo = "notificacao_codigo";
            objCodigo.Descricao = "C�digo do registro da notifica��o";
            objCodigo.CampoIdentificador = true;
            objCodigo.CampoObrigatorio = true;
            objCodigo.Tipo = System.Data.DbType.Int32;
            objAtributos.Add(objCodigo);

            objTabela.Campo = "tabela";
            objTabela.Descricao = "Nome da tabela que emitiu a notifica��o.";
            objTabela.CampoIdentificador = false;
            objTabela.CampoObrigatorio = true;
            objTabela.Tipo = System.Data.DbType.String;
            objAtributos.Add(objTabela);

            objIdentificadorTabela.Campo = "identificador_tabela";
            objIdentificadorTabela.Descricao = "C�digo do registro na tabela que emitiu a notifica��o.";
            objIdentificadorTabela.CampoIdentificador = false;
            objIdentificadorTabela.CampoObrigatorio = true;
            objIdentificadorTabela.Tipo = System.Data.DbType.Int32;
            objAtributos.Add(objIdentificadorTabela);

            objCodigoUsuarioEmissor.Campo = "codigo_usuario_emissor";
            objCodigoUsuarioEmissor.Descricao = "C�digo do usu�rio que emite a notifica��o.";
            objCodigoUsuarioEmissor.CampoIdentificador = false;
            objCodigoUsuarioEmissor.CampoObrigatorio = true;
            objCodigoUsuarioEmissor.Tipo = System.Data.DbType.Int32;
            objAtributos.Add(objCodigoUsuarioEmissor);

            objCodigoUsuarioReceptor.Campo = "codigo_usuario_receptor";
            objCodigoUsuarioReceptor.Descricao = "C�digo do usu�rio que recebe a notifica��o.";
            objCodigoUsuarioReceptor.CampoIdentificador = false;
            objCodigoUsuarioReceptor.CampoObrigatorio = true;
            objCodigoUsuarioReceptor.Tipo = System.Data.DbType.Int32;
            objAtributos.Add(objCodigoUsuarioReceptor);

            objDtInclusao.Campo = "data_inclusao";
            objDtInclusao.Descricao = "Data e hora da inclus�o da notifica��o no banco de dados.";
            objDtInclusao.CampoIdentificador = false;
            objDtInclusao.CampoObrigatorio = true;
            objDtInclusao.Tipo = System.Data.DbType.DateTime;
            objAtributos.Add(objDtInclusao);

            objDtResposta.Campo = "data_resposta";
            objDtResposta.Descricao = "Data e hora da resposta da notifica��o.";
            objDtResposta.CampoIdentificador = false;
            objDtResposta.CampoObrigatorio = false;
            objDtResposta.Tipo = System.Data.DbType.DateTime;
            objAtributos.Add(objDtResposta);

            objFlagAprovado.Campo = "flag_aprovado";
            objFlagAprovado.Descricao = "Flag indicador da aprova��o\n�o aprova��o.";
            objFlagAprovado.CampoIdentificador = false;
            objFlagAprovado.CampoObrigatorio = false;
            objFlagAprovado.Tipo = System.Data.DbType.String;
            objAtributos.Add(objFlagAprovado);

            objJustificativa.Campo = "justificativa";
            objJustificativa.Descricao = "Justificativa";
            objJustificativa.CampoIdentificador = false;
            objJustificativa.CampoObrigatorio = false;
            objJustificativa.Tipo = System.Data.DbType.String;
            objAtributos.Add(objJustificativa);

            objDescricao.Campo = "descricao";
            objDescricao.Descricao = "Descri��o da notifica��o";
            objDescricao.CampoIdentificador = false;
            objDescricao.CampoObrigatorio = true;
            objDescricao.Tipo = System.Data.DbType.String;
            objAtributos.Add(objDescricao);

            objTipo.Campo = "tipo_codigo";
            objTipo.Descricao = "Tipo de Notifica��o";
            objTipo.CampoIdentificador = false;
            objTipo.CampoObrigatorio = false;
            objTipo.Tipo = System.Data.DbType.Int32;
            objAtributos.Add(objTipo);
        }
          catch (Exception ex)
          {
            throw ex;
          }
        }
        #endregion

        #region metodo geraDropDownList
        /// <summary>
        /// Gera um novo DropDownList de acordo com a cole��o de atributos.
        /// </summary>
        /// <param name="objDropDownList">DropDownList</param>
        public static void geraDropDownList(System.Web.UI.WebControls.DropDownList objDropDownList)
        {
          try
          {
            ClsNotificacao objNotificacao = new ClsNotificacao();
            objDropDownList.DataTextField = objNotificacao.objJustificativa.Campo;
            objDropDownList.DataValueField = objNotificacao.objCodigo.Campo;
            ServiceDesk.Controle.ClsDropDownList.geraDropDownList(objDropDownList, objNotificacao.objAtributos);
            objNotificacao = null;

            //Adiciona a op��o default no dropdownlist.
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
        /// Gera uma nova geraGridView de acordo com a cole��o de atributos.
        /// </summary>
        /// <param name="objGridView">objeto gridview</param>
        public void geraGridView(System.Web.UI.WebControls.GridView objGridView)
        {
          try
          {
            objGridView.AutoGenerateColumns = false;
            ClsNotificacao objNotificacao = new ClsNotificacao();
            ServiceDesk.Controle.ClsGridView.geraGridView(objGridView, objNotificacao.objAtributos);
            objNotificacao = null;
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
        public void geraGridView(System.Web.UI.WebControls.GridView objGridView, ClsNotificacao objNotificacao, bool bolCondicao)
        {
          try
          {
            objGridView.AutoGenerateColumns = false;
            ServiceDesk.Controle.ClsGridView.geraGridView(objGridView, objNotificacao.objAtributos, bolCondicao);
          }
          catch (Exception ex)
          {
            throw ex;
          }
        }

        #endregion

        #region GeraGridView
        /// <summary>
        /// Gera grid view com campos especificos de acordo com c�digo do usu�rio atual
        /// </summary>
        /// <param name="objGridView">geraGridView</param>
        /// <param name="param name="CodigoUsuario"> C�digo do usu�rio que esta logado no sistema</param>
        public void geraGridView(System.Web.UI.WebControls.GridView objGridView, int CodigoUsuario)
        {
          try
          {
            objGridView.AutoGenerateColumns = false;

            string strSql = " select notificacao_codigo, tabela as tipo, Pessoa.nome as emissor, Notificacao.data_inclusao, justificativa, Notificacao.descricao, flag_aprovado, identificador_tabela";
            strSql += " from Notificacao, Pessoa";
            strSql += " where Pessoa.pessoa_codigo = Notificacao.codigo_usuario_emissor";
            strSql += " and codigo_usuario_receptor = " + CodigoUsuario + "";
            strSql += " and codigo_usuario_receptor = " + CodigoUsuario + "";
            strSql += " order by flag_aprovado";

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

        #region GeraGridView
        /// <summary>
        /// Gera grid view com campos especificos de acordo com c�digo do usu�rio atual
        /// </summary>
        /// <param name="intCodigoEmissor">C�digo do usu�rio emissor.</param> 
        /// <param name="intTabelaIdentificador">C�digo do registro identificador.</param> 
        /// <param name="objGridView">Nome da Grid View.</param> 
        /// <param name="strTabela">Nome da tabela que representa o processo.</param> 
        public static void geraGridView(System.Web.UI.WebControls.GridView objGridView, string strTabela, int intTabelaIdentificador, int intCodigoEmissor)
        {
            try
            {
                objGridView.AutoGenerateColumns = false;

                string strSql = "select N.*, (select nome from pessoa where pessoa_codigo = N.codigo_usuario_emissor) nome_emissor,";
                strSql += " (select nome from pessoa where pessoa_codigo = N.codigo_usuario_receptor)nome_receptor";
                strSql += " from Notificacao N";
                strSql += " where N.notificacao_codigo is not null";

                if(strTabela != string.Empty) strSql += " and tabela = '"+ strTabela.Trim() +"'";

                if(intTabelaIdentificador > 0 ) strSql +=  " and identificador_tabela = "+ intTabelaIdentificador +"";

                if(intCodigoEmissor > 0 ) strSql += " and codigo_usuario_emissor = "+ intCodigoEmissor +"";

                strSql += " order by flag_aprovado ";

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

        #region Pega o m�ximo c�digo identificador da tabela
        /// <summary>
        /// Pega o pr�ximo registro identificador da tabela.
        /// </summary>
        /// <returns>Retorna um n�mero inteiro com o pr�ximo registro ou zero se apresentar erro.</returns>
        public int GetMaxId()
        {
          try
          {
            string strSql = "select max(notificacao_codigo) as maximo from Notificacao";
            System.Data.SqlClient.SqlDataReader objDtReader = ServiceDesk.Banco.ClsBanco.geraDataReader(strSql);

            if (objDtReader.Read())
            {
              string valor = objDtReader["maximo"].ToString();
              objDtReader.Dispose();
              if (valor == string.Empty)
                  return 1;
              else
                  return Convert.ToInt32(valor) + 1;
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

        #region Envia mensagem de notifica��o
        /// <summary>
        /// Envia mensagem de notifica��o 
        /// </summary>
        /// <param name="objNotificacao">Inst�ncia do objeto que contem a notifica��o a ser enviada</param>
        /// <param name="strTexto">Texto HTML para compor o corpo da mensagem.</param>
        /// <example>
        /// Enviar mensagem com os dados da notifica��o:
        /// ClsNotifica��o.EnviaMensagemNotificacao(meuObjetoNotifica��o)
        /// </example>
        /// <returns>retorna TRUE se conseguiu enviar o email. FALSE se ocorreu erro.</returns>
        public static bool EnviaMensagemNotificacao(ClsNotificacao objNotificacao)
        {
          StringBuilder strCorpo = new StringBuilder();
          bool bEnvioOk;
          String strDataCompletaExibicao = ClsParametro.DataCompletaExibicao;
          String strServidorSMTP = ClsParametro.SMTP;
          String strPortaServidorSMTP = ClsParametro.SMTPPorta;
          String strDataEnvioNotificacao = string.Empty;
          String strDataRespostaNotificacao = string.Empty;
          String strNomeEmissorNotificacao = string.Empty;
          String strEmailEmissorNotificacao = string.Empty;
          String strNomeReceptorNotificacao = string.Empty;
          String strEmailReceptorNotificacao = string.Empty;
          String strDescricaoNotificacao = string.Empty;
          String strIdentificadorTabela = string.Empty;
          String strJustificativa = string.Empty;
          String strNumeroNotificacao = string.Empty;
          String strNomeTabela = string.Empty;
          String strCabecalho = string.Empty;
          String strDatas = string.Empty;
          String strCaminho = string.Empty;
          String strLinkRegistro = string.Empty;

        strNumeroNotificacao = objNotificacao.Codigo.Valor;
        strNomeEmissorNotificacao = ClsUsuario.getNomeUsuario(objNotificacao.CodigoUsuarioEmissor.Valor);
        strEmailEmissorNotificacao = ClsUsuario.getEMailUsuario(objNotificacao.CodigoUsuarioEmissor.Valor);
        strNomeReceptorNotificacao = ClsUsuario.getNomeUsuario(objNotificacao.CodigoUsuarioReceptor.Valor);
        strEmailReceptorNotificacao = ClsUsuario.getEMailUsuario(objNotificacao.CodigoUsuarioReceptor.Valor);
        
        if (objNotificacao.Tabela.Valor.ToLower() == "escalacaohorizontal")
        {
            ClsEscalacaoHorizontal objEscalacaoHorizontal = new ClsEscalacaoHorizontal(Convert.ToInt32(objNotificacao.IdentificadorTabela.Valor));
            strNomeTabela = objEscalacaoHorizontal.Tabela.Valor.ToUpper();
            strIdentificadorTabela = objEscalacaoHorizontal.Identificador.Valor;
            objEscalacaoHorizontal = null;
        }
        else
        {
            strNomeTabela = objNotificacao.Tabela.Valor.ToUpper();
            strIdentificadorTabela = objNotificacao.IdentificadorTabela.Valor;
        }

        //Monta o cabe�alho
        strCabecalho += "<b>Notifica&ccedil;&atilde;o</b> N�mero&nbsp;" + strNumeroNotificacao +".";

        //Datas de Envio e Resposta
        if ((objNotificacao.DtInclusao.Valor != null) && (objNotificacao.DtInclusao.Valor != string.Empty))
        {
            strDataEnvioNotificacao = Convert.ToDateTime(objNotificacao.DtInclusao.Valor).ToString(strDataCompletaExibicao, null);
            strDatas += "<b>Data de Envio:</b>&nbsp;" + strDataEnvioNotificacao + "<br />" ;
        }
        if ((objNotificacao.DtResposta.Valor != null) && (objNotificacao.DtResposta.Valor != string.Empty))
        {
            strDataRespostaNotificacao = Convert.ToDateTime(objNotificacao.DtResposta.Valor).ToString(strDataCompletaExibicao, null);
            strDatas += "<b>Data de Resposta:</b>&nbsp;" + strDataRespostaNotificacao + "<br />";
        }

        //Monta o corpo da mensagem (descricao)
        strDescricaoNotificacao = ServiceDesk.Generica.ClsTexto.trocaHtmlPorAspa(objNotificacao.Descricao.Valor);
        strJustificativa = objNotificacao.Justificativa.Valor;
        if (strJustificativa != string.Empty)
        {
            strDescricaoNotificacao += "<br /><br /><b>Justificativa:</b>&nbsp;<br />" + strJustificativa + "<br />";
        }
        
        //links para a aplicacao e registro.
        strCaminho = System.Web.HttpContext.Current.Server.MapPath(".");

            switch (strNomeTabela.ToLower())
            {
            case "chamado":
            {
                strLinkRegistro = "chamadoOperador.aspx?chamado=" + strIdentificadorTabela;
                break;
            }
            case "incidente":
            {
                strLinkRegistro = "incidente.aspx?incidente=" + strIdentificadorTabela;
                break;
            }
            case "requisicaoservico":
            {
                strLinkRegistro = "requisicaoservico.aspx?requisicaoservico=" + strIdentificadorTabela;
                break;
            }
            case "requisicaomudanca":
            {
                strLinkRegistro = "requisicaomudanca.aspx?requisicaomudanca=" + strIdentificadorTabela;
                break;
            }
            case "itemconfiguracao":
            {
                strLinkRegistro = "itemconfiguracao.aspx?itemconfiguracao=" + strIdentificadorTabela;
                break;
            }

            case "escalacaohorizontal":
            {
                //Quando uma notificacao � do tipo escalacaohorizontal, o identificador aponta para um
                //registro dessa tabela. Esse registro guarda a informacao se essa escala��o foi para
                //chamado, RS, RM ou incidente.
                if (strIdentificadorTabela.Trim() != string.Empty)
                {
                ClsEscalacaoHorizontal objEscalacaoHorizontal = new ClsEscalacaoHorizontal(Convert.ToInt32(strIdentificadorTabela));
                
                if (objEscalacaoHorizontal.Tabela.Valor.ToLower() == "chamado")
                    strLinkRegistro = "chamadoOperador.aspx?chamado=" + objEscalacaoHorizontal.Identificador;

                if (objEscalacaoHorizontal.Tabela.Valor.ToLower() == "incidente")
                    strLinkRegistro = "incidente.aspx?incidente=" + objEscalacaoHorizontal.Identificador;

                if (objEscalacaoHorizontal.Tabela.Valor.ToLower() == "requisicaoservico")
                    strLinkRegistro = "requisicaoservico.aspx?requisicaoservico=" + objEscalacaoHorizontal.Identificador;

                if (objEscalacaoHorizontal.Tabela.Valor.ToLower() == "requisicaomudanca")
                    strLinkRegistro = "requisicaomudanca.aspx?requisicaomudanca=" + objEscalacaoHorizontal.Identificador;

                objEscalacaoHorizontal = null;
                }
                break;
            }
            }

        strDescricaoNotificacao += "<br /><br /><b>Para acessar o registro que originou esta notifica��o, " + "<a href="+ strCaminho + "\\" + strLinkRegistro + " target='_blank'>clique aqui</a>";
        strDescricaoNotificacao += "<br /><b>Para acessar o sistema HELP DESK, " + "<a href=" + strCaminho + " target='_blank'>clique aqui</a>";
        
        //Busca o layout do e-mail no arquivo de modelo.            
        string strMail = ServiceDesk.Generica.ClsArquivo.leArquivo(strCaminho + "\\mail\\mail.htm");

        //Substitui as vari�veis dentro do layout de e-mail        
        strMail = strMail.Replace("%Cabecalho%",strCabecalho.Trim()); 
        strMail = strMail.Replace("%Data%",strDatas.Trim()); 
        strMail = strMail.Replace("%Descricao%",strDescricaoNotificacao.Trim());
        strMail = strMail.Replace("\t", "");

        bEnvioOk = true;        

        //=================================================================================================================//
        // - O que: Envia e-mail se o destinat�rio e se o remetente estiver com o endere�o do e-mail cadastrado no sistema.
        // - Quem: Fernanda Passos
        // - Quando: 16/03/2006 �s 17:15hs
        //=================================================================================================================//
        if (strEmailEmissorNotificacao.Trim() != string.Empty && strEmailReceptorNotificacao.Trim() != string.Empty) ServiceDesk.Generica.ClsEmail.enviaEmail(strServidorSMTP, strPortaServidorSMTP, strEmailEmissorNotificacao, strEmailReceptorNotificacao, "", "", "", "Sistema Help Desk - Notifica��o", strMail, "", "HTML");
        //=================================================================================================================//

        return bEnvioOk;
        }

        #endregion

        #region Envia Notificacao
        /// <summary>
        /// Envia Notificacao
        /// </summary>
        /// <param name="Tabela"></param>
        /// <param name="IdentificadorTabela"></param>
        /// <param name="DataInclusao"></param>
        /// <param name="CodigoUsuarioEmissor"></param>
        /// <param name="CodigoUsuarioReceptor"></param>
        /// <param name="Descricao"></param>
        /// <param name="CodigoNotificacaoGerada"></param>
        /// <returns></returns>
        public static bool EnviaNotificacao(string Tabela, string IdentificadorTabela ,DateTime DataInclusao, string CodigoUsuarioEmissor, string CodigoUsuarioReceptor, string Descricao, out string CodigoNotificacaoGerada)
        {
          string strMensagem = string.Empty;
          CodigoNotificacaoGerada = string.Empty;

          ClsNotificacao objNotificacao = new ClsNotificacao();
          objNotificacao.Tabela.Valor = Tabela;
          objNotificacao.DtInclusao.Valor = DataInclusao.ToString(ClsParametro.DataInclusao);
          objNotificacao.IdentificadorTabela.Valor = IdentificadorTabela;
          objNotificacao.CodigoUsuarioEmissor.Valor = CodigoUsuarioEmissor;
          objNotificacao.CodigoUsuarioReceptor.Valor = CodigoUsuarioReceptor;
          objNotificacao.Descricao.Valor = Descricao;

          ServiceDesk.Negocio.ClsIdentificador objIdentificadorNotificacao = new ServiceDesk.Negocio.ClsIdentificador();
          objIdentificadorNotificacao.Tabela.Valor = "Notificacao";
          objNotificacao.Codigo.Valor = objIdentificadorNotificacao.getProximoValor().ToString();
                  
          //grava a notificacao
          if (objNotificacao.enviar(out strMensagem))
          {
            objIdentificadorNotificacao.atualizaValor();
            CodigoNotificacaoGerada = objNotificacao.Codigo.Valor;
            objNotificacao = null;
            return true;
          }
          else
          {          
            objNotificacao = null;
            return false; 
          }
        }
        
        #endregion
      
        #region existeNotificacaoPendente
        /// <summary>
        /// Para o usuario informado. Retorna se o mesmo possui ou nao notifica��es pendendes
        /// de aprova��o
        /// </summary>
        /// <param name="CodigoUsuario">Codigo do Usuario</param>
        /// <returns>true/false</returns>
        public static bool existeNotificacaoPendente(string CodigoUsuario)
        {
          bool existePendente = false;
          try
          {  
            string strSql = " select notificacao_codigo, tabela as tipo, Pessoa.nome as emissor, Notificacao.data_inclusao, justificativa, Notificacao.descricao, flag_aprovado";
            strSql += " from Notificacao, Pessoa";
            strSql += " where Pessoa.pessoa_codigo = Notificacao.codigo_usuario_emissor";
            strSql += " and codigo_usuario_receptor = " + CodigoUsuario + "";
            strSql += " AND Flag_aprovado IS null ";
            strSql += " order by flag_aprovado";

            SqlDataReader objReader = ServiceDesk.Banco.ClsBanco.geraDataReader(strSql);
            if (objReader.HasRows)
            {
              existePendente =  true;
            }           
          }
          catch (Exception ex)
          {           
            throw ex;            
          }

          return existePendente;

        }
        #endregion

      #endregion
    }
}
