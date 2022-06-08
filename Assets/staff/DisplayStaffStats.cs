using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayStaffStats : MonoBehaviour
{
    public GameObject canvas;
    private Outline outline;
    bool isLocked = false;
    // Start is called before the first frame update
    void Start()
    {
        outline = GetComponentInChildren<Outline>();
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
        if (!isLocked)
        {
            canvas.SetActive(true);
            outline.enabled = true;
        }
    }
    public void hide()
    {
        if (!isLocked)
        {
            canvas.SetActive(false);
            outline.enabled = false;
        }
    }
    public void showNLock()
    {
        canvas.SetActive(true);
        isLocked = true;
    }
    public void hideNUnlock()
    {
        canvas.SetActive(false);
        isLocked = false;
    }
}
