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

public partial class WUCPerfil : System.Web.UI.UserControl
{
    #region Page_Load
    /// <summary>
    /// Carregamento da Página
    /// </summary>
    protected void Page_Load(object sender, EventArgs e)
    {
      //Verificar acessibilidade
      /*
      int intCodigoFuncao = 36;
      if (!ServiceDesk.Negocio.ClsUsuario.verificaAcessoUsuarioFuncao(ClsUsuario.getCodigoUsuario(ClsUsuario.getCodigoRede()), intCodigoFuncao.ToString(), ClsUsuario.getTipoUsuarioCodigo(ClsUsuario.getCodigoRede()).ToString()))
      {
        Response.Redirect("AcessoNegado.aspx", false);
        return;
      }*/
      
      if (!Page.IsPostBack)
        {
            SServiceDesk.Negocio.ClsAplicacao.geraDropDownList(this.ddlAplicacao);
            this.ddlAplicacao.Items.Insert(0, "--");
            this.ddlAplicacao.Items[0].Value = "";

            SServiceDesk.Negocio.ClsTipoUsuario.geraGridView(this.gvTipoUsuario);
        }

        divMensagem.Visible = false;
    }
    #endregion

    #region btnSalvar_Click
    /// <summary>
    /// Atualiza um perfil, podendo inserir, alterar ou excluir.
    /// </summary>
    protected void btnSalvar_Click(object sender, EventArgs e)
    {
        if (ddlAplicacao.SelectedIndex > 0)
        {

            String strMensagem = String.Empty;
            String strMensagemFinal = String.Empty;
            String strExiste = String.Empty;
            String strTipoUsuario = String.Empty;

            //Percorrendo os itens da GridView
            for (int i = 0; i < gvTipoUsuario.Rows.Count; i++)
            {
                GridViewRow objRow = gvTipoUsuario.Rows[i];
                Label lblCodigo = (Label)objRow.FindControl("lblCodigo");
                CheckBox cbCodigo = (CheckBox)objRow.FindControl("cbCodigo");
                TextBox lblTipoUsuario = (TextBox)objRow.FindControl("lblTipoUsuario");
                MetaBuilders.WebControls.GlobalRadioButton rbTipoUsuario = (MetaBuilders.WebControls.GlobalRadioButton)objRow.FindControl("rbTipoUsuario");

                //Item selecionado
                if (cbCodigo.Checked)
                {
                    try
                    {
                        SServiceDesk.Negocio.ClsPerfil objPerfil = new SServiceDesk.Negocio.ClsPerfil();

                        ServiceDesk.Negocio.ClsIdentificador objIdentificador = new ServiceDesk.Negocio.ClsIdentificador();
                        objIdentificador.Tabela.Valor = objPerfil.Atributos.NomeTabela;
                        objPerfil.Codigo.Valor = objIdentificador.getProximoValor().ToString();

                        objPerfil.AplicacaoCodigo.Valor = ddlAplicacao.SelectedValue;
                        objPerfil.TipoUsuarioCodigo.Valor = lblCodigo.Text;
                        if (rbTipoUsuario.Checked)
                        {
                            objPerfil.FlagPadrao.Valor = "1";
                        }
                        else
                        {
                            objPerfil.FlagPadrao.Valor = "0";
                        }

                        //verifica se já existe este perfil, caso não exista, insere!
                        strExiste = objPerfil.existeRegistro();
                        if (strExiste == String.Empty)
                        {

                            if (objPerfil.gravaSelecionados(out strMensagem))
                            {
                                objIdentificador.atualizaValor();
                                strMensagemFinal = "Gravação efetuada com sucesso.";
                            }
                            else
                            {
                                strMensagem = "Não foi possivel gravar o tipo de usuário '" + lblTipoUsuario.Text + "'";
                                strMensagemFinal = strMensagemFinal + "<br>" + strMensagem;
                            }
                        }
                        else
                        {
                            SServiceDesk.Negocio.ClsPerfil objPerfilAtualiza = new SServiceDesk.Negocio.ClsPerfil(Convert.ToInt32(objPerfil.AplicacaoCodigo.Valor), Convert.ToInt32(objPerfil.TipoUsuarioCodigo.Valor));
                            objPerfilAtualiza.FlagPadrao.Valor = objPerfil.FlagPadrao.Valor;
                            objPerfilAtualiza.altera(out strMensagem);
                            objPerfilAtualiza = null;
                        }

                        objPerfil = null;
                        objIdentificador = null;

                    }
                    catch //(Exception ex)
                    {
                        strMensagem = "Não foi possivel gravar o tipo de usuário '" + lblTipoUsuario.Text + "'";
                        strMensagemFinal = strMensagemFinal + "<br>" + strMensagem;
                    }
                }
                else //item nao selecionado
                {
                    try
                    {
                        SServiceDesk.Negocio.ClsPerfil objPerfil = new SServiceDesk.Negocio.ClsPerfil();

                        objPerfil.AplicacaoCodigo.Valor = ddlAplicacao.SelectedValue;
                        objPerfil.TipoUsuarioCodigo.Valor = lblCodigo.Text;

                        //verifica se já existe este perfil, caso exista, delete!
                        strExiste = objPerfil.existeRegistro();
                        if (strExiste != String.Empty)
                        {
                            objPerfil.Codigo.Valor = strExiste;

                            if (objPerfil.apagaNaoSelecionados(out strMensagem))
                            {
                                strMensagemFinal = "Gravação efetuada com sucesso.";
                            }
                            else
                            {
                                strMensagem = "Não foi possivel apagar o tipo de usuário '" + lblTipoUsuario.Text + "'";
                                strMensagemFinal = strMensagemFinal + "<br>" + strMensagem;
                            }

                            objPerfil = null;
                        }
                    }
                    catch
                    {
                        strMensagem = "Não foi possivel apagar o(s) tipo de usuário:<br>";
                        strTipoUsuario = strTipoUsuario + lblTipoUsuario.Text + "<br>";
                        strMensagemFinal = strMensagem + strTipoUsuario;// +ex.Message;                   
                    }
                } // fim do if else

                rbTipoUsuario = null;
                lblTipoUsuario = null;
                cbCodigo = null;
                lblCodigo = null;
                objRow = null;
            }

            carregaTipoUsuario();
            if (strMensagemFinal != String.Empty)
            {
                lblMensagem.Text = strMensagemFinal;
                imgIcone.ImageUrl = "images/icones/aviso.gif";
                divMensagem.Visible = true;
            }

        }
        else
        {
            lblMensagem.Text = "Selecione uma aplicação.";
            imgIcone.ImageUrl = "images/icones/aviso.gif";
            divMensagem.Visible = true;
        }

    }
    #endregion

