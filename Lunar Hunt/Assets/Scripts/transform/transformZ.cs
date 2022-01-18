using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class transformZ : MonoBehaviour
{
    public float offsetZ = 0;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y + offsetZ);
    }
}
