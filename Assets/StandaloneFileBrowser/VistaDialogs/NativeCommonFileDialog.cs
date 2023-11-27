using System;
using System.Runtime.InteropServices;

namespace StandaloneFileBrowser
{
	[ComImport,
	 ClassInterface(ClassInterfaceType.None),
	 TypeLibType(TypeLibTypeFlags.FCanCreate),
	 Guid(CLSIDGuid.FileOpenDialog)]
	internal class FileOpenDialogRCW
	{
	}

	[ClassInterface(ClassInterfaceType.None)]
	[TypeLibType(TypeLibTypeFlags.FCanCreate)]
	[Guid(CLSIDGuid.FileSaveDialog)]
	[ComImport]
	internal class FileSaveDialogRCW
	{
		//[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
		//public extern FileSaveDialogRCW();
	}


	// Property System structs and consts
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	internal struct PROPERTYKEY
	{
		internal Guid fmtid;
		internal uint pid;
	}
}