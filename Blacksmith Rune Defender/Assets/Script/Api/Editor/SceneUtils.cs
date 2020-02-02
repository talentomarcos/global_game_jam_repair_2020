using UnityEngine;
using UnityEditor;
using System.Collections;
using UnityEditor.SceneManagement;

public class SceneUtils : Editor
{

    [MenuItem("Scenes/Open Main Menu")]
    private static void OpenMainMenu()
    {
        EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
        EditorSceneManager.OpenScene("Assets/_Scenes/Main_Menu.unity");
    }

    [MenuItem("Scenes/Open Game Scene")]
    private static void OpenGameScene()
    {
        EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
        EditorSceneManager.OpenScene("Assets/_Scenes/Level_Scene.unity");
    }

    [MenuItem("Scenes/Open Boss Scene")]
    private static void OpenBossScene()
    {
        EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
        EditorSceneManager.OpenScene("Assets/_Scenes/BossScene.unity");
    }

    [MenuItem("Scenes/Open Room Editor")]
    private static void OpenRoomEditor()
    {
        EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
        EditorSceneManager.OpenScene("Assets/_Scenes/RoomEditor.unity");
    }

}
