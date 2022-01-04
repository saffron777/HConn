
<%@ Page Title="Detalle de TipoPas" Language="C#" AutoEventWireup="true" MasterPageFile="~/Master/Site.Master" CodeBehind="TipoPasoDetalle.aspx.cs" Inherits="HConnexum.Tramitador.Sitio.Modulos.Parametrizador.TipoPasoDetalle" meta:resourcekey="PageResource1" %>
<%@ Register Src= "~/ControlesComunes/Publicacion.ascx" TagName="Publicacion" TagPrefix="hcc" %>
<%@ Register Src= "~/ControlesComunes/Auditoria.ascx" TagName="Auditoria" TagPrefix="hcc" %>
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
            <asp:Panel ID="PanelMaster" runat="server" meta:resourcekey="PanelMasterResource1">
                <fieldset>
                    <legend>
                        <asp:Label ID="lgLegendTipoPaso" runat="server" 
                                   Text="Tipo Paso" Font-Bold="True" meta:resourcekey="lgLegendTipoPasoResource1" 
                                   />
                    </legend>
                    <table>
                        <tr>
                            <td>
                                <asp:Label ID="lblDescripcion" runat="server" Text="Descripcion:" 
                                           meta:resourcekey="lblDescripcionResource1"/>
                            </td>
                            <td>
                                <asp:TextBox ID="txtDescripcion" runat="server" 
                                             meta:resourcekey="txtDescripcionResource1" 
                                    style="margin-right: 31px" Width="128px" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblPrograma" runat="server" Text="Programa:" 
                                           meta:resourcekey="lblProgramaResource1"/>
                            </td>
                            <td>
                                <asp:TextBox ID="txtPrograma" runat="server" 
                                             meta:resourcekey="txtProgramaResource1" Width="128px" />
                            </td>
                        </tr>
                    </table>
                </fieldset>
                <hcc:Publicacion id="Publicacion" runat="server"/>
                <hcc:Auditoria id="Auditoria" runat="server"/>
            </asp:Panel>
            <telerik:RadInputManager ID="RadInputManager1" runat="server">
                <telerik:TextBoxSetting   BehaviorID="BehaviorDescripcion" EmptyMessage="Escriba Descripcion"  Validation-IsRequired="True" ErrorMessage="Campo Obligatorio">
                    <TargetControls>
                        <telerik:TargetInput ControlID="TxtDescripcion"/>
                    </TargetControls>
                </telerik:TextBoxSetting>
               

            </telerik:RadInputManager>
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
</asp:Content>