<%@ Application Language="C#" %>

<script runat="server">
    //teste
    void Application_Start(Object sender, EventArgs e) {
        // Code that runs on application startup

    }
    
    void Application_End(Object sender, EventArgs e) {
        //  Code that runs on application shutdown

    }
        
    void Application_Error(Object sender, EventArgs e) { 
        // Code that runs when an unhandled error occurs

    }

    void Session_Start(Object sender, EventArgs e) {
        // Code that runs when a new session is started
        Session.Timeout = 3600;
        Session.LCID = 1046;
        
        Session["strCodigoRede"] = "";
        Session["strTipUsu"] = "";
        Session["TipoUsuarioLogado"] = "";
        Session["CodigoUsuarioLogado"] = "";

        //Carrega os parametros
        string strCodigoRede = ServiceDesk.Negocio.ClsUsuario.getCodigoRede();
        string strTipoUsuarioLogado = ServiceDesk.Negocio.ClsUsuario.getTipoUsuarioCodigo(strCodigoRede).ToString();
        string strCodigoUsuarioLogado = ServiceDesk.Negocio.ClsUsuario.getCodigoUsuario().ToString();
        Session["TipoUsuarioLogado"] = strTipoUsuarioLogado;
        Session["CodigoUsuarioLogado"] = strCodigoUsuarioLogado;
        ClsParametro.CarregaParametros();    
    }

    void Session_End(Object sender, EventArgs e) {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.

    }
       
</script>
