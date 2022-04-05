using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ServiceDesk.Banco;

/// <summary>
/// Summary description for ClsLog
/// </summary>
namespace ServiceDesk.Negocio
{
    public class ClsLog
    {
        #region Declara��o
        //Colecao de atributos do Log de Status
        private ServiceDesk.Banco.ClsAtributos objAtributos = new ServiceDesk.Banco.ClsAtributos();

        //Atributos de um Log de Status
        private ServiceDesk.Banco.ClsAtributo objCodigo = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objDescricao = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objPessoa = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objTipoLog = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objData = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objOrigem = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objCodigoIdentificador = new ServiceDesk.Banco.ClsAtributo();
        #endregion

        #region Propriedades

        public enum enumTipoLog : int
        {
            UPDATE = 0,
            INSERT = 1,
            DELETE = 2,
            SELECT = 3,
            ACESSO = 4,
            EDIT = 5,
            ERRO = 6
        }

        public ServiceDesk.Banco.ClsAtributos Atributos
        {
            get { return this.objAtributos; }
        }

        /// <summary>
        /// C�digo
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo Codigo
        {
            get { return objCodigo; }
            set { this.objCodigo = value; }
        }

        /// <summary>
        /// Descri��o
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo Descricao
        {
            get { return objDescricao; }
            set { this.objDescricao = value; }
        }

        /// <summary>
        /// Pessoa 
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo Pessoa
        {
            get { return objPessoa; }
            set { this.objPessoa = value; }
        }

        /// <summary>
        /// StatusOrigem
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo TipoLog
        {
            get { return objTipoLog; }
            set { this.objTipoLog = value; }
        }

        /// <summary>
        /// Data
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo Data
        {
            get { return objData; }
            set { this.objData = value; }
        }

        /// <summary>
        /// Tabela
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo Origem
        {
            get { return objOrigem; }
            set { this.objOrigem = value; }
        }

        /// <summary>
        /// Identificador
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo Identificador
        {
          get { return objCodigoIdentificador; }
          set { this.objCodigoIdentificador = value; }
        }

        #endregion

        #region M�todos

        #region Construtor da classe
        /// <summary>
        /// Construtor da Classe
        /// </summary>
        public ClsLog()
        {
        this.alimentaColecaoCampos();
        }
        #endregion

        #region alimentaColecaoCampos

        /// <summary>
        /// M�todo que alimenta a cole��o de atributos
        /// </summary>
        private void alimentaColecaoCampos()
        {
            objAtributos.NomeTabela = "LogEvento";
            objAtributos.DescricaoTabela = "Log de Eventos";

            objCodigo.Campo = "log_evento_codigo";
            objCodigo.Descricao = "C�digo";
            objCodigo.CampoIdentificador = true;
            objCodigo.CampoObrigatorio = true;
            objCodigo.Tipo = System.Data.DbType.Int32;
            objAtributos.Add(objCodigo);

            objDescricao.Campo = "descricao";
            objDescricao.Descricao = "Descri��o";
            objDescricao.CampoIdentificador = false;
            objDescricao.CampoObrigatorio = true;
            objDescricao.Tipo = System.Data.DbType.String;
            objAtributos.Add(objDescricao);

            objData.Campo = "data";
            objData.Descricao = "Data";
            objData.CampoIdentificador = false;
            objData.CampoObrigatorio = true;
            objData.Tipo = System.Data.DbType.DateTime;
            objAtributos.Add(objData);

            objPessoa.Campo = "pessoa_codigo";
            objPessoa.Descricao = "Pessoa";
            objPessoa.CampoIdentificador = false;
            objPessoa.CampoObrigatorio = true;
            objPessoa.Tipo = System.Data.DbType.Int32;
            objAtributos.Add(objPessoa);

            objTipoLog.Campo = "evento_tipo";
            objTipoLog.Descricao = "Tipo de Evento";
            objTipoLog.CampoIdentificador = false;
            objTipoLog.CampoObrigatorio = true;
            objTipoLog.Tipo = System.Data.DbType.String;
            objAtributos.Add(objTipoLog);

            objOrigem.Campo = "origem";
            objOrigem.Descricao = "Origem do Evento";
            objOrigem.CampoIdentificador = false;
            objOrigem.CampoObrigatorio = true;
            objOrigem.Tipo = System.Data.DbType.String;
            objAtributos.Add(objOrigem);

            objCodigoIdentificador.Campo = "identificador";
            objCodigoIdentificador.Descricao = "Codigo Identificador do Registro";
            objCodigoIdentificador.CampoIdentificador = false;
            objCodigoIdentificador.CampoObrigatorio = true;
            objCodigoIdentificador.Tipo = System.Data.DbType.Int32;
            objAtributos.Add(objCodigoIdentificador);

        }
        #endregion

