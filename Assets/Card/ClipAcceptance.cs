using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClipAcceptance : MonoBehaviour, IPointerDownHandler
{

    public void OnPointerDown(PointerEventData eventData)
    {
        GlobalVariables.instance.clipPendingCount -= 1;
        if (GlobalVariables.instance.selectionState == SelectionState.InMaps)
        {
            GetComponentInParent<CardActionHandler>().GoToHomeContainer();
        }
        else
        {
            GetComponentInParent<CardDisplay>().DoUnselect();
        }
        Destroy(this);
    }

}
