using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveTowardsTarget : MonoBehaviour
{
    public Transform Target;
    public string IsMovingParam = "IsMoving";

    private Animator animator;
    private NavMeshAgent agent;
    public GameObject Player;

    void Awake()
    {
        if (Target == null)
        {
            Target = GameObject.FindGameObjectWithTag("Player").transform;
        }
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }


    void OnEnable()
    {
        animator.SetBool(IsMovingParam, true);
        agent.isStopped = false;
    }

    void OnDisable()
    {
        animator.SetBool(IsMovingParam, false);
        agent.isStopped = true;
    }
	// Update is called once per frame
	void Update ()
	{
	    agent.SetDestination(Target.position);
	}
   
}
