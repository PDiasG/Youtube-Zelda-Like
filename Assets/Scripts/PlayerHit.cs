using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHit : MonoBehaviour
{
    public float thrust;
    public float knockbackTime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Breakable"))
        {
            // Enemy can only break stuff if is knockbacked
            if (gameObject.CompareTag("Enemy") && GetComponent<Enemy>().currentState != EnemyState.stagger) return;

            other.GetComponent<Pot>().Smash();
        }

        else if (other.gameObject.CompareTag("Enemy")|| other.gameObject.CompareTag("Player"))
        {
            // Handles knockback
            Rigidbody2D rigidbody = other.GetComponent<Rigidbody2D>();

            if (rigidbody != null)
            {
                Vector2 diff = rigidbody.transform.position - transform.position;
                rigidbody.AddForce(diff.normalized * thrust, ForceMode2D.Impulse);

                // Only applies knockback once
                if (other.gameObject.CompareTag("Enemy") && rigidbody.GetComponent<Enemy>().currentState != EnemyState.stagger)
                {
                    rigidbody.GetComponent<Enemy>().currentState = EnemyState.stagger;
                    other.GetComponent<Enemy>().Knockback(rigidbody, knockbackTime);
                }
                if (other.gameObject.CompareTag("Player") && rigidbody.GetComponent<PlayerMovement>().currentState != PlayerState.stagger)
                {
                    rigidbody.GetComponent<PlayerMovement>().currentState = PlayerState.stagger;
                    other.GetComponent<PlayerMovement>().Knockback(knockbackTime);
                }
            }
        }
    }
}
