using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ambientInst : Inst {
	
	/// <summary>
	/// Instance of a Disengaging dialogue.
	/// </summary>
	public ambientInst(int i, bool dis, NextToTrigger next){
		id = i;
		disengage = dis;
		NextTrigger (next.name,next.isDialogue);
	}
	
	/// <summary>
	/// Instance of a Non-disengaging dialogue.
	/// </summary>
	public ambientInst(int i, string resp, string thou, float respDel, float thouDel){
		id = i;
		response = resp;
		thoughts = thou;
		responseDelay = respDel;
		thoughtsDelay = thouDel;
		
	}
	
}


public class ambientVoiceContainer : MonoBehaviour {
	public static ambientVoiceContainer instance { get; private set; }

	public List<ambientInst> ambientContainer = new List<ambientInst>();
	
	public Dictionary<string, List<ambientInst>> allAmbients = new Dictionary<string, List<ambientInst>>();


	void Awake()
	{
		if (instance != null && instance != this) {
			Destroy (gameObject);
		}
		instance = this;
		DontDestroyOnLoad (gameObject);

		#region firstVillage
	//FIRST VILLAGE INTRO
	/*	ambientInst firstVillage = new ambientInst ();
		firstVillage.id = 0;
		firstVillage.response = "'What?' ¤'Yes. He's coming today.'¤'When?' ¤'Don't know. Soon.'";
		firstVillage.thoughts = "I live in a small hovel on the outskirts of Caudden City, far enough away that you can’t smell the shit but still close enough that the chimneys line the sky.";
		firstVillage.thoughtsDelay = 1.5f;
		ambientContainer.Add (firstVillage);

		ambientInst firstVillage2 = new ambientInst ();
		firstVillage2.id = 1;
		firstVillage2.response = "'Well, I better go tell Rasmin.¤'And I gotta prepare. See you at the inn.'";
		firstVillage2.thoughts = "The most exciting thing that happens in this town is when the smoke changes colour because of a mage experiment gone wrong in the city.";
		firstVillage2.thoughtsDelay = 1.5f;
		
		ambientContainer.Add (firstVillage2);

		ambientInst firstVillage3 = new ambientInst ();
		firstVillage3.id = 2;
		firstVillage3.disengage = true;
		firstVillage3.NextTrigger("innIntro",false);
		ambientContainer.Add (firstVillage3);

		allAmbients.Add ("firstVillage", ambientContainer);
		ambientContainer = new List<ambientInst> ();*/
		#endregion

	}

}
