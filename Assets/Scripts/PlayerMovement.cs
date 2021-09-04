using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rigidbody2d;
    private Vector3 change;
    private Animator animator;

    InputMaster inputMaster;

    void Awake()
    {
        inputMaster = new InputMaster();
        inputMaster.Player.Movement.performed += ctx => change = ctx.ReadValue<Vector2>();
        inputMaster.Player.Movement.canceled += ctx => change = Vector3.zero;
    }

    private void OnEnable()
    {
        inputMaster.Player.Enable();
    }

    private void OnDisable()
    {
        inputMaster.Player.Disable();
    }

    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {

    }

    private void FixedUpdate()
    {
        UpdateAnimationAndMove();
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
        rigidbody2d.MovePosition(transform.position + change * speed * Time.fixedDeltaTime);
    }

}
