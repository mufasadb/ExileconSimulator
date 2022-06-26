using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_JoinQueue : Action_Base
{
    List<System.Type> SupportedGoals = new List<System.Type>(new System.Type[] { typeof(Goal_JoinQueue) });

    Goal_JoinQueue queueGoal;
    bool begunMove = false;

    public override List<System.Type> GetSupportedGoals()
    {
        return SupportedGoals;
    }

    public override float GetCost()
    {
        return 0f;
    }

    public override void OnActivated(Goal_Base _linkedGoal)
    {
        base.OnActivated(_linkedGoal);

        // cache the chase goal
        queueGoal = (Goal_JoinQueue)LinkedGoal;

        GetComponentInParent<EnemyAI>().JoinQueue();
        gameObject.AddComponent<QueueMember>();
        QueueMember qMem = gameObject.GetComponent<QueueMember>();
        QueueManager qMan = queueGoal.CurrentTarget.GetComponent<QueueManager>();
        qMem.RegisterSelf(qMan);

    }

    public override void OnDeactivated()
    {
        base.OnDeactivated();

        queueGoal = null;
    }

    public override void OnTick()
    {
        // Agent.MoveTo(queueGoal.CurrentTarget.getStaffQueueEndPoint());
    }


}
