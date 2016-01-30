using UnityEngine;
using System.Collections;

public class script_tutorial : MonoBehaviour {

	public bool touchOrClickButtonPress;
	public Camera camera1,camera2;
	public float timeStart;
	public float timeCurrent;
	public float timeElapsed;
	public bool timedOut;

	// Use this for initialization
	void Start () {
		timeStart=Time.time;
		timedOut=false;
	}
	
	// Update is called once per frame
	void Update () {
		//Mouse clicking button...
		if(Input.GetMouseButtonDown(0)){
			touchOrClickButtonPress = true;
		}
		
		//Finger touching button...
		for (var i = 0; i < Input.touchCount; ++i) {
			if (Input.GetTouch(i).phase == TouchPhase.Began) {
				touchOrClickButtonPress = true;
			}
		}
		timeElapsed =  Time.time - timeStart;
		if(timeElapsed>4){
			timedOut=true;
		}
		if(Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("360_A") || Input.GetButtonDown("360_Start") || touchOrClickButtonPress || timedOut)
		{	
			timedOut=false;
			timeStart=Time.time;
			touchOrClickButtonPress=false;
			if(camera2.camera.enabled==false)
			{
				camera2.camera.enabled=true;
				camera1.camera.enabled=false;
			}else if(camera2.camera.enabled)
			{
				Application.LoadLevel("wasp1");
			}
		}
	}
}
