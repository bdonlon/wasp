using UnityEngine;
using System.Collections;

public class waspPassthrough : MonoBehaviour {

	public GameObject waspParent;
	
	void OnCollisionEnter2D(Collision2D col)
	{
		
		if(waspParent.GetComponent<waspBehaviour>().isDead()){
			//do nothing
		}else if(col.gameObject.tag == "player"){

			//subtract health
			StartCoroutine(waspParent.GetComponent<waspBehaviour>().attack());
			//waspParent.GetComponent<waspBehaviour>().attack();
		}
	}
}
