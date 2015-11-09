using UnityEngine;
using System.Collections;

public class GUIScript : MonoBehaviour {
	
	ScreenOrientation orientation;
	private string message;
	public bool displayLives = false;
	public GUIStyle style;
	
	void Start()
	{
		Screen.orientation = ScreenOrientation.AutoRotation;
	}
	
	//Creates score + life GUI
	void OnGUI()
	{
		GUI.Box (new Rect(10,10,200, 50), message, style);
		
	}
	
	//Quits game when ESC (or WP8 back key) is pressed
	void Update()
	{
		message = "Eaten: " + GetComponent<Statistics>().eaten;
		if (displayLives)
		{
			message += " Lives: " + GameObject.Find ("Player").GetComponent<PlayerScript>().lives;
		}
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			Application.Quit();
		}
	}
	
}
