using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkArrow : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    public void TurnOnAtPoint(Vector3 destination)
    {
        transform.position = destination;
        gameObject.SetActive(true);
        StartCoroutine(TurnOff());
    }
    IEnumerator TurnOff()
    {
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
    }
}
