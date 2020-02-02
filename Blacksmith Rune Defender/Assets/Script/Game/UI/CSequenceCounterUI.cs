using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CSequenceCounterUI : MonoBehaviour
{
    private TextMeshProUGUI _text;

    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        _text.text = CSequenceManager.Inst.GetCompletedSeqCounter().ToString();
    }
}
