using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapDeviceInteraction : Interactable
{
    [SerializeField] private Outline outline;

    private float clickCooldown = 0;

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
        if (clickCooldown <= 0)
        {
            clickCooldown = 1f;
            GlobalVariables.instance.atFrontOfQueue = false;
            GameEventManager.instance.ShowFastForward();
            GameEventManager.instance.AutoFastForward();
            QueueManager qMan = GetComponent<QueueManager>();
            GameObject player = GameObject.Find("Player");
            player.AddComponent<QueueMember_Player>();
            player.GetComponent<QueueMember_Player>().RegisterSelf(qMan);
            GameEventManager.instance.BeginMapScreen();
            // GameEventManager.instance.BeginCraftScreen();
        }
    }
    public override void Update()
    {
        base.Update();
        if (clickCooldown > 0) { clickCooldown -= Time.deltaTime; }
    }
}
