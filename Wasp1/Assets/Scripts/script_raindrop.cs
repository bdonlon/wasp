﻿using UnityEngine;
using System.Collections;

public class script_raindrop : MonoBehaviour {
	
	Animator anim;
	Rigidbody2D rigidbody;
	public Vector2 splashCoordY;
	public SpriteRenderer spriteRenderer;
	public GameObject storm;

	// Use this for initialization
	void Start () {
		storm = GameObject.Find("Storm");
		transform.parent = GameObject.Find("Storm").transform;
		anim = GetComponent<Animator>();
		rigidbody = GetComponent<Rigidbody2D>();
		splashCoordY = new Vector2(0,storm.transform.position.y - Random.Range(100,200)*0.1f);
		spriteRenderer.sortingOrder = (int)Camera.main.WorldToScreenPoint (splashCoordY).y * -1;
	}
	
	// Update is called once per frame
	void Update () {
		if(transform.position.y < (splashCoordY).y){
			transform.parent = GameObject.Find("cloneRain").transform;
			int splash = Random.Range (1,3);	//(inclusive,exclusive)
			switch(splash)
			{
			case 1:
				anim.SetTrigger("splash_01");
				break;
			case 2:
				anim.SetTrigger("splash_02");
				break;
			}
			rigidbody.isKinematic=true;
			//rigidbody.velocity = Vector3.zero;
			Destroy(this.gameObject,0.1f);
		}
	}
}
