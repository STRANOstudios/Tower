using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private Level level;
    private int currentWave = 0;

    private void Start()
    {
        // Assicurati che LevelManager sia istanziato
        if (LevelManager.Instance == null)
        {
            Debug.LogError("LevelManager instance not found!");
            return;
        }

        // Ottieni il livello corrente
        level = LevelManager.Instance.GetCurrentLevel();

        // Assicurati che il livello non sia null
        if (level == null)
        {
            Debug.LogError("Current level not found!");
            return;
        }

        StartCoroutine(SpawnWaves());
    }

    private IEnumerator SpawnWaves()
    {
        while (currentWave < level.waves.Count)
        {
            for (int i = 0; i < level.waves[currentWave].enemies; i++)
            {
                ObjectPoolerManager.SpawnObject(level.enemyPrefab, transform.position, Quaternion.identity);

                yield return new WaitForSeconds(level.waves[currentWave].timeBetweenSpawns);
            }
            yield return new WaitForSeconds(level.timeBetweenWaves);

            currentWave++;
        }
        currentWave = 0;
    }
}
