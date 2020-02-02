using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;

public class CAudioManager : MonoBehaviour
{

    #region SINGLETON
    private static CAudioManager _inst;
    public static CAudioManager Inst
    {
        get
        {
            if (_inst == null)
            {
                _inst = FindObjectOfType(typeof(CAudioManager)) as CAudioManager;
                if (_inst == null)
                {
                    GameObject obj = new GameObject("AudioManager");
                    _inst = obj.AddComponent<CAudioManager>();
                }
            }
            return _inst;
        }
    }
    #endregion

    public float maxHearingDistance = 30f; //max distance in meters in which the sound dies off.

    #region PRIVATE VARIABLES

    [System.Serializable]
    public class AudioDetails
    {
        public AudioSource Source;
        public AudioSerial Serial;
    }

    public enum MusicMixerGroup
    {
        DEFAULT_LOW,
        DEFAULT_HIGH,
        PAUSE
    }

    private Dictionary<string, AudioSerial> musicList;
    private Dictionary<string, AudioSerial> sfxList;
    private AudioDetails activeMusicAudioSource;
    private AudioDetails activeSecondaryMusicAudioSource;
    private AudioDetails pauseMusicSource;
    private List<AudioDetails> activeSFXAudioSources;
    private string activeMusicAudioHash;
    //private string activeSecondaryMusicAudioHash;
    
    private bool sfxPaused = false;
    #endregion

    #region VOLUMES
    [SerializeField]
    private float musicVolume = 1f;
    [SerializeField]
    private float sfxVolume = 1f;

    [SerializeField]
    private float _pauseModifier = .5f;

    //private float _currentMusicSourceVolume;
    #endregion


    public float _fadeTime = .3f;
    public AudioMixerGroup _musicMixerGroup;
    //public AudioMixerGroup _musicSecondaryMixerGroup;
    public AudioMixerGroup _sfxMixerGroup;
    public AudioMixerGroup _pauseMixerGroup;

    //private AudioMixerSnapshot _currentSnapshot;
    private AudioDetails _currentMusicAudio;
    //public AudioMixerSnapshot _mainSnap;
    //public AudioMixerSnapshot _secondarySnap;
    //public AudioMixerSnapshot _pauseSnapshot;
    //public float _toSecondaryTransitionTime = 0;
    //public float _toMainTransitionTime = 1;
    //public float _toSlomoTime = .2f;

    public delegate void LogAudioDelegate(string audioName, int type, bool loops, float timeStamp);
    private LogAudioDelegate OnLogAudio;
    private bool _isLoggingAudio = false;

    private Coroutine _fadeRoutine;

    public void SubscribeOnLogAudio(LogAudioDelegate action)
    {
        OnLogAudio += action;
    }

    public void UnSubscribeOnLogAudio(LogAudioDelegate action)
    {
        OnLogAudio -= action;
    }

    public void EnableAudioLogging()
    {
        _isLoggingAudio = true;
    }

    public void DisableAudioLogging()
    {
        _isLoggingAudio = false;
    }

    private void Awake()
    {
        if (_inst != null && _inst != this)
        {
            Destroy(this.gameObject);
            return;
        }
        _inst = this;
        DontDestroyOnLoad(gameObject);

        //creates music audio source
        BuildMusicSource();
        activeSFXAudioSources = new List<AudioDetails>();

        //creates music&sfx audio reference sheet
        musicList = new Dictionary<string, AudioSerial>();
        sfxList = new Dictionary<string, AudioSerial>();

        AudioSerial[] assetList = Resources.LoadAll<AudioSerial>("Audio/");

        for (int i = 0; i < assetList.Length; i++)
        {
            if (sfxList.ContainsKey(assetList[i].name))
                Debug.LogError(assetList[i].name + " is on the list");
            if (assetList[i]._type == AudioType.MUSIC)
            {
                musicList.Add(assetList[i].name, assetList[i]);
            }
            else if (assetList[i]._type == AudioType.SFX && assetList[i]._enabled)
                sfxList.Add(assetList[i].name, assetList[i]);
        }

        BuildMusicSource();
        BuildSecondaryMusicSource();
        BuildPauseMusicSource();

        //_currentSnapshot = _mainSnap;
        activeSecondaryMusicAudioSource.Source.volume = 0;
        pauseMusicSource.Source.volume = 0;

        UpdateVolumes();
        //SetMasterVolume(CSaveDataUtility.LoadMasterVolume());
        //SetMusicVolume(CSaveDataUtility.LoadMusicVolume());
        //SetSFXVolume(CSaveDataUtility.LoadSFXVolume());
    }

