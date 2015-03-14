using UnityEngine;
using System.Collections;

public class script_storm_behaviour : MonoBehaviour {

	public GameObject graphic_clouds;
	private Vector3 cloudsForce;
	private int timer,delay;
	public GameObject[] rainSpawners;

	// Use this for initialization
	void Start () {
		timer=0;
		delay=80;
		cloudsForce = new Vector3(0,-3,0);
	}
	
	// Update is called once per frame
	void Update () {
		if(timer>delay){
			graphic_clouds.gameObject.active=true;
			rain();
			rollClouds ();
		}else{
			timer++;
		}
	}

	private void rollClouds(){
		if(graphic_clouds.transform.position.y > 0){
			graphic_clouds.rigidbody2D.AddForce(cloudsForce);
		}else{
			graphic_clouds.rigidbody2D.velocity = Vector3.zero;
		}
	}

	private void rain(){
		for(int i=0; i < rainSpawners.Length; i++){
			rainSpawners[i].gameObject.active=true;
		}
	}
}
