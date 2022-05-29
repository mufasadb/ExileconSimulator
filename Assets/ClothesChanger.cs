using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClothesChanger : MonoBehaviour
{
    [SerializeField] GameObject[] shirts;
    [SerializeField] GameObject[] hair;
    [SerializeField] GameObject[] pants;
    [SerializeField] GameObject[] shoes;
    [SerializeField] GameObject[] optionals;
    // Start is called before the first frame update
    void Start()
    {
        TurnOneOn(shirts);
        TurnOneOn(hair);
        TurnOneOn(pants);
        TurnOneOn(shoes);
        // OptionalOns(optionals);
        TurnOneOn(optionals);
    }
    void TurnOneOn(GameObject[] arrayOfObjects)
    {
        arrayOfObjects[Random.Range(0, arrayOfObjects.Length)].SetActive(true);
    }
    void OptionalOns(GameObject[] arrayOfObjects)
    {
        int startingIndex = Random.Range(0, arrayOfObjects.Length);
        for (int i = 0; i < arrayOfObjects.Length; i++)
        {
        
            int actingIndex = startingIndex + i;
            if (actingIndex > arrayOfObjects.Length) { actingIndex -= arrayOfObjects.Length + 1; }
            Debug.Log(actingIndex);
            arrayOfObjects[actingIndex].SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
