using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    //public float moveSpeed = 5f;

    //public Rigidbody2D rb;

    //Vector2 movement;

    bool isControl = true;

    void Update()
    {
        //movement.x = Input.GetAxisRaw("Horizontal");
        //movement.y = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        //rb.MovePosition(rb.position + movement * moveSpeed * Time.deltaTime);
    }

    void controlInput()
    {
        
        if (Input.GetKeyDown(KeyCode.E))
        {
            //trigger dialogue

        }
    }
}
