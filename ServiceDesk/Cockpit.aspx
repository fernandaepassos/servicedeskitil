<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Cockpit.aspx.cs" Inherits="Cockpit" %>

<%@ Register Src="AtendimentoProcessos.ascx" TagName="AtendimentoProcessos" TagPrefix="uc3" %>

<%@ Register Assembly="eWorld.UI" Namespace="eWorld.UI" TagPrefix="ew" %>



<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN" "http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >

<SCRIPT LANGUAGE="JavaScript" SRC="js/PopUps.js"></SCRIPT>

<script language=javascript>
<!--
self.resizeTo(screen.availWidth,screen.availHeight);
self.focus();
//-->
</script> 

<head id="Head1" runat="server">
    <title>Help Desk  ITIL Compliance :: Cockpit</title>
    <link rel="stylesheet" href="css/estilo.css" type="text/css">    
</head>



<body bottommargin="0" leftmargin="0" rightmargin="0" topmargin="0" class="body">
    <form id="form1" runat="server" style="margin:0px">
        <div id="divMensagem" runat="server" class="Mensagem" style="width: 100%" visible="false">
            <table border="0" cellpadding="0" cellspacing="5" width="776">
            <tr>
                <td align="center" valign="bottom" width="60">
                <asp:Image ID="imgIcone" runat="server" /></td>
                <td align="center" valign="bottom">
                <asp:Label ID="lblMensagem" runat="server" Font-Names="Trebuchet MS" ForeColor="#1D6E6F"></asp:Label></td>
            </tr>
            </table>
        </div>        
        <uc3:AtendimentoProcessos ID="AtendimentoProcessos1" runat="server" />
    </form>
</body>    
</html>
