using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class CharacterAnimator : MonoBehaviour
{
    Animator animator;
    NavMeshAgent agent;
    // PlayerMotor playerMotor;
    Vector3 target;

    [Range(0.001f, 0.1f)]
    public float lowerIdleThreshold = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        // PlayerMotor playerMotor = GetComponentInParent<PlayerMotor>();
        Transform target;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
    }
    public void TargetChange(Vector3 _target)
    {
        target = _target;
    }
    // Update is called once per frame
    void Update()
    {
        float speedPerecent = agent.velocity.magnitude / agent.speed;

        if (target != null)
        {
            float angle = Vector3.SignedAngle(agent.transform.position, target, agent.transform.up);
            animator.SetFloat("angularSpeed", angle);
        }
        //the slow down is the .1f can put that as an editable
        animator.SetFloat("speedPercent", speedPerecent, .1f, Time.deltaTime);
        if (speedPerecent < lowerIdleThreshold) { animator.SetBool("isMoving", false); } else { animator.SetBool("isMoving", true); }
    }
}
