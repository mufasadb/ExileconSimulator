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
    private float verticalGap = 140;
    private float topCardPosition = 900;
    private float leftCardPosition = 150;
    private float horizontalGap = 150;
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
        if (card.selected)
        {
            int unselectedCard = 0;
            switch (card.card.type)
            {
                case Type.TwoHandedWeapon:
                    unselectedCard = twoHandedWeapons.FindIndex(c => c.name == card.name);
                    twoHandedWeapons[unselectedCard].DoUnselect();
                    twoHandedWeapons.RemoveAt(unselectedCard);
                    if (twoHandedWeapons.Count > 0)
                    {
                        for (int i = 0; i < twoHandedWeapons.Count; i++)
                        {
                            twoHandedWeapons[i].DoSelect(new Vector3(leftCardPosition + i * horizontalGap, topCardPosition, 0));
                        }
                    }
                    break;

                case Type.OneHandedWeapon:
                    unselectedCard = oneHandedWeapons.FindIndex(c => c.name == card.name);
                    oneHandedWeapons[unselectedCard].DoUnselect();
                    oneHandedWeapons.RemoveAt(unselectedCard);
                    if (oneHandedWeapons.Count > 0)
                    {
                        for (int i = 0; i < oneHandedWeapons.Count; i++)
                        {
                            oneHandedWeapons[i].DoSelect(new Vector3(leftCardPosition + i * horizontalGap, topCardPosition, 0));
                        }
                    }
                    break;
                case Type.Shield:
                    unselectedCard = shields.FindIndex(c => c.name == card.name);
                    shields[unselectedCard].DoUnselect();
                    shields.RemoveAt(unselectedCard);
                    if (shields.Count > 0)
                    {
                        for (int i = 0; i < shields.Count; i++)
                        {
                            shields[i].DoSelect(new Vector3(leftCardPosition + i * horizontalGap, topCardPosition, 0));
                        }
                    }
                    break;
                case Type.Chest:
                    unselectedCard = chests.FindIndex(c => c.name == card.name);
                    chests[unselectedCard].DoUnselect();
                    chests.RemoveAt(unselectedCard);
                    if (chests.Count > 0)
                    {
                        for (int i = 0; i < chests.Count; i++)
                        {
                            chests[i].DoSelect(new Vector3(leftCardPosition + i * horizontalGap, topCardPosition - verticalGap, 0));
                        }
                    }
                    break;
                case Type.Ring:
                    unselectedCard = rings.FindIndex(c => c == card);
                    Debug.Log(unselectedCard);
                    rings[unselectedCard].DoUnselect();
                    rings.RemoveAt(unselectedCard);
                    if (rings.Count > 0)
                    {
                        for (int i = 0; i < rings.Count; i++)
                        {
                            rings[i].DoSelect(new Vector3(leftCardPosition + i * horizontalGap, topCardPosition - verticalGap * 3, 0));
                        }
                    }

                    break;
                case Type.Amulet:
                    unselectedCard = amulets.FindIndex(c => c.name == card.name);
                    amulets[unselectedCard].DoUnselect();
                    amulets.RemoveAt(unselectedCard);
                    if (amulets.Count > 0)
                    {
                        for (int i = 0; i < amulets.Count; i++)
                        {
                            amulets[i].DoSelect(new Vector3(leftCardPosition + i * horizontalGap, topCardPosition - verticalGap * 2, 0));
                        }
                    }
                    break;
            }
        }
        else
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
                position = new Vector3(leftCardPosition + (horizontalGap * 0.5f), topCardPosition, 0);
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
                position = new Vector3(leftCardPosition, topCardPosition, 0);

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
                position = new Vector3(leftCardPosition + horizontalGap, topCardPosition, 0);

            }
            else if (card.card.type == Type.Chest)
            {
                if (chests.Count >= chestLimit)
                {
                    chests[0].DoUnselect();
                    chests.RemoveAt(0);
                }
                chests.Add(card);
                position = new Vector3(leftCardPosition + horizontalGap * 0.5f, topCardPosition - verticalGap, 0);

            }
            else if (card.card.type == Type.Amulet)
            {
                position = new Vector3(leftCardPosition + horizontalGap * 0.5f, topCardPosition - verticalGap * 2, 0);
                if (amulets.Count >= amuletLimit)
                {
                    amulets[0].DoUnselect();
                    amulets.RemoveAt(0);
                }

                amulets.Add(card);

            }
            else if (card.card.type == Type.Ring)
            {
                position = new Vector3(leftCardPosition, topCardPosition - verticalGap * 3, 0);
                if (rings.Count >= ringLimit)
                {
                    rings[0].DoUnselect();
                    rings.RemoveAt(0);
                }
                if (rings.Count == 1)
                {
                    position = new Vector3(leftCardPosition + horizontalGap, topCardPosition - verticalGap * 3, 0);
                }
                rings.Add(card);

            }

            card.DoSelect(position);
        }
    }
}
