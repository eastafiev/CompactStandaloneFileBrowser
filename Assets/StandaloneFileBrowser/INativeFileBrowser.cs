using System;

namespace NativeFileBrowser
{
	public interface INativeFileBrowser
	{
		string[] OpenFilePanel(string title, ExtensionFilter[] extensions, bool multiselect);

		void OpenFilePanelAsync(string title, ExtensionFilter[] extensions, bool multiselect,
			Action<string[]> cb);

		string[] OpenFolderPanel(string title);
		void OpenFolderPanelAsync(string title, Action<string[]> callback);

		string SaveFilePanel(string title, string defaultName, ExtensionFilter[] extensions);

		void SaveFilePanelAsync(string title, string defaultName, ExtensionFilter[] extensions,
			Action<string> cb);
	}
}