        #region metodo insereLog
        /// <summary>
        /// M�todo que insere um novo registro de Log por Tipo Pre definido.
        /// </summary>
        /// <param name="intTipoLog">Tipo de Log a ser Registrado</param>
        /// <param name="strPessoaCodigo">C�digo da Pessoa que est� executando o evento</param>
        /// <param name="strOrigem">Origem de onde foi acessado no evento</param>
        /// <param name="strIdentificadorTabela">Identificador do registro da tabela acessada ("0" para log de acesso a p�ginas)</param>
        /// <param name="strDescricaoLog">Descri��o complementar do evento (Ex. Descricao das altera��es efetuadas em um registro no banco)</param>
        /// <returns>Retorna true ou false. Se o registro foi inserido ou n�o.</returns>
        public static bool insereLog(enumTipoLog intTipoLog, string strPessoaCodigo, string strOrigem, string strIdentificadorTabela, string strDescricaoLog)
        {
            string strMensagem = string.Empty;
            string strTipoLog = string.Empty;
            bool bolRetorno = false;

            if (strPessoaCodigo != string.Empty)
            {
                ClsLog objLog = new ClsLog();

                ServiceDesk.Negocio.ClsIdentificador objIdentificador = new ServiceDesk.Negocio.ClsIdentificador();
                objIdentificador.Tabela.Valor = objLog.objAtributos.NomeTabela;
                
                objLog.objCodigo.Valor = objIdentificador.getProximoValor().ToString();

                switch (intTipoLog)
                {
                    case enumTipoLog.ACESSO:
                        {
                            strTipoLog = "ACESSO";
                            break;
                        }
                    case enumTipoLog.DELETE:
                        {
                            strTipoLog = "DELETE";
                            break;
                        }
                    case enumTipoLog.INSERT:
                        {
                            strTipoLog = "INSERT";
                            break;
                        }
                    case enumTipoLog.SELECT:
                        {
                            strTipoLog = "SELECT";
                            break;
                        }
                    case enumTipoLog.UPDATE:
                        {
                            strTipoLog = "UPDATE";
                            break;
                        }
                    case enumTipoLog.EDIT:
                        {
                            strTipoLog = "EDIT";
                            break;
                        }
                    case enumTipoLog.ERRO:
                        {
                            strTipoLog = "ERRO";
                            break;
                        }
                }

                objLog.objDescricao.Valor = strDescricaoLog;
                objLog.objPessoa.Valor = strPessoaCodigo;
                objLog.objTipoLog.Valor = strTipoLog;
                objLog.objData.Valor = DateTime.Now.ToString(ClsParametro.DataInclusao);
                objLog.objOrigem.Valor = strOrigem;
                
                if (strIdentificadorTabela.Trim() != string.Empty)
                { objLog.objCodigoIdentificador.Valor = strIdentificadorTabela.Trim(); }
                else
                { objLog.objCodigoIdentificador.Valor = "0"; }

                if (objLog.insere(out strMensagem))
                {
                    //Atualizando o valor na tabela identificador
                    objIdentificador.atualizaValor();
                    bolRetorno = true;
                }

                objIdentificador = null;
                objLog = null;
            }
            return bolRetorno;
        }


