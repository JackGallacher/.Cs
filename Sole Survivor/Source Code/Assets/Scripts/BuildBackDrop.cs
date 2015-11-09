using UnityEngine;
using System.Collections;

public class BuildBackDrop : MonoBehaviour {

	public GameObject underwater; // Background of underwater
	public GameObject sky; // Background sky
	
	// Use this for initialization
	void Start () 
	{
		Object objInfo = Instantiate(underwater, new Vector3(Camera.main.ViewportToWorldPoint(Vector3.zero).x, Camera.main.ViewportToWorldPoint(Vector3.one).y -2.3f, 1), Quaternion.identity); // Instantiate off screen
		objInfo.name = "Underwater"; // Change the instance name
		GameObject Inst = GameObject.Find (objInfo.name); // Store the game object
		Inst.transform.localScale += new Vector3(Camera.main.pixelWidth, 0, 0);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
