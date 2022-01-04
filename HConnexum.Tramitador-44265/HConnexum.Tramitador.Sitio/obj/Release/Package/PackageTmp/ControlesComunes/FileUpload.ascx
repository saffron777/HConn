<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FileUpload.ascx.cs" Inherits="HConnexum.Tramitador.Sitio.FileUpload" %>
<telerik:RadProgressManager ID="fileUploadRadProgressManager" runat="server" meta:resourcekey="fileUploadRadProgressManagerResource1" />
<telerik:RadAsyncUpload ID="fileUploadRadAsyncUpload" runat="server" 
	MultipleFileSelection="Automatic" 
	OnFileUploaded="fileUploadRadAsyncUpload_FileUploaded" 
	meta:resourcekey="fileUploadRadAsyncUploadResource1" >
	<Localization Cancel="Cancelar" Remove="Remover" Select="Seleccionar" />
</telerik:RadAsyncUpload>
<asp:Panel ID="PanValidator" runat="server" meta:resourcekey="PanValidatorResource1" />
<telerik:RadProgressArea ID="fileUploadRadProgressArea" runat="server"  Language="" meta:resourcekey="fileUploadRadProgressAreaResource1" />
