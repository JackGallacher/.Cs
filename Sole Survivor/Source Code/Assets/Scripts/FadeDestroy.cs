using UnityEngine;
using System.Collections;

// Fades and destroys an object
public class FadeDestroy : MonoBehaviour {
	
	public int interval = 1; // The interval at which a fade occurs.
	public float start; // The amount of time in seconds to start

	private float intervalTimer; // Used for decreasing interval
	private float currentFade; // The current level of fade
	// Use this for initialization
	void Start () {
		intervalTimer = interval;
		currentFade = 1.0f;
	}
	
	// Update is called once per frame
	void Update () {

		start -= 1 * Time.deltaTime; // Decrease the start time relative to frames
		if (start <= 0) // Start fading
		{
			intervalTimer -= 0.2f; // Decrease the interval time relative to frames
			if (intervalTimer <= 0)
			{
				currentFade -= 0.1f; // Increase the current fade
				intervalTimer = interval; // Reset the interval
				this.GetComponent<Renderer>().material.color = new Color(1.0f, 1.0f, 1.0f, currentFade); // Apply fade
			}

			if (currentFade <= 0f) // If the time has elapsed object should be destroyed
			{
				Destroy(this.gameObject);
			}
		}

	}
}
