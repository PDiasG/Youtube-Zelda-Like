using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
 * Script used to make camera follow a target, clamped to two positions to avoid camera getting out of bounds
 */
public class CameraMovement : MonoBehaviour
{
    public Transform target;
    public float smoothing;
    public Vector2 maxPosition;
    public Vector2 minPostion;

    void Start()
    {
        
    }

    // Use LateUpdate so that camera is the last thing to update each frame
    void LateUpdate()
    {
        if (transform.position != target.position)
        {
            Vector3 targetPosition = new Vector3(target.position.x, target.position.y, transform.position.z);
            targetPosition.x = Mathf.Clamp(targetPosition.x, minPostion.x, maxPosition.x);
            targetPosition.y = Mathf.Clamp(targetPosition.y, minPostion.y, maxPosition.y);

            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing);
        }
    }
}
