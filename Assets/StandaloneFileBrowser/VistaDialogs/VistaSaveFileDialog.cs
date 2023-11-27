namespace StandaloneFileBrowser
{
	public class VistaSaveFileDialog : VistaFileDialog
	{
		internal override IFileDialog CreateFileDialog() => (IFileDialog)new FileSaveDialogRCW();
	}
}