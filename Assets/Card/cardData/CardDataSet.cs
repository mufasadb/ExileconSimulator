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
        // List<CardDataObject> neighbourTierBelow = set.FindAll(item => item.tier == tier);
        List<CardDataObject> neighbourTierAbove = set.FindAll(item => item.tier == tier + 1);
        return ChooseFromWeightedCards(specificTierItems.ToArray(), neighbourTierAbove.ToArray());
    }
    private CardDataObject ChooseFromWeightedCards(CardDataObject[] cardsMain, CardDataObject[] cardsNeighbourTierAbove)
    {
        int[] weights = new int[cardsMain.Length + cardsNeighbourTierAbove.Length];
        int totalInt = 0;
        for (int i = 0; i < weights.Length; i++)
        {
            if (i < cardsMain.Length)
            {
                weights[i] = cardsMain[i].rate;
                totalInt += cardsMain[i].rate;
            }
            else
            {
                weights[i] = Mathf.RoundToInt(cardsNeighbourTierAbove[i - cardsMain.Length].rate * Settings.instance.hiddenNeighbourTierRateMulti);
                totalInt += Mathf.RoundToInt(cardsNeighbourTierAbove[i - cardsMain.Length].rate * Settings.instance.hiddenNeighbourTierRateMulti);
            }
        }
        int selectedVal = Random.Range(0, totalInt);
        for (int i = 0; i < weights.Length; i++)
        {
            if (selectedVal < weights[i])
            {
                if (i < cardsMain.Length) return cardsMain[i];
                else return cardsNeighbourTierAbove[i - cardsMain.Length];
            }
            else
            {
                selectedVal -= weights[i];
            }
        }
        Debug.LogError("failed to find a proper card, returning last card");
        return cardsMain[cardsMain.Length - 1];
    }
}