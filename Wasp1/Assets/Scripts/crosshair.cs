﻿using UnityEngine;
using System.Collections;

public class crosshair : MonoBehaviour {

	public Quaternion q;
	public float angle;

	public GameObject localMouse;

	// Update is called once per frame
	void Update () {

		Vector3 mousePosition = localMouse.transform.position;
		Vector3 playerPosition = transform.parent.position;

		float vx = playerPosition.x - mousePosition.x;	//x vector between origin (static) and mouse position
		float vy = playerPosition.y - mousePosition.y;	//y vector between origin (static) and mouse position

		float mag = Mathf.Sqrt(vx*vx + vy*vy);	//length between origin and mouse position

		if(playerPosition!=mousePosition){	//guards against divide by 0
			vx = vx/mag;	//normalise
			vy = vy/mag;	//normalise
		}
			
		float distance = 1.5f;		//distance to travel along vector from player position
			
		float px = (mousePosition.x + (vx * (mag - distance)));		// x value displacement from center of origin or player
		float py = (mousePosition.y + (vy * (mag - distance)));		// y value displacement from center of origin or player

			
		Vector3 newPosition = new Vector3(px, py, -3);		//add x and y displacements to player position to get crosshair position
			
		transform.position = newPosition;

		
		//Rotate the crosshair
		Vector3 vectorToTarget = playerPosition - transform.position;
		angle = (Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg) + 90;
		q = Quaternion.AngleAxis(angle, Vector3.forward);
		transform.rotation = Quaternion.RotateTowards(transform.rotation, q, Time.deltaTime * 10000);










//		Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
//		Vector3 playerPosition = transform.parent.position;
//		Vector3 OriginPosition = new Vector3(0,0,0);
//
//		float vx = OriginPosition.x - mousePosition.x;	//x vector between origin (static) and mouse position
//		float vy = OriginPosition.y - mousePosition.y;	//y vector between origin (static) and mouse position
//		
//		float mag = Mathf.Sqrt(vx*vx + vy*vy);	//length between origin and mouse position
//		
//		vx /= mag;	//normalise
//		vy /= mag;	//normalise
//		
//		float distance = 1.5f;		//distance to travel along vector from player position
//		
//		float px = (mousePosition.x + (vx * (mag - distance)));		// x value displacement from center of origin or player
//		float py = (mousePosition.y + (vy * (mag - distance)));		// y value displacement from center of origin or player
//		
//		Vector3 newPosition = new Vector3(playerPosition.x+px, playerPosition.y+py, -3);		//add x and y displacements to player position to get crosshair position
//
//		transform.position = newPosition;
//
//		//Rotate the crosshair
//		Vector3 vectorToTarget = playerPosition - transform.position;
//		angle = (Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg) + 90;
//		q = Quaternion.AngleAxis(angle, Vector3.forward);
//		transform.rotation = Quaternion.RotateTowards(transform.rotation, q, Time.deltaTime * 10000);
	}
}
