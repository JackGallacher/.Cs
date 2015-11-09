using UnityEngine;
using System.Collections;

public class EnemySpawnerScript : MonoBehaviour {

	
	public Sprite pfab_Enemy;
	float spawnDelay = 1.0f;
	float spawnTimer = 0.0f;
	
	// Update is called once per frame
	void Update () {
		
		//Spawn timer
		//Will count spawnTimer up until it >= spawnDelay, then spawns an enemy
		//eg spawnDelay = 1.0f will spawn every 1 second; spawnDelay = 2.0f will spawn every 2 seconds, etc
		if(spawnTimer >= spawnDelay)
		{
			Instantiate(pfab_Enemy);
			spawnTimer -= spawnDelay;
		}
		else
		{
			spawnTimer += Time.deltaTime;
		}
	}
}
