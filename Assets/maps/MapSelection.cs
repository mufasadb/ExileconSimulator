using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MapSelection : MonoBehaviour, IDropHandler
{
    // Start is called before the first frame update
    public bool currencySlot;
    // [SerializeField] 
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            CardDisplay cardDisplay = eventData.pointerDrag.GetComponent<CardDisplay>();
            if (currencySlot)
            {
                if (cardDisplay.card.type == Type.Map)
                {
                    Debug.Log("do map stuff");
                    // GlobalVariables.
                    // gameObject.transform.parent.GetComponent<CraftHandler>().currency = cardDisplay;
                    // cardDisplay.DoSelect(GetComponent<Transform>().position, gameObject.transform.parent.gameObject);

                }
            }
        }
    }
}
