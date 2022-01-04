<%@ Page Language="C#" AutoEventWireup="True" MasterPageFile="~/Master/Site.Master" Title="Detalle de Agrupacion" CodeBehind="AgrupacionDetalle.aspx.cs" Inherits="HConnexum.Tramitador.Sitio.Modulos.Estructura.AgrupacionDetalle" %>
<%@ Register Src="~/ControlesComunes/Publicacion.ascx" TagName="Publicacion" TagPrefix="hcc" %>
<%@ Register Src="~/ControlesComunes/Auditoria.ascx" TagName="Auditoria" TagPrefix="hcc" %>
<asp:Content ID="cHead" ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>
<asp:Content ID="cBody" ContentPlaceHolderID="cphBody" runat="server">
		<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" DefaultLoadingPanelID="RadAjaxLoadingPanel1">
			<AjaxSettings> 
				<telerik:AjaxSetting AjaxControlID="RadGridDetails">
					<UpdatedControls>
						<telerik:AjaxUpdatedControl ControlID="PanelMaster" />
					</UpdatedControls>
				</telerik:AjaxSetting>
			</AjaxSettings>
		</telerik:RadAjaxManager>
		<div>
			<asp:Panel ID="PanelMaster" runat="server">
				<fieldset>
					<legend><b><asp:Label runat="server" Text="Agrupacion" Font-Bold="True" meta:resourcekey="LblLegendAgrupacionResource"/></b></legend>
					<table>
						<tr>
				<td><asp:Label ID="lblNombre" runat="server" Text="Nombre:"/></td>
				<td>
				<asp:TextBox ID="txtNombre" runat="server" />
				</td>
			</tr>
					</table>
				</fieldset>
				<hcc:Publicacion id="Publicacion" runat="server"/>
				<hcc:Auditoria id="Auditoria" runat="server"/>
			</asp:Panel>
			<telerik:RadInputManager ID="RadInputManager1" runat="server">
			<telerik:TextBoxSetting   BehaviorID="BehaviorNombre" EmptyMessage="Escriba Nombre"  Validation-IsRequired="True" ErrorMessage="Campo Obligatorio">
    <TargetControls><telerik:TargetInput ControlID="TxtNombre"/></TargetControls>
</telerik:TextBoxSetting>
			</telerik:RadInputManager>
			<br /><br />
			<asp:Button ID="cmdGuardar" runat="server" Text="Guardar" onclick="cmdGuardar_Click"/>
			<asp:Button ID="cmdGuardaryAgregar" runat="server" Text="Guardar y Agregar Otro" onclick="cmdGuardaryAgregar_Click"/>
			<asp:Button ID="cmdLimpiar" runat="server" Text="Limpiar"  onclientclick="$('form').clearForm();return false"/>
		</div>
</asp:Content>