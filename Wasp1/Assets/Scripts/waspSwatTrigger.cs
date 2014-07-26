﻿using UnityEngine;
using System.Collections;

public class waspSwatTrigger : MonoBehaviour {

	public GameObject waspParent;

	void OnTriggerEnter2D(Collider2D col)
	{

		if(waspParent.GetComponent<waspBehaviour>().isDead()){
			//do nothing
		}else if(col.gameObject.tag == "swatter"){
			//subtract health

			//then if health <=0
				gameObject.SendMessageUpwards("kill");
				Destroy(this.gameObject,0);
		}
	}
}
