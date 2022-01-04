<%@ Page Title="Consolidado de Servicios" Language="C#" MasterPageFile="~/Master/Site.Master" AutoEventWireup="true" CodeBehind="PantallaReporteConsolidadoServicio.aspx.cs" Inherits="HConnexum.Tramitador.Sitio.Modulos.Estadisticas.PantallaReporteConsolidadoServicio" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
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
					</UpdatedControls>
				</telerik:AjaxSetting>
				<telerik:AjaxSetting AjaxControlID="ddlSucursal">
					<UpdatedControls>
						<telerik:AjaxUpdatedControl ControlID="ddlServicio" />
					</UpdatedControls>
				</telerik:AjaxSetting>
			</AjaxSettings>
		</telerik:RadAjaxManager>
	<asp:Panel ID="ReporteConsolidadoServicio" runat="server" Width="80%" GroupingText="Consolidado de Servicio">
		<table>
			<tr>
				<td>
					<asp:Label ID="lblGrupoEmpresarial" runat="server" Text="Grupo Empresarial:" />
				</td>
				<td>
					<telerik:RadComboBox ID="ddlGrupoEmpresarial" DataValueField="Id" 
						DataTextField="Nombre" EmptyMessage="Seleccione" runat="server" Width="200px" 
						AutoPostBack="true" Culture="es-ES" Visible="true" 
						onselectedindexchanged="ddlGrupoEmpresarial_SelectedIndexChanged" CausesValidation="false" />
				</td>
				<td>
					<asp:Label ID="lblSucursal" runat="server" Text="Sucursal:" />
				</td>
				<td>
					<telerik:RadComboBox ID="ddlSucursal" DataValueField="Id" 
						DataTextField="Nombre" EmptyMessage="Seleccione" runat="server" Width="200px" 
						AutoPostBack="true" Culture="es-ES" Visible="true" 
						onselectedindexchanged="ddlSucursal_SelectedIndexChanged" />
				</td>
			</tr>
			<tr>
				<td>
					<asp:Label ID="lblSuscriptor" runat="server" Text="Suscriptor:" />
				</td>
				<td style=" width:240px;" >
					<telerik:RadComboBox ID="ddlSuscriptor" DataValueField="Id" 
						DataTextField="Nombre" EmptyMessage="Seleccione" runat="server" Width="200px" 
						AutoPostBack="true" Culture="es-ES" Visible="true" 
						onselectedindexchanged="ddlSuscriptor_SelectedIndexChanged" />
					<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ControlToValidate="ddlSuscriptor" CssClass="validator" Width="25px" ></asp:RequiredFieldValidator>
				</td>
				<td>
					<asp:Label ID="lblServicio" runat="server" Text="Servicio:" />
				</td>
				<td>
					<telerik:RadComboBox ID="ddlServicio" DataValueField="Id" DataTextField="Nombre" EmptyMessage="Seleccione" runat="server" Width="200px" AutoPostBack="true" Culture="es-ES" Visible="true"  />
				</td>
			</tr>
			<tr>
				<td >
					<asp:Label ID="lblFechaDesde" runat="server" Text="Fecha Desde:" />
				</td>
				<td align="left">
					<telerik:RadDateTimePicker ID="txtFechaDesde" runat="server" Width="200px" 
						onselecteddatechanged="txtFechaDesde_SelectedDateChanged" AutoPostBackControl="Both" DateInput-CausesValidation="false" >
					<DatePopupButton ToolTip="Especifique la fecha de b&uacute;squeda"/>
					<TimePopupButton  ToolTip="Especifique la hora de b&uacute;squeda"/>
						<TimeView ID="TimeView1" runat="server" >
							<HeaderTemplate>
								<div class="headerTemplate">
									Tiempo Estimado</div>
							</HeaderTemplate>
						</TimeView>
					</telerik:RadDateTimePicker>
				</td>
				<td >
					<asp:Label ID="lblFechaHasta" runat="server" Text="Fecha Hasta:" />
				</td>
				<td align="left">
					<telerik:RadDateTimePicker ID="txtFechaHasta" runat="server" Width="200px" Enabled="false" >
					<DatePopupButton ToolTip="Especifique la fecha de b&uacute;squeda"/>
					<TimePopupButton  ToolTip="Especifique la hora de b&uacute;squeda"/>
						<TimeView ID="TimeView3" runat="server" >
							<HeaderTemplate>
								<div class="headerTemplate">
									Tiempo Estimado</div>
							</HeaderTemplate>
						</TimeView>
					</telerik:RadDateTimePicker>
				</td>
			</tr>
			<tr>
				<td colspan="4">
					<asp:CompareValidator ID="dateCompareValidator" runat="server" Display="Dynamic" ControlToValidate="txtFechaHasta" ControlToCompare="txtFechaDesde" Operator="GreaterThanEqual" ErrorMessage="* El campo <strong>Fecha Hasta</strong> no puede ser menor al campo <strong>Fecha Desde</strong>" />
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
