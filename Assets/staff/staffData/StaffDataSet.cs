using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class StaffDataSet
{
    public List<StaffDataObject> set = new List<StaffDataObject>();
    // public void AddCard(){
    //     set.Add(new CardDataObject());
    // }
    public StaffDataObject GetStaffBaseData(int tier)
    {
        // if (tier > 3) { Debug.LogError("Tried to get a card with tier greater than 3"); return null; }
        List<StaffDataObject> specificTierItems = set.FindAll(item => item.tier == tier);
        int choice = Random.Range(0, specificTierItems.Count);
        return specificTierItems[choice];
    }
}