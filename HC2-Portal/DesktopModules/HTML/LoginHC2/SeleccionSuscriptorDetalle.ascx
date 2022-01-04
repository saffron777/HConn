<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SeleccionSuscriptorDetalle.ascx.cs"
    Inherits="SeleccionSuscriptorDetalle.SeleccionSuscriptorDetalle" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<style type="text/css">
    .ancho1
    {width:300px; text-align:right; }
    .ancho2
     {width:260px}
    .ancho3
     {width:70px}
    .ancho4
    {width:15px }
    .redondo 
    { border-radius:3px; 
    -moz-border-radius:10px;
    -webkit-border-radius:10px; 
    } 
</style>
<script type="text/javascript">


    function confirmCallBackFn(args) {
        window.parent.location = "MisAplicaciones.aspx";
     
    }
        Telerik.Web.UI.RadWindowUtils.Localization =    
{   
    "OK" : "Aceptar",       
};
</script>
<div style=" padding: 1px 0px 0px 0px; background: url(DesktopModules/HTML/HC.GadgetAplicacion/Imagenes/bgAplicaciones.png) no-repeat; background-position: bottom;">
   <form id="form1" style=" top: 0px; left: 0px;">
      <table>
        <tr>	
          <div id="Div6" style=" text-align: center;">	
		    <td align="center" colspan="3">
			    
		    </td>
          </div>
	    </tr>
        <tr>	
         <div id="Div5" style=" text-align: center;">	
		    <td align="center" colspan="4">
			    <div style="width: 344px; height: 30px; padding: 5px; padding-top: 15px; text-align: center; font-size: 14px; color: #6d6d6d; font-weight: bold">
				    <asp:Label ID="Label1" runat="server" Text="Suscriptores" meta:resourcekey="lblSuscriptorResource1">
                    </asp:Label>
                </div>
		    </td>
         </div>
	    </tr>
      </table>
      <asp:Panel ID="Panel1" runat="server"  >
       <table>
	    <tr align="center">	
          <div id="Suscriptor" style=" text-align: center;">
		    <td class="ancho1">		
		         <asp:Label ID="LbSuscriptor" runat="server" Text = "Sucriptor:"></asp:Label>
            </td>	
		    <td class="ancho3" align="center">
                <telerik:RadComboBox ID="dropSuscriptor"
                DataValueField="IdUsuarioSuscriptor" DataTextField="Nombre"  runat="server" 
                EmptyMessage="Seleccione un suscriptor" ErrorMessage="Campo obligatorio"
                onitemsrequested="DdlUsuarioSuscriptorItemsRequested" EnableLoadOnDemand="True"
                width="320px" AutoPostBack="true" 
                OnSelectedIndexChanged="DdlUsuarioSuscriptor_SelectedIndexChanged"
                meta:resourcekey="ddlUsuarioSuscriptorResource1"  />  
		    </td>	
		    <td class="ancho4">	
		    </td>	
            <td class="ancho2" align ="left">
             <asp:Button runat="server" Text="ACEPTAR" ForeColor="White"
              AlternateText="Iniciar Sesión" OnClick="BtnAceptar_Click" ValidationGroup="SuscriptoresVal"
              BackColor="#152471" BorderColor="#152471" BorderStyle="Solid" ID="cmdAceptar" Font-Names="Arial"
              Font-Size="8pt" Width="100px" CssClass="redondo" />
            </td>
          </div>
	    </tr>
       </table>
      </asp:Panel>
      <asp:Panel ID="PSucursalP" runat="server" Visible="false" >
       <table>
        <tr align="center">	
         <div id="Sucursal" style=" text-align: center;">
		    <td class="ancho1">			
                <asp:Label ID="LSucursal" runat="server" Text = "Sucursal:"></asp:Label>			
		    </td>	
		    <td class="ancho3" align="center">
			      <telerik:RadComboBox ID="ddlSucursal" 
                    DataValueField="Id" DataTextField="Nombre" runat="server" 
                    EmptyMessage="Seleccione una sucursal" ErrorMessage="Campo obligatorio"
                    onitemsrequested="DdlSucursalItemsRequested" EnableLoadOnDemand="True"
					width="320px" AutoPostBack="true" /> 
		    </td>	
		    <td class="ancho4">			
	        </td>
	        <td class="ancho2">			
	        </td>
         </div>	
	    </tr>
       </table>
      </asp:Panel>
      <asp:Panel ID="Psimulacion" runat="server" Visible="false" >
       <table >
        <tr align="center">
         <div id="Div1" style=" text-align: center;">
          <td colspan="4" >
            <div style="width: 344px; height: 30px; padding: 5px; padding-top: 15px; text-align: center; font-size: 14px; color: #6d6d6d; font-weight: bold">
				    <asp:Label ID="Label2" runat="server" Text="Simulación" meta:resourcekey="lblSuscriptorResource1">
                    </asp:Label>
            </div>
          </td>   
         </div>
        </tr>
        <tr align="center">
         <div id="Div2" style=" text-align: center;">
          <td class="ancho1" >
              <asp:Label ID="LbSuscriptorS" runat="server" Text="Suscriptor:"></asp:Label>
          </td>   
          <td class="ancho3" align="center">
             <telerik:RadComboBox  ID="ddlSuscriptorIdx" runat="server" Width="320" Height="150"
            EmptyMessage="Seleccione un Suscriptor" ShowMoreResultsBox="true"
            EnableVirtualScrolling="true" AutoPostBack="true" DataTextField = "Nombre" DataValueField="Id"
            onitemsrequested="DdlSuscriptorIdx_ItemsRequested" EnableLoadOnDemand="True"
            OnSelectedIndexChanged="DdlSuscriptorIdx_SelectedIndexChanged"  />  
          </td>
          <td class="ancho4">
          </td>    
          <td class="ancho2">
          </td>
         </div>
        </tr>
       </table>
      </asp:Panel>
      <asp:Panel ID="PUsuarioS" runat="server" Visible="false" >
        <table>
            <tr align="center">
                <div id="Div3" style=" text-align: center;">         
                  <td class="ancho1">
                    <asp:Label ID="LbUsusario" runat="server" Text="Usuario:"></asp:Label>
                  </td>   
                  <td class="ancho3" align="center">
                    <telerik:RadComboBox ID="ddlUsuario" DataValueField="IdUsuarioSuscriptor" ShowMoreResultsBox="true"
                    DataTextField="Nombre" runat="server" EmptyMessage="Seleccione" AutoPostBack="true" EnableVirtualScrolling="true"
                    onitemsrequested="ddlUsuarios_ItemsRequested" Width="320px" EnableLoadOnDemand="True" 
                    OnSelectedIndexChanged="Ddlusuario_SelectedIndexChanged"/>
                  </td>
                  <td class="ancho4">
                  </td>
                  <td class="ancho2" align ="left">
                     <asp:Panel ID="BSimular" runat="server" Visible="false" >
                        <asp:Button runat="server" Text="SIMULAR" ForeColor="White"
                        AlternateText="Iniciar Sesión" OnClick="CmdSimular_Click" 
                        BackColor="#152471" BorderColor="#152471" BorderStyle="Solid" ID="cmdSimular" Font-Names="Arial"
                        Font-Size="8pt" Width="100px" CssClass="redondo" />
                     </asp:Panel>
                  </td>
                </div> 
            </tr>
        </table>
      </asp:Panel> 
      <asp:Panel ID="PSuscursalS" runat="server" Visible="false">
        <table>
         <tr align="center">
            <div id="Div4" style=" text-align: center;">
                <td class="ancho1">
                <asp:Label ID="LbSucursalS" runat="server" Text="Sucursal:"></asp:Label>
                </td>   
                <td class="ancho3" align="center">
                <telerik:RadComboBox ID="ddlSucursalS" 
                    DataValueField="Id" DataTextField="Nombre" runat="server" 
                    EmptyMessage="Seleccione una sucursal" width="320px" AutoPostBack="true"/>
                </td>
                <td class="ancho4"></td>
                <td class="ancho2"></td>   
            </div> 
         </tr>
        </table>  
      </asp:Panel> 
