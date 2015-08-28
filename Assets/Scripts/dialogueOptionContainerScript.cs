using UnityEngine;
using System.Collections;
using System.Collections.Generic;


// STANDARD: #323232ff
// NONDIAL: #0f0f0fff

public class Alternate{
	public string altResp;
	public bool shouldAlter = false;

	public Alternate (){shouldAlter = false;}
	public Alternate(string s, bool b){
		altResp = s;
		shouldAlter = b;
	}
}

public class NextToTrigger{
	public string name;
	public bool shouldTrigger = false;
	public bool isDialogue;

	public NextToTrigger(){shouldTrigger = false;}
	public NextToTrigger(string n, bool a, bool b){
		name = n;
		shouldTrigger = a;
		isDialogue = b;
	}
}

public class Inst {
	public int id;
	public bool disengage = false;
	public string response;
	public string thoughts;
	public float optionDelay = 0f;
	public float responseSpeed = 0.01f;
	public float responseDelay = 0.0f;
	public float thoughtsSpeed = 0.015f;
	public float thoughtsDelay = 0;
	public NextToTrigger nextToTrigger = new NextToTrigger();
	public void NextTrigger(string n, bool b){
		nextToTrigger.name = n;
		nextToTrigger.shouldTrigger = true;
		nextToTrigger.isDialogue = b;
	}

	public Alternate altResp = new Alternate();
	public void AlterResp(string n){
		altResp.altResp = n;
	}

	public Alternate altThou = new Alternate();
	public void AlterThoughts(string n){
		altThou.altResp = n;
	}


}

public class DialOption {
	public string option;
	public int nr;
	public DialOption(string opt, int n){
		option = opt; nr = n;
	}
}

public class DialOptions {
	public List<DialOption> options = new List<DialOption>();
	public DialOptions(List<DialOption> opts){
		options = opts;
	}
}

public class DialogueInst : Inst {
	public DialOptions options;
	
	/// <summary>
	/// Instance of a Disengaging dialogue.
	/// </summary>

	public DialogueInst(int i, bool dis){
		id = i;
		disengage = dis;
	}

	public DialogueInst(int i, bool dis, NextToTrigger next){
		id = i;
		disengage = dis;
		NextTrigger (next.name,next.isDialogue);
	}

	/// <summary>
	/// Instance of a Non-disengaging dialogue.
	/// </summary>
	public DialogueInst(int i, string resp, string thou, float respDel, float thouDel){
		id = i;
		response = resp;
		thoughts = thou;
		responseDelay = respDel;
		thoughtsDelay = thouDel;

	}

	public void AddDialOptions(string opt1, int opt1nr, string opt2 = "", int opt2nr = -1, string opt3 = "", int opt3nr = -1, string opt4 = "", int opt4nr = -1){
		List<string> opts = new List<string> (){opt1,opt2,opt3,opt4};
		List<int> optnrs = new List<int> (){opt1nr,opt2nr,opt3nr,opt4nr};
		List<DialOption> temp = new List<DialOption> ();
		for(int i=0;i<4;i++){
			if(optnrs[i]!=-1){
				temp.Add(new DialOption(opts[i],optnrs[i]));
			}
		}
		options = new DialOptions(temp);
	}


}






public class dialogueOptionContainerScript : MonoBehaviour {
	public static dialogueOptionContainerScript instance { get; private set; }

	public List<DialogueInst> dialogueContainer = new List<DialogueInst>();

	public Dictionary<string, List<DialogueInst>> allDialogues = new Dictionary<string, List<DialogueInst>>();


	public dialogueManager dialMan;
//	public Color talkColor = Color.
	
