using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Collectable : MonoBehaviour
{
    public CustomSignal collectableSignal;
    public abstract void Collect();

    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (CollectCondition(other))
        {
            Collect();
            Destroy(this.gameObject);
        }
    }

    public virtual bool CollectCondition(Collider2D other)
    {
        return other.CompareTag("Player") && !other.isTrigger;
    }
}
