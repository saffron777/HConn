<%@ Page Title="Mis Actividades" MasterPageFile="~/Master/Site.Master" Language="C#" AutoEventWireup="true" CodeBehind="MisActividades.aspx.cs" Inherits="HConnexum.Tramitador.Sitio.Modulos.Estructura.MisActividades" %>

<asp:Content ID="cHead" ContentPlaceHolderID="cphHead" runat="server">

 
    <style type="text/css">
		html { overflow-x: hidden; }
		.RadTreeView div.rtIn { width: 98%; }
	</style>
	<script language="javascript" type="text/javascript">
		function RecargarPagina() {
			var wnd = GetRadWindow();
			wnd.reload(true);
		}

		function AjutarRadTreeChrome() {
		    $("#fieldsetMisActividades").css({ "width": $(window).width() - 40 });
		}

		$(window).resize(function () {
		    var version = comprobarnavegador();
		    if (version == "chrome")
		        AjutarRadTreeChrome();
		});

		$(document).ready(function () {
		    var version = comprobarnavegador();
		    if (version == "chrome")
		        AjutarRadTreeChrome();
		});

		var nombreBoton = '<%=btnActividadSiguiente.ClientID%>';

		var blink = function () {
			degradado();
		}

		var retroceso = false;
		var color_inicio = new Array(119, 136, 153);
		var color_fin = new Array(238, 238, 238);
		var pasos = 100;
		var iteracion = 7;
		var color_actual = new Array(3);
		var diferencia = new Array(3);
		for (i = 0; i < 3; i++)
			diferencia[i] = (color_fin[i] - color_inicio[i]) / pasos;

		function convierteHexadecimal(num) {
			return (num).toString(16);
		}

		function degradado() {
			if (iteracion == pasos)
				retroceso = true;
			else if (iteracion == 7)
				retroceso = false;
			if (!retroceso) {
				iteracion += 1
				for (i = 0; i < 3; i++)
					color_actual[i] = (iteracion * diferencia[i]) + color_inicio[i]
			}
			else {
				iteracion -= 1;
				for (i = 0; i < 3; i++)
					color_actual[i] = color_fin[i] + ((iteracion - pasos) * diferencia[i]);
			}
			colorAplicar = convierteHexadecimal(Math.round(color_actual[0])) + convierteHexadecimal(Math.round(color_actual[1])) + convierteHexadecimal(Math.round(color_actual[2]));
			document.getElementById(nombreBoton).setAttribute('style', 'background-Color: #' + colorAplicar + ';');
		}
	</script>
