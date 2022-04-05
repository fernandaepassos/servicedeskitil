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
public partial class WUCNomenclatura : System.Web.UI.UserControl
{


    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                pnlTreeview.Visible = false;

                btnExcluir.Attributes.Add("onclick", "return verifica();");
                btnExcluir.Visible = false;

                this.ddlIdioma.Items.Insert(0, "--");
                this.ddlIdioma.Items[0].Value = "";

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
            ClsNomenclatura objNomenclatura = new ClsNomenclatura();
            SqlDataReader dr;
            if (ddlAplicacao.SelectedValue != String.Empty && ddlIdioma.SelectedValue != String.Empty)
            {
                tvNomenclatura.Nodes.Clear();
                dr = objNomenclatura.PopulaNivelRaiz(ddlAplicacao.SelectedValue, ddlIdioma.SelectedValue);
                if (dr.Read())
                {
                    PopulateNodes(objNomenclatura.PopulaNivelRaiz(ddlAplicacao.SelectedValue, ddlIdioma.SelectedValue), tvNomenclatura.Nodes);
                    tvNomenclatura.ExpandAll();
                    pnlTreeview.Visible = true;
                }
                else
                {
                    if (strParametro == "consulta")
                    {
                        lblMensagem.Text = "Sua consulta não retornou resultados.";
                        imgIcone.ImageUrl = "images/icones/aviso.gif";
                        divMensagem.Visible = true;
                        tvNomenclatura.Nodes.Clear();
                        pnlTreeview.Visible = false;
                    }
                }
                dr.Dispose();
            }
            else
            {
                lblMensagem.Text = "Selecione o idioma e o nome da aplicação.";
                imgIcone.ImageUrl = "images/icones/aviso.gif";
                divMensagem.Visible = true;
                tvNomenclatura.Nodes.Clear();
            }

            objNomenclatura = null;
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

                tn.Text = dr["identificador"].ToString() + " - " + dr["texto"].ToString();
                tn.Value = dr["nomenclatura_codigo"].ToString();
                nodes.Add(tn);
                tn.PopulateOnDemand = ((int)(dr["NumNosFilho"]) > 0);
            }

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
            ClsNomenclatura objNomenclatura = new ClsNomenclatura();
            PopulateNodes(objNomenclatura.PopulaSubNivel(parentid, ddlAplicacao.SelectedValue, ddlIdioma.SelectedValue), parentNode.ChildNodes);
            objNomenclatura = null;
        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }

    protected void tvNomenclatura_TreeNodePopulate(object sender, TreeNodeEventArgs e)
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

    protected void tvNomenclatura_SelectedNodeChanged(object sender, EventArgs e)
    {
        try
        {
            ClsNomenclatura objNomenclatura = new ClsNomenclatura();
            SqlDataReader drItem, drPai;

            btnExcluir.Visible = true;

            ddlGrupo.Items.Clear();
            drItem = objNomenclatura.RetornaNomenclatura(Convert.ToInt32(tvNomenclatura.SelectedValue));
            if (drItem.Read())
            {
                ddlIdioma.SelectedValue = drItem["idioma"].ToString();
                ddlAplicacao.SelectedValue = drItem["aplicacao"].ToString();

                if (drItem["nomenclatura_codigo_superior"].ToString() != "null" && drItem["nomenclatura_codigo_superior"].ToString() != String.Empty)
                {
                    drPai = objNomenclatura.RetornaNomenclatura(Convert.ToInt32(drItem["nomenclatura_codigo_superior"]));
                    if (drPai.Read())
                    {
                        ddlGrupo.Items.Insert(0, drPai["texto"].ToString());
                        ddlGrupo.Items[0].Value = drPai["nomenclatura_codigo"].ToString();
                    }
                    drPai.Dispose();
                }

                txtIdentificador.Text = drItem["identificador"].ToString();
                txtTexto.Text = drItem["texto"].ToString();
                txtUrl.Text = drItem["url"].ToString();
                txtParametro.Text = drItem["parametro"].ToString();
                ViewState["codigo"] = drItem["nomenclatura_codigo"].ToString();
            }
            drItem.Dispose();
            objNomenclatura = null;
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
            ClsNomenclatura objNomenclatura = new ClsNomenclatura();
            SqlDataReader drItem;

            if (ViewState["codigo"] != null)
            {
                drItem = objNomenclatura.RetornaNomenclatura(Convert.ToInt32(ViewState["codigo"]));
                if (drItem.Read())
                {
                    LimpaCampos();

                    ddlGrupo.Items.Insert(0, drItem["texto"].ToString());
                    ddlGrupo.Items[0].Value = drItem["nomenclatura_codigo"].ToString();

                    ddlGrupo.Items.Insert(1, "--");
                    ddlGrupo.Items[1].Value = "";

                    txtIdentificador.Text = drItem["identificador"].ToString();
                    ViewState["codigo"] = null;
                    btnExcluir.Visible = false;
                }
                drItem.Dispose();
                objNomenclatura = null;
            }
            else
            {
                lblMensagem.Text = "Para incluir um novo sub-item é necessário selecionar um item da aplicação.<br />Selecione um Idioma e uma Aplicação, clique em procurar e clique no item desejado.";
                imgIcone.ImageUrl = "images/icones/aviso.gif";
                divMensagem.Visible = true;
            }
        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }

    protected void LimpaCampos()
    {
        ddlGrupo.Items.Clear();
        txtIdentificador.Text = String.Empty;
        txtTexto.Text = String.Empty;
        txtUrl.Text = String.Empty;
        txtParametro.Text = String.Empty;
    }

    protected void btnSalva_Click(object sender, EventArgs e)
    {
        try
        {
            ClsNomenclatura objNomenclatura = new ClsNomenclatura();
            String strMensagem = String.Empty;

            ServiceDesk.Negocio.ClsIdentificador objIdentificador = new ServiceDesk.Negocio.ClsIdentificador();
            objIdentificador.Tabela.Valor = objNomenclatura.Atributos.NomeTabela;

            // inserir ou alterar
            objNomenclatura.NomenclaturaCodigoSuperior.Valor = ServiceDesk.Generica.ClsTexto.trocaAspaPorHtml(ddlGrupo.SelectedValue);
            objNomenclatura.Idioma.Valor = ServiceDesk.Generica.ClsTexto.trocaAspaPorHtml(ddlIdioma.SelectedValue);
            objNomenclatura.Aplicacao.Valor = ServiceDesk.Generica.ClsTexto.trocaAspaPorHtml(ddlAplicacao.SelectedValue);
            objNomenclatura.Identificador.Valor = ServiceDesk.Generica.ClsTexto.trocaAspaPorHtml(txtIdentificador.Text);
            objNomenclatura.Texto.Valor = ServiceDesk.Generica.ClsTexto.trocaAspaPorHtml(txtTexto.Text);
            objNomenclatura.Url.Valor = ServiceDesk.Generica.ClsTexto.trocaAspaPorHtml(txtUrl.Text);
            objNomenclatura.Parametro.Valor = ServiceDesk.Generica.ClsTexto.trocaAspaPorHtml(txtParametro.Text);

            // Inserir...
            if (ViewState["codigo"] == null)
            {
                if (objNomenclatura.ValidaInsercao(String.Empty, ddlAplicacao.SelectedValue.Trim(), txtIdentificador.Text.Trim(), ddlIdioma.SelectedValue.Trim()) == false)
                {
                    objNomenclatura.NomenclaturaCodigo.Valor = objIdentificador.getProximoValor().ToString();
                    if (objNomenclatura.insere(out strMensagem))
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
                        lblMensagem.Text = "Não foi possivel realizar a operação.<br />" + strMensagem;
                        imgIcone.ImageUrl = "images/icones/erro.gif";
                        divMensagem.Visible = true;
                    }
                }
                else
                {
                    lblMensagem.Text = "Já existe um aplicação cadastrada com esse indentificador.<br />Informe outro indentificador.";
                    imgIcone.ImageUrl = "images/icones/aviso.gif";
                    divMensagem.Visible = true;
                }
            }
            else //Alterar...
            {
                if (objNomenclatura.ValidaInsercao(ViewState["codigo"].ToString().Trim(), ddlAplicacao.SelectedValue.Trim(), txtIdentificador.Text.Trim(), ddlIdioma.SelectedValue.Trim()) == false)
                {
                    objNomenclatura.NomenclaturaCodigo.Valor = ViewState["codigo"].ToString();
                    if (objNomenclatura.altera(out strMensagem))
                    {
                        lblMensagem.Text = "Item atualizado com sucesso.";
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
                    lblMensagem.Text = "Já existe um aplicação cadastrada com esse indentificador.<br />Informe outro indentificador.";
                    imgIcone.ImageUrl = "images/icones/aviso.gif";
                    divMensagem.Visible = true;
                }
            }
            CriaArvore("");
            objIdentificador = null;
            objNomenclatura = null;
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
            ClsNomenclatura objNomenclatura = new ClsNomenclatura();
            SqlDataReader dr = objNomenclatura.RetornaNomenclatura(Convert.ToInt32(ViewState["codigo"]));
            if (dr.Read())
            {
                if (objNomenclatura.exclui(dr["identificador"].ToString()))
                {
                    lblMensagem.Text = "Item excluido com sucesso.";
                    imgIcone.ImageUrl = "images/icones/info.gif";
                    divMensagem.Visible = true;
                    ViewState["codigo"] = null;
                }
                else
                {
                    lblMensagem.Text = "Não foi possivel realizar a operação.";
                    imgIcone.ImageUrl = "images/icones/erro.gif";
                    divMensagem.Visible = true;
                }
            }
            btnExcluir.Visible = false;
            CriaArvore("");
            LimpaCampos();
            dr.Dispose();
            objNomenclatura = null;

        }
        catch (Exception ex)
        {
            ClsLog.insereLog(ServiceDesk.Negocio.ClsLog.enumTipoLog.ERRO, ClsUsuario.getCodigoUsuario().ToString(), this.Request.Path, "0", ex.ToString());
        }
    }
}