<br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
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
					    <telerik:AjaxUpdatedControl ControlID="PSucursalP" UpdatePanelRenderMode="Inline" />                        
				    </UpdatedControls>
			    </telerik:AjaxSetting> 
                <telerik:AjaxSetting AjaxControlID="ddlUsuarioSuscriptor">
				    <UpdatedControls>
					    <telerik:AjaxUpdatedControl ControlID="Psimulacion" UpdatePanelRenderMode="Inline" />                        
				    </UpdatedControls>
			    </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="ddlSuscriptorIdx">
				    <UpdatedControls>
					    <telerik:AjaxUpdatedControl ControlID="PUsuarioS" UpdatePanelRenderMode="Inline" />                        
				    </UpdatedControls>
			    </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="ddlUsuario">
				    <UpdatedControls>
					    <telerik:AjaxUpdatedControl ControlID="BSimular" UpdatePanelRenderMode="Inline" />                        
				    </UpdatedControls>
			    </telerik:AjaxSetting>
				<telerik:AjaxSetting AjaxControlID="ddlUsuario">
				    <UpdatedControls>
					    <telerik:AjaxUpdatedControl ControlID="PSuscursalS" UpdatePanelRenderMode="Inline" />                        
				    </UpdatedControls>
			    </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="ddlSuscriptorIdx">
				    <UpdatedControls>
					    <telerik:AjaxUpdatedControl ControlID="PSuscursalS" UpdatePanelRenderMode="Inline" />                        
				    </UpdatedControls>
			    </telerik:AjaxSetting>
            </AjaxSettings>       											
        </telerik:RadAjaxManager>
   </form>
</div>
