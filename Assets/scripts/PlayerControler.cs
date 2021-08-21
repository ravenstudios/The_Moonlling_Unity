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


    [Header("Movment")]
        public float _speed = 10f;
        public float _maxVel = 7;
        public float _slideFriction = 4f;


    [Header("Jump")]
    public bool _isJumping = false;
        public float _jumpVel;
        public float _jumpSpeed = 30f;
        public float _maxJumpVel = 300f;
    






    Rigidbody2D rb;
    

    private void Awake()
    {
        _playerInputActions = new PlayerInputActions();

        rb = this.GetComponent<Rigidbody2D>();

    }

    private void Start()
    {
        _playerInputActions.Player.Jump.started += StartJump();
        _playerInputActions.Player.Jump.canceled += EndJump();
    }

    

    void StartJump()
    {
        _isJumping = true;
    }


    private Action<InputAction.CallbackContext> EndJump()
    {
        throw new NotImplementedException();
    }



    private void Update()
    {
        ModifyPhysics();
    }

    private void FixedUpdate()
    {
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


    private void OnJump(InputValue movementValue)
    {
        //_jumpVel = movementValue.Get<Axis>();
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

        //void Jump()
        //{
        //    rb.velocity = new Vector2(rb.velocity.x, 0);
        //    rb.AddForce(Vector2.up * _jumpSpeed, ForceMode2D.Impulse);
        //}
    }
    //private void OnJump()
    //{
    //    Jump();

    //}

    //Vector2 Jump()
    //{
    //    Debug.Log("j");
    //    float x = 0;
    //    float y = _jumpSpeed;
    //    return new Vector2(x, y);

    //    //var gamepad = Gamepad.current;
    //    //if (gamepad == null)
    //    //{
    //    //    return;
    //    //}

    //    //if (gamepad.buttonSouth.isPressed)
    //    //{
    //    //    rb.AddForce(Jump());
    //    //}


    //}



