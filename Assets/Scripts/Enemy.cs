using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    void Awake()
    {
        health = maxHealth.initialValue;
    }

    public void Knockback(Rigidbody2D rigidbody, float knockbackTime, float damage)
    {
        StartCoroutine(KnockbackCoroutine(rigidbody, knockbackTime));
        TakeDamage(damage);
    }

    private void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            this.gameObject.SetActive(false);
        }
    }

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
