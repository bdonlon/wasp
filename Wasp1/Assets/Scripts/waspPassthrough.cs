using UnityEngine;
using System.Collections;

public class waspPassthrough : MonoBehaviour {

	public GameObject waspParent;
	string colliderString;
	
	void OnCollisionEnter2D(Collision2D col)
	{
		colliderString = col.gameObject.tag;

		if(waspParent.GetComponent<waspBehaviour>().isDead()){
			//do nothing
		}else if(colliderString.Equals(waspParent.GetComponent<waspBehaviour>().getTarget())){

			if(colliderString.Equals("food")){
				//eat food
				StartCoroutine(waspParent.GetComponent<waspBehaviour>().eatFood());
			}else{
				//sting
				StartCoroutine(waspParent.GetComponent<waspBehaviour>().attack());
			}
			//waspParent.GetComponent<waspBehaviour>().attack();
		}
	}

	void OnCollisionExit2D(Collision2D col) {
//		Debug.Log("No longer in contact with " + col.transform.name);

		colliderString = col.gameObject.tag;
		
		if(waspParent.GetComponent<waspBehaviour>().isDead()){
			//do nothing
		}else if(colliderString.Equals(waspParent.GetComponent<waspBehaviour>().getTarget())){
			
			if(colliderString.Equals("food")){
				//stop eating food
				waspParent.GetComponent<waspBehaviour>().stopEatingFood();
			}
			else{
				//sting
				waspParent.GetComponent<waspBehaviour>().stopAttacking();
			}
			//waspParent.GetComponent<waspBehaviour>().attack();
		}
	}
}
