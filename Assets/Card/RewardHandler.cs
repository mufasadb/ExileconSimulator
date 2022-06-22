using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardHandler : MonoBehaviour
{
    [SerializeField] Card hydraFrag;
    [SerializeField] Card chimeraFrag;
    [SerializeField] Card PhoenixFrag;
    [SerializeField] Card MinotaurFrag;
    [SerializeField] Card Level1Map;
    public void DoReward(int numberOfCardsOffered, int tier, int takeCount)
    {
        GameEventManager.instance.BeginRewardScreen();

        for (int i = 0; i < numberOfCardsOffered; i++)
        {
            CreateRewardCard(i, tier, takeCount);
        }
        GlobalVariables.instance.rewardPendingCount = takeCount;

    }
    public void TakeReward()
    {

        GlobalVariables.instance.rewardPendingCount -= 1;
        if (GlobalVariables.instance.rewardPendingCount == 0)
        {

            for (int i = 0; i < transform.childCount; i++)
            {
                GameObject card = transform.GetChild(i).gameObject;
                Destroy(card);
            }
            GameEventManager.instance.StepToNextFight();
        }

    }
    private void CreateRewardCard(int rewardNumber, int tier, int takeCount)
    {
        GameObject cardPrefab = PrefabHolder.instance.CardPrefab;
        CardDisplay cardDisplay = cardPrefab.GetComponent<CardDisplay>();
        cardDisplay.card = Card.CreateInstance(tier, "null");
        GameObject newCard = Instantiate(cardPrefab, new Vector3(0, 0, 0), Quaternion.identity, Hand.instance.handContainer.transform);

        //tell it where to go (to the hand container roughly)
        newCard.GetComponent<CardDisplay>().parentContainer = Hand.instance.handContainer;
        newCard.GetComponent<CardDisplay>().DoUnselect();
        newCard.GetComponent<CardDisplay>().destination = GlobalVariables.instance.RewardContainer.transform.position + new Vector3(rewardNumber * 250 - 250, 0, 0);
        newCard.transform.SetParent(GlobalVariables.instance.RewardContainer.transform);
        newCard.AddComponent<RewardSelection>();
        newCard.GetComponent<Animator>().SetBool("facingForward", true);
    }
    public void DoSpecificReward(string reward)
    {
        GlobalVariables.instance.rewardPendingCount = 1;
        GameObject cardPrefab = PrefabHolder.instance.CardPrefab;
        CardDisplay cardDisplay = cardPrefab.GetComponent<CardDisplay>();
        if (reward == "Fragment of Hydra") cardDisplay.card = hydraFrag;
        if (reward == "Fragment of Phoenix") cardDisplay.card = hydraFrag;
        if (reward == "Fragment of Chimera") cardDisplay.card = hydraFrag;
        if (reward == "Fragment of Minotaur") cardDisplay.card = hydraFrag;
        GameObject newCard = Instantiate(cardPrefab, new Vector3(0, 0, 0), Quaternion.identity, Hand.instance.handContainer.transform);
        newCard.GetComponent<CardDisplay>().parentContainer = Hand.instance.handContainer;
        newCard.GetComponent<CardDisplay>().DoUnselect();
        newCard.GetComponent<CardDisplay>().destination = GlobalVariables.instance.RewardContainer.transform.position + new Vector3(0 * 250 - 250, 0, 0);
        newCard.transform.SetParent(GlobalVariables.instance.RewardContainer.transform);
        newCard.AddComponent<RewardSelection>();
        newCard.GetComponent<Animator>().SetBool("facingForward", true);
    }
}
