using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyAI))]
public class Goal_GoToQueue : Goal_Base
{
    [SerializeField] int queuePriority = 60;
    [SerializeField] float MinAwarenessToChase = 1.5f;
    [SerializeField] float AwarenessToStopChase = 1f;
    public DetectableTarget CurrentTarget;
    int CurrentPriority = 0;

    public Vector3 MoveTarget => CurrentTarget != null ? CurrentTarget.transform.position : transform.position;
    public override void OnTickGoal()
    {
        CurrentPriority = 0;
        if (NerdAI.state == State.InQueue) return;

        // no targets
        if (Sensors.ActiveTargets == null || Sensors.ActiveTargets.Count == 0)
            return;

        // acquire a new target if possible
        foreach (var candidate in Sensors.ActiveTargets.Values)
        {
            // found a target to acquire
            if (candidate.Awareness >= MinAwarenessToChase)
            {
                float distToTarget = (candidate.Detectable.transform.position - transform.position).magnitude;
                if (distToTarget > candidate.Detectable.interactRange)
                {
                    if (CurrentTarget == null)
                    {
                        // Debug.Log("here");
                        // Debug.Log(CurrentTarget);
                        // if (CurrentTarget == null)
                        // if (CurrentTarget == null || candidate.Detectable.GetComponent<QueueManager>().AllMembers.Count < CurrentTarget.GetComponent<QueueManager>().AllMembers.Count)
                        // {
                        CurrentTarget = candidate.Detectable;
                        CurrentPriority = queuePriority;
                        return;
                    }
                    // }
                }
            }
        }

    }

    public override void OnGoalDeactivated()
    {
        // base.OnGoalDeactivated();

        // Debug.Log("check");
        CurrentTarget = null;
    }

    public override int CalculatePriority()
    {
        return CurrentPriority;
    }

    public override bool CanRun()
    {
        if (NerdAI.state == State.InQueue) return false;

        // no targets
        if (Sensors.ActiveTargets == null || Sensors.ActiveTargets.Count == 0)
            return false;

        // check if we have anything we are aware of
        foreach (var candidate in Sensors.ActiveTargets.Values)
        {
            if (candidate.Awareness >= MinAwarenessToChase)
                return true;
        }

        return false;
    }
}
