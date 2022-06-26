using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueueMember : MonoBehaviour
{
    QueueManager qMan;
    EnemyAI nerdAI;
    public bool isPlayer = false;

    CharacterAgent agent;
    public void Awake()
    {
        nerdAI = GetComponent<EnemyAI>();
        agent = GetComponent<CharacterAgent>();
    }
    public void RegisterSelf(QueueManager _qMan)
    {
        qMan = _qMan;
        qMan.Register(this);
    }
    public void UpdatePosition(Vector3 destination)
    {
        agent.MoveTo(destination);
        agent.FaceTarget(qMan.gameObject.transform);
    }
    public void LeaveQueue(DetectableTarget foughtStaff, Vector3 newPosition)
    {
        agent.MoveTo(newPosition);
        if (nerdAI != null) nerdAI.FinishedFightingStaff(foughtStaff);
        qMan.Deregister(this);
        Destroy(this);
    }

}
