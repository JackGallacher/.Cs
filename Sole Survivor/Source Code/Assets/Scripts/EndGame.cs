using UnityEngine;
using System.Collections;

public class EndGame : MonoBehaviour {

	//public Texture2D pixel; // The pixel to use for the box - Allows to change colour
	float width; // Width of the box overlay
	float height; // Height of the box overlay

	// Coordinates for top left position of box
	float x; // x position of box
	float y; // y position of box

	Rect box; // The box

	public GUIStyle style; // Style of the box
	public Color color; // The colour of the box
	private Color savedGUIColor; // The GUI colour before being changed
	public GameObject gameover; // The Game Over screen

	public GUIText pollutionEaten;
	public GUIText eaten;
	public GUIText timeAlive;

	// Use this for initialization
	void Start () 
	{
		pollutionEaten.text += GetComponent<Statistics>().pollutionEaten.ToString();
		eaten.text += GetComponent<Statistics>().eaten.ToString();
		timeAlive.text += Mathf.RoundToInt (GetComponent<Statistics>().timeAlive).ToString ();;
		// Set the width and height
		width = Screen.width / 2;
		height = Screen.height / 2;

		// Position in center
		x = width / 2; 
		y = height / 2;

		box = new Rect (x, y, width, height); // Create the box
		gameover.SetActive (true);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (!gameover.GetComponent<AudioSource>().isPlaying)
			AudioListener.volume = 0;
	}

	void OnGUI()
	{
		//savedGUIColor = GUI.color; // Saved the GUI color before changing
		//GUI.color = color; // Set the GUI Color
		//GUI.Box (box,"",style); // Draw box
		//GUI.color = savedGUIColor; // Set the GUI color back to original
	}
}
