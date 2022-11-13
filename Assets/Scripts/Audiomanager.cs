using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;



public class Audiomanager : MonoBehaviour
{

    public Sound[] sounds;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public AudioMixer mixer;


    public static Audiomanager instance;

    void Awake()
    {

        DontDestroyOnLoad(gameObject);

        

        if (instance == null )
            instance = this;

            else
            {
                Destroy(gameObject);
                return;
            }

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }


    
    }

    public void Play (string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();

    }
}