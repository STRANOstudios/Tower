using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Level", menuName = "ScriptableObjects/Level")]
public class Level : ScriptableObject
{
    public GameObject mazePrefab;
    public GameObject enemyPrefab;
    public float timeBetweenWaves;
    public List<Wave> waves = new();
}

[System.Serializable]
public class Wave
{
    public int enemies;
    public float timeBetweenSpawns;
}