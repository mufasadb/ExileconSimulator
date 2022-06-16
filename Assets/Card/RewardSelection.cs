using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RewardSelection : MonoBehaviour, IPointerDownHandler
{

    public void OnPointerDown(PointerEventData eventData)
    {
        if (GlobalVariables.instance.clipPendingCount < 1)
        {
            Hand.instance.AddCardToHand(this.gameObject);
            GlobalVariables.instance.RewardContainer.GetComponent<RewardHandler>().TakeReward();
        }
    }
    // IEnumerator
}
