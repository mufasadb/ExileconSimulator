using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(QueueManager))]
public class DetectableTarget : MonoBehaviour
{
    public int tier;
    public float interactRange = 3f;
    public QueueManager qMan;

    // Start is called before the first frame update
    void Start()
    {
        qMan = GetComponent<QueueManager>();
        DetectableTargetManager.Instance.Register(this);
        if (tier == 0)
        {
            tier = GetComponent<staffDetails>().staffData.tier;
        }
        if (tier == 0) { Debug.LogError("wasnt able to find the tier for the staff member"); }

    }


    // Update is called once per frame

    void OnDestroy()
    {
        if (DetectableTargetManager.Instance != null)
            DetectableTargetManager.Instance.Deregister(this);
    }
}
