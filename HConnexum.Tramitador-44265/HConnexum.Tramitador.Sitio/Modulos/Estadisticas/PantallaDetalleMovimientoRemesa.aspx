<%@ Page Title="Detalle de la Solicitud" Language="C#" MasterPageFile="~/Master/Site.Master" AutoEventWireup="true" CodeBehind="PantallaDetalleMovimientoRemesa.aspx.cs" Inherits="HConnexum.Tramitador.Sitio.Modulos.Estadisticas.PantallaDetalleMovimientoRemesa" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
	<style type="text/css">
	table.ancho { font-weight: bold; }
	.PanelMargen { margin-top: 10px; }
	.ocultar { visibility: hidden; }
	.boton-accion { background-color: #d9e5f2; }
	.readOnly { background: #D7D7D7; }
	.number { text-align: right; }
	.arrange { float: left; }
	html { overflow-x: hidden; }
	</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
	<asp:Panel runat="server" GroupingText="DATOS DEL TITULAR">
		<table width="100%" cellpadding="0" cellspacing="0" border="0">
			<tr>
				<td class="labelCell4colP" style="vertical-align: top;"><asp:Label ID="lblAsegurado" runat="server" Text="Asegurado" /></td>
				<td class="fieldCell4colP" style="vertical-align: top;" colspan="3"><asp:Label ID="asegurado" runat="server" /></td>
			</tr>
			<tr>
				<td class="labelCell4colP" style="vertical-align: top;"><asp:Label ID="lblCedulaTitular" runat="server" Text="Documento de Identidad del Titular" /></td>
				<td class="labelCell4colP" style="vertical-align: top;"><asp:Label ID="lblSexo" runat="server" Text="Sexo" /></td>
				<td class="labelCell4colP" style="vertical-align: top;"><asp:Label ID="lblFechaNacimiento" runat="server" Text="Fecha de Nacimiento" /></td>
				<td class="labelCell4colP" style="vertical-align: top;"><asp:Label ID="lblEstado" runat="server" Text="Estado" /></td>
			</tr>
			<tr>
				<td class="fieldCell4colP" style="vertical-align: top;"><asp:Label ID="cedulatitular" runat="server" /></td>
				<td class="fieldCell4colP" style="vertical-align: top;"><asp:Label ID="sexo" runat="server" /></td>
				<td class="fieldCell4colP" style="vertical-align: top;"><asp:Label ID="fechanacimiento" runat="server" /></td>
				<td class="fieldCell4colP" style="vertical-align: top;"><asp:Label ID="estado" runat="server" /></td>
			</tr>
		</table>
	</asp:Panel>
	<br />
	<asp:Panel runat="server" GroupingText="DATOS DEL PACIENTE">
		<table width="100%" cellpadding="0" cellspacing="0" border="0">
			<tr>
				<td class="labelCell4colP" style="vertical-align: top;"><asp:Label ID="lblPacienteAsegurado" runat="server" Text="Asegurado" /></td>
				<td class="fieldCell4colP" style="vertical-align: top;" colspan="3"><asp:Label ID="pacienteasegurado" runat="server" /></td>
			</tr>
			<tr>
				<td class="labelCell4colP" style="vertical-align: top;"><asp:Label ID="lblCedulaAsegurado" runat="server" Text="Documento de Identidad del Asegurado" /></td>
				<td class="labelCell4colP" style="vertical-align: top;"><asp:Label ID="lblSexo2" runat="server" Text="Sexo" /></td>
				<td class="labelCell4colP" style="vertical-align: top;"><asp:Label ID="lblFechaNac" runat="server" Text="Fecha Nac." /></td>
				<td class="labelCell4colP" style="vertical-align: top;"><asp:Label ID="lblEstado2" runat="server" Text="Estado" /></td>
				<td class="labelCell4colP" style="vertical-align: top;"><asp:Label ID="lblParentesco" runat="server" Text="Parentesco" /></td>
			</tr>
			<tr>
				<td class="fieldCell4colP" style="vertical-align: top;"><asp:Label ID="cedulaasegurado" runat="server" /></td>
				<td class="fieldCell4colP" style="vertical-align: top;"><asp:Label ID="sexopaciente" runat="server" /></td>
				<td class="fieldCell4colP" style="vertical-align: top;"><asp:Label ID="fechanacpaciente" runat="server" /></td>
				<td class="fieldCell4colP" style="vertical-align: top;"><asp:Label ID="estadopaciente" runat="server" /></td>
				<td class="fieldCell4colP" style="vertical-align: top;"><asp:Label ID="parentesco" runat="server" /></td>
			</tr>
		</table>
	</asp:Panel>
	<br />
	<asp:Panel runat="server" GroupingText="DATOS DE LA P&Oacute;LIZA">
		<table width="100%" cellpadding="0" cellspacing="0" border="0">
			<tr>
				<td class="labelCell4colP" style="vertical-align: top;"><asp:Label ID="lblContratante" runat="server" Text="Contratante" /></td>
				<td class="fieldCell4colP" style="vertical-align: top;" colspan="3"><asp:Label ID="contratantepoliza" runat="server" /></td>
			</tr>
			<tr>
				<td class="labelCell4colP" style="vertical-align: top;"><asp:Label ID="lblPoliiza" runat="server" Text="P&oacute;liza" /></td>
				<td class="labelCell4colP" style="vertical-align: top;"><asp:Label ID="lblCertificado" runat="server" Text="Certificado" /></td>
				<td class="labelCell4colP" style="vertical-align: top;"><asp:Label ID="lblFechaDesde" runat="server" Text="Fecha Desde" /></td>
				<td class="labelCell4colP" style="vertical-align: top;"><asp:Label ID="lblFechaHasta" runat="server" Text="Fecha Hasta" /></td>
			</tr>
			<tr>
				<td class="fieldCell4colP" style="vertical-align: top;"><asp:Label ID="poliza" runat="server" /></td>
				<td class="fieldCell4colP" style="vertical-align: top;"><asp:Label ID="certificado" runat="server" /></td>
				<td class="fieldCell4colP" style="vertical-align: top;"><asp:Label ID="fechadesde" runat="server" /></td>
				<td class="fieldCell4colP" style="vertical-align: top;"><asp:Label ID="fechahasta" runat="server" /></td>
			</tr>
		</table>
	</asp:Panel>
	<br />
	<asp:Panel runat="server" GroupingText="DATOS DE LA SOLICITUD">
		<table width="100%" cellpadding="0" cellspacing="0" border="0">
			<tr>
				<td class="labelCell4colP" style="vertical-align: top;"><asp:Label ID="lblContratante2" runat="server" Text="Contratante" /></td>
				<td class="fieldCell4colP" style="vertical-align: top;" colspan="3"><asp:Label ID="contratantesolicitud" runat="server" /></td>
			</tr>
			<tr>
				<td class="labelCell4colP" style="vertical-align: top;"><asp:Label ID="lblProveedor" runat="server" Text="Proveedor" /></td>
				<td class="fieldCell4colP" style="vertical-align: top;" colspan="3"><asp:Label ID="proveedor" runat="server" /></td>
			</tr>
			<tr>
				<td class="labelCell4colP" style="vertical-align: top;"><asp:Label ID="lblDiagnostico" runat="server" Text="Diagn&oacute;stico" /></td>
				<td class="fieldCell4colP" style="vertical-align: top;" colspan="3"><asp:Label ID="diagnostico" runat="server" /></td>
			</tr>
			<tr>
				<td class="labelCell4colP" style="vertical-align: top;"><asp:Label ID="lblCodClave" runat="server" Text="Cod. Clave" /></td>
				<td class="labelCell4colP" style="vertical-align: top;"><asp:Label ID="lblFechaOcurrencia" runat="server" Text="Fecha de Ocurrencia" /></td>
				<td class="labelCell4colP" style="vertical-align: top;"><asp:Label ID="lblFechaNotificacion" runat="server" Text="Fecha de Notificaci&oacute;n" /></td>
				<td class="labelCell4colP" style="vertical-align: top;"><asp:Label ID="lblFechaLiquidadoEsperaPago" runat="server" Text="Fecha Liquidado Espera Pago" /></td>
			</tr>
			<tr>
				<td class="fieldCell4colP" style="vertical-align: top;"><asp:Label ID="codclave" runat="server" /></td>
				<td class="fieldCell4colP" style="vertical-align: top;"><asp:Label ID="fechaocurrencia" runat="server" /></td>
				<td class="fieldCell4colP" style="vertical-align: top;"><asp:Label ID="fechanotificacion" runat="server" /></td>
				<td class="fieldCell4colP" style="vertical-align: top;"><asp:Label ID="fechaliquidadoesperapago" runat="server" /></td>
			</tr>
			<tr>
				<td class="labelCell4colP" style="vertical-align: top;"><asp:Label ID="lblFechaEmisionFactura" runat="server" Text="Fecha de Emisi&oacute;n de Factura" /></td>
				<td class="labelCell4colP" style="vertical-align: top;"><asp:Label ID="lblFechaRecepcionFactura" runat="server" Text="Fecha Recepci&oacute;n Factura" /></td>
				<td class="labelCell4colP" style="vertical-align: top;"><asp:Label ID="lblStatus" runat="server" Text="Estatus" /></td>
				<td class="labelCell4colP" style="vertical-align: top;"><asp:Label ID="lblSubCategoria" runat="server" Text="SubCategoria" /></td>
			</tr>
			<tr>
				<td class="fieldCell4colP" style="vertical-align: top;"><asp:Label ID="fechaemisionfactura" runat="server" /></td>
				<td class="fieldCell4colP" style="vertical-align: top;"><asp:Label ID="fecharecepcionfactura" runat="server" /></td>
				<td class="fieldCell4colP" style="vertical-align: top;"><asp:Label ID="status" runat="server" /></td>
				<td class="fieldCell4colP" style="vertical-align: top;"><asp:Label ID="subcategoria" runat="server" /></td>
			</tr>
			<tr>
				<td class="labelCell4colP" style="vertical-align: top;"><asp:Label ID="lblNumeroControl" runat="server" Text="Nro. de Control" /></td>
				<td class="labelCell4colP" style="vertical-align: top;"><asp:Label ID="lblNumeroFactura" runat="server" Text="Nro. de Factura" /></td>
				<td class="labelCell4colP" style="vertical-align: top;"><asp:Label ID="lblNumeroPoliza" runat="server" Text="Nro. de P&oacute;liza" /></td>
				<td class="labelCell4colP" style="vertical-align: top;"><asp:Label ID="lblCertificado2" runat="server" Text="Certificado" /></td>
			</tr>
			<tr>
				<td class="fieldCell4colP" style="vertical-align: top;"><asp:Label ID="numerocontrol" runat="server" /></td>
				<td class="fieldCell4colP" style="vertical-align: top;"><asp:Label ID="numerofactura" runat="server" /></td>
				<td class="fieldCell4colP" style="vertical-align: top;"><asp:Label ID="numeropoliza" runat="server" /></td>
				<td class="fieldCell4colP" style="vertical-align: top;"><asp:Label ID="certificadosolicitud" runat="server" /></td>
			</tr>
			<tr>
				<td class="labelCell4colP" style="vertical-align: top;"><asp:Label ID="lblMontoPresupuestoIncial" runat="server" Text="Monto de Presupuesto Inicial" /></td>
				<td class="labelCell4colP" style="vertical-align: top;"><asp:Label ID="lblDeducible" runat="server" Text="Deducible" /></td>
				<td class="labelCell4colP" style="vertical-align: top;"><asp:Label ID="lblMontoCubierto" runat="server" Text="Monto Cubierto" /></td>
				<td class="labelCell4colP" style="vertical-align: top;"><asp:Label ID="lblGastosnoCubiertos" runat="server" Text="Gastos no Cubiertos" /></td>
			</tr>
			<tr>
				<td class="fieldCell4colP" style="vertical-align: top;"><asp:Label ID="montopresupuestoincial" runat="server" /></td>
				<td class="fieldCell4colP" style="vertical-align: top;"><asp:Label ID="deducible" runat="server" /></td>
				<td class="fieldCell4colP" style="vertical-align: top;"><asp:Label ID="montocubierto" runat="server" /></td>
				<td class="fieldCell4colP" style="vertical-align: top;"><asp:Label ID="gastosnocubiertos" runat="server" /></td>
			</tr>
			<tr>
				<td class="labelCell4colP" style="vertical-align: top;"><asp:Label ID="lblMontoSujetoRetencion" runat="server" Text="Monto Sujeto a Retenci&oacute;n" /></td>
				<td class="labelCell4colP" style="vertical-align: top;"><asp:Label ID="lblGastosClinicos" runat="server" Text="Gastos Cl&iacute;nicos" /></td>
				<td class="labelCell4colP" style="vertical-align: top;"><asp:Label ID="lblGastosMedicos" runat="server" Text="Gastos M&eacute;dicos" /></td>
				<td class="labelCell4colP" style="vertical-align: top;"><asp:Label ID="lbldeRetencion" runat="server" Text="% de Retenci&oacute;" /></td>
			</tr>
			<tr>
				<td class="fieldCell4colP" style="vertical-align: top;"><asp:Label ID="montosujetoretencion" runat="server" /></td>
				<td class="fieldCell4colP" style="vertical-align: top;"><asp:Label ID="gastosclinicos" runat="server" /></td>
				<td class="fieldCell4colP" style="vertical-align: top;"><asp:Label ID="gastosmedicos" runat="server" /></td>
				<td class="fieldCell4colP" style="vertical-align: top;"><asp:Label ID="porcentajeretencion" runat="server" /></td>
			</tr>
			<tr>
				<td class="labelCell4colP" style="vertical-align: top;"><asp:Label ID="lblRetencion" runat="server" Text="Retenci&oacute;n" /></td>
				<td class="labelCell4colP" style="vertical-align: top;"><asp:Label ID="lblParentesco2" runat="server" Text="Parentesco" /></td>
				<td class="labelCell4colP" style="vertical-align: top;"><asp:Label ID="lblLiquidador" runat="server" Text="Liquidador" /></td>
				<td class="labelCell4colP" style="vertical-align: top;">&nbsp;</td>
			</tr>
			<tr>
				<td class="fieldCell4colP" style="vertical-align: top;"><asp:Label ID="retencion" runat="server" /></td>
				<td class="fieldCell4colP" style="vertical-align: top;"><asp:Label ID="parentescosolicitud" runat="server" /></td>
				<td class="fieldCell4colP" style="vertical-align: top;"><asp:Label ID="liquidador" runat="server" /></td>
				<td class="fieldCell4colP" style="vertical-align: top;">&nbsp;</td>
			</tr>
		</table>
	</asp:Panel>
	<br />
	<table width="100%" cellpadding="0" cellspacing="0" border="0">
		<tr>
			<td align="right">
				<asp:Button ID="ButtonImprimir" runat="server" Text="Imprimir" OnClick="ButtonImprimir_Click" />
			</td>
		</tr>
	</table>
</asp:Content>
