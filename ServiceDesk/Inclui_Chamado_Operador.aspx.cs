using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ServiceDesk.Negocio;

public partial class Inclui_Chamado_Operador : BasePage
{
    protected static string strCodigoRede;
    protected static string strCodigoUsuario;
    protected static string strTipoUsuarioCodigo;

    protected void Page_Load(object sender, EventArgs e)
    {
        CheckAcesso(46);

        lblMensagem.Visible = false;
        divMensagem.Visible = false;

        if (!Page.IsPostBack)
        {

            populaNoRaiz();

            this.txtContador.Text = "1000";
            mvwAbas.ActiveViewIndex = 0;
            WUCAnexo1.DesabilitarBotaoSalvar();

            if (Convert.ToInt32(ClsParametro.QuantidaMaximaSelecaoServicos) > 0) //0 -sem limite
                lblServicosDesejadosQuantidade.Text = "(Permitido apenas " + ClsParametro.QuantidaMaximaSelecaoServicos.ToString() + " serviço(s) por chamado)";

            if (strTipoUsuarioCodigo == ClsParametro.CodigoUsuarioFinal)
                WUCUsuario1.PreencheDadosPessoa(Convert.ToInt32(strCodigoUsuario));

            //Ações
            ClsAcao.geraDropDownList(ddlAcao, "IC", "0");
        }
    }

    protected void cbkAgendar_CheckedChanged(object sender, EventArgs e)
    {
        if (!this.chkAgendar.Checked)
        {
            //this.dpkDataAgendamento.Enabled = false;
            this.tpHoraAgendamento.Enabled = false;
            //this.dpkDataAgendamento.Visible = false;
            this.tpHoraAgendamento.Visible = false;
            this.lblDataAgendamento.Visible = false;
            this.lblHoraAgendamento.Visible = false;
        }
        else
        {
            //this.dpkDataAgendamento.Enabled = true;
            this.tpHoraAgendamento.Enabled = true;
            //this.dpkDataAgendamento.Visible = true;
            this.tpHoraAgendamento.Visible = true;
            this.lblDataAgendamento.Visible = true;
            this.lblHoraAgendamento.Visible = true;
        }
    }

    #region VerificaAcao
    /// <summary>
    /// Verifica se o IC tem ações relacionadas e 
    /// se a ação foi selecionada.
    /// </summary>
    /// <returns>Retorna true ou false. Se foi aprovado ou não.</returns>
    public bool VerificaAcao(out string strMensagem)
    {
        strMensagem = string.Empty;

        if (trv_servico.CheckedNodes[0].Value != string.Empty)
        {
            if (SServiceDesk.Negocio.ClsVinculo.VerificaSeRegistroJaExiste("Acao", 0, "IC", Convert.ToInt32(trv_servico.CheckedNodes[0].Value)) == true && ddlAcao.SelectedValue == string.Empty)
            {
                strMensagem = "Por favor, selecione uma das ações que estão para o item de configuração selecionado.";
                return false;
            }
            else return true;
        }
        return true;
    }
    #endregion

