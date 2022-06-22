using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clippers : MonoBehaviour
{
    public void TurnMeOff()
    {
        StartCoroutine(
        DoLater.DoAfterXSeconds(1.5f,
        () =>
        {
            gameObject.SetActive(false);
        }
        ));
    }
}
