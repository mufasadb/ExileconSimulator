using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CraftHandler : MonoBehaviour
{
    public CardDisplay item;
    public CardDisplay currency;
    [SerializeField] GameObject acceptReward;
    [SerializeField] QuestHandler questHandler;
    [SerializeField] Button buttonComponent;
    // Start is called before the first frame update
    public void DoCraft()
    {
        if (!GlobalVariables.instance.atFrontOfQueue)
        {
            GlobalVariables.instance.errorHandler.NewError("you must be at the front of the queue, fast forward to get there faster!");
            return;
        }
        if (item != null && currency != null)
        {
            if (currency.card.name == "Blacksmith Whetstone")
            {
                if (item.card.type == Type.OneHandedWeapon || item.card.type == Type.TwoHandedWeapon)
                {
                    if (item.card.isCrafted)
                    {
                        ThrowError("Blacksmith Whestones can't be applied to already qualitied weapons");
                        return;
                    }
                    //Craft quality handles the added stat and the images, just send weather or not its an item to dictate stat
                    item.CraftQuality(true);
                }
                else
                {
                    ThrowError("Blacksmith Whetstones can only be applied to weapons");
                    return;
                }
            }
            if (currency.card.name == "Armourer Scrap")
            {
                if (item.card.type == Type.Chest || item.card.type == Type.Shield)
                {
                    if (item.card.isCrafted)
                    {
                        ThrowError("Armourer Scraps can't be applied to already qualitied Armour pieces");
                        return;
                    }
                    //Craft quality handles the added stat and the images, just send weather or not its an item to dictate stat=
                    item.CraftQuality(false);
                }
                else
                {
                    ThrowError("Armourer Scraps can only be aplied to chest armour or shields");
                    return;
                }
            }
            Rarity newRarity = item.card.rarity;
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
                    newRarity = Rarity.Magic;
                }
                else { ThrowError("The item must be Normal to craft with an Alchemy Orb"); return; }
            }
            if (currency.card.name == "Alteration Orb")
            {
                if (item.card.rarity == Rarity.Magic)
                {
                    item.card.rarity = Rarity.Normal;
                    newRarity = Rarity.Magic;
                }
                else { ThrowError("The item must be Normal to craft with an Alchemy Orb"); return; }
            }
            if (currency.card.name == "Chaos Orb")
            {
                if (item.card.rarity == Rarity.Rare)
                {
                    item.card.rarity = Rarity.Normal;
                    newRarity = Rarity.Rare;
                }
                else { ThrowError("The item must be Normal to craft with an Alchemy Orb"); return; }
            }
            if (currency.card.name == "Scour Orb")
            {
                if (item.card.rarity == Rarity.Magic || item.card.rarity == Rarity.Rare)
                {
                    newRarity = Rarity.Normal;
                }
                else { ThrowError("The item must be rare or magic to craft with a soure orb"); }

            }

            //do craft
            Hand.instance.hand.Remove(currency.gameObject);
            Destroy(currency.gameObject);
            CheckQuest();

            if (item.card.rarity != newRarity)
            {

                item.card.rarity = newRarity;
                item.craftingSticker.SetActive(true);
                if (item.card.type != Type.Map)
                {

                    for (int i = 0; i < item.explicitContainer.transform.childCount; i++)
                    {
                        Destroy(item.explicitContainer.transform.GetChild(i).gameObject);
                    }
                    item.card.explicits.makeExplicit(item.card.type, newRarity);
                    item.card.explicits.StatDisplay(item.explicitContainer.transform);
                }
                else
                {
                    item.card.RollMapMods();
                    item.descriptiveText.text = item.card.extraDescription;
                }
            }
        }
        else
        {
            Debug.LogError("can't craft without two items");
            return;
        }
    }
    public void Update()
    {
        if (!GlobalVariables.instance.atFrontOfQueue)
        {
            buttonComponent.enabled = false;
            buttonComponent.gameObject.GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
            acceptReward.GetComponent<Button>().enabled = false;
            acceptReward.GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
        }
        else
        {
            buttonComponent.enabled = true;
            buttonComponent.gameObject.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            acceptReward.GetComponent<Button>().enabled = true;
            acceptReward.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        }

    }
    void CheckQuest()
    {
        questHandler.MarkCraftQuestComplete(item.card.type.ToString(), currency.card.name);
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
    }
}
