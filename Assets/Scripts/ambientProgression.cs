using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class ambientProgression : MonoBehaviour {

	GameObject curambientOptions;
	
	public string ambientName;
	public ambientInst curInst;
	dialogueManager dialMan;

	// Use this for initialization
	void Start () {
		dialMan = dialogueManager.instance;
		TriggerNextInstance ();
		GlitchManager.instance.OnChangingText();
	}
	
	public void TriggerNextInstance(){
		
		curInst = ambientVoiceContainer.instance.allAmbients [ambientName].Find (x => x.id == dialogueManager.instance.dialoguePosition);
		
		dialMan.response = Instantiate (dialMan.ambientRePrefab,dialMan.ambientRePos.transform.position,dialMan.ambientRePos.transform.rotation) as GameObject;
		dialMan.thoughts = Instantiate (dialMan.ambientThPrefab,dialMan.ambientThPos.transform.position,dialMan.ambientThPos.transform.rotation) as GameObject;
		
		dialMan.response.transform.SetParent (dialMan.ambientRePos.transform);
		dialMan.thoughts.transform.SetParent (dialMan.ambientThPos.transform);
		dialMan.response.GetComponent<RectTransform> ().localScale = Vector3.one;
		dialMan.thoughts.GetComponent<RectTransform> ().localScale = Vector3.one;



		if(curInst.altResp.shouldAlter)
			dialMan.response.GetComponent<textRoll> ().textToDisplay = curInst.altResp.altResp;
		else
			dialMan.response.GetComponent<textRoll> ().textToDisplay = curInst.response;
		
		if(curInst.altThou.shouldAlter)
			dialMan.thoughts.GetComponent<textRoll> ().textToDisplay = curInst.altThou.altResp;
		else
			dialMan.thoughts.GetComponent<textRoll> ().textToDisplay = curInst.thoughts;

		dialMan.response.GetComponent<textRoll> ().startDel = curInst.responseDelay;
		dialMan.thoughts.GetComponent<textRoll> ().startDel = curInst.thoughtsDelay;
		dialMan.response.GetComponent<textRoll> ().del = curInst.responseSpeed;
		dialMan.thoughts.GetComponent<textRoll> ().del = curInst.thoughtsSpeed;

	}

}
