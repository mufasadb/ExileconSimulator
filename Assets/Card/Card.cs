using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Card")]
public class Card : ScriptableObject
{
    public new string name;

    private int[] rarityChances = new int[] { 60, 20, 1 };

    public string description;

    public Stats implicits;

    public Stats explicits;

    public Type type;

    public int durability = 2;

    public Rarity rarity;

    public static Card CreateInstance(int tier, string newname)
    {
        // if (tier > ) Debug.LogWarning("Tier higher than 4 submitted");

        var data = ScriptableObject.CreateInstance<Card>();
        data.Init(tier, newname);
        return data;
    }

    public void Init(int tier, string newname)
    {
        this.rarity = genRarity(tier);
        CardDataObject cardData =
            CardDataSystem.instance.cardDataSet.GetCardBaseData(tier);
        this.type = cardData.type;
        if (this.type == Type.Currency) { this.rarity = Rarity.Currency; }
        if(cardData.isUnique){this.rarity = Rarity.Unique;}
        //Enable this line to name cards by creation (1, 2, 3, etc.)
        // this.name = newname;
        this.name = cardData.name;
        this.description = this.name;
        if (cardData.type != Type.Currency && cardData.type != Type.Map)
        {
            this.implicits = new Stats();
            this.implicits.DeclareStats(StatStringToIntArray(cardData.implicits));
            this.explicits = new Stats();
            this.explicits.makeExplicit(this.type, this.rarity);
        }
    }

    private int[] StatStringToIntArray(string str)
    {
        // int fire, int cold, int lightning, int physical, int life, int armour, int chaos, int wild
        int[] ints = new int[8];
        string[] stringArray = str.Split(",");
        for (int i = 0; i < stringArray.Length; i++)
        {
            if (stringArray[i] == "Fire")
            {
                ints[0]++;
            }
            if (stringArray[i] == "Cold")
            {
                ints[1]++;
            }
            if (stringArray[i] == "Lightning")
            {
                ints[2]++;
            }
            if (stringArray[i] == "Physical")
            {
                ints[3]++;
            }
            if (stringArray[i] == "Life")
            {
                ints[4]++;
            }
            if (stringArray[i] == "Armour")
            {
                ints[5]++;
            }
            if (stringArray[i] == "Chaos")
            {
                ints[6]++;
            }
            if (stringArray[i] == "Wild")
            {
                ints[7]++;
            }
        }
        return ints;
    }

    public Rarity genRarity(int tier)
    {
        int rarityTotal = 0;
        foreach (int val in rarityChances)
        {
            rarityTotal += val * tier;
        }
        int randNumber = Random.Range(1, rarityTotal);
        if (randNumber < rarityChances[1] * tier)
        {
            return Rarity.Rare;
        }
        if (randNumber < rarityChances[0] * tier)
        {
            return Rarity.Magic;
        }
        return Rarity.Normal;
    }

    public string genName(Type type)
    {
        return "Sword";
    }

    public Type genType(int tier)
    {
        return Type.OneHandedWeapon;
    }
}

public enum Type
{
    OneHandedWeapon,
    TwoHandedWeapon,
    Shield,
    Chest,
    Amulet,
    Ring,
    Currency,
    Map
}

public enum Rarity
{
    Currency,
    Normal,
    Magic,
    Rare,
    Unique
}
