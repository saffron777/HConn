<%@ Page Title="Detalle de Bloque" MasterPageFile="~/Master/Site.Master" Language="C#" AutoEventWireup="true" CodeBehind="BloqueDetalle.aspx.cs" Inherits="HConnexum.Tramitador.Sitio.Modulos.Parametrizador.BloqueDetalle" meta:resourcekey="PageResource1" %>
<%@ Register Src= "~/ControlesComunes/Publicacion.ascx" TagName="Publicacion" TagPrefix="hcc" %>
<%@ Register Src= "~/ControlesComunes/Auditoria.ascx" TagName="Auditoria" TagPrefix="hcc" %>
<asp:Content ID="cHead" ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>
<asp:Content ID="cBody" ContentPlaceHolderID="cphBody" runat="server">

    <telerik:RadScriptBlock runat="server" id="RadScriptBlock2">
        <script type="text/javascript">
            $(window).load(function () {
                $('#' + '<%=txtNombre.ClientID%>').focus();
            }); 
            </script>
    </telerik:RadScriptBlock>
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
                        <asp:Label ID="lblBloque" runat="server" Text="Bloque" Font-Bold="True" 
                                   meta:resourcekey="lblBloqueResource1"></asp:Label>
                    </legend>
                    <table>
                        <tr>
                            <td>
                                <asp:Label ID="lblNombre" runat="server" Text="Nombre:" 
                                           meta:resourcekey="lblNombreResource1"/>
                            </td>
                            <td>
                                <asp:TextBox ID="txtNombre" runat="server" 
                                             meta:resourcekey="txtNombreResource1" Width="156px" />
                            </td>
                        </tr>
                        <tr>
                        <td>
								<asp:RadioButtonList ID="RB_TipoBloque" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="CambioTipoBloque" AutoPostBack="True" meta:resourcekey="RB_OrigenValorResource1">
									<asp:ListItem Text="Libre" Value="L" Selected="True">
                                    </asp:ListItem><asp:ListItem Text="Dinamico" Value="D" ></asp:ListItem>
								</asp:RadioButtonList>
							</td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblNombrePrograma" runat="server" Text="Nombre Programa:" 
                                           meta:resourcekey="lblNombreProgramaResource1"/>
                            </td>
                            <td>
                                <asp:TextBox ID="txtNombrePrograma" runat="server" 
                                             meta:resourcekey="txtNombreProgramaResource1" TabIndex="1" 
                                    Width="156px" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblIdListaValor" runat="server" Text="Lista Valor:" 
                                           meta:resourcekey="lblIdListaValorResource1"/>
                            </td>
                            <td>				
                                <telerik:RadComboBox ID="ddlIdListaValor"  DataValueField="Id"  
                                                     DataTextField="Descripcion"  EmptyMessage="Seleccione" 
                                                     ErrorMessage="Campo Obligatorio" runat="server" 
                                                     Culture="es-ES" meta:resourcekey="ddlIdListaValorResource1" TabIndex="2">
                                </telerik:RadComboBox>                                
                                <asp:RequiredFieldValidator ID="rfvIdListaValor" runat="server" 
                                                            ErrorMessage="*" ControlToValidate="ddlIdListaValor"  CssClass="validator" 
                                                            Width="25px" meta:resourcekey="rfvIdListaValorResource1"/>                                
                            </td>
                        </tr>
                    </table>
                </fieldset>
                <hcc:Publicacion id="Publicacion" runat="server"/>
                <hcc:Auditoria id="Auditoria" runat="server"/>
            </asp:Panel>
            <telerik:RadInputManager ID="RadInputManager1" runat="server">
                <telerik:TextBoxSetting   BehaviorID="BehaviorNombre" EmptyMessage="Escriba Nombre"  Validation-IsRequired="True" ErrorMessage="Campo Obligatorio">
                    <TargetControls>
                        <telerik:TargetInput ControlID="TxtNombre"/>
                    </TargetControls>
                </telerik:TextBoxSetting>
                <telerik:TextBoxSetting   BehaviorID="BehaviorNombrePrograma" EmptyMessage="Escriba Nombre del Programa"  Validation-IsRequired="True" ErrorMessage="Campo Obligatorio">
                    <TargetControls>
                        <telerik:TargetInput ControlID="txtNombrePrograma"/>
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