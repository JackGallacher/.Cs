using UnityEngine;
using System.Collections;

public class PopBubbles : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnParticleTrigger2D(GameObject other)
	{
		print ("Bubble pop");
	}
}
