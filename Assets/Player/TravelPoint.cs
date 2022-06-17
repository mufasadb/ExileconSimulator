using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TravelPoint : Interactable
{
    [SerializeField] Outline outline;
    [SerializeField] Transform destination;
    [SerializeField] GameObject playerObj;
    // Start is called before the first frame update
    public override void Interact()
    {
        NavMeshAgent agent = playerObj.GetComponent<NavMeshAgent>();
        PlayerMotor motor = playerObj.GetComponent<PlayerMotor>();
        motor.target = destination;

        agent.Warp(destination.position);
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
}
