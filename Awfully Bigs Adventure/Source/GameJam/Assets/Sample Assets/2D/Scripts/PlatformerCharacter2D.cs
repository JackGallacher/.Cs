using UnityEngine;

public class PlatformerCharacter2D : MonoBehaviour 
{
	bool facingRight = true;							// For determining which way the player is currently facing.

	[SerializeField] float maxSpeed = 10f;				// The fastest the player can travel in the x axis.
	[SerializeField] float jumpForce = 400f;			// Amount of force added when the player jumps.	

	[Range(0, 1)]
	[SerializeField] float crouchSpeed = .36f;			// Amount of maxSpeed applied to crouching movement. 1 = 100%
	
	[SerializeField] bool airControl = false;			// Whether or not a player can steer while jumping;
	[SerializeField] LayerMask whatIsGround;			// A mask determining what is ground to the character
	
	Transform groundCheck;								// A position marking where to check if the player is grounded.
	float groundedRadius = .2f;							// Radius of the overlap circle to determine if grounded
	bool grounded = false;								// Whether or not the player is grounded.
	Transform ceilingCheck;								// A position marking where to check for ceilings
	float ceilingRadius = .01f;							// Radius of the overlap circle to determine if the player can stand up
	Animator anim;										// Reference to the player's animator component.

	public int health;// sets the health of the character
	public int good_karma;// sets the good karma of the character
	public int bad_karma;// sets the bas karma of the character.
	public float plant_health = 100;//sets the health of the plant. 

	public bool footstep_play = false;

	public GameObject explosion; 
	
	public bool speed_boost = false;
	float speed_timer = 3;//length of speed boost buff.

	public bool speed_slow = false;
	float slow_timer = 3;//length of the slow speed buff.
	
    void Awake()
	{
		// Setting up references.
		groundCheck = transform.Find("GroundCheck");
		ceilingCheck = transform.Find("CeilingCheck");
		anim = GetComponent<Animator>();
	}

	void FixedUpdate()
	{
		// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
		grounded = Physics2D.OverlapCircle (groundCheck.position, groundedRadius, whatIsGround);
		anim.SetBool ("Ground", grounded);

		// Set the vertical animation
		anim.SetFloat ("vSpeed", GetComponent<Rigidbody2D>().velocity.y);

		if(speed_boost == true)//timer for speed boost.
		{
			speed_timer -= Time.deltaTime;
			if(speed_timer < 0)
			{
				speed_boost = false;
				maxSpeed = 10f;//brings speed back to normal when timer is zero.
				speed_timer = 3;
			}
		}
		if(speed_slow == true)// timer for speed slow
		{
			slow_timer -= Time.deltaTime;
			if(slow_timer < 0)
			{
				speed_slow = false;
				maxSpeed = 10f;
				slow_timer = 3;
			}
		}
		if(plant_health > 100)//makes it so you cant go above 100 health.
		{
			plant_health = 100;
		}
		if(plant_health <= 0)
		{
			Application.LoadLevel("LoseGameMenu");
		}
	}
	public void Move(float move, bool crouch, bool jump)
	{
		if(grounded || airControl)
		{
			// Reduce the speed if crouching by the crouchSpeed multiplier
			//move = (crouch ? move * crouchSpeed : move);

			// The Speed animator parameter is set to the absolute value of the horizontal input.
			anim.SetFloat("Speed", Mathf.Abs(move));


			// Move the character
			GetComponent<Rigidbody2D>().velocity = new Vector2(move * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);
			
			// If the input is moving the player right and the player is facing left...
			if(move > 0 && !facingRight)
				// ... flip the player.
				Flip();
			// Otherwise if the input is moving the player left and the player is facing right...
			else if(move < 0 && facingRight)
				// ... flip the player.
				Flip();
		}
        // If the player should jump...
        if (grounded && jump) {
            // Add a vertical force to the player.
            anim.SetBool("Ground", false);
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce));
        }
	}
		
	void Flip ()
	{
		// Switch the way the player is labelled as facing.
		facingRight = !facingRight;
		
		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		if(col.gameObject.tag == "Enemy")
		{
			Instantiate(explosion, col.transform.position, col.transform.rotation); 
			Destroy(col.gameObject);
			plant_health -= 20;//takes 5 from the health of the char when a building is hit.
		}
		if (col.gameObject.tag == "powerup_speed")//collision for speed power up. 
		{ 
				speed_boost = true;//sets the speed boost bool to true;
				Destroy (col.gameObject);//destroys the speed power up.
				maxSpeed = 20f;//increseas speed of object.
		} 
		if (col.gameObject.tag == "powerup_slow")//collision for speed power up. 
		{ 
			speed_slow = true;//sets the slow boost bool to true;
			Destroy (col.gameObject);//destroys the slow power up.
			maxSpeed = 2f;//slows speed of object.
		} 
		if (col.gameObject.tag == "powerup_health")//collision for speed power up. 
		{ 
			Destroy (col.gameObject);//destroys the speed power up.
			plant_health += 10;

		} 
		if (col.gameObject.tag == "EndGame")
		{
			Application.LoadLevel("WinGameMenu");
		}
	}
	void OnGUI()//displays scores on the side of the screen.
	{
		GUI.Label(new Rect(10, 10, 100, 20), "Plant Health: " + plant_health);
	}
}
