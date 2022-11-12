using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    public PlayerMovement playerMovement;

    public Transform firePoint;
    public Transform armAnchor;

    private bool aiming = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        aiming = Input.GetMouseButton(0);
        armAnchor.gameObject.SetActive(aiming);
        if (aiming)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 aimDir = mousePos - (Vector2)transform.position;
            armAnchor.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(aimDir.y, aimDir.x) * Mathf.Rad2Deg);

            if (Input.GetMouseButtonUp(0)) Shoot();
        }
    }
    public void Shoot()
    {

    }
}
