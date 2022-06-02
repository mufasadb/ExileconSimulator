using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectionContainer : MonoBehaviour, IDropHandler
{
    public Type type;
    // Start is called before the first frame update
    void Start()
    {
        Color newColor = new Color(0.3f, 0.3f, 0.3f, 07f);
        GetComponent<Image>().color = newColor;
        GetComponentInChildren<Image>().color = newColor;

    }
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            CardDisplay cardDisplay = eventData.pointerDrag.GetComponent<CardDisplay>();
            if (cardDisplay.card.type == Type.TwoHandedWeapon && type == Type.OneHandedWeapon)
            {
                cardDisplay.DoSelect(GetComponent<Transform>().position, gameObject.transform.parent.gameObject);
                Hand.instance.cardSelection.Select(cardDisplay);
            }
            if (cardDisplay.card.type == type)
            {
                cardDisplay.DoSelect(GetComponent<Transform>().position, gameObject.transform.parent.gameObject);
                Hand.instance.cardSelection.Select(cardDisplay);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
