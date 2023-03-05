using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayOneShotBehavior : StateMachineBehaviour
{

    public AudioClip soundPlay;
    public float volume = 0.5f;
    public bool playOnEnter = true; 
    public bool playOnExit = false; 
    public bool playAfterDelay = false;

    public float PlayDelay = 0.25f;
    private float timeSinceEntered = 0;
    private bool  hasDelaySoundPlayed = false;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(playOnEnter){
            AudioSource.PlayClipAtPoint(soundPlay, animator.gameObject.transform.position, volume);
        }
        timeSinceEntered = 0f;
        hasDelaySoundPlayed = false;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(playAfterDelay && hasDelaySoundPlayed){
            timeSinceEntered += Time.deltaTime;
            if(timeSinceEntered >PlayDelay){
                AudioSource.PlayClipAtPoint(soundPlay, animator.gameObject.transform.position, volume);
                hasDelaySoundPlayed = true;
            }
        }
        
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(playOnExit){
            AudioSource.PlayClipAtPoint(soundPlay, animator.gameObject.transform.position, volume);
        }
    }

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
