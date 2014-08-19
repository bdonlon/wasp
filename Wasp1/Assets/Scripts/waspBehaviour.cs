using UnityEngine;
using System.Collections;

public class waspBehaviour : MonoBehaviour {

	public bool dead = false;
	private bool encounteredPlayer = false;
	private bool encounteredSpouse = false;
	private Vector3 playerLocation;
	private Vector3 foodLocation;
	private Vector3 spouseLocation;
	private Vector3 targetLocation;

	public string target = "food";

	private Vector2 travelDirection;
	public Vector3 spawnLocation;
	private float xDiff,yDiff;
	private float xDiffPos,yDiffPos;
	public float speed = 20;
	private bool up,down,left,right = false;
	public bool eating = false;
	public bool attacking = false;
	public int destroyDelay = 7;
	public int stingDamage = 10;
	public int health = 100;
	public GameObject hitSound;
	public GameObject killSound;

	public BoxCollider2D swatTrigger;
	public BoxCollider2D passthrough;

	public GameObject _GM;
	public GameObject player;
	public GameObject food;
	public GameObject spouse;

	Animator anim;
	Animator shadowAnim;
	public GameObject shadow;

	void Start () {
		speed = 40;
		target = "food";
		left = true;
		//set up wasp colliders
		swatTrigger.size = new Vector2(0.71f,0.44f);
		swatTrigger.transform.localPosition = Vector2.zero;

		passthrough.size = new Vector2(0.15f,0.15f);
		passthrough.transform.localPosition = Vector2.zero;

		//Set was Z level above player
		transform.position = new Vector3(transform.position.x,transform.position.y,-2);
		spawnLocation = transform.position;

		player = GameObject.Find("player");
		_GM = GameObject.Find("_GM");
		food = GameObject.Find("picnic_food");
		spouse = GameObject.Find("picnic_spouse");

		anim = GetComponent<Animator>();
		shadowAnim = shadow.GetComponent<Animator>();
	}
	
	void Update () {
		playerLocation= player.transform.position;
		foodLocation= food.transform.position;
		spouseLocation= spouse.transform.position;
	
		if(!encounteredPlayer && distanceBetween(playerLocation,transform.position)<2.5f){
			int rand = Random.Range(1,100);
			if(rand>50){
				changeTarget("player");
			}
			encounteredPlayer=true;
		}

		if(!encounteredSpouse && distanceBetween(spouseLocation,transform.position)<1.9f){
			int rand = Random.Range(1,100);
			if(rand>75){
				changeTarget("spouse");
			}
			encounteredSpouse=true;
		}

		if(target.Equals("food")){
			travel(transform.position,foodLocation);
		}else if(target.Equals("spouse")){
			travel(transform.position,spouseLocation);
		}else if(target.Equals("player")){
			if(!player.GetComponent<playerMovement>().isDead())
			{
				//player is ALIVE
				travel(transform.position,playerLocation);
			}
			else
			{
				//player is DEAD
				speed = 7.5f;
				travel(transform.position,spawnLocation);
			}
		}
	}

	public float distanceBetween(Vector3 pos1, Vector3 pos2){

		float vx = pos1.x - pos2.x;	//x vector between origin (static) and mouse position
		float vy = pos1.y - pos2.y;	//y vector between origin (static) and mouse position
		
		float mag = Mathf.Sqrt(vx*vx + vy*vy);	//length between origin and mouse position

		return mag;
	}

	public string getTarget(){
		return target;
	}

	void changeTarget(string newTarget){
		stopEatingFood();
		stopAttacking();

		target = newTarget;
	}

	public IEnumerator attack(){
		attacking = true;

		while(attacking)
		{
			anim.SetTrigger("wasp_attacking_start");
			if(target.Equals("player")){
				player.GetComponent<playerMovement>().injure(stingDamage);
			}else if (target.Equals("spouse")){
				spouse.GetComponent<spouse_behavior>().injure(stingDamage);
			}else if (target.Equals("food")){
				//eating animation

			}
			yield return new WaitForSeconds(0.2f);
			anim.SetTrigger("wasp_attacking_end");

			yield return new WaitForSeconds(1.2f);
		}
	}

	public IEnumerator eatFood(){
		eating=true;
		anim.SetTrigger("wasp_eating_start");
		while(eating)
		{
			food.GetComponent<food_behavior>().injure(1);
			yield return new WaitForSeconds(0.5f);
		}
		anim.SetTrigger("wasp_eating_end");

	}

	public void stopEatingFood(){
		eating = false;
		anim.SetTrigger("wasp_eating_end");
	}

	public void stopAttacking(){
		attacking = false;
		//anim.SetTrigger("wasp_attacking_end");
	}

	public void giveScore(){
		player.GetComponent<playerMovement>().score++;
	}

	public void flip(){
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	private void travel(Vector2 from, Vector2 to){
		if(dead){
			//do nothing
		}else{
			xDiff = to.x - from.x;
			yDiff = to.y - from.y;
			
			xDiffPos=Mathf.Abs(xDiff);
			yDiffPos=Mathf.Abs(yDiff);
			
			if((to.x < from.x)){
				if(right){flip();}
				left=true;
				right=false;
			}
			if((to.x > from.x)){
				if(left){flip();}
				right=true;
				left=false;
			}
			if((to.x < from.x)){
				down=true;
				up=false;
			}
			if((to.x > from.x)){
				up=true;
				down=false;
			}
			
			int jitter = Random.Range(-5,5);
			
			if((left||right)&&(yDiffPos<1)){
				up=false;
				down=false;
			}
			
			
			if((up||down)&&(yDiffPos>1.5)){
				xDiff = xDiff+jitter;
			}
			if((left||right)&&(xDiffPos>1.5)){
				yDiff = yDiff+jitter;
			}
			travelDirection = new Vector2(xDiff,yDiff);
			rigidbody2D.AddForce(travelDirection.normalized * speed);
			
			//xDiff = spawnLocation.x - transform.position.x;
			//yDiff = spawnLocation.y - transform.position.y;
			
			//xDiffPos=Mathf.Abs(xDiff);
			//yDiffPos=Mathf.Abs(yDiff);
			
			//rigidbody2D.AddForce(travelDirection.normalized * speed);
		}
	}

	public bool isDead(){
		return dead;
	}

	public void takeDamage(int damage){
		knockback();
		changeTarget("player");
		health = health - damage;
		if (health <= 0) {
			killSound.GetComponent<playSound>().play();
			kill();
		} else {
			hitSound.GetComponent<playSound>().play();
		}
	}

	private void knockback(){
		rigidbody2D.AddForce(-travelDirection.normalized * speed * 35);
	}
	
	public void kill(){
		dead=true;
		giveScore();
		anim.SetTrigger("wasp_death");
		shadowAnim.SetTrigger ("wasp_death");
		transform.position = new Vector3(transform.position.x,transform.position.y-0.3f,-0.5f);
		shadow.transform.position = new Vector3(shadow.transform.position.x,shadow.transform.position.y+0.2f,shadow.transform.position.z);
		shadow.transform.localScale = new Vector3(shadow.transform.localScale.x+0.15f,shadow.transform.localScale.y+0.15f,shadow.transform.localScale.z);
		destroyWasp();
		_GM.GetComponent<Setup>().numWasps--;
		//_GM.GetComponent<Setup>().maxWasps++;
	}

	public void destroyWasp(){
		Destroy(transform.Find("passthrough").gameObject);
		//Destroy(transform.Find("waspSwatTrigger").gameObject);	//Causing game crash
		Destroy(this.gameObject,Random.Range(destroyDelay-1,destroyDelay+1));
	}

}