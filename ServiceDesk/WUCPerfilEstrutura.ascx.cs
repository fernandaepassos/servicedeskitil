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

public partial class WUCPerfilEstrutura : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //Verificar acessibilidade
        /*int intCodigoFuncao = 37;
        if (!ServiceDesk.Negocio.ClsUsuario.verificaAcessoUsuarioFuncao(ClsUsuario.getCodigoUsuario(ClsUsuario.getCodigoRede()), intCodigoFuncao.ToString(), ClsUsuario.getTipoUsuarioCodigo(ClsUsuario.getCodigoRede()).ToString()))
        {
            Response.Redirect("AcessoNegado.aspx", false);
            return;
        }*/

        if (!Page.IsPostBack)
        {

            SServiceDesk.Negocio.ClsEstruturaOrganizacional.geraDropDownListPorEmpresa(this.ddlEmpresa, Convert.ToInt32(ClsParametro.CodigoTipoEmpresa));
            this.ddlEmpresa.Items.Insert(0, "--");
            this.ddlEmpresa.Items[0].Value = "";

            carregaPerfil();
        }

        divMensagem.Visible = false;
    }

    protected void btnSalvar_Click(object sender, EventArgs e)
    {
        if (ddlEmpresa.SelectedValue != String.Empty)
        {
            TreeNodeCollection objTreeView = tvPerfil.CheckedNodes;
            int intI = 0;
            String strExiste = String.Empty;
            String strMensagem = String.Empty;
            String strMensagemExcluir = String.Empty;
            String strMensagemCatch = String.Empty;
            String strMensagemFinal = String.Empty;
            String strSelecionados = String.Empty;

            SServiceDesk.Negocio.ClsPerfilEstrutura objPerfilEstrutura = new SServiceDesk.Negocio.ClsPerfilEstrutura();

            //trecho que gravará os itens selecionados
            for (intI = 0; intI < objTreeView.Count; intI++)
            {
                //Response.Write("<span style='color:White'>Nó: " + objTreeView[intI].Text + " Valor: " + objTreeView[intI].Value + "</span><br>");

                strSelecionados = strSelecionados + objTreeView[intI].Value + ",";
                try
                {
                    ServiceDesk.Negocio.ClsIdentificador objIdentificador = new ServiceDesk.Negocio.ClsIdentificador();
                    objIdentificador.Tabela.Valor = objPerfilEstrutura.Atributos.NomeTabela;
                    objPerfilEstrutura.Codigo.Valor = objIdentificador.getProximoValor().ToString();

                    objPerfilEstrutura.EstruturaCodigo.Valor = this.ddlEmpresa.SelectedValue;
                    objPerfilEstrutura.PerfilCodigo.Valor = objTreeView[intI].Value;

                    //verifica se já existe este PerfilEstrutura, caso não exista, insere!
                    strExiste = objPerfilEstrutura.existeRegistro();
                    if (strExiste == String.Empty)
                    {
                        if (objPerfilEstrutura.gravaSelecionados(out strMensagem))
                        {
                            objIdentificador.atualizaValor();
                            strMensagem += strMensagem + "Perfil (" + objTreeView[intI].Value + ") gravado com sucesso.<br>";
                        }

                        objIdentificador = null;

                    }
                }
                catch //(Exception ex)
                {
                    strMensagemCatch = strMensagemCatch + "Perfil (" + objTreeView[intI].Value + ") não foi possível gravar.<br>";
                }
            }

            if (strSelecionados != String.Empty)
            {
                strSelecionados = strSelecionados.Substring(0, strSelecionados.Length - 1);
            }


            objPerfilEstrutura.EstruturaCodigo.Valor = this.ddlEmpresa.SelectedValue;
            String strNaoSelecionados = objPerfilEstrutura.getNaoSelecionados(strSelecionados);

            if (strNaoSelecionados != String.Empty)
            {
                String[] arrNaoSelecionado = strNaoSelecionados.Split(',');

                for (intI = 0; intI < arrNaoSelecionado.Length; intI++)
                {
                    try
                    {
                        objPerfilEstrutura.PerfilCodigo.Valor = arrNaoSelecionado[intI];
                        objPerfilEstrutura.PerfilCodigo.CampoIdentificador = true;
                        //objPerfilEstrutura.EstruturaCodigo.Valor = this.ddlEmpresa.SelectedValue;
                        objPerfilEstrutura.EstruturaCodigo.CampoIdentificador = true;

                        objPerfilEstrutura.Codigo.CampoIdentificador = false;

                        if (objPerfilEstrutura.excluiNaoSelecionados())
                        {
                            strMensagemExcluir = strMensagemExcluir + "Perfil (" + arrNaoSelecionado[intI] + ") excluído com sucesso.<br>";
                        }
                    }
                    catch //(Exception ex)
                    {
                        strMensagemCatch = strMensagemCatch + "Perfil (" + arrNaoSelecionado[intI] + ") não foi possível excluir.<br>";
                    }
                }

                objPerfilEstrutura = null;
            }

            lblMensagem.Text = strMensagem + strMensagemExcluir + strMensagemCatch; //+ex.Message;
            if (lblMensagem.Text == String.Empty)
            {
                lblMensagem.Text = "Selecione os itens desejados para concluir a operação.";
            }
            imgIcone.ImageUrl = "images/icones/aviso.gif";
            divMensagem.Visible = true;
            carregaPerfil();

            objPerfilEstrutura = null;
        }
        else
        {
            lblMensagem.Text = "Selecione uma empresa.";
            imgIcone.ImageUrl = "images/icones/aviso.gif";
            divMensagem.Visible = true;
        }
    }

    protected void ddlEmpresa_SelectedIndexChanged(object sender, EventArgs e)
    {
        carregaPerfil();
    }

    /// <summary>
    /// Procedimento que marca/desmarca os perfis de acordo com a empresa selecionada
    /// </summary>
    public void carregaPerfil()
    {
        SServiceDesk.Negocio.ClsPerfilEstrutura objPerfilEstrutura = new SServiceDesk.Negocio.ClsPerfilEstrutura();
        objPerfilEstrutura.EstruturaCodigo.Valor = this.ddlEmpresa.Text;
        SServiceDesk.Negocio.ClsPerfilEstrutura.geraArvore(tvPerfil, objPerfilEstrutura);
        objPerfilEstrutura = null;
    }


}