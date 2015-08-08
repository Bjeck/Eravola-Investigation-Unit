using UnityEngine;
using System.Collections;

public class MC : MonoBehaviour {
	public static MC instance { get; private set; }


	void Awake()
	{
		if (instance != null && instance != this) {
			Destroy (gameObject);
		}
		instance = this;
		DontDestroyOnLoad (gameObject);
	}


}