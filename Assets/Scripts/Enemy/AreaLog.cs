using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaLog : Log
{
    public Collider2D boundary;

    public override void CheckDistance()
    {
        float dist = Vector3.Distance(target.position, transform.position);
        if (dist <= chaseRadius && dist > attackRadius && boundary.bounds.Contains(target.transform.position) )
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
        else if (dist > chaseRadius || !boundary.bounds.Contains(target.transform.position) )
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
}
