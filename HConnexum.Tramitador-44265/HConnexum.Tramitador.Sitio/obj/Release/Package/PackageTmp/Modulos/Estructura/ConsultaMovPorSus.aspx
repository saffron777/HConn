<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="ConsultaMovPorSus.aspx.cs" MasterPageFile="~/Master/Site.Master" Inherits="HConnexum.Tramitador.Sitio.Modulos.Estructura.ConsultaMovPorSus" %>
<asp:Content ID="cHead" ContentPlaceHolderID="cphHead" runat="server">
    <style type="text/css">
        .style1
        {
            width: 637px;
        }
    </style>
</asp:Content>
<asp:Content ID="cBody" ContentPlaceHolderID="cphBody" runat="server">
    <asp:HiddenField ID="HidenIdUsuario" runat="server" />
    <telerik:RadAjaxManager ID="RadAjaxManager1" DefaultLoadingPanelID="RadAjaxLoadingPanel1" runat="server">
		<AjaxSettings>
			<telerik:AjaxSetting AjaxControlID="RadGridDetails">
				<UpdatedControls>
					<telerik:AjaxUpdatedControl ControlID="PanelMaster" />
				</UpdatedControls>
			</telerik:AjaxSetting>
			<telerik:AjaxSetting AjaxControlID="RadGridMaster">
				<UpdatedControls>
					<telerik:AjaxUpdatedControl ControlID="RadGridMaster" />
				</UpdatedControls>
			</telerik:AjaxSetting>
           
             <telerik:AjaxSetting AjaxControlID="ddlSuscriptor">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="ddlServicio"  />
                     <telerik:AjaxUpdatedControl ControlID="ddlUsuarioAsignado"  />
                </UpdatedControls>
            </telerik:AjaxSetting>
			<telerik:AjaxSetting AjaxControlID="RadAjaxManager1">
				<UpdatedControls>
					<telerik:AjaxUpdatedControl ControlID="RadGridMaster" />
				</UpdatedControls>
			</telerik:AjaxSetting>
			<telerik:AjaxSetting AjaxControlID="RadFilterMaster">
				<UpdatedControls>
					<telerik:AjaxUpdatedControl ControlID="RadFilterMaster" />
					<telerik:AjaxUpdatedControl ControlID="LblMessege" />
				</UpdatedControls>
			</telerik:AjaxSetting>
			<telerik:AjaxSetting AjaxControlID="ApplyButton">
				<UpdatedControls>
					<telerik:AjaxUpdatedControl ControlID="RadGridMaster" />
					<telerik:AjaxUpdatedControl ControlID="RadFilterMaster" />
				</UpdatedControls>
			</telerik:AjaxSetting>
		</AjaxSettings>
	</telerik:RadAjaxManager>
	<div>
		<asp:Panel ID="PanelMaster" runat="server">
			<fieldset>
				<legend>
					<asp:Label ID="lgLegendBuscarMovimientos" runat="server" Font-Bold="True" Text="Buscar Movimientos" />
				</legend>
				<table style="width: 391px">
					<tr>
						<td >
							<asp:Label ID="lblSuscriptor" runat="server" Text="Suscriptor:" Font-Bold="True" />
						</td>
                        
						<td >
							<telerik:RadComboBox ID="ddlSuscriptor" DataValueField="Id" 
                                DataTextField="Nombre" EmptyMessage="Seleccione" runat="server" Width="300px" 
                                OnSelectedIndexChanged="ddlSuscriptor_SelectedIndexChanged" AutoPostBack="true" 
                                MarkFirstMatch="True"  />
						</td>
                    </tr>
                    <tr>
						<td style="text-align: left;" >
							<asp:Label ID="lblServicio" runat="server" Text="Servicio:" Font-Bold="True" />
						</td>
						<td class="style1">
							<telerik:RadComboBox ID="ddlServicio" DataValueField="Id" DataTextField="Nombre" Width="300px" EmptyMessage="Seleccione" runat="server" />
						</td>
					
					</tr>
                    <tr>
						<td style="text-align: left;" >
							<asp:Label ID="lblUsuarioAsignado" runat="server" Text="Usuario Asignado:" Font-Bold="True" />
						</td>
						<td class="style1">
							<telerik:RadComboBox ID="ddlUsuarioAsignado" DataValueField="Id" DataTextField="Nombre" Width="195px" EmptyMessage="Seleccione" runat="server" />
						</td>
					
					</tr>
				
				</table>
				<table style="width: 95%">
					<tr>
						<td align="right">
							<asp:Button ID="cmdBuscar" runat="server" Text="Buscar" OnClick="cmdBuscar_Click" Width="80px" />
							<asp:Button ID="cmdLimpiar" runat="server" Text="Limpiar" OnClientClick="cmdLimpiar_Click(),true" Width="74px" />
						</td>
					</tr>
				</table>
			</fieldset>
		</asp:Panel>
		
		<br />
		<telerik:RadScriptBlock runat="server" ID="RadScriptBlock1">
			<script type="text/javascript">
			    var nombreVentana = '<%=RadWindow1.ClientID %>';
			    var nombreVentana2 = '<%=RadWindow2.ClientID %>';
			    var nombreGrid = '<%=RadGridMaster.ClientID%>';
			    var nombreRadFilter = '<%=RadFilterMaster.ClientID %>';
			    var nombreRadAjaxManager = '<%= RadAjaxManager1.ClientID %>';
			    var idMaster = "<%= idMaster %>";
			    var AccionAgregar = "<%= AccionAgregar %>";
			    var AccionModificar = "<%= AccionModificar %>";
			    var AccionVer = "<%= AccionVer %>";
			    var idMenu = '<%= IdMenuEncriptado %>';
			    var idMaster = "<%= idMaster %>";
			    var AccionVer = "<%= AccionVer %>";
			    var idMenu = '<%= IdMenuEncriptado %>';
			 
               
		        function cmdLimpiar_Click() {
		            var comboS = $find("<%= ddlSuscriptor.ClientID %>");
		            comboS.set_text("");
		            comboS._applyEmptyMessage();
		            var combov = $find("<%= ddlUsuarioAsignado.ClientID %>");
		            combov.set_text("");
		            combov._applyEmptyMessage();
		            var comboSer = $find("<%= ddlServicio.ClientID %>");
		            comboSer.set_text("");
		            comboSer._applyEmptyMessage();
		            var btn = $("#<%= Actualizar.ClientID%>");
		            btn.click();

		        }
		        function PanelBarItemClickedMoviento(sender, args) {
		            var wnd = GetRadWindow();
		            var comando = args.get_item().get_commandName();
		            switch (comando) {
		                case "OpenRadFilter":
		                    $find(nombreVentana).show();
		                    break;

		                case "Asignar":
		                    grid = $find(nombreGrid);
		                    var selectedIndexes = grid._selectedIndexes;
		                    if (selectedIndexes.length == 0) {
		                        radalert("Seleccione el registro a mostrar", 380, 50, "Ver detalle de Registro")
		                    }
		                    else {
		                        if (selectedIndexes.length > 0) {
                                var oWnd1 = $find(nombreVentana2);
		                            oWnd1.setUrl("UsuarioSuscriptorLista.aspx?IdUsuarioAsignado=" + grid._clientKeyValues[selectedIndexes[0]].IdUsuarioAsignado);
		                            oWnd1.show();		                        
		                        }
		                    }
		                    break;
		           
		            }
		        }

		        function resizeRadGridWithScroll(sender, args) {
		            resizeRadGrid("<%= RadGridMaster.ClientID %>");
		        }
			</script>
		</telerik:RadScriptBlock>
            <telerik:RadGrid id="RadGridMaster" runat="server" autogeneratecolumns="False" width="100%"
                             cellspacing="0" culture="es-ES" gridlines="None" 
                allowcustompaging="True" allowpaging="True"
                             allowmultirowselection="True" allowsorting="True"  onpageindexchanged="RadGridMaster_PageIndexChanged"
                             onsortcommand="RadGridMaster_SortCommand" 
                onpagesizechanged="RadGridMaster_PageSizeChanged" OnNeedDataSource="RadGridMaster_NeedDataSource"
                             onitemcommand="RadGridMaster_ItemCommand" 
                >
                <ClientSettings EnableRowHoverStyle="true">
                    <Selecting AllowRowSelect="True" />
                    <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                    <ClientEvents OnGridCreated="resizeRadGridWithScroll" />
                </ClientSettings>
                <MasterTableView CommandItemDisplay="Top" NoMasterRecordsText="No se encontraron registros" DataKeyNames="IdUsuarioAsignado,IdEncriptado,UsuarioAsignado" ClientDataKeyNames="IdUsuarioAsignado,IdEncriptado,UsuarioAsignado">
                    <CommandItemSettings ExportToPdfText="Export to PDF"></CommandItemSettings>
                    <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column">
                        <HeaderStyle Width="20px"></HeaderStyle>
                    </RowIndicatorColumn>
                    <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column">
                        <HeaderStyle Width="20px"></HeaderStyle>
                    </ExpandCollapseColumn>
                    <Columns>
					<telerik:GridBoundColumn  DataField="Id" FilterControlAltText="Filtrar columna Id" HeaderText="Id" UniqueName="Id" Visible="false"></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="Intermediario" FilterControlAltText="Filtrar columna Intermediario" HeaderText="Intermediario" UniqueName="Intermediario"><HeaderStyle Width="18%" /><ItemStyle Width="18%" /></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="IdCaso" FilterControlAltText="Filtrar columna IdCaso" HeaderText="Caso" UniqueName="IdCaso" ><HeaderStyle Width="5%" /><ItemStyle Width="5%" /></telerik:GridBoundColumn>
					<telerik:GridBoundColumn DataField="NombreAsegurado" FilterControlAltText="Filtrar columna NombreAsegurado" HeaderText="Afiliado" UniqueName="NombreAsegurado"><HeaderStyle Width="20%" /><ItemStyle Width="20%" /></telerik:GridBoundColumn>
					<telerik:GridBoundColumn DataField="DocumentoAsegurado" FilterControlAltText="Filtrar columna DocumentoAsegurado" HeaderText="Documento Identidad" UniqueName="DocumentoAsegurado"><HeaderStyle Width="9%" /><ItemStyle Width="9%" /></telerik:GridBoundColumn>
					<telerik:GridBoundColumn DataField="Movimiento" FilterControlAltText="Filtrar columna Movimiento" HeaderText="Actividad" UniqueName="Movimiento"><HeaderStyle Width="14%" /><ItemStyle Width="14%" /></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="FechaCreacion"  FilterControlAltText="Filtrar columna FechaCreacion" DataFormatString="{0:d}" HeaderText="Fecha de Creacion" UniqueName="FechaCreacion"><HeaderStyle Width="9%" /><ItemStyle Width="9%" /></telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="Estatus" FilterControlAltText="Filtrar columna Estatus" HeaderText="Estatus" UniqueName="Estatus"><HeaderStyle Width="11%" /><ItemStyle Width="11%" /></telerik:GridBoundColumn>
					<telerik:GridBoundColumn DataField="UsuarioAsignado" FilterControlAltText="Filtrar columna UsuarioAsignado" HeaderText="Usuario Asignado" UniqueName="UsuarioAsignado"><HeaderStyle Width="14%" /><ItemStyle Width="14%" /></telerik:GridBoundColumn>
                    
				</Columns>

                    <EditFormSettings>
                        <EditColumn FilterControlAltText="Filter EditCommandColumn column">
                        </EditColumn>
                    </EditFormSettings>
                    <PagerStyle AlwaysVisible="True" />
                    <CommandItemTemplate>
                        <table cellpadding="0" cellspacing="0" border="0" width="100%">
                            <tr>
                                <td align="right">
                                    <telerik:RadToolBar ID="RadToolBar1" OnClientButtonClicking="ClientButtonClicking" OnClientButtonClicked="PanelBarItemClickedMoviento"
                                                        runat="server"  onbuttonclick="RadToolBar1_ButtonClick1" >
                                        <Items>
                                        <telerik:RadToolBarButton runat="server" CommandName="Refrescar" ImagePosition="Right" ImageUrl="~/Imagenes/Refresh.gif" Text="Refrescar"/>
                                        <telerik:RadToolBarButton runat="server" CommandName="OpenRadFilter" ImagePosition="Right" ImageUrl="~/Imagenes/Filter.gif" Text="Mostrar Filtro"/>
                                        <telerik:RadToolBarButton runat="server" CommandName="Asignar"       ImagePosition="Right" ImageUrl="~/Imagenes/AddRecord.gif" Text="Asignar" Owner="" />
                                        <telerik:RadToolBarButton runat="server" CommandName="Desasignar"    ImagePosition="Right" ImageUrl="~/Imagenes/Delete.gif" Text="Desasignar"  Owner=""/>
                                        </Items>
                                    </telerik:RadToolBar>
                                </td>
                            </tr>
                        </table>
                    </CommandItemTemplate>
                </MasterTableView>
                <PagerStyle AlwaysVisible="True"  />
                <FilterMenu EnableImageSprites="False">
                </FilterMenu>
                <HeaderContextMenu CssClass="GridContextMenu GridContextMenu_Default">
                </HeaderContextMenu>
            </telerik:RadGrid>
	</div>
    <div style="display:none">
    <asp:Button Id="Actualizar" runat="server" onclick="Actualizar_Click" />
    <asp:Button ID="BtnAsignar" runat="server" Text="Voy" OnClick="BtnAsignar_Click" Style="display: none" CausesValidation="False"   />
    </div>
    <telerik:RadWindow id="RadWindow1" runat="server" height="350px" Modal="true"
		destroyonclose="true" title="Filtro" width="600px" keepinscreenbounds="true">
		<ContentTemplate>
			<fieldset>
				<legend><b><asp:Label ID="Label1" runat="server" Text="Busqueda Avanzada" Font-Bold="True" /></b></legend>
				<table>
					<tr>
						<td>
							<telerik:RadFilter ID="RadFilterMaster" runat="server" FilterContainerID="RadGridMaster" OnItemCommand="RadFilterMaster_ItemCommand" ShowApplyButton="False"  CssClass="RadFilter RadFilter_Windows7 RadFilter RadFilter_Windows7 "/>
						</td>
					</tr>
					<tr>
						<td>
							<asp:Label ID="LblMessege" runat="server"></asp:Label>
						</td>
					</tr>
					<tr>
						<td>
							<asp:ImageButton ID="ApplyButton" runat="server" ImageUrl="~/Imagenes/Aceptar.gif" OnClick="ApplyButton_Click" OnClientClick="hideFilterBuilderDialog()" />
						</td>
					</tr>
				</table>
			</fieldset>
		</ContentTemplate>
	</telerik:RadWindow>
     <telerik:RadWindow id="RadWindow2" runat="server" height="350px" Modal="true"
		destroyonclose="true" title="Lista Suscriptores" width="600px" 
        keepinscreenbounds="true" VisibleStatusbar="False" >
		
	</telerik:RadWindow>
     <telerik:RadCodeBlock id="RadCodeBlock1" runat="server">
                <script src="../../Scripts/RadFilterScriptBlock.js" type="text/javascript"></script>
                 <script src="../../Scripts/ListaRadScriptBlock1.js" type="text/javascript"></script>       
	</telerik:RadCodeBlock>

</asp:Content>
