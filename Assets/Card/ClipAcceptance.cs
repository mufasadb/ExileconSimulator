using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClipAcceptance : MonoBehaviour, IPointerDownHandler
{

    public void OnPointerDown(PointerEventData eventData)
    {
        GlobalVariables.instance.clipPendingCount -= 1;
        GetComponentInParent<CardDisplay>().DoUnselect();
        Destroy(this);
    }

}
