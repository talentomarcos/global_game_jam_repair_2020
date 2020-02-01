﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CLane : MonoBehaviour
{
    public int _laneIndex;
    public CMercenary _mercenary;
    public Transform _enemySpawnPonint;
    private CEnemy _currentEnemy;

    /// <summary>
    /// Called when the player succesfully completes a sequence.
    /// </summary>
    public void OnSequenceComplete()
    {

    }

    /// <summary>
    /// Called when a sequence's time is finished without success.
    /// </summary>
    public void OnSequenceEnded()
    {

    }

    public void SpawnEnemy()
    {
        float maxProbab = 0;
        int maxProbIndex = 0;
        float highestProb = 0;
        List<EnemyData> enemies = CLevelManager.Inst._enemies;
        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i].IsValidEnemy(CSequenceManager.Inst.GetCurrentRuneCount()))
            {
                maxProbab += enemies[i].spawnProbability;
                if (highestProb < enemies[i].spawnProbability)
                {
                    highestProb = enemies[i].spawnProbability;
                    maxProbIndex = i;
                }
            }
        }
        float rand = Random.value;
        int spawnIndex = -1;
        float spawnProb = 1;
        for (int i = 0; i < enemies.Count; i++)
        {
            float prob = enemies[i].spawnProbability / maxProbab;
            if (rand <= prob && spawnProb > prob)
            {
                spawnIndex = i;
                spawnProb = prob;
            }
        }
        if (spawnIndex < 0)
        {
            CreateEnemy(maxProbIndex);
        }
        else
        {
            CreateEnemy(spawnIndex);
        }
        if (_currentEnemy == null)
        {
            return;
        }
        CSequenceData data = CSequenceManager.Inst.RequestSequence(_laneIndex);
        // To do: use data to update mercenary dialog (UI).
        //_mercenary
    }

    private void CreateEnemy(int aIndex)
    {
        if (aIndex<0 || aIndex >= CLevelManager.Inst._enemies.Count)
        {
            return;
        }
        GameObject enemy = Instantiate(CLevelManager.Inst._enemies[aIndex].prefab,transform);
        _currentEnemy = enemy.GetComponent<CEnemy>();
        enemy.transform.position = _enemySpawnPonint.position;
    }
}
