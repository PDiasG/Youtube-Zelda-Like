using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public Enemy[] enemies;
    public Pot[] pots;

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") && !other.isTrigger)
        {
            foreach(Enemy enemy in enemies)
            {
                ChangeActivation(enemy, true);
            }

            foreach(Pot pot in pots)
            {
                ChangeActivation(pot, true);
            }
        }
    }

    public virtual void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            foreach (Enemy enemy in enemies)
            {
                ChangeActivation(enemy, false);
            }

            foreach (Pot pot in pots)
            {
                ChangeActivation(pot, false);
            }
        }
    }

    void ChangeActivation(Component component, bool state)
    {
        component.gameObject.SetActive(state);
    }
}
