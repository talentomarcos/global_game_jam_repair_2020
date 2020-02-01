using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyData
{
    public GameObject prefab;
    [Tooltip("This is to have some enemies not spawn until the difficulty is a certain amount.")]
    public int minRuneCountToSpawn = 0;
    [Tooltip("The higher this is the more of this enemies that will be spawned if possible.")]
    [Range(0,1)]
    public float spawnProbability = .5f;

    public bool IsValidEnemy(int aRuneCount)
    {
        return aRuneCount >= minRuneCountToSpawn;
    }
}
