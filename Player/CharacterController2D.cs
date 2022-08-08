///////////////////////////////////////////////
/// COPY FROM GITHUB - Credit to ATBrackeys ///
///////////////////////////////////////////////
/////////////// MODFIY BY JEFF ////////////////
///////////////////////////////////////////////
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{
	[SerializeField] private float m_JumpForce;
	[Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;	// How much to smooth out the movement
	[SerializeField] private bool m_AirControl = false;							// Whether or not a player can steer while jumping;
	[SerializeField] private LayerMask m_WhatIsGround;							// A mask determining what is ground to the character
	[SerializeField] private Transform m_GroundCheck;							// A position marking where to check if the player is grounded.
	[Range(0, 200)][SerializeField] private int jfac;							// Jumping Force Add Constat

	const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
	public bool m_Grounded;            // Whether or not the player is grounded.
	private Rigidbody2D m_Rigidbody2D;
	public bool m_FacingRight = true;  // For determining which way the player is currently facing.
	private Vector3 m_Velocity = Vector3.zero;
	

	[Header("Events")]
	[Space]

	public UnityEvent OnLandEvent;

	private void Awake()
	{
		m_Rigidbody2D = GetComponent<Rigidbody2D>();

		if (OnLandEvent == null)
			OnLandEvent = new UnityEvent();

	} 

	private void FixedUpdate()
	{
		bool wasGrounded = m_Grounded;
		m_Grounded = false;

		// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
		// This can be done using layers instead but Sample Assets will not overwrite your project settings.
		Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
				m_Grounded = true;
				if (!wasGrounded)
					OnLandEvent.Invoke();
			}
		}
	}


	public void Move(float move, bool jump)
	{

		//only control the player if grounded or airControl is turned on
		if (m_Grounded || m_AirControl)
		{
			
			// Move the character by finding the target velocity
			Vector3 targetVelocity = new Vector2(move * 100f, m_Rigidbody2D.velocity.y);
			// And then smoothing it out and applying it to the character
			m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

			// If the input is moving the player right and the player is facing left...
			if (move > 0 && !m_FacingRight)
			{
				// ... flip the player.
				Flip();
			}
			// Otherwise if the input is moving the player left and the player is facing right...
			else if (move < 0 && m_FacingRight)
			{
				// ... flip the player.
				Flip();
			}
		}
		// If the player should jump...
		if (m_Grounded && jump)
		{
			m_Grounded = false;
			StartCoroutine("JumpForceAdding"); // Add a vertical force to the player.
		}
	}

	private IEnumerator JumpForceAdding() 
	{
		float orginalgravity = m_Rigidbody2D.gravityScale;	
		for(int i = 1; i < jfac; i++) 
		{

			m_Rigidbody2D.gravityScale = 0;
			if (jfac % 10 == 0)
			{
				m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce/10));
			}
			m_Rigidbody2D.gravityScale = 0;
			yield return new WaitForSeconds(0.00002f);
		}
		m_Rigidbody2D.gravityScale = orginalgravity;
	}

/* 
		for(int i = 1; i < jfac; i++) 
		{
		if ((jfac/2 - jfac/4) <= i && i >= (jfac/2 + jfac/4)) 
		{
			m_Rigidbody2D.gravityScale = 0;
			yield return new WaitForSeconds(0.00005f);
		} else {
			if (jfac % 10 == 0)
			{
				m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce/10));
			}
			m_Rigidbody2D.gravityScale = -1;
			yield return new WaitForSeconds(0.00002f);
		}
*/

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
