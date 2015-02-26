using UnityEngine;
using System.Collections;

public class script_food_consumable_collider : MonoBehaviour {

	public GameObject _GM;
	public GameObject foodParent;
	string colliderString;
	
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		colliderString = col.gameObject.tag;
		if(colliderString.Equals("player_feet") && _GM.GetComponent<Setup>().foodAvailable){
				//eat food
			eatenByPlayer();
		}
	}

	public void eatenByPlayer(){
		foodParent.GetComponent<food_behavior>().healPlayer();
		_GM.GetComponent<Setup>().foodConsumed();
	}
}
