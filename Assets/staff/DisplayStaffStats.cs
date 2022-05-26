using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayStaffStats : MonoBehaviour
{
    public GameObject canvas;
    private Outline outline;
    // Start is called before the first frame update
    void Start()
    {
        outline = GetComponent<Outline>();
        hide();
    }
    private void OnMouseEnter()
    {
        show();
    }
    private void OnMouseExit()
    {
        hide();
    }
    // Update is called once per frame
    public void show()
    {
        canvas.SetActive(true);
        outline.enabled = true;
    }
    public void hide()
    {
        canvas.SetActive(false);
        outline.enabled = false;
    }
}
