using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ParallaxObject
{
    public string name;
    public GameObject obj;
    public float parallaxEffect;
    public Vector2 initSpeed = Vector2.zero, acceleration = Vector2.zero, maxSpeed = Vector2.positiveInfinity;
    public Vector2 speed;
    public Vector2 size;
    public Vector2 offset;
    public bool loopX, loopY;

    public void Tick(Camera cam, Vector2 lastCamPos)
    {
        speed += acceleration * Time.deltaTime; //acceleration
        speed = new Vector2(Mathf.Clamp(speed.x, -maxSpeed.x, maxSpeed.x), Mathf.Clamp(speed.y, -maxSpeed.y, maxSpeed.y)); //stop ved max speed (maxSpeed kan sættes til infinite)

        obj.transform.Translate(((Vector2)cam.transform.position - lastCamPos) * parallaxEffect);
        obj.transform.Translate(speed * Time.deltaTime, Space.World); //tyngdekraft

        Vector2 objOffset = obj.transform.position - cam.transform.position - (Vector3)offset;

        if (loopY && Mathf.Abs(objOffset.y) >= size.y) //loop tilbage når man går for langt
        {
            obj.transform.Translate(Vector2.up * -Mathf.Sign(objOffset.y) * size.y);
        }
        if (loopX && Mathf.Abs(objOffset.x) >= size.x) //loop tilbage når man går for langt
        {
            obj.transform.Translate(Vector2.right * -Mathf.Sign(objOffset.x) * size.x);
        }
    }

    public void createBackgroundClone(Vector2 dir)
    {
        GameObject background = obj;
        GameObject backgroundClone = GameObject.Instantiate(background, background.transform);
        for (int i = 0; i < backgroundClone.transform.childCount; i++)
        {
            GameObject.Destroy(backgroundClone.transform.GetChild(i).gameObject);
        }
        backgroundClone.transform.localScale = Vector2.one;
        backgroundClone.transform.position = background.transform.position + (Vector3)(size * dir);

    }
}

public class ParallaxScroll : MonoBehaviour
{
    /*public float initSpeed, acceleration, maxSpeed;
    public float speed;
    public float parallaxEffect = 1;

    public GameObject background; //skal fylde hele kameraet og være child*/

    public ParallaxObject[] parallaxObjects;

    private float lengthY, lengthX;
    private Camera cam;
    private Vector2 lastCamPos;
    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();

        foreach(ParallaxObject parObj in parallaxObjects)
        {
            parObj.speed = parObj.initSpeed;

            if (parObj.loopY)
            {
                parObj.createBackgroundClone(Vector2.up);
                parObj.createBackgroundClone(Vector2.down);
            }
            if (parObj.loopX)
            {
                parObj.createBackgroundClone(Vector2.left);
                parObj.createBackgroundClone(Vector2.right);
            }
            if (parObj.loopX && parObj.loopY)
            {
                parObj.createBackgroundClone(Vector2.right + Vector2.up);
                parObj.createBackgroundClone(Vector2.right + Vector2.down);
                parObj.createBackgroundClone(Vector2.left + Vector2.down);
                parObj.createBackgroundClone(Vector2.left + Vector2.up);
            }
            
        }

        lastCamPos = cam.transform.position;

        
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + (Vector3.up * GetComponent<Camera>().orthographicSize));
    }
    // Update is called once per frame
    void Update()
    {
        foreach (ParallaxObject parObj in parallaxObjects)
        {
            parObj.Tick(cam, lastCamPos);
        }



        /*background.transform.Translate(((Vector2)cam.transform.position - lastCamPos) * parallaxEffect); //relativt til kamera

        background.transform.Translate(Vector2.up * speed * Time.deltaTime, Space.World); //tyngdekraft
        if (Mathf.Abs(background.transform.position.y - transform.position.y) >= lengthY) //loop tilbage når man går for langt
        {
            background.transform.Translate(Vector2.up * -Mathf.Sign(background.transform.position.y - transform.position.y) * lengthY);
        }
        if (Mathf.Abs(background.transform.position.x - transform.position.x) >= lengthX) //loop tilbage når man går for langt
        {
            background.transform.Translate(Vector2.right * -Mathf.Sign(background.transform.position.x - transform.position.x) * lengthX);
        }
        */
        lastCamPos = cam.transform.position;
    }

    
}
