using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CScriptAnim : MonoBehaviour {

    public float _animationTime;
    public float _delayTime;

    protected bool _finishedDelay = false;
    protected bool _animate = false;
    protected float _elapsedAnimTime = 0;

    public enum AnimationFunction
    {
        EASE_IN,
        EASE_OUT,
        EASE_IN_OUT,
        BOING,
        CUSTOM_CURVE
    }

    [Space(5)]
    public AnimationFunction _evaluationType;
    public AnimationCurve _customCurve;

    public virtual void StartAnimation()
    {
        _finishedDelay = false;
        _animate = true;
        _elapsedAnimTime = 0;
    }

    public virtual void StopAnimation()
    {
        _animate = false;
    }

    public virtual bool IsFinished()
    {
        return _elapsedAnimTime >= _animationTime;
    }
}
