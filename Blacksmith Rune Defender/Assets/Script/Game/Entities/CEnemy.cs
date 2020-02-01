using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEnemy : CGameObject
{
    public const int STATE_SPAWN = 0;
    public const int STATE_IDLE = 1;
    public const int STATE_ATTACK = 2;
    public const int STATE_DEATH = 3;

    private SpriteRenderer _spriteRenderer;
    public CBaseStats _stats;

    public override void ApiAwake()
    {
        base.ApiAwake();

        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        SetState(STATE_SPAWN);
        return;
    }


    public override void ApiUpdate()
    {
        base.ApiUpdate();


#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SetState(STATE_SPAWN);
            return;
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            SetState(STATE_IDLE);
            return;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            SetState(STATE_ATTACK);
            return;
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            SetState(STATE_DEATH);
            return;

        }
#endif
    }

    public override void SetState(int aState)
    {
        base.SetState(aState);

        switch (aState)
        {
            case STATE_SPAWN:
                {
                    _spriteRenderer.color = Color.magenta;
                }
                break;

            case STATE_IDLE:
                {
                    _spriteRenderer.color = Color.yellow;
                }
                break;

            case STATE_ATTACK:
                {
                    _spriteRenderer.color = Color.green;
                }
                break;

            case STATE_DEATH:
                {
                    _spriteRenderer.color = Color.red;
                }
                break;
        }
    }
}

