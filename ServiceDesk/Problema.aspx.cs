/*
- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
  � Formul�rio de cadastramento dos problemas.
  
  	Data: 29/11/2005
  	Autor: Fernanda Passos
  	Descri��o: Este WebForm apresenta funcionalidade como inserir, excluir, alterar e permite ainda
    fazer associa��es do de incidentes, intes de configura��o e mudan�as ao problema selecionado.
  
  
  � Altera��es
  	Data: 22/12/2005
  	Autor: Fernanda Passos
  	Descri��o: Inclus�o da aba de solu��o e todos os objetos e funcionalidades necess�rias para itera��o com 
    a solu��o.
- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
*/
using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ServiceDesk.Negocio;

public partial class Problema : BasePage
{

    #region Declaracoes
    private ServiceDesk.Negocio.ClsProblema objProblema = new ServiceDesk.Negocio.ClsProblema();
    private ServiceDesk.Negocio.ClsProblemaIncidente objProblemaIncidente = new ServiceDesk.Negocio.ClsProblemaIncidente();
    private ServiceDesk.Negocio.ClsProblemaItemConfig objProblemaItemConfigura = new ServiceDesk.Negocio.ClsProblemaItemConfig();
    private ServiceDesk.Negocio.ClsProblemaMudanca objProblemaMudanca = new ServiceDesk.Negocio.ClsProblemaMudanca();
    private ServiceDesk.Negocio.ClsNotificacao objNotificacao = new ServiceDesk.Negocio.ClsNotificacao();
    private bool bolAltera = new bool();

    #endregion

    #region Page_Load
    /// <summary>
    /// Page_Load
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <param name="intCodigoProblema">C�digo do problema a ser exibido. Se for zero trar� tela em branco.</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            CheckAcesso(48);

            divMensagem.Visible = false;
            if (!Page.IsPostBack)
            {
                //Alimenta DropDow
                ClsUsuario.geraDropDownList(cboPessoaCadastro, "--");
                ClsEquipe.geraDropDownList(cboNomeEquipe);
                ClsStatus.geraDropDownList(cboStatusProblema);
                ClsItemConfiguracao.geraDropDownList(dlItemConfiguracao);
                ClsMudanca.geraDropDownList(dlMudanca);
                ClsIncidente.geraDropDownList(dlNomeIncidente);
                ClsProblemaTipo.geraDropDownList(dlTipoProblema, "Selecione o tipo do problema.");
                ClsUsuario.geraDropDownList(dlPessoaAlocada, "Selecione a pessoa.");

                mtwAbas.ActiveViewIndex = 0;

                if (Request.QueryString["CodProblema"] != null)
                {
                    PreencheCampos(Convert.ToInt32(Request.QueryString["CodProblema"]));
                    ClsProblemaIncidente.geraGridView(gdIncidente, Convert.ToInt32(Request.QueryString["CodProblema"]));
                    mtwAbas.ActiveViewIndex = 0;
                }
            }
        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ClsLog.enumTipoLog.ERRO, user.IDusuario.ToString(), this.Request.Path, "0", ex.ToString());

        }
    }
    #endregion

    #region M�todos

    /// <summary>
    /// Limpa os objetos na tela.
    /// </summary>
    private void novo()
    {
        try
        {
            bolAltera = false;

            txtDescricao.Text = "";
            txtNome.Text = "";
            txtCdProblema.Text = "";
            dlTipoProblema.SelectedValue = "";
            cboNomeEquipe.SelectedValue = "";
            dlFlgProbFechado.SelectedValue = "N";
            cboStatusProblema.SelectedValue = "";
            cboPessoaCadastro.SelectedValue = "";
            dlPessoaAlocada.SelectedValue = "";
        }
        catch (Exception ex)
        {
            lblMensagem.Text = ex.Message;
            imgIcone.ImageUrl = "images/icones/aviso.gif";
            lblMensagem.Visible = true;
            divMensagem.Visible = true;

            ClsLog.insereLog(ClsLog.enumTipoLog.ERRO, user.IDusuario.ToString(), this.Request.Path, "0", ex.ToString());

        }
    }

    /// <summary>
    /// M�todo salvar problema.
    /// </summary>
    private void SalvarProblema()
    {
        try
        {
            string strMsg = string.Empty;

            //Verifica se caracteres da descri��o execede o limite.
            if (txtDescricao.Text.Length > 255)
            {
                objProblema.Descricao.Valor = ServiceDesk.Generica.ClsTexto.trocaAspaPorHtml(txtDescricao.Text.Substring(0, 255));
                txtDescricao.Text = ServiceDesk.Generica.ClsTexto.trocaAspaPorHtml(txtDescricao.Text.Substring(0, 255));
            }
            else objProblema.Descricao.Valor = ServiceDesk.Generica.ClsTexto.trocaAspaPorHtml(txtDescricao.Text);

            objProblema.FlagFechado.Valor = dlFlgProbFechado.SelectedValue.ToString();
            objProblema.Nome.Valor = txtNome.Text.Trim();
            objProblema.CodigoEquipeAlocacao.Valor = cboNomeEquipe.SelectedValue.ToString();
            objProblema.Status.Valor = cboStatusProblema.SelectedValue.ToString();
            objProblema.CodigoProblemaTipo.Valor = dlTipoProblema.SelectedValue;
            objProblema.CodigoPessoaAlocacao.Valor = dlPessoaAlocada.SelectedValue.ToString();

            if (bolAltera == false && txtCdProblema.Text.Trim() == string.Empty) //Insere
            {
                objProblema.Codigo.Valor = objProblema.GetMaxId().ToString();

                objProblema.DtInclusao.Valor = DateTime.Now.ToString(ClsParametro.DataInclusao);
                objProblema.UsuarioQueCadastrou.Valor = user.IDusuario.ToString();

                if (objProblema.insere(out strMsg) == false)
                {
                    lblMensagem.Text = strMsg;
                    lblMensagem.Visible = true;
                    imgIcone.ImageUrl = "images/icones/info.gif";
                    divMensagem.Visible = true;
                }
                else
                {
                    lblMensagem.Text = strMsg;
                    lblMensagem.Visible = true;
                    imgIcone.ImageUrl = "images/icones/info.gif";
                    divMensagem.Visible = true;
                    txtCdProblema.Text = objProblema.Codigo.Valor;
                }
            }
            else //Altera
            {
                objProblema.Codigo.Valor = txtCdProblema.Text.Trim();
                objProblema.DtAlteracao.Valor = DateTime.Now.ToString(ClsParametro.DataInclusao);
                objProblema.UsuarioQueAlterou.Valor = user.IDusuario.ToString();

                if (objProblema.altera(out strMsg) == false)
                {
                    lblMensagem.Text = strMsg;
                    lblMensagem.Visible = true;
                    imgIcone.ImageUrl = "images/icones/info.gif";
                    divMensagem.Visible = true;
                }
                else
                {
                    lblMensagem.Text = strMsg;
                    lblMensagem.Visible = true;
                    imgIcone.ImageUrl = "images/icones/info.gif";
                    divMensagem.Visible = true;
                }
            }
        }
        catch (Exception ex)
        {
            lblMensagem.Text = ex.Message;
            imgIcone.ImageUrl = "images/icones/aviso.gif";
            divMensagem.Visible = true;
            lblMensagem.Visible = true;

            ClsLog.insereLog(ClsLog.enumTipoLog.ERRO, user.IDusuario.ToString(), this.Request.Path, "0", ex.ToString());
        }
    }

    /// <summary>
    /// Insere registro de notifica��o no banco de dados.
    /// </summary>
    /// <param name="CodigoIdentificadorTabela">C�digo inteiro da tabela que esta emitindo a notifica��o.</param>
    /// <param name="intCodigoRegistroTabela" >C�digo do registro da tabela que origina as notifica��es</param>
    private void SalvaNotififica��o()
    {
        try
        {
            string strMsg = string.Empty;

            //Declara��es locais
            System.Data.SqlClient.SqlDataReader objDtReaderUser;
            ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
            ServiceDesk.Negocio.ClsEquipe objEquipe = new ServiceDesk.Negocio.ClsEquipe();

            //Pega dados do lider da equipe.
            objDtReaderUser = objEquipe.GetDadosLiderEquipe(Convert.ToInt32(objBanco.retornaValorCampo("Problema", "equipe_codigo_alocacao", "problema_codigo = " + Convert.ToInt32(txtCdProblema.Text.Trim()) + "")));

            while (objDtReaderUser.Read())
            {
                objNotificacao.CodigoUsuarioReceptor.Valor = objDtReaderUser["pessoa_codigo"].ToString().Trim();
                objNotificacao.Codigo.Valor = objNotificacao.GetMaxId().ToString();
                objNotificacao.Tabela.Valor = "Problema";
                objNotificacao.IdentificadorTabela.Valor = txtCdProblema.Text.Trim();

                string strMatricula = ClsUsuario.getCodigoRede();

                string strNomUserReceptor = ClsUsuario.getNomeUsuario(objDtReaderUser["pessoa_codigo"].ToString());

                objNotificacao.Descricao.Valor = strMatricula + " vem notificar ao " + strNomUserReceptor + " sobre o problema " + this.txtNome.Text.Trim() + " .";

                string strSql = " matricula = '" + strMatricula + "'";

                objNotificacao.CodigoUsuarioEmissor.Valor = objBanco.retornaValorCampo("Pessoa", "pessoa_codigo", strSql);

                objNotificacao.DtInclusao.Valor = DateTime.Now.ToString(ClsParametro.DataInclusao);

                if (objNotificacao.enviar(out strMsg) == false)
                {
                    strMsg += " ** " + strMsg;
                }

            }
            objDtReaderUser.Close();
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);

            ClsLog.insereLog(ClsLog.enumTipoLog.ERRO, user.IDusuario.ToString(), this.Request.Path, "0", ex.ToString());
        }
    }

    /// <summary>
    /// Envia notifica��o para equie
    /// </summary>
    private void EnviaProblemaParaEquipe()
    {
        try
        {
            //Verifica se foi selecionado o problema.
            if (txtCdProblema.Text.Trim() == string.Empty)
            {
                lblMensagem.Text = "Selecione o problema.";
                imgIcone.ImageUrl = "images/icones/aviso.gif";
                lblMensagem.Visible = true;
                divMensagem.Visible = true;
                return;
            }
            //Verifica se foi informado a equipe.
            else if (cboNomeEquipe.SelectedIndex == 0)
            {
                lblMensagem.Text = "Selecione a equipe.";
                imgIcone.ImageUrl = "images/icones/aviso.gif";
                lblMensagem.Visible = true;
                divMensagem.Visible = true;
                return;
            }

            SalvaNotififica��o();
        }
        catch (Exception ex)
        {
            lblMensagem.Text = ex.Message;
            imgIcone.ImageUrl = "images/icones/aviso.gif";
            lblMensagem.Visible = true;
            divMensagem.Visible = true;

            ClsLog.insereLog(ClsLog.enumTipoLog.ERRO, user.IDusuario.ToString(), this.Request.Path, "0", ex.ToString());

        }

    }
    #endregion

    #region Eventos

    /// <summary>
    /// Incidente - Evento que abilita a aba de incidente.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lkbIncidente_Click(object sender, EventArgs e)
    {
        try
        {
            this.mtwAbas.ActiveViewIndex = 0;

            if (txtCdProblema.Text.Trim() != string.Empty) ServiceDesk.Negocio.ClsProblemaIncidente.geraGridView(gdIncidente, Convert.ToInt32(txtCdProblema.Text.Trim()));
        }
        catch (Exception ex)
        {
            lblMensagem.Text = ex.Message;
            imgIcone.ImageUrl = "images/icones/aviso.gif";
            lblMensagem.Visible = true;
            divMensagem.Visible = true;

            ClsLog.insereLog(ClsLog.enumTipoLog.ERRO, user.IDusuario.ToString(), this.Request.Path, "0", ex.ToString());

        }
    }

    /// <summary>
    /// Item de configura��o - Evento que abilita a aba de item de configura��o.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lkbItemConfig_Click(object sender, EventArgs e)
    {
        try
        {
            mtwAbas.ActiveViewIndex = 1;

            if (txtCdProblema.Text.Trim() != string.Empty) ServiceDesk.Negocio.ClsProblemaItemConfig.geraGridView(grItemConfiguracao, Convert.ToInt32(txtCdProblema.Text.Trim()));

        }
        catch (Exception ex)
        {
            lblMensagem.Text = ex.Message;
            imgIcone.ImageUrl = "images/icones/aviso.gif";
            lblMensagem.Visible = true;
            divMensagem.Visible = true;

            ClsLog.insereLog(ClsLog.enumTipoLog.ERRO, user.IDusuario.ToString(), this.Request.Path, "0", ex.ToString());

        }

    }

    /// <summary>
    /// Mudan�a - Envento que abilita a aba de mudan�a.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lkbMudanca_Click(object sender, EventArgs e)
    {
        try
        {
            mtwAbas.ActiveViewIndex = 2;

            if (txtCdProblema.Text.Trim() != string.Empty) ServiceDesk.Negocio.ClsProblemaMudanca.geraGridView(gdMudanca, Convert.ToInt32(txtCdProblema.Text.Trim()));

        }
        catch (Exception ex)
        {
            lblMensagem.Text = ex.Message;
            imgIcone.ImageUrl = "images/icones/aviso.gif";
            lblMensagem.Visible = true;
            divMensagem.Visible = true;

            ClsLog.insereLog(ClsLog.enumTipoLog.ERRO, user.IDusuario.ToString(), this.Request.Path, "0", ex.ToString());
        }
    }

    /// <summary>
    /// Inserir problema.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSalvar_Click(object sender, EventArgs e)
    {
        try
        {
            SalvarProblema();
        }
        catch (Exception ex)
        {
            lblMensagem.Text = ex.Message;
            imgIcone.ImageUrl = "images/icones/aviso.gif";
            lblMensagem.Visible = true;
            divMensagem.Visible = true;

            ClsLog.insereLog(ClsLog.enumTipoLog.ERRO, user.IDusuario.ToString(), this.Request.Path, "0", ex.ToString());

        }
    }

    /// <summary>
    /// Remover problema.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnExcluir_Click(object sender, EventArgs e)
    {
        try
        {
            //Verifica se foi selecionado algum problema.
            if (txtCdProblema.Text.Trim() == string.Empty)
                return;

            string strMsg = string.Empty;

            objProblema.Codigo.Valor = txtCdProblema.Text.Trim();
            if (objProblema.exclui(out strMsg) == true) { lblMensagem.Text = "Registro exclu�do com sucesso."; } else { lblMensagem.Text = "N�o foi poss�vel excluir o registro."; }

            lblMensagem.Text = strMsg;
            imgIcone.ImageUrl = "images/icones/info.gif";
            lblMensagem.Visible = true;
            divMensagem.Visible = true;

            ServiceDesk.Negocio.ClsProblemaMudanca.geraGridView(gdMudanca, Convert.ToInt32(txtCdProblema.Text.Trim()));
            ServiceDesk.Negocio.ClsProblemaItemConfig.geraGridView(gdIncidente, Convert.ToInt32(txtCdProblema.Text.Trim()));
            ServiceDesk.Negocio.ClsProblemaItemConfig.geraGridView(grItemConfiguracao, Convert.ToInt32(txtCdProblema.Text.Trim()));

            novo();
        }
        catch (Exception ex)
        {
            lblMensagem.Text = ex.Message;
            imgIcone.ImageUrl = "images/icones/aviso.gif";
            lblMensagem.Visible = true;
            divMensagem.Visible = true;

            ClsLog.insereLog(ClsLog.enumTipoLog.ERRO, user.IDusuario.ToString(), this.Request.Path, "0", ex.ToString());

        }
    }

    /// <summary>
    /// Inserir item de configura��o.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAddItemConfiguracao_Click1(object sender, EventArgs e)
    {
        try
        {
            //Verifica se foi selecionado algum problema.
            if (txtCdProblema.Text == string.Empty)
            {
                lblMensagem.Text = "Selecione o problema.";
                imgIcone.ImageUrl = "images/icones/aviso.gif";
                lblMensagem.Visible = true;
                divMensagem.Visible = true;
                return;
            }
            else
            {
                lblMensagem.Visible = false;
                divMensagem.Visible = false;
            }

            string strMsg = string.Empty;

            //Preenche os campos.
            objProblemaItemConfigura.CodigoItemConfig.Valor = dlItemConfiguracao.SelectedValue.ToString();
            objProblemaItemConfigura.CodigoProblema.Valor = txtCdProblema.Text.Trim();

            //Insere a associa��o do registro do problema ao item de configura��o.
            if (objProblemaItemConfigura.insere(out strMsg) == false)
            {
                //Exibe descri��o do motivo da n�o inser��o do registro no banco.
                lblMensagem.Text = strMsg;
                imgIcone.ImageUrl = "images/icones/aviso.gif";
                lblMensagem.Visible = true;
                divMensagem.Visible = true;
            }
            else
            {   //>>> Log de Recorr�ncia. <<<
                //Verifica se � recorr�ncia do IC.
                if (ServiceDesk.Negocio.ClsRecorrenciaLog.VerificaSeRecorrencia(Convert.ToInt32(dlItemConfiguracao.SelectedValue), "Problema", Convert.ToInt32(txtCdProblema.Text.Trim())) == true)
                //Atualiza log de recorr�ncia.
                {
                    ServiceDesk.Negocio.ClsRecorrenciaLog objRecorrenciaLog = new ServiceDesk.Negocio.ClsRecorrenciaLog();
                    objRecorrenciaLog.Codigo.Valor = objRecorrenciaLog.GetMaxId().ToString();
                    objRecorrenciaLog.Data.Valor = DateTime.Now.ToString();
                    objRecorrenciaLog.IdentificadorOrigem.Valor = dlItemConfiguracao.SelectedValue.ToString();
                    objRecorrenciaLog.IdentificadorRegistro.Valor = txtCdProblema.Text.Trim();
                }
                //<<< Fim Log Recorr�ncia <<<

                //Carrega o grid para atualizar com o novo registro.
                ServiceDesk.Negocio.ClsProblemaItemConfig.geraGridView(grItemConfiguracao, Convert.ToInt32(txtCdProblema.Text.Trim()));
                lblMensagem.Visible = false;
                divMensagem.Visible = false;
            }
        }
        catch (Exception ex)
        {
            //Exibe a descri��o da mensagem de erro.
            lblMensagem.Text = ex.Message;
            imgIcone.ImageUrl = "images/icones/aviso.gif";
            lblMensagem.Visible = true;
            divMensagem.Visible = true;

            ClsLog.insereLog(ClsLog.enumTipoLog.ERRO, user.IDusuario.ToString(), this.Request.Path, "0", ex.ToString());
        }
    }

    /// <summary>
    /// Inserir incidente.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAddIncidente_Click1(object sender, EventArgs e)
    {
        try
        {
            if (txtCdProblema.Text == string.Empty)
            {
                lblMensagem.Text = "Selecione o problema.";
                imgIcone.ImageUrl = "images/icones/aviso.gif";
                lblMensagem.Visible = true;
                divMensagem.Visible = true;
                return;
            }
            else
            {
                lblMensagem.Visible = false;
                divMensagem.Visible = false;
            }

            string strMgs = string.Empty;

            objProblemaIncidente.CodigoIncidente.Valor = dlNomeIncidente.SelectedValue.ToString();
            objProblemaIncidente.CodigoProblema.Valor = txtCdProblema.Text.Trim();

            if (objProblemaIncidente.insere(out strMgs) == false)
            {
                lblMensagem.Text = strMgs;
                imgIcone.ImageUrl = "images/icones/aviso.gif";
                lblMensagem.Visible = true;
                divMensagem.Visible = true;
            }
            else
            {
                ServiceDesk.Negocio.ClsProblemaIncidente.geraGridView(gdIncidente, Convert.ToInt32(txtCdProblema.Text.Trim()));
                lblMensagem.Visible = false;
                divMensagem.Visible = false;
            }
        }
        catch (Exception ex)
        {
            lblMensagem.Text = ex.Message;
            imgIcone.ImageUrl = "images/icones/aviso.gif";
            lblMensagem.Visible = true;
            divMensagem.Visible = true;

            ClsLog.insereLog(ClsLog.enumTipoLog.ERRO, user.IDusuario.ToString(), this.Request.Path, "0", ex.ToString());

        }
    }

    /// <summary>
    /// Inserir mudan�a.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAddMudanca_Click1(object sender, EventArgs e)
    {
        try
        {
            if (txtCdProblema.Text == string.Empty)
            {
                lblMensagem.Text = "Selecione o problema.";
                imgIcone.ImageUrl = "images/icones/aviso.gif";
                lblMensagem.Visible = true;
                divMensagem.Visible = true;
                return;
            }
            else
            {
                lblMensagem.Visible = false;
                divMensagem.Visible = false;
            }

            string strMsg = string.Empty;

            objProblemaMudanca.CodigoProblema.Valor = txtCdProblema.Text.Trim();
            objProblemaMudanca.CodigoMudanca.Valor = dlMudanca.SelectedValue.ToString();

            if (objProblemaMudanca.insere(out strMsg) == false)
            {
                lblMensagem.Text = strMsg;
                imgIcone.ImageUrl = "images/icones/aviso.gif";
                lblMensagem.Visible = true;
                divMensagem.Visible = true;
            }
            else
            {
                ServiceDesk.Negocio.ClsProblemaMudanca.geraGridView(gdMudanca, Convert.ToInt32(txtCdProblema.Text.Trim()));
                lblMensagem.Visible = false;
                divMensagem.Visible = false;
            }
        }
        catch (Exception ex)
        {
            lblMensagem.Text = ex.Message;
            imgIcone.ImageUrl = "images/icones/aviso.gif";
            lblMensagem.Visible = true;
            divMensagem.Visible = true;

            ClsLog.insereLog(ClsLog.enumTipoLog.ERRO, user.IDusuario.ToString(), this.Request.Path, "0", ex.ToString());

        }
    }

    /// <summary>
    /// Remover mudan�a.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnRemMudanca_Click1(object sender, EventArgs e)
    {
        try
        {
            string strMsg = string.Empty;

            objProblemaMudanca.CodigoMudanca.Valor = gdMudanca.SelectedIndex.ToString();
            objProblemaMudanca.CodigoProblema.Valor = txtCdProblema.Text.Trim();

            if (objProblemaMudanca.exclui(out strMsg) == false)
            {
                lblMensagem.Text = strMsg;
                imgIcone.ImageUrl = "images/icones/aviso.gif";
                lblMensagem.Visible = true;
                divMensagem.Visible = true;
            }
            else
            {
                lblMensagem.Visible = false;
                divMensagem.Visible = false;
            }
        }
        catch (Exception ex)
        {
            lblMensagem.Text = ex.Message;
            imgIcone.ImageUrl = "images/icones/aviso.gif";
            lblMensagem.Visible = true;
            divMensagem.Visible = true;

            ClsLog.insereLog(ClsLog.enumTipoLog.ERRO, user.IDusuario.ToString(), this.Request.Path, "0", ex.ToString());
        }
    }

    /// <summary>
    /// Novo registro
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnNovo_Click(object sender, EventArgs e)
    {
        novo();
    }

    /// <summary>
    /// Preenche os campos
    /// </summary>
    /// <param name="intCodProblema"></param>
    protected void PreencheCampos(int intCodProblema)
    {
        try
        {
            bolAltera = true;

            String strSql = String.Empty;

            strSql = "select * from Problema";
            strSql += " where problema_codigo = " + intCodProblema + "";

            System.Data.SqlClient.SqlDataReader objDataReader = ServiceDesk.Banco.ClsBanco.geraDataReader(strSql);

            if (objDataReader.Read())
            {
                string strFormatoDataSimples = ClsParametro.DataSimplesExibicao;

                txtCdProblema.Text = objDataReader["problema_codigo"].ToString().Trim();
                txtDescricao.Text = objDataReader["descricao"].ToString().Trim();
                txtNome.Text = objDataReader["nome"].ToString().Trim();
                cboNomeEquipe.SelectedValue = objDataReader["equipe_codigo_alocacao"].ToString();

                if (objDataReader["pessoa_codigo_proprietario"].ToString() != string.Empty) cboPessoaCadastro.SelectedValue = objDataReader["pessoa_codigo_proprietario"].ToString();
                if (objDataReader["status_codigo"].ToString() != string.Empty) cboStatusProblema.SelectedValue = objDataReader["status_codigo"].ToString();
                if (objDataReader["pessoa_codigo_alocacao"].ToString() != string.Empty) dlPessoaAlocada.SelectedValue = objDataReader["pessoa_codigo_alocacao"].ToString();

                dlFlgProbFechado.SelectedValue = objDataReader["flag_fechado"].ToString();
                if (objDataReader["problema_tipo_codigo"].ToString() != string.Empty) dlTipoProblema.SelectedValue = objDataReader["problema_tipo_codigo"].ToString();
            }

            objDataReader = null;
        }
        catch (Exception ex)
        {
            lblMensagem.Text = ex.Message;
            imgIcone.ImageUrl = "images/icones/aviso.gif";
            lblMensagem.Visible = true;
            divMensagem.Visible = true;

            ClsLog.insereLog(ClsLog.enumTipoLog.ERRO, user.IDusuario.ToString(), this.Request.Path, "0", ex.ToString());

        }
    }

    /// <summary>
    /// RowCommand Mudanca
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gdMudanca_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Excluir")
            {
                GridViewRow objRow = gdMudanca.Rows[Convert.ToInt32(e.CommandArgument)];

                if (objRow != null)
                {
                    string strMsg = string.Empty;

                    Label lblCodigo = (Label)objRow.FindControl("lblCodigoMudanca");

                    objProblemaMudanca.CodigoMudanca.Valor = lblCodigo.Text.Trim();
                    objProblemaMudanca.CodigoProblema.Valor = txtCdProblema.Text.Trim();

                    if (objProblemaMudanca.exclui(out strMsg) == false)
                    {
                        lblMensagem.Text = strMsg;
                        imgIcone.ImageUrl = "images/icones/aviso.gif";
                        lblMensagem.Visible = true;
                        divMensagem.Visible = true;
                    }
                    else
                    {
                        ServiceDesk.Negocio.ClsProblemaMudanca.geraGridView(gdMudanca, Convert.ToInt32(txtCdProblema.Text.Trim()));
                        lblMensagem.Visible = false;
                        divMensagem.Visible = false;
                    }

                }
                objRow = null;
            }
        }
        catch (Exception ex)
        {
            lblMensagem.Text = ex.Message;
            imgIcone.ImageUrl = "images/icones/aviso.gif";
            lblMensagem.Visible = true;
            divMensagem.Visible = true;


            ClsLog.insereLog(ClsLog.enumTipoLog.ERRO, user.IDusuario.ToString(), this.Request.Path, "0", ex.ToString());

        }
    }

    /// <summary>
    /// RowCommand Item Configura��o
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grItemConfiguracao_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Excluir")
            {
                GridViewRow objRow = grItemConfiguracao.Rows[Convert.ToInt32(e.CommandArgument)];

                if (objRow != null)
                {
                    string strMsg = string.Empty;

                    Label lblCodigo = (Label)objRow.FindControl("lblCodigoItemConfig");

                    objProblemaItemConfigura.CodigoItemConfig.Valor = lblCodigo.Text.Trim();
                    objProblemaItemConfigura.CodigoProblema.Valor = txtCdProblema.Text.Trim();

                    if (objProblemaItemConfigura.exclui(out strMsg) == false)
                    {
                        lblMensagem.Text = strMsg;
                        imgIcone.ImageUrl = "images/icones/aviso.gif";
                        lblMensagem.Visible = true;
                        divMensagem.Visible = true;

                    }
                    else
                    {
                        ServiceDesk.Negocio.ClsProblemaItemConfig.geraGridView(grItemConfiguracao, Convert.ToInt32(txtCdProblema.Text.Trim()));
                        lblMensagem.Visible = false;
                        divMensagem.Visible = false;

                    }

                }
                objRow = null;
            }
        }
        catch (Exception ex)
        {
            lblMensagem.Text = ex.Message;
            imgIcone.ImageUrl = "images/icones/aviso.gif";
            lblMensagem.Visible = true;
            divMensagem.Visible = true;

            ClsLog.insereLog(ClsLog.enumTipoLog.ERRO, user.IDusuario.ToString(), this.Request.Path, "0", ex.ToString());

        }
    }

    /// <summary>
    /// RowCommand Incidente
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gdIncidente_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Excluir")
            {
                GridViewRow objRow = gdIncidente.Rows[Convert.ToInt32(e.CommandArgument)];

                if (objRow != null)
                {
                    string strMsg = string.Empty;

                    Label lblCodigo = (Label)objRow.FindControl("lblCodigoIncidente");

                    objProblemaIncidente.CodigoIncidente.Valor = lblCodigo.Text.Trim();
                    objProblemaIncidente.CodigoProblema.Valor = txtCdProblema.Text.Trim();

                    if (objProblemaIncidente.exclui(out strMsg) == false)
                    {
                        lblMensagem.Text = strMsg;
                        imgIcone.ImageUrl = "images/icones/aviso.gif";
                        lblMensagem.Visible = true;
                        divMensagem.Visible = true;
                    }
                    else
                    {
                        ServiceDesk.Negocio.ClsProblemaIncidente.geraGridView(gdIncidente, Convert.ToInt32(txtCdProblema.Text.Trim()));
                        lblMensagem.Visible = false;
                        divMensagem.Visible = false;
                    }

                }
                objRow = null;
            }
        }
        catch (Exception ex)
        {
            lblMensagem.Text = ex.Message;
            imgIcone.ImageUrl = "images/icones/aviso.gif";
            lblMensagem.Visible = true;
            divMensagem.Visible = true;

            ClsLog.insereLog(ClsLog.enumTipoLog.ERRO, user.IDusuario.ToString(), this.Request.Path, "0", ex.ToString());

        }
    }

    /// <summary>
    /// Envia notifica��o para pessoas da equipe.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnEnviaEquipe_Click(object sender, EventArgs e)
    {
        try
        {
            ///Verifica se o problema esta fechado ou n�o.
            if (dlFlgProbFechado.SelectedValue == "S")
            {
                lblMensagem.Text = "O problema esta fechado. Imposs�vel enviar para equipe.";
                imgIcone.ImageUrl = "images/icones/aviso.gif";
                lblMensagem.Visible = true;
                divMensagem.Visible = true;
                return;
            }
            else
            {
                lblMensagem.Visible = false;
                divMensagem.Visible = false;
            }


            EnviaProblemaParaEquipe();
        }
        catch (Exception ex)
        {
            lblMensagem.Text = ex.Message;
            imgIcone.ImageUrl = "images/icones/aviso.gif";
            lblMensagem.Visible = true;
            divMensagem.Visible = true;

            ClsLog.insereLog(ClsLog.enumTipoLog.ERRO, user.IDusuario.ToString(), this.Request.Path, "0", ex.ToString());
        }
    }

    /// <summary>
    ///  Row Command do Grid Recorr�ncias
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gdRecorrencia_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Exibir")
            {
                GridViewRow objRow = gdRecorrencia.Rows[Convert.ToInt32(e.CommandArgument)];
                if (objRow != null)
                {
                    Label lblCodigoOrigemIC = (Label)objRow.FindControl("lblCodigoOrigem");
                    Response.Redirect("itemconfiguracao.aspx?codigo=" + lblCodigoOrigemIC.Text.Trim(), false);
                }
                objRow = null;
            }
        }
        catch (Exception ex)
        {
            lblMensagem.Text = ex.Message;
            imgIcone.ImageUrl = "images/icones/aviso.gif";
            lblMensagem.Visible = true;
            divMensagem.Visible = true;

            ClsLog.insereLog(ClsLog.enumTipoLog.ERRO, user.IDusuario.ToString(), this.Request.Path, "0", ex.ToString());

        }
    }

    /// <summary>
    /// Exibe grid com recorr�ncias.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lkbRecorrencia_Click(object sender, EventArgs e)
    {
        try
        {
            ServiceDesk.Negocio.ClsRecorrenciaLog.geraGridView(gdRecorrencia, "Problema");
            mtwAbas.ActiveViewIndex = 3;
        }
        catch (Exception ex)
        {
            lblMensagem.Text = ex.Message;
            imgIcone.ImageUrl = "images/icones/aviso.gif";
            lblMensagem.Visible = true;
            divMensagem.Visible = true;


            ClsLog.insereLog(ClsLog.enumTipoLog.ERRO, user.IDusuario.ToString(), this.Request.Path, "0", ex.ToString());
        }
    }

    /// <summary>
    /// RowDataBound do Grid Recorr�nciaLog
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gdRecorrencia_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if ((e.Row.RowState.ToString() == "Normal") || (e.Row.RowState.ToString() == "Alternate"))
            {
                //>>> Formata data do campo data da recorr�ncia para mostrar horas. >>>>>
                string strFormatoDataSimples = ClsParametro.DataCompletaExibicao;
                Label lblDataRecorrencia = (Label)e.Row.FindControl("lblDataRecorrencia");
                if (lblDataRecorrencia.Text.Trim() != string.Empty)
                {
                    DateTime dataRecorrencia = Convert.ToDateTime(lblDataRecorrencia.Text.Trim());
                    e.Row.Cells[4].Text = dataRecorrencia.Date.ToString(strFormatoDataSimples);
                }
                //<<< Fim Formata data <<<<
            }
        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ClsLog.enumTipoLog.ERRO, user.IDusuario.ToString(), this.Request.Path, "0", ex.ToString());

        }
    }

    /// <summary>
    /// Evento que torna visivel a guia de solu��o.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lkbSolucao_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtCdProblema.Text.Trim() != string.Empty)
            {
                WUCSolucaoFiltro1.PreencheCampo("Problema", Convert.ToInt32(txtCdProblema.Text.Trim()));
                WUCSolucaoFiltro1.Filtrar();
            }
            mtwAbas.ActiveViewIndex = 4;

        }
        catch (Exception ex)
        {
            lblMensagem.Text = ex.Message;
            imgIcone.ImageUrl = "images/icones/aviso.gif";
            lblMensagem.Visible = true;
            divMensagem.Visible = true;

            ClsLog.insereLog(ClsLog.enumTipoLog.ERRO, user.IDusuario.ToString(), this.Request.Path, "0", ex.ToString());
        }
    }

    /// <summary>
    /// Solu��o - Evento RowDataBound do Grid que apresenta as solu��es.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvSolucaoProblema_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if ((e.Row.RowState.ToString() == "Normal") || (e.Row.RowState.ToString() == "Alternate"))
            {
                string strFormatoDataSimples = ClsParametro.DataCompletaExibicao;
                Label lblDtInclusao = (Label)e.Row.FindControl("lblDataInclusao");
                if (lblDtInclusao.Text.Trim() != string.Empty)
                {
                    DateTime dataInclusao = Convert.ToDateTime(lblDtInclusao.Text.Trim());
                    e.Row.Cells[4].Text = dataInclusao.Date.ToString(strFormatoDataSimples);
                }
            }
        }
        catch (Exception ex)
        {
            lblMensagem.Text = ex.Message;
            imgIcone.ImageUrl = "images/icones/aviso.gif";
            lblMensagem.Visible = true;
            divMensagem.Visible = true;

            ClsLog.insereLog(ClsLog.enumTipoLog.ERRO, user.IDusuario.ToString(), this.Request.Path, "0", ex.ToString());

        }
    }
    #endregion
}