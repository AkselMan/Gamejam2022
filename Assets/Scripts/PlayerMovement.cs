using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Stats")]
    public float speed;
    public float upForce;
    public float idleForce;
    public float downForce;
    [Space]
    [Header("Movement Settings")]
    [Range(0, .3f)] public float m_MovementSmoothing = .05f;
    private Rigidbody2D m_Rigidbody2D;
    private bool m_FacingRight = true;
    private Vector3 m_Velocity = Vector3.zero;
    [Space]
    [Header("Misc")]
    public PhysicsMaterial2D nonStickMat;
    public PhysicsMaterial2D stickyMat;
    public Vector2 m_input;
    int dir = 0;

    private void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void Update()
    {
        //Input
        m_input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        Move(m_input);
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

    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
