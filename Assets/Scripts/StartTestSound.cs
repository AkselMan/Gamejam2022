using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartTestSound : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<AudioManager>().Play("Test");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
