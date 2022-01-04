<%@ Page Title="Solicitud de Servicio" Language="C#" MasterPageFile="~/Master/Site.Master" AutoEventWireup="true" CodeBehind="SolicitudServicio.aspx.cs" Inherits="HConnexum.Tramitador.Sitio.Modulos.Estructura.SolicitudServicio" %>
<asp:Content ID="cHead" ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>
<asp:Content ID="cBody" ContentPlaceHolderID="cphBody" runat="server">
	<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" DefaultLoadingPanelID="RadAjaxLoadingPanel1">
		<AjaxSettings>
			<telerik:AjaxSetting AjaxControlID="ddlSuscriptor">
				<UpdatedControls>
					<telerik:AjaxUpdatedControl ControlID="ddlServicio" UpdatePanelRenderMode="Inline" />
				</UpdatedControls>
			</telerik:AjaxSetting>
		</AjaxSettings>
	</telerik:RadAjaxManager>
	<div style="width: 100%">
		<asp:Panel ID="PanelSimulaProveedor" runat="server" Width="100%">
			<fieldset>
				<legend>
					<asp:Label ID="LblSimulaProveedor" runat="server" Text="Simula Proveedor" Font-Bold="True" />
				</legend>
				<table>
					<tr>
						<%--<td >
						</td>--%>
						<td width=125px style="padding-left:10px;" colspan=2>
							Proveedor a simular:
						</td>
						<td>
							<telerik:RadComboBox runat="server" ID="rcbProveeServSimulados" EmptyMessage="Seleccione..." DataTextField="Nombre" DataValueField="Id" OnSelectedIndexChanged="rcbProveeServSimulados_SelectedIndexChanged" AutoPostBack="True" Width="300px" ValidationGroup="simulado" />
						</td>
						<td style="width: 10px">
						</td>
						<td>
							<asp:Button ID="btnSimulaProveedor" runat="server" Text="Simula proveedor" OnClick="btnSimulaProveedor_Click" ValidationGroup="simulado" />
						</td>
					</tr>
				</table>
			</fieldset>
		</asp:Panel>
		<br />
		<asp:Panel ID="PanelMaster" runat="server" Width="100%">
			<fieldset>
				<legend><b><asp:Label runat="server" Text="Solicitud de Servicio" /></b></legend>
				<table>
					<tr>
						<td style="padding-left:10px;" width=125px>
							<asp:Label runat="server" Text="Suscriptor:" />
						</td>
						<td>
							<telerik:RadComboBox runat="server" ID="ddlSuscriptor" DataValueField="IdIntermediario" DataTextField="SuscriptorIntermediario" EmptyMessage="Seleccione" ErrorMessage="Campo Obligatorio" Width="300px" OnSelectedIndexChanged="ddlSuscriptor_SelectedIndexChanged" AutoPostBack="True" CausesValidation="false" ValidationGroup="servicio" />
							<asp:RequiredFieldValidator ID="rfvSuscriptor" runat="server" ErrorMessage="*" ControlToValidate="ddlSuscriptor" CssClass="validator" Width="25px" />
						</td>
						<td>
							<asp:Label runat="server" Text="Servicio:" />
						</td>
						<td>
							<telerik:RadComboBox runat="server" ID="ddlServicio" DataValueField="IdServicioQuePuedeSolicitar" DataTextField="ServicioQuePuedeSolicitar" EmptyMessage="Seleccione" ErrorMessage="Campo Obligatorio" Width="300px" Enabled="False" ValidationGroup="servicio" />
							<asp:RequiredFieldValidator ID="rfvServicio" runat="server" ErrorMessage="*" ControlToValidate="ddlServicio" CssClass="validator" Width="25px" />
						</td>
					</tr>
				</table>
			</fieldset>
		</asp:Panel>
		<br />
		<table width="100%">
			<tr>
				<td style="text-align: center;">
					<asp:Label ID="lblNotiene" runat="server" Text="No Puede Procesar Solicitudes de Casos" />
				</td>
				<td style="text-align: right;">
					<asp:Button ID="cmdSolicitar" runat="server" Text="Solicitar" OnClick="cmdSolicitar_Click" />
				</td>
			</tr>
		</table>
		<br />
	</div>
</asp:Content>
