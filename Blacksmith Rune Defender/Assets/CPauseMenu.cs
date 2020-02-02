using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPauseMenu : MonoBehaviour
{

    public CLevelManager _levelManager;

    public void TooglePauseMenu()
    {
        gameObject.SetActive(!gameObject.activeSelf);
        _levelManager.SetPause(gameObject.activeSelf);
    }
}
