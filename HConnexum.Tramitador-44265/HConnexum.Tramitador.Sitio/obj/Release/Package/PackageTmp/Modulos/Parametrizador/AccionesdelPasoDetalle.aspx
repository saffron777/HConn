<%@ Page Title="Acciones del Paso" MasterPageFile="~/Master/Site.Master" Language="C#" AutoEventWireup="true" CodeBehind="AccionesdelPasoDetalle.aspx.cs" Inherits="HConnexum.Tramitador.Sitio.Modulos.Parametrizador.AccionesdelPasoDetalle" meta:resourcekey="PageResource1" %>
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
                    <asp:Label ID="lblAccionesPaso" runat="server" Text="Acciones del Paso" 
                               Font-Bold="True" meta:resourcekey="lblAccionesPasoResource1"></asp:Label>
                </legend>
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="lblEtapa" runat="server" Text="Etapa:" 
                                       meta:resourcekey="lblEtapaResource1"/>
                        </td>
                        <td>
                            <telerik:RadComboBox ID="ddlIdEtapa"  DataValueField="Id" 
                                                 DataTextField="Nombre"  EmptyMessage="Seleccione" 
                                                 ErrorMessage="Campo Obligatorio"   runat="server" 
                                                 AutoPostBack = "True" 
                                                 OnSelectedIndexChanged="ddlIdEtapa_SelectedIndexChanged" 
                                                 Culture="es-ES" meta:resourcekey="ddlIdEtapaResource1" 
                                                 CausesValidation = "False"/>
<%--                            <asp:RequiredFieldValidator ID="rfvIdEtapa" runat="server" ErrorMessage="*" 
                                                        ControlToValidate="ddlIdEtapa"  CssClass="validator" Width="25px" 
                                                        meta:resourcekey="rfvIdEtapaResource1"/>--%>
                        </td>
                        <td>
                            <asp:Label ID="lblIdPasoOrigen" runat="server" Text="Paso Origen:" 
                                       meta:resourcekey="lblIdPasoOrigenResource1"/>
                        </td>
                        <td>
                            <telerik:RadComboBox ID="ddlIdPasoOrigen"  DataValueField="Id"  
                                                 DataTextField="Nombre"  EmptyMessage="Seleccione" 
                                                 ErrorMessage="Campo Obligatorio"  runat="server" 
                                                 AutoPostBack = "True" 
                                                         
                                                 OnSelectedIndexChanged="ddlIdPasoOrigen_SelectedIndexChanged" Culture="es-ES" 
                                                 meta:resourcekey="ddlIdPasoOrigenResource1" CausesValidation = "False"/>
<%--                            <asp:RequiredFieldValidator ID="rfvIdPasoOrigen" runat="server" 
                                                        ErrorMessage="*" ControlToValidate="ddlIdPasoOrigen"  CssClass="validator" 
                                                        Width="25px" meta:resourcekey="rfvIdPasoOrigenResource1"/> --%>
                        </td>
                        <td>
                            <asp:Label ID="lblRespuesta" runat="server" Text="Respuesta:" 
                                       meta:resourcekey="lblRespuestaResource1"/>
                        </td>
                        <td>
                            <telerik:RadComboBox ID="ddlIdPasoRespuesta"  DataValueField="Id"  
                                                 DataTextField="ValorRespuesta"  EmptyMessage="Seleccione" ErrorMessage="Campo Obligatorio" runat="server" 
                                                 Culture="es-ES" meta:resourcekey="ddlIdRespuestaResource1" />
<%--                            <asp:RequiredFieldValidator ID="rfvIdRespuesta" runat="server" ErrorMessage="*" 
                                                        ControlToValidate="ddlIdPasoRespuesta"  CssClass="validator" Width="25px" 
                                                        meta:resourcekey="rfvIdRespuestaResource1"/>--%>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblTipoPaso" runat="server" Text="Tipo Próximo Paso:" 
                                       meta:resourcekey="lblTipoPasoResource1"/>
                        </td>
                        <td>
                            <telerik:RadComboBox ID="ddlIdTipoPaso"  DataValueField="Id"  
                                                 DataTextField="Descripcion"  EmptyMessage="Seleccione" 
                                                 ErrorMessage="Campo Obligatorio" runat="server"
                                                 AutoPostBack = "True" 
                                                         
                                                 OnSelectedIndexChanged="ddlIdTipoPaso_SelectedIndexChanged" Culture="es-ES" 
                                                 meta:resourcekey="ddlIdTipoPasoResource1" CausesValidation = "False"/>
