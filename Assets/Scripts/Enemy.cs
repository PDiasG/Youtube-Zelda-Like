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
    public int health;
    public string enemyName;
    public int baseDamage;
    public float moveSpeed;

    public void Knockback(Rigidbody2D rigidbody, float knockbackTime)
    {
        StartCoroutine(KnockbackCoroutine(rigidbody, knockbackTime));
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