    public static bool Exists()
    {
        return _inst != null;
    }

    public void UpdateVolumes()
    {
        musicVolume = CSaveDataUtility.LoadMusicVolume();
        sfxVolume = CSaveDataUtility.LoadSFXVolume();
        Debug.Log("Loaded sfx volume: " + sfxVolume);
        Debug.Log("Loaded music volume: " + musicVolume);
        UpdateMusicVolume();
        UpdateSFXVolume();
        //if(_currentMusicSourceVolume)
    }

    private void BuildMusicSource()
    {
        if (activeMusicAudioSource != null && activeMusicAudioSource.Source != null)
            return;
        GameObject source = new GameObject("MusicSource"); //create new object
        AudioListener listener = GameObject.FindObjectOfType<AudioListener>() as AudioListener; //gets listener

        activeMusicAudioSource = new AudioDetails();
        //source.transform.parent = listener.gameObject.transform; //set the audiolistener object as parent
        source.transform.localPosition = Vector3.zero; //set position to middle of object
        activeMusicAudioSource.Source = source.AddComponent<AudioSource>(); //adds an audiosource & saves reference
        activeMusicAudioSource.Source.outputAudioMixerGroup = _musicMixerGroup;
        DontDestroyOnLoad(source);
        //SetMusicVolume(musicVolume);
    }

    private void BuildSecondaryMusicSource()
    {
        GameObject source = new GameObject("SecondaryMusicSource"); //create new object
        AudioListener listener = GameObject.FindObjectOfType<AudioListener>() as AudioListener; //gets listener

        activeSecondaryMusicAudioSource = new AudioDetails();
        //source.transform.parent = listener.gameObject.transform; //set the audiolistener object as parent
        source.transform.localPosition = Vector3.zero; //set position to middle of object
        activeSecondaryMusicAudioSource.Source = source.AddComponent<AudioSource>(); //adds an audiosource & saves reference
        //activeSecondaryMusicAudioSource.Source.outputAudioMixerGroup = _musicSecondaryMixerGroup;
        activeSecondaryMusicAudioSource.Source.outputAudioMixerGroup = _musicMixerGroup;
        DontDestroyOnLoad(source);
        //SetMusicVolume(musicVolume);
    }

    private void BuildPauseMusicSource()
    {
        GameObject source = new GameObject("PauseMusicSource"); //create new object
        AudioListener listener = GameObject.FindObjectOfType<AudioListener>() as AudioListener; //gets listener

        pauseMusicSource = new AudioDetails();
        //source.transform.parent = listener.gameObject.transform; //set the audiolistener object as parent
        source.transform.localPosition = Vector3.zero; //set position to middle of object
        pauseMusicSource.Source = source.AddComponent<AudioSource>(); //adds an audiosource & saves reference
        //pauseMusicSource.Source.outputAudioMixerGroup = _pauseMixerGroup;
        pauseMusicSource.Source.outputAudioMixerGroup = _musicMixerGroup;
        DontDestroyOnLoad(source);
        //SetMusicVolume(musicVolume);
    }

    void LateUpdate()
    {
        //Debug.Log(activeSFXAudioSources);
        for (int i = activeSFXAudioSources.Count - 1; i >= 0; i--)
        {
            AudioSource auxSource = activeSFXAudioSources[i].Source;
            if (auxSource == null || (!auxSource.loop && !auxSource.isPlaying)) //sfx has ended and not looping? or was deleted?
            {
                if (auxSource != null)
                {
                    auxSource.Stop();
                    Destroy(auxSource.gameObject); //TODO: once a pool is made, remove gameobject instead of destroying
                }
                activeSFXAudioSources.RemoveAt(i);
            }
        }
    }

    public bool IsMusicPlaying(string hash)
    {
        return activeMusicAudioHash == hash;
    }

    public void TransitionToHigh()
    {
        Debug.Log("transition to high");
        CrossFade(_currentMusicAudio, activeSecondaryMusicAudioSource, _fadeTime);
        _currentMusicAudio = activeSecondaryMusicAudioSource;
    }

    public void TransitionToLow()
    {
        Debug.Log("transition to low");

        if (_currentMusicAudio == activeMusicAudioSource)
        {
            return;
        }
        CrossFade(_currentMusicAudio, activeMusicAudioSource, _fadeTime);
        _currentMusicAudio = activeMusicAudioSource;
    }

    public void TransitionToPause()
    {
        Debug.Log("transition to pause");

        StartCoroutine(CrossFadeTo(_currentMusicAudio, pauseMusicSource, _fadeTime));
        _currentMusicAudio = pauseMusicSource;
    }

