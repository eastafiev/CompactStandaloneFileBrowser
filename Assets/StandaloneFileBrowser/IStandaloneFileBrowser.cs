using System;

namespace StandaloneFileBrowser
{
	public interface IStandaloneFileBrowser
	{
		string[] OpenFilePanel(string title, ExtensionFilter[] extensions, bool multiselect);

		void OpenFilePanelAsync(string title, ExtensionFilter[] extensions, bool multiselect,
			Action<string[]> cb);

		string[] OpenFolderPanel(string title);
		void OpenFolderPanelAsync(string title, Action<string[]> cb);

		string SaveFilePanel(string title, string defaultName, ExtensionFilter[] extensions);

		void SaveFilePanelAsync(string title, string defaultName, ExtensionFilter[] extensions,
			Action<string> cb);
	}
}