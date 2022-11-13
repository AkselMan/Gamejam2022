using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class DroneMovement : MonoBehaviour
{
    public Transform player;
    public Vector2 target;

    [Header("Movement")]
    public float moveSpeed;
    public float intervalWaitTime;
    public float moveReach, minMoveReach, attackReach;
    public LayerMask moveBlocker;
    float f = 0;

    [Header("Attacking")]
    public GameObject projectile;
    public Transform firePoint;
    public float waitTimeShoot = 1;
    private float nextShoot = 0;
    private bool lastReach;
    public GameObject gunGFX;

    public GameObject explosionfx;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerMovement>().transform;
        target = transform.position;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, moveReach);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, minMoveReach);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, attackReach);
    }
    // Update is called once per frame
    void Update()
    {
        Vector2 dir = target - (Vector2)transform.position;
        if (dir.sqrMagnitude > Mathf.Pow(moveSpeed * Time.deltaTime, 2)) //det her tjekker bare afstand, men mere processor-venligt
            transform.Translate(dir.normalized * moveSpeed * Time.deltaTime, Space.World);
        else transform.position = target;

        if (Time.time > f)
        {
            f = Time.time + intervalWaitTime;
            Move();
        }

        //if (Input.GetMouseButtonDown(0)) Move();

        dir = player.position - transform.position;
        gunGFX.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + 90);

        if (Vector2.Distance(player.position, transform.position) <= attackReach) {
            if (!lastReach) nextShoot = Time.time + waitTimeShoot;

            if (Time.time > nextShoot) //pew pew
            {
                nextShoot = Time.time + waitTimeShoot;

                Instantiate(projectile, firePoint.position, firePoint.rotation);
            }
            lastReach = true;
        }else
        {
            lastReach = false;
        }
    }
    public void Move() //bev?ger den i intervaler
    {
        RaycastHit2D hit = new RaycastHit2D();
        bool hasDir = false;
        Vector2 dir = Vector2.zero;
        bool inReach = Vector2.Distance(player.position, transform.position) <= attackReach;
        for (int i = 0; i < 10; i++) //hvis nu den ikke kan bev?ge sig nogen steder, s? skal vi ligesom ikke have et crash
        {
            if (inReach) dir = Random.onUnitSphere;
            else dir = Rotate(player.position - transform.position, Random.Range(-30f, 30f)).normalized;

            hit = Physics2D.Raycast(transform.position, dir, moveReach, moveBlocker);

            if (!hit || hit.distance > minMoveReach) { hasDir = true; break; }
        }
        if (!hasDir) return;
        
        target = dir * Random.Range(minMoveReach, hit ? hit.distance : moveReach) + (Vector2)transform.position;


    }
    public static Vector2 Rotate(Vector2 v, float degrees)
    {
        float sin = Mathf.Sin(degrees * Mathf.Deg2Rad);
        float cos = Mathf.Cos(degrees * Mathf.Deg2Rad);

        float tx = v.x;
        float ty = v.y;
        v.x = (cos * tx) - (sin * ty);
        v.y = (sin * tx) + (cos * ty);
        return v;
    }

    public void Death()
    {

        FindObjectOfType<CameraShaker>()?.ShakeOnce(3f, 3f, 1f, 1f);
        FindObjectOfType<Audiomanager>()?.Play("explosion");
        Instantiate(explosionfx, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
