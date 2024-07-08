using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JolleenSemanticAnimations : MonoBehaviour
{
    public Animator animator;
    public Toggle toggleHappy;
    public Toggle toggleSad;
    public Toggle toggleAngry;
    public Toggle toggleGreeting;
    public Toggle toggleProud;
    public Toggle toggleTalking;
    public Toggle toggleDisapprove;
    public Toggle toggleThinking;

    // Start is called before the first frame update
    void Start()
    {
        // ----Add listeners to call the appropriate functions when toggle values change----
        toggleHappy.onValueChanged.AddListener(OnToggleHappyValueChanged);
        toggleSad.onValueChanged.AddListener(OnToggleSadValueChanged);
        toggleAngry.onValueChanged.AddListener(OnToggleAngryValueChanged);
        toggleGreeting.onValueChanged.AddListener(OnToggleGreetingValueChanged);
        toggleProud.onValueChanged.AddListener(OnToggleProudValueChanged);
        toggleTalking.onValueChanged.AddListener(OnToggleTalkingValueChanged);
        toggleDisapprove.onValueChanged.AddListener(OnToggleDisapproveValueChanged);
        toggleThinking.onValueChanged.AddListener(OnToggleThinkingValueChanged);
    }

    // // ----Set the animator's bool parameter based on the toggle state----

    void OnToggleHappyValueChanged(bool isOn){
        animator.SetBool("IsHappy", isOn);
    }

    void OnToggleSadValueChanged(bool isOn){
        animator.SetBool("IsSad", isOn);
    }
    void OnToggleAngryValueChanged(bool isOn){
        animator.SetBool("IsAngry", isOn);
    }
    void OnToggleGreetingValueChanged(bool isOn){
        animator.SetBool("IsGreeting", isOn);
    }
    void OnToggleProudValueChanged(bool isOn){
        animator.SetBool("IsProud", isOn);
    }
    void OnToggleTalkingValueChanged(bool isOn){
        animator.SetBool("IsTalking", isOn);
    }
    void OnToggleDisapproveValueChanged(bool isOn){
        animator.SetBool("IsDisapprove", isOn);
    }
    void OnToggleThinkingValueChanged(bool isOn){
        animator.SetBool("IsThinking", isOn);
    }
}
