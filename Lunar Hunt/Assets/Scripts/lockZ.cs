using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lockZ : MonoBehaviour
{
    public float zValue = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, zValue);
    }
}
