using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MapSelection : MonoBehaviour, IDropHandler
{
    // Start is called before the first frame update
    public bool currencySlot;
    public CardDisplay cardDisplay;
    // [SerializeField] 
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            CardDisplay _cardDisplay = eventData.pointerDrag.GetComponent<CardDisplay>();
            if (_cardDisplay.card.type == Type.Map)
            {
                _cardDisplay.DoSelect(transform.position, gameObject.transform.parent.gameObject);
                // Debug.Log("do map stuff");
                cardDisplay = _cardDisplay;
                // GlobalVariables.
                // gameObject.transform.parent.GetComponent<CraftHandler>().currency = cardDisplay;
                // cardDisplay.DoSelect(GetComponent<Transform>().position, gameObject.transform.parent.gameObject);
            }
        }
    }
    public void MoveContainer(bool toCentre)
    {
        if (toCentre)
        {
            transform.position = new Vector3(0, 50, 0);
            if (cardDisplay) { cardDisplay.destination = transform.position; }
        }
        else
        {
            transform.position = new Vector3(0, 350, 0);
            if (cardDisplay) { cardDisplay.destination = transform.position; }
        }
    }
}
