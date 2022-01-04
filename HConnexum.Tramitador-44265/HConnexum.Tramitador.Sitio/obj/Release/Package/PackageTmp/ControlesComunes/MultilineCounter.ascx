<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MultilineCounter.ascx.cs" Inherits="HConnexum.Tramitador.Sitio.MultilineCounter" %>
<div style="width:100%">
	<table border="0" cellpadding="0" cellspacing="0" width="100%">
		<tr>
			<td>
				<asp:TextBox ID="txtControl" runat="server" Height="50px" width="100%" />
			</td>
		</tr>
		<tr>
			<td style="text-align: right">
				<asp:Label ID="lblCounter" runat="server" ForeColor="GrayText" Font-Size="Small" />
			</td>
		</tr>
	</table>
	<telerik:RadInputManager ID="rimMultilineCounter" runat="server" >
		<telerik:TextBoxSetting BehaviorID="MultilineControl">
			<TargetControls>
				<telerik:TargetInput ControlID="txtControl" />
			</TargetControls>
		</telerik:TextBoxSetting>
	</telerik:RadInputManager>
</div>