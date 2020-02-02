using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CMenuAux : MonoBehaviour
{
    public GameObject _credits;
    public GameObject _menu;

    public Button _onEnableSelect;
    public Button _onDisableSelect;
    public bool _playMusic = false;

    private void Start()
    {
        if(_playMusic)
            CAudioManager.Inst.PlayMusic("MenuMusic");
    }

    public void GoToScene(int aScene)
    {
        SceneManager.LoadScene(aScene);
    }
    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
    public void ToogleCredits()
    {
        _credits.SetActive(!_credits.activeSelf);
    }

    public void ToogleMenu()
    {
        _menu.SetActive(!_menu.activeSelf);
    }

    public void PlayButtonSound()
    {
        CAudioManager.Inst.PlaySFX("Button", false, null, false,.1f);
    }

    protected virtual void OnEnable()
    {
        _onEnableSelect.Select();
        _onEnableSelect.OnSelect(null);
        EventSystem.current.SetSelectedGameObject(_onEnableSelect.gameObject);
    }

    private void OnDisable()
    {
        if (_onDisableSelect != null)
        {
            EventSystem.current.SetSelectedGameObject(_onDisableSelect.gameObject);
            _onDisableSelect.Select();
        }
    }
}
