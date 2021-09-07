using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float moveSpeed;
    public float lifetime;
    private float lifetimeSecs;
    public Rigidbody2D rigidbody2d;

    private void Update()
    {
        lifetimeSecs -= Time.deltaTime;
        if (lifetimeSecs <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void Launch(Vector2 direction)
    {
        lifetimeSecs = lifetime;
        rigidbody2d.velocity = direction.normalized * moveSpeed;
    }

    // Using just destroy here make projectiles blockable using sword
    // That's because the hitboxes do not have a rigidbody attached to them so PlayerHit never triggers the damage code
    private void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(gameObject);
    }
}
