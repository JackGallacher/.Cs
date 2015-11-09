using UnityEngine;
using System.Collections;

public class Statistics : MonoBehaviour {

	public int pollutionEaten = 0;
	public int eaten = 0;
	public float timeAlive = 0;
	// Use this for initialization
	void Start () 
	{
	}
	
	// Update is called once per frame
	void Update () 
	{
		timeAlive += Time.deltaTime;
	}

	public void DisableAndSwitch()
	{
		GetComponent<EndGame> ().gameover.SetActive (false);
		GetComponent<EndGame> ().enabled = false;
		AudioListener.volume = 100;
		Application.LoadLevel ("MainMenu");

	}

	public void DisableAndRestart()
	{
		GetComponent<EndGame> ().gameover.SetActive (false);
		GetComponent<EndGame> ().enabled = false;
		AudioListener.volume = 100;
		Application.LoadLevel ("scene1");
	}
}
