<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ContactoInicial.ascx.cs" Inherits="HConnexum.Tramitador.Sitio.Modulos.Bloques.ContactoInicial" %>

<asp:Panel ID="Panel1" runat="server" Width="100%">
	<br />
	<table width="100%" cellpadding="0" cellspacing="0">
		<tr>
			<td class="labelCell"><asp:Label ID="lblAfiliadoContacto" runat="server" Text="¿El afiliado fue contactado?" /></td>
			<td class="fieldCell">
				<telerik:RadComboBox ID="AfiliadoContacto" DataTextField="NombreValor" DataValueField="NombreValor"
										runat="server" AutoPostBack="false" EmptyMessage="Seleccione..." CausesValidation="true" ValidationGroup="Validaciones"
										OnSelectedIndexChanged="AfiliadoContacto_SelectedIndexChanged" OnClientSelectedIndexChanged="ClientSelectedIndexChanged"
										Width="100%">
				</telerik:RadComboBox>
			</td>
			<td class="labelCell"><asp:RequiredFieldValidator ID="rfvAfiliadoContacto" runat="server" ErrorMessage="*" ControlToValidate="AfiliadoContacto" CssClass="validator" Width="20px" ValidationGroup="Validaciones" /></td>
			<td class="fieldCell"></td>
		</tr>
	</table>
</asp:Panel>
<asp:Panel runat="server" ID="pFechaProxLlamada" Width="100%">
	<table width="100%" cellpadding="0" cellspacing="0">
		<tr>
			<td class="labelCell">
				<asp:Label ID="lblfechaproxllamada" runat="server" Text="Fecha de próxima llamada:" />
			</td>
			<td class="fieldCell">
				<telerik:RadDatePicker ID="fechaproxllamada" runat="server" DateInput-EmptyMessage="DD/MM/YYYY" Width="94%">
					<Calendar ID="Calendar1" runat="server">
						<SpecialDays>
							<telerik:RadCalendarDay Repeatable="Today">
								<ItemStyle Font-Bold="true" BorderColor="Red" />
							</telerik:RadCalendarDay>
						</SpecialDays>
					</Calendar>
				</telerik:RadDatePicker>
                <asp:RequiredFieldValidator ID="rfv_fechaproxllamada" runat="server" ErrorMessage="*" ControlToValidate="fechaproxllamada" CssClass="validator" ValidationGroup="Validaciones" Width="20px" />
			</td>
			<td class="labelCell"></td>
			<td class="fieldCell"></td>
		</tr>
	</table>
</asp:Panel>
<asp:Panel runat="server" ID="pHoraProxLlamada" Width="100%">
	<table width="100%" cellpadding="0" cellspacing="0">
		<tr>
			<td class="labelCell"><asp:Label ID="lblhoraproxllamada" runat="server" Text="Hora de próxima llamada:" /></td>
			<td class="fieldCell">
                <telerik:RadTimePicker ID="horaproxllamada" runat="server" Width="94%" />
                <asp:RequiredFieldValidator ID="rfv_horaproxllamada" runat="server" ErrorMessage="*" ControlToValidate="horaproxllamada" CssClass="validator" ValidationGroup="Validaciones" Width="20px" />
            </td>
			<td class="labelCell"></td>
			<td class="fieldCell"></td>
		</tr>
	</table>
</asp:Panel>
<asp:Panel runat="server" ID="pSolicitudCambioMedico" Width="100%">
	<table width="100%" cellpadding="0" cellspacing="0">
		<tr>
			<td class="labelCell"><asp:Label ID="lblsolicitudcambiomedico" runat="server" Text="¿Solicitar cambio de médico?" /></td>
			<td class="fieldCell"><telerik:RadComboBox ID="CambioDeMedico" DataTextField="NombreValor" EmptyMessage="Seleccione..." DataValueField="NombreValor" runat="server" Width="100%"></telerik:RadComboBox></td>
			<td class="labelCell"><asp:RequiredFieldValidator ID="rfvCambioDeMedico" runat="server" ErrorMessage="*" ControlToValidate="CambioDeMedico" CssClass="validator" Width="20px" ValidationGroup="Validaciones" /></td>
			<td class="fieldCell"></td>
		</tr>
	</table>
</asp:Panel>
<asp:Panel runat="server" ID="pSolicitudAnulacion" Width="100%">
	<table width="100%" cellpadding="0" cellspacing="0">
		<tr>
			<td class="labelCell"><asp:Label ID="lblSolicitudAnulacion" runat="server" Text="¿Solicitar la anulación del servicio?" /></td>
			<td class="fieldCell"><asp:CheckBox ID="SolicitudAnulacion" runat="server" onclick="javascript:ObservacionRequerida();" /></td>
			<td class="labelCell"></td>
			<td class="fieldCell"></td>
		</tr>
		<tr>
			<td class="labelCell" style="vertical-align: top;">
				<div id="d1ObservacionAnulacion" style="display: none;">
					<asp:Label ID="lblObservacioAnulacion" runat="server" Text="Observación por anulacion:" />
				</div>
			</td>
			<td class="fieldCell" colspan="3">
				<div id="d2ObservacionAnulacion" style="display: none;">
					<asp:TextBox ID="ObservacionAnulacion" runat="server" TextMode="MultiLine" Width="99%" onclick="aspMaxLength(this, 500); return false;"></asp:TextBox>
				</div>
			</td>
		</tr>
	</table>