        /// <summary>
        /// M�todo que insere um novo registro de Log por Tipo Pre definido.
        /// </summary>
        /// <param name="intTipoLog">Tipo de Log a ser Registrado</param>
        /// <param name="strPessoaCodigo">C�digo da Pessoa que est� executando o evento</param>
        /// <param name="strOrigem">Origem de onde foi acessado no evento</param>
        /// <param name="strIdentificadorTabela">Identificador do registro da tabela acessada ("0" para log de acesso a p�ginas)</param>
        /// <param name="AtributosRegistroAntigo">Registro antigo</param>
        /// <param name="AtributosRegistroAtualizado">Registro atualizado</param>
        /// <returns>Retorna true ou false. Se o registro foi inserido ou n�o.</returns>
        public static bool insereLog(enumTipoLog intTipoLog, string strPessoaCodigo, string strOrigem, string strIdentificadorTabela, ClsAtributos AtributosRegistroAtualizado, ClsAtributos AtributosRegistroAntigo)
        {
          string strMensagem = string.Empty;
          string strDescricaoLog = string.Empty;
          string strTipoLog = string.Empty;
          bool bolRetorno = false;

          if (strPessoaCodigo != string.Empty)
          {
              ClsLog objLog = new ClsLog();
              ServiceDesk.Negocio.ClsIdentificador objIdentificador = new ServiceDesk.Negocio.ClsIdentificador();

              objIdentificador.Tabela.Valor = objLog.objAtributos.NomeTabela;
              objLog.objCodigo.Valor = objIdentificador.getProximoValor().ToString();

              switch (intTipoLog)
              {
                  case enumTipoLog.ACESSO:
                      {
                          strTipoLog = "ACESSO";
                          break;
                      }
                  case enumTipoLog.DELETE:
                      {
                          strTipoLog = "DELETE";
                          break;
                      }
                  case enumTipoLog.INSERT:
                      {
                          strTipoLog = "INSERT";
                          break;
                      }
                  case enumTipoLog.SELECT:
                      {
                          strTipoLog = "SELECT";
                          break;
                      }
                  case enumTipoLog.UPDATE:
                      {
                          strTipoLog = "UPDATE";
                          break;
                      }
                  case enumTipoLog.EDIT:
                      {
                          strTipoLog = "EDIT";
                          break;
                      }
                  case enumTipoLog.ERRO:
                      {
                          strTipoLog = "ERRO";
                          break;
                      }

              }


              String[] strAlteracoes = new String[AtributosRegistroAtualizado.Count];
              strAlteracoes = ClsLog.comparaRegistro(AtributosRegistroAtualizado, AtributosRegistroAntigo);
              for (int contador = 0; contador < strAlteracoes.GetUpperBound(0); contador++)
              {
                  strDescricaoLog += strAlteracoes.GetValue(contador);
              }
              strAlteracoes = null;

              if (strDescricaoLog.Trim() != string.Empty)
              {
                  objLog.objDescricao.Valor = strDescricaoLog;
                  objLog.objPessoa.Valor = strPessoaCodigo;
                  objLog.objTipoLog.Valor = strTipoLog;
                  objLog.objData.Valor = DateTime.Now.ToString(ClsParametro.DataInclusao);
                  objLog.objOrigem.Valor = strOrigem;
                  if (strIdentificadorTabela.Trim() != string.Empty)
                  { objLog.objCodigoIdentificador.Valor = strIdentificadorTabela.Trim(); }
                  else
                  { objLog.objCodigoIdentificador.Valor = "0"; }

                  if (objLog.insere(out strMensagem))
                  {
                      //Atualizando o valor na tabela identificador
                      objIdentificador.atualizaValor();
                      bolRetorno = true;
                  }
              }

              objIdentificador = null;
              objLog = null;
          }
          return bolRetorno;
        }

        #endregion

        #region metodo insere
        /// <summary>
        /// M�todo que insere um novo registro de Log.
        /// </summary>
        /// <param name="strMensagem">Mensagem com informa��o da execu��o do m�todo.</param>
        /// <returns>Retorna true ou false. Se o registro foi inserido ou n�o.</returns>
        public bool insere(out String strMensagem)
        {
          strMensagem = String.Empty;
          bool bolRetorno = true;

          if (this.objOrigem.Valor.Trim() == String.Empty)
          {
              strMensagem = "Favor informar a Origem do Item.";
          }
          else
          {
              ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
              if (objBanco.insereColecao(this.objAtributos))
              {
                  strMensagem = "Item inserido com sucesso.";
                  bolRetorno = true;
              }
              objBanco = null;
          }

          return bolRetorno;
        }
        #endregion

