using UnityEngine;
using System.Collections;

public class script_UI_restart : MonoBehaviour {
	
	public Sprite[] sprites;
	SpriteRenderer rend1;

	// Use this for initialization
	void Start () {

		rend1=this.GetComponent<SpriteRenderer>();

		if(Input.GetJoystickNames().Length > 0){
			//Use controller graphic if controller is connected
			rend1.sprite=sprites[0];
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
