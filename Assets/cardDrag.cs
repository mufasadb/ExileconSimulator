using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class cardDrag : MonoBehaviour, IDragHandler, IPointerEnterHandler, IPointerExitHandler, IEndDragHandler, IBeginDragHandler
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
        cardDisplay.destination = eventData.position;

    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        // canvasGroup.blocksRaycasts = false;
        cardDisplay.isDragged = true;
        cardDisplay.DoUnselect();
        GlobalVariables.instance.currentlyDragging = true;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("end drag");
        cardDisplay.Smallerise();
        // canvasGroup.blocksRaycasts = true;
        cardDisplay.isDragged = false;
        GlobalVariables.instance.currentlyDragging = false;
        if (!cardDisplay.selected) { cardDisplay.DoUnselect(); Debug.Log("unselectingAgain"); }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!GlobalVariables.instance.currentlyDragging)
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
