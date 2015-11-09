using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {
	public float interval; // The interval of the spawn
	//public int amount; // The amount of times to spawn per interval
	public GameObject[] fishes; // The fishes to use for spawning
	public GameObject[] sharks; // The sharks
	public GameObject[] pollutedFish; // The polluted fish for spawning
	public GameObject[] powerups; // The powerups for spawning
	public GameObject eatPin; // The eat pin to display above prey
	public float maxFishScale = 10.0f; // The maximum fish scale
	public int percentPrey = 0; // The percent at which prey fish spawn
	public int percentPredator = 0; // The percent at which predator fish spawn

	public int percentPowerupSpeed = 0; //The percent at which speed powerups spawn
	public int percentPowerupSpeedDown = 0; //The percent at which speed down powerups spawn
	public int percentPowerupInvincible = 0;

	public GameObject[] boats; // The boats to use for spawning.
	public bool boatOnScreen = false; // Is a boat on screen.

	private float timer; // Holds the value of the timer.
	public float boatInterval; // The Interval to spawn boats after the last one
	private float boatTimer; // Holds the value of the boat timer.
	private float sharkTimer; // Holds the value of the shark timer
	public bool sharkOnScreen = false; // Is there a shark on screen
	public float sharkInterval; // The interval at which to spawn sharks
	public float sharkSpeed;
	public float powerupInterval; // The Interval to spawn boats after the last one
	private float powerupTimer; // Holds the value of the boat timer.
	public bool powerupOnScreen = false; // Is there a powerup on screen

	public GameObject cloud; // Clouds for the sky
	public float cloudInterval;
	private float cloudTimer;
	// Use this for initialization
	void Start () 
	{
		InitPowerups(); // Initialize powerup variables
		// Don't allow percentage to go over 100
		if (percentPrey > 100)
		{
			percentPrey = 100;
		}
		if (percentPredator > 100)
		{
			percentPredator = 100;
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		MarineLifeSpawner();
		BoatSpawner ();
		SharkSpawner ();
		UpdatePowerups ();
		CloudSpawner ();
	}

	void CloudSpawner() // Handles spawning of clouds
	{
			cloudTimer += Time.deltaTime;
			if (cloudTimer >= cloudInterval)
			{
				float scale = Random.Range (0.05f, 0.1f); // Set the shark size one larger than the player
				float randomSide = Random.Range (0, 2); // Select a side to spawn
				float bottom = Camera.main.ViewportToWorldPoint(Vector3.one).y - 2.0f; // bottom screen
				float top = Camera.main.ViewportToWorldPoint(Vector3.one).y; // top screen
				
				float randomRangeY = Random.Range (top - cloud.GetComponent<Renderer>().bounds.extents.y, bottom + cloud.GetComponent<Renderer>().bounds.extents.y); // Select the height to spawn
				
				if (randomSide == 0) // Left Side
				{
					float left = Camera.main.ViewportToWorldPoint(Vector3.zero).x - 1; // Left off screen
					float xPos =  left - cloud.GetComponent<Renderer>().bounds.extents.x; // The xPos to spawn
					Object objInfo = Instantiate(cloud, new Vector3(xPos, randomRangeY, 0), Quaternion.LookRotation (new Vector3(0, 0, -1))); // Instantiate off screen
					objInfo.name = "CloudClone"; // Change the instance name
					GameObject Inst = GameObject.Find (objInfo.name); // Store the game object
					Inst.GetComponent<Movement>().speed = 0.3f;
					Inst.transform.localScale = new Vector3(scale, scale, scale);
					objInfo.name = "CloudClone"+Random.value; // Change the instance name
				}
				else // Right Side
				{
					float right = Camera.main.ViewportToWorldPoint(Vector3.one).x + 1; // Right off screen
					float xPos = right + cloud.GetComponent<Renderer>().bounds.extents.x; // The xPos to spawn
					Object objInfo = Instantiate(cloud, new Vector3(xPos, randomRangeY, 0), Quaternion.LookRotation (new Vector3(0, 0, 1))); // Instantiate off screen
					objInfo.name = "CloudClone"; // Change the instance name
					GameObject Inst = GameObject.Find (objInfo.name); // Store the game object
				Inst.GetComponent<Movement>().speed = 0.3f;
					Inst.transform.localScale = new Vector3(scale, scale, scale);
					objInfo.name = "CloudClone"+Random.value; // Change the instance name
				}	
				cloudTimer = 0;
			}
	}
	void SharkSpawner() // Handles spawning of sharks
	{
		if (!sharkOnScreen)
		{
			sharkTimer += Time.deltaTime;
			if (sharkTimer >= sharkInterval)
			{
				int randomShark = Random.Range (0, sharks.Length); // Select a random shark from array
				float scale = this.transform.localScale.y + 0.1f; // Set the shark size one larger than the player
				float randomSide = Random.Range (0, 2); // Select a side to spawn
				float bottom = Camera.main.ViewportToWorldPoint(Vector3.zero).y; // bottom screen
				float top = Camera.main.ViewportToWorldPoint(Vector3.one).y; // top screen
				
				float randomRangeY = Random.Range (top - sharks[randomShark].GetComponent<Renderer>().bounds.extents.y, bottom + sharks[randomShark].GetComponent<Renderer>().bounds.extents.y); // Select the height to spawn
				
				if (randomSide == 0) // Left Side
				{
					float left = Camera.main.ViewportToWorldPoint(Vector3.zero).x - 1; // Left off screen
					float xPos =  left - sharks[randomShark].GetComponent<Renderer>().bounds.extents.x; // The xPos to spawn
					Object objInfo = Instantiate(sharks[randomShark], new Vector3(xPos, randomRangeY, 0), Quaternion.LookRotation (new Vector3(0, 0, -1))); // Instantiate off screen
					objInfo.name = "SharkClone"; // Change the instance name
					GameObject Inst = GameObject.Find (objInfo.name); // Store the game object
					Inst.GetComponent<SpriteRenderer>().color = new Color(0.0f, 0.73f, 0.81f);
					Inst.GetComponent<Movement>().speed = sharkSpeed;
					Inst.transform.localScale = new Vector3(scale, scale, scale);
					objInfo.name = "SharkClone"+Random.value; // Change the instance name
				}
				else // Right Side
				{
					float right = Camera.main.ViewportToWorldPoint(Vector3.one).x + 1; // Right off screen
					float xPos = right + sharks[randomShark].GetComponent<Renderer>().bounds.extents.x; // The xPos to spawn
					Object objInfo = Instantiate(sharks[randomShark], new Vector3(xPos, randomRangeY, 0), Quaternion.LookRotation (new Vector3(0, 0, 1))); // Instantiate off screen
					objInfo.name = "SharkClone"; // Change the instance name
					GameObject Inst = GameObject.Find (objInfo.name); // Store the game object
					Inst.GetComponent<SpriteRenderer>().color = new Color(0.0f, 0.73f, 0.81f);
					Inst.GetComponent<Movement>().speed = sharkSpeed;
					Inst.transform.localScale = new Vector3(scale, scale, scale);
					objInfo.name = "SharkClone"+Random.value; // Change the instance name
				}	
				sharkTimer = 0;
				sharkOnScreen = true;
			}
		}

	}

	// Chance of spawning a polluted fish is pollutionLevel

	void MarineLifeSpawner() //  Handles the spawning of marine life
	{
		timer += Time.deltaTime;
		if (timer >= interval)
		{
			GameObject fish; // Holds the fish to instantiate
			int randomFish; // Holds the random index of a fish to use in instantiating
			if (Random.Range (0, Camera.main.GetComponent<Pollution>().maxPollution + 1) <= Camera.main.GetComponent<Pollution>().pollutionLevel) // Polluted fish chance, based on pollution level
			{
				randomFish = Random.Range (0, pollutedFish.Length); // Select a random fish from array
				fish = pollutedFish[randomFish];
			}
			else // Normal fish
			{
				randomFish = Random.Range (0, fishes.Length); // Select a random fish from array
				fish = fishes[randomFish];
			}
			int randomPercent = Random.Range(1, 100 + 1); // The percent for spawning
			if (randomPercent <= percentPrey) // Creates a prey
			{
				int randomSpeed = Random.Range (1, fish.GetComponent<Movement>().maxSpeed + 1);
				
				float randomScale = Random.Range (1.0f, this.transform.localScale.y); // Select a random size of the fish
				float randomSide = Random.Range (0, 2); // Select a side to spawn
				
				float bottom; // Bottom spawn
				float top; // Top Spawn
				if (fish.tag.Contains("Crab")) // If the selected marine life is a crab
				{
					bottom = Camera.main.ViewportToWorldPoint(Vector3.zero).y + 1.5f; // bottom screen
					top = Camera.main.ViewportToWorldPoint(Vector3.zero).y+2.0f; // top screen
				}
				else
				{
					bottom = Camera.main.ViewportToWorldPoint(Vector3.zero).y + 1.0f; // bottom screen
					top = Camera.main.ViewportToWorldPoint(Vector3.one).y - 2.0f; // top screen
				}
				
				
				float randomRangeY = Random.Range (top - fish.GetComponent<Renderer>().bounds.extents.y, bottom + fish.GetComponent<Renderer>().bounds.extents.y); // Select the height to spawn
				if (randomSide == 0) // Left Side
				{
					float left = Camera.main.ViewportToWorldPoint(Vector3.zero).x - 1; // Left off screen
					float xPos =  left - fish.GetComponent<Renderer>().bounds.extents.x; // The xPos to spawn
					Object objInfo = Instantiate(fish, new Vector3(xPos, randomRangeY, 0), Quaternion.LookRotation (new Vector3(0, 0, -1))); // Instantiate off screen
					objInfo.name = "Clone"; // Change the instance name
					GameObject Inst = GameObject.Find (objInfo.name); // Store the game object
					Inst.GetComponent<SpriteRenderer>().color = new Color(0.0f, 0.73f, 0.81f);
					Inst.GetComponent<Movement>().speed = randomSpeed;
					Inst.transform.localScale = new Vector3(randomScale, randomScale, randomScale);
					objInfo.name = "Clone"+Random.value; // Change the instance name

				}
				else // Right Side
				{
					float right = Camera.main.ViewportToWorldPoint(Vector3.one).x + 1; // Right off screen
					float xPos = right + fish.GetComponent<Renderer>().bounds.extents.x; // The xPos to spawn
					Object objInfo = Instantiate(fish, new Vector3(xPos, randomRangeY, 0), Quaternion.LookRotation (new Vector3(0, 0, 1))); // Instantiate off screen
					objInfo.name = "Clone"; // Change the instance name
					GameObject Inst = GameObject.Find (objInfo.name); // Store the game object
					Inst.GetComponent<SpriteRenderer>().color = new Color(0.0f, 0.73f, 0.81f);
					Inst.GetComponent<Movement>().speed = randomSpeed;
					Inst.transform.localScale = new Vector3(randomScale, randomScale, randomScale);
					objInfo.name = "Clone"+Random.value; // Change the instance name
				}
			}
			if (randomPercent <= percentPredator) // Creates predator
			{
				randomFish = Random.Range (0, fishes.Length); // Select a random fish from array
				int randomSpeed = Random.Range (1, fishes[randomFish].GetComponent<Movement>().maxSpeed + 1);
				//fishes[randomFish].GetComponent<Movement>().speed = randomSpeed;
				float randomScale = Random.Range (this.transform.localScale.y + 1.0f, maxFishScale + 1.0f); // Select a random size of the fish
				//fishes[randomFish].transform.localScale = new Vector3(randomScale, randomScale, randomScale);
				float randomSide = Random.Range (0, 2); // Select a side to spawn
				float bottom = Camera.main.ViewportToWorldPoint(Vector3.zero).y; // bottom screen
				float top = Camera.main.ViewportToWorldPoint(Vector3.one).y; // top screen
				
				float randomRangeY = Random.Range (top - fishes[randomFish].GetComponent<Renderer>().bounds.extents.y, bottom + fishes[randomFish].GetComponent<Renderer>().bounds.extents.y); // Select the height to spawn
				
				if (randomSide == 0) // Left Side
				{
					float left = Camera.main.ViewportToWorldPoint(Vector3.zero).x - 1; // Left off screen
					float xPos =  left - fishes[randomFish].GetComponent<Renderer>().bounds.extents.x; // The xPos to spawn
					Object objInfo = Instantiate(fishes[randomFish], new Vector3(xPos, randomRangeY, 0), Quaternion.LookRotation (new Vector3(0, 0, -1))); // Instantiate off screen
					objInfo.name = "Clone"; // Change the instance name
					GameObject Inst = GameObject.Find (objInfo.name); // Store the game object
					Inst.GetComponent<SpriteRenderer>().color = new Color(0.0f, 0.73f, 0.81f);
					Inst.GetComponent<Movement>().speed = randomSpeed;
					Inst.transform.localScale = new Vector3(randomScale, randomScale, randomScale);
					objInfo.name = "Clone"+Random.value; // Change the instance name
				}
				else // Right Side
				{
					float right = Camera.main.ViewportToWorldPoint(Vector3.one).x + 1; // Right off screen
					float xPos = right + fishes[randomFish].GetComponent<Renderer>().bounds.extents.x; // The xPos to spawn
					Object objInfo = Instantiate(fishes[randomFish], new Vector3(xPos, randomRangeY, 0), Quaternion.LookRotation (new Vector3(0, 0, 1))); // Instantiate off screen
					objInfo.name = "Clone"; // Change the instance name
					GameObject Inst = GameObject.Find (objInfo.name); // Store the game object
					Inst.GetComponent<SpriteRenderer>().color = new Color(0.0f, 0.73f, 0.81f);
					Inst.GetComponent<Movement>().speed = randomSpeed;
					Inst.transform.localScale = new Vector3(randomScale, randomScale, randomScale);
					objInfo.name = "Clone"+Random.value; // Change the instance name
				}	
				
				
			}
			timer = 0;
		}
	}
	void BoatSpawner()
	{
		if (!boatOnScreen) // If there is no boat on screen spawn one
		{
			boatTimer += Time.deltaTime;
			if (boatTimer >= boatInterval)
			{
				int randomBoat = Random.Range (0, boats.Length); // Select a random boat to spawn
				int randomSide = Random.Range (0, 1 + 1); // Select a side to spawn
				if (boats[randomBoat].tag.Contains ("Overfisher")) //TEMP FIX -  TODO: Fix bug where when overfisher spawns on the left side it breaks
				{
					randomSide = 1;
				}
				float spawnX = 0.0f; // The x coordinates to spawn 
				float spawnY = 3.76f; // The y coordinate to spawn
				switch (randomSide)
				{
				case 0: // Left Side
					spawnX = (Camera.main.ViewportToWorldPoint(Vector3.zero).x - 1) - boats[randomBoat].GetComponent<Renderer>().bounds.extents.x;
					break;
				case 1: // Right Side
					spawnX = (Camera.main.ViewportToWorldPoint(Vector3.one).x + 1) + boats[randomBoat].GetComponent<Renderer>().bounds.extents.x;
					break;
				default:
					print ("Something went wrong with selecting a side to spawn boat");
					break;
				}
				Instantiate(boats[randomBoat], new Vector3(spawnX, spawnY, 0), Quaternion.LookRotation (new Vector3(0, 0, (spawnX > 0) ? 1 : -1))); // Instantiate off screen
				boatOnScreen = true; // Set that there is a boat on screen
				boatTimer = 0;
			}
			
		}
	}

	void InitPowerups() // Initialize powerup variables
	{
		if (percentPowerupSpeed > 100)
		{
			percentPowerupSpeed = 100;
		}
		if (percentPowerupSpeedDown > 100)
		{
			percentPowerupSpeedDown = 100;
		}
		if (percentPowerupInvincible > 100)
		{
			percentPowerupInvincible = 100;
		}
	}

	void UpdatePowerups() // Update the state of the powerups
	{
		if (!powerupOnScreen)
		{
			powerupTimer += Time.deltaTime;
			if (powerupTimer >= powerupInterval)
			{

				int randomPowerup = Random.Range (0, powerups.Length); // Select a random powerup from array
				float randomSide = Random.Range (0, 2); // Select a side to spawn
				float bottom = Camera.main.ViewportToWorldPoint(Vector3.zero).y + 1.0f; // bottom screen
				float top = Camera.main.ViewportToWorldPoint(Vector3.one).y - 2.0f; // top screen
				int randomSpeed = Random.Range (1, powerups[randomPowerup].GetComponent<Movement>().maxSpeed + 1);
				float randomRangeY = Random.Range (top - powerups[randomPowerup].GetComponent<Renderer>().bounds.extents.y, bottom + powerups[randomPowerup].GetComponent<Renderer>().bounds.extents.y); // Select the height to spawn
				
				if (randomSide == 0) // Left Side
				{
					float left = Camera.main.ViewportToWorldPoint(Vector3.zero).x - 1; // Left off screen
					float xPos =  left - powerups[randomPowerup].GetComponent<Renderer>().bounds.extents.x; // The xPos to spawn
					Object objInfo = Instantiate(powerups[randomPowerup], new Vector3(xPos, randomRangeY, 0), Quaternion.LookRotation (new Vector3(0, 0, -1))); // Instantiate off screen
					objInfo.name = "PowerupClone"; // Change the instance name
					GameObject Inst = GameObject.Find (objInfo.name); // Store the game object
					Inst.GetComponent<SpriteRenderer>().color = new Color(0.0f, 0.73f, 0.81f);
					Inst.GetComponent<Movement>().speed = randomSpeed;
					objInfo.name = "PowerupClone"+Random.value; // Change the instance name
				}
				else // Right Side
				{
					float right = Camera.main.ViewportToWorldPoint(Vector3.one).x + 1; // Right off screen
					float xPos = right + powerups[randomPowerup].GetComponent<Renderer>().bounds.extents.x; // The xPos to spawn
					Object objInfo = Instantiate(powerups[randomPowerup], new Vector3(xPos, randomRangeY, 0), Quaternion.LookRotation (new Vector3(0, 0, 1))); // Instantiate off screen
					objInfo.name = "PowerupClone"; // Change the instance name
					GameObject Inst = GameObject.Find (objInfo.name); // Store the game object
					Inst.GetComponent<SpriteRenderer>().color = new Color(0.0f, 0.73f, 0.81f);
					Inst.GetComponent<Movement>().speed = randomSpeed;
					objInfo.name = "PowerupClone"+Random.value; // Change the instance name
				}	
				powerupTimer = 0;
				powerupOnScreen = true;
			}
		}
	}

}
