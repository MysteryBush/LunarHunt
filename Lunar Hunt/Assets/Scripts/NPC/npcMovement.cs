using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npcMovement : MonoBehaviour
{
    public float speed = 6f;    // The speed that the npc will move at.
    private Rigidbody2D npcRigidbody;      // Reference to the npc's rigidbody.
    private Vector2 movement;      // The vector to store the direction of the npc's movement
    private Animator anim;

    private Vector3 movementZ;
    private Transform npcTransform;

    bool isControl = true;
    bool isDead = false;

    float xValue;
    float yValue;
    //Facing
    float xFace;
    float yFace;

    //player Transform
    Transform playerTransform;

    void Awake()
    {
        // Set up references.
        npcRigidbody = GetComponent<Rigidbody2D>();
        npcTransform = GetComponent<Transform>();

        anim = GetComponent<Animator>();

        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y);
    }

    void FixedUpdate()
    {
        if (isControl == true)
        {
            // Store the input axes.
            Facing();
        }

        else
        {
            //return value back to 0
            xValue = 0f;
            yValue = 0f;
        }

        // Move the npc around the scene.
        //Move(x, y);
        MoveZ(xValue, yValue);
        // .magnitude and .sqrMagnitude both works
        // .sqrMagnitude is more optimized because "there's no need to calculate for sqaure root calculation on this vector"
        // Animate the npc
        Animating(xValue, yValue);
        anim.SetFloat("Horizontal", movement.x);
        anim.SetFloat("Vertical", movement.y);
        anim.SetFloat("Speed", movement.sqrMagnitude);
        anim.SetFloat("FaceHorizontal", xFace);
        anim.SetFloat("FaceVertical", yFace);
        toggleControl();
    }

    void MoveZ(float x, float y)
    {
        movement.x = x;
        movement.y = y;

        //npcRigidbody.MovePosition(npcRigidbody.position + movement * speed * Time.fixedDeltaTime);

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

        // Move the npc to it's current position plus the movement.
        npcRigidbody.MovePosition(v2 + movement);

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

    void findPlayer()
    {
        Debug.Log("Finding player");
        playerTransform = FindObjectOfType<PlayerMovement>().GetComponent<Transform>();
    }

    public void faceToPlayer()
    {
        findPlayer();
        float distance1x = (float)transform.position.x;
        float distance1y = (float)transform.position.y;
        float distance2x = (float)playerTransform.transform.position.x;
        float distance2y = (float)playerTransform.transform.position.y;
        //float distance2x = (float)FindObjectOfType<PlayerMovement>().GetComponent<Transform>().transform.position.x;
        //float distance2y = (float)FindObjectOfType<PlayerMovement>().GetComponent<Transform>().transform.position.y;

        xFace = distanceMath(distance1x, distance2x);
        yFace = distanceMath(distance1y, distance2y);
    }

    float distanceMath(float distance1, float distance2)
    {
        //if (distance1 > distance2)
        //{
        //    Debug.Log("-1");
        //    return -1;
        //}
        //if (distance1 < distance2)
        //{
        //    Debug.Log("1");
        //    return 1;
        //}
        //else
        //{
        //    Debug.Log("0");
        //    return 0;
        //}

        return distance2 - distance1;
    }

    void Animating(float x, float y)
    {
        bool isWalking = x != 0f || y != 0f;
        anim.SetBool("isWalking", isWalking);
    }

    public void toggleControl()
    {
        //if (DialogueManager.ins.isDone == true)
        //{
        //    isControl = true;
        //}
        //if (DialogueManager.ins.isDone == false)
        //{
        //    isControl = false;
        //}
        if (anim.GetBool("isDead") == true)
        {
            isControl = false;
        }
        if (anim.GetBool("isDead") == false)
        {
            isControl = true;
        }
    }
}
