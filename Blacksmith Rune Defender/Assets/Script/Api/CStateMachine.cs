using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CStateMachine : MonoBehaviour
{
    private int _state = 0;
    private float _timeState = 0.0f;


    virtual public void ApiUpdate()
    {
        _timeState += Time.deltaTime;

    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        ApiUpdate();
	}

    virtual public void SetState(int aState)
    {
        _state = aState;
        _timeState = 0.0f;
    }

    public int GetState()
    {
        return _state;
    }

    public float GetStateTime()
    {
        return _timeState;
    }
}
