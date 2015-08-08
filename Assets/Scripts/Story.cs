using UnityEngine;
using System.Collections;

public class Story : MonoBehaviour {
	public static Story instance { get; private set; }

	void Awake()
	{
		if (instance != null && instance != this) {
			Destroy (gameObject);
		}
		instance = this;
		DontDestroyOnLoad (gameObject);
	}

	public string StartDial;
	public GameObject DatabaseButton;
	public bool shouldStartTextGlitching = false;


	public void OnCanvasChange(Canvas c){
		/*if(c==canvasManager.instance.houseCanvas){ //IF HOUSE
			if(!hasTalkedToMomIntro){
				canvasManager.instance.ActivateDialogueButton(c,"momIntro", "Talk to Mom");
				return;
			}
		}*/

	}


	public void OnExitDialogue(string dName, int pos){
		if (dName == "bootIntro" && pos == 2) {
			canvasManager.instance.ActivateMode(canvasManager.instance.Archive);
		}
		if (dName == "bootIntro" && pos == 3) {
			canvasManager.instance.ActivateMode(canvasManager.instance.Log);
		}
		if (dName == "bootIntro" && pos == 4) {
			dialogueManager.instance.EnterDialogue("controlIntro",null);
		}

	}
	public void OnExitDialogue(string dName){
		
	}

	public void OnDialogueTrigger(string dial, int pos){
		ChangeGlitchTimings (dial,pos);
		ActivateSounds (dial, pos);
	}



	public void ActivateSounds(string dial, int pos){

	}



	public void ChangeGlitchTimings(string dial, int pos){

	}




}
