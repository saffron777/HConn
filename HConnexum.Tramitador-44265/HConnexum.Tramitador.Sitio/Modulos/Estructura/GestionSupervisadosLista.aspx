<%@ Page Language="C#" AutoEventWireup="True" MasterPageFile="~/Master/Site.Master" Title="Gestion de Operadores" CodeBehind="GestionSupervisadosLista.aspx.cs" Inherits="HConnexum.Tramitador.Sitio.Modulos.Estructura.GestionSupervisadosLista" %>
<asp:Content ID="cHead" ContentPlaceHolderID="cphHead" runat="server">
<style type="text/css">
        html { overflow-x:hidden; }
    </style>
</asp:Content>
<asp:Content ID="cBody" ContentPlaceHolderID="cphBody" runat="server">

<asp:HiddenField ID="HiddenIdUsuarioSuscriptror" runat="server" />
<asp:HiddenField ID="HiddenIdServicio" runat="server" />
<asp:HiddenField ID="HiddenIdSuscriotor" runat="server" />
    <telerik:RadAjaxManager id="RadAjaxManager1" DefaultLoadingPanelID="RadAjaxLoadingPanel1" runat="server">
        <AjaxSettings>
           <telerik:AjaxSetting AjaxControlID="RadGridMaster">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGridMaster" />
                </UpdatedControls>
            </telerik:AjaxSetting>         
         
            <telerik:AjaxSetting AjaxControlID="RadAjaxManager1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGridMaster"/>
                </UpdatedControls>
            </telerik:AjaxSetting>
             <telerik:AjaxSetting AjaxControlID="btnClear">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="PanelMaster"/>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>

    <div>
        <asp:Panel ID="PanelMaster" runat="server">
            <fieldset>
                <legend>
                    <b>
                        <asp:Label ID="lblAnularCaso" runat="server" Text="Gestion de Operadores" Font-Bold="True" meta:resourcekey="LblLegendCasoResource"/>
                    </b>
                </legend>
                <table> 
                    <tr>
                        <td>
                            <asp:Label ID="Label1" runat="server" Text="Usuario:"></asp:Label>
                        </td>
                        <td>
                            
                            <asp:Label ID="lblUsuarioLog" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblSuscriptor" runat="server" Text="Suscriptor:"/>
                        </td>
                        <td>
                            <telerik:RadComboBox ID="ddlSuscriptor" DataValueField="Id"
                                                 DataTextField="Nombre"  EmptyMessage="Seleccione" runat="server" 
                                                 Width="200px" 
                                                 onselectedindexchanged="ddlSuscriptor_SelectedIndexChanged" AutoPostBack="true"/>
                        </td>
                        <td>
                            <asp:Label ID="LblServicio" runat="server" Text="Servicio:"/>
                        </td>
                        <td>
                            <telerik:RadComboBox ID="ddlIdServicio" DataValueField="Id"
                                                 DataTextField="Nombre"  EmptyMessage="Seleccione" runat="server" 
                                                 Width="200px" Culture="es-ES" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblUsuarioSupervisado" runat="server" Text="Usuario Supervisado:"/>
                        </td>
                        <td>
                            <telerik:RadComboBox ID="ddlUsuarioSupervisado" DataValueField="Id"
                                                 DataTextField="Nombre"  EmptyMessage="Seleccione" runat="server" 
                                                 Width="200px" />
                        </td>
                        <td>
                            <asp:Label ID="lblEstatusSupervisados" runat="server" Text="Estatus de Usuario:"/>
                        </td>
                        <td>
                            <telerik:RadComboBox ID="ddlEstatusSupervisados" DataValueField="Id"
                                                 DataTextField="NombreValor"  EmptyMessage="Seleccione" runat="server" 
                                                 Width="200px" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblCargo" runat="server" Text="Cargos:"/>
                        </td>
                        <td>
                            <telerik:RadComboBox ID="ddlCargo" DataValueField="Id"
                                                 DataTextField="Nombre"  EmptyMessage="Seleccione" runat="server" 
                                                 Width="200px" />
                        </td>
                        <td>
                            <asp:Label ID="lblHabilidad" runat="server" Text="Habilidad:"/>
                        </td>
                        <td>
                            <telerik:RadComboBox ID="ddlHabilidad" DataValueField="Id"
                                                 DataTextField="Nombre"  EmptyMessage="Seleccione" runat="server" 
                                                 Width="200px"/>
                        </td>
                        <td>
                            <asp:Label ID="lblAutonomia" runat="server" Text="Autonomia:"/>
                        </td>
                        <td>
                            <telerik:RadComboBox ID="ddlAutonomia" DataValueField="Id"
                                                 DataTextField="Nombre"  EmptyMessage="Seleccione" runat="server" 
                                                 Width="200px"/>
                        </td>
                    </tr>
                </table>
                <table width="100%">
                    <tr>
                        <td style="text-align:right">
                            <asp:Button ID="btnBuscar" runat="server" Text="Buscar"
                                        onclick="btnBuscar_Click"/>
                            <asp:Button ID="btnClear" runat="server"
                                        Text="Limpiar" onclick="btnClear_Click" />
                        </td>
                    </tr>
                </table>

            </fieldset>
        </asp:Panel>
    </div>
    <telerik:RadScriptBlock runat="server" id="RadScriptBlock1">
        <script type="text/javascript">
            var HabVentana1 = '<%=RWHabilidades.ClientID %>'; //habilidades
            var AutVentana2 = '<%=RWAutonomias.ClientID %>'; //autonomias
            var CarVentana3 = '<%=RWCargos.ClientID %>'; //cargos
            var nombreVentana4 = '<%=RadWindow4.ClientID %>'; //carga laboral
            var accesoConf = '<%=AccesoConf %>';
            var UrlConfi = '<%=UrlConfigurador %>';
          
            var nombreGrid = '<%=RadGridMaster.ClientID%>';
            var nombreRadAjaxManager = '<%= RadAjaxManager1.ClientID %>';
          
            var idMaster = "<%= idMaster %>";
            var AccionAgregar = "<%= AccionAgregar %>";
            var AccionModificar = "<%= AccionModificar %>";
            var AccionVer = "<%= AccionVer %>";
            var idMenu = '<%= IdMenuEncriptado %>';
            var IdservicioEnc = '<%=IdservicioEnc %>';
            var IdSuscriptorEnc = '<%=IdSuscriptorEnc %>';

            
            function PanelBarItemClicked2(sender, args) {
                var wnd = GetRadWindow();
                var comando = args.get_item().get_commandName();
                grid = $find(nombreGrid);
                var selectedIndexes1 = grid._selectedIndexes;
               var currentLoadingPanel = $find('<%= Master.FindControl(@"RadAjaxLoadingPanel1").ClientID %>');             
                    switch (comando) {
                       
                        case "Habilidades":
                            if (selectedIndexes1.length == 0) {
                                radalert("Seleccione el registro a mostrar", 380, 50, "Ver detalle de Registro")
                            }
                            else {
                                if (accesoConf) {
                                    var oWnd1 = $find(HabVentana1);
                                    oWnd1.setUrl(UrlConfi + "/Modulos/Usuarios/HabilidadesUsuarioDetalle.aspx?IdMenu=" + idMenu + "&id=" + grid._clientKeyValues[selectedIndexes1[0]].IdEncriptado);
                                        oWnd1.show();
                                    } else radalert("No Tiene Acceso", 380, 50, "Sin Permiso")
                                }
                                currentLoadingPanel.hide(grid);
                            break;
                        case "Autonomia":

                            if (selectedIndexes1.length == 0) {
                                radalert("Seleccione el registro a mostrar", 380, 50, "Ver detalle de Registro")
                            }
                            else {
                                if (accesoConf) {
                                    var oWnd2 = $find(AutVentana2);
                                    oWnd2.setUrl(UrlConfi+"/Modulos/Usuarios/AutonomiasUsuarioLista.aspx?IdMenu=" + idMenu + "&id=" + grid._clientKeyValues[selectedIndexes1[0]].IdEncriptado);
                                    oWnd2.show();
                                }
                                else radalert("No Tiene Acceso", 380, 50, "Sin Permiso")
                            }
                            currentLoadingPanel.hide(grid);
                            break;
                        case "Cargos":

                            if (selectedIndexes1.length == 1) {
                                if (accesoConf) {
                                    
                                    var oWnd3 = $find(CarVentana3);
                                    oWnd3.setUrl(UrlConfi + "/Modulos/Usuarios/UsuariosSuscriptorMaestroDetalleCargos.aspx?IdMenu=" + idMenu + "&id=" + grid._clientKeyValues[selectedIndexes1[0]].IdEncriptado);
                                    oWnd3.show();
                                }
                            } else radalert("Seleccione Solo un registro a mostrar", 380, 50, "Ver detalle de Registro")
                            currentLoadingPanel.hide(grid);
                            break;
                        case "CargaLaboral":

                            if (selectedIndexes1.length == 1) {
                                var url = "Modulos/Estructura/ConsultaMovPorSus.aspx?IdMenu=" + idMenu + "&IdUsuarioSuscriptor=" + grid._clientKeyValues[selectedIndexes1[0]].IdEncriptado;
                                var IdServicio = document.getElementById('<%=ddlIdServicio.ClientID%>').value;
                                var IdSuscriptor = document.getElementById('<%=ddlSuscriptor.ClientID%>').value;
                                if (IdServicio != "") {
                                   
                                    url = url + "&IdServicio=" + IdservicioEnc;
                                }
                                if (IdSuscriptor != "") {
                                   
                                    url = url + "&IdSuscriptor=" + IdSuscriptorEnc;
                                }
                              
                                wnd.setUrl(url);

                            } else radalert("Seleccione Solo un registro a mostrar", 380, 50, "Ver detalle de Registro")
                            currentLoadingPanel.hide(grid);
                            break;
                        default:
                            currentLoadingPanel.hide(grid);
                            break;
                    }
                }
            
        </script>
    </telerik:RadScriptBlock>
    <telerik:RadGrid id="RadGridMaster" runat="server" autogeneratecolumns="False" width="100%"
                     cellspacing="0" gridlines="None" allowcustompaging="True" 
        allowpaging="True" allowsorting="True"  onpageindexchanged="RadGridMaster_PageIndexChanged"
                     onsortcommand="RadGridMaster_SortCommand" 
                     onpagesizechanged="RadGridMaster_PageSizeChanged" OnNeedDataSource="RadGridMaster_NeedDataSource"
                     onitemcommand="RadGridMaster_ItemCommand" Culture="es-ES"  
        OnItemDataBound="RadGridMaster_ItemDataBound">
        <ClientSettings EnableRowHoverStyle="true">
            <Selecting AllowRowSelect="True" />
            <Scrolling AllowScroll="True" UseStaticHeaders="True" />
        </ClientSettings>
        <MasterTableView TableLayout="Fixed" CommandItemDisplay="Top"  NoMasterRecordsText="No se encontraron registros" DataKeyNames="IdEncriptado, IdUsuario" ClientDataKeyNames="IdEncriptado, IdUsuario" width="100%">
            <CommandItemSettings ExportToPdfText="Export to PDF" ></CommandItemSettings>
            <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column">
                <HeaderStyle Width="20px"></HeaderStyle>
            </RowIndicatorColumn>
            <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column">
                <HeaderStyle Width="20px"></HeaderStyle>
            </ExpandCollapseColumn>
            <Columns>
                <telerik:GridBoundColumn DataField="IdUsuario"   
                                         FilterControlAltText="Filtrar columna IdUsuario" DataFormatString="{0:N0}" 
                                         HeaderText="IdUsuario" UniqueName="IdUsuario" 
                    Display="False" >
                 </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="NombreUsuario" 
                                         FilterControlAltText="Filtrar columna NombreUsuario"  
                                         HeaderText="Nombre" UniqueName="NombreUsuario">
                 </telerik:GridBoundColumn>
                   <telerik:GridBoundColumn DataField="CargaLaboral" 
                                         FilterControlAltText="Filtrar columna CargaLaboral"  
                                         HeaderText="Carga Laboral" UniqueName="CargaLaboral">
                 </telerik:GridBoundColumn>               
  
                <telerik:GridTemplateColumn HeaderText="Temp" UniqueName="Temp" >  
                    <ItemTemplate> 
                        <telerik:RadComboBox ID="RadComboBox1" runat="server" DataValueField="Id"
                                             DataTextField="NombreValor" OnSelectedIndexChanged="RadComboBox1_SelectedIndexChanged" AutoPostBack="true">
                        </telerik:RadComboBox>
                    </ItemTemplate> 
                </telerik:GridTemplateColumn> 


                <telerik:GridBoundColumn DataField="EstatusUsuario" 
                                         FilterControlAltText="Filter Estatus column" 
                                         HeaderText="EstatusUsuario" UniqueName="EstatusUsuario" Visible="False">
                </telerik:GridBoundColumn>


            </Columns>
            <EditFormSettings>
                <EditColumn FilterControlAltText="Filter EditCommandColumn column"></EditColumn>
            </EditFormSettings>
            <PagerStyle AlwaysVisible="True" />
            <CommandItemTemplate>
                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                    <tr>
                        <td align="right">
                            <telerik:RadToolBar ID="RadToolBar1"  OnClientButtonClicking="ClientButtonClicking" OnClientButtonClicked="PanelBarItemClicked2" runat="server"  onbuttonclick="RadToolBar1_ButtonClick1" >
                                <Items>
                                    
                                    <telerik:RadToolBarButton runat="server" CommandName="Refrescar" ImagePosition="Right" ImageUrl="~/Imagenes/Refresh.gif" Text="Refrescar" Owner=""/>
                                    <telerik:RadToolBarButton runat="server" CommandName="CargaLaboral" ImagePosition="Right" ImageUrl="~/Imagenes/icon_lupa18x18.png" Text="Ver Carga Laboral" Owner=""/>
                                    <telerik:RadToolBarButton runat="server" CommandName="Cargos" ImagePosition="Right" ImageUrl="~/Imagenes/icon_lupa18x18.png" Text="Cargos" Owner=""/>
                                    <telerik:RadToolBarButton runat="server" CommandName="Habilidades" ImagePosition="Right"  ImageUrl="~/Imagenes/icon_lupa18x18.png" Text="Habilidades" Owner=""/>
                                    <telerik:RadToolBarButton runat="server" CommandName="Autonomia" ImagePosition="Right" ImageUrl="~/Imagenes/icon_lupa18x18.png" Text="Autonomia" Owner=""/>


                                </Items>
                            </telerik:RadToolBar>
                        </td>
                    </tr>
                </table>
            </CommandItemTemplate>
        </MasterTableView>
        <PagerStyle AlwaysVisible="True"  />
        <FilterMenu EnableImageSprites="False"></FilterMenu>
        <HeaderContextMenu CssClass="GridContextMenu GridContextMenu_Default"></HeaderContextMenu>
    </telerik:RadGrid>
         <telerik:RadWindow id="RWHabilidades" runat="server" height="350px" 
		title="Habilidades" width="600px" keepinscreenbounds="true" />
		
        <telerik:RadWindow id="RWAutonomias" runat="server" height="350px" 
		title="Autonomias" width="600px" keepinscreenbounds="true"  />
        
        <telerik:RadWindow id="RWCargos" runat="server" height="350px"
		 title="Cargos" width="600px" keepinscreenbounds="true"  />

         <telerik:RadWindow id="RadWindow4" runat="server" height="350px"  
		 title="Carga Laboral" width="600px" keepinscreenbounds="true"  />

    
    
</asp:Content>
