﻿using UnityEngine;
using System.Collections;

public class spouse_behavior : MonoBehaviour {
	
	public bool dead = false;

	public GameObject _GM;

	public GameObject hitSound;
	public GameObject deathSound;

	public GameObject picnic;

	public void injure(int damage)
	{
			picnic.GetComponent<picnic_health_script>().damagePicnicIntegrity(damage);
			hitSound.GetComponent<playSound>().play();
//			if(health<=0){
//				deathSound.GetComponent<playSound>().play();
//				_GM.GetComponent<Setup>().failureCondition=true;
//				Screen.showCursor = true;
//			}
	}

	public void kill(){
		dead=true;
		Destroy(GetComponent<BoxCollider2D>());
//		Vector3 theScale;
//		theScale = transform.localScale;
//		theScale.y *= -1;
//		transform.localScale=theScale;
	}
	
	public bool isDead(){
		return dead;
	}

//	public int getHealth(){
//		return health;
//	}
}
