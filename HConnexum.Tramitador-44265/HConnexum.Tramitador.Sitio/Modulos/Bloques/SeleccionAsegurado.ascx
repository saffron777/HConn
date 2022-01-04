<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SeleccionAsegurado.ascx.cs" Inherits="HConnexum.Tramitador.Sitio.Modulos.Bloques.SeleccionAsegurado" %>

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

<script type="text/javascript">
    function regFormato(source, args) {
        var patt = /^0[24][12](\d{1})(\d{3})(\d{4})$/
        if (document.getElementById("<%= TlfSolicitante.ClientID %>").value != "") {
            if (document.getElementById("<%= TlfSolicitante.ClientID %>").value.search(patt) == 0) {
                args.IsValid = true;
            }
            else {
                if (document.getElementById("<%= TlfSolicitante.ClientID %>").value == "Campo obligatorio.")
                    args.IsValid = true;
                else
                    args.IsValid = false;
            }
        }
        else {
            args.IsValid = true;
        }
        return;
    }
</script>
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
<asp:Panel ID="pnlAfiliadoConsulta" runat="server" GroupingText="CONSULTA DE AFILIADO" CssClass="PanelMargen">
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
				<td class="labelCell4colP" style="vertical-align: top;"><asp:Label ID="Label1" 
                        runat="server" Text="Documento Identidad Titular/Beneficiario" /></td>
				<td class="labelCell4colP" style="vertical-align: top;">&nbsp;</td>
				<td class="labelCell4colP" style="vertical-align: top;">&nbsp;</td>
				<td class="labelCell4colP" style="vertical-align: top;">&nbsp;</td>
			</tr>
			<tr>
				<td class="fieldCell4colP" style="vertical-align: top;"><asp:TextBox ID="txtAseguradoCedula" runat="server" CssClass="number" MaxLength="8" Width="90%" TabIndex="1" /></td>
				<td class="fieldCell4colP" style="vertical-align: top;"><asp:Button ID="btnBuscarAsegurado" runat="server" Text="Buscar" OnClick="btnBuscarAsegurado_Click" TabIndex="2" CausesValidation="False" /></td>
				<td class="fieldCell4colP" style="vertical-align: top;">&nbsp;</td>
				<td class="fieldCell4colP" style="vertical-align: top;">&nbsp;</td>
			</tr>
			<tr>
				<td class="labelCell4colP" style="vertical-align: top;"><asp:Label ID="lblAfiliadoNoEncontradoMensaje" runat="server" Text="Si el paciente es beneficiario de la póliza, realice la búsqueda por la cédula del titular" Visible="False" /></td>
				<td class="labelCell4colP" style="vertical-align: top;"><asp:Button ID="btnAfiliadoNoEncontradoAgregar" runat="server" Text="Agregar beneficiario no existente" Visible="False" OnClick="btnAfiliadoNoEncontradoAgregar_Click" TabIndex="5" CausesValidation="False" /></td>
				<td class="labelCell4colP" style="vertical-align: top;">&nbsp;</td>
				<td class="labelCell4colP" style="vertical-align: top;">&nbsp;</td>
			</tr>
		</table>
	</div>
