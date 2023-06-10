using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;

    public float speed;

    private float inputX;

    private float inputY;

    private Vector2 movementInput;

    private Animator[] animators;

    private bool isMoving;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animators = GetComponentsInChildren<Animator>();
    }
    private void Update()
    {
        MoveInput();
    }

    private void FixedUpdate()
    {
        Movement();
        SwitchAnimation();
    }

    private void MoveInput()
    {
        inputX = Input.GetAxisRaw("Horizontal");
        inputY = Input.GetAxisRaw("Vertical");
        //先统一缩小一半
        inputX *= 0.5f;
        inputY *= 0.5f;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            inputX *= 2;
            inputY *= 2;
        }
        if (inputX != 0 && inputY != 0)
        {
            inputX *= 0.6f;
            inputY *= 0.6f;
        }

        movementInput = new Vector2(inputX, inputY);
        isMoving = movementInput != Vector2.zero;
    }

    private void Movement()
    {
        rb.MovePosition(rb.position + movementInput * speed * Time.deltaTime);
    }

    private void SwitchAnimation()
    {
        foreach (var anim in animators)
        {
            anim.SetBool("IsMoving", isMoving);
            if (isMoving)
            {
                anim.SetFloat("InputX", inputX);
                anim.SetFloat("InputY", inputY);
            }
        }
    }
}
