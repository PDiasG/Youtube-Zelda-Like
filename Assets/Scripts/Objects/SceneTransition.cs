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
    public GameObject fadeInPanel;
    public GameObject fadeOutPanel;
    public float fadeWaitTime;

    private void Awake()
    {
        if (fadeInPanel != null)
        {
            GameObject panel = Instantiate(fadeInPanel, Vector3.zero, Quaternion.identity) as GameObject;
            Destroy(panel, 1);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            // We store the new player position in a VectorValue scriptable object
            // When new scene is loaded player is set to that position
            positionStorage.runtimeValue = playerPosition;
            other.gameObject.SetActive(false);
            StartCoroutine(FadeCoroutine());
        }
    }

    public IEnumerator FadeCoroutine()
    {
        if (fadeOutPanel != null)
        {
            GameObject panel = Instantiate(fadeOutPanel, Vector3.zero, Quaternion.identity) as GameObject;
        }
        yield return new WaitForSeconds(fadeWaitTime);
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneToLoad);
        while (!asyncOperation.isDone)
        {
            yield return null;
        }
    }
}
