using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControler : MonoBehaviour
{

    public PlayerInputActions _playerInputActions;



    public Vector2 _movementVector;
    public float _speed = 3f;
    public float _jumpSpeed = 30f;
    public float _maxJumpVel = 300f;
    public Vector2 pos;
    Rigidbody2D rb;

    private void Awake()
    {
        _playerInputActions = new PlayerInputActions();

        rb = this.GetComponent<Rigidbody2D>();

    }

    private void Update()
    {
        
    }

    private void FixedUpdate()
    {
        var gamepad = Gamepad.current;
        if (gamepad == null)
        {
            return;
        }

        if (gamepad.buttonSouth.isPressed)
        {
            rb.AddForce(Jump());
        }


        //var z = _playerInputActions.GetEnumerator<
        //if (_playerInputActions.)
        //{
        //    rb.AddForce(Jump());
        //}


        pos = rb.position;
        float x = _movementVector.x * _speed;
        float y = rb.position.y;
        Vector2 v = new Vector2(x, y);
        rb.AddForce(v);
    }

    private void OnMove(InputValue movementValue)
    {
        _movementVector = movementValue.Get<Vector2>();
    }

    private void OnJump()
    {
        //Debug.Log("j");
        //float x = 0;
        //float y = _jumpSpeed;
        //Vector2 v = new Vector2(x, y);
        ////rb.MovePosition(v);
        //rb.AddForce(v);

    }

    Vector2 Jump()
    {
        Debug.Log("j");
        float x = 0;
        float y = _jumpSpeed;
        return new Vector2(x, y);
        
       
    }
}


