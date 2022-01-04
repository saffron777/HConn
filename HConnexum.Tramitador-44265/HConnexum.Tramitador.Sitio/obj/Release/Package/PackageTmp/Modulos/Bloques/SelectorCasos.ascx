<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SelectorCasos.ascx.cs"
    Inherits="HConnexum.Tramitador.Sitio.Modulos.Bloques.SelectorCasos" %>
<style type="text/css">
    .RadComboBox_Default
    {
        font: 12px "Segoe UI" ,Arial,sans-serif;
        color: #333;
    }
    .RadComboBox
    {
        vertical-align: middle;
        display: -moz-inline-stack;
        display: inline-block;
    }
    .RadComboBox
    {
        text-align: left;
    }
    .RadComboBox_Default
    {
        font: 12px "Segoe UI" ,Arial,sans-serif;
        color: #333;
    }
    .RadComboBox
    {
        vertical-align: middle;
        display: -moz-inline-stack;
        display: inline-block;
    }
    .RadComboBox
    {
        text-align: left;
    }
    .RadComboBox_Default
    {
        font: 12px "Segoe UI" ,Arial,sans-serif;
        color: #333;
    }
    .RadComboBox
    {
        vertical-align: middle;
        display: -moz-inline-stack;
        display: inline-block;
    }
    .RadComboBox
    {
        text-align: left;
    }
    .RadComboBox_Default
    {
        font: 12px "Segoe UI" ,Arial,sans-serif;
        color: #333;
    }
    .RadComboBox
    {
        vertical-align: middle;
        display: -moz-inline-stack;
        display: inline-block;
    }
    .RadComboBox
    {
        text-align: left;
    }
    .RadComboBox_Default
    {
        font: 12px "Segoe UI" ,Arial,sans-serif;
        color: #333;
    }
    .RadComboBox
    {
        vertical-align: middle;
        display: -moz-inline-stack;
        display: inline-block;
    }
    .RadComboBox
    {
        text-align: left;
    }
    .RadComboBox *
    {
        margin: 0;
        padding: 0;
    }
    .RadComboBox *
    {
        margin: 0;
        padding: 0;
    }
    .RadComboBox *
    {
        margin: 0;
        padding: 0;
    }
    .RadComboBox *
    {
        margin: 0;
        padding: 0;
    }
    .RadComboBox *
    {
        margin: 0;
        padding: 0;
    }
    .RadComboBox_Default .rcbInputCellLeft
    {
        background-image: url('mvwres://Telerik.Web.UI, Version=2012.1.215.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.ComboBox.rcbSprite.png');
    }
    .RadComboBox .rcbInputCellLeft
    {
        background-color: transparent;
        background-repeat: no-repeat;
    }
    .RadComboBox_Default .rcbInputCellLeft
    {
        background-image: url('mvwres://Telerik.Web.UI, Version=2012.1.215.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.ComboBox.rcbSprite.png');
    }
    .RadComboBox .rcbInputCellLeft
    {
        background-color: transparent;
        background-repeat: no-repeat;
    }
    .RadComboBox_Default .rcbInputCellLeft
    {
        background-image: url('mvwres://Telerik.Web.UI, Version=2012.1.215.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.ComboBox.rcbSprite.png');
    }
    .RadComboBox .rcbInputCellLeft
    {
        background-color: transparent;
        background-repeat: no-repeat;
    }
    .RadComboBox_Default .rcbInputCellLeft
    {
        background-image: url('mvwres://Telerik.Web.UI, Version=2012.1.215.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.ComboBox.rcbSprite.png');
    }
    .RadComboBox .rcbInputCellLeft
    {
        background-color: transparent;
        background-repeat: no-repeat;
    }
    .RadComboBox_Default .rcbInputCellLeft
    {
        background-image: url('mvwres://Telerik.Web.UI, Version=2012.1.215.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.ComboBox.rcbSprite.png');
    }
    .RadComboBox .rcbInputCellLeft
    {
        background-color: transparent;
        background-repeat: no-repeat;
    }
    .RadComboBox .rcbReadOnly .rcbInput
    {
        cursor: default;
    }
    .RadComboBox .rcbReadOnly .rcbInput
    {
        cursor: default;
    }
    .RadComboBox .rcbReadOnly .rcbInput
    {
        cursor: default;
    }
    .RadComboBox .rcbReadOnly .rcbInput
    {
        cursor: default;
    }
    .RadComboBox .rcbReadOnly .rcbInput
    {
        cursor: default;
    }
    .RadComboBox_Default .rcbInput
    {
        font: 12px "Segoe UI" ,Arial,sans-serif;
        color: #333;
    }
    .RadComboBox .rcbInput
    {
        text-align: left;
    }
    .RadComboBox_Default .rcbInput
    {
        font: 12px "Segoe UI" ,Arial,sans-serif;
        color: #333;
    }
    .RadComboBox .rcbInput
    {
        text-align: left;
    }
    .RadComboBox_Default .rcbInput
    {
        font: 12px "Segoe UI" ,Arial,sans-serif;
        color: #333;
    }
    .RadComboBox .rcbInput
    {
        text-align: left;
    }
    .RadComboBox_Default .rcbInput
    {
        font: 12px "Segoe UI" ,Arial,sans-serif;
        color: #333;
    }
    .RadComboBox .rcbInput
    {
        text-align: left;
    }
    .RadComboBox_Default .rcbInput
    {
        font: 12px "Segoe UI" ,Arial,sans-serif;
        color: #333;
    }
    .RadComboBox .rcbInput
    {
        text-align: left;
    }
    .RadComboBox_Default .rcbArrowCellRight
    {
        background-image: url('mvwres://Telerik.Web.UI, Version=2012.1.215.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.ComboBox.rcbSprite.png');
    }
    .RadComboBox .rcbArrowCellRight
    {
        background-color: transparent;
        background-repeat: no-repeat;
    }
    .RadComboBox_Default .rcbArrowCellRight
    {
        background-image: url('mvwres://Telerik.Web.UI, Version=2012.1.215.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.ComboBox.rcbSprite.png');
    }
    .RadComboBox .rcbArrowCellRight
    {
        background-color: transparent;
        background-repeat: no-repeat;
    }
    .RadComboBox_Default .rcbArrowCellRight
    {
        background-image: url('mvwres://Telerik.Web.UI, Version=2012.1.215.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.ComboBox.rcbSprite.png');
    }
    .RadComboBox .rcbArrowCellRight
    {
        background-color: transparent;
        background-repeat: no-repeat;
    }
    .RadComboBox_Default .rcbArrowCellRight
    {
        background-image: url('mvwres://Telerik.Web.UI, Version=2012.1.215.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.ComboBox.rcbSprite.png');
    }
    .RadComboBox .rcbArrowCellRight
    {
        background-color: transparent;
        background-repeat: no-repeat;
    }
    .RadComboBox_Default .rcbArrowCellRight
    {
        background-image: url('mvwres://Telerik.Web.UI, Version=2012.1.215.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.ComboBox.rcbSprite.png');
    }
    .RadComboBox .rcbArrowCellRight
    {
        background-color: transparent;
        background-repeat: no-repeat;
    }
    .RadPicker
    {
        vertical-align: middle;
    }
    .rdfd_
    {
        position: absolute;
    }
    .RadPicker .rcTable
    {
        table-layout: auto;
    }
    .RadPicker .RadInput
    {
        vertical-align: baseline;
    }
    .RadInput_Default
    {
        font: 12px "segoe ui" ,arial,sans-serif;
    }
    .riSingle
    {
        box-sizing: border-box;
        -moz-box-sizing: border-box;
        -ms-box-sizing: border-box;
        -webkit-box-sizing: border-box;
        -khtml-box-sizing: border-box;
    }
    .riSingle
    {
        position: relative;
        display: inline-block;
        white-space: nowrap;
        text-align: left;
    }
    .RadInput
    {
        vertical-align: middle;
    }
    .riSingle .riDisplay
    {
        box-sizing: border-box;
        -moz-box-sizing: border-box;
        -ms-box-sizing: border-box;
        -webkit-box-sizing: border-box;
        -khtml-box-sizing: border-box;
    }
    .riDisplay
    {
        position: absolute;
        padding: 2px 0 3px 5px;
        border: 0 solid transparent;
        border-width: 1px 2px 0 1px;
        width: 100%;
        height: 100%;
        overflow: hidden;
        white-space: nowrap;
        text-align: left;
        cursor: default;
        margin-left: 1px;
    }
    .riSingle .riTextBox
    {
        box-sizing: border-box;
        -moz-box-sizing: border-box;
        -ms-box-sizing: border-box;
        -webkit-box-sizing: border-box;
        -khtml-box-sizing: border-box;
    }
    .RadPicker_Default .rcCalPopup
    {
        background-position: 0 0;
    }
    .RadPicker_Default .rcCalPopup
    {
        background-image: url('mvwres://Telerik.Web.UI, Version=2012.1.215.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Calendar.sprite.gif');
    }
    .RadPicker .rcCalPopup
    {
        display: block;
        overflow: hidden;
        width: 22px;
        height: 22px;
        background-color: transparent;
        background-repeat: no-repeat;
        text-indent: -2222px;
        text-align: center;
    }
    .style1
    {
        width: 100%;
        border-style: none;
        border-color: inherit;
        border-width: 0;
        margin: 0;
        padding: 0;
    }
    .style9
    {
        width: 11px;
    }
    .style29
    {
        width: 124px;
    }
    .style39
    {
        width: 353px;
    }
    .style40
    {
        width: 235px;
    }
    .style41
    {
        width: 235px;
        border-style: none;
        border-color: inherit;
        border-width: 0;
        margin: 0;
        padding: 0;
    }
    .style42
    {
        width: 229px;
    }
    .style43
    {
        width: 229px;
        border-style: none;
        border-color: inherit;
        border-width: 0;
        margin: 0;
        padding: 0;
    }
    .style44
    {
        width: 87px;
    }
        .RadToolBar { text-align: right; }
        </style>
