using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    idle,
    walk,
    attack,
    interact,
    stagger
}

public class PlayerMovement : MonoBehaviour
{
    public PlayerState currentState;
    public float speed;
    private Rigidbody2D rigidbody2d;
    private Vector3 change;
    private Animator animator;

    void Start()
    {
        currentState = PlayerState.walk;
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        animator.SetFloat("moveX", 0);
        animator.SetFloat("moveY", -1);
    }

    void Update()
    {
        change = Vector3.zero;
        // This is using the old Unity Input System. Can be updated to the new one for better support for multiple input devices
        // Check controller-support branch for updated code
        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");
        
        // If else prevents attacking and moving at the same time
        // State machine prevents spamming attack button, animation has to complete to attack again
        if (Input.GetButtonDown("Attack") && currentState != PlayerState.attack && currentState != PlayerState.stagger)
        {
            StartCoroutine(AttackCoroutine());
        } 
        else if (currentState == PlayerState.idle || currentState == PlayerState.walk)
        {
            UpdateAnimationAndMove();
        }
    }

    void UpdateAnimationAndMove()
    {
        if (change != Vector3.zero)
        {
            MoveCharacter();
            animator.SetFloat("moveX", change.x);
            animator.SetFloat("moveY", change.y);
            animator.SetBool("moving", true);
        }
        else
        {
            animator.SetBool("moving", false);
        }
    }

    void MoveCharacter()
    {
        change.Normalize(); // keep same speed diagonally
        rigidbody2d.MovePosition(transform.position + change * speed * Time.fixedDeltaTime);
    }

    private IEnumerator AttackCoroutine()
    {
        currentState = PlayerState.attack;
        animator.SetBool("attacking", true);
        yield return null;
        animator.SetBool("attacking", false);
        yield return new WaitForSeconds(.3f);
        currentState = PlayerState.walk;
    }

    public void Knockback(float knockbackTime)
    {
        StartCoroutine(KnockbackCoroutine(knockbackTime));
    }

    private IEnumerator KnockbackCoroutine(float knockbackTime)
    {
        if (rigidbody2d != null)
        {
            yield return new WaitForSeconds(knockbackTime);
            rigidbody2d.velocity = Vector2.zero;
            currentState = PlayerState.idle;
            rigidbody2d.velocity = Vector2.zero;
        }
    }
}
