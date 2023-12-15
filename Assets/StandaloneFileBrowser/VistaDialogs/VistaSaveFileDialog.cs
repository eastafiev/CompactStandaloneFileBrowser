using System.ComponentModel;

namespace NativeFileBrowser
{
	public sealed class VistaSaveFileDialog : VistaFileDialog
	{
		public VistaSaveFileDialog()
		{
			Reset();
			OverwritePrompt = true;
		}

		internal override IFileDialog CreateFileDialog() => (IFileDialog)new FileSaveDialogRCW();

		[Category("Behavior")]
		[DefaultValue(true)]
		[Description(
			"A value indicating whether the Save As dialog box displays a warning if the user specifies a file name that already exists.")]
		public bool OverwritePrompt
		{
			get => GetOption(FOS.FOS_OVERWRITEPROMPT);
			set { SetOption(FOS.FOS_OVERWRITEPROMPT, value); }
		}
	}
}