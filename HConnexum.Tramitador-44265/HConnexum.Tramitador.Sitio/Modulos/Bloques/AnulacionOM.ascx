<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AnulacionOM.ascx.cs" Inherits="HConnexum.Tramitador.Sitio.Modulos.Bloques.AnulacionOM" %>

<style type="text/css">
	.fondo{color: Gray;}
</style>
<table width="100%" cellpadding="0" cellspacing="0">
	<tr>
		<td class="labelCell"><asp:Label runat="server" ID="lblSolicitudAnulacion" Text="¿Aprueba la anulación del caso?" /></td>
		<td class="fieldCell">
			<telerik:RadComboBox ID="ApruebaAnula" runat="server" DataValueField="NombreValor"
				DataTextField="NombreValor" AllowCustomText="true" MarkFirstMatch="true" AutoPostBack="false"
				CausesValidation="false" Culture="es-ES" EmptyMessage="Seleccione" Width="100%" />
			
		</td>
		<td class="labelCell"><asp:RequiredFieldValidator ID="rfvApruebaAnula" runat="server" ControlToValidate="ApruebaAnula"
				ErrorMessage="*" CssClass="validator" Width="25px" ValidationGroup="Validaciones"></asp:RequiredFieldValidator></td>
		<td class="fieldCell"></td>
	</tr>
	<tr>
		<td class="labelCell"><asp:Label ID="lblObservacioAnulacion" runat="server" Text="Observacion por Anulacion" /></td>
		<td class="fieldCell" colspan="3"><asp:TextBox ID="ObservacionAnulacion" runat="server" TextMode="MultiLine" Width="100%" CssClass="fondo"/></td>
	</tr>
</table>
<telerik:RadCodeBlock runat="server" ID="RadScriptBlock3">
	<script type="text/javascript">
		var _lblSolicitudAnulacion = document.getElementById("<%= lblSolicitudAnulacion.ClientID %>");
		var _lblObservacioAnulacion = document.getElementById("<%= lblObservacioAnulacion.ClientID %>");

		$(window).resize(function () {
			if (($(window).width() + 50) <= 1024) {
				_lblSolicitudAnulacion.innerHTML = "¿Aprueba la anulación <br>del caso?";
				_lblObservacioAnulacion.innerHTML = "Observacion por <br>Anulacion";
			}
			else {
				_lblSolicitudAnulacion.innerHTML = "¿Aprueba la anulación del caso?";
				_lblObservacioAnulacion.innerHTML = "Observacion por Anulacion";
			}
		});
	</script>
</telerik:RadCodeBlock>
