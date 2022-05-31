using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fight : Interactable
{
    private Hand hand;
    private CardSelection cardSelection;
    public void Start()
    {
        hand = Hand.instance;
        cardSelection = hand.cardSelection;
    }
    public override void Interact()
    {
        checkSelection();
    }
    private void checkSelection()
    {
        // Debug.Log(cardSelection.twoHandedWeapons.Count);
        Debug.Log(cardSelection.attack.physical);
        Debug.Log(cardSelection.defence.life);
        Debug.Log(cardSelection.defence.armour);
        FightHandler.instance.InitiateFight(gameObject);
    }
}
