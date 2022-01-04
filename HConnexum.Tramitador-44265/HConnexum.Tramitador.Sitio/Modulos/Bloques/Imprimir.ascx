<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Imprimir.ascx.cs" Inherits="HConnexum.Tramitador.Sitio.Modulos.Bloques.Imprimir" %>
<%@ Register Src="~/ControlesComunes/MultilineCounter.ascx" TagName="MultilineCounter" TagPrefix="hcc" %>
<%@ Register Src="~/ControlesComunes/FileUpload.ascx" TagName="FileUpload" TagPrefix="hcc" %>

<table border="0" width="100%" cellpadding="0" cellspacing="0">
	<tr>
		<td class="labelCell" style="vertical-align: top;">
			<asp:Label ID="lblNuevoContacto" runat="server" Text="¿Está de acuerdo con el procedimiento?" />
		</td>
		<td class="fieldCell" style="vertical-align: top;">
			<telerik:RadComboBox runat="server" ID="OpMed" DataValueField="NombreValor" DataTextField="NombreValor" EmptyMessage="Seleccione" ErrorMessage="Campo Obligatorio" Culture="es-ES" Width="90%" />
			<asp:RequiredFieldValidator runat="server" ID="rfvOpMed" ErrorMessage="*" ControlToValidate="OpMed" CssClass="validator" Width="25px" ValidationGroup="Validaciones" />
		</td>
		<td class="labelCell" style="vertical-align: top;">&nbsp;</td>
		<td class="fieldCell" style="vertical-align: top;">&nbsp;</td>
	</tr>
	<tr>
		<td class="labelCell" style="vertical-align: top;">
			<asp:Label ID="lblAdjuntarArchivo" runat="server" Text="Adjuntar Archivo:" meta:resourcekey="lblArchivosResource1" />
		</td>
		<td class="fieldCell" style="vertical-align: top;">
			<hcc:FileUpload ID="FileUpload" runat="server" InputSize="62" />
			<asp:HiddenField ID="fechaOpMed" runat="server" />
		</td>
		<td class="labelCell" style="vertical-align: top;">&nbsp;</td>
		<td class="fieldCell" style="vertical-align: top;">&nbsp;</td>
	</tr>
	<tr>
		<td class="labelCell" style="vertical-align: top;">
			<asp:Label ID="Lblobservacionmed" runat="server" Text="Observaciones:" />
		</td>
		<td class="fieldCell" style="vertical-align: top;">
			<asp:TextBox ID="observacionmed" runat="server" TextMode="MultiLine" Width="89%" CausesValidation="True" ></asp:TextBox>
			<asp:Label ID="lblMsjError" runat="server" meta:resourcekey="lblArchivosResource1"></asp:Label>
		</td>
	</tr>
	<tr>
		<td class="labelCell" style="vertical-align: top;" colspan="4">
			&nbsp;
		</td>
	</tr>
	<tr>
		<td class="labelCell" style="vertical-align: top;" colspan="4">
			<h4><u>Datos Adicionales</u></h4>
		</td>
	</tr>
	<tr>
		<td class="labelCell" style="vertical-align: top;" colspan="4">
			&nbsp;
		</td>
	</tr>
	<tr>
		<td class="labelCell" style="vertical-align: top;">
			<asp:Label ID="lblAntecedentes" runat="server" Text="Antecedentes (médicos y quirúrgicos)" />
		</td>
		<td class="fieldCell" style="vertical-align: top;" colspan="3">
			<hcc:MultilineCounter ID="antecedentes" runat="server" MaxLength="200" Width="382" />
		</td>
	</tr>
	<tr>
		<td class="labelCell" style="vertical-align: top;">
			<asp:Label ID="lblCicatrices" runat="server" Text="Presenta cicatrices o heridas quirúrgicas" />
		</td>
		<td class="fieldCell" style="vertical-align: top;" colspan="3">
			<hcc:MultilineCounter ID="cicatrices" runat="server" MaxLength="200" Width="382" />
		</td>
	</tr>
	<tr>
		<td class="labelCell" style="vertical-align: top;">
			<asp:Label ID="lblpeso" runat="server" Text="Peso:" />
		</td>
		<td class="fieldCell" style="vertical-align: top;">
			<asp:TextBox ID="peso" runat="server" Width="89%"></asp:TextBox>
		</td>
		<td class="labelCell" style="vertical-align: top;">&nbsp;</td>
		<td class="fieldCell" style="vertical-align: top;">&nbsp;</td>
	</tr>
	<tr>
		<td class="labelCell" style="vertical-align: top;">
			<asp:Label ID="lbltalla" runat="server" Text="Talla:" />
		</td>
		<td class="fieldCell" style="vertical-align: top;">
			<asp:TextBox ID="talla" runat="server" Width="89%"></asp:TextBox>
		</td>
		<td class="labelCell" style="vertical-align: top;">&nbsp;</td>
		<td class="fieldCell" style="vertical-align: top;">&nbsp;</td>
	</tr>
	<tr>
		<td class="labelCell" style="vertical-align: top;">
			<asp:Label ID="lbltension" runat="server" Text="Tension Arterial:" />
		</td>
		<td class="fieldCell" style="vertical-align: top;">
			<asp:TextBox ID="tension" runat="server" Width="89%"></asp:TextBox>
		</td>
		<td class="labelCell" style="vertical-align: top;">&nbsp;</td>
		<td class="fieldCell" style="vertical-align: top;">&nbsp;</td>
	</tr>
	<tr>
		<td class="labelCell" style="vertical-align: top;">&nbsp;</td>
		<td class="fieldCell" style="vertical-align: top;">&nbsp;</td>
		<td class="labelCell" style="vertical-align: top;">&nbsp;</td>
		<td class="fieldCell" style="vertical-align: top;" align="right">
			<asp:Button ID="btnImprimir" runat="server" Text="Imprimir Evaluación Médica" CausesValidation="true" OnClick="btnImprimir_Click" ValidationGroup="Validaciones" />
		</td>
	</tr>
	<tr>
		<td class="labelCell" style="vertical-align: top;" colspan="4">
			<img alt="alerta" src="../../Imagenes/alertIcon.png" />
			&nbsp;
			<asp:Label ID="etiqrec" runat="server" Text="Luego de Imprimir su Evaluación Médica, Oprima el botón &quot;Guardar y Continuar&quot; para su registro y cierre." Font-Bold="True" />
		</td>
	</tr>
	<tr>
		<td class="fieldCell" style="vertical-align: top;" colspan="4">
			<asp:UpdatePanel ID="UpdatePanel1" runat="server">
				<ContentTemplate>
					<asp:Label ID="lbMensajeImpresion" runat="server" Text="Verifique que la impresión fue correcta, de lo contrario presione nuevamente el botón imprimir" Visible="false" />
				</ContentTemplate>
			</asp:UpdatePanel>
		</td>
	</tr>
	<telerik:RadInputManager ID="RadInputManager1" runat="server">
		<telerik:TextBoxSetting BehaviorID="BehaviorNombre" EmptyMessage="Escriba Observacion" Validation-IsRequired="True" ErrorMessage="Campo Obligatorio" Validation-ValidationGroup="Validaciones">
			<TargetControls>
				<telerik:TargetInput ControlID="observacionmed" />
			</TargetControls>
		</telerik:TextBoxSetting>
	</telerik:RadInputManager>
