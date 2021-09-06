using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 *  Controls transition between rooms inside the same scene
 */

public class RoomMove : MonoBehaviour
{
    // Using two Vectors for camera position to support rooms with different sizes
    // Use proper values instead of change between two rooms
    public Vector2 maxPosition;
    public Vector2 minPosition;
    public Vector3 playerChange;
    private CameraMovement cam;

    // Add support for Place Name title cards
    public bool needText;
    public string placeName;
    public GameObject text;
    public Text placeText;

    void Start()
    {
        cam = Camera.main.GetComponent<CameraMovement>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            cam.minPostion = minPosition;
            cam.maxPosition = maxPosition;
            other.transform.position += playerChange;

            // Add support for Place Name title cards
            if (needText)
            {
                StartCoroutine(placeNameCoroutine());
            } 
            else
            {
                text.SetActive(false);
            }
        }
    }

    // Can add other animations or sound effect here for a better visual

    // TODO: there is a small bug here when you enter and exit the room in less than 3 seconds
    // The "timer" is not reset and the second text stays for less than 3 seconds
    // Possible fix: check if text is still the same before text.SetActive(false)
    private IEnumerator placeNameCoroutine()
    {
        text.SetActive(true);
        placeText.text = placeName;
        yield return new WaitForSeconds(3f);
        text.SetActive(false);
    }
}
