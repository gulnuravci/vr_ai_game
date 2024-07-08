using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class GameMenuManager : MonoBehaviour
{
    public Transform head;
    public float spawnDistance = 2;
    public GameObject menu;
    public InputActionProperty showButton;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(showButton.action.WasPressedThisFrame()){
            menu.SetActive(!menu.activeSelf);

            menu.transform.position = head.position + new Vector3(head.forward.x, 0, head.forward.z).normalized * spawnDistance;
        }

        // LookAt method is used to rotate a GameObject so that its forward direction points towards a specified target. The method adjusts the GameObject's rotation so that it aligns with the direction of the target.
        menu.transform.LookAt(new Vector3 (head.position.x, menu.transform.position.y, head.position.z));

        // The forward property in Unity is a part of the Transform class, and it represents the forward direction of a GameObject. In Unity's 3D space, this forward direction corresponds to the blue axis in the Scene view, and it's represented as a Vector3. By default, the forward vector of a GameObject is (0, 0, 1), which means it points in the positive Z-direction in world space.
        menu.transform.forward *= -1;
    }
}
