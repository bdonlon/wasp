using UnityEngine;
using System.Collections;

public class playSound : MonoBehaviour {

	public AudioClip sound;
	public AudioSource audioSource;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void play(){
		if (sound)
			audioSource.clip = sound;
		audioSource.Play();
	}
}
