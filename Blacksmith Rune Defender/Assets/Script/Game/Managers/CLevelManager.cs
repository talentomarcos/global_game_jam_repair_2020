using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CLevelManager : CStateMachine
{
    private static CLevelManager _inst;
    public static CLevelManager Inst
    {
        get
        {
            if (_inst == null)
            {
                GameObject obj = new GameObject("LevelManager");
                return obj.AddComponent<CLevelManager>();
            }
            return _inst;
        }
    }

    public const int STATE_PLAYING = 0;
    public const int STATE_PAUSE = 1;
    public const int STATE_GAME_OVER = 2;

    public List<EnemyData> _enemies;

    public Vector2 _minTimeEnemySpawn;
    public Vector2 _maxTimeEnemySpawn;
    public Vector2 _timeEnemySpawnDecrese;

    private Vector2 _currentTimeEnemySpawn;

    public CPlayer _player;

    public GameObject _deathMenu;
    public GameObject _pauseMenu;

    private bool _pauseAxisDown = true;

    void Awake()
    {
        if (_inst != null && _inst != this)
        {
            Debug.Log("destroying " + CLevelManager.Inst.gameObject.name);
            Destroy(CLevelManager.Inst.gameObject);
            //return;
        }
        _inst = this;

        _currentTimeEnemySpawn = _maxTimeEnemySpawn;
        GameData.IsPause = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        SetState(STATE_PLAYING);
        _player._stats.SubscribeOnChangeHealth(OnPlayerChangeHealth);
    }


    // Update is called once per frame
    void Update()
    {
        UpdatePauseInput();
    }

    private void UpdatePauseInput()
    {
        if (_pauseAxisDown && Input.GetAxisRaw("Pause")>0)
        {
            TogglePause();
        }
        _pauseAxisDown = Input.GetAxisRaw("Pause") == 0;
    }

    public Vector2 GetCurrentTimeEnemySpawn()
    {
        return _currentTimeEnemySpawn;
    }


    private void OnPlayerChangeHealth(int aValue)
    {
        if (_player._stats.IsHealthZero())
        {
            SetState(STATE_GAME_OVER);
        }
    }

    public override void SetState(int aState)
    {
        base.SetState(aState);
        switch (GetState())
        {
            case STATE_PLAYING:
                _pauseMenu.SetActive(false);
                _deathMenu.SetActive(false);
                break;
            case STATE_PAUSE:
                _pauseMenu.SetActive(true);
                break;
            case STATE_GAME_OVER:
                _deathMenu.SetActive(true);
                GameData.IsPause = true;
                break;
            default:
                break;
        }
    }

    public void SetPause(bool aPause)
    {
        if (aPause == GameData.IsPause)
        {
            return;
        }

        GameData.IsPause = aPause;

        if (aPause)
        {
            SetState(STATE_PAUSE);
        }
        else
        {
            SetState(STATE_PLAYING);
        }
    }

    public void TogglePause()
    {
        SetPause(!GameData.IsPause);
    }

    public bool CanTimeBetweenSpawnsBeDecreased()
    {
        return (_currentTimeEnemySpawn - _minTimeEnemySpawn).sqrMagnitude < .01f;
    }

    public void DecreaseTimeBetweenSpawns()
    {
        _currentTimeEnemySpawn = new Vector2(Mathf.Clamp((_currentTimeEnemySpawn.x - _timeEnemySpawnDecrese.x), _minTimeEnemySpawn.x, _maxTimeEnemySpawn.x),
            Mathf.Clamp((_currentTimeEnemySpawn.y - _timeEnemySpawnDecrese.y), _minTimeEnemySpawn.y, _maxTimeEnemySpawn.y));
    }
}
