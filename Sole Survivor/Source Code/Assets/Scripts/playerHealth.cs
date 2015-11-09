using UnityEngine;
using System.Collections;

public class playerHealth : MonoBehaviour {

	// Public Section
	public int maxHealth; // Player max health
	public int currentHealth; // Current player health
	public Texture2D healthBarFront; // The health bar colour
	public Texture2D healthBarBack; // The health bar white
	public GUIStyle healthBarStyle; // The style of the health bar
	public float glowDuration = 1.0f; // The duration of the player glow.
	public Color lerpCol = Color.white; // The start colour of the player glow.
	public int drainInterval = 1; // The rate at which the health drains after pollution is maxed out.
	private float drainTimer; // Used with the drain interval.
	public Sprite yuck; // The prefab displayed when eating a polluted fish.
	public Sprite yum; // The prefab displayed when eating a yummy fish.
	// Private Section
	private float healthDisplay; // The health which displays to the screen.

	// 720p
	private float maxScreenWidth = 1280;
	private float maxScreenHeight = 720;
	private float healthBarWidth;  // The width of the health bar taking resolution into account
	private float healthBarHeight; // The height of the health bar taking resolution into account

	public AudioClip eatSound;
	// Use this for initialization
	void Start () 
	{
		healthBarWidth =(healthBarFront.width / maxScreenWidth)*Screen.width; // The width of the health bar based on resolution
		healthBarHeight = (healthBarFront.height / maxScreenHeight)*Screen.height;
		//drainInterval = 1; // Set the default drain interval
		drainTimer = drainInterval; // Set the drain timer.
		currentHealth = maxHealth; // Set the current health.
	}
	
	// Update is called once per frame
	void Update () {
	
		checkPollution(); // Check the pollution level
		healthDisplay = (((float)healthBarFront.width / 100f) * (((float)currentHealth / (float)maxHealth) * 100)); // Calculate the health to display
	}

	void OnTriggerEnter2D(Collider2D collider) 
	{
		if (!collider.gameObject.tag.Contains ("Barrel"))
		    {
		if (collider.gameObject.transform.localScale.y > transform.localScale.y && collider.gameObject.tag.Contains("MarineLife")) // If the collided fish is bigger
		{
			print ("Scale of Fish: " + collider.gameObject.transform.localScale.y);
			print ("Scale of Player: " + transform.localScale.y);

				if (!GetComponent<Powerups>().invincibleOn)
					GetComponent<PlayerScript>().lives = 0; // Player Gets Eaten
		}
		else if (collider.gameObject.transform.localScale.y < transform.localScale.y && collider.gameObject.tag.Contains("MarineLife") || collider.gameObject.tag.Contains("Pollution")) // If the collided fish is smaller
		{
			// Eat the fish / pollution item

			if (collider.gameObject.tag.Contains ("Pollution")) // Polluted Fish / Pollution item
				{Camera.main.GetComponent<Statistics>().pollutionEaten++; // Increase the amount of pollution eaten
				collider.gameObject.GetComponent<SpriteRenderer>().color = Color.white; // Make sure the colour isn't effected by others
				collider.gameObject.GetComponent<SpriteRenderer>().sprite = yuck; // Set the sprite

					if (!GetComponent<Powerups>().invincibleOn)
						currentHealth -= 10; // Decrease the player's health



				if (collider.gameObject.tag.Contains ("MarineLife")) // If marine life
				{
						Camera.main.GetComponent<Statistics>().eaten++; // Increase the amount of fish eaten
						if ((Camera.main.GetComponent<Pollution>().pollutionLevel) >= 1) // TODO: Remove static value
							Camera.main.GetComponent<Pollution>().pollutionLevel -= 1;
					else
							Camera.main.GetComponent<Pollution>().pollutionLevel = 0;
				}
				else // If pollution that was dropped
				{
						if ((Camera.main.GetComponent<Pollution>().pollutionLevel) >= Camera.main.GetComponent<Pollution>().minPollution + 5) // TODO: Remove static value
							Camera.main.GetComponent<Pollution>().pollutionLevel -= 5;
					else
							Camera.main.GetComponent<Pollution>().pollutionLevel = Camera.main.GetComponent<Pollution>().minPollution;
				}
				checkHealth (); // Check the health of the player
			}
			else
			{
					Camera.main.GetComponent<Statistics>().eaten++; // Increase the amount of fish eaten
				collider.gameObject.GetComponent<SpriteRenderer>().color = Color.white; // Make sure the colour isn't effected by others
				collider.gameObject.GetComponent<SpriteRenderer>().sprite = yum; // Set the sprite
					GetComponent<PlayerScript>().score += 1; // Increase the player's score
				if (currentHealth < maxHealth) // Make sure the player's health does not increase above the maximum
				{
					currentHealth += 1; // Increase the player's health
				}
			}

			collider.gameObject.GetComponent<Collider2D>().isTrigger = true; // Disable the collider
			Destroy(collider.gameObject.GetComponent<Movement>()); // Disable movement
			Destroy(collider.gameObject.GetComponent<Rigidbody2D>()); // Disable Rigidbody
			Destroy(collider.gameObject.GetComponent<BoxCollider2D>()); // Disable Box Collider
			collider.transform.localScale = new Vector3(3, 3, 3); // Set the scale
			collider.gameObject.AddComponent<FadeDestroy>();
			GetComponent<AudioSource>().PlayOneShot(eatSound, 0.1f); // Play the eat sound
		}
		}
	}

	void OnGUI()
	{
		GUI.BeginGroup(new Rect((Screen.width - healthBarWidth) - 10, (Screen.height - healthBarHeight ), healthBarWidth, healthBarHeight)); // Create a group to hold both sections
		GUI.Box(new Rect(0, 0, healthBarFront.width, healthBarFront.height), healthBarBack, healthBarStyle); // The back of the pollution bar
		GUI.BeginGroup(new Rect(0, 0, (healthDisplay / maxScreenWidth)*Screen.width, healthBarHeight)); // Create a group to hold the front of the pollution bar.
		GUI.Box (new Rect(0, 0, 256, healthBarFront.height), healthBarFront, healthBarStyle); // The front pollution bar.
		GUI.EndGroup();
		GUI.EndGroup();
	}

	void checkPollution()
	{
		drainTimer -= Time.deltaTime;
		if (Camera.main.GetComponent<Pollution>().pollutionLevel >= Camera.main.GetComponent<Pollution>().maxPollution && drainTimer <= 0)
		{
			currentHealth -= 1;
			drainTimer = drainInterval;
			checkHealth();
		}

		glowPlayer ();
	}

	void checkHealth()
	{
		if (currentHealth <= 0)
		{
			GetComponent<PlayerScript>().lives--; // Take a life from the player when the health bar is depleted
			Camera.main.GetComponent<Pollution>().pollutionLevel = 0.0f;
			currentHealth = maxHealth; // Reset the health bar
		}
	}

	void glowPlayer() // Makes the player glow.
	{
		// Needs re-doing as been converted to 2D messed some things up.
	}
}
