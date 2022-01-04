function pageLoad() {
	var filter = $find(nombreRadFilter);
	var menu = filter.get_contextMenu();
	menu.add_showing(FilterMenuShowing);
}
function FilterMenuShowing(sender, args) {
	var filter = $find(nombreRadFilter);
	var currentExpandedItem = sender.get_attributes()._data.ItemHierarchyIndex;
	var fieldName = filter._expressionItems[currentExpandedItem];
	var allFields = filter._dataFields;
	for (var i = 0, j = allFields.length; i < j; i++) {
		if ((allFields[i].DataType == "System.String") && (allFields[i].FieldName == fieldName)) {
			sender.findItemByValue("StartsWith").set_visible(false);
			sender.findItemByValue("EndsWith").set_visible(false);
			sender.findItemByValue("GreaterThan").set_visible(false);
			sender.findItemByValue("GreaterThanOrEqualTo").set_visible(false);
			sender.findItemByValue("LessThan").set_visible(false);
			sender.findItemByValue("LessThanOrEqualTo").set_visible(false);
			sender.findItemByValue("Contains").set_visible(false);
			sender.findItemByValue("DoesNotContain").set_visible(false);
			sender.findItemByValue("Between").set_visible(false);
			sender.findItemByValue("NotBetween").set_visible(false);
		}
		if ((allFields[i].DataType == "System.Boolean") && (allFields[i].FieldName == fieldName)) {
			sender.findItemByValue("StartsWith").set_visible(false);
			sender.findItemByValue("EndsWith").set_visible(false);
			sender.findItemByValue("GreaterThan").set_visible(false);
			sender.findItemByValue("GreaterThanOrEqualTo").set_visible(false);
			sender.findItemByValue("LessThan").set_visible(false);
			sender.findItemByValue("LessThanOrEqualTo").set_visible(false);
			sender.findItemByValue("Contains").set_visible(false);
			sender.findItemByValue("DoesNotContain").set_visible(false);
			sender.findItemByValue("Between").set_visible(false);
			sender.findItemByValue("NotBetween").set_visible(false);
			sender.findItemByValue("IsNull").set_visible(false);
			sender.findItemByValue("NotIsNull").set_visible(false);
		}
	}
}