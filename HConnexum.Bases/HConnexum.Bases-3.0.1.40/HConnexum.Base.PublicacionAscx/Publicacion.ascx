<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Publicacion.ascx.cs" Inherits="HConnexum.Base.ControlPublicacion.Publicacion" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<fieldset id="fieldsetPublicacion" class="coolfieldset">
	<legend><b>Publicación</b></legend>
	<div class="control-publicacion-seccion">
		<table>
			<tr>
				<td style="width: 100px"><asp:Label ID="lblFechaValidez" runat="server" Text="Fecha Validez:" /></td>
				<td style="width: 200px">
					<telerik:RadDatePicker ID="rdpFechaValidez" runat="server" MinDate="1900-01-01" DateInput-EmptyMessage="DD/MM/YYYY">
						<Calendar runat="server">
							<SpecialDays>
								<telerik:RadCalendarDay Repeatable="Today">
									<ItemStyle Font-Bold="true" BorderColor="Red" />
								</telerik:RadCalendarDay>
							</SpecialDays>
						</Calendar>
					</telerik:RadDatePicker>
					<asp:RequiredFieldValidator ID="rfvtxtFechaValidez" runat="server" ErrorMessage="*" ControlToValidate="rdpFechaValidez" CssClass="validator" Width="25px" />
				</td>
				<td style="width: 80px"><asp:Label ID="lblIndVigente" runat="server" Text="Publicar:" /></td>
				<td style="width: 220px"><asp:CheckBox ID="chkIndVigente" runat="server" /></td>
			</tr>
		</table>
	</div>
</fieldset>
<asp:Literal runat="server" ID="ltrCollapsedPublicacion" />

