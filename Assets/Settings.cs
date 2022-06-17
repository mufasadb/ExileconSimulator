using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
    #region Singleton

    public static Settings instance;
    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of Settings");
            return;
        }
        instance = this;
    }
    #endregion
    public TimeDisplay timePreference;
}
public enum TimeDisplay
{
    DHMRemaining,
    Time,
    TimeMil,
    HMRemaining,
    MRemaining
}
