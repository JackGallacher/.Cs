using UnityEngine;
using System.Collections;

public class Polluter : MonoBehaviour {

	float dropTargetX; // Target location for dropping the pollution

	public GameObject[] pollution; // The types of pollution
	public int amount; // The amount of pollution to drop
	private int item; // The pollution item to drop

	public AudioClip splash;
	// Use this for initialization
	void Start () 
	{
		for (int i = 0; i < amount; i++)
		{
			dropTargetX = Random.Range (Camera.main.ViewportToWorldPoint(Vector3.zero).x, Camera.main.ViewportToWorldPoint(Vector3.one).x);
			//print (Screen.width);
			GameObject targetArea = new GameObject("PollutionTarget");
			targetArea.AddComponent<BoxCollider2D>().isTrigger = true;
			targetArea.GetComponent<BoxCollider2D>().size = new Vector2(1, 4);
			targetArea.transform.position = new Vector3(dropTargetX ,transform.position.y, transform.position.z);
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
	}

	void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.name == "PollutionTarget")
		{
			item = Random.Range (0, pollution.Length); // Set the item to instantiate
			Instantiate (pollution[item], this.transform.position - new Vector3(0, 1.5f, 0), Quaternion.identity);
			Camera.main.GetComponent<Pollution>().increasePollution(); // Increase the pollution
			Destroy(collider.gameObject);
			GetComponent<AudioSource>().PlayOneShot(splash, 1.0f);

		}
	}
}
