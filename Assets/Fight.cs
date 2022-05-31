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

        FightHandler.instance.InitiateFight(gameObject, staffMember);
    }
}
