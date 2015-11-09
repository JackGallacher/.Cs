using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

	public Vector3 direction; // Holds the direction of movement
	public float speed; // Speed of the movement
	public int maxSpeed; // The maximum speed
	private Camera cam; // The main camera
	private Plane[] planes; // The planes which making up the camera's viewing frustum

	private float amplitude = 0.1f; // The distance at which to move both sides
	private float animSpeed = 2.0f; // The speed of the float
	private bool enteredBounds = false; // Has an object entered bounds
	private bool childEnteredBounds = false; // Has the child of the object entered the bounds
	private GameObject player; // The player object

	private float yPos; // Object y position
	private Transform net; // The net for trawler boat

	// Use this for initialization
	void Start () {
		cam = Camera.main; // Store the main camera
		planes = GeometryUtility.CalculateFrustumPlanes(cam); // Store the planes which make up the viewing frustum

		direction = Vector3.left;

		yPos = transform.position.y; // Store the object y position

		player = GameObject.FindGameObjectWithTag("Player"); // Find the player

		foreach (Transform child in this.transform)
		{
			if (child.name=="Net")
			{
				net = child;
			}
		}

	}
	
	// Update is called once per frame
	void Update () {
		if (GameObject.Find("Player") != null)
		{
		checkBounds (); // Check the bounds of the object

		if (this.tag.Contains("Crab") || this.tag == "Cloud") // If a crab we do not want to animate swimming
		{
			transform.Translate(speed * (direction * Time.deltaTime)); // Move the object
		}
		else if(this.tag.Contains ("Shark"))
		{
			//this.transform.rotation = new Quaternion(this.transform.rotation.x, this.transform.rotation.y-90, this.transform.rotation.z, 1);
			//transform.LookAt(player.transform);
			if (GameObject.FindWithTag("Player") && player.GetComponent<PlayerScript>().hidden == false)
			{
				if (transform.position.x <= player.transform.position.x)
				{
					direction = new Vector3(-1, 0, 0);
					transform.eulerAngles = new Vector3(0, 180, 0);
				}
				else
					transform.eulerAngles = new Vector3(0, 0, 0);

				if (transform.position.y <= player.transform.position.y)
					transform.Translate(speed * ((direction+new Vector3(0, 1,0)) * Time.deltaTime)); // Move the object
				else
					transform.Translate(speed * ((direction+new Vector3(0, -1,0)) * Time.deltaTime)); // Move the object
			}
			else
			{
				transform.Translate(speed * ((direction * Time.deltaTime)));
			}
		}
		else
		{
			transform.Translate(speed * (direction * Time.deltaTime)); // Move the object
			transform.position = new Vector3(transform.position.x, yPos + amplitude * Mathf.Sin(animSpeed*Time.time), transform.position.z); // Create the float effect
		}
		}
		else
		{
			if (GetComponent<Rigidbody2D>())
				GetComponent<Rigidbody2D>().isKinematic = true;
		}
	}

	void checkBounds() // Checks if the objects have left the bounds
	{
		if(net != null && !GeometryUtility.TestPlanesAABB (planes, net.GetComponent<Renderer>().bounds) && enteredBounds && childEnteredBounds)
		{
			Destroy(this.gameObject);
			GameObject.Find("Player").GetComponent<Spawner>().boatOnScreen = false;
		}
		else if (!GeometryUtility.TestPlanesAABB (planes, this.gameObject.GetComponent<Renderer>().bounds) && enteredBounds)
		{
			//print (this.gameObject.name + " left the bounds");
			if (this.gameObject.name == "Net")
			{
				print ("Net left the bounds");
			}
			else if (this.gameObject.tag.Contains("Boat") && !this.gameObject.tag.Contains("Overfisher"))
			{
				Destroy(this.gameObject);
				GameObject.Find("Player").GetComponent<Spawner>().boatOnScreen = false;
			}
			if(this.gameObject.tag.Contains("Shark"))
			{
				Destroy(this.gameObject);
				GameObject.Find("Player").GetComponent<Spawner>().sharkOnScreen = false;
			}
			if (this.gameObject.tag.Contains("Powerup"))
			{
				Destroy(this.gameObject);
				GameObject.Find("Player").GetComponent<Spawner>().powerupOnScreen = false;
			}
			if(this.gameObject.tag.Contains("MarineLife"))
			{
				Destroy(this.gameObject);
			}

		}
		else if (net != null && GeometryUtility.TestPlanesAABB (planes, net.GetComponent<Renderer>().bounds))
		{
			childEnteredBounds = true; // Object has entered viewd
		}
		else if (GeometryUtility.TestPlanesAABB (planes, this.gameObject.GetComponent<Renderer>().bounds))
		{
			enteredBounds = true; // Object has entered viewd
		}
		//print (this.gameObject.renderer.isVisible);
//		if (!this.gameObject.renderer.isVisible)
//		{
//			Destroy(this.gameObject);
//		}
	}
}
