using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal_Idle : Goal_Base
{
    [SerializeField] int Priority = 10;

    public override int CalculatePriority()
    {
        return Priority;
    }
    public override void OnGoalActivated(Action_Base _linkedAction)
    {
        base.OnGoalActivated(_linkedAction);
        NerdAI.Idling();
    }
    public override bool CanRun()
    {
        if (NerdAI.state == State.InQueue) { return false; }
        return true;
    }
}
