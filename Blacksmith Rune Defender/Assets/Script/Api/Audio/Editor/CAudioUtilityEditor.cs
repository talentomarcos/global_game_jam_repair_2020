using UnityEngine;
using UnityEditor;
using System.Collections;

public class CAudioUtilityEditor
{

    [MenuItem("GeckoGecko/Audio/Create Serial")]
    public static void CreateAudioSerialTopMenu()
    {
        MakeAudioSerial();
    }

    [MenuItem("Assets/GeckoGecko/Audio/Create Serial")]
    public static void CreateAudioSerialContextMenu()
    {
        MakeAudioSerial();
    }

    private static void MakeAudioSerial()
    {
        ScriptableObjectUtility.CreateAsset<AudioSerial>();
    }
}