<%--                            <asp:RequiredFieldValidator ID="rfvIdTipoPaso" runat="server" ErrorMessage="*" 
                                 ControlToValidate="ddlIdTipoPaso"  CssClass="validator" Width="25px" meta:resourcekey="rfvIdTipoPasoResource1"/>--%>
                        </td>
                        <td>
                            <asp:Label ID="lblIdPasoDestino" runat="server" Text="Próximo Paso:" 
                                       meta:resourcekey="lblIdPasoDestinoResource1"/>
                        </td>
                        <td>
                            <telerik:RadComboBox ID="ddlIdPasoDestino"  DataValueField="Id"  
                                                 DataTextField="Nombre"  EmptyMessage="Seleccione" 
                                                 ErrorMessage="Campo Obligatorio" runat="server" 
                                                 Culture="es-ES" 
                                                 meta:resourcekey="ddlIdPasoDestinoResource1" 
                                onselectedindexchanged="ddlIdPasoDestino_SelectedIndexChanged" 
                                AutoPostBack="True" CausesValidation="False" />
<%--                        <asp:RequiredFieldValidator ID="rfvIdPasoDestino" runat="server" ErrorMessage="*" 
                            ControlToValidate="ddlIdPasoDestino"  CssClass="validator" Width="25px"/>--%>
                        </td>
                            
                        <td>
                            <asp:Label ID="lblIndReinicioRepeticion" runat="server" Text="Reinicio Repeticion:" />
                        </td>
                        <td>
                            <asp:CheckBox ID="chkIndReinicioRepeticion" runat="server"/>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblCondición" runat="server" Text="Condición:" 
                                       meta:resourcekey="lblCondiciónResource1"/>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCondicion" runat="server" 
                                         meta:resourcekey="txtCondicionResource1"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Label ID="lblIdPasoDesborde" runat="server" Text="Paso Desborde:" />
                        </td>
                        <td>
                            <telerik:RadComboBox ID="ddlIdPasoDesborde" runat="server" Culture="es-ES" 
                                                 DataTextField="Nombre" DataValueField="Id" 
                                                 EmptyMessage="Seleccione" 
                                ErrorMessage="Campo Obligatorio"/>
                        </td>
                    </tr>
                </table>
            </fieldset>
            <hcc:Publicacion id="Publicacion" runat="server"/>
            <hcc:Auditoria id="Auditoria" runat="server"/>
        </asp:Panel>
        <telerik:RadInputManager ID="RadInputManager1" runat="server">
            <telerik:NumericTextBoxSetting DecimalDigits="0" GroupSeparator=""  BehaviorID="BehaviorCondicion" EmptyMessage="Escriba Condicion" Type="Number" Validation-IsRequired="False" ErrorMessage="Campo Obligatorio">
                <TargetControls>
                    <telerik:TargetInput ControlID="TxtCondicion"/>
                </TargetControls>
            </telerik:NumericTextBoxSetting>
            <telerik:NumericTextBoxSetting DecimalDigits="0" GroupSeparator=""  BehaviorID="BehaviorTipoProcesoDestino" EmptyMessage="Escriba TipoProcesoDestino" Type="Number" Validation-IsRequired="True" ErrorMessage="Campo Obligatorio">
                <TargetControls>
                    <telerik:TargetInput ControlID="TxtTipoProcesoDestino"/>
                </TargetControls>
            </telerik:NumericTextBoxSetting>
        </telerik:RadInputManager>
        <br />
        <br />
        <asp:Button ID="cmdGuardar" runat="server" Text="Guardar" 
                    onclick="cmdGuardar_Click" meta:resourcekey="cmdGuardarResource1"  />
        <asp:Button ID="cmdGuardaryAgregar" runat="server" 
                    Text="Guardar y Agregar Otro" onclick="cmdGuardaryAgregar_Click" 
                    meta:resourcekey="cmdGuardaryAgregarResource1"  />
        <asp:Button ID="cmdLimpiar" runat="server" Text="Limpiar"  
                    onclientclick="$('form').clearForm();

                    return false" 
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