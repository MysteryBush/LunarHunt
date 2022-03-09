using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //public GameObject cameraManager;
    public float speed = 6f;    // The speed that the player will move at.
    private Rigidbody2D playerRigidbody;      // Reference to the player's rigidbody.
    private Vector2 movement;      // The vector to store the direction of the player's movement
    private Animator anim;

    private Vector3 movementZ;
    private Transform playerTransform;

    //bool isDead = false;

    float xValue;
    float yValue;
    //Facing
    float xFace;
    float yFace;
    //is it facing a target?
    public bool isFacingTarget = false;

    void Awake()
    {
        // Set up references.
        playerRigidbody = GetComponent<Rigidbody2D>();
        playerTransform = GetComponent<Transform>();
        anim = GetComponent<Animator>();
        //isControl = GetComponent<PlayerControl>().isControl;

        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y);

        FindObjectOfType<spawnPoint>().setSpawnPoint(this.gameObject);
        
    }

    void FixedUpdate()
    {
        if (GetComponent<PlayerControl>().isControl == true && GetComponent<PlayerControl>().doingAction == false && GetComponent<PlayerControl>().controlUI == false)
        {
            // Store the input axes.
            xValue = Input.GetAxisRaw("Horizontal");
            yValue = Input.GetAxisRaw("Vertical");
            if (isFacingTarget == false)
                Facing();
        }

        else
        {
            //return value back to 0
            xValue = 0f;
            yValue = 0f;
        }

        // Move the player around the scene.
        //Move(x, y);
        MoveZ(xValue, yValue);
        // .magnitude and .sqrMagnitude both works
        // .sqrMagnitude is more optimized because "there's no need to calculate for sqaure root calculation on this vector"
        // Animate the player
        Animating(xValue, yValue);
        anim.SetFloat("Horizontal", movement.x);
        anim.SetFloat("Vertical", movement.y);
        anim.SetFloat("Speed", movement.sqrMagnitude);
        anim.SetFloat("FaceHorizontal", xFace);
        anim.SetFloat("FaceVertical", yFace);
    }

    void MoveZ(float x, float y)
    {
        movement.x = x;
        movement.y = y;

        //playerRigidbody.MovePosition(playerRigidbody.position + movement * speed * Time.fixedDeltaTime);

        //transform.position = new Vector3(transform.position.x + movement.x * speed * Time.fixedDeltaTime, transform.position.y + movement.y * speed * Time.fixedDeltaTime, transform.position.y);
        transform.position = new Vector3(transform.position.x + movement.x * speed * Time.fixedDeltaTime, transform.position.y + movement.y * speed * Time.fixedDeltaTime, transform.position.y + movement.y * speed * Time.fixedDeltaTime);
    }

    void Move(float x, float y)
    {
        // Set the movement vector based on the axis input.
        movement.Set(x, y);


        // Normalise the movement vector and make it proportional to the speed per second
        //movement = movement.normalized * speed * Time.deltaTime;
        movement = movement.normalized * speed * Time.fixedDeltaTime;

        //set position in Vector 2
        Vector2 v2 = transform.position;

        // Move the player to it's current position plus the movement.
        playerRigidbody.MovePosition(v2 + movement);

        //transform.Translate(x * Time.deltaTime * speed, 0, 0);
        //transform.Translate(0, y * Time.deltaTime * speed, 0);

        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y);
    }

    void Facing()
    {
        if (xValue != 0 || yValue != 0)
        {
            //return facing direction
            xFace = xValue;
            yFace = yValue;
        }
    }

    public void targetFacing(Transform target)
    {
        isFacingTarget = true;
        float distance1x = (float)transform.position.x;
        float distance1y = (float)transform.position.y;
        float distance2x = (float)target.position.x;
        float distance2y = (float)target.position.y;
        xFace = distance2x - distance1x;
        yFace = distance2y - distance1y;
    }
    void Animating(float x, float y)
    {
        bool isWalking = x != 0f || y != 0f;
        anim.SetBool("isWalking", isWalking);
    }
}
