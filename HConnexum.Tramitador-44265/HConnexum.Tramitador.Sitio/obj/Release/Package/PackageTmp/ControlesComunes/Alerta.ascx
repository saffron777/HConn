<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Alerta.ascx.cs" Inherits="HConnexum.Tramitador.Sitio.ControlesComunes.Alerta" %>
<script type="text/javascript">
    function OnClientUpdated(sender, args) 
    {
        if ((sender.get_value() != null) && (sender.get_value() != ""))
        {
			sender.show();
		}
    }
    function AbrirVentanas(sender) 
    {           var wnd;
                wnd = window.radopen("Modulos/Alertas/BuzonAlertaLista.aspx?");
                wnd.set_modal(true);
                wnd.setSize(500, 450);
                
    }
    

</script>
<telerik:RadNotification ID="NotificacionAlerta" runat="server" LoadContentOn="TimeInterval"
	Animation="Fade" EnableRoundedCorners="true" EnableShadow="true" Style="z-index: 900000"
	OffsetX="-5" OffsetY="30" Position="TopRight" OnClientUpdated="OnClientUpdated"
	UpdateInterval="100000" AutoCloseDelay="1500" OnCallbackUpdate="OnCallbackUpdate"
	KeepOnMouseOver="true" >
	<ContentTemplate>
		<asp:Literal ID="lblMessage" runat="server"></asp:Literal>      
	</ContentTemplate>
</telerik:RadNotification>
