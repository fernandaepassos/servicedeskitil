/*
- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
  • Classe para manipulação dos registros da tabela de FunçãoAplicação.
  
  	Data: 26/01/2006
  	Autor: Fernanda Passos
  	Descrição: Esta classe apresenta vários recursos para manipulação dos registros da 
    tabela função aplicação e para atender as regras de negócia.
  
  • Alterações
  	Data:
  	Autor:
  	Descrição: 
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

public partial class FuncaoAplicacao : BasePage
{
    #region Métodos

    #region Método Gera Função Superior
    /// <summary>
    /// Gera Funcao Superior
    /// </summary>
    public void geraFuncaoSuperior()
    {
        SServiceDesk.Negocio.ClsFuncaoAplicacao.geraDropDownList(this.ddlFuncaoSuperior);
        this.ddlFuncaoSuperior.Items.Insert(0, "Selecione a Função Superior");
        this.ddlFuncaoSuperior.Items[0].Value = "";
    }
    #endregion

    #region Método Limpa Campos
    /// <summary>
    /// Limpa Campos
    /// </summary>
    private void limpaCampos()
    {
        txtDescricao.Text = String.Empty;
        ddlAplicacao.SelectedValue = "";
        ddlFuncaoSuperior.SelectedValue = "";
        txtUrl.Text = String.Empty;
    }
    #endregion

    #endregion

    #region Eventos

    #region Page Load
    /// <summary>
    /// Page Load
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        CheckAcesso(39);

        if (!Page.IsPostBack)
        {
            SServiceDesk.Negocio.ClsFuncaoAplicacao.geraGridView(gvFuncaoAplicacao);
            geraFuncaoSuperior();
        }

        divMensagem.Visible = false;
    }
    #endregion

    #region Evento gvFuncaoAplicacao_RowCommand
    /// <summary>
    /// Evento gvFuncaoAplicacao_RowCommand
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvFuncaoAplicacao_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Excluir")
        {
            GridViewRow objRow = gvFuncaoAplicacao.Rows[Convert.ToInt32(e.CommandArgument)];
            if (objRow != null)
            {
                Label lblCodigo = (Label)objRow.FindControl("lblCodigo");

                SServiceDesk.Negocio.ClsFuncaoAplicacao objFuncaoAplicacao = new SServiceDesk.Negocio.ClsFuncaoAplicacao();
                objFuncaoAplicacao.Codigo.Valor = lblCodigo.Text;
                try
                {
                    if (objFuncaoAplicacao.exclui())
                    {
                        lblMensagem.Text = "Função excluída com sucesso.";
                        imgIcone.ImageUrl = "images/icones/info.gif";
                        divMensagem.Visible = true;
                    }
                }
                catch //(Exception ex)
                {
                    lblMensagem.Text = "Não foi possível excluir a Função.<br> Verifique se não há algum relacionamento.";// +ex.Message;
                    imgIcone.ImageUrl = "images/icones/aviso.gif";
                    divMensagem.Visible = true;
                }

                objRow = null;
                objFuncaoAplicacao = null;
            }
            SServiceDesk.Negocio.ClsFuncaoAplicacao.geraGridView(gvFuncaoAplicacao);
        }
    }
    #endregion

    #region Evento gvFuncaoAplicacao_RowEditing
    /// <summary>
    /// Evento gvFuncaoAplicacao_RowEditing
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvFuncaoAplicacao_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gvFuncaoAplicacao.EditIndex = e.NewEditIndex;
        SServiceDesk.Negocio.ClsFuncaoAplicacao.geraGridView(gvFuncaoAplicacao);
    }
    #endregion

    #region Evento gvFuncaoAplicacao_RowDataBound
    /// <summary>
    /// Evento gvFuncaoAplicacao_RowDataBound
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvFuncaoAplicacao_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //condicao IF que exibe os dados no GridView (estado: não-editável)
            if ((e.Row.RowState == DataControlRowState.Normal) || (e.Row.RowState == DataControlRowState.Alternate))
            {
                Label lblAplicacao = (Label)e.Row.FindControl("lblAplicacaoCodigo");
                if (lblAplicacao.Text != String.Empty)
                {
                    SServiceDesk.Negocio.ClsAplicacao objAplicacao = new SServiceDesk.Negocio.ClsAplicacao();
                    objAplicacao.alimentaAplicacao(Convert.ToInt32(lblAplicacao.Text));
                    lblAplicacao.Text = objAplicacao.Descricao.Valor;
                }

                Label lblFuncaoSuperior = (Label)e.Row.FindControl("lblCodigoFuncaoSuperior");
                if (lblFuncaoSuperior.Text != String.Empty)
                {
                    SServiceDesk.Negocio.ClsFuncaoAplicacao objFuncaoAplicacao = new SServiceDesk.Negocio.ClsFuncaoAplicacao();
                    objFuncaoAplicacao.alimentaFuncaoAplicacao(Convert.ToInt32(lblFuncaoSuperior.Text));
                    lblFuncaoSuperior.Text = objFuncaoAplicacao.Descricao.Valor;
                }

                //adiciona um evento javascript no botão Excluir
                ImageButton btnExcluir = (ImageButton)e.Row.Cells[6].Controls[0];
                btnExcluir.Attributes.Add("onclick", "return verifica();");
            }

            // condicao IF que exibe os controles e dados destes durante edição de uma linha (estado: editável)
            if ((e.Row.RowState == DataControlRowState.Edit) || (e.Row.RowState.ToString() == "Alternate, Edit"))
            {

                Label lblCodigo = (Label)e.Row.FindControl("lblCodigo");

                TextBox txtFuncao = (TextBox)e.Row.FindControl("txtFuncao");
                txtFuncao.Text = ServiceDesk.Generica.ClsTexto.trocaHtmlPorAspa(txtFuncao.Text);

                Label lblAplicacaoCodigo = (Label)e.Row.FindControl("lblAplicacaoCodigo");
                DropDownList ddlAplicacaoCodigo = (DropDownList)e.Row.FindControl("ddlAplicacaoCodigo");
                SServiceDesk.Negocio.ClsAplicacao.geraDropDownList(ddlAplicacaoCodigo);
                ddlAplicacaoCodigo.Items.Insert(0, "");
                ddlAplicacaoCodigo.Items[0].Value = "";
                ddlAplicacaoCodigo.SelectedValue = lblAplicacaoCodigo.Text;

                Label lblCodigoFuncaoSuperior = (Label)e.Row.FindControl("lblCodigoFuncaoSuperior");
                DropDownList ddlFuncaoSuperior = (DropDownList)e.Row.FindControl("ddlFuncaoSuperior");
                SServiceDesk.Negocio.ClsFuncaoAplicacao.geraDropDownListExcecao(ddlFuncaoSuperior, Convert.ToInt32(lblCodigo.Text.Trim()));
                ddlFuncaoSuperior.Items.Insert(0, "");
                ddlFuncaoSuperior.Items[0].Value = "";
                ddlFuncaoSuperior.SelectedValue = lblCodigoFuncaoSuperior.Text;

                TextBox txtUrl = (TextBox)e.Row.FindControl("txtUrl");
                txtUrl.Text = ServiceDesk.Generica.ClsTexto.trocaHtmlPorAspa(txtUrl.Text);
            }
        }
    }
    #endregion

    #region Evento gvFuncaoAplicacao_RowCancelingEdit
    /// <summary>
    /// Evento gvFuncaoAplicacao_RowCancelingEdit
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvFuncaoAplicacao_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvFuncaoAplicacao.EditIndex = -1;
        SServiceDesk.Negocio.ClsFuncaoAplicacao.geraGridView(gvFuncaoAplicacao);
    }
    #endregion

    #region Evento gvFuncaoAplicacao_RowUpdating
    /// <summary>
    /// Evento gvFuncaoAplicacao_RowUpdating
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvFuncaoAplicacao_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        String strMensagem = String.Empty;

        GridViewRow objRow = gvFuncaoAplicacao.Rows[e.RowIndex];
        if (objRow != null)
        {
            Label lblCodigo = (Label)objRow.FindControl("lblCodigo");
            TextBox txtFuncao = (TextBox)objRow.FindControl("txtFuncao");

            Label lblAplicacaoCodigo = (Label)objRow.FindControl("lblAplicacaoCodigo");
            DropDownList ddlAplicacaoCodigo = (DropDownList)objRow.FindControl("ddlAplicacaoCodigo");
            lblAplicacaoCodigo.Text = ddlAplicacaoCodigo.SelectedValue; //atualiza a label de exibicao

            Label lblCodigoFuncaoSuperior = (Label)objRow.FindControl("lblCodigoFuncaoSuperior");
            DropDownList ddlFuncaoSuperior = (DropDownList)objRow.FindControl("ddlFuncaoSuperior");
            lblCodigoFuncaoSuperior.Text = ddlFuncaoSuperior.SelectedValue; //atualiza a label de exibicao

            TextBox txtUrl = (TextBox)objRow.FindControl("txtUrl");

            SServiceDesk.Negocio.ClsFuncaoAplicacao objFuncaoAplicacao = new SServiceDesk.Negocio.ClsFuncaoAplicacao();
            objFuncaoAplicacao.Codigo.Valor = lblCodigo.Text;
            objFuncaoAplicacao.Descricao.Valor = ServiceDesk.Generica.ClsTexto.trocaAspaPorHtml(txtFuncao.Text);
            objFuncaoAplicacao.AplicacaoCodigo.Valor = lblAplicacaoCodigo.Text;
            objFuncaoAplicacao.FuncaoSuperior.Valor = lblCodigoFuncaoSuperior.Text;
            objFuncaoAplicacao.Url.Valor = ServiceDesk.Generica.ClsTexto.trocaAspaPorHtml(txtUrl.Text);

            try
            {
                if (objFuncaoAplicacao.altera(out strMensagem))
                {
                    objFuncaoAplicacao.atualizaChaveFuncao();
                    imgIcone.ImageUrl = "images/icones/info.gif";
                    gvFuncaoAplicacao.EditIndex = -1;
                    SServiceDesk.Negocio.ClsFuncaoAplicacao.geraGridView(gvFuncaoAplicacao);
                }
                else
                {
                    strMensagem = "Não foi possível realizar a operação.<br>" + strMensagem;
                    imgIcone.ImageUrl = "images/icones/aviso.gif";
                }

                //Mensagem da operação          
                lblMensagem.Text = strMensagem;
                divMensagem.Visible = true;

                objRow = null;
                objFuncaoAplicacao = null;
            }
            catch //(Exception ex)
            {
                strMensagem = "Não foi possível realizar a operação.<br>";
                lblMensagem.Text = strMensagem + "Verifique se os campos estão todos corretos!!";// +ex.Message;
                imgIcone.ImageUrl = "images/icones/aviso.gif";
                divMensagem.Visible = true;
            }
        }
    }
    #endregion

    #region Evento gvFuncaoAplicacao_PageIndexChanging
    /// <summary>
    /// Evento gvFuncaoAplicacao_PageIndexChanging
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvFuncaoAplicacao_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvFuncaoAplicacao.PageIndex = e.NewPageIndex;
        SServiceDesk.Negocio.ClsFuncaoAplicacao.geraGridView(gvFuncaoAplicacao);
    }
    #endregion

    #region Evento imgNovaFuncaoAplicacao_Click
    /// <summary>
    /// Evento imgNovaFuncaoAplicacao_Click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void imgNovaFuncaoAplicacao_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            pnlNovaFuncao.Visible = true;
            plPesquisar.Visible = false;

            DropDownList ddlAplicacao = (DropDownList)FindControl("ddlAplicacao");
            SServiceDesk.Negocio.ClsAplicacao.geraDropDownList(ddlAplicacao);
            ddlAplicacao.Items.Insert(0, "Selecione a Aplicação");
            ddlAplicacao.Items[0].Value = "";

            SServiceDesk.Negocio.ClsFuncaoAplicacao.geraDropDownList(ddlFuncaoSuperior);

            txtDescricao.Text = String.Empty;
            ddlAplicacao.SelectedValue = "";
            ddlFuncaoSuperior.SelectedValue = "";
            txtUrl.Text = String.Empty;

            //geraFuncaoSuperior();
        }
        catch
        { }
    }
    #endregion

    #region Evento imgPesquisar_Click
    /// <summary>
    /// Evento imgPesquisar_Click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void imgPesquisar_Click(object sender, ImageClickEventArgs e)
    {
        pnlNovaFuncao.Visible = false;// divPesquisar.Visible = true;
        plPesquisar.Visible = true;// divNovaFuncaoAplicacao.Visible = false;

        DropDownList ddlAplicacaoCodigo = (DropDownList)FindControl("ddlAplicacaoCodigo");
        SServiceDesk.Negocio.ClsAplicacao.geraDropDownList(ddlAplicacaoCodigo);
        ddlAplicacaoCodigo.Items.Insert(0, "Selecione a Aplicação");
        ddlAplicacaoCodigo.Items[0].Value = "";

        DropDownList ddlFuncaoSuperiorCodigo = (DropDownList)FindControl("ddlFuncaoSuperiorCodigo");
        SServiceDesk.Negocio.ClsFuncaoAplicacao.geraDropDownList(ddlFuncaoSuperiorCodigo);
        ddlFuncaoSuperiorCodigo.Items.Insert(0, "Selecione a Função Superior");
        ddlFuncaoSuperiorCodigo.Items[0].Value = "";


        txtDescricaoFuncao.Text = String.Empty;
        ddlAplicacaoCodigo.SelectedValue = "";
        ddlFuncaoSuperiorCodigo.SelectedValue = "";

        SServiceDesk.Negocio.ClsFuncaoAplicacao.geraGridView(gvFuncaoAplicacao);
    }
    #endregion

    #region Evento imgSalvar_Click
    /// <summary>
    /// Evento imgSalvar_Click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void imgSalvar_Click(object sender, EventArgs e)
    {
        SServiceDesk.Negocio.ClsFuncaoAplicacao objFuncaoAplicacao = new SServiceDesk.Negocio.ClsFuncaoAplicacao();
        String strMensagem = String.Empty;

        ServiceDesk.Negocio.ClsIdentificador objIdentificador = new ServiceDesk.Negocio.ClsIdentificador();
        objIdentificador.Tabela.Valor = objFuncaoAplicacao.Atributos.NomeTabela;
        objFuncaoAplicacao.Codigo.Valor = objIdentificador.getProximoValor().ToString();

        objFuncaoAplicacao.Descricao.Valor = ServiceDesk.Generica.ClsTexto.trocaAspaPorHtml(this.txtDescricao.Text);
        objFuncaoAplicacao.AplicacaoCodigo.Valor = this.ddlAplicacao.SelectedValue;
        objFuncaoAplicacao.FuncaoSuperior.Valor = this.ddlFuncaoSuperior.SelectedValue;
        objFuncaoAplicacao.Url.Valor = ServiceDesk.Generica.ClsTexto.trocaAspaPorHtml(this.txtUrl.Text);

        try
        {
            if (objFuncaoAplicacao.insere(out strMensagem))
            {
                objIdentificador.atualizaValor();
                objFuncaoAplicacao.atualizaChaveFuncao();
                imgIcone.ImageUrl = "images/icones/info.gif";
                SServiceDesk.Negocio.ClsFuncaoAplicacao.geraGridView(gvFuncaoAplicacao);
                geraFuncaoSuperior();
                limpaCampos();
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
        catch //(Exception ex)
        {
            strMensagem = "Não foi possível realizar a operação.<br>";
            lblMensagem.Text = strMensagem + "Verifique se os campos estão corretos!!";// +ex.Message;
            imgIcone.ImageUrl = "images/icones/aviso.gif";
            divMensagem.Visible = true;
        }

        objFuncaoAplicacao = null;

    }
    #endregion

    #region Evento Filtrar
    /// <summary>
    /// Evento Filtrar
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnFiltrar_Click(object sender, EventArgs e)
    {
        TextBox txtDescricaoFuncao = (TextBox)FindControl("txtDescricaoFuncao");
        DropDownList ddlAplicacaoCodigo = (DropDownList)FindControl("ddlAplicacaoCodigo");
        DropDownList ddlFuncaoSuperiorCodigo = (DropDownList)FindControl("ddlFuncaoSuperiorCodigo");

        SServiceDesk.Negocio.ClsFuncaoAplicacao objFuncaoAplicacao = new SServiceDesk.Negocio.ClsFuncaoAplicacao();

        objFuncaoAplicacao.Descricao.Valor = ServiceDesk.Generica.ClsTexto.trocaAspaPorHtml(txtDescricaoFuncao.Text);
        objFuncaoAplicacao.Descricao.CampoIdentificador = true;
        objFuncaoAplicacao.AplicacaoCodigo.Valor = ddlAplicacaoCodigo.SelectedValue;
        objFuncaoAplicacao.AplicacaoCodigo.CampoIdentificador = true;
        objFuncaoAplicacao.FuncaoSuperior.Valor = ddlFuncaoSuperiorCodigo.SelectedValue;
        objFuncaoAplicacao.FuncaoSuperior.CampoIdentificador = true;

        try
        {
            if ((objFuncaoAplicacao.Descricao.Valor == String.Empty) && (objFuncaoAplicacao.AplicacaoCodigo.Valor == String.Empty) && (objFuncaoAplicacao.FuncaoSuperior.Valor == String.Empty))
            {
                SServiceDesk.Negocio.ClsFuncaoAplicacao.geraGridView(gvFuncaoAplicacao);
            }
            else
                SServiceDesk.Negocio.ClsFuncaoAplicacao.geraGridView(gvFuncaoAplicacao, objFuncaoAplicacao, true);
        }
        catch (Exception ex)
        {
            lblMensagem.Text = "Houve algum problema na geração da tabela.<br>" + ex.ToString();
            imgIcone.ImageUrl = "images/icones/aviso.gif";
            divMensagem.Visible = true;
        }

        objFuncaoAplicacao = null;
        divPesquisar.Visible = true;
    }
    #endregion

    #endregion

}