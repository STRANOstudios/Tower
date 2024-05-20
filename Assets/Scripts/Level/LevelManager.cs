using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [Header("Levels")]
    [SerializeField] private List<Level> levels = new List<Level>();

    private int levelIndex = 0;
    private static LevelManager instance;

    public static LevelManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<LevelManager>();
                if (instance == null)
                {
                    GameObject obj = new GameObject("LevelManager");
                    instance = obj.AddComponent<LevelManager>();
                }
            }
            return instance;
        }
    }

    private Dictionary<string, Level> levelDictionary = new Dictionary<string, Level>();

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;

        // Stacca l'oggetto dal suo genitore se ce l'ha
        if (transform.parent != null)
        {
            transform.SetParent(null);
        }

        DontDestroyOnLoad(gameObject);

        // Cache delle scene dei livelli per un accesso più veloce
        foreach (var level in levels)
        {
            levelDictionary[level.name] = level;
        }
    }

    public Level GetCurrentLevel() => levels[levelIndex];

    public Level GetLevel(int index) => (index >= 0 && index < levels.Count) ? levels[index] : null;

    public void NextLevel()
    {
        levelIndex++;
        if (levelIndex >= levels.Count)
        {
            levelIndex = 0; // Opzionale: ricomincia dal primo livello se si raggiunge la fine
        }
    }

    public void ResetLevel() => levelIndex = 0;
}
