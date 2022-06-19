using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class TimedEvent
{
    public string name;
    public int triggerEveryXHours = 8;
    public int triggerCount = -1;
    public int triggerLimit = 1;
    public int timeToSkip = 30;
    public string notification = "no notifcation set";
    public int nextTimedEvent = 960;
    public void createNextTimedEvent()
    {
        if (triggerCount < triggerLimit)
        {
            triggerCount++;
            nextTimedEvent -= triggerEveryXHours * 60;
        }
        else
        {
            nextTimedEvent = -10;
        }
    }
}
