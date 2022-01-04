<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Inicio.aspx.cs" Inherits="HConnexum.Tramitador.Sitio.Inicio" %>
<%@ Register Src="~/ControlesComunes/LoginControl.ascx" TagName="LoginControl" TagPrefix="hcc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title></title>
</head>
<body>
	<form id="form1" runat="server">
	<div>
		<telerik:RadScriptManager ID="RadScriptManager1" runat="server"/>
		<hcc:LoginControl ID="ContingenciaLoginControl" runat="server" TecladoReal="true" TecladoVirtual="true" />
		<telerik:RadWindowManager ID="singleton" runat="server"  EnableShadow="True"/>
	</div>
	</form>
</body>
</html>
