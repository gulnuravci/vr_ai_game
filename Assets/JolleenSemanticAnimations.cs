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

    // ----Set the animator's bool parameter based on the toggle state----

    void OnToggleHappyValueChanged(bool isOn){
        if (isOn) {
            animator.SetTrigger("IsHappy");
        }
    }

    void OnToggleSadValueChanged(bool isOn){
        if (isOn) {
            animator.SetTrigger("IsSad");
        }
    }
    void OnToggleAngryValueChanged(bool isOn){
        if (isOn){
            animator.SetTrigger("IsAngry");
        }
    }
    void OnToggleGreetingValueChanged(bool isOn){
        if (isOn){
            animator.SetTrigger("IsGreeting");
        }
    }
    void OnToggleProudValueChanged(bool isOn){
        if (isOn){
            animator.SetTrigger("IsProud");
        }
    }
    void OnToggleTalkingValueChanged(bool isOn){
        if (isOn){
            animator.SetTrigger("IsTalking");
        }
    }
    void OnToggleDisapproveValueChanged(bool isOn){
        if (isOn){
            animator.SetTrigger("IsDisapprove");
        }
    }
    void OnToggleThinkingValueChanged(bool isOn){
        if (isOn){
            animator.SetTrigger("IsThinking");
        }
    }
}
