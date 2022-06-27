using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    // Start is called before the first frame update
    public float radius = 3f;
    bool isFocus = false;
    public Transform interactionTransform;
    bool hasInteracted = false;
    Transform player;
    public virtual void Interact()
    {
        //this method is meant to be overwritten
        Debug.Log("No interact overwritten");
    }
    void Start()
    {
        if (!interactionTransform) { interactionTransform = transform; }
    }
    public virtual void Update()
    {
        if (isFocus && !hasInteracted)
        {
            float distance = Vector3.Distance(player.position, interactionTransform.position);
            if (distance <= radius)
            {
                Interact();
                hasInteracted = true;
            }
        }
    }
    public void OnFocused(Transform playerTransform)
    {
        isFocus = true;
        player = playerTransform;
        hasInteracted = false;
    }
    public void OnDeFocused()
    {
        player = null;
        isFocus = false;
        hasInteracted = false;
    }

    private void OnDrawGizmosSelected()
    {
        if (!interactionTransform) { interactionTransform = transform; }
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(interactionTransform.position, radius);
    }
}
