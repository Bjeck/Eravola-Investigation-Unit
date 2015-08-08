using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class buttonSound : MonoBehaviour {

	public AudioSource OnHoverSound;
	public AudioSource OnClickSound;

	public Button b;

	void Start(){
		AudioSource[] temp = GetComponents<AudioSource> ();
		foreach (AudioSource t in temp) {
			if(t.clip.name == "OnHoverSound"){
				OnHoverSound = t;
			}
		}
		b = gameObject.GetComponent<Button> ();
		b.GetComponent<Button>().onClick.AddListener(() => SoundManager.instance.PlayClickSound());
	}

	public void PlayOnHover(){
		OnHoverSound.Play ();
	}

	public void PlayOnClick(){
		OnClickSound.Play ();
	}


}
