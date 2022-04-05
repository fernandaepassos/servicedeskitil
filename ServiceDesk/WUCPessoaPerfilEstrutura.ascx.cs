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

public partial class WUCPessoaPerfilEstrutura : System.Web.UI.UserControl
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
        /*int intCodigoFuncao = 40;
        if (!ServiceDesk.Negocio.ClsUsuario.verificaAcessoUsuarioFuncao(ClsUsuario.getCodigoUsuario(ClsUsuario.getCodigoRede()), intCodigoFuncao.ToString(), ClsUsuario.getTipoUsuarioCodigo(ClsUsuario.getCodigoRede()).ToString()))
        {
            Response.Redirect("AcessoNegado.aspx", false);
            return;
        }*/

        if (!Page.IsPostBack)
        {
            SServiceDesk.Negocio.ClsEstruturaOrganizacional.geraDropDownListPorEmpresa(ddlEstruturaCodigo, Convert.ToInt32(ClsParametro.CodigoTipoEmpresa));
            this.ddlEstruturaCodigo.Items.Insert(0, "Selecione a Empresa");
            this.ddlEstruturaCodigo.Items[0].Value = "";

            carregaDropDownlistNovo();
        }
    }
    #endregion

    #region Metodo carregaDropDownlistNovo
    /// <summary>
    /// 
    /// </summary>
    private void carregaDropDownlistNovo()
    {
        SServiceDesk.Negocio.ClsEstruturaOrganizacional.geraDropDownListPorEmpresa(ddlEstruturaNova, Convert.ToInt32(ClsParametro.CodigoTipoEmpresa));
        this.ddlEstruturaNova.Items.Insert(0, "Selecione a Empresa");
        this.ddlEstruturaNova.Items[0].Value = "";
    }
    #endregion

    #region Evento ddlEstruturaCodigo_SelectedIndexChanged
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlEstruturaCodigo_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.ddlEstruturaCodigo.Text != String.Empty)
        {
            try
            {
                SServiceDesk.Negocio.ClsPessoaPerfilEstrutura.geraGridView(gvPessoaPerfilEstrutura, ddlEstruturaCodigo.SelectedValue);
                ddlEstruturaNova.SelectedIndex = -1;
                ddlEstruturaNova.Items.FindByValue(ddlEstruturaCodigo.SelectedValue).Selected = true;
                ddlEstruturaNova_SelectedIndexChanged(sender, e);
            }
            catch
            {
            }
        }
    }
    #endregion

    #region Evento gvPessoaPerfilEstrutura_RowDataBound
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvPessoaPerfilEstrutura_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //condicao IF que exibe os dados no GridView (estado: não-editável)
            if ((e.Row.RowState == DataControlRowState.Normal) || (e.Row.RowState == DataControlRowState.Alternate))
            {
                Label lblPessoa = (Label)e.Row.FindControl("lblPessoa");
                if (lblPessoa.Text != String.Empty)
                {
                    SServiceDesk.Negocio.ClsPessoa objPessoa = new SServiceDesk.Negocio.ClsPessoa();
                    objPessoa.alimentaPessoa(Convert.ToInt32(lblPessoa.Text));
                    lblPessoa.Text = objPessoa.Nome.Valor;
                }

                Label lblAplicacao = (Label)e.Row.FindControl("lblAplicacao");
                if (lblAplicacao.Text != String.Empty)
                {
                    SServiceDesk.Negocio.ClsAplicacao objAplicacao = new SServiceDesk.Negocio.ClsAplicacao();
                    objAplicacao.alimentaAplicacao(Convert.ToInt32(lblAplicacao.Text));
                    lblAplicacao.Text = objAplicacao.Descricao.Valor;
                }

                Label lblTipoUsuario = (Label)e.Row.FindControl("lblTipoUsuario");
                if (lblTipoUsuario.Text != String.Empty)
                {
                    SServiceDesk.Negocio.ClsTipoUsuario objTipoUsuario = new SServiceDesk.Negocio.ClsTipoUsuario();
                    objTipoUsuario.alimentaTipoUsuario(Convert.ToInt32(lblTipoUsuario.Text));
                    lblTipoUsuario.Text = objTipoUsuario.Descricao.Valor;
                }

                //adiciona um evento javascript no botão Excluir
                // ImageButton btnExcluir = (ImageButton)e.Row.Cells[5].Controls[0];
                // btnExcluir.Attributes.Add("onclick", "return verifica();");
            }

            // condicao IF que exibe os controles e dados destes durante edição de uma linha (estado: editável)
            if ((e.Row.RowState == DataControlRowState.Edit) || (e.Row.RowState.ToString() == "Alternate, Edit"))
            {
                Label lblTipoUsuario = (Label)e.Row.FindControl("lblTipoUsuario");
                DropDownList ddlTipoUsuario = (DropDownList)e.Row.FindControl("ddlTipoUsuario");
                SServiceDesk.Negocio.ClsTipoUsuario.geraDropDownList(ddlTipoUsuario);
                ddlTipoUsuario.Items.Insert(0, "");
                ddlTipoUsuario.Items[0].Value = "";
                ddlTipoUsuario.SelectedValue = lblTipoUsuario.Text;
            }
        }
    }
    #endregion

    #region Evento gvPessoaPerfilEstrutura_OnPageIndexChanging
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvPessoaPerfilEstrutura_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView objGridView = (GridView)sender;
        objGridView.PageIndex = e.NewPageIndex;
        ddlEstruturaCodigo_SelectedIndexChanged(sender, e);
        objGridView = null;
    }
    #endregion

    #region Evento ddlEstruturaNova_SelectedIndexChanged
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlEstruturaNova_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.ddlEstruturaNova.Text != String.Empty)
        {
            //Busca os funcionários para a empresa selecionada.
            SServiceDesk.Negocio.ClsPessoa.geraDropDownList(ddlPessoaNova, ddlEstruturaNova.SelectedValue);
            this.ddlPessoaNova.Items.Insert(0, "Selecione a Pessoa");
            this.ddlPessoaNova.Items[0].Value = "";
        }

    }
    #endregion

    #region Evento ddlPessoaNova_SelectedIndexChanged
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlPessoaNova_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            SServiceDesk.Negocio.ClsPerfil.geraGridView(gvNovoPerfil, ddlEstruturaNova.SelectedValue);
        }
        catch (Exception ex)
        {
            Response.Write(ex.ToString());
        }
    }
    #endregion

    #region Evento gvNovoPerfil_RowDataBound
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvNovoPerfil_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //condicao IF que exibe os dados no GridView (estado: não-editável)
            if ((e.Row.RowState == DataControlRowState.Normal) || (e.Row.RowState == DataControlRowState.Alternate))
            {

                Label lblAplicacao = (Label)e.Row.FindControl("lblAplicacao");
                Label lblTipoUsuario = (Label)e.Row.FindControl("lblTipoUsuario");

                //verifica de o item esta atribuido e marca ou nao a checkbox.
                bool retorno = SServiceDesk.Negocio.ClsPessoaPerfilEstrutura.verificaAtribuicaoPerfil(this.ddlPessoaNova.SelectedValue.ToString(), this.ddlEstruturaNova.SelectedValue.ToString(), lblAplicacao.Text, lblTipoUsuario.Text);

                if (retorno == true) //está atribuido ao usuário o perfil consultado
                {
                    CheckBox ckPerfil = (CheckBox)e.Row.FindControl("cbCodigo");
                    if (!ckPerfil.Checked)
                    {
                        ckPerfil.Checked = true;
                    }
                }

                if (lblAplicacao.Text != String.Empty)
                {
                    SServiceDesk.Negocio.ClsAplicacao objAplicacao = new SServiceDesk.Negocio.ClsAplicacao();
                    objAplicacao.alimentaAplicacao(Convert.ToInt32(lblAplicacao.Text));
                    lblAplicacao.Text = objAplicacao.Descricao.Valor;
                }


                if (lblTipoUsuario.Text != String.Empty)
                {
                    SServiceDesk.Negocio.ClsTipoUsuario objTipoUsuario = new SServiceDesk.Negocio.ClsTipoUsuario();
                    objTipoUsuario.alimentaTipoUsuario(Convert.ToInt32(lblTipoUsuario.Text));
                    lblTipoUsuario.Text = objTipoUsuario.Descricao.Valor;
                }


            }
        }
    }
    #endregion

    #region Evento salvaPerfil
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void salvaPerfil(object sender, EventArgs e)
    {
        SServiceDesk.Negocio.ClsPessoaPerfilEstrutura objPessoaPerfilEstrutura = new SServiceDesk.Negocio.ClsPessoaPerfilEstrutura();
        String strMensagem = String.Empty;

        try
        {

            if ((ddlPessoaNova.SelectedIndex > 0) && (ddlEstruturaNova.SelectedIndex > 0))
            {
                //Apaga os perfis relacionados ao usuário/empresa selecionado antes de gravar as opcoes.
                SServiceDesk.Negocio.ClsPessoaPerfilEstrutura.apagaPerfis(ddlPessoaNova.SelectedValue, ddlEstruturaNova.SelectedValue);

                for (int intI = 0; intI < gvNovoPerfil.Rows.Count; intI++)
                {
                    ServiceDesk.Negocio.ClsIdentificador objIdentificador = new ServiceDesk.Negocio.ClsIdentificador();
                    objIdentificador.Tabela.Valor = objPessoaPerfilEstrutura.Atributos.NomeTabela;
                    objPessoaPerfilEstrutura.Codigo.Valor = objIdentificador.getProximoValor().ToString();

                    objPessoaPerfilEstrutura.PessoaCodigo.Valor = this.ddlPessoaNova.SelectedValue;

                    GridViewRow objRow = gvNovoPerfil.Rows[intI];
                    bool bolMarcado = ((CheckBox)objRow.FindControl("cbCodigo")).Checked;

                    if (bolMarcado)
                    {
                        Label lblValorMarcado = (Label)objRow.FindControl("lblCodigo");
                        objPessoaPerfilEstrutura.PerfilEmpresaCodigo.Valor = lblValorMarcado.Text;
                        String strEmpresa = this.ddlEstruturaNova.SelectedValue;

                        if (objPessoaPerfilEstrutura.insere(out strMensagem, strEmpresa))
                        {
                            objIdentificador.atualizaValor();
                            imgIcone.ImageUrl = "images/icones/info.gif";
                            //limpaCampos();
                            objRow = null;
                            strMensagem = "Operação executada com sucesso.<br>";
                        }
                        else
                        {
                            strMensagem = "Não foi possível realizar a operação.<br>" + strMensagem;
                            imgIcone.ImageUrl = "images/icones/aviso.gif";
                        }

                        //Mensagem da operação
                        lblMensagem.Text = strMensagem;
                        divMensagem.Visible = true;
                    }
                }

                //Atualiza a tela para exibir os novos dados.
                //grid de usuarios
                if (gvPessoaPerfilEstrutura.Visible == true)
                {
                    SServiceDesk.Negocio.ClsPessoaPerfilEstrutura.geraGridView(gvPessoaPerfilEstrutura, ddlEstruturaCodigo.SelectedValue);
                }
            }
            else
            {
                lblMensagem.Text = "Nenhuma empresa/pessoa selecionada.";
                imgIcone.ImageUrl = "images/icones/aviso.gif";
                divMensagem.Visible = true;
            }

        }
        catch
        {
            lblMensagem.Text = "Não foi possível realizar a operação.<br>";
            imgIcone.ImageUrl = "images/icones/aviso.gif";
            divMensagem.Visible = true;
        }

        objPessoaPerfilEstrutura = null;
    }
    #endregion

}