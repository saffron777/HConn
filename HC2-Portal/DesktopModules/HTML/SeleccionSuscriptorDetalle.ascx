<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SeleccionSuscriptorDetalle.ascx.cs" Inherits="SeleccionSuscriptorDetalle.SeleccionSuscriptorDetalle" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register tagPrefix="telerik" namespace="Telerik.Web.UI" assembly="Telerik.Web.UI" %>
<style type="text/css">
      .ancho2
    {
        width:75px;
    }
    .ancho3
    {
        width:335px
    }
    .ancho4
    {
        width:30px
    }
    .ancho5
    {
         width:70px
    }
     .ancho5
    {
         width:70px
    }
    .table td, table th {
    padding: 0;
    width: 35%;
   }

</style>
 <div style=" padding: 1px 0px 0px 0px; background: url(DesktopModules/HTML/HC.GadgetAplicacion/Imagenes/bgAplicaciones.png) no-repeat; background-position: bottom;">
<form id="form1" style="position: absolute; top: 0px; left: 0px;">

<table border="0" cellpadding="0" cellspacing="0"  align="center"  width="50px;"  >
	<tr>		
		<td align="center" colspan="4">
			<div style="width: 344px; height: 30px; padding: 5px; padding-top: 15px; text-align: center; font-size: 14px; color: #6d6d6d; font-weight: bold">
				<asp:Label ID="Label1" runat="server" Text="Suscriptores" meta:resourcekey="lblSuscriptorResource1">
                </asp:Label>
             </div>
		</td>
	</tr>
	<tr class="table">	
      <div id="Suscriptor" style=" text-align: center;">
		<td align="right" class="ancho1">		
		    Suscriptor:
        </td>	
		<td class="ancho3" align="center">
			 <telerik:RadComboBox ID="dropSuscriptor" 
                DataValueField="IdUsuarioSuscriptor" DataTextField="Nombre" runat="server" 
                EmptyMessage="Seleccione" ErrorMessage="Campo Obligatorio" 
                meta:resourcekey="ddlUsuarioSuscriptorResource1"  
                OnSelectedIndexChanged="ddlUsuarioSuscriptor_SelectedIndexChanged"               
				width="320px" AutoPostBack="true" />
		</td>	
		<td class="ancho4">	
         <asp:Button runat="server" Text="ACEPTAR" ForeColor="White"
                        AlternateText="Iniciar Sesión" OnClick="btnAceptar_Click" ValidationGroup="SuscriptoresVal"
                        BackColor="#152471" BorderColor="#152471" BorderStyle="Solid" ID="cmdAceptar" Font-Names="Arial"
                        Font-Size="8pt" Width="100px" />		
		</td>	
		<td class="ancho5">
			
		</td>
      </div>
	</tr>
    <tr class="table">	
     <div id="Sucursal" style=" text-align: center;">
		<td align="right" class="ancho1">			
            <asp:Label ID="LSucursal" runat="server" Text = "Sucursal:" Visible="false" ></asp:Label>			
		</td>	
		<td class="ancho3" align="center">
			  <telerik:RadComboBox ID="ddlSucursal" 
                             DataValueField="CodIdExterno" DataTextField="Nombre" runat="server" 
                             EmptyMessage="Seleccione una sucursal" ErrorMessage="Campo obligatorio"
                             onitemsrequested="ddlSucursal_ItemsRequested" EnableLoadOnDemand="True"
                             OnSelectedIndexChanged="ddlSucursal_SelectedIndexChanged"               
						     width="320px" AutoPostBack="true" /> 
		</td>	
		<td class="ancho4">			
	   </td>
	   <td class"ancho5">			
	   </td>
       </div>	
	</tr>
   
    </table>   

<br />
<br />
<br />
<br />
<br />
<br />
<br />
<br />
<br />
<br />
<br />
<br />
<br />
<br />
<br />
<br />
<br />

<telerik:RadWindowManager ID="singleton" runat="server" EnableShadow="True" EnableEmbeddedSkins="False" Skin="Windows7" ></telerik:RadWindowManager>

<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" DefaultLoadingPanelID="RadAjaxLoadingPanel1" meta:resourcekey="RadAjaxManager1Resource1">
		<AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="ddlUsuarioSuscriptor">
				    <UpdatedControls>
					    <telerik:AjaxUpdatedControl ControlID="ddlSucursal" UpdatePanelRenderMode="Inline" />                        
				    </UpdatedControls>
			</telerik:AjaxSetting>   
            <telerik:AjaxSetting AjaxControlID="ddlUsuarioSuscriptor">
				    <UpdatedControls>
					    <telerik:AjaxUpdatedControl ControlID="LSucursal" UpdatePanelRenderMode="Inline" />                        
				    </UpdatedControls>
			</telerik:AjaxSetting>
        </AjaxSettings>       											
</telerik:RadAjaxManager>

</form>
 </div>


        