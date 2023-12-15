using System;

namespace NativeFileBrowser
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

	public static class StandaloneFileBrowser
	{
		private static readonly NativeFileBrowserWindows NativeFileBrowser;

		static StandaloneFileBrowser()
		{
			NativeFileBrowser = new NativeFileBrowserWindows();
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
			return NativeFileBrowser.OpenFilePanel(title, extensions, multiselect);
		}

		/// <summary>
		/// Native open folder dialog
		/// </summary>
		/// <param name="title"></param>
		/// <returns>Returns array of chosen paths. Zero length array when cancelled</returns>
		public static string[] OpenFolderPanel(string title)
		{
			return NativeFileBrowser.OpenFolderPanel(title);
		}

		/// <summary>
		/// Native open folder dialog async
		/// </summary>
		/// <param name="title"></param>
		/// <param name="cb">Callback")</param>
		public static void OpenFolderPanelAsync(string title, Action<string[]> cb)
		{
			NativeFileBrowser.OpenFolderPanelAsync(title, cb);
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
			return NativeFileBrowser.SaveFilePanel(title, defaultName, extensions);
		}

		/// <summary>
		/// Native save file dialog async
		/// </summary>
		/// <param name="title">Dialog title</param>
		/// <param name="defaultName">Default file name</param>
		/// <param name="filterName">filter name</param>
		/// <param name="extension">File extension</param>
		/// <param name="cb">Callback")</param>
		public static void SaveFilePanelAsync(string title, string defaultName, string filterName,
			string extension,
			Action<string> cb)
		{
			var extensions = string.IsNullOrEmpty(extension)
				? null
				: new[] { new ExtensionFilter(filterName, extension) };
			SaveFilePanelAsync(title, defaultName, extensions, cb);
		}

		/// <summary>
		/// Native save file dialog async
		/// </summary>
		/// <param name="title">Dialog title</param>
		/// <param name="defaultName">Default file name</param>
		/// <param name="extensions">List of extension filters. Filter Example: new ExtensionFilter("Image Files", "jpg", "png")</param>
		/// <param name="cb">Callback")</param>
		public static void SaveFilePanelAsync(string title, string defaultName,
			ExtensionFilter[] extensions, Action<string> cb)
		{
			NativeFileBrowser.SaveFilePanelAsync(title, defaultName, extensions, cb);
		}
	}
}