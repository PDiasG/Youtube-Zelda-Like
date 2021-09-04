using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomMove : MonoBehaviour
{
    public Vector2 maxPosition;
    public Vector2 minPosition;
    public Vector3 playerChange;
    private CameraMovement cam;

    void Start()
    {
        cam = Camera.main.GetComponent<CameraMovement>();
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            cam.minPostion = minPosition;
            cam.maxPosition = maxPosition;
            other.transform.position += playerChange;
        }
    }
}
