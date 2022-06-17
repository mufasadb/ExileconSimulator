using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftHandler : MonoBehaviour
{
    public CardDisplay item;
    public CardDisplay currency;
    [SerializeField] GameObject acceptReward;
    [SerializeField] GameObject cantAcceptReward;
    [SerializeField] QuestHandler questHandler;
    // Start is called before the first frame update
    public void DoCraft()
    {
        if (item != null && currency != null)
        {
            if (currency.card.name == "Blacksmith Whetstone")
            {
                if (item.card.type == Type.OneHandedWeapon || item.card.type == Type.TwoHandedWeapon)
                {
                    //Craft quality handles the added stat and the images, just send weather or not its an item to dictate stat
                    item.CraftQuality(true);
                }
                else
                {
                    ThrowError("Blacksmith Whetstones can only be applied to weapons");
                }
                return;
            }
            if (currency.card.name == "Armourer Scrap")
            {
                if (item.card.type == Type.Chest || item.card.type == Type.Shield)
                {
                    //Craft quality handles the added stat and the images, just send weather or not its an item to dictate stat=
                    item.CraftQuality(false);
                }
                else
                {
                    ThrowError("Armourer Scraps can only be aplied to chest armour or shields");
                }
                return;
            }
            Rarity newRarity = Rarity.Magic;
            if (currency.card.name == "Alchemy Orb")
            {
                if (item.card.rarity == Rarity.Normal)
                {
                    newRarity = Rarity.Rare;
                }
                else { ThrowError("The item must be Normal to craft with an Alchemy Orb"); return; }
            }
            if (currency.card.name == "Transmutation Orb")
            {
                if (item.card.rarity == Rarity.Normal)
                {

                }
                else { ThrowError("The item must be Normal to craft with an Alchemy Orb"); return; }
            }
            if (currency.card.name == "Alteration Orb")
            {
                if (item.card.rarity == Rarity.Magic)
                {

                }
                else { ThrowError("The item must be Normal to craft with an Alchemy Orb"); return; }
            }
            if (currency.card.name == "Chaos Orb")
            {
                if (item.card.rarity == Rarity.Rare)
                {
                    newRarity = Rarity.Rare;
                }
                else { ThrowError("The item must be Normal to craft with an Alchemy Orb"); return; }
            }

            //do craft
            Hand.instance.hand.Remove(currency.gameObject);
            Destroy(currency.gameObject);
            CheckQuest();

            item.card.rarity = newRarity;
            item.craftingSticker.SetActive(true);
            for (int i = 0; i < item.explicitContainer.transform.childCount; i++)
            {
                Destroy(item.explicitContainer.transform.GetChild(i).gameObject);
            }
            item.card.explicits.makeExplicit(item.card.type, newRarity);
            item.card.explicits.StatDisplay(item.explicitContainer.transform);
        }
        else
        {
            Debug.LogError("can't craft without two items");
            return;
        }
    }
    void CheckQuest()
    {
        questHandler.MarkCraftQuestComplete(item.card.name, currency.card.name);
    }
    private void ThrowError(string error)
    {
        GlobalVariables.instance.errorHandler.NewError(error);
    }
    public void CancelCraft()
    {
        if (item != null) { item.DoUnselect(); }
        if (currency != null) { currency.DoUnselect(); }
        GameEventManager.instance.EndCraftScreen();
    }
    public void DisplayAcceptRewardButton(bool show)
    {
        acceptReward.SetActive(show);
        acceptReward.SetActive(!show);
    }
}
