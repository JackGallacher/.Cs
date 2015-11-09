using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]

public class Tilling : MonoBehaviour {
	
	public int offsetX = 2;
	
	public bool hasARightBuddy = false;
	public bool hasALeftBuddy = false; 	
	public bool reverseScale = false; 
	
	private float spriteWidth = 0f; 
	private Camera cam; 
	private Transform myTransform; 

	void Awake()
	{
		cam = Camera.main; 
		myTransform = transform; 		
	}
	void Start () 
	{		
		SpriteRenderer sRenderer = GetComponent<SpriteRenderer>();
		spriteWidth = sRenderer.sprite.bounds.size.x; 		
	}

	void Update () 
	{	
		if (hasALeftBuddy == false || hasARightBuddy == false) 
		{
			float camHorizontalExtent = cam.orthographicSize * Screen.width / Screen.height; 
			float edgeVisiblePositionRight = (myTransform.position.x + spriteWidth/2) - camHorizontalExtent; //2 
			float edgeVisiblePositionLeft = (myTransform.position.x + spriteWidth/2) + camHorizontalExtent; //2
			
			if (cam.transform.position.x >= edgeVisiblePositionRight - offsetX && hasARightBuddy == false)
			{
				MakeNewBuddy(2.5f); 
				hasARightBuddy = true; 
			}
			else if (cam.transform.position.x <= edgeVisiblePositionLeft + offsetX && hasALeftBuddy == false)
			{
				MakeNewBuddy (2.5f);
				hasALeftBuddy = true; 
			}
		}
		
	}
	void MakeNewBuddy(float rightOrLeft)
	{
		Vector3 newPosition = new Vector3 (myTransform.position.x + spriteWidth * rightOrLeft, myTransform.position.y, myTransform.position.z);
		Transform newBuddy = Instantiate (myTransform, newPosition,myTransform.rotation) as Transform; 

		newBuddy.parent = myTransform.parent; 
		if (rightOrLeft > 0) 
		{
			newBuddy.GetComponent<Tilling> ().hasALeftBuddy = true; 
		} 
		else 
		{
			newBuddy.GetComponent<Tilling>().hasARightBuddy = true; 
		}
	}
}