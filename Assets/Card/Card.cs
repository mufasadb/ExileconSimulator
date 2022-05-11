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
        if (tier > 4)
            Debug.LogWarning("Tier higher than 4 submitted");


        var data = ScriptableObject.CreateInstance<Card>();
        data.Init(tier, newname);
        return data;
    }
    public void Init(int tier, string newname)
    {
        this.rarity = genRarity(tier);
        this.type = genType(tier);
        // this.name = genName(this.type);
        this.name = newname;
        this.description = this.name;
        this.implicits = new Stats(StatsType.Implicits, this.type);
        this.explicits = new Stats(StatsType.Explicits, this.type);
    }
    public Rarity genRarity(int tier)
    {
        int rarityTotal = 0;
        foreach (int val in rarityChances) { rarityTotal += val * tier; }
        int randNumber = Random.Range(1, rarityTotal);
        if (randNumber < rarityChances[2] * tier) { return Rarity.Unique; }
        if (randNumber < rarityChances[1] * tier) { return Rarity.Rare; }
        if (randNumber < rarityChances[0] * tier) { return Rarity.Magic; }
        return Rarity.Normal;
    }
    public string genName(Type type) { return "Sword"; }
    public Type genType(int tier) { return Type.OneHandedWeapon; }

}
public enum Type { OneHandedWeapon, TwoHandedWeapon, Shield, Chest, Amulet, Ring }
public enum Rarity { Normal, Magic, Rare, Unique }