<asp:Panel ID="PanelMaster" runat="server" Width="100%">
    <table>
        <tr>
            <td class="style44">
                &nbsp;</td>
            <td class="style39">
                &nbsp;</td>
            <td class="style29">
                &nbsp;&nbsp;</td>
            <td class="style29">
                &nbsp;</td>
            <td class="style40">
                &nbsp;</td>
            <td class="style42">
                &nbsp;&nbsp;</td>
            <td class="style42">
                &nbsp;</td>
            <td class="style9">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style44">
                <asp:Label ID="lblSuscriptor" runat="server" Font-Bold="True" 
                    Text="Suscriptor:" Width="80px" />
            </td>
            <td class="style39">
                <telerik:RadComboBox ID="ddlSuscriptor" runat="server" AutoPostBack="true" 
                    Culture="es-ES" DataTextField="NombreSuscriptor" DataValueField="IdSuscriptor" 
                    EmptyMessage="Seleccione"
                    OnSelectedIndexChanged="DdlSuscriptorSelectedIndexChanged" Visible="false" 
                    Width="300px" />
                <br />
                <telerik:RadComboBox ID="ddlSuscriptorASimular" runat="server" 
                    AutoPostBack="true" Culture="es-ES" DataTextField="Nombre" DataValueField="Id" 
                    EmptyMessage="Seleccione" 
                    OnSelectedIndexChanged="DdlSuscriptorSelectedIndexChanged" Visible="false" 
                    Width="300px" />
            </td>
            <td class="style29">
                &nbsp;</td>
            <td class="style29">
                <asp:Label ID="lblEstatus" runat="server" Font-Bold="True" Text="Estatus:" 
                    Width="100px" />
            </td>
            <td class="style40">
                <telerik:RadComboBox ID="ddlEstatus" runat="server" DataTextField="NombreValor" 
                    DataValueField="Id" EmptyMessage="Seleccione" Width="110px">
                </telerik:RadComboBox>
            </td>
            <td class="style42">
                &nbsp;</td>
            <td class="style42">
                <asp:Label ID="lblAsegurado" runat="server" Font-Bold="True" 
                    Text="Asegurado:" />
            </td>
            <td class="style9">
                <asp:TextBox ID="txtAsegurado" runat="server" Width="150px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style44">
                <asp:Label ID="lblIdServicio" runat="server" Text="Servicio:" Font-Bold="True" 
                    Width="100px" />
            </td>
            <td align="left" class="style39">
                <telerik:RadComboBox ID="ddlServicio" runat="server" DataTextField="NombreServicioSuscriptor" 
                    DataValueField="IdServicioSuscriptor" EmptyMessage="Seleccione" Width="300px">
                </telerik:RadComboBox>
            </td>
            <td class="style29">
                &nbsp;</td>
            <td class="style29">
                <asp:Label ID="lblFechaDesde" runat="server" Font-Bold="True" 
                    Text="Fecha desde:" Width="100px" />
            </td>
            <td class="style41">
                <telerik:RadDatePicker ID="txtFechaDesde" runat="server" 
                    DateInput-EmptyMessage="DD/MM/YYYY" MinDate="1900-01-01" Width="115px">
                    <Calendar ID="Calendar5" runat="server">
                        <SpecialDays>
                            <telerik:RadCalendarDay Repeatable="Today">
                                <ItemStyle BorderColor="Red" Font-Bold="true" />
                            </telerik:RadCalendarDay>
                        </SpecialDays>
                    </Calendar>
                </telerik:RadDatePicker>
            </td>
            <td class="style43">
                &nbsp;</td>
            <td class="style43">
                <asp:Label ID="lblIntermediario" runat="server" Font-Bold="True" 
                    Text="Intermediario:" />
            </td>
            <td class="style1">
                <asp:TextBox ID="txtIntermediario" runat="server" Width="150px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style44">
                <asp:Label ID="lblCaso" runat="server" Font-Bold="True" Text="Filtro:" 
                    Width="100px" />
            </td>
            <td class="style39" align="left">
                <table cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            <telerik:RadComboBox ID="ddlFiltro" runat="server" AutoPostBack="True" 
                                EmptyMessage="Seleccione..." 
                                OnSelectedIndexChanged="DdlFiltroSelectedIndexChanged" Width="150px">
                                <Items>
                                    <telerik:RadComboBoxItem Text="No. de Caso" Value="Id" />
                                    <telerik:RadComboBoxItem Text="No. de Codigo Externo" Value="Idcasoexterno" />
                                    <telerik:RadComboBoxItem Text="No. de soporte del Incidente" 
                                        Value="SupportIncident" />
                                    <telerik:RadComboBoxItem Text="No. de Documento" Value="NumDocSolicitante" />
                                    <telerik:RadComboBoxItem Text="Ticket" Value="Ticket" />
                                </Items>
                            </telerik:RadComboBox>
                        </td>
                        <td style="padding-left: 4px">
	                        <asp:TextBox ID="txtFiltro" runat="server" Width="140px" />
                        </td>
                    </tr>
                </table>
            </td>
            <td class="style29">
                &nbsp;</td>
            <td class="style29">
                <asp:Label ID="lblFechaHasta" runat="server" Font-Bold="True" 
                    Text="Fecha hasta:" Width="100px" />
            </td>
            <td class="style41">
                <telerik:RadDatePicker ID="txtFechaHasta" runat="server" 
                    DateInput-EmptyMessage="DD/MM/YYYY" MinDate="1900-01-01" Width="115px">
                    <Calendar ID="Calendar6" runat="server">
                        <SpecialDays>
                            <telerik:RadCalendarDay Repeatable="Today">
                                <ItemStyle BorderColor="Red" Font-Bold="true" />
                            </telerik:RadCalendarDay>
                        </SpecialDays>
                    </Calendar>
                </telerik:RadDatePicker>
            </td>
            <td class="style43">
                &nbsp;</td>
            <td class="style43">
                &nbsp;</td>
            <td class="style1">
                &nbsp;</td>
        </tr>
    </table>
    <table width="100%">
        <tr>
            <td align="right">
                <asp:Button ID="cmdBuscar" runat="server" Text="Buscar" OnClick="CmdBuscarClick"   Width="100px" />&nbsp;
                <asp:Button ID="btnLimpiar" runat="server" onclick="cmdLimpiar_Click" Text="Limpiar" Width="100px" /> &nbsp;
                <%--<asp:Button ID="cmdLimpiar" runat="server" Text="Limpiar" OnClientClick="cmdLimpiar_Click();return false;" Width="100px" CausesValidation="true"   />--%>
            </td>
        </tr>
    </table>
    <div style="display: none">
        <asp:Button ID="Button1" runat="server" Text="Button" OnClick="RefrescarGridClick" />
    </div>
