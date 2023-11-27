using System.ComponentModel;

namespace StandaloneFileBrowser
{
	public class VistaOpenFileDialog : VistaFileDialog
	{
		private bool _showReadOnly;
		private bool _readOnlyChecked;

		[Description("A value indicating whether the dialog box allows multiple files to be selected.")]
		public bool Multiselect
		{
			get => GetOption(FOS.FOS_ALLOWMULTISELECT);
			set { SetOption(FOS.FOS_ALLOWMULTISELECT, value); }
		}

		/// <summary>
		/// Creates a new instance of the <see cref="VistaFolderBrowserDialog" /> class.
		/// </summary>
		public VistaOpenFileDialog()
		{
			Reset();
		}


		internal override IFileDialog CreateFileDialog()
		{
			return (IFileDialog)new FileOpenDialogRCW();
		}

		internal override void GetResult(IFileDialog dialog)
		{
			if (Multiselect)
			{
				IShellItemArray ppenum;
				((IFileOpenDialog)dialog).GetResults(out ppenum);
				uint pdwNumItems;
				ppenum.GetCount(out pdwNumItems);
				string[] strArray = new string[(int)pdwNumItems];
				for (uint dwIndex = 0; dwIndex < pdwNumItems; ++dwIndex)
				{
					IShellItem ppsi;
					ppenum.GetItemAt(dwIndex, out ppsi);
					string ppszName;
					ppsi.GetDisplayName(SIGDN.SIGDN_FILESYSPATH, out ppszName);
					strArray[(int)dwIndex] = ppszName;
				}

				FileNamesInternal = strArray;
			}
			else
				FileNamesInternal = (string[])null;

			base.GetResult(dialog);
		}
	}
}