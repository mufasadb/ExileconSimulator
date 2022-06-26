using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_GoToQueue : Action_Base
{
    List<System.Type> SupportedGoals = new List<System.Type>(new System.Type[] { typeof(Goal_GoToQueue) });

    Goal_GoToQueue queueGoal;

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
        queueGoal = (Goal_GoToQueue)LinkedGoal;

        GetComponentInParent<EnemyAI>().ToQueue();
        // StartCoroutine(Queue());
        Agent.MoveTo(queueGoal.CurrentTarget.transform.position);


    }

    public override void OnDeactivated()
    {
        base.OnDeactivated();

        queueGoal = null;
    }

    public override void OnTick()
    {
        // Agent.MoveTo(queueGoal.CurrentTarget.transform.position);
    }
    // IEnumerator Queue()
    // {
    //     Agent.MoveTo(queueGoal.CurrentTarget.getStaffQueueEndPoint());
    //     while (!Agent.IsMoving)
    //     {
    //         yield return new WaitForEndOfFrame();
    //     }
    //     begunMove = true;
    //     while (begunMove == true && Agent.IsMoving)
    //     {
    //         Agent.MoveTo(queueGoal.CurrentTarget.getStaffQueueEndPoint());
    //         yield return new WaitForEndOfFrame();
    //     }
    //     Agent.FaceTarget(queueGoal.CurrentTarget.transform);
    //     GetComponentInParent<EnemyAI>().JoinQueue();
    //     gameObject.AddComponent<QueueMember>();
    //     QueueManager qMan = queueGoal.CurrentTarget.GetComponent<QueueManager>();
    //     GetComponent<QueueMember>().RegisterSelf(qMan);
    //     OnDeactivated();
    //     //turn toface (lerped)
    // }

}
