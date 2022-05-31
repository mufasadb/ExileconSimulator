using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NerdSpawner : MonoBehaviour
{
    public GameObject NerdCollection;
    public float queueGap = 0.5f;
    // Start is called before the first frame update

    public void DoNerdGen(Vector3 startPosition, Vector3 direction, int quant, Transform lookAtTarget)
    {
        // Debug.Log(direction);
        GameObject nerdPrefab = PrefabHolder.instance.NerdPrefab;
        for (int i = 0; i < quant; i++)
        {
            GameObject clone = 
            Instantiate(nerdPrefab, startPosition + (direction * queueGap * (i + 1)), Quaternion.identity, NerdCollection.transform);
            clone.transform.LookAt(lookAtTarget, Vector3.up);
        }

    }
    // Update is called once per frame

}
