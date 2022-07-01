using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioButtonReference : MonoBehaviour
{
    float buttonCLickCD = 0f;
    float walkCD = 0f;
    float interactCD = 0f;

    public void PlayWalkSound()
    {
        if (walkCD <= 0)
        {
            AudioManager.instance.Play("move");
            walkCD = 0.5f;
        }
    }
    public void PlayButtonSound()
    {
        if (buttonCLickCD <= 0)
        {
            AudioManager.instance.Play("buttonClick");
            buttonCLickCD = 0.1f;
        }
    }
    public void PlayInteractSound()
    {
        if (interactCD <= 0)
        {
            AudioManager.instance.Play("interact");
            interactCD = 0.1f;
        }
    }

    void Update()
    {
        if (buttonCLickCD > 0) buttonCLickCD -= Time.deltaTime;
        if (walkCD > 0) walkCD -= Time.deltaTime;
        if (interactCD > 0) interactCD -= Time.deltaTime;
    }
}
