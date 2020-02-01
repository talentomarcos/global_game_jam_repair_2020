using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPlayer : CGameObject
{
    public List<float> _lanesXPos;
    private int _currentLane = 0;
    private bool _axisDown;

    private List<Runes> _currentSequence = new List<Runes>();

    private SpriteRenderer _spriteRend;

    private void Awake()
    {
        _spriteRend = GetComponentInChildren<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _lanesXPos.Sort();
        _currentLane = Mathf.FloorToInt(_lanesXPos.Count / 2f);
        SetX(_lanesXPos[_currentLane]);
    }

    // Update is called once per frame
    void Update()
    {
        updateLaneMovement();
        updateRuneInput();
    }

    private void updateLaneMovement()
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
    }

    private void updateRuneInput()
    {
    }
}
