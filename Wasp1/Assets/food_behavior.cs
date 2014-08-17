using UnityEngine;
using System.Collections;

public class food_behavior : MonoBehaviour {
	
		public int health = 200;
		public bool dead = false;
		
		public GameObject _GM;
		
		public GameObject hitSound;
		public GameObject deathSound;
		
		public void injure(int damage){
			if(!dead){	//may still recieve injure instructions after death
				health = health-damage;
				hitSound.GetComponent<playSound>().play();
				if(health<=0){
					deathSound.GetComponent<playSound>().play();
					kill();
					_GM.GetComponent<Setup>().failureCondition=true;
					Screen.showCursor = true;
				}
			}
		}
		
		public void kill(){
			dead=true;
			Destroy(GetComponent<BoxCollider2D>());
			Vector3 theScale;
			theScale = transform.localScale;
			theScale.y *= -1;
			transform.localScale=theScale;
		}
		
		public bool isDead(){
			return dead;
		}

}
