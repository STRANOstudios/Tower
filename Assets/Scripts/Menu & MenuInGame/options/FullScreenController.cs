using UnityEngine;
using UnityEngine.UI;

[DisallowMultipleComponent]
public class FullScreenController : MonoBehaviour
{
    [SerializeField] private Toggle fullScreenToggle = null;

    private void Awake()
    {
        Initialize();
    }

    private void OnEnable()
    {
        fullScreenToggle.onValueChanged.AddListener(SetFullScreen);
    }

    private void OnDestroy()
    {
        fullScreenToggle.onValueChanged.RemoveListener(SetFullScreen);
    }

    private void SetFullScreen(bool value)
    {
        Screen.fullScreen = value;
        PlayerPrefs.SetInt("FullScreen", value ? 1 : 0);
    }

    public void Initialize()
    {
        fullScreenToggle.isOn = PlayerPrefs.GetInt("FullScreen") == 0 ? false : true;
        Screen.fullScreen = fullScreenToggle.isOn;
    }
}