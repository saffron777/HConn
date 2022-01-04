<%@ Page Title="ReporteCA" Language="C#" MasterPageFile="~/Master/Site.Master" AutoEventWireup="true" CodeBehind="ReporteCA.aspx.cs" Inherits="HConnexum.Tramitador.Sitio.Modulos.Reportes.ReporteCA" %>

<asp:Content ID="cHead" ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>

<asp:Content ID="cBody" ContentPlaceHolderID="cphBody" runat="server">
    <telerik:RadScriptBlock runat="server" ID="RadScriptBlock1">
		<script type="text/javascript">
		    var nombreRadAjaxManager = '<%= RadAjaxManager1.ClientID %>';
		</script>
	</telerik:RadScriptBlock>
    <telerik:RadAjaxManager ID="RadAjaxManager1"  
        OnAjaxRequest="RadAjaxManager1_AjaxRequest" runat="server" />
</asp:Content>
