using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CLane : MonoBehaviour
{
    public int _laneIndex;
    public CMercenary _mercenary;
    public Transform _enemySpawnPonint;
    private CEnemy _currentEnemy;

    private float _elapsedTimeNoEnemy = 0;

    private void Start()
    {
        _elapsedTimeNoEnemy = CMath.RandomFloatBetween(0, CLevelManager.Inst.GetCurrentTimeEnemySpawn() + 1);
    }

    /// <summary>
    /// Called when the player succesfully completes a sequence.
    /// </summary>
    public void OnSequenceComplete()
    {
        if (_currentEnemy == null)
        {
            return;
        }
        _currentEnemy.SetDead(true);
        _currentEnemy = null;
        _elapsedTimeNoEnemy = 0;
    }

    /// <summary>
    /// Called when a sequence's time is finished without success.
    /// </summary>
    public void OnSequenceEnded()
    {
        if (_currentEnemy == null)
        {
            return;
        }
        if (_currentEnemy.GetState() == CEnemy.STATE_ATTACK)
        {
            return;
        }
        _currentEnemy.SetState(CEnemy.STATE_ATTACK);
        //_elapsedTimeNoEnemy = 0;
    }

    private void Update()
    {
        if (GameData.IsPause)
        {
            return;
        }
        if (_currentEnemy == null)
        {
            _elapsedTimeNoEnemy += Time.deltaTime;
            if (_elapsedTimeNoEnemy > CLevelManager.Inst.GetCurrentTimeEnemySpawn())
            {
                _elapsedTimeNoEnemy = 0;
                SpawnEnemy();
                return;
            }
        }
        else
        {
            if (_currentEnemy.IsDead())
            {
                _currentEnemy = null;
                _elapsedTimeNoEnemy = 0;
            }
        }
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
