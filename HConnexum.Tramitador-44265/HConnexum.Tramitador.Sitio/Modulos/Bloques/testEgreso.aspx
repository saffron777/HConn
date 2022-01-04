<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="testEgreso.aspx.cs" Inherits="HConnexum.Tramitador.Sitio.Modulos.Bloques.testEgreso" %>
<%@ Register src="Egreso.ascx" tagname="Egreso" tagprefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body class="master-body">
    <form id="form1" runat="server">
<telerik:RadScriptManager ID="RadScriptManager1" runat="server" />
              <telerik:RadWindowManager ID="RadWindowManager1" runat="server" EnableShadow="true">
        </telerik:RadWindowManager>
        <div><uc1:Egreso ID="Egreso1" runat="server" /></div>

    </form>
</body>
</html>
