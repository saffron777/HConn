<%@ Page Title="Configuración de Paso" MasterPageFile="~/Master/Site.Master" Language="C#" AutoEventWireup="true" CodeBehind="ConfiguracionPaso.aspx.cs" Inherits="HConnexum.Tramitador.Sitio.Modulos.Parametrizador.ConfiguracionPaso" %>
<asp:Content ID="cHead" ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>
<asp:Content ID="cBody" ContentPlaceHolderID="cphBody" runat="server">
    <telerik:RadStyleSheetManager ID="RadStyleSheetManager1" runat="server">
    </telerik:RadStyleSheetManager>
    <div >
        <telerik:RadAjaxManager runat="server" ID="RadAjaxManager1" >
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="DeleteButton">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="tvConfiguracion" LoadingPanelID="RadAjaxLoadingPanel1"/>
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:RadAjaxManager>
        <Table align="center">
            <tr>  
                <td>
                    <asp:Panel ID="Panel1" runat="server" Direction="NotSet" >
                        <table>
                            <tr>
                                <td>
                                    <asp:Label ID="lblTipo" runat="server" Text="Tipo:"></asp:Label>
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="ddlTipo"  DataValueField="Id"  DataTextField="Nombre"  
                                                         EmptyMessage="Seleccione" 
                                                         ErrorMessage="Campo Obligatorio" runat="server" 
                                                         Culture="es-ES" 
                                                         onselectedindexchanged="ddlTipo_SelectedIndexChanged" 
                                                         AutoPostBack="True" CausesValidation="False" ValidationGroup="Nodo"/>
                                    <asp:RequiredFieldValidator ID="rfvTipo" runat="server" 
                                                                ErrorMessage="*" InitialValue="" ControlToValidate="ddlTipo"  
                                                                CssClass="validator" Width="25" ValidationGroup="Nodo"/>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblNombre" runat="server" Text="Nombre:"></asp:Label>
                                </td>
                                <td>
                                    <telerik:RadTextBox runat="Server" ID="txtNombre" ValidationGroup="Nodo"></telerik:RadTextBox>
                                    <asp:RequiredFieldValidator ID="rfvNombre" runat="server" 
                                                                ErrorMessage="*" InitialValue="" ControlToValidate="txtNombre"  
                                                                CssClass="validator" Width="25" ValidationGroup="Nodo"/>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblAmbito" runat="server" Text="Ambito:"></asp:Label>
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="ddlAmbito"  DataValueField="Id"  DataTextField="NombreValor"  
                                                         EmptyMessage="Seleccione" ErrorMessage="Campo Obligatorio" runat="server" 
                                                         Culture="es-ES" ValidationGroup="Nodo" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblAcumulado" runat="server" Text="Acumulado:"></asp:Label>
                                </td>
                                <td>
                                    <asp:CheckBox ID="chkAcumulado" runat="server" ValidationGroup="Nodo" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblOrigen" runat="server" Text="Origen:"></asp:Label>
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="ddlOrigen"  DataValueField="Id"  DataTextField="Nombre"  
                                                         EmptyMessage="Seleccione" ErrorMessage="Campo Obligatorio" runat="server" 
                                                         Culture="es-ES" ValidationGroup="Nodo" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblDestino" runat="server" Text="Destino:"></asp:Label>
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="ddlDestino"  DataValueField="Id"  DataTextField="Nombre"  
                                                         EmptyMessage="Seleccione" ErrorMessage="Campo Obligatorio" runat="server" 
                                                         Culture="es-ES" ValidationGroup="Nodo"/>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Button ID="AddButton" runat="server" 
                                                Text="Nuevo " 
                                                onclick="AddButton_Click" ValidationGroup="Nodo"></asp:Button>
                                </td>
                                <td>
                                    <asp:Button ID="btnEliminar" runat="server"
                                                ValidationGroup="DeleteRequiresSelection" Text="Eliminar" 
                                                onclick="DeleteButton_Click"></asp:Button>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Seleccione el nivel a gestionar."
                                                                Display="Dynamic" ControlToValidate="tvConfiguracion" ValidationGroup="DeleteRequiresSelection"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                        </table>     
                    </asp:Panel>
                </td>
                <td>
                    &nbsp;
                    &nbsp;
                    &nbsp;
                    &nbsp;
                    &nbsp;
                    &nbsp;
                    &nbsp;
                    &nbsp;
                    &nbsp;
                    &nbsp;
                    &nbsp;
                    &nbsp;
                    &nbsp;
                    &nbsp;
                    &nbsp;
                    &nbsp;
                    &nbsp;
                    &nbsp;
                    &nbsp; 
                    &nbsp;
                </td>
                <td rowspan="2" align="left">
                    <asp:Panel ID="Panel2" runat="server" ScrollBars="Auto" BorderColor="Black" 
                               BorderStyle="Double">
                        <telerik:RadTreeView ID="tvConfiguracion" runat="server" Height="380px" Width="300px" OnNodeClick="TreeView_Click"  ValidationGroup="Tree" MultipleSelect ="false"  
                                             >
                        </telerik:RadTreeView>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td></td>
            </tr>
            <tr>
                <asp:XmlDataSource ID="XmlDataSource1" runat="server" 
                                   DataFile="~/Modulos/Parametrizador/prueba.xml" XPath="PARAMETRO"></asp:XmlDataSource>
            </tr>
        </Table>
        <telerik:RadInputManager ID="RadInputManager1" runat="server">
            <%--<telerik:NumericTextBoxSetting DecimalDigits="0" GroupSeparator=""  BehaviorID="BehaviorNombre" EmptyMessage="Escriba Nombre" Type="Number" Validation-IsRequired="False" ErrorMessage="Campo Obligatorio">
            <TargetControls>
            <telerik:TargetInput ControlID="txtNombre"/>
            </TargetControls>
            </telerik:NumericTextBoxSetting>--%>
        </telerik:RadInputManager>
        <br />
        <asp:Button ID="cmdGuardar" runat="server" Text="Guardar" 
                    onclick="cmdGuardar_Click" ValidationGroup="Tree"/>
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