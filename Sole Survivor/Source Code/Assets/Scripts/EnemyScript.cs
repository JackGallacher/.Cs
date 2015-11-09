using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {

	Transform myTransform;
	float minSpeed = 15.0f;
	float maxSpeed = 25.0f;
	float speed;
	string direction;
	int randomNumber;
	int x, y, z;
	
	void Start()
	{
		//Sets random direction at spawn
		randomNumber = Random.Range(0,3);
		
		if(randomNumber <=3 && randomNumber >=2)
		{
			x = 20;
			direction = "left";
		}
		
		else if(randomNumber <= 1 && randomNumber >=0)
		{
			x = -20;
			direction = "right";
		}
		
		//Sets random y coord and random speed based on minSpeed and maxSpeed
		z = 0;
		y = Random.Range (-8, 10); //PC = -10, 10; Lumia 920 = -8, 10;
		myTransform = transform;
		myTransform.position = new Vector3(x, y, z);
		speed = Random.Range (minSpeed, maxSpeed);
		
		
	
	}
	
	void Update()
	{
		y = Random.Range (-8, 10); //PC = -10, 10; Lumia 920 = -8, 10;
		
		float left = Camera.main.ViewportToWorldPoint(Vector3.zero).x;
		float right = Camera.main.ViewportToWorldPoint(Vector3.one).x;
		float top = Camera.main.ViewportToWorldPoint(Vector3.zero).y;
		float bottom = Camera.main.ViewportToWorldPoint(Vector3.one).y;
		
		//Destroys object when it moves too far to the left
		if(direction == "left")
		{
			myTransform.Translate (Vector3.left * speed * Time.deltaTime);
			
			if(myTransform.position.x <= left + GetComponent<Renderer>().bounds.extents.x)
			{
				Destroy (this.gameObject);
			}
			
		}
		
		
		
		//Destroys the object when it moves too farto the right
		else if(direction == "right")
		{
			myTransform.Translate (Vector3.right * speed * Time.deltaTime);
			
		
			if(myTransform.position.x >= right - GetComponent<Renderer>().bounds.extents.y)
			{
				Destroy (this.gameObject);
			}
		}
	}
		
	
	
	//Collisions
	void OnTriggerEnter(Collider collider)
	{
		//Dies when hits projectile AND gives +10 score
		if(collider.gameObject.CompareTag("projectile"))
		{
			GameObject.Find ("Player").GetComponent<PlayerScript>().score += 10;
			Destroy(this.gameObject);
		}
		
		//Dies when hits player DOESN'T give score
		if(collider.gameObject.CompareTag("player"))
		{
			Destroy(this.gameObject);
		}
	}

}
