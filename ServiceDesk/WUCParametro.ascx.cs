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

public partial class WUCParametro2 : System.Web.UI.UserControl
{


    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //Verificar acessibilidade
            /*int intCodigoFuncao = 30;
            if (!ServiceDesk.Negocio.ClsUsuario.verificaAcessoUsuarioFuncao(ClsUsuario.getCodigoUsuario(ClsUsuario.getCodigoRede()), intCodigoFuncao.ToString(), ClsUsuario.getTipoUsuarioCodigo(ClsUsuario.getCodigoRede()).ToString()))
            {
                Response.Redirect("AcessoNegado.aspx", false);
                return;
            }*/

            if (!Page.IsPostBack)
            {
                //lblAba.Text = ClsNomenclatura.CriaNomenclatura("01.01.01.07");
                //lblAplicacao.Text = ClsNomenclatura.CriaNomenclatura("01.01.01.01");
                //lblGrupo.Text = ClsNomenclatura.CriaNomenclatura("01.01.01.02");
                //lblIdentificador.Text = ClsNomenclatura.CriaNomenclatura("01.01.01.03");
                //lblTipo.Text = ClsNomenclatura.CriaNomenclatura("01.01.01.04");
                //lblValor.Text = ClsNomenclatura.CriaNomenclatura("01.01.01.05");
                //lblDescricao.Text = ClsNomenclatura.CriaNomenclatura("01.01.01.06");
                //btnSalva.Text = ClsNomenclatura.CriaNomenclatura("01.01.01.08");
                //btnNovo.Text = ClsNomenclatura.CriaNomenclatura("01.01.01.09");
                //btnProcurar.Text = ClsNomenclatura.CriaNomenclatura("01.01.01.10");
                //btnExcluir.Text = ClsNomenclatura.CriaNomenclatura("01.01.01.11");

                pnlTreeview.Visible = false;

                btnExcluir.Attributes.Add("onclick", "return verifica();");
                btnExcluir.Visible = false;

                SServiceDesk.Negocio.ClsAplicacao.geraDropDownList(ddlAplicacao);
                ddlAplicacao.Items.Insert(0, "--");
                ddlAplicacao.Items[0].Value = String.Empty;
            }

