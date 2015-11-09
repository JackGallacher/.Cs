using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour 
{
	public GameObject player;
	// Use this for initialization
	void Start () 
	{
		player = GameObject.Find ("2D Character");
	}
	
	// Update is called once per frame
	void Update () 
	{
		transform.position = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);
	}
}
