using UnityEngine;
using System.Collections;

public class Powerups : MonoBehaviour {
	
	// Public Section
	private float powerupTimer; // Timer used for powerups
	public int powerupDuration;
	public bool powerupActive = false;
	public bool powerupCol = false;
	public float playerSpeedPowerup;
	public float playerSpeedPowerupDown;
	public float playerSpeedNoPowerup; 
	public bool invincibleOn = false;
	public string powerupType = "";
	
	// Private Section
	
	// Use this for initialization
	void Start () 
	{
		powerupTimer = powerupDuration; // Sets the powerup timer
		playerSpeedNoPowerup = GameObject.Find ("Player").GetComponent<PlayerScript>().playerSpeed;
		playerSpeedPowerup = GameObject.Find ("Player").GetComponent<PlayerScript>().playerSpeed * 2;
		playerSpeedPowerupDown = GameObject.Find ("Player").GetComponent<PlayerScript>().playerSpeed / 2;
	}
	
	// Update is called once per frame
	void Update () 
	{
		//Powerup stuff
		//print("Powerup Timer: " + powerupTimer);
		
		if (powerupCol == true)
		{
			powerupTimerFunc();
		}
		
		if (powerupActive == true)
		{
			if (powerupType == "Speed")
			{
				GameObject.Find ("Player").GetComponent<PlayerScript>().playerSpeed = playerSpeedPowerup;
			}
			
			if (powerupType == "SpeedDown")
			{
				GameObject.Find ("Player").GetComponent<PlayerScript>().playerSpeed = playerSpeedPowerupDown;
			}
			if (powerupType == "Invincible")
			{
				invincibleOn = true;
			}
		}
		else if (powerupActive == false)
		{
			GameObject.Find ("Player").GetComponent<PlayerScript>().playerSpeed = playerSpeedNoPowerup;
			invincibleOn = false;
		}
	}
	
	void powerupTimerFunc()
	{
		powerupActive = true;
		
		powerupTimer -= Time.deltaTime;
		
		if (powerupTimer <= 0)
		{
			powerupTimer = powerupDuration;
			
			powerupActive = false;
			powerupCol = false;
			powerupType = "";
		}
	}
}