            divMensagem.Visible = false;

        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());

        }
    }

    protected void btnProcurar_Click(object sender, EventArgs e)
    {
        try
        {
            CriaArvore("consulta");
            LimpaCampos();

        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }

    protected void CriaArvore(String strParametro)
    {
        try
        {
            ClsParametro objParametro = new ClsParametro();
            SqlDataReader dr;
            if (ddlAplicacao.SelectedValue != String.Empty)
            {
                tvParametro.Nodes.Clear();
                dr = objParametro.PopulaNivelRaiz(ddlAplicacao.SelectedValue);
                if (dr.Read())
                {

                    PopulateNodes(objParametro.PopulaNivelRaiz(ddlAplicacao.SelectedValue), tvParametro.Nodes);
                    tvParametro.ExpandAll();
                    pnlTreeview.Visible = true;
                }
                else
                {
                    if (strParametro == "consulta")
                    {
                        lblMensagem.Text = "Sua consulta não retornou resultados";
                        imgIcone.ImageUrl = "images/icones/aviso.gif";
                        divMensagem.Visible = true;
                        tvParametro.Nodes.Clear();
                        pnlTreeview.Visible = false;
                    }
                }
                dr.Dispose();
            }
            else
            {
                lblMensagem.Text = "Selecione uma apliacação";
                imgIcone.ImageUrl = "images/icones/aviso.gif";
                divMensagem.Visible = true;
                tvParametro.Nodes.Clear();
            }
            objParametro = null;
        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }

    private void PopulateNodes(SqlDataReader dr, TreeNodeCollection nodes)
    {
        try
        {
            while (dr.Read())
            {
                TreeNode tn = new TreeNode();

                tn.Text = dr["parametro_codigo"].ToString() + " - " + dr["identificador"].ToString() + " - " + dr["valor"].ToString();
                tn.Value = dr["parametro_codigo"].ToString();
                nodes.Add(tn);
                tn.PopulateOnDemand = ((int)(dr["NumNosFilho"]) > 0);
            }
            dr.Dispose();

        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }

    private void PopulateSubLevel(int parentid, TreeNode parentNode)
    {
        try
        {
            ClsParametro objParametro = new ClsParametro();
            PopulateNodes(objParametro.PopulaSubNivel(parentid, ddlAplicacao.SelectedValue), parentNode.ChildNodes);
            objParametro = null;
        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }

    protected void tvParametro_TreeNodePopulate(object sender, TreeNodeEventArgs e)
    {
        try
        {
            PopulateSubLevel(Int32.Parse(e.Node.Value), e.Node);

        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }

    protected void tvParametro_SelectedNodeChanged(object sender, EventArgs e)
    {
        try
        {
            ClsParametro objParametro = new ClsParametro();
            SqlDataReader drItem, drPai;

            btnExcluir.Visible = true;

            ddlGrupo.Items.Clear();
            drItem = objParametro.RetornaParametro(Convert.ToInt32(tvParametro.SelectedValue));
            if (drItem.Read())
            {
                ddlAplicacao.SelectedValue = drItem["aplicacao"].ToString();
                if (drItem["parametro_codigo_superior"].ToString() != "NULL" && drItem["parametro_codigo_superior"].ToString() != String.Empty)
                {
                    drPai = objParametro.RetornaParametro(Convert.ToInt32(drItem["parametro_codigo_superior"]));
                    if (drPai.Read())
                    {
                        ddlGrupo.Items.Insert(0, drPai["identificador"].ToString());
                        ddlGrupo.Items[0].Value = drPai["parametro_codigo"].ToString();
                    }
                    drPai.Dispose();
                }


                txtIdentificador.Text = drItem["identificador"].ToString();
                ddlTipo.SelectedValue = drItem["tipo"].ToString();
                txtValor.Text = drItem["valor"].ToString();
                txtDescricao.Text = drItem["descricao"].ToString();
                ViewState["codigo"] = drItem["parametro_codigo"].ToString();
            }
            drItem.Dispose();
            objParametro = null;

        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }

    protected void btnNovo_Click(object sender, EventArgs e)
    {
        try
        {
            ClsParametro objParametro = new ClsParametro();
            SqlDataReader drItem;

            if (ViewState["codigo"] != null)
            {
                drItem = objParametro.RetornaParametro(Convert.ToInt32(ViewState["codigo"]));
                if (drItem.Read())
                {
                    LimpaCampos();

                    ddlGrupo.Items.Insert(0, drItem["identificador"].ToString());
                    ddlGrupo.Items[0].Value = drItem["parametro_codigo"].ToString();

                    ddlGrupo.Items.Insert(1, "--");
                    ddlGrupo.Items[1].Value = "";

                    txtIdentificador.Text = drItem["identificador"].ToString();
                    ViewState["codigo"] = null;
                    btnExcluir.Visible = false;
                }
                drItem.Dispose();
            }
            else
            {
                lblMensagem.Text = "Para incluir um novo sub-item é necessário selecionar um item da aplicação.<br />Selecione uma aplicação, clique em procurar e escolha o item desejado.";
                imgIcone.ImageUrl = "images/icones/aviso.gif";
                divMensagem.Visible = true;
            }

            objParametro = null;

        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());

        }
    }

    protected void LimpaCampos()
    {
        ddlGrupo.Items.Clear();
        ddlTipo.SelectedValue = String.Empty;
        txtIdentificador.Text = String.Empty;
        ddlTipo.SelectedValue = String.Empty;
        txtValor.Text = String.Empty;
        txtDescricao.Text = String.Empty;
    }

    protected void btnSalva_Click(object sender, EventArgs e)
    {
        try
        {
            ClsParametro objParametro = new ClsParametro();
            String strMensagem = String.Empty;

            ServiceDesk.Negocio.ClsIdentificador objIdentificador = new ServiceDesk.Negocio.ClsIdentificador();
            objIdentificador.Tabela.Valor = objParametro.Atributos.NomeTabela;

            objParametro.ParametroCodigoSuperior.Valor = ServiceDesk.Generica.ClsTexto.trocaAspaPorHtml(ddlGrupo.SelectedValue);
            objParametro.Aplicacao.Valor = ServiceDesk.Generica.ClsTexto.trocaAspaPorHtml(ddlAplicacao.SelectedValue);
            objParametro.Identificador.Valor = ServiceDesk.Generica.ClsTexto.trocaAspaPorHtml(txtIdentificador.Text.ToUpper());
            txtIdentificador.Text = objParametro.Identificador.Valor;
            objParametro.Tipo.Valor = ServiceDesk.Generica.ClsTexto.trocaAspaPorHtml(ddlTipo.SelectedValue);
            objParametro.Valor.Valor = ServiceDesk.Generica.ClsTexto.trocaAspaPorHtml(txtValor.Text);

            if (txtDescricao.Text.Length > 255)
                objParametro.Descricao.Valor = ServiceDesk.Generica.ClsTexto.trocaAspaPorHtml(txtDescricao.Text.Substring(0, 254));
            else
                objParametro.Descricao.Valor = ServiceDesk.Generica.ClsTexto.trocaAspaPorHtml(txtDescricao.Text);

            // Inserir...
            if (ViewState["codigo"] == null)
            {
                if (objParametro.ValidaInsercao(String.Empty, ddlAplicacao.SelectedValue.Trim(), txtIdentificador.Text.Trim()) == false)
                {
                    objParametro.ParametroCodigo.Valor = objIdentificador.getProximoValor().ToString();
                    if (objParametro.insere(out strMensagem))
                    {
                        ViewState["codigo"] = objIdentificador.getProximoValor().ToString();
                        objIdentificador.atualizaValor();

                        lblMensagem.Text = "Item inserido com sucesso.";
                        imgIcone.ImageUrl = "images/icones/info.gif";
                        divMensagem.Visible = true;
                        btnExcluir.Visible = true;
                    }
                    else
                    {
                        lblMensagem.Text = "Não foi possível realizar a operação.<br />" + strMensagem;
                        imgIcone.ImageUrl = "images/icones/erro.gif";
                        divMensagem.Visible = true;
                    }
                }
                else
                {
                    lblMensagem.Text = "Já existe um registro com esta configuração.";
                    imgIcone.ImageUrl = "images/icones/aviso.gif";
                    divMensagem.Visible = true;
                }
            }
            else //Alterar...
            {
                if (objParametro.ValidaInsercao(ViewState["codigo"].ToString().Trim(), ddlAplicacao.SelectedValue.Trim(), txtIdentificador.Text.Trim()) == false)
                {
                    objParametro.ParametroCodigo.Valor = ViewState["codigo"].ToString();
                    if (objParametro.altera(out strMensagem))
                    {
                        lblMensagem.Text = "Item alterado com sucesso.";
                        imgIcone.ImageUrl = "images/icones/info.gif";
                        divMensagem.Visible = true;
                        btnExcluir.Visible = true;
                    }
                    else
                    {
                        lblMensagem.Text = "Não foi possivel realizar a operação.<br />" + strMensagem;
                        imgIcone.ImageUrl = "images/icones/erro.gif";
                        divMensagem.Visible = true;
                    }
                }
                else
                {
                    lblMensagem.Text = "Já existe um registro cadastrado com esta configuração.";
                    imgIcone.ImageUrl = "images/icones/aviso.gif";
                    divMensagem.Visible = true;
                }
            }
            ClsParametro.CarregaParametros();
            CriaArvore("");
            objIdentificador = null;
            objParametro = null;

        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }

    protected void btnExcluir_Click(object sender, EventArgs e)
    {
        try
        {
            ClsParametro objParametro = new ClsParametro();
            SqlDataReader dr = objParametro.RetornaParametro(Convert.ToInt32(ViewState["codigo"]));
            if (dr.Read())
            {
                if (objParametro.exclui(dr["identificador"].ToString()))
                {
                    lblMensagem.Text = "Item excluido com sucesso.";
                    imgIcone.ImageUrl = "images/icones/info.gif";
                    divMensagem.Visible = true;
                    ViewState["codigo"] = null;
                }
                else
                {
                    lblMensagem.Text = "Não foi possível realizar a operação.";
                    imgIcone.ImageUrl = "images/icones/erro.gif";
                    divMensagem.Visible = true;
                }
            }
            btnExcluir.Visible = false;
            ClsParametro.CarregaParametros();
            CriaArvore("");
            LimpaCampos();
            dr.Dispose();
            objParametro = null;

        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
}