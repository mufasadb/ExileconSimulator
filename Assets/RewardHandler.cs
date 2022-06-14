using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardHandler : MonoBehaviour
{
    public void DoReward(int numberOfCardsOffered, int tier)
    {
        GameEventManager.instance.BeginRewardScreen();
        for (int i = 0; i < numberOfCardsOffered; i++)
        {
            CreateRewardCard(i, tier);
        }
        GlobalVariables.instance.rewardPending = false;

    }
    public void TakeReward()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject card = transform.GetChild(i).gameObject;
            Destroy(card);
        }
        GlobalVariables.instance.rewardPending = false;
        GameEventManager.instance.StepToNextFight();

    }
    private void CreateRewardCard(int rewardNumber, int tier)
    {
        GameObject cardPrefab = PrefabHolder.instance.CardPrefab;
        CardDisplay cardDisplay = cardPrefab.GetComponent<CardDisplay>();
        cardDisplay.card = Card.CreateInstance(tier, "null");
        GameObject newCard = Instantiate(cardPrefab, new Vector3(0, 0, 0), Quaternion.identity, GlobalVariables.instance.handContainer.transform);
        newCard.GetComponent<CardDisplay>().destination = GlobalVariables.instance.RewardContainer.transform.position + new Vector3(rewardNumber * 250 - 250, 0, 0);
        newCard.AddComponent<RewardSelection>();
        newCard.GetComponent<CardDisplay>().updatePositionScaleCaches(rewardNumber);
        newCard.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
        newCard.transform.SetParent(GlobalVariables.instance.RewardContainer.transform);
    }
}
