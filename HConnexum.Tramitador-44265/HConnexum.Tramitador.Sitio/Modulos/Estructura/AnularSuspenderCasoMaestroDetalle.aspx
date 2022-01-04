<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Master/Site.Master" Title="Detalle de Caso" CodeBehind="AnularSuspenderCasoMaestroDetalle.aspx.cs" Inherits="HConnexum.Tramitador.Sitio.Modulos.Estructura.AnularSuspenderCasoMaestroDetalle" %>

<%@ Register Src="~/ControlesComunes/MultilineCounter.ascx" TagName="MultilineCounter" TagPrefix="hcc" %>
<asp:Content ID="cHead" ContentPlaceHolderID="cphHead" runat="server">
    <style type="text/css">
        html { overflow-x:hidden; }
		.style1
		{
			color: #000000;
		}
	    .style2
        {
            width: 72px;
        }
	</style>
</asp:Content>
<asp:Content ID="cBody" ContentPlaceHolderID="cphBody" runat="server">
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
			<telerik:AjaxSetting AjaxControlID="RadAjaxManager1">
				<UpdatedControls>
					<telerik:AjaxUpdatedControl ControlID="RadGridMaster" />
				</UpdatedControls>
			</telerik:AjaxSetting>
		</AjaxSettings>
	</telerik:RadAjaxManager>
	<div>
		<asp:Panel ID="PanelMaster" runat="server">
			<fieldset>
				<legend><b>
					<asp:Label runat="server" Text="Caso" Font-Bold="True" meta:resourcekey="LblLegendCasoResource" /></b></legend>
				<table>
					<tr>
						<td>
							<asp:Label ID="lblCaso" runat="server" Text="Caso Numero:" />
						</td>
						<td>
							<asp:TextBox ID="txtCasoNumero" runat="server" Enabled="False" Width="249px" />
						</td>
                        <td class="style2"></td>
						<td>
							<asp:Label ID="lblEstatus" runat="server" Text="Estatus:" CssClass="style1" />
						</td>
						<td>
							<asp:TextBox ID="txtEstatus" runat="server" Enabled="False" Width="249px"/>
						</td>
					</tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblSolicitud" runat="server" Text="Solicitud:" />
                        </td>
                        <td>
                            <asp:TextBox ID="txtIdSolicitud" runat="server" Enabled="False" Width="249px" />
                        </td>
                            <td class="style2"></td>
                        <td>
                            <asp:Label ID="lblFechaSolicitud" runat="server" CssClass="style1" 
                                Text="FechaSolicitud:" />
                        </td>
                        <td>
                            <telerik:RadDatePicker ID="txtFechaSolicitud" runat="server" 
                                DateInput-EmptyMessage="DD/MM/YYYY" Enabled="False" MinDate="1900-01-01" Width="260px">
                                <calendar>
                                    <specialdays>
                                        <telerik:RadCalendarDay Repeatable="Today">
                                            <ItemStyle BorderColor="Red" Font-Bold="true" />
                                        </telerik:RadCalendarDay>
                                    </specialdays>
                                </calendar>
                            </telerik:RadDatePicker>
                        </td>
                    </tr>
					<tr>
						<td>
							<asp:Label ID="lblSuscriptor" runat="server" Text="Suscriptor:" />
						</td>
						<td>
							<asp:TextBox ID="txtSuscriptor" runat="server" Enabled="False" Width="249px" />
						</td>
                            <td class="style2"></td>
						<td>
							<asp:Label ID="lblCreadorPor" runat="server" Text="CreadorPor:" />
						</td>
						<td>
							<asp:TextBox ID="txtCreadorPor" runat="server" Enabled="False"  Width="249px"/>
						</td>
                        
                       
					</tr>
					<tr>
						 <td>
                         <asp:Label ID="lblDocumento" runat="server" Text="Documento:" />
							
						</td>
						<td>
							<asp:TextBox ID="txtTipoDoc" runat="server" Enabled="False" Width="83px" />
                            <asp:TextBox ID="txtDocSolicitante" runat="server" Enabled="False" 
                                Width="158px" />
						</td>
                            <td class="style2"></td>
						<td>
							<asp:Label ID="lblFechaCreacion" runat="server" Text="Fecha Creación:" />
						</td>
						<td>
							<telerik:RadDatePicker ID="txtFechaCreacion2" runat="server" MinDate="1900-01-01" DateInput-EmptyMessage="DD/MM/YYYY"  Width="260px" Enabled="False">
								<Calendar>
									<SpecialDays>
										<telerik:RadCalendarDay Repeatable="Today">
											<ItemStyle Font-Bold="true" BorderColor="Red" />
										</telerik:RadCalendarDay>
									</SpecialDays>
								</Calendar>
							</telerik:RadDatePicker>
						</td>
					</tr>
					<tr>
						<td>
							<asp:Label ID="lblIdServicio" runat="server" Text="Servicio:" CssClass="style1" />
						</td>
						<td>
							<asp:TextBox ID="txtServicio" runat="server" Enabled="False"  Width="249px"/>
						</td>
                            <td class="style2"></td>
						<td>
							<asp:Label ID="Label1" runat="server" Text="Versión:" />
						</td>
						<td>
							<asp:TextBox ID="txtVersion" runat="server" Enabled="False" Width="249px"/>
						</td>
					</tr>
					<tr>
						<td>
							<asp:Label ID="lblFechaAnulacion" runat="server" Text="FechaAnulacion:"  />
						</td>
						<td>
							<telerik:RadDatePicker ID="txtFechaAnulacion" runat="server"   
                                MinDate="1900-01-01" DateInput-EmptyMessage="DD/MM/YYYY" Enabled="False" 
                                Height="23px" Width="260px" >
								<Calendar>
									<SpecialDays>
										<telerik:RadCalendarDay Repeatable="Today">
											<ItemStyle Font-Bold="true" BorderColor="Red" />
										</telerik:RadCalendarDay>
									</SpecialDays>
								</Calendar>
							    <dateinput dateformat="dd/MM/yyyy" displaydateformat="dd/MM/yyyy" 
                                    emptymessage="DD/MM/YYYY" enablesingleinputrendering="True" Width="159px" 
                                    Height="23px">
                                </dateinput>
                                <datepopupbutton cssclass="rcCalPopup rcDisabled" hoverimageurl="" 
                                    imageurl="" />
							</telerik:RadDatePicker>
						</td>
                            <td class="style2"></td>
						<td>
							<asp:Label ID="lblFechaRechazo" runat="server" Text="FechaRechazo:" CssClass="style1" />
						</td>
						<td>
							<telerik:RadDatePicker ID="txtFechaRechazo" runat="server" MinDate="1900-01-01" 
                                DateInput-EmptyMessage="DD/MM/YYYY" Enabled="False" Width="260px">
								<Calendar>
									<SpecialDays>
										<telerik:RadCalendarDay Repeatable="Today">
											<ItemStyle Font-Bold="true" BorderColor="Red" />
										</telerik:RadCalendarDay>
									</SpecialDays>
								</Calendar>
							</telerik:RadDatePicker>
						</td>
					</tr>
					<tr>
						<td>
							<asp:Label ID="lblModificadoPor2" runat="server" Text="Modificado Por:" CssClass="style1" />
						</td>
						<td>
							<asp:TextBox ID="txtModificado" runat="server" Enabled="False" Width="249px"/>
						</td>
                            <td class="style2"></td>
						<td>
							<asp:Label ID="lblFechaModificacion" runat="server" Text="Fecha Modificacion" />
						</td>
						<td>
							<telerik:RadDatePicker ID="RadDatePicker1" runat="server" MinDate="1900-01-01" DateInput-EmptyMessage="DD/MM/YYYY" Width="260px" Enabled="False">
								<Calendar>
									<SpecialDays>
										<telerik:RadCalendarDay Repeatable="Today">
											<ItemStyle Font-Bold="true" BorderColor="Red" />
										</telerik:RadCalendarDay>
									</SpecialDays>
								</Calendar>
							</telerik:RadDatePicker>
						</td>
					</tr>
					<tr>
						<td>
							<asp:Label ID="lblPrioridadAtencion" runat="server" Text="Prioridad Atencion:" CssClass="style1" />
						</td>
						<td>
							<asp:TextBox ID="txtPrioridadAtencion" runat="server" Enabled="False" Width="249px"/>
						</td>
					</tr>
				</table>
			</fieldset>
		</asp:Panel>
		<br />
		<br />
		<telerik:RadScriptBlock runat="server" ID="RadScriptBlock2">
			<script type="text/javascript">
				var nombreVentana = '<%=RadWindow1.ClientID %>';
				var nombreGrid = '<%=RadGridMaster.ClientID%>';
				var nombreRadAjaxManager = '<%= RadAjaxManager1.ClientID %>';
				var nombreRadFilter = '<%=RadFilterMaster.ClientID %>';
				var idMaster = "<%= idMaster %>";
				var AccionAgregar = "<%= AccionAgregar %>";
				var AccionModificar = "<%= AccionModificar %>";
				var AccionVer = "<%= AccionVer %>";
				var idMenu = '<%= IdMenuEncriptado %>';
				var ventanaDetalle = "Modulos/Estructura/MovimientoDetalle.aspx?IdMenu=" + idMenu + "&";
			</script>
		</telerik:RadScriptBlock>
		<telerik:RadGrid ID="RadGridMaster" runat="server" AutoGenerateColumns="False" 
            Width="100%" CellSpacing="0" GridLines="None" AllowCustomPaging="True"
            AllowPaging="True" AllowSorting="True"  OnPageIndexChanged="RadGridMaster_PageIndexChanged"
			OnSortCommand="RadGridMaster_SortCommand" 
            OnPageSizeChanged="RadGridMaster_PageSizeChanged" 
            OnNeedDataSource="RadGridMaster_NeedDataSource" 
            OnItemCommand="RadGridMaster_ItemCommand" 
            OnItemDataBound="RadGridMaster_ItemDataBound" Culture="es-ES">
			<ClientSettings EnableRowHoverStyle="true">
				<Selecting AllowRowSelect="True" />
				
				<Scrolling AllowScroll="True" UseStaticHeaders="True" />
				<Selecting CellSelectionMode="None" AllowRowSelect="True"></Selecting>
				
				<Scrolling AllowScroll="True" UseStaticHeaders="True"></Scrolling>
			</ClientSettings>
			<MasterTableView CommandItemDisplay="Top" NoMasterRecordsText="No se encontraron registros" DataKeyNames="IdEncriptado, NombreEstatusCaso" ClientDataKeyNames="IdEncriptado" width="100%">
				<CommandItemSettings ExportToPdfText="Export to PDF"></CommandItemSettings>
				<RowIndicatorColumn FilterControlAltText="Filter RowIndicator column">
					<HeaderStyle Width="20px"></HeaderStyle>
				</RowIndicatorColumn>
				<ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column">
					<HeaderStyle Width="20px"></HeaderStyle>
				</ExpandCollapseColumn>
				<Columns>
					<telerik:GridBoundColumn DataField="Id" 
                        FilterControlAltText="Filtrar columna Id" DataFormatString="{0:N0}" 
                        HeaderText="Numero" UniqueName="Id">
					</telerik:GridBoundColumn>
					<telerik:GridBoundColumn DataField="IdCaso" 
                        FilterControlAltText="Filtrar columna IdCaso" DataFormatString="{0:N0}" 
                        HeaderText="Id Caso" UniqueName="IdCaso">
					</telerik:GridBoundColumn>
					<telerik:GridBoundColumn DataField="Movimiento" 
                        FilterControlAltText="Filtrar columna Movimiento" HeaderText="Nombre" 
                        UniqueName="Movimiento">
					</telerik:GridBoundColumn>
					<telerik:GridBoundColumn DataField="NombreTipoMovimiento" 
                        FilterControlAltText="Filtrar columna NombreTipoMovimiento" 
                        DataFormatString="{0:N0}" HeaderText="TipoMovimiento" 
                        UniqueName="NombreTipoMovimiento">
					</telerik:GridBoundColumn>
					<telerik:GridBoundColumn DataField="NombreEstatusCaso" 
                        FilterControlAltText="Filtrar columna NombreEstatusCaso" 
                        DataFormatString="{0:N0}" HeaderText="Estatus" UniqueName="NombreEstatusCaso">
					</telerik:GridBoundColumn>
					<telerik:GridTemplateColumn HeaderText="Reversar" UniqueName="Reversar" HeaderStyle-Width="70px" ItemStyle-HorizontalAlign="Center">
						<ItemTemplate>
							<asp:ImageButton runat="server" ImageUrl="~/Imagenes/Delete.gif" ToolTip="Reversar" CommandName="Reversar" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"Id") %>' OnClientClick='<%# "MostrarObservacionesReversar(" + Eval("Id") + "); return false;" %>' />
						</ItemTemplate>
						<HeaderStyle Width="70px"></HeaderStyle>
						<ItemStyle HorizontalAlign="Center"></ItemStyle>
					</telerik:GridTemplateColumn>
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
								<telerik:RadToolBar ID="RadToolBar1" OnClientButtonClicking="ClientButtonClicking" OnClientButtonClicked="PanelBarItemClicked" runat="server"  OnButtonClick="RadToolBar1_ButtonClick1">
									<Items>
										<telerik:RadToolBarButton runat="server" CommandName="Refrescar" ImagePosition="Right" ImageUrl="~/Imagenes/Refresh.gif" Text="Refrescar" />
										<telerik:RadToolBarButton runat="server" CommandName="OpenRadFilter" ImagePosition="Right" ImageUrl="~/Imagenes/Filter.gif" Text="Mostrar Filtro" />
										<telerik:RadToolBarButton runat="server" CommandName="ViewDetails" PostBack="false" ImagePosition="Right" ImageUrl="~/Imagenes/icon_lupa18x18.png" Text="Ver Detalle" />
									</Items>
								</telerik:RadToolBar>
							</td>
						</tr>
					</table>
				</CommandItemTemplate>
			</MasterTableView>
			<PagerStyle AlwaysVisible="True" />
			<FilterMenu EnableImageSprites="False">
			</FilterMenu>
			<HeaderContextMenu CssClass="GridContextMenu GridContextMenu_Default">
			</HeaderContextMenu>
		</telerik:RadGrid>
		<telerik:RadWindow ID="RadWindowAnularSuspenderPregunta" runat="server" class="RadWindow" Title="Anular/Suspender Caso" VisibleStatusbar="False" EnableViewState="False" DestroyOnClose="True" KeepInScreenBounds="True" Behaviors="Close, Move" Modal="True"
			BorderStyle="Dotted" EnableTheming="False" BorderWidth="10px" Height="150px" Width="380px" Behavior="Close, Move">
			<ContentTemplate>
				<div>
					<table width="350" cellspacing="0" cellpadding="0" border="0">
						<tr>
							<td align="center">
								<br />
								<b><asp:Label ID="lblPreguntaAnularSuspender" runat="server"/></b>
							</td>
						</tr>
						<tr>
							<td align="center">
								<br />
								<asp:Button ID="btnSiAnularSuspender" runat="server" Text="Si" OnClientClick="CerrarVentanaAnularSuspenderPregunta();MostrarVentanaOmitir();return false;" />
								<asp:Button ID="btnNoAnularSuspender" runat="server" Text="No" OnClientClick="CerrarVentanaAnularSuspenderPregunta();return false;" />
							</td>
						</tr>
					</table>
				</div>
			</ContentTemplate>
		</telerik:RadWindow>
		<telerik:RadWindow ID="RadWindowAnularSuspender" runat="server" class="RadWindow" Title="Anular/Suspender Caso" VisibleStatusbar="False" EnableViewState="False" DestroyOnClose="True" KeepInScreenBounds="True" Behaviors="Close, Move" Modal="True"
			BorderStyle="Dotted" EnableTheming="False" BorderWidth="10px" Height="200px" Width="372px" Behavior="Close, Move">
			<ContentTemplate>
				<div>
					<table width="350" cellspacing="0" cellpadding="0" border="0">
						<tr>
							<td align="center">
								<asp:Label ID="lblAnularSuspender" runat="server" Text="Razones por las que se está realizando dicha acción:"></asp:Label>
							</td>
                            <td></td>
						</tr>
                     
						<tr>
                        
							<td align="center">
                            <BR>
								<hcc:MultilineCounter ID="txtObservacion" runat="server" Width="300" MaxLength="500" />
							</td> 
						</tr>
						<tr>
							<td align="center">
								<asp:Button ID="btnSiguiente3" runat="server"  Width="70px" Text="Siguiente" OnClick="cmdAnularSuspender_Click" />
								<asp:Button ID="btnAtras" runat="server" Text="Atras" Width="70px" OnClientClick="CerrarVentanaAnularSuspender();return false;" />
							</td>
						</tr>
					</table>
				</div>
			</ContentTemplate>
		</telerik:RadWindow>
		<telerik:RadWindow ID="RadWindowReversarMovimiento" runat="server" class="RadWindow" Title="Reverso de Movimiento" VisibleStatusbar="False" EnableViewState="False" DestroyOnClose="True" KeepInScreenBounds="True" Behaviors="Close, Move" Modal="True"
			BorderStyle="Dotted" EnableTheming="False" BorderWidth="10px" Height="200px" Width="372px" Behavior="Close, Move">
			<ContentTemplate>
				<div>
					<table width="350" cellspacing="0" cellpadding="0" border="0">
						<tr>
							<td align="center">
                            <br />
								<asp:Label runat="server" Text="Razones por las que se está realizando dicha acción:"></asp:Label>
                               <b></b>
							</td>
						</tr>
						<tr>
							<td align="center">
                            <br />
								<hcc:MultilineCounter ID="txtObservacionesReversar"  runat="server" Width="300" MaxLength="500" />
							</td>
						</tr>
						<tr>
							<td align="center">
								<asp:HiddenField ID="txtIndexMov" runat="server"/>
								<asp:Button ID="btnReversarAtras" runat="server" Width="70px" Text="Atras" OnClientClick="CerrarVentanaReversarMovimiento(); return false;" />
								<asp:Button ID="btnReversarSiguiente" runat="server" Width="70px" Text="Siguiente" OnClick="btnReversarSiguiente_Click" />
							</td>
						</tr>
					</table>
				</div>
			</ContentTemplate>
		</telerik:RadWindow>
		<telerik:RadWindow ID="RadWindow1" runat="server" Height="350px"  DestroyOnClose="true" Title="Filtro" Width="600px" KeepInScreenBounds="true">
			<ContentTemplate>
				<fieldset>
					<legend><b>
						<asp:Label runat="server" Text="Busqueda Avanzada" Font-Bold="True" meta:resourcekey="LblLegendBusquedaAvanzadaResource" /></b></legend>
					<table>
						<tr>
							<td>
								<telerik:RadFilter ID="RadFilterMaster" runat="server" FilterContainerID="RadGridMaster" OnItemCommand="RadFilterMaster_ItemCommand" ShowApplyButton="False"  CssClass="RadFilter RadFilter_Windows7 RadFilter RadFilter_Windows7" />
							</td>
						</tr>
						<tr>
							<td>
								<asp:Label ID="LblMessege" runat="server" Text=""></asp:Label>
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
		<telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
			<script src="../../Scripts/RadFilterScriptBlock.js" type="text/javascript"></script>
		</telerik:RadCodeBlock>
		<br />
        <asp:HiddenField runat="server" ID="HiddenFieldMov"></asp:HiddenField>
        <asp:HiddenField runat="server" ID="hdAnular"></asp:HiddenField>
		<br />
		<asp:Button ID="cmdGuardar" runat="server" Text="Guardar" OnClick="cmdGuardar_Click" />
		<asp:Button ID="cmdGuardaryAgregar" runat="server" Text="Guardar y Agregar Otro" OnClick="cmdGuardaryAgregar_Click" />
		<asp:Button ID="cmdLimpiar" runat="server" Text="Limpiar" OnClientClick="$('form').clearForm(); return false" />
		<asp:Button ID="cmdAnularSuspender" runat="server" Text="Anular Suspender" OnClientClick="MostrarVentanaOmitir(); return false;"/>
	</div>
	<telerik:RadScriptBlock runat="server" ID="RadScriptBlock1">
		<script type="text/javascript">
		    function CerrarVentanaAnularSuspenderPregunta() {
		        var oWindow = $find("<%= RadWindowAnularSuspenderPregunta.ClientID %>");
		        oWindow.close();
		    }

		    function MostrarObservacionesReversar(index) {
		    	document.getElementById('<%= txtIndexMov.ClientID %>').value = index;
		    	var oWindow = $find("<%= RadWindowReversarMovimiento.ClientID %>");
		    	oWindow.show();
		    }
		    function CerrarVentanaReversarMovimiento() {
		    	var oWindow = $find("<%= RadWindowReversarMovimiento.ClientID %>");
		    	oWindow.close();
		    }

		    function MostrarVentanaOmitir() {
		        try {
		            var oWindow;
		            var etiqueta = document.getElementById("<%= HiddenFieldMov.ClientID %>");
		            if (etiqueta.value != "")
		                oWindow = $find("<%= RadWindowAnularSuspenderPregunta.ClientID %>");
		            else
		                oWindow = $find("<%= RadWindowAnularSuspender.ClientID %>");
		            oWindow.show();
		        }
		        catch (ex) {
		            alert('Error al intentar abrir la ventana, consulte al administrador del sistema...');
		        } 
		    }

		    function CerrarVentanaAnularSuspender() {
		        var oWindow = $find("<%= RadWindowAnularSuspender.ClientID %>");
		        oWindow.close();
		    }
		</script>
	</telerik:RadScriptBlock>
</asp:Content>
