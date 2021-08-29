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
    public float _speed = 20f;
    public float _maxVel = 10;
    public float _slideFriction = 4f;
    public float gravity = 1f;
    public float fallMultiplier = 5f;


    [Header("Components")]
    public LayerMask _groundLayer;
    public Rigidbody2D rb;



    [Header("Jump")]
    public bool _isJumping = false;
    public float _jumpSpeed = 10f;
    public float _maxJumpVel = 50f;

    [Header("Collision")]
    public bool _onGround = false;
    public float _rayCastLength = 0.6f;
    public Vector3 _rayCastOffset;





    
    

    private void Awake()
    {
        _playerInputActions = new PlayerInputActions();

        rb = this.GetComponent<Rigidbody2D>();

        //adds the abilty to see when a button is pressed "performed"
        //and released "canceled". ctx is the conext of the input used
        //as an argument.
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

    void OnReset()
    {
        transform.position = new Vector2(0, 0);
    }

    private void Update()
    {
        ModifyPhysics();
        //draws a line from the object to the layer assigned 
        _onGround =
            Physics2D.Raycast(transform.position + _rayCastOffset, Vector2.down, _rayCastLength, _groundLayer)
            ||
            Physics2D.Raycast(transform.position - _rayCastOffset, Vector2.down, _rayCastLength, _groundLayer);


        
        
    }



    private void FixedUpdate()
    {
        velicoty = rb.velocity;
        pos = rb.position;
        Move();

        
    }


    private void OnMove(InputValue movementValue)
    {
        _leftStickVector = movementValue.Get<Vector2>();
    }




    public void StartJump()
    {
        if (_onGround)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            _isJumping = true;
            Jump();
        }
    }

    public void EndJump()
    {
        _isJumping = false;
    }

    void Jump() => rb.AddForce(Vector2.up * _jumpSpeed, ForceMode2D.Impulse);



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

        if (_onGround)
        {
            rb.gravityScale = 1;
            if (Mathf.Abs(_leftStickVector.x) < 0.4f || changingDirections)
            {
                rb.drag = _slideFriction;
            }
            else
            {
                rb.drag = 0f;
            }
        }

        else
        {
            rb.gravityScale = gravity;
            rb.drag = _slideFriction * 0.15f;
            if (rb.velocity.y < 0)
            {
                rb.gravityScale = gravity * fallMultiplier;
            }
            else if (rb.velocity.y > 0 && !_isJumping)
            {
                rb.gravityScale = gravity * (fallMultiplier / 2);
            }
        }

        //if (_isJumping)
        //{
        //    Jump();
        //}

        if (rb.velocity.y > _maxJumpVel)
        {

            _isJumping = false;
        }


    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position + _rayCastOffset, transform.position + _rayCastOffset + Vector3.down * _rayCastLength);
        Gizmos.DrawLine(transform.position - _rayCastOffset, transform.position - _rayCastOffset + Vector3.down * _rayCastLength);

    }
}
    


