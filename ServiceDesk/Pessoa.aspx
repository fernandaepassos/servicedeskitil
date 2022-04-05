<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Pessoa.aspx.cs" Inherits="Pessoa" %>
<%@ Register Src="WUCPessoa.ascx" TagName="WUCPessoa" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN" "http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Help Desk  ITIL Compliance :: Item de Configuração</title>
    <link rel="stylesheet" href="css/estilo.css" type="text/css">
</head>
<body topmargin="0" leftmargin="0" rightmargin="0" bottommargin="0" class="body">
    <form id="form1" runat="server">
    <div>
      <uc1:WUCPessoa ID="WUCPessoa1" runat="server" />
    </div>
    </form>
</body>
</html>
