using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace HConnexum.Tramitador.Sitio
{
	public partial class FileUpload : System.Web.UI.UserControl
	{
		public int InputSize
		{
			get
			{
				return this.fileUploadRadAsyncUpload.InputSize;
			}
			set
			{
				this.fileUploadRadAsyncUpload.InputSize = value;
			}
		}

		private IList<UploadedFile> _UploadedFilesList = new List<UploadedFile>();

		/// <summary>
		/// Gets or Sets the Max File Length/Size to be uploaded
		/// </summary>
		public int FileMaxLength { get; set; }

		/// <summary>
		/// Gets or sets the maximum file input fields that can be added to the control
		/// </summary>
		public int MaxFilesCount { get; set; }

		/// <summary>
		/// Gets or Sets wether this control verifies if there is there is at least 1 file uploaded
		/// </summary>
		public bool IsRequired { get; set; }

		/// <summary>
		/// Gets or Sets the Allowed files to be uploaded
		/// </summary>
		public string[] AllowedFileExtensions { get; set; }

		/// <summary>
		/// Gets or Sets the Error Message to be displayed by the IsRequired Validator
		/// </summary>
		public string RequiredErrorMessage { get; set; }

		/// <summary>
		/// Gets or Sets the Validation Group of the IsRequired Validator
		/// </summary>
		public string RequiredValidationGroup { get; set; }

		/// <summary>
		/// Gets the list of Uploaded Files
		/// </summary>
		public IList<UploadedFile> UploadedFilesList
		{
			get
			{
				return this._UploadedFilesList;
			}
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			if(this.IsRequired)
			{
				string script = "function validateUpload(sender, args) {";
				script += "var upload = $find(\"<%= fileUploadRadAsyncUpload.ClientID %>\")";
				script += "args.IsValid = upload.getUploadedFiles().length != 0;}";
				this.Page.ClientScript.RegisterClientScriptBlock(typeof(string), "VerificacionFileUpload", script);
				CustomValidator customValidatorFileUpload = new CustomValidator();
				customValidatorFileUpload.ID = "fileUploadCustomValidator";
				customValidatorFileUpload.ClientValidationFunction = "validateUpload";
				customValidatorFileUpload.ErrorMessage = this.RequiredErrorMessage;
				customValidatorFileUpload.ValidationGroup = this.RequiredValidationGroup;
			}
			if(this.FileMaxLength != 0)
				this.fileUploadRadAsyncUpload.MaxFileSize = this.FileMaxLength;
			else
				this.fileUploadRadAsyncUpload.MaxFileSize = 0;
			if(this.MaxFilesCount != 0)
				this.fileUploadRadAsyncUpload.MaxFileInputsCount = this.MaxFilesCount;
			else
				this.fileUploadRadAsyncUpload.MaxFileInputsCount = 0;
			if(this.AllowedFileExtensions != null)
				this.fileUploadRadAsyncUpload.AllowedFileExtensions = this.AllowedFileExtensions;
		}

		protected void fileUploadRadAsyncUpload_FileUploaded(object sender, Telerik.Web.UI.FileUploadedEventArgs e)
		{
			this._UploadedFilesList.Add(e.File);
		}
	}
}