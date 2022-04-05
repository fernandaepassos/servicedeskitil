<%@ Page Language="C#" AutoEventWireup="true" MaintainScrollPositionOnPostback="true" CodeFile="ItemConfiguracao.aspx.cs" Inherits="ItemConfiguracao" %>
<%@ Register Src="WUCItemConfiguracao.ascx" TagName="WUCItemConfiguracao" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN" "http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<SCRIPT LANGUAGE="JavaScript" SRC="js/PopUps.js"></SCRIPT>
<head id="Head1" runat="server">
    <title>Help Desk  ITIL Compliance :: Item de Configuração</title>
    <link rel="stylesheet" href="css/estilo.css" type="text/css">
</head>
<body bottommargin="0" leftmargin="0" rightmargin="0" topmargin="0" class="body">
  <form id="form1" runat="server" style="margin:0">
    <uc1:WUCItemConfiguracao ID="WUCItemConfiguracao1" runat="server" />
  </form>
</body>
</html>