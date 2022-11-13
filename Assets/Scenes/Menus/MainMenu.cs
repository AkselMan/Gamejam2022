using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void GameScene()
    {
        
        FindObjectOfType<Audiomanager>().Play("Click");
        Invoke("LoadGameScene", 0.255f);
    }

    public void OptionsScene()
    {
        FindObjectOfType<Audiomanager>().Play("Click");
        Invoke("LoadOptionsScene", 0.255f);
        

    }

    

    public void QuitGame()
    {
        Application.Quit();
        FindObjectOfType<Audiomanager>().Play("Click");
    }

    public void LoadOptionsScene()
    {
        SceneManager.LoadScene("Options");
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene("TestGameScene");

    }

    void Start()
    {
        
        FindObjectOfType<Audiomanager>().Play("MainMenu");
        
            
            
        
        
        
    }


}
