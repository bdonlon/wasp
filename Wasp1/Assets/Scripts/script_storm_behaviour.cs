using UnityEngine;
using System.Collections;

public class script_storm_behaviour : MonoBehaviour {

	public GameObject graphic_clouds;
	private Vector3 cloudsForce;
	private int timer,delay;
	public GameObject[] rainSpawners;
	SpriteRenderer cloudSpriteRenderer;
	private float alpha;
	public GameObject _GM;

	// Use this for initialization
	void Start () {
		timer=0;
		delay=80;
		cloudsForce = new Vector3(0,-3,0);
		cloudSpriteRenderer=graphic_clouds.GetComponent<SpriteRenderer>();
		alpha = cloudSpriteRenderer.color.a*255;
	}
	
	// Update is called once per frame
	void Update () {
		if(timer>delay){
			graphic_clouds.gameObject.active=true;
			rain();
			rollClouds();
		}else{
			timer++;
		}
	}

	private void rollClouds(){
		if(graphic_clouds.transform.position.y > 0){
			graphic_clouds.rigidbody2D.AddForce(cloudsForce);
		}else{
			graphic_clouds.rigidbody2D.velocity = Vector3.zero;
			StartCoroutine(fade());
		}
	}

	private void rain(){
		for(int i=0; i < rainSpawners.Length; i++){
			rainSpawners[i].gameObject.active=true;
		}
	}

	IEnumerator fade(){
		for (float i = alpha; i < 255; i++) {	//Fade clouds to full opaque
			cloudSpriteRenderer.color = new Color(1,1,1, i/255);
			_GM.GetComponent<Setup>().setSpecialAlpha(i/255);
			yield return new WaitForSeconds(0.0f);
		}
	}
}
