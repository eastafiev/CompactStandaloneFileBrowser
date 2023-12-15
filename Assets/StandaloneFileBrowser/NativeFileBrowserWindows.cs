using System;
using System.IO;

namespace NativeFileBrowser
{
	public class NativeFileBrowserWindows : INativeFileBrowser
	{
		public string[] OpenFilePanel(string title, ExtensionFilter[] extensions, bool multiselect)
		{
			var fd = new VistaOpenFileDialog();
			fd.Title = title;
			if (extensions != null)
			{
				fd.Filter = GetFilterFromFileExtensionList(extensions);
				fd.FilterIndex = 1;
			}
			else
			{
				fd.Filter = string.Empty;
			}

			fd.Multiselect = multiselect;
	
			var res = fd.ShowDialog();
			var filenames = res == true ? fd.FileNames : Array.Empty<string>();
			return filenames;
		}

		public void OpenFilePanelAsync(string title, ExtensionFilter[] extensions, bool multiselect,
			Action<string[]> cb)
		{
			cb.Invoke(OpenFilePanel(title, extensions, multiselect));
		}

		public string[] OpenFolderPanel(string title)
		{
			var fd = new VistaFolderBrowserDialog();
			fd.Title = title;
	
			var res = fd.ShowDialog();
			var filenames = res == true ? new[] { fd.SelectedPath } : Array.Empty<string>();
			return filenames;
		}

		public void OpenFolderPanelAsync(string title,  Action<string[]> callback)
		{
			callback.Invoke(OpenFolderPanel(title));
		}


		public string SaveFilePanel(string title,  string defaultName, ExtensionFilter[] extensions)
		{
			var fd = new VistaSaveFileDialog();
			fd.Title = title;

			var finalFilename = "";

			if (!string.IsNullOrEmpty(defaultName))
			{
				finalFilename += defaultName;
			}

			fd.FileName = finalFilename;
			if (extensions != null)
			{
				fd.Filter = GetFilterFromFileExtensionList(extensions);
				fd.FilterIndex = 1;
				fd.DefaultExt = extensions[0].Extensions[0];
				fd.AddExtension = true;
			}
			else
			{
				fd.DefaultExt = string.Empty;
				fd.Filter = string.Empty;
				fd.AddExtension = false;
			}

			var res = fd.ShowDialog();
			var filename = res == true ? fd.FileName : "";

			return filename;
		}

		public void SaveFilePanelAsync(string title, string defaultName, ExtensionFilter[] extensions,
			Action<string> cb)
		{
			cb.Invoke(SaveFilePanel(title, defaultName, extensions));
		}

		// .NET Framework FileDialog Filter format
		// https://msdn.microsoft.com/en-us/library/microsoft.win32.filedialog.filter
		private static string GetFilterFromFileExtensionList(ExtensionFilter[] extensions)
		{
			var filterString = "";
			foreach (var filter in extensions)
			{
				filterString += filter.Name + "(";

				foreach (var ext in filter.Extensions)
				{
					filterString += "*." + ext + ",";
				}

				filterString = filterString.Remove(filterString.Length - 1);
				filterString += ") |";

				foreach (var ext in filter.Extensions)
				{
					filterString += "*." + ext + "; ";
				}

				filterString += "|";
			}

			filterString = filterString.Remove(filterString.Length - 1);
			return filterString;
		}

		private static string GetDirectoryPath(string directory)
		{
			var directoryPath = Path.GetFullPath(directory);
			if (!directoryPath.EndsWith("\\"))
			{
				directoryPath += "\\";
			}

			return Path.GetDirectoryName(directoryPath) + Path.DirectorySeparatorChar;
		}
	}
}