using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;



public class PlayerMovement : MonoBehaviour
{

    public Animator animator;

    [Header("Movement Stats")]
    [Range(0, 5f)] public float speed = 1.5f;
    [Range(0, 750f)] public float upForce = 100f;
    [Range(0, 250f)] public float idleForce = 50f;
    [Range(0, -500f)] public float downForce = -200f;
    [Space]
    [Header("Movement Settings")]
    [Range(0, .3f)] public float m_MovementSmoothing = .05f;
    private Rigidbody2D m_Rigidbody2D;
    private bool m_FacingRight = true;
    private Vector3 m_Velocity = Vector3.zero;
    [Space]
    [Header("Misc")]
    public Transform wallCheck;
    public float wallCheckRadius;
    public LayerMask whatIsGrabable;
    public bool isWallGrabbing;
    public Vector2 m_input;
    public AudioSource windSounds;
    [Space]
    [Header("Shooting")]
    public GameObject gunArm;
    public Transform firePoint;
    private bool aiming;
    private float gunArmRot;
    public float gunArmRotSpeed = 45;
    public GameObject projectile;
    public ParticleSystem shootEffect;
    public float waitTimeShoot = 2;
    float nextShoot;
    public GameObject reloadAnim;

    [Header("Events")]
    [Space]

    public UnityEvent OnWallGrabEvent;


    bool death = false;

    private void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void FixedUpdate()
    {
        if (death)
            return;

        bool wasWallGrabbing = isWallGrabbing;
        isWallGrabbing = false;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(wallCheck.position, wallCheckRadius, whatIsGrabable);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                if (m_input.x < 0 && !m_FacingRight || m_input.x > 0 && m_FacingRight) {
                    isWallGrabbing = true;
                    if (!wasWallGrabbing)
                        OnWallGrabEvent.Invoke();
                }
            }
        }
    }


    public void Update()
    {
        if (death)
            return;
        //Input
        m_input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        Move(m_input);

        animator.SetFloat("speed", Mathf.Abs(Input.GetAxisRaw("Horizontal")));
        animator.SetFloat("Vertical", Input.GetAxisRaw("Vertical"));
        animator.SetBool("isGrabbing", isWallGrabbing);

        if (!isWallGrabbing)
        {
            windSounds.volume = 0.25f;
        } else
        {
            windSounds.volume = 0.15f;
        }

        if (isWallGrabbing)
        {
            gunArm.SetActive(true);
            gunArm.transform.localScale = new Vector3(m_FacingRight ? 1 : -1, m_FacingRight ? -1 : 1, gunArm.transform.localScale.z);
            if (aiming && Input.GetMouseButtonUp(0) && Time.time > nextShoot)
            {
                nextShoot = Time.time + waitTimeShoot;

                aiming = false;
                Shoot();
            }
            aiming = Input.GetMouseButton(0);
            reloadAnim.SetActive(Time.time <= nextShoot);
            if (aiming)
            {
                Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 aimDir = mousePos - (Vector2)transform.position;
                gunArmRot = Mathf.Atan2(aimDir.y, aimDir.x) * Mathf.Rad2Deg;

            }
            else
            {
                gunArmRot = -90;
            }

            gunArm.transform.rotation = Quaternion.Lerp(gunArm.transform.rotation, Quaternion.Euler(0, 0, gunArmRot), gunArmRotSpeed * Time.deltaTime / Quaternion.Angle(Quaternion.Euler(0, 0, gunArmRot), gunArm.transform.rotation));
        }
        else
        {
            aiming = false;
            gunArm.SetActive(false);
        }
    }

    public void Move(Vector2 move)
    {
        Vector3 targetVelocity = new Vector2(move.x * speed, m_Rigidbody2D.velocity.y);
        m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

        //Up and Down force
        if (move.y > 0.1f) {
            //Move Down
            m_Rigidbody2D.AddForce(move.y * upForce * Vector2.up * Time.deltaTime, ForceMode2D.Force);
        } else if (move.y < -0.1f) {
            //Move Up
            m_Rigidbody2D.AddForce(move.y * downForce * Vector2.down * Time.deltaTime, ForceMode2D.Force);

        } 

        //Idle Force
        m_Rigidbody2D.AddForce(idleForce * Vector2.up * Time.deltaTime, ForceMode2D.Force);



        //Flip
        if (move.x > 0 && !m_FacingRight) {
            Flip();
        } else if (move.x < 0 && m_FacingRight) {
            Flip();
        }
    }

    public void Shoot()
    {

        shootEffect.Play();
        Destroy(Instantiate(projectile, firePoint.position, firePoint.rotation), 20);
    }

    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    public void Death()
    {
        m_Rigidbody2D.freezeRotation = false;
        m_Rigidbody2D.AddForce(2000f * Vector2.down);
        death = true;
        Time.timeScale = 0.5f;
        Invoke("LoadDeathScene", 2f);
        FindObjectOfType<Audiomanager>()?.Play("death");
    }

    public void LoadDeathScene()
    {
        SceneManager.LoadScene("DeathScene");
    }
}
