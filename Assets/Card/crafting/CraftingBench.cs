using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingBench : Interactable
{

    // Start is called before the first frame update
    [SerializeField]private Outline outline;
    // Start is called before the first frame update
    void Start()
    {
        // outline = GetComponentInParent<Outline>();
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
        outline.enabled = true;
    }
    public void hide()
    {
        outline.enabled = false;
    }
    public override void Interact()
    {
        GameEventManager.instance.BeginCraftScreen();
    }
    // Update is called once per frame
}
