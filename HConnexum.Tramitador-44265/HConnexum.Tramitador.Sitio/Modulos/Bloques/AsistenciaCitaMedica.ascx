<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AsistenciaCitaMedica.ascx.cs" Inherits="HConnexum.Tramitador.Sitio.Modulos.Bloques.AsistenciaCitaMedica" %>

<table class="ancho">
	<tr>
		<td class="labelCell"><asp:Label ID="lblAfiliadoContacto" runat="server" Text="¿El afiliado fue contactado?" /></td>
		<td class="fieldCell"><telerik:RadComboBox ID="AfiliadoContacto" DataTextField="NombreValor" DataValueField="NombreValor" AutoPostBack="True" EmptyMessage="Seleccione..." OnSelectedIndexChanged="AfiliadoContacto_SelectedIndexChanged" CausesValidation="False" runat="server" Width="100%" /></td>
		<td class="labelCell">&nbsp;</td>
		<td class="fieldCell">&nbsp;</td>
	</tr>
	<asp:Panel runat="server" ID="pCambioMedico">
		<tr>
			<td class="labelCell">
				<asp:Label ID="lblcambiomedico" runat="server" Text="¿Desea solicitar cambio de médico?" />
			</td>
			<td class="fieldCell">
				<telerik:RadComboBox ID="CambioDeMedico" DataTextField="NombreValor" DataValueField="NombreValor" AutoPostBack="True" EmptyMessage="Seleccione..." OnSelectedIndexChanged="CambioDeMedico_SelectedIndexChanged" CausesValidation="False" runat="server" Width="100%" />
			</td>
			<td class="labelCell">&nbsp;</td>
			<td class="fieldCell">&nbsp;</td>
		</tr>
	</asp:Panel>
	<asp:Panel runat="server" ID="pAsistio">
		<tr>
			<td class="labelCell">
				<asp:Label ID="lblAsistio" runat="server" Text="¿Asistió a la cita médica?" />
			</td>
			<td class="fieldCell">
				<telerik:RadComboBox ID="Asistencia" DataTextField="NombreValor" DataValueField="NombreValor" AutoPostBack="True" EmptyMessage="Seleccione..." CausesValidation="False" runat="server" OnSelectedIndexChanged="Asistencia_SelectedIndexChanged" Width="100%" />
			</td>
			<td class="labelCell">&nbsp;</td>
			<td class="fieldCell">&nbsp;</td>
		</tr>
	</asp:Panel>
	<asp:Panel runat="server" ID="pAccion">
		<tr>
			<td class="labelCell"><asp:Label ID="lblAccion" runat="server" Text="Acción a ejecutar:" /></td>
			<td class="fieldCell">
				<telerik:RadComboBox ID="Accion" DataTextField="NombreValor" DataValueField="NombreValor" AutoPostBack="True" EmptyMessage="Seleccione..." OnSelectedIndexChanged="Accion_SelectedIndexChanged" CausesValidation="False" runat="server" Width="100%" />
			</td>
			<td class="labelCell">&nbsp;</td>
			<td class="fieldCell">&nbsp;</td>
		</tr>
	</asp:Panel>
	<asp:Panel runat="server" ID="pFechaProxLlamada">
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
			<td class="labelCell">&nbsp;</td>
			<td class="fieldCell">&nbsp;</td>
		</tr>
	</asp:Panel>
	<asp:Panel runat="server" ID="pHoraProxLlamada">
		<tr>
			<td class="labelCell"><asp:Label ID="lblhoraproxllamada" runat="server" Text="Hora próxima llamada:" /></td>
			<td class="fieldCell"><telerik:RadTimePicker ID="horaproxllamada" runat="server" Width="100%"></telerik:RadTimePicker></td>
			<td class="labelCell">&nbsp;</td>
			<td class="fieldCell">&nbsp;</td>
		</tr>
	</asp:Panel>
	<asp:Panel runat="server" ID="pFechaCita">
		<tr>
			<td class="labelCell"><asp:Label ID="lblFechaCita" runat="server" Text="Fecha de cita médica:" /></td>
			<td class="fieldCell">
				<telerik:RadDatePicker ID="fechacita" runat="server" MinDate="1900-01-01" DateInput-EmptyMessage="DD/MM/YYYY" Width="100%">
					<Calendar ID="Calendar2" runat="server">
						<SpecialDays>
							<telerik:RadCalendarDay Repeatable="Today">
								<ItemStyle Font-Bold="true" BorderColor="Red" />
							</telerik:RadCalendarDay>
						</SpecialDays>
					</Calendar>
					<DateInput runat="server" DisplayDateFormat="dd/MM/yyyy" DateFormat="dd/MM/yyyy" EnableSingleInputRendering="True" EmptyMessage="DD/MM/YYYY" LabelWidth="64px"></DateInput>
					<DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
				</telerik:RadDatePicker>
			</td>
			<td class="labelCell">&nbsp;</td>
			<td class="fieldCell">&nbsp;</td>
		</tr>
	</asp:Panel>
	<asp:Panel runat="server" ID="pHoraCita">
		<tr>
			<td class="labelCell"><asp:Label ID="lblhoracita" runat="server" Text="Hora de cita médica:" /></td>
			<td class="fieldCell"><telerik:RadTimePicker ID="horacita" runat="server" Width="100%" /></td>
			<td class="labelCell">&nbsp;</td>
			<td class="fieldCell">&nbsp;</td>
		</tr>
	</asp:Panel>
</table>
