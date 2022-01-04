<%@ Page Title="Búsqueda de casos" Language="C#" MasterPageFile="~/Master/Site.Master" AutoEventWireup="true" CodeBehind="PantallaReporteCasos.aspx.cs" Inherits="HConnexum.Tramitador.Sitio.Modulos.Estadisticas.PantallaReporteCasos" %>
<asp:Content ID="cHead" ContentPlaceHolderID="cphHead" runat="server"></asp:Content>
<asp:Content ID="cBody" ContentPlaceHolderID="cphBody" runat="server">
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
	<asp:Panel ID="ReporteCasos" runat="server" Width="100%" GroupingText="Casos">
		<table>
			<tr>
				<td class="labelCell"><asp:Label ID="lblGrupoEmpresarial" runat="server" Text="Grupo Empresarial:" /></td>
				<td class="fieldCell"><telerik:RadComboBox ID="ddlGrupoEmpresarial" 
                        DataValueField="Id" DataTextField="Nombre" EmptyMessage="Seleccione" 
                        runat="server" Width="210px" AutoPostBack="true" Culture="es-ES" Visible="true" 
                        OnSelectedIndexChanged="DdlGrupoEmpresarialSelectedIndexChanged" 
                        CausesValidation="false" /></td>
				<td class="labelCell"><asp:Label ID="lblPoliza" runat="server" Text="P&oacute;liza:" /></td>
				<td class="fieldCell"><asp:TextBox id="txtPoliza" runat="server" Width="200px" /></td>
			</tr>
			<tr>
				<td class="labelCell"><asp:Label ID="lblSuscriptor" runat="server" Text="Suscriptor:" /></td>
				<td style=" width:240px;" class="fieldCell">
					<telerik:RadComboBox ID="ddlSuscriptor" DataValueField="Id" 
                        DataTextField="Nombre" EmptyMessage="Seleccione" 
                                         runat="server" Width="210px" AutoPostBack="true" 
                        Culture="es-ES" Visible="true" 
						                 OnSelectedIndexChanged="DdlSuscriptorSelectedIndexChanged">
                    </telerik:RadComboBox>
					<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ControlToValidate="ddlSuscriptor" 
                                                CssClass="validator" Width="25px" >
                    </asp:RequiredFieldValidator>
				</td>
				<td class="labelCell"><asp:Label ID="lblCertificado" runat="server" Text="Certificado:" /></td>
				<td class="fieldCell"><asp:TextBox id="txtCertificado" runat="server" Width="200px" /></td>
			</tr>
			<tr>
				<td class="labelCell"><asp:Label ID="lblSucursal" runat="server" Text="Sucursal:" /></td>
				<td class="fieldCell"><telerik:RadComboBox ID="ddlSucursal" DataValueField="Id" 
                        DataTextField="Nombre" EmptyMessage="Seleccione" runat="server" Width="210px" 
                        AutoPostBack="true" Culture="es-ES" Visible="true" 
                        OnSelectedIndexChanged="DdlSucursalSelectedIndexChanged" /></td>
				<td class="labelCell"><asp:Label ID="lblCIBeneficiario" runat="server" Text="C.I. Beneficiario:" /></td>
				<td class="fieldCell"><asp:TextBox id="txtCIBeneficiario" runat="server" Width="200px" /></td>
			</tr>
			<tr>
				<td class="labelCell"><asp:Label ID="lblServicio" runat="server" Text="Servicio:" /></td>
				<td class="fieldCell">
                    <telerik:RadComboBox ID="ddlServicio" DataValueField="Id" 
                                         DataTextField="Nombre" EmptyMessage="Seleccione" runat="server" Width="210px" 
                                         AutoPostBack="true" Culture="es-ES" Visible="true" />
                </td>
			</tr>
			<tr>
				<td class="labelCell"><asp:Label ID="lblFechaDesde" runat="server" Text="Fecha desde:" /></td>
				<td class="fieldCell" align="left">
					<telerik:RadDateTimePicker ID="txtFechaDesde" runat="server" Width="215px" 
						                       OnSelectedDateChanged="txtFechaDesde_SelectedDateChanged" 
                                               AutoPostBackControl="Both" DateInput-CausesValidation="false">
					<DatePopupButton ToolTip="Especifique la fecha de b&uacute;squeda"/>
					<TimePopupButton  ToolTip="Especifique la hora de b&uacute;squeda"/>
						<TimeView ID="TimeView1" runat="server" >
							<HeaderTemplate>
								<div class="headerTemplate">Tiempo estimado</div>
							</HeaderTemplate>
						</TimeView>
					</telerik:RadDateTimePicker>
				</td>
				<td class="labelCell"><asp:Label ID="lblFechaHasta" runat="server" Text="Fecha hasta:" /></td>
				<td class="fieldCell" align="left">
					<telerik:RadDateTimePicker ID="txtFechaHasta" runat="server" Width="215px"  Enabled="false" >
					<DatePopupButton ToolTip="Especifique la fecha de b&uacute;squeda"/>
					<TimePopupButton  ToolTip="Especifique la hora de b&uacute;squeda"/>
						<TimeView ID="TimeView3" runat="server" >
							<HeaderTemplate>
								<div class="headerTemplate">Tiempo Estimado</div>
							</HeaderTemplate>
						</TimeView>
					</telerik:RadDateTimePicker>
				</td>
			</tr>
			<tr>
				<td colspan="4">
					<asp:CompareValidator ID="dateCompareValidator" runat="server" Display="Dynamic" ControlToValidate="txtFechaHasta" ControlToCompare="txtFechaDesde" Operator="GreaterThanEqual" ErrorMessage="* El campo <strong>Fecha hasta</strong> no puede ser menor al campo <strong>Fecha desde</strong>" />
				</td>
			</tr>
			<tr>
				<td valign="middle" align="right" colspan="4">
					<asp:Button ID="ButtonProcesar" runat="server" Text="Procesar" OnClick="ButtonProcesar_Click" Width="80px" />&nbsp;
                    <br />
                </td>
			</tr>
		</table>
	</asp:Panel>
</asp:Content>
