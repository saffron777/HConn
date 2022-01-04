<%@ Page Title="Detalle de Paso" MasterPageFile="~/Master/Site.Master" Language="C#" AutoEventWireup="True" CodeBehind="PasoMaestroDetalle.aspx.cs" Inherits="HConnexum.Tramitador.Sitio.Modulos.Parametrizador.PasoMaestroDetalle" meta:resourcekey="PageResource1" %>
<%@ Register Src="~/ControlesComunes/MultilineCounter.ascx" TagName="MultilineCounter" TagPrefix="hcc" %>
<%@ Register Src="~/ControlesComunes/HorasMinutosSegundos.ascx" TagName="HorasMinutosSegundos" TagPrefix="hcc" %>
<%@ Register Src="~/ControlesComunes/Publicacion.ascx" TagName="Publicacion" TagPrefix="hcc" %>
<%@ Register Src="~/ControlesComunes/Auditoria.ascx" TagName="Auditoria" TagPrefix="hcc" %>
<asp:Content ID="cHead" ContentPlaceHolderID="cphHead" runat="server">
	<style type="text/css">
        html { overflow-x:hidden; }
		.style1
		{
			width: 185px;
		}
		.style2
		{
			width: 176px;
		}
		.style3
		{
			width: 99px;
		}
		.style4
		{
			width: 79px;
		}
		.style5
		{
			width: 141px;
		}
		.style6
		{
			width: 90px;
		}
		.style7
		{
			width: 168px;
		}
		.style8
		{
			width: 262px;
		}
		.style10
		{
			width: 140px;
		}
		.style11
		{
			width: 163px;
		}
		.style12
		{
			width: 191px;
		}
		.style13
		{
			width: 165px;
		}
		.style14
		{
			width: 148px;
		}
		.style15
		{
			width: 92px;
		}
		.style16
		{
			width: 180px;
		}
	</style>
