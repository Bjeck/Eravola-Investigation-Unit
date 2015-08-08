using UnityEngine;
using System.Collections;

public class setactive : MonoBehaviour {

	public GameObject ObjectToActivate;


	public void SetActive(){
		ObjectToActivate.SetActive (!this.ObjectToActivate.activeInHierarchy);
	}

}