    public void PlayMusic(string hash, bool loop = true, MusicMixerGroup group = MusicMixerGroup.DEFAULT_LOW, bool fadeToSnapshot = false, Vector3 position = default(Vector3))
    {
        Debug.Log("Playing song " + hash);
        if (hash == null || !musicList.ContainsKey(hash))
            return;        

        AudioDetails musicDetail = activeMusicAudioSource;
        switch (group)
        {
            case MusicMixerGroup.DEFAULT_LOW:
                Debug.Log("set as low");
                musicDetail = activeMusicAudioSource;
                break;
            case MusicMixerGroup.DEFAULT_HIGH:
                Debug.Log("set as high");
                musicDetail = activeSecondaryMusicAudioSource;
                break;
            case MusicMixerGroup.PAUSE:
                Debug.Log("set as pause");
                musicDetail = pauseMusicSource;
                break;
        }

        if (musicDetail.Source.clip == null || musicDetail.Source.clip != musicList[hash].source)
        {

            Debug.Log("music source set to " + musicDetail.Source.name);
            //get music object, set the audio clip to it and save reference
            AudioSerial audioToPlay = musicList[hash];
            musicDetail.Source.Stop();
            musicDetail.Source.clip = audioToPlay.source;
            musicDetail.Source.priority = 0;
            musicDetail.Source.volume = musicVolume * audioToPlay._relativeVolume;
            musicDetail.Source.loop = loop;
            musicDetail.Source.maxDistance = maxHearingDistance;
            musicDetail.Source.Play();
            musicDetail.Serial = musicList[hash];
            activeMusicAudioHash = hash;
        }

        
        if (_currentMusicAudio != null && fadeToSnapshot && _currentMusicAudio.Source.clip != musicDetail.Source.clip)
            CrossFade(_currentMusicAudio, musicDetail, _fadeTime);

        _currentMusicAudio = musicDetail;


        //_currentMusicSourceVolume = musicVolume * audioToPlay._relativeVolume;
        if (_isLoggingAudio)
        {
            if (OnLogAudio != null)
                OnLogAudio(hash, 0, loop, Time.time);
        }
    }

    /// <summary>
    /// Fades primary theme with secondary theme
    /// </summary>
    /// <param name="hash"></param>
    //public void PlaySecondaryMusic(string hash, bool loop = true, bool startsMuted = false, Vector3 position = default(Vector3))
    //{
    //    //if (activeSecondaryMusicAudioSource.Source == null)
    //    //    BuildSecondaryMusicSource();

    //    //if (activeSecondaryMusicAudioSource.clip == musicList[hash])
    //    //    return;

    //    //get music object, set the audio clip to it and save reference
    //    AudioSerial audioToPlay = musicList[hash];
    //    activeSecondaryMusicAudioSource.Source.Stop();
    //    activeSecondaryMusicAudioSource.Source.clip = audioToPlay.source;
    //    activeSecondaryMusicAudioSource.Source.priority = 0;
    //    activeSecondaryMusicAudioSource.Source.volume = startsMuted ? 0 : musicVolume * audioToPlay._relativeVolume;
    //    activeSecondaryMusicAudioSource.Source.loop = loop;
    //    activeSecondaryMusicAudioSource.Source.maxDistance = maxHearingDistance;
    //    activeSecondaryMusicAudioSource.Source.Play();
    //    //Debug.Log("Playing secondary");
    //    activeSecondaryMusicAudioHash = hash;

    //    activeSecondaryMusicAudioSource.Serial = audioToPlay;
    //    _currentMusicAudio = activeSecondaryMusicAudioSource;
    //    if (_isLoggingAudio)
    //    {
    //        if (OnLogAudio != null)
    //            OnLogAudio(hash, 2, loop, Time.time);
    //    }

    //    //_secondarySnap.TransitionTo(_toSecondaryTransitionTime);
    //}

    //public void TransitionToMainSnapshot()
    //{
    //    _mainSnap.TransitionTo(_toMainTransitionTime);
    //    //StopCoroutine("StopSecondaryMusicIn");
    //    //StartCoroutine("StopSecondaryMusicIn", _toMainTransitionTime);
    //}

    //public void TransitionToSlomoSnapshot()
    //{
    //    _slowMotionSnapShot.TransitionTo(_toSlomoTime);
    //}

    //IEnumerator StopSecondaryMusicIn(float time)
    //{
    //    yield return new WaitForSeconds(time);
    //    if (activeSecondaryMusicAudioSource.Source != null)
    //    {
    //        activeSecondaryMusicAudioSource.Source.Stop();
    //        Debug.Log("SEcondary music stopped");
    //    }

