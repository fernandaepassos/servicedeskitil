<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PessoaPerfilEstrutura.aspx.cs" Inherits="PessoaPerfilEstrutura" %>
<%@ Register Src="WUCPessoaPerfilEstrutura.ascx" TagName="WUCPessoaPerfilEstrutura"
  TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN" "http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Help Desk  ITIL Compliance :: Pessoa Perfil Estrutura</title>
    <link rel="stylesheet" href="css/estilo.css" type="text/css">
</head>
<body bottommargin="0" leftmargin="0" rightmargin="0" topmargin="0" class="body">
    <form id="form1" runat="server">
    <div>
      <uc1:WUCPessoaPerfilEstrutura ID="WUCPessoaPerfilEstrutura1" runat="server" />
    </div>
    </form>
</body>
</html>
