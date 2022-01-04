<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Telefono.ascx.cs" Inherits="HConnexum.Tramitador.Sitio.Telefono" %>
<table cellpadding="0" cellspacing="0">
    <tr>
        <td class="fieldCell" style="padding:0;">
            <asp:TextBox runat="server" ID="txtCodPais" Enabled="false" Width="30px" />
            <asp:TextBox runat="server" ID="txtCodArea" Width="30px" />
            <asp:TextBox runat="server" ID="txtNumero" Width="177px"/>
        </td>
    </tr>
</table>
<telerik:RadInputManager ID="rimTelefono" runat="server">
	<telerik:TextBoxSetting BehaviorID="NumericTelefono" EmptyMessage="">
		<TargetControls>
			<telerik:TargetInput ControlID="txtCodPais" />
			<telerik:TargetInput ControlID="txtCodArea" />
			<telerik:TargetInput ControlID="txtNumero" />
		</TargetControls>
	</telerik:TextBoxSetting>
</telerik:RadInputManager>
