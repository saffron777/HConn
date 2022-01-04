<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DatosGeneralesCasoDetalle.aspx.cs" Inherits="HConnexum.Tramitador.Sitio.Modulos.Tracking.DatosGeneralesDetalle" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <title></title>
    </head>
    <body>
        <form id="form1" runat="server">
            <telerik:RadScriptManager ID="RadScriptManager" runat="server">
            </telerik:RadScriptManager>
            <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" DefaultLoadingPanelID="RadAjaxLoadingPanel1">
                <AjaxSettings> 
                    <telerik:AjaxSetting AjaxControlID="RadGridDetails">
                        <UpdatedControls>
                            <telerik:AjaxUpdatedControl ControlID="PanelMaster" />
                        </UpdatedControls>
                    </telerik:AjaxSetting>
                </AjaxSettings>
            </telerik:RadAjaxManager>
            <div align=left>
                <telerik:RadGrid ID="RadGrid1" runat="server" SkinID="gridviewSkin"
                    CellSpacing="0" Culture="es-ES" GridLines="None"
                    AutoGenerateColumns="False" Width="100%" ShowHeader="False" 
                    AllowSorting="True">
                    <ClientSettings>
                    <Selecting CellSelectionMode="None"></Selecting>
                    </ClientSettings>

                    <MasterTableView AutoGenerateColumns="false" AllowCustomSorting="False" AllowSorting="True">
                    <CommandItemSettings ExportToPdfText="Export to PDF"></CommandItemSettings>

                    <RowIndicatorColumn Visible="True" FilterControlAltText="Filter RowIndicator column">
                    <HeaderStyle Width="20px"></HeaderStyle>
                    </RowIndicatorColumn>

                    <ExpandCollapseColumn Visible="True" FilterControlAltText="Filter ExpandColumn column">
                    <HeaderStyle Width="20px"></HeaderStyle>
                    </ExpandCollapseColumn>

                        <Columns>
                            <telerik:GridBoundColumn FilterControlAltText="Filter column column" UniqueName="column" DataField="ETIQUETA" ItemStyle-Width="30%" />
                            <telerik:GridBoundColumn FilterControlAltText="Filter column1 column" UniqueName="column1" DataField="VALOR" ItemStyle-Width="70%" ItemStyle-Wrap="true" />
                        </Columns>

                    <EditFormSettings>
                    <EditColumn FilterControlAltText="Filter EditCommandColumn column"></EditColumn>
                    </EditFormSettings>
                    </MasterTableView>

                    <FilterMenu EnableImageSprites="False"></FilterMenu>
                </telerik:RadGrid>
                <br />
                <%--<telerik:RadSkinManager runat="server" ID="RadSkinManager1">
			    <Skins>
					<telerik:SkinReference Assembly="HConnexum.Web.UI.Skins" />
				</Skins>
			</telerik:RadSkinManager>--%>
            </div>
        </form>
    </body>
</html>
