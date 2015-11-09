using UnityEngine;
using System.Collections;

public class WinMenu : MonoBehaviour 
{
	public Texture backgroundTexture;	
	void OnGUI()
	{
		//display background texture.
		GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), backgroundTexture);
		
		//displays gui buttons.
		if(GUI.Button (new Rect(Screen.width * .35f, Screen.height * .5f, Screen.width *.3f, Screen.height* .1f), "Play Again"))
		{
			Application.LoadLevel("Main");
		}
		if(GUI.Button (new Rect(Screen.width * .35f, Screen.height * .61f, Screen.width *.3f, Screen.height* .1f), "Quit"))
		{
			Application.Quit();//quits the game.
		}
	}
}
