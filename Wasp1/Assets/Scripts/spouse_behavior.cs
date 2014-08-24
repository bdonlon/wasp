using UnityEngine;
using System.Collections;

public class spouse_behavior : MonoBehaviour {
	
	public bool dead = false;

	public GameObject _GM;

	public GameObject hitSound;
	public GameObject deathSound;

	public GameObject picnic;


	Animator anim;

	void Start(){
		anim = GetComponent<Animator>();
	}

	public void injure(int damage)
	{
			anim.SetTrigger("spouse_hurt_start");
			picnic.GetComponent<picnic_health_script>().damagePicnicIntegrity(damage);
			hitSound.GetComponent<playSound>().play();
			//anim.SetTrigger("spouse_hurt_end");
//			if(health<=0){
//				deathSound.GetComponent<playSound>().play();
//				_GM.GetComponent<Setup>().failureCondition=true;
//				Screen.showCursor = true;
//			}
	}

	public void kill(){
		dead=true;
		Destroy(GetComponent<BoxCollider2D>());
//		Vector3 theScale;
//		theScale = transform.localScale;
//		theScale.y *= -1;
//		transform.localScale=theScale;
	}
	
	public bool isDead(){
		return dead;
	}

	public void cry(){
		anim.SetTrigger("spouse_cry_start");
	}

//	public int getHealth(){
//		return health;
//	}
}
