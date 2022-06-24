using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : ScriptableObject
{
    public new string name;
    public Stats defence;
    public Stats attack;
    public ClipMethod clipMethod;
    public int clipCount;
    public int tier { get; private set; }

    public static Monster CreateInstance(int tier)
    {
        var data = ScriptableObject.CreateInstance<Monster>();

        MonsterDataObject monsterDataObject = MonsterDataSystem.instance.monsterDataSet.GetMonsterBaseData(tier);
        if(monsterDataObject == null){
            Debug.LogError("couldnt find amonster with tier " + tier);
        }
        // data.Init(Random.Range(1, 5));
        data.Init(monsterDataObject);
        return data;
    }
    public void Init(MonsterDataObject monsterDataObject)
    {
        this.name = monsterDataObject.name;
        this.clipCount = 1;
        this.clipMethod = (ClipMethod)Random.Range(0, 8);
        if(this.clipMethod == ClipMethod.All) this.clipMethod = ClipMethod.Common;
        // this.clipCount = monsterDataObject.clipCount;
        // this.clipMethod = (ClipMethod)System.Enum.Parse(typeof(ClipMethod), monsterDataObject.clipMethod);
        this.defence = new Stats();
        this.defence.DeclareStats(StatStringToIntArray(monsterDataObject.defence));
        this.attack = new Stats();
        this.attack.DeclareStats(StatStringToIntArray(monsterDataObject.attack));
        this.tier = monsterDataObject.tier;
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
}
