using System.ComponentModel;

namespace NativeFileBrowser
{
	public sealed class VistaOpenFileDialog : VistaFileDialog
	{
		private bool _showReadOnly;
		private bool _readOnlyChecked;

		[Description("A value indicating whether the dialog box allows multiple files to be selected.")]
		public bool Multiselect
		{
			get => GetOption(FOS.FOS_ALLOWMULTISELECT);
			set { SetOption(FOS.FOS_ALLOWMULTISELECT, value); }
		}

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
				((IFileOpenDialog)dialog).GetResults(out var ppenum);
				ppenum.GetCount(out var pdwNumItems);
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