<%@ Page Title="Detalle de Movimiento por Usuario" Language="C#" MasterPageFile="~/Master/Site.Master" AutoEventWireup="true" CodeBehind="PantallaDetalleMovimiento.aspx.cs" Inherits="HConnexum.Tramitador.Sitio.Modulos.Estadisticas.PantallaDetalleMovimiento" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
<telerik:RadScriptBlock runat="server" ID="RadScriptBlock2">
	<script type="text/javascript">
		function btnSuscriptor_Click() {
			var wnd;
			var idMenu = '<%= IdMenuEncriptado %>';
			wnd = window.radopen("../../Modulos/Parametrizador/SuscriptorLista.aspx?IdMenu=" + idMenu, "RadWindow1");
			wnd.set_modal(true);
			wnd.setSize(900, 450);
		}
	</script>
</telerik:RadScriptBlock>
		<telerik:RadAjaxManager ID="RadAjaxManager1" DefaultLoadingPanelID="RadAjaxLoadingPanel1" runat="server" meta:resourcekey="RadAjaxManager1Resource1">
			<AjaxSettings>
				<telerik:AjaxSetting AjaxControlID="lblGrupoEmpresarial">
					<UpdatedControls>
						<telerik:AjaxUpdatedControl ControlID="ddlSuscriptor" UpdatePanelRenderMode="Inline"/>
					</UpdatedControls>
				</telerik:AjaxSetting>
				<telerik:AjaxSetting AjaxControlID="ddlSuscriptor" >
					<UpdatedControls>
						<telerik:AjaxUpdatedControl ControlID="ddlSucursal" />
						<telerik:AjaxUpdatedControl ControlID="ddlServicio" />
						<telerik:AjaxUpdatedControl ControlID="ddlUsuario" />
						<telerik:AjaxUpdatedControl ControlID="TxtIdSuscriptor" />
						<telerik:AjaxUpdatedControl ControlID="Button1" />
					</UpdatedControls>
				</telerik:AjaxSetting>
				<telerik:AjaxSetting AjaxControlID="ddlSucursal">
					<UpdatedControls>
						<telerik:AjaxUpdatedControl ControlID="ddlArea" />
						<telerik:AjaxUpdatedControl ControlID="ddlServicio" />
						<telerik:AjaxUpdatedControl ControlID="ddlUsuario" />
					</UpdatedControls>
				</telerik:AjaxSetting>
				<telerik:AjaxSetting AjaxControlID="ddlArea">
					<UpdatedControls>
						<telerik:AjaxUpdatedControl ControlID="ddlServicio" />
						<telerik:AjaxUpdatedControl ControlID="ddlUsuario" />
					</UpdatedControls>
				</telerik:AjaxSetting>
				<telerik:AjaxSetting AjaxControlID="ddlPais">
					<UpdatedControls>
						<telerik:AjaxUpdatedControl ControlID="ddlDivTerr1" />
						<telerik:AjaxUpdatedControl ControlID="ddlDivTerr2" />
						<telerik:AjaxUpdatedControl ControlID="ddlDivTerr3" />
					</UpdatedControls>
				</telerik:AjaxSetting>
				<telerik:AjaxSetting AjaxControlID="ddlDivTerr1">
					<UpdatedControls>
						<telerik:AjaxUpdatedControl ControlID="ddlDivTerr2" />
						<telerik:AjaxUpdatedControl ControlID="ddlDivTerr3" />
					</UpdatedControls>
				</telerik:AjaxSetting>
				<telerik:AjaxSetting AjaxControlID="ddlDivTerr2">
					<UpdatedControls>
						<telerik:AjaxUpdatedControl ControlID="ddlDivTerr3" />
					</UpdatedControls>
				</telerik:AjaxSetting>
			</AjaxSettings>
		</telerik:RadAjaxManager>
	<asp:Panel ID="ReporteDetalleMovimientosUsuario" runat="server" Width="80%" GroupingText="Detalle de Movimientos por Usuario">
				<table>
			<tr>
				<td >
					<asp:Label ID="lblGrupoEmpresarial" runat="server" Text="Grupo Empresarial:" />
				</td>
				<td>
					<telerik:RadComboBox ID="ddlGrupoEmpresarial" DataValueField="Id"  
						DataTextField="Nombre" EmptyMessage="Seleccione" runat="server" Width="200px" 
						AutoPostBack="true" Culture="es-ES" Visible="true" 
						onselectedindexchanged="ddlGrupoEmpresarial_SelectedIndexChanged" CausesValidation="false" />
				</td>
				<td >
					<asp:Label ID="lblProveedor" runat="server" Text="Proveedor:" />
				</td>
				<td>
					<asp:TextBox ID="TxtIdSuscriptor" runat="server" Enabled="false" meta:resourcekey="TxtIdSuscriptorResource1" Width="194px"/>
					<asp:HiddenField ID="TxtHiddenId" runat="server" />
					<asp:HiddenField ID="TxtHiddenTipo" runat="server" />
				</td>
				<td>
				<asp:Button ID="Button1" runat="server" Enabled="false" Text="Buscar" OnClientClick="btnSuscriptor_Click(); return false;" CausesValidation="False" meta:resourcekey="btnSuscriptorResource1" />
				</td>
			</tr>
			<tr>
				<td >
					<asp:Label ID="lblSuscriptor" runat="server" Text="Suscriptor:" />
				</td>
				<td style=" width:240px;">
					<telerik:RadComboBox ID="ddlSuscriptor" DataValueField="Id" 
						DataTextField="Nombre" EmptyMessage="Seleccione" runat="server" Width="200px" 
						AutoPostBack="true" Culture="es-ES" Visible="true" 
						onselectedindexchanged="ddlSuscriptor_SelectedIndexChanged" />
					<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
						ErrorMessage="*" ControlToValidate="ddlSuscriptor" CssClass="validator" 
						Width="27px" Height="19px" ></asp:RequiredFieldValidator>
				</td>
				<td >
					<asp:Label ID="lblPais" runat="server" Text="Pa&iacute;s:" />
				</td>
				<td>
					<telerik:RadComboBox ID="ddlPais" DataValueField="Id" DataTextField="Nombre" 
						EmptyMessage="Seleccione" runat="server" Width="200px" AutoPostBack="true" 
						Culture="es-ES" Visible="true" 
						onselectedindexchanged="ddlPais_SelectedIndexChanged" />
				</td>
			</tr>
			<tr>
				<td >
					<asp:Label ID="lblSucursal" runat="server" Text="Sucursal:" />
				</td>
				<td>
					<telerik:RadComboBox ID="ddlSucursal" DataValueField="Id" 
						DataTextField="Nombre" EmptyMessage="Seleccione" runat="server" Width="200px" 
						AutoPostBack="true" Culture="es-ES" Visible="true" 
						onselectedindexchanged="ddlSucursal_SelectedIndexChanged" />
				</td>
				<td >
					<asp:Label ID="lblDivTerr1" runat="server" Text="Div. Terr. 1:" />
				</td>
				<td>
					<telerik:RadComboBox ID="ddlDivTerr1" DataValueField="Id" 
						DataTextField="NombreDivTer1" EmptyMessage="Seleccione" runat="server" Width="200px" 
						AutoPostBack="true" Culture="es-ES" Visible="true" 
						onselectedindexchanged="ddlDivTerr1_SelectedIndexChanged" />
				</td>
			</tr>
			<tr>
				<td >
					<asp:Label ID="lblArea" runat="server" Text="Area:" />
				</td>
				<td >
					<telerik:RadComboBox ID="ddlArea" DataValueField="Id" DataTextField="Nombre" 
						EmptyMessage="Seleccione" runat="server" Width="200px" AutoPostBack="true" 
						Culture="es-ES" Visible="true" 
						onselectedindexchanged="ddlArea_SelectedIndexChanged" />
				</td>
				<td >
					<asp:Label ID="lblDivTerr2" runat="server" Text="Div. Terr. 2:" />
				</td>
				<td>
					<telerik:RadComboBox ID="ddlDivTerr2" DataValueField="Id" 
						DataTextField="NombreDivTer1" EmptyMessage="Seleccione" runat="server" Width="200px" 
						AutoPostBack="true" Culture="es-ES" Visible="true" 
						onselectedindexchanged="ddlDivTerr2_SelectedIndexChanged" />
				</td>
			</tr>
			<tr>
				<td >
					<asp:Label ID="lblServicio" runat="server" Text="Servicio:" />
				</td>
				<td>
					<telerik:RadComboBox ID="ddlServicio" DataValueField="Id" 
						DataTextField="Nombre" EmptyMessage="Seleccione" runat="server" Width="200px" 
						AutoPostBack="true" Culture="es-ES" Visible="true" />
				</td>
				<td >
					<asp:Label ID="lblDivTerr3" runat="server" Text="Div. Terr. 3:" />
				</td>
				<td>
					<telerik:RadComboBox ID="ddlDivTerr3" DataValueField="Id" 
						DataTextField="NombreDivTer1" EmptyMessage="Seleccione" runat="server" Width="200px" 
						AutoPostBack="true" Culture="es-ES" Visible="true" />
				</td>
			</tr>
			<tr>
				<td >
					<asp:Label ID="lblUsuario" runat="server" Text="Usuario:" />
				</td>
				<td>
					<telerik:RadComboBox ID="ddlUsuario" DataValueField="IdUsuario" DataTextField="Nombre" 
						EmptyMessage="Seleccione" runat="server" Width="200px" AutoPostBack="true" 
						Culture="es-ES" Visible="true" />
				</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
			</tr>
			<tr>
				<td >
					<asp:Label ID="lblFechaDesde" runat="server" Text="Fecha Desde:" />
				</td>
				<td align="left" >
					<telerik:RadDateTimePicker ID="txtFechaDesde" runat="server" Width="200px" OnSelectedDateChanged="txtFechaDesde_SelectedDateChanged"
						AutoPostBackControl="Both" DateInput-CausesValidation="false">
						<DatePopupButton ToolTip="Especifique la fecha de b&uacute;squeda" />
						<TimePopupButton ToolTip="Especifique la hora de b&uacute;squeda" />
						<TimeView ID="TimeView1" runat="server">
							<HeaderTemplate>
								<div class="headerTemplate">
									Tiempo Estimado</div>
							</HeaderTemplate>
						</TimeView>
					</telerik:RadDateTimePicker>
				</td>
				<td>
					<asp:Label ID="lblFechaHasta" runat="server" Text="Fecha Hasta:" />
				</td>
				<td align="left">
					<telerik:RadDateTimePicker ID="txtFechaHasta" runat="server" Width="200px" Enabled="false">
						<DatePopupButton ToolTip="Especifique la fecha de b&uacute;squeda" />
						<TimePopupButton ToolTip="Especifique la hora de b&uacute;squeda" />
						<TimeView ID="TimeView3" runat="server">
							<HeaderTemplate>
								<div class="headerTemplate">
									Tiempo Estimado</div>
							</HeaderTemplate>
						</TimeView>
					</telerik:RadDateTimePicker>
				</td>
			</tr>
			<tr>
				<td colspan="4" align="left" >
					<asp:CompareValidator ID="dateCompareValidator" runat="server" Display="Dynamic"
						ControlToValidate="txtFechaHasta" ControlToCompare="txtFechaDesde" Operator="GreaterThanEqual"
						ErrorMessage="* El campo <strong>Fecha Hasta</strong> no puede ser menor al campo <strong>Fecha Desde</strong>" />
				</td>
			</tr>
			<tr>
				<td valign="middle" align="right" colspan="4">
					<asp:Button ID="ButtonProcesar" runat="server" Text="Procesar" OnClick="ButtonProcesar_Click" />
				</td>
			</tr>
		</table>
	</asp:Panel>
</asp:Content>
