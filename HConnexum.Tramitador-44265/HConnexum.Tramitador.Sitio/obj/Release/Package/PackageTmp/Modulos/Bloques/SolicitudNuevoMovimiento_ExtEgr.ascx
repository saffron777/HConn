<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SolicitudNuevoMovimiento_ExtEgr.ascx.cs"  Inherits="HConnexum.Tramitador.Sitio.Modulos.Bloques.SolicitudNuevoMovimiento_ExtEgr" %>

<style type="text/css">
	table.ancho { font-weight: bold; }
	.number { text-align: right; }
	.arrange { float: left; }
	.PanelStyle { margin-top: 10px; padding: 5px 5px 5px 5px; }
</style>
<asp:Panel ID="pnlMovimientoDatos" runat="server" GroupingText="Datos del Movimiento" CssClass="PanelStyle">
	<table runat="server" id="T1" border="0" width="100%" class="ancho">
		<tr>
			<td class="labelCell4colP" style="vertical-align: top;"><asp:Label ID="Label3" runat="server" Text="Tipo de Movimiento"/></td>
			<td class="labelCell4colP" style="vertical-align: top;"><asp:Label ID="lblDiagnostico" runat="server" Text="Diagnóstico" /></td>
			<td class="labelCell4colP" style="vertical-align: top;"><asp:Label ID="lblProcedimiento" runat="server" Text="Procedimiento" /></td>
			<td class="labelCell4colP" style="vertical-align: top;"><asp:Label ID="lblModo" runat="server" Text="Modo" /></td>
		</tr>
		<tr>
			<td class="fieldCell4colP" style="vertical-align: top;">
				<telerik:RadComboBox ID="NomTipoMov" runat="server" DataValueField="Id" DataTextField="NombreValor" OnSelectedIndexChanged="NomTipoMov_SelectedIndexChanged" AutoPostBack="True" CausesValidation="False" AppendDataBoundItems="True" Culture="es-ES" EmptyMessage="Seleccione" Width="85%" />
				<asp:RequiredFieldValidator ID="rfvMovTipo" runat="server" ControlToValidate="NomTipoMov" ErrorMessage="*" CssClass="validator" Width="25px" ValidationGroup="Validaciones" />
			</td>
			<td class="fieldCell4colP" style="vertical-align: top;">
				<telerik:RadComboBox ID="NomDiagnostico" runat="server" DataValueField="HCM_DIAG_DIAGNOTICO_Id" DataTextField="Des_Diagnostico" OnSelectedIndexChanged="NomDiagnostico_SelectedIndexChanged" AutoPostBack="True" CausesValidation="False" AppendDataBoundItems="true" Culture="es-ES" EmptyMessage="Seleccione" Width="85%" MaxHeight="180px" />
				<asp:RequiredFieldValidator ID="rfvDiagnostico" runat="server" ControlToValidate="NomDiagnostico" ErrorMessage="*" CssClass="validator" Width="25px" ValidationGroup="Validaciones" />
			</td>
			<td class="fieldCell4colP" style="vertical-align: top;">
				<telerik:RadComboBox ID="NomProcedimiento" runat="server" DataValueField="Hcm_Tratamientos_Id" DataTextField="Descripcion" OnSelectedIndexChanged="NomProcedimiento_SelectedIndexChanged" AutoPostBack="True" CausesValidation="False" AppendDataBoundItems="true" Culture="es-ES" EmptyMessage="Seleccione" Width="85%" MaxHeight="180px" />
				<asp:RequiredFieldValidator ID="rfvProcedimiento" runat="server" ControlToValidate="NomProcedimiento" ErrorMessage="*" CssClass="validator" Width="25px" ValidationGroup="Validaciones" />
			</td>
			<td class="fieldCell4colP" style="vertical-align: top;">
				<telerik:RadComboBox ID="NomModoMov" runat="server" DataTextField="NombreValor" DataValueField="Id" AppendDataBoundItems="True" Culture="es-ES" EmptyMessage="Seleccione" Width="85%" />
				<asp:RequiredFieldValidator ID="rfvMovModo" runat="server" ControlToValidate="NomModoMov" ErrorMessage="*" CssClass="validator" Width="25px" ValidationGroup="Validaciones" />
			</td>
		</tr>
		<tr>
			<td class="labelCell4colP" style="vertical-align: top;">
				<asp:Label ID="lblMedico" runat="server" Text="Médico Tratante"/>
				<asp:Label ID="lblObservacion2" runat="server" Text="Observaciones" Visible="False" />
			</td>
			<td class="labelCell4colP" style="vertical-align: top;"><asp:Label ID="lblMontoPresupuesto" runat="server" Text="Monto Presupuesto" /></td>
			<td class="labelCell4colP" style="vertical-align: top;"><asp:Label ID="lblDiasHospitalizacion" runat="server" Text="Días de Hospitalización" /></td>
			<td class="labelCell4colP" style="vertical-align: top;">&nbsp;</td>
		</tr>
		<tr>
			<td class="fieldCell4colP" style="vertical-align: top;">
				<asp:TextBox ID="NomMedico" runat="server" MaxLength="200" Width="83%" onkeypress="return ValidarSoloTexto(event);" />
				<asp:TextBox ID="ObsAnulacion" runat="server" MaxLength="200" Rows="5" TextMode="MultiLine" Visible="False" Width="83%" Style="overflow:auto" />
			</td>
			<td class="fieldCell4colP" style="vertical-align: top;"><asp:TextBox ID="MontoPresup" runat="server" MaxLength="13" Width="83%" /></td>
			<td class="fieldCell4colP" style="vertical-align: top;"><asp:TextBox ID="NumDiasHosp" runat="server" MaxLength="2" Width="83%" /></td>
			<td class="fieldCell4colP" style="vertical-align: top;">&nbsp;</td>
		</tr>
        <tr>
            <td class="fieldCell4colP" style="vertical-align: top;" width="50%" colspan="4"><asp:Label ID="lblObservacion" runat="server" Text="Observaciones" /></td>
        </tr>
        <tr>
            <td class="fieldCell4colP" style="vertical-align: top;" width="50%" colspan="4"><asp:TextBox ID="ObsDiagnostico" runat="server" MaxLength="200" Rows="5" TextMode="MultiLine" Width="50%" Style="overflow:auto"/></td>
        </tr>
	</table>