</asp:Panel>
<asp:Panel ID="pnlAfiliadoPolizas" runat="server" GroupingText="LISTADO DE PÓLIZAS ASOCIADAS A LA CONSULTA" Visible="False" CssClass="PanelMargen">
	<table runat="server" id="Table3" border="0" width="100%" class="ancho">
		<tr>
			<td style="width: 100%; vertical-align: top;">
				<asp:Label ID="lblNoRegistros" runat="server" Text="No existen registros a mostrar." Visible="False" />
				<telerik:RadGrid ID="rgAfiliadoPolizas" runat="server" AutoGenerateColumns="False" TabIndex="3" Width="100%" CellSpacing="0" Culture="es-ES" GridLines="None">
					<ClientSettings EnableRowHoverStyle="True" Selecting-AllowRowSelect="True" ClientEvents-OnRowDblClick="DobleClickOnAsegurado" ClientEvents-OnRowSelected="GetSelectedPolizas">
						<Selecting AllowRowSelect="True" CellSelectionMode="None" />
						<ClientEvents OnRowDblClick="DobleClickOnAsegurado" OnRowSelected="GetSelectedPolizas" />
					</ClientSettings>
					<MasterTableView ShowHeadersWhenNoRecords="true" NoMasterRecordsText="No records." DataKeyNames="idpoliza, ramo, FechaPolizaInicio, FechaPolizaFin, idAsegurado, cedulaBenef, nacionalidad, beneficiario, sexo, fechaNacimiento">
						<CommandItemSettings ExportToPdfText="Export to PDF" />
						<RowIndicatorColumn FilterControlAltText="Filter RowIndicator column" Visible="True" />
						<ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column" Visible="True" />
						<Columns>
							<telerik:GridBoundColumn DataField="contratante" HeaderText="Contratante" UniqueName="contratante">
								<HeaderStyle HorizontalAlign="Left" Width="18%" />
								<ItemStyle HorizontalAlign="Left" />
							</telerik:GridBoundColumn>
							<telerik:GridBoundColumn DataField="poliza" HeaderText="Póliza" UniqueName="poliza">
								<HeaderStyle HorizontalAlign="Center" Width="4%" />
								<ItemStyle HorizontalAlign="Center" />
							</telerik:GridBoundColumn>
							<telerik:GridBoundColumn DataField="certificado" HeaderText="Certificado" UniqueName="certificado">
								<HeaderStyle HorizontalAlign="Center" Width="10%" />
								<ItemStyle HorizontalAlign="Center" />
							</telerik:GridBoundColumn>
							<telerik:GridBoundColumn DataField="beneficiario"  HeaderText="Asegurado">
								<HeaderStyle HorizontalAlign="Left" Width="35%" />
								<ItemStyle HorizontalAlign="Left" />
							</telerik:GridBoundColumn>
							<telerik:GridBoundColumn DataField="cedulaBenef"  HeaderText="Documento de Identidad">
								<HeaderStyle HorizontalAlign="Center" Width="18%" />
								<ItemStyle HorizontalAlign="Center" />
							</telerik:GridBoundColumn>
							<telerik:GridBoundColumn DataField="fechaNacimiento" DataFormatString="{0:d}" HeaderText="Fecha de Nacimiento" Visible="False">
								<HeaderStyle HorizontalAlign="Center" Width="" />
								<ItemStyle HorizontalAlign="Center" />
							</telerik:GridBoundColumn>
							<telerik:GridBoundColumn DataField="parentesco" HeaderText="Parentesco">
								<HeaderStyle HorizontalAlign="Left" Width="10%" />
								<ItemStyle HorizontalAlign="Left" />
							</telerik:GridBoundColumn>
							<telerik:GridBoundColumn DataField="idpoliza" HeaderText="Póliza ID" Visible="false" />
							<telerik:GridBoundColumn DataField="ramo" HeaderText="Ramo" Visible="false" />
							<telerik:GridBoundColumn DataField="FechaPolizaInicio" HeaderText="Inicio" UniqueName="FechaPolizaInicio" Visible="false" />
							<telerik:GridBoundColumn DataField="FechaPolizaFin" HeaderText="Vence" UniqueName="FechaPolizaFin" Visible="false" />
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
				<telerik:RadGrid ID="rgPolizaGrupoFamiliar" runat="server" AutoGenerateColumns="False" AlternatingItemStyle-BackColor="#E1E1E1" TabIndex="4" Width="100%" ValidationSettings-EnableModelValidation="False" ValidationSettings-EnableValidation="False">
					<ClientSettings EnableRowHoverStyle="True" Selecting-AllowRowSelect="True" ClientEvents-OnRowDblClick="DobleClickOnBeneficiario" ClientEvents-OnRowSelected="GetSelectedBeneficiarios" />
					<MasterTableView ShowHeadersWhenNoRecords="true" NoMasterRecordsText="No records." DataKeyNames="beneficiario, cedulaBenef, sexo, idAsegurado, cedulaTitular, fechaNacimiento, nacionalidad, parentesco, ramo, idEstado">
						<Columns>
							<telerik:GridBoundColumn DataField="cedulaBenef" HeaderText="Documento de Identidad" UniqueName="cedulaBenef" HeaderStyle-Width="25%" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
							<telerik:GridBoundColumn DataField="beneficiario" HeaderText="Asegurado" UniqueName="beneficiario" HeaderStyle-Width="50%" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
							<telerik:GridBoundColumn DataField="sexo" HeaderText="Sexo" UniqueName="sexo" HeaderStyle-Width="10%" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
							<telerik:GridBoundColumn DataField="parentesco" HeaderText="Parentesco" UniqueName="parentesco" HeaderStyle-Width="15%" />
							<telerik:GridBoundColumn DataField="idAsegurado" HeaderText="AseguradoId" Visible="false" />
							<telerik:GridBoundColumn DataField="cedulaTitular" HeaderText="Titular Nro. Cédula" UniqueName="cedulaTitular" Visible="false" />
							<telerik:GridBoundColumn DataField="fechaNacimiento" HeaderText="Nacimiento" UniqueName="fechaNacimiento" Visible="false" />
							<telerik:GridBoundColumn DataField="nacionalidad" HeaderText="Nacionalidad" UniqueName="nacionalidad" Visible="false" />
							<telerik:GridBoundColumn DataField="ramo" HeaderText="Ramo" UniqueName="ramo" Visible="false" />
							<telerik:GridBoundColumn DataField="idEstado" HeaderText="Estado" UniqueName="idEstado" Visible="false" />
						</Columns>
					</MasterTableView>
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
			<td class="fieldCell4colP" style="vertical-align: top;"><asp:TextBox ID="SegundoNombreTit" runat="server" MaxLength="200" Width="73%" onkeypress="return ValidarSoloTexto(event);" /></td>
			<td class="fieldCell4colP" style="vertical-align: top;"><asp:TextBox ID="PrimerApellidoTit" runat="server" MaxLength="200" Width="71%" onkeypress="return ValidarSoloTexto(event);" /></td>
			<td class="fieldCell4colP" style="vertical-align: top;"><asp:TextBox ID="SegundoApellidoTit" runat="server" MaxLength="200" Width="71%" onkeypress="return ValidarSoloTexto(event);" /></td>
		</tr>
		<tr>
			<td class="labelCell4colP" style="vertical-align: top;"><asp:Label runat="server" 
                    Text="Documento Identidad" /></td>
			<td class="labelCell4colP" style="vertical-align: top;"><asp:Label runat="server" Text="Sexo" /></td>
			<td class="labelCell4colP" style="vertical-align: top;"><asp:Label runat="server" Text="Fecha de Nacimiento" /></td>
			<td class="labelCell4colP" style="vertical-align: top;"><asp:Label runat="server" Text="Contratante" /></td>
		</tr>
		<tr>
			<td class="fieldCell4colP" style="vertical-align: top;">
				<telerik:RadComboBox ID="tipdoctitular" runat="server" Width="45" DataValueField="Id" DataTextField="NombreValorCorto" />
				<asp:TextBox ID="txtTitularNumDoc" runat="server" MaxLength="8" CssClass="number" Width="51%" />
			</td>
			<td class="fieldCell4colP" style="vertical-align: top;">
				<telerik:RadComboBox ID="SexoTitu" runat="server" DataValueField="NombreValorCorto" DataTextField="NombreValorCorto" AppendDataBoundItems="True" CausesValidation="False" Culture="es-ES" EmptyMessage="Seleccione" Width="73%" />
				<asp:RequiredFieldValidator ID="rfvTitularSexo" runat="server" ControlToValidate="SexoTitu" ErrorMessage="*" Width="20px" CssClass="validator" />
			</td>
			<td class="fieldCell4colP" style="vertical-align: top;">
				<telerik:RadDatePicker ID="FecNacTit" runat="server" MinDate="1900-01-01" Culture="es-ES" Width="86%">
					<Calendar runat="server" FastNavigationStep="12" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x" />
					<DateInput runat="server" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" EnableSingleInputRendering="True" LabelWidth="64px" />
					<DatePopupButton HoverImageUrl="" ImageUrl="" />
				</telerik:RadDatePicker>
				<asp:RequiredFieldValidator ID="rfvTitularNacimientoFecha" runat="server" ControlToValidate="FecNacTit" ErrorMessage="*" Width="20px" CssClass="validator"></asp:RequiredFieldValidator>
			</td>
			<td class="fieldCell4colP" style="vertical-align: top;"><asp:TextBox ID="txtTitularContratante" runat="server" MaxLength="200" Width="71%" /></td>
		</tr>
		<tr>
			<td class="labelCell4colP" style="vertical-align: top;" colspan="4"><asp:Label runat="server" Text="¿El titular es el beneficiario?" /></td>
		</tr>
		<tr>
			<td class="fieldCell4colP" style="vertical-align: top;">
				<div style="float:left; width:40%;">
					<asp:RadioButtonList ID="optListTitularEsBeneficiario" runat="server" Width="100%" RepeatDirection="Horizontal" AutoPostBack="True" OnSelectedIndexChanged="optListTitularEsBeneficiario_SelectedIndexChanged" CssClass="RadioButtonListValidator">
						<asp:ListItem Text="Si" Value="Si"></asp:ListItem>
						<asp:ListItem Text="No" Value="No"></asp:ListItem>
					</asp:RadioButtonList>
				</div>
				<div style="float:left; width:60%;">
					<asp:RequiredFieldValidator ID="rfvTitularEsBeneficiario" runat="server" ControlToValidate="optListTitularEsBeneficiario" ErrorMessage="*" Width="100%" CssClass="validator" />
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
			<td class="labelCell4colP" style="vertical-align: top;"><asp:Label runat="server" Text="Primer Nombre"  /></td>
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
				<telerik:RadComboBox ID="parentescobenefne" runat="server" DataValueField="NombreValor" DataTextField="NombreValor" AppendDataBoundItems="True" AutoPostBack="true"
					CausesValidation="false" Culture="es-ES" OnSelectedIndexChanged="parentescobenefne_SelectedIndexChanged" Width="75%" EmptyMessage="Seleccione">
				</telerik:RadComboBox>
				<asp:RequiredFieldValidator ID="rfvBeneficiarioParentesco" runat="server" ControlToValidate="parentescobenefne" ErrorMessage="*" Width="20px" CssClass="validator" />
			</td>
			<td class="fieldCell4colP" style="vertical-align: top;">
				<telerik:RadComboBox ID="tipdocbenefne" runat="server" Width="45" DataValueField="Id" DataTextField="NombreValorCorto" />
				<asp:TextBox ID="numdocbenefne" runat="server" MaxLength="8" CssClass="number" Width="51%" />
			</td>
			<td class="fieldCell4colP" style="vertical-align: top;">
				<telerik:RadComboBox ID="sexobenefne" runat="server" DataValueField="NombreValorCorto" DataTextField="NombreValorCorto" AppendDataBoundItems="True" AutoPostBack="True"
					CausesValidation="False" Culture="es-ES" OnSelectedIndexChanged="sexobenefne_SelectedIndexChanged" Width="72%" EmptyMessage="Seleccione" />
				<asp:RequiredFieldValidator ID="rfvBeneficiarioSexo" runat="server" ControlToValidate="sexobenefne" ErrorMessage="*" Width="20px" CssClass="validator" />
			</td>
			<td class="fieldCell4colP" style="vertical-align: top;">
				<telerik:RadDatePicker ID="FecNacBenef" runat="server" MinDate="1900-01-01" Culture="es-ES" Width="86%">
					<Calendar runat="server" FastNavigationStep="12" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x" />
					<DateInput runat="server" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" EnableSingleInputRendering="True" LabelWidth="64px" />
					<DatePopupButton HoverImageUrl="" ImageUrl="" />
				</telerik:RadDatePicker>
				<asp:RequiredFieldValidator ID="rfvFechaNacBenef" runat="server" ControlToValidate="FecNacBenef" ErrorMessage="*" Width="20px" CssClass="validator" />
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
				<div style="float:left; width:40%;">
					<asp:RadioButtonList ID="IndMenor" runat="server" Width="100%" RepeatDirection="Horizontal" AutoPostBack="true" CssClass="RadioButtonListValidator" onselectedindexchanged="IndMenor_SelectedIndexChanged">
						<asp:ListItem Text="Si" Value="Si"></asp:ListItem>
						<asp:ListItem Text="No" Value="No"></asp:ListItem>
					</asp:RadioButtonList>
				</div>
				<div style="float:left; width:60%;">
					<asp:RequiredFieldValidator ID="rfvBeneficiarioMenorSinCedula" runat="server" ControlToValidate="IndMenor" ErrorMessage="*" Width="100%" CssClass="validator" />
				</div>
			</td>
			<td class="fieldCell4colP" style="vertical-align: top;">
				<div style="float:left; width:40%;">
					<asp:RadioButtonList ID="IndCondEspecial" runat="server" Width="100%" RepeatDirection="Horizontal" CssClass="RadioButtonListValidator">
						<asp:ListItem Text="Si" Value="Si"></asp:ListItem>
						<asp:ListItem Text="No" Value="No"></asp:ListItem>
					</asp:RadioButtonList>
				</div>
			</td>
			<td class="fieldCell4colP" style="vertical-align: top;">&nbsp;</td>
			<td class="fieldCell4colP" style="vertical-align: top;">&nbsp;</td>
		</tr>
		<tr>
			<td class="labelCell4colP" colspan="4" style="vertical-align: top;"><asp:Label ID="lblBeneficiarioMensaje" runat="server" /></td>
		</tr>
	</table>
