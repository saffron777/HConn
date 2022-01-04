<%@ Page Title="Detalle de Agenda para un Paso" Language="C#" MasterPageFile="~/Master/Site.Master" AutoEventWireup="true" CodeBehind="ParametrosAgendaDetalle.aspx.cs" Inherits="HConnexum.Tramitador.Sitio.Modulos.Parametrizador.ParametrosAgendaDetalle" meta:resourcekey="PageResource1" %>
<%@ Register Src="~/ControlesComunes/Publicacion.ascx" TagName="Publicacion" TagPrefix="hcc" %>
<%@ Register Src="~/ControlesComunes/Auditoria.ascx" TagName="Auditoria" TagPrefix="hcc" %>
<%@ Register Src="~/ControlesComunes/HorasMinutosSegundos.ascx" TagName="HorasMinutosSegundos" TagPrefix="hcc" %>
<asp:Content ID="cHead" ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>
<asp:Content ID="cBody" ContentPlaceHolderID="cphBody" runat="server">
	<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" DefaultLoadingPanelID="RadAjaxLoadingPanel1" meta:resourcekey="RadAjaxManager1Resource1">
		<AjaxSettings>
			<telerik:AjaxSetting AjaxControlID="RadGridDetails">
				<UpdatedControls>
					<telerik:AjaxUpdatedControl ControlID="PanelMaster" />
				</UpdatedControls>
			</telerik:AjaxSetting>
		</AjaxSettings>
	</telerik:RadAjaxManager>
	<div>
		<asp:Panel ID="PanelMaster" runat="server" meta:resourcekey="PanelMasterResource1">
			<fieldset>
				<legend>
					<asp:Label ID="lblLegendAgendaparaunPaso" runat="server" Text="Agenda para un Paso:" meta:resourcekey="lblLegendAgendaparaunPasoResource1" />
				</legend>
				<table>
					<tr>
						<td>
							<asp:Label ID="lblIdPaso" runat="server" Text="Paso:" meta:resourcekey="lblIdPasoResource1" />
						</td>
						<td>
							<asp:TextBox ID="txtPaso" runat="server" ReadOnly="True" Width="300" meta:resourcekey="txtPasoResource1" />
						</td>
					</tr>
					<tr>
						<td>
							<asp:Label runat="server" Text="Key Fecha Ejecución:"/>
						</td>
						<td>
							<asp:TextBox ID="txtKeyFechaEjec" runat="server" Width="300"/>
						</td>
					</tr>
					<tr>
						<td>
							<asp:Label runat="server" Text="Key Hora Ejecución:"/>
						</td>
						<td>
							<asp:TextBox ID="txtKeyHoraEjec" runat="server" Width="300"/>
						</td>
					</tr>
					<tr>
						<td>
							<asp:Label ID="lblCantidad" runat="server" Text="Frecuencia de Ejecución:" meta:resourcekey="lblCantidadResource1" />
						</td>
						<td>
							<hcc:HorasMinutosSegundos runat="server" ID="txtCantidad" Width="200" IsRequired="True" />
						</td>
					</tr>
					<tr>
						<td>
							<asp:Label ID="lblIndInmediato" runat="server" Text="Ind Inmediato:" meta:resourcekey="lblIndInmediatoResource1" />
						</td>
						<td>
							<asp:CheckBox ID="chkIndInmediato" runat="server" meta:resourcekey="chkIndInmediatoResource1" />
						</td>
					</tr>
				</table>
			</fieldset>
			<hcc:Publicacion ID="Publicacion" runat="server" />
			<hcc:Auditoria ID="Auditoria" runat="server" />
		</asp:Panel>
		<telerik:RadInputManager ID="RadInputManager1" runat="server">
			<telerik:NumericTextBoxSetting DecimalDigits="0" GroupSeparator="" BehaviorID="BehaviorIdPaso" EmptyMessage="Escriba IdPaso" Type="Number" Validation-IsRequired="True" ErrorMessage="Campo Obligatorio">
				<TargetControls>
					<telerik:TargetInput ControlID="TxtIdPaso" />
				</TargetControls>
			</telerik:NumericTextBoxSetting>
			<telerik:TextBoxSetting BehaviorID="BehaviorNombre" EmptyMessage="Escriba Nombre" Validation-IsRequired="True" ErrorMessage="Campo Obligatorio">
				<TargetControls>
					<telerik:TargetInput ControlID="TxtNombre" />
				</TargetControls>
			</telerik:TextBoxSetting>
			<telerik:TextBoxSetting BehaviorID="BehaviorDia" EmptyMessage="Escriba Dia" Validation-IsRequired="True" ErrorMessage="Campo Obligatorio">
				<TargetControls>
					<telerik:TargetInput ControlID="TxtDia" />
				</TargetControls>
			</telerik:TextBoxSetting>
		</telerik:RadInputManager>
		<br />
		<br />
		<asp:Button ID="cmdGuardar" runat="server" Text="Guardar" OnClick="cmdGuardar_Click" meta:resourcekey="cmdGuardarResource1" />
		<asp:Button ID="cmdGuardaryAgregar" runat="server" Text="Guardar y Agregar Otro" OnClick="cmdGuardaryAgregar_Click" meta:resourcekey="cmdGuardaryAgregarResource1" />
		<asp:Button ID="cmdLimpiar" runat="server" Text="Limpiar" OnClientClick="$('form').clearForm(); return false" meta:resourcekey="cmdLimpiarResource1" />
	</div>
	<telerik:RadScriptBlock runat="server" ID="RadScriptBlock1">
		<script type="text/javascript">
			function IrAnterior() {
				var wnd = GetRadWindow();
				wnd.setUrl('<%= RutaPadreEncriptada %>');
			}
		</script>
	</telerik:RadScriptBlock>
</asp:Content>
