<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="HorasMinutosSegundos.ascx.cs" Inherits="HConnexum.Tramitador.Sitio.ControlesComunes.HorasMinutosSegundos" %>
<table>
    <tr>
        <td><asp:Label runat="server" ID="lblHoras" Text="Hrs:"></asp:Label></td>
        <td><asp:TextBox ID="txtHoras" runat="server" Width="30px"/></td>
        <td><asp:Label runat="server" ID="lblMinutos" Text="Min:"></asp:Label></td>
        <td><asp:TextBox ID="txtMinutos" runat="server" Width="30px"/></td>
        <td><asp:Label runat="server" ID="lblSegundos" Text="Seg:"></asp:Label></td>
        <td><asp:TextBox ID="txtSegundos" runat="server" Width="30px"/></td>
    </tr>
</table>
<telerik:RadInputManager ID="rimHorasMinutosSegundos" runat="server">
	<telerik:NumericTextBoxSetting BehaviorID="NumericHoras" EmptyMessage="" MinValue="0" DecimalDigits="0" GroupSeparator="" >
		<TargetControls>
			<telerik:TargetInput ControlID="txtHoras" />
		</TargetControls>
	</telerik:NumericTextBoxSetting>
	<telerik:NumericTextBoxSetting BehaviorID="NumericMinutos" EmptyMessage="" MaxValue="59" MinValue="0" DecimalDigits="0">
		<TargetControls>
			<telerik:TargetInput ControlID="txtMinutos" />
		</TargetControls>
	</telerik:NumericTextBoxSetting>
	<telerik:NumericTextBoxSetting BehaviorID="NumericSegundos" EmptyMessage="" MaxValue="59" MinValue="0" DecimalDigits="0">
		<TargetControls>
			<telerik:TargetInput ControlID="txtSegundos" />
		</TargetControls>
	</telerik:NumericTextBoxSetting>
</telerik:RadInputManager>