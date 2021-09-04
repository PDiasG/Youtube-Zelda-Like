using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomMove : MonoBehaviour
{
    public Vector2 maxPosition;
    public Vector2 minPosition;
    public Vector3 playerChange;
    private CameraMovement cam;

    public bool needText;
    public string placeName;
    public GameObject text;
    public Text placeText;

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

            if (needText)
            {
                StartCoroutine(placeNameCoroutine());
            }
        }
    }

    private IEnumerator placeNameCoroutine()
    {
        text.SetActive(true);
        placeText.text = placeName;
        yield return new WaitForSeconds(4f);
        text.SetActive(false);
    }
}