    #region ddlAplicacao_SelectedIndexChanged
    /// <summary>
    /// Evento que ocorre quando e mudado a seleçao de uma aplicação.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlAplicacao_SelectedIndexChanged(object sender, EventArgs e)
    {
        carregaTipoUsuario();
    }
    #endregion

    #region carregaTipoUsuario
    /// <summary>
    /// Procedimento que marca/desmarca os tipos de usuarios de acordo com a aplicação selecionada
    /// </summary>
    public void carregaTipoUsuario()
    {
        String strExiste = String.Empty;

        for (int i = 0; i < gvTipoUsuario.Rows.Count; i++)
        {
            GridViewRow objRow = gvTipoUsuario.Rows[i];
            CheckBox cbCodigo = (CheckBox)objRow.FindControl("cbCodigo");
            MetaBuilders.WebControls.GlobalRadioButton rbTipoUsuario = (MetaBuilders.WebControls.GlobalRadioButton)objRow.FindControl("rbTipoUsuario");

            if (ddlAplicacao.SelectedIndex > 0)
            {
                Label lblCodigo = (Label)objRow.FindControl("lblCodigo");

                SServiceDesk.Negocio.ClsPerfil objPerfil = new SServiceDesk.Negocio.ClsPerfil(Convert.ToInt32(ddlAplicacao.SelectedValue), Convert.ToInt32(lblCodigo.Text));

                if (objPerfil.Codigo.Valor != String.Empty)
                {
                    cbCodigo.Checked = true;
                    if (objPerfil.FlagPadrao.Valor != "0")
                    {
                        rbTipoUsuario.Checked = true;
                    }
                }
                else
                {
                    cbCodigo.Checked = false;
                    rbTipoUsuario.Checked = false;
                }

                objPerfil = null;
            }
            else
            {
                cbCodigo.Checked = false;
                rbTipoUsuario.Checked = false;
            }
        }

    }
    #endregion

}
