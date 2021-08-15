using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
    
{
    public float _speed = 5f;
    public float _jumpSpeed = 5000f;

    Rigidbody2D selfRigidbody;

    void Start()
    {
        selfRigidbody = GetComponent<Rigidbody2D>();
    }

    

    //void FixedUpdate()
    //{
    //    if (_canJump)
    //    {
    //        _canJump = false;
    //        selfRigidbody.AddForce(new Vector2(0, _jumpSpeed));
    //    }
    //}



    void Update()
    {
        //float HzMovement = Input.GetAxis("Horizontal");

        //gameObject.transform.position = new Vector2(transform.position.x + (HzMovement * _speed * Time.deltaTime), transform.position.y);

        //if (Input.GetButtonDown("Jump"))
        //{
        //    selfRigidbody.AddForce(new Vector2(0f, _jumpSpeed * Time.deltaTime));
        //}

    }
}
