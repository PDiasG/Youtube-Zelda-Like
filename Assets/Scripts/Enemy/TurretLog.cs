using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretLog : Log
{
    public GameObject projectile;
    public float fireDelay;
    public float fireDelaySecs;
    public bool canFire = true;

    private void Update()
    {
        fireDelaySecs -= Time.deltaTime;
        if (fireDelaySecs <= 0)
        {
            fireDelaySecs = fireDelay;
            canFire = true;
        }
    }

    public override void CheckDistance()
    {
        float dist = Vector3.Distance(target.position, transform.position);
        if (dist <= chaseRadius && dist > attackRadius)
        {
            contextClueOn.Raise(contextClueId);
            // Don't move if is staggered or attacking
            if ((currentState == EnemyState.idle || currentState == EnemyState.walk) && canFire)
            {
                canFire = false;
                Vector3 projectileDirection = target.position - transform.position;
                GameObject current = Instantiate(projectile, transform.position, Quaternion.identity);
                current.GetComponent<Projectile>().Launch(projectileDirection);
                ChangeState(EnemyState.walk);
                animator.SetBool("wakeUp", true);
            }
        }
        else if (dist > chaseRadius)
        {
            contextClueOff.Raise(contextClueId);
            // Go back to sleep if player has gone too far
            ChangeState(EnemyState.idle);
            animator.SetBool("wakeUp", false);
        }
    }
}
