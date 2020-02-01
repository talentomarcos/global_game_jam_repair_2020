using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSequenceManager : MonoBehaviour
{
    private static CSequenceManager _inst;
    public static CSequenceManager Inst
    {
        get
        {
            if (_inst == null)
            {
                GameObject obj = new GameObject("LevelManager");
                return obj.AddComponent<CSequenceManager>();
            }
            return _inst;
        }
    }

    private CSequenceData[] _openSequences = new CSequenceData[3];
    public float _sequenceWaitTime = 7f;

    public int _startSequenceCount = 2;
    public int _maxSequenceCount = 4;
    [Tooltip("The amount of time that will be discounted if the player is mistaken. In seconds.")]
    public float _errorTimeReduce = 1;

    private int _currentSequenceCount;

    public List<CLane> _lanes;

    private int _completedSeqCounter = 0;

    void Awake()
    {
        if (_inst != null && _inst != this)
        {
            Debug.Log("destroying " + CSequenceManager.Inst.gameObject.name);
            Destroy(CSequenceManager.Inst.gameObject);
            //return;
        }
        _inst = this;
        _currentSequenceCount = _startSequenceCount;
    }

    private void Start()
    {
        _openSequences = new CSequenceData[GameData.LaneAmount];
    }

    private void Update()
    {
        if (GameData.IsPause)
        {
            return;
        }
        for (int i = 0; i < _openSequences.Length; i++)
        {
            if (_openSequences[i] == null)
            {
                continue;
            }
            _openSequences[i].Update();
            if (_openSequences[i].HasEnded())
            {
                // To do: Notify and do stuff.
                if (_lanes.Count > i)
                {
                    _lanes[i].OnSequenceEnded();
                }
                _openSequences[i] = null;
            }
        }
    }

    public CSequenceData RequestSequence(int aLane)
    {
        List<Runes> sequence = new List<Runes>();
        for (int i = 0; i < _currentSequenceCount; i++)
        {
            int random = CMath.randomIntBetween(0, 3);
            switch (random)
            {
                case 0:
                    sequence.Add(Runes.FIRE);
                    break;
                case 1:
                    sequence.Add(Runes.OCCULT);
                    break;
                case 2:
                    sequence.Add(Runes.ENERGY);
                    break;
                case 3:
                default:
                    sequence.Add(Runes.STRENGTH);
                    break;
            }
        }
        Debug.Log(sequence.Count);
        CSequenceData data = new CSequenceData(_sequenceWaitTime, sequence);
        _openSequences[aLane] = data;
        return data;
    }

    public bool CheckSequence(int aLane, List<Runes> aSequence)
    {
        if (_openSequences[aLane] == null)
        {
            return true;
        }
        if (_openSequences[aLane].GetRuneAmount() == aSequence.Count)
        {
            _openSequences[aLane].waitTime -= _errorTimeReduce;
            return false;
        }
        int i = 0;
        bool foundError = false;
        while (!foundError && i < aSequence.Count)
        {
            foundError = aSequence[i] != _openSequences[aLane].GetRuneAt(i);
        }
        if (foundError)
        {
            _openSequences[aLane].waitTime -= _errorTimeReduce;
        }
        return !foundError;
    }

    public int GetCurrentRuneCount()
    {
        return _currentSequenceCount;
    }

    public void CompleteSequence(int aLane)
    {
        if (aLane > 0)
        {
            if (_lanes.Count > aLane)
            {
                _lanes[aLane].OnSequenceComplete();
            }
            if (_openSequences.Length > aLane)
            {
                _openSequences[aLane] = null;
                _completedSeqCounter++;
            }
        }
    }
}
