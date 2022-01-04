<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="banners.ascx.cs" Inherits="Banners.banner" %>

<!--
<script src="DesktopModules/HTML/Banners/Scripts/jquery-1.7.2.js" type="text/javascript"></script>
<script src="DesktopModules/HTML/Banners/Scripts/jquery-1.7.2.min.js" type="text/javascript"></script>
-->
<script src="DesktopModules/HTML/Banners/Scripts/jflow.plus.js" type="text/javascript"></script>
<link href="DesktopModules/HTML/Banners/Estilos/jflow.style.css" rel="stylesheet" type="text/css" />

<script type="text/javascript">
    $(document).ready(function () {
        $("#myController").jFlow({
            controller: ".jFlowControl",        // ..:: [ must be class, use . sign ] ::..
            slideWrapper: "#jFlowSlider",       // ..:: [ must be id, use # sign ] ::..
            slides: "#mySlides",                // ..:: [ the div where all your sliding divs are nested in ] ::..
            selectedWrapper: "jFlowSelected",   // ..:: [ just pure text, no sign ] ::..
            effect: "flow",                     // ..:: [ this is the slide effect (rewind or flow) ] ::..
            width: "991px",                     // ..:: [ this is the width for the content-slider ] ::..
            height: "280px",                    // ..:: [ this is the height for the content-slider ] ::..
            duration: 400,                      // ..:: [ time in milliseconds to transition one slide ] ::..
            pause: 4000,                        // ..:: [ time between transitions ] ::..
            prev: ".jFlowPrev",                 // ..:: [ must be class, use . sign ] ::..
            next: ".jFlowNext",                 // ..:: [ must be class, use . sign ] ::..
            auto: true
        });
    });
</script>

<div style="border: 1px solid transparent; margin-left: -1px;">
    <table cellpadding="0" cellspacing="0" border="0">
        <tr>
            <td style="text-align: center">
                <div id="sliderContainer">
                    <div id="mySlides">
                        <div id="slide1" class="slide">
                            <asp:Image ID="imgBanner_1" runat="server" AlternateText="" ImageUrl="Imagenes/imgBanner_1.png" Width="991px" heigth="280px" />
                            <div class="slideContent"></div>
                        </div>
                        <div id="slide2" class="slide">
                            <asp:Image ID="imgBanner_2" runat="server" AlternateText="" ImageUrl="Imagenes/imgBanner_2.png" Width="991px"  heigth="280px"/>
                            <div class="slideContent"></div>
                        </div>
                        <div id="slide3" class="slide">
                            <asp:Image ID="imgBanner_3" runat="server" AlternateText="" ImageUrl="Imagenes/imgBanner_3.png" Width="991px"  heigth="280px"/>
                            <div class="slideContent"></div>
                        </div>
                    </div>
                    <div id="myController">
                        <!-- ..:: [ COLOCAR UN span PARA CADA IMAGEN ] ::.. -->
                        <%--slide1--%><span class="jFlowControl"></span>
                        <%--slide2--%><span class="jFlowControl"></span>
                        <%--slide3--%><span class="jFlowControl"></span>
                    </div>
                    <div class="jFlowPrev"></div>
                    <div class="jFlowNext"></div>
                </div>
            </td>
        </tr>
    </table>
</div>


