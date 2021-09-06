using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * Transition between scenes
 */

public class SceneTransition : MonoBehaviour
{
    public string sceneToLoad; // could change this to reference of scene in order to avoid breaking when changing scene name
    public Vector2 playerPosition;
    public VectorValue positionStorage;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            // We store the new player position in a VectorValue scriptable object
            // When new scene is loaded player is set to that position
            positionStorage.runtimeValue = playerPosition;
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
