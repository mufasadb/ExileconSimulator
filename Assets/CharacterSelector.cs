using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelector : MonoBehaviour
{
    [SerializeField] private GameObject[] characters;
    // Start is called before the first frame update
    void Start()
    {
        TurnOneOn(characters);
    }
    void TurnOneOn(GameObject[] arrayOfObjects)
    {
        arrayOfObjects[Random.Range(0, arrayOfObjects.Length)].SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {

    }
}
