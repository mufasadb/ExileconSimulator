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
    public void UnSelectAllCards()
    {
        RemoveAllType(Type.Shield);
        RemoveAllType(Type.OneHandedWeapon);
        RemoveAllType(Type.TwoHandedWeapon);
        RemoveAllType(Type.Amulet);
        RemoveAllType(Type.Ring);
        RemoveAllType(Type.Chest);
    }
    private void RemoveAllType(Type type)
    {
        switch (type)
        {
            case Type.TwoHandedWeapon:
                {
                    for (int i = 0; i < twoHandedWeapons.Count; i++)
                    {
                        twoHandedWeapons[0].DoUnselect();
                    }
                    break;
                }
            case Type.OneHandedWeapon:
                {
                    for (int i = 0; i < oneHandedWeapons.Count; i++)
                    {
                        oneHandedWeapons[0].DoUnselect();
                    }
                    break;
                }
            case Type.Shield:
                {
                    for (int i = 0; i < shields.Count; i++)
                    {
                        shields[0].DoUnselect();
                    }

                    break;
                }
            case Type.Chest:
                {
                    for (int i = 0; i < chests.Count; i++)
                    {
                        chests[0].DoUnselect();
                    }
                    break;
                }
            case Type.Amulet:
                {
                    for (int i = 0; i < amulets.Count; i++)
                    {
                        amulets[0].DoUnselect();
                    }
                    break;
                }
            case Type.Ring:
                {
                    for (int i = 0; i < rings.Count; i++)
                    {
                        rings[0].DoUnselect();
                    }
                    break;
                }
            default:
                break;
        }
    }
}
