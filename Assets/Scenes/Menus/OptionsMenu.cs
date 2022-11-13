using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    


    

    public void BackButton()
    {
        
        FindObjectOfType<Audiomanager>().Play("Click");
        Invoke("LoadScene", 0.255f);
    }



    public AudioMixer audioMixer;
    public Audiomanager audioManager;




    public void SetVolume (float volume)
    {
        audioMixer.SetFloat("Volume", volume);
    }


    public void SetQuality (int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        FindObjectOfType<Audiomanager>().Play("Click");
    }


    public void LoadScene()
    {
        SceneManager.LoadScene("MainMenu");
    }

}

