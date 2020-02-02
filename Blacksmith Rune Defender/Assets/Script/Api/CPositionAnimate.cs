using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPositionAnimate : CScriptAnim
{
    public Vector3 _initialPos;
    public Vector3 _endPos;

    public bool _useRecTf = true;
    public bool _playOnAwake = false;

    private RectTransform _rectTf;
    public bool _loop;
    private bool _returning = false;

    public bool _unscaledDeltaTime = false;
    
    void Awake()
    {
        if (_useRecTf)
        {
            _rectTf = GetComponent<RectTransform>();
        }

        if (_playOnAwake)
        {
            StartAnimation();
        }
    }
    	
	// Update is called once per frame
	void Update ()
    {
        if (_useRecTf)
        {
            RecTransformUpdate();
        }
        else
            TransformUpdate();
	}

    public virtual void TransformUpdate()
    {
        if (_animate)
        {
            if (_unscaledDeltaTime)
            {
                _elapsedAnimTime += Time.unscaledDeltaTime;
            }
            else _elapsedAnimTime += Time.deltaTime;
            if (!_finishedDelay)
            {
                if (_elapsedAnimTime > _delayTime)
                {
                    _elapsedAnimTime = 0;
                    _finishedDelay = true;
                }
                return;
            }
            if (_elapsedAnimTime >= _animationTime)
            {
                if (_loop)
                {
                    _elapsedAnimTime = 0;
                    _returning = !_returning;
                }
                else
                {
                    transform.localPosition = _endPos;
                    _animate = false;
                    return;
                }
            }
            float time = _elapsedAnimTime / _animationTime;
            if (!_returning)
            {
                if (_evaluationType == AnimationFunction.EASE_IN)
                {
                    transform.localPosition = Mathfx.Coserp(_initialPos, _endPos, time);
                }
                else if (_evaluationType == AnimationFunction.EASE_OUT)
                {
                    transform.localPosition = Mathfx.Sinerp(_initialPos, _endPos, time);
                }
                else if (_evaluationType == AnimationFunction.EASE_IN_OUT)
                {
                    transform.localPosition = Mathfx.Hermite(_initialPos, _endPos, time);
                }
                else if (_evaluationType == AnimationFunction.BOING)
                {
                    transform.localPosition = Mathfx.Berp(_initialPos, _endPos, time);
                }
                else if (_evaluationType == AnimationFunction.CUSTOM_CURVE)
                {
                    transform.localPosition = Vector3.Lerp(_initialPos, _endPos, _customCurve.Evaluate(time));
                }
            }
            else
            {
                if (_evaluationType == AnimationFunction.EASE_IN)
                {
                    transform.localPosition = Mathfx.Coserp(_endPos, _initialPos, time);
                }
                else if (_evaluationType == AnimationFunction.EASE_OUT)
                {
                    transform.localPosition = Mathfx.Sinerp(_endPos, _initialPos, time);
                }
                else if (_evaluationType == AnimationFunction.EASE_IN_OUT)
                {
                    transform.localPosition = Mathfx.Hermite(_endPos, _initialPos, time);
                }
                else if (_evaluationType == AnimationFunction.BOING)
                {
                    transform.localPosition = Mathfx.Berp(_endPos, _initialPos, time);
                }
                else if (_evaluationType == AnimationFunction.CUSTOM_CURVE)
                {
                    transform.localPosition = Vector3.Lerp(_endPos, _initialPos, _customCurve.Evaluate(time));
                }
            }
            
        }
    }

    public virtual void RecTransformUpdate()
    {
        if (_rectTf == null)
        {
            return;
        }
        if (_animate)
        {
            _elapsedAnimTime += Time.deltaTime;
            if (!_finishedDelay && _elapsedAnimTime > _delayTime)
            {
                _elapsedAnimTime = 0;
                _finishedDelay = true;
            }
            if (_elapsedAnimTime >= _animationTime)
            {
                _rectTf.anchoredPosition3D = _endPos;
                _animate = false;
                return;
            }

            float time = _elapsedAnimTime / _animationTime;
            if (_evaluationType == AnimationFunction.EASE_IN)
            {
                _rectTf.anchoredPosition3D = Mathfx.Coserp(_initialPos, _endPos, time);
            }
            else if (_evaluationType == AnimationFunction.EASE_OUT)
            {
                _rectTf.anchoredPosition3D = Mathfx.Sinerp(_initialPos, _endPos, time);
            }
            else if (_evaluationType == AnimationFunction.EASE_IN_OUT)
            {
                _rectTf.anchoredPosition3D = Mathfx.Hermite(_initialPos, _endPos, time);
            }
            else if (_evaluationType == AnimationFunction.BOING)
            {
                _rectTf.anchoredPosition3D = Mathfx.Berp(_initialPos, _endPos, time);
            }
            else if (_evaluationType == AnimationFunction.CUSTOM_CURVE)
            {
                _rectTf.anchoredPosition3D = Vector3.Lerp(_initialPos, _endPos, _customCurve.Evaluate(time));
            }
        }
    }

    public override void StartAnimation()
    {
        base.StartAnimation();
        if (_useRecTf)
        {
            _rectTf.anchoredPosition3D = _initialPos;
        }
        else
            transform.localPosition = _initialPos;
    }
    public override bool IsFinished()
    {
        return base.IsFinished() && !_loop;
    }
}
