using UnityEngine;
using System.Collections;

public class playSound : MonoBehaviour {

	public AudioClip sound;
	public AudioSource audioSource;
	
	public float audio2Volume = 0.0f;

	public float soundTime;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void playLooped(){
		audioSource.loop=true;
		play();
	}

	public void play(){
		if(sound){
			audioSource.clip = sound;
		}
		audioSource.time=audioSource.time+Random.Range(0.0f,7.0f);
		audioSource.Play();
	}

	public void stop(){
		audioSource.Stop();
	}

	public IEnumerator  fadeIn() {
		for(int i = 0; i <= 1000; i++)
		{
			audio2Volume += 0.01f;
			audioSource.volume = audio2Volume;
			yield return new WaitForSeconds(0.0f);
		}
	}
}
