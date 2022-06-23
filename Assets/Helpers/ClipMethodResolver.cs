using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ClipMethodResolver
{
    public static void HandleClip(List<CardDisplay> cardList, ClipMethod clipMethod, int clipCount)
    {
        //Handle Unique Card Atiziris Acuity and Lioneyes defence. forces clipping on these cards
        foreach (CardDisplay card in cardList)
        {
            if (card.name == "Atziris Acuity" || card.name == "Lioneeyes Defence")
            {
                DoClip(card, clipCount);
                return;
            }
        }
        if (clipMethod == ClipMethod.All) { foreach (CardDisplay card in cardList) { DoClip(card, clipCount); } }
        else if (clipMethod == ClipMethod.Jewellery)
        {
            foreach (CardDisplay card in cardList)
            {
                if (card.card.type == Type.Amulet || card.card.type == Type.Ring)
                {
                    DoClip(card, clipCount);
                }
                else
                {
                    DontClip(card);
                }
            }
        }
        else
        {
            CardDisplay selectedCard = Comparer(cardList, clipMethod);
            DoClip(selectedCard, clipCount);
            foreach (CardDisplay card in cardList)
            {
                if (card != selectedCard) { DontClip(card); }
            }
        }
    }
    private static void DontClip(CardDisplay card)
    {
        if (GlobalVariables.instance.selectionState == SelectionState.InMaps)
        {

        }
        else
        {
            card.DoUnselect();
        }
    }
    private static void DoClip(CardDisplay card, int clipCount)
    {
        card.DoClip(clipCount);
        GlobalVariables.instance.clipPendingCount++;
    }
    // Start is called before the first frame update
    public static CardDisplay Comparer(List<CardDisplay> cardList, ClipMethod clipMethod)
    {
        CardDisplay selectedCardDisplay = cardList[0];
        switch (clipMethod)
        {
            case ClipMethod.Rare:
                {
                    foreach (CardDisplay card in cardList)
                    {
                        if (card.card.rarity > selectedCardDisplay.card.rarity) { selectedCardDisplay = card; }
                    }
                    break;
                }
            case ClipMethod.Common:
                {
                    foreach (CardDisplay card in cardList)
                    {
                        if (card.card.rarity < selectedCardDisplay.card.rarity) { selectedCardDisplay = card; }
                    }
                    break;
                }
            case ClipMethod.Broken:
                {
                    foreach (CardDisplay card in cardList)
                    {
                        if (card.card.durability < selectedCardDisplay.card.durability) { selectedCardDisplay = card; }
                    }
                    break;
                }
            case ClipMethod.Chest:
                {
                    bool cardFound = false;
                    foreach (CardDisplay card in cardList)
                    {
                        if (card.card.type == Type.Chest) { selectedCardDisplay = card; }
                    }
                    if (!cardFound) { selectedCardDisplay = cardList[Random.Range(0, cardList.Count)]; }
                    break;
                }
            case ClipMethod.Shield:
                {
                    bool cardFound = false;
                    foreach (CardDisplay card in cardList)
                    {
                        if (card.card.type == Type.Shield) { selectedCardDisplay = card; }
                    }
                    if (!cardFound) { selectedCardDisplay = cardList[Random.Range(0, cardList.Count)]; }
                    break;
                }
            case ClipMethod.Weapon:
                {
                    bool cardFound = false;
                    foreach (CardDisplay card in cardList)
                    {
                        if (card.card.type == Type.TwoHandedWeapon || card.card.type == Type.OneHandedWeapon) { selectedCardDisplay = card; }
                    }
                    if (!cardFound) { selectedCardDisplay = cardList[Random.Range(0, cardList.Count)]; }
                    break;
                }
            default:
                {
                    Debug.LogError("unable to resolve clipMethod"); return null;
                }
        }
        return selectedCardDisplay;
    }

}
