
#if ENABLE_WINMD_SUPPORT

using System;
using System.Collections.Generic;
using Windows.Storage.Pickers;
using Windows.Foundation;
using Windows.Storage;
using UnityEngine;
using System.Threading.Tasks;


namespace NativeFileBrowser
{
    public class UwpFilePicker : INativeFileBrowser
    {
       
        public string[] OpenFilePanel(string title, ExtensionFilter[] extensions, bool multiselect)
        {
            Debug.LogWarning("Opening uwpw file picker");
            UnityEngine.WSA.Application.InvokeOnUIThread(async () =>
            {
                FileOpenPicker openPicker = new FileOpenPicker();
                openPicker.ViewMode = PickerViewMode.Thumbnail;
                openPicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
                openPicker.FileTypeFilter.Add(".jpg");
                openPicker.FileTypeFilter.Add(".jpeg");
                openPicker.FileTypeFilter.Add(".png");
                
                // Open the picker
                IAsyncOperation<IReadOnlyList<StorageFile>> files = await openPicker.PickMultipleFilesAsync();
                
                // Come back to Unity's/Game's main thread
                UnityEngine.WSA.Application.InvokeOnAppThread(() =>
                {
                    // Process the computed result, it's safe to interact with all scene objects and Unity API here.
                }, false);
            },true);
            //IAsyncOperation<IReadOnlyList<StorageFile>> file =  openPicker.PickMultipleFilesAsync();
            return null;

        }
        public void  OpenFilePanelAsync(string title, ExtensionFilter[] extensions, bool multiselect, Action<string[]> cb)
        {
            
        }
        public string[] OpenFolderPanel(string title)
        {
            throw new NotImplementedException();
        }
        public void OpenFolderPanelAsync(string title, Action<string[]> callback)
        {
            throw new NotImplementedException();
        }
        public string SaveFilePanel(string title, string defaultName, ExtensionFilter[] extensions)
        {
            throw new NotImplementedException();
        }
        public void SaveFilePanelAsync(string title, string defaultName, ExtensionFilter[] extensions, Action<string> cb)
        {
            throw new NotImplementedException();
        }
    }
}
#endif
