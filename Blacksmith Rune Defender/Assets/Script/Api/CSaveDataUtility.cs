using UnityEngine;
using System.Collections;

class CSaveDataUtility
{
    private static string NETWORK_REGION = "NetworkRegion";
    private static string QUALITY_LEVEL = "QualityLevel";
    private static string VSYNC = "VSync";
    private static string FPS_DISPLAY = "FPSDisplay";
    private static string MASTER_VOLUME = "MasterVolume";
    private static string MUSIC_VOLUME = "MusicVolume";
    private static string SFX_VOLUME = "SFXVolume";
    private static string SCREENSHAKE = "Screenshake";

    public static void SaveGameOptionsData(int qualityLevel, int vsync, float masterVolume, float musicVolume, float sfxVolume, int screenshake, int aimByMoving)
    {
        PlayerPrefs.SetInt(QUALITY_LEVEL, qualityLevel);
        PlayerPrefs.SetInt(VSYNC, vsync);
        PlayerPrefs.SetFloat(MASTER_VOLUME, masterVolume);
        PlayerPrefs.SetFloat(MUSIC_VOLUME, musicVolume);
        PlayerPrefs.SetFloat(SFX_VOLUME, sfxVolume);
        PlayerPrefs.SetInt(SCREENSHAKE, screenshake);
        PlayerPrefs.Save();
        //CNotificationSystem.Inst.NotifyAutoSave();
    }

    public static void SaveGraphicSettings(int qualityLevel)
    {
        PlayerPrefs.SetInt(QUALITY_LEVEL, qualityLevel);
        PlayerPrefs.Save();
        //CNotificationSystem.Inst.NotifyAutoSave();
    }

    public static void SaveVSync(int vsync)
    {
        PlayerPrefs.SetInt(VSYNC, vsync);
        PlayerPrefs.Save();
        //CNotificationSystem.Inst.NotifyAutoSave();
    }

    public static void SaveShowFPS(int showFps)
    {
        PlayerPrefs.SetInt(FPS_DISPLAY, showFps);
        PlayerPrefs.Save();
        //CNotificationSystem.Inst.NotifyAutoSave();
    }

    public static void SaveAudioSettings(float masterVolume, float musicVolume, float sfxVolume)
    {
        PlayerPrefs.SetFloat(MASTER_VOLUME, masterVolume);
        PlayerPrefs.SetFloat(MUSIC_VOLUME, musicVolume);
        PlayerPrefs.SetFloat(SFX_VOLUME, sfxVolume);
        PlayerPrefs.Save();
        //CNotificationSystem.Inst.NotifyAutoSave();
    }

    public static void SaveGameplaySettings(int screenshake)
    {
        PlayerPrefs.SetInt(SCREENSHAKE, screenshake);
        PlayerPrefs.Save();
        //CNotificationSystem.Inst.NotifyAutoSave();
    }

    public static void SaveSelectedRegion(int regionIndex)
    {
        PlayerPrefs.SetInt(NETWORK_REGION, regionIndex);
    }

    public static int GetSavedRegion()
    {
        return PlayerPrefs.GetInt(NETWORK_REGION, 0);
    }

    public static int GetVSync()
    {
        return PlayerPrefs.GetInt(VSYNC, 0);
    }

    public static float LoadMasterVolume()
    {
        //Debug.Log(MASTER_VOLUME + " exists " + PlayerPrefs.HasKey(MASTER_VOLUME));
        return PlayerPrefs.GetFloat(MASTER_VOLUME, 1);
    }

    public static float LoadMusicVolume()
    {
        //Debug.Log(MUSIC_VOLUME + " exists " + PlayerPrefs.HasKey(MUSIC_VOLUME));
        return PlayerPrefs.GetFloat(MUSIC_VOLUME, 1);
    }

    public static float LoadSFXVolume()
    {
        //Debug.Log(SFX_VOLUME + " exists " + PlayerPrefs.HasKey(SFX_VOLUME));
        return PlayerPrefs.GetFloat(SFX_VOLUME, 1);
    }

    public static int LoadQualityLevel()
    {
        return PlayerPrefs.GetInt(QUALITY_LEVEL, 0);
    }

    public static int LoadScreenshakeMultiplier()
    {
        return PlayerPrefs.GetInt(SCREENSHAKE, 0);
    }

    public static int LoadFPSDisplayShown()
    {
        return PlayerPrefs.GetInt(FPS_DISPLAY, 1);
    }
}
