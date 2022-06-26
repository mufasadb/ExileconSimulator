using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MapMods
{
    public Stats addedStats = new Stats();
    public int extraClipCount = 0;
    public List<int> monstersAffected = new List<int>();
    public Mods mod;
    public string description;
    public void RollMods(int tier, Rarity rarity)
    {
        this.mod = (Mods)Random.Range(0, 3);
        if (mod == Mods.ExtraClip) AddClip(tier, rarity);
        else { Stats(tier, rarity, mod); }
    }
    void AddClip(int tier, Rarity rarity)
    {
        int rarityBonus = 0;
        if (rarity == Rarity.Rare) rarityBonus = 1;
        for (int i = 0; i < tier + rarityBonus; i++)
        {
            monstersAffected.Add(Random.Range(0, 3));
        }
        monstersAffected.Distinct().ToList();
        DescribeClip();
    }
    void DescribeClip()
    {
        string list = "";
        monstersAffected.Sort();
        foreach (var mon in monstersAffected) { list += mon.ToString() + ", "; }
        description = "Monsters " + list + "do twice as much damage to items";
    }
    void Stats(int tier, Rarity rarity, Mods mod)
    {
        //monsters Attack Cant be physical (4) 
        //monsters defence cant be life or armour ( 3 or 4)
        //tier 1 magic should be 1.
        //tier 3 rare should be 4
        int addedDamageVal = tier;
        int chosenStat = Random.Range(0, 8);
        if (rarity == Rarity.Rare) addedDamageVal++;
        if (mod == Mods.Defence)
        {
            if (chosenStat == 4 | chosenStat == 5) chosenStat = 7;
        }
        else
        {
            if (chosenStat == 3) chosenStat = 7;
        }
        int[] declaredStats = { 0, 0, 0, 0, 0, 0, 0, 0 };
        declaredStats[chosenStat] = addedDamageVal;
        addedStats.DeclareStats(declaredStats);
        DescribeStats(addedDamageVal, chosenStat, mod);
    }
    void DescribeStats(int dam, int stat, Mods mod)
    {
        string attackOrDamage = "attack";
        if (mod == Mods.Defence) { attackOrDamage = "defence"; }
        string chosenStat;
        if (stat == 0) { chosenStat = " fire "; }
        if (stat == 1) { chosenStat = " cold "; }
        if (stat == 2) { chosenStat = " lightning "; }
        if (stat == 3) { chosenStat = " physical "; }
        if (stat == 4) { chosenStat = " life "; }
        if (stat == 5) { chosenStat = " armour "; }
        if (stat == 6) { chosenStat = " chaos "; }
        else { chosenStat = " wild "; }
        description = "The first monster in the map has " + dam + " added" + chosenStat + "damage on their " + attackOrDamage;
    }
}

public enum Mods { ExtraClip, Attack, Defence }
