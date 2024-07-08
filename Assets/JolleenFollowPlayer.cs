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

    // public Transform player;
    // public float followDelay = 0.5f;
    // public float followDistance = 2f;
    // public float rotationSpeed = 5f;
    
    // private Queue<Vector3> playerPositions;
    // private Queue<Quaternion> playerRotations;

    // // Start is called before the first frame update
    // void Start()
    // {
    //     playerPositions = new Queue<Vector3>();
    //     playerRotations = new Queue<Quaternion>();
    //     StartCoroutine(FollowPlayer());
    // }

    // // Update is called once per frame
    // void Update()
    // {
    //     // Record the player's position and rotation at each frame
    //     playerPositions.Enqueue(player.position);
    //     playerRotations.Enqueue(player.rotation);
    // }

    // IEnumerator FollowPlayer(){
    //     while (true){
    //         // If there are recorded positions and the queue has enough elements to cover the delay
    //         if (playerPositions.Count > followDelay / Time.deltaTime){
    //             // Move towards the position recorded after the delay
    //             Vector3 targetPosition = playerPositions.Dequeue();
    //             Vector3 followPosition = Vector3.MoveTowards(transform.position, targetPosition, followDistance * Time.deltaTime);

    //             // Rotate towards the rotation recorded after the delay
    //             Quaternion targetRotation = playerRotations.Dequeue();
    //             transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
                
    //             transform.position = followPosition;
    //         }

    //         // Wait for the next frame
    //         yield return null;
    //     }
    // }
}
