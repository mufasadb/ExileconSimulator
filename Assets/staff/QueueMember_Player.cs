using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueueMember_Player : QueueMember_Base
{

    PlayerMotor motor;
    public override void UpdatePosition(Vector3 destination, float waitTime)
    {
        base.UpdatePosition(destination, waitTime);
        if (motor == null)
        {
            motor = GetComponent<PlayerMotor>();
        }
        motor.StopCollisions();
        StartCoroutine(MoveTo(motor, destination, waitTime));

    }
    IEnumerator MoveTo(PlayerMotor motor, Vector3 destination, float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        if (qMan != null)
        {
            motor.StopFollowingTarget();
            motor.MoveToPoint(destination);
            motor.FaceTarget(qMan.gameObject.transform);
        }
    }
    public override void LeaveQueue(DetectableTarget foughtStaff, Vector3 newPosition, float waitTime)
    {
        qMan.Deregister(this);

        // motor.MoveToPoint(newPosition);

        // GameEventManager.instance.NormalTime();
        // GlobalVariables.instance.atFrontOfQueue = true;
        // GlobalVariables.instance.standingAtFrontOfQueue = true;
        // motor.StartCollisions();
        // // FightHandler.instance.atFrontOfQueue = true;
        // base.LeaveQueue(foughtStaff, newPosition, waitTime);
        StartCoroutine(DoLeaveQueue(foughtStaff, newPosition, waitTime));
    }
    IEnumerator DoLeaveQueue(DetectableTarget foughtStaff, Vector3 newPosition, float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        GameEventManager.instance.NormalTime();
        GlobalVariables.instance.atFrontOfQueue = true;
        GlobalVariables.instance.standingAtFrontOfQueue = true;
        motor.StartCollisions();
        // motor.StopMoving();
        // FightHandler.instance.atFrontOfQueue = true;
        base.LeaveQueue(foughtStaff, newPosition, waitTime);
    }
}
