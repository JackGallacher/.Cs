using UnityEngine;
using System.Collections;

public class EndGame : MonoBehaviour {
	
	private Vector3 startPosition;

	public float timeLeftUntilSpawn = 10f; 
	public bool placeOne = true; 

	public GameObject endGame;
	
	Transform myTransform; 

	void Awake()
	{		
		myTransform = transform; 
	}
	void Start () 
	{	
		startPosition = transform.position; 		
	}
	void SpawnEndGameObject()
	{
		GameObject myObj = Instantiate (endGame) as GameObject;
		myObj.transform.position = transform.position;
	}
	void Update () 
	{
		timeLeftUntilSpawn -= Time.deltaTime; 
		
		if (timeLeftUntilSpawn < 1 && timeLeftUntilSpawn >0 && placeOne == true)
		{
			SpawnEndGameObject(); 
			placeOne = false; 
			
		}
		myTransform.Translate(Vector3.right * Time.deltaTime * 10);
	}
}
