<%@ Page Title="Detalle de FlujosServicio" Language="C#" MasterPageFile="~/Master/Site.Master" AutoEventWireup="True" CodeBehind="FlujosServicioMaestroDetalle.aspx.cs" Inherits="HConnexum.Tramitador.Sitio.Modulos.Parametrizador.FlujosServicioMaestroDetalle" meta:resourcekey="PageResource1" %>
<%@ Register Src="~/ControlesComunes/Publicacion.ascx" TagName="Publicacion" TagPrefix="hcc" %>
<%@ Register Src="~/ControlesComunes/Auditoria.ascx" TagName="Auditoria" TagPrefix="hcc" %>
<%@ Register Src="~/ControlesComunes/HorasMinutosSegundos.ascx" TagName="HorasMinutosSegundos" TagPrefix="hcc" %>
<asp:Content ID="cHead" ContentPlaceHolderID="cphHead" runat="server">
    <style type="text/css">
        html { overflow-x:hidden; }
    </style>
    <script src="../../Scripts/jquery-1.7.1.min.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            changeTextRadAlert();
        });
    </script>
</asp:Content>
<asp:Content ID="cBody" ContentPlaceHolderID="cphBody" runat="server">
	<asp:HiddenField ID="AccionC" runat="server" />
	<asp:HiddenField ID="IdFlujoServicioOculto" runat="server" />
	<asp:HiddenField ID="accionEncriptada" runat="server" />
	<div style="position: relative; width: 100%; height: 100%; bottom: 0; left: 0; z-index: 1">
		<telerik:RadAjaxManager ID="RadAjaxManager1" DefaultLoadingPanelID="RadAjaxLoadingPanel1" OnAjaxRequest="RadAjaxManager1_AjaxRequest" runat="server" meta:resourcekey="RadAjaxManager1Resource1">
			<AjaxSettings>
				<telerik:AjaxSetting AjaxControlID="RadGridDetails">
					<UpdatedControls>
						<telerik:AjaxUpdatedControl ControlID="PanelMaster" />
					</UpdatedControls>
				</telerik:AjaxSetting>
				<telerik:AjaxSetting AjaxControlID="RadGridMaster">
					<UpdatedControls>
						<telerik:AjaxUpdatedControl ControlID="RadGridMaster" />
						<telerik:AjaxUpdatedControl ControlID="PanelMaster" />
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
				<telerik:AjaxSetting AjaxControlID="Button1">
					<UpdatedControls>
						<telerik:AjaxUpdatedControl ControlID="ddlIdServicioSuscriptor" />
					</UpdatedControls>
				</telerik:AjaxSetting>
			</AjaxSettings>
		</telerik:RadAjaxManager>
		<div>
			<asp:Panel ID="PanelMaster" runat="server" meta:resourcekey="PanelMasterResource1">
				<fieldset>
					<legend>
						<asp:Label ID="lgLegendFlujosServicio" runat="server" Font-Bold="True" Text="Flujos Servicio" meta:resourcekey="lgLegendFlujosServicioResource1" />
					</legend>
					<table>
						<tr>
							<td>
								<asp:Label ID="lblIdSuscriptor" runat="server" Text="Suscriptor:" meta:resourcekey="lblIdSuscriptorResource1" />
							</td>
							<td class="style1">
								<asp:TextBox ID="TxtIdSuscriptor" runat="server" Enabled="false" meta:resourcekey="TxtIdSuscriptorResource1" />
								<asp:HiddenField ID="TxtHiddenId" runat="server" />
								<asp:HiddenField ID="TxtHiddenTipo" runat="server" />
								<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ControlToValidate="TxtIdSuscriptor" CssClass="validator" Width="25px" meta:resourcekey="RequiredFieldValidator1Resource1" />
							</td>
							<td>
								<asp:Label ID="Label2" runat="server" Text="Tipo:" meta:resourcekey="Label2Resource1" />
							</td>
							<td>
								<asp:TextBox ID="TxtTipo" Enabled="false" runat="server" meta:resourcekey="TxtTipoResource1" />
							</td>
						</tr>
						<tr>
							<td>
							</td>
							<td align="right" class="style1">
								<asp:Button ID="btnSuscriptor" runat="server" Text="+" OnClientClick="btnSuscriptor_Click(); return false;" CausesValidation="False" meta:resourcekey="btnSuscriptorResource1" />
								<asp:Button ID="btnMenos" runat="server" Text="-" OnClientClick="$('form').clearForm(); return false" CausesValidation="False" meta:resourcekey="btnMenosResource1" />
							</td>
							<td>
								<asp:Label ID="lblSlaPromedio" runat="server" Text="Sla Promedio:" meta:resourcekey="lblSlaPromedioResource1" />
							</td>
							<td>
								<hcc:HorasMinutosSegundos runat="server" ID="txtSlaPromedio" Enabled="false" IsRequired="False" Width="200" />
							</td>
						</tr>
						<tr>
							<td>
								<asp:Label ID="lblIdServicioSuscriptor" runat="server" Text="Servicio:" meta:resourcekey="lblIdServicioSuscriptorResource1" />
							</td>
							<td class="style1">
								<telerik:RadComboBox ID="ddlIdServicioSuscriptor" DataTextField="Nombre" DataValueField="Id" EmptyMessage="Seleccione..." runat="server" Culture="es-ES" meta:resourcekey="ddlIdServicioSuscriptorResource1">
								</telerik:RadComboBox>
								<asp:RequiredFieldValidator ID="rfvIdTipoTransaccion" runat="server" ErrorMessage="*" ControlToValidate="ddlIdServicioSuscriptor" CssClass="validator" Width="25px" meta:resourcekey="rfvIdTipoTransaccionResource1" />
							</td>
							<td>
								<asp:Label ID="Label1" runat="server" Text="Sla Tolerancia:" meta:resourcekey="Label1Resource1" />
							</td>
							<td>
								<hcc:HorasMinutosSegundos runat="server" ID="txtSlaTolerancia" IsRequired="True" Width="200" />
							</td>
						</tr>
						<tr>
							<td>
								<asp:Label ID="LblPasoInicial" runat="server" Text="Paso Inicial:" meta:resourcekey="LblResource1" />
							</td>
							<td>
								<telerik:RadComboBox ID="ddlIdPasoInicial" DataTextField="Nombre" DataValueField="Id" EmptyMessage="Seleccione..." runat="server" Culture="es-ES" meta:resourcekey="ddlIdPasoInicialResource1">
								</telerik:RadComboBox>
							</td>
						</tr>
						<tr>
							<td>
								<asp:Label ID="lblPrioridad" runat="server" Text="Prioridad:" meta:resourcekey="lblPrioridadResource1" />
							</td>
							<td class="style1">
								<telerik:RadComboBox ID="ddlIdPrioridad" DataValueField="Id" DataTextField="NombreValor" ErrorMessage="Campo Obligatorio" runat="server" Enabled="True" Culture="es-ES" Width="280" meta:resourcekey="ddlIdFlujoServicioResource1" EmptyMessage="Seleccione" />
								<asp:RequiredFieldValidator ID="rfvIdPrioridad" runat="server" ErrorMessage="*" ControlToValidate="ddlIdPrioridad" CssClass="validator" Width="25px" />
							</td>
							<td>
								<asp:Label ID="lblIndPublico" runat="server" Text="Publico:" meta:resourcekey="lblIndPublicoResource1" />
							</td>
							<td>
								<asp:CheckBox ID="chkIndPublico" runat="server" meta:resourcekey="chkIndPublicoResource1" />
							</td>
						</tr>
						<tr>
							<td>
								<asp:Label ID="lblVersion" runat="server" Text="Version:" meta:resourcekey="lblVersionResource1" />
							</td>
							<td class="style1">
								<asp:TextBox ID="txtVersion" runat="server" meta:resourcekey="txtVersionResource1" />
							</td>
							<td>
								<asp:Label ID="lblIndCms" runat="server" Text="CMS:" meta:resourcekey="lblIndCmsResource1" />
							</td>
							<td>
								<asp:CheckBox ID="chkIndCms" runat="server" meta:resourcekey="chkIndCmsResource1" />
							</td>
						</tr>
						<tr>
							<td>
								<asp:Label runat="server" Text="Bloque Generico en Solicitud:"/>
							</td>
							<td >
								<asp:CheckBox ID="chkIndBloqueGenericoSolicitud" runat="server"/>
							</td>
                            <td >								
							    <asp:Label ID="lblIndSimulable" runat="server" meta:resourcekey="lblIndSimulableResource1" 
                                    Text="IndSimulable:" />								
							</td>
                            <td >								
							    <asp:CheckBox ID="chkIndSimulable" runat="server" />								
							</td>
						</tr>
						<tr>
							<td>
								<asp:Label ID="lblMetodoPreSolicitud" runat="server" Text="Método Pre-Solicitud:"/>
							</td>
							<td class="style1">
								<asp:TextBox ID="txtMetodoPreSolicitud" runat="server" />
							</td>
							<td>
								<asp:Label ID="lblMetodoPostSolicitud" runat="server" Text="Método Post-Solicitud:"/>
							</td>
							<td>
								<asp:TextBox ID="txtMetodoPostSolicitud" runat="server" />
							</td>
						</tr>
						<tr>
                            <td>
								<asp:Label ID="lblIndChat" runat="server" Text="Ind Chat:"/>
							</td>
							<td class="style1">
								<asp:CheckBox ID="chkIndChat" runat="server" />
							</td>
							<td>
								<asp:Label ID="lblNomProg" runat="server" Text="Nombre del Programa:"/>
							</td>
							<td>
								<asp:TextBox ID="txtNomProg" runat="server" />
							</td>
                        </tr>
						<tr>
							<td align="center" colspan="4">
								<asp:Button ID="btnConfiguracionPaso" runat="server" Text="Configuración en XML" OnClientClick="AbrirVentanas(this); return false;" meta:resourcekey="btnConfiguracionPasoResource1" />
								<asp:Button ID="cmdOperacionalizar" runat="server" Text="Sucursales" Visible="False" OnClientClick="AbrirVentanas(this); return false;" meta:resourcekey="cmdOperacionalizarResource1" />
								<asp:Button ID="cmdAccionesPaso" runat="server" meta:resourcekey="cmdAccionesPasoResource1" OnClientClick="AbrirVentanas(this); return false;" Text="Acciones de Paso" Visible="False" />
								<asp:Button ID="btnDocumentosServicio" runat="server" OnClientClick="AbrirVentanas(this); return false;" Text="Documentos" meta:resourcekey="btnDocumentosServicioResource1" />
								<asp:Button ID="cmdSolicitud" runat="server" OnClientClick="AbrirVentanas(this); return false;" Text="Solicitud Bloques" Visible="false" />
							</td>
							<td>
								<asp:Button ID="Button1" runat="server" Text="Voy" OnClick="Button1_Click" Style="display: none" CausesValidation="False" ValidationGroup="Boton" meta:resourcekey="Button1Resource1" />
							</td>
						</tr>
					</table>
				</fieldset>
				<hcc:Publicacion ID="Publicacion" runat="server" />
				<hcc:Auditoria ID="Auditoria" runat="server" />
				<br />
				<asp:Button ID="cmdGuardar" runat="server" Text="Guardar" OnClick="cmdGuardar_Click" meta:resourcekey="cmdGuardarResource1" />
				<asp:Button ID="cmdGuardaryAgregar" runat="server" Text="Guardar y Agregar Otro" OnClick="cmdGuardaryAgregar_Click" meta:resourcekey="cmdGuardaryAgregarResource1" />
				<asp:Button ID="cmdLimpiar" runat="server" Text="Limpiar" OnClientClick="$('form').clearForm(); return false" meta:resourcekey="cmdLimpiarResource1" />
			</asp:Panel>
			<telerik:RadInputManager ID="RadInputManager1" runat="server">
				<telerik:NumericTextBoxSetting DecimalDigits="0" GroupSeparator="" BehaviorID="BehaviorIdOrigen" EmptyMessage="Escriba IdOrigen" Type="Number" Validation-IsRequired="True" ErrorMessage="Campo Obligatorio">
					<TargetControls>
						<telerik:TargetInput ControlID="TxtIdOrigen" />
					</TargetControls>
				</telerik:NumericTextBoxSetting>
				<telerik:NumericTextBoxSetting DecimalDigits="0" GroupSeparator="" BehaviorID="BehaviorIdServicioSuscriptor" EmptyMessage="Escriba IdServicioSuscriptor" Type="Number" Validation-IsRequired="True" ErrorMessage="Campo Obligatorio">
					<TargetControls>
						<telerik:TargetInput ControlID="TxtIdServicioSuscriptor" />
					</TargetControls>
				</telerik:NumericTextBoxSetting>
				<telerik:NumericTextBoxSetting DecimalDigits="0" GroupSeparator="" BehaviorID="BehaivorId" EmptyMessage="Escriba Prioridad" Type="Number" Validation-IsRequired="true" ErrorMessage="Campo Obligatorio">
					<TargetControls>
						<telerik:TargetInput ControlID="txtPrioridad" />
					</TargetControls>
				</telerik:NumericTextBoxSetting>
				<telerik:NumericTextBoxSetting DecimalDigits="0" GroupSeparator="" BehaviorID="BehaivorVersion" EmptyMessage="Escriba Version" Type="Number" Validation-IsRequired="true" ErrorMessage="Campo Obligatorio">
					<TargetControls>
						<telerik:TargetInput ControlID="TxtVersion" />
					</TargetControls>
				</telerik:NumericTextBoxSetting>
			</telerik:RadInputManager>
			<br />
			<br />
			<telerik:RadGrid ID="RadGridMaster" runat="server" AutoGenerateColumns="False" Width="100%" CellSpacing="0" Culture="es-ES" GridLines="None" AllowCustomPaging="True" AllowPaging="True" AllowMultiRowSelection="True" AllowSorting="True" OnPageIndexChanged="RadGridMaster_PageIndexChanged"
				OnSortCommand="RadGridMaster_SortCommand" OnPageSizeChanged="RadGridMaster_PageSizeChanged" OnNeedDataSource="RadGridMaster_NeedDataSource" OnItemCommand="RadGridMaster_ItemCommand" Style="margin-right: 0px">
				<ClientSettings EnableRowHoverStyle="true">
					<Selecting AllowRowSelect="True" />
					<ClientEvents OnRowDblClick="validarRegistro" />
					<Scrolling AllowScroll="True" UseStaticHeaders="True" />
				</ClientSettings>
				<MasterTableView CommandItemDisplay="Top" NoMasterRecordsText="No se encontraron registros" DataKeyNames="IdEncriptado" ClientDataKeyNames="IdEncriptado" width="100%">
					<CommandItemSettings ExportToPdfText="Export to PDF"></CommandItemSettings>
					<RowIndicatorColumn FilterControlAltText="Filter RowIndicator column">
						<HeaderStyle Width="20px"></HeaderStyle>
					</RowIndicatorColumn>
					<ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column">
						<HeaderStyle Width="20px"></HeaderStyle>
					</ExpandCollapseColumn>
					<Columns>
						<telerik:GridBoundColumn DataField="Nombre" FilterControlAltText="Filtrar columna Nombre" HeaderText="Etapa" UniqueName="Nombre" meta:resourcekey="GridBoundColumnResource1">
						</telerik:GridBoundColumn>
						<telerik:GridCheckBoxColumn DataField="IndVigente" FilterControlAltText="Filtrar columna IndVigente" HeaderText="Publicar" UniqueName="IndVigente" meta:resourcekey="GridCheckBoxColumnResource1">
						</telerik:GridCheckBoxColumn>
						<telerik:GridBoundColumn DataField="FechaValidez" FilterControlAltText="Filtrar columna FechaValidez" DataFormatString="{0:d}" HeaderText="Fecha de Validez" UniqueName="FechaValidez" meta:resourcekey="GridBoundColumnResource2">
						</telerik:GridBoundColumn>
						<telerik:GridCheckBoxColumn DataField="IndEliminado" FilterControlAltText="Filtrar columna IdEliminado" HeaderText="Eliminado" UniqueName="IndEliminado">
						</telerik:GridCheckBoxColumn>
						<telerik:GridTemplateColumn HeaderText="Tomado" UniqueName="Tomado" HeaderStyle-Width="70px" ItemStyle-HorizontalAlign="Center" meta:resourcekey="GridTemplateColumnResource1">
							<ItemTemplate>
								<asp:Image runat="server" ID="imgTomado" ImageUrl='<%# this.ResolveUrl(Eval("Tomado", "~/Imagenes/{0}.png")) %>' ToolTip='<%# Eval("UsuarioTomado") %>' meta:resourcekey="imgTomadoResource1" />
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
									<telerik:RadToolBar ID="RadToolBar1" OnClientButtonClicking="ClientButtonClicking" OnClientButtonClicked="PanelBarItemClicked" runat="server" OnButtonClick="RadToolBar1_ButtonClick1" meta:resourcekey="RadToolBar1Resource1">
										<Items>
											<telerik:RadToolBarButton runat="server" CommandName="Refrescar" ImagePosition="Right" ImageUrl="~/Imagenes/Refresh.gif" Text="Refrescar" meta:resourcekey="RadToolBarButtonResource1" Owner="">
											</telerik:RadToolBarButton>
											<telerik:RadToolBarButton runat="server" CommandName="OpenRadFilter" ImagePosition="Right" ImageUrl="~/Imagenes/Filter.gif" Text="Mostrar Filtro" meta:resourcekey="RadToolBarButtonResource2" Owner="">
											</telerik:RadToolBarButton>
											<telerik:RadToolBarButton runat="server" Text="Ver Detalle" CommandName="ViewDetails" PostBack="False" ImagePosition="Right" ImageUrl="~/Imagenes/icon_lupa18x18.png" meta:resourcekey="RadToolBarButtonResource3" Owner="">
											</telerik:RadToolBarButton>
											<telerik:RadToolBarButton runat="server" CommandName="Add" Text="Agregar" ImagePosition="Right" PostBack="False" ImageUrl="~/Imagenes/AddRecord.gif" meta:resourcekey="RadToolBarButtonResource4" Owner="">
											</telerik:RadToolBarButton>
											<telerik:RadToolBarButton runat="server" CommandName="Edit" ImagePosition="Right" PostBack="False" ImageUrl="~/Imagenes/Edit.gif" Text="Editar" meta:resourcekey="RadToolBarButtonResource5" Owner="">
											</telerik:RadToolBarButton>
											<telerik:RadToolBarButton runat="server" CommandName="Eliminar" ImagePosition="Right" ImageUrl="~/Imagenes/Delete.gif" Text="Eliminar" meta:resourcekey="RadToolBarButtonResource6" Owner="">
											</telerik:RadToolBarButton>
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
			<div style="display: none">
				<telerik:RadButton ID="btnActivarEliminado" runat="server" Text="" OnClick="btnActivarEliminado_Click">
				</telerik:RadButton>
			</div>
			<telerik:RadWindow ID="RadWindow1" runat="server" Height="350px" DestroyOnClose="True" Title="Filtro" Width="600px" KeepInScreenBounds="True">
				<ContentTemplate>
					<fieldset>
						<legend>
							<asp:Label ID="lgLegendBusquedaAvanzada" runat="server" Font-Bold="True" Text="Busqueda Avanzada" meta:resourcekey="lgLegendBusquedaAvanzadaResource1"></asp:Label>
						</legend>
						<table>
							<tr>
								<td>
									<telerik:RadFilter ID="RadFilterMaster" runat="server" FilterContainerID="RadGridMaster" OnItemCommand="RadFilterMaster_ItemCommand" ShowApplyButton="False" CssClass="RadFilter RadFilter_Windows7 RadFilter RadFilter_Windows7 " Culture="es-ES" meta:resourcekey="RadFilterMasterResource1">
									</telerik:RadFilter>
								</td>
							</tr>
							<tr>
								<td>
									<asp:Label ID="LblMessege" runat="server" meta:resourcekey="LblMessegeResource1"></asp:Label>
								</td>
							</tr>
							<tr>
								<td>
									<asp:ImageButton ID="ApplyButton" runat="server" ImageUrl="~/Imagenes/Aceptar.gif" OnClick="ApplyButton_Click" OnClientClick="hideFilterBuilderDialog()" meta:resourcekey="ApplyButtonResource1" />
								</td>
							</tr>
						</table>
					</fieldset>
				</ContentTemplate>
			</telerik:RadWindow>
			<telerik:RadWindow ID="RadWindow2" runat="server" Height="350px" DestroyOnClose="True" Title="SeleccionandoSuscriptor" Width="600px" KeepInScreenBounds="True" NavigateUrl="SuscriptorLista.aspx" meta:resourcekey="RadWindow2Resource1">
			</telerik:RadWindow>
			<telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
				<script src="../../Scripts/RadFilterScriptBlock.js" type="text/javascript"></script>
			</telerik:RadCodeBlock>
			<br />
			<br />
		</div>
	</div>
	<asp:Button ID="btnConfirm" runat="server" OnClick="ConfirmOK" />
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
			var ventanaDetalle = "Modulos/Parametrizador/EtapaMaestroDetalle.aspx?IdMenu=" + idMenu + "&";
			var nombreBoton = '<%=btnActivarEliminado.ClientID%>';

			$(window).load(function () {
				changeTextRadAlert();
				$("#" + '<%=  Button1.ClientID %>').hide();
				$("#" + '<%=  btnConfirm.ClientID %>').hide();
			});

			function AbrirVentanas(sender) {
				var wnd = GetRadWindow();
				var idMenu = '<%= IdMenuEncriptado %>';
				switch ($(sender).attr("Id")) {
					case '<%=  btnConfiguracionPaso.ClientID %>':
						wnd.setUrl("Modulos/Parametrizador/ConfiguracionXmlGenerales.aspx?IdMenu=" + idMenu + "&AccionC=" + $("#" + '<%=AccionC.ClientID%>').val() + "&accion=" + $("#" + '<%=accionEncriptada.ClientID%>').val() + "&id=" + $("#" + '<%=IdFlujoServicioOculto.ClientID%>').val());
						//wnd.setUrl("Modulos/Parametrizador/ConfiguracionPaso.aspx?IdMenu=" + idMenu + "&AccionC=" + $("#" + '<%=AccionC.ClientID%>').val() + "&accion=" + $("#" + '<%=accionEncriptada.ClientID%>').val());
						break;
					case '<%=  btnDocumentosServicio.ClientID %>':
						wnd.setUrl("Modulos/Parametrizador/DocumentosServicioLista.aspx?IdMenu=" + idMenu + "&id=" + $("#" + '<%=IdFlujoServicioOculto.ClientID%>').val());
						break;
					case '<%=  cmdAccionesPaso.ClientID %>':
						wnd.setUrl("Modulos/Parametrizador/AccionesdelPasoLista.aspx?IdMenu=" + idMenu + "&id=" + $("#" + '<%=IdFlujoServicioOculto.ClientID%>').val());
						break;
					case '<%=  cmdOperacionalizar.ClientID %>':
						wnd.setUrl("Modulos/Parametrizador/ServicioSucursalLista.aspx?IdMenu=" + idMenu + "&id=" + $("#" + '<%=IdFlujoServicioOculto.ClientID%>').val());
						break;
					case '<%=  cmdSolicitud.ClientID %>':
						wnd.setUrl("Modulos/Parametrizador/SolicitudBloqueMaestroDetalle.aspx?IdMenu=" + idMenu + "&id=" + $("#" + '<%=IdFlujoServicioOculto.ClientID%>').val());
						break;
				}
			}

			function btnSuscriptor_Click() {
				var wnd;
				var idMenu = '<%= IdMenuEncriptado %>';
				wnd = window.radopen("SuscriptorLista.aspx?IdMenu=" + idMenu, null);
				wnd.set_modal(true);
				wnd.setSize(800, 450);
			}

			function Confirm(args) {
				if (args == false) {
					var objBtn = document.getElementById("<%= btnConfirm.ClientID %>");
					__doPostBack(objBtn.name, "")
				}
			}

			function IrAnterior() {
				var wnd = GetRadWindow();
				wnd.setUrl("Modulos/Parametrizador/FlujosServicioLista.aspx?IdMenu=" + idMenu);
			}
		</script>
	</telerik:RadScriptBlock>
</asp:Content>
