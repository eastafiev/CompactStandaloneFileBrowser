using System;

namespace StandaloneFileBrowser
{
	public struct ExtensionFilter
	{
		public string Name;
		public string[] Extensions;

		public ExtensionFilter(string filterName, params string[] filterExtensions)
		{
			Name = filterName;
			Extensions = filterExtensions;
		}
	}

	public class StandaloneFileBrowser
	{
		static StandaloneFileBrowserWindows _standaloneFileBrowser;

		static StandaloneFileBrowser()
		{
			_standaloneFileBrowser = new StandaloneFileBrowserWindows();
		}

		/// <summary>
		/// Native open file dialog
		/// </summary>
		/// <param name="title">Dialog title</param>
		/// <param name="extensions">List of extension filters. Filter Example: new ExtensionFilter("Image Files", "jpg", "png")</param>
		/// <param name="multiselect">Allow multiple file selection</param>
		/// <returns>Returns array of chosen paths. Zero length array when cancelled</returns>
		public static string[] OpenFilePanel(string title, ExtensionFilter[] extensions,
			bool multiselect)
		{
			return _standaloneFileBrowser.OpenFilePanel(title, extensions, multiselect);
		}

		/// <summary>
		/// Native open folder dialog
		/// NOTE: Multiple folder selection doesn't supported on Windows
		/// </summary>
		/// <param name="title"></param>
		/// <param name="multiselect"></param>
		/// <returns>Returns array of chosen paths. Zero length array when cancelled</returns>
		public static string[] OpenFolderPanel(string title)
		{
			return _standaloneFileBrowser.OpenFolderPanel(title);
		}

		/// <summary>
		/// Native open folder dialog async
		/// NOTE: Multiple folder selection doesn't supported on Windows
		/// </summary>
		/// <param name="title"></param>
		/// <param name="cb">Callback")</param>
		public static void OpenFolderPanelAsync(string title, Action<string[]> cb)
		{
			_standaloneFileBrowser.OpenFolderPanelAsync(title, cb);
		}

		/// <summary>
		/// Native save file dialog
		/// </summary>
		/// <param name="title">Dialog title</param>
		/// <param name="defaultName">Default file name</param>
		/// <param name="filterName">filter name</param>
		/// <param name="extension">File extension</param>
		/// <returns>Returns chosen path. Empty string when cancelled</returns>
		public static string SaveFilePanel(string title, string defaultName, string filterName, string extension)
		{
			var extensions = string.IsNullOrEmpty(extension)
				? null
				: new[] { new ExtensionFilter(filterName, extension) };
			return SaveFilePanel(title, defaultName, extensions);
		}

		/// <summary>
		/// Native save file dialog
		/// </summary>
		/// <param name="title">Dialog title</param>
		/// <param name="defaultName">Default file name</param>
		/// <param name="extensions">List of extension filters. Filter Example: new ExtensionFilter("Image Files", "jpg", "png")</param>
		/// <returns>Returns chosen path. Empty string when cancelled</returns>
		public static string SaveFilePanel(string title, string defaultName,
			ExtensionFilter[] extensions)
		{
			return _standaloneFileBrowser.SaveFilePanel(title, defaultName, extensions);
		}

		/// <summary>
		/// Native save file dialog async
		/// </summary>
		/// <param name="title">Dialog title</param>
		/// <param name="defaultName">Default file name</param>
		/// <param name="filterName">filter name</param>
		/// <param name="extension">File extension</param>
		/// <param name="cb">Callback")</param>
		public static void SaveFilePanelAsync(string title, string directory, string defaultName, string filterName,
			string extension,
			Action<string> cb)
		{
			var extensions = string.IsNullOrEmpty(extension)
				? null
				: new[] { new ExtensionFilter(filterName, extension) };
			SaveFilePanelAsync(title, directory, defaultName, extensions, cb);
		}

		/// <summary>
		/// Native save file dialog async
		/// </summary>
		/// <param name="title">Dialog title</param>
		/// <param name="defaultName">Default file name</param>
		/// <param name="extensions">List of extension filters. Filter Example: new ExtensionFilter("Image Files", "jpg", "png")</param>
		/// <param name="cb">Callback")</param>
		public static void SaveFilePanelAsync(string title, string directory, string defaultName,
			ExtensionFilter[] extensions, Action<string> cb)
		{
			_standaloneFileBrowser.SaveFilePanelAsync(title, defaultName, extensions, cb);
		}
	}
}