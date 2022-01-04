function validatePicker(sender, args) {
	var picker = $find(Page_Validators[jQuery.inArray(sender, Page_Validators)].controltovalidate);
	if (!picker.get_selectedDate()) {
		picker.get_dateInput()._invalid = true;
		picker.get_dateInput().updateCssClass();
		args.IsValid = false;
		picker._dateInput._textBoxElement.value = Page_Validators[jQuery.inArray(sender, Page_Validators)].errormessage;
	}
	else {
		picker.get_dateInput()._invalid = false;
		picker.get_dateInput().updateCssClass();
		args.IsValid = true;
	}
}