using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapDeviceInteraction : Interactable
{
    [SerializeField] private Outline outline;


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
        outline.enabled = true;
    }
    public void hide()
    {
        outline.enabled = false;
    }
    public override void Interact()
    {
        GameEventManager.instance.BeginMapScreen();
    }
}
