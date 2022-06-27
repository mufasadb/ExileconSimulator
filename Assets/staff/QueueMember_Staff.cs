using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueueMember_Staff : QueueMember_Base
{
    EnemyAI nerdAI;
    public bool isPlayer = false;
    CharacterAgent agent;
    public void Awake()
    {

        nerdAI = GetComponent<EnemyAI>();
        agent = GetComponent<CharacterAgent>();
    }
    public override void UpdatePosition(Vector3 destination)
    {
        base.UpdatePosition(destination);
        agent.MoveTo(destination);
        agent.FaceTarget(qMan.gameObject.transform);
    }
    public override void LeaveQueue(DetectableTarget foughtStaff, Vector3 newPosition)
    {

        agent.MoveTo(newPosition);
        if (nerdAI != null) nerdAI.FinishedFightingStaff(foughtStaff);
        base.LeaveQueue(foughtStaff, newPosition);

    }
}
