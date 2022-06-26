using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_Wander : Action_Base
{
    [SerializeField] float SearchRange = 10f;

    float timeMovingToDestination = 0;

    List<System.Type> SupportedGoals = new List<System.Type>(new System.Type[] { typeof(Goal_Wander) });

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

        Vector3 location = Agent.PickLocationInRange(SearchRange);

        Agent.MoveTo(location);
    }

    public override void OnTick()
    {
        timeMovingToDestination += Time.deltaTime;
        if (timeMovingToDestination > 20f)
        {
            Vector3 location = Agent.PickLocationInRange(SearchRange);
            Agent.MoveTo(location);
            timeMovingToDestination = 0;
        }
        // arrived at destination?
        if (Agent.AtDestination)
            OnActivated(LinkedGoal);
    }
}
