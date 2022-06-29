using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerMotor : MonoBehaviour
{
    NavMeshAgent agent;
    public Transform target;
    CharacterAnimator characterAnimator;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        characterAnimator = GetComponent<CharacterAnimator>();
    }
    void Update()
    {
        if (target != null)
        {
            agent.SetDestination(target.position);
            FaceTarget(target);
        }
    }
    public void MoveToPoint(Vector3 point)
    {
        agent.SetDestination(point);
        if (characterAnimator) { characterAnimator.TargetChange(point); }
    }
    public void StopCollisions()
    {
        agent.obstacleAvoidanceType = ObstacleAvoidanceType.NoObstacleAvoidance;
        agent.avoidancePriority = 0;
    }
    public void StartCollisions()
    {
        agent.obstacleAvoidanceType = ObstacleAvoidanceType.MedQualityObstacleAvoidance;
        agent.avoidancePriority = 50;
    }
    public void StopMoving(){
        StopFollowingTarget();
        agent.destination = transform.position;
    }

    public void FollowTarget(Interactable newTarget)
    {
        agent.stoppingDistance = newTarget.radius * .8f;
        agent.updateRotation = false;
        target = newTarget.interactionTransform;

    }
    public void StopFollowingTarget()
    {
        agent.stoppingDistance = 0f;
        agent.updateRotation = true;
        target = null;
    }
    public void FaceTarget(Transform trans)
    {
        Vector3 direction = (trans.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0.01f, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }
}
