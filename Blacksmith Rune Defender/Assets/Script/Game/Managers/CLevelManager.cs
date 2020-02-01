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

    public float _minTimeEnemySpawn = .3f;
    public float _maxTimeEnemySpawn = 2f;

    private float _currentTimeEnemySpawn;

    public CPlayer _player;

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
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float GetCurrentTimeEnemySpawn()
    {
        return _currentTimeEnemySpawn;
    }
}
