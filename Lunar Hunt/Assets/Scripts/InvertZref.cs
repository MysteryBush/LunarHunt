using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvertZref : MonoBehaviour
{
    public GameObject refObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        gameObject.transform.position = new Vector3 (transform.position.x, transform.position.y, refObject.transform.position.z);
    }
}
