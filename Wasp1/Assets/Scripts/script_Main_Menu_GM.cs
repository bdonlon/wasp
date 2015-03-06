using UnityEngine;
using System.Collections;

public class script_Main_Menu_GM : MonoBehaviour {

	public Camera cam1;
	public Camera cam2;
	public GameObject[] cameras;
	public GameObject[] cursors;

	public 

	// Use this for initialization
	void Start () {
		changeScreen(0);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void changeScreen(int cam){
		for(int i=0; i<cameras.Length; i++)
		{
			if(i==cam){
				cameras[i].camera.enabled=true;
				cursors[i].gameObject.active=true;
			}else{
				cameras[i].camera.enabled=false;
				cursors[i].gameObject.active=false;
			}
		}
	}
}
