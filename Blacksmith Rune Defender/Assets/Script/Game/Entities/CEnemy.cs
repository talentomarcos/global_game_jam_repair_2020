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

        if (GetState() == STATE_SPAWN)
        {
            float delta = GetStateTime() / 1f;
            if (delta >= 1)
            {
                _spriteRenderer.color = Color.white;
                SetState(STATE_IDLE);
                return;
            }
            float value = Mathfx.Coserp(0, 1, delta);
            _spriteRenderer.color = new Color(value,value,value,value);

        }
        else if (GetState() == STATE_ATTACK)
        {
            if (GetStateTime() >= 1f)
            {
                SetState(STATE_DEATH);
            }
        }
        else if (GetState() == STATE_DEATH)
        {
            if (GetStateTime() >= .5f)
            {
                Destroy(gameObject);
            }
        }
    }

    public override void SetState(int aState)
    {
        base.SetState(aState);

        switch (aState)
        {
            case STATE_SPAWN:
                {
                    CAudioManager.Inst.PlaySFX("WerewolfSpawn", false, transform,false,1);
                    _spriteRenderer.color = Color.black.WithAlpha(0);
                }
                break;

            case STATE_IDLE:
                {
                    //_spriteRenderer.color = Color.yellow;
                }
                break;

            case STATE_ATTACK:
                {
                    _spriteRenderer.color = Color.green;
                    CLevelManager.Inst._player.Damage(_stats._attack);
                }
                break;

            case STATE_DEATH:
                {
                    _spriteRenderer.color = Color.red;
                }
                break;
        }
    }

    public override void SetDead(bool aIsDead)
    {
        base.SetDead(aIsDead);
        if (aIsDead)
        {
            CAudioManager.Inst.PlaySFX("WerewolfDeath", false, null, false, 1);
            SetState(STATE_DEATH);
        }
    }
}

