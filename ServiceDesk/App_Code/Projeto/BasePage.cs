using ServiceDesk.Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

/// <summary>
/// Summary description for BasePage
/// </summary>
public class BasePage : Page
{
    protected UsuarioLogado user;

    public BasePage()
    {
    }

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
    }

    /// <summary>
    /// Verifica se existe usuário logado e se este tem permissão para acessar a página.
    /// Caso negativo, redireciona para página de login ou mostra mensagem de acesso negado
    /// </summary>
    /// <param name="funcao">código de permissão de funcionalidade</param>
    protected void CheckAcesso(int funcao)
    {
        user = (UsuarioLogado)Session["USUARIOLOGADO"];

        if (user == null)
        {
            //Sem user logado, redireciona para página de logon
            Response.Redirect("default.aspx", false);
            return;
        }
        else if (!ClsUsuario.verificaAcessoUsuarioFuncao(user.IDusuario, funcao, user.TipoUsuario))
        {
            //User sem permissão
            Response.Redirect("AcessoNegado.aspx", false);
            return;
        }
        else
        {
            if (!Page.IsPostBack)
            {
                //faz log de acesso
                ClsLog.insereLog(ClsLog.enumTipoLog.ACESSO, user.IDusuario.ToString(), Request.Path, "0", string.Empty);
            }
        }
    }

    protected bool CheckPermissao(int funcao)
    {
        user = (UsuarioLogado)Session["USUARIOLOGADO"];
        return ClsUsuario.verificaAcessoUsuarioFuncao(user.IDusuario, funcao, user.TipoUsuario);
    }

    //Fazer log de excepções e mostrar página de erro
    protected void Page_Error(object sender, EventArgs e)
    {
        Exception ex = Server.GetLastError();

        string idUsuario = user == null ? string.Empty : user.IDusuario.ToString();
        //ClsLog.insereLog(ClsLog.enumTipoLog.ERRO, idUsuario, Request.Path, "0", ex.ToString());

        //Server.Transfer("~/erro.htm");
    }
}