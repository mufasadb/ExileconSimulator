using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fight : Interactable
{
    private Hand hand;
    private CardSelection cardSelection;
    private StaffMember staffMember;
    private float clickCooldown = 0;
    public void Start()
    {
        hand = Hand.instance;
        cardSelection = hand.cardSelection;
        staffMember = GetComponent<staffDetails>().staffData;
    }
    public override void Interact()
    {
        if (clickCooldown <= 0)
        {
            clickCooldown = 0.05f;
            checkSelection();
        }
    }
    private void checkSelection()
    {
        if (staffMember.name == "The Shaper")
        {
            if (!UserHasFrags()) { GlobalVariables.instance.errorHandler.NewError("In order to fight Chris, you need all 4 Gaurdian Shards"); return; }
            else
            {
                AudioManager.instance.SwitchToEndMusic();
                AudioManager.instance.Play("imChris");
            }
        }
        if (FightHandler.instance.InitiateFight(gameObject, staffMember))
        {
            GameEventManager.instance.AutoFastForward();
            QueueManager qMan = GetComponent<QueueManager>();
            GameObject player = GameObject.Find("Player");
            // player.AddComponent<QueueMember_Player>();
            player.GetComponent<QueueMember_Player>().RegisterSelf(qMan);
        }

    }
    public override void Update()
    {
        base.Update();
        if (clickCooldown > 0) { clickCooldown -= Time.deltaTime; }
        interactionTransform.position = gameObject.GetComponent<QueueManager>().GetEndPosition();
    }
    private bool UserHasFrags()
    {
        bool hasFrags = true;
        if (Hand.instance.hand.Find(gameObject => gameObject.GetComponent<CardDisplay>().card.name == "Fragment of Hydra")) { } else { hasFrags = false; }
        if (Hand.instance.hand.Find(gameObject => gameObject.GetComponent<CardDisplay>().card.name == "Fragment of Phoenix")) { } else { hasFrags = false; }
        if (Hand.instance.hand.Find(gameObject => gameObject.GetComponent<CardDisplay>().card.name == "Fragment of Chimera")) { } else { hasFrags = false; }
        if (Hand.instance.hand.Find(gameObject => gameObject.GetComponent<CardDisplay>().card.name == "Fragment of Minotaur")) { } else { hasFrags = false; }
        return hasFrags;
    }
}
