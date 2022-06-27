using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastForward : MonoBehaviour
{

    public void SkipTime()
    {
        GameEventManager.instance.FastForward();
    }
}
