using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Archive : MonoBehaviour {

	public Text text;
	public InputField ipfield;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public void ReceiveInput(){
		string s = ipfield.text.ToLower ();
		text.text = ArchiveOutput (s);
	}


	public string ArchiveOutput(string s){

		switch (s) {
		case "eravola":
			return "[CLASSIFIED]";
		case "blabla":
			return "What are you trying to say?";
		case "tari":
			return "Little is known about her life. She died in 943 as a victim of Eravola in Saudden Village.           \n\n\nRecorded by client c_591.";
		case "caudden":
			return "Old Mage City. Has been infiltrated by Eravola since the beginning. Some rumors indicate that this is the origin of the disease. \n\nA message was received from here. it said:\n";
		default:
			return "No entries found under '" + s + "'.";
		}

	}

}
