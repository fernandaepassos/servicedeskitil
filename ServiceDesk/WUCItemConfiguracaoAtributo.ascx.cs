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

public partial class WUCItemConfiguracaoAtributo : System.Web.UI.UserControl
{


    #region evento Page_Load
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// 
    private void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //Verificar acessibilidade
            /*int intCodigoFuncao = 8;
            if (!ServiceDesk.Negocio.ClsUsuario.verificaAcessoUsuarioFuncao(ClsUsuario.getCodigoUsuario(ClsUsuario.getCodigoRede()), intCodigoFuncao.ToString(), ClsUsuario.getTipoUsuarioCodigo(ClsUsuario.getCodigoRede()).ToString()))
            {
                Response.Redirect("AcessoNegado.aspx", false);
                return;

            }*/

            int intCodigoAtributo = 0;

            //Esconde a mensagem de erro
            lblMensagem.Visible = false;
            divMensagem.Visible = false;

            if (!Page.IsPostBack)
            {

                //Gerando o DropDownList com as Unidade de Medida
                SServiceDesk.Negocio.ClsUnidadeMedida.geraDropDownList(ddlUnidadeMedida);
                //Gerando o DropDownList com os Tipos do Atributo do Item de Configuração
                ServiceDesk.Negocio.ClsItemConfiguracaoAtributo.geraDropDownListTipo(ddlAtributoTipo);
                //Gerando o GridView com os Atributos do Item de Configuração cadastrados
                ServiceDesk.Negocio.ClsItemConfiguracaoAtributo.geraGridView(gvAtributo);

                if (Request.QueryString["codigo"] != null)
                {
                    intCodigoAtributo = Convert.ToInt32(Request.QueryString["codigo"].ToString().Trim());

                    //Monta os dados na tela
                    montaDadosAtributo(intCodigoAtributo);
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

    #region metodo montaDadosAtributo
    /// <summary>
    /// Método que popula os controles da tela com os dados do Atributo do Item de Configuração
    /// </summary>
    /// <param name="intCodigoItem">Código do Atributo do Item de Configuração</param>
    /// <param name="e">Evento que chamou o método</param>
    private void montaDadosAtributo(int intCodigoAtributo)
    {
        try
        {
            ServiceDesk.Negocio.ClsItemConfiguracaoAtributo objItemConfiguracaoAtributo = new ServiceDesk.Negocio.ClsItemConfiguracaoAtributo(intCodigoAtributo);

            if (objItemConfiguracaoAtributo.Tipo.Valor != String.Empty)
            {
                ddlAtributoTipo.Items.FindByValue(objItemConfiguracaoAtributo.Tipo.Valor).Selected = true;
            }
            if (objItemConfiguracaoAtributo.CodigoUnidadeMedida.Valor.Trim() != String.Empty)
            {
                ddlUnidadeMedida.Items.FindByValue(objItemConfiguracaoAtributo.CodigoUnidadeMedida.Valor.Trim()).Selected = true;
            }

            txtDescricao.Text = objItemConfiguracaoAtributo.Descricao.Valor;

            objItemConfiguracaoAtributo = null;

        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region Evento salvaItemConfiguracaoAtributo
    /// <summary>
    /// Método que salva o Tipo do Item de Configuração (inserir/alterar)
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void salvaItemConfiguracaoAtributo(object sender, EventArgs e)
    {
        try
        {
            if (Request.QueryString["codigo"] == null)
            {
                insereItemConfiguracaoAtributo();
            }
            else
            {
                if (Request.QueryString["codigo"] != null)
                {
                    alteraItemConfiguracaoAtributo(Convert.ToInt32(Request.QueryString["codigo"].ToString().Trim()));
                }
            }
        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
    #endregion

    #region metodo insereItemConfiguracaoAtributo
    /// <summary>
    /// Método que insere um novo Atributo do Item de Configuração
    /// </summary>
    private void insereItemConfiguracaoAtributo()
    {
        try
        {
            String strMensagem = String.Empty;

            ServiceDesk.Negocio.ClsItemConfiguracaoAtributo objItemConfiguracaoAtributo = new ServiceDesk.Negocio.ClsItemConfiguracaoAtributo();
            ServiceDesk.Negocio.ClsIdentificador objIdentificador = new ServiceDesk.Negocio.ClsIdentificador();

            objIdentificador.Tabela.Valor = objItemConfiguracaoAtributo.Atributos.NomeTabela;
            objItemConfiguracaoAtributo.Codigo.Valor = objIdentificador.getProximoValor().ToString();
            if ((ddlAtributoTipo.SelectedValue != "") && (ddlAtributoTipo.SelectedValue != null))
            {
                objItemConfiguracaoAtributo.Tipo.Valor = ddlAtributoTipo.SelectedValue;
            }
            if ((ddlUnidadeMedida.SelectedValue != "") && (ddlUnidadeMedida.SelectedValue != null))
            {
                objItemConfiguracaoAtributo.CodigoUnidadeMedida.Valor = ddlUnidadeMedida.SelectedValue;
            }
            objItemConfiguracaoAtributo.Descricao.Valor = ServiceDesk.Generica.ClsTexto.trocaAspaPorHtml(txtDescricao.Text);

            if (ServiceDesk.Negocio.ClsItemConfiguracaoAtributo.existeDescricao(objItemConfiguracaoAtributo.Descricao.Valor))
            {
                strMensagem = "Não foi possível alterar o Atributo. Já existe o atributo escolhido.";
            }
            else if (objItemConfiguracaoAtributo.insere(out strMensagem))
            {
                //Atualizando o valor na tabela identificador
                objIdentificador.atualizaValor();

                //Informando a mensagem do status da operação
                Session["strStatus"] = "Atributo do Item de Configuração inserido com sucesso.";

                Response.Redirect("itemconfiguracaoatributo.aspx");

            }

            lblMensagem.Text = strMensagem;
            imgIcone.ImageUrl = "images/icones/aviso.gif";
            lblMensagem.Visible = true;
            divMensagem.Visible = true;

            objItemConfiguracaoAtributo = null;
            objIdentificador = null;

        }
        catch (Exception ex)
        {

            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());

        }
    }
    #endregion

    #region metodo alteraItemConfiguracaoAtributo
    /// <summary>
    /// Método que altera o Atributo do Item de Configuração
    /// </summary>
    /// <param name="intCodigo"></param>
    private void alteraItemConfiguracaoAtributo(int intCodigoAtributo)
    {
        try
        {
            String strMensagem = String.Empty;

            ServiceDesk.Negocio.ClsItemConfiguracaoAtributo objItemConfiguracaoAtributo = new ServiceDesk.Negocio.ClsItemConfiguracaoAtributo(intCodigoAtributo);

            objItemConfiguracaoAtributo.Descricao.Valor = ServiceDesk.Generica.ClsTexto.trocaAspaPorHtml(txtDescricao.Text);
            objItemConfiguracaoAtributo.Tipo.Valor = ddlAtributoTipo.SelectedValue;
            objItemConfiguracaoAtributo.CodigoUnidadeMedida.Valor = ddlUnidadeMedida.SelectedValue;

            if (objItemConfiguracaoAtributo.existeDescricao())
            {
                strMensagem = "Não foi possível alterar o Atributo. Já existe o atributo escolhido.";
            }
            else if (objItemConfiguracaoAtributo.altera(out strMensagem))
            {
                strMensagem = "Atributo do Item de Configuração alterado com sucesso.";
            }

            lblMensagem.Text = strMensagem;
            imgIcone.ImageUrl = "images/icones/aviso.gif";
            lblMensagem.Visible = true;
            divMensagem.Visible = true;

            objItemConfiguracaoAtributo = null;

            //Remontando o GridView com os Atributos do Item de Configuração cadastrados
            ServiceDesk.Negocio.ClsItemConfiguracaoAtributo.geraGridView(gvAtributo);

        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());

        }
    }
    #endregion

    #region Evento novoItemConfiguracaoAtributo
    /// <summary>
    /// Método que redireciona a página para a inserção de um novo Atributo do Item de Configuração.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void novoItemConfiguracaoAtributo(object sender, EventArgs e)
    {
        Response.Redirect(Page.Request.FilePath);
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
            GridView objGridView = (GridView)sender;
            String strMensagem = String.Empty;

            switch (e.CommandName)
            {
                case "Editar":
                    {
                        GridViewRow objRow = objGridView.Rows[Convert.ToInt32(e.CommandArgument)];
                        if (objRow != null)
                        {
                            Label lblCodigo = (Label)objRow.FindControl("lblCodigo");
                            Response.Redirect("itemconfiguracaoatributo.aspx?codigo=" + lblCodigo.Text);
                        }
                        objRow = null;
                        ServiceDesk.Negocio.ClsItemConfiguracaoAtributo.geraGridView(objGridView);
                        break;
                    }
                case "Excluir":
                    {
                        GridViewRow objRow = objGridView.Rows[Convert.ToInt32(e.CommandArgument)];
                        if (objRow != null)
                        {
                            Label lblCodigo = (Label)objRow.FindControl("lblCodigo");
                            ServiceDesk.Negocio.ClsItemConfiguracaoAtributo objAtributo = new ServiceDesk.Negocio.ClsItemConfiguracaoAtributo(Convert.ToInt32(lblCodigo.Text));
                            try
                            {
                                objAtributo.exclui();
                                strMensagem = "Atributo do Item de Configuração excluído com sucesso";
                            }
                            catch
                            {
                                strMensagem = "Não foi possível excluir o atributo do Item de Configuração";
                            }
                            objAtributo = null;
                        }
                        objRow = null;
                        lblMensagem.Text = strMensagem;
                        imgIcone.ImageUrl = "images/icones/aviso.gif";
                        lblMensagem.Visible = true;
                        divMensagem.Visible = true;
                        ServiceDesk.Negocio.ClsItemConfiguracaoAtributo.geraGridView(objGridView);
                        break;
                    }
            }

            objGridView = null;

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

    #region Evento gvAtributo_RowDataBound
    /// <summary>
    /// Evento que ocorre na montagem da GridView
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvAtributo_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            int intCodigoAtributo = 0;

            if (Request.QueryString["codigo"] != null)
            {
                intCodigoAtributo = Convert.ToInt32(Request.QueryString["codigo"].ToString().Trim());
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                try
                {
                    Label lblCodigo = (Label)e.Row.FindControl("lblCodigo");
                    Label lblTipo = (Label)e.Row.FindControl("lblTipo");
                    Label lblCodigoMedida = (Label)e.Row.FindControl("lblCodigoMedida");

                    lblTipo.Text = ServiceDesk.Negocio.ClsItemConfiguracaoAtributo.retornaDescricaoTipo(Convert.ToChar(lblTipo.Text));

                    SServiceDesk.Negocio.ClsUnidadeMedida objUnidadeMedida = new SServiceDesk.Negocio.ClsUnidadeMedida(Convert.ToInt32(lblCodigoMedida.Text));
                    lblCodigoMedida.Text = objUnidadeMedida.Descricao.Valor;
                    objUnidadeMedida = null;

                    //adiciona um evento javascript no botão Excluir
                    ImageButton btnExcluir = (ImageButton)e.Row.Cells[5].Controls[0];
                    btnExcluir.Attributes.Add("onclick", "return verifica();");

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

}