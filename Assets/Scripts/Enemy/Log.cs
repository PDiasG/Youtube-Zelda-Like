 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *  Controls the Log enemy time
 */
public class Log : Enemy
{
    public Transform target;
    protected Rigidbody2D _rigidbody;
    public float chaseRadius;
    public float attackRadius;
    public Animator animator;

    // signal id
    public CustomSignal contextClueOn;
    public CustomSignal contextClueOff;

    // Debugging purposes only, draw chase and attack radius
    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, chaseRadius);
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }

    void Start()
    {
        currentState = EnemyState.idle;
        target = GameObject.FindWithTag("Player").transform;
        _rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        animator.SetBool("wakeUp", true);
    }

    void FixedUpdate()
    {
        // While player is active (alive) keeps searching for it, otherwise just go back to sleep
        if (target.gameObject.activeInHierarchy)
        {
            CheckDistance();
        }
        else
        {
            contextClueOff.Raise(signalId);
            ChangeState(EnemyState.idle);
            animator.SetBool("wakeUp", false);
        }
    }

    // This could be refactored to a child of enemy called "FollowEnemy" and have Log be a child of that, so logic can be reused for different enemies
    public virtual void CheckDistance()
    {
        float dist = Vector3.Distance(target.position, transform.position);
        if (dist <= chaseRadius && dist > attackRadius)
        {
            contextClueOn.Raise(signalId);
            // Don't move if is staggered or attacking
            if (currentState == EnemyState.idle || currentState == EnemyState.walk)
            {
                Vector3 temp = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.fixedDeltaTime);
                ChangeAnim(temp - transform.position);
                _rigidbody.MovePosition(temp);
                ChangeState(EnemyState.walk);
                animator.SetBool("wakeUp", true);
            }
        }
        else if (dist > chaseRadius)
        {
            contextClueOff.Raise(signalId);
            // Go back to home position if player has gone too far
            float distToHome = Vector3.Distance(homePosition, transform.position);
            if (distToHome <= homeRadius)
            {
                ChangeState(EnemyState.idle);
                animator.SetBool("wakeUp", false);
            }
            else
            {
                Vector3 temp = Vector3.MoveTowards(transform.position, homePosition, moveSpeed * Time.fixedDeltaTime);
                ChangeAnim(temp - transform.position);
                _rigidbody.MovePosition(temp);
                ChangeState(EnemyState.walk);
                animator.SetBool("wakeUp", true);
            }
        }
    }

    // Controls the direction the log is moving so BlendTree can change the animation that is playing
    protected void ChangeAnim(Vector2 direction)
    {
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            if (direction.x > 0)
            {
                SetAnimFloat(Vector2.right);
            }
            else if (direction.x < 0)
            {
                SetAnimFloat(Vector2.left);
            }
        }
        else if (Mathf.Abs(direction.x) < Mathf.Abs(direction.y))
        {
            if (direction.y > 0)
            {
                SetAnimFloat(Vector2.up);
            }
            else if (direction.y < 0)
            {
                SetAnimFloat(Vector2.down);
            }
        }
    }

    protected void SetAnimFloat(Vector2 setVector)
    {
        animator.SetFloat("moveX", setVector.x);
        animator.SetFloat("moveY", setVector.y);
    }

    // Avoid updating the state machine to the same state
    protected void ChangeState(EnemyState newState)
    {
        if (currentState != newState)
        {
            currentState = newState;
        }
    }
}
