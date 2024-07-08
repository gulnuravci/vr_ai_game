using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JolleenIdleBehavior : StateMachineBehaviour
{
    [SerializeField]
    private float _timeUntilBored;

    [SerializeField]
    private int _numberOfBoredAnimations;

    private bool _isBored;
    private float _idleTime;
    private int _boredAnimation;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       ResetIdle();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // If not bored
        if (_isBored == false){
            // Increase idle time
            _idleTime += Time.deltaTime;
            // If been idle long enough to become bored and ensure that the code for becoming bored is only executed at the very beginning of an animation loop (first 2% of current loop). It prevents the bored logic from being triggered multiple times during the same loop.
            if (_idleTime > _timeUntilBored && stateInfo.normalizedTime % 1 < 0.02f){
                // Set bored
                _isBored = true;
                // Choose random bored animation
                _boredAnimation = Random.Range(1, _numberOfBoredAnimations + 1);
                // Accounting for the extra idle animation between every bored animation for smoother transitions
                _boredAnimation = (_boredAnimation * 2) - 1;
                // Update 'boredAnimation' parameter in 'Idle' blend tree in animator to closest default bored animation
                animator.SetFloat("BoredAnimation", _boredAnimation-1);
            }
        }
        // If bored and animation is playing. Normalized time of animation tells you how far along in animation (0 to 1). Since there are multiple animations, the float will go up to 2 for the second animation, etc. Modulo by 1 to get remainder to see how far along in this specific animation
        else if (stateInfo.normalizedTime % 1 > 0.98){
            // Reset to default idle animation
            ResetIdle();
        }
        // Update 'boredAnimation' parameter in 'Idle' blend tree in animator to bored animation
        animator.SetFloat("BoredAnimation", _boredAnimation, 0.2f, Time.deltaTime);
    }

    private void ResetIdle(){
        // If bored
        if (_isBored){
            // Go back to closest previous default idle animation
            _boredAnimation--;
        }

        _isBored = false;
        _idleTime = 0;
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
