<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DatosGenerales.ascx.cs" Inherits="HConnexum.Tramitador.Sitio.Modulos.Bloques.DatosGenerales" %>
<%@ Register Src="~/ControlesComunes/Telefono.ascx" TagName="Telefono" TagPrefix="hcc" %>

<table width="100%" cellpadding="0" cellspacing="0" border="0">
	<tr>
		<td class="labelCell"><asp:Label ID="lblDocumento" runat="server" Text="Documento Identidad:" /></td>
		<td class="fieldCell">
            <div style="width: 98%">
			    <div style="width: 7%; float: left; padding-right: 1%;"><asp:TextBox ID="tipdocaseg" runat="server" Width="100%"/></div>
			    <div style="width: 90%; float: right;"><asp:TextBox ID="numdocaseg" runat="server" Width="100%" /></div>
            </div>
		</td>
        <td class="labelCell"><asp:Label ID="lblServicio" runat="server" Text="Servicio:" /></td>
		<td class="fieldCell"><asp:TextBox ID="servicio" runat="server" Width="98%"/></td>
	</tr>
	<tr>
		<td class="labelCell"><asp:Label ID="lblAsegurado" runat="server" Text="Asegurado:" /></td>
		<td class="fieldCell"><asp:TextBox ID="nomaseg" runat="server" Width="98%"/></td>
		<td class="labelCell"><asp:Label ID="lblIdExterno" runat="server" Text="Nro. de caso externo:"/></td>
		<td class="fieldCell"><asp:TextBox ID="IdCasoExterno" runat="server" Width="98%"/></td>
	</tr>
	<tr>
		<td class="labelCell"><asp:Label runat="server" Text="Edad:"/></td>
		<td class="fieldCell"><asp:TextBox ID="EdadAseg" runat="server" Width="98%"/></td>
		<td class="labelCell"><asp:Label runat="server" Text="Sexo:"/></td>
		<td class="fieldCell"><asp:TextBox ID="SexoAseg" runat="server" Width="98%"/></td>
	</tr>
	<tr>
		<td class="labelCell"><asp:Label runat="server" Text="Teléfono:"/></td>
		<td class="fieldCell"><asp:TextBox ID="tlfaseg" runat="server" Width="98%"/></td>
		<td class="labelCell"><asp:Label runat="server" Text="Intermediario:" /></td>
		<td class="fieldCell"><asp:TextBox ID="Intermediario" runat="server" Width="98%"/></td>
	</tr>
	<tr>
		<td class="labelCell" align="left"><asp:Label ID="lblContratante" runat="server" Text="Contratante:" /></td>
		<td class="fieldCell"><asp:TextBox ID="contratante" runat="server" Width="98%"/></td>
		<td class="labelCell"><asp:Label ID="lblClinica" runat="server" Text="Proveedor:"/></td>
		<td class="fieldCell"><asp:TextBox ID="nomclinica" runat="server" Width="98%"/></td>
	</tr>
	<tr>
		<td class="labelCell"><asp:Label ID="lblFechaSolicitud" runat="server" Text="Fecha de solicitud:"/></td>
		<td class="fieldCell">
			<telerik:RadDatePicker ID="fechasolicitud" runat="server" Height="24px" Width="98%">
				<Calendar runat="server" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"></Calendar>
				<DateInput runat="server" DisplayDateFormat="dd/MM/yyyy" DateFormat="dd/MM/yyyy" EnableSingleInputRendering="True" LabelWidth="64px" Height="24px"></DateInput>
			</telerik:RadDatePicker>
		</td>
		<td class="labelCell">
			<asp:Label ID="lblCaso" runat="server" Text="Nro. de caso:"/>
		</td>
		<td class="fieldCell"><asp:TextBox ID="IdCasoHc" runat="server" Width="98%"/></td>
	</tr>
    <tr>
        <td class="labelCell"><asp:Label ID="lblDiagnostico" runat="server" Text="Diagnóstico:" Font-Bold="true" Width="124px"/></td>
        <td colspan="3" style="padding-left: 5px; padding-right: 6px; white-space:nowrap;"><asp:TextBox ID="diagnostico" runat="server" Width="99%"/></td>
    </tr>
</table>