</asp:Panel>
<asp:Panel ID="pnlSolicitudDatos" runat="server" GroupingText="DATOS SOLICITUD" Visible="False" CssClass="PanelMargen">
	<asp:Label ID="lblSeccionSolicitudMensaje" runat="server" Visible="False" />
	<table runat="server" id="T7" border="0" width="100%" class="ancho">
		<tr>
			<td class="labelCell3colP" style="vertical-align: top;"><asp:Label runat="server" Text="Número de Celular" /></td>
			<td class="labelCell3colP" style="vertical-align: top;"><asp:Label runat="server" Text="Fecha de Ocurrencia" /></td>
			<td class="labelCell3colP" style="vertical-align: top;"><asp:Label runat="server" Text="Síntomas" /></td>
		</tr>
		<tr>
			<td class="fieldCell3colP" style="vertical-align: top;"><asp:TextBox ID="TlfSolicitante" runat="server" CssClass="number" MaxLength="11" Width="80%" /> <asp:CustomValidator runat="server" ID="CustomValidator1" ClientValidationFunction="regFormato" ErrorMessage="*" Width="8%" CssClass="validator" ValidationGroup="Validaciones"/></td>
			<td class="fieldCell3colP" style="vertical-align: top;">
				<telerik:RadDatePicker ID="FecOcurrencia" runat="server" Culture="es-ES" MinDate="1900-01-01" Width="90%" >
					<Calendar runat="server" FastNavigationStep="12" UseColumnHeadersAsSelectors="false" UseRowHeadersAsSelectors="false" ViewSelectorText="x" />
					<DateInput runat="server" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" EnableSingleInputRendering="true" LabelWidth="64px"/>
				</telerik:RadDatePicker>
				<asp:RequiredFieldValidator ID="rfvFechaOcurrencia" runat="server" ControlToValidate="FecOcurrencia" ErrorMessage="*" Width="20px" CssClass="validator" />
			</td>
			<td class="fieldCell3colP" style="vertical-align: top;" rowspan="3"><asp:TextBox ID="NomSintomas" runat="server" MaxLength="200" TextMode="MultiLine" Rows="4" Width="90%" Style="overflow: auto;" /></td>
		</tr>
		<tr>
			<td class="labelCell3colP" style="vertical-align: top;"><asp:Label ID="lblSolicitudNacimientoFecha" runat="server" Text="Fecha de Nacimiento" Visible="false" /></td>
			<td class="labelCell3colP" style="vertical-align: top;"><asp:Label ID="lblSolicitudEdad" runat="server" Text="Edad" Visible="false" /></td>
		</tr>
		<tr>
			<td class="fieldCell3colP" style="vertical-align: top;">
				<telerik:RadDatePicker ID="dtpAseguradoNacimientoFecha" runat="server" Visible="False" Culture="es-ES" MinDate="1900-01-01" Width="91%">
					<Calendar runat="server" FastNavigationStep="12" UseColumnHeadersAsSelectors="False" UseRowHeadersAsSelectors="False" ViewSelectorText="x" />
					<DateInput runat="server" DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy" EnableSingleInputRendering="True" LabelWidth="64px" />
					<DatePopupButton HoverImageUrl="" ImageUrl="" />
				</telerik:RadDatePicker>
				<asp:RequiredFieldValidator ID="rfvAseguradoNacimientoFecha" runat="server"  ControlToValidate="dtpAseguradoNacimientoFecha"  ErrorMessage="*" Width="20px" CssClass="validator" />
			</td>
			<td class="fieldCell3colP" style="vertical-align: top;">
				<asp:TextBox ID="txtAseguradoEdad" runat="server" MaxLength="3" CssClass="number" Visible="false" Width="80%" />
			</td>
		</tr>
	</table>
</asp:Panel>
<asp:Panel ID="pnlResponsableDatos" runat="server" GroupingText="DATOS DEL RESPONSABLE DEL PACIENTE ANTE LA CLÍNICA" Visible="False" CssClass="PanelMargen">
	<table runat="server" id="T8" border="0" width="100%" class="ancho">
		<tr>
			<td class="labelCell3colP" style="vertical-align: top;"><asp:Label runat="server" Text="Nombre" /></td>
			<td class="labelCell3colP" style="vertical-align: top;"><asp:Label runat="server" Text="Teléfono de Contacto" /></td>
			<td class="labelCell3colP" style="vertical-align: top;"><asp:Label runat="server" Text="Email" /></td>
		</tr>
		<tr>
			<td class="fieldCell3colP" style="vertical-align: top;"><asp:TextBox ID="NomResponsable" runat="server" MaxLength="200" Width="80%" onkeypress="return ValidarSoloTexto(event);" /></td>
			<td class="fieldCell3colP" style="vertical-align: top;"><asp:TextBox ID="tlfresponsable" runat="server" MaxLength="11" CssClass="number" Width="80%" /></td>
			<td class="fieldCell3colP" style="vertical-align: top;"><asp:TextBox ID="EmailResponsable" runat="server" Width="90%" /></td>
		</tr>
	</table>
