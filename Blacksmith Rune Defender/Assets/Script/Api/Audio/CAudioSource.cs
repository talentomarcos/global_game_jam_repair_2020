using UnityEngine;
using UnityEngine.Audio;

public class CAudioSource : CGameObject
{
    AudioSource _source;
    float _length;
    float _time;
    //Transform _transform;
    bool _ignoreSpatialBlend = false;

    enum State
    {
        PLAY,
        STOP,
    }
    State _state;

    public override void ApiAwake()
    {
        base.ApiAwake();
        _source = GetComponent<AudioSource>();
        if (_source == null)
            _source = gameObject.AddComponent<AudioSource>();
        //_transform = transform;
        SetState(State.STOP);
        if (!_ignoreSpatialBlend)
            _source.spatialBlend = 0.7f;
        else
            _source.spatialBlend = 0f;
    }

    void SetState(State aState)
    {
        _state = aState;
        _time = 0;
        if (_state == State.PLAY)
        {
            gameObject.SetActive(true);
            SetDead(false);
            _source.Play();
            if (_source.clip != null)
                _length = _source.clip.length * _source.pitch;
        }
        else if (_state == State.STOP)
        {
            SetDead(true);
            gameObject.SetActive(false);
        }
    }

    public override void ApiUpdate()
    {
        if (_state == State.PLAY)
        {
            if (!_source.loop && _time > _length)
            {
                SetState(State.STOP);
                return;
            }
        }
        base.ApiUpdate();
        _time += Time.unscaledDeltaTime;
    }

    public void SetGroup(AudioMixerGroup aGroup)
    {
        _source.outputAudioMixerGroup = aGroup;
    }

    public void IgnoreSpatialBlend(bool ignore)
    {
        _ignoreSpatialBlend = ignore;
    }

    public void Play(AudioClip aClip, bool aLoops=false)
    {
        _source.clip = aClip;
        _source.loop = aLoops;
        SetState(State.PLAY);
    }
    
    public void PlayClip()
    {
        SetState(State.PLAY);
    }

    public void Stop()
    {
        SetState(State.STOP);
    }

    public void SetVolume(float aVolume)
    {
        _source.volume = aVolume;
    }

    public void SetPitch(float aPitch, float aTime = 0)
    {
        _source.pitch = aPitch;
    }

    public void SetLoop(bool aLoop)
    {
        _source.loop = aLoop;
    }

    public float GetPitch()
    {
        return _source.pitch;
    }

    public AudioClip GetClip()
    {
        return _source.clip;
    }
}
