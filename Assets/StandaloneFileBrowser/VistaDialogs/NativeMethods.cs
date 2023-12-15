using System;
using System.Runtime.InteropServices;

namespace NativeFileBrowser
{
	internal class NativeMethods
	{
		[DllImport("user32.dll")]
		public static extern IntPtr GetActiveWindow();

		// [DllImport("Comdlg32.dll", SetLastError = true, CharSet = CharSet.Auto)]
		// public static extern bool GetOpenFileName([In, Out] OpenFileName ofn);
		//
		// [DllImport("Comdlg32.dll", SetLastError = true, CharSet = CharSet.Auto)]
		// public static extern bool GetSaveFileName([In, Out] OpenFileName ofn);

		[DllImport("shell32.dll", CharSet = CharSet.Unicode)]
		public static extern int SHCreateItemFromParsingName([MarshalAs(UnmanagedType.LPWStr)] string pszPath,
			IntPtr pbc, ref Guid riid, [MarshalAs(UnmanagedType.Interface)] out object ppv);


		public static IShellItem CreateItemFromParsingName(string path)
		{
			object item;
			Guid guid = new Guid("43826d1e-e718-42ee-bc55-a1e261c37bfe"); // IID_IShellItem
			int hr = NativeMethods.SHCreateItemFromParsingName(path, IntPtr.Zero, ref guid, out item);
			if (hr != 0)
				throw new System.ComponentModel.Win32Exception(hr);
			return (IShellItem)item;
		}
	}

	internal enum FDE_OVERWRITE_RESPONSE
	{
		FDEOR_DEFAULT = 0x00000000,
		FDEOR_ACCEPT = 0x00000001,
		FDEOR_REFUSE = 0x00000002
	}

	internal enum FDE_SHAREVIOLATION_RESPONSE
	{
		FDESVR_DEFAULT = 0x00000000,
		FDESVR_ACCEPT = 0x00000001,
		FDESVR_REFUSE = 0x00000002
	}

	[Flags]
	internal enum FOS : uint
	{
		FOS_OVERWRITEPROMPT = 0x00000002,
		FOS_STRICTFILETYPES = 0x00000004,
		FOS_NOCHANGEDIR = 0x00000008,
		FOS_PICKFOLDERS = 0x00000020,
		FOS_FORCEFILESYSTEM = 0x00000040, // Ensure that items returned are filesystem items.
		FOS_ALLNONSTORAGEITEMS = 0x00000080, // Allow choosing items that have no storage.
		FOS_NOVALIDATE = 0x00000100,
		FOS_ALLOWMULTISELECT = 0x00000200,
		FOS_PATHMUSTEXIST = 0x00000800,
		FOS_FILEMUSTEXIST = 0x00001000,
		FOS_CREATEPROMPT = 0x00002000,
		FOS_SHAREAWARE = 0x00004000,
		FOS_NOREADONLYRETURN = 0x00008000,
		FOS_NOTESTFILECREATE = 0x00010000,
		FOS_HIDEMRUPLACES = 0x00020000,
		FOS_HIDEPINNEDPLACES = 0x00040000,
		FOS_NODEREFERENCELINKS = 0x00100000,
		FOS_DONTADDTORECENT = 0x02000000,
		FOS_FORCESHOWHIDDEN = 0x10000000,
		FOS_DEFAULTNOMINIMODE = 0x20000000
	}

	public enum SIGDN : uint
	{
		SIGDN_NORMALDISPLAY = 0x00000000, // SHGDN_NORMAL
		SIGDN_PARENTRELATIVEPARSING = 0x80018001, // SHGDN_INFOLDER | SHGDN_FORPARSING
		SIGDN_DESKTOPABSOLUTEPARSING = 0x80028000, // SHGDN_FORPARSING
		SIGDN_PARENTRELATIVEEDITING = 0x80031001, // SHGDN_INFOLDER | SHGDN_FOREDITING
		SIGDN_DESKTOPABSOLUTEEDITING = 0x8004c000, // SHGDN_FORPARSING | SHGDN_FORADDRESSBAR
		SIGDN_FILESYSPATH = 0x80058000, // SHGDN_FORPARSING
		SIGDN_URL = 0x80068000, // SHGDN_FORPARSING
		SIGDN_PARENTRELATIVEFORADDRESSBAR = 0x8007c001, // SHGDN_INFOLDER | SHGDN_FORPARSING | SHGDN_FORADDRESSBAR
		SIGDN_PARENTRELATIVE = 0x80080001 // SHGDN_INFOLDER
	}

	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto, Pack = 4)]
	internal struct COMDLG_FILTERSPEC
	{
		[MarshalAs(UnmanagedType.LPWStr)]
		internal string pszName;

		[MarshalAs(UnmanagedType.LPWStr)]
		internal string pszSpec;
	}

	internal enum CDCONTROLSTATE
	{
		CDCS_INACTIVE = 0x00000000,
		CDCS_ENABLED = 0x00000001,
		CDCS_VISIBLE = 0x00000002
	}

	internal enum SIATTRIBFLAGS
	{
		SIATTRIBFLAGS_AND = 0x00000001, // if multiple items and the attirbutes together.
		SIATTRIBFLAGS_OR = 0x00000002, // if multiple items or the attributes together.
		SIATTRIBFLAGS_APPCOMPAT = 0x00000003, // Call GetAttributes directly on the ShellFolder for multiple attributes
	}
}