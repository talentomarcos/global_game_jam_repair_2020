using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CShakeManager : MonoBehaviour {

	private List<Shake> _shakes;
	private bool _XYOnly;
	private Vector3 _lastValue;

	void Awake ()
	{
		_shakes = new List<Shake> ();
	}

	public void SetXYOnly(bool enabled)
	{
		_XYOnly = enabled;
	}

	public void AddShake(float aIntensity, float aDuration, bool aIsInfinite = false)
	{
		Shake newShake = new Shake ()
		{
			duration = aDuration,
			intensity = aIntensity,
			isInfinite = aIsInfinite
		};

		_shakes.Add (newShake);
	}

	public void StopShake()
	{
		_shakes.Clear ();
	}

	public void StopInfiniteShakes(bool withDecay = true)
	{
		for (int i = _shakes.Count - 1; i >= 0; i--)
		{
			if (_shakes [i].isInfinite)
			{
				if (!withDecay) {
					_shakes [i] = null;
					_shakes.RemoveAt (i);
				} else
					_shakes [i].isInfinite = false;
			}
		}
	}

	public Vector3 SmoothShake(float smoothness)
	{
        if (Time.deltaTime == 0)
            return Vector3.zero;
		Vector3 last = _lastValue;
        return Vector3.Lerp(last, Shake(), smoothness * Time.deltaTime);
	}

	public Vector3 Shake()
	{
        //if (CLevelManager.Inst.IsStopTime())
        //{
        //    return Vector3.zero;
        //}
		//actualizar y borrar
		for (int i = _shakes.Count - 1; i >= 0; i--)
		{
			if (_shakes [i].isInfinite)
				continue;

//			Shake shake = _shakes [i];
//			_shakes[i].elapsedTime += Time.deltaTime;
			_shakes[i].AddDeltaTime(Time.deltaTime);

			if (_shakes [i].NormalTime() >= 1)
			{
				_shakes [i] = null;
				_shakes.RemoveAt (i);
			}
		}

		//calcular
		int count = 0;
		float shakeAmount = 0;

		for (int i = 0; i < _shakes.Count; i++)
		{
			Shake shake = _shakes [i];
			float t = shake.NormalTime ();

			shakeAmount += shake.intensity * (1-t);
			count += 1;
		}

		if (count != 0)
		{
			shakeAmount /= count;
		}

		Vector3 offset;
		if (_XYOnly)
			offset = Random.insideUnitCircle;
		else
			offset = Random.insideUnitSphere;
		
		_lastValue = offset * shakeAmount;
		return offset * shakeAmount;
	}

}

[System.Serializable]
public class Shake
{
	public float elapsedTime;
	public float duration;
	public float intensity;
	public bool isInfinite;

	public void AddDeltaTime(float dt)
	{
		elapsedTime += dt;
	}

	public float NormalTime()
	{
		return elapsedTime / duration;					
	}
}
