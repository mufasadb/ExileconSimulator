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
    public CardDataObject GetCardBaseData(int tier, bool forMaps)
    {

        List<CardDataObject> specificTierItems = set.FindAll(item => item.tier == tier);
        List<CardDataObject> neighbourTierAbove = new List<CardDataObject>();
        if (!forMaps && tier != 4)
        {
            neighbourTierAbove = set.FindAll(item => item.tier == tier + 1);
        }

        return ChooseFromWeightedCards(specificTierItems.ToArray(), neighbourTierAbove.ToArray(), forMaps, tier);
    }
    public CardDataObject GetSpecificCard(string cardName)
    {
        return set.Find(c => c.name == cardName);
    }
    private CardDataObject ChooseFromWeightedCards(CardDataObject[] cardsMain, CardDataObject[] cardsNeighbourTierAbove, bool forMaps, int tier)
    {
        int[] weights = new int[cardsMain.Length + cardsNeighbourTierAbove.Length];
        int totalInt = 0;
        for (int i = 0; i < weights.Length; i++)
        {
            if (i < cardsMain.Length)
            {
                if (forMaps)
                {
                    weights[i] = cardsMain[i].mapRate;
                    totalInt += cardsMain[i].mapRate;
                }
                else
                {
                    weights[i] = cardsMain[i].rate;
                    totalInt += cardsMain[i].rate;
                }
            }
            else
            {
                if (forMaps)
                {
                    if (tier == 5)
                    {

                        weights[i] = Mathf.RoundToInt(cardsNeighbourTierAbove[i - cardsMain.Length].mapRate);
                        totalInt += Mathf.RoundToInt(cardsNeighbourTierAbove[i - cardsMain.Length].mapRate);
                    }
                    else
                    {

                        weights[i] = Mathf.RoundToInt(cardsNeighbourTierAbove[i - cardsMain.Length].mapRate * Settings.instance.hiddenNeighbourTierRateMulti);
                        totalInt += Mathf.RoundToInt(cardsNeighbourTierAbove[i - cardsMain.Length].mapRate * Settings.instance.hiddenNeighbourTierRateMulti);
                    }
                }
                else
                {
                    if (tier == 1 && GlobalVariables.instance.selectionState != SelectionState.InitialDeal)
                    {
                        weights[i] = Mathf.RoundToInt(cardsNeighbourTierAbove[i - cardsMain.Length].rate * 1.5f);
                        totalInt += Mathf.RoundToInt(cardsNeighbourTierAbove[i - cardsMain.Length].rate * 1.5f);
                    }
                    else
                    {
                        weights[i] = Mathf.RoundToInt(cardsNeighbourTierAbove[i - cardsMain.Length].rate * Settings.instance.hiddenNeighbourTierRateMulti);
                        totalInt += Mathf.RoundToInt(cardsNeighbourTierAbove[i - cardsMain.Length].rate * Settings.instance.hiddenNeighbourTierRateMulti);
                    }
                }
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