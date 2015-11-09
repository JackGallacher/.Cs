using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	//loads texture
	public Texture backgroundTexture;

	//button Y values
	public float playgameY;
	public float exitY;
	public float creditsY;

	//button X values
	public float playgameX;
	public float exitX;
	public float creditsX;

	// Button Rectangles
	private Rect playButton;
	private Rect exitButton;
	private Rect creditsButton;

	// If the sound has played
	private bool soundPlayed; 

	// The hover sound
	public AudioClip hoverSound;

	// Use this for initialization
	void Start () 
	{
		playButton = new Rect(Screen.width * playgameX, Screen.height * playgameY, Screen.width * .25f, Screen.height * .1f);
		exitButton = new Rect(Screen.width * exitX, Screen.height * exitY, Screen.width * .25f, Screen.height * .1f);
		creditsButton = new Rect(Screen.width * creditsX, Screen.height * creditsY, Screen.width * .25f, Screen.height * .1f);
	}

	void Update()
	{
		if (HoverCheck() && !soundPlayed)
		{
			soundPlayed = true; // Set the sound has been played for this hover
			GetComponent<AudioSource>().PlayOneShot(hoverSound, 1.0f);
		}

	}
	void OnGUI()
	{

		//displays our background texture
		GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), backgroundTexture);

		//displays our buttons
		if (GUI.Button(playButton,"Play Game")){
			print("Clicked Play Game");
			Application.LoadLevel("scene1");
		}
		if (GUI.Button(exitButton,"Exit Game")){
			print("Clicked Exit Game");
			Application.Quit ();
		}
		if (GUI.Button(creditsButton,"Credits")){
			print("Clicked Credits");
			Application.LoadLevel("Credits");
		}

	}

	bool HoverCheck() // Returns if a button is being hovered
	{
		if (playButton.Contains(Input.mousePosition) || exitButton.Contains(Input.mousePosition) || creditsButton.Contains(Input.mousePosition))
		{
			return true;
		}

		soundPlayed = false; // Make sure that when we are no longer hovering, that we reset soundPlayed
		return false;
	}
}
