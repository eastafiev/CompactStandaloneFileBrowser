using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.InteropServices;

namespace NativeFileBrowser
{
	public abstract class VistaFileDialog

	{
		private FOS _options;
		private string _filter;
		private int _filterIndex;
		private string[] _fileNames;
		private string _defaultExt;
		private bool _addExtension;
		private string _initialDirectory;
		private bool _showHelp;
		private string _title;
		private bool _supportMultiDottedExtensions;
		private IntPtr _hwndOwner;

		[Description(
			"A value indicating whether the dialog box automatically adds an extension to a file name if the user omits the extension.")]
		public bool AddExtension
		{
			get => _addExtension;
			set { _addExtension = value; }
		}

		[Description("The file dialog box title.")]
		public string Title
		{
			get { return _title != null ? _title : string.Empty; }
			set { _title = value; }
		}

		[Description(
			"The current file name filter string, which determines the choices that appear in the \"Save as file type\" or \"Files of type\" box in the dialog box.")]
		[Category("Behavior")]
		[DefaultValue("")]
		public string Filter
		{
			get => _filter;
			set
			{
				if (value != _filter)
				{
					if (!string.IsNullOrEmpty(value))
					{
						string[] strArray = value.Split('|');
						if (strArray == null || strArray.Length % 2 != 0)
							throw new ArgumentException("InvalidFilterString");
					}
					else
						value = (string)null;

					_filter = value;
				}
			}
		}

		[Description("The index of the filter currently selected in the file dialog box.")]
		public int FilterIndex
		{
			get => _filterIndex;
			set { _filterIndex = value; }
		}

		[Description("A string containing the file name selected in the file dialog box.")]
		public string FileName
		{
			get
			{
				return _fileNames == null || _fileNames.Length == 0 ||
				       string.IsNullOrEmpty(_fileNames[0])
					? string.Empty
					: _fileNames[0];
			}
			set
			{
				_fileNames = new string[1];
				_fileNames[0] = value;
			}
		}

		[Description("The default file name extension.")]
		public string DefaultExt
		{
			get => _defaultExt ?? string.Empty;
			set
			{
				if (value != null)
				{
					if (value.StartsWith(".", StringComparison.Ordinal))
						value = value.Substring(1);
					else if (value.Length == 0)
						value = (string)null;
				}

				_defaultExt = value;
			}
		}

		[Description("The file names of all selected files in the dialog box.")]
		public string[] FileNames => FileNamesInternal;

		internal string[] FileNamesInternal
		{
			private get => this._fileNames == null ? new string[0] : (string[])this._fileNames.Clone();
			set => this._fileNames = value;
		}

		protected VistaFileDialog() => Reset();
		internal abstract IFileDialog CreateFileDialog();

		/// <summary>
		/// Displays the folder browser dialog.
		/// </summary>
		/// <returns>If the user clicks the OK button, <see langword="true" /> is returned; otherwise, <see langword="false" />.</returns>
		public bool? ShowDialog()
		{
			IntPtr ownerHandle = NativeMethods.GetActiveWindow();
			return RunDialog(ownerHandle);
		}

		private bool RunDialog(IntPtr hwndOwner)
		{
			if (hwndOwner == IntPtr.Zero)
			{
				return false;
			}

			IFileDialog dialog = null;
			try
			{
				dialog = CreateFileDialog();
				SetDialogProperties(dialog);
				int result = dialog.Show(hwndOwner);
				if (result < 0)
				{
					if ((uint)result == (uint)HRESULT.ERROR_CANCELLED)
						return false;
					else
						throw Marshal.GetExceptionForHR(result);
				}

				GetResult(dialog);
				return true;
			}
			finally
			{
				if (dialog != null)
					Marshal.FinalReleaseComObject(dialog);
			}
		}

		internal bool GetOption(FOS option) => (_options & option) > (FOS)0;

		internal void SetOption(FOS option, bool value)
		{
			if (value)
				_options |= option;
			else
				_options &= ~option;
		}

		internal virtual void GetResult(IFileDialog dialog)
		{
			if (GetOption(FOS.FOS_ALLOWMULTISELECT))
				return;
			_fileNames = new string[1];
			IShellItem ppsi;
			dialog.GetResult(out ppsi);
			ppsi.GetDisplayName(SIGDN.SIGDN_FILESYSPATH, out _fileNames[0]);
		}

		internal virtual void SetDialogProperties(IFileDialog dialog)
		{
			if (_fileNames != null && _fileNames.Length != 0 && !string.IsNullOrEmpty(_fileNames[0]))
			{
				string directoryName = Path.GetDirectoryName(_fileNames[0]);
				if (directoryName == null || !Directory.Exists(directoryName))
				{
					dialog.SetFileName(this._fileNames[0]);
				}
				else
				{
					string fileName = Path.GetFileName(this._fileNames[0]);
					dialog.SetFolder(NativeMethods.CreateItemFromParsingName(directoryName));
					dialog.SetFileName(fileName);
				}
			}

			if (!string.IsNullOrEmpty(_filter))
			{
				string[] strArray = _filter.Split('|');
				COMDLG_FILTERSPEC[] rgFilterSpec =
					new COMDLG_FILTERSPEC[strArray.Length / 2];
				for (int index = 0; index < strArray.Length; index += 2)
				{
					rgFilterSpec[index / 2].pszName = strArray[index];
					rgFilterSpec[index / 2].pszSpec = strArray[index + 1];
				}

				dialog.SetFileTypes((uint)rgFilterSpec.Length, rgFilterSpec);
				if (_filterIndex > 0 && _filterIndex <= rgFilterSpec.Length)
					dialog.SetFileTypeIndex((uint)_filterIndex);
			}

			if (_addExtension && !string.IsNullOrEmpty(this._defaultExt))
				dialog.SetDefaultExtension(_defaultExt);
			if (!string.IsNullOrEmpty(_title))
				dialog.SetTitle(_title);
			dialog.SetOptions(_options | FOS.FOS_FORCEFILESYSTEM);
		}


		protected virtual void Reset()
		{
			_fileNames = (string[])null;
			_filter = (string)null;
			_filterIndex = 1;
			_addExtension = true;
			_defaultExt = (string)null;
			_options = (FOS)0;
			_showHelp = false;
			_title = (string)null;
			_supportMultiDottedExtensions = false;
		}
	}
}