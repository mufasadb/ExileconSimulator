using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueueManager : MonoBehaviour
{
    public List<QueueMember_Base> AllMembers { get; private set; } = new List<QueueMember_Base>();
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
                timeSinceFight = timeBetweenFights;
                AllMembers[0].LeaveQueue(GetComponent<DetectableTarget>(), transform.position + Vector3.back * 2);
                UpdatePlayerPosition();
            }
        }


    }
    void UpdatePlayerPosition()
    {
        for (int i = 0; i < AllMembers.Count; i++)
        {
            AllMembers[i].UpdatePosition((transform.position + transform.forward * queueGap * (i + 1)));
        }
    }
}