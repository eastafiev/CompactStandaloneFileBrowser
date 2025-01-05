using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
#if ENABLE_WINMD_SUPPORT
using Windows.Storage.Pickers;
using Windows.Foundation;
using Windows.Storage;
#endif
using UnityEngine.WSA;
public class FilePicker : MonoBehaviour
{
   // [SerializeField] private Button _openFileButton;

    private void Awake()
    {
        MonoFilePicker.OnRunFileBrowser += OpenFilePicker;

        //_openFileButton.onClick.AddListener(OpenFilePicker);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            OpenFilePicker();
           
        }
    }

    public void OpenFilePicker()
    {
       
#if ENABLE_WINMD_SUPPORT
        RunUWPFilePicker();
#else
        Debug.Log("File picker is not supported on this platform.");
#endif
    }

#if ENABLE_WINMD_SUPPORT
    private async void RunUWPFilePicker()
    {
        // Run on the UI thread
        UnityEngine.WSA.Application.InvokeOnUIThread(async () =>
        {
            var picker = new Windows.Storage.Pickers.FileOpenPicker();
            picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.DocumentsLibrary;

            // Add file types you want to allow
            picker.FileTypeFilter.Add(".txt");
            picker.FileTypeFilter.Add(".jpg");
            picker.FileTypeFilter.Add(".png");

            // Open the picker
            Windows.Storage.StorageFile file = await picker.PickSingleFileAsync();
            if (file != null)
            {
                // Read the file or get its path
                string filePath = file.Path;
                Debug.Log("Picked file: " + filePath);

                // Optionally, read the content
                string fileContent = await Windows.Storage.FileIO.ReadTextAsync(file);
                Debug.Log("File content: " + fileContent);
            }
            else
            {
                Debug.Log("No file was picked.");
            }
        }, true);
    }
#endif
}
