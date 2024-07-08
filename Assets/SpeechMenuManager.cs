using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class SpeechMenuManager : MonoBehaviour
{
    public Transform playerHeadTransform;
    public float spawnDistance = 1.5f;
    public float rightOffsetDistance = 0.5f;
    public float armDistanceOffset = 0;
    public float height= 160;
    public InputActionProperty showButtonReference;
    public GameObject speechMenuCanvas;
    public GameObject playerModel;
    public GameObject leftHandGuide;
    public GameObject rightHandGuide;
    public GameObject cameraOffset;
    public Button closeButton;
    public Button updateArmDistanceOffset; 
    public Button updateHeight;
    public Slider spawnDistanceSlider;
    public Slider armDistanceSlider;
    public Slider heightSlider;
    public Vector3 baseScale = new Vector3(1.2f, 1.2f, 1.2f);

    public TMP_Text testingText;
    public TMP_Text playerHeight;

    void Start(){
        closeButton.onClick.AddListener(closeButtonClicked);
        updateArmDistanceOffset.onClick.AddListener(updateArmDistanceOffsetButtonClicked);
        updateHeight.onClick.AddListener(updateHeightButtonClicked);
        spawnDistanceSlider.onValueChanged.AddListener(delegate {spawnDistanceSliderValueChanged(); });
        armDistanceSlider.onValueChanged.AddListener(delegate {armDistanceSliderValueChanged(); });
        heightSlider.onValueChanged.AddListener(delegate {heightSliderValueChanged(); });

        // initialize speech menu spawn distance
        // initialize arm distance offset
        // intialize height offset

        // initially set hand guides to false
        leftHandGuide.SetActive(false);
        rightHandGuide.SetActive(false);

        // initially set menu to false
        speechMenuCanvas.SetActive(false);
    }

    void Update()
    {
        // if show menu button is pressed
        if(showButtonReference.action.WasPressedThisFrame()){
            // toggle the menu
            speechMenuCanvas.SetActive(!speechMenuCanvas.activeSelf);
            // show hand guides so the player can adjust their body scale correctly
            leftHandGuide.SetActive(!leftHandGuide.activeSelf);
            rightHandGuide.SetActive(!rightHandGuide.activeSelf);
        }
        
        // if the menu is active
        if(speechMenuCanvas.activeSelf){
            // place the speech menu infront of the player at a spawn distance
            speechMenuCanvas.transform.position = playerHeadTransform.position + new Vector3(playerHeadTransform.forward.x, 0, playerHeadTransform.forward.z).normalized * spawnDistance;

            // make the menu face toward the player head's x and z transform
            speechMenuCanvas.transform.LookAt(new Vector3 (playerHeadTransform.position.x, playerHeadTransform.position.y, playerHeadTransform.position.z));

            // forward direction corresponds to the blue axis in the Scene view
            // by default it is (0, 0, 1) aka positive z-direction in world space
            // note: i think this fixes the invert of the game menu
            speechMenuCanvas.transform.forward *= -1;
        }
    }

    public void closeButtonClicked(){
        // close speech menu
        speechMenuCanvas.SetActive(false);
        leftHandGuide.SetActive(false);
        rightHandGuide.SetActive(false);
    }

    public void updateArmDistanceOffsetButtonClicked(){        
        Vector3 newScale = baseScale + new Vector3(armDistanceOffset, armDistanceOffset, armDistanceOffset);
        
        playerModel.transform.localScale = newScale;
    }

    public void updateHeightButtonClicked(){
        Vector3 newHeight = new Vector3(cameraOffset.transform.position.x, height, cameraOffset.transform.position.z);

        cameraOffset.transform.position = newHeight;
        // debugging text
        testingText.text = newHeight.ToString("F2");
    }

    public void spawnDistanceSliderValueChanged(){
        spawnDistance = spawnDistanceSlider.value;
    }

    public void armDistanceSliderValueChanged(){
        armDistanceOffset = armDistanceSlider.value;
        
        // debugging text
        testingText.text = "Height: " + armDistanceOffset.ToString("F2");
    }

    public void heightSliderValueChanged(){
        height = heightSlider.value;
        playerHeight.text = height.ToString("F2") + " cm";
    }
}
