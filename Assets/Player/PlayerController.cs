using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
[RequireComponent(typeof(PlayerMotor))]
[RequireComponent(typeof(AudioButtonReference))]
public class PlayerController : MonoBehaviour
{
    Camera cam;
    public LayerMask movementMask;
    PlayerMotor motor;
    public Interactable focus;
    public WalkArrow walkArrow;
    AudioButtonReference audioRef;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        motor = GetComponent<PlayerMotor>();
        audioRef = GetComponent<AudioButtonReference>();
    }

    // Update is called once per frame
    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (Input.GetMouseButton(0) || Input.GetMouseButton(1))
        {
            if (!GlobalVariables.instance.preventMoving)
            {
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 100))
                {
                    Interactable interactable = hit.collider.GetComponent<Interactable>();
                    if (interactable != null)
                    {
                        SetFocus(interactable);
                        GlobalVariables.instance.standingAtFrontOfQueue = false;
                        audioRef.PlayInteractSound();
                        return;
                    }
                }
                //check if we hit an interactble - set it as focus if we do
            }
        }
        if (Input.GetMouseButton(0) || Input.GetMouseButton(1))
        {
            if (!GlobalVariables.instance.preventMoving)
            {
                if (!GlobalVariables.instance.preventMoving && !GlobalVariables.instance.currentlyDragging)
                {
                    Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit, 100, movementMask))
                    {
                        GlobalVariables.instance.standingAtFrontOfQueue = false;
                        //move our player to what we hit
                        motor.MoveToPoint(hit.point);
                        removeFocus();
                        walkArrow.TurnOnAtPoint(hit.point);
                        audioRef.PlayWalkSound();
                        // stop focus an item
                    }
                }
            }
        }
    }
    void SetFocus(Interactable newFocus)
    {
        if (newFocus != focus)
        {
            if (focus != null)
            {
                focus.OnDeFocused();
            }
            focus = newFocus;
            motor.FollowTarget(newFocus);
        }
        newFocus.OnFocused(transform);
    }
    void removeFocus()
    {
        if (focus != null)
        {
            focus.OnDeFocused();
        }
        focus = null;
        motor.StopFollowingTarget();
    }
}