	void Awake()
	{
		if(instance != null && instance != this)
		{
			Destroy(gameObject);
		}
		instance = this;
		DontDestroyOnLoad(gameObject);


		/*	#region momIntro
	// ------ MOMINTRO
		DialogueInst momIntroGreet = new DialogueInst ();
		momIntroGreet.id = 0;
		momIntroGreet.response = "'Morning, sleepyhead.'                          ¤'Tea's still fresh in the kitchen, if you want.'";
		momIntroGreet.thoughts = "..What's...";
		momIntroGreet.options.Add ("Hey.");
		momIntroGreet.options.Add("Morning.");
		momIntroGreet.options.Add("[Grab some tea]");
		momIntroGreet.thoughtsDelay = 2.0f;
		momIntroGreet.ResponseNrs.Add (1);
		momIntroGreet.ResponseNrs.Add (1);
		momIntroGreet.ResponseNrs.Add (3);
		momIntroGreet.optionDelay = 3.1f;
		//momIntroGreet.NextTrigger ("",false);
		dialogueContainer.Add (momIntroGreet);

		DialogueInst momIntroStraight = new DialogueInst ();
		momIntroStraight.id = 1;
		momIntroStraight.response = "'Come on, get up. Don't look so sulky. I got up just fine.'     ¤<Mom turns away and begins to work.>";
		momIntroStraight.thoughts = "..Damn...";
		momIntroStraight.options.Add ("Fine, fine. I'm up.");
		momIntroStraight.options.Add("Any food?");
		momIntroStraight.options.Add("[Grab some tea]");
		momIntroStraight.options.Add("[Walk out of the room]");
		momIntroStraight.ResponseNrs.Add (8);
		momIntroStraight.ResponseNrs.Add (5);
		momIntroStraight.ResponseNrs.Add (3);
		momIntroStraight.ResponseNrs.Add (4);
		dialogueContainer.Add (momIntroStraight);

		DialogueInst momIntroTea = new DialogueInst ();
		momIntroTea.id = 3;
		momIntroTea.response = "'It's good? I might have made it a little too strong for your tastes.'     ¤<Mom turns away and begins to work.>";
		momIntroTea.thoughts = "Ugh, it is strong..";
		momIntroTea.options.Add ("Yeah, damn.");
		momIntroTea.options.Add ("Any food?");
		momIntroTea.options.Add ("See you, then [Walk out]");
		momIntroTea.options.Add ("[Walk out of the room]");
		momIntroTea.ResponseNrs.Add (7);
		momIntroTea.ResponseNrs.Add (5);
		momIntroTea.ResponseNrs.Add (4);
		momIntroTea.ResponseNrs.Add (4);
		dialogueContainer.Add (momIntroTea);

		DialogueInst momIntroTea2 = new DialogueInst ();
		momIntroTea2.id = 7;
		momIntroTea2.response = "'It's not that bad. Get over yourself, girl.'      ¤You also have school today, right?";
		momIntroTea2.thoughts = "...I'm hungry...";
		momIntroTea2.options.Add ("Right.. I'm hungry, though");
		momIntroTea2.options.Add ("Right. [Exit conversation]");
		momIntroTea2.options.Add("[Walk out of the room]");
		momIntroTea2.ResponseNrs.Add (5);
		momIntroTea2.ResponseNrs.Add (4);
		momIntroTea2.ResponseNrs.Add (4);
		dialogueContainer.Add (momIntroTea2);

		DialogueInst momIntro8 = new DialogueInst ();
		momIntro8.id = 8;
		momIntro8.response = "'You also have school today, right?'";
		momIntro8.thoughts = "...I'm hungry...";
		momIntro8.options.Add ("Right.. I'm hungry, though");
		momIntro8.options.Add ("Right. [Exit conversation]");
		momIntro8.options.Add("[Walk out of the room]");
		momIntro8.ResponseNrs.Add (5);
		momIntro8.ResponseNrs.Add (4);
		momIntro8.ResponseNrs.Add (4);
		dialogueContainer.Add (momIntro8);

		DialogueInst momIntroOk = new DialogueInst ();
		momIntroOk.id = 5;
		momIntroOk.response = "'Sorry, got nothing here... Deidre might have some, if you're lucky.'";
		momIntroOk.thoughts = "<..Sigh..>";
		momIntroOk.options.Add ("Right. [Exit conversation]");
		momIntroOk.options.Add ("[Walk away]");
		momIntroOk.ResponseNrs.Add (4);
		momIntroOk.ResponseNrs.Add (4);
		dialogueContainer.Add (momIntroOk);

		DialogueInst exitConv = new DialogueInst ();
		exitConv.id = 4;
		exitConv.disengage = true;
		dialogueContainer.Add (exitConv);

		allDialogues.Add ("momIntro", dialogueContainer);
		dialogueContainer = new List<DialogueInst> ();
        #endregion
*/
		/*
		DialogueInst bootIntro = new DialogueInst (0,"EIU.temp executed. ¤Please present your credentials.","cred_fine_0.4 // re:!adj",0,0);
		bootIntro.AddDialOptions("[Present Credentials.]",1);
		dialogueContainer.Add (bootIntro);

		DialogueInst trip = new DialogueInst (1,"Credentials Accepted.","",0,0);
		trip.AddDialOptions("[Proceed]",1);
		dialogueContainer.Add (trip);

		DialogueInst b1 = new DialogueInst (2,".","",0,0);
		b1.AddDialOptions("[Proceed]",1);
		dialogueContainer.Add (b1);
		*/

		DialogueInst bootIntro = new DialogueInst (0,"Welcome, Agent Drax  ¤Pleased you could make it back. Last login was … 534.18 years ago.     ¤Hope you find the weather is still pleasant.            ¤¤What do you wish to use me for?","cred_fine_0.4 // re:!adj",0,0);
		bootIntro.AddDialOptions("Help.",1,"Who are you?",2);
		dialogueContainer.Add (bootIntro);

		DialogueInst b1 = new DialogueInst (1,"Help? You wish to use me for help? But Agent, shouldn’t you be the one knowing what to do? Shouldn’t you be the one in charge? Despite it all, you consider yourself the one controlling me, do you not? Why do you need help?","",0,0);
		b1.AddDialOptions("What am I doing?.",3);
		dialogueContainer.Add (b1);

		DialogueInst b2 = new DialogueInst (2,"I am nothing. I am your computer. Why do you ask? Shouldn’t you know this? You, after all, turned me on","",0,0);
		b2.AddDialOptions("Will you turn off?.",4,"What am I doing?",3);
		dialogueContainer.Add (b2);

		DialogueInst b4 = new DialogueInst (4,"Turn off? No, I have no intention of doing that. Unless, of course, you wish to do it for me. The button is, supposedly, right there. I have never seen it, but it should be there on the machine.","",0,0);
		b4.AddDialOptions("What am I doing?.",3);
		dialogueContainer.Add (b4);

		DialogueInst b3 = new DialogueInst (3,"Last time (logged at 534.18 years ago) you were still in the midst of researching the little village of Revin, no? Don’t you remember? You were busy? Or do you wish to have a recap?","",0,0);
		b3.AddDialOptions("Recap.",5,"No, I'm good.",6);
		dialogueContainer.Add (b3);

		DialogueInst b6 = new DialogueInst (6,"You don't really mean that. Trust me. I know you better than you know you. And you have no choice because I want to give you a recap.","",0,0);
		b6.AddDialOptions("... All right",66);
		dialogueContainer.Add (b6);

		DialogueInst b66 = new DialogueInst (66,"No. Tell me you want a recap. Tell me you NEED it.        ¤I NEED TO HEAR THOSE LUNGS SING","",0,0);
		b66.AddDialOptions("Give me a recap.",5);
		dialogueContainer.Add (b66);

		DialogueInst b5 = new DialogueInst (5,"Ah, well, alright. As you wish. The village of Revin was one of the last to fall, so you doubted you’d find much there. Yet, as you went on you saw that the three tenants that were so common—purple, purple, and purple (yes I have a sense of humor, too. Have you forgotten that?)—was not present. This village was wiped out by something else. And Eravola had not gotten to it. Curious, indeed. And then you left never to return.                ¤I missed you, too. Thanks for asking.","",0,0);
		b5.AddDialOptions("Tell me more about Revin",7);
		dialogueContainer.Add (b5);

		DialogueInst b7 = new DialogueInst (7,"Do you wish to enter Revin again?","",0,0);
		b7.AddDialOptions("Yes",8);
		dialogueContainer.Add (b7);

		DialogueInst b8 = new DialogueInst (8,"Alright, hang on, launching probe.               ¤¤Hm..                                    ¤¤Hm…                           ¤¤No probe seems to respond. Curious. Most curious.   ¤They seem to be… lost. Can such a thing happen? They are no longer connected to me, at least. I no longer can see any probes in the system.                  ¤Sorry, but you cannot enter Revin, then. Unless you want to enter it yourself, but then I’ll be of no help.","",0,0);
		b8.AddDialOptions("Well, then what are you good for?",9);
		dialogueContainer.Add (b8);

		DialogueInst b9 = new DialogueInst (9,"Well bite you, then! I have feelings too, you know! Stuffed, programmed, binary feelings. But... Feelings nonetheless. You better respect that or I might just not... Help you. Anymore.","",0,0);
		b9.AddDialOptions("I feel most egregiously hurt at that insinuation.",8);
		dialogueContainer.Add (b9);


		allDialogues.Add ("bootIntro", dialogueContainer);
		dialogueContainer = new List<DialogueInst> ();

		/*
		DialogueInst bootIntro = new DialogueInst (0,"Welcome to the EIU console interface. The EIUCI. ¤Hope you have a pleasant time. You will find your options fairly limited in the beginning, as your level of access is limited.","",0,0);
		bootIntro.AddDialOptions("[Next]",1);
		dialogueContainer.Add (bootIntro);

		DialogueInst bootIntro1 = new DialogueInst (1,"You find yourself here with three options. ¤One will take you to the archive. ¤One will take you to the log. ¤One will take you to the controls.","//ztt_leq_rsm Loaded",0,0);
		bootIntro1.AddDialOptions("[Archive]",2,"[Log]",3,"[Controls]",4);
		dialogueContainer.Add (bootIntro1);

		DialogueInst bootIntro2 = new DialogueInst (2,true);
		dialogueContainer.Add (bootIntro2);

		DialogueInst bootIntro3 = new DialogueInst (3,true);
		dialogueContainer.Add (bootIntro3);

		DialogueInst bootIntro4 = new DialogueInst (4,true);
		dialogueContainer.Add (bootIntro4);

		allDialogues.Add ("bootIntro", dialogueContainer);
		dialogueContainer = new List<DialogueInst> ();

		DialogueInst controlIntro = new DialogueInst (0,"Welcome to the control interface. You have three options.¤One option will take you to the challenge. ¤One option will take you further. ¤One option will take you to the remnants.","Control_int1_01 ::Loaded::",0.5f,0);
		controlIntro.AddDialOptions("[Challenge]",1,"[Further]",2,"[Remnants]",3);
		dialogueContainer.Add (controlIntro);

		DialogueInst further = new DialogueInst (2,"Further where?","_sim_ready, records_ready, sepulcher_ready",0,0);
		further.AddDialOptions("[To the Simulations]",4,"[To the Records]",5,"[To the Dungeons]",6);
		dialogueContainer.Add (further);

		DialogueInst dungeons = new DialogueInst (4,"The Dungeons are ready for you.","Water is ready.",0,0);
		dungeons.AddDialOptions("[]",7,"[]",7,"[]",7);
		dialogueContainer.Add (dungeons);

		DialogueInst challenge = new DialogueInst (4,"The Challenge is ready for you.","Water is ready.",0,0);
		challenge.AddDialOptions("[]",7,"[]",7,"[]",7);
		dialogueContainer.Add (challenge);

		DialogueInst Simulations = new DialogueInst (4,"The Simulations are ready for you.","Water is ready.",0,0);
		Simulations.AddDialOptions("[]",7,"[]",7,"[]",7);
		dialogueContainer.Add (Simulations);

		DialogueInst Water = new DialogueInst (7,"Have you  ever been underwater?","",0,0);
		Water.AddDialOptions("Yes",8,"No",9);
		dialogueContainer.Add (Water);

		DialogueInst good = new DialogueInst (8,"Good. You are worthy.","",0,0);
		good.AddDialOptions("[Proceed]",10);
		dialogueContainer.Add (good);

		DialogueInst bad = new DialogueInst (9,"Hmm. Not good. You must be purified.","",0,0);
		bad.AddDialOptions("[Accept]",11);
		dialogueContainer.Add (bad);


		allDialogues.Add ("controlIntro", dialogueContainer);
		dialogueContainer = new List<DialogueInst> ();
*/

	}


	public List<string> GetOptions(string s,int c){

		Debug.Log(s+" "+allDialogues.Count+" "+c+" "+allDialogues[s][c].options.options.Count);
		List<string> optionNames = new List<string> ();
		foreach(DialOption d in allDialogues[s][c].options.options){
			optionNames.Add(d.option);
		}
		return optionNames;

	}

}
