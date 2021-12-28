using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collectiblePotion : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            InventoryManager.potionCount++;
            Destroy(gameObject);
        }
    }
}
