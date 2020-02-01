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

    void Awake()
    {
        if (_inst != null && _inst != this)
        {
            Debug.Log("destroying " + CLevelManager.Inst.gameObject.name);
            Destroy(CLevelManager.Inst.gameObject);
            //return;
        }
        _inst = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
