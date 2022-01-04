<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SeguimientoCita.ascx.cs" Inherits="HConnexum.Tramitador.Sitio.Modulos.Bloques.SeguimientoCita" %>

<table class="ancho" border="0">
	<tr>
		<td class="labelCell"><asp:Label ID="lblAfiliadoContacto" runat="server" Text="¿El afiliado fue contactado?" /></td>
		<td class="fieldCell">
			<telerik:RadComboBox ID="AfiliadoContacto" DataTextField="NombreValor" DataValueField="NombreValor" AutoPostBack="True" EmptyMessage="Seleccione..." CausesValidation="False" runat="server" Width="258px" OnSelectedIndexChanged="AfiliadoContacto_SelectedIndexChanged" />
		</td>
		<td class="labelCell">&nbsp;</td>
		<td class="fieldCell">&nbsp;</td>
	</tr>
	<asp:Panel runat="server" ID="pFechaHoraProxLlamada">
		<tr>
			<td class="labelCell"><asp:Label ID="lblfechaproxllamada" runat="server" Text="Fecha próxima llamada:" /></td>
			<td class="fieldCell">
				<telerik:RadDatePicker ID="fechaproxllamada" runat="server" MinDate="1900-01-01" DateInput-EmptyMessage="DD/MM/YYYY" Width="100%">
					<Calendar ID="Calendar1" runat="server">
						<SpecialDays>
							<telerik:RadCalendarDay Repeatable="Today">
								<ItemStyle Font-Bold="true" BorderColor="Red" />
							</telerik:RadCalendarDay>
						</SpecialDays>
					</Calendar>
				</telerik:RadDatePicker>
			</td>
			<td class="labelCell"><asp:Label ID="lblhoraproxllamada" runat="server" Text="Hora de próxima llamada:" /></td>
			<td class="fieldCell"><telerik:RadTimePicker ID="horaproxllamada" runat="server" Width="230px" /></td>
		</tr>
	</asp:Panel>
	<asp:Panel runat="server" ID="pSolicitudCambioMedico">
		<tr>
			<td class="labelCell"><asp:Label ID="lblsolicitudcambiomedico" runat="server" Text="¿Desea solicitar cambio de médico?" /></td>
			<td class="fieldCell"><telerik:RadComboBox ID="CambioDeMedico" DataTextField="NombreValor" DataValueField="NombreValor" runat="server" AutoPostBack="True" EmptyMessage="Seleccione..." CausesValidation="False" Width="258px" OnSelectedIndexChanged="CambioDeMedico_SelectedIndexChanged" /></td>
			<td class="labelCell">&nbsp;</td>
			<td class="fieldCell">&nbsp;</td>
		</tr>
	</asp:Panel>
	<asp:Panel runat="server" ID="pCita">
		<tr>
			<td class="labelCell"><asp:Label ID="lblcita" runat="server" Text="¿Cita médica acordada?" /></td>
			<td class="fieldCell"><telerik:RadComboBox ID="Cita" DataTextField="NombreValor" DataValueField="Id" runat="server" AutoPostBack="True" EmptyMessage="Seleccione..." CausesValidation="False" Width="258px" OnSelectedIndexChanged="Cita_SelectedIndexChanged" /></td>
			<td class="labelCell">&nbsp;</td>
			<td class="fieldCell">&nbsp;</td>
		</tr>
	</asp:Panel>
	<asp:Panel runat="server" ID="pFechaHoraCita">
		<tr>
			<td class="labelCell"><asp:Label ID="lblFechaCita" runat="server" Text="Fecha de cita médica:" /></td>
			<td class="fieldCell">
				<telerik:RadDatePicker ID="fechacita" runat="server" MinDate="1900-01-01" DateInput-EmptyMessage="DD/MM/YYYY" Height="20px" Width="100%">
					<Calendar ID="Calendar2" runat="server">
						<SpecialDays>
							<telerik:RadCalendarDay Repeatable="Today">
								<ItemStyle Font-Bold="true" BorderColor="Red" />
							</telerik:RadCalendarDay>
						</SpecialDays>
					</Calendar>
					<DateInput runat="server" DisplayDateFormat="dd/MM/yyyy" DateFormat="dd/MM/yyyy" EnableSingleInputRendering="True" EmptyMessage="DD/MM/YYYY" LabelWidth="64px" Height="20px">
					</DateInput>
					<DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
				</telerik:RadDatePicker>
			</td>
			<td class="labelCell"><asp:Label ID="lblhoracita" runat="server" Text="Hora de cita médica:" /></td>
			<td class="fieldCell"><telerik:RadTimePicker ID="horacita" runat="server" Width="230px" /></td>
		</tr>
	</asp:Panel>
	<asp:Panel runat="server" ID="pSolicitudAnulacion">
		<tr>
			<td class="labelCell"><asp:Label ID="lblSolicitudAnulacion" runat="server" Text="¿Desea solicitar la anulación del servicio de opinión médica?" /></td>
			<td class="fieldCell"><asp:CheckBox ID="SolicitudAnulacion" runat="server" Width="100%" /></td>
			<td class="labelCell">&nbsp;</td>
			<td class="fieldCell">&nbsp;</td>
		</tr>
	</asp:Panel>
</table>
