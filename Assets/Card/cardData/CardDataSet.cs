using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class CardDataSet
{
    public List<CardDataObject> set = new List<CardDataObject>();
    // public void AddCard(){
    //     set.Add(new CardDataObject());
    // }
    public CardDataObject GetCardBaseData(int tier)
    {
        // if (tier > 3) { Debug.LogError("Tried to created a card with tier greater than 3"); return null; }
        List<CardDataObject> specificTierItems = set.FindAll(item => item.tier == tier);
        int choice = Random.Range(0, specificTierItems.Count);
        return specificTierItems[choice];
    }
}