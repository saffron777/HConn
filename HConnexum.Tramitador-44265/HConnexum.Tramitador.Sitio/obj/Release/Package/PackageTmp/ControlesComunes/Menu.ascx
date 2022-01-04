<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Menu.ascx.cs" Inherits="HConnexum.Tramitador.Sitio.ControlesComunes.Menu" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<telerik:RadMenu Flow="Vertical" ID="menuWeb" runat="server"  ClickToOpen="false"
	OnDataBound="menuWeb_DataBound" Style="top: 0px; left: 0px" />
