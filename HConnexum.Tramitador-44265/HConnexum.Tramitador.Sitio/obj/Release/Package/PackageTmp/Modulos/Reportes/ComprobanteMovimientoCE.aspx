<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Site.Master" AutoEventWireup="true" CodeBehind="ComprobanteMovimientoCE.aspx.cs" Inherits="HConnexum.Tramitador.Sitio.Modulos.Reportes.ComprobanteMovimientoCE" %>

<%@ Register Assembly="Telerik.ReportViewer.WebForms, Version=7.2.13.1016, Culture=neutral, PublicKeyToken=a9d7983dfcc261be" Namespace="Telerik.ReportViewer.WebForms" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
<style type="text/css">
		.sheet { height: 100% !important; }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
	<telerik:ReportViewer ID="ComprobanteMovimiento" runat="server" Width="780px" 
		Height="1250px" ProgressText="Generando Reporte..." ShowPrintPreviewButton="False">
		<resources currentpagetooltip="Página actual" exportbuttontext="Exportar" 
			exportselectformattext="Exportar al formato seleccionado" 
			exporttooltip="Exportar" firstpagetooltip="Primera Página" 
			lastpagetooltip="Ultima Página" navigatebacktooltip="Regresar" 
			navigateforwardtooltip="Adelante" nextpagetooltip="Siguiente Página" 
			previouspagetooltip="Página Anterior" printtooltip="Imprimir" 
			processingreportmessage="Generando Reporte..." refreshtooltip="Actualizar" 
			togglepagelayouttooltip="Vista previa de Impresión" />
	</telerik:ReportViewer>
</asp:Content>
