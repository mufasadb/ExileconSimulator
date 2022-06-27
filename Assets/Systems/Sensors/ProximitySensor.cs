using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyAI))]
[RequireComponent(typeof(AwarenessSystem))]
public class ProximitySensor : MonoBehaviour
{
    EnemyAI LinkedAI;
    AwarenessSystem awareness;
    // Start is called before the first frame update
    void Start()
    {
        LinkedAI = GetComponent<EnemyAI>();
        awareness = GetComponent<AwarenessSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        for (int index = 0; index < DetectableTargetManager.Instance.AllTargets.Count; ++index)
        {
            var candidateTarget = DetectableTargetManager.Instance.AllTargets[index];

            if (candidateTarget.tier != LinkedAI.tier) continue;
            if (candidateTarget == LinkedAI.lastFaughtTarget) continue;
            awareness.StoreTarget(candidateTarget);
        }
    }
}
