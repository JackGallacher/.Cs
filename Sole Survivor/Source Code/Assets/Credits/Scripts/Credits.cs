using UnityEngine;
using System.Collections;

public class Credits : MonoBehaviour {

	public Texture backgroundTexture;

	public float returnbuttonX;
	public float returnbuttonY;

	void OnGUI(){
		//displays out background texture
		GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), backgroundTexture);

		if (GUI.Button(new Rect(Screen.width * returnbuttonX, Screen.height * returnbuttonY, Screen.width * .25f, Screen.height * .1f),"Return to Main Menu")){
			print("Clicked Return Button");
			Application.LoadLevel("MainMenu");
		}
		}
}
