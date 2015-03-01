using UnityEngine;
using System.Collections;

public class script_grass_rustle : MonoBehaviour {

	Animator anim;

	void Start()
	{
		anim = GetComponent<Animator>();
	}

	void OnTriggerEnter2D(Collider2D col) {

		if(col.gameObject.tag=="player_feet"){
			anim.SetTrigger("rustle");
		}
	}
}
