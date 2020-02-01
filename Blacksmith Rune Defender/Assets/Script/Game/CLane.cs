using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CLane : MonoBehaviour
{
    public int _laneIndex;
    public CMercenary _mercenary;
    // To Do: add enemy.

    /// <summary>
    /// Called when the player succesfully completes a sequence.
    /// </summary>
    public void OnSequenceComplete()
    {

    }

    /// <summary>
    /// Called when a sequence's time is finished without success.
    /// </summary>
    public void OnSequenceEnded()
    {

    }

    public void SpawnEnemy()
    {
        // To Do: add spawning logic here.
        CSequenceData data = CSequenceManager.Inst.RequestSequence(_laneIndex);
        // To do: use data to update mercenary dialog (UI).
    }
}
