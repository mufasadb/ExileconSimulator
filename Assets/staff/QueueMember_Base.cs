using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueueMember_Base : MonoBehaviour
{
    public QueueManager qMan;
    public void RegisterSelf(QueueManager _qMan)
    {
        qMan = _qMan;
        qMan.Register(this);
    }
    public virtual void UpdatePosition(Vector3 destination)
    {

    }
    public virtual void LeaveQueue(DetectableTarget foughtStaff, Vector3 newPosition)
    {

        qMan.Deregister(this);
        Destroy(this);
    }

}
