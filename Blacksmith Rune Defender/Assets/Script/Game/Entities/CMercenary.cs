using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMercenary : CGameObject
{
    public const int STATE_IDLE = 0;
    public const int STATE_ATTACK = 1;
    public const int STATE_REQUEST = 2;
    public const int STATE_WRONG_RUNE = 3;


    private SpriteRenderer _spriteRenderer;

    public override void ApiAwake()
    {
        base.ApiAwake();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();


        SetState(STATE_IDLE);
    }

    public override void ApiUpdate()
    {
        base.ApiUpdate();




#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetState(STATE_IDLE);
            return;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetState(STATE_ATTACK);
            return;

        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SetState(STATE_REQUEST);
            return;

        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SetState(STATE_WRONG_RUNE);
            return;

        }
#endif
    }

    public override void SetState(int aState)
    {
        base.SetState(aState);


        switch (aState)
        {
            case STATE_IDLE:
                {
                    _spriteRenderer.color = Color.white;
                }
                break;

            case STATE_ATTACK:
                {
                    _spriteRenderer.color = Color.red;
                }
                break;

            case STATE_REQUEST:
                {
                    _spriteRenderer.color = Color.green;
                }
                break;

            case STATE_WRONG_RUNE:
                {
                    _spriteRenderer.color = Color.magenta;
                }
                break;
        }
    }
}