</asp:Panel>
    <telerik:RadCodeBlock runat="server" ID="RadScriptBlock3">
        <script type="text/javascript">
            function cmdLimpiarSuscriptor_Click() {
                var combo = $find("<%= ddlSuscriptor.ClientID %>");
                combo.set_text("");
                combo._applyEmptyMessage();
            }

            function cmdLimpiarEstatus_Click() {
                var combo = $find("<%= ddlEstatus.ClientID %>");
                combo.set_text("");
                combo._applyEmptyMessage();
            }
            function cmdLimpiarFiltro_Click() {
                var combo = $find("<%= ddlFiltro.ClientID %>");
                combo.set_text("");
                combo._applyEmptyMessage();


            }
            function cmdlimpiarServicio() {
                var combo2 = $telerik.findControl(theForm, "ddlServicio");
                combo2.set_text("");
                combo2.set_value("");
                combo2.set_emptyMessage("Seleccione...");
            }
            function cmdLimpiar_Click() {


                try {
                    cmdLimpiarSuscriptor_Click();
                }
                catch (err) { }
                try {
                    cmdLimpiarCreadoPor_Click();
                }
                catch (err) { }

                cmdLimpiarEstatus_Click();
                cmdLimpiarFiltro_Click();
                cmdlimpiarServicio();
                document.getElementById("<%= txtFiltro.ClientID %>").value = "";
                document.getElementById("<%= txtFiltro.ClientID %>").disabled = true;
                $telerik.findControl(theForm, "txtFechaDesde")._dateInput.set_value("");
                $telerik.findControl(theForm, "txtFechaHasta")._dateInput.set_value("");

              
            }
                
        </script>
        
    </telerik:RadCodeBlock>