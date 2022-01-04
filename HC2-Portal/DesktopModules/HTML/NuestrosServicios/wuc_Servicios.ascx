<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="wuc_Servicios.ascx.cs" Inherits="NuestrosServicios.wuc_Servicios" %>

<style>
    .bgHC1 {
        background: url(DesktopModules/HTML/NuestrosServicios/Imagenes/bg_hc1.jpg) repeat-x;
        height: 320px;
    }
    .bgHC2 {
		background: url(DesktopModules/HTML/NuestrosServicios/Imagenes/bg_hc2.jpg) repeat-y;
        height: 320px;
    }
</style>
<div>
    <table cellpadding="0" cellspacing="0" border="0">
        <tr>
            <td class="bgTitulo" colspan="3"><strong style="padding-left: 20px;">NUESTROS SERVICIOS</strong></td>
        </tr>
        <tr>
            <td class="bgHC1">
                <table cellpadding="0" cellspacing="0" border="0" align="center">
                    <tr>
                        <td style="width: 50%; text-align:center;"></td>
                        <td style="width: 50%; text-align:center;"></td>
                    </tr>
                    <tr>
                        <td style="width: 50%; text-align:center;"><asp:ImageButton ID="ib_farmacia" runat="server" ImageUrl="Imagenes/farmacia.png" /></td>
                        <td style="width: 50%; text-align:center;"><asp:ImageButton ID="ib_aps" runat="server" ImageUrl="Imagenes/aps.png" /></td>
                    </tr>
                </table>
            </td>
            <td style="width:2px"></td>
            <td class="bgHC2">
				<div style="padding-top: 50px;">
					<table cellpadding="0" cellspacing="0" border="0" width="100%">
						<tr>
							<td>
                                <asp:ImageButton ID="imgLnkHc2" runat="server" ImageUrl="Imagenes/img_lnk_hc2.png" />
							</td>
							<td><asp:ImageButton ID="imgLnkConsulta" runat="server" ImageUrl="Imagenes/img_lnk_consulta.png" /></td>
						</tr>
					</table>
				</div>
            </td>
        </tr>
    </table>
</div>