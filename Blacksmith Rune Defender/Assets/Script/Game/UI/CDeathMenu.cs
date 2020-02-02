using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CDeathMenu : CMenuAux
{
    protected override void OnEnable()
    {
        //base.OnEnable();
        EventSystem.current.SetSelectedGameObject(null);
        StartCoroutine("WaitToEnableButtons");
    }

    public IEnumerator WaitToEnableButtons()
    {
        //yield return new WaitForSeconds(0f);

        _onEnableSelect.Select();
        _onEnableSelect.OnSelect(null);
        EventSystem.current.SetSelectedGameObject(_onEnableSelect.gameObject);

        yield return null;
    }
}
