using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    public bool active;
    public BoolValue storedValue;
    public Sprite inactiveSprite;
    public Sprite activeSprite;
    private SpriteRenderer sprite;
    public string switchCode;
    public SwitchDoor door;
    public Collider2D other;

    public Vector2 knockbackDirection;
    public float damage;
    public float thrust;
    public float knockbackTime;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        active = storedValue.runtimeValue;
        if (active)
        {
            ActivateSwitch();
        } 
        else
        {
            DeactivateSwitch();
        }
    }

    public void ActivateSwitch()
    {
        active = true;
        storedValue.runtimeValue = active;
        sprite.sprite = activeSprite;
        
        if (!door.AddSwitch(switchCode))
        {
            Rigidbody2D rigidbody = other.GetComponent<Rigidbody2D>();
            rigidbody.AddForce(knockbackDirection * thrust, ForceMode2D.Impulse);
            rigidbody.GetComponent<PlayerMovement>().currentState = PlayerState.stagger;
            other.GetComponent<PlayerMovement>().Hit(knockbackTime, damage);
        }
    }

    public void DeactivateSwitch()
    {
        active = false;
        storedValue.runtimeValue = active;
        sprite.sprite = inactiveSprite;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")  && !other.isTrigger && !active)
        {
            this.other = other;
            ActivateSwitch();
        }
    }
}
