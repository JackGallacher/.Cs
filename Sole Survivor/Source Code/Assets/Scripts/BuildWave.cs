using UnityEngine;
using System.Collections;

public class BuildWave : MonoBehaviour {

	public GameObject prefab; // The wave prefab

	private GameObject wave; // Holds the instantiated wave prefabs
	private GameObject frontWave;
	private GameObject middleWave;
	private GameObject backWave;

	//private float amplitude = 0.1f; // The distance at which to move both sides
	private float animSpeed = 2.0f; // The speed of the float
	private float yPos; // Object y position
	// Use this for initialization
	void Start () 
	{
		wave = new GameObject(); // Create a new Game Object as the parent for the wave layers
		wave.name = "Waves"; // Set the name of the parent

		// Allocate memory for the wave layers
		frontWave = new GameObject();
		middleWave = new GameObject();
		backWave = new GameObject();

		// Set the front, middle and back waves as children to the main wave
		frontWave.transform.parent = wave.transform;
		middleWave.transform.parent = wave.transform;
		backWave.transform.parent = wave.transform;

		yPos = transform.position.y; // Store the object y position

		//print (Camera.main.ViewportToWorldPoint(Vector3.zero).x + " " + Camera.main.ViewportToWorldPoint(Vector3.one).x ); // debug
		// Front Wave
		float size = 0.0f; // Size of the wave
		for (int x = 0; size < Camera.main.ViewportToWorldPoint(Vector3.one).x; x++)
		{
			size = Camera.main.ViewportToWorldPoint(Vector3.zero).x + (x * 0.64f);
			Object objInfo = Instantiate(prefab, new Vector3(Camera.main.ViewportToWorldPoint(Vector3.zero).x + (x * 0.64f), Camera.main.ViewportToWorldPoint(Vector3.one).y -2, 1), Quaternion.identity); // Instantiate off screen
			objInfo.name = "WaveCloneFront"; // Change the instance name
			GameObject Inst = GameObject.Find (objInfo.name); // Store the game object
			Inst.transform.parent = frontWave.transform; // Set the segment as a child of the wave
			Inst.GetComponent<SpriteRenderer>().color = new Color(0.0f, 0.73f, 0.81f);
			objInfo.name = "WaveCloneFront"+Random.value; // Change the instance name
		}

		// Middle Wave
		size = 0.0f; // Reset the size for the middle wave
		for (int x = 0; size < Camera.main.ViewportToWorldPoint(Vector3.one).x; x++)
		{
			size = Camera.main.ViewportToWorldPoint(Vector3.zero).x + (x * 0.64f);
			Object objInfo = Instantiate(prefab, new Vector3(Camera.main.ViewportToWorldPoint(Vector3.zero).x + (x * 0.64f), Camera.main.ViewportToWorldPoint(Vector3.one).y -2.1f, 0), Quaternion.identity); // Instantiate off screen
			objInfo.name = "WaveCloneMiddle"; // Change the instance name
			GameObject Inst = GameObject.Find (objInfo.name); // Store the game object
			Inst.transform.parent = middleWave.transform; // Set the segment as a child of the wave
			Inst.GetComponent<SpriteRenderer>().color = new Color(0.058f, 0.52f, 0.80f);
			objInfo.name = "WaveCloneMiddle"+Random.value; // Change the instance name
		}

		// Back Wave
		size = 0.0f; // Reset the size for the middle wave
		for (int x = 0; size < Camera.main.ViewportToWorldPoint(Vector3.one).x; x++)
		{
			size = Camera.main.ViewportToWorldPoint(Vector3.zero).x + (x * 0.64f);
			Object objInfo = Instantiate(prefab, new Vector3(Camera.main.ViewportToWorldPoint(Vector3.zero).x + (x * 0.64f), Camera.main.ViewportToWorldPoint(Vector3.one).y -2.2f, -2), Quaternion.identity); // Instantiate off screen
			objInfo.name = "WaveCloneBack"; // Change the instance name
			GameObject Inst = GameObject.Find (objInfo.name); // Store the game object
			Inst.transform.parent = backWave.transform; // Set the segment as a child of the wave
			Inst.GetComponent<SpriteRenderer>().color = new Color(0.016f, 0.28f, 0.8f);
			objInfo.name = "WaveCloneBack"+Random.value; // Change the instance name
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		frontWave.transform.position = new Vector3(frontWave.transform.position.x, yPos + 0.03f * Mathf.Sin(animSpeed*Time.time), frontWave.transform.position.z); // Create the float effect
		middleWave.transform.position = new Vector3(middleWave.transform.position.x, yPos - 0.03f * Mathf.Sin(animSpeed*Time.time*0.9f), middleWave.transform.position.z); // Create the float effect
		backWave.transform.position = new Vector3(backWave.transform.position.x, yPos - 0.03f * Mathf.Sin(animSpeed*Time.time*0.8f), backWave.transform.position.z); // Create the float effect
	}
}
