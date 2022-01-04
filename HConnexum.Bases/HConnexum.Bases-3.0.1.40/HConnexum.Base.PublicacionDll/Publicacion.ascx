<%@ Control Language="C#"%>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<fieldset id="fieldsetPublicacion" class="coolfieldset">
	<legend><b>Publicación</b></legend>
	<div class="control-publicacion-seccion">
		<table>
			<tr>
				<td style="width: 100px"><asp:Label ID="lblFechaValidez" runat="server" Text="Fecha Validez:" /></td>
				<td style="width: 200px">
					<telerik:RadDatePicker id="rdpFechaValidez" runat="server" mindate="1900-01-01" dateinput-emptymessage="DD/MM/YYYY">
						<Calendar runat="server">
							<SpecialDays>
								<telerik:RadCalendarDay repeatable="Today">
									<ItemStyle font-bold="true" bordercolor="Red" />
								</telerik:RadCalendarDay>
							</SpecialDays>
						</Calendar>
					</telerik:RadDatePicker>
					<asp:RequiredFieldValidator ID="rfvtxtFechaValidez" runat="server" ErrorMessage="*" ControlToValidate="rdpFechaValidez" CssClass="validator" Width="25px"/>
				</td>
				<td style="width: 80px"><asp:Label ID="lblIndVigente" runat="server" Text="Publicar:" /></td>
				<td style="width: 220px"><asp:CheckBox ID="chkIndVigente" runat="server"/></td>
			</tr>
		</table>
	</div>
</fieldset>
<asp:Literal runat="server" ID="ltrCollapsedPublicacion" />
