using UnityEngine;
using System.Collections;

public class BuildSeaBed : MonoBehaviour {

	public GameObject prefab; // The sand prefab for the sea bed

	private GameObject seaBed;

	public GameObject[] rocks; // Rock prefabs.
	// Use this for initialization
	void Start () 
	{
		seaBed = new GameObject(); // Create an instance
		float size = 0.0f; // Size of the wave
		for (int x = 0; size < Camera.main.ViewportToWorldPoint(Vector3.one).x; x++)
		{
			int randomRock = Random.Range (0, rocks.Length); // Select a random rock prefab from array
			size = Camera.main.ViewportToWorldPoint(Vector3.zero).x + (x * 0.64f);
			Object objInfo = Instantiate(prefab, new Vector3(Camera.main.ViewportToWorldPoint(Vector3.zero).x + (x * 0.64f), Camera.main.ViewportToWorldPoint(Vector3.zero).y, 1), Quaternion.identity); // Instantiate off screen
			objInfo.name = "SandClone"; // Change the instance name
			GameObject Inst = GameObject.Find (objInfo.name); // Store the game object
			Inst.transform.parent = seaBed.transform; // Set the segment as a child of the wave
			Inst.GetComponent<SpriteRenderer>().color = new Color(0.0f, 0.73f, 0.81f);
			objInfo.name = "SandClone"+Random.value; // Change the instance name

			objInfo = Instantiate(rocks[randomRock], new Vector3(Camera.main.ViewportToWorldPoint(Vector3.zero).x + (x * 0.64f), Camera.main.ViewportToWorldPoint(Vector3.zero).y+0.5f, 0), Quaternion.identity); // Instantiate off screen
			objInfo.name = "RockClone"; // Change the instance name
			Inst = GameObject.Find (objInfo.name); // Store the game object
			Inst.transform.parent = seaBed.transform; // Set the segment as a child of the wave
			Inst.GetComponent<SpriteRenderer>().color = new Color(0.0f, 0.73f, 0.81f);
			objInfo.name = "RockClone"+Random.value; // Change the instance name

		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
