using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CHeart : CGameObject
{
    public const int STATE_SPAWN = 0;
    public const int STATE_IDLE = 1;
    public const int STATE_PICKED_UP = 2;
    public const int STATE_DEATH = 3;

    private SpriteRenderer _spriteRenderer;
    public CBaseStats _stats;

    public GameObject _pickUpParticle;
    public Sprite _brokenHeart;

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
            _spriteRenderer.color = new Color(value, value, value, value);

        }
        else if (GetState() == STATE_PICKED_UP)
        {
            if (GetStateTime() >= 1f)
            {
                Destroy(gameObject);
            }
        }
        else if (GetState() == STATE_DEATH)
        {
            float delta = GetStateTime() / 1f;
            if (delta >= 1)
            {
                Destroy(gameObject);
                return;
            }
            float value = Mathfx.Coserp(1, 0, delta);
            _spriteRenderer.color = new Color(value, value, value, value);
        }
    }

    public override void SetState(int aState)
    {
        base.SetState(aState);

        switch (aState)
        {
            case STATE_SPAWN:
                {
                    _spriteRenderer.color = Color.black.WithAlpha(0);
                }
                break;

            case STATE_IDLE:
                {
                }
                break;

            case STATE_PICKED_UP:
                {
                    CLevelManager.Inst._player._stats.IncreaseHealth(_stats._attack);
                    Instantiate(_pickUpParticle, GetPos(), Quaternion.identity);
                }
                break;

            case STATE_DEATH:
                {
                    _spriteRenderer.sprite = _brokenHeart;
                }
                break;
        }
    }

    public override void SetDead(bool aIsDead)
    {
        base.SetDead(aIsDead);
        if (aIsDead)
        {
            SetState(STATE_DEATH);
        }
    }
}
