using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftHandler : MonoBehaviour
{
    public CardDisplay item;
    public CardDisplay currency;
    // Start is called before the first frame update
    public void DoCraft()
    {
        if (item != null && currency != null)
        {
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
            Hand.instance.hand.Remove(currency.card);
            Destroy(currency.gameObject);


            item.card.rarity = newRarity;
            item.craftingSticker.SetActive(true);
            for (int i = 0; i < item.explicitContainer.transform.childCount; i++)
            {
                Destroy(item.explicitContainer.transform.GetChild(i));
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
}
