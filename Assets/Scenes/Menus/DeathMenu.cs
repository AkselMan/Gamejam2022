using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;



public class DeathMenu : MonoBehaviour




{
    // Start is called before the first frame update


    SpriteRenderer rend;


    void Start()
    {
        FindObjectOfType<AudioManager>().Play("DarkClick");
    }













    // Update is called once per frame
    void Update()
    {
        


    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