    #region Salva um chamado
    /// <summary>
    /// Salva um chamado
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSalvar_Click(object sender, EventArgs e)
    {
        bool bOcorreuErro = false;
        divMensagem.Visible = false;
        string strFormatoData = ClsParametro.DataInclusao;
        string strDataMinimaSistema = ClsParametro.DataMinimaSistema;
        String strMensagem = String.Empty;

        //================================================================//
        // - O que: Verifica se o item de configução selecionado tem 
        //   ações associados e verifica se o usuário selecionou uma ação.
        // - Quem: Fernanda Passos
        // - Quando: 03/03/2006 ás 15:37hs
        //================================================================//
        if (trv_servico.CheckedNodes.Count != 0)
        {
            if (VerificaAcao(out strMensagem) == false)
            {
                lblMensagem.Text = strMensagem;
                imgIcone.ImageUrl = "images/icones/info.gif";
                lblMensagem.Visible = true;
                divMensagem.Visible = true;
                bOcorreuErro = true;
            }
        }
        //================================================================//

        //Validações
        if (WUCUsuario1.PessoaCodigo() == 0)
        {
            lblMensagem.Text = "Por favor selecione o solicitante.";
            imgIcone.ImageUrl = "images/icones/aviso.gif";
            lblMensagem.Visible = true;
            divMensagem.Visible = true;
            bOcorreuErro = true;
        }

        if ((this.chkAgendar.Checked) && (bOcorreuErro == false))
        {
            //if ((this.dpkDataAgendamento.SelectedDate.Date.ToString() == string.Empty) || (this.tpHoraAgendamento.SelectedTime.TimeOfDay.ToString() == string.Empty))
            //{
            //    lblMensagem.Text = "Por favor informe a data e horário de agendamento.";
            //    imgIcone.ImageUrl = "images/icones/aviso.gif";
            //    lblMensagem.Visible = true;
            //    divMensagem.Visible = true;
            //    bOcorreuErro = true;
            //}
            //if ((this.dpkDataAgendamento.SelectedDate.Date < DateTime.Now.Date))
            //{
            //    lblMensagem.Text = "A data de agendamento deve ser uma data futura.";
            //    imgIcone.ImageUrl = "images/icones/aviso.gif";
            //    lblMensagem.Visible = true;
            //    divMensagem.Visible = true;
            //    bOcorreuErro = true;
            //}
        }

        if ((this.txtDescricao.Text == "") && (bOcorreuErro == false))
        {
            lblMensagem.Text = "Por favor informe a descrição do chamado.";
            imgIcone.ImageUrl = "images/icones/aviso.gif";
            lblMensagem.Visible = true;
            divMensagem.Visible = true;
            bOcorreuErro = true;
        }


        if (Convert.ToInt32(ClsParametro.QuantidaMaximaSelecaoServicos) > 0) //0 -sem limite
        {
            ServiceDesk.FrameWork.ClsTreeView objTreeView = new ServiceDesk.FrameWork.ClsTreeView();
            int quantidadeItensSelecionados = objTreeView.quantidadeItensSelecionados(this.trv_servico);
            objTreeView = null;

            if (quantidadeItensSelecionados > Convert.ToInt32(ClsParametro.QuantidaMaximaSelecaoServicos))
            {
                lblMensagem.Text = "Selecione no máximo " + ClsParametro.QuantidaMaximaSelecaoServicos + " serviço(s) na lista de serviços disponíveis.";
                imgIcone.ImageUrl = "images/icones/aviso.gif";
                lblMensagem.Visible = true;
                divMensagem.Visible = true;
                bOcorreuErro = true;
            }
        }


        if (bOcorreuErro == false)
        {

            //cria o objeto Chamado
            ServiceDesk.Negocio.ClsChamado objChamado = new ServiceDesk.Negocio.ClsChamado();


            if (strTipoUsuarioCodigo == ClsParametro.CodigoUsuarioFinal)
                objChamado.MatriculaInclusor.Valor = "";
            else
                //Fernanda Passos - 05/07/2017 - Incluído tratamento para evitar erro na abertura de chamado. 
                objChamado.MatriculaInclusor.Valor = (string.IsNullOrEmpty(strCodigoUsuario) ? WUCUsuario1.PessoaCodigo().ToString() : strCodigoUsuario);

            objChamado.MatriculaSolicitante.Valor = WUCUsuario1.PessoaCodigo().ToString();

            //Busca de o usuário é vip. O status de Vip do chamado corresponderá ao
            //status de vip do usuário. Podendo ser alterado posteriormente.
            String strChamadoVip = "N";
            try
            {
                SqlDataReader objReaderInfoUsuario = ClsUsuario.getInfoUsuario(WUCUsuario1.PessoaCodigo().ToString());
                if (objReaderInfoUsuario.Read())
                {
                    if (objReaderInfoUsuario["flag_vip"].ToString().Trim() != string.Empty)
                    {
                        strChamadoVip = objReaderInfoUsuario["flag_vip"].ToString().Trim();
                    }
                }
                objReaderInfoUsuario.Close();
                objReaderInfoUsuario.Dispose();
                objReaderInfoUsuario = null;
            }
            catch
            { }


            objChamado.Vip.Valor = strChamadoVip;

            objChamado.Acao.Valor = ddlAcao.SelectedValue;

            if (this.txtDescricao.Text.Length > 1000)
            {
                objChamado.Descricao.Valor = ServiceDesk.Generica.ClsTexto.trocaAspaPorHtml(this.txtDescricao.Text.Substring(0, 1000));
                txtDescricao.Text = ServiceDesk.Generica.ClsTexto.trocaAspaPorHtml(this.txtDescricao.Text.Substring(0, 1000));
            }
            else
            {
                objChamado.Descricao.Valor = ServiceDesk.Generica.ClsTexto.trocaAspaPorHtml(this.txtDescricao.Text);
            }
            objChamado.DataInclusao.Valor = System.DateTime.Now.ToString(strFormatoData);

            if (this.chkAgendar.Checked)
            {
                //string myDateTimeValue = dpkDataAgendamento.SelectedDate.Date.Year.ToString() + "-" + dpkDataAgendamento.SelectedDate.Date.Month.ToString() + "-" + dpkDataAgendamento.SelectedDate.Date.Day.ToString() + " " + tpHoraAgendamento.SelectedTime.Hour.ToString() + ":" + tpHoraAgendamento.SelectedTime.Minute.ToString() + ":00";
                //if (myDateTimeValue.Trim() != string.Empty)
                //{
                //    objChamado.DataAgendamento.Valor = myDateTimeValue;
                //}
                //else
                //{
                objChamado.DataInclusao.Valor = DateTime.Now.ToString().Trim();
                //}
            }

            //Nivel e Equipe de Atendimento padrão (escalacao horizontal)
            objChamado.NivelAtendimento.Valor = ClsParametro.NivelAtendimentoPadrao;
            objChamado.Equipe.Valor = ClsParametro.EquipeAtendimentoPadrao;
            objChamado.Status.Valor = SServiceDesk.Negocio.ClsWorkFlow.primeiroStatus("CHAMADO").ToString();

            ServiceDesk.Negocio.ClsIdentificador objIdentificador = new ServiceDesk.Negocio.ClsIdentificador();
            objIdentificador.Tabela.Valor = objChamado.Atributos.NomeTabela;
            objChamado.Codigo.Valor = objIdentificador.getProximoValor().ToString();


            if (lblIDChamado.Text.Trim() == string.Empty)
            {
                if (objChamado.insere(out strMensagem))
                {
                    objIdentificador.atualizaValor();
                    //Grava a escalação horizontal padrao
                    ClsEscalacaoHorizontal objEscalacao = new ClsEscalacaoHorizontal();
                    objEscalacao.insereEscalacao(objChamado.Atributos.NomeTabela, objChamado.Codigo.Valor, objChamado.NivelAtendimento.Valor, objChamado.Equipe.Valor, objChamado.Tecnico.Valor, objChamado.MatriculaInclusor.Valor);
                    objEscalacao = null;
                    ServiceDesk.FrameWork.ClsTreeView objTreeView = new ServiceDesk.FrameWork.ClsTreeView();
                    string strItensSelecionados = string.Empty;

                    //grava itens marcados na treeview
                    objTreeView.gravaItensSelecionados(trv_servico, objChamado.Atributos.NomeTabela, objChamado.Codigo.Valor);

                    #region Cria o Log de Status Inicial
                    try
                    {
                        SServiceDesk.Negocio.ClsWorkFlow.gravaLog(Convert.ToInt32(objChamado.Codigo.Valor), objChamado.Atributos.NomeTabela, "0", objChamado.Status.Valor);
                    }
                    catch (Exception ex)
                    {

                        ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, strCodigoUsuario, this.Request.Path, "0", ex.ToString());

                    }
                    #endregion

                    //Atribui o valor do ID do Chamado na label
                    lblIDChamado.Text = objChamado.Codigo.Valor;

                    WUCAnexo1.salvaDocumento(true, lblIDChamado.Text, "CHAMADO");

                    if (strTipoUsuarioCodigo != ClsParametro.CodigoUsuarioFinal)
                    {
                        Response.Redirect(ClsParametro.RedirecionamentoAberturaChamadoOperador + "?chamado=" + lblIDChamado.Text.Trim());
                    }
                    else
                    {
                        Response.Redirect(ClsParametro.RedirecionamentoAberturaChamado + "?chamado=" + lblIDChamado.Text.Trim());
                    }

                }
                else
                {
                    strMensagem = "Não foi possível realizar a operação.<br>" + strMensagem;
                    imgIcone.ImageUrl = "images/icones/aviso.gif";
                    lblMensagem.Visible = true;
                    divMensagem.Visible = true;
                }
            }
        }
    }
    #endregion

    #region Evento mudaAba
    /// <summary>
    /// Evento do clique do Botao da aba 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void mudaAba(object sender, EventArgs e)
    {
        int intAbaSelecionada = 0;

        //Coloquei o switch pelo motivo que nao tinha certeza qual controle ficara (button/link)
        switch (sender.GetType().ToString())
        {
            case "System.Web.UI.WebControls.Button":
                {
                    Button btnAba = (Button)sender;
                    intAbaSelecionada = Convert.ToInt32(btnAba.CommandArgument);
                    mvwAbas.ActiveViewIndex = intAbaSelecionada;
                    btnAba = null;
                    break;
                }
            case "System.Web.UI.WebControls.LinkButton":
                {
                    LinkButton lkbAba = (LinkButton)sender;
                    intAbaSelecionada = Convert.ToInt32(lkbAba.CommandArgument);
                    mvwAbas.ActiveViewIndex = intAbaSelecionada;
                    lkbAba = null;
                    break;
                }
        }

        //Verificando qual aba foi escolhida
        //Esse switch pode deixar de existir, colocando o carregamento de todas as grids no momento de carregamento
        //mas o carregamento poderá ser desnecessário, uma vez nem sempre todas as abas serão carregadas
        switch (intAbaSelecionada)
        {
            case 0:
                {
                    break;
                }
            case 1:
                {
                    break;
                }
        }
    }

    #endregion

    #region metodo populaNoRaiz
    /// <summary>
    /// Método que popula os nós que não possuem pai
    /// </summary>
    public void populaNoRaiz()
    {
        String strSql = String.Empty;
        System.Data.SqlClient.SqlDataReader objDataReader = null;

        try
        {
            strSql = "SELECT ic_codigo, ic_codigo_superior, nome";
            strSql += ", (SELECT count(*) FROM IC WHERE ic_codigo_superior = item.ic_codigo) pai";
            strSql += " FROM IC item";
            strSql += " WHERE ic_codigo_superior is null";
            strSql += " AND ic_tipo_codigo = " + ClsParametro.TipoItemConfiguracaoServico;

            if (ClsUsuario.getTipoUsuarioCodigo(ClsUsuario.getCodigoRede()).ToString() == ClsParametro.CodigoUsuarioFinal)
            {
                strSql += " AND interno_ti IS null ";
            }

            strSql += " ORDER BY nome";

            objDataReader = ServiceDesk.Banco.ClsBanco.geraDataReader(strSql);

            trv_servico.Nodes.Clear();

            populaNos(objDataReader, trv_servico.Nodes);

        }
        catch (Exception ex)
        {
            //Grava Log de Erro
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, user.IDusuario.ToString(), this.Request.Path, "0", ex.ToString());

        }
        finally
        {

            objDataReader.Dispose();
            objDataReader = null;
        }

    }
    #endregion

    #region metodo populaNos
    /// <summary>
    /// Método que popula os nós
    /// </summary>
    public void populaNos(System.Data.SqlClient.SqlDataReader objDataReader, TreeNodeCollection objTreeNodeCollection)
    {
        try
        {
            while (objDataReader.Read())
            {
                TreeNode objTreeNode = new TreeNode();
                if (ClsUsuario.getTipoUsuarioCodigo(ClsUsuario.getCodigoRede()).ToString() != ClsParametro.CodigoUsuarioFinal)
                    objTreeNode.Text = "<a href='javascript:VisualizarItemConfiguracao(" + objDataReader["ic_codigo"].ToString() + ")'>" + objDataReader["nome"].ToString() + "</a>";
                else
                    objTreeNode.Text = objDataReader["nome"].ToString();

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
            //Grava Log de Erro
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, user.IDusuario.ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region metodo populaSubNivel
    /// <summary>
    /// Método que popula os nós filhos da TreeView
    /// </summary>
    /// <param name="intCodigoPai"></param>
    /// <param name="objTreeNode"></param>
    public void populaSubNivel(int intCodigoPai, TreeNode objTreeNodePai)
    {
        String strSql = String.Empty;

        strSql = "SELECT ic_codigo, ic_codigo_superior, nome";
        strSql += ", (SELECT count(*) FROM IC WHERE ic_codigo_superior = item.ic_codigo) pai";
        strSql += " FROM IC item";
        strSql += " WHERE ic_codigo_superior = " + intCodigoPai.ToString();

        if (ClsUsuario.getTipoUsuarioCodigo(ClsUsuario.getCodigoRede()).ToString() == ClsParametro.CodigoUsuarioFinal)
        {
            strSql += " AND interno_ti IS null ";
        }

        strSql += " ORDER BY nome";

        System.Data.SqlClient.SqlDataReader objDataReader = ServiceDesk.Banco.ClsBanco.geraDataReader(strSql);

        populaNos(objDataReader, objTreeNodePai.ChildNodes);

        objDataReader.Close();
        objDataReader.Dispose();
        objDataReader = null;

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
        populaSubNivel(Convert.ToInt32(e.Node.Value), e.Node);
    }
    #endregion

    protected void trv_servico_TreeNodeCheckChanged(object sender, TreeNodeEventArgs e)
    {
        int intContadorItensSelecionados = (trv_servico.CheckedNodes.Count - Convert.ToInt32(ClsParametro.QuantidaMaximaSelecaoServicos));
        int intContador = 0;

        if (intContadorItensSelecionados > 0)
            for (intContador = 0; intContador < intContadorItensSelecionados; intContador++)
                trv_servico.CheckedNodes[intContador].Checked = false;

        //Atualiza a ddl de acao para o item selecionado
        ClsAcao.geraDropDownList(ddlAcao, "IC", e.Node.Value);

    }


}