</asp:Panel>
<telerik:RadInputManager ID="RadInputManager1" runat="server">
	<telerik:TextBoxSetting BehaviorID="txtSettings">
		<TargetControls>
			<telerik:TargetInput ControlID="PrimerNombreTit" />
			<telerik:TargetInput ControlID="PrimerApellidoTit" />
			<telerik:TargetInput ControlID="txtTitularContratante" />
			<telerik:TargetInput ControlID="PrimerNombreBenef" />
			<telerik:TargetInput ControlID="PrimerApellidoBenef" />
			<telerik:TargetInput ControlID="txtAseguradoEdad" />
			<telerik:TargetInput ControlID="NomSintomas" />
			<telerik:TargetInput ControlID="TlfSolicitante" />
			<telerik:TargetInput ControlID="numdocbenefne" />
		</TargetControls>
	</telerik:TextBoxSetting>
	<telerik:TextBoxSetting BehaviorID="txtNumericSettings" EnabledCssClass="number">
		<TargetControls>
			<telerik:TargetInput ControlID="txtTitularNumDoc" />
			<%--<telerik:TargetInput ControlID="numdocbenefne" />--%>
		</TargetControls>
	</telerik:TextBoxSetting>
	<telerik:RegExpTextBoxSetting BehaviorID="txtEmailSettings" ValidationExpression="^([0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$" ErrorMessage="Formato Email incorrecto.">
		<TargetControls>
			<telerik:TargetInput ControlID="EmailResponsable" />

		</TargetControls>
	</telerik:RegExpTextBoxSetting>
	<telerik:RegExpTextBoxSetting BehaviorID="txtCelularSettings" ValidationExpression="^0[24][12](\d{1})(\d{3})(\d{4})$" IsRequiredFields="true" ErrorMessage="Formato telefono incorrecto.">
		<TargetControls>
			<telerik:TargetInput ControlID="TlfSolicitante" />
			<telerik:TargetInput ControlID="tlfresponsable" />
		</TargetControls>
	</telerik:RegExpTextBoxSetting>
