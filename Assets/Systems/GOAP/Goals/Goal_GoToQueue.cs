using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyAI))]
public class Goal_GoToQueue : Goal_Base
{
    [SerializeField] int queuePriority = 60;
    [SerializeField] float qLengthDisinsentive = 2f;
    // [SerializeField] float AwarenessToStopChase = 1f;
    public DetectableTarget CurrentTarget;

    public int CurrentPriority = 0;
    public int currentTargetViablityPoints = 0;
    [SerializeField] float differenceToSwitchTarget = 5f;

    public Vector3 MoveTarget => CurrentTarget != null ? CurrentTarget.transform.position : transform.position;
    public override void OnTickGoal()
    {
        CurrentPriority = 0;
        if (CurrentTarget == NerdAI.lastFaughtTarget) CurrentTarget = null;
        for (int i = 0; i < Sensors.Targets.Count; i++)
        {
            if (CurrentTarget == Sensors.Targets[i]) { continue; }
            if (Sensors.Targets[i] == NerdAI.lastFaughtTarget) { continue; }
            if (CurrentTarget == null)
            {
                CurrentTarget = Sensors.Targets[i];
                currentTargetViablityPoints = CalculateTargetViability(CurrentTarget);
            }
            else
            {
                if (currentTargetViablityPoints < CalculateTargetViability(Sensors.Targets[i]))
                {
                    currentTargetViablityPoints = CalculateTargetViability(Sensors.Targets[i]);
                    CurrentTarget = Sensors.Targets[i];
                }
            }
        }
        if (CurrentTarget != null && CurrentTarget.tier != NerdAI.tier) CurrentTarget = null;
        if (CurrentTarget != null)
        {
            CurrentPriority = Mathf.RoundToInt(queuePriority - CurrentTarget.qMan.AllMembers.Count * qLengthDisinsentive);
        }
        // Debug.Log(CurrentTarget.name);
        // Debug.Log(Sensors.Targets.Count);

    }
    int CalculateTargetViability(DetectableTarget target)
    {
        int value = 0;

        if (Mathf.Abs(target.transform.position.y - transform.position.y) > 15) value -= 50;
        if (Mathf.Abs(target.transform.position.y - transform.position.y) > 5) value -= 50;
        value += (100 - Mathf.RoundToInt((target.transform.position - transform.position).magnitude));
        value -= Mathf.RoundToInt(target.qMan.AllMembers.Count * qLengthDisinsentive);
        return value;

    }
    public override void OnGoalDeactivated()
    {
        base.OnGoalDeactivated();
        CurrentTarget = null;
    }
    public override void OnGoalActivated(Action_Base _linkedAction)
    {
        base.OnGoalActivated(_linkedAction);

    }

    public override int CalculatePriority()
    {
        return CurrentPriority;
    }

    public override bool CanRun()
    {
        if (NerdAI.state == State.InQueue) return false;

        // no targets
        if (Sensors.Targets == null || Sensors.Targets.Count == 0)
        {
            return false;
        }

        // check if we have anything we are aware of
        foreach (var candidate in Sensors.Targets)
        {
            return true;
        }

        return false;
    }
}
