<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Auditoria.ascx.cs" Inherits="HConnexum.Base.ControlAuditoria.Auditoria" %>
<fieldset id="fieldsetAuditoria" class="coolfieldset">
	<legend><b>Auditoría</b></legend>
	<div class="control-auditoria-seccion">
		<table>
			<tr>
				<td style="width: 100px"><asp:Label ID="lblCreadoPor" runat="server" Text="Creado Por:" /></td>
				<td style="width: 200px">
					<asp:TextBox ID="txtCreadoPor" runat="server" Enabled="false" />
					<asp:HiddenField ID="hdnCreadoPor" runat="server" />
				</td>
				<td style="width: 140px"><asp:Label ID="lblFechaCreacion" runat="server" Text="Fecha Creación:" /></td>
				<td style="width: 200px"><asp:TextBox ID="txtFechaCreacion" runat="server" /></td>
			</tr>
			<tr>
				<td style="width: 100px"><asp:Label ID="lblModificadoPor" runat="server" Text="Modificado Por:" /></td>
				<td style="width: 200px"><asp:TextBox ID="txtModificadoPor" runat="server" /></td>
				<td style="width: 140px"><asp:Label ID="lblFechaModificacion" runat="server" Text="Fecha Modificación:" /></td>
				<td style="width: 200px"><asp:TextBox ID="txtFechaModificacion" runat="server" /></td>
			</tr>
			<tr>
				<td style="width: 100px"><asp:Label ID="lblIndEliminado" runat="server" Text="Eliminado:" /></td>
				<td colspan="3" style="width: 500px"><asp:CheckBox ID="chkIndEliminado" runat="server" /></td>
			</tr>
		</table>
	</div>
</fieldset>
<asp:Literal runat="server" ID="ltrCollapsedAuditoria" />

