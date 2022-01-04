<%@ Page Title="Detalle de MensajesMetodosDestinatario" Language="C#" MasterPageFile="~/Master/Site.Master"  AutoEventWireup="True" CodeBehind="MensajesMetodosDestinatarioCorreoDetalle.aspx.cs" Inherits="HConnexum.Tramitador.Sitio.Modulos.Parametrizador.MensajesMetodosDestinatarioCorreoDetalle" meta:resourcekey="PageResource1" %>
<%@ Register Src="~/ControlesComunes/Publicacion.ascx" TagName="Publicacion" TagPrefix="hcc" %>
<%@ Register Src="~/ControlesComunes/MultilineCounter.ascx" TagName="MultilineCounter" TagPrefix="hcc" %>
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
            <asp:UpdatePanel ID="Idupdatepanel" runat="server">
                <ContentTemplate>
                    <fieldset>
                        <legend>
                            <asp:Label ID="lgLegendMensajesMetodosDestinatario" runat="server" 
                                       Font-Bold="True" 
                                       meta:resourcekey="lgLegendMensajesMetodosDestinatarioResource1" 
                                       Text="Mensajes Metodos Destinatario"></asp:Label>
                        </legend>
                        <table>
                            <tr>
                                <td>
                                    <asp:Label ID="lblIdPaso" runat="server" meta:resourcekey="lblIdPasoResource1" 
                                               Text="Paso:"></asp:Label>
                                </td>
                                <td class="style1">
                                    <telerik:RadComboBox ID="ddlIdPaso" runat="server" Culture="es-ES" Width="250"
                                                         DataTextField="Nombre" DataValueField="Id" Enabled="False" 
                                                         meta:resourcekey="ddlIdPasoResource1">
                                    </telerik:RadComboBox>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblIdMensaje" runat="server" 
                                               meta:resourcekey="lblIdMensajeResource1" Text="Mensaje:"></asp:Label>
                                </td>
                                <td class="style1">
                                    <telerik:RadComboBox ID="ddlIdMensaje" runat="server" Culture="es-ES" Width="250"
                                                         DataTextField="Mensaje" DataValueField="Id" EmptyMessage="Seleccione" 
                                                         meta:resourcekey="ddlIdMensajeResource1">
                                    </telerik:RadComboBox>
                                    <asp:RequiredFieldValidator ID="rfvIdMensaje" runat="server" 
                                                                ControlToValidate="ddlIdMensaje" CssClass="validator" ErrorMessage="*" 
                                                                meta:resourcekey="rfvIdMensajeResource1" Width="25px"></asp:RequiredFieldValidator>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" rowspan="3">
                                    <asp:Label ID="lblTipoBusquedaDestinatario" runat="server" 
                                               meta:resourcekey="lblTipoBusquedaDestinatarioResource1" 
                                               Text="Tipo Busqueda Destinatario:"></asp:Label>
                                    <telerik:RadListBox ID="rlbListaCasosMovimientos" runat="server" 
                                                        CssClass="Item" Culture="es-ES" DataTextField="Nombre" DataValueField="Id" 
                                                        Height="200px" meta:resourceKey="rlbListaRolesResource1" 
                                                        SelectionMode="Multiple" Width="220px">
                                        <ButtonSettings TransferButtons="All" />
                                    </telerik:RadListBox>
                                </td>
                                <td>
                                    <asp:Button ID="cmdPara" runat="server" meta:resourcekey="cmdParaResource1" 
                                                OnClick="cmdPara_Click" Text="Para:" />
                                </td>
                                <td>
                                    <hcc:MultilineCounter ID="MultilineCounterPara" runat="server" MaxLength="5000" 
                                                          Width="250" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Button ID="cmdCC" runat="server" meta:resourcekey="cmdCCResource1" 
                                                OnClick="cmdCC_Click1" Text="CC:" />
                                </td>
                                <td>
                                    <hcc:MultilineCounter ID="MultilineCounterCC" runat="server" MaxLength="5000" 
                                                          Width="250" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Button ID="cmdCCO" runat="server" meta:resourcekey="cmdCCOResource1" 
                                                OnClick="cmdCCO_Click" Text="CCO:" />
                                </td>
                                <td>
                                    <hcc:MultilineCounter ID="MultilineCounterCCO" runat="server" MaxLength="5000" 
                                                          Width="250" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblValorBusqueda" runat="server" 
                                               meta:resourcekey="lblValorBusquedaResource1" Text="Rutina:"></asp:Label>
                                </td>
                                <td class="style1">
                                    <asp:TextBox ID="txtRutina" runat="server" Width="250"
                                                 meta:resourcekey="txtRutinaResource1"></asp:TextBox>
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label7" runat="server" meta:resourcekey="Label7Resource1" 
                                               Text="Contantes (Emails):"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="Label1" runat="server" meta:resourcekey="Label1Resource1" 
                                               Text="Para:"></asp:Label>
                                </td>
                                <td>
                                    <hcc:MultilineCounter ID="MultilineCounterPara1" runat="server" 
                                                          MaxLength="5000" Width="250" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                    <asp:Label ID="Label2" runat="server" meta:resourcekey="Label2Resource1" 
                                               Text="CC:"></asp:Label>
                                </td>
                                <td>
                                    <hcc:MultilineCounter ID="MultilineCounterCC1" runat="server" MaxLength="5000" 
                                                          Width="250" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                    <asp:Label ID="Label3" runat="server" meta:resourcekey="Label3Resource1" 
                                               Text="CCO:"></asp:Label>
                                </td>
                                <td>
                                    <hcc:MultilineCounter ID="MultilineCounterCCO1" runat="server" MaxLength="5000" 
                                                          Width="250" />
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                    <hcc:Publicacion ID="Publicacion" runat="server" />
                    <hcc:Auditoria ID="Auditoria" runat="server" />
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger 
                        ControlID="cmdPara" EventName="Click"/>
                </Triggers>
            </asp:UpdatePanel>
        </asp:Panel>
        <telerik:RadInputManager ID="RadInputManager1" runat="server">
            <telerik:NumericTextBoxSetting DecimalDigits="0" GroupSeparator=""  BehaviorID="BehaviorIdPaso" EmptyMessage="Escriba IdPaso" Type="Number" Validation-IsRequired="False" ErrorMessage="Campo Obligatorio">
                <TargetControls>
                    <telerik:TargetInput ControlID="TxtIdPaso"/>
                </TargetControls>
            </telerik:NumericTextBoxSetting>
            <telerik:NumericTextBoxSetting DecimalDigits="0" GroupSeparator=""  BehaviorID="BehaviorIdMetodo" EmptyMessage="Escriba IdMetodo" Type="Number" Validation-IsRequired="False" ErrorMessage="Campo Obligatorio">
                <TargetControls>
                    <telerik:TargetInput ControlID="TxtIdMetodo"/>
                </TargetControls>
            </telerik:NumericTextBoxSetting>
            <telerik:NumericTextBoxSetting DecimalDigits="0" GroupSeparator=""  BehaviorID="BehaviorIdMensaje" EmptyMessage="Escriba IdMensaje" Type="Number" Validation-IsRequired="True" ErrorMessage="Campo Obligatorio">
                <TargetControls>
                    <telerik:TargetInput ControlID="TxtIdMensaje"/>
                </TargetControls>
            </telerik:NumericTextBoxSetting>
            <telerik:NumericTextBoxSetting DecimalDigits="0" GroupSeparator=""  BehaviorID="BehaviorTipoBusquedaDestinatario" EmptyMessage="Escriba TipoBusquedaDestinatario" Type="Number" Validation-IsRequired="False" ErrorMessage="Campo Obligatorio">
                <TargetControls>
                    <telerik:TargetInput ControlID="TxtTipoBusquedaDestinatario"/>
                </TargetControls>
            </telerik:NumericTextBoxSetting>
            <telerik:TextBoxSetting   BehaviorID="BehaviorValorBusqueda" EmptyMessage="Escriba ValorBusqueda"  Validation-IsRequired="True" ErrorMessage="Campo Obligatorio">
                <TargetControls>
                    <telerik:TargetInput ControlID="TxtValorBusqueda"/>
                </TargetControls>
            </telerik:TextBoxSetting>
            <telerik:NumericTextBoxSetting DecimalDigits="0" GroupSeparator=""  BehaviorID="BehaviorTipoPrivacidad" EmptyMessage="Escriba TipoPrivacidad" Type="Number" Validation-IsRequired="False" ErrorMessage="Campo Obligatorio">
                <TargetControls>
                    <telerik:TargetInput ControlID="TxtTipoPrivacidad"/>
                </TargetControls>
            </telerik:NumericTextBoxSetting>
        </telerik:RadInputManager>
        <br />
        <br />
        <asp:Button ID="cmdGuardar" runat="server" Text="Guardar" 
                    onclick="cmdGuardar_Click" meta:resourcekey="cmdGuardarResource1"  />
        <asp:Button ID="cmdGuardaryAgregar" runat="server" 
                    Text="Guardar y Agregar Otro" onclick="cmdGuardaryAgregar_Click" 
                    Visible="False" meta:resourcekey="cmdGuardaryAgregarResource1"/>
        <asp:Button ID="cmdLimpiar" runat="server" Text="Limpiar"  
                    onclientclick="$('form').clearForm();return false" Visible="False" 
                    meta:resourcekey="cmdLimpiarResource1"  />
    </div>
    <script type="text/javascript">
        $("#" + '<%=  MultilineCounterPara.FindControl("txtControl").ClientID %>').live("keypress", function (event) {

            if (event.which != 8) {

                event.preventDefault();
            }
        });
        $("#" + '<%=  MultilineCounterCC.FindControl("txtControl").ClientID %>').live("keypress", function (event) {

            if (event.which != 8) {

                event.preventDefault();
            }
        });
        $("#" + '<%=  MultilineCounterCCO.FindControl("txtControl").ClientID %>').live("keypress", function (event) {

            if (event.which != 8) {

                event.preventDefault();
            }
        });

        function IrAnterior() {
            var wnd = GetRadWindow();
            wnd.setUrl('<%= RutaPadreEncriptada %>');
        }
    </script>
</asp:Content>