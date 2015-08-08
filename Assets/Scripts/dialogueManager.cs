using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;


public class dialogueManager : MonoBehaviour {

	public static dialogueManager instance { get; private set; }

	public int dialoguePosition = 0;
	//public Dictionary<string,string> dialNames = new  Dictionary<string,string> ();

	public GameObject responsePrefab;
	public GameObject thoughtsPrefab;

	public GameObject response;
	public GameObject thoughts;

	public GameObject responsePos;
	public GameObject thoughtsPos;

	public GameObject dialogueCanvas;

	public GameObject CurDialogueObject;
	public GameObject DialoguePrefab;


	public GameObject ambientRePrefab;
	public GameObject ambientThPrefab;
	
	public GameObject ambientRePos;
	public GameObject ambientThPos;

	public GameObject CurAmbientObject;
	public GameObject AmbientPrefab;

	public GameObject ambientButton;

	public GameObject buttonThatActivatedThisDialogue;
	public bool isDialogue;

	void Awake()
	{
		if (instance != null && instance != this) {
			Destroy (gameObject);
		}
		instance = this;
		DontDestroyOnLoad (gameObject);
	}

	public void EnterDialogue(string dialName, GameObject b){
		isDialogue = true;
		Debug.Log ("Entering Dialogue "+dialName);
		CurDialogueObject = Instantiate (DialoguePrefab,this.transform.position,this.transform.rotation) as GameObject;
		CurDialogueObject.transform.parent = dialogueCanvas.transform;
		CurDialogueObject.GetComponent<dialogueProgression> ().dialName = dialName;
		ambientButton.SetActive (false);
		canvasManager.instance.ChangeDialogueCanvas (true);
		buttonThatActivatedThisDialogue = b;
	}

	public void ExitDialogue(){
		isDialogue = false;
		Debug.Log ("Exit dialogue");
		string dName = CurDialogueObject.GetComponent<dialogueProgression> ().dialName;
		int pos = dialoguePosition;
		Destroy (CurDialogueObject);
		Destroy (response.gameObject);
		Destroy (thoughts.gameObject);
		dialoguePosition = 0;
		ambientButton.SetActive (false);
		canvasManager.instance.ChangeDialogueCanvas (false);
		if (buttonThatActivatedThisDialogue != null) {
			Destroy (buttonThatActivatedThisDialogue);
			buttonThatActivatedThisDialogue = null;
		}
		Story.instance.OnExitDialogue (dName, pos);
	}


	public void TriggerNextDialogue(GameObject but){
		Destroy (but.transform.parent.gameObject);
		Destroy (response.gameObject);
		Destroy (thoughts.gameObject);

		//Debug.Log("trigger next");

		if (dialoguePosition == 0) {
			Story.instance.OnDialogueTrigger (CurDialogueObject.GetComponent<dialogueProgression>().dialName,dialoguePosition);
		}
		dialoguePosition = int.Parse (but.GetComponent<Button>().name);

		if(dialoguePosition > 0)
			Story.instance.OnDialogueTrigger (CurDialogueObject.GetComponent<dialogueProgression>().dialName,dialoguePosition);

		if (dialogueOptionContainerScript.instance.allDialogues [CurDialogueObject.GetComponent<dialogueProgression> ().dialName].Find (x => x.id == dialogueManager.instance.dialoguePosition).disengage == true) {

			//If dialogue or should trigger on exit of this one... do this
			Debug.Log("is disengaging");
			if(dialogueOptionContainerScript.instance.allDialogues[CurDialogueObject.GetComponent<dialogueProgression> ().dialName].Find (x => x.id == dialogueManager.instance.dialoguePosition).nextToTrigger.shouldTrigger){
				int pos = dialogueManager.instance.dialoguePosition;
				if(dialogueOptionContainerScript.instance.allDialogues[CurDialogueObject.GetComponent<dialogueProgression> ().dialName].Find (x => x.id == pos).nextToTrigger.isDialogue){
					ExitDialogue ();
					Debug.Log("is entering new dialogue from dialogue ");
					EnterDialogue(dialogueOptionContainerScript.instance.allDialogues[CurDialogueObject.GetComponent<dialogueProgression> ().dialName].Find (x => x.id == pos).nextToTrigger.name,null);
				}
				else{
					ExitDialogue ();
					Debug.Log("is entering new ambient from dialogue ");
					Debug.Log(CurDialogueObject.GetComponent<dialogueProgression> ().dialName+" "+pos);
//					Debug.Log(dialogueOptionContainerScript.instance.allDialogues[CurDialogueObject.GetComponent<dialogueProgression> ().dialName]);
//					Debug.Log(dialogueOptionContainerScript.instance.allDialogues[CurDialogueObject.GetComponent<dialogueProgression> ().dialName].Find (x => x.id == pos));
					Debug.Log(dialogueOptionContainerScript.instance.allDialogues[CurDialogueObject.GetComponent<dialogueProgression> ().dialName].Find (x => x.id == pos).nextToTrigger.name);
					EnterAmbient(dialogueOptionContainerScript.instance.allDialogues[CurDialogueObject.GetComponent<dialogueProgression> ().dialName].Find (x => x.id == pos).nextToTrigger.name,null);
				}
			}
			else{
				Debug.Log("No NextTrigger exists");
				ExitDialogue ();
			}
		} else {
			CurDialogueObject.GetComponent<dialogueProgression> ().TriggerNextInstance ();
			GlitchManager.instance.OnChangingText();
		}
	}



// AMBIENT

