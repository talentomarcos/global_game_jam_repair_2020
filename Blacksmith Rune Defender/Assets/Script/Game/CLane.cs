using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CLane : MonoBehaviour
{
    public int _laneIndex;
    public CMercenary _mercenary;
    public Transform _enemySpawnPonint;
    private CEnemy _currentEnemy;
    private CHeart _currentHeart;

    private float _elapsedTimeNoEnemy = 0;
    private float _timeToWaitBetweenEnemies;

    public CRuneContainerUI _ui;

    public GameObject _anvil;

    private void Start()
    {
        _elapsedTimeNoEnemy = CMath.RandomFloatBetween(0, CMath.RandomFloatBetween(CLevelManager.Inst.GetCurrentTimeEnemySpawn().x, CLevelManager.Inst.GetCurrentTimeEnemySpawn().y) + 1);
        _timeToWaitBetweenEnemies = CLevelManager.Inst.GetCurrentTimeEnemySpawn().y+1;
        _ui.SetSequence(new List<Runes>());
    }

    /// <summary>
    /// Called when the player succesfully completes a sequence.
    /// </summary>
    public void OnSequenceComplete()
    {
        if (_currentEnemy == null && _currentHeart == null)
        {
            return;
        }
        if (_currentHeart != null)
        {
            _currentHeart.SetState(CHeart.STATE_PICKED_UP);
            _currentHeart = null;
        }
        else
        {
            _currentEnemy.SetDead(true);
            _currentEnemy = null;
        }
        _elapsedTimeNoEnemy = 0;
        _timeToWaitBetweenEnemies = CMath.RandomFloatBetween(CLevelManager.Inst.GetCurrentTimeEnemySpawn().x, CLevelManager.Inst.GetCurrentTimeEnemySpawn().y);
        _ui.SetSequence(new List<Runes>());
        _ui.SetClockValue(0);
        _mercenary.SetState(CMercenary.STATE_ATTACK);
    }

    /// <summary>
    /// Called when a sequence's time is finished without success.
    /// </summary>
    public void OnSequenceEnded()
    {
        if (_currentEnemy == null && _currentHeart == null)
        {
            return;
        }
        if (_currentHeart != null)
        {
            _currentHeart.SetState(CHeart.STATE_DEATH);
            _currentHeart = null;
        }
        else
        {
            if (_currentEnemy.GetState() == CEnemy.STATE_ATTACK)
            {
                return;
            }
            _currentEnemy.SetState(CEnemy.STATE_ATTACK);
        }
        _ui.SetSequence(new List<Runes>());
        _elapsedTimeNoEnemy = 0;
        _ui.SetClockValue(0);
        _timeToWaitBetweenEnemies = CMath.RandomFloatBetween(CLevelManager.Inst.GetCurrentTimeEnemySpawn().x, CLevelManager.Inst.GetCurrentTimeEnemySpawn().y);
        _mercenary.SetState(CMercenary.STATE_IDLE);
    }

    private void Update()
    {
        if (GameData.IsPause)
        {
            return;
        }
        if (_currentEnemy == null && _currentHeart == null)
        {
            _elapsedTimeNoEnemy += Time.deltaTime;
            if (_elapsedTimeNoEnemy > _timeToWaitBetweenEnemies)
            {
                _elapsedTimeNoEnemy = 0;
                _ui.SetClockValue(0);
                _timeToWaitBetweenEnemies = CMath.RandomFloatBetween(CLevelManager.Inst.GetCurrentTimeEnemySpawn().x, CLevelManager.Inst.GetCurrentTimeEnemySpawn().y);
                SpawnEnemy();
                return;
            }
        }
        else
        {
            if (_currentEnemy != null && _currentEnemy.IsDead())
            {
                _currentEnemy = null;
                _elapsedTimeNoEnemy = 0;
                _ui.SetClockValue(0);
                _timeToWaitBetweenEnemies = CMath.RandomFloatBetween(CLevelManager.Inst.GetCurrentTimeEnemySpawn().x, CLevelManager.Inst.GetCurrentTimeEnemySpawn().y);
            }
            if (_currentHeart != null &&  _currentHeart.IsDead())
            {
                _currentHeart = null;
                _elapsedTimeNoEnemy = 0;
                _ui.SetClockValue(0);
                _timeToWaitBetweenEnemies = CMath.RandomFloatBetween(CLevelManager.Inst.GetCurrentTimeEnemySpawn().x, CLevelManager.Inst.GetCurrentTimeEnemySpawn().y);
            }
        }
    }

    public void SpawnEnemy()
    {
        float randSpawn = Random.value;
        if (randSpawn < .02f)
        {
            GameObject heart = Instantiate(CLevelManager.Inst._heart, transform);
            _currentHeart = heart.GetComponent<CHeart>();
            heart.transform.position = _enemySpawnPonint.position;
        }
        else
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
        }
        CSequenceData data = CSequenceManager.Inst.RequestSequence(_laneIndex);
        data.ui = _ui;
        _ui.SetSequence(data.sequence);
        _mercenary.SetState(CMercenary.STATE_REQUEST);
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
        _currentEnemy._attackAnim._initialPos = _enemySpawnPonint.localPosition;
        _currentEnemy._attackAnim._endPos = _anvil.transform.localPosition;
    }

    public void SetAnvilVisible(bool aVisible)
    {
        _anvil.SetActive(aVisible);
    }
}
