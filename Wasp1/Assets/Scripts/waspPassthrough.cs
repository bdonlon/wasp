using UnityEngine;
using System.Collections;

public class waspPassthrough : MonoBehaviour {

	public GameObject waspParent;
	
	void OnCollisionEnter2D(Collision2D col)
	{
		Debug.Log(col.gameObject.tag);
		if(waspParent.GetComponent<waspBehaviour>().isDead()){
			//do nothing
		}else if(col.gameObject.tag.Equals(waspParent.GetComponent<waspBehaviour>().getTarget())){

			//subtract health
			StartCoroutine(waspParent.GetComponent<waspBehaviour>().attack());
			//waspParent.GetComponent<waspBehaviour>().attack();
		}
	}
}
