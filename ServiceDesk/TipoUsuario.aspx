<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TipoUsuario.aspx.cs" Inherits="TipoUsuario" %>

<%@ Register Src="WUCTipoUsuario.ascx" TagName="WUCTipoUsuario" TagPrefix="uc1" %>
<%@ Register Assembly="eWorld.UI" Namespace="eWorld.UI" TagPrefix="ew" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN" "http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Help Desk  ITIL Compliance :: Tipos de Usuário</title>
    <link rel="stylesheet" href="css/estilo.css" type="text/css">
</head>
<body bottommargin="0" leftmargin="0" rightmargin="0" topmargin="0" class="body">
    <form id="form1" runat="server">
        <table width="100%" border="0" cellpadding="0" cellspacing="0">          
            <uc1:WUCTipoUsuario ID="WUCTipoUsuario1" runat="server" />
        </table>
    </form>
</body>