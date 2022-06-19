using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSet : MonoBehaviour
{
    #region Singleton

    public static EventSet instance;
    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of GlobalVariables");
            return;
        }
        instance = this;
    }
    #endregion

    // public List<TimedEvent> timedEvents = new List<TimedEvent>();
    public TimedEvent[] timedEvents;


    public void Start()
    {
        foreach (TimedEvent tevent in timedEvents)
        {
            tevent.createNextTimedEvent();
        }
    }

    public void Update()
    {
        float time = GlobalVariables.instance.timer;
        foreach (TimedEvent tevent in timedEvents)
        {
            if (time < tevent.nextTimedEvent)
            {
                //trigger event
                // takeaway time

                tevent.createNextTimedEvent();
                GlobalVariables.instance.errorHandler.NewError(tevent.notification);
                // Debug.Log(tevent.notification);
            }
        }
    }
}
