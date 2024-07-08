using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitTransform : MonoBehaviour
{
    void Update(){
        Vector3 currentPosition = transform.position;
        currentPosition.y += 0.5f;
        transform.position = currentPosition;
    }

    // [SerializeField] private Vector3 minPositionLimits = new Vector3(-10f, -10f, -10f);
    // [SerializeField] private Vector3 maxPositionLimits = new Vector3(10f, 10f, 10f);

    // [SerializeField] private Vector3 minRotationLimits = new Vector3(-45f, -45f, -45f);
    // [SerializeField] private Vector3 maxRotationLimits = new Vector3(45f, 45f, 45f);

    // void Update()
    // {
        // Clamp position
        // transform.position = new Vector3(
        //     Mathf.Clamp(transform.position.x, minPositionLimits.x, maxPositionLimits.x),
        //     Mathf.Clamp(transform.position.y, minPositionLimits.y, maxPositionLimits.y),
        //     Mathf.Clamp(transform.position.z, minPositionLimits.z, maxPositionLimits.z)
        // );

        // // Clamp rotation
        // transform.eulerAngles = new Vector3(
        //     Mathf.Clamp(NormalizeAngle(transform.eulerAngles.x), minRotationLimits.x, maxRotationLimits.x),
        //     Mathf.Clamp(NormalizeAngle(transform.eulerAngles.y), minRotationLimits.y, maxRotationLimits.y),
        //     Mathf.Clamp(NormalizeAngle(transform.eulerAngles.z), minRotationLimits.z, maxRotationLimits.z)
        // );
    // }

    // Normalize angles to a 0-360 range
    // float NormalizeAngle(float angle)
    // {
    //     while (angle > 360) angle -= 360;
    //     while (angle < 0) angle += 360;
    //     return angle;
    // }
}
