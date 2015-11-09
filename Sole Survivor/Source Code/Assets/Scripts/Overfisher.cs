using UnityEngine;
using System.Collections;

public class Overfisher : MonoBehaviour {

	private bool netEntered = false;
	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	void OnTriggerEnter2D(Collider2D collider)
	{
		//print ("trigger with: " + collider.name);
		if (collider.name == "NetEntry") // Fish entered net
		{
			netEntered = true;
		}
		else if (collider.name == "NetCapture") // Fish entered capture zone of net
		{
			if (netEntered) // If the fish entered the net
			{
				this.tag = "Dead";
				this.GetComponent<Movement>().enabled = false;
				this.GetComponent<BoxCollider2D>().enabled = false;
				this.transform.parent = collider.transform;
			}
		}

	}

	void OnCollisionEnter2D(Collision2D other)
	{
		print ("Collision with: " + other.gameObject.name);
		if (other.collider.name == "NetEntry") // Fish entered net
		{
			netEntered = true;
		}
		else if (other.collider.name == "NetCapture") // Fish entered capture zone of net
		{
			if (netEntered) // If the fish entered the net
			{
				this.tag = "Dead";
				this.GetComponent<Movement>().enabled = false;
				this.GetComponent<BoxCollider2D>().enabled = false;
				this.transform.parent = other.transform;
			}
		}
		
	}
}
