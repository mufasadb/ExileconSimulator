using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightButtons : MonoBehaviour
{
    // Start is called before the first frame update    

    public void Fight()
    {
        if (GlobalVariables.instance.atFrontOfQueue) FightHandler.instance.doFight();
        else GlobalVariables.instance.errorHandler.NewError("You must be at the front of the queue to actually fight the staff member, fast forward to get there quicker!");
    }

    public void Cancel()
    {
        FightHandler.instance.CancelFight();
        GameEventManager.instance.NormalTime();
        QueueMember_Player queueMember_Player = GlobalVariables.instance.player.GetComponent<QueueMember_Player>();
        if (queueMember_Player) queueMember_Player.qMan.Deregister(queueMember_Player);
    }
    // Update is called once per frame

}
