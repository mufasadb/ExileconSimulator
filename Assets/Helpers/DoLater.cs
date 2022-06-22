using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class DoLater
{
    public static IEnumerator DoAfterXSeconds(float seconds, Action func)
    {
        yield return new WaitForSeconds(seconds);
        func();
    }
}
