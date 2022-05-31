using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectionContainer : MonoBehaviour, IDropHandler
{
    public Type type;
    // Start is called before the first frame update
    void Start()
    {
        Color newColor = new Color(0.3f, 0.3f, 0.3f, 0.3f);
        GetComponent<Image>().color = newColor;
        GetComponentInChildren<Image>().color = newColor;

    }
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("dropped");
        if (eventData.pointerDrag != null)
        {
            eventData.pointerDrag.GetComponent<CardDisplay>().DoSelect(GetComponent<Transform>().position);
            Hand.instance.cardSelection.Select(eventData.pointerDrag.GetComponent<CardDisplay>());
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
