using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CPlayerHealthUI : MonoBehaviour
{
    public Slider _slider;

    private int _maxHealth;

    // Start is called before the first frame update
    void Start()
    {
        _maxHealth = CLevelManager.Inst._player._stats.GetMaxHealth();
        CLevelManager.Inst._player._stats.SubscribeOnChangeHealth(OnChangeHealth);
        CLevelManager.Inst._player._stats.SubscribeOnChangeMaxHealth(OnChangeMaxHealth);
        _slider.value = CLevelManager.Inst._player._stats.GetHealth() / _maxHealth;
    }

    private void OnChangeMaxHealth(int aValue)
    {
        _maxHealth = aValue;
    }

    private void OnChangeHealth(int aValue)
    {
        _slider.value = aValue / (float)_maxHealth;
    }

}