        #region metodo altera
        /// <summary>
        /// M�todo que altera um log
        /// </summary>
        /// <returns>Retorna true ou false. Se o registro foi alterado ou n�o.</returns>
        public bool altera(out String strMensagem)
        {

            strMensagem = String.Empty;
            bool bolRetorno = false;

            if (this.objDescricao.Valor.Trim() == String.Empty)
            {
                strMensagem = "Favor informar a Descri��o do Tipo.";
            }
            else
            {
                ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
                if (objBanco.alteraColecao(this.objAtributos))
                {
                    strMensagem = "Item atualizado com sucesso.";
                    bolRetorno = true;
                }
                objBanco = null;
            }

            return bolRetorno;
        }
        #endregion

        #region metodo exclui
        /// <summary>
        /// M�todo que exclui um registro de log
        /// </summary>
        /// <returns>Retorna true ou false. Se o registro foi excluido ou n�o.</returns>
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

        #region metodo geraGridView
        /// <summary>
        /// Gera uma nova geraGridView de acordo com a cole��o de atributos.
        /// </summary>
        /// <param name="objGridView">geraGridView</param>
        public static void geraGridView(System.Web.UI.WebControls.GridView objGridView)
        {
          objGridView.AutoGenerateColumns = false;
          ClsLog objLog = new ClsLog();
          ServiceDesk.Controle.ClsGridView.geraGridView(objGridView, objLog.objAtributos);
          objLog = null;
        }
        #endregion

        #region metodo geraGridView
        /// <summary>
        /// Gera uma nova geraGridView de acordo com a cole��o de atributos.
        /// </summary>
        /// <param name="objGridView">geraGridView</param>
        /// <param name="bolCondicao">Condi��o para verificar se ser� utilizado a cl�usula Where</param>
        public static void geraGridView(System.Web.UI.WebControls.GridView objGridView, ClsLog objLog, bool bolCondicao)
        {
          objGridView.AutoGenerateColumns = false;
          ServiceDesk.Controle.ClsGridView.geraGridView(objGridView, objLog.objAtributos, bolCondicao);
        }
        #endregion

        #region metodo geraDropDownList
        /// <summary>
        /// Gera um novo DropDownList de acordo com a cole��o de atributos.
        /// </summary>
        /// <param name="objDropDownList">DropDownList</param>
        public static void geraDropDownList(System.Web.UI.WebControls.DropDownList objDropDownList)
        {
          ClsLog objLog = new ClsLog();
          objDropDownList.DataTextField = objLog.objDescricao.Campo;
          objDropDownList.DataValueField = objLog.Codigo.Campo;
          ServiceDesk.Controle.ClsDropDownList.geraDropDownList(objDropDownList, objLog.objAtributos);
          objLog = null;

          //Adiciona a op��o default no dropdownlist.
          ListItem itemDefault = new ListItem();
          itemDefault.Text = "--";
          itemDefault.Value = "";
          itemDefault.Selected = true;
          objDropDownList.Items.Insert(0, itemDefault);
        }
        #endregion

        #region metodo comparaRegistro
        /// <summary>
        /// Compara duas cole��es de atributos retornando um vetor de string com as altera��es (diferen�as)
        /// entre os itens da cole��o.
        /// </summary>
        /// <param name="objAtributosRegistroAtualizado">Cole��o de atributos do registro Atualizado</param>
        /// <param name="objAtributosRegistroAntigo">Cole��o de atributos do registro antes da Altera��o.</param>
        /// <returns>Vetor de String</returns>
        public static string[] comparaRegistro(ClsAtributos AtributosRegistroAtualizado, ClsAtributos AtributosRegistroAntigo)
        {
          string[] strRetorno = new string[AtributosRegistroAtualizado.Count];
          int intI = 0;

          for (intI = 0; intI < AtributosRegistroAtualizado.Count; intI++)
          {
              if ( ((AtributosRegistroAtualizado[intI].Valor.Trim()) != (AtributosRegistroAntigo[intI].Valor.Trim())) && (AtributosRegistroAtualizado[intI].VerificaAlteracao) )
              {
                  strRetorno[intI] += "O campo " + AtributosRegistroAntigo[intI].Campo + " foi alterado de \"" + AtributosRegistroAntigo[intI].Valor.Trim() + "\" para \"" + AtributosRegistroAtualizado[intI].Valor.Trim() + "\".";
              }
          }

          return strRetorno;
        }
        #endregion
        
#endregion

    }
}