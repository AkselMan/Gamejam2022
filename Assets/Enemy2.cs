using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    public Transform player;
    public LineRenderer lr;
    public LayerMask laserMask;
    public float deathZone;
    public GameObject explosion;
    public bool shooting = false;
    public float rotateSpeed = 10;
    private bool kamikazed = false;
    public GameObject parachute;
    public AudioSource laserSound;

    public void Start()
    {
        player = FindObjectOfType<PlayerMovement>().transform;

        StartCoroutine(onoff());
    }

    public void FixedUpdate()
    {
        Vector3 diff = player.position - transform.position;
        diff.Normalize();

        transform.position = player.position + (Vector3)DroneMovement.Rotate((transform.position - player.position).normalized * 3, rotateSpeed * Time.fixedDeltaTime);


        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z);

        if (shooting)
        {
            laserSound.enabled = true;
            lr.enabled = true;

            lr.SetPosition(0, transform.position);

            RaycastHit2D hit = Physics2D.Raycast(transform.position, diff, 20, laserMask);
            if (hit)
            {
                lr.SetPosition(1, hit.point);
                if (hit.collider.CompareTag("Player"))
                {
                    print("Death by beam");
                    hit.collider.GetComponent<HealthManager>().takeDamage(1);
                    StopCoroutine(onoff());
                    StartCoroutine(onoff());
                }
            }
            else
            {
                lr.SetPosition(1, transform.position + (diff.normalized * 20));
            }
        }else
        {
            laserSound.enabled = false;
            lr.enabled = false;
        }
        

        if ((player.position - transform.position).sqrMagnitude <= Mathf.Pow(deathZone, 2))
        {
            if (!kamikazed) Kamikaze();
        }
    }

    public void Kamikaze()
    {
        kamikazed = true;
        Instantiate(explosion, transform.position, Quaternion.identity);
        player.GetComponent<HealthManager>().Death();
    }
    IEnumerator onoff()
    {
        while (true)
        {
            shooting = false;
            yield return new WaitForSeconds(4);

            shooting = true;
            yield return new WaitForSeconds(5);
        }
    }
    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, deathZone);
    }
    public void Death()
    {
        FindObjectOfType<Audiomanager>()?.Play("explosion");
        Instantiate(explosion, transform.position, Quaternion.identity);
        Instantiate(parachute, new Vector3(0,0,0), Quaternion.identity);
        Destroy(gameObject);
    }
}
