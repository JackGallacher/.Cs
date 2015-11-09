using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))] // Require A rigidbody component
public class FloatDown : MonoBehaviour {

	private float xPos;
	private float zRot;
	private float amplitude;
	private float rotAmplitude;
	private float speed;
	private bool atBottom;
	// Use this for initialization
	void Start () 
	{
		atBottom = false;
		speed = 1;
		amplitude = 1.0f;
		rotAmplitude = 15;
		xPos = transform.position.x;
		zRot = transform.rotation.z;
		GetComponent<SpriteRenderer>().color = new Color(0.0f, 0.73f, 0.81f);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (!atBottom)
		{
			transform.localEulerAngles =  new Vector3(0,0,zRot + rotAmplitude * Mathf.Sin(speed*Time.time));
			transform.position = new Vector3(xPos + amplitude * Mathf.Sin(speed*Time.time), transform.position.y, transform.position.z);
		}
	}

	void OnCollisionEnter2D(Collision2D collider)
	{
		if (collider.gameObject.name == "SeaBed")
		{


		}
	}

	void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.name == "SeaBed")
		{
			if(tag.Contains("Barrel"))
			{
				Transform sludge = null;
				foreach (Transform child in transform)
				{
					if (child.name == "ToxicSludge")
					{
						sludge = child;
						break;
					}
					
				}
				if (sludge != null && !sludge.GetComponent<Sludge>().sludgeGone)
					Camera.main.GetComponent<Pollution>().minPollution += 5;
			}

			GetComponent<Collider2D>().enabled = false;
			if(!tag.Contains("Barrel"))
				Camera.main.GetComponent<Pollution>().minPollution += 5;
			GetComponent<Rigidbody2D>().isKinematic = true;
			atBottom = true;
			print ("Hit the Sea Bed");
			
			GetComponent<ParticleEmitter>().minEmission = 0;
			GetComponent<ParticleEmitter>().maxEmission = 0;
		}

	}
}
