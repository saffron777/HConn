<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RegistrodeOpinionMedico.ascx.cs" Inherits="HConnexum.Tramitador.Sitio.Modulos.Bloques.RegistrodeOpinionMedico" %>
<%@ Register Src="~/ControlesComunes/MultilineCounter.ascx" TagName="MultilineCounter" TagPrefix="hcc" %>
<%@ Register Src="~/ControlesComunes/FileUpload.ascx" TagName="FileUpload" TagPrefix="hcc" %>

<table class="ancho" border="0">
	<tr>
		<td class="labelCell">
			<asp:Label ID="lblNuevoContacto" runat="server" Text="¿Está de acuerdo con el procedimiento?" />
		</td>
		<td class="fieldCell">
			<telerik:RadComboBox runat="server" ID="OpMed" DataValueField="NombreValor" DataTextField="NombreValor" EmptyMessage="Seleccione" ErrorMessage="Campo Obligatorio" Culture="es-ES" Width="220px"/>
			<asp:RequiredFieldValidator runat="server" ID="rfvOpMed" ErrorMessage="*" ControlToValidate="OpMed" CssClass="validator" Width="25px" ValidationGroup="Validaciones"/>
		</td>
		<td class="fieldCell">&nbsp;</td>
	</tr>
	<tr>
		<td class="labelCell">
			<asp:Label ID="lblAdjuntarArchivo" runat="server" Text="Adjuntar Archivo:" meta:resourcekey="lblArchivosResource1"></asp:Label>
		</td>
		<td class="fieldCell">
			<hcc:FileUpload ID="FileUpload" runat="server" InputSize="64" />
			<asp:HiddenField ID="fechaOpMed" runat="server" />
		</td>
		<td class="fieldCell"></td>
	</tr>
	<tr>
		<td class="labelCell"><asp:Label ID="Lblobservacionmed" runat="server" Text="Observaciones:" /></td>
		<td class="fieldCell" colspan="2"><asp:TextBox ID="observacionmed" runat="server" 
                TextMode="MultiLine" Height="50px" Width="400" CausesValidation="True"></asp:TextBox>
			
			
			
			<asp:Label ID="lblMsjError" runat="server" 
                meta:resourcekey="lblArchivosResource1"></asp:Label>
			
			
			
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
<script type="text/javascript">
	function Cambia() {
		if ($("#" + "<%= OpMed.ClientID %>").val() == '<%= ValorDesicion %>') {
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
