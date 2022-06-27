using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

#if UNITY_EDITOR
using UnityEditor;
#endif // UNITY_EDITOR
public enum State { Idle, Wander, GoToQueue, InQueue }

[RequireComponent(typeof(AwarenessSystem))]
public class EnemyAI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI FeedbackDisplay;

    [SerializeField] float _VisionConeAngle = 60f;
    [SerializeField] float _VisionConeRange = 30f;
    [SerializeField] Color _VisionConeColour = new Color(1f, 0f, 0f, 0.25f);

    [SerializeField] float _HearingRange = 20f;
    [SerializeField] Color _HearingRangeColour = new Color(1f, 1f, 0f, 0.25f);

    [SerializeField] float _ProximityDetectionRange = 3f;
    [SerializeField] Color _ProximityRangeColour = new Color(1f, 1f, 1f, 0.25f);


    public Vector3 EyeLocation => transform.position;
    public Vector3 EyeDirection => transform.forward;

    public float VisionConeAngle => _VisionConeAngle;
    public float VisionConeRange => _VisionConeRange;
    public Color VisionConeColour => _VisionConeColour;

    public float HearingRange => _HearingRange;
    public Color HearingRangeColour => _HearingRangeColour;

    public float ProximityDetectionRange => _ProximityDetectionRange;
    public Color ProximityDetectionColour => _ProximityRangeColour;

    public float CosVisionConeAngle { get; private set; } = 0f;
    public State state = State.Idle;
    public int tier = 1;
    public DetectableTarget lastFaughtTarget;


    AwarenessSystem Awareness;

    void Awake()
    {
        CosVisionConeAngle = Mathf.Cos(VisionConeAngle * Mathf.Deg2Rad);
        Awareness = GetComponent<AwarenessSystem>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void FinishedFightingStaff(DetectableTarget foughtStaff)
    {
        int oldTier = tier;
        lastFaughtTarget = foughtStaff;
        Goal_JoinQueue joinQ = GetComponent<Goal_JoinQueue>();
        if (joinQ)
        {
            joinQ.CurrentTarget = null;
            // joinQ.OnGoalDeactivated();
        }
        state = State.Idle;
        FeedbackDisplay.text = "I'm done fighting now";

        //tier represents tier of staff but 6 is crafting and 7 is maps


        if (tier == 6) { tier = Random.Range(2, 4); }
        else if (tier == 7)
        {
            if (Random.Range(0, 2) < 1) tier = 5;
        }
        else if (tier == 4)
        {
            if (Random.Range(0, 20) < 1) tier = 7;
        }
        else
        {
            if (Random.Range(0, 15) < 1) tier++;
        }
        if (Random.Range(0, 10) < 1) tier = 6;
        if (tier > 7) Debug.LogError("an ai got to 4");


        Awareness.ChangedTier();


        // Debug.Log(Awareness.Targets.Count);
    }
    public void Wandering()
    {
        state = State.Wander;
        FeedbackDisplay.text = "wandering";
    }
    public void Idling()
    {
        state = State.Idle;
        FeedbackDisplay.text = "now Idle";
    }
    public void ToQueue()
    {
        state = State.GoToQueue;
        FeedbackDisplay.text = "On my way to the queue for the ";
    }
    public void JoinQueue()
    {
        state = State.InQueue;
        FeedbackDisplay.text = "I'm in the Queue now";
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(EnemyAI))]
public class EnemyAIEditor : Editor
{
    public void OnSceneGUI()
    {
        var ai = target as EnemyAI;

        // draw the detectopm range
        Handles.color = ai.ProximityDetectionColour;
        Handles.DrawSolidDisc(ai.transform.position, Vector3.up, ai.ProximityDetectionRange);

        // draw the hearing range
        Handles.color = ai.HearingRangeColour;
        Handles.DrawSolidDisc(ai.transform.position, Vector3.up, ai.HearingRange);

        // work out the start point of the vision cone
        Vector3 startPoint = Mathf.Cos(-ai.VisionConeAngle * Mathf.Deg2Rad) * ai.transform.forward +
                             Mathf.Sin(-ai.VisionConeAngle * Mathf.Deg2Rad) * ai.transform.right;

        // draw the vision cone
        Handles.color = ai.VisionConeColour;
        Handles.DrawSolidArc(ai.transform.position, Vector3.up, startPoint, ai.VisionConeAngle * 2f, ai.VisionConeRange);
    }
}
#endif // UNITY_EDITOR