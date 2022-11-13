using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public static SceneTransition instance;

    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        if (instance != null) Destroy(gameObject);

        instance = this;
        DontDestroyOnLoad(gameObject);
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void loadScene(int scene)
    {
        StartCoroutine(loadScene(scene, 0.2f));
    }
    public void loadScene(string scene)
    {
        StartCoroutine(loadScene(scene, 0.2f));
    }
    IEnumerator loadScene(int scene, float waitTime)
    {
        anim.SetTrigger("Close");

        yield return new WaitForSeconds(waitTime);

        SceneManager.LoadScene(scene);

        anim.SetTrigger("Open");
    }
    IEnumerator loadScene(string scene, float waitTime)
    {
        anim.SetTrigger("Close");

        yield return new WaitForSeconds(waitTime);

        SceneManager.LoadScene(scene);

        anim.SetTrigger("Open");
    }
}
