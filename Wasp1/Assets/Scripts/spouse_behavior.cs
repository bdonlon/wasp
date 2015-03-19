using UnityEngine;
using System.Collections;

public class spouse_behavior : MonoBehaviour {
	
	public bool dead = false;

	public GameObject _GM;

	public GameObject hitSound;
	public GameObject deathSound;

	public GameObject picnic;
	private bool invulPeriod;


	Animator anim;

	void Start(){
		anim = GetComponent<Animator>();
	}

	public void injure(int damage)
	{
		if(!invulPeriod){
			anim.SetTrigger("spouse_hurt_start");
			picnic.GetComponent<picnic_health_script>().damagePicnicIntegrity(damage);
			StartCoroutine(setInvulnerable());
			hitSound.GetComponent<playSound>().play();
		}
	}

	private IEnumerator setInvulnerable(){
		invulPeriod=true;
		yield return new WaitForSeconds(0.2f);
		invulPeriod=false;
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