</asp:Content>
<asp:Content ID="cBody" ContentPlaceHolderID="cphBody" runat="server">
    <telerik:RadAjaxManager ID="RadAjaxManager1" DefaultLoadingPanelID="RadAjaxLoadingPanel1" runat="server">
		<AjaxSettings>
            			
			<telerik:AjaxSetting AjaxControlID="RadGrid1">
				<UpdatedControls>
					<telerik:AjaxUpdatedControl ControlID="RadGrid1" />
				</UpdatedControls>
			</telerik:AjaxSetting>
		
		</AjaxSettings>
	</telerik:RadAjaxManager>

    <div>
		<asp:Panel ID="PanelMaster" runat="server">
			<table width="100%">
				<tr>
					<td><asp:Label ID="lbUsuario" runat="server" Text="Usuario" />&nbsp;/&nbsp;<asp:Label ID="lbSuscriptor" runat="server" Text="Suscriptor" /></td>
					<td align="right"><asp:Button runat="server" ID="btnNuevoCaso" Text="Nuevo Caso" OnClick="btnNuevoCaso_Click" />&nbsp;&nbsp;<asp:Button runat="server" ID="btnActividadSiguiente" Text="Actividades Pendientes" OnClick="btnActividadSiguiente_Click" /></td>
				</tr>
			</table>
			<fieldset style="width: 100%;height:100%">
				<legend><asp:Label ID="lblMisActividades" runat="server" Text="Mis Actividades" Font-Bold="True" /></legend>
			
                <telerik:RadGrid runat="server" ID="RadGrid1"   
                     AllowPaging="True" 
                    AllowSorting="True" CellSpacing="0" Culture="es-ES" GridLines="None" 
                    PageSize="50" Width="100%"  AllowFilteringByColumn="True" OnNeedDataSource="RadGridMaster_NeedDataSource"
                    AutoGenerateColumns="False" ShowGroupPanel="True" Skin="Windows7" OnItemDataBound="RadGrid1_ItemDataBound"  >
                    <ClientSettings AllowDragToGroup="True">
                        <Selecting CellSelectionMode="None" AllowRowSelect="True" />
                    </ClientSettings>
                    <GroupHeaderItemStyle BackColor="Transparent" BorderColor="Transparent" 
                        Font-Bold="True" Font-Size="Small" ForeColor="#000040" />
                    <groupingsettings casesensitive="false"></groupingsettings>
                    <MasterTableView PageSize="50" Width="99%" Height="100%" allowfilteringbycolumn="True" >
                     <GroupByExpressions>
                          <telerik:GridGroupByExpression>
                            <SelectFields>
                              <telerik:GridGroupByField FieldName="NombreServicioSuscriptor" HeaderText="Servicio" />
                            
                            </SelectFields>
                            <GroupByFields>
                                  <telerik:GridGroupByField FieldName="NombreServicioSuscriptor" SortOrder="Ascending" />     
                                                            
                            </GroupByFields>
                          </telerik:GridGroupByExpression>
                    </GroupByExpressions>
                        <CommandItemSettings ExportToPdfText="Export to PDF" />
                        <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column" 
                            Visible="True">
                        </RowIndicatorColumn>
                        <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column" 
                            Visible="True">
                        </ExpandCollapseColumn>
                        <Columns>
                          
                           <telerik:GridBoundColumn DataField="Id" FilterControlAltText="Filter Id column" 
                                HeaderText="Id" UniqueName="Id" Visible="false" >
                            </telerik:GridBoundColumn>
                             <telerik:GridBoundColumn DataField="NombreServicioSuscriptor" FilterControlAltText="Filter NombreServicioSuscriptor column" 
                                HeaderText="Servicio" UniqueName="NombreServicioSuscriptor" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                            </telerik:GridBoundColumn>

                            <telerik:GridBoundColumn DataField="IdCaso" FilterControlAltText="Filter IdCaso column" 
                                HeaderText="Caso" UniqueName="IdCaso"  AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                    ShowFilterIcon="false" >
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Intermediario" FilterControlAltText="Filter Intermediario column" 
                                HeaderText="Intermediario" UniqueName="Intermediario"  AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                    ShowFilterIcon="false">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Solicitante" FilterControlAltText="Filter Solicitante column" 
                                HeaderText="Asegurado" UniqueName="Solicitante"  
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                    ShowFilterIcon="false" AndCurrentFilterFunction="Contains">
                            </telerik:GridBoundColumn>
                            <telerik:GridHyperLinkColumn DataTextField="Movimiento" FilterControlAltText="Filter Movimiento column" 
                                HeaderText="Movimiento" UniqueName="Movimiento"  SortExpression="Movimiento"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                    ShowFilterIcon="false" AndCurrentFilterFunction="Contains" >
                            </telerik:GridHyperLinkColumn>
                            
                            
                            <telerik:GridBoundColumn DataField="SLAToleranciaPaso"   
                                HeaderText="Tiempo" UniqueName="SLAToleranciaPaso" 
                                AllowFiltering="False"  >
                                <FooterStyle ForeColor="Green" />
                            </telerik:GridBoundColumn>
                            

                        </Columns>
                        <EditFormSettings>
                            <EditColumn FilterControlAltText="Filter EditCommandColumn column">
                            </EditColumn>
                        </EditFormSettings>
                    </MasterTableView>
                    <HeaderStyle BackColor="ButtonFace" Font-Bold="True" 
                        Font-Size="Small" ForeColor="#000040" />
                    <FilterMenu EnableImageSprites="False">
                    </FilterMenu>
                </telerik:RadGrid>
			</fieldset>
		</asp:Panel>
		<br />
		<br />
	</div>
</asp:Content>
