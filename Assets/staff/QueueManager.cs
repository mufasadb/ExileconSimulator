using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueueManager : MonoBehaviour
{
    public List<QueueMember> AllMembers { get; private set; } = new List<QueueMember>();
    public float timeBetweenFights = 0f;
    public float timeSinceFight = 0f;
    // private Vector3 myPosition;
    // private Vector3 firstPositionInQueue;
    private float queueGap = 1.5f;

    public void Register(QueueMember queueMember)
    {
        AllMembers.Add(queueMember);
        UpdatePlayerPosition();
    }
    public void Deregister(QueueMember queueMember)
    {
        AllMembers.Remove(queueMember);
    }
    public void Update()
    {
        if (AllMembers.Count > 0)
        {
            if (AllMembers[0].isPlayer)
            {
                //handle player
            }
            else
            {

                if (timeSinceFight > 0) timeSinceFight -= Time.deltaTime;
                if (timeSinceFight <= 0)
                {
                    timeSinceFight = timeBetweenFights;
                    AllMembers[0].LeaveQueue(GetComponent<DetectableTarget>(), transform.position + Vector3.back * 2);
                    UpdatePlayerPosition();
                }
            }
        }

    }
    void UpdatePlayerPosition()
    {
        for (int i = 0; i < AllMembers.Count; i++)
        {
            AllMembers[i].UpdatePosition((transform.position + Vector3.forward * queueGap * (i + 1)));
        }
    }
}