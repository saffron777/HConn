<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReporteCartaAvalConsultSalud.aspx.cs" Inherits="HConnexum.Tramitador.Sitio.Modulos.Reportes.ReporteCartaAvalConsultSalud" %>


<%@ Register Assembly="Telerik.ReportViewer.WebForms, Version=7.2.13.1016, Culture=neutral, PublicKeyToken=a9d7983dfcc261be" Namespace="Telerik.ReportViewer.WebForms" TagPrefix="telerik" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div  >
    
        <telerik:ReportViewer ID="ReportViewer1" runat="server" 
            ShowParametersButton="False" Height="1080px" Width="100%" 
            ZoomMode="FullPage" 
            ReportBookID="ReportBookControl1" ShowPrintPreviewButton="False">
        </telerik:ReportViewer>
        <telerik:ReportBookControl ID="ReportBookControl1" runat="server">
        </telerik:ReportBookControl>
    
    </div>
    </form>
</body>
</html>
