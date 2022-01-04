<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ActualizacionDatosMedico.ascx.cs" Inherits="HConnexum.Tramitador.Sitio.Modulos.Bloques.ActualizacionDatosMedico" %>
<%@ Register Src="../../ControlesComunes/Telefono.ascx" TagName="Telefono" TagPrefix="hcc" %>

<table class="ancho" border="0">
	<tr>
		<td class="labelCell">
			<asp:Label runat="server" Text="¿Encontró nueva vía de contacto?" />
		</td>
		<td class="fieldCell">
			<telerik:RadComboBox ID="NuevoContacto" runat="server" AutoPostBack="True" CausesValidation="False" DataTextField="NombreValor" DataValueField="NombreValor" EmptyMessage="Seleccione..." OnSelectedIndexChanged="NuevoContacto_SelectedIndexChanged" Width="258px" />
		</td>
		<td class="labelCell">
			&nbsp;
		</td>
		<td class="fieldCell">
			&nbsp;
		</td>
	</tr>
	<asp:Panel runat="server" ID="pTelefono">
		<tr>
			<td class="labelCell">
				<asp:Label runat="server" Text="Teléfono:" />
			</td>
			<td class="fieldCell">
				<hcc:Telefono ID="tlfmed" runat="server" ClientIDMode="Inherit" />
			</td>
			<td class="labelCell">
				&nbsp;
			</td>
			<td class="fieldCell">
				&nbsp;
			</td>
		</tr>
	</asp:Panel>
	<asp:Panel runat="server" ID="pSolicitudAnula">
		<tr>
			<td class="labelCell">
				<asp:Label runat="server" Text="¿Desea anular los casos pendientes del médico?" />
			</td>
			<td class="fieldCell">
				<telerik:RadComboBox ID="SolicitudAnulacion" runat="server" DataTextField="NombreValor" DataValueField="NombreValor" EmptyMessage="Seleccione..." Width="258px" />
			</td>
			<td class="labelCell">
				&nbsp;
			</td>
			<td class="fieldCell">
				&nbsp;
			</td>
		</tr>
	</asp:Panel>
</table>
