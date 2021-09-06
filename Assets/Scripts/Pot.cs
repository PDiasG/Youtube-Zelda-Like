using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *  Controls breakable objects
 */

public class Pot : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Smash()
    {
        animator.SetBool("smash", true);
        StartCoroutine(breakCoroutine());
    }

    IEnumerator breakCoroutine()
    {
        yield return new WaitForSeconds(.3f);
        this.gameObject.SetActive(false);
    }
}
