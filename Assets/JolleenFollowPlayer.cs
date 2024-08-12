using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class JolleenFollowPlayer : MonoBehaviour
{
    public Transform target;
    public NavMeshAgent agent;
    public Animator animator;

    void Start(){
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    void Update(){
        agent.destination = target.position;
        animator.SetBool("IsWalking", agent.velocity.magnitude > 0.01f);
    }
}
