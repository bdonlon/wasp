using UnityEngine;
using System.Collections;

public class food_behavior : MonoBehaviour {

		public bool dead = false;
		
		public GameObject _GM;
		
		public GameObject hitSound;
		public GameObject deathSound;

		public GameObject picnic;
		
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
}
