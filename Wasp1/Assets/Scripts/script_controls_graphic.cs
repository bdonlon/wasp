﻿using UnityEngine;
using System.Collections;

public class script_controls_graphic : MonoBehaviour {

	public GameObject graphic_move;
	public GameObject graphic_swing;
	public Sprite[] Controls;
	SpriteRenderer rend1;
	SpriteRenderer rend2;

	// Use this for initialization
	void Start () {
		//StartCoroutine(moveGraphics());
		rend1=graphic_move.GetComponent<SpriteRenderer>();
		rend2=graphic_swing.GetComponent<SpriteRenderer>();

		if(Input.GetJoystickNames().Length > 0){
			//Use controller graphic if controller is connected
			rend1.sprite=Controls[0];
			rend2.sprite=Controls[1];
		}
		if(ApplicationModel.autoSwing){
			//No swing controlls when easy mode enabled
			rend2.sprite=Controls[2];
			rend2.transform.localScale = new Vector2(1.5f,1.5f);
		}
		StartCoroutine(fadeOut());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public IEnumerator fadeOut()
	{
		for (float i = 255; i >= 0; i--) {
			rend1.color = new Color(rend1.color.r, rend1.color.g, rend1.color.b, i/255*3);
			rend2.color = new Color(rend2.color.r, rend2.color.g, rend2.color.b, i/255*3);
			yield return new WaitForSeconds(0.0f);
		}

		Destroy(this.gameObject);
	}
}