    //}

    public AudioSource PlaySFX(string hash, bool loop, Transform parent, bool singleInstance = false, float addedRandomPitch = 0, float addedPitch = 0)
    {
        if (hash == "")
            return null;

        if (singleInstance)
            StopSFX(hash, true);

        if (!sfxList.ContainsKey(hash))
            return null;

        AudioDetails audioDetail = new AudioDetails();

        //get new audiosource object, set the clip and properties and save reference
        AudioSerial audioToPlay = sfxList[hash];

        GameObject sfxObj = new GameObject(hash); //TODO: once a pool is made, get gameobject from pool instead of creating new
        sfxObj.transform.parent = parent;
        sfxObj.transform.localPosition = Vector3.zero;
        AudioSource source = sfxObj.AddComponent<AudioSource>();
        source.GetComponent<AudioSource>().clip = audioToPlay.source;
        source.volume = audioToPlay._relativeVolume * sfxVolume;
        source.loop = loop;
        source.minDistance = maxHearingDistance;
        source.maxDistance = maxHearingDistance;
        source.pitch += (Random.value - 0.5f) * 2 * addedRandomPitch + addedPitch;
        source.dopplerLevel = 0f; //sets doppler to 0 to cancel pitch distortions on movement
        source.outputAudioMixerGroup = _sfxMixerGroup;
        DontDestroyOnLoad(source.gameObject);
        source.Play();

        if (_isLoggingAudio)
        {
            if (OnLogAudio != null)
                OnLogAudio(hash, 1, loop, Time.time);
        }

        audioDetail.Source = source;
        audioDetail.Serial = audioToPlay;
        activeSFXAudioSources.Add(audioDetail);
        return source;
    }

    public float GetMasterVolume()
    {
        return AudioListener.volume;
    }

    public float GetPauseModifier()
    {
        return _pauseModifier;
    }

    public float GetMusicVolume()
    {
        return musicVolume;
    }

    public float GetSFXVolume()
    {
        return sfxVolume;
    }

    public void SetPauseVolume(bool on)
    {
        _pauseModifier = on ? .2f : 1;
        UpdateMusicVolume();
    }

    public void SetMasterVolume(float aVolume)
    {
        AudioListener.volume = aVolume;
    }

    public void SetMusicVolume(float aVolume)
    {
        musicVolume = aVolume;
        UpdateMusicVolume();
    }

    public void SetSFXVolume(float aVolume)
    {
        sfxVolume = aVolume;
        UpdateSFXVolume();
    }

    private void UpdateMusicVolume()
    {
        if (_currentMusicAudio == null)
            return;
        _currentMusicAudio.Source.volume = musicVolume * _currentMusicAudio.Serial._relativeVolume * _pauseModifier;
        //if (activeMusicAudioSource.Source != null && activeMusicAudioSource.Serial != null)
        //{
        //    Debug.Log(activeMusicAudioSource + " " + activeMusicAudioSource.Serial);
        //    activeMusicAudioSource.Source.volume = musicVolume * activeMusicAudioSource.Serial._relativeVolume * _pauseModifier;
        //}
        //if (activeSecondaryMusicAudioSource.Source != null && activeSecondaryMusicAudioSource.Serial != null)
        //    activeSecondaryMusicAudioSource.Source.volume = musicVolume * activeMusicAudioSource.Serial._relativeVolume * _pauseModifier;
    }

    private void UpdateSFXVolume()
    {
        for (int i = 0; i < activeSFXAudioSources.Count; i++)
        {
            activeSFXAudioSources[i].Source.volume = sfxVolume * activeSFXAudioSources[i].Serial._relativeVolume;
        }
    }

    public void TogglePauseAll()
    {
        TogglePauseMusic();
        TogglePauseSFX();
    }

    public void TogglePauseMusic()
    {
        if (activeMusicAudioSource.Source.isPlaying)
        {
            activeMusicAudioSource.Source.Pause();
        }
        else
        {
            activeMusicAudioSource.Source.Play();
        }

    }

    public void TogglePauseSFX()
    {
        for (int i = 0; i < activeSFXAudioSources.Count; i++)
        {
            if (sfxPaused)
            {
                activeSFXAudioSources[i].Source.Play();
            }
            else
            {
                activeSFXAudioSources[i].Source.Pause();
            }
        }

        sfxPaused = !sfxPaused;
    }

    public void ResetCurrentMusic()
    {
        activeMusicAudioSource.Source.time = 0f;
    }

