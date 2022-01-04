<%@ Page Title="Reporte de Facturas" Language="C#" MasterPageFile="~/Master/Site.Master" AutoEventWireup="true" CodeBehind="ReporteFacturas.aspx.cs" Inherits="HConnexum.Tramitador.Sitio.Modulos.Estadisticas.ReporteFacturas" %>

<%@ Register Assembly="Telerik.ReportViewer.WebForms, Version=7.2.13.1016, Culture=neutral, PublicKeyToken=a9d7983dfcc261be" Namespace="Telerik.ReportViewer.WebForms" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
	<telerik:ReportViewer ID="ReporteFactura" runat="server" Width="100%" 
		ProgressText="Generando Reporte..." ShowPrintPreviewButton="False" 
		Height="600px">
		<Resources CurrentPageToolTip="Página actual" ExportButtonText="Exportar" ExportSelectFormatText="Exportar al formato seleccionado" ExportToolTip="Exportar"
				   FirstPageToolTip="Primera Página" LastPageToolTip="Ultima Página" NavigateBackToolTip="Regresar" NavigateForwardToolTip="Adelante" NextPageToolTip="Siguiente Página"
				   PreviousPageToolTip="Página Anterior" PrintToolTip="Imprimir" ProcessingReportMessage="Generando Reporte..." RefreshToolTip="Actualizar" TogglePageLayoutToolTip="Vista previa de Impresión" />
	</telerik:ReportViewer>
</asp:Content>
