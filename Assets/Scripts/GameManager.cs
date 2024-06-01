using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    private bool isGameRunning = true;

    public static GameManager Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this as GameManager;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0) return;

        if (Input.GetKeyDown(KeyCode.Escape)) ToggleGameRunning();
    }

    public void ToggleGameRunning()
    {
        isGameRunning = !isGameRunning;
        Time.timeScale = isGameRunning ? 1 : 0;
    }

    public bool IsGameRunning
    {
        get { return isGameRunning; }
        set { isGameRunning = value; }
    }
}
