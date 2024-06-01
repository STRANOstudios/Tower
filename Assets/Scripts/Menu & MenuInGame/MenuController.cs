using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[DisallowMultipleComponent]
public class MenuController : MonoBehaviour
{
    [Header("Levels To Load")]
    [SerializeField, Tooltip("The name of the scene to be loaded")] private string sceneToBeLoad;

    [Header("References")]
    [SerializeField, Tooltip("The canvas that contains the menu")] private GameObject menuCanvas;
    [SerializeField] private Slider timeSlider;

    private bool previousState;

    private void Start()
    {
        previousState = GameManager.Instance.IsGameRunning;
    }

    private void Update()
    {
        if (previousState != GameManager.Instance.IsGameRunning)
        {
            menuCanvas.SetActive(!GameManager.Instance.IsGameRunning);
            timeSlider.interactable = GameManager.Instance.IsGameRunning;
            previousState = GameManager.Instance.IsGameRunning;
        }
    }

    #region Menu Buttons

    public void PlayButton()
    {
#if UNITY_EDITOR
        SceneManager.LoadScene(sceneToBeLoad);
#else
        SceneManager.LoadScene(1);
#endif
    }

    public void ExitButton()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void ReturnButton()
    {
#if UNITY_EDITOR
        SceneManager.LoadScene(sceneToBeLoad);
#else
        SceneManager.LoadScene(0);
#endif
    }

    #endregion
}