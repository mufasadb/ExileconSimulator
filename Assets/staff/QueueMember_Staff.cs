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
        agent.DontCollide();
    }
    public override void UpdatePosition(Vector3 destination, float waitTime)
    {
        base.UpdatePosition(destination, waitTime);

        StartCoroutine(Moveto(destination, waitTime));
    }
    IEnumerator Moveto(Vector3 destination, float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        agent.MoveTo(destination);
        agent.FaceTarget(qMan.gameObject.transform);
    }
    public override void LeaveQueue(DetectableTarget foughtStaff, Vector3 newPosition, float waitTime)
    {
        qMan.Deregister(this);

        // agent.ForcePosition(Vector3.zero);
        // agent.MoveTo(newPosition);
        // if (nerdAI != null) nerdAI.FinishedFightingStaff(foughtStaff);
        // agent.Collide();
        // base.LeaveQueue(foughtStaff, newPosition, waitTime);
        StartCoroutine(DoLeaveQueue(foughtStaff, newPosition, waitTime));
    }
    IEnumerator DoLeaveQueue(DetectableTarget foughtStaff, Vector3 newPosition, float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        agent.MoveTo(newPosition);
        if (nerdAI != null) nerdAI.FinishedFightingStaff(foughtStaff);
        agent.Collide();
        base.LeaveQueue(foughtStaff, newPosition, waitTime);
        // Destroy(this);
    }
}
