using UnityEngine;
using System.Collections;
	
public class Spawn : MonoBehaviour 
{
	private Vector3 startPosition;

	public float timeLeftUntilSpawn = 0f; 
	public float startTime = 0f; 
	public float secondsBetweenSpawn; 
	
	public GameObject[] gameObjectSet; 
		
	Transform myTransform; 

	void Awake()
	{	
		myTransform = transform; 
	}
	void Start () 
	{		
		startPosition = transform.position; 		
	}
	void SpawnRandomObject()
	{
		int whichItem = Random.Range (0, 4); 
		
		GameObject myObj = Instantiate (gameObjectSet[whichItem]) as GameObject;
		myObj.transform.position = transform.position;
	}
	void Update () 
	{		
		float randomSpawnTime = Random.Range (1f, 100f); 
		timeLeftUntilSpawn = Time.time - startTime; 
		
		if (timeLeftUntilSpawn >= secondsBetweenSpawn)
		{
			startTime = Time.time; 
			timeLeftUntilSpawn = 0; 
			SpawnRandomObject(); 			 
		}
		myTransform.Translate(Vector3.right * Time.deltaTime * 12);
		secondsBetweenSpawn = randomSpawnTime;
	}
}
