using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectionContainer : MonoBehaviour, IDropHandler
{
    public Type type;
    public bool isUsed;
    [SerializeField] private SelectedJewellery selectedJewellery;
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
            if (cardDisplay.card.durability == 0)
            {
                GameEventManager.instance.DisplayError("This card cannot be used, as it is out of durability");
                return;
            }
            if (cardDisplay.card.type == Type.TwoHandedWeapon && type == Type.OneHandedWeapon)
            {
                if (cardDisplay.card.durability > 0)
                {
                    ActionSelection(cardDisplay);
                }
            }
            if (cardDisplay.card.type == type)
            {
                if (selectedJewellery)
                {
                    selectedJewellery.cardDisplay = cardDisplay;
                    selectedJewellery.UpdatePosition();
                }
                ActionSelection(cardDisplay);
            }
        }
    }
    public void ActionSelection(CardDisplay cardDisplay)
    {
        cardDisplay.DoSelect(GetComponent<Transform>().position, gameObject.transform.parent.gameObject);
        Hand.instance.cardSelection.Select(cardDisplay);
        cardDisplay.cardActionHandler.AttachToSelectionContainer(this.GetInstanceID());
        isUsed = true;
    }
    // Update is called once per frame
    void Update()
    {

    }
}
