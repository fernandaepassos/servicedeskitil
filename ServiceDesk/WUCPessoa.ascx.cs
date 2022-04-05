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

public partial class WUCPessoa : System.Web.UI.UserControl
{

    #region Evento Page_Load
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        //Verificar acessibilidade
        /*int intCodigoFuncao = 33;
        if (!ServiceDesk.Negocio.ClsUsuario.verificaAcessoUsuarioFuncao(ClsUsuario.getCodigoUsuario(ClsUsuario.getCodigoRede()), intCodigoFuncao.ToString(), ClsUsuario.getTipoUsuarioCodigo(ClsUsuario.getCodigoRede()).ToString()))
        {
            Response.Redirect("AcessoNegado.aspx", false);
            return;
        }*/

        if (!Page.IsPostBack)
        {
            if (Request.QueryString["codigo"] != null)
            {
                pnPessoa.Visible = true;
                pnGrid.Visible = false;
                montaDropDows();
                if (Request.QueryString["codigo"].ToString() != "0")
                {
                    montaPessoaDados(Convert.ToInt32(Request.QueryString["codigo"].ToString()));
                }
            }
            else
            {

                SServiceDesk.Negocio.ClsEstruturaOrganizacional.geraDropDownListPorEmpresa(ddlEstruturaOrganizacionalPesquisa, Convert.ToInt32(ClsParametro.CodigoTipoEmpresa));
                ddlEstruturaOrganizacionalPesquisa.Items.Insert(0, "Selecione a Empresa");
                ddlEstruturaOrganizacionalPesquisa.Items[0].Value = "";

                if (ClsParametro.CodigoEmpresa != null)
                {
                    ListItem objItem = (ListItem)ddlEstruturaOrganizacionalPesquisa.Items.FindByValue(ClsParametro.CodigoEmpresa.ToString());
                    if (objItem != null)
                    {
                        objItem.Selected = true;
                    }
                    objItem = null;
                }

                SServiceDesk.Negocio.ClsTipoUsuario.geraDropDownList(ddpTipoUsuarioPesquisa);
                ddpTipoUsuarioPesquisa.Items.Insert(0, "Selecione o Tipo de Usuário");
                ddpTipoUsuarioPesquisa.Items[0].Value = "";

                SServiceDesk.Negocio.ClsPessoa.geraGridView(gvPessoa);
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
    #endregion

    #region Metodo montaDropDows
    /// <summary>
    /// 
    /// </summary>
    private void montaDropDows()
    {

        SServiceDesk.Negocio.ClsEstruturaOrganizacional.geraDropDownListPorEmpresa(ddlEstruturaOrganizacional, Convert.ToInt32(ClsParametro.CodigoTipoEmpresa));
        ddlEstruturaOrganizacional.Items.Insert(0, "Selecione a Empresa");
        ddlEstruturaOrganizacional.Items[0].Value = "";

        SServiceDesk.Negocio.ClsCargo.geraDropDownList(ddlCargo);
        ddlCargo.Items.Insert(0, "Selecione o Cargo");
        ddlCargo.Items[0].Value = "";

        ddlTipoColaborador.Items.Insert(0, "Selecione o Tipo de Colaborador");
        ddlTipoColaborador.Items[0].Value = "";
        ddlTipoColaborador.Items.Insert(1, "Interno");
        ddlTipoColaborador.Items[1].Value = "I";
        ddlTipoColaborador.Items.Insert(2, "Externo");
        ddlTipoColaborador.Items[2].Value = "E";

        ddlSexo.Items.Insert(0, "Selecione o Sexo");
        ddlSexo.Items[0].Value = "";
        ddlSexo.Items.Insert(1, "Masculino");
        ddlSexo.Items[1].Value = "M";
        ddlSexo.Items.Insert(2, "Feminino");
        ddlSexo.Items[2].Value = "F";

        ServiceDesk.Negocio.ClsStatusTabela.geraDropDownList(ddlStatus, "pessoa");
        //  ddlStatus.Items.Insert(0, "Selecione o Status");
        //ddlStatus.Items[0].Value = "";
        //ddlStatus.Items.Insert(1, "Ativo");
        //ddlStatus.Items[1].Value = "16";
        //ddlStatus.Items.Insert(2, "Inativo");
        //ddlStatus.Items[2].Value = "20";

        SServiceDesk.Negocio.ClsTipoUsuario.geraDropDownList(ddlTipoUsuario);
        ddlTipoUsuario.Items.Insert(0, "Selecione o Tipo de Usuário");
        ddlTipoUsuario.Items[0].Value = "";
    }
    #endregion

    #region Evento gvPessoa_RowCommand
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvPessoa_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        String strMensagem = String.Empty;

        switch (e.CommandName)
        {
            case "Select":
                {
                    GridViewRow objRow = gvPessoa.Rows[Convert.ToInt32(e.CommandArgument)];
                    Label lblCodigo = (Label)objRow.FindControl("lblCodigo");
                    Response.Redirect(Page.Request.FilePath + "?codigo=" + lblCodigo.Text);
                    lblCodigo = null;
                    objRow = null;
                    break;
                }
            case "Excluir":
                {
                    GridViewRow objRow = gvPessoa.Rows[Convert.ToInt32(e.CommandArgument)];
                    if (objRow != null)
                    {
                        Label lblCodigo = (Label)objRow.FindControl("lblCodigo");

                        SServiceDesk.Negocio.ClsPessoa objPessoa = new SServiceDesk.Negocio.ClsPessoa();
                        objPessoa.Codigo.Valor = lblCodigo.Text;
                        try
                        {
                            if (objPessoa.exclui(out strMensagem))
                            {
                                lblMensagem.Text = "Pessoa excluída com sucesso.";
                                imgIcone.ImageUrl = "images/icones/info.gif";
                                divMensagem.Visible = true;
                            }
                            else
                            {
                                lblMensagem.Text = "Não foi possível excluir a Pessoa.<br>";
                                lblMensagem.Text += strMensagem;
                                imgIcone.ImageUrl = "images/icones/info.gif";
                                divMensagem.Visible = true;
                            }
                        }
                        catch
                        {
                            lblMensagem.Text = "Não foi possível excluir a Pessoa.<br>";
                            imgIcone.ImageUrl = "images/icones/aviso.gif";
                            divMensagem.Visible = true;
                        }

                        objRow = null;
                        objPessoa = null;
                    }

                    gvPessoa.SelectedIndex = -1;
                    SServiceDesk.Negocio.ClsPessoa.geraGridView(gvPessoa);

                    break;
                }
        }
    }
    #endregion

    #region Evento gvPessoa_RowDataBound
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvPessoa_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //adiciona um evento javascript no botão Excluir
            ImageButton btnExcluir = (ImageButton)e.Row.Cells[6].Controls[0];
            btnExcluir.Attributes.Add("onclick", "return verifica();");

            Label lblEstruturaOrganizacional = (Label)e.Row.FindControl("lblEstruturaOrganizacional");
            if (lblEstruturaOrganizacional.Text != String.Empty)
            {
                SServiceDesk.Negocio.ClsEstruturaOrganizacional objEstruturaOrganizacional = new SServiceDesk.Negocio.ClsEstruturaOrganizacional(Convert.ToInt32(lblEstruturaOrganizacional.Text));
                lblEstruturaOrganizacional.Text = objEstruturaOrganizacional.Descricao.Valor;
                objEstruturaOrganizacional = null;
            }

            Label lblTipoUsuario = (Label)e.Row.FindControl("lblTipoUsuario");
            if (lblTipoUsuario.Text != String.Empty)
            {
                SServiceDesk.Negocio.ClsTipoUsuario objTipoUsuario = new SServiceDesk.Negocio.ClsTipoUsuario();
                objTipoUsuario.alimentaTipoUsuario(Convert.ToInt32(lblTipoUsuario.Text));
                lblTipoUsuario.Text = objTipoUsuario.Descricao.Valor;
                objTipoUsuario = null;
            }
        }
    }
    #endregion

    #region Evento gvPessoa_PageIndexChanging
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvPessoa_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView objGridView = (GridView)sender;
        objGridView.PageIndex = e.NewPageIndex;
        //SServiceDesk.Negocio.ClsPessoa.geraGridView(objGridView);
        pesquisar(sender, e);
        objGridView = null;
    }
    #endregion

    #region Evento imgNovaPessoa_Click
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void imgNovaPessoa_Click(object sender, ImageClickEventArgs e)
    {
        pnPessoa.Visible = true;
        pnGrid.Visible = false;
        montaDropDows();
        Response.Redirect(Page.Request.FilePath + "?codigo=0");
    }
    #endregion

    #region Evento salvaPessoa
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void salvaPessoa(object sender, EventArgs e)
    {
        if (Request.QueryString["codigo"] != String.Empty)
        {
            if (Request.QueryString["codigo"].ToString() == "0")
            {
                inserePessoa();
            }
            else
            {
                alteraPessoa(Convert.ToInt32(Request.QueryString["codigo"].ToString()));
            }
        }
    }
    #endregion

    #region Metodo inserePessoa
    /// <summary>
    /// 
    /// </summary>
    private void inserePessoa()
    {
        try
        {
            String strMensagem = String.Empty;
            SServiceDesk.Negocio.ClsPessoa objPessoa = new SServiceDesk.Negocio.ClsPessoa();
            ServiceDesk.Negocio.ClsIdentificador objIdentificador = new ServiceDesk.Negocio.ClsIdentificador();
            objIdentificador.Tabela.Valor = objPessoa.Atributos.NomeTabela;

            objPessoa.Codigo.Valor = objIdentificador.getProximoValor().ToString();
            objPessoa.Matricula.Valor = txtMatricula.Text;
            objPessoa.Nome.Valor = ServiceDesk.Generica.ClsTexto.trocaAspaPorHtml(txtNome.Text);
            objPessoa.EstruturaCodigo.Valor = ddlEstruturaOrganizacional.SelectedValue;
            objPessoa.CargoCodigo.Valor = ddlCargo.SelectedValue;
            objPessoa.TipoUsuarioCodigo.Valor = ddlTipoUsuario.SelectedValue;

            if (cldDataInicioTrabalho.PostedDate != String.Empty)
            {
                objPessoa.DataInicioTrabalho.Valor = cldDataInicioTrabalho.SelectedDate.ToString(ClsParametro.DataSimplesBanco, null);
            }
            else objPessoa.DataInicioTrabalho.Valor = String.Empty;

            if (cldDataFimTrabalho.PostedDate != String.Empty)
            {
                objPessoa.DataFimTrabalho.Valor = cldDataFimTrabalho.SelectedDate.ToString(ClsParametro.DataSimplesBanco, null);
            }
            else objPessoa.DataFimTrabalho.Valor = String.Empty;

            objPessoa.TipoColaborador.Valor = ddlTipoColaborador.SelectedValue;
            objPessoa.Ramal.Valor = txtRamal.Text;
            objPessoa.Email.Valor = txtEmail.Text;

            if (cldDataNascimento.PostedDate != String.Empty)
            {
                objPessoa.DataNascimento.Valor = cldDataNascimento.SelectedDate.ToString(ClsParametro.DataSimplesBanco, null);
            }
            else objPessoa.DataNascimento.Valor = String.Empty;

            objPessoa.Sexo.Valor = ddlSexo.SelectedValue;
            objPessoa.Cpf.Valor = txtCpf.Text;
            objPessoa.Logradouro.Valor = ServiceDesk.Generica.ClsTexto.trocaAspaPorHtml(txtLogradouro.Text);
            objPessoa.Bairro.Valor = ServiceDesk.Generica.ClsTexto.trocaAspaPorHtml(txtBairro.Text);
            objPessoa.Cidade.Valor = ServiceDesk.Generica.ClsTexto.trocaAspaPorHtml(txtCidade.Text);
            objPessoa.Uf.Valor = txtUf.Text;
            objPessoa.Cep.Valor = txtCep.Text;
            objPessoa.Telefone.Valor = txtTelefone.Text;
            try
            {
                objPessoa.LinhaOnibus.Valor = Convert.ToInt32(txtLinhaOnibus.Text).ToString();
            }
            catch
            {
                strMensagem = "A Linha de Ônibus deve ser um valor númerico.";
            }
            try
            {
                objPessoa.PontoOnibus.Valor = Convert.ToInt32(txtPontoOnibus.Text).ToString();
            }
            catch
            {
                strMensagem = "O Ponto do Ônibus deve ser um valor númerico.";
            }
            objPessoa.CodigoRede.Valor = txtCodigoRede.Text;

            try
            {
                objPessoa.ValorHora.Valor = ServiceDesk.Generica.ClsTexto.getNumeroInsercao(txtValorHora.Text, null);
            }
            catch
            {
                strMensagem = "O Valor da hora deve ser um valor númerico.";
            }

            objPessoa.FlagVip.Valor = rblVip.Text;
            objPessoa.LocalizacaoFisica.Valor = ServiceDesk.Generica.ClsTexto.trocaAspaPorHtml(txtLocalizacaoFisica.Text);
            objPessoa.Senha.Valor = txtSenha.Text;
            objPessoa.Status.Valor = ddlStatus.SelectedValue;
            objPessoa.TipoSangue.Valor = txtTipoSangue.Text;
            objPessoa.NomeGuerra.Valor = ServiceDesk.Generica.ClsTexto.trocaAspaPorHtml(txtNomeGuerra.Text);
            objPessoa.Cnh.Valor = txtCnh.Text;

            if (cldDataExpedicao.PostedDate != String.Empty)
            {
                objPessoa.DataExpedicaoCnh.Valor = cldDataExpedicao.SelectedDate.ToString(ClsParametro.DataSimplesBanco, null);
            }
            else objPessoa.DataExpedicaoCnh.Valor = String.Empty;

            if (cldDataValidade.PostedDate != String.Empty)
            {
                objPessoa.DataValidadeCnh.Valor = cldDataValidade.SelectedDate.ToString(ClsParametro.DataSimplesBanco, null);
            }
            else objPessoa.DataValidadeCnh.Valor = String.Empty;

            if (strMensagem != String.Empty)
            {
                strMensagem = "Não foi possível realizar a operação.<br>" + strMensagem;
                imgIcone.ImageUrl = "images/icones/aviso.gif";
            }
            else if (SServiceDesk.Negocio.ClsPessoa.existeMatricula(objPessoa.Matricula.Valor))
            {
                strMensagem = "Não foi possível realizar a operação.<br>";
                strMensagem += "Já existe uma pessoa com a matrícula informada.";
                imgIcone.ImageUrl = "images/icones/aviso.gif";
            }
            else if ((objPessoa.Senha.Valor != txtConfirmaSenha.Text) && (objPessoa.Senha.Valor != String.Empty))
            {
                strMensagem = "Não foi possível realizar a operação.<br>";
                strMensagem += "Os campos senha e confirma senha não coincidem!";
                imgIcone.ImageUrl = "images/icones/aviso.gif";
            }
            else if (objPessoa.insere(out strMensagem))
            {
                objIdentificador.atualizaValor();
                imgIcone.ImageUrl = "images/icones/info.gif";

                Session["strStatus"] = strMensagem;

                Response.Redirect(Page.Request.FilePath + "?codigo=" + objPessoa.Codigo.Valor);

            }
            else
            {
                imgIcone.ImageUrl = "images/icones/aviso.gif";
            }

            lblMensagem.Text = strMensagem;
            lblMensagem.Visible = true;
            divMensagem.Visible = true;

            objIdentificador = null;
            objPessoa = null;
        }
        catch (Exception ex)
        {
            Response.Write(ex.ToString());
        }
    }
    #endregion

    #region Metodo alteraPessoa
    /// <summary>
    /// 
    /// </summary>
    /// <param name="intPessoaCodigo"></param>
    private void alteraPessoa(int intPessoaCodigo)
    {
        try
        {
            String strMensagem = String.Empty;
            SServiceDesk.Negocio.ClsPessoa objPessoa = new SServiceDesk.Negocio.ClsPessoa();

            objPessoa.Codigo.Valor = intPessoaCodigo.ToString();
            objPessoa.Matricula.Valor = txtMatricula.Text;
            objPessoa.Nome.Valor = ServiceDesk.Generica.ClsTexto.trocaAspaPorHtml(txtNome.Text);
            objPessoa.EstruturaCodigo.Valor = ddlEstruturaOrganizacional.SelectedValue;
            objPessoa.CargoCodigo.Valor = ddlCargo.SelectedValue;
            objPessoa.TipoUsuarioCodigo.Valor = ddlTipoUsuario.SelectedValue;

            if (cldDataInicioTrabalho.PostedDate != String.Empty)
            {
                objPessoa.DataInicioTrabalho.Valor = cldDataInicioTrabalho.SelectedDate.ToString(ClsParametro.DataSimplesBanco, null);
            }
            else objPessoa.DataInicioTrabalho.Valor = String.Empty;

            if (cldDataFimTrabalho.PostedDate != String.Empty)
            {
                objPessoa.DataFimTrabalho.Valor = cldDataFimTrabalho.SelectedDate.ToString(ClsParametro.DataSimplesBanco, null);
            }
            else objPessoa.DataFimTrabalho.Valor = String.Empty;

            objPessoa.TipoColaborador.Valor = ddlTipoColaborador.SelectedValue;
            objPessoa.Ramal.Valor = txtRamal.Text;
            objPessoa.Email.Valor = txtEmail.Text;

            if (cldDataNascimento.PostedDate != String.Empty)
            {
                objPessoa.DataNascimento.Valor = cldDataNascimento.SelectedDate.ToString(ClsParametro.DataSimplesBanco, null);
            }
            else objPessoa.DataNascimento.Valor = String.Empty;

            objPessoa.Sexo.Valor = ddlSexo.SelectedValue;
            objPessoa.Cpf.Valor = txtCpf.Text;
            objPessoa.Logradouro.Valor = ServiceDesk.Generica.ClsTexto.trocaAspaPorHtml(txtLogradouro.Text);
            objPessoa.Bairro.Valor = ServiceDesk.Generica.ClsTexto.trocaAspaPorHtml(txtBairro.Text);
            objPessoa.Cidade.Valor = ServiceDesk.Generica.ClsTexto.trocaAspaPorHtml(txtCidade.Text);
            objPessoa.Uf.Valor = txtUf.Text;
            objPessoa.Cep.Valor = txtCep.Text;
            objPessoa.Telefone.Valor = txtTelefone.Text;
            try
            {
                if (txtLinhaOnibus.Text != String.Empty)
                {
                    objPessoa.LinhaOnibus.Valor = Convert.ToInt32(txtLinhaOnibus.Text).ToString();
                }
            }
            catch
            {
                strMensagem = "A Linha de Ônibus deve ser um valor númerico.";
            }
            try
            {
                if (txtPontoOnibus.Text != String.Empty)
                {
                    objPessoa.PontoOnibus.Valor = Convert.ToInt32(txtPontoOnibus.Text).ToString();
                }
            }
            catch
            {
                strMensagem = "O Ponto do Ônibus deve ser um valor númerico.";
            }
            objPessoa.CodigoRede.Valor = txtCodigoRede.Text;

            try
            {
                objPessoa.ValorHora.Valor = ServiceDesk.Generica.ClsTexto.getNumeroInsercao(txtValorHora.Text, null);
            }
            catch
            {
                strMensagem = "O Valor da hora deve ser um valor númerico.";
            }

            objPessoa.FlagVip.Valor = rblVip.Text;
            objPessoa.LocalizacaoFisica.Valor = ServiceDesk.Generica.ClsTexto.trocaAspaPorHtml(txtLocalizacaoFisica.Text);
            objPessoa.Senha.Valor = txtSenha.Text;
            objPessoa.Status.Valor = ddlStatus.SelectedValue;
            objPessoa.TipoSangue.Valor = txtTipoSangue.Text;
            objPessoa.NomeGuerra.Valor = ServiceDesk.Generica.ClsTexto.trocaAspaPorHtml(txtNomeGuerra.Text);
            objPessoa.Cnh.Valor = txtCnh.Text;

            if (cldDataExpedicao.PostedDate != String.Empty)
            {
                objPessoa.DataExpedicaoCnh.Valor = cldDataExpedicao.SelectedDate.ToString(ClsParametro.DataSimplesBanco, null);
            }
            else objPessoa.DataExpedicaoCnh.Valor = String.Empty;

            if (cldDataValidade.PostedDate != String.Empty)
            {
                objPessoa.DataValidadeCnh.Valor = cldDataValidade.SelectedDate.ToString(ClsParametro.DataSimplesBanco, null);
            }
            else objPessoa.DataValidadeCnh.Valor = String.Empty;

            if (strMensagem != String.Empty)
            {
                strMensagem = "Não foi possível realizar a operação.<br>" + strMensagem;
                imgIcone.ImageUrl = "images/icones/aviso.gif";
            }
            else if (objPessoa.existeMatricula())
            {
                strMensagem = "Não foi possível realizar a operação.<br>";
                strMensagem += "Já existe uma pessoa com a matrícula informada.";
                imgIcone.ImageUrl = "images/icones/aviso.gif";
            }
            else if ((objPessoa.Senha.Valor != txtConfirmaSenha.Text) && (objPessoa.Senha.Valor != String.Empty))
            {
                strMensagem = "Não foi possível realizar a operação.<br>";
                strMensagem += "Os campos senha e confirma senha não coincidem!!!";
                imgIcone.ImageUrl = "images/icones/aviso.gif";
            }
            else if (objPessoa.altera(out strMensagem))
            {
                imgIcone.ImageUrl = "images/icones/info.gif";
            }
            else
            {
                imgIcone.ImageUrl = "images/icones/aviso.gif";
            }

            lblMensagem.Text = strMensagem;
            lblMensagem.Visible = true;
            divMensagem.Visible = true;

            objPessoa = null;
        }
        catch (Exception ex)
        {
            Response.Write(ex.ToString());
        }
    }
    #endregion

    #region metodo montaPessoaDados
    /// <summary>
    /// 
    /// </summary>
    /// <param name="intPessoaCodigo"></param>
    private void montaPessoaDados(int intPessoaCodigo)
    {
        try
        {
            SServiceDesk.Negocio.ClsPessoa objPessoa = new SServiceDesk.Negocio.ClsPessoa(intPessoaCodigo);
            txtNome.Text = objPessoa.Nome.Valor;
            txtMatricula.Text = objPessoa.Matricula.Valor;
            if (objPessoa.EstruturaCodigo.Valor != String.Empty)
            {
                ListItem objItem = ddlEstruturaOrganizacional.Items.FindByValue(objPessoa.EstruturaCodigo.Valor);
                if (objItem != null)
                {
                    ddlEstruturaOrganizacional.SelectedValue = objPessoa.EstruturaCodigo.Valor;
                }
                objItem = null;
            }
            if (objPessoa.CargoCodigo.Valor != String.Empty)
            {
                ddlCargo.SelectedValue = objPessoa.CargoCodigo.Valor;
            }
            if (objPessoa.TipoUsuarioCodigo.Valor != String.Empty)
            {
                ddlTipoUsuario.SelectedValue = objPessoa.TipoUsuarioCodigo.Valor;
            }
            if (objPessoa.TipoColaborador.Valor != String.Empty)
            {
                ddlTipoColaborador.SelectedValue = objPessoa.TipoColaborador.Valor;
            }
            if (objPessoa.Sexo.Valor != String.Empty)
            {
                ddlSexo.SelectedValue = objPessoa.Sexo.Valor;
            }
            if (objPessoa.DataInicioTrabalho.Valor != String.Empty)
            {
                cldDataInicioTrabalho.SelectedDate = Convert.ToDateTime(objPessoa.DataInicioTrabalho.Valor);
            }
            if (objPessoa.DataFimTrabalho.Valor != String.Empty)
            {
                cldDataFimTrabalho.SelectedDate = Convert.ToDateTime(objPessoa.DataFimTrabalho.Valor);
            }
            if (objPessoa.DataNascimento.Valor != String.Empty)
            {
                cldDataNascimento.SelectedDate = Convert.ToDateTime(objPessoa.DataNascimento.Valor);
            }
            txtEmail.Text = objPessoa.Email.Valor;
            txtRamal.Text = objPessoa.Ramal.Valor;
            txtCpf.Text = objPessoa.Cpf.Valor;
            txtLogradouro.Text = objPessoa.Logradouro.Valor;
            txtBairro.Text = objPessoa.Bairro.Valor;
            txtCidade.Text = objPessoa.Cidade.Valor;
            txtUf.Text = objPessoa.Uf.Valor;
            txtCep.Text = objPessoa.Cep.Valor;
            txtTelefone.Text = objPessoa.Telefone.Valor;
            txtLinhaOnibus.Text = objPessoa.LinhaOnibus.Valor;
            txtPontoOnibus.Text = objPessoa.PontoOnibus.Valor;
            txtCodigoRede.Text = objPessoa.CodigoRede.Valor;
            txtValorHora.Text = objPessoa.ValorHora.Valor;
            ListItem objListVip = rblVip.Items.FindByValue(objPessoa.FlagVip.Valor);
            if (objListVip != null)
            {
                rblVip.SelectedValue = objPessoa.FlagVip.Valor;
            }
            objListVip = null;
            txtLocalizacaoFisica.Text = objPessoa.LocalizacaoFisica.Valor;
            ddlStatus.SelectedValue = objPessoa.Status.Valor;
            txtTipoSangue.Text = objPessoa.TipoSangue.Valor;
            txtNomeGuerra.Text = objPessoa.NomeGuerra.Valor;
            txtCnh.Text = objPessoa.Cnh.Valor;
            if (objPessoa.DataExpedicaoCnh.Valor != String.Empty)
            {
                cldDataExpedicao.SelectedDate = Convert.ToDateTime(objPessoa.DataExpedicaoCnh.Valor);
            }
            if (objPessoa.DataValidadeCnh.Valor != String.Empty)
            {
                cldDataValidade.SelectedDate = Convert.ToDateTime(objPessoa.DataValidadeCnh.Valor);
            }

            objPessoa = null;
        }
        catch
        {
        }
    }
    #endregion

    #region Evento pesquisar
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void pesquisar(object sender, EventArgs e)
    {

        int intEstrutura = 0;
        int intUsuarioTipo = 0;

        if (ddlEstruturaOrganizacionalPesquisa.SelectedIndex > 0)
        {
            intEstrutura = Convert.ToInt32(ddlEstruturaOrganizacionalPesquisa.SelectedValue);
        }
        if (ddpTipoUsuarioPesquisa.SelectedIndex > 0)
        {
            intUsuarioTipo = Convert.ToInt32(ddpTipoUsuarioPesquisa.SelectedValue);
        }

        SServiceDesk.Negocio.ClsPessoa.geraGridViewBusca(gvPessoa, ServiceDesk.Generica.ClsTexto.trocaAspaPorHtml(txtNomePesquisa.Text), txtMatriculaPesquisa.Text, intEstrutura, intUsuarioTipo);

    }
    #endregion

    #region Evento listaPessoa
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void listaPessoa(object sender, EventArgs e)
    {
        Response.Redirect(Page.Request.FilePath);
    }
    #endregion

}