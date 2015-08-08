using UnityEngine;
using System.Collections;

public class waitBeforeActivation : MonoBehaviour {

	public float time;
	bool hasStarted = false;

	// Use this for initialization
	void Start () {


		if (time > 0)
			this.gameObject.SetActive (false);
		else
			hasStarted = true;
	
		Debug.Log (time + " " + hasStarted);
	}
	
	// Update is called once per frame
	void Update () {

		if (hasStarted) {
			return;
		}
		Debug.Log (time + " " + hasStarted);
		
		if (time >= 0) {
			hasStarted = true;
			this.gameObject.SetActive(false);
		} else {
			time -= Time.deltaTime;
		}

	
	}
}