</asp:Panel>
<asp:Panel ID="pnlDocumentos" runat="server" GroupingText="Documentos" CssClass="PanelStyle">
	<table runat="server" id="Table1" border="0" width="100%" class="ancho">
        <tr>
			<td style="vertical-align: top; width: 100%" colspan="2">
				<asp:CheckBoxList ID="Documentos" runat="server" RepeatColumns="4" RepeatDirection="Horizontal" Width="100%" >
					<asp:ListItem>Informe Médico</asp:ListItem>
					<asp:ListItem>Radiología</asp:ListItem>
					<asp:ListItem>Presupuesto</asp:ListItem>
					<asp:ListItem>Carta Narrativa</asp:ListItem>
					<asp:ListItem>Facturas</asp:ListItem>
					<asp:ListItem>Laboratorio</asp:ListItem>
					<asp:ListItem>Datos Filiatorios</asp:ListItem>
				</asp:CheckBoxList></td>
		</tr>
		<tr>
            <td style="vertical-align: top; width: 50%" colspan="2"><asp:CustomValidator runat="server" ID="CustomValidator1" ClientValidationFunction="ValidarDocumentos" ErrorMessage="* &nbsp; &nbsp; Debe seleccionar y enviar al menos un documento." Width="100%" CssClass="validator" ValidationGroup="Validaciones"/></td>
		</tr>
        <tr>
            <td style="vertical-align: top; width: 50%" colspan="2"><asp:Label ID="Label2" runat="server" Text="Otros Documentos" /></td>
        </tr>
	    <tr>
            <td style="vertical-align: top; width: 50%"><asp:TextBox ID="TextBox1" runat="server" MaxLength="200" TextMode="MultiLine" Rows="5" Width="100%" Style="overflow:auto"/><br/></td>
            <td style="vertical-align: top; width: 50%">&nbsp;</td>
        </tr>
	</table>
</asp:Panel>
<br />
<telerik:RadInputManager ID="RadInputManager1" runat="server">
	<telerik:TextBoxSetting BehaviorID="txtSettings" Validation-ValidationGroup="Validaciones">
		<TargetControls>
			<telerik:TargetInput ControlID="NomMedico" />
			<telerik:TargetInput ControlID="ObsDiagnostico" />
			<telerik:TargetInput ControlID="ObsAnulacion" />
		</TargetControls>
	</telerik:TextBoxSetting>
	<telerik:TextBoxSetting BehaviorID="txtNumericSettings" EnabledCssClass="number" Validation-ValidationGroup="Validaciones">
		<TargetControls>
			<telerik:TargetInput ControlID="NumDiasHosp" />
		</TargetControls>
	</telerik:TextBoxSetting>
	<telerik:NumericTextBoxSetting BehaviorID="BehaviorNumeric" Validation-IsRequired="true" DecimalDigits="2" DecimalSeparator="," Validation-ValidationGroup="Validaciones">
		<TargetControls>
			<telerik:TargetInput ControlID="MontoPresup" />
		</TargetControls>
	</telerik:NumericTextBoxSetting>
</telerik:RadInputManager>
<!-- CLAVES XML-INICIO -->
<span class="arrange"><asp:HiddenField runat="server" ID="IdDiagnostico"/></span>
<span class="arrange"><asp:HiddenField runat="server" ID="IdProcedimiento"/></span>
<span class="arrange"><asp:HiddenField runat="server" ID="TipoMov"/></span>
<span class="arrange"><asp:HiddenField runat="server" ID="TipoCaso"/></span>
<span class="arrange"><asp:HiddenField runat="server" ID="EstatusMovimientoWeb"/></span>
<span class="arrange"><asp:HiddenField runat="server" ID="IdIntermediario" /></span>
<span class="arrange"><asp:HiddenField runat="server" ID="IdProveedor" /></span>
<span class="arrange"><asp:HiddenField runat="server" ID="Ult_Mvto_Reversable" /></span>
<!-- CLAVES XML-FIN -->
<span class="arrange"><asp:HiddenField runat="server" ID="hidBloqueTipo" Value="Egreso" /></span>
<span class="arrange"><asp:HiddenField runat="server" ID="ULTIMOTIPOMOV" /></span>
<span class="arrange"><asp:HiddenField runat="server" ID="hidSuscriptorIntermediarioConnectionString" /></span>

<script type="text/javascript">
	function ValidarDocumentos(source, args) {
		var chkDocumentos = document.getElementById('<%= Documentos.ClientID %>');
		var chkLista = chkDocumentos.getElementsByTagName("input");
		for (var i = 0; i < chkLista.length; i++) {
			if (chkLista[i].checked) {
				args.IsValid = true;
				return;
			}
		}
		args.IsValid = false;
	}
</script>