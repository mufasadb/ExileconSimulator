using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CraftingSelection : MonoBehaviour, IDropHandler
{
    // Start is called before the first frame update
    public bool currencySlot;
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("dropping");
        if (eventData.pointerDrag != null)
        {

            CardDisplay cardDisplay = eventData.pointerDrag.GetComponent<CardDisplay>();
            if (currencySlot)
            {
                if (cardDisplay.card.type == Type.Currency)
                {
                    gameObject.transform.parent.GetComponent<CraftHandler>().currency = cardDisplay;
                    cardDisplay.DoSelect(GetComponent<Transform>().position, gameObject.transform.parent.gameObject);

                }
            }
            else
            {
                if (cardDisplay.card.rarity != Rarity.Unique && cardDisplay.card.type != Type.Currency)
                {
                    gameObject.transform.parent.GetComponent<CraftHandler>().item = cardDisplay;
                    cardDisplay.DoSelect(GetComponent<Transform>().position, gameObject.transform.parent.gameObject);
                }
            }
        }
    }
}
