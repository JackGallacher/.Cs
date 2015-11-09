using UnityEngine;
using System.Collections;

public class LoseMenu : MonoBehaviour 
{
	public Texture backgroundTexture;	
	void OnGUI()
	{
		//display background texture.
		GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), backgroundTexture);
		
		//displays gui buttons.
		if(GUI.Button (new Rect(Screen.width * .35f, Screen.height * .5f, Screen.width *.3f, Screen.height* .1f), "Play Again"))
		{
			print ("clicked play game");
			Application.LoadLevel("Main");// - load the main game.
		}
		if(GUI.Button (new Rect(Screen.width * .35f, Screen.height * .61f, Screen.width *.3f, Screen.height* .1f), "Quit"))
		{
			print ("clicked quit game");
			Application.Quit();//quits the game.
		}
	}

}
