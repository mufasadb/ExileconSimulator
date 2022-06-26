using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterAgent))]
public class AnimateNerd : MonoBehaviour
{
    CharacterAgent characterAgent;
    Animator anim;

    public void Awake()
    {
        characterAgent = GetComponent<CharacterAgent>();
        anim = GetComponentInChildren<Animator>();
    }
    public void Update()
    {
        if (characterAgent.IsMoving) anim.SetBool("isMoving", true);
        else { anim.SetBool("isMoving", false); }
    }
}
