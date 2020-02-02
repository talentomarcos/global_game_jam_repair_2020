using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CRuneContainerUI : MonoBehaviour
{
    public List<CRuneUI> _runes;
    public Slider _clock;

    public void SetSequence(List<Runes> aSequence)
    {
        for (int i = 0; i < aSequence.Count; i++)
        {
            _runes[i].gameObject.SetActive(true);
            _runes[i].SetRune(aSequence[i]);
        }
        for (int i = aSequence.Count; i < _runes.Count; i++)
        {
            _runes[i].gameObject.SetActive(false);
        }
    }

    public void SetClockValue(float aValue)
    {
        _clock.value = aValue;
    }
}
