using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;



public class DeathMenu : MonoBehaviour




{
    // Start is called before the first frame update





    void Start()
    {
        FindObjectOfType<Audiomanager>().Play("DarkClick");
        Invoke("BackToMenu", 5f);
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
