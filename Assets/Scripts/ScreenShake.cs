using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ShakeOptions
{
    public float Strength;
    public float Duration;
}

public class ScreenShake : MonoBehaviour
{
    float shake = 0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale <= 0)
            return;
        Vector2 newPos = Random.onUnitSphere * shake;
        transform.position = new Vector3(newPos.x, newPos.y, transform.position.z);
    }
    public void screenShake(float strength, float length)
    {
        StartCoroutine(shaker(strength, length));
    }
    public void screenShake(ShakeOptions shakeOptions)
    {
        StartCoroutine(shaker(shakeOptions.Strength, shakeOptions.Duration));
    }
    IEnumerator shaker(float strength, float length)
    {
        shake += strength;
        yield return new WaitForSeconds(length);
        shake -= strength;
    }
}