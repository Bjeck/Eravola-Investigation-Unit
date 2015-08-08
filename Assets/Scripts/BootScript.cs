using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class BootScript : MonoBehaviour {
	public static BootScript instance { get; private set; }
	
	public Text text;
	public InputField ipf;
	public textRoll t;
	public textRoll buttonText;
	public GameObject button;
	public GameObject panel;
	float bootWaitTime = 1f;
	bool haveplayedBootSound;

	float passDel;
	public GameObject bootCanvas;
	public GameObject mainGameCanvas;

	public bool hasBooted;
	public int bootSeq;
	public bool buttonShouldSpawn;
	public float rollTime;
	public bool buttonHasSpawned;

	public bool inSearchMode = false;
	public bool hasStartedReboot = false;
	public GameObject DatabaseButton;

	//
	void Awake()
	{
		if (instance != null && instance != this) {
			Destroy (gameObject);
		}
		instance = this;
		DontDestroyOnLoad (gameObject);
	}

	// Use this for initialization
	void Start () {
		t.del = 0.05f;
		//passDel = text.gameObject.GetComponent<textRoll> ().textToDisplay.Length * text.gameObject.GetComponent<textRoll> ().del+2f;
		button.SetActive (false);

	}
	
	// Update is called once per frame
	void Update () {

		if (!haveplayedBootSound) {
			if (bootWaitTime > 0) {
				bootWaitTime -= Time.deltaTime;
			} else {
				SoundManager.instance.PlayBootSound ();
				haveplayedBootSound = true;
			}
		}



		if (!inSearchMode) {

			if(hasBooted){
				if (rollTime > 0) {
					rollTime -= Time.deltaTime;
				} else if(!buttonShouldSpawn && !buttonHasSpawned) {
					buttonShouldSpawn = true;
					buttonHasSpawned = false;
				}
				
				if (buttonShouldSpawn && !buttonHasSpawned) {
					if(hasStartedReboot){
						button.SetActive(true);
						WriteButtonText();
						buttonHasSpawned = true;
					}
				}
			}

			
			if (!hasBooted) {
				if (passDel <= 0) {
					ipf.gameObject.SetActive (true);
					ipf.GetComponent<activateInputField> ().ActivateInputField ();
				} else {
					passDel -= Time.deltaTime;
				}
			}
		}
	}

	public void NextBootWindow(){
		button.SetActive(false);
		t.del = 0.02f;
		buttonShouldSpawn = false;
		buttonHasSpawned = false;
		ipf.gameObject.SetActive(false);
		hasBooted = true;

		ClearBootText ();
		GlitchManager.instance.GlitchScreenOnCommand (0.4f);

		switch (bootSeq) {
			case 0:
				
				t.textToDisplay = "----- SEQUENCE TERMINATED -----¤Invalid Argument Error: Query exceeds data string count. Exiting…¤…Done.";
				buttonText.textToDisplay = "[Load Site] ";
				rollTime = 3f;
				break;
			case 1:
				SoundManager.instance.startProcessSound.Play ();
				t.textToDisplay = "Loading process.contamination              ¤Accessing system files           ¤3-8 Ready ¤2-7 Ready ¤1-9 Ready  ¤7-4 Ready     ¤Filling memory banks... ... ...Done.";
				buttonText.textToDisplay = "[Access Site] ";
				rollTime = 5f;
				break;
			case 2:
				t.textToDisplay = "Loading surveySite.sat    ¤Requesting access… Granted. ¤Server ready.        ¤Sending Message  ¤:: Hello ::          ¤…            ¤…         ¤No response from Site.                                   ¤¤Sending Message ¤:: Status(Caudden) ::  ¤Response:    ¤:: Dead ::";
				rollTime = 8f;
				break;
			case 3:
				t.textToDisplay = "POI Requested… Searching...   ¤1 POI Found...  ¤Name: Sauddoc Village  ¤Marked of Interest: Eravola Outbreak Reported   ¤Investigate?";
				rollTime = 4f;
				break;
			case 4:
				t.textToDisplay = "Requesting Access... Access Granted.  ¤Loading POI...  ¤Processing site... ... Done.  ¤Loading status… Done. ¤Checking Population in area... Done. 0 Person(s) present.  ¤Activating Probe... Done.¤Finding Marks of Relevance... Done.  ¤Loading descriptions... Done.   ¤Loading Natural Language Interface... Done.";
				rollTime = 4f;
				break;
			case 5:
				ActivateSearchMode();
				break;
		}

		if (!inSearchMode) {
			t.shouldStopRolling = false;
			buttonText.shouldStopRolling = false;
			
			//buttonText.Start();
			t.Start();
			
			bootSeq++;
		}	
	}

	public void StartReboot(){
		SoundManager.instance.StopAmbients();
		canvasManager.instance.ActivateCanvas(canvasManager.instance.bootCanvas);
		GlitchManager.instance.GlitchScreenOnCommand (5f,2f);
		SoundManager.instance.warningSound.Play ();
		t.textToDisplay = "";
		text.text = "";
		buttonText.gameObject.GetComponent<Text> ().text = "";
		buttonText.shouldStopRolling = true;
		buttonText.StopCoroutine(buttonText.RollText());
		button.SetActive (true);
		buttonText.del = 0.04f;
		buttonText.textToDisplay = ": : : : : : : : : : : : : : : : : : : : : : : : : : : : : : : : : : : : : : : : : : : : : : : : : : : : : : : : : : : : : : :N: : : :X:T";
		buttonText.Start();
		SoundManager.instance.workSound.Play ();
	}

	IEnumerator waitToReboot(){
		float t = 5;
		while (t > 0) {
			t -= Time.deltaTime;
			yield return 0;
		}
		canvasManager.instance.ActivateCanvas(canvasManager.instance.bootCanvas);
		BootScript.instance.NextBootWindow();
	}
	public void HasClickedBootButton(){
		hasStartedReboot = true;
	}





	public void ActivateSearchMode(){
		inSearchMode = true;
		ClearBootText ();
		dialogueManager.instance.EnterDialogue ("search",null);
		SoundManager.instance.PlayScanningSound();
		DatabaseButton.SetActive (false);
	}	









	public void WriteButtonText(){
		buttonText.del = 0.05f;
		buttonText.shouldStopRolling = true;
		buttonText.StopCoroutine(buttonText.RollText());
		buttonText.gameObject.GetComponent<Text> ().text = "";
		buttonText.textToDisplay = "";

		switch (bootSeq - 1) {
		case 0:
			buttonText.textToDisplay = "[Load Site] ";
			break;
		case 1:
			buttonText.textToDisplay = "[Access Site] ";
			break;
		case 2:
			buttonText.textToDisplay = "[Load Nearest POI] ";
			break;
		case 3:
			buttonText.textToDisplay = "[Investigate] ";
			break;
		case 4:
			buttonText.textToDisplay = "[Enter] ";
			break;
		}
		buttonText.Start();
	}


	public void ClearBootText(){
		//t.shouldStopRolling = true;
		//buttonText.shouldStopRolling = true;
		t.StopCoroutine(t.RollText());
		buttonText.StopCoroutine(buttonText.RollText());
		t.gameObject.GetComponent<Text> ().text = "";
		buttonText.gameObject.GetComponent<Text> ().text = "";
		t.textToDisplay = "";
		buttonText.textToDisplay = "";
	}



	public void CheckPassword(){
		string passInp = ipf.text.ToLower();
		if(passInp.Length > 0){
			bootCanvas.SetActive(false);
			ipf.gameObject.SetActive(false);
			hasBooted = true;
			SoundManager.instance.ChangeMasterMixerValue("volume",-5f);
			SoundManager.instance.curMasterVolume = -5f;
			SoundManager.instance.passWordSound.Play();
			//canvasManager.instance.ActivateCanvas(canvasManager.instance.villageCanvas);
			dialogueManager.instance.EnterDialogue("bootIntro", null);
		}
	}
}

