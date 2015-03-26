using UnityEngine;
using System.Collections;

public class spouse_behavior : MonoBehaviour {
	
	public bool dead = false;

	public GameObject _GM;

	public GameObject hitSound;
	public GameObject deathSound;
	public RuntimeAnimatorController[] AnimationControllers;

	public GameObject picnic;
	private bool invulPeriod;


	Animator anim;

	void Start(){
		anim = GetComponent<Animator>();
		if(!ApplicationModel.equalityMode){
			anim.runtimeAnimatorController=AnimationControllers[0];
		}else if(ApplicationModel.equalityMode){
			anim.runtimeAnimatorController=AnimationControllers[1];
		}

		//Weird hack. This shouldn't be required, but when I switch the animation
		//controller from it's default (female), the basket animation breaks
		//unless I trigger it here, then it works fine. No idea why.
		anim.SetTrigger("spouse_basket_start");
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
