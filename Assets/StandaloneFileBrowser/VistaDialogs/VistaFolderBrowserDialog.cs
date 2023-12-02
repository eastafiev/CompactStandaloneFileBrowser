using System;
using System.ComponentModel;

namespace NativeFileBrowsers
{
	public class VistaFolderBrowserDialog : VistaFileDialog
	{
		private string _selectedPath;
		private Environment.SpecialFolder _rootFolder;


		#region Public Properties

		/// <summary>
		/// Gets or sets the folder path selected by the user.
		/// </summary>
		/// <value>
		/// The path of the folder first selected in the dialog box or the last folder selected by the user. The default is an empty string ("").
		/// </value>
		[Browsable(true),
		 Editor(
			 "System.Windows.Forms.Design.SelectedPathEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
			 typeof(System.Drawing.Design.UITypeEditor)), Description("The path selected by the user."),
		 DefaultValue(""), Localizable(true), Category("Folder Browsing")]
		public string SelectedPath
		{
			get { return _selectedPath; }
			set { _selectedPath = value ?? string.Empty; }
		}

		#endregion

		internal override IFileDialog CreateFileDialog()
		{
			return (IFileDialog)new FileOpenDialogRCW();
		}

		protected override void Reset()
		{
			_selectedPath = string.Empty;
			base.Reset();
		}

		internal override void SetDialogProperties(IFileDialog dialog)
		{
			dialog.SetOptions(FOS.FOS_PICKFOLDERS | FOS.FOS_FORCEFILESYSTEM |
			                  FOS.FOS_FILEMUSTEXIST);
		}

		internal override void GetResult(IFileDialog dialog)
		{
			IShellItem item;
			dialog.GetResult(out item);
			item.GetDisplayName(SIGDN.SIGDN_FILESYSPATH, out _selectedPath);
		}
	}
}