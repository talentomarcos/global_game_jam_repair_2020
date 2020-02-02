using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMercenary : CGameObject
{
    public const int STATE_IDLE = 0;
    public const int STATE_ATTACK = 1;
    public const int STATE_REQUEST = 2;
    public const int STATE_WRONG_RUNE = 3;

    private Animator _anim;

    public GameObject _explosion;

    public override void ApiAwake()
    {
        base.ApiAwake();
        _anim = GetComponent<Animator>();

        SetState(STATE_IDLE);
    }

    public override void ApiUpdate()
    {
        base.ApiUpdate();
        if (GetState() == STATE_ATTACK)
        {
            if (GetStateTime() >= .34f)
            {
                SetState(STATE_IDLE);
            }
        }
    }

    public override void SetState(int aState)
    {
        base.SetState(aState);


        switch (aState)
        {
            case STATE_IDLE:
                {
                    _anim.Play("Idle");
                }
                break;

            case STATE_ATTACK:
                {
                    _anim.Play("Attack");
                }
                break;

            case STATE_REQUEST:
                {
                    _anim.Play("Request");
                }
                break;

            case STATE_WRONG_RUNE:
                {
                }
                break;
        }
    }

    public void SetExplotionActive()
    {
        _explosion.SetActive(true);
    }
}