</table>

<telerik:RadCodeBlock runat="server" ID="RadScriptBlock3">
	<script type="text/javascript">
		var _lblAntecedentes = document.getElementById("<%= lblAntecedentes.ClientID %>");
		var _lblCicatrices = document.getElementById("<%= lblCicatrices.ClientID %>");

		$(window).resize(function () {
			if (($(window).width() + 50) <= 1024) {
				_lblAntecedentes.innerHTML = "Antecedentes (médicos y <br>quirúrgicos):";
				_lblCicatrices.innerHTML = "Presenta cicatrices o <br>heridas quirúrgicas:";
			}
			else {
				_lblAntecedentes.innerHTML = "Antecedentes (médicos y quirúrgicos):";
				_lblCicatrices.innerHTML = "Presenta cicatrices o heridas quirúrgicas:";
			}
		});

		function Cambia() {
			if ($("#" + "<%= OpMed.ClientID %>").val() == "<%= ValorDesicion %>") {
				$telerik.findControl(theForm, 'BehaviorNombre').remove_validating = false;
				$find("<%= RadInputManager1.ClientID %>").get_targetInput("<%= observacionmed.ClientID %>")._owner._isRequired = true;
			}
			else {
				$telerik.findControl(theForm, 'BehaviorNombre').remove_validating = true;
				$find("<%= RadInputManager1.ClientID %>").get_targetInput("<%= observacionmed.ClientID %>")._owner._isRequired = false;
			}
		}
		function valueChanged(sender, args) {
			if (sender.get_value().length < 7) {
				setTimeout(function () {
					var eventArgs = new Telerik.Web.UI.MaskedTextBoxEventArgs(args.get_newValue(), args.get_oldValue(), null);
					sender.raise_error(eventArgs);
					sender.set_value("");
					sender._textBoxElement.value = "Minimo 7 caracteres";
				}, 0);
			}
		}
	</script>
</telerik:RadCodeBlock>
