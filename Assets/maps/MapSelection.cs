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
                cardDisplay = _cardDisplay;
                MapHandler.instance.map = _cardDisplay;

            }
        }
    }
    public void MoveContainer(bool toCentre)
    {
        if (toCentre)
        {
            transform.localPosition = new Vector3(0, 50, 0);
            if (cardDisplay) { cardDisplay.destination = transform.position; }
        }
        else
        {
            transform.localPosition = new Vector3(50, 350, 0);
            if (cardDisplay) { cardDisplay.destination = transform.position; }
        }
    }
}
