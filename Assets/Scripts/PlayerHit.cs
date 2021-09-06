using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *  Controls collisions between entities
 *  This is attached to any entity that can hit another i.e. player hitboxes and enemies
 */
public class PlayerHit : MonoBehaviour
{
    public float thrust;
    public float knockbackTime;
    public float damage;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Smashing objects
        if (other.gameObject.CompareTag("Breakable"))
        {
            // Enemy can only break stuff if is knockbacked, to avoid enemies walking over pots
            if (gameObject.CompareTag("Enemy") && GetComponent<Enemy>().currentState != EnemyState.stagger) return;

            other.GetComponent<Pot>().Smash();
        }

        // Enemy hitting player and player hitting enemy, maybe can split for more readability
        else if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Player"))
        {
            // Handles knockback and damage, if entity is staggered it can't have another hit registered
            Rigidbody2D rigidbody = other.GetComponent<Rigidbody2D>();

            if (rigidbody != null)
            {
                // Add knockback force to rigibody that was hit
                Vector2 diff = rigidbody.transform.position - transform.position;

                // Deal damage to enemy
                // isTrigger to only register his on the hitbox not on the collision box
                // Also we make sure only the player can damage the enemy
                if (gameObject.CompareTag("Player") && 
                    other.gameObject.CompareTag("Enemy") && 
                    other.isTrigger && 
                    rigidbody.GetComponent<Enemy>().currentState != EnemyState.stagger)
                {
                    rigidbody.AddForce(diff.normalized * thrust, ForceMode2D.Impulse);
                    rigidbody.GetComponent<Enemy>().currentState = EnemyState.stagger;
                    other.GetComponent<Enemy>().Hit(rigidbody, knockbackTime, damage);
                }

                // Deal damage to player
                // isTrigger to only register his on the hitbox not on the collision box
                if (other.gameObject.CompareTag("Player") &&
                    other.isTrigger &&
                    rigidbody.GetComponent<PlayerMovement>().currentState != PlayerState.stagger)
                {
                    rigidbody.AddForce(diff.normalized * thrust, ForceMode2D.Impulse);
                    rigidbody.GetComponent<PlayerMovement>().currentState = PlayerState.stagger;
                    other.GetComponent<PlayerMovement>().Hit(knockbackTime, damage);
                }
            }
        }
    }
}
