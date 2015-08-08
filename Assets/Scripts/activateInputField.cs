using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class activateInputField : MonoBehaviour {

	InputField ipf;


	// Use this for initialization
	void Start () {

	
	}

	public void ActivateInputField(){
		ipf = GetComponent<InputField> ();
		ipf.ActivateInputField ();
		ipf.Select ();
	}

}
