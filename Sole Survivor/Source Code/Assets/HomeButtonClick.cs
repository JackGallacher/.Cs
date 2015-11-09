using UnityEngine;
using System.Collections;

public class HomeButtonClick : MonoBehaviour {
	// Use this for initialization
	void Start () 
	{
	
	}

	void Update()
	{

	}
	void OnMouseDown () 
	{
		Camera.main.GetComponent<Statistics> ().DisableAndSwitch ();
	}
}
