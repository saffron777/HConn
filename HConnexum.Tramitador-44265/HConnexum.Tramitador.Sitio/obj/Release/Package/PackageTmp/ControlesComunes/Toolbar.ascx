<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Toolbar.ascx.cs" Inherits="HConnexum.Tramitador.Sitio.ControlesComunes.Toolbar" %>
<%@ Register Src="~/ControlesComunes/Menu.ascx" TagName="Menu" TagPrefix="hcc" %>
<telerik:RadToolBar ID="rtbMenu" runat="server" Width="100%" Style=" position: absolute; z-index: 200000; top: 0; left: 0;" OnClientDropDownOpened="DropDownOpened" OnClientButtonClicking="MenuBarItemClicked">
	<Items>
		<telerik:RadToolBarButton>
			<ItemTemplate>
				<hcc:Menu ID="mnPrincipal" runat="server" ItemClicking="OnClientItemClickedHandler" />
			</ItemTemplate>
		</telerik:RadToolBarButton>
		<telerik:RadToolBarDropDown Text="0 Ventanas" ExpandDirection="Down"/>
		<telerik:RadToolBarButton>
			<ItemTemplate>
				<telerik:RadSkinManager runat="server" ID="rsmGeneral" ShowChooser="false"></telerik:RadSkinManager>
			</ItemTemplate>
		</telerik:RadToolBarButton>
		<telerik:RadToolBarButton>
			<ItemTemplate>
                <div id="divSuscriptor">
				    <telerik:RadComboBox runat="server" ID="ddlRadCombo" AutoPostBack="true" OnSelectedIndexChanged="ddlRadCombo_SelectedIndexChanged" OnClientSelectedIndexChanged="closeAllRadWindow" />
                </div>
			</ItemTemplate>
		</telerik:RadToolBarButton>
 	    <telerik:RadToolBarButton>
			<ItemTemplate>
                <table>
					<tr>
						<td id="tdlblSuscriptor"><asp:Label runat="server" ID="lblSuscriptor"/><asp:Label runat="server" ID="lblSucursal"/></td>
						<td id="tdlblusuario"><asp:Label runat="server" ID="lblusuario" Font-Bold="true" /></td>
					</tr>
				</table>
			</ItemTemplate>
		</telerik:RadToolBarButton>
 	</Items>
 
