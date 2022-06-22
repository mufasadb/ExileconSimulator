using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fight : Interactable
{
    private Hand hand;
    private CardSelection cardSelection;
    private StaffMember staffMember;
    public void Start()
    {
        hand = Hand.instance;
        cardSelection = hand.cardSelection;
        staffMember = GetComponent<staffDetails>().staffData;
    }
    public override void Interact()
    {
        checkSelection();
    }
    private void checkSelection()
    {
        if (staffMember.name == "The Shaper")
        {
            if (!UserHasFrags()) Debug.Log("You dont have the frags you need");
            else
            {
                AudioManager.instance.SwitchToEndMusic();
                AudioManager.instance.Play("imChris");
            }
        }
        FightHandler.instance.InitiateFight(gameObject, staffMember);
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
