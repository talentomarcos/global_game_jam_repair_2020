using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CRuneUI : MonoBehaviour
{
    private Runes _type;
    private Image _image;

    public Sprite _energyRune;
    public Sprite _fireRune;
    public Sprite _occultRune;
    public Sprite _strengthRune;
    
    private void Awake()
    {
        _image = GetComponent<Image>();
    }

    public void SetRune(Runes aRune)
    {
        //if (aRune == _type)
        //{
        //    return;
        //}
        _type = aRune;
        switch (aRune)
        {
            case Runes.FIRE:
                _image.sprite = _fireRune;
                break;
            case Runes.STRENGTH:
                _image.sprite = _strengthRune;
                break;
            case Runes.OCCULT:
                _image.sprite = _occultRune;
                break;
            case Runes.ENERGY:
            default:
                _image.sprite = _energyRune;
                break;
        }
    }
}
