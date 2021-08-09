using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Temporary vector
     Vector3 temp = GameObject.Find("Robi").transform.position;
     temp.x = transform.position.x;
     temp.y = transform.position.y;
     temp.z = -10;
    //  // Assign value to Camera position
     transform.position = temp;
     transform.rotation = new Quaternion(0, 0, 0, 0);
    }
}
