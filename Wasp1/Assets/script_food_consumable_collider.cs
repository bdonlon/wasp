using UnityEngine;
using System.Collections;

public class script_food_consumable_collider : MonoBehaviour {

	public GameObject _GM;
	string colliderString;
	
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		colliderString = col.gameObject.tag;

		if(colliderString.Equals("player_feet")){
				//eat food
			eatenByPlayer();
		}
	}

	public void eatenByPlayer(){
		_GM.GetComponent<Setup>().foodConsumed();
	}
}
