<%@ Control language="vb" AutoEventWireup="false" Explicit="True" Inherits="DotNetNuke.UI.Skins.Skin" %>
<%@ Register TagPrefix="dnn" TagName="LOGO" Src="~/Admin/Skins/Logo.ascx" %>
<%@ Register TagPrefix="dnn" TagName="NAV" Src="~/Admin/Skins/Nav.ascx" %>
<%@ Register TagPrefix="dnn" TagName="COPYRIGHT" Src="~/Admin/Skins/Copyright.ascx" %>

        <div id="wrapper">
            <div id="header">
                <div id="logo">
                    <dnn:LOGO runat="server" id="Object1" />
                </div>
            </div>
            <div id="menu" class="menuBar centrar">
            <div style="padding-left:10px; height: 28px;">
                <dnn:NAV runat="server" id="dnnNAV"  ProviderName="DDRMenuNavigationProvider" IndicateChildren="false" ControlOrientation="Horizontal" CSSControl="mainMenu" />
             </div>
            </div>
            <div id="content">
                <div id="BannerPane" runat="server"></div>
                <div id="TopPane" runat="server"></div>
                <div id="LeftPane" runat="server"></div>
                <div id="RightPane" runat="server"></div>
                <div id="ContentPane" runat="server"></div>
                <div id="BottomPane" runat="server"></div>
            </div>
            <div id="Social">
                <div class="bgSocialPane">
                    <table style="width: 430px;" align="center">
                        <tr>
                            <td style="width: 83px;"><a href="https://www.facebook.com/hconnexum" target="_blank"><img alt="" src="/portal/portals/_default/skins/hconnexum/images/imgFacebookHC.png" border="0" /></a></td>
                            <td style="width: 15px;"></td>
                            <td style="width: 83px;"><a href="https://twitter.com/hconnexum" target="_blank"><img alt="" src="/portal/portals/_default/skins/hconnexum/images/imgTwitterHC.png" border="0" /></a></td>
                            <td style="width: 15px;"></td>
                            <td style="width: 83px;"><a href="http://www.youtube.com/user/hconnexum" target="_blank"><img alt="" src="/portal/portals/_default/skins/hconnexum/Images/imgYouTube.png" border="0" /></a></td>
                        </tr>
                    </table>
                </div>

				<div id="footer" class="footer_style" runat="server">
					<table>
						<tr>
							<td style="padding-left: 10px;">
								<dnn:COPYRIGHT runat="server" id="dnnCOPYRIGHT"  CssClass="links" />
							</td>
							<td style="text-align: right;">
								<img alt="" src="/portal/portals/_default/skins/hconnexum/images/imgPoweredByNubise.png" />
							</td>
						</tr>
					</table>
				</div>
			</div>
        </div>
    
