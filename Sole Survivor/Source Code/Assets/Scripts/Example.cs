using UnityEngine;
using System.Collections;

public class Example : MonoBehaviour {
	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "Enemy")
			print ("Collision");//coll.gameObject.SendMessage("ApplyDamage", 10);
		
	}
}