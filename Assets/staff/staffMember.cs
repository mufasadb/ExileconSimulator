using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaffMember : ScriptableObject
{
    public new string name;
    public Stats defence;
    public Stats attack;

    public static StaffMember CreateInstance(int tier)
    {
        var data = ScriptableObject.CreateInstance<StaffMember>();
        data.defence = new Stats();
        data.attack = new Stats();
        data.defence.createStaffDefence(1);
        data.attack.createStaffAttack(1);
        return data;
    }
}
