using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxScroll : MonoBehaviour
{
    public float initSpeed, acceleration, maxSpeed;
    public float speed;
    public float parallaxEffect = 1;

    public GameObject background; //skal fylde hele kameraet og være child

    private float lengthY, lengthX;
    private Camera cam;
    private Vector2 lastCamPos;
    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();

        lengthY = background.GetComponent<SpriteRenderer>().bounds.size.y;
        lengthX = background.GetComponent<SpriteRenderer>().bounds.size.x;
        speed = initSpeed;
        lastCamPos = cam.transform.position;

        createBackgroundClone(Vector2.up);
        createBackgroundClone(Vector2.down);
        createBackgroundClone(Vector2.left);
        createBackgroundClone(Vector2.right);
        createBackgroundClone(Vector2.right + Vector2.up);
        createBackgroundClone(Vector2.right + Vector2.down);
        createBackgroundClone(Vector2.left + Vector2.down);
        createBackgroundClone(Vector2.left + Vector2.up);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + (Vector3.up * GetComponent<Camera>().orthographicSize));
    }
    // Update is called once per frame
    void Update()
    {
        speed += acceleration * Time.deltaTime; //acceleration
        speed = Mathf.Clamp(speed, -maxSpeed, maxSpeed); //stop ved max speed (maxSpeed kan sættes til infinite)

        background.transform.Translate(((Vector2)cam.transform.position - lastCamPos) * parallaxEffect); //relativt til kamera

        background.transform.Translate(Vector2.up * speed * Time.deltaTime, Space.World); //tyngdekraft
        if (Mathf.Abs(background.transform.position.y - transform.position.y) >= lengthY) //loop tilbage når man går for langt
        {
            background.transform.Translate(Vector2.up * -Mathf.Sign(background.transform.position.y - transform.position.y) * lengthY);
        }
        if (Mathf.Abs(background.transform.position.x - transform.position.x) >= lengthX) //loop tilbage når man går for langt
        {
            background.transform.Translate(Vector2.right * -Mathf.Sign(background.transform.position.x - transform.position.x) * lengthX);
        }
        lastCamPos = cam.transform.position;
    }

    void createBackgroundClone(Vector2 dir)
    {
        GameObject backgroundClone = Instantiate(background, background.transform.position, Quaternion.identity, background.transform);
        for (int i = 0; i < backgroundClone.transform.childCount; i++)
        {
            Destroy(backgroundClone.transform.GetChild(i).gameObject);
        }
        backgroundClone.transform.localScale = Vector2.one;
        backgroundClone.transform.localPosition = dir;
    }
}
