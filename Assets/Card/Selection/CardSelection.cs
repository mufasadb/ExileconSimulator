using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSelection : MonoBehaviour
{
    public int twoHandedWeaponLimit = 1;
    public int oneHandedWeaponLimit = 1;
    public int shieldLimit = 1;
    public int chestLimit = 1;
    public int amuletLimit = 1;
    public int ringLimit = 2;
    public float verticalGap = 140;
    public float topCardPosition = 900;
    public float leftCardPosition = 150;
    public float horizontalGap = 150;
    public List<CardDisplay> twoHandedWeapons = new List<CardDisplay>();
    public List<CardDisplay> shields = new List<CardDisplay>();
    public List<CardDisplay> amulets = new List<CardDisplay>();
    public List<CardDisplay> oneHandedWeapons = new List<CardDisplay>();
    public List<CardDisplay> rings = new List<CardDisplay>();
    public List<CardDisplay> chests = new List<CardDisplay>();
    public Stats attack = new Stats();
    public Stats defence = new Stats();
    public int extraDraw = 0;
    public int extraTakes = 0;
    public int GetLimit(Type type)
    {
        int limit;
        switch (type)
        {
            case Type.TwoHandedWeapon:
                limit = twoHandedWeaponLimit;
                break;
            case Type.OneHandedWeapon:
                limit = oneHandedWeaponLimit;
                break;
            case Type.Chest:
                limit = chestLimit;
                break;
            case Type.Amulet:
                limit = amuletLimit;
                break;
            case Type.Ring:
                limit = ringLimit;
                break;
            case Type.Shield:
                limit = shieldLimit;
                break;
            default:
                Debug.LogError("tried to create a selection locaton without a type");
                limit = 1;
                break;
        }
        return limit;

    }
    public void statCalc()
    {
        //int fire, int cold, int lightning, int physical, int life, int armour, int chaos, int wild

        //impliment quality
        extraTakes = 0;
        extraDraw = 0;
        int[] attackStats = new int[8];
        int[] defenceStats = new int[8];
        (attackStats, defenceStats) = ReadGroupOfCards(attackStats, defenceStats, twoHandedWeapons);
        (attackStats, defenceStats) = ReadGroupOfCards(attackStats, defenceStats, oneHandedWeapons);
        (attackStats, defenceStats) = ReadGroupOfCards(attackStats, defenceStats, shields);
        (attackStats, defenceStats) = ReadGroupOfCards(attackStats, defenceStats, chests);
        (attackStats, defenceStats) = ReadGroupOfCards(attackStats, defenceStats, amulets);
        (attackStats, defenceStats) = ReadGroupOfCards(attackStats, defenceStats, rings);

        attack.DeclareStats(attackStats);
        defence.DeclareStats(defenceStats);
    }
    (int[], int[]) ReadGroupOfCards(int[] attackStats, int[] defenceStats, List<CardDisplay> list)
    {
        // int[] stats = new int[8];
        foreach (CardDisplay cardDisplay in list)
        {
            if (cardDisplay.asWeapon)
            {
                attackStats[0] += cardDisplay.card.implicits.fire;
                attackStats[0] += cardDisplay.card.explicits.fire;
                attackStats[1] += cardDisplay.card.implicits.cold;
                attackStats[1] += cardDisplay.card.explicits.cold;
                attackStats[2] += cardDisplay.card.implicits.lightning;
                attackStats[2] += cardDisplay.card.explicits.lightning;
                attackStats[3] += cardDisplay.card.implicits.physical;
                attackStats[3] += cardDisplay.card.explicits.physical;
                defenceStats[4] += cardDisplay.card.implicits.life;
                defenceStats[4] += cardDisplay.card.explicits.life;
                defenceStats[5] += cardDisplay.card.implicits.armour;
                defenceStats[5] += cardDisplay.card.explicits.armour;
                attackStats[6] += cardDisplay.card.implicits.chaos;
                attackStats[6] += cardDisplay.card.explicits.chaos;
                attackStats[7] += cardDisplay.card.implicits.wild;
                attackStats[7] += cardDisplay.card.explicits.wild;
                attackStats[3] += cardDisplay.card.quality.physical;
            }
            else
            {
                defenceStats[0] += cardDisplay.card.implicits.fire;
                defenceStats[0] += cardDisplay.card.explicits.fire;
                defenceStats[1] += cardDisplay.card.implicits.cold;
                defenceStats[1] += cardDisplay.card.explicits.cold;
                defenceStats[2] += cardDisplay.card.implicits.lightning;
                defenceStats[2] += cardDisplay.card.explicits.lightning;
                attackStats[3] += cardDisplay.card.implicits.physical;
                attackStats[3] += cardDisplay.card.explicits.physical;
                defenceStats[4] += cardDisplay.card.implicits.life;
                defenceStats[4] += cardDisplay.card.explicits.life;
                defenceStats[5] += cardDisplay.card.implicits.armour;
                defenceStats[5] += cardDisplay.card.explicits.armour;
                defenceStats[6] += cardDisplay.card.implicits.chaos;
                defenceStats[6] += cardDisplay.card.explicits.chaos;
                defenceStats[7] += cardDisplay.card.implicits.wild;
                defenceStats[7] += cardDisplay.card.explicits.wild;
                defenceStats[5] += cardDisplay.card.quality.armour;
            }
            extraTakes += cardDisplay.card.extraTakes;
            extraDraw += cardDisplay.card.extraDraw;
        }
        return (attackStats, defenceStats);
    }
    public void UnSelect(CardDisplay cardDisplay)
    {
        switch (cardDisplay.card.type)
        {
            case Type.TwoHandedWeapon:
                {
                    twoHandedWeapons.Remove(cardDisplay);
                    break;
                }
            case Type.OneHandedWeapon:
                {
                    oneHandedWeapons.Remove(cardDisplay);
                    break;
                }
            case Type.Shield:
                {
                    shields.Remove(cardDisplay);
                    break;
                }
            case Type.Chest:
                {
                    chests.Remove(cardDisplay);

                    break;
                }
            case Type.Amulet:
                {
                    amulets.Remove(cardDisplay);
                    break;
                }
            case Type.Ring:
                {
                    rings.Remove(cardDisplay);
                    break;
                }
            case Type.Map:
                {
                    GlobalVariables.instance.mapSelection.cardDisplay = null;
                    break;
                }
            default:
                // Debug.Log("didn't have a type");
                break;
        }
        statCalc();
        FightHandler.instance.reCalculateStats();
    }
    public void Select(CardDisplay cardDisplay)
    {
        switch (cardDisplay.card.type)
        {
            case Type.TwoHandedWeapon:
                {
                    twoHandedWeapons.Add(cardDisplay);
                    if (twoHandedWeapons.Count > twoHandedWeaponLimit)
                    {
                        twoHandedWeapons.RemoveAt(0);
                    }
                    RemoveAllType(Type.OneHandedWeapon);
                    RemoveAllType(Type.Shield);
                    break;
                }
            case Type.OneHandedWeapon:
                {
                    oneHandedWeapons.Add(cardDisplay);
                    if (oneHandedWeapons.Count > oneHandedWeaponLimit)
                    {
                        oneHandedWeapons.RemoveAt(0);
                    }
                    RemoveAllType(Type.TwoHandedWeapon);
                    break;
                }
            case Type.Shield:
                {
                    RemoveAllType(Type.TwoHandedWeapon);
                    shields.Add(cardDisplay);
                    if (shields.Count > shieldLimit)
                    {
                        shields.RemoveAt(0);
                    }
                    break;
                }
            case Type.Chest:
                {

                    chests.Add(cardDisplay);
                    if (chests.Count > chestLimit)
                    {
                        chests.RemoveAt(0);
                    }
                    break;
                }
            case Type.Amulet:
                {
                    amulets.Add(cardDisplay);
                    if (amulets.Count > amuletLimit)
                    {
                        amulets.RemoveAt(0);
                    }
                    break;
                }
            case Type.Ring:
                {
                    rings.Add(cardDisplay);
                    if (rings.Count > ringLimit)
                    {
                        rings.RemoveAt(0);
                    }
                    break;
                }
            default:
                Debug.Log("didn't have a type");
                break;
        }
        statCalc();
        FightHandler.instance.reCalculateStats();

        if (GlobalVariables.instance.selectionState == SelectionState.EnteringMaps)
        {
            GlobalVariables.instance.mapHandler.CheckCanEnterMap();
        }
    }
    private void RemoveAllType(Type type)
    {
        switch (type)
        {
            case Type.TwoHandedWeapon:
                {
                    foreach (CardDisplay cardDisplay in twoHandedWeapons)
                    {
                        cardDisplay.DoUnselect();
                        twoHandedWeapons.Remove(cardDisplay);
                    }
                    break;
                }
            case Type.OneHandedWeapon:
                {
                    foreach (CardDisplay cardDisplay in oneHandedWeapons)
                    {
                        cardDisplay.DoUnselect();
                        oneHandedWeapons.Remove(cardDisplay);
                    }
                    break;
                }
            case Type.Shield:
                {
                    foreach (CardDisplay cardDisplay in shields)
                    {
                        cardDisplay.DoUnselect();
                        shields.Remove(cardDisplay);
                    }
                    break;
                }
            case Type.Chest:
                {
                    foreach (CardDisplay cardDisplay in chests)
                    {
                        cardDisplay.DoUnselect();
                        chests.Remove(cardDisplay);
                    }
                    break;
                }
            case Type.Amulet:
                {
                    foreach (CardDisplay cardDisplay in amulets)
                    {
                        cardDisplay.DoUnselect();
                        amulets.Remove(cardDisplay);
                    }
                    break;
                }
            case Type.Ring:
                {
                    foreach (CardDisplay cardDisplay in rings)
                    {
                        cardDisplay.DoUnselect();
                        rings.Remove(cardDisplay);
                    }
                    break;
                }
            default:
                break;
        }
    }
    // public void Select(CardDisplay card)
    // {
    //     if (card.selected)
    //     {
    //         int unselectedCard = 0;
    //         switch (card.card.type)
    //         {
    //             case Type.TwoHandedWeapon:
    //                 // unselectedCard = twoHandedWeapons.FindIndex(c => c.name == card.name);
    //                 // twoHandedWeapons[unselectedCard].DoUnselect();
    //                 twoHandedWeapons.RemoveAt(unselectedCard);
    //                 if (twoHandedWeapons.Count > 0)
    //                 {
    //                     for (int i = 0; i < twoHandedWeapons.Count; i++)
    //                     {
    //                         twoHandedWeapons[i].DoSelect(new Vector3(leftCardPosition + i * horizontalGap, topCardPosition, 0));
    //                     }
    //                 }
    //                 break;

    //             case Type.OneHandedWeapon:
    //                 unselectedCard = oneHandedWeapons.FindIndex(c => c.name == card.name);
    //                 oneHandedWeapons[unselectedCard].DoUnselect();
    //                 oneHandedWeapons.RemoveAt(unselectedCard);
    //                 if (oneHandedWeapons.Count > 0)
    //                 {
    //                     for (int i = 0; i < oneHandedWeapons.Count; i++)
    //                     {
    //                         oneHandedWeapons[i].DoSelect(new Vector3(leftCardPosition + i * horizontalGap, topCardPosition, 0));
    //                     }
    //                 }
    //                 break;
    //             case Type.Shield:
    //                 unselectedCard = shields.FindIndex(c => c.name == card.name);
    //                 shields[unselectedCard].DoUnselect();
    //                 shields.RemoveAt(unselectedCard);
    //                 if (shields.Count > 0)
    //                 {
    //                     for (int i = 0; i < shields.Count; i++)
    //                     {
    //                         shields[i].DoSelect(new Vector3(leftCardPosition + i * horizontalGap, topCardPosition, 0));
    //                     }
    //                 }
    //                 break;
    //             case Type.Chest:
    //                 unselectedCard = chests.FindIndex(c => c.name == card.name);
    //                 chests[unselectedCard].DoUnselect();
    //                 chests.RemoveAt(unselectedCard);
    //                 if (chests.Count > 0)
    //                 {
    //                     for (int i = 0; i < chests.Count; i++)
    //                     {
    //                         chests[i].DoSelect(new Vector3(leftCardPosition + i * horizontalGap, topCardPosition - verticalGap, 0));
    //                     }
    //                 }
    //                 break;
    //             case Type.Ring:
    //                 unselectedCard = rings.FindIndex(c => c == card);
    //                 Debug.Log(unselectedCard);
    //                 rings[unselectedCard].DoUnselect();
    //                 rings.RemoveAt(unselectedCard);
    //                 if (rings.Count > 0)
    //                 {
    //                     for (int i = 0; i < rings.Count; i++)
    //                     {
    //                         rings[i].DoSelect(new Vector3(leftCardPosition + i * horizontalGap, topCardPosition - verticalGap * 3, 0));
    //                     }
    //                 }

    //                 break;
    //             case Type.Amulet:
    //                 unselectedCard = amulets.FindIndex(c => c.name == card.name);
    //                 amulets[unselectedCard].DoUnselect();
    //                 amulets.RemoveAt(unselectedCard);
    //                 if (amulets.Count > 0)
    //                 {
    //                     for (int i = 0; i < amulets.Count; i++)
    //                     {
    //                         amulets[i].DoSelect(new Vector3(leftCardPosition + i * horizontalGap, topCardPosition - verticalGap * 2, 0));
    //                     }
    //                 }
    //                 break;
    //         }
    //     }
    //     else
    //     {


    //         Vector3 position = new Vector3(0, 0, 0);
    //         if (card.card.type == Type.TwoHandedWeapon)
    //         {
    //             if (twoHandedWeapons.Count >= twoHandedWeaponLimit)
    //             {
    //                 twoHandedWeapons[0].DoUnselect();
    //                 twoHandedWeapons.RemoveAt(0);
    //             }
    //             if (oneHandedWeapons.Count > 0)
    //             {
    //                 for (int i = 0; i < oneHandedWeapons.Count; i++)
    //                 {
    //                     oneHandedWeapons[0].DoUnselect();
    //                     oneHandedWeapons.RemoveAt(0);
    //                 }
    //             }
    //             if (shields.Count > 0)
    //             {
    //                 for (int i = 0; i < shields.Count; i++)
    //                 {
    //                     shields[0].DoUnselect();
    //                     shields.RemoveAt(0);
    //                 }
    //             }
    //             twoHandedWeapons.Add(card);
    //             position = new Vector3(leftCardPosition + (horizontalGap * 0.5f), topCardPosition, 0);
    //         }
    //         else if (card.card.type == Type.OneHandedWeapon)
    //         {
    //             if (oneHandedWeapons.Count >= oneHandedWeaponLimit)
    //             {
    //                 oneHandedWeapons[0].DoUnselect();
    //                 oneHandedWeapons.RemoveAt(0);
    //             }
    //             if (twoHandedWeapons.Count > 0)
    //             {
    //                 for (int i = 0; i < twoHandedWeapons.Count; i++)
    //                 {
    //                     twoHandedWeapons[0].DoUnselect();
    //                     twoHandedWeapons.RemoveAt(0);
    //                 }
    //             }
    //             oneHandedWeapons.Add(card);
    //             position = new Vector3(leftCardPosition, topCardPosition, 0);

    //         }
    //         else if (card.card.type == Type.Shield)
    //         {
    //             if (shields.Count >= shieldLimit)
    //             {
    //                 shields[0].DoUnselect();
    //                 shields.RemoveAt(0);
    //             }
    //             if (twoHandedWeapons.Count > 0)
    //             {
    //                 for (int i = 0; i < twoHandedWeapons.Count; i++)
    //                 {
    //                     twoHandedWeapons[0].DoUnselect();
    //                     twoHandedWeapons.RemoveAt(0);
    //                 }
    //             }
    //             shields.Add(card);
    //             position = new Vector3(leftCardPosition + horizontalGap, topCardPosition, 0);

    //         }
    //         else if (card.card.type == Type.Chest)
    //         {
    //             if (chests.Count >= chestLimit)
    //             {
    //                 chests[0].DoUnselect();
    //                 chests.RemoveAt(0);
    //             }
    //             chests.Add(card);
    //             position = new Vector3(leftCardPosition + horizontalGap * 0.5f, topCardPosition - verticalGap, 0);

    //         }
    //         else if (card.card.type == Type.Amulet)
    //         {
    //             position = new Vector3(leftCardPosition + horizontalGap * 0.5f, topCardPosition - verticalGap * 2, 0);
    //             if (amulets.Count >= amuletLimit)
    //             {
    //                 amulets[0].DoUnselect();
    //                 amulets.RemoveAt(0);
    //             }

    //             amulets.Add(card);

    //         }
    //         else if (card.card.type == Type.Ring)
    //         {
    //             position = new Vector3(leftCardPosition, topCardPosition - verticalGap * 3, 0);
    //             if (rings.Count >= ringLimit)
    //             {
    //                 rings[0].DoUnselect();
    //                 rings.RemoveAt(0);
    //             }
    //             if (rings.Count == 1)
    //             {
    //                 position = new Vector3(leftCardPosition + horizontalGap, topCardPosition - verticalGap * 3, 0);
    //             }
    //             rings.Add(card);

    //         }

    //         // card.DoSelect(position);
    //         statCalc();
    //         FightHandler.instance.reCalculateStats();
    //     }
    // }
}
