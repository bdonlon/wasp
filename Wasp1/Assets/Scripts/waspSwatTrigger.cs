using UnityEngine;
using System.Collections;

public class waspSwatTrigger : MonoBehaviour {

	public GameObject waspParent;
	public GameObject swatter;

	void Start(){
		swatter = GameObject.Find("swatter");
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if(waspParent.GetComponent<waspBehaviour>().isDead()){
			//do nothing
		}else if(col.gameObject.tag == "swatter"){
			//play swatter slap sound
			swatter.GetComponent<swatter_script>().playSwatterHitSound();

			//subtract health
			waspParent.GetComponent<waspBehaviour>().takeDamage(50);
		}
	}
}
