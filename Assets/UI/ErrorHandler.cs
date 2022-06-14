using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ErrorHandler : MonoBehaviour
{
    public TextMeshProUGUI textMesh;
    // Start is called before the first frame update
    void Start()
    {
        // textMesh = GetComponentInChildren<TextMeshPro>();
    }

    // Update is called once per frame
    public void NewError(string error)
    {
        textMesh.text = error;
        gameObject.SetActive(true);
    }
    public void CloseError()
    {
        gameObject.SetActive(false);
    }
}
