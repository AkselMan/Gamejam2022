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
        
        FindObjectOfType<AudioManager>().Play("Click");
        Invoke("LoadScene", 0.255f);
    }



    public AudioMixer audioMixer;
    public AudioManager audioManager;




    public void SetVolume (float volume)
    {
        audioMixer.SetFloat("Volume", volume);
    }


    public void SetQuality (int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        FindObjectOfType<AudioManager>().Play("Click");
    }


    public void LoadScene()
    {
        SceneManager.LoadScene("MainMenu");
    }

}