    public void StopAll()
    {
        StopMusic();
        StopSFX();
    }

    public void StopMusic()
    {
        activeMusicAudioSource.Source.Stop();
    }

    public void StopSFX()
    {
        for (int i = 0; i < activeSFXAudioSources.Count; i++)
        {
            activeSFXAudioSources[i].Source.Stop();
        }
    }

    public void StopSFX(string name, bool all = false)
    {
        for (int i = activeSFXAudioSources.Count - 1; i >= 0; i--)
        {
            if (activeSFXAudioSources[i].Source == null)
                continue;
            if (activeSFXAudioSources[i].Source.gameObject.name == name)
            {
                activeSFXAudioSources[i].Source.Stop();
                Destroy(activeSFXAudioSources[i].Source.gameObject);
                activeSFXAudioSources.RemoveAt(i);
                if (!all)
                    break;
            }

        }
    }

    public void StopSFX(AudioSource source)
    {
        for (int i = 0; i < activeSFXAudioSources.Count; i++)
        {
            if (activeSFXAudioSources[i].Source.GetInstanceID() == source.GetInstanceID())
            {
                Destroy(source.gameObject);
                activeSFXAudioSources.RemoveAt(i);
                return;
            }
        }
    }

    private void CrossFade(AudioDetails source, AudioDetails destination, float duration)
    {
        if (_fadeRoutine != null)
            StopCoroutine(_fadeRoutine);
        _fadeRoutine = StartCoroutine(CrossFadeTo(source, destination, duration));
    }

    IEnumerator CrossFadeTo(AudioDetails source, AudioDetails destination, float duration)
    {
        //Debug.Log("fading to " + destination.Source.clip.name);
        var timer = Time.time + duration;
        while (Time.unscaledTime < timer)
        {
            var t = (timer - Time.unscaledTime) / duration;
            //Debug.Log(destination.Source + " " + destination.Serial);
            destination.Source.volume = Mathf.Sqrt(1 - t) * musicVolume * destination.Serial._relativeVolume;
            source.Source.volume *= Mathf.Sqrt(t);
            yield return null;
        }
        _currentMusicAudio = destination;
        destination.Source.volume = musicVolume * destination.Serial._relativeVolume;
        source.Source.volume = 0;
        _fadeRoutine = null;
    }
    ////Unscaled time fader by Shadiradio
    ////http://www.sombr.com/2015/10/22/audiomixersnapshot-transitions-independent-of-timescale/
    //private Coroutine transitionCoroutine;
    //public void TransitionSnapshots(AudioMixerSnapshot fromSnapshot, AudioMixerSnapshot toSnapshot, float transitionTime)
    //{
    //    EndTransition(fromSnapshot);
    //    Debug.Log("Transition from " + fromSnapshot.name + " to " + toSnapshot.name);
    //    transitionCoroutine = StartCoroutine(TransitionSnapshotsCoroutine(fromSnapshot, toSnapshot, transitionTime));
    //}

    //IEnumerator TransitionSnapshotsCoroutine(AudioMixerSnapshot fromSnapshot, AudioMixerSnapshot toSnapshot, float transitionTime)
    //{
    //    // transition values
    //    int steps = 20;
    //    float timeStep = (transitionTime / (float)steps);
    //    float transitionPercentage = 0.0f;
    //    float startTime = 0f;

    //    // set up snapshots
    //    //endSnapshot = toSnapshot;
    //    AudioMixerSnapshot[] snapshots = new AudioMixerSnapshot[] { fromSnapshot, toSnapshot };
    //    float[] weights = new float[2];

    //    // stepped-transition
    //    for (int i = 0; i < steps; i++)
    //    {
    //        transitionPercentage = ((float)i) / steps;
    //        weights[0] = 1.0f - transitionPercentage;
    //        weights[1] = transitionPercentage;
    //        fromSnapshot.audioMixer.TransitionToSnapshots(snapshots, weights, 0f);

    //        // this is required because WaitForSeconds doesn't work when Time.timescale == 0
    //        startTime = Time.realtimeSinceStartup;
    //        while (Time.realtimeSinceStartup < (startTime + timeStep))
    //        {
    //            yield return null;
    //        }
    //    }

    //    // finalize
    //    EndTransition(toSnapshot);
    //}

    //void EndTransition(AudioMixerSnapshot toSnapshot)
    //{
    //    if ((transitionCoroutine == null))
    //    {
    //        return;
    //    }

    //    StopCoroutine(transitionCoroutine);
    //    toSnapshot.TransitionTo(0f);
    //    _currentSnapshot = toSnapshot;
    //}
}
