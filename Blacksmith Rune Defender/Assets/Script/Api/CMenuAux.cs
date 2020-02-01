using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CMenuAux : MonoBehaviour
{
    public GameObject _credits;

    private void Start()
    {
        //CAudioManager.Inst.PlayMusic("Music");
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
}
