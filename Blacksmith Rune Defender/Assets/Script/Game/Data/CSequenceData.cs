using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CSequenceData
{
    public float waitTime = 7f;
    public List<Runes> sequence = new List<Runes>();

    private float _elapsedTime;

    public CRuneContainerUI ui;

    public CSequenceData(float aWaitTime, List<Runes> aRunes)
    {
        waitTime = aWaitTime;
        sequence = aRunes;
    }

    public int GetRuneAmount()
    {
        return sequence.Count;
    }

    public void Update()
    {
        _elapsedTime += Time.deltaTime;
        if (ui != null)
        {
            ui.SetClockValue(_elapsedTime / waitTime);
        }
    }

    public bool HasEnded()
    {
        return _elapsedTime >= waitTime;
    }

    public Runes GetRuneAt(int aIndex)
    {
        return sequence[aIndex];
    }
}
