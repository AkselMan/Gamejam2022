using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathTrigger : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collision)
    {
        print("DEAD");
        FindObjectOfType<PlayerMovement>().Death();
    }
}
