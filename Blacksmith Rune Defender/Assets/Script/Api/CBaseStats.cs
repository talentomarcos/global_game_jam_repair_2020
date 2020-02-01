using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CBaseStats
{
    public float _movementSpeed;
    protected int _health;
    public int _maxHealth = 1;
    public float _knockBack = 8;

    public delegate void IntDelegate(int aValue);

    protected GenericDelegate OnHeal;

    protected IntDelegate OnChangeHealth;
    public void SubscribeOnChangeHealth(IntDelegate action)
    {
        UnSubscribeOnChangeHealth(action);
        OnChangeHealth += action;
    }
    public void UnSubscribeOnChangeHealth(IntDelegate action)
    {
        OnChangeHealth -= action;
    }

    public void SubscribeOnHeal(GenericDelegate action)
    {
        UnSubscribeOnHeal(action);
        OnHeal += action;
    }
    public void UnSubscribeOnHeal(GenericDelegate action)
    {
        OnHeal -= action;
    }

    protected IntDelegate OnChangeMaxHealth;
    public void SubscribeOnChangeMaxHealth(IntDelegate action)
    {
        UnSubscribeOnChangeMaxHealth(action);
        OnChangeMaxHealth += action;
    }
    public void UnSubscribeOnChangeMaxHealth(IntDelegate action)
    {
        OnChangeMaxHealth -= action;
    }

    public void SetHealth(int aHealth)
    {
        if (aHealth > _health)
        {
            if (OnHeal != null)
                OnHeal();
        }
        if (aHealth < 0)
        {
            _health = 0;
        }
        else if (aHealth > _maxHealth)
        {
            _health = _maxHealth;
        }
        else
        {
            _health = aHealth;
        }
        if (OnChangeHealth != null)
        {
            OnChangeHealth(_health);
        }
    }

    public void LowerHealth(int aDamage)
    {
        if (_health < aDamage)
        {
            _health = 0;
        }
        else
        {
            _health -= aDamage;
        }
        if (OnChangeHealth != null)
        {
            OnChangeHealth(_health);
        }
    }

    public bool IncreaseHealth(int aBoost)
    {
        if (_health == _maxHealth)
            return false;
        if (OnHeal != null)
            OnHeal();
        _health += aBoost;
        if (_health > _maxHealth)
        {
            _health = _maxHealth;
        }
        if (OnChangeHealth != null)
        {
            OnChangeHealth(_health);
        }
        return true;
    }

    public void MaxHealthStat()
    {
        _health = _maxHealth;
    }

    public void BoostMaxHealth(int aBoost)
    {
        _maxHealth += aBoost;

        if (OnChangeMaxHealth != null)
        {
            OnChangeMaxHealth(_maxHealth);
        }
    }

    public int GetHealth()
    {
        return _health;
    }

    public int GetMaxHealth()
    {
        return _maxHealth;
    }

    public bool IsHealthZero()
    {
        return _health == 0;
    }

    public bool IsHealthFull()
    {
        return _health == _maxHealth;
    }

    public virtual float GetMovementSpeed()
    {
        return _movementSpeed /*/ GameData.SPEED_MULTIP*/;
    }
}
