<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SeleccionAseguradoCA.ascx.cs" Inherits="HConnexum.Tramitador.Sitio.Modulos.Bloques.SeleccionAseguradoCA" %>

<style type="text/css">
	table.ancho { font-weight: bold; }
	.PanelMargen { margin-top: 10px; }
	.ocultar { visibility: hidden; }
	.boton-accion { background-color: #d9e5f2; }
	.readOnly { background: #D7D7D7; }
	.number { text-align: right; }
	.arrange { float: left; }
	
	.RadPicker_Office2010Blue { zoom:1 !important; }
    .RadPicker_Office2010Black { zoom:1 !important; }
    .RadPicker_Office2010Silver { zoom:1 !important; }
</style>
<asp:Panel ID="pnlIntermediarioDatos" runat="server" GroupingText="DATOS DE CONTACTO">
	<table runat="server" id="Table1" border="0" width="100%" class="ancho">
		<tr>
			<td class="labelCell4colP" style="vertical-align: top;"><asp:Label runat="server" Text="Suscriptor" /></td>
			<td class="labelCell4colP" style="vertical-align: top;"><asp:Label runat="server" Text="Número de Fax" /></td>
			<td class="labelCell4colP" style="vertical-align: top;">&nbsp;</td>
			<td class="labelCell4colP" style="vertical-align: top;">&nbsp;</td>
		</tr>
		<tr>
			<td class="fieldCell4colP" style="vertical-align: top;"><asp:TextBox ID="NomSusIntermed" runat="server" MaxLength="200" ReadOnly="true" Width="90%" CssClass="readOnly" /></td>
			<td class="fieldCell4colP" style="vertical-align: top;"><asp:TextBox ID="txtSuscriptorFaxNumero" runat="server" MaxLength="200" ReadOnly="true" Width="90%" CssClass="readOnly number" /></td>
			<td class="fieldCell4colP" style="vertical-align: top;">&nbsp;</td>
			<td class="fieldCell4colP" style="vertical-align: top;">&nbsp;</td>
		</tr>
	</table>
</asp:Panel>
<asp:Panel ID="pnlAfiliadoConsulta" runat="server" CssClass="PanelMargen" GroupingText="CONSULTA DE AFILIADO">
	<div id="loadingDiv" style="width: 100%; height: 69px">
		<table style="width: 100%; height: 100%; background-color: White;">
			<tr style="height: 100%">
				<td align="center" valign="middle" style="width: 100%"><asp:Image ID="Image1" runat="server" ImageUrl="~/Imagenes/imgLoader.gif"></asp:Image></td>
			</tr>
		</table>
	</div>
	<div id="contentDiv" style="width: 100%; height: 100%;">
		<table runat="server" id="Table2" border="0" width="100%" class="ancho">
			<tr>
				<td class="labelCell4colP" style="vertical-align: top;"><asp:Label runat="server" 
                        Text="Documento Identidad Titular/Beneficiario" /></td>
				<td class="labelCell4colP" style="vertical-align: top;">&nbsp;</td>
				<td class="labelCell4colP" style="vertical-align: top;">&nbsp;</td>
				<td class="labelCell4colP" style="vertical-align: top;">&nbsp;</td>
			</tr>
			<tr>
				<td class="fieldCell4colP" style="vertical-align: top;"><asp:TextBox ID="txtAseguradoCedula" runat="server" CssClass="number" Width="90%" MaxLength="8" TabIndex="1" /></td>
				<td class="fieldCell4colP" style="vertical-align: top;"><asp:Button ID="btnBuscarAsegurado" runat="server" Text="Buscar" OnClick="btnBuscarAsegurado_Click" CausesValidation="False" TabIndex="2" /></td>
				<td class="fieldCell4colP" style="vertical-align: top;">&nbsp;</td>
				<td class="fieldCell4colP" style="vertical-align: top;">&nbsp;</td>
			</tr>
			<tr>
				<td class="labelCell4colP" style="vertical-align: top;"><asp:Label ID="lblAfiliadoNoEncontradoMensaje" runat="server" Text="Si el paciente es beneficiario de la póliza, realice la búsqueda por la cédula del titular" Visible="false" /></td>
				<td class="labelCell4colP" style="vertical-align: top;"><asp:Button ID="btnAfiliadoNoEncontradoAgregar" runat="server" Text="Agregar beneficiario no existente" Visible="false" OnClick="btnAfiliadoNoEncontradoAgregar_Click" TabIndex="5" CausesValidation="False" /></td>
				<td class="labelCell4colP" style="vertical-align: top;">&nbsp;</td>
				<td class="labelCell4colP" style="vertical-align: top;">&nbsp;</td>
			</tr>
		</table>
	</div>
</asp:Panel>
<asp:Panel ID="pnlAfiliadoPolizas" runat="server" GroupingText="LISTADO DE PÓLIZAS ASOCIADAS A LA CONSULTA" Visible="false" CssClass="PanelMargen">
	<table runat="server" id="Table3" border="0" width="100%" class="ancho">
		<tr>
			<td style="width: 100%; vertical-align: top;">
				<asp:Label ID="lblNoRegistros" runat="server" Text="No existen registros a mostrar." Visible="False" />
				<telerik:RadGrid ID="rgAfiliadoPolizas" runat="server" AutoGenerateColumns="False" TabIndex="3" Width="100%" CellSpacing="0" Culture="es-ES" GridLines="None">
					<ClientSettings EnableRowHoverStyle="True" Selecting-AllowRowSelect="True" ClientEvents-OnRowDblClick="DobleClickOnAsegurado" ClientEvents-OnRowSelected="GetSelectedPolizas">
						<Selecting AllowRowSelect="True" CellSelectionMode="None" />
						<ClientEvents OnRowDblClick="DobleClickOnAsegurado" OnRowSelected="GetSelectedPolizas" />
					</ClientSettings>
					<MasterTableView ShowHeadersWhenNoRecords="true" NoMasterRecordsText="No records." DataKeyNames="id_poliza, a00Contratante, a02asegurado, a02NoDocasegurado, Fecha_Nacimiento, a00FechaHasta, a02Sexo">
						<CommandItemSettings ExportToPdfText="Export to PDF" />
						<RowIndicatorColumn FilterControlAltText="Filter RowIndicator column" Visible="True" />
						<ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column" Visible="True" />
						<Columns>
							<telerik:GridBoundColumn DataField="a02NroPoliza" HeaderText="Póliza" UniqueName="poliza">
								<HeaderStyle HorizontalAlign="Center" Width="12%" />
								<ItemStyle HorizontalAlign="Center" Width="12%" />
							</telerik:GridBoundColumn>
							<telerik:GridBoundColumn DataField="a02Certificado" HeaderText="Certificado" UniqueName="certificado">
								<HeaderStyle HorizontalAlign="Center" Width="12%" />
								<ItemStyle HorizontalAlign="Center" Width="12%" />
							</telerik:GridBoundColumn>
							<telerik:GridBoundColumn DataField="a02asegurado" HeaderText="Asegurado">
								<HeaderStyle HorizontalAlign="Left" Width="40%" />
								<ItemStyle HorizontalAlign="Left" Width="40%" />
							</telerik:GridBoundColumn>
							<telerik:GridBoundColumn DataField="a02NoDocasegurado" HeaderText="Documento de Identidad">
								<HeaderStyle HorizontalAlign="Center" Width="21%" />
								<ItemStyle HorizontalAlign="Center" Width="21%" />
							</telerik:GridBoundColumn>
							<telerik:GridBoundColumn DataField="Fecha_Nacimiento" DataFormatString="{0:d}" HeaderText="Fecha de Nacimiento" Visible="False">
								<HeaderStyle HorizontalAlign="Center" />
								<ItemStyle HorizontalAlign="Center" />
							</telerik:GridBoundColumn>
							<telerik:GridBoundColumn DataField="Parentesco_txt" HeaderText="Parentesco">
								<HeaderStyle HorizontalAlign="Left" Width="15%" />
								<ItemStyle HorizontalAlign="Left" Width="15%" />
							</telerik:GridBoundColumn>
							<telerik:GridBoundColumn DataField="id_poliza" HeaderText="Póliza ID" Visible="false">
								<HeaderStyle Width="15%" />
								<ItemStyle Width="15%" />
							</telerik:GridBoundColumn>
						</Columns>
						<EditFormSettings>
							<EditColumn FilterControlAltText="Filter EditCommandColumn column" />
						</EditFormSettings>
					</MasterTableView>
					<FilterMenu EnableImageSprites="False" />
				</telerik:RadGrid>
			</td>
		</tr>
	</table>
</asp:Panel>
<asp:Panel ID="pnlPolizaGrupoFamiliar" runat="server" GroupingText="GRUPO FAMILIAR" Visible="False" CssClass="PanelMargen">
	<table runat="server" id="Table4" border="0" width="100%" class="ancho">
		<tr>
			<td style="width: 100%; vertical-align: top;">
				<telerik:RadGrid ID="rgPolizaGrupoFamiliar" runat="server" AutoGenerateColumns="False" AlternatingItemStyle-BackColor="#E1E1E1" TabIndex="4" Width="100%" ValidationSettings-EnableModelValidation="False" ValidationSettings-EnableValidation="False" CellSpacing="0" Culture="es-ES" GridLines="None">
					<ValidationSettings EnableModelValidation="False" EnableValidation="False" />
					<ClientSettings ClientEvents-OnRowDblClick="DobleClickOnBeneficiario" ClientEvents-OnRowSelected="GetSelectedBeneficiarios" EnableRowHoverStyle="True" Selecting-AllowRowSelect="True">
						<Selecting AllowRowSelect="True" CellSelectionMode="None" />
						<ClientEvents OnRowDblClick="DobleClickOnBeneficiario" OnRowSelected="GetSelectedBeneficiarios" />
					</ClientSettings>
					<AlternatingItemStyle BackColor="#E1E1E1" />
					<MasterTableView DataKeyNames="id_asegurado, fecha_nacimiento, parentesco_txt, a02RamoPoliza, a02NoDocasegurado, a00FechaDesde, a00FechaHasta, a02Sexo, a02Asegurado, a02Estado" NoMasterRecordsText="No records." ShowHeadersWhenNoRecords="true">
						<CommandItemSettings ExportToPdfText="Export to PDF" />
						<RowIndicatorColumn FilterControlAltText="Filter RowIndicator column" Visible="True" />
						<ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column" Visible="True" />
						<Columns>
							<telerik:GridBoundColumn DataField="a02NoDocasegurado" HeaderText="Documento de Identidad" UniqueName="cedulaBenef">
								<HeaderStyle HorizontalAlign="Center" Width="20%" />
								<ItemStyle HorizontalAlign="Center" Width="20%" />
							</telerik:GridBoundColumn>
							<telerik:GridBoundColumn DataField="a02Asegurado" HeaderText="Asegurado" UniqueName="beneficiario">
								<HeaderStyle HorizontalAlign="Left" Width="60%" />
								<ItemStyle HorizontalAlign="Left" Width="60%" />
							</telerik:GridBoundColumn>
							<telerik:GridBoundColumn DataField="a02Sexo" HeaderText="Sexo" UniqueName="sexo">
								<HeaderStyle HorizontalAlign="Center" Width="10%" />
								<ItemStyle HorizontalAlign="Center" Width="10%" />
							</telerik:GridBoundColumn>
							<telerik:GridBoundColumn DataField="parentesco_txt" HeaderText="Parentesco" UniqueName="parentesco">
								<HeaderStyle HorizontalAlign="Left" Width="10%" />
								<ItemStyle HorizontalAlign="Left" Width="10%" />
							</telerik:GridBoundColumn>
							<telerik:GridBoundColumn DataField="id_asegurado" HeaderText="AseguradoId" Visible="false" />
							<telerik:GridBoundColumn DataField="fecha_nacimiento" HeaderText="Nacimiento" UniqueName="fechaNacimiento" Visible="false" />
							<telerik:GridBoundColumn DataField="a02RamoPoliza" HeaderText="Ramo" UniqueName="ramo" Visible="false" />
							<telerik:GridBoundColumn DataField="a02Estado" HeaderText="Estado" UniqueName="a02Estado" Visible="false" />
						</Columns>
						<EditFormSettings>
							<EditColumn FilterControlAltText="Filter EditCommandColumn column" />
						</EditFormSettings>
					</MasterTableView>
					<FilterMenu EnableImageSprites="False" />
				</telerik:RadGrid>
			</td>
		</tr>
	</table>
</asp:Panel>
<asp:Panel ID="pnlTitularDatos" runat="server" GroupingText="DATOS TITULAR" Visible="false" CssClass="PanelMargen">
	<table runat="server" id="T5" border="0" width="100%" class="ancho">
		<tr>
			<td class="labelCell4colP" style="vertical-align: top;"><asp:Label runat="server" Text="Primer Nombre" /></td>
			<td class="labelCell4colP" style="vertical-align: top;"><asp:Label runat="server" Text="Segundo Nombre" /></td>
			<td class="labelCell4colP" style="vertical-align: top;"><asp:Label runat="server" Text="Primer Apellido" /></td>
			<td class="labelCell4colP" style="vertical-align: top;"><asp:Label runat="server" Text="Segundo Apellido" /></td>
		</tr>
		<tr>
			<td class="fieldCell4colP" style="vertical-align: top;"><asp:TextBox ID="PrimerNombreTit" runat="server" MaxLength="200" Width="74%" onkeypress="return ValidarSoloTexto(event);" /></td>
			<td class="fieldCell4colP" style="vertical-align: top;"><asp:TextBox ID="SegundoNombreTit" runat="server" MaxLength="200" Width="73%"  onkeypress="return ValidarSoloTexto(event);" /></td>
			<td class="fieldCell4colP" style="vertical-align: top;"><asp:TextBox ID="PrimerApellidoTit" runat="server" MaxLength="200" Width="71%"  onkeypress="return ValidarSoloTexto(event);" /></td>
			<td class="fieldCell4colP" style="vertical-align: top;"><asp:TextBox ID="SegundoApellidoTit" runat="server" MaxLength="200" Width="71%" onkeypress="return ValidarSoloTexto(event);" /></td>
		</tr>
		<tr>
			<td class="labelCell4colP" style="vertical-align: top;"><asp:Label runat="server" Text="Nro. de Cédula" /></td>
			<td class="labelCell4colP" style="vertical-align: top;"><asp:Label runat="server" Text="Sexo" /></td>
			<td class="labelCell4colP" style="vertical-align: top;"><asp:Label runat="server" Text="Fecha de Nacimiento" /></td>
			<td class="labelCell4colP" style="vertical-align: top;"><asp:Label runat="server" Text="Contratante" /></td>
		</tr>
		<tr>
			<td class="fieldCell4colP" style="vertical-align: top;">
				<telerik:RadComboBox ID="tipdoctitular" runat="server" Width="45" DataValueField="Id" DataTextField="NombreValorCorto" OnSelectedIndexChanged="tipdoctitular_SelectedIndexChanged" />
				<asp:TextBox ID="txtTitularNumDoc" runat="server" MaxLength="8" CssClass="number" Width="51%" />
			</td>
			<td class="fieldCell4colP" style="vertical-align: top;">
				<telerik:RadComboBox ID="SexoTitu" runat="server" DataValueField="NombreValorCorto" DataTextField="NombreValorCorto" AppendDataBoundItems="True" CausesValidation="False" Culture="es-ES" EmptyMessage="Seleccione" Width="73%" />
				<asp:RequiredFieldValidator ID="rfvTitularSexo" runat="server" CssClass="validator" ControlToValidate="SexoTitu" ErrorMessage="*" Width="20px" />
			</td>
			<td class="fieldCell4colP" style="vertical-align: top;">
				<telerik:RadDatePicker ID="FecNacTit" runat="server" Culture="es-ES" MinDate="1900-01-01" Width="86%">
					<Calendar runat="server" FastNavigationStep="12" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x" />
					<DateInput runat="server" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" EnableSingleInputRendering="True" LabelWidth="64px" />
					<DatePopupButton HoverImageUrl="" ImageUrl="" />
				</telerik:RadDatePicker>
				<asp:RequiredFieldValidator ID="rfvTitularNacimientoFecha" runat="server" CssClass="validator" ControlToValidate="FecNacTit" ErrorMessage="*" Width="20px" />
			</td>
			<td class="fieldCell4colP" style="vertical-align: top;"><asp:TextBox ID="txtTitularContratante" runat="server" MaxLength="200" Width="71%" /></td>
		</tr>
		<tr>
			<td class="labelCell4colP" style="vertical-align: top;" colspan="4"><asp:Label runat="server" Text="¿El titular es el beneficiario?" /></td>
		</tr>
		<tr>
			<td class="fieldCell4colP" style="vertical-align: top;">
				<div style="float: left; width: 40%;">
					<asp:RadioButtonList ID="optListTitularEsBeneficiario" runat="server" Width="100%" AutoPostBack="True" RepeatDirection="Horizontal" OnSelectedIndexChanged="optListTitularEsBeneficiario_SelectedIndexChanged" CssClass="RadioButtonListValidator">
						<asp:ListItem Text="Si" Value="Si"></asp:ListItem>
						<asp:ListItem Text="No" Value="No"></asp:ListItem>
					</asp:RadioButtonList>
				</div>
				<div style="float: left; width: 60%;">
					<asp:RequiredFieldValidator ID="rfvTitularEsBeneficiario" runat="server" CssClass="validator" ControlToValidate="optListTitularEsBeneficiario" ErrorMessage="*" Width="100%" />
				</div>
			</td>
			<td class="fieldCell4colP" style="vertical-align: top;">&nbsp;</td>
			<td class="fieldCell4colP" style="vertical-align: top;">&nbsp;</td>
			<td class="fieldCell4colP" style="vertical-align: top;">&nbsp;</td>
		</tr>
	</table>
</asp:Panel>
<asp:Panel ID="pnlBeneficiarioDatos" runat="server" GroupingText="DATOS BENEFICIARIO" Visible="false" CssClass="PanelMargen">
	<table runat="server" id="T6" border="0" width="100%" class="ancho">
		<tr>
			<td class="labelCell4colP" style="vertical-align: top;"><asp:Label runat="server" Text="Primer Nombre" /></td>
			<td class="labelCell4colP" style="vertical-align: top;"><asp:Label runat="server" Text="Segundo Nombre" /></td>
			<td class="labelCell4colP" style="vertical-align: top;"><asp:Label runat="server" Text="Primer Apellido" /></td>
			<td class="labelCell4colP" style="vertical-align: top;"><asp:Label runat="server" Text="Segundo Apellido" /></td>
		</tr>
		<tr>
			<td class="fieldCell4colP" style="vertical-align: top;"><asp:TextBox ID="PrimerNombreBenef" runat="server" MaxLength="200" Width="74%" onkeypress="return ValidarSoloTexto(event);" /></td>
			<td class="fieldCell4colP" style="vertical-align: top;"><asp:TextBox ID="SegundoNombreBenef" runat="server" MaxLength="200" Width="73%" onkeypress="return ValidarSoloTexto(event);" /></td>
			<td class="fieldCell4colP" style="vertical-align: top;"><asp:TextBox ID="PrimerApellidoBenef" runat="server" MaxLength="200" Width="71%" onkeypress="return ValidarSoloTexto(event);" /></td>
			<td class="fieldCell4colP" style="vertical-align: top;"><asp:TextBox ID="SegundoApellidoBenef" runat="server" MaxLength="200" Width="71%" onkeypress="return ValidarSoloTexto(event);" /></td>
		</tr>
		<tr>
			<td class="labelCell4colP" style="vertical-align: top;"><asp:Label runat="server" Text="Parentesco" /></td>
			<td class="labelCell4colP" style="vertical-align: top;"><asp:Label runat="server" 
                    Text="Documento Identidad" /></td>
			<td class="labelCell4colP" style="vertical-align: top;"><asp:Label runat="server" Text="Sexo" /></td>
			<td class="labelCell4colP" style="vertical-align: top;"><asp:Label runat="server" Text="Fecha de Nacimiento" /></td>
		</tr>
		<tr>
			<td class="fieldCell4colP" style="vertical-align: top;">
				<telerik:RadComboBox ID="parentescobenefne" runat="server" DataValueField="NombreValor" DataTextField="NombreValor" AppendDataBoundItems="true" AutoPostBack="True"
					CausesValidation="False" Culture="es-ES" OnSelectedIndexChanged="parentescobenefne_SelectedIndexChanged" Width="75%" EmptyMessage="Seleccione">
				</telerik:RadComboBox>
				<asp:RequiredFieldValidator ID="rfvBeneficiarioParentesco" runat="server" CssClass="validator" ControlToValidate="parentescobenefne" ErrorMessage="*" Width="20px" />
			</td>
			<td class="fieldCell4colP" style="vertical-align: top;">
				<telerik:RadComboBox ID="tipdocbenefne" runat="server" Width="45" DataValueField="Id" DataTextField="NombreValorCorto" OnSelectedIndexChanged="tipdocbenefne_SelectedIndexChanged" />
				<asp:TextBox ID="numdocbenefne" runat="server" MaxLength="8" CssClass="number" Width="51%" />
			</td>
			<td class="fieldCell4colP" style="vertical-align: top;">
				<telerik:RadComboBox ID="sexobenefne" runat="server" DataValueField="NombreValorCorto" DataTextField="NombreValorCorto" AppendDataBoundItems="True" AutoPostBack="True"
					CausesValidation="False" Culture="es-ES" OnSelectedIndexChanged="sexobenefne_SelectedIndexChanged" Width="72%" EmptyMessage="Seleccione" />
				<asp:RequiredFieldValidator ID="rfvBeneficiarioSexo" runat="server" CssClass="validator" ControlToValidate="sexobenefne" ErrorMessage="*" Width="20px" />
			</td>
			<td class="fieldCell4colP" style="vertical-align: top;">
				<telerik:RadDatePicker ID="FecNacBenef" runat="server" MinDate="1900-01-01" Culture="es-ES" Width="86%">
					<Calendar runat="server" FastNavigationStep="12" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x" />
					<DateInput runat="server" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" EnableSingleInputRendering="True" LabelWidth="64px" />
					<DatePopupButton HoverImageUrl="" ImageUrl="" />
				</telerik:RadDatePicker>
				<asp:RequiredFieldValidator ID="rfvFechaNacBenef" runat="server" CssClass="validator" ControlToValidate="FecNacBenef" ErrorMessage="*" Width="20px" />
			</td>
		</tr>
		<tr>
			<td class="labelCell4colP" style="vertical-align: top;"><asp:Label runat="server" Text="¿Menor sin Cédula?" /></td>
			<td class="labelCell4colP" style="vertical-align: top;"><asp:Label runat="server" Text="¿Posee condición especial?" /></td>
			<td class="labelCell4colP" style="vertical-align: top;">&nbsp;</td>
			<td class="labelCell4colP" style="vertical-align: top;">&nbsp;</td>
		</tr>
		<tr>
			<td class="fieldCell4colP" style="vertical-align: top;">
				<div style="float: left; width: 40%;">
					<asp:RadioButtonList ID="IndMenor" runat="server" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="IndMenor_SelectedIndexChanged" CssClass="RadioButtonListValidator" Width="100%">
						<asp:ListItem Text="Si" Value="Si"></asp:ListItem>
						<asp:ListItem Text="No" Value="No"></asp:ListItem>
					</asp:RadioButtonList>
				</div>
				<div style="float: left; width: 60%;">
					<asp:RequiredFieldValidator ID="rfvBeneficiarioMenorSinCedula" runat="server" ControlToValidate="IndMenor" ErrorMessage="*" Width="100%" CssClass="validator" />
				</div>
			</td>
			<td class="fieldCell4colP" style="vertical-align: top;">
				<div style="float:left; width:40%;">
					<asp:RadioButtonList ID="IndCondEspecial" runat="server" RepeatDirection="Horizontal" CssClass="RadioButtonListValidator">
						<asp:ListItem Text="Si" Value="Si"></asp:ListItem>
						<asp:ListItem Text="No" Value="No"></asp:ListItem>
					</asp:RadioButtonList>
				</div>
			</td>
			<td class="fieldCell4colP" style="vertical-align: top;">&nbsp;</td>
			<td class="fieldCell4colP" style="vertical-align: top;">&nbsp;</td>
		</tr>
		<tr>
			<td class="labelCell4colP" colspan="4" style="vertical-align: top;"> <asp:Label ID="lblBeneficiarioMensaje" runat="server" /></td>
		</tr>
	</table>
</asp:Panel>
<asp:Label ID="lblMensaje" runat="server" Visible="false"></asp:Label>
<asp:Panel ID="pnlSolicitudDatos" runat="server" GroupingText="DATOS SOLICITUD" Visible="false" CssClass="PanelMargen">
	<table runat="server" id="Table7" border="0" width="100%" class="ancho">
		<tr>
			<td class="labelCell4colP" style="vertical-align: top;"><asp:Label runat="server" Text="Fecha de Solicitud" /></td>
			<td class="labelCell4colP" style="vertical-align: top;"><asp:Label runat="server" Text="Fecha de Emisión" /></td>
			<td class="labelCell4colP" style="vertical-align: top;"><asp:Label runat="server" Text="Médico" /></td>
			<td class="labelCell4colP" style="vertical-align: top;"><asp:Label runat="server" Text="Días de Hospitalización" /></td>
		</tr>
		<tr>
			<td class="fieldCell4colP" style="vertical-align: top;">
				<telerik:RadDatePicker ID="FecSolicitud" runat="server" Culture="es-ES" MinDate="1900-01-01" AutoPostBack="True" Width="86%">
					<Calendar runat="server" FastNavigationStep="12" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x" />
					<DateInput runat="server" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" EnableSingleInputRendering="True" LabelWidth="64px" />
					<DatePopupButton HoverImageUrl="" ImageUrl="" />
				</telerik:RadDatePicker>
				<asp:RequiredFieldValidator ID="rfvSolicitudFecha" runat="server" CssClass="validator" ControlToValidate="FecSolicitud" ErrorMessage="*" Width="20px" />
			</td>
			<td class="fieldCell4colP" style="vertical-align: top;">
				<telerik:RadDatePicker ID="FecEmision" runat="server" Culture="es-ES" MinDate="1900-01-01" Width="86%">
					<Calendar runat="server" FastNavigationStep="12" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x" />
					<DateInput runat="server" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" EnableSingleInputRendering="True" LabelWidth="64px" />
					<DatePopupButton HoverImageUrl="" ImageUrl="" />
				</telerik:RadDatePicker>
				<asp:RequiredFieldValidator ID="rfvSolicitudEmisionFecha" runat="server" CssClass="validator" ControlToValidate="FecEmision" ErrorMessage="*" Width="20px" />
			</td>
			<td class="fieldCell4colP" style="vertical-align: top;"><asp:TextBox ID="NomMedico" runat="server" MaxLength="200" Width="72%" onkeypress="return ValidarSoloTexto(event);" /></td>
			<td class="fieldCell4colP" style="vertical-align: top;">
				<telerik:RadComboBox ID="NumDiasHosp" runat="server" DataValueField="Id" DataTextField="NombreValor" CausesValidation="False" EmptyMessage="Seleccione" Width="72%" />
				<asp:RequiredFieldValidator ID="rfvNumDiasHosp" runat="server" CssClass="validator" ControlToValidate="NumDiasHosp" ErrorMessage="*" Width="20px" />
			</td>
		</tr>
		<tr>
			<td class="labelCell4colP" style="vertical-align: top;"><asp:Label ID="Label1" runat="server" Text="Cita Post-Operatoria" /></td>
			<td class="labelCell4colP" style="vertical-align: top;"><asp:Label runat="server" Text="Monto Presupuestado"   /></td>
			<td class="labelCell4colP" style="vertical-align: top;"><asp:Label runat="server" Text="Número de Celular" /></td>
			<td class="labelCell4colP" style="vertical-align: top;"><asp:Label runat="server" Text="Email" /></td>
		</tr>
		<tr>
			<td class="fieldCell4colP" style="vertical-align: top;">
				<asp:RadioButtonList ID="IndCitaPost" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="IndCitaPost_SelectedIndexChanged" AutoPostBack="True" CausesValidation="false" CssClass="RadioButtonListValidator">
					<asp:ListItem Text="Si" Value="Si"></asp:ListItem>
					<asp:ListItem Text="No" Value="No" Selected="True"></asp:ListItem>
				</asp:RadioButtonList>
			</td>
			<td class="fieldCell4colP" style="vertical-align: top;"><asp:TextBox ID="MontoPresup" runat="server" MaxLength="13" Width="72%" onkeypress="return Decimal(event);"  /></td>
			<td class="fieldCell4colP" style="vertical-align: top;"><asp:TextBox ID="TlfResponsable" runat="server" CssClass="number" Width="72%" MaxLength="11" /></td>
			<td class="fieldCell4colP" style="vertical-align: top;"><asp:TextBox ID="EmailResponsable" runat="server" Width="72%" /></td>
		</tr>
	</table>
	<table runat="server" id="solicitudMenorDatos" border="0" width="100%" class="ancho">
		<tr>
			<td class="labelCell4colP" style="vertical-align: top;"><asp:Label ID="lblSolicitudNacimientoFecha" runat="server" Text="Fecha de Nacimiento" /></td>
			<td class="labelCell4colP" style="vertical-align: top;"><asp:Label ID="lblSolicitudEdad" runat="server" Text="Edad" /></td>
			<td class="labelCell4colP" style="vertical-align: top;">&nbsp;</td>
			<td class="labelCell4colP" style="vertical-align: top;">&nbsp;</td>
		</tr>
		<tr>
			<td class="fieldCell4colP" style="vertical-align: top;">
				<telerik:RadDatePicker ID="dtpAseguradoNacimientoFecha" runat="server" Culture="es-ES" MinDate="1900-01-01" Width="86%">
					<Calendar runat="server" FastNavigationStep="12" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x" />
					<DateInput runat="server" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" EnableSingleInputRendering="True" />
					<DatePopupButton HoverImageUrl="" ImageUrl="" />
				</telerik:RadDatePicker>
				<asp:RequiredFieldValidator ID="rfvAseguradoNacimientoFecha" runat="server" CssClass="validator" ControlToValidate="dtpAseguradoNacimientoFecha" ErrorMessage="*" Width="20px" ValidationGroup="" />
			</td>
			<td class="fieldCell4colP" style="vertical-align: top;"><asp:TextBox ID="txtAseguradoEdad" runat="server" CssClass="number" MaxLength="2" Width="72%" /></td>
			<td class="fieldCell4colP" style="vertical-align: top;">&nbsp;</td>
			<td class="fieldCell4colP" style="vertical-align: top;">&nbsp;</td>
		</tr>
	</table>
	<table runat="server" id="T8" border="0" width="100%" class="ancho">
		<tr>
			<td class="labelCell3colP" style="vertical-align: top;"><asp:Label runat="server" Text="Descripción Cita Post-Operatoria" /></td>
			<td class="labelCell3colP" style="vertical-align: top;"><asp:Label runat="server" Text="Diagnóstico" /></td>
			<td class="labelCell3colP" style="vertical-align: top;"><asp:Label runat="server" Text="Procedimiento" /></td>
		</tr>
		<tr>
			<td class="fieldCell3colP" style="vertical-align: top;"><asp:TextBox ID="ObservaCitaPost" runat="server" TextMode="MultiLine" Rows="4" Width="90%" MaxLength="200" Style="overflow: auto;" /></td>
			<td class="fieldCell3colP" style="vertical-align: top;"><asp:TextBox ID="ObsDiagnostico" runat="server" TextMode="MultiLine" Rows="4" Width="90%" MaxLength="200" Style="overflow: auto;" /></td>
			<td class="fieldCell3colP" style="vertical-align: top;"><asp:TextBox ID="ObsProcedimiento" runat="server" TextMode="MultiLine" Rows="4" Width="90%" MaxLength="200" Style="overflow: auto;" /></td>
		</tr>
	</table>
</asp:Panel>
<telerik:RadInputManager ID="RadInputManager1" runat="server">
	<telerik:TextBoxSetting BehaviorID="txtSettings">
		<TargetControls>
			<telerik:TargetInput ControlID="ObservaCitaPost" />
			<telerik:TargetInput ControlID="NomDiagnostico" />
			<telerik:TargetInput ControlID="NomProcedimiento" />
			<telerik:TargetInput ControlID="PrimerNombreTit" />
			<telerik:TargetInput ControlID="PrimerApellidoTit" />
			<telerik:TargetInput ControlID="txtTitularContratante" />
			<telerik:TargetInput ControlID="PrimerNombreBenef" />
			<telerik:TargetInput ControlID="PrimerApellidoBenef" />
			<telerik:TargetInput ControlID="NomSintomas" />
			<telerik:TargetInput ControlID="NomMedico" />
			<telerik:TargetInput ControlID="MontoPresup" />
					
			<telerik:TargetInput ControlID="txtAseguradoEdad" />
			<telerik:TargetInput ControlID="ObsDiagnostico" />
			<telerik:TargetInput ControlID="ObsProcedimiento" />
			<telerik:TargetInput ControlID="numdocbenefne" />
		</TargetControls>
	</telerik:TextBoxSetting>
	<telerik:NumericTextBoxSetting BehaviorID="txtMontoSettings" Validation-IsRequired="false" ErrorMessage="Campo Obligatorio" DecimalDigits="2" DecimalSeparator="," Validation-ValidationGroup="Validaciones">
		<TargetControls>
			<telerik:TargetInput ControlID="MontoPresup" />
		</TargetControls>
	</telerik:NumericTextBoxSetting>
	<telerik:TextBoxSetting BehaviorID="txtNumericSettings" EnabledCssClass="number">
		<TargetControls>
			<telerik:TargetInput ControlID="txtAseguradoEdad" />
			<telerik:TargetInput ControlID="txtTitularNumDoc" />
			<%--<telerik:TargetInput ControlID="numdocbenefne" />--%>
		</TargetControls>
	</telerik:TextBoxSetting>
	<telerik:RegExpTextBoxSetting BehaviorID="txtEmailSettings" Validation-IsRequired="true"  ValidationExpression="^([0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$" ErrorMessage="Formato Email incorrecto.">
		<TargetControls>
			<telerik:TargetInput ControlID="EmailResponsable" />
		</TargetControls>
	</telerik:RegExpTextBoxSetting>
	<telerik:RegExpTextBoxSetting BehaviorID="txtCelularSettings"  Validation-IsRequired="true" ValidationExpression="^0[24][12](\d{1})(\d{3})(\d{4})$" ErrorMessage="Formato Celular incorrecto.">
		<TargetControls>
			<telerik:TargetInput ControlID="TlfResponsable" />
		</TargetControls>
	</telerik:RegExpTextBoxSetting>
</telerik:RadInputManager>
<!-- CAMPOS A XML KEYS - INICIO -->
<span class="arrange"><asp:HiddenField runat="server" ID="NomTipoServicio" Value="Carta" /></span>
<span class="arrange"><asp:HiddenField runat="server" ID="IdSusIntermediario" /></span>
<span class="arrange"><asp:HiddenField runat="server" ID="IdIntermediario" /></span>
<span class="arrange"><asp:HiddenField runat="server" ID="IdSusProveedor" /></span>
<span class="arrange"><asp:HiddenField runat="server" ID="IdProveedor" /></span>
<span class="arrange"><asp:HiddenField runat="server" ID="IdCodEnlaceIntermediario" /></span>
<span class="arrange"><asp:HiddenField runat="server" ID="intermediarioPermiteAseguradoNoEncontrado" /></span>
<span class="arrange"><asp:HiddenField runat="server" ID="hidExisteTitular" /></span>
<span class="arrange"><asp:HiddenField runat="server" ID="IdTit" /></span>
<span class="arrange"><asp:HiddenField runat="server" ID="NacionalidadTit" /></span>
<span class="arrange"><asp:HiddenField runat="server" ID="NumDocTit" /></span>
<span class="arrange"><asp:HiddenField runat="server" ID="NomCompletoTit" /></span>
<span class="arrange"><asp:HiddenField runat="server" ID="NomContratante" /></span>
<span class="arrange"><asp:HiddenField runat="server" ID="IndBenefNoExiste" /></span>
<span class="arrange"><asp:HiddenField runat="server" ID="IdBenef" /></span>
<span class="arrange"><asp:HiddenField runat="server" ID="NacionalidadBenef" /></span>
<span class="arrange"><asp:HiddenField runat="server" ID="NumDocBenef" /></span>
<span class="arrange"><asp:HiddenField runat="server" ID="NomCompletoBenef" /></span>
<span class="arrange"><asp:HiddenField runat="server" ID="SexoBenef" /></span>
<span class="arrange"><asp:HiddenField runat="server" ID="SexoTit" /></span>
<span class="arrange"><asp:HiddenField runat="server" ID="ParentescoBenef" /></span>
<span class="arrange"><asp:HiddenField runat="server" ID="RamoBenef" /></span>
<span class="arrange"><asp:HiddenField runat="server" ID="FecVigDesde" /></span>
<span class="arrange"><asp:HiddenField runat="server" ID="FecVigHasta" /></span>
<span class="arrange"><asp:HiddenField runat="server" ID="estacion" /></span>
<span class="arrange"><asp:HiddenField runat="server" ID="NumPoliza" /></span>
<span class="arrange"><asp:HiddenField runat="server" ID="NumCertificado" /></span>
<span class="arrange"><asp:HiddenField runat="server" ID="EstadoTit" Value="Activo" /></span>
<span class="arrange"><asp:HiddenField runat="server" ID="EstadoBenef" Value="Activo" /></span>
<!-- CAMPOS A XML KEYS - FIN -->
<div runat="server" id="divHidden" style="visibility: collapse;">
	<asp:Button ID="btnBuscarGrupoFamiliar" runat="server" Text="BuscarGrupoFamiliar" CssClass="ocultar" OnClick="btnBuscarGrupoFamiliar_Click" CausesValidation="False" />
	<asp:Button ID="btnBuscarBeneficiario" runat="server" Text="BuscarBeneficiario" CssClass="ocultar" OnClick="btnBuscarBeneficiario_Click" CausesValidation="False" />
</div>
<script type="text/javascript">
	var imgUrl = null;
	function alertCallBackFn(arg) {
	    radalert("returned the following result:", 380, 50, "Result");
	}

	function GetSelectedPolizas(sender, eventArgs) {
		var grid = sender;
		var MasterTable = grid.get_masterTableView();
		var selectedRows = MasterTable.get_selectedItems();
		if (selectedRows.length > 0) {
			for (var i = 0; i < selectedRows.length; i++) {
				var row = selectedRows[i];
				var colName; var clientId;
				for (var j = 1; j < 3; j++) {
					switch (j) {
						case 1: colName = "poliza"; clientId = "<%= NumPoliza.ClientID %>"; break;
						case 2: colName = "certificado"; clientId = "<%= NumCertificado.ClientID %>"; break;
					}
					cell = MasterTable.getCellByColumnUniqueName(row, colName);
					var control = document.getElementById(clientId);
					control.value = cell.innerHTML;
				}
			}
		}
	}

	function DobleClickOnAsegurado() {
		var _btnBuscarGrupoFamiliar = document.getElementById("<%= btnBuscarGrupoFamiliar.ClientID %>");
		if (_btnBuscarGrupoFamiliar != null)
			_btnBuscarGrupoFamiliar.click();
	}

	function GetSelectedBeneficiarios(sender, eventArgs) {
		var grid = sender;
		var MasterTable = grid.get_masterTableView();
		var selectedRows = MasterTable.get_selectedItems();
		if (selectedRows.length > 0) {
			for (var i = 0; i < selectedRows.length; i++) {
				var row = selectedRows[i];
				var colName; var clientId;
				for (var j = 1; j < 5; j++) {
					switch (j) {
						case 1:
							colName = "cedulaBenef";
							clientId = "<%= NumDocBenef.ClientID %>";
							break;
						case 2:
							colName = "beneficiario";
							clientId = "<%= NomCompletoBenef.ClientID %>";
							break;
						case 3:
							colName = "sexo";
							clientId = "<%= SexoBenef.ClientID %>";
							break;
						case 4:
							colName = "parentesco";
							clientId = "<%= ParentescoBenef.ClientID %>";
							break;
					}
					cell = MasterTable.getCellByColumnUniqueName(row, colName);
					var control = document.getElementById(clientId);
					control.value = cell.innerHTML;
				}
			}
		}
	}

	function DobleClickOnBeneficiario() {
		var _btnBuscarBeneficiario = document.getElementById("<%= btnBuscarBeneficiario.ClientID %>");
		if (_btnBuscarBeneficiario != null)
			_btnBuscarBeneficiario.click();
	}

	var prm = Sys.WebForms.PageRequestManager.getInstance();
	prm.add_initializeRequest(prm_InitializeRequest);
	prm.add_endRequest(prm_EndRequest);
	$('#loadingDiv').hide();
	$('#contentDiv').show();

	function prm_InitializeRequest(sender, args) {
		var _botonPostBack = args._postBackElement.id.split("_");
		var aux = _botonPostBack[_botonPostBack.length - 1];
		if (aux == "btnBuscarAsegurado") {
			$('#loadingDiv').show();
			$('#contentDiv').hide();
		}
		else {
			$('#loadingDiv').hide();
			$('#contentDiv').show();
		}
	}

	function prm_EndRequest(sender, args) {
		$('#loadingDiv').hide();
		$('#contentDiv').show();
	}
</script>
