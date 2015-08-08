using UnityEngine;
using System.Collections;

public class dialOptionControl : MonoBehaviour {



	public void ButtonClicked(GameObject button){
		dialogueManager.instance.TriggerNextDialogue (button);
	}



}
