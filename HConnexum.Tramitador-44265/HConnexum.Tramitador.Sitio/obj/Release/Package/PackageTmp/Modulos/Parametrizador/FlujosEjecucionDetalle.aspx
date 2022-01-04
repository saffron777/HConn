<%@ Page Title="Detalle de FlujosEjecucion" Language="C#" MasterPageFile="~/Master/Site.Master"  AutoEventWireup="true" CodeBehind="FlujosEjecucionDetalle.aspx.cs" Inherits="HConnexum.Tramitador.Sitio.Modulos.Parametrizador.FlujosEjecucionDetalle" meta:resourcekey="PageResource1" %>
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
                        <asp:Label ID="lgLegendFlujosEjecucion" runat="server" 
                                   Font-Bold="True" Text="Flujos Ejecucion" 
                            meta:resourcekey="lgLegendFlujosEjecucionResource1"  />
                    </legend>
                    <table>
                        <tr>
                            <td>
                                <asp:Label ID="lblIdPasoOrigen" runat="server" Text="IdPasoOrigen:" 
                                    meta:resourcekey="lblIdPasoOrigenResource1"/>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="ddlIdPasoOrigen"  DataValueField="IdPasoOrigen"  
                                    DataTextField="Nombre"  EmptyMessage="Seleccione" 
                                    ErrorMessage="Campo Obligatorio" runat="server" Culture="es-ES" 
                                    meta:resourcekey="ddlIdPasoOrigenResource1" />
                                <asp:RequiredFieldValidator ID="rfvIdPasoOrigen" runat="server" 
                                    ErrorMessage="*" ControlToValidate="ddlIdPasoOrigen"  CssClass="validator" 
                                    Width="25px" meta:resourcekey="rfvIdPasoOrigenResource1"/>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblIdPasoDestino" runat="server" Text="IdPasoDestino:" 
                                    meta:resourcekey="lblIdPasoDestinoResource1"/>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="ddlIdPasoDestino"  DataValueField="IdPasoDestino"  
                                    DataTextField="Nombre"  EmptyMessage="Seleccione" runat="server" 
                                    Culture="es-ES" meta:resourcekey="ddlIdPasoDestinoResource1" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblIdRespuesta" runat="server" Text="IdRespuesta:" 
                                    meta:resourcekey="lblIdRespuestaResource1"/>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="ddlIdRespuesta"  DataValueField="IdRespuesta"  
                                    DataTextField="Nombre"  EmptyMessage="Seleccione" 
                                    ErrorMessage="Campo Obligatorio" runat="server" Culture="es-ES" 
                                    meta:resourcekey="ddlIdRespuestaResource1" />
                                <asp:RequiredFieldValidator ID="rfvIdRespuesta" runat="server" ErrorMessage="*" 
                                    ControlToValidate="ddlIdRespuesta"  CssClass="validator" Width="25px" 
                                    meta:resourcekey="rfvIdRespuestaResource1"/>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblCondicion" runat="server" Text="Condicion:" 
                                    meta:resourcekey="lblCondicionResource1"/>
                            </td>
                            <td>
                                <asp:TextBox ID="txtCondicion" runat="server" 
                                    meta:resourcekey="txtCondicionResource1" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblTipoProcesoDestino" runat="server" Text="TipoProcesoDestino:" 
                                    meta:resourcekey="lblTipoProcesoDestinoResource1"/>
                            </td>
                            <td>
                                <asp:TextBox ID="txtTipoProcesoDestino" runat="server" 
                                    meta:resourcekey="txtTipoProcesoDestinoResource1" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblFechaModicacion" runat="server" Text="FechaModicacion:" 
                                    meta:resourcekey="lblFechaModicacionResource1"/>
                            </td>
                            <td>
                                <telerik:RadDatePicker ID="txtFechaModicacion" runat="server" 
                                    MinDate="1900-01-01" Culture="es-VE" 
                                    meta:resourcekey="txtFechaModicacionResource1" >
                                    <Calendar UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" 
                                        ViewSelectorText="x">
                                    </Calendar>
                                    <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" 
                                        EmptyMessage="DD/MM/YYYY" EnableSingleInputRendering="True" LabelWidth="64px">
                                    </DateInput>
                                    <DatePopupButton CssClass="" HoverImageUrl="" ImageUrl="" />
                                </telerik:RadDatePicker>
                                <Calendar>
                                    <SpecialDays>
                                        <telerik:RadCalendarDay Repeatable="Today">
                                            <ItemStyle Font-Bold="true" BorderColor="Red" />
                                        </telerik:RadCalendarDay>
                                    </SpecialDays>
                                </Calendar>
                            </td>
                        </tr>
                    </table>
                </fieldset>
                <hcc:Publicacion id="Publicacion" runat="server"/>
                <hcc:Auditoria id="Auditoria" runat="server"/>
            </asp:Panel>
            <telerik:RadInputManager ID="RadInputManager1" runat="server">
                <telerik:NumericTextBoxSetting DecimalDigits="0" GroupSeparator=""  BehaviorID="BehaviorIdPasoOrigen" EmptyMessage="Escriba IdPasoOrigen" Type="Number" Validation-IsRequired="True" ErrorMessage="Campo Obligatorio">
                    <TargetControls>
                        <telerik:TargetInput ControlID="TxtIdPasoOrigen"/>
                    </TargetControls>
                </telerik:NumericTextBoxSetting>
                <telerik:NumericTextBoxSetting DecimalDigits="0" GroupSeparator=""  BehaviorID="BehaviorIdPasoDestino" EmptyMessage="Escriba IdPasoDestino" Type="Number" Validation-IsRequired="False" ErrorMessage="Campo Obligatorio">
                    <TargetControls>
                        <telerik:TargetInput ControlID="TxtIdPasoDestino"/>
                    </TargetControls>
                </telerik:NumericTextBoxSetting>
                <telerik:NumericTextBoxSetting DecimalDigits="0" GroupSeparator=""  BehaviorID="BehaviorIdRespuesta" EmptyMessage="Escriba IdRespuesta" Type="Number" Validation-IsRequired="True" ErrorMessage="Campo Obligatorio">
                    <TargetControls>
                        <telerik:TargetInput ControlID="TxtIdRespuesta"/>
                    </TargetControls>
                </telerik:NumericTextBoxSetting>
                <telerik:NumericTextBoxSetting DecimalDigits="0" GroupSeparator=""  BehaviorID="BehaviorCondicion" EmptyMessage="Escriba Condicion" Type="Number" Validation-IsRequired="True" ErrorMessage="Campo Obligatorio">
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
                onclientclick="$('form').clearForm();return false" 
                meta:resourcekey="cmdLimpiarResource1"  />
        </div>
</asp:Content>