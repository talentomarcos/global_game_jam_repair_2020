using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CPlayer : CGameObject
{
    public List<float> _lanesXPos;
    private int _currentLane = 0;
    private bool _axisDown = true;
    private bool _axisDownSend = true;

    private List<Runes> _currentSequence = new List<Runes>();

    private SpriteRenderer _spriteRend;

    private bool _axisDownFire = true;
    private bool _axisDownOccult = true;
    private bool _axisDownStrength = true;
    private bool _axisDownEnergy = true;
    //private float _movement;

    public CBaseStats _stats;

    public CRuneContainerUI _ui;

    public override void ApiAwake()
    {
        base.ApiAwake();
        _spriteRend = GetComponentInChildren<SpriteRenderer>();
        _stats.SetHealth(_stats.GetMaxHealth());
    }

    // Start is called before the first frame update
    void Start()
    {
        _lanesXPos.Sort();
        GameData.LaneAmount = _lanesXPos.Count;
        _currentLane = Mathf.FloorToInt(_lanesXPos.Count / 2f);
        SetX(_lanesXPos[_currentLane]);
        _ui.SetSequence(_currentSequence);
    }

    public override void ApiUpdate()
    {
        base.ApiUpdate();
        UpdateLaneInput();
        UpdateRuneInput();
    }

    private void UpdateLaneInput()
    {
        float xInput = Input.GetAxisRaw("Horizontal");
        // Right
        if (_axisDown && xInput > 0)
        {
            if (_currentLane + 1 < _lanesXPos.Count)
            {
                _currentLane++;
                SetX(_lanesXPos[_currentLane]);
            }
        }
        // Left
        else if (_axisDown && xInput < 0)
        {
            if (_currentLane - 1 >= 0)
            {
                _currentLane--;
                SetX(_lanesXPos[_currentLane]);
            }
        }
        _axisDown = xInput == 0;

        if (_axisDownSend && Input.GetAxisRaw("SendSequence") > 0)
        {
            if (CSequenceManager.Inst.CheckSequence(_currentLane,_currentSequence))
            {
                // If the sequence is right
                CSequenceManager.Inst.CompleteSequence(_currentLane);
            }
            _currentSequence = new List<Runes>();
            _ui.SetSequence(_currentSequence);
        }
        _axisDownSend = Input.GetAxisRaw("SendSequence") == 0;
    }

    public void AddRune(Runes aRune)
    {
        Debug.Log(aRune);
        _currentSequence.Add(aRune);
        if (_currentSequence.Count > CSequenceManager.Inst.GetCurrentRuneCount())
        {
            _currentSequence = new List<Runes>();
        }
        _ui.SetSequence(_currentSequence);
    }

    private void UpdateRuneInput()
    {
        if (_axisDownFire && Input.GetAxisRaw("FireRune") > 0)
        {
            AddRune(Runes.FIRE);
        }
        if (_axisDownOccult && Input.GetAxisRaw("OccultRune") > 0)
        {
            AddRune(Runes.OCCULT);
        }
        if (_axisDownStrength && Input.GetAxisRaw("StrengthRune") > 0)
        {
            AddRune(Runes.STRENGTH);
        }
        if (_axisDownEnergy && Input.GetAxisRaw("EnergyRune") > 0)
        {
            AddRune(Runes.ENERGY);
        }
        _axisDownFire = Input.GetAxisRaw("FireRune") == 0; 
        _axisDownOccult = Input.GetAxisRaw("OccultRune") == 0;
        _axisDownStrength = Input.GetAxisRaw("StrengthRune") == 0;
        _axisDownEnergy = Input.GetAxisRaw("EnergyRune") == 0;
    }

    public void Damage(int aDamage)
    {
        _stats.LowerHealth(aDamage);
        // To Do damage feedback here.
    }
}
