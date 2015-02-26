using UnityEngine;
using System.Collections;

public class food_behavior : MonoBehaviour {

	public bool dead = false;
	
	public GameObject _GM;
	
	public GameObject hitSound;
	public GameObject deathSound;
	public GameObject player;
	public GameObject picnic;

	public void Update(){
		if(_GM.GetComponent<Setup>().foodAvailable){
			//Onscreen indication that food is available
		}
	}
	
	public void injure(int damage){
			picnic.GetComponent<picnic_health_script>().damagePicnicIntegrity(damage);
			hitSound.GetComponent<playSound>().play();
	}
	
	public void kill(){
		dead=true;
		Destroy(GetComponent<BoxCollider2D>());
	}
	
	public bool isDead(){
		return dead;
	}

	public void healPlayer(){
		int healValue=40;
		player.GetComponent<playerMovement>().heal(healValue);
	}
}