</telerik:RadInputManager>
<!-- CAMPOS A XML KEYS - INICIO -->
<span class="arrange"><asp:HiddenField runat="server" ID="categoria" /></span>
<span class="arrange"><asp:HiddenField runat="server" ID="IdSusIntermediario" /></span>
<span class="arrange"><asp:HiddenField runat="server" ID="IdIntermediario" /></span>
<span class="arrange"><asp:HiddenField runat="server" ID="IdCodEnlaceIntermediario" /></span>
<span class="arrange"><asp:HiddenField runat="server" ID="intermediarioPermiteAseguradoNoEncontrado" /></span>
<span class="arrange"><asp:HiddenField runat="server" ID="IdSusProveedor" /></span>
<span class="arrange"><asp:HiddenField runat="server" ID="IdProveedor" /></span>
<span class="arrange"><asp:HiddenField runat="server" ID="IdCodEnlaceProveedor" /></span>
<span class="arrange"><asp:HiddenField runat="server" ID="IdContratante" /></span>
<span class="arrange"><asp:HiddenField runat="server" ID="NomContratante" /></span>
<span class="arrange"><asp:HiddenField runat="server" ID="NumPoliza" /></span>
<span class="arrange"><asp:HiddenField runat="server" ID="FecVigDesde" /></span>
<span class="arrange"><asp:HiddenField runat="server" ID="FecVigHasta" /></span>
<span class="arrange"><asp:HiddenField runat="server" ID="NumCertificado" /></span>
<span class="arrange"><asp:HiddenField runat="server" ID="hidExisteTitular" /></span>
<span class="arrange"><asp:HiddenField runat="server" ID="IdTit" /></span>
<span class="arrange"><asp:HiddenField runat="server" ID="NumDocTit" /></span>
<span class="arrange"><asp:HiddenField runat="server" ID="NomCompletoTit" /></span>
<span class="arrange"><asp:HiddenField runat="server" ID="EstadoTit" Value="Activo" /></span>
<span class="arrange"><asp:HiddenField runat="server" ID="NacionalidadTit" /></span>
<span class="arrange"><asp:HiddenField runat="server" ID="IndBenefNoExiste" /></span>
<span class="arrange"><asp:HiddenField runat="server" ID="IdBenef" /></span>
<span class="arrange"><asp:HiddenField runat="server" ID="NumDocBenef" /></span>
<span class="arrange"><asp:HiddenField runat="server" ID="NomCompletoBenef" /></span>
<span class="arrange"><asp:HiddenField runat="server" ID="SexoBenef" /></span>
<span class="arrange"><asp:HiddenField runat="server" ID="ParentescoBenef" /></span>
<span class="arrange"><asp:HiddenField runat="server" ID="NacionalidadBenef" /></span>
<span class="arrange"><asp:HiddenField runat="server" ID="EstadoBenef" Value="Activo" /></span>
<span class="arrange"><asp:HiddenField runat="server" ID="RamoBenef" /></span>
<span class="arrange"><asp:HiddenField runat="server" ID="NomTipoServicio" Value="Clave" /></span>
<span class="arrange"><asp:HiddenField runat="server" ID="FecSolicitud" /></span>
<span class="arrange"><asp:HiddenField runat="server" ID="estacion" /></span>
<span class="arrange"><asp:HiddenField runat="server" ID="TipoMov" Value="VERIFICACION" /></span>
<span class="arrange"><asp:HiddenField runat="server" ID="SexoTit" /></span>
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
				for (var j = 1; j < 4; j++) {
					switch (j) {
						case 1: colName = "contratante"; clientId = "<%= NomContratante.ClientID %>"; break;
						case 2: colName = "poliza"; clientId = "<%= NumPoliza.ClientID %>"; break;
						case 3: colName = "certificado"; clientId = "<%= NumCertificado.ClientID %>"; break;
						case 4: colName = "beneficiario"; clientId = "<%= NomCompletoTit.ClientID %>"; break;
						case 5: colName = "cedulaBenef"; clientId = "<%= NumDocTit.ClientID %>"; break;
					}
					cell = MasterTable.getCellByColumnUniqueName(row, colName);
					var control = document.getElementById(clientId);
					control.value = cell.innerHTML;
				}
			}
		}
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
						case 1: colName = "cedulaBenef"; clientId = "<%= NumDocBenef.ClientID %>"; break;
						case 2: colName = "beneficiario"; clientId = "<%= NomCompletoBenef.ClientID %>"; break;
						case 3: colName = "sexo"; clientId = "<%= SexoBenef.ClientID %>"; break;
						case 4: colName = "parentesco"; clientId = "<%= ParentescoBenef.ClientID %>"; break;
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
