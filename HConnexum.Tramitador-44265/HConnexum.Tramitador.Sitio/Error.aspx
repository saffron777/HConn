<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="HConnexum.Tramitador.Sitio.Error" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>Pagina Error</title>
</head>
<body>
	<form id="form1" runat="server">
	<div>
		Se ha producido un error inesperado en la aplicacion, por favor Comuniquese con el Administrador e intente mas tarde. Pagina que presento el Error:<br />
		<asp:Label runat="server" ID="lblPagina"></asp:Label>
	</div>
	</form>
</body>
</html>
