using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Parent script for all Enemies
 */

// Simple enemy state machine
public enum EnemyState
{
    idle,
    walk,
    attack,
    stagger
}

public class Enemy : MonoBehaviour
{
    public EnemyState currentState;
    public FloatValue maxHealth;
    public float health;
    public string enemyName;
    public int baseDamage;
    public float moveSpeed;
    public GameObject deathEffect;
    public CustomSignal kickSignal;

    void Awake()
    {
        // Use the FloatValue prefab as a base for health, so it's easier to customize
        health = maxHealth.runtimeValue;
    }

    // Actions when enemy is hit
    public void Hit(Rigidbody2D rigidbody, float knockbackTime, float damage)
    {
        StartCoroutine(KnockbackCoroutine(rigidbody, knockbackTime));
        TakeDamage(damage);
    }

    private void DeathEffect()
    {
        if (deathEffect != null)
        {
            GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(effect, 1);
        }
    }

    private void TakeDamage(float damage)
    {
        kickSignal.Raise();
        health -= damage;
        if (health <= 0)
        {
            DeathEffect();
            this.gameObject.SetActive(false);
        }
    }

    // The force for knockback is added in PlayerHit.cs, this just controls the length of the knockback and reset the body's velocity 
    private IEnumerator KnockbackCoroutine(Rigidbody2D rigidbody, float knockbackTime)
    {
        if (rigidbody != null)
        {
            yield return new WaitForSeconds(knockbackTime);
            rigidbody.velocity = Vector2.zero;
            currentState = EnemyState.idle;
            rigidbody.velocity = Vector2.zero;
        }
    }
}
