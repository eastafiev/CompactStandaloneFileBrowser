using System;
using System.Runtime.InteropServices;

namespace NativeFileBrowser
{
	[ComImport]
	[ClassInterface(ClassInterfaceType.None)]
	[TypeLibType(TypeLibTypeFlags.FCanCreate)]
	[Guid(CLSIDGuid.FileOpenDialog)]
	internal class FileOpenDialogRCW
	{
	}

	[ClassInterface(ClassInterfaceType.None)]
	[TypeLibType(TypeLibTypeFlags.FCanCreate)]
	[Guid(CLSIDGuid.FileSaveDialog)]
	[ComImport]
	internal class FileSaveDialogRCW
	{
	}

	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	internal struct PROPERTYKEY
	{
		internal Guid fmtid;
		internal uint pid;
	}
}