using UnityEngine;
using UnityEngine.UI;
using NativeFileBrowser;

public class BasicSample : MonoBehaviour
{
	[SerializeField] public Button _openFileButton;
	[SerializeField] public Button _openFolderButton;
	[SerializeField] public Button _saveFileButton;


	private void Awake()
	{
		_openFileButton.onClick.AddListener(OpenFiles);
		_openFolderButton.onClick.AddListener(SelectFolder);
		_saveFileButton.onClick.AddListener(SaveFile);
	}

	private void OpenFiles()
	{
		var title = "Open Image File";
		var extensions = new[]
		{
			new ExtensionFilter("Image Files", "png", "jpg", "jpeg"),
			new ExtensionFilter("JPG ", "jpg", "jpeg"),
			new ExtensionFilter("PNG ", "png"),
		};

		var path = StandaloneFileBrowser.OpenFilePanel(title, extensions, true);
	}

	private void SelectFolder()
	{
		var title = "Select Folder";

		var path = StandaloneFileBrowser.OpenFolderPanel(title);
	}

	private void SaveFile()
	{
		var title = "Save file";
		var extensionList = new[]
		{
			new ExtensionFilter("Binary", "bin"),
			new ExtensionFilter("Text", "txt"),
		};
		var file = StandaloneFileBrowser.SaveFilePanel(title,  "Save file", extensionList);
	}
}