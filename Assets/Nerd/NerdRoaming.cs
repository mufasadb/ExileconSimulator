using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NerdRoaming : MonoBehaviour
{
    [SerializeField] private List<Transform> destinations = new List<Transform>();
    PlayerMotor motor;
    public Interactable focus;
    public float TimeAtLocation = 19;
    public float TimeToWaitPerLocation = 20;
    [SerializeField] private NavMeshAgent agent;
    private Animator animator;
    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        motor = GetComponent<PlayerMotor>();
    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (TimeAtLocation > TimeToWaitPerLocation)
        {
            motor.MoveToPoint(destinations[Random.Range(0, destinations.Count)].position);

            // removeFocus();
        }
        if (agent.velocity.magnitude < 0.01)
        {
            animator.SetBool("isMoving", false);
            TimeAtLocation += Time.deltaTime;
        }
        if (agent.velocity.magnitude > 0.01)
        {
            animator.SetBool("isMoving", true);
            TimeAtLocation = 0;
        }
    }
}
