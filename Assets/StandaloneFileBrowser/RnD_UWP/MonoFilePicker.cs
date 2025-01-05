using System;
using UnityEngine;
using UnityEngine.UI;


public class MonoFilePicker : MonoBehaviour
{
    [SerializeField] private Button _openFileButton;
    public static Action OnRunFileBrowser;
    private void Awake()
    {

        _openFileButton.onClick.AddListener(() => OnRunFileBrowser?.Invoke());
    }


}
