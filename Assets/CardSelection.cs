using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSelection : MonoBehaviour
{
    private int twoHandedWeaponLimit = 1;
    private int oneHandedWeaponLimit = 1;
    private int shieldLimit = 1;
    private int chestLimit = 1;
    private int amuletLimit = 1;
    private int ringLimit = 2;
    private List<CardDisplay> twoHandedWeapons = new List<CardDisplay>();
    private List<CardDisplay> shields = new List<CardDisplay>();
    private List<CardDisplay> amulets = new List<CardDisplay>();
    private List<CardDisplay> oneHandedWeapons = new List<CardDisplay>();
    private List<CardDisplay> rings = new List<CardDisplay>();
    private List<CardDisplay> chests = new List<CardDisplay>();
    private void Start()
    {

    }
    public void Select(CardDisplay card)
    {
        Vector3 position = new Vector3(0, 0, 0);
        if (card.card.type == Type.TwoHandedWeapon)
        {
            if (twoHandedWeapons.Count >= twoHandedWeaponLimit)
            {
                twoHandedWeapons[0].DoUnselect();
                twoHandedWeapons.RemoveAt(0);
            }
            if (oneHandedWeapons.Count > 0)
            {
                for (int i = 0; i < oneHandedWeapons.Count; i++)
                {
                    oneHandedWeapons[0].DoUnselect();
                    oneHandedWeapons.RemoveAt(0);
                }
            }
            if (shields.Count > 0)
            {
                for (int i = 0; i < shields.Count; i++)
                {
                    shields[0].DoUnselect();
                    shields.RemoveAt(0);
                }
            }
            twoHandedWeapons.Add(card);
            position = new Vector3(0, 125, 0);
        }
        else if (card.card.type == Type.OneHandedWeapon)
        {
            if (oneHandedWeapons.Count >= oneHandedWeaponLimit)
            {
                oneHandedWeapons[0].DoUnselect();
                oneHandedWeapons.RemoveAt(0);
            }
            if (twoHandedWeapons.Count > 0)
            {
                for (int i = 0; i < twoHandedWeapons.Count; i++)
                {
                    twoHandedWeapons[0].DoUnselect();
                    twoHandedWeapons.RemoveAt(0);
                }
            }
            oneHandedWeapons.Add(card);
            position = new Vector3(-70, 125, 0);

        }
        else if (card.card.type == Type.Shield)
        {
            if (shields.Count >= shieldLimit)
            {
                shields[0].DoUnselect();
                shields.RemoveAt(0);
            }
            if (twoHandedWeapons.Count > 0)
            {
                for (int i = 0; i < twoHandedWeapons.Count; i++)
                {
                    twoHandedWeapons[0].DoUnselect();
                    twoHandedWeapons.RemoveAt(0);
                }
            }
            shields.Add(card);
            position = new Vector3(70, 125, 0);

        }
        else if (card.card.type == Type.Amulet)
        {
            position = new Vector3(0, -75, 0);
            if (amulets.Count >= amuletLimit)
            {
                amulets[0].DoUnselect();
                amulets.RemoveAt(0);
            }

            amulets.Add(card);

        }
        else if (card.card.type == Type.Ring)
        {
            position = new Vector3(-70, -250, 0);
            if (rings.Count >= ringLimit)
            {
                rings[0].DoUnselect();
                rings.RemoveAt(0);
            }
            if (rings.Count == 1)
            {
                position = new Vector3(70, -250, 0);
            }
            rings.Add(card);

        }
        else if (card.card.type == Type.Chest)
        {
            if (chests.Count >= chestLimit)
            {
                chests[0].DoUnselect();
                chests.RemoveAt(0);
            }
            chests.Add(card);
            position = new Vector3(0, 0, 0);

        }
        card.DoSelect(position);
    }
}
