using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class cardDrag : MonoBehaviour, IDragHandler, IPointerEnterHandler, IPointerExitHandler, IEndDragHandler, IBeginDragHandler, IDropHandler
{
    private CardDisplay cardDisplay;
    CanvasGroup canvasGroup;


    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        cardDisplay = GetComponent<CardDisplay>();
    }
    // Start is called before the first frame update
    public void OnDrag(PointerEventData eventData)
    {
        if (GlobalVariables.instance.clipPendingCount == 0 && GlobalVariables.instance.selectionState != SelectionState.InMaps)
        {
            cardDisplay.destination = eventData.position;
        }

    }
    public void OnDrop(PointerEventData eventData)
    {
        CardDisplay droppedCardDisplay = eventData.pointerDrag.GetComponent<CardDisplay>();
        if (cardDisplay.selected)
        {
            if (cardDisplay.card.type == Type.OneHandedWeapon || cardDisplay.card.type == Type.TwoHandedWeapon)
            {
                if (droppedCardDisplay.card.type == Type.OneHandedWeapon || droppedCardDisplay.card.type == Type.TwoHandedWeapon)
                {
                    cardDisplay.DoUnselect();
                    droppedCardDisplay.DoSelect(GetComponent<Transform>().position, GlobalVariables.instance.SelectionContainer);
                    Hand.instance.cardSelection.Select(droppedCardDisplay);
                }
            }
            else
            {
                if (cardDisplay.card.type == droppedCardDisplay.card.type)
                {
                    if (droppedCardDisplay.card.durability == 0)
                    {
                        GameEventManager.instance.DisplayError("This card cannot be used, as it is out of durability");
                    }
                    else
                    {
                        cardDisplay.DoUnselect();
                        droppedCardDisplay.DoSelect(GetComponent<Transform>().position, GlobalVariables.instance.SelectionContainer);
                        Hand.instance.cardSelection.Select(droppedCardDisplay);
                    }
                }
            }
        }
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (GlobalVariables.instance.clipPendingCount == 0 && GlobalVariables.instance.selectionState != SelectionState.InMaps)
        {
            canvasGroup.blocksRaycasts = false;
            cardDisplay.isDragged = true;
            if (cardDisplay.selected)
            {
                cardDisplay.DoUnselect();

            }
            GlobalVariables.instance.currentlyDragging = true;
            cardDisplay.Smallerise();
            cardDisplay.speed = 50f;
        }

    }
    public void OnEndDrag(PointerEventData eventData)
    {
        if (GlobalVariables.instance.clipPendingCount == 0 && GlobalVariables.instance.selectionState != SelectionState.InMaps)
        {

            // Debug.Log("end drag");
            cardDisplay.Smallerise();
            canvasGroup.blocksRaycasts = true;
            cardDisplay.isDragged = false;
            GlobalVariables.instance.currentlyDragging = false;
            if (!cardDisplay.selected) { cardDisplay.DoUnselect(); }
            cardDisplay.speed = 5f;
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!GlobalVariables.instance.currentlyDragging && GlobalVariables.instance.clipPendingCount == 0)
        {

            if (Hand.instance.cooldDownHover <= 0)
            {
                cardDisplay.Biggerise();
                Hand.instance.cooldDownHover = 0.01f;
            }
        }

    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (!cardDisplay.isDragged)
        {
            cardDisplay.Smallerise();

        }
    }

}
