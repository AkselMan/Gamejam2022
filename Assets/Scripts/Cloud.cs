using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    public Sprite[] sprites;
    public float speed;

    public void Start()
    {
        transform.localScale = new Vector3(Random.Range(0.8f, 1.2f), Random.Range(0.8f, 1.2f), 0);
        Destroy(gameObject, 120);
        GetComponent<SpriteRenderer>().sprite = sprites[Random.Range(0, sprites.Length)];
    }

    public void FixedUpdate()
    {
        transform.position += new Vector3(0, speed, 0);
    }
}
