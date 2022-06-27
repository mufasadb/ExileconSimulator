using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyAI))]
public class Goal_JoinQueue : Goal_Base
{
    [SerializeField] int queuePriority = 70;
    [SerializeField] float qLengthDisinsentive = 1f;
    // [SerializeField] float AwarenessToStopChase = 1f;
    public DetectableTarget CurrentTarget;
    public int CurrentPriority = 0;


    public override void OnTickGoal()
    {
        CurrentPriority = 0;
        if (CurrentTarget == NerdAI.lastFaughtTarget) CurrentTarget = null;
        if (NerdAI.state == State.InQueue)
        {
            CurrentPriority = Mathf.RoundToInt(queuePriority - CurrentTarget.qMan.AllMembers.Count * qLengthDisinsentive);
            return;
        }
        foreach (DetectableTarget candidate in Sensors.Targets)
        {
            if ((candidate.transform.position - transform.position).magnitude < candidate.interactRange)
            {
                if (CurrentTarget == null) CurrentTarget = candidate;
                else
                {
                    if (CurrentTarget.qMan.AllMembers.Count > candidate.qMan.AllMembers.Count) CurrentTarget = candidate;
                }
            }
        }
        if (CurrentTarget != null) CurrentPriority = Mathf.RoundToInt(queuePriority - CurrentTarget.qMan.AllMembers.Count * qLengthDisinsentive);
    }

    public override void OnGoalDeactivated()
    {
        base.OnGoalDeactivated();
        CurrentTarget = null;
    }

    public override int CalculatePriority()
    {
        return CurrentPriority;
    }

    public override bool CanRun()
    {
        if (NerdAI.state == State.InQueue) return true;
        // if (NerdAI.state != State.GoToQueue) return false;

        // no targets
        if (Sensors.Targets == null || Sensors.Targets.Count == 0)
            return false;


        return true;
    }
}
