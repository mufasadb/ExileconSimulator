using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(CardActionHandler))]
public class cardDrag : MonoBehaviour, IDragHandler, IPointerEnterHandler, IPointerExitHandler, IEndDragHandler, IBeginDragHandler, IDropHandler, IPointerClickHandler
{
    private CardDisplay cardDisplay;
    CardActionHandler cardActionHandler;
    CanvasGroup canvasGroup;
    float doubleClickTracker;
    public float doubleClickMinimum = 0.7f;

    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        cardDisplay = GetComponent<CardDisplay>();
        cardActionHandler = GetComponent<CardActionHandler>();
    }
    private void Update()
    {
        if (doubleClickTracker > 0) doubleClickTracker -= Time.deltaTime;
        if (doubleClickTracker < 0) doubleClickTracker = 0;
    }
    // Start is called before the first frame update
    public void OnDrag(PointerEventData eventData)
    {
        if (GlobalVariables.instance.clipPendingCount == 0 && GlobalVariables.instance.selectionState != SelectionState.InMaps && !GlobalVariables.instance.cardAnimation)
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
    public void OnPointerClick(PointerEventData eventData)
    {

        if (doubleClickTracker > 0)
        {
            if (cardDisplay.selected) { cardDisplay.DoUnselect(); }
            else
            {
                cardActionHandler.AutoSelect();
            }
        }
        else
        {
            doubleClickTracker = doubleClickMinimum;
        }
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (GlobalVariables.instance.clipPendingCount == 0 && GlobalVariables.instance.selectionState != SelectionState.InMaps && !GlobalVariables.instance.cardAnimation)
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
        if (GlobalVariables.instance.clipPendingCount == 0 && GlobalVariables.instance.selectionState != SelectionState.InMaps && !GlobalVariables.instance.cardAnimation)
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
        if (!GlobalVariables.instance.currentlyDragging && !GlobalVariables.instance.cardAnimation)
        {

            if (Hand.instance.cooldDownHover <= 0)
            {
                AudioManager.instance.Play("cardSlide");
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
