using UnityEngine;
using System.Collections;

public class food_behavior : MonoBehaviour {

	public bool dead = false;
	
	public GameObject _GM;
	
	public GameObject hitSound;
	public GameObject deathSound;
	public GameObject player;
	public GameObject picnic;

	public int timer;
	public float scaleRate;

	public Vector3 defaultSize;

	void Start () {
		defaultSize = transform.localScale;
		timer = 0;
		scaleRate = 0.02f;
	}

	public void Update()
	{
		if(_GM.GetComponent<Setup>().foodAvailable)	//Make the food grow/shrink to indicate that it can be interacted with
		{
			if(timer>20){
				scaleRate = scaleRate*-1;
				timer=0;
			}
			transform.localScale = transform.localScale + (Vector3.one * scaleRate);
			timer++;
		}else{
			transform.localScale = defaultSize;
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
		player.GetComponent<playerMovement>().heal(_GM.GetComponent<Setup>().getPlayerHealValue());
	}
}
