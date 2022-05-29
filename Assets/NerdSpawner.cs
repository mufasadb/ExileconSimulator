using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NerdSpawner : MonoBehaviour
{
    public GameObject NerdCollection;
    public float queueGap = 0.5f;
    // Start is called before the first frame update

    private void DoStaffGen(Vector3 startPosition, Vector3 direction)
    {
        GameObject nerdPrefab = PrefabHolder.instance.NerdPrefab;
        for (int i = 0; i < 5; i++)
        {
            Instantiate(nerdPrefab, startPosition + direction * queueGap, Quaternion.identity, NerdCollection.transform);
        }

    }
    // Update is called once per frame

}
