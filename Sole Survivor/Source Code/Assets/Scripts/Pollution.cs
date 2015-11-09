using UnityEngine;
using System.Collections;

// TODO
/* 
 * 
 * 
 * 
	There is a bug with the pollution boat which makes it destroy from time to time
 **DONE**
 *	Make Shark always a predator
 * 	Stop the player going though the bottom of screen - Done
 * 	Sort the pollution and health bars to scale when in small view - Done
 * 	Add sand texture to bottom - Done
	Make Fish Spawn below the waves and above the sand - Done
	Stop Player going above the waves - Done
	Make Crabs only go across the bottom - Done
	Add boat which drops Pollution objects - Done
	Add other pollution objects - Done
	Make pollution objects spawn at waves not above them - Done
	Change pollution increase state to coincide with pollution drops - Done
	Make when player collects pollution if less than 10 it resets polluton to 0 - Done
	Make more fish spawn as polluted fish as pollution rises - Done
	Bubbles for pollution drop - Done
	
 * */
// Probably better not to change much here. Maybe create as classes then link to program.cs - Possibly
public class Pollution : MonoBehaviour {

	public float aPollutionLevel;
	public int maxPollution;
	public int minPollution;
	public float pollutionLevel; // The current level of pollution
	private float pollutionDisplay; // The Pollution bar which displays to the screen 
	public Color color0 = Color.red; // needs changing
	public Color color1 = Color.blue; // needs changing

	public Color lerpColor;
	public Texture2D pollBarBack; // The pollution bar at the back.
	public Texture2D pollBar; // The pollution bar.
	public GUIStyle pollBarStyle; // Style of the pollution bar

	public GUIStyle overlay; // The settings for the overlay which dims the screen
	private Texture2D texture;

	public float overlayOpacity; // The opacity of the overlay for pollution

	public int drainInterval = 1; // The rate at which the pollution increases.

	public GameObject pixel;
	public GameObject overlayColor;

	private float maxScreenWidth;
	private float maxScreenHeight;
	private float pollBarWidth; // Pollution bar width based on resolution
	private float pollBarHeight; // Pollution bar height based on resolution 
	// Use this for initialization
	void Start () 
	{
		minPollution = 0;
		// 720p
		maxScreenWidth = 1280;
		maxScreenHeight = 720;
		pollBarWidth = (pollBar.width / maxScreenWidth)*Screen.width;
		pollBarHeight = (pollBar.height / maxScreenHeight)*Screen.height;
		Object objInfo = Instantiate(pixel, new Vector3(0, -2.3f, -1), Quaternion.identity); // Instantiate off screen
		objInfo.name = "PollutionOverlay"; // Change the instance name
		overlayColor = GameObject.Find (objInfo.name); // Store the game object

		float width = overlayColor.GetComponent<SpriteRenderer>().sprite.bounds.size.x; // Store the X bounds of sprite
		float height = overlayColor.GetComponent<SpriteRenderer>().sprite.bounds.size.y; // Store the Y bounds of sprite
		
		float worldScreenHeight = Camera.main.orthographicSize * 2; // Store the world screen height
		float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width; // Store the world screen width
		overlayColor.transform.localScale = new Vector3(worldScreenWidth / width, worldScreenHeight / height, 10); // Scale the sprite to the width of screen and height with offset

		maxPollution = 100; // Set the max pollution
		pollutionLevel = aPollutionLevel;
		texture = new Texture2D(1, 1); // Create a texture for

		for (int i = 0; i < texture.height; i++)
		{
			for (int ii = 0; ii < texture.width; ii++)
			{
				texture.SetPixel(i, ii, Color.white);
			}
		}
		texture.Apply();	

		overlayColor.GetComponent<SpriteRenderer>().color = new Color(0.92f, 0.68f, 0.06f, 0); // Set the initial overlay to transparent
	}

	// Update is called once per frame
	void Update () {

		aPollutionLevel = pollutionLevel;

		pollutionDisplay = (((float)pollBar.width / 100f) * (((float)pollutionLevel / (float)maxPollution) * 100)); // Calculate the pollution level to display
	}

	void OnCollisionEnter2D(Collision2D collider) {

		if (collider.gameObject.tag == "Pollution")
		{

		}
	}
	void OnGUI()
	{

//		float y = (Camera.main.WorldToScreenPoint(new Vector3(0.0f,3.8f,0.0f)).y); // Offset the overlay to match the waves position
//		GUI.color = new Color(1, 0, 0, 0.5f);
//		overlay.normal.background = texture;
//		GUI.Box(new Rect(0, Screen.height - y, Screen.width, Screen.height),"", overlay);
		GUI.color = Color.white;
		//* 1366 * 597 * Screen.width / Screen.height
//		GUI.BeginGroup(new Rect(10, (Screen.height-pollBarHeight-1), pollBarWidth, pollBarHeight-1)); // Create a group to hold both sections
//		GUI.Box(new Rect(0, 0, pollBarWidth, pollBarHeight), pollBarBack, pollBarStyle); // The back of the pollution bar
//		GUI.BeginGroup(new Rect(0, 0, (pollutionDisplay / maxScreenWidth)*Screen.width, pollBarHeight)); // Create a group to hold the front of the pollution bar.
//		GUI.Box (new Rect(0, 0, pollBarWidth, pollBarHeight), pollBar, pollBarStyle); // The front pollution bar.
//			GUI.EndGroup();
//		GUI.EndGroup();

		GUI.BeginGroup(new Rect(10, (Screen.height-pollBarHeight), pollBarWidth, pollBarHeight)); // Create a group to hold both sections
		GUI.Box(new Rect(0, 0, pollBar.width, pollBarHeight), pollBarBack, pollBarStyle); // The back of the pollution bar
		GUI.BeginGroup(new Rect(0, 0, (pollutionDisplay / maxScreenWidth)*Screen.width, pollBarHeight)); // Create a group to hold the front of the pollution bar.
		GUI.Box (new Rect(0, 0, pollBar.width, pollBarHeight), pollBar, pollBarStyle); // The front pollution bar.
		GUI.EndGroup();
		GUI.EndGroup();
	}

	public void increasePollution()
	{
		if (pollutionLevel < maxPollution)
		{
			pollutionLevel += 5;
			overlayOpacity = 0.005f * pollutionLevel;
			overlayColor.GetComponent<SpriteRenderer>().color = new Color(0.92f, 0.68f, 0.06f, overlayOpacity);
		}
	}

}
