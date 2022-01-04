<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ContingenciaLogin.aspx.cs" Inherits="HConnexum.Tramitador.Sitio.ContingenciaLogin" %>
<%@ Register Src="~/ControlesComunes/LoginControl.ascx" TagName="LoginControl" TagPrefix="hcc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>HC-Tramitador</title>
</head>
<body>
	<form id="formContingenciaLogin" runat="server">
		<telerik:RadScriptManager ID="RadScriptManager1" runat="server"/>
		<telerik:RadWindowManager ID="singleton" runat="server"  EnableShadow="True"/>
	</form>
</body>
</html>