</asp:Content>
<asp:Content ID="cBody" ContentPlaceHolderID="cphBody" runat="server">
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
						<telerik:AjaxUpdatedControl ControlID="RadGridMasterS" />
					</UpdatedControls>
				</telerik:AjaxSetting>
				<telerik:AjaxSetting AjaxControlID="RadFilterMaster">
					<UpdatedControls>
						<telerik:AjaxUpdatedControl ControlID="RadFilterMasterS" />
						<telerik:AjaxUpdatedControl ControlID="LblMessegeS" />
					</UpdatedControls>
				</telerik:AjaxSetting>
				<telerik:AjaxSetting AjaxControlID="ApplyButton">
					<UpdatedControls>
						<telerik:AjaxUpdatedControl ControlID="RadGridMasterS" />
						<telerik:AjaxUpdatedControl ControlID="RadFilterMasterS" />
					</UpdatedControls>
				</telerik:AjaxSetting>
				<telerik:AjaxSetting AjaxControlID="RadAjaxManager1">
					<UpdatedControls>
						<telerik:AjaxUpdatedControl ControlID="RadGridMasterS" />
						<telerik:AjaxUpdatedControl ControlID="RadGridMasterB" />
					</UpdatedControls>
				</telerik:AjaxSetting>
			</AjaxSettings>
		</telerik:RadAjaxManager>
		<div>
			<asp:Panel ID="PanelMaster" runat="server" meta:resourcekey="PanelMasterResource1">
				<fieldset>
					<legend><b>
						<asp:Label runat="server" ID="lblCabecera" Text="Datos básicos" meta:resourcekey="lblCabeceraResource1"></asp:Label>
					</b></legend>
					<table>
						<tr>
							<td align="right" class="style3">
								<asp:Label ID="lblNombreServicio" runat="server" Text="Servicio:" meta:resourcekey="lblNombreServicioResource1" />
							</td>
							<td class="style2">
								<asp:TextBox ID="txtNombreServicio" runat="server" ReadOnly="True" meta:resourcekey="txtNombreServicioResource1"></asp:TextBox>
							</td>
							<td align="right" class="style4">
								<asp:Label ID="lblNombreEstatus" runat="server" Text="Estatus:" meta:resourcekey="lblNombreEstatusResource1" />
							</td>
							<td class="style1">
								<asp:TextBox ID="txtNombreEstatus" runat="server" ReadOnly="True" meta:resourcekey="txtNombreEstatusResource1"></asp:TextBox>
							</td>
						</tr>
						<tr>
							<td align="right" class="style3">
								<asp:Label ID="lblNombreEtapa" runat="server" Text="Etapa:" meta:resourcekey="lblNombreEtapaResource1" />
							</td>
							<td class="style2">
								<asp:TextBox ID="txtNombreEtapa" runat="server" ReadOnly="True" meta:resourcekey="txtNombreEtapaResource1"></asp:TextBox>
							</td>
							<td align="right" class="style4">
								<asp:Label ID="lblNombreVersion" runat="server" Text="Versión:" meta:resourcekey="lblNombreVersionResource1" />
							</td>
							<td class="style1">
								<asp:TextBox ID="txtNombreVersion" runat="server" ReadOnly="True" meta:resourcekey="txtNombreVersionResource1"></asp:TextBox>
							</td>
						</tr>
					</table>
				</fieldset>
				<br />
				<fieldset>
					<legend>
						<asp:Label ID="lblPaso" runat="server" Text="Paso" Font-Bold="True" meta:resourcekey="lblPasoResource1"></asp:Label>
					</legend>
					<table align="center">
						<tr>
							<td align="right" class="style16">
								<asp:Label ID="lblIdTipoPaso" runat="server" Text="Tipo:" meta:resourcekey="lblIdTipoPasoResource1" />
							</td>
							<td colspan="3">
								<telerik:RadComboBox ID="ddlIdTipoPaso" DataValueField="Id" EnableLoadOnDemand="false" DataTextField="Descripcion" EmptyMessage="Seleccione" ErrorMessage="Campo Obligatorio" runat="server" Width="200px" OnClientSelectedIndexChanged="ActivarTipoPaso" CausesValidation="False"
									Culture="es-ES" meta:resourcekey="ddlIdTipoPasoResource1" />
								<asp:RequiredFieldValidator ID="rfvIdTipoPaso" runat="server" ErrorMessage="*" ControlToValidate="ddlIdTipoPaso" CssClass="validator" Width="25px" meta:resourcekey="rfvIdTipoPasoResource1" />
							</td>
							<td align="right" class="style16">
								<asp:Label runat="server" Text="Estatus Inicial:" />
							</td>
							<td colspan="3">
								<telerik:RadComboBox ID="ddlIdEstatusInicial" DataValueField="Id" EnableLoadOnDemand="false" DataTextField="NombreValor" EmptyMessage="Seleccione" runat="server" Width="200px" />
							</td>
						</tr>
						<tr>
							<td align="right" class="style5">
								<asp:Label ID="lblNombre" runat="server" Text="Nombre:" meta:resourcekey="lblNombreResource1" />
							</td>
							<td colspan="3">
								<asp:TextBox ID="txtNombre" runat="server" Width="200px" MaxLength="50" meta:resourcekey="txtNombreResource1" />
							</td>
							<td align="right" class="style16">
								<asp:Label ID="lblObservacion" runat="server" Text="Observación:" meta:resourcekey="lblObservacionResource1" />
							</td>
							<td class="style8" rowspan="3">
								<hcc:MultilineCounter runat="server" ID="txtObservacion" TextMode="MultiLine" Width="200" MaxLength="255" ErrorMessage="Campo Obligatorio" IsRequired="true" />
							</td>
						</tr>
						<tr>
							<td align="right" class="style5">
								<asp:Label ID="lblOrden" runat="server" Text="Orden:" meta:resourcekey="lblOrdenResource1" />
							</td>
							<td colspan="3">
								<asp:TextBox ID="txtOrden" runat="server" Width="80px" meta:resourcekey="txtOrdenResource1" />
							</td>
						</tr>
						<tr>
							<td align="right" class="style5">
								&nbsp;
							</td>
							<td colspan="3">
								&nbsp;
							</td>
						</tr>
						<tr>
							<td align="right" class="style5">
								<asp:Label ID="lblEtiqSincroIn" runat="server" Text="Etiqueta entrada:" meta:resourcekey="lblEtiqSincroInResource1" />
							</td>
							<td colspan="3">
								<asp:TextBox ID="txtEtiqSincroIn" runat="server" Width="200px" MaxLength="10" meta:resourcekey="txtEtiqSincroInResource1" />
							</td>
							<td align="right" class="style16">
								<asp:Label ID="lblEtiqSincroOut" runat="server" Text="Etiqueta salida:" meta:resourcekey="lblEtiqSincroOutResource1" />
							</td>
							<td class="style8">
								<asp:TextBox ID="txtEtiqSincroOut" runat="server" Width="200px" MaxLength="10" meta:resourcekey="txtEtiqSincroOutResource1" />
							</td>
						</tr>
						<tr>
							<td align="right" class="style5">
								<asp:Label ID="lblSlaTolerancia" runat="server" Text="SLA Tolerancia:" meta:resourcekey="lblSlaToleranciaResource1" />
							</td>
							<td colspan="3">
								<hcc:HorasMinutosSegundos runat="server" ID="txtSlaTolerancia" Width="200" IsRequired="True" />
							</td>
							<td align="right" class="style16">
								<asp:Label ID="lblPgmObtieneRespuestas" runat="server" Text="Programa Obtiene Respuesta:" meta:resourcekey="lblPgmObtieneRespuestasResource1" />
							</td>
							<td class="style8">
								<asp:TextBox ID="txtPgmObtieneRespuestas" runat="server" Width="200px" MaxLength="2500" meta:resourcekey="txtPgmObtieneRespuestasResource1" />
							</td>
						</tr>
						<tr>
							<td align="right" class="style5">
								<asp:Label ID="lblCantidadRepeticion" runat="server" Text="Repetición:" meta:resourcekey="lblCantidadRepeticionResource1" />
							</td>
							<td>
								<asp:TextBox ID="txtCantidadRepeticion" runat="server" Width="50px" meta:resourcekey="txtCantidadRepeticionResource1" />
							</td>
							<td align="right" class="style15">
								<asp:Label ID="lblReintentos" runat="server" Text="Reintentos:" meta:resourcekey="lblReintentosResource1" />
							</td>
							<td class="style6">
								<asp:TextBox ID="txtReintentos" runat="server" Width="50px" meta:resourcekey="txtReintentosResource1" />
							</td>
							<td align="right" class="style16">
								<asp:Label ID="lblPorcSlaCritico" runat="server" Text="% SLA Crítico:" meta:resourcekey="lblPorcSlaCriticoResource1" />
							</td>
							<td class="style8">
								<asp:TextBox ID="txtPorcSlaCritico" runat="server" Width="200px" meta:resourcekey="txtPorcSlaCriticoResource1" />
							</td>
						</tr>
						<tr id="trAlerta">
							<td align="right" class="style5">
								<asp:Label ID="lblAlerta" runat="server" Text="Alerta:" meta:resourcekey="lblAlertaResource1" />
							</td>
							<td colspan="5">
								<telerik:RadComboBox ID="ddlAlerta" DataValueField="Id" DataTextField="Nombre" EmptyMessage="Seleccione" runat="server" Width="200px" Culture="es-ES" meta:resourcekey="ddlAlertaResource1" />
								<asp:RequiredFieldValidator ID="rfvAlerta" runat="server" ErrorMessage="*" ControlToValidate="ddlAlerta" CssClass="validator" Width="25px" meta:resourcekey="rfvAlertaResource1" />
							</td>
						</tr>
						<tr id="trSubServicio">
							<td align="right" class="style5">
								<asp:Label ID="lblIdSubServicio" runat="server" Text="Sub-servicio:" Enabled="False" meta:resourcekey="lblIdSubServicioResource1" />
							</td>
							<td colspan="5">
								<telerik:RadComboBox ID="ddlIdSubServicio" DataValueField="Id" DataTextField="NombreServicioSuscriptor" EmptyMessage="Seleccione" runat="server" Width="200px" Culture="es-ES" meta:resourcekey="ddlIdSubServicioResource1" />
								<asp:RequiredFieldValidator ID="rfvIdSubServicio" runat="server" ErrorMessage="*" ControlToValidate="ddlIdSubServicio" CssClass="validator" Width="25px" meta:resourcekey="rfvIdSubServicioResource1" />
							</td>
						</tr>
						<tr id="trPrograma">
							<td align="right" class="style5">
								<asp:Label ID="lblURL" runat="server" Text="URL:" meta:resourcekey="lblURLResource1" />
							</td>
							<td colspan="3">
								<asp:TextBox ID="txtURL" runat="server" Width="200px" Enabled="False" MaxLength="2500"/>
							</td>
							<td align="right" class="style16">
								<asp:Label ID="lblMetodo" runat="server" Text="Método:" meta:resourcekey="lblMetodoResource1" />
							</td>
							<td class="style8">
								<asp:TextBox ID="txtMetodo" runat="server" Width="200px" Enabled="False" MaxLength="2500" />
							</td>
						</tr>
						<tr id="trAsignacion">
							<td align="right" class="style5">
								<asp:Label runat="server" Text="Metodo de Asignacion: " />
							</td>
							<td colspan="3">
								<asp:TextBox ID="txtMetodoAsisgnacion" runat="server" Enabled="False" MaxLength="2500" Width="200px" />
							</td>
							<td align="right" class="style16">
								<asp:Label runat="server" Text="M&eacute;todo de Asignaci&oacute;n (cha)" />
							</td>
							<td class="style8">
								<asp:TextBox ID="txtAsignacionCHA" runat="server" Width="200px" MaxLength="100" ToolTip="M&eacute;todo crea atributos de Asignaci&oacute;n (Cha)"/>
							</td>
						</tr>
					</table>
					<br />
					<table align="center">
						<tr>
							<td class="style11" align="right">
								<asp:CheckBox ID="chkIndIniciaEtapa" runat="server" Text="Inicia:" TextAlign="Left" meta:resourcekey="chkIndIniciaEtapaResource1" />
							</td>
							<td class="style14" align="right">
								<asp:CheckBox ID="chkIndSeguimiento" runat="server" Text="Seguimiento:" TextAlign="Left" meta:resourcekey="chkIndSeguimientoResource1" />
							</td>
							<td class="style10" align="right">
								<asp:CheckBox ID="chkIndAgendable" runat="server" Text="Agendable:" TextAlign="Left" onclick="changeChkAgendable();" meta:resourcekey="chkIndAgendableResource1" />
							</td>
							<td class="style12" align="right">
								<asp:CheckBox ID="chkIndRequiereRespuesta" runat="server" Text="Espera respuesta:" TextAlign="Left" meta:resourcekey="chkIndRequiereRespuestaResource1" />
							</td>
							<td class="style10" align="right">
								<asp:CheckBox ID="chkIndEncadenado" runat="server" Text="Encadenado:" TextAlign="Left" meta:resourcekey="chkIndEncadenadoResource1" />
							</td>
						</tr>
						<tr>
							<td class="style11" align="right">
								<asp:CheckBox ID="chkIndSegSubServicio" runat="server" Text="Seg. Sub-servicio:" TextAlign="Left" Enabled="False" meta:resourcekey="chkIndSegSubServicioResource1" />
							</td>
							<td class="style14" align="right">
								<asp:CheckBox ID="chkIndObligatorio" runat="server" Text="Obligatorio:" TextAlign="Left" meta:resourcekey="chkIndObligatorioResource1" />
							</td>
							<td class="style10" align="right">
								<asp:CheckBox ID="chkIndCerrarEtapa" runat="server" Text="Cierra etapa:" TextAlign="Left" meta:resourcekey="chkIndCerrarEtapaResource1" />
							</td>
							<td class="style13" align="right">
								<asp:CheckBox ID="chkIndCerrarServicio" runat="server" Text="Cierra servicio:" TextAlign="Left" meta:resourcekey="chkIndCerrarServicioResource1" />
							</td>
							<td align="right">
							    <asp:CheckBox ID="chkIndAnulacion" runat="server" 
                                    meta:resourcekey="chkIndEncadenadoResource1" Text="Anulado" TextAlign="Left" />
							</td>
						</tr>
					</table>
				</fieldset>
				<hcc:Publicacion ID="Publicacion" runat="server" />
				<hcc:Auditoria ID="Auditoria" runat="server" />
				<asp:Panel runat="server" ID="botonera" meta:resourcekey="botoneraResource1">
					<table cellspacing="10">
						<tr>
							<td>
								<asp:Button ID="btnDocumentos" runat="server" Text="Documentos" OnClientClick="btnDocumentos_Click(); return false;" meta:resourcekey="btnDocumentosResource1" />
							</td>
							<td>
								<asp:Button ID="btnChaPaso" runat="server" Text="CHA del Paso" OnClientClick="btnChaPaso_Click(); return false;" meta:resourcekey="btnChaPasoResource1" />
							</td>
							<td>
								<asp:Button ID="btnConfigurarCorreo" runat="server" Text="Configurar Email" disabled="disabled" OnClientClick="btnConfigurarCorreo_Click(); return false;" meta:resourcekey="btnConfigurarCorreoResource1" />
							</td>
							<td>
								<asp:Button ID="btnConfigurarSMS" runat="server" Text="Configurar SMS" disabled="disabled" OnClientClick="btnConfigurarSMS_Click(); return false;" meta:resourcekey="btnConfigurarSMSResource1" />
							</td>
							<td>
								<asp:Button ID="btnAgenda" runat="server" Text="Agenda" disabled="disabled" OnClientClick="btnAgenda_Click(); return false;" meta:resourcekey="btnAgendaResource1" />
							</td>
							<td>
								<asp:Button ID="btnBloques" runat="server" Text="Bloques" disabled="disabled" OnClientClick="btnBloques_Click(); return false;" meta:resourcekey="btnBloquesResource1" />
							</td>
						</tr>
					</table>
				</asp:Panel>
			</asp:Panel>
			<telerik:RadInputManager ID="RadInputManager1" runat="server">
				<telerik:TextBoxSetting BehaviorID="BehaviorNombre" EmptyMessage="Escriba Nombre" Validation-IsRequired="True" ErrorMessage="Campo Obligatorio">
					<TargetControls>
						<telerik:TargetInput ControlID="txtNombre" />
					</TargetControls>
				</telerik:TextBoxSetting>
				<telerik:NumericTextBoxSetting DecimalDigits="0" MaxValue="32767" GroupSeparator="" BehaviorID="BehaviorCantidadRepeticion" EmptyMessage="" Type="Number" Validation-IsRequired="True" ErrorMessage="Campo Obligatorio">
					<TargetControls>
						<telerik:TargetInput ControlID="txtCantidadRepeticion" />
					</TargetControls>
				</telerik:NumericTextBoxSetting>
				<telerik:NumericTextBoxSetting DecimalDigits="0" MaxValue="255" GroupSeparator="" BehaviorID="BehaviorReintentos" EmptyMessage="" Type="Number" Validation-IsRequired="True" ErrorMessage="Campo Obligatorio">
					<TargetControls>
						<telerik:TargetInput ControlID="txtReintentos" />
					</TargetControls>
				</telerik:NumericTextBoxSetting>
				<telerik:NumericTextBoxSetting DecimalDigits="0" GroupSeparator="" MaxValue="100" BehaviorID="BehaviorPorcSlaCritico" EmptyMessage="" Type="Number" Validation-IsRequired="True" ErrorMessage="Campo Obligatorio">
					<TargetControls>
						<telerik:TargetInput ControlID="txtPorcSlaCritico" />
					</TargetControls>
				</telerik:NumericTextBoxSetting>
				<telerik:NumericTextBoxSetting DecimalDigits="0" GroupSeparator="" MaxValue="2147483647" BehaviorID="BehaviorOrden" EmptyMessage="Escriba Orden" Type="Number" Validation-IsRequired="True" ErrorMessage="Campo Obligatorio">
					<TargetControls>
						<telerik:TargetInput ControlID="txtOrden" />
					</TargetControls>
				</telerik:NumericTextBoxSetting>
				<telerik:TextBoxSetting BehaviorID="BehaviorPgmObtieneRespuestas" EmptyMessage="" Validation-IsRequired="False" ErrorMessage="Programa Obligatorio">
					<TargetControls>
						<telerik:TargetInput ControlID="txtPgmObtieneRespuestas" />
					</TargetControls>
				</telerik:TextBoxSetting>
				<telerik:TextBoxSetting BehaviorID="BehaviorURL" EmptyMessage="" Validation-IsRequired="False" ErrorMessage="URL o Método Obligatorio">
					<TargetControls>
						<telerik:TargetInput ControlID="txtURL" />
					</TargetControls>
				</telerik:TextBoxSetting>
				<telerik:TextBoxSetting BehaviorID="BehaviorMetodo" EmptyMessage="" Validation-IsRequired="False" ErrorMessage="URL o Método Obligatorio">
					<TargetControls>
						<telerik:TargetInput ControlID="txtMetodo" />
					</TargetControls>
				</telerik:TextBoxSetting>
				<telerik:TextBoxSetting BehaviorID="BehaviorEtiqSincroIn" EmptyMessage="" Validation-IsRequired="False" ErrorMessage="Etiqueta de entrada o salida Obligatorio">
					<TargetControls>
						<telerik:TargetInput ControlID="txtEtiqSincroIn" />
					</TargetControls>
				</telerik:TextBoxSetting>
				<telerik:TextBoxSetting BehaviorID="BehaviorEtiqSincroOut" EmptyMessage="" Validation-IsRequired="False" ErrorMessage="Etiqueta de entrada o salida Obligatorio">
					<TargetControls>
						<telerik:TargetInput ControlID="txtEtiqSincroOut" />
					</TargetControls>
				</telerik:TextBoxSetting>
			</telerik:RadInputManager>
			<br />
			<br />
			<telerik:RadScriptBlock runat="server" ID="RadScriptBlock2">
				<script type="text/javascript">
					var nombreVentana = '<%=ventanaId %>';
					var nombreGrid = '<%= clientId %>';
					var nombreRadAjaxManager = '<%= RadAjaxManager1.ClientID %>';
					var nombreRadFilter = '<%=filtroId %>';
					var idMaster = "<%= idMaster %>";
					var AccionAgregar = "<%= AccionAgregar %>";
					var AccionModificar = "<%= AccionModificar %>";
					var AccionVer = "<%= AccionVer %>";
					var ventanaDetalle = '<%=ventanaDetalle %>';
				</script>
			</telerik:RadScriptBlock>
			<telerik:RadGrid ID="RadGridMaster" runat="server" AutoGenerateColumns="False" Width="100%" CellSpacing="0" Culture="es-ES" GridLines="None" AllowCustomPaging="True" AllowPaging="True" AllowMultiRowSelection="True" AllowSorting="True" OnPageIndexChanged="RadGridMaster_PageIndexChanged"
				OnSortCommand="RadGridMaster_SortCommand" OnPageSizeChanged="RadGridMaster_PageSizeChanged" OnNeedDataSource="RadGridMaster_NeedDataSource" OnItemCommand="RadGridMaster_ItemCommand">
				<ClientSettings EnableRowHoverStyle="true">
					<Selecting AllowRowSelect="True" />
					<ClientEvents OnRowDblClick="ShowMessage" />
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
						<telerik:GridBoundColumn DataField="NombreRespuesta" FilterControlAltText="Filtrar columna NombreRespuesta" HeaderText="Respuesta" UniqueName="NombreRespuesta" meta:resourcekey="GridBoundColumnResource1">
						</telerik:GridBoundColumn>
						<telerik:GridCheckBoxColumn DataField="IndCierre" FilterControlAltText="Filtrar columna IndCierre" HeaderText="Cierre" UniqueName="IndCierre" meta:resourcekey="GridCheckBoxColumnResource1">
						</telerik:GridCheckBoxColumn>
						<telerik:GridBoundColumn DataField="Orden" FilterControlAltText="Filtrar columna Secuencia" DataFormatString="{0:N0}" HeaderText="Posición" UniqueName="Orden" meta:resourcekey="GridBoundColumnResource2">
						</telerik:GridBoundColumn>
						<telerik:GridCheckBoxColumn DataField="IndVigente" FilterControlAltText="Filtrar columna IndVigente" HeaderText="Publicar" UniqueName="IndVigente" meta:resourcekey="GridCheckBoxColumnResource2">
						</telerik:GridCheckBoxColumn>
						<telerik:GridBoundColumn DataField="FechaValidez" FilterControlAltText="Filtrar columna FechaValidez" DataFormatString="{0:d}" HeaderText="Fecha Validez" UniqueName="FechaValidez" meta:resourcekey="GridBoundColumnResource3">
						</telerik:GridBoundColumn>
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
			<telerik:RadWindow ID="RadWindow1" runat="server" Height="350px" DestroyOnClose="True" Title="Filtro" Width="600px" KeepInScreenBounds="True">
				<ContentTemplate>
					<fieldset>
						<legend>
							<asp:Label ID="lblBusquedaAvanzada" runat="server" Text="Búsqueda Avanzada" Font-Bold="True" meta:resourcekey="lblBusquedaAvanzadaResource1"></asp:Label>
						</legend>
						<table>
							<tr>
								<td>
									<telerik:RadFilter ID="RadFilterMaster" runat="server" FilterContainerID="RadGridMaster" OnItemCommand="RadFilterMaster_ItemCommand" ShowApplyButton="False" CssClass="RadFilter RadFilter_Windows7 RadFilter RadFilter_Windows7 " Culture="es-ES">
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
			<telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
				<script src="../../Scripts/RadFilterScriptBlock.js" type="text/javascript"></script>
			</telerik:RadCodeBlock>
			<br />
			<br />
			<asp:Button ID="cmdGuardar" runat="server" Text="Guardar" OnClick="cmdGuardar_Click" OnClientClick="ValidarControles(this);" meta:resourcekey="cmdGuardarResource1" />
			<asp:Button ID="cmdGuardaryAgregar" runat="server" Text="Guardar y Agregar Otro" OnClick="cmdGuardaryAgregar_Click" OnClientClick="ValidarControles(this);" meta:resourcekey="cmdGuardaryAgregarResource1" />
			<asp:Button ID="cmdLimpiar" runat="server" Text="Limpiar" OnClientClick="$('form').clearForm(); return false" meta:resourcekey="cmdLimpiarResource1" />
		</div>
	</div>
	<telerik:RadWindow CssClass="RadWindow" ID="RadWindowMensaje" runat="server" Title="Ayuda" VisibleStatusbar="False" EnableViewState="False" Behaviors="Close, Move" Modal="True" BorderStyle="Dotted" EnableTheming="False" BorderWidth="10px" Height="195px"
		Width="322px" Behavior="Close, Move">
		<ContentTemplate>
			<table>
				<tr>
					<td align="center">
						<asp:Label runat="server" ID="lblMensaje" Text="Si cambia el tipo de paso los valores asociados al tipo anterior se eliminarán, igualmente se eliminarán los registros de las tablas relacionadas de acuerdo al tipo" meta:resourcekey="lblMensajeResource1"></asp:Label>
					</td>
				</tr>
				<tr>
					<td align="center">
						<asp:Button runat="server" ID="btnAceptar" Text="Aceptar" OnClientClick="cerrar(); return false;" meta:resourcekey="btnAceptarResource1" />
					</td>
				</tr>
			</table>
		</ContentTemplate>
	</telerik:RadWindow>
	<telerik:RadCodeBlock runat="server" ID="RadScriptBlock3">
		<script type="text/javascript">
			var primera = true;
			var idMenu = '<%= IdMenuEncriptado %>';
			var pasoPantalla = '<%= ConfigurationManager.AppSettings[@"PasoPantalla"] %>';
			var pasoPrograma = '<%= ConfigurationManager.AppSettings[@"PasoPrograma"] %>';
			var pasoServicio = '<%= ConfigurationManager.AppSettings[@"PasoServicio"] %>';
			var pasoAlerta = '<%= ConfigurationManager.AppSettings[@"PasoAlerta"] %>';
			var pasoMensaje = '<%= ConfigurationManager.AppSettings[@"PasoMensaje"] %>';

			function ValidarControles(sender) {
				var errorURL = $find("cphBody_RadInputManager1").get_targetInput("<%= txtURL.ClientID %>")._owner._errorMessage;
				var errorMetodo = $find("cphBody_RadInputManager1").get_targetInput("<%= txtMetodo.ClientID %>")._owner._errorMessage;
				if (($("#" + '<%=txtURL.ClientID%>').val() == "" || $("#" + '<%=txtURL.ClientID%>').val() == errorURL) && ($("#" + '<%=txtMetodo.ClientID%>').val() == "" || $("#" + '<%=txtMetodo.ClientID%>').val() == errorMetodo)) {
					$find("<%= RadInputManager1.ClientID %>").get_targetInput("<%= txtURL.ClientID %>").get_owner().set_isRequired(true);
					$find("<%= RadInputManager1.ClientID %>").get_targetInput("<%= txtMetodo.ClientID %>").get_owner().set_isRequired(true);
				}
				else {
					$find("<%= RadInputManager1.ClientID %>").get_targetInput("<%= txtURL.ClientID %>").get_owner().set_isRequired(false);
					$find("<%= RadInputManager1.ClientID %>").get_targetInput("<%= txtMetodo.ClientID %>").get_owner().set_isRequired(false)
					$("#" + '<%=txtURL.ClientID%>').focus();
					$("#" + '<%=txtMetodo.ClientID%>').focus();
				}
				sender.focus();
			}

			$(window).load(function () {
				ValidatorEnable(document.getElementById('<%=rfvIdSubServicio.ClientID%>'), false);
				var combo = $find("<%= ddlIdSubServicio.ClientID %>");
				combo.disable();
				ValidatorEnable(document.getElementById('<%=rfvAlerta.ClientID%>'), false);
				combo = $find("<%= ddlAlerta.ClientID %>");
				combo.disable();
				$("#" + '<%=txtURL.ClientID%>').attr("disabled", "disabled");
				$("#" + '<%=txtMetodo.ClientID%>').attr("disabled", "disabled");
				$("#" + '<%=chkIndEncadenado.ClientID%>').attr("disabled", "disabled");
				$("#" + '<%=chkIndSegSubServicio.ClientID%>').attr("disabled", "disabled");
				try {
					$("#" + '<%=btnBloques.ClientID%>').attr("disabled", "disabled");
					$("#" + '<%=btnConfigurarCorreo.ClientID%>').attr("disabled", "disabled");
					$("#" + '<%=btnConfigurarSMS.ClientID%>').attr("disabled", "disabled");
					if ($("#" + '<%=chkIndAgendable.ClientID%>').is(":checked"))
						$("#" + '<%=btnAgenda.ClientID%>').removeAttr("disabled");
					else
						$("#" + '<%=btnAgenda.ClientID%>').attr("disabled", "disabled");
				}
				catch (err) {
				}
				combo = $find("<%= ddlIdTipoPaso.ClientID %>");
				if ("<%=accionVista%>" == "Modificar") {
					switch (combo.get_value()) {
						case pasoPantalla:
							$("#trAlerta").hide();
							$("#trSubServicio").hide();
							$("#trPrograma").hide();
							$("#" + '<%=chkIndEncadenado.ClientID%>').removeAttr("disabled");
							$("#" + '<%=btnBloques.ClientID%>').removeAttr("disabled");
							break;
						case pasoPrograma:
							$("#trAlerta").hide();
							$("#trSubServicio").hide();
							$("#" + '<%=txtURL.ClientID%>').removeAttr("disabled");
							$find("<%= RadInputManager1.ClientID %>").get_targetInput("<%= txtURL.ClientID %>").get_owner().set_isRequired(true);
							$("#" + '<%=txtMetodo.ClientID%>').removeAttr("disabled");
							$find("<%= RadInputManager1.ClientID %>").get_targetInput("<%= txtMetodo.ClientID %>").get_owner().set_isRequired(true);
							break;
						case pasoServicio:
							$("#trAlerta").hide();
							$("#trPrograma").hide();
							$("#" + '<%=chkIndSegSubServicio.ClientID%>').removeAttr("disabled");
							combo = $find("<%= ddlIdSubServicio.ClientID %>");
							combo.enable();
							validador = document.getElementById('<%=rfvIdSubServicio.ClientID%>');
							ValidatorEnable(validador, true);
							validador.isvalid = true;
							ValidatorUpdateDisplay(validador);
							break;
						case pasoAlerta:
							$("#trSubServicio").hide();
							$("#trPrograma").hide();
							combo = $find("<%= ddlAlerta.ClientID %>");
							combo.enable();
							validador = document.getElementById('<%=rfvAlerta.ClientID%>');
							ValidatorEnable(validador, true);
							validador.isvalid = true;
							ValidatorUpdateDisplay(validador);
							break;
						case pasoMensaje:
							$("#trAlerta").hide();
							$("#trSubServicio").hide();
							$("#trPrograma").hide();
							$("#" + '<%=btnConfigurarCorreo.ClientID%>').removeAttr("disabled");
							$("#" + '<%=btnConfigurarSMS.ClientID%>').removeAttr("disabled");
							break;
						default:
							$("#trAlerta").hide();
							$("#trSubServicio").hide();
							$("#trPrograma").hide();
							break;
					}
				}
				else {
					switch (combo.get_value()) {
						case pasoPantalla:
							$("#trAlerta").hide();
							$("#trSubServicio").hide();
							$("#trPrograma").hide();
							$("#" + '<%=btnBloques.ClientID%>').removeAttr("disabled");
						case pasoPrograma:
							$("#trAlerta").hide();
							$("#trSubServicio").hide();
							break;
						case pasoServicio:
							$("#trAlerta").hide();
							$("#trPrograma").hide();
							break;
						case pasoAlerta:
							$("#trSubServicio").hide();
							$("#trPrograma").hide();
							break;
						case pasoMensaje:
							$("#trAlerta").hide();
							$("#trSubServicio").hide();
							$("#trPrograma").hide();
							$("#" + '<%=btnConfigurarCorreo.ClientID%>').removeAttr("disabled");
							$("#" + '<%=btnConfigurarSMS.ClientID%>').removeAttr("disabled");
							break;
						default:
							$("#trAlerta").hide();
							$("#trSubServicio").hide();
							$("#trPrograma").hide();
							break;
					}
				}
			})

			function ActivarTipoPaso(sender, args) {
				if ("<%=accionVista%>" == "Modificar" && primera == true) {
					var oWindow = $find("<%= RadWindowMensaje.ClientID %>");
					oWindow.show();
					primera = false;
				}
				$("#trAlerta").show();
				$("#trSubServicio").show();
				$("#trPrograma").show();
				$find("<%= RadInputManager1.ClientID %>").get_targetInput("<%= txtCantidadRepeticion.ClientID %>").get_owner().set_isRequired(true);
				$("#" + '<%=txtCantidadRepeticion.ClientID%>').removeAttr("disabled");
				$find("<%= RadInputManager1.ClientID %>").get_targetInput("<%= txtReintentos.ClientID %>").get_owner().set_isRequired(true);
				$("#" + '<%=txtReintentos.ClientID%>').removeAttr("disabled");
				$find("<%= RadInputManager1.ClientID %>").get_targetInput("<%= txtPorcSlaCritico.ClientID %>").get_owner().set_isRequired(true);
				$("#" + '<%=txtPorcSlaCritico.ClientID%>').removeAttr("disabled");
				$find('<%=txtSlaTolerancia_rimHorasMinutosSegundos%>').get_targetInput("<%=txtSlaTolerancia_txtHoras%>").get_owner().set_isRequired(true);
				$("#" + '<%=txtSlaTolerancia_txtHoras%>').removeAttr("disabled");
				$find('<%=txtSlaTolerancia_rimHorasMinutosSegundos%>').get_targetInput("<%=txtSlaTolerancia_txtMinutos%>").get_owner().set_isRequired(true);
				$("#" + '<%=txtSlaTolerancia_txtMinutos%>').removeAttr("disabled");
				$find('<%=txtSlaTolerancia_rimHorasMinutosSegundos%>').get_targetInput("<%=txtSlaTolerancia_txtSegundos%>").get_owner().set_isRequired(true);
				$("#" + '<%=txtSlaTolerancia_txtSegundos%>').removeAttr("disabled");
				ValidatorEnable(document.getElementById('<%=rfvIdSubServicio.ClientID%>'), false);
				var combo = $find("<%= ddlIdSubServicio.ClientID %>");
				combo.set_text("");
				combo._applyEmptyMessage();
				combo.disable();
				ValidatorEnable(document.getElementById('<%=rfvAlerta.ClientID%>'), false);
				combo = $find("<%= ddlAlerta.ClientID %>");
				combo.set_text("");
				combo._applyEmptyMessage();
				combo.disable();
				if ($("#" + '<%=txtURL.ClientID%>').attr("disabled") != "disabled") {
					$find("<%= RadInputManager1.ClientID %>").get_targetInput("<%= txtURL.ClientID %>").get_owner().set_isRequired(false);
					$("#" + '<%=txtURL.ClientID%>').val("");
					$("#" + '<%=txtURL.ClientID%>').focus();
					$("#" + '<%=txtURL.ClientID%>').blur();
					$("#" + '<%=txtURL.ClientID%>').attr("disabled", "disabled");
					$find("<%= RadInputManager1.ClientID %>").get_targetInput("<%= txtMetodo.ClientID %>").get_owner().set_isRequired(false);
					$("#" + '<%=txtMetodo.ClientID%>').val("");
					$("#" + '<%=txtMetodo.ClientID%>').focus();
					$("#" + '<%=txtMetodo.ClientID%>').blur();
					$("#" + '<%=txtMetodo.ClientID%>').attr("disabled", "disabled");
				}
				try {
					$("#" + '<%=btnBloques.ClientID%>').attr("disabled", "disabled");
					$("#" + '<%=btnConfigurarCorreo.ClientID%>').attr("disabled", "disabled");
					$("#" + '<%=btnConfigurarSMS.ClientID%>').attr("disabled", "disabled");
				}
				catch (err) {
				}
				$("#" + '<%=chkIndEncadenado.ClientID%>').removeAttr("checked");
				$("#" + '<%=chkIndEncadenado.ClientID%>').attr("disabled", "disabled");
				$("#" + '<%=chkIndSegSubServicio.ClientID%>').removeAttr("checked");
				$("#" + '<%=chkIndSegSubServicio.ClientID%>').attr("disabled", "disabled");
				var item = args.get_item();
				switch (item.get_value()) {
					case pasoPantalla:
						$("#trAlerta").hide();
						$("#trSubServicio").hide();
						$("#trPrograma").hide();
						$("#" + '<%=chkIndEncadenado.ClientID%>').removeAttr("disabled");
						$("#" + '<%=btnBloques.ClientID%>').removeAttr("disabled");
						break;
					case pasoPrograma:
						$("#trAlerta").hide();
						$("#trSubServicio").hide();
						$("#" + '<%=txtURL.ClientID%>').removeAttr("disabled");
						$find("<%= RadInputManager1.ClientID %>").get_targetInput("<%= txtURL.ClientID %>").get_owner().set_isRequired(true);
						$("#" + '<%=txtMetodo.ClientID%>').removeAttr("disabled");
						$find("<%= RadInputManager1.ClientID %>").get_targetInput("<%= txtMetodo.ClientID %>").get_owner().set_isRequired(true);
						break;
					case pasoServicio:
						$("#trAlerta").hide();
						$("#trPrograma").hide();
						$("#" + '<%=chkIndSegSubServicio.ClientID%>').removeAttr("disabled");
						combo = $find("<%= ddlIdSubServicio.ClientID %>");
						combo.enable();
						validador = document.getElementById('<%=rfvIdSubServicio.ClientID%>');
						ValidatorEnable(validador, true);
						validador.isvalid = true;
						ValidatorUpdateDisplay(validador);
						break;
					case pasoAlerta:
						$("#trSubServicio").hide();
						$("#trPrograma").hide();
						combo = $find("<%= ddlAlerta.ClientID %>");
						combo.enable();
						validador = document.getElementById('<%=rfvAlerta.ClientID%>');
						ValidatorEnable(validador, true);
						validador.isvalid = true;
						ValidatorUpdateDisplay(validador);
						break;
					case pasoMensaje:
						$("#trAlerta").hide();
						$("#trSubServicio").hide();
						$("#trPrograma").hide();
						$("#" + '<%=btnConfigurarCorreo.ClientID%>').removeAttr("disabled");
						$("#" + '<%=btnConfigurarSMS.ClientID%>').removeAttr("disabled");
						break;
					default:
						$("#trAlerta").hide();
						$("#trSubServicio").hide();
						$("#trPrograma").hide();
						break;
				}
			}

			function changeChkAgendable() {
				if ($("#" + '<%=chkIndAgendable.ClientID%>').is(":checked"))
					$("#" + '<%=btnAgenda.ClientID%>').removeAttr("disabled");
				else
					$("#" + '<%=btnAgenda.ClientID%>').attr("disabled", "disabled");
			}

			function btnDocumentos_Click() {
				var wnd = GetRadWindow();
				wnd.setUrl("Modulos/Parametrizador/DocumentosPasoLista.aspx?IdMenu=" + idMenu + "&id=" + "<%= idEncriptado %>");
			}

			function btnChaPaso_Click() {
				var wnd = GetRadWindow();
				wnd.setUrl("Modulos/Parametrizador/ChadePasoLista.aspx?IdMenu=" + idMenu + "&id=" + "<%= idEncriptado %>");
			}

			function btnAgenda_Click() {
				var wnd = GetRadWindow();
				wnd.setUrl("Modulos/Parametrizador/ParametrosAgendaLista.aspx?IdMenu=" + idMenu + "&id=" + "<%= idEncriptado %>");
			}

			function btnConfigurarSMS_Click() {
				var wnd = GetRadWindow();
				wnd.setUrl("Modulos/Parametrizador/MensajesMetodosDestinatarioDetalle.aspx?IdMenu=" + idMenu + "&accion=" + "<%= accionEncriptada %>" + "&id=" + "<%= idEncriptado %>");
			}

			function btnConfigurarCorreo_Click() {
				var wnd = GetRadWindow();
				wnd.setUrl("Modulos/Parametrizador/MensajesMetodosDestinatarioCorreoDetalle.aspx?IdMenu=" + idMenu + "&accion=" + "<%= accionEncriptada %>" + "&id=" + "<%= idEncriptado %>");
			}

			function btnBloques_Click() {
				var wnd = GetRadWindow();
				wnd.setUrl("Modulos/Parametrizador/PasoMaestroDetalleBloques.aspx?IdMenu=" + idMenu + "&id=" + "<%= idEncriptado %>");
			}

			function cerrar() {
				var oWindow = $find("<%= RadWindowMensaje.ClientID %>");
				oWindow.close();
			}

			function IrAnterior() {
				var wnd = GetRadWindow();
				wnd.setUrl('<%= RutaPadreEncriptada %>');
			}
		</script>
	</telerik:RadCodeBlock>
</asp:Content>
