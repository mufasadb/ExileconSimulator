using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Card")]
public class Card : ScriptableObject
{
    public new string name;
    //rarity chance by tier
    int[] rareChance = { 2, 5, 10, 10, 10 };
    int[] magicChance = { 5, 10, 15, 20, 25 };

    public string description;

    public Stats implicits;

    public Stats explicits;
    public int extraDraw;
    public int extraTakes;

    public Type type;
    public MapMods mapMods;

    public int durability = 2;

    public Rarity rarity;
    public Stats quality = new Stats();
    public bool isCrafted = false;
    public int mapTier;
    public string extraDescription;

    public static Card CreateInstance(int tier, bool inMaps)
    {
        // if (tier > ) Debug.LogWarning("Tier higher than 4 submitted");

        var data = ScriptableObject.CreateInstance<Card>();
        data.Init(tier, inMaps);
        return data;
    }
    public static Card CreateSpecificInstance(string cardName)
    {

        var data = ScriptableObject.CreateInstance<Card>();
        data.Init(cardName);
        return data;
    }


    public void Init(string cardName)
    {
        this.rarity = genRarity(1);
        CardDataObject cardData = CardDataSystem.instance.cardDataSet.GetSpecificCard(cardName);
        this.type = cardData.type;
        if (this.type == Type.Currency) { this.rarity = Rarity.Currency; }
        if (cardData.isUnique) { this.rarity = Rarity.Unique; }
        this.name = cardData.name;
        this.description = this.name;
        this.extraDescription = cardData.extraDescription;
        this.mapTier = cardData.mapTier;
        if (cardData.type != Type.Currency && cardData.type != Type.Map)
        {
            this.implicits = new Stats();
            this.implicits.DeclareStats(StatStringToIntArray(cardData.implicits));
            this.explicits = new Stats();
            this.explicits.makeExplicit(this.type, this.rarity);
        }
        if (cardData.type == Type.Map && this.rarity != Rarity.Normal)
        {
            mapMods = new MapMods();
            mapMods.RollMods(this.mapTier, this.rarity); this.extraDescription = mapMods.description;
        }
        if (cardData.isUnique)
        {
            UniqueDataObject uniqueDataObject = CardDataSystem.instance.uniqueDataSet.GetExplicitStringByUniqueName(cardData.name);
            this.explicits.DeclareStats(StatStringToIntArray(uniqueDataObject.explicits));
            this.extraDraw = uniqueDataObject.extraDraws;
            this.extraTakes = uniqueDataObject.extraTakes;
        }
    }
    public void Init(int tier, bool forMaps)
    {
        this.rarity = genRarity(tier);
        CardDataObject cardData = CardDataSystem.instance.cardDataSet.GetCardBaseData(tier, forMaps);
        this.type = cardData.type;
        if (this.type == Type.Currency) { this.rarity = Rarity.Currency; }
        if (cardData.isUnique) { this.rarity = Rarity.Unique; }
        //Enable this line to name cards by creation (1, 2, 3, etc.)
        // this.name = newname;
        this.name = cardData.name;
        this.description = this.name;
        this.extraDescription = cardData.extraDescription;
        this.mapTier = cardData.mapTier;
        if (cardData.type != Type.Currency && cardData.type != Type.Map)
        {
            this.implicits = new Stats();
            this.implicits.DeclareStats(StatStringToIntArray(cardData.implicits));
            this.explicits = new Stats();
            this.explicits.makeExplicit(this.type, this.rarity);
        }
        if (cardData.type == Type.Map) { mapMods = new MapMods(); mapMods.RollMods(this.mapTier, this.rarity); this.extraDescription = mapMods.description; }
        if (cardData.isUnique)
        {
            UniqueDataObject uniqueDataObject = CardDataSystem.instance.uniqueDataSet.GetExplicitStringByUniqueName(cardData.name);
            this.explicits.DeclareStats(StatStringToIntArray(uniqueDataObject.explicits));
            this.extraDraw = uniqueDataObject.extraDraws;
            this.extraTakes = uniqueDataObject.extraTakes;
        }
    }
    public void RollMapMods()
    {
        mapMods = new MapMods();
        mapMods.RollMods(mapTier, rarity);
        extraDescription = mapMods.description;
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


        int randNumber = Random.Range(1, 100);
        if (randNumber < rareChance[tier - 1])
        {
            return Rarity.Rare;
        }

        if (randNumber < magicChance[tier - 1])
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
    Map,
    Tool
}

public enum Rarity
{
    Currency,
    Normal,
    Magic,
    Rare,
    Unique
}
