using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaffMember : ScriptableObject
{
    public new string name;
    public Stats defence;
    public Stats attack;
    public ClipMethod clipMethod;
    public int clipCount;
    public int staffQueueSize;

    public static StaffMember CreateInstance(int tier)
    {
        var data = ScriptableObject.CreateInstance<StaffMember>();
        data.Init(Random.Range(1, 5));
        return data;
    }
    public void Init(int tier)
    {
        var staffDetails = StaffDataSystem.instance.staffDataSet.GetStaffBaseData(tier);
        this.staffQueueSize = Random.Range(0, 5);
        this.name = staffDetails.name;
        this.clipCount = staffDetails.clipCount;
        this.clipMethod = (ClipMethod)System.Enum.Parse(typeof(ClipMethod), staffDetails.clipMethod);
        this.defence = new Stats();
        this.defence.DeclareStats(StatStringToIntArray(staffDetails.defence));
        this.attack = new Stats();
        this.attack.DeclareStats(StatStringToIntArray(staffDetails.attack));
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

public enum ClipMethod { Rare, Common, Shield, Chest, Jewellery, All, Weapon, Broken }