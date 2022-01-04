<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Master/Site.Master" Title="Detalle de Movimiento" CodeBehind="MovimientoDetalle.aspx.cs" Inherits="HConnexum.Tramitador.Sitio.Modulos.Tracking.MovimientoDetalle" %>

<asp:Content ID="cHead" ContentPlaceHolderID="cphHead" runat="server">
    <style type="text/css">
        html { overflow-x:hidden; }
    </style>
</asp:Content>
<asp:Content ID="cBody" ContentPlaceHolderID="cphBody" runat="server">
		<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" DefaultLoadingPanelID="RadAjaxLoadingPanel1">
			<AjaxSettings> 
				<telerik:AjaxSetting AjaxControlID="RadGridDetails">
					<UpdatedControls>
						<telerik:AjaxUpdatedControl ControlID="PanelMaster" />
					</UpdatedControls>
				</telerik:AjaxSetting>
			</AjaxSettings>
		</telerik:RadAjaxManager>
        <br />
		<div>
			<asp:Panel ID="PanelMaster" runat="server">
				<fieldset>
					<legend><b><asp:Label ID="Label1" runat="server" Text="Tracking de casos - Movimientos" Font-Bold="True" meta:resourcekey="LblLegendMovimientoResource"/></b></legend>
				    <table width="100%">
					    <tr>
				            <td><asp:Label ID="lblServicio" runat="server" 
                                    Text="Servicio:" Font-Bold="True"/></td>
				            <td><asp:TextBox ID="txtServicio" Enabled="false" runat="server" Width="300px" /></td>
				            <td><asp:Label ID="lblVersion" runat="server" Text="Versión:" 
                                    Font-Bold="True"/></td>
				            <td><asp:TextBox ID="txtVersion" Enabled="false" runat="server" Width="50px" /></td>
			            </tr>
			            <tr>
				            <td><asp:Label ID="lblCaso" runat="server" 
                                    Text="Nro. de caso:" Font-Bold="True"/></td>
				            <td><asp:TextBox ID="txtCaso" Enabled="false" runat="server" Width="50px" /></td>
				            <td><asp:Label ID="lblEstatusCaso" runat="server" 
                                    Text="Estatus:" Font-Bold="True"/></td>
				            <td><asp:TextBox ID="txtEstatusCaso" Enabled="false" runat="server" /></td>
			            </tr>
			            <tr>
				            <td><asp:Label ID="lblMovimiento" runat="server" 
                                    Text="Movimiento:" Font-Bold="True"/></td>
				            <td><asp:TextBox ID="txtMovimiento" Enabled="false" runat="server" Width="300px" /></td>
				            <td><asp:Label ID="lblEstatusMov" runat="server" 
                                    Text="Estatus:" Font-Bold="True"/></td>
				            <td><asp:TextBox ID="txtEstatusMovimiento" Enabled="false" runat="server" /></td>
			            </tr>
			            <tr>
				            <td><asp:Label ID="lblTipoMovimiento" runat="server" 
                                    Text="Tipo movimiento:" Font-Bold="True"/></td>
				            <td><asp:TextBox ID="txtTipoMovimiento" Enabled="false" runat="server" Width="150px" /></td>
				            <td></td>
				            <td></td>
			            </tr>
				    </table>
				</fieldset>
			</asp:Panel>
			<br />
		</div>
        <table  width="100%" class="tabla">
            <tr>
                <td><br />
                    <telerik:RadTabStrip ID="RadTabStrip1" runat="server" MultiPageID="RadMultiPage1" SelectedIndex="0" CssClass="tabStrip">
                        <Tabs>
                            <telerik:RadTab Text="Auditoria" Selected="True"></telerik:RadTab>
                            <telerik:RadTab Text="Observaciones"></telerik:RadTab>
                            <telerik:RadTab Text="Resultado"></telerik:RadTab>
                            <telerik:RadTab Text="Tiempos"></telerik:RadTab>
                        </Tabs>
                    </telerik:RadTabStrip>
	                <telerik:RadMultiPage ID="RadMultiPage1" runat="server" SelectedIndex="0" CssClass="cssRadMultiPage">
                        <telerik:RadPageView ID="tabAuditoria"  runat="server" Width="100%" Height="225px"></telerik:RadPageView>
                        <telerik:RadPageView ID="tabObservaciones" runat="server" Width="100%" Height="225px"></telerik:RadPageView>
                        <telerik:RadPageView ID="tabResultado" runat="server" Width="100%" Height="225px"></telerik:RadPageView>
                        <telerik:RadPageView ID="tabTiempos" runat="server" Width="100%" Height="225px"></telerik:RadPageView>
                    </telerik:RadMultiPage>
                </td>
            </tr>
        </table>
    <br />
    <asp:Button ID="cmdCerrar" runat="server" Text="Cerrar" CausesValidation="false" OnClientClick="cerrarVentana();" />&nbsp;
    <asp:Button ID="btnVerCaso" runat="server" Text="Ver Caso" CausesValidation="false"  OnClientClick="VerCaso();return false;" />

    <telerik:RadScriptBlock runat="server" id="RadScriptBlock2">
        <script type="text/javascript">
            var nombreRadAjaxManager = '<%= RadAjaxManager1.ClientID %>';
            //var idMenu = '<%= IdMenuEncriptado %>';
            var idMenu;
            var IdCaso = '<%= _IdCasoEncriptado %>';
            function VerCaso() {
                var wnd;
                wnd = window.parent.radopen("CasoDetalle.aspx?IdMenu=" + idMenu + "&id=" + IdCaso);
                wnd.set_modal(true);
                wnd.setSize(800, 800);
            }
//            $(document).ready(function () {
//                window.resizeTo($(window).width(), 450)
//            });
        </script>
    </telerik:RadScriptBlock>
    <telerik:RadWindow id="RadWindow2" runat="server" height="800px" Behaviors="Close"
            destroyonclose="True" title="Detalle Movimiento"   
                       width="800px" keepinscreenbounds="True" 
            NavigateUrl="MovimientoCasoLista.aspx">
    </telerik:RadWindow>
</asp:Content>