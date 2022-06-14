using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public class MonsterDataSet
{
    public List<MonsterDataObject> set = new List<MonsterDataObject>();

    public MonsterDataObject GetMonsterBaseData(int tier)
    {
        List<MonsterDataObject> specificTierItems = set.FindAll(item => item.tier == tier);
        int choice = Random.Range(0, specificTierItems.Count);
        return specificTierItems[choice];
    }
}
