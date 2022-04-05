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
using ServiceDesk.Banco;

/// <summary>
/// Classe ClsItemConfiguracao
/// </summary>
/// 
namespace ServiceDesk.Negocio
{
    public class ClsItemConfiguracao
    {
        #region Declarações Publicas na classe
        //Colecao de atributos de Item de Configuracao
        private ServiceDesk.Banco.ClsAtributos objAtributos = new ServiceDesk.Banco.ClsAtributos();

        //Atributos de um Item de Configuracao
        private ServiceDesk.Banco.ClsAtributo objCodigo = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objSuperior = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objTipo = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objJanela = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objSuporte = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objNumero = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objNome = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objDescricao = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objVersao = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objStatusAtual = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objStatusProgramado = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objFlagModelo = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objChave = new ServiceDesk.Banco.ClsAtributo();
        private ServiceDesk.Banco.ClsAtributo objInternoTI = new ServiceDesk.Banco.ClsAtributo();
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
        /// Código.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo Codigo
        {
            get { return objCodigo; }
            set { this.objCodigo = value; }
        }

        /// <summary>
        /// Código do Item Superior.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo Superior
        {
            get { return objSuperior; }
            set { this.objSuperior = value; }
        }

        /// <summary>
        /// Tipo do Item.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo Tipo
        {
            get { return objTipo; }
            set { this.objTipo = value; }
        }

        /// <summary>
        /// Tempo da janela de serviço.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo Janela
        {
            get { return objJanela; }
            set { this.objJanela = value; }
        }

        /// <summary>
        /// Tempo do suporte.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo Suporte
        {
            get { return objSuporte; }
            set { this.objSuporte = value; }
        }

        /// <summary>
        /// Código do Item Superior.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo Numero
        {
            get { return objNumero; }
            set { this.objNumero = value; }
        }

        /// <summary>
        /// Código do Item Superior.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo Nome
        {
            get { return objNome; }
            set { this.objNome = value; }
        }

        /// <summary>
        /// Descrição do Item.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo Descricao
        {
            get { return objDescricao; }
            set { this.objDescricao = value; }
        }

        /// <summary>
        /// Código do Item Superior.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo Versao
        {
            get { return objVersao; }
            set { this.objVersao = value; }
        }

        /// <summary>
        /// Status atual do item
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo StatusAtual
        {
            get { return objStatusAtual; }
            set { this.objStatusAtual = value; }
        }

        /// <summary>
        /// Status programado do item
        /// </summary>
        /// 
        public ServiceDesk.Banco.ClsAtributo StatusProgramado
        {
            get { return objStatusProgramado; }
            set { this.objStatusProgramado = value; }
        }

        /// <summary>
        /// Flag modelo do Item.
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo FlagModelo
        {
            get { return objFlagModelo; }
            set { this.objFlagModelo = value; }
        }

        /// <summary>
        /// Chave de tratamento (caminho na arvore) do item
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo Chave
        {
            get { return objChave; }
            set { this.objChave = value; }
        }

        /// <summary>
        /// Indica o IC como Interno da TI
        /// </summary>
        public ServiceDesk.Banco.ClsAtributo InternoTI
        {
            get { return objInternoTI; }
            set { this.objInternoTI = value; }
        }


        #endregion

        #region Métodos

        #region alimentaColecaoCampos

        /// <summary>
        /// Método que alimenta a coleção de atributos
        /// </summary>
        private void alimentaColecaoCampos()
        {
            objAtributos.NomeTabela = "IC";
            objAtributos.DescricaoTabela = "Item de Configuração";

            objCodigo.Campo = "ic_codigo";
            objCodigo.Descricao = "Código";
            objCodigo.CampoIdentificador = true;
            objCodigo.CampoObrigatorio = true;
            objCodigo.Tipo = System.Data.DbType.Int32;
            objAtributos.Add(objCodigo);

            objSuperior.Campo = "ic_codigo_superior";
            objSuperior.Descricao = "Código Superior";
            objSuperior.CampoIdentificador = false;
            objSuperior.CampoObrigatorio = true;
            objSuperior.Tipo = System.Data.DbType.Int32;
            objAtributos.Add(objSuperior);

            objTipo.Campo = "ic_tipo_codigo";
            objTipo.Descricao = "Tipo";
            objTipo.CampoIdentificador = false;
            objTipo.CampoObrigatorio = true;
            objTipo.Tipo = System.Data.DbType.Int32;
            objAtributos.Add(objTipo);

            objJanela.Campo = "janela_servico";
            objJanela.Descricao = "Janela de Serviço";
            objJanela.CampoIdentificador = false;
            objJanela.CampoObrigatorio = true;
            objJanela.Tipo = System.Data.DbType.Int32;
            objAtributos.Add(objJanela);

            objSuporte.Campo = "suporte";
            objSuporte.Descricao = "Suporte";
            objSuporte.CampoIdentificador = false;
            objSuporte.CampoObrigatorio = true;
            objSuporte.Tipo = System.Data.DbType.Int32;
            objAtributos.Add(objSuporte);

            objNumero.Campo = "numero";
            objNumero.Descricao = "Número";
            objNumero.CampoIdentificador = false;
            objNumero.CampoObrigatorio = true;
            objNumero.Tipo = System.Data.DbType.String;
            objAtributos.Add(objNumero);

            objNome.Campo = "nome";
            objNome.Descricao = "Nome";
            objNome.CampoIdentificador = false;
            objNome.CampoObrigatorio = true;
            objNome.Tipo = System.Data.DbType.String;
            objAtributos.Add(objNome);

            objDescricao.Campo = "descricao";
            objDescricao.Descricao = "Descrição";
            objDescricao.CampoIdentificador = false;
            objDescricao.CampoObrigatorio = false;
            objDescricao.Tipo = System.Data.DbType.String;
            objAtributos.Add(objDescricao);

            objVersao.Campo = "versao";
            objVersao.Descricao = "Versão";
            objVersao.CampoIdentificador = false;
            objVersao.CampoObrigatorio = false;
            objVersao.Tipo = System.Data.DbType.String;
            objAtributos.Add(objVersao);

            objStatusAtual.Campo = "status_atual";
            objStatusAtual.Descricao = "Status Atual";
            objStatusAtual.CampoIdentificador = false;
            objStatusAtual.CampoObrigatorio = false;
            objStatusAtual.Tipo = System.Data.DbType.String;
            objAtributos.Add(objStatusAtual);

            objStatusProgramado.Campo = "status_programado";
            objStatusProgramado.Descricao = "Status";
            objStatusProgramado.CampoIdentificador = false;
            objStatusProgramado.CampoObrigatorio = false;
            objStatusProgramado.Tipo = System.Data.DbType.String;
            objAtributos.Add(objStatusProgramado);

            objFlagModelo.Campo = "flag_modelo";
            objFlagModelo.Descricao = "Flag Modelo";
            objFlagModelo.CampoIdentificador = false;
            objFlagModelo.CampoObrigatorio = false;
            objFlagModelo.Tipo = System.Data.DbType.String;
            objAtributos.Add(objFlagModelo);

            objChave.Campo = "chave";
            objChave.Descricao = "Chave";
            objChave.CampoIdentificador = false;
            objChave.CampoObrigatorio = false;
            objChave.Tipo = System.Data.DbType.String;
            objAtributos.Add(objChave);

            objInternoTI.Campo = "interno_ti";
            objInternoTI.Descricao = "Interno TI";
            objInternoTI.CampoIdentificador = false;
            objInternoTI.CampoObrigatorio = false;
            objInternoTI.Tipo = System.Data.DbType.String;
            objAtributos.Add(objInternoTI);


        }
        #endregion

        #region Construtor da Classe
        /// <summary>
        /// Construtor da classe
        /// </summary>
        public ClsItemConfiguracao()
        {
            this.alimentaColecaoCampos();
        }
        #endregion

        #region metodo Construtor da classe com passagem de parametro
        /// <summary>
        /// Construtor da classe
        /// </summary>
        public ClsItemConfiguracao(int intCodigo)
        {
            try
            {
                this.alimentaColecaoCampos();
                this.objCodigo.Valor = intCodigo.ToString();
                ClsBanco objBanco = new ClsBanco();
                objBanco.alimentaColecao(this.objAtributos);
                objBanco = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region metodo insere
        /// <summary>
        /// Método que insere um novo Item de Configuração.
        /// </summary>
        /// <param name="strMensagem">Mensagem com informação da execução do método.</param>
        /// <returns>Retorna true ou false. Se o registro foi inserido ou não.</returns>
        public bool insere(out String strMensagem)
        {
            try
            {
                strMensagem = String.Empty;
                bool bolRetorno = false;

                if (this.objNome.Valor.Trim() == String.Empty)
                {
                    strMensagem = "Favor informar o Nome do Item de Configuração.";
                }
                else if (this.objTipo.Valor.Trim() == String.Empty)
                {
                    strMensagem = "Favor informar o Tipo do Item de Configuração.";
                }
                else if (this.objNumero.Valor.Trim() == String.Empty)
                {
                    strMensagem = "Favor informar o Número do Item de Configuração.";
                }
                else if (ServiceDesk.Negocio.ClsItemConfiguracao.retornaCodigoItemPorTipoNumero(0, Convert.ToInt32(this.objTipo.Valor), this.objNumero.Valor) != 0)
                {
                    strMensagem = "Já existe um Item de Configuração com o prefixo e o número informado.";
                }
                else
                {
                    ClsBanco objBanco = new ClsBanco();
                    if (objBanco.insereColecao(this.objAtributos))
                    {
                        strMensagem = "Item inserido com sucesso.";
                        bolRetorno = true;
                    }
                    objBanco = null;
                }

                return bolRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region metodo insereSemNumero
        /// <summary>
        /// Método que insere um novo Item de Configuração. Não verifica o número do item.
        /// </summary>
        /// <param name="strMensagem">Mensagem com informação da execução do método.</param>
        /// <returns>Retorna true ou false. Se o registro foi inserido ou não.</returns>
        private bool insereSemNumero(out String strMensagem)
        {
            try
            {
                strMensagem = String.Empty;
                bool bolRetorno = false;

                if (this.objNome.Valor.Trim() == String.Empty)
                {
                    strMensagem = "Favor informar o Nome do Item de Configuração.";
                }
                else if (this.objTipo.Valor.Trim() == String.Empty)
                {
                    strMensagem = "Favor informar o Tipo do Item de Configuração.";
                }
                else
                {
                    ClsBanco objBanco = new ClsBanco();
                    if (objBanco.insereColecao(this.objAtributos))
                    {
                        strMensagem = "Item inserido com sucesso.";
                        bolRetorno = true;
                    }
                    objBanco = null;
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
        /// <param name="objGridView">objeto gridview</param>
        public static void geraGridView(System.Web.UI.WebControls.GridView objGridView)
        {
            try
            {
                objGridView.AutoGenerateColumns = false;
                ClsItemConfiguracao objItemConfiguracao = new ClsItemConfiguracao();
                ServiceDesk.Controle.ClsGridView.geraGridView(objGridView, objItemConfiguracao.objAtributos);
                objItemConfiguracao = null;
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
        /// <param name="objGridView">objjeto Grid View</param>
        /// <param name="bolCondicao">Condição para verificar se será utilizado a cláusula Where</param>
        public static void geraGridView(System.Web.UI.WebControls.GridView objGridView, ClsItemConfiguracao objItemConfiguracao, bool bolCondicao)
        {
            try
            {
                objGridView.AutoGenerateColumns = false;
                ServiceDesk.Controle.ClsGridView.geraGridView(objGridView, objItemConfiguracao.objAtributos, bolCondicao);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region metodo altera
        /// <summary>
        /// Método que altera um Item de Configuração
        /// </summary>
        /// <returns>Retorna true ou false. Se o registro foi alterado ou não.</returns>
        public bool altera(out String strMensagem)
        {
            try
            {
                strMensagem = String.Empty;
                bool bolRetorno = false;

                if (this.objNome.Valor.Trim() == String.Empty)
                {
                    strMensagem = "Favor informar o Nome do Item de Configuração.";
                }
                else if (this.objNumero.Valor.Trim() == String.Empty)
                {
                    strMensagem = "Favor informar o Número do Item de Configuração.";
                }
                else if (ServiceDesk.Negocio.ClsItemConfiguracao.retornaCodigoItemPorTipoNumero(Convert.ToInt32(this.objCodigo.Valor), Convert.ToInt32(this.objTipo.Valor), this.objNumero.Valor) != 0)
                {
                    strMensagem = "Já existe um Item de Configuração com o prefixo e o número informado.";
                }
                else
                {
                    //string strDescricaoAnterior = objDescricao.Valor;

                    ClsBanco objBanco = new ClsBanco();
                    if (objBanco.alteraColecao(this.objAtributos))
                    {
                        strMensagem += "Item atualizado com sucesso.<br />";

                        #region Envia notificação de alteração para pessoas relacionadas ao item

                        ClsNotificacao objNotificacao = new ClsNotificacao();
                        objNotificacao.Tabela.Valor = "IC";
                        objNotificacao.DtInclusao.Valor = DateTime.Now.ToString(ClsParametro.DataInclusao);
                        objNotificacao.IdentificadorTabela.Valor = objCodigo.Valor;
                        objNotificacao.CodigoUsuarioEmissor.Valor = ClsUsuario.getCodigoUsuario().ToString();
                        objNotificacao.Descricao.Valor = Generica.ClsTexto.trocaAspaPorHtml("O Item de configuração #" + objCodigo.Valor + " com a descricao '" + objNome.Valor + "' foi alterado no sistema pelo usuário '" + ClsUsuario.getNomeUsuario(objNotificacao.CodigoUsuarioEmissor.Valor) + "'");

                        ClsIdentificador objIdentificadorNotificacao = new ClsIdentificador();
                        objIdentificadorNotificacao.Tabela.Valor = "Notificacao";

                        SqlDataReader dr = getPessoasRelacaoItem(Convert.ToInt32(objCodigo.Valor));
                        while (dr.Read())
                        {
                            if (dr["email"].ToString() != String.Empty || dr["email"].ToString() != "null")
                            {
                                objNotificacao.Codigo.Valor = objIdentificadorNotificacao.getProximoValor().ToString();
                                objNotificacao.CodigoUsuarioReceptor.Valor = dr["pessoa_codigo"].ToString();

                                //grava a notificacao no banco
                                if (objNotificacao.enviar(out strMensagem))
                                {
                                    objIdentificadorNotificacao.atualizaValor();

                                    //envia o mensagem de notificacao;
                                    ClsNotificacao.EnviaMensagemNotificacao(objNotificacao);
                                }
                            }
                        }

                        dr.Close();
                        dr.Dispose();
                        objIdentificadorNotificacao = null;
                        objNotificacao = null;

                        #endregion

                        bolRetorno = true;
                    }
                    objBanco = null;
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
        /// Método que exclui um Item de Configuração
        /// </summary>
        /// <returns>Retorna true ou false. Se o registro foi excluido ou não.</returns>
        public bool exclui()
        {
            try
            {
                string strMensagem = string.Empty;

                //Valida a exclusão.
                //if (ServiceDesk.Negocio.ClsIdentificador.ValidaExclusao(objCodigo.Campo, objCodigo.Valor.Trim(), out strMsg, true, false, objAtributos.NomeTabela.Trim()) == false) return false;

                ClsBanco objBanco = new ClsBanco();
                if (objBanco.apagaColecao(this.objAtributos))
                {
                    #region Envia notificação de exclusão para pessoas relacionadas ao item

                    ClsNotificacao objNotificacao = new ClsNotificacao();
                    objNotificacao.Tabela.Valor = "IC";
                    objNotificacao.DtInclusao.Valor = DateTime.Now.ToString(ClsParametro.DataInclusao);
                    objNotificacao.IdentificadorTabela.Valor = objCodigo.Valor;
                    objNotificacao.CodigoUsuarioEmissor.Valor = ClsUsuario.getCodigoUsuario().ToString();
                    objNotificacao.Descricao.Valor = Generica.ClsTexto.trocaAspaPorHtml("O Item de configuração #" + objCodigo.Valor + " com a descricao '" + objNome.Valor + "' foi excluido do sistema pelo usuário '" + ClsUsuario.getNomeUsuario(objNotificacao.CodigoUsuarioEmissor.Valor) + "'");

                    ClsIdentificador objIdentificadorNotificacao = new ClsIdentificador();
                    objIdentificadorNotificacao.Tabela.Valor = "Notificacao";

                    SqlDataReader dr = getPessoasRelacaoItem(Convert.ToInt32(objCodigo.Valor));
                    while (dr.Read())
                    {
                        if (dr["email"].ToString() != String.Empty || dr["email"].ToString() != "null")
                        {
                            objNotificacao.Codigo.Valor = objIdentificadorNotificacao.getProximoValor().ToString();
                            objNotificacao.CodigoUsuarioReceptor.Valor = dr["pessoa_codigo"].ToString();

                            //grava a notificacao no banco
                            if (objNotificacao.enviar(out strMensagem))
                            {
                                objIdentificadorNotificacao.atualizaValor();

                                //envia o mensagem de notificacao;
                                ClsNotificacao.EnviaMensagemNotificacao(objNotificacao);
                            }
                        }
                    }

                    dr.Close();
                    dr.Dispose();
                    objIdentificadorNotificacao = null;
                    objNotificacao = null;

                    #endregion

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

        #region Retorna as pessoas que estão relacionadas ao item de configuração
        /// <summary>
        /// Método que pega as pessoas relacionadas a um item de configuração.
        /// </summary>
        /// <param name="intCodigoItemConfiguracao">Código do Item de configuração</param>
        /// <returns>Retorna um DataReader com matricula, nome e email dos usuários relacionados ao item.</returns>
        public SqlDataReader getPessoasRelacaoItem(int intCodigoItemConfiguracao)
        {
            try
            {
                SqlDataReader dr;
                String strSql = String.Empty;

                strSql = "SELECT p.pessoa_codigo, p.matricula, p.nome, p.email FROM ICrelacaopessoa ic, pessoa p ";
                strSql += "WHERE ic.ic_codigo = " + intCodigoItemConfiguracao + " AND p.pessoa_codigo = ic.pessoa_codigo";

                dr = ServiceDesk.Banco.ClsBanco.geraDataReader(strSql);

                return dr;
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
                ClsItemConfiguracao objItemConfiguracao = new ClsItemConfiguracao();
                objDropDownList.DataTextField = objItemConfiguracao.objNome.Campo;
                objDropDownList.DataValueField = objItemConfiguracao.objCodigo.Campo;

                ClsBanco objBanco = new ClsBanco();
                strSql = objBanco.montaQuery(objItemConfiguracao.objAtributos, false);

                strSql += " ORDER BY nome";
                System.Data.DataSet objDataSet = ServiceDesk.Banco.ClsBanco.geraDataSet(strSql);
                objDropDownList.DataSource = objDataSet;
                objDropDownList.DataBind();
                objDataSet.Dispose();
                objDataSet = null;

                objItemConfiguracao = null;

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

        #region metodo atualizaChave
        /// <summary>
        /// Método responsavel por montar a chave do Item de Configuração
        /// </summary>
        /// <returns>Retorna uma String com a chave do Item de Configuração</returns>
        public void atualizaChave()
        {
            try
            {
                this.objChave.Valor = this.objCodigo.Valor;
                if ((this.objCodigo.Valor != null) && (this.objCodigo.Valor != ""))
                {
                    if ((this.objSuperior.Valor != null) && (this.objSuperior.Valor != String.Empty))
                    {
                        String strSql = String.Empty;
                        strSql = "SELECT chave FROM IC";
                        strSql += " WHERE ic_codigo = " + this.objSuperior.Valor;
                        System.Data.SqlClient.SqlDataReader objDateReader = ServiceDesk.Banco.ClsBanco.geraDataReader(strSql);
                        if (objDateReader.Read())
                        {
                            if (objDateReader[0] != null)
                            {
                                this.objChave.Valor = objDateReader[0].ToString() + "," + this.objCodigo.Valor.ToString();
                            }
                        }
                        objDateReader.Dispose();
                        objDateReader = null;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region metodo atualizaChaveFilhos
        /// <summary>
        /// Método que atualiza a chave dos itens filhos de um determinado item
        /// </summary>
        /// <param name="intCodigoPai">Código do Pai dos itens que serão atualizados</param>
        public static void atualizaChaveFilhos(int intCodigoPai)
        {
            try
            {
                String strSql = String.Empty;

                strSql = "SELECT ic_codigo,ic_codigo_superior FROM IC";
                strSql += " WHERE ic_codigo_superior = " + intCodigoPai.ToString();
                System.Data.SqlClient.SqlDataReader objDateReader = ServiceDesk.Banco.ClsBanco.geraDataReader(strSql);

                while (objDateReader.Read())
                {

                    strSql = "UPDATE IC";
                    strSql += " SET chave = ";
                    strSql += " (SELECT chave FROM IC WHERE ic_codigo = " + objDateReader["ic_codigo_superior"].ToString() + ") + ',' + LTRIM(STR(ic_codigo))";
                    strSql += " WHERE ic_codigo = " + objDateReader["ic_codigo"].ToString();
                    ClsBanco objBanco = new ClsBanco();
                    objBanco.executaSQL(strSql);
                    objBanco = null;

                    atualizaChaveFilhos(Convert.ToInt32(objDateReader["ic_codigo"].ToString()));

                }

                objDateReader.Dispose();
                objDateReader = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region metodo insereTipoAtributoValor
        /// <summary>
        /// Insere ou atualiza um determinado Item de Configuração Atributo Valor
        /// </summary>
        /// <param name="intCodigoItem">Código do Item de Configuração</param>
        /// <param name="intCodigoAtributo">Código do Atributo</param>
        /// <param name="strValor">Valor do Atributo</param>
        /// <returns>Verdadeiro ou Falso (true/false)</returns>
        public static void insereTipoAtributoValor(int intCodigoItem, int intCodigoAtributo, String strValor)
        {
            try
            {
                ClsItemConfiguracao objItemConfiguracao = new ClsItemConfiguracao(intCodigoItem);

                ClsBanco objBanco = new ClsBanco();
                if (objBanco.retornaValorCampo("ICTipoAtributoValor", "ic_codigo", "ic_codigo = " + intCodigoItem.ToString() + " AND ic_atributo_codigo = " + intCodigoAtributo.ToString()) == String.Empty)
                {
                    objItemConfiguracao.insereTipoAtributoValor(intCodigoAtributo, strValor);
                }
                else
                {
                    objItemConfiguracao.alteraTipoAtributoValor(intCodigoAtributo, strValor);
                }

                objBanco = null;
                objItemConfiguracao = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region metodo insereTipoAtributoValor
        /// <summary>
        /// Insere um novo relacionamento entre Item de Configuração e Atributo
        /// </summary>
        /// <param name="intCodigoAtributo">Código do Atributo</param>
        /// <param name="strValor">Valor do Atributo</param>
        /// <returns>Verdadeiro ou Falso (true/false)</returns>
        private bool insereTipoAtributoValor(int intCodigoAtributo, String strValor)
        {
            try
            {
                String strSql = String.Empty;
                bool bolRetorno = false;

                strSql = "INSERT INTO ICTipoAtributoValor";
                strSql += " VALUES";
                strSql += " (" + this.Codigo.Valor + ", " + intCodigoAtributo.ToString();
                strSql += ", '" + ServiceDesk.Generica.ClsTexto.trocaAspaPorHtml(strValor) + "')";

                ClsBanco objBanco = new ClsBanco();
                if (objBanco.executaSQL(strSql))
                {
                    bolRetorno = true;
                }
                objBanco = null;

                return bolRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region metodo alteraTipoAtributoValor
        /// <summary>
        /// Altera um relacionamento entre Item de Configuração e Atributo
        /// </summary>
        /// <param name="intCodigoItem">Código do Item de Configuração</param>
        /// <param name="intCodigoAtributo">Código do Atributo</param>
        /// <param name="strValor">Valor do Atributo</param>
        /// <returns>Verdadeiro ou Falso (true/false)</returns>
        private bool alteraTipoAtributoValor(int intCodigoAtributo, String strValor)
        {
            try
            {
                String strSql = String.Empty;
                bool bolRetorno = false;

                strSql = "UPDATE ICTipoAtributoValor SET ";
                strSql += " valor = '" + ServiceDesk.Generica.ClsTexto.trocaAspaPorHtml(strValor) + "'";
                strSql += " WHERE ic_codigo = " + this.Codigo.Valor;
                strSql += " AND ic_atributo_codigo = " + intCodigoAtributo.ToString();

                ClsBanco objBanco = new ClsBanco();
                if (objBanco.executaSQL(strSql))
                {
                    bolRetorno = true;
                }
                objBanco = null;

                return bolRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region metodo retornaTipoAtributoValor
        /// <summary>
        /// Retorna o o valor do atributo relacionado com o Item de Configuração
        /// </summary>
        /// <param name="intCodigoItem">Código do Item de Configuração</param>
        /// <param name="intCodigoAtributo">Código do Atributo</param>
        /// <returns>String com o valor do Atributo</returns>
        public static String retornaTipoAtributoValor(int intCodigoItem, int intCodigoAtributo)
        {
            try
            {
                String strRetorno = String.Empty;
                ClsItemConfiguracao objItemConfiguracao = new ClsItemConfiguracao(intCodigoItem);

                ClsBanco objBanco = new ClsBanco();
                strRetorno = objBanco.retornaValorCampo("ICTipoAtributoValor", "valor", "ic_codigo = " + intCodigoItem.ToString() + " AND ic_atributo_codigo = " + intCodigoAtributo.ToString());
                objBanco = null;
                objItemConfiguracao = null;

                return strRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region metodo geraGridViewRelacao
        /// <summary>
        /// Método que gera um Grid View de acordo com o Tipo do Atributo do Item de Configuração
        /// </summary>
        /// <param name="objGridView">ObjetoGridView</param>
        /// <param name="intCodigoItemTipo">Código do Tipo do Item</param>
        public static void geraGridRelacao(GridView objGridView, int intCodigoItem)
        {
            try
            {
                String strSql = String.Empty;

                strSql = " select ic_codigo_destino, ";
                strSql += " (select descricao from ICRelacaoTipo where ic_relacao_tipo_codigo = ICRelacao.ic_relacao_tipo_codigo) descricao  ,";
                strSql += " ic_relacao_tipo_codigo, ic_codigo_origem";
                strSql += " from ICRelacao";
                strSql += " where ic_codigo_origem = " + intCodigoItem.ToString();

                System.Data.DataSet objDataSet = ServiceDesk.Banco.ClsBanco.geraDataSet(strSql);

                objGridView.DataSource = objDataSet;
                objGridView.DataBind();

                objDataSet.Dispose();
                objDataSet = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region metodo geraGridViewAuditado
        /// <summary>
        /// Método que gera um Grid View de acordo com o Tipo do Atributo do Item de Configuração
        /// </summary>
        /// <param name="objGridView">ObjetoGridView</param>
        /// <param name="intCodigoItemTipo">Código do Tipo do Item</param>
        public static void geraGridViewAuditado(GridView objGridView, int intCodigoTipo)
        {
            try
            {
                String strSql = String.Empty;

                strSql = "SELECT ic_codigo,ic_tipo_codigo,numero,nome,status_atual,auditado.*";
                strSql += " FROM IC,auditado";
                strSql += " WHERE tabela_identificador =* ic_codigo";
                if (intCodigoTipo > 0)
                {
                    strSql += " AND ic_tipo_codigo = " + intCodigoTipo.ToString();
                }
                strSql += " ORDER BY auditado_codigo DESC";

                System.Data.DataSet objDataSet = ServiceDesk.Banco.ClsBanco.geraDataSet(strSql);

                objGridView.DataSource = objDataSet;
                objGridView.DataBind();

                objDataSet.Dispose();
                objDataSet = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region metodo geraDropDownListItem
        /// <summary>
        /// Gera o DropDownList dos itens de configuração com excessão do item que tem o código igual ao passado pelo parametro
        /// </summary>
        /// <param name="objDropDownList">Objeto DropDownList</param>
        /// <param name="intCodigoItem">Código do Item que não será listado</param>
        public static void geraDropDownListItem(DropDownList objDropDownList, int intCodigoItem)
        {
            try
            {

                String strSql = String.Empty;
                objDropDownList.Items.Clear();
                ClsItemConfiguracao objItemConfiguracao = new ClsItemConfiguracao();
                objDropDownList.DataTextField = objItemConfiguracao.Nome.Campo;
                objDropDownList.DataValueField = objItemConfiguracao.Codigo.Campo;

                ClsBanco objBanco = new ClsBanco();
                strSql = objBanco.montaQuery(objItemConfiguracao.objAtributos, false);

                strSql += " ORDER BY nome";
                System.Data.DataSet objDataSet = ServiceDesk.Banco.ClsBanco.geraDataSet(strSql);
                objDropDownList.DataSource = objDataSet;
                objDropDownList.DataBind();
                objDataSet.Dispose();
                objDataSet = null;

                objDropDownList.Items.FindByValue(intCodigoItem.ToString()).Enabled = false;
                objItemConfiguracao = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region metodo insereItemConfiguracaoRelacao
        /// <summary>
        /// Método que insere a relação entre os itens
        /// </summary>
        /// <param name="intCodigoOrigem">Código do Item de Origem</param>
        /// <param name="intCodigoDestino">Código do Item de Destino</param>
        /// <param name="intCodigoRelacaoTipo">Códito do Tipo de Relação</param>
        /// <returns>Verdadeiro ou Falso</returns>
        public static bool insereItemConfiguracaoRelacao(int intCodigoOrigem, int intCodigoDestino, int intCodigoRelacaoTipo)
        {
            try
            {
                String strSql = String.Empty;
                bool bolRetorno = false;

                ClsBanco objBanco = new ClsBanco();
                String strRetono = objBanco.retornaValorCampo("ICrelacao", "ic_codigo_origem", " ic_codigo_origem = " + intCodigoOrigem.ToString() + " AND ic_codigo_destino = " + intCodigoDestino.ToString());

                if (strRetono == String.Empty)
                {
                    //pode inserir
                    strSql = "INSERT INTO ICRelacao";
                    strSql += " VALUES";
                    strSql += " (" + intCodigoOrigem.ToString() + ", " + intCodigoDestino.ToString() + ",";
                    strSql += intCodigoRelacaoTipo + ")";
                    if (objBanco.executaSQL(strSql))
                    {
                        bolRetorno = true;
                    }
                }

                objBanco = null;

                return bolRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region metodo insereItemConfiguracaoEstrutura
        /// <summary>
        /// Método que insere a relação entre os itens (Item Configuracao x Estrutura Organizacional x Estrutura Organizacional Tipo)
        /// </summary>
        /// <param name="intCodigoItem">Código do Item de Configuração</param>
        /// <param name="intCodigoEstrutura">Código da Estrutura Organizacional</param>
        /// <param name="intCodigoEstruturaTipo">Códito do Tipo da Estrutura Organizacional</param>
        /// <returns>Verdadeiro ou Falso</returns>
        public static bool insereItemConfiguracaoEstrutura(int intCodigoItem, int intCodigoEstrutura, int intCodigoEstruturaTipo)
        {
            try
            {
                String strSql = String.Empty;
                bool bolRetorno = false;

                ClsBanco objBanco = new ClsBanco();
                String strRetono = objBanco.retornaValorCampo("ICestruturaorganizacional", "ic_codigo", " ic_codigo = " + intCodigoItem.ToString() + " AND estrutura_codigo = " + intCodigoEstrutura.ToString() + " AND ic_estrutura_tipo_codigo = " + intCodigoEstruturaTipo.ToString());

                if (strRetono == String.Empty)
                {
                    strSql = "INSERT INTO ICEstruturaOrganizacional";
                    strSql += " VALUES";
                    strSql += " (" + intCodigoItem.ToString() + ", " + intCodigoEstrutura.ToString() + ",";
                    strSql += intCodigoEstruturaTipo.ToString() + ")";
                    if (objBanco.executaSQL(strSql))
                    {
                        bolRetorno = true;
                    }
                }

                objBanco = null;

                return bolRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region metodo excluiItemConfiguracaoEstrutura
        /// <summary>
        /// Método que exclui a relação entre os itens (Item Configuracao x Estrutura Organizacional x Estrutura Organizacional Tipo)
        /// </summary>
        /// <param name="intCodigoItem">Código do Item de Configuração</param>
        /// <param name="intCodigoEstruturaTipo">Códito do Tipo da Estrutura Organizacional</param>
        /// <param name="intCodigoItemEstrutura">Código do Tipo do Item de Configuração Estrutura Organizacional</param>
        /// <returns>Verdadeiro ou Falso</returns>
        public static bool excluiItemConfiguracaoEstrutura(int intCodigoItem, int intCodigoEstruturaTipo, int intCodigoItemEstrutura)
        {
            try
            {
                String strSql = String.Empty;
                bool bolRetorno = false;

                strSql = "DELETE FROM ICEstruturaOrganizacional";
                strSql += " WHERE ic_codigo = " + intCodigoItem.ToString();
                strSql += " AND ic_estrutura_tipo_codigo = " + intCodigoItemEstrutura.ToString();
                strSql += " AND estrutura_codigo IN (SELECT estrutura_codigo FROM estruturaorganizacional WHERE tipo_estrutura_codigo = " + intCodigoEstruturaTipo.ToString() + ")";
                ClsBanco objBanco = new ClsBanco();
                if (objBanco.executaSQL(strSql))
                {
                    bolRetorno = true;
                }

                objBanco = null;

                return bolRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region metodo retornaItemEstruturaTipo
        /// <summary>
        /// Pega o codigo do Tipo da Estrutura Organizacional do Item de Configuração
        /// </summary>
        /// <param name="intCodigoItem">Código do Item de Configuração</param>
        /// <param name="intCodigoEstrutura">Código da Estrutura Organizacional</param>
        /// <param name="intCodigoEstruturaTipo">Código do Tipo da Estrutura Organizacional</param>
        /// <returns>Retorna um inteiro com o código do Tipo da Estrutura Organizacional do Item de Configuração, se não encontrar retorna 0.</returns>
        public static int retornaItemEstruturaTipo(int intCodigoItem, int intCodigoEstrutura, int intCodigoEstruturaTipo)
        {
            try
            {
                int intRetorno = 0;
                String strRetorno = String.Empty;

                ClsBanco objBanco = new ClsBanco();
                strRetorno = objBanco.retornaValorCampo("ICEstruturaOrganizacional", "ic_estrutura_tipo_codigo", "ic_codigo = " + intCodigoItem.ToString() + " AND estrutura_codigo = " + intCodigoEstrutura.ToString() + " AND ic_estrutura_tipo_codigo = " + intCodigoEstruturaTipo.ToString());
                if (strRetorno != String.Empty)
                {
                    intRetorno = Convert.ToInt32(strRetorno);
                }
                objBanco = null;

                return intRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region metodo retornaItemPessoaTipo
        /// <summary>
        /// Pega o codigo do Tipo da Pessoa do Item de Configuração
        /// </summary>
        /// <param name="intCodigoItem">Código do Item de Configuração</param>
        /// <param name="intCodigoPessoa">Código da Pessoa</param>
        /// <returns>Retorna um inteiro com o código do Tipo da Pessoa do Item de Configuração, se não encontrar retorna 0.</returns>
        public static int retornaItemPessoaTipo(int intCodigoItem, int intCodigoPessoa, int intCodigoItemPessoa)
        {
            try
            {
                int intRetorno = 0;
                String strRetorno = String.Empty;

                ClsBanco objBanco = new ClsBanco();
                strRetorno = objBanco.retornaValorCampo("ICRelacaoPessoa", "ic_pessoa_tipo_codigo", "ic_codigo = " + intCodigoItem.ToString() + " AND pessoa_codigo = " + intCodigoPessoa.ToString() + " AND ic_pessoa_tipo_codigo = " + intCodigoItemPessoa.ToString());
                if (strRetorno != String.Empty)
                {
                    intRetorno = Convert.ToInt32(strRetorno);
                }
                objBanco = null;

                return intRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region metodo insereItemConfiguracaoPessoa
        /// <summary>
        /// Método que insere a relação entre os itens (Item Configuracao x Pessoa x Pessoa Tipo)
        /// </summary>
        /// <param name="intCodigoItem">Código do Item de Configuração</param>
        /// <param name="intCodigoPessoa">Código da Pessoa</param>
        /// <param name="intCodigoPessoaTipo">Códito do Tipo da Pessoa</param>
        /// <returns>Verdadeiro ou Falso</returns>
        public static bool insereItemConfiguracaoPessoa(int intCodigoItem, int intCodigoPessoa, int intCodigoPessoaTipo)
        {
            try
            {
                String strSql = String.Empty;
                bool bolRetorno = false;

                ClsBanco objBanco = new ClsBanco();
                String strRetono = objBanco.retornaValorCampo("ICrelacaopessoa", "ic_codigo", " ic_codigo = " + intCodigoItem.ToString() + " AND pessoa_codigo = " + intCodigoPessoa.ToString() + " AND ic_pessoa_tipo_codigo = " + intCodigoPessoaTipo.ToString());

                if (strRetono == String.Empty)
                {
                    strSql = "INSERT INTO ICRelacaoPessoa";
                    strSql += " VALUES";
                    strSql += " (" + intCodigoItem.ToString() + ", " + intCodigoPessoa.ToString() + ",";
                    strSql += intCodigoPessoaTipo.ToString() + ")";
                    if (objBanco.executaSQL(strSql))
                    {
                        bolRetorno = true;
                    }
                }

                objBanco = null;
                return bolRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region metodo excluiItemConfiguracaoPessoa
        /// <summary>
        /// Método que exclui a relação entre os itens (Item Configuracao x Pessoa x Pessoa Tipo)
        /// </summary>
        /// <param name="intCodigoItem">Código do Item de Configuração</param>
        /// <param name="intCodigoItemPessoa">Código do Tipo de Pessoa do Item de Configuração</param>
        /// <returns>Verdadeiro ou Falso</returns>
        public static bool excluiItemConfiguracaoPessoa(int intCodigoItem, int intCodigoItemPessoa)
        {
            try
            {
                String strSql = String.Empty;
                bool bolRetorno = false;

                strSql = "DELETE FROM ICRelacaoPessoa";
                strSql += " WHERE ic_codigo = " + intCodigoItem.ToString();
                strSql += " AND ic_pessoa_tipo_codigo = " + intCodigoItemPessoa.ToString();
                ClsBanco objBanco = new ClsBanco();
                if (objBanco.executaSQL(strSql))
                {
                    bolRetorno = true;
                }

                objBanco = null;

                return bolRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region metodo retornaItemRelacaoPai
        /// <summary>
        /// Retorna o código do Tipo de Relação (Pai) do Item de Configuração
        /// </summary>
        /// <returns></returns>
        public static int retornaItemRelacaoPai()
        {
            try
            {
                int intRetorno = 0;
                try
                {
                    intRetorno = Convert.ToInt32(ClsParametro.ItemRelacaoTipoPai);
                }
                catch
                {
                }
                return intRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region metodo retornaItemRelacaoFilho
        /// <summary>
        /// Retorna o código do Tipo de Relação (Filho) do Item de Configuração
        /// </summary>
        /// <returns></returns>
        public static int retornaItemRelacaoFilho()
        {
            try
            {
                int intRetorno = 0;
                try
                {
                    intRetorno = Convert.ToInt32(ClsParametro.ItemRelacaoTipoFilho);
                }
                catch
                {
                }
                return intRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region metodo atualizaRelacaoPaiFilho
        /// <summary>
        /// Metodo que atualiza o relacionamento entre itens pai e filho
        /// </summary>
        /// <param name="intCodigoPai"></param>
        /// <param name="intCodigoFilho"></param>
        /// <returns></returns>
        public static bool atualizaRelacaoPaiFilho(int intCodigoPai, int intCodigoFilho)
        {
            try
            {
                bool bolRetorno = false;
                String strSql = String.Empty;
                String strRetorno = String.Empty;

                ClsBanco objBanco = new ClsBanco();

                //Deletando a relação de pai
                strSql = "DELETE FROM ICRelacao";
                strSql += " WHERE ic_codigo_destino = " + intCodigoFilho.ToString();
                strSql += " AND ic_relacao_tipo_codigo = " + ServiceDesk.Negocio.ClsItemConfiguracao.retornaItemRelacaoPai().ToString();
                objBanco.executaSQL(strSql);

                //Deletando a relação de filho
                strSql = "DELETE FROM ICRelacao";
                strSql += " WHERE ic_codigo_origem = " + intCodigoFilho.ToString();
                strSql += " AND ic_relacao_tipo_codigo = " + ServiceDesk.Negocio.ClsItemConfiguracao.retornaItemRelacaoFilho().ToString();
                objBanco.executaSQL(strSql);

                //Atualizando o ItemSuperio
                ServiceDesk.Negocio.ClsItemConfiguracao objItemConfiguracao = new ClsItemConfiguracao(intCodigoFilho);
                objItemConfiguracao.Superior.Valor = intCodigoPai.ToString();
                objItemConfiguracao.altera(out strRetorno);
                objItemConfiguracao = null;

                //Inserindo a relação de pai
                strSql = "INSERT INTO ICRelacao";
                strSql += " (ic_codigo_origem,ic_codigo_destino,ic_relacao_tipo_codigo)";
                strSql += " VALUES";
                strSql += " (" + intCodigoPai.ToString() + ", " + intCodigoFilho.ToString();
                strSql += ", " + ServiceDesk.Negocio.ClsItemConfiguracao.retornaItemRelacaoPai().ToString() + ")";
                objBanco.executaSQL(strSql);

                if (objBanco.retornaValorCampo("ICRelacao", "ic_codigo_origem", " ic_codigo_origem = " + intCodigoFilho.ToString() + " AND ic_relacao_tipo_codigo = " + ServiceDesk.Negocio.ClsItemConfiguracao.retornaItemRelacaoFilho().ToString()) == String.Empty)
                {
                    //Inserindo a relação de filho
                    strSql = "INSERT INTO ICRelacao";
                    strSql += " (ic_codigo_origem,ic_codigo_destino,ic_relacao_tipo_codigo)";
                    strSql += " VALUES";
                    strSql += " (" + intCodigoFilho.ToString() + ", " + intCodigoPai.ToString();
                    strSql += ", " + ServiceDesk.Negocio.ClsItemConfiguracao.retornaItemRelacaoFilho().ToString() + ")";
                    objBanco.executaSQL(strSql);
                }

                objBanco = null;

                return bolRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region metodo retornaCodigoItemPorTipoNumero
        /// <summary>
        /// Retorna o Código do Item de Configuração de acordo com o código do tipo do item de configuração e seu número
        /// </summary>
        /// <param name="intCodigoTipoItem">Código do Item de Configuração</param>
        /// <param name="strNumero">Número do Item de Configuração</param>
        /// <returns>Retorna o número do Item ou Zero(0) caso não encontrou</returns>
        public static int retornaCodigoItemPorTipoNumero(int intCodigoItem, int intCodigoTipoItem, String strNumero)
        {
            try
            {
                String strRetorno = String.Empty;

                ClsBanco objBanco = new ClsBanco();
                strRetorno = objBanco.retornaValorCampo("IC", "ic_codigo", " ic_tipo_codigo = " + intCodigoTipoItem.ToString() + " AND numero = '" + strNumero.Trim() + "'" + " AND ic_codigo <> " + intCodigoItem);
                objBanco = null;

                if (strRetorno == String.Empty) strRetorno = "0";

                return Convert.ToInt32(strRetorno);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region metodo retornaAlteracao
        /// <summary>
        /// Método que retorna um vetor de String com as alterações ocorridas
        /// Ps. Esse método pode ser melhorado e adicionado a framework
        /// </summary>
        /// <param name="objItemConfiguracao">Novo objeto</param>
        /// <returns>Vetor de String</returns>
        public String[] retornaAlteracao(ClsItemConfiguracao objItemConfiguracao)
        {
            try
            {
                String[] strRetorno = new String[this.Atributos.Count];
                int intI = 0;

                for (intI = 0; intI < this.Atributos.Count; intI++)
                {
                    if ((this.Atributos[intI].Valor.Trim()) != (objItemConfiguracao.Atributos[intI].Valor.Trim()))
                    {
                        strRetorno[intI] += "O campo " + this.Atributos[intI].Campo + " foi alterado de \"" + this.Atributos[intI].Valor.Trim() + "\" para \"" + objItemConfiguracao.Atributos[intI].Valor.Trim() + "\".<br>";
                    }
                }

                return strRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region metodo copiaItem
        /// <summary>
        /// Copia um determinado item e seus relacionamentos
        /// </summary>
        /// <returns>Verdadeiro ou falso</returns>
        public int copiaItem(int intCodigoPai, ClsItemConfiguracao objItemConfiguracaoOriginal, out String strMensagem)
        {
            try
            {
                int intRetorno = 0;
                String strMensagemAltera = String.Empty;
                String strSql = String.Empty;
                System.Data.SqlClient.SqlDataReader objDataReader = null;

                strMensagem = String.Empty;

                //Verificando a premissa básica para realização da cópia
                if (objItemConfiguracaoOriginal.Codigo.Valor != String.Empty)
                {
                    ClsItemConfiguracao objItemConfiguracao = new ClsItemConfiguracao(Convert.ToInt32(objItemConfiguracaoOriginal.Codigo.Valor));
                    ClsIdentificador objIdentificador = new ClsIdentificador();

                    objIdentificador.Tabela.Valor = objItemConfiguracao.Atributos.NomeTabela;
                    objItemConfiguracao.Codigo.Valor = objIdentificador.getProximoValor().ToString();

                    if (intCodigoPai != 0)
                    {
                        objItemConfiguracao.Superior.Valor = intCodigoPai.ToString();
                    }

                    objItemConfiguracao.Nome.Valor = "Cópia de " + objItemConfiguracaoOriginal.Nome.Valor;
                    objItemConfiguracao.Numero.Valor = "";

                    if (objItemConfiguracao.insereSemNumero(out strMensagem))
                    {
                        //Atualizando o valor na tabela identificador
                        objIdentificador.atualizaValor();

                        //Atualizando a chave do Item de Configuracao
                        objItemConfiguracao.atualizaChave();
                        objItemConfiguracao.altera(out strMensagemAltera);

                        //Atualizando as relacoes de pai e filho
                        if (objItemConfiguracao.Superior.Valor != String.Empty)
                        {
                            ServiceDesk.Negocio.ClsItemConfiguracao.atualizaRelacaoPaiFilho(Convert.ToInt32(objItemConfiguracao.Superior.Valor), Convert.ToInt32(objItemConfiguracao.Codigo.Valor));
                        }

                        intRetorno = Convert.ToInt32(objItemConfiguracao.Codigo.Valor);

                        //Inserindo os relacionamentos com os atributos
                        strSql = "SELECT ic_atributo_codigo,valor";
                        strSql += " FROM ICTipoAtributoValor";
                        strSql += " WHERE ic_codigo = " + objItemConfiguracaoOriginal.Codigo.Valor;
                        objDataReader = ServiceDesk.Banco.ClsBanco.geraDataReader(strSql);
                        while (objDataReader.Read())
                        {
                            try
                            {
                                ClsItemConfiguracao.insereTipoAtributoValor(intRetorno, Convert.ToInt32(objDataReader["ic_atributo_codigo"].ToString()), objDataReader["valor"].ToString());
                            }
                            catch
                            {
                            }
                        }

                        //Inserindo os relacionamentos com os itens de configuracao
                        strSql = "SELECT ic_codigo_destino, ic_relacao_tipo_codigo";
                        strSql += " FROM ICRelacao";
                        strSql += " WHERE ic_codigo_origem = " + objItemConfiguracaoOriginal.Codigo.Valor;
                        strSql += " AND ic_relacao_tipo_codigo <> " + ClsItemConfiguracao.retornaItemRelacaoPai().ToString();
                        strSql += " AND ic_relacao_tipo_codigo <> " + ClsItemConfiguracao.retornaItemRelacaoFilho().ToString();
                        objDataReader = ServiceDesk.Banco.ClsBanco.geraDataReader(strSql);
                        while (objDataReader.Read())
                        {
                            try
                            {
                                ClsItemConfiguracao.insereItemConfiguracaoRelacao(intRetorno, Convert.ToInt32(objDataReader["ic_codigo_destino"].ToString()), Convert.ToInt32(objDataReader["ic_relacao_tipo_codigo"].ToString()));
                            }
                            catch
                            {
                            }
                        }

                        //Inserindo os relacionamentos com as estruturas
                        strSql = "SELECT estrutura_codigo, ic_estrutura_tipo_codigo";
                        strSql += " FROM ICEstruturaOrganizacional";
                        strSql += " WHERE ic_codigo = " + objItemConfiguracaoOriginal.Codigo.Valor;
                        objDataReader = ServiceDesk.Banco.ClsBanco.geraDataReader(strSql);
                        while (objDataReader.Read())
                        {
                            try
                            {
                                ClsItemConfiguracao.insereItemConfiguracaoEstrutura(intRetorno, Convert.ToInt32(objDataReader["estrutura_codigo"].ToString()), Convert.ToInt32(objDataReader["ic_estrutura_tipo_codigo"].ToString()));
                            }
                            catch
                            {
                            }
                        }

                        //Inserindo os relacionamentos com as pessoas
                        strSql = "SELECT pessoa_codigo, ic_pessoa_tipo_codigo";
                        strSql += " FROM ICRelacaoPessoa";
                        strSql += " WHERE ic_pessoa_tipo_codigo = " + objItemConfiguracaoOriginal.Codigo.Valor;
                        objDataReader = ServiceDesk.Banco.ClsBanco.geraDataReader(strSql);
                        while (objDataReader.Read())
                        {
                            try
                            {
                                ClsItemConfiguracao.insereItemConfiguracaoPessoa(intRetorno, Convert.ToInt32(objDataReader["pessoa_codigo"].ToString()), Convert.ToInt32(objDataReader["ic_pessoa_tipo_codigo"].ToString()));
                            }
                            catch
                            {
                            }
                        }

                    }

                    ClsBanco objBanco = new ClsBanco();
                    strSql = "SELECT ic_codigo";
                    strSql += " FROM IC";
                    strSql += " WHERE ic_codigo_superior = " + objItemConfiguracaoOriginal.Codigo.Valor;

                    objIdentificador = null;
                    objItemConfiguracao = null;

                    objDataReader = ServiceDesk.Banco.ClsBanco.geraDataReader(strSql);
                    while (objDataReader.Read())
                    {
                        try
                        {
                            ClsItemConfiguracao objItemNovo = new ClsItemConfiguracao(Convert.ToInt32(objDataReader["ic_codigo"].ToString()));
                            copiaItem(intRetorno, objItemNovo, out strMensagem);
                            objItemNovo = null;
                        }
                        catch
                        {
                        }
                    }

                }

                objDataReader.Dispose();
                objDataReader = null;

                return intRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region metodo retornaCodigoItemRaiz
        /// <summary>
        /// Retorna o código do Item que não tem pai, ou seja, o maior item de sua árvore
        /// </summary>
        /// <param name="intCodigoItem"></param>
        /// <returns></returns>
        public static int retornaCodigoItemRaiz(int intCodigoItem)
        {
            try
            {
                int intRetorno = 0;
                String strChave = String.Empty;

                ClsBanco objBanco = new ClsBanco();
                strChave = objBanco.retornaValorCampo("IC", "chave", "ic_codigo = " + intCodigoItem.ToString());

                if (strChave != String.Empty)
                {
                    String[] vetChave = strChave.Split(',');
                    intRetorno = Convert.ToInt32(vetChave[0].ToString());
                }

                objBanco = null;
                return intRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Método que popula nós raiz do item de configuração por tipo
        /// <summary>
        /// Método que popula nós raiz do item de configuração por tipo
        /// </summary>
        /// <param name="intCodigoTipoIC">Código do tipo de IC</param>
        /// <param name="objTreeView">Nome da treeview</param>
        public static void PopulaNoRaizPorTipoDeIC(int intCodigoTipoIC, TreeView objTreeView)
        {
            try
            {
                String strSql = String.Empty;

                strSql = "SELECT ic_codigo, ic_codigo_superior, nome";
                strSql += " , (SELECT count(*) FROM IC WHERE ic_codigo_superior = item.ic_codigo) pai";
                strSql += " FROM IC item, ICTipo ICT";
                strSql += " where item.ic_tipo_codigo = ICT.ic_tipo_codigo";
                if (intCodigoTipoIC > 0) strSql += " and item.ic_tipo_codigo = " + intCodigoTipoIC + "";
                if (intCodigoTipoIC <= 0) strSql += " and item.ic_tipo_codigo is not null";
                strSql += " ORDER BY nome";

                System.Data.SqlClient.SqlDataReader objDataReader = ServiceDesk.Banco.ClsBanco.geraDataReader(strSql);

                objTreeView.Nodes.Clear();

                populaNos(objDataReader, objTreeView.Nodes);

                objDataReader = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region metodo populaNoRaiz
        /// <summary>
        /// Método que popula os nós que não possuem pai
        /// </summary>
        public static void populaNoRaiz(int intCodigoItem, TreeView trvItemConfiguracao)
        {
            try
            {
                String strSql = String.Empty;

                strSql = "SELECT ic_codigo, ic_codigo_superior, nome";
                strSql += ", (SELECT count(*) FROM IC WHERE ic_codigo_superior = item.ic_codigo) pai";

                strSql += " FROM IC item";
                if (intCodigoItem > 0)
                {
                    strSql += " WHERE ic_codigo = " + intCodigoItem.ToString();
                }
                else
                {
                    strSql += " WHERE ic_codigo_superior is null";
                }
                strSql += " ORDER BY nome";

                System.Data.SqlClient.SqlDataReader objDataReader = ServiceDesk.Banco.ClsBanco.geraDataReader(strSql);

                trvItemConfiguracao.Nodes.Clear();

                populaNos(objDataReader, trvItemConfiguracao.Nodes);

                objDataReader.Dispose();
                objDataReader = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region metodo populaNos
        /// <summary>
        /// Método que popula os nós
        /// </summary>
        public static void populaNos(System.Data.SqlClient.SqlDataReader objDataReader, TreeNodeCollection objTreeNodeCollection)
        {
            try
            {
                while (objDataReader.Read())
                {
                    TreeNode objTreeNode = new TreeNode();
                    objTreeNode.Text = "<a class=\"menu\" href=\"itemconfiguracao.aspx?codigo=" + objDataReader["ic_codigo"].ToString() + "\">" + objDataReader["nome"].ToString() + "</a>";
                    objTreeNode.Value = objDataReader["ic_codigo"].ToString().Trim();
                    if (Convert.ToInt32(objDataReader["pai"]) > 0)
                    {
                        objTreeNode.PopulateOnDemand = true;
                    }
                    objTreeNodeCollection.Add(objTreeNode);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region metodo populaSubNivel
        /// <summary>
        /// Método que popula os nós filhos da TreeView
        /// </summary>
        /// <param name="intCodigoPai"></param>
        /// <param name="objTreeNode"></param>
        public static void populaSubNivel(int intCodigoPai, TreeNode objTreeNodePai)
        {
            try
            {
                String strSql = String.Empty;

                strSql = "SELECT ic_codigo, ic_codigo_superior, nome";
                strSql += ", (SELECT count(*) FROM IC WHERE ic_codigo_superior = item.ic_codigo) pai";
                strSql += " FROM IC item";
                strSql += " WHERE ic_codigo_superior = " + intCodigoPai.ToString();
                strSql += " ORDER BY nome";

                System.Data.SqlClient.SqlDataReader objDataReader = ServiceDesk.Banco.ClsBanco.geraDataReader(strSql);

                populaNos(objDataReader, objTreeNodePai.ChildNodes);

                objDataReader.Dispose();
                objDataReader = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region metodo retornaTotalPorStatus
        /// <summary>
        /// Retorna o total de Itens de Configuracao de um determinado status
        /// </summary>
        /// <param name="intCodigoStatus">Código do Status</param>
        /// <returns>Retorna um inteiro com o total de Itens de Configuração, se não encontrar retorna 0.</returns>
        public static int retornaTotalPorStatus(int intCodigoStatus)
        {
            try
            {
                int intRetorno = 0;
                String strRetorno = String.Empty;

                ClsBanco objBanco = new ClsBanco();
                strRetorno = objBanco.retornaValorCampo("IC", "COUNT(ic_codigo)", "status_atual = " + intCodigoStatus.ToString());
                if (strRetorno != String.Empty)
                {
                    intRetorno = Convert.ToInt32(strRetorno);
                }
                objBanco = null;

                return intRetorno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region AdicionaItemConfiguracao

        /// <summary>
        /// Adiciona um item de configuração relacionado à tabela e registro especificado.
        /// </summary>
        /// <param name="strCodigoIdentificador">Codigo do Registro na Tabela</param>
        /// <param name="strTabela">Tabela </param>
        /// <param name="strCodigoItemConfiguracao">Codigo do Item de Configuracao</param>
        static public void AdicionaItemConfiguracao(String strCodigoIdentificador, String strTabela, String strCodigoItemConfiguracao)
        {
            try
            {
                string strSql = string.Empty;

                if ((strCodigoIdentificador != string.Empty) && (strCodigoItemConfiguracao != string.Empty) && (strTabela != string.Empty))
                {
                    if (VerificaExistenciaItemConfiguracao(strCodigoIdentificador, strTabela, strCodigoItemConfiguracao) == false)
                    {
                        strSql = "INSERT INTO " + strTabela + "IC ";
                        strSql += " (" + strTabela + "_codigo, ic_codigo) ";
                        strSql += " VALUES ('" + strCodigoIdentificador + "', '" + strCodigoItemConfiguracao + "') ";

                        ClsBanco objBanco = new ClsBanco();
                        objBanco.executaSQL(strSql);
                        objBanco = null;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region RemoveItemConfiguracao
        /// <summary>
        /// Remove um item de configuração relacionado à tabela e registro especificado.
        /// </summary>
        /// <param name="strCodigoIdentificador">Codigo do Registro na Tabela</param>
        /// <param name="strTabela">Tabela </param>
        /// <param name="strCodigoItemConfiguracao">Codigo " + strTabela + "do Item de Configuracao</param>  
        static public void RemoveItemConfiguracao(String strCodigoIdentificador, String strTabela, String strCodigoItemConfiguracao)
        {
            try
            {
                string strSql = string.Empty;

                if ((strCodigoIdentificador != string.Empty) && (strCodigoItemConfiguracao != ""))
                {
                    strSql = "DELETE FROM " + strTabela + "IC ";
                    strSql += "WHERE " + strTabela + "_codigo = '" + strCodigoIdentificador + "' and ic_codigo = '" + strCodigoItemConfiguracao + "' ";
                    ClsBanco objBanco = new ClsBanco();
                    objBanco.executaSQL(strSql);
                    objBanco = null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region VerificaExistenciaItemConfiguracao
        /// <summary>
        /// Verifica se um item já está relacionado ao registro da tabela informada.
        /// </summary>
        /// <param name="strCodigoIdentificador">Codigo do Registro na Tabela</param>
        /// <param name="strTabela">Tabela </param>
        /// <param name="strCodigoItemConfiguracao">Codigo do Item de Configuracao</param>  
        /// <returns>True se Existe. False se nao existe.</returns>
        static public bool VerificaExistenciaItemConfiguracao(String strCodigoIdentificador, String strTabela, String strCodigoItemConfiguracao)
        {
            try
            {
                string strSql = string.Empty;

                if ((strCodigoIdentificador != string.Empty) && (strCodigoItemConfiguracao != ""))
                {
                    strSql = "Select " + strTabela + "_codigo FROM " + strTabela + "IC ";
                    strSql += "where " + strTabela + "_codigo = '" + strCodigoIdentificador + "' and ic_codigo = '" + strCodigoItemConfiguracao + "' ";
                    SqlDataReader objReader = ServiceDesk.Banco.ClsBanco.geraDataReader(strSql);
                    if (objReader.HasRows)
                    {
                        objReader.Dispose();
                        objReader = null;
                        return true;
                    }
                    else
                    {
                        objReader.Dispose();
                        objReader = null;
                        return false;
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Verifica se ação esta associada ao IC
        /// <summary>
        /// Verifica se ação esta associada ao IC
        /// </summary>
        /// <param name="intCodigoAcao">Código identificador da Ação</param>
        /// <param name="intCodigoIC">Código identificador do IC</param>
        /// <returns>Retorna true ou false. Se esta associado ou não</returns>
        public static bool VerificaSeAcaoAssociadaAoIC(int intCodigoAcao, int intCodigoIC)
        {
            //=================================================================//
            // - O que: Verifica se a ação já esta associada ao IC             //                                                    
            // - Quem: Fernanda Passos                                         //
            // - Quando: 03/02/2006 ás 11:06                                   //
            //=================================================================//
            string strSql = " tabela_destino = 'IC'";
            strSql += " and identificador_destino = " + intCodigoIC + "";
            strSql += " and tabela_origem = 'acao'";
            strSql += " and identificador_origem = " + intCodigoAcao + "";

            ClsBanco objBanco = new ClsBanco();
            if (objBanco.retornaValorCampo("vinculo", "vinculo_codigo", strSql) == string.Empty) return false; else return true;
        }
        #endregion

        #region Deleta relacionamento entre itens de configuração
        /// <summary>
        /// Exclui relacionamento de um IC com outro IC na tabela ICRelacao
        /// </summary>
        /// <param name="intCodigoIcOrigem">Código do IC de origem</param>
        /// <param name="intCodigoIcDestino">Código do IC de destino</param>
        /// <param name="intCodigoTipoRelacaoIcComIc">Código do tipo de relacionamento</param>
        /// <returns>Retorna true ou false. Se foi removido ou não</returns>
        public static bool ExcluiRelacaoIcComIc(int intCodigoIcOrigem, int intCodigoIcDestino, int intCodigoTipoRelacaoIcComIc)
        {
            try
            {
                //============================================================================================//
                // - O que: Deleta um relacionamento de um item de configuração com outro item de configuração 
                // - Quem: Fernanda Passos
                // - Quando: 05/04/2006
                //============================================================================================//
                bool bolRetorno = false;

                string strSql = "delete ICRelacao ";
                strSql += " where ic_codigo_origem is not null";
                if (intCodigoIcDestino != 0) strSql += " and ic_codigo_origem = " + intCodigoIcOrigem + " ";
                if (intCodigoIcDestino != 0) strSql += " and ic_codigo_destino = " + intCodigoIcDestino + " ";
                if (intCodigoTipoRelacaoIcComIc != 0) strSql += " and ic_relacao_tipo_codigo = " + intCodigoTipoRelacaoIcComIc + "";

                ClsBanco objBanco = new ClsBanco();
                if (objBanco.executaSQL(strSql) == true) bolRetorno = true; else bolRetorno = false;
                objBanco = null;

                return bolRetorno;
                //============================================================================================//
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region metodo retornaUltimoNumero
        /// <summary>
        /// Retorna o ultimo numero de item de configuração
        /// </summary>
        /// <returns>vazio se erro ou nulo</returns>
        public static string retornaUltimoNumero()
        {
            try
            {
                var result = ClsBanco.ExecuteScalar("select max(cast(numero as int)) + 1 from ic");

                return result == null ? string.Empty : result.ToString();
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
        #endregion

        #endregion
    }
}