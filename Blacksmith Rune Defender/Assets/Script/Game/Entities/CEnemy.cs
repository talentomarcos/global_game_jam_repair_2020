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
    private Animator _anim;
    public CBaseStats _stats;

    public override void ApiAwake()
    {
        base.ApiAwake();

        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _anim = GetComponent<Animator>();

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
                    _anim.Play("Idle");
                }
                break;

            case STATE_ATTACK:
                {
                    CLevelManager.Inst._player.Damage(_stats._attack);
                    _anim.Play("Attack");
                }
                break;

            case STATE_DEATH:
                {
                    _anim.Play("Death");
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