</asp:Panel>
<asp:HiddenField ID="FechaTopeCargaOM" runat="server" />
<br />
<telerik:RadInputManager ID="RadInputManager1" runat="server">
	<telerik:TextBoxSetting BehaviorID="BehaviorNombre" Validation-IsRequired="false" ErrorMessage="Campo obligatorio" Validation-ValidationGroup="Validaciones">
		<TargetControls>
			<telerik:TargetInput ControlID="ObservacionAnulacion" />
		</TargetControls>
	</telerik:TextBoxSetting>
</telerik:RadInputManager>

<telerik:RadCodeBlock runat="server" ID="RadScriptBlock3">
    <script type="text/javascript">
	    var _pHoraProxLlamada = document.getElementById("<%= pHoraProxLlamada.ClientID %>");
	    var _pFechaProxLlamada = document.getElementById("<%= pFechaProxLlamada.ClientID %>");
	    var _pSolicitudCambioMedico = document.getElementById("<%= pSolicitudCambioMedico.ClientID %>");
	    var _pSolicitudAnulacion = document.getElementById("<%= pSolicitudAnulacion.ClientID %>");
	    var _AfiliadoContacto = document.getElementById("<%= AfiliadoContacto.ClientID %>");
	    var _lblSolicitudAnulacion = document.getElementById("<%= lblSolicitudAnulacion.ClientID %>");

	    $(document).ready(function () {
	        _pHoraProxLlamada.style.display = "none";
	        _pFechaProxLlamada.style.display = "none";
	        _pSolicitudCambioMedico.style.display = "";
	        _pSolicitudAnulacion.style.display = "";
	        document.getElementById("<%= rfv_fechaproxllamada.ClientID%>").validationGroup = "";
	        document.getElementById("<%= rfv_horaproxllamada.ClientID%>").validationGroup = "";
	    });

	    $(window).resize(function () {
	        if (($(window).width() + 50) <= 1024)
	            _lblSolicitudAnulacion.innerHTML = "¿Solicitar la anulación del <br>servicio?";
	        else
	            _lblSolicitudAnulacion.innerHTML = "¿Solicitar la anulación del servicio?";
	    });

	    function ClientSelectedIndexChanged(e, args) {
	        _pHoraProxLlamada.style.display = "none";
	        _pFechaProxLlamada.style.display = "none";
	        _pSolicitudCambioMedico.style.display = "none";
	        _pSolicitudAnulacion.style.display = "none";
	        $telerik.findControl(theForm, 'CambioDeMedico').clearSelection;
	        document.getElementById('<%=SolicitudAnulacion.ClientID%>').checked = false;
	        var _fechaproxllamada = $telerik.findControl(theForm, 'fechaproxllamada_dateInput');
	        var _horaproxllamada = $telerik.findControl(theForm, 'horaproxllamada_dateInput');
	        _fechaproxllamada.set_value("");
	        _horaproxllamada.set_value("");
	        document.getElementById("<%= rfvCambioDeMedico.ClientID%>").validationGroup = "";
	        document.getElementById("<%= rfvAfiliadoContacto.ClientID%>").validationGroup = "";
	        document.getElementById("<%= rfv_horaproxllamada.ClientID%>").validationGroup = "";
	        document.getElementById("<%= rfv_fechaproxllamada.ClientID%>").validationGroup = "";

	        if (_AfiliadoContacto.value != null) {
	            switch (_AfiliadoContacto.value.toUpperCase()) {
	                case "REPETIR LLAMADA":
	                    _pHoraProxLlamada.style.display = "";
	                    _pFechaProxLlamada.style.display = "";
	                    $telerik.findControl(theForm, 'BehaviorNombre')._validationGroup = "";
	                    document.getElementById("<%= rfv_fechaproxllamada.ClientID%>").validationGroup = "Validaciones";
	                    document.getElementById("<%= rfv_horaproxllamada.ClientID%>").validationGroup = "Validaciones";
	                    break;

	                case "SÍ":
	                case "SI": 
                        _pSolicitudCambioMedico.style.display = "";
                        _pSolicitudAnulacion.style.display = "";
                        document.getElementById("<%= rfvCambioDeMedico.ClientID%>").validationGroup = "Validaciones";
	                    break;

	                default:
	                    var dt = new Date();
	                    _fechaproxllamada.MinDate = dt.format("yyyy") + "-01-01";
	                    _fechaproxllamada.MaxDate = dt.format("yyyy") + "-12-31";
	                    break;
	            }
	        }
	    }

	    function ObservacionRequerida() {
	        $telerik.findControl(theForm, 'BehaviorNombre').remove_validating = true;
	        $telerik.findControl(theForm, 'BehaviorNombre')._validationGroup = "";
	        if (document.getElementById('<%=SolicitudAnulacion.ClientID%>').checked) {
	            $telerik.findControl(theForm, 'BehaviorNombre').remove_validating = false;
	            $telerik.findControl(theForm, 'BehaviorNombre')._validationGroup = "Validaciones";
	            $telerik.findControl(theForm, 'RadInputManager1').get_targetInput("<%= ObservacionAnulacion.ClientID %>")._owner._isRequired = true;
	            document.getElementById('d1ObservacionAnulacion').style.display = "block";
	            document.getElementById('d2ObservacionAnulacion').style.display = "block";
	        } else {
	            $telerik.findControl(theForm, 'BehaviorNombre')._validationGroup = "";
	            document.getElementById('d1ObservacionAnulacion').style.display = "none";
	            document.getElementById('d2ObservacionAnulacion').style.display = "none";
	        }
	    }
    </script>
</telerik:RadCodeBlock>