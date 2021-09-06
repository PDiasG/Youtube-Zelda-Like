using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolLog : Log
{
    public Transform[] path;
    public int currentPoint;
    public float roudingDistance = .2f;

    // This could be refactored to a child of enemy called "PatrolEnemy" and have Log be a child of that, so logic can be reused for different enemies
    public override void CheckDistance()
    {
        float dist = Vector3.Distance(target.position, transform.position);
        if (dist <= chaseRadius && dist > attackRadius)
        {
            // Don't move if is staggered or attacking
            if (currentState == EnemyState.idle || currentState == EnemyState.walk)
            {
                Vector3 temp = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.fixedDeltaTime);
                ChangeAnim(temp - transform.position);
                _rigidbody.MovePosition(temp);
                animator.SetBool("wakeUp", true);
            }
        }
        else if (dist > chaseRadius)
        {
            // Go back to patrolling if player is too far away
            if (Vector3.Distance(transform.position, path[currentPoint].position) < roudingDistance)
            {
                ChangeGoal();
            }
            else
            {
                Vector3 temp = Vector3.MoveTowards(transform.position, path[currentPoint].position, moveSpeed * Time.fixedDeltaTime);
                ChangeAnim(temp - transform.position);
                _rigidbody.MovePosition(temp);
            }
        }
    }

    private void ChangeGoal()
    {
        if (currentPoint == path.Length - 1)
        {
            currentPoint = 0;
        }
        else
        {
            currentPoint++;
        }
    }
}
