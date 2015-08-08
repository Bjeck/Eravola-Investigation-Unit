using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using System.Text;

public class glitchText : MonoBehaviour {

	public float timeToGlitchMin;
	public float timeToGlitchMax;
	float timeToGlitch;
	public float sustainGlitchTimeMin = 0.03f;
	public float sustainGlitchTimeMax = 0.4f;
	Text text;

	string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZqwertyuiopåasdfghjklæøzxcvbnm,.-<1234567890+½§!#%&/()=?*<>@£$€{[]}'*-/";
	List<char> listOfChars = new List<char>();

	// Use this for initialization
	void Start () {
		text = GetComponent<Text> ();
		/*for (int i = char.MinValue; i <= char.MaxValue; i++)
		{
			char c = Convert.ToChar(i);
			if (!char.IsControl(c))
			{
				listOfChars.Add(c);
			}
		}*/
		listOfChars.AddRange(chars.ToCharArray ());
		timeToGlitch = UnityEngine.Random.Range (timeToGlitchMin, timeToGlitchMax);
		//Debug.Log(timeToGlitch);
	}
	
	// Update is called once per frame
	void Update () {
		if (timeToGlitch > 0) {
			timeToGlitch -= Time.deltaTime;
		} else {
			StartCoroutine(GlitchText());
		}
	
	}

	public IEnumerator GlitchText(){
		timeToGlitch = UnityEngine.Random.Range (timeToGlitchMin, timeToGlitchMax);
		char origChar = '&';
		int charToGlitch = UnityEngine.Random.Range (0, text.text.Length);
		int i = 0;

		if (UnityEngine.Random.Range (0, 2) == 0) {
			SoundManager.instance.PlayTextGlitchSound ();
		}

		foreach (char c in text.text) {
			if(i==charToGlitch){

				try{
					char newChar = listOfChars[UnityEngine.Random.Range(0,listOfChars.Count)];
					
					origChar = text.text[i];
					StringBuilder sb = new StringBuilder(text.text);
					sb[charToGlitch] = newChar;
					text.text = sb.ToString();
				}
				catch(Exception e){
					Debug.Log("string glitch error"+e);
				};

			}
			i++;
		}

		float time = UnityEngine.Random.Range(sustainGlitchTimeMin,sustainGlitchTimeMax);
		while (time > 0) {
			time -= Time.deltaTime;
			yield return 0;
		}

		if (text.text.Length != 0) {
			StringBuilder sbo = new StringBuilder(text.text);
			sbo[charToGlitch] = origChar;
			text.text = sbo.ToString();
		}

		yield return 0;
	}
}
