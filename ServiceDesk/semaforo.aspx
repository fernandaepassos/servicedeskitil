<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Semaforo.aspx.cs" Inherits="Semaforo" %>

<%@ Register Src="WUCSemaforo.ascx" TagName="WUCSemaforo" TagPrefix="uc3" %>

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
    <title>Help Desk  ITIL Compliance :: Semáforo</title>
    <link rel="stylesheet" href="css/estilo.css" type="text/css">
    
   
</head>
<body bottommargin="0" leftmargin="0" rightmargin="0" topmargin="0" class="body">
    <form id="form1" runat="server">
        <uc3:WUCSemaforo ID="WUCSemaforo1" runat="server" />
    </form>
</body>    
</html>
