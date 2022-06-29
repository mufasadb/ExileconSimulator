using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FightButtons : MonoBehaviour
{
    // Start is called before the first frame update    
    [SerializeField] GameObject fightbutton;
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
        if (queueMember_Player != null) queueMember_Player.qMan.Deregister(queueMember_Player);
    }
    public void Update()
    {
        if (!GlobalVariables.instance.atFrontOfQueue)
        {
            fightbutton.GetComponent<Button>().enabled = false;
            fightbutton.GetComponent<Image>().color = new Color(100, 100, 100, 0.5f);
        }
        else
        {
            fightbutton.GetComponent<Button>().enabled = true;
            fightbutton.GetComponent<Image>().color = new Color(1, 1, 1, 1f);
        }
    }
    // Update is called once per frame

}
