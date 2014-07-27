using UnityEngine;
using System.Collections;

public class waspSwatTrigger : MonoBehaviour {

	public GameObject waspParent;
	public GameObject hitSound;
	public GameObject killSound;

	void OnTriggerEnter2D(Collider2D col)
	{

		if(waspParent.GetComponent<waspBehaviour>().isDead()){
			//do nothing
		}else if(col.gameObject.tag == "swatter"){
			//subtract health
			waspParent.GetComponent<waspBehaviour>().takeDamage(50);
			hitSound.GetComponent<playSound>().play();
			//then if health <=0
				//gameObject.SendMessageUpwards("kill");
				//Destroy(this.gameObject,0);
		}
	}
}
