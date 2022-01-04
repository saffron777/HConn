<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Egreso.ascx.cs" Inherits="HConnexum.Tramitador.Sitio.Modulos.Bloques.Egreso" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/ControlesComunes/MultilineCounter.ascx" TagName="MultilineCounter" TagPrefix="uc1" %>

<style type="text/css">
	body { font-family: Verdana; font-size: 0.70em; }
	td.labelCell { margin: 0; padding: 5px 0 0 5px; text-align: left; width: 200px; }
	td.labelCellBold { margin: 0; padding: 5px 0 0 5px; text-align: left; font-weight: bold; width: 200px; }
	.negrita { font-weight:bold }
	.styleComboBox { font-family: Arial; font-size: 16pt }
	.style1 { width: 33%; }
	.style3 { width: 50%; }
	.style18 { width: 205px; }
	.styleLabel { vertical-align: text-bottom; }
    .style19 { width: 33%; height: 17px; }
    .style20 { width: 33%; height: 26px; }
    .cssButton {background-color:#d9e5f2;}
</style>

<div style="margin:auto; width:940px;">

<asp:Panel ID="Panel9" runat="server" GroupingText="Datos de la Póliza"  width="100%">
	<asp:Table ID="Table9" runat="server" CssClass="ancho">
		<asp:TableRow>
			<asp:TableCell CssClass="labelCellBold" Width="462">
				<asp:Label ID="Label30" runat="server" Text="Contratante"></asp:Label>
			</asp:TableCell>
			<asp:TableCell CssClass="labelCell" Width="462">
				<asp:Label ID="Label31" runat="server" Text="Empaquetadora El Rancel"></asp:Label>
			</asp:TableCell>
		</asp:TableRow>
		<asp:TableRow>
			<asp:TableCell CssClass="labelCellBold" Width="462">
				<asp:Label ID="Label32" runat="server" Text="Póliza"></asp:Label>
			</asp:TableCell>
			<asp:TableCell CssClass="labelCellBold" Width="462">
				<asp:Label ID="Label33" runat="server" Text="Certificado"></asp:Label>
			</asp:TableCell>
			<asp:TableCell CssClass="labelCellBold" Width="462">
				<asp:Label ID="Label34" runat="server" Text="Fecha Desde"></asp:Label>
			</asp:TableCell>
			<asp:TableCell CssClass="labelCellBold" Width="462">
				<asp:Label ID="Label35" runat="server" Text="Fecha Hasta"></asp:Label>
			</asp:TableCell>
		</asp:TableRow>
		<asp:TableRow>
			<asp:TableCell CssClass="labelCell" Width="462">
				<asp:Label ID="Label37" runat="server" Text="100000"></asp:Label>
			</asp:TableCell>
			<asp:TableCell CssClass="labelCell" Width="462">
				<asp:Label ID="Label38" runat="server" Text="234517"></asp:Label>
			</asp:TableCell>
			<asp:TableCell CssClass="labelCell" Width="462">
				<asp:Label ID="Label39" runat="server" Text="21/03/2012"></asp:Label>
			</asp:TableCell>
			<asp:TableCell CssClass="labelCell" Width="462">
				<asp:Label ID="Label40" runat="server" Text="21/03/2013"></asp:Label>
			</asp:TableCell>
		</asp:TableRow>
	</asp:Table>
</asp:Panel>
<br />
<asp:Panel ID="Panel10" runat="server" GroupingText="Datos del Titular"  width="100%">
	<asp:Table ID="Table10" runat="server" CssClass="ancho">
		<asp:TableRow>
			<asp:TableCell CssClass="labelCellBold" Width="462">
				<asp:Label ID="Label41" runat="server" Text="Asegurado"></asp:Label>
			</asp:TableCell>
			<asp:TableCell CssClass="labelCell" Width="462">
				<asp:Label ID="Label42" runat="server" Text="Guevara Francisco"></asp:Label>
			</asp:TableCell>
		</asp:TableRow>
		<asp:TableRow>
			<asp:TableCell CssClass="labelCellBold" Width="462">
				<asp:Label ID="Label43" runat="server" Text="Cédula"></asp:Label>
			</asp:TableCell>
			<asp:TableCell CssClass="labelCellBold" Width="462">
				<asp:Label ID="Label44" runat="server" Text="Sexo"></asp:Label>
			</asp:TableCell>
			<asp:TableCell CssClass="labelCellBold" Width="462">
				<asp:Label ID="Label45" runat="server" Text="Fecha de Nacimiento"></asp:Label>
			</asp:TableCell>
			<asp:TableCell CssClass="labelCellBold" Width="462">
				<asp:Label ID="Label46" runat="server" Text="Estado"></asp:Label>
			</asp:TableCell>
		</asp:TableRow>
		<asp:TableRow>
			<asp:TableCell CssClass="labelCell" Width="462">
				<asp:Label ID="Label47" runat="server" Text="CIV-4578903"></asp:Label>
			</asp:TableCell>
			<asp:TableCell CssClass="labelCell" Width="462">
				<asp:Label ID="Label48" runat="server" Text="M"></asp:Label>
			</asp:TableCell>
			<asp:TableCell CssClass="labelCell" Width="462">
				<asp:Label ID="Label49" runat="server" Text="12/10/1978"></asp:Label>
			</asp:TableCell>
			<asp:TableCell CssClass="labelCell" Width="462">
				<asp:Label ID="Label50" runat="server" Text="A"></asp:Label>
			</asp:TableCell>
		</asp:TableRow>
	</asp:Table>
</asp:Panel>
<br />
<asp:Panel ID="Panel1" runat="server" GroupingText="Datos del Paciente"  width="100%">
	<asp:Table ID="Table1" runat="server" CssClass="ancho">
		<asp:TableRow>
			<asp:TableCell CssClass="labelCellBold" Width="462">
				<asp:Label ID="Label1" runat="server" Text="Asegurado"></asp:Label>
			</asp:TableCell>
			<asp:TableCell CssClass="labelCell" Width="462">
				<asp:Label ID="Label2" runat="server" Text="Guevara Francisco"></asp:Label>
			</asp:TableCell>
		</asp:TableRow>
		<asp:TableRow>
			<asp:TableCell CssClass="labelCellBold" Width="462">
				<asp:Label ID="Label3" runat="server" Text="Cédula"></asp:Label>
			</asp:TableCell>
			<asp:TableCell CssClass="labelCellBold" Width="462">
				<asp:Label ID="Label4" runat="server" Text="Sexo"></asp:Label>
			</asp:TableCell>
			<asp:TableCell CssClass="labelCellBold" Width="462">
				<asp:Label ID="Label5" runat="server" Text="Fecha de Nacimiento"></asp:Label>
			</asp:TableCell>
			<asp:TableCell CssClass="labelCellBold" Width="462">
				<asp:Label ID="Label6" runat="server" Text="Estado"></asp:Label>
			</asp:TableCell>
			<asp:TableCell CssClass="labelCellBold" Width="462">
				<asp:Label ID="Label7" runat="server" Text="Parentesco"></asp:Label>
			</asp:TableCell>
		</asp:TableRow>
		<asp:TableRow>
			<asp:TableCell CssClass="labelCell" Width="462">
				<asp:Label ID="Label8" runat="server" Text="CIV-4578903"></asp:Label>
			</asp:TableCell>
			<asp:TableCell CssClass="labelCell" Width="462">
				<asp:Label ID="Label9" runat="server" Text="M"></asp:Label>
			</asp:TableCell>
			<asp:TableCell CssClass="labelCell" Width="462">
				<asp:Label ID="Label10" runat="server" Text="12/10/1978"></asp:Label>
			</asp:TableCell>
			<asp:TableCell CssClass="labelCell" Width="462">
				<asp:Label ID="Label11" runat="server" Text="A"></asp:Label>
			</asp:TableCell>
			<asp:TableCell CssClass="labelCell" Width="462">
				<asp:Label ID="Label12" runat="server" Text="TITULAR"></asp:Label>
			</asp:TableCell>
		</asp:TableRow>
	</asp:Table>
</asp:Panel>
<br />
<asp:Panel ID="Panel2" runat="server" GroupingText="Datos del Expediente"  width="100%">
	<asp:Table ID="Table2" runat="server" CssClass="ancho">
		<asp:TableRow>
			<asp:TableCell CssClass="labelCellBold" Width="462">
				<asp:Label ID="Label13" runat="server" Text="Diagnóstico"></asp:Label>
			</asp:TableCell>
			<asp:TableCell CssClass="labelCell" Width="462">
				<asp:Label ID="Label14" runat="server" Text="Dolor abdominal"></asp:Label>
			</asp:TableCell>
		</asp:TableRow>
		<asp:TableRow>
			<asp:TableCell CssClass="labelCellBold" Width="462">
				<asp:Label ID="Label15" runat="server" Text="Clave"></asp:Label>
			</asp:TableCell>
			<asp:TableCell CssClass="labelCellBold" Width="462">
				<asp:Label ID="Label16" runat="server" Text="Fecha Ocurrencia"></asp:Label>
			</asp:TableCell>
			<asp:TableCell CssClass="labelCellBold" Width="462">
				<asp:Label ID="Label17" runat="server" Text="Ultimo movimiento hecho"></asp:Label>
			</asp:TableCell>
		</asp:TableRow>
		<asp:TableRow>
			<asp:TableCell CssClass="labelCell" Width="462">
				<asp:Label ID="Label18" runat="server" Text="4025"></asp:Label>
			</asp:TableCell>
			<asp:TableCell CssClass="labelCell" Width="462">
				<asp:Label ID="Label19" runat="server" Text="13/07/2012"></asp:Label>
			</asp:TableCell>
			<asp:TableCell CssClass="labelCell" Width="462">
				<asp:Label ID="Label20" runat="server" Text="VERIFICACIÓN"></asp:Label>
			</asp:TableCell>
		</asp:TableRow>
	</asp:Table>
</asp:Panel>

<asp:Panel ID="Panel4" runat="server" GroupingText="Datos del Movimiento" width="100%">
	<table style="width: 760px">
		<tr>
			<td></td>
		</tr>
		<tr>
			<td>
				<asp:Table ID="Table5" runat="server" CssClass="ancho">
					<asp:TableRow>
						<asp:TableCell CssClass="labelCellBold" Width="662">
							<asp:Label ID="Label24" runat="server" Text="Los campos indicados con (*) son obligatorios."></asp:Label>
						</asp:TableCell>
					</asp:TableRow>
				</asp:Table>
			</td>
		</tr>
		<tr>
			<td>
				<table style="width: 100%;">
					<tr>
						<td class="style3">
							<asp:Label ID="LabelDiagnostico" runat="server" Text="Diagnostico" 
                                CssClass="negrita"></asp:Label>
							&nbsp;
						</td>
						<td class="style3">
							<asp:Label ID="LabelProcedimiento" runat="server" Text="Procedimiento" 
                                CssClass="negrita"></asp:Label>
						</td>
					</tr>
					<tr>
						<td class="style3">
							<telerik:RadComboBox CssClass="styleComboBox" ID="RadComboBoxDiagnostico" runat="server" AutoPostBack="true" DataTextField="Diagnostico" DataValueField="Valor" EmptyMessage="Seleccione ..." EnableLoadOnDemand="true" MarkFirstMatch="true" 
												 width="100%" OnSelectedIndexChanged="RadComboBoxDiagnostico_SelectedIndexChanged">
							</telerik:RadComboBox>
						</td>
						<td class="style3">
							<telerik:RadComboBox CssClass="styleComboBox" ID="RadComboBoxProcedimiento" runat="server" DataTextField="Procedimiento" DataValueField="Valor" EmptyMessage="Seleccione ..." EnableLoadOnDemand="true" MarkFirstMatch="true" width="100%">
							</telerik:RadComboBox>
							</td>
					</tr>

				</table>
			</td>
		</tr>
		<tr>
			<td>
				<table width="100%">
					<tr>
						<td class="style1">
							<asp:Label ID="Label25" runat="server" Text="Tipo de movimiento" 
                                CssClass="negrita"></asp:Label>
						</td>
						<td class="style18" style="width: 10px" >
							&nbsp;</td>
						<td class="style1"><span class="negrita">Modo</span></td>
						<td style="width: 10px" >
							&nbsp;</td>
						<td class="style1">
						<asp:Label ID="Label28" runat="server" Text="Médico tratante" CssClass="negrita"></asp:Label>
						</td>
					    <td class="style1">
                            &nbsp;</td>
					</tr>
					<tr>
						<td style="vertical-align:top" class="style1">
							<telerik:RadComboBox CssClass="styleComboBox" ID="RadComboBoxDiagnosticoTipoMovimiento" runat="server" EmptyMessage="Seleccione ..." EnableLoadOnDemand="true" MarkFirstMatch="true" 
												 Width="100%"  DataTextField="Ingreso" DataValueField="Valor">
							</telerik:RadComboBox>
						</td>
						<td style="width: 10px;" >
							&nbsp;</td>
						<td style="vertical-align:top" class="style1">
						<telerik:RadComboBox CssClass="styleComboBox" ID="RadComboBoxDiagnosticoModo" runat="server" DataTextField="Modo" DataValueField="Valor" EmptyMessage="Seleccione ..." EnableLoadOnDemand="true" 
                                             Width="100%" MarkFirstMatch="true">
						</telerik:RadComboBox>
						&nbsp;</td>
						<td style="width: 10px;" >
							</td>
						<td style="vertical-align:top" class="style1">
						<asp:TextBox ID="TextBox15" runat="server" Width="100%"></asp:TextBox>
						</td>
					    <td class="style1" style="vertical-align:top">
                            &nbsp;</td>
					</tr>
					<tr>
						<td style="height: 17px" class="style1">
							<asp:Label ID="Label52" runat="server" Text="Monto Presupuesto" 
                                CssClass="negrita" ></asp:Label>
						</td>
						<td class="style18" style="height: 17px; width: 10px">
							</td>
						<td style="height: 17px; " class="style1">
                            <asp:Label ID="Label53" runat="server" 
                                Text="Días de hospitalización acumulados" CssClass="negrita"></asp:Label>
                        </td>
						<td style="height: 17px; width: 10px">
							</td>
						<td class="style19">
						<asp:Label ID="Label54" runat="server" Text="Observaciones/Diagnóstico" 
                                CssClass="negrita"></asp:Label>
						</td>
					    <td class="style19">
                            &nbsp;</td>
					</tr>
					<tr>
						<td style="height: 26px" class="style1">
							<asp:TextBox ID="TextBox12" runat="server" Text="" Width="100%"></asp:TextBox>
						</td>
						<td class="style18" style="height: 26px; width: 10px;">
							&nbsp;</td>
						<td style="height: 26px; " class="style1">
                            <asp:TextBox ID="TextBox13" runat="server" Text="0" Width="100%"></asp:TextBox>
                        </td>
						<td style="height: 26px; width: 10px;">
							&nbsp;</td>
						<td class="style20">
						<asp:TextBox ID="TextBox14" runat="server" Text="" Width="100%"></asp:TextBox>
						</td>
					    <td class="style20">
                            &nbsp;</td>
					</tr>
				</table>

			</td>
		</tr>
	</table>
   
</asp:Panel>

<asp:Panel GroupingText="Documentos" runat="server" Style="width: 100%" 
    ID="Panel11">
	<table style="width: 100%">
		<tr>
			<td style="width:50%; vertical-align:top">
            <br />
				<table width="100%">
					<tr>
						<td>
							<asp:CheckBox ID="CheckBox1" Text="Informe M&eacute;dico" runat="server" />
						</td>
						<td>
							<asp:CheckBox ID="CheckBox2" Text="Radiolog&iacute;a" runat="server" />
						</td>
						<td>
							<asp:CheckBox ID="CheckBox3" Text="Presupuesto" runat="server" />
						</td>
						<td>
							<asp:CheckBox ID="CheckBox4" Text="Carta Narrativa" runat="server" />
						</td>
					</tr>
					<tr><td>&nbsp;</td></tr>
					<tr>
						<td>
							<asp:CheckBox ID="CheckBox5" Text="Facturas" runat="server" />
						</td>
						<td>
							<asp:CheckBox ID="CheckBox6" Text="Laboratorio" runat="server" />
						</td>
						<td>
							<asp:CheckBox ID="CheckBox7" Text="Datos Filiatorios" runat="server" />
						</td>
					</tr>
				</table>
			</td>
			<td style="width:50%; vertical-align:top">
				<table width="100%">
					<tr>
						<td align="left"><strong>Otros Documentos:</strong></td>
					</tr>
					<tr>
						<td>
							<uc1:MultilineCounter ID="Ingreso1" runat="server" Rows="5" Width="450" />
						</td>
					</tr>
				</table>
			</td>
		</tr>
	</table>
</asp:Panel>
<table>
	<tr>
		<td>&nbsp;</td>
		<td>&nbsp;</td>
		<td>&nbsp;</td>
	</tr>
	<tr>
		<td><asp:Button ID="Button1" Text="Guardar" runat="server" CssClass="cssButton"/></td>
		<td><asp:Button ID="Button2" Text="Guardar y Continuar" runat="server" 
                CssClass="cssButton"/></td>
		<td><asp:Button ID="Button3" Text="Cancelar" runat="server" CssClass="cssButton"/></td>
	</tr>
	<tr>
		<td>&nbsp;</td>
		<td>&nbsp;</td>
		<td>&nbsp;</td>
	</tr>
</table>
</div>
