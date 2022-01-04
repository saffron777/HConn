<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Noticias.Default" %>

<%@ Register src="PublicarNoticias.ascx" tagname="PublicarNoticias" tagprefix="uc1" %>
<%@ Register src="AdmNoticias.ascx" tagname="AdmNoticias" tagprefix="uc2" %>
<%@ Register src="verNoticia.ascx" tagname="verNoticia" tagprefix="uc3" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html>
    <head runat="server">
        <title></title>
        <style type="text/css">
            .FuentePublicacion { font-family: Helvetica,Arial,sans-serif; font-size: 12px; }
            .SubTitulosPublicacion { font-weight: bold; }
            .validator { background-position: right center; color: Red; background-image: url('Imagenes/alertIcon.png'); background-repeat: no-repeat; }
        </style>
    </head>
    <body>
        <form id="form1" runat="server">
            <asp:ScriptManager runat="server" ID="ScriptManager1"></asp:ScriptManager>
            <asp:Panel ID="Panel1" runat="server">
                <div>
                    <uc2:AdmNoticias ID="AdmNoticias1" runat="server" />
                </div>
            </asp:Panel>
        </form>
    </body>
</html>
