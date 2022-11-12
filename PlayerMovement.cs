using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Stats")]
    public float speed;
    public float fallGravityScale;
    public float idleGravityScale;
    public float risingGravityScale;
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
        m_input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        Move(m_input);
    }

    public void Move(Vector2 move)
    {
        Vector3 targetVelocity = new Vector2(move.x * speed, m_Rigidbody2D.velocity.y);
        m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

        if (move.x > 0 && !m_FacingRight)
        {
            // ... flip the player.
            Flip();
        }
        // Otherwise if the input is moving the player left and the player is facing right...
        else if (move.x < 0 && m_FacingRight)
        {
            // ... flip the player.
            Flip();
        }

        if (move.y < -0.1f) {
            dir = -1;
            m_Rigidbody2D.gravityScale = fallGravityScale;
        }
        else if ( move.y > 0.1) {
            dir = 1;
            m_Rigidbody2D.gravityScale = risingGravityScale;
            
        } else {
            dir = 0;
            m_Rigidbody2D.gravityScale = idleGravityScale;
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
