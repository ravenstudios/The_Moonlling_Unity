using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControler : MonoBehaviour
{

    public PlayerInputActions _playerInputActions;


    [Header("POS")]
        public Vector2 pos;
        public Vector2 _leftStickVector;
    public Vector2 velicoty;


    [Header("Movment")]
        public float _speed = 10f;
        public float _maxVel = 7;
        public float _slideFriction = 4f;


    [Header("Jump")]
        public bool _isJumping = false;
        public float _jumpVel;
        public float _jumpSpeed = 10;
        public float _maxJumpVel = 20;
    






    Rigidbody2D rb;
    

    private void Awake()
    {
        _playerInputActions = new PlayerInputActions();

        rb = this.GetComponent<Rigidbody2D>();


        _playerInputActions.Player.Jump.performed += ctx => StartJump();
        _playerInputActions.Player.Jump.canceled += ctx => EndJump();
    }





    private void OnEnable()
    {
        _playerInputActions.Enable();
    }


    private void OnDisable()
    {
        _playerInputActions.Disable();
    }


    private void Update()
    {
        ModifyPhysics();


        //clamp max speed
        if (Mathf.Abs(rb.velocity.y) > _maxJumpVel)
        {
            rb.velocity = new Vector2(rb.velocity.y, Mathf.Sign(rb.velocity.y) * _maxVel);
            _isJumping = false;
        }

        if (_isJumping)
        {

            rb.velocity += Vector2.up * Physics2D.gravity.y * -_jumpSpeed * Time.deltaTime;
        }
        
    }



    private void FixedUpdate()
    {
        velicoty = rb.velocity;
        pos = rb.position;
        Move();

        var gamepad = Gamepad.current;
        if (gamepad == null)
        {
            return;
        }

        if (gamepad.buttonSouth.isPressed)
        {
            Vector2 jump = new Vector2(0, _jumpSpeed);
            rb.AddForce(jump, ForceMode2D.Impulse);

        }
    }


    private void OnMove(InputValue movementValue)
    {
        _leftStickVector = movementValue.Get<Vector2>();
    }


    public void StartJump()
    {
        _isJumping = true;
        Debug.Log("jump");
    }

    public void EndJump()
    {
        _isJumping = false;
        Debug.Log("stop jump");
    }




    void Move()
    {
        //move
        rb.AddForce(Vector2.right * _leftStickVector.x * _speed);

        //clamp max speed
        if (Mathf.Abs(rb.velocity.x) > _maxVel)
        {
            rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * _maxVel, rb.velocity.y);
        }


    }

    void ModifyPhysics()
    {
        //If input direction is opposite of veocity
        bool changingDirections = (_leftStickVector.x > 0 && rb.velocity.x < 0) || (_leftStickVector.x < 0 && rb.velocity.x > 0);

        if (Mathf.Abs(_leftStickVector.x) < 0.4f || changingDirections)
        {
            rb.drag = _slideFriction;
        }
        else
        {
            rb.drag = 0f;
        }
    }
}
    


