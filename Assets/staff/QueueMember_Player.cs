using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueueMember_Player : QueueMember_Base
{

    PlayerMotor motor;
    public override void UpdatePosition(Vector3 destination)
    {
        base.UpdatePosition(destination);
        if (motor == null)
        {
            motor = GetComponent<PlayerMotor>();
        }
        motor.StopFollowingTarget();
        motor.MoveToPoint(destination);
        motor.FaceTarget(qMan.gameObject.transform);
        Debug.Log("tried to move");
    }
    public override void LeaveQueue(DetectableTarget foughtStaff, Vector3 newPosition)
    {
        // motor.MoveToPoint(newPosition);
        GameEventManager.instance.NormalTime();
        GlobalVariables.instance.atFrontOfQueue = true;
        // FightHandler.instance.atFrontOfQueue = true;
        base.LeaveQueue(foughtStaff, newPosition);
    }
}
