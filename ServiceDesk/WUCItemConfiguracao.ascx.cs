using System;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ServiceDesk.Negocio;

public partial class WUCItemConfiguracao : System.Web.UI.UserControl
{



    #region metodo Page_Load
    /// <summary>
    /// Metodo de Carregamento da pagina
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //Verificar acessibilidade
            /*int intCodigoFuncao = 4;
            if (!ClsUsuario.verificaAcessoUsuarioFuncao(ClsUsuario.getCodigoUsuario(ClsUsuario.getCodigoRede()), intCodigoFuncao.ToString(), ClsUsuario.getTipoUsuarioCodigo(ClsUsuario.getCodigoRede()).ToString()))
            {
                Response.Redirect("AcessoNegado.aspx", false);
                return;
            }*/

            Mensagem(false, string.Empty, false);

            string strCodigoItem = string.Empty;

            //Esconde a mensagem de erro
            lblMensagem.Visible = false;
            divMensagem.Visible = false;

            if (!Page.IsPostBack)
            {
                //Grava Log de Acesso
                ClsLog.insereLog(ClsLog.enumTipoLog.ACESSO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", "");

                ClsItemConfiguracao.geraDropDownList(ddlSuperior);
                ClsStatusTabela.geraDropDownList(ddlStatus, "IC");
                ClsItemConfiguracaoTipo.geraDropDownList(ddlTipoItem);

                SServiceDesk.Negocio.ClsPeriodoTrabalho.geraDropDownList(ddlServicoJanela);
                ddlServicoJanela.Items.Insert(0, "Selecione o Período");
                ddlServicoJanela.Items[0].Value = "";
                SServiceDesk.Negocio.ClsPeriodoTrabalho.geraDropDownList(ddlSuporte);
                ddlSuporte.Items.Insert(0, "Selecione o Período");
                ddlSuporte.Items[0].Value = "";

                txtNumero.Text = ClsItemConfiguracao.retornaUltimoNumero();

                ClsItemConfiguracaoTipo.populaNoRaiz(0, trvTipo, null, "ItemConfiguracao.aspx?");

                if (Request.QueryString["tipo"] != null)
                {
                    pnItem.Visible = false;
                    pnTab.Visible = false;
                    pnLista.Visible = true;
                    ServiceDesk.Negocio.ClsItemConfiguracao objItemConfiguracao = new ServiceDesk.Negocio.ClsItemConfiguracao();
                    objItemConfiguracao.Codigo.CampoIdentificador = false;
                    objItemConfiguracao.Tipo.CampoIdentificador = true;
                    objItemConfiguracao.Tipo.Valor = Request.QueryString["tipo"].ToString().Trim();
                    ClsItemConfiguracao.geraGridView(gvItemConfiguracao, objItemConfiguracao, true);
                    objItemConfiguracao = null;
                }
                else if (Request.QueryString["codigo"] != null)
                {
                    strCodigoItem = Request.QueryString["codigo"].ToString().Trim();
                    pnTab.Visible = true;

                    //Monta os dados na tela
                    montaDadosItemConfiguracao(Convert.ToInt32(strCodigoItem), e);

                    ClsItemConfiguracao.populaNoRaiz(ClsItemConfiguracao.retornaCodigoItemRaiz(Convert.ToInt32(strCodigoItem)), trvItemConfiguracao);

                }

            }

            if (Session["strStatus"] != null)
            {
                //Exibindo mensagem com o status da operação
                lblMensagem.Text = Session["strStatus"].ToString();
                imgIcone.ImageUrl = "images/icones/aviso.gif";
                lblMensagem.Visible = true;
                divMensagem.Visible = true;
            }

            Session["strStatus"] = null;

        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region metodo montaDadosItemConfiguracao
    /// <summary>
    /// Método que popula os controles da tela com os dados do Item de Configuração
    /// </summary>
    /// <param name="intCodigoItem">Código do Item de Configuração</param>
    /// <param name="e">Evento que chamou o método</param>
    private void montaDadosItemConfiguracao(int intCodigoItem, EventArgs e)
    {
        try
        {
            ServiceDesk.Negocio.ClsItemConfiguracao objItemConfiguracao = new ServiceDesk.Negocio.ClsItemConfiguracao(intCodigoItem);

            if (objItemConfiguracao.Superior.Valor != String.Empty)
            {
                ddlSuperior.Items[0].Selected = false;
                ddlSuperior.Items.FindByValue(objItemConfiguracao.Superior.Valor).Selected = true;
            }

            if (objItemConfiguracao.Tipo.Valor != String.Empty)
            {
                ddlTipoItem.Items[0].Selected = false;
                ddlTipoItem.Items.FindByValue(objItemConfiguracao.Tipo.Valor).Selected = true;

                //Montando a GridView dos Atributos
                ServiceDesk.Negocio.ClsItemConfiguracaoAtributo.geraGridViewPorTipo(gvAtributo, Convert.ToInt32(objItemConfiguracao.Tipo.Valor));

            }

            if (objItemConfiguracao.Janela.Valor != String.Empty)
            {
                ddlServicoJanela.SelectedIndex = -1;
                ddlServicoJanela.Items.FindByValue(objItemConfiguracao.Janela.Valor).Selected = true;
            }

            if (objItemConfiguracao.Suporte.Valor != String.Empty)
            {
                ddlSuporte.SelectedIndex = -1;
                ddlSuporte.Items.FindByValue(objItemConfiguracao.Suporte.Valor).Selected = true;
            }

            //Desabilitando o item atual na lista dos itens superiores
            ddlSuperior.Items.FindByValue(objItemConfiguracao.Codigo.Valor).Enabled = false;

            //Colocando o prefixo do Tipo do Item de Configuracao
            buscaPrefixoTipo(ddlTipoItem, e);

            txtNome.Text = objItemConfiguracao.Nome.Valor;
            txtDescricao.Text = objItemConfiguracao.Descricao.Valor;
            txtNumero.Text = objItemConfiguracao.Numero.Valor;
            if (objItemConfiguracao.InternoTI.Valor == "1")
                ckbInternoTI.Checked = true;
            else
                ckbInternoTI.Checked = false;


            if (objItemConfiguracao.StatusAtual.Valor != String.Empty)
            {
                ddlStatus.Items[0].Selected = false;
                try
                {
                    ddlStatus.Items.FindByValue(objItemConfiguracao.StatusAtual.Valor).Selected = true;
                }
                catch
                {
                }
            }

            objItemConfiguracao = null;

        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region metodo btnSalva_Click
    /// <summary>
    /// Método que salva o Tipo do Item de Configuração (inserir/alterar)
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSalva_Click(object sender, EventArgs e)
    {
        try
        {
            if (Request.QueryString["codigo"] == null)
            {
                insereItemConfiguracaoTipo();
            }
            else
            {
                if (Request.QueryString["codigo"] != null)
                {
                    alteraItemConfiguracaoTipo(Convert.ToInt32(Request.QueryString["codigo"].ToString().Trim()));
                }
            }

            switch (mtvItem.ActiveViewIndex)
            {
                case 0:
                    {
                        mudaAba(lkbAtributo, e);
                        break;
                    }
                case 1:
                    {
                        mudaAba(btnRelacionamento, e);
                        break;
                    }
                case 2:
                    {
                        mudaAba(btnEstrutura, e);
                        break;
                    }
                case 3:
                    {
                        mudaAba(btnPessoa, e);
                        break;
                    }
                case 4:
                    {
                        mudaAba(btnStatusLog, e);
                        break;
                    }
            }

            //Atualizando a TreeView
            ServiceDesk.Negocio.ClsItemConfiguracaoTipo.populaNoRaiz(0, trvTipo, null, "itemconfiguracao.aspx?");
            trvTipo.ExpandAll();

        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region metodo insereItemConfiguracaoTipo
    /// <summary>
    /// Método que insere um novo Tipo do Item de Configuração
    /// </summary>
    private void insereItemConfiguracaoTipo()
    {
        try
        {
            String strMensagem = String.Empty;
            String strMensagemAltera = String.Empty;

            ServiceDesk.Negocio.ClsItemConfiguracao objItemConfiguracao = new ServiceDesk.Negocio.ClsItemConfiguracao();
            ClsIdentificador objIdentificador = new ClsIdentificador();

            objIdentificador.Tabela.Valor = objItemConfiguracao.Atributos.NomeTabela;
            objItemConfiguracao.Codigo.Valor = objIdentificador.getProximoValor().ToString();
            if ((ddlSuperior.SelectedValue != "") && (ddlSuperior.SelectedValue != null))
            {
                objItemConfiguracao.Superior.Valor = ddlSuperior.SelectedValue;
            }
            if ((ddlTipoItem.SelectedValue != "") && (ddlTipoItem.SelectedValue != null))
            {
                objItemConfiguracao.Tipo.Valor = ddlTipoItem.SelectedValue;
            }
            if ((ddlServicoJanela.SelectedValue != "") && (ddlServicoJanela.SelectedValue != null))
            {
                objItemConfiguracao.Janela.Valor = ddlServicoJanela.SelectedValue;
            }
            if ((ddlSuporte.SelectedValue != "") && (ddlSuporte.SelectedValue != null))
            {
                objItemConfiguracao.Suporte.Valor = ddlSuporte.SelectedValue;
            }
            objItemConfiguracao.Nome.Valor = ServiceDesk.Generica.ClsTexto.trocaAspaPorHtml(txtNome.Text);
            objItemConfiguracao.Descricao.Valor = ServiceDesk.Generica.ClsTexto.trocaAspaPorHtml(txtDescricao.Text);
            objItemConfiguracao.Numero.Valor = ServiceDesk.Generica.ClsTexto.trocaAspaPorHtml(txtNumero.Text);
            if (ckbInternoTI.Checked)
                objItemConfiguracao.InternoTI.Valor = "1";
            else
                objItemConfiguracao.InternoTI.Valor = "0";

            if ((ddlStatus.SelectedValue != "") && (ddlStatus.SelectedValue != null))
            {
                objItemConfiguracao.StatusAtual.Valor = ddlStatus.SelectedValue;
            }

            if (objItemConfiguracao.insere(out strMensagem))
            {
                //Atualizando o valor na tabela identificador
                objIdentificador.atualizaValor();

                //Atualizando a chave do Item de Configuracao
                objItemConfiguracao.atualizaChave();
                objItemConfiguracao.altera(out strMensagemAltera);

                if (ddlSuperior.SelectedValue != String.Empty)
                {
                    //Atualizando a relação de pai e filho
                    ClsItemConfiguracao.atualizaRelacaoPaiFilho(Convert.ToInt32(objItemConfiguracao.Superior.Valor), Convert.ToInt32(objItemConfiguracao.Codigo.Valor));
                }

                //Informando a mensagem do status da operação
                Session["strStatus"] = "Item de Configuração inserido com sucesso.";

                Response.Redirect("itemconfiguracao.aspx?codigo=" + objItemConfiguracao.Codigo.Valor);

            }
            else
            {
                lblMensagem.Text = strMensagem;
                imgIcone.ImageUrl = "images/icones/aviso.gif";
                lblMensagem.Visible = true;
                divMensagem.Visible = true;
            }

            objItemConfiguracao = null;
            objIdentificador = null;

        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region metodo alteraItemConfiguracaoTipo
    /// <summary>
    /// Método que altera novo Tipo do Item de Configuração
    /// </summary>
    private void alteraItemConfiguracaoTipo(int intCodigoItem)
    {
        try
        {
            String strMensagem = String.Empty;
            bool bolAtualizaStatusLog = false;
            String strStatusOrigem = String.Empty;

            ServiceDesk.Negocio.ClsItemConfiguracao objItemConfiguracao = new ServiceDesk.Negocio.ClsItemConfiguracao(intCodigoItem);

            objItemConfiguracao.Superior.Valor = ddlSuperior.SelectedValue;
            objItemConfiguracao.Tipo.Valor = ddlTipoItem.SelectedValue;
            objItemConfiguracao.Nome.Valor = ServiceDesk.Generica.ClsTexto.trocaAspaPorHtml(txtNome.Text);
            objItemConfiguracao.Descricao.Valor = ServiceDesk.Generica.ClsTexto.trocaAspaPorHtml(txtDescricao.Text);
            objItemConfiguracao.Numero.Valor = ServiceDesk.Generica.ClsTexto.trocaAspaPorHtml(txtNumero.Text);
            if (ckbInternoTI.Checked)
                objItemConfiguracao.InternoTI.Valor = "1";
            else
                objItemConfiguracao.InternoTI.Valor = "0";

            //Verificando se foi alterado o status do Item de Configuração
            if (objItemConfiguracao.StatusAtual.Valor.Trim() != ddlStatus.SelectedValue.Trim())
            {
                bolAtualizaStatusLog = true;
                strStatusOrigem = objItemConfiguracao.StatusAtual.Valor.Trim();
            }
            objItemConfiguracao.StatusAtual.Valor = ddlStatus.SelectedValue;

            if ((ddlServicoJanela.SelectedValue != "") && (ddlServicoJanela.SelectedValue != null))
            {
                objItemConfiguracao.Janela.Valor = ddlServicoJanela.SelectedValue;
            }
            if ((ddlSuporte.SelectedValue != "") && (ddlSuporte.SelectedValue != null))
            {
                objItemConfiguracao.Suporte.Valor = ddlSuporte.SelectedValue;
            }

            objItemConfiguracao.atualizaChave();
            if (objItemConfiguracao.altera(out strMensagem))
            {
                if (ddlSuperior.SelectedValue != String.Empty)
                {
                    //Atualizando a relação de pai e filho
                    ClsItemConfiguracao.atualizaRelacaoPaiFilho(Convert.ToInt32(objItemConfiguracao.Superior.Valor), Convert.ToInt32(objItemConfiguracao.Codigo.Valor));
                }

                ClsItemConfiguracao.atualizaChaveFilhos(intCodigoItem);

                if (bolAtualizaStatusLog)
                {
                    ClsStatusLog objStatusLog = new ClsStatusLog();

                    ClsIdentificador objIdentificador = new ClsIdentificador();
                    objIdentificador.Tabela.Valor = objStatusLog.Atributos.NomeTabela;
                    objStatusLog.Codigo.Valor = objIdentificador.getProximoValor().ToString();
                    objStatusLog.Pessoa.Valor = ClsUsuario.getCodigoUsuario().ToString();
                    objStatusLog.StatusOrigem.Valor = strStatusOrigem;
                    objStatusLog.StatusDestino.Valor = ddlStatus.SelectedValue.Trim();
                    objStatusLog.Data.Valor = DateTime.Now.ToString(ClsParametro.DataInclusao);
                    objStatusLog.Tabela.Valor = objItemConfiguracao.Atributos.NomeTabela;
                    objStatusLog.TabelaIdentificador.Valor = objItemConfiguracao.Codigo.Valor;
                    if (objStatusLog.insere(out strMensagem))
                    {
                        objIdentificador.atualizaValor();
                    }

                    objIdentificador = null;
                    objStatusLog = null;
                }

                strMensagem = "Item de Configuração alterado com sucesso.";
            }

            lblMensagem.Text = strMensagem;
            imgIcone.ImageUrl = "images/icones/aviso.gif";
            lblMensagem.Visible = true;
            divMensagem.Visible = true;

            objItemConfiguracao = null;

        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region evento trvTipo_TreeNodePopulate
    /// <summary>
    /// Evento que ocorre quando um no da tree view é criado
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void trvTipo_TreeNodePopulate(object sender, TreeNodeEventArgs e)
    {
        try
        {
            ServiceDesk.Negocio.ClsItemConfiguracaoTipo.populaNoRaiz(Convert.ToInt32(e.Node.Value), null, e.Node, "itemconfiguracao.aspx?");

        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region Evento gvItemConfiguracao_OnRowCommand
    /// <summary>
    /// Evento que ocorre quando um comando da GridView é solicitado
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvItemConfiguracao_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Editar")
            {
                GridViewRow objRow = gvItemConfiguracao.Rows[Convert.ToInt32(e.CommandArgument)];
                if (objRow != null)
                {
                    Label lblCodigo = (Label)objRow.FindControl("lblCodigo");

                    Response.Redirect("itemconfiguracao.aspx?codigo=" + lblCodigo.Text);
                }
                objRow = null;
                ClsItemConfiguracao.geraGridView(gvItemConfiguracao);
            }

        }
        catch (Exception ex)
        {

            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region Evento gvItemConfiguracao_PageIndexChanging
    /// <summary>
    /// Evento que ocorre quando é mudada a página da GridView
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvItemConfiguracao_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvItemConfiguracao.PageIndex = e.NewPageIndex;
            ClsItemConfiguracao.geraGridView(gvItemConfiguracao);

        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region Evento gvItemConfiguracao_RowDataBound
    /// <summary>
    /// Evento que ocorre na montagem da GridView
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvItemConfiguracao_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            try
            {
                Label lblCodigo = (Label)e.Row.Cells[0].Controls[1];
                Label lblCodigoTipo = (Label)e.Row.Cells[0].Controls[3];
                Label lblPrefixoNumero = (Label)e.Row.Cells[0].Controls[5];
                Label lblCodigoStatus = (Label)e.Row.Cells[2].Controls[1];

                ServiceDesk.Negocio.ClsItemConfiguracao objItemConfiguracao = new ServiceDesk.Negocio.ClsItemConfiguracao(Convert.ToInt32(lblCodigo.Text));

                lblPrefixoNumero.Text = buscaPrefixoTipo(Convert.ToInt32(lblCodigoTipo.Text)) + objItemConfiguracao.Numero.Valor;

                if (lblCodigoStatus.Text != String.Empty)
                {
                    ServiceDesk.Negocio.ClsStatus objStatus = new ServiceDesk.Negocio.ClsStatus(Convert.ToInt32(lblCodigoStatus.Text));
                    lblCodigoStatus.Text = objStatus.Descricao.Valor;
                    objStatus = null;
                }
            }
            catch (Exception ex)
            {
                ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
            }
        }
    }
    #endregion

    #region Evento gvItemConfiguracao_Load
    /// <summary>
    /// Ocorre depois que a grid é carregada
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvItemConfiguracao_Load(object sender, EventArgs e)
    {
        try
        {
            GridView objGridView = (GridView)sender;
            if (objGridView.Rows.Count <= 0)
            {
                lblMensagemGrid.Visible = true;
                lblMensagemGrid.Text = "Nenhum Item de Configuração cadastrado para o Tipo Selecionado.";
            }

        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region Evento buscaPrefixoTipo
    /// <summary>
    /// Evento que ocorre quando alterado o indice do DropDownList ddlTipoItem
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void buscaPrefixoTipo(object sender, EventArgs e)
    {
        int intCodigoTipoItem = 0;

        try
        {
            intCodigoTipoItem = Convert.ToInt32(ddlTipoItem.SelectedValue);
            txtPrefixoTipo.Text = buscaPrefixoTipo(intCodigoTipoItem);

        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region Evento buscaPrefixoTipo
    /// <summary>
    /// Método que retorna o Prefixo do Tipo do Item de Configuração
    /// </summary>
    /// <param name="intCodigoTipo">Código do Tipo do Item de Configuração</param>
    public String buscaPrefixoTipo(int intCodigoTipo)
    {
        String strRetorno = String.Empty;
        try
        {
            ServiceDesk.Negocio.ClsItemConfiguracaoTipo objItemConfiguracaoTipo = new ServiceDesk.Negocio.ClsItemConfiguracaoTipo(intCodigoTipo);
            strRetorno = objItemConfiguracaoTipo.Prefixo.Valor;
            objItemConfiguracaoTipo = null;

        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
        return strRetorno;
    }
    #endregion

    #region Evento gvAtributo_OnRowCommand
    /// <summary>
    /// Evento que ocorre quando um comando da GridView é solicitado
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvAtributo_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            String strCodigoItem = String.Empty;

            if (Request.QueryString["codigo"] != null)
            {
                strCodigoItem = Request.QueryString["codigo"].ToString().Trim();
            }

            if (e.CommandName == "Salvar")
            {
                GridViewRow objRow = gvAtributo.Rows[Convert.ToInt32(e.CommandArgument)];
                if (objRow != null)
                {
                    if (strCodigoItem != String.Empty)
                    {
                        Label lblCodigo = (Label)objRow.FindControl("lblCodigo");
                        TextBox txtValor = (TextBox)objRow.FindControl("txtValor");
                        ClsItemConfiguracao.insereTipoAtributoValor(Convert.ToInt32(strCodigoItem), Convert.ToInt32(lblCodigo.Text), txtValor.Text);
                    }
                }
                objRow = null;

                if (strCodigoItem != String.Empty)
                {
                    ServiceDesk.Negocio.ClsItemConfiguracao objItemConfiguracao = new ServiceDesk.Negocio.ClsItemConfiguracao(Convert.ToInt32(strCodigoItem));
                    if (objItemConfiguracao.Tipo.Valor != String.Empty)
                    {
                        ServiceDesk.Negocio.ClsItemConfiguracaoAtributo.geraGridViewPorTipo(gvAtributo, Convert.ToInt32(objItemConfiguracao.Tipo.Valor));
                    }
                    objItemConfiguracao = null;
                }
            }

        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region Evento gvAtributo_PageIndexChanging
    /// <summary>
    /// Evento que ocorre quando é mudada a página da GridView
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvAtributo_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvAtributo.PageIndex = e.NewPageIndex;
            if (Request.QueryString["codigo"] != null)
            {
                ServiceDesk.Negocio.ClsItemConfiguracao objItemConfiguracao = new ServiceDesk.Negocio.ClsItemConfiguracao(Convert.ToInt32(Request.QueryString["codigo"].ToString().Trim()));
                if (objItemConfiguracao.Tipo.Valor != String.Empty)
                {
                    ServiceDesk.Negocio.ClsItemConfiguracaoAtributo.geraGridViewPorTipo(gvAtributo, Convert.ToInt32(objItemConfiguracao.Tipo.Valor));
                }
                objItemConfiguracao = null;
            }
        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region Evento gvAtributo_RowDataBound
    /// <summary>
    /// Evento que ocorre na montagem da GridView
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvAtributo_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        String strCodigoItem = String.Empty;

        if (Request.QueryString["codigo"] != null)
        {
            strCodigoItem = Request.QueryString["codigo"].ToString().Trim();
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            try
            {
                Label lblCodigo = (Label)e.Row.Cells[0].Controls[1];
                TextBox txtValor = (TextBox)e.Row.Cells[2].Controls[1];
                Label lblMedida = (Label)e.Row.Cells[2].Controls[3];

                txtValor.Text = ClsItemConfiguracao.retornaTipoAtributoValor(Convert.ToInt32(strCodigoItem), Convert.ToInt32(lblCodigo.Text));

                if (lblMedida.Text != String.Empty)
                {
                    SServiceDesk.Negocio.ClsUnidadeMedida objUnidadeMedida = new SServiceDesk.Negocio.ClsUnidadeMedida(Convert.ToInt32(lblMedida.Text.Trim()));
                    lblMedida.Text = objUnidadeMedida.SiglaMedida.Valor;
                    objUnidadeMedida = null;
                }

            }
            catch (Exception ex)
            {
                ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
            }
        }
    }
    #endregion

    #region Evento salvaAtributos
    /// <summary>
    /// Salva os valores dos atributos de todos os atributos listados na GridView gvAtributo
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void salvaAtributos(object sender, EventArgs e)
    {
        try
        {
            divMensagem.Visible = false;

            int intContador = 0;
            String strCodigoItem = String.Empty;

            if (Request.QueryString["codigo"] != null) strCodigoItem = Request.QueryString["codigo"].ToString().Trim();

            for (intContador = 0; intContador < gvAtributo.Rows.Count; intContador++)
            {
                GridViewRow objRow = (GridViewRow)gvAtributo.Rows[intContador];
                if (objRow != null)
                {
                    if (strCodigoItem != String.Empty)
                    {
                        Label lblCodigo = (Label)objRow.FindControl("lblCodigo");
                        TextBox txtValor = (TextBox)objRow.FindControl("txtValor");
                        ClsItemConfiguracao.insereTipoAtributoValor(Convert.ToInt32(strCodigoItem), Convert.ToInt32(lblCodigo.Text), txtValor.Text);
                    }
                }
                objRow = null;
            }
            if (intContador > 0)
            {
                Mensagem(true, "Operação realizada com sucesso.", false);
            }
        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }

    }
    #endregion

    #region Mensagem
    /// <summary>
    /// Exibição de mensagem
    /// </summary>
    /// <param name="bolExibir">Recebe true ou false. Se para exibir ou não a mensagem</param>
    /// <param name="strMensagem">Conteúdo da mensagem a ser exibida</param>
    /// <param name="bolMsgCritica">Recebe true ou false. Se é para usar ícone crítico ou não</param>
    public void Mensagem(bool bolExibir, String strMensagem, bool bolMsgCritica)
    {
        try
        {
            divMensagem.Visible = false;
            lblMensagem.Visible = false;

            if (bolExibir == true && strMensagem.Trim() != string.Empty)
            {
                divMensagem.Visible = true;
                lblMensagem.Text = strMensagem;
                lblMensagem.Visible = true;
                if (bolMsgCritica == true) imgIcone.ImageUrl = "images/icones/aviso.gif";
                else imgIcone.ImageUrl = "images/icones/info.gif";
            }
            else
            {
                divMensagem.Visible = false;
                lblMensagem.Visible = false;
            }
        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region Evento mudaAba
    /// <summary>
    /// Evento do clique do Botao da aba atributo
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void mudaAba(object sender, EventArgs e)
    {
        try
        {

            int intAbaSelecionada = 0;
            int intCodigoItem = 0;

            if (Request.QueryString["codigo"] != null)
            {
                intCodigoItem = Convert.ToInt32(Request.QueryString["codigo"].ToString().Trim());
            }

            switch (sender.GetType().ToString())
            {
                case "System.Web.UI.WebControls.Button":
                    {
                        Button btnAba = (Button)sender;
                        intAbaSelecionada = Convert.ToInt32(btnAba.CommandArgument);
                        mtvItem.ActiveViewIndex = intAbaSelecionada;
                        btnAba = null;
                        break;
                    }
                case "System.Web.UI.WebControls.LinkButton":
                    {
                        LinkButton lkbAba = (LinkButton)sender;
                        intAbaSelecionada = Convert.ToInt32(lkbAba.CommandArgument);
                        mtvItem.ActiveViewIndex = intAbaSelecionada;
                        lkbAba = null;
                        break;
                    }
            }

            //Verificando qual aba foi escolhida
            switch (intAbaSelecionada)
            {
                case 0:
                    {
                        break;
                    }
                case 1:
                    {
                        //Montando a grid Item Configuração x Relacao
                        ClsItemConfiguracao.geraGridRelacao(gvRelacionamento, intCodigoItem);
                        ClsItemConfiguracao.geraDropDownListItem(ddlItemConfiguracaoFilho, intCodigoItem);
                        ServiceDesk.Negocio.ClsItemConfiguracaoRelacaoTipo.geraDropDownList(ddlRelacaoTipo, 0);
                        break;
                    }
                case 2:
                    {
                        SServiceDesk.Negocio.ClsTipoEstruturaOrganizacional.geraDropDownList(ddlEstruturaTipo);
                        SServiceDesk.Negocio.ClsEstruturaOrganizacional objEstrutura = new SServiceDesk.Negocio.ClsEstruturaOrganizacional();
                        objEstrutura.CodigoEstrutura.CampoIdentificador = false;
                        objEstrutura.TipoEstruturaCodigo.Valor = ddlEstruturaTipo.SelectedValue;
                        objEstrutura.TipoEstruturaCodigo.CampoIdentificador = true;
                        SServiceDesk.Negocio.ClsEstruturaOrganizacional.geraGridView(gvEstrutura, objEstrutura, true);
                        ServiceDesk.Negocio.ClsItemConfiguracaoEstruturaOrganizacionalTipo.geraDropDownList(ddlItemEstruturaTipo, 0);
                        objEstrutura = null;
                        break;
                    }
                case 3:
                    {
                        //Alimenta Grid
                        SServiceDesk.Negocio.ClsPessoa.geraGridView(gvPessoa);

                        //Alimenta drop de tipo de relacionamento
                        ServiceDesk.Negocio.ClsItemConfiguracaoRelacaoPessoa.geraDropDownList(ddlItemPessoaTipo, 0);

                        //Alimenta drop das empresas
                        SServiceDesk.Negocio.ClsEstruturaOrganizacional.geraDropDownListPorEmpresa(ddlEstruturaOrganizacional, Convert.ToInt32(ClsParametro.CodigoTipoEmpresa));
                        this.ddlEstruturaOrganizacional.Items.Insert(0, "Todos");
                        this.ddlEstruturaOrganizacional.Items[0].Value = "";


                        break;
                    }
                case 4:
                    {
                        ClsStatusLog objStatusLog = new ClsStatusLog();
                        objStatusLog.Codigo.CampoIdentificador = false;
                        objStatusLog.Tabela.Valor = "IC";
                        objStatusLog.Tabela.CampoIdentificador = true;
                        objStatusLog.TabelaIdentificador.Valor = intCodigoItem.ToString();
                        objStatusLog.TabelaIdentificador.CampoIdentificador = true;
                        ServiceDesk.Negocio.ClsStatusLog.geraGridView(gvStatusLog, objStatusLog, true);
                        objStatusLog = null;
                        break;
                    }
                case 5:
                    {
                        ServiceDesk.Negocio.ClsAcao.geraGridView(gvAcao);
                        CheckAcaoAssociaAoIC();
                        break;
                    }
            }
            //Colocando o prefixo do Tipo do Item de Configuracao
            buscaPrefixoTipo(ddlTipoItem, e);
        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region evento novoItemConfiguracao
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void novoItemConfiguracao(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect(Page.Request.FilePath);
        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region Evento montaEstruturaPorTipo
    /// <summary>
    /// Muda a Grid View Estrutura Organizacional de acordo com o tipo de Estrutura Organizacional selecionada
    /// Ocorre quando alterado a seleção do DropDownList do Tipo de Estrutura
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void montaEstruturaPorTipo(object sender, EventArgs e)
    {
        try
        {
            SServiceDesk.Negocio.ClsEstruturaOrganizacional objEstrutura = new SServiceDesk.Negocio.ClsEstruturaOrganizacional();
            objEstrutura.CodigoEstrutura.CampoIdentificador = false;
            objEstrutura.TipoEstruturaCodigo.Valor = ddlEstruturaTipo.SelectedValue;
            objEstrutura.TipoEstruturaCodigo.CampoIdentificador = true;
            SServiceDesk.Negocio.ClsEstruturaOrganizacional.geraGridView(gvEstrutura, objEstrutura, true);
            objEstrutura = null;

        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region Evento gvRelacionamento_OnRowCommand
    /// <summary>
    /// Evento que ocorre quando um comando da GridView é solicitado
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvRelacionamento_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Excluir")
            {
                string strCodigoItem = string.Empty;

                GridViewRow objRow = gvRelacionamento.Rows[Convert.ToInt32(e.CommandArgument)];
                if (objRow != null)
                {
                    Label lblCodigoTipoRelacao = (Label)objRow.FindControl("lblCodigoTipoRelacao");
                    Label lblCodigoIcDestino = (Label)objRow.FindControl("lblCodigoIcDestino");
                    Label lblCodigoOrigem = (Label)objRow.FindControl("lblCodigoOrigem");

                    if (lblCodigoOrigem != null && lblCodigoIcDestino != null && lblCodigoTipoRelacao != null)
                    {
                        if (ClsItemConfiguracao.ExcluiRelacaoIcComIc(Convert.ToInt32(lblCodigoOrigem.Text.Trim()), Convert.ToInt32(lblCodigoIcDestino.Text.Trim()), Convert.ToInt32(lblCodigoTipoRelacao.Text.Trim())) == true)
                        {
                            Mensagem(true, "Exclusão realizada com sucesso.", false);
                            ClsItemConfiguracao.geraGridRelacao(gvRelacionamento, Convert.ToInt32(lblCodigoOrigem.Text.Trim()));
                        }
                        else Mensagem(true, "Não foi possível excluir. Entre em contato com o administrador do sistema.", true);
                    }
                }
                objRow = null;

            }
        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region Evento gvRelacionamento_PageIndexChanging
    /// <summary>
    /// Evento que ocorre quando é mudada a página da GridView
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvRelacionamento_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvAtributo.PageIndex = e.NewPageIndex;
    }
    #endregion

    #region Evento gvRelacionamento_RowDataBound
    /// <summary>
    /// Evento que ocorre na montagem da GridView
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvRelacionamento_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            String strCodigoItem = String.Empty;

            if (Request.QueryString["codigo"] != null)
            {
                strCodigoItem = Request.QueryString["codigo"].ToString().Trim();
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                try
                {
                    Label lblCodigo = (Label)e.Row.FindControl("lblCodigo");

                    ServiceDesk.Negocio.ClsItemConfiguracao objItemConfiguracao = new ServiceDesk.Negocio.ClsItemConfiguracao(Convert.ToInt32(lblCodigo.Text.Trim()));
                    lblCodigo.Text = objItemConfiguracao.Nome.Valor;
                    objItemConfiguracao = null;

                }
                catch
                {
                    //
                }
            }

        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region Evento btnSalvarRelacionamento_Click
    /// <summary>
    /// Evento do botão salvar relacionamento. Insere um novo relacionamento entre itens de configuração
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSalvarRelacionamento_Click(object sender, EventArgs e)
    {
        try
        {
            String strMensagem = String.Empty;
            int intCodigoItem = 0;

            if (Request.QueryString["codigo"] != null)
            {
                intCodigoItem = Convert.ToInt32(Request.QueryString["codigo"].ToString().Trim());
            }

            int intCodigoDestino = Convert.ToInt32(ddlItemConfiguracaoFilho.SelectedValue);
            int intCodigoTipo = Convert.ToInt32(ddlRelacaoTipo.SelectedValue);

            //Verificando se a relacao é de Pai para Filho
            if (intCodigoTipo == ClsItemConfiguracao.retornaItemRelacaoPai())
            {
                ClsItemConfiguracao.atualizaRelacaoPaiFilho(intCodigoItem, intCodigoDestino);
            }
            else if (intCodigoTipo == ClsItemConfiguracao.retornaItemRelacaoFilho())
            {
                ClsItemConfiguracao.atualizaRelacaoPaiFilho(intCodigoDestino, intCodigoItem);
            }
            else
            {
                ClsItemConfiguracao.insereItemConfiguracaoRelacao(intCodigoItem, intCodigoDestino, intCodigoTipo);
            }

            Mensagem(true, "Operação realizada com sucesso.", false);

            mudaAba(btnRelacionamento, e);
            montaDadosItemConfiguracao(intCodigoItem, e);

        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region Evento gvEstrutura_OnRowCommand
    /// <summary>
    /// Evento que ocorre quando um comando da GridView é solicitado
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvEstrutura_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
    }
    #endregion

    #region Evento gvEstrutura_PageIndexChanging
    /// <summary>
    /// Evento que ocorre quando é mudada a página da GridView
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvEstrutura_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            GridView objGridView = (GridView)sender;
            objGridView.PageIndex = e.NewPageIndex;
            objGridView = null;

        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region Evento gvEstrutura_RowDataBound
    /// <summary>
    /// Evento que ocorre na montagem da GridView
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvEstrutura_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            int intCodigoItem = 0;
            int intCodigoEstrutura = 0;
            int intCodigoEstruturaTipo = 0;

            if (Request.QueryString["codigo"] != null)
            {
                intCodigoItem = Convert.ToInt32(Request.QueryString["codigo"].ToString().Trim());
            }

            try
            {
                intCodigoEstruturaTipo = Convert.ToInt32(ddlItemEstruturaTipo.SelectedValue);
            }
            catch
            {
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                try
                {
                    CheckBox chbCodigo = (CheckBox)e.Row.Cells[0].Controls[1];
                    Label lblCodigo = (Label)e.Row.Cells[0].Controls[3];

                    if (lblCodigo.Text != String.Empty)
                    {
                        intCodigoEstrutura = ClsItemConfiguracao.retornaItemEstruturaTipo(intCodigoItem, Convert.ToInt32(lblCodigo.Text), intCodigoEstruturaTipo);
                    }

                    if (intCodigoEstrutura > 0)
                    {
                        chbCodigo.Checked = true;
                    }

                }
                catch
                {
                }
            }

        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region Evento pesquisarPessoaPorNome
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void pesquisarPessoaPorNome(object sender, EventArgs e)
    {
        if (ddlEstruturaOrganizacional.SelectedValue == string.Empty) SServiceDesk.Negocio.ClsPessoa.geraGridViewBusca(gvPessoa, ServiceDesk.Generica.ClsTexto.trocaAspaPorHtml(txtPessoaNomeBusca.Text), String.Empty, 0, 0); else SServiceDesk.Negocio.ClsPessoa.geraGridViewBusca(gvPessoa, ServiceDesk.Generica.ClsTexto.trocaAspaPorHtml(txtPessoaNomeBusca.Text), String.Empty, Convert.ToInt32(ddlEstruturaOrganizacional.SelectedValue.Trim()), 0);
    }
    #endregion

    #region Evento gvPessoa_OnRowCommand
    /// <summary>
    /// Evento que ocorre quando um comando da GridView é solicitado
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvPessoa_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
    }
    #endregion

    #region Evento gvPessoa_PageIndexChanging
    /// <summary>
    /// Evento que ocorre quando é mudada a página da GridView
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvPessoa_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            GridView objGridView = (GridView)sender;
            objGridView.PageIndex = e.NewPageIndex;
            pesquisarPessoaPorNome(sender, e);
            objGridView = null;
        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region Evento gvPessoa_RowDataBound
    /// <summary>
    /// Evento que ocorre na montagem da GridView
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvPessoa_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            int intCodigoItem = 0;
            int intCodigoItemPessoa = 0;

            if (Request.QueryString["codigo"] != null)
            {
                intCodigoItem = Convert.ToInt32(Request.QueryString["codigo"].ToString().Trim());
            }
            try
            {
                intCodigoItemPessoa = Convert.ToInt32(ddlItemPessoaTipo.SelectedValue);
            }
            catch
            {
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                try
                {
                    CheckBox chbCodigo = (CheckBox)e.Row.Cells[0].Controls[1];
                    Label lblCodigo = (Label)e.Row.Cells[0].Controls[3];

                    if (lblCodigo.Text != String.Empty)
                    {
                        intCodigoItemPessoa = ClsItemConfiguracao.retornaItemPessoaTipo(intCodigoItem, Convert.ToInt32(lblCodigo.Text), intCodigoItemPessoa);
                    }

                    if (intCodigoItemPessoa > 0)
                    {
                        chbCodigo.Checked = true;
                    }

                }
                catch
                {
                }
            }

        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region Evento insereItemEstrutura
    /// <summary>
    /// Evento que ocorre quando clicado no botão Salvar Item Configuração x Estrutura Organizacional
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void insereItemEstrutura(object sender, EventArgs e)
    {
        try
        {
            int intContador = 0;
            int intCodigoItem = 0;

            if (Request.QueryString["codigo"] != null)
            {
                intCodigoItem = Convert.ToInt32(Request.QueryString["codigo"].ToString().Trim());
            }

            //Excluir todos relacionamento que tiverem o mesmo tipo da estrutura selecionado no dropdownlist
            ClsItemConfiguracao.excluiItemConfiguracaoEstrutura(intCodigoItem, Convert.ToInt32(ddlEstruturaTipo.SelectedValue), Convert.ToInt32(ddlItemEstruturaTipo.SelectedValue));

            for (intContador = 0; intContador < gvEstrutura.Rows.Count; intContador++)
            {
                GridViewRow objRow = (GridViewRow)gvEstrutura.Rows[intContador];
                if (objRow != null)
                {
                    CheckBox chbCodigo = (CheckBox)objRow.FindControl("chbCodigo");
                    Label lblCodigoEstrutura = (Label)objRow.FindControl("lblCodigo");
                    if (chbCodigo.Checked)
                    {
                        //Insere uma nova relacao entre item configuracao x estrutura organizacional
                        ClsItemConfiguracao.insereItemConfiguracaoEstrutura(intCodigoItem, Convert.ToInt32(lblCodigoEstrutura.Text), Convert.ToInt32(ddlItemEstruturaTipo.SelectedValue));
                    }
                }
                objRow = null;
            }

            Mensagem(true, "Operação realizada com sucesso.", false);

        }
        catch (Exception ex)
        {
            Mensagem(true, "Não foi possível realizar a operação.", true);
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region Evento insereItemRelacaoPessoa
    /// <summary>
    /// Evento que ocorre quando clicado no botão Salvar Item Configuração x Pessoa
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void insereItemRelacaoPessoa(object sender, EventArgs e)
    {
        try
        {
            int intContador = 0;
            int intCodigoItem = 0;
            int intCodigoItemPessoa = 0;

            if (Request.QueryString["codigo"] != null)
            {
                intCodigoItem = Convert.ToInt32(Request.QueryString["codigo"].ToString().Trim());
            }

            try
            {
                intCodigoItemPessoa = Convert.ToInt32(ddlItemPessoaTipo.SelectedValue);
            }
            catch
            {
            }

            ClsItemConfiguracao.excluiItemConfiguracaoPessoa(intCodigoItem, intCodigoItemPessoa);

            for (intContador = 0; intContador < gvPessoa.Rows.Count; intContador++)
            {
                GridViewRow objRow = (GridViewRow)gvPessoa.Rows[intContador];
                if (objRow != null)
                {
                    CheckBox chbCodigo = (CheckBox)objRow.FindControl("chbCodigo");
                    Label lblCodigo = (Label)objRow.FindControl("lblCodigo");
                    if (chbCodigo.Checked)
                    {
                        //Insere uma nova relacao entre item configuracao x estrutura organizacional
                        ClsItemConfiguracao.insereItemConfiguracaoPessoa(intCodigoItem, Convert.ToInt32(lblCodigo.Text), intCodigoItemPessoa);
                    }
                }
                objRow = null;
            }

            Mensagem(true, "Operação realizada com sucesso", false);
        }
        catch (Exception ex)
        {
            Mensagem(true, "Não foi possível realizar esta operação", true);
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region Evento atualizaItemPessoaTipo
    /// <summary>
    /// Evento que ocorre quando é alterado o indice do DropDown list com os tipos do item de configuração pessoa
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void atualizaItemPessoaTipo(object sender, EventArgs e)
    {
        try
        {
            DropDownList ddlObjetoAtual = (DropDownList)sender;
            int intCodigoItemPessoa = Convert.ToInt32(ddlObjetoAtual.SelectedValue);
            mudaAba(btnPessoa, e);
            ddlObjetoAtual.Items.FindByValue(intCodigoItemPessoa.ToString()).Selected = true;

        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region Evento gvStatusLog_OnRowCommand
    /// <summary>
    /// Evento que ocorre quando um comando da GridView é solicitado
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvStatusLog_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
    }
    #endregion

    #region Evento gvStatusLog_PageIndexChanging
    /// <summary>
    /// Evento que ocorre quando é mudada a página da GridView
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvStatusLog_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            GridView objGridView = (GridView)sender;
            objGridView.PageIndex = e.NewPageIndex;
            objGridView = null;

        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region Evento gvStatusLog_RowDataBound
    /// <summary>
    /// Evento que ocorre na montagem da GridView
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvStatusLog_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            int intCodigoItem = 0;

            if (Request.QueryString["codigo"] != null)
            {
                intCodigoItem = Convert.ToInt32(Request.QueryString["codigo"].ToString().Trim());
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                try
                {
                    Label lblPessoaCodigo = (Label)e.Row.Cells[0].Controls[1];
                    Label lblPessoa = (Label)e.Row.Cells[0].Controls[3];
                    Label lblStatusOrigem = (Label)e.Row.Cells[1].Controls[1];
                    Label lblStatusDestino = (Label)e.Row.Cells[2].Controls[1];
                    Label lblData = (Label)e.Row.Cells[3].Controls[1];

                    lblPessoa.Text = ClsUsuario.getNomeUsuario(lblPessoaCodigo.Text);

                    lblStatusOrigem.Text = ServiceDesk.Negocio.ClsStatus.getDescricaoStatus(lblStatusOrigem.Text);
                    lblStatusDestino.Text = ServiceDesk.Negocio.ClsStatus.getDescricaoStatus(lblStatusDestino.Text);

                }
                catch
                {
                }
            }

        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region Evento btnCopiaItem_Click
    /// <summary>
    /// Evento que chama a função que realizará a cópia do item
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCopiaItem_Click(object sender, EventArgs e)
    {
        try
        {
            int intCodigoItem = 0;
            int intCodigoItemCopiado = 0;
            String strMensagem = String.Empty;

            if (Request.QueryString["codigo"] != null)
            {
                intCodigoItem = Convert.ToInt32(Request.QueryString["codigo"].ToString().Trim());
            }

            if (intCodigoItem > 0)
            {
                ServiceDesk.Negocio.ClsItemConfiguracao objItemConfiguracao = new ServiceDesk.Negocio.ClsItemConfiguracao(intCodigoItem);
                intCodigoItemCopiado = objItemConfiguracao.copiaItem(0, objItemConfiguracao, out strMensagem);
                objItemConfiguracao = null;
            }

            if (intCodigoItemCopiado > 0)
            {
                Session["strStatus"] = "Item de Configuração copiado com sucesso.";
                Response.Redirect("itemconfiguracao.aspx?codigo=" + intCodigoItemCopiado.ToString());
            }
            else
            {
                lblMensagem.Text = "Não foi possível copiar o Item de Configuração.<br>" + strMensagem;
                imgIcone.ImageUrl = "images/icones/aviso.gif";
                lblMensagem.Visible = true;
                divMensagem.Visible = true;
            }

        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region evento trvItemConfiguracao
    /// <summary>
    /// Evento que ocorre quando um no da tree view é criado
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void trvItemConfiguracao_TreeNodePopulate(object sender, TreeNodeEventArgs e)
    {
        try
        {
            ClsItemConfiguracao.populaSubNivel(Convert.ToInt32(e.Node.Value), e.Node);

        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region Evento gvAcao_OnRowCommand
    /// <summary>
    /// Evento que ocorre quando um comando da GridView é solicitado
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvAcao_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            String strCodigoItem = String.Empty;

            if (Request.QueryString["codigo"] != null)
            {
                strCodigoItem = Request.QueryString["codigo"].ToString().Trim();
            }

            if (e.CommandName == "Salvar")
            {

            }

        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region Evento gvAcao_PageIndexChanging
    /// <summary>
    /// Evento que ocorre quando é mudada a página da GridView
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvAcao_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            GridView objGridView = (GridView)sender;
            objGridView.PageIndex = e.NewPageIndex;
            objGridView = null;

        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region Evento gvAcao_RowDataBound
    /// <summary>
    /// Evento que ocorre na montagem da GridView
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvAcao_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            String strCodigoItem = String.Empty;

            if (Request.QueryString["codigo"] != null)
            {
                strCodigoItem = Request.QueryString["codigo"].ToString().Trim();
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                try
                {
                    Label lblCodigo = (Label)e.Row.FindControl("lblCodigo");
                    TextBox txtValor = (TextBox)e.Row.FindControl("txtValor");

                    txtValor.Text = ServiceDesk.Negocio.ClsAcao.retornaValorItemAcao(Convert.ToInt32(strCodigoItem), Convert.ToInt32(lblCodigo.Text));

                }
                catch (Exception ex)
                {
                    Response.Write(ex.ToString());
                }
            }

        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region salvaAcao
    /// <summary>
    /// Evento que salva o valor de cada ação
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void salvaAcao(object sender, EventArgs e)
    {
        try
        {
            int intContador = 0;
            String strCodigoItem = String.Empty;
            String strMensagem = string.Empty;

            if (Request.QueryString["codigo"] != null)
            {
                strCodigoItem = Request.QueryString["codigo"].ToString().Trim();
            }

            //=================================================================//
            // - O que: Associa ações selecionadas ao item de configuração     //                                                    
            // - Quem: Fernanda Passos                                         //
            // - Quando: 03/02/2006 as 10:43hs                                 //
            //=================================================================//
            int intCount = 0;

            SServiceDesk.Negocio.ClsVinculo objVinculo = new SServiceDesk.Negocio.ClsVinculo();

            string strSql = "delete vinculo ";
            strSql += " where tabela_origem = 'Acao'";
            strSql += " and  tabela_destino = 'IC'";
            strSql += " and identificador_destino = " + Convert.ToInt32(strCodigoItem.Trim()) + "";

            ServiceDesk.Banco.ClsBanco objBanco = new ServiceDesk.Banco.ClsBanco();
            objBanco.executaSQL(strSql);
            objBanco = null;

            while (intCount < gvAcao.Rows.Count)
            {
                GridViewRow objRow = (GridViewRow)gvAcao.Rows[intCount];
                if (objRow != null)
                {
                    CheckBox cbxAssociaAcao = (CheckBox)objRow.FindControl("cbxAssociaAcao");
                    Label lblCodigo = (Label)objRow.FindControl("lblCodigo");

                    if (cbxAssociaAcao.Checked == true) SServiceDesk.Negocio.ClsVinculo.CriaVinculoEntreTabela("Acao", "IC", Convert.ToInt32(lblCodigo.Text.Trim()), Convert.ToInt32(strCodigoItem), out strMensagem);
                }
                intCount++;
            }
            objVinculo = null;

            CheckAcaoAssociaAoIC();

            //=================================================================//
            // - O que: Insere a descrição da ação associada ao IC             //                                                    
            // - Quem: Diego Pandolf                                           //
            // - Quando:                                                       //
            //=================================================================//
            for (intContador = 0; intContador < gvAcao.Rows.Count; intContador++)
            {
                GridViewRow objRow = (GridViewRow)gvAcao.Rows[intContador];
                if (objRow != null)
                {
                    if (strCodigoItem != String.Empty)
                    {
                        Label lblCodigo = (Label)objRow.FindControl("lblCodigo");
                        TextBox txtValor = (TextBox)objRow.FindControl("txtValor");

                        ServiceDesk.Negocio.ClsAcao.insereValorItemAcao(Convert.ToInt32(strCodigoItem), Convert.ToInt32(lblCodigo.Text), txtValor.Text);
                    }
                }
                objRow = null;
            }
            Mensagem(true, "Operação realizada com sucesso.", false);
        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region Coloca um Check na ação associada ao IC
    /// <summary>
    /// Verificar se a ação esta selecionada ao IC e marca como selecionada.
    /// </summary>
    public void CheckAcaoAssociaAoIC()
    {
        try
        {
            int intCount = 0;
            string strCodigoItem = string.Empty;

            if (Request.QueryString["codigo"] != null)
            {
                strCodigoItem = Request.QueryString["codigo"].ToString().Trim();
            }

            if (strCodigoItem == string.Empty) return;

            //=================================================================//
            // - O que: Executa a verificação e associação                     //                                                    
            // - Quem: Fernanda Passos                                         //
            // - Quando: 03/02/2006 ás 11:17hs                                 //
            //=================================================================//
            while (intCount < gvAcao.Rows.Count)
            {
                GridViewRow objRow = (GridViewRow)gvAcao.Rows[intCount];
                if (objRow != null)
                {
                    CheckBox cbxAssociaAcao = (CheckBox)objRow.FindControl("cbxAssociaAcao");
                    Label lblCodigo = (Label)objRow.FindControl("lblCodigo");

                    if (ClsItemConfiguracao.VerificaSeAcaoAssociadaAoIC(Convert.ToInt32(lblCodigo.Text.Trim()), Convert.ToInt32(strCodigoItem.Trim())) == true)
                        cbxAssociaAcao.Checked = true;
                    else
                        cbxAssociaAcao.Checked = false;
                }

                intCount++;
            }
        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

}