
<%@ Page Title="Detalle de ServicioSucursal" Language="C#" MasterPageFile="~/Master/Site.Master" AutoEventWireup="true" CodeBehind="ServicioSucursalDetalle.aspx.cs" Inherits="HConnexum.Tramitador.Sitio.Modulos.Parametrizador.ServicioSucursalDetalle" meta:resourcekey="PageResource1" %>
<%@ Register Src="~/ControlesComunes/Publicacion.ascx" TagName="Publicacion" TagPrefix="hcc" %>
<%@ Register Src="~/ControlesComunes/Auditoria.ascx" TagName="Auditoria" TagPrefix="hcc" %>
<asp:Content ID="cHead" ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>
<asp:Content ID="cBody" ContentPlaceHolderID="cphBody" runat="server">
        <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" 
            DefaultLoadingPanelID="RadAjaxLoadingPanel1" 
            meta:resourcekey="RadAjaxManager1Resource1">
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="RadGridDetails">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="PanelMaster" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:RadAjaxManager>
        <div>
            <asp:Panel ID="PanelMaster" runat="server" 
                meta:resourcekey="PanelMasterResource1">
                <fieldset>
                    <legend>
                        <asp:Label ID="lgLegendServicioSucursal" runat="server" 
                                        Font-Bold="True" Text="Servicio Sucursal" 
                            meta:resourcekey="lgLegendServicioSucursalResource1"></asp:Label>
                    </legend>
                    <table>
                        <tr>
                            <td>
                                <asp:Label ID="lblIdFlujoServicio" runat="server" Text="Servicio:" 
                                    meta:resourcekey="lblIdFlujoServicioResource1"/>
                            </td>
                            <td>
                                <asp:TextBox ID="txtIdFlujoServicio" runat="server" 
                                    meta:resourcekey="txtIdFlujoServicioResource1" Enabled="False" />

                            </td>
                            <td>
                                <asp:Label ID="lblIdSucursal" runat="server" Text="Sucursal:" 
                                    meta:resourcekey="lblIdSucursalResource1"/>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="ddlSucursal" DataTextField="Nombre" 
                                    DataValueField="Id" EmptyMessage="Seleccione..." runat="server" Culture="es-ES" 
                                    meta:resourcekey="ddlSucursalResource1"/>
                            </td>
                        </tr>
                    </table>
                </fieldset>
                <hcc:Publicacion id="Publicacion" runat="server"/>
                <hcc:Auditoria id="Auditoria" runat="server"/>
            </asp:Panel>

            <br />
            <br />
            <asp:Button ID="cmdGuardar" runat="server" Text="Guardar" 
                onclick="cmdGuardar_Click" meta:resourcekey="cmdGuardarResource1"  />
            <asp:Button ID="cmdGuardaryAgregar" runat="server" 
                Text="Guardar y Agregar Otro" onclick="cmdGuardaryAgregar_Click" 
                meta:resourcekey="cmdGuardaryAgregarResource1"  />
            <asp:Button ID="cmdLimpiar" runat="server" Text="Limpiar"  
                onclientclick="$('form').clearForm();return false" 
                meta:resourcekey="cmdLimpiarResource1"  />
        </div>
    <telerik:RadScriptBlock runat="server" id="RadScriptBlock1">
        <script type="text/javascript">


            function IrAnterior() {
                var wnd = GetRadWindow();
                wnd.setUrl('<%= RutaPadreEncriptada %>');
            }
        </script>
    </telerik:RadScriptBlock>
</asp:Content>