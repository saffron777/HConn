<%@ Page Title="Reporte de Casos "Language="C#" MasterPageFile="~/Master/Site.Master" AutoEventWireup="true" CodeBehind="ReporteCasos.aspx.cs" Inherits="HConnexum.Tramitador.Sitio.Modulos.Estadisticas.ReporteCasos" %>

<%@ Register Assembly="Telerik.ReportViewer.WebForms, Version=7.2.13.1016, Culture=neutral, PublicKeyToken=a9d7983dfcc261be" Namespace="Telerik.ReportViewer.WebForms" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
	<table width="100%">
		<tr>
			<td>
				<telerik:ReportViewer ID="ReportCasos" runat="server" Width="100%" 
					Height="500px" ProgressText="Generando Reporte...">
					<resources currentpagetooltip="Página actual" exportbuttontext="Exportar" 
						exportselectformattext="Exportar al formato seleccionado" 
						exporttooltip="Exportar" firstpagetooltip="Primera Página" 
						lastpagetooltip="Ultima Página" navigatebacktooltip="Regresar" 
						navigateforwardtooltip="Adelante" nextpagetooltip="Siguiente Página" 
						previouspagetooltip="Página Anterior" printtooltip="Imprimir" 
						processingreportmessage="Generando Reporte..." refreshtooltip="Actualizar" 
						togglepagelayouttooltip="Vista previa de Impresión" />
				</telerik:ReportViewer>
			</td>
		</tr>
	</table>
</asp:Content>
