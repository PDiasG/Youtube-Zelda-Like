using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Class controls the player, not just the movement
 */

// Simple player state machine
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
    public CustomSignal playerHealthSignal;
    public Inventory playerInventory;
    public SpriteRenderer receivedItemSprite;

    // Each scene has its own Player object
    // Information is saved and passed between scenes using scriptable objects
    public FloatValue currentHealth;
    public VectorValue startingPosition;

    void Start()
    {
        currentState = PlayerState.idle;
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        // set initial direction for attack and animation
        animator.SetFloat("moveX", 0);
        animator.SetFloat("moveY", -1);
        // Set position according to VectorValue
        transform.position = startingPosition.runtimeValue;
    }

    void Update()
    {
        if (currentState == PlayerState.interact) return;

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
            SetAnimatorDirection(change);
            animator.SetBool("moving", true);
        }
        else
        {
            animator.SetBool("moving", false);
        }
    }

    // Fix bug of activating multiple hitboxes when going to two directions
    public void SetAnimatorDirection(Vector2 movementInput)
    {
        if (Mathf.Abs(movementInput.x) > Mathf.Abs(movementInput.y))
        {
            animator.SetFloat("moveX", movementInput.x * (1 / Mathf.Abs(movementInput.x)));
            animator.SetFloat("moveY", 0);
        }
        else
        {
            animator.SetFloat("moveX", 0);
            animator.SetFloat("moveY", movementInput.y * (1 / Mathf.Abs(movementInput.y)));
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
        if (currentState != PlayerState.interact)
        {
            currentState = PlayerState.walk;
        }
    }

    // Actions when player is hit
    public void Hit(float knockbackTime, float damage)
    {
        TakeDamage(damage);
        // Raise signal to UI in order to update heart displays
        playerHealthSignal.Raise();
        if (currentHealth.runtimeValue > 0)
        {
            StartCoroutine(KnockbackCoroutine(knockbackTime));
        }
    }

    // Using Scritable objects to keep track of health
    private void TakeDamage(float damage)
    {
        currentHealth.runtimeValue -= damage;
        if (currentHealth.runtimeValue <= 0)
        {
            this.gameObject.SetActive(false);
        }
    }

    // The force for knockback is added in PlayerHit.cs, this just controls the length of the knockback and reset the body's velocity 
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

    public void RaiseItem()
    {
        if (playerInventory.currentItem != null)
        {
            if (currentState != PlayerState.interact)
            {
                animator.SetBool("receiveItem", true);
                currentState = PlayerState.interact;
                receivedItemSprite.sprite = playerInventory.currentItem.itemSprite;
            }
            else
            {
                animator.SetBool("receiveItem", false);
                currentState = PlayerState.idle;
                receivedItemSprite.sprite = null;
                playerInventory.currentItem = null;
            }
        }
    }
}
