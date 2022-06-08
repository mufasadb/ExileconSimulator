using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RewardSelection : MonoBehaviour, IPointerDownHandler
{

    public void OnPointerDown(PointerEventData eventData)
    {
        Hand.instance.AddCardToHand(this.gameObject);
        // GlobalVariables.instance.RewardContainer.SetActive(false);
        GameEventManager.instance.EndRewardScreen();
    }
}