	public void EnterAmbient(string name, GameObject b){
		isDialogue = false;
		Debug.Log ("Entering Ambient "+name);
		CurDialogueObject = Instantiate (AmbientPrefab,this.transform.position,this.transform.rotation) as GameObject;
		CurDialogueObject.transform.parent = dialogueCanvas.transform;
		CurDialogueObject.GetComponent<ambientProgression> ().ambientName = name;
		canvasManager.instance.ChangeDialogueCanvas (true);
		ambientButton.SetActive (true);
		buttonThatActivatedThisDialogue = b;

	}

	public void ExitAmbient(){
		isDialogue = false;
		Debug.Log ("Exiting Ambient");
		string dName = CurDialogueObject.GetComponent<dialogueProgression> ().dialName;
		Destroy (CurDialogueObject);
		Destroy (response.gameObject);
		Destroy (thoughts.gameObject);
		dialoguePosition = 0;
		canvasManager.instance.ChangeDialogueCanvas (false);
		ambientButton.SetActive (false);
		if (buttonThatActivatedThisDialogue != null) {
			Destroy (buttonThatActivatedThisDialogue);
			buttonThatActivatedThisDialogue = null;
		}
		Story.instance.OnExitDialogue (dName);
	}

	public void TriggerNextAmbient(){
		Destroy (response.gameObject);
		Destroy (thoughts.gameObject);

		if (dialoguePosition == 0) {
			Story.instance.OnDialogueTrigger (CurDialogueObject.GetComponent<ambientProgression>().ambientName,dialoguePosition);
		}

		dialoguePosition++;

		Story.instance.OnDialogueTrigger (CurDialogueObject.GetComponent<ambientProgression>().ambientName,dialoguePosition);

		//Debug.Log (CurDialogueObject.GetComponent<ambientProgression> ().ambientName+" "+dialogueManager.instance.dialoguePosition);
		//Debug.Log (ambientVoiceContainer.instance.allAmbients [CurDialogueObject.GetComponent<ambientProgression> ().ambientName]);
		//Debug.Log (ambientVoiceContainer.instance.allAmbients [CurDialogueObject.GetComponent<ambientProgression> ().ambientName].Find (x => x.id == dialogueManager.instance.dialoguePosition).disengage);



		if (ambientVoiceContainer.instance.allAmbients [CurDialogueObject.GetComponent<ambientProgression> ().ambientName].Find (x => x.id == dialogueManager.instance.dialoguePosition).disengage == true) {
			Debug.Log("is disengaging");
			//If dialogue or should trigger on exit of this one... do this
			if(ambientVoiceContainer.instance.allAmbients[CurDialogueObject.GetComponent<ambientProgression> ().ambientName].Find (x => x.id == dialogueManager.instance.dialoguePosition).nextToTrigger.shouldTrigger){
				int pos = dialogueManager.instance.dialoguePosition;
				if(ambientVoiceContainer.instance.allAmbients[CurDialogueObject.GetComponent<ambientProgression> ().ambientName].Find (x => x.id == pos).nextToTrigger.isDialogue){
					ExitAmbient ();
					Debug.Log("is entering new dialogue from ambient");
					EnterDialogue(ambientVoiceContainer.instance.allAmbients[CurDialogueObject.GetComponent<ambientProgression> ().ambientName].Find (x => x.id == pos).nextToTrigger.name,null);
				}
				else{
					ExitAmbient ();
					Debug.Log("is entering new ambient from ambient");
					Debug.Log(ambientVoiceContainer.instance.allAmbients[CurDialogueObject.GetComponent<ambientProgression> ().ambientName].Find (x => x.id == pos).nextToTrigger.name);
					EnterAmbient(ambientVoiceContainer.instance.allAmbients[CurDialogueObject.GetComponent<ambientProgression> ().ambientName].Find (x => x.id == pos).nextToTrigger.name,null);
				}
			}
			else{
				ExitAmbient ();
			}

		} else {
			CurDialogueObject.GetComponent<ambientProgression> ().TriggerNextInstance ();
			GlitchManager.instance.OnChangingText();
		}
	}

}
