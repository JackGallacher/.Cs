using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {
	
	public float playerSpeed = 5f; //speed has to be 0.05 for touch screen and 10+ for keyboard
	public int lives = 1;
	public int score = 0;	//score is only part of the player class - can call it anywhere using scriptname.variable
	public bool canBeHit = true;
	float timer = 0f;
	
	private bool facingLeft; // Holds if the sprite is facing left
	private Transform myTransform;

	private float amplitude = 0.01f; // How much to float up and down
	private float animSpeed = 2.0f; // The speeed to float

	private Vector3 previousPosition = new Vector3(0,0,0);
	private bool movingVertically = false;
	//Variable to reference Prefab
	public GameObject pfab_Projectile;
	public bool hidden = false; // If the player is hiding

	public AudioClip powerup; // Powerup sound
	public AudioClip gameover;

	private Transform toxicSludge;
	// Use this for initialization
	void Start () {

		facingLeft = true; // Set the initial value

		//Setting myTransform to transform to save on resources
		myTransform = transform;
		
		//Player Spawn Point
		myTransform.position = new Vector3(0,0,0);
		
	}

	void FixedUpdate()
	{

		if (previousPosition == transform.position)
			movingVertically = false;
		else
			movingVertically = true;
	}
	
	// Update is called once per frame
	void Update () 
	{
		previousPosition = transform.position;
		handleKeyPress (); // Handles what happens on key presses
		
		
	//Player Movement
	//PC Controls
	//Horizontal
	
		if (facingLeft)
		{
			myTransform.Translate (Vector3.right * Input.GetAxis("Horizontal") * playerSpeed * Time.deltaTime);
		}
		else
		{
			myTransform.Translate (Vector3.right * Input.GetAxis("Horizontal") * -playerSpeed * Time.deltaTime);
		}
		
	//Vertical

		if (!movingVertically) // Float player if not moving vertically
		{
			transform.position = new Vector3(transform.position.x, transform.position.y + (amplitude * Mathf.Sin(animSpeed*Time.time)), transform.position.z); // Create float effect
		}

			myTransform.Translate (Vector3.up * Input.GetAxis("Vertical") * playerSpeed * Time.deltaTime);

	
	
	//Touch screen controls	
	//if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
	//	{
	//		Vector3 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
	//		myTransform.Translate (touchDeltaPosition.x * playerSpeed, touchDeltaPosition.y * playerSpeed, 0);
	//	}
		
	
	//Keeping player inside the screen regardless of resolution
	float left = Camera.main.ViewportToWorldPoint(Vector3.zero).x;
	float right = Camera.main.ViewportToWorldPoint(Vector3.one).x;
	float top = Camera.main.ViewportToWorldPoint(Vector3.zero).y + 1.3f;
	float bottom = Camera.main.ViewportToWorldPoint(Vector3.one).y - 2.0f;
	
	float x = myTransform.position.x;
	float y = myTransform.position.y;
	
	if(myTransform.position.x <= left + GetComponent<Renderer>().bounds.extents.x)
		{
			x = left + GetComponent<Renderer>().bounds.extents.x;
		}
	else if(myTransform.position.x >= right - GetComponent<Renderer>().bounds.extents.y)
		{
			x = right - GetComponent<Renderer>().bounds.extents.x;
		}
		
	if (myTransform.position.y <= top + GetComponent<Renderer>().bounds.extents.y)
		{
         	y = top + GetComponent<Renderer>().bounds.extents.y;
       	}
	else if (myTransform.position.y >= bottom - GetComponent<Renderer>().bounds.extents.y)
		{
         	y = bottom - GetComponent<Renderer>().bounds.extents.y;
       	}
		
		myTransform.position = new Vector3 (x, y, transform.position.z);
		
	//Player dies when lives = 0
	if(lives <= 0)
	{
		Destroy(this.gameObject);
			Camera.main.GetComponent<EndGame>().enabled = true;
	}
		
		
		
	//Prints the lives and score to the console
//	print("Lives: " + lives + " Score: " + score);
		
		
		
	//Stops player getting hit too often
	if(Time.time - timer > 0.2)
		{
			
			canBeHit = true;
			
		}
}
	
	
	
//Player loses a life when collision with enemy
void OnTriggerEnter(Collider collider)
	{
		if(collider.gameObject.CompareTag("Enemy") && canBeHit == true)
		{
			//lives -= 1;
			
			//Stops player getting hit too often
			canBeHit = false;
			timer = Time.time;
		}
	}

void handleKeyPress()
{
	if (Input.GetKeyDown (KeyCode.A) && !facingLeft)
	{
			flip ();
	}
	else if (Input.GetKeyDown (KeyCode.D) && facingLeft)
	{
		flip ();
	}
	

}

	void flip()
	{
		Vector3 rotation = transform.eulerAngles;
		if (facingLeft)
		{
			rotation.y = 180;
			facingLeft = false;
		}
		else
		{
			rotation.y = 0;
			facingLeft = true;
		}
		// Switch the direction the sprite is facing

		transform.eulerAngles = rotation;
	}

	void OnTriggerStay2D(Collider2D collider)
	{
		if (collider.gameObject.tag.Contains ("Barrel"))
		{
			if (toxicSludge.GetComponent<ParticleEmitter>().maxSize == 0 && toxicSludge.GetComponent<ParticleEmitter>().minSize == 0 && !toxicSludge.GetComponent<Sludge>().sludgeGone)
			{
				toxicSludge.GetComponent<Sludge>().sludgeGone = true;
				if ((Camera.main.GetComponent<Pollution>().pollutionLevel) >= Camera.main.GetComponent<Pollution>().minPollution + 5) // TODO: Remove static value
					Camera.main.GetComponent<Pollution>().pollutionLevel -= 5;
				else
					Camera.main.GetComponent<Pollution>().pollutionLevel = Camera.main.GetComponent<Pollution>().minPollution;

			}
			else
			{
				toxicSludge.GetComponent<ParticleEmitter>().maxSize -= (1f * Time.deltaTime);
				toxicSludge.GetComponent<ParticleEmitter>().minSize -= (1f * Time.deltaTime);
			}

		}
	}


	void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.gameObject.tag.Contains ("Barrel"))
		{
			
			foreach (Transform child in collider.transform)
			{
				if (child.name == "ToxicSludge")
				{
					print ("Woop");
					toxicSludge = child;
					break;
				}
				
			}
		}
		if (collider.gameObject.tag == "Hidey")
		{
			hidden = true;
		}
		if(collider.gameObject.tag.Contains ("Powerup")) //Powerup
		{
			print ("Colliding with powerup");
			if (collider.gameObject.tag.Contains("Speed"))
			{
				GetComponent<Powerups>().powerupType = "Speed";
			}
			if (collider.gameObject.tag.Contains("Down"))
			{
				GetComponent<Powerups>().powerupType = "SpeedDown";
			}
			if (collider.gameObject.tag.Contains("Invincible"))
			{
				GetComponent<Powerups>().powerupType = "Invincible";
			}
			
			GetComponent<Powerups>().powerupCol = true;
			GetComponent<Spawner>().powerupOnScreen = false;
			Destroy(collider.gameObject);
			GetComponent<AudioSource>().PlayOneShot(powerup, 0.1f); // Play the eat sound
			
		}

		if(collider.gameObject.tag.Contains("Boat"))
			print ("Colliding with boat");
	}

	void OnTriggerExit2D(Collider2D collider)
	{
		if (collider.gameObject.tag == "Hidey")
		{
			hidden = false;
		}
	}

	void OnParticleCollision(GameObject other)
	{
		print ("Collided with particles");
	}

}