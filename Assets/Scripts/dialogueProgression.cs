using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class dialogueProgression : MonoBehaviour {

	public GameObject dialogueOptionsPrefab;

	GameObject curDialogueOptions;

	public string dialName;
	public DialogueInst curInst;
 	dialogueManager dialMan;

	public List<Button> buttonList = new List<Button>();


	// Use this for initialization
	void Start () {
		dialMan = dialogueManager.instance;
		TriggerNextInstance ();
		GlitchManager.instance.OnChangingText();
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void TriggerNextInstance(){

		curInst = dialogueOptionContainerScript.instance.allDialogues [dialName].Find (x => x.id == dialogueManager.instance.dialoguePosition);

		dialMan.response = Instantiate (dialMan.responsePrefab,dialMan.responsePos.transform.position,dialMan.responsePos.transform.rotation) as GameObject;
		dialMan.thoughts = Instantiate (dialMan.thoughtsPrefab,dialMan.thoughtsPos.transform.position,dialMan.thoughtsPos.transform.rotation) as GameObject;
		
		dialMan.response.transform.SetParent (dialMan.responsePos.transform);
		dialMan.thoughts.transform.SetParent (dialMan.thoughtsPos.transform);
		dialMan.response.GetComponent<RectTransform> ().localScale = Vector3.one;
		dialMan.thoughts.GetComponent<RectTransform> ().localScale = Vector3.one;

		//IF IT FAILS HERE, IT CAN'T FIND THE INST
		if(curInst.altResp.shouldAlter)
			dialMan.response.GetComponent<textRoll> ().textToDisplay = curInst.altResp.altResp;
		else
			dialMan.response.GetComponent<textRoll> ().textToDisplay = curInst.response;

		if(curInst.altThou.shouldAlter)
			dialMan.thoughts.GetComponent<textRoll> ().textToDisplay = curInst.altThou.altResp;
		else
			dialMan.thoughts.GetComponent<textRoll> ().textToDisplay = curInst.thoughts;

		dialMan.response.GetComponent<textRoll> ().del = curInst.responseSpeed;
		dialMan.response.GetComponent<textRoll> ().startDel = curInst.responseDelay;
		dialMan.thoughts.GetComponent<textRoll> ().del = curInst.thoughtsSpeed;
		dialMan.thoughts.GetComponent<textRoll> ().startDel = curInst.thoughtsDelay;


		//if (dialName == "search") {
		//	dialMan.response.GetComponent<Text>().fontSize = 24;
			dialMan.response.GetComponent<Text>().color = new Color(0.34f,0.77f,0.14f);
		//}



		StartCoroutine (SpawnButtons(curInst.optionDelay));


	}


	IEnumerator SpawnButtons(float t){
		while (t > 0) {
			t -= Time.deltaTime;
			yield return 0;
		}

		curDialogueOptions = Instantiate (dialogueOptionsPrefab,this.transform.position,this.transform.rotation) as GameObject;
		
		curDialogueOptions.transform.SetParent (dialogueManager.instance.dialogueCanvas.transform);
		curDialogueOptions.GetComponent<RectTransform> ().localScale = Vector3.one;
		curDialogueOptions.GetComponent<RectTransform> ().anchoredPosition3D = new Vector3(-150,150,0);
		
		
		List<Button> buttonList = new List<Button> ();
		buttonList.AddRange (curDialogueOptions.GetComponentsInChildren<Button> ()); 
		
		//Remove uneeded buttons.
		if (buttonList.Count > curInst.options.options.Count) {
			for(int j=0;j<4-curInst.options.options.Count;j++){
				Destroy(buttonList[j].gameObject);
			}
			buttonList.RemoveRange(0,4-curInst.options.options.Count);
			
		}
		

		List<string> stringList = new List<string> ();
		List<int> intList = new List<int> ();
		foreach(DialOption d in curInst.options.options){
			stringList.Add(d.option);
			intList.Add(d.nr);
		}

		
		int i = 0;
		foreach (Button b in buttonList) {
			buttonList[i].GetComponentInChildren<Text>().text = stringList[i];
			buttonList[i].name = intList[i].ToString();
			if (dialName == "search") {
				buttonList[i].GetComponentInChildren<Text>().color = new Color(0.34f,0.77f,0.14f);
			}

			i++;
		}
	}








}