</telerik:RadToolBar>
<telerik:RadScriptBlock runat="server" ID="RadScriptBlock1">
	<script type="text/javascript">
		function MenuItem() { }

		MenuItem.prototype = {
			id: "",
			title: ""
		};

		var queryArr = [];
		window.onload = window.onresize = function posicionPrincipal() {
		    $('#RestrictionZone').height($(window).height() - $('#<%=rtbMenu.ClientID %>').height())
		    $('#MinimizeZone').height($(window).height() - $('#<%=rtbMenu.ClientID %>').height())
		    $('#divSuscriptor').css('width', $(window).width() - ($("#tdlblSuscriptor").width() + $("#tdlblusuario").width() + 190));
		};

		function MenuBarItemClicked(sender, args) {
			switch (args.get_item().get_commandName()) {
				case "CerrarSesion":
					break;
				default:
					ActivateWindowPopup(sender, args);
					args.set_cancel(true);
					break;
			}
		}

		function DropDownOpened(sender, args) {
			var dropDown = args.get_item();
			var templength = 0;

			for (var i = 0; i < dropDown.get_buttons().get_count(); i++) {
				var dropDownItemWidth = $.textMetrics(dropDown.get_buttons().getButton(i)._element.innerHTML).width;
				if (dropDownItemWidth > templength)
					templength = dropDownItemWidth;
			}

			if (templength > 0) {
				args.get_item()._animationContainer.style.width = (templength + 80).toString() + "px"

				for (var j = 0; j < args.get_item()._animationContainer.childNodes.length; j++)
					args.get_item()._animationContainer.childNodes[j].style.width = (templength + 80).toString() + "px";
			}
		}

		function WindowActivate(sender, eventArgs) {
			var Windows = GetRadWindowManager().GetWindows();
			$.each(Windows, function () {
				if (this._popupElement.id != sender._popupElement.id) {
					$('#' + this._popupElement.id).css('z-index', '3000');
				}
			});
			$('#' + sender._popupElement.id).css('z-index', '100000');
		}

		function ActivateWindowPopup(sender, args) {
		    $.each(GetRadWindowManager().GetWindows(), function () {
		        if (this._name == args.get_item()._itemData) {
		            GetRadWindowManager().GetWindowByName(this._name).SetActive(true);
		        }
		    })
		}

		function ActivateWindowPopupFromMenu(tituloVentana) {
		    $.each(GetRadWindowManager().GetWindows(), function () {
		        if (tituloVentana == this._title) {
		            GetRadWindowManager().GetWindowByName(this._name).SetActive(true);
		        }
		    })
		}

		function AddNewItem(Titulo, window) {
		    var toolBar = $find("<%=rtbMenu.ClientID %>");
		    toolBar.trackChanges();
		    var dropDown;
		    if (toolBar._findItemByText('0 Ventanas') == null)
		        dropDown = toolBar.get_items().getItem(1);
		    else
		        dropDown = toolBar._findItemByText(queryArr.length.toString() + " Ventanas");
		    var dropDownButton = new Telerik.Web.UI.RadToolBarButton();
		    dropDownButton.set_text(Titulo);
		    dropDownButton._itemData = window._name;
		    dropDown.get_buttons().add(dropDownButton);
		    dropDown.set_text((queryArr.length + 1).toString() + " Ventanas");

		    dropDown.get_dropDownElement().parentNode.style.width = (Titulo.length * 10).toString();
		    dropDown.get_dropDownElement().style.width = (Titulo.length * 10).toString();

		    toolBar.commitChanges();
		}

		function OnClientItemClickedHandler(sender, eventArgs) {
		    if ((eventArgs._item.get_value()) != "" && (eventArgs._item.get_value() != null)) {
		        var Bandera = true;
		        jQuery.each(queryArr, function () {
		            if (this.title == eventArgs._item.get_text()) {
		                Bandera = false;
		            }
		        });
		        if (Bandera) {
		            var own = window.radopen(eventArgs._item.get_value(), null);
		            AddNewItem(eventArgs._item.get_text(), own)
		            own.set_minimizeZoneID('MinimizeZone');
		            var _menuItem = new MenuItem();
		            _menuItem.title = eventArgs._item.get_text();
		            _menuItem.id = eventArgs._item.get_value().split('?')[1].split('&')[0].split('=')[1];
		            queryArr.push(_menuItem);
		            if (queryArr.length > 1) {
		                own.moveTo(own._restoreRect.x + (5 * queryArr.length), own._restoreRect.y + (10 * queryArr.length));
		            }
		            own.add_close(closeRadWindow);
		        }
		        else {
		            ActivateWindowPopupFromMenu(eventArgs._item._text);
                    args.set_cancel(true);
		        }
		    }
		}

		function closeRadWindow(oWnd, EventArgs) {
		    var ajaxArgs;
		    var toolBar = $find("<%=rtbMenu.ClientID %>");
		    toolBar.trackChanges();
		    var dropDown = toolBar._findItemByText((queryArr.length).toString() + " Ventanas");
		    for (var i = 0; i < dropDown.get_buttons()._array.length; i++) {
		        var button = dropDown.get_buttons().getButton(i);
		        if (button._itemData == oWnd._name) {
		            var removeItem = button._text
		            queryArr = jQuery.grep(queryArr, function (value) {
		                if (value.title != removeItem)
		                    return true;
		                else {
		                    ajaxArgs = value.id;
		                    return false;
		                }
		            });
		            dropDown.get_buttons().removeAt(i);
		        }
		    }
		    dropDown.set_text((queryArr.length).toString() + " Ventanas");
		    toolBar.commitChanges();

		    $find(nombreRadAjaxManager).ajaxRequest(ajaxArgs);
		}

        function closeAllRadWindow(oWnd, EventArgs) {
            var toolBar = $find("<%=rtbMenu.ClientID %>");
            toolBar.trackChanges();
            var dropDown = toolBar._findItemByText((queryArr.length).toString() + " Ventanas");
            if (toolBar._findItemByText((queryArr.length).toString() + ' Ventanas') != null) {
                for (var i = 0; i < dropDown.get_buttons()._array.length + 1; i++) {
                    dropDown.get_buttons().removeAt(0);
                }
                dropDown.set_text(dropDown.get_buttons().get_count().toString() + " Ventanas");
                toolBar.commitChanges();
            }
        }
	</script>
</telerik:RadScriptBlock>
