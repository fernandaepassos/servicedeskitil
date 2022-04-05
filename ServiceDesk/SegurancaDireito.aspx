<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SegurancaDireito.aspx.cs" Inherits="SegurancaDireito" %>

<%@ Register Src="WUCSegurancaDireito.ascx" TagName="WUCSegurancaDireito" TagPrefix="uc1" %>
<%@ Register Assembly="eWorld.UI" Namespace="eWorld.UI" TagPrefix="ew" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN" "http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Help Desk  ITIL Compliance :: Direitos da Segurança</title>
    <link href="css/estilo.css" rel="stylesheet" type="text/css">
</head>
<body bottommargin="0" class="body" leftmargin="0" rightmargin="0" topmargin="0">
    <form id="form1" runat="server">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <uc1:WUCSegurancaDireito ID="WUCSegurancaDireito1" runat="server" />
        </table>
    </form>
</body>
</html>
