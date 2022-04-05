using System;
using System.Web.UI.WebControls;
using ServiceDesk.Negocio;
using System.DirectoryServices;
using SServiceDesk.Negocio;

public partial class _Default : System.Web.UI.Page
{
    public enum ValidatorRet
    {
        UserInvalido,
        UserNegado,
        UserBloqueado,
        UserLiberado,
        erroLogin,
        SenhaInvalida
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            LoginUser.Visible = false;
            UsuarioLogado usuarioLogado;

            //Tentar logar direto pelo AD, Se existe, verifica se o user existe e está ativo no banco de dados
            string user = Environment.UserName;
            if (Request["logout"] != "1" && !string.IsNullOrEmpty(user) && AD.UserExists(user) && ClsUsuario.GetLogin(user, out usuarioLogado))
            {
                Session["USUARIOLOGADO"] = usuarioLogado;
                Response.Redirect("~/Default2.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            else
            {
                txtMessage.Visible = false;
                LoginUser.Visible = true;
            }
        }
        catch (DirectoryServicesCOMException ex)
        {
            //Erro no AD
            txtMessage.Text = "Não foi possível conectar ao Active Directory: " + ex.Message;
        }
        catch (Exception ex)
        {
            //Erro genérico
            txtMessage.Text = "Ocorreu um Erro: " + ex.Message;
        }
    }

    #region METODOS PRIVADOS
    private ValidatorRet ValidaUser(string UserName, string Password, out string message)
    {
        try
        {
            message = string.Empty;

            //Encripta a password e valida o user, se existe, se esta bloqueado
            //Password = Util.EncryptPass(Password);
            UsuarioLogado usuarioLogado;

            string result = ClsUsuario.GetLogin(UserName, Password, out usuarioLogado);

            if (string.IsNullOrEmpty(result))
                return ValidatorRet.erroLogin;
            else if (result == "INVALIDO")
                return ValidatorRet.UserInvalido;
            else if (result == "SENHA")
                return ValidatorRet.SenhaInvalida;
            else if (result == "DESATIVADO")
                return ValidatorRet.UserBloqueado;

            Session["USUARIOLOGADO"] = usuarioLogado;
            return ValidatorRet.UserLiberado;
        }
        catch (Exception ex)
        {
            message = ex.Message;
            return ValidatorRet.erroLogin;
        }
    }
    #endregion

    #region EVENTOS
    //Chamada quando aciona o botão de logar
    protected void LoginUser_Authenticate(object sender, AuthenticateEventArgs e)
    {
        string error = string.Empty;

        switch (ValidaUser(LoginUser.UserName, LoginUser.Password, out error))
        {
            case ValidatorRet.UserInvalido:
                LoginUser.FailureText = "Usuário inválido.";
                e.Authenticated = false;
                break;

            case ValidatorRet.SenhaInvalida:
                LoginUser.FailureText = "Senha inválida.";
                e.Authenticated = false;
                break;

            case ValidatorRet.UserNegado:
                LoginUser.FailureText = "Você não tem permissão para utilizar este sistema.";
                e.Authenticated = false;
                break;

            case ValidatorRet.UserBloqueado:
                LoginUser.FailureText = "Usuário inativo. Acesso negado.";
                e.Authenticated = false;
                break;

            case ValidatorRet.erroLogin:
                LoginUser.FailureText = "Erro no sistema: " + error;
                e.Authenticated = false;
                break;

            case ValidatorRet.UserLiberado:
                e.Authenticated = true;
                break;

            default:
                LoginUser.FailureText = "Erro ao autenticar. Contate o administrador do sistema.";
                e.Authenticated = false;
                break;
        }
    }

    protected void Login1_LoggedIn(object sender, EventArgs e)
    {
        Response.Redirect("~/Default2.aspx");
    }
    #endregion
}