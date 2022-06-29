using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueueManager : MonoBehaviour
{
    public List<QueueMember_Base> AllMembers = new List<QueueMember_Base>();
    public float timeBetweenFights = 0f;
    public float timeSinceFight = 0f;
    // private Vector3 myPosition;
    // private Vector3 firstPositionInQueue;
    private float queueGap = 1.5f;

    public void Register(QueueMember_Base queueMember)
    {
        AllMembers.Add(queueMember);
        UpdatePlayerPosition();
    }
    public Vector3 GetEndPosition()
    {
        return transform.position + transform.forward * queueGap * AllMembers.Count;
    }
    public void Deregister(QueueMember_Base queueMember)
    {
        AllMembers.Remove(queueMember);
    }
    public void Update()
    {
        if (timeSinceFight > 0) timeSinceFight -= Time.deltaTime;
        if (AllMembers.Count > 0)
        {
            if (timeSinceFight <= 0)
            {
                if (!GlobalVariables.instance.standingAtFrontOfQueue)
                {
                    timeSinceFight = timeBetweenFights;
                    UpdatePlayerPosition();
                    AllMembers[0].LeaveQueue(GetComponent<DetectableTarget>(), transform.position + Vector3.back * 2, 0.1f);
                }
            }
        }


    }
    void UpdatePlayerPosition()
    {
        for (int i = 0; i < AllMembers.Count; i++)
        {
            float timeToMove = timeBetweenFights;
            if (i == AllMembers.Count - 1) timeToMove = 0;
            AllMembers[i].UpdatePosition((transform.position + transform.forward * queueGap * (i + 1)), 0);
        }
    }
}