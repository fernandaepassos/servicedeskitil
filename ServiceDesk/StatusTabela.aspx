<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StatusTabela.aspx.cs" Inherits="StatusTabela" %>

<%@ Register Src="WUCStatusTabela.ascx" TagName="WUCStatusTabela" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN" "http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Help Desk  ITIL Compliance :: Status por Tabela</title>
    <link rel="stylesheet" href="css/estilo.css" type="text/css">
</head>
<body bottommargin="0" leftmargin="0" rightmargin="0" topmargin="0" class="body">
    <form id="form1" runat="server">
        <table width="100%" border="0" cellspacing="0" cellpadding="0">           
            <tr>
                <td valign="top">
                    <uc1:WUCStatusTabela ID="WUCStatusTabela1" runat="server" />
                </td>
            </tr>    
          </table>
    </form>
</body>
</html>
