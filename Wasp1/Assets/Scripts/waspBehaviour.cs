using UnityEngine;
using System.Collections;

public class waspBehaviour : MonoBehaviour {

	public bool dead = false;
	private bool encounteredPlayer = false;
	private bool encounteredSpouse = false;
	private Vector3 playerLocation;
	public Vector3 foodLocation;
	public Vector2 foodOffset;
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
	public GameObject flySound;

	public BoxCollider2D swatTrigger;
	public BoxCollider2D passthrough;

	public GameObject _GM;
	public GameObject player;
	public GameObject food;
	public GameObject spouse;

	Animator anim;
	Animator shadowAnim;
	public GameObject shadow;
	public SpriteRenderer spriteRenderer;

	void Start () {
		speed = 40;
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

		flySound.GetComponent<playSound>().playLooped();

		foodOffset.x=0;
		foodOffset.y=0;
		changeTarget("food");
		//target="food";
	}
	
	void Update () {

		if(_GM.GetComponent<Setup>().pauseGame){
			//Game paused - Do nothing!
		}else{
			if(dead){
				spriteRenderer.sortingOrder = (int)Camera.main.WorldToScreenPoint (spriteRenderer.bounds.min).y * -1;
			}else{
				playerLocation= player.transform.position;
				foodLocation= new Vector3(food.transform.position.x+foodOffset.x,food.transform.position.y+foodOffset.y,food.transform.position.z);
				spouseLocation= spouse.transform.position;
			
				if(!_GM.GetComponent<Setup>().failureCondition){	//failure condition not met
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
					}
				}else{	//failure condition met
					if(target.Equals("food")){
						travel(transform.position,foodLocation);
					}else if(target.Equals("spouse")){	//if failure condition true, ROLL and change target from spouse to food (50%) or return to spawn (50%)
						int rand = Random.Range(1,100);
						if(rand>50){
							changeTarget("food");

						}else{
							speed = 7.5f;
							changeTarget ("spawn");
						}
					}else if(target.Equals ("spawn")){
						travel(transform.position,spawnLocation);
					}else if(target.Equals("player")){
						if(!player.GetComponent<playerMovement>().isDead()) //if failure condition true, but player is still alive (picnic ruined), and current target is player, continue targeting player
						{
							//player is ALIVE
							travel(transform.position,playerLocation);
						}
						else //if failure condition true, current target is player, and player is dead - ROLL and change target from player to food (50%) or return to spawn (50%)
						{
							int rand = Random.Range(1,100);
							if(rand>50){
								changeTarget("food");
							}else{
								speed = 7.5f;
								changeTarget("spawn");
							}
						}
					}
				}
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

	public void setFoodOffset(){
		// Create an food location offset per wasp to pervent them gathering on the same xy coordinate (as wasps do not collide with eachother or the food itself)
		float rand = Random.Range(-0.2f,0.2f);
		foodOffset.x=rand;
		//rand = Random.Range(-0.2f,0.2f);
		//foodOffset.y=rand;
	}

	void changeTarget(string newTarget){
		if(newTarget.Equals("food")){
			setFoodOffset();
		}
		if(eating){		stopEatingFood();}
		if(attacking){	stopAttacking();}
		target = newTarget;
		adjustColliders();
	}

	void adjustColliders(){
		//reset all colliders
		Physics2D.IgnoreCollision(passthrough,food.collider2D,false);
		Physics2D.IgnoreCollision(passthrough,spouse.collider2D,false);
		Physics2D.IgnoreCollision(passthrough,player.collider2D,false);

		//then disable colliders depending on current target
		if(getTarget().Equals("player")){
			gameObject.layer = 12;
			Physics2D.IgnoreCollision(passthrough,food.collider2D);	//Targeting player, therefore ignore collisions with food
			Physics2D.IgnoreCollision(passthrough,spouse.collider2D);	//Targeting player, therefore ignore collisions with spouse
		}
		else if(getTarget().Equals("spouse")){
			Physics2D.IgnoreCollision(passthrough,food.collider2D);
			Physics2D.IgnoreCollision(passthrough,player.collider2D);
			gameObject.layer = 14;
		}
		else if(getTarget().Equals("food")){
			gameObject.layer = 14;
			Physics2D.IgnoreCollision(passthrough,player.collider2D);
			Physics2D.IgnoreCollision(passthrough,spouse.collider2D);
		}
		else if(getTarget().Equals("spawn")){
			gameObject.layer = 14;
			Physics2D.IgnoreCollision(passthrough,food.collider2D);
			Physics2D.IgnoreCollision(passthrough,spouse.collider2D);
			Physics2D.IgnoreCollision(passthrough,player.collider2D);
		}
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
		flySound.GetComponent<playSound>().stop();
		eating=true;
		anim.SetTrigger("wasp_eating_start");
		while(eating)
		{
			food.GetComponent<food_behavior>().injure(1);
			yield return new WaitForSeconds(0.5f);
		}
	}

	public void stopEatingFood(){
		eating = false;
		anim.SetTrigger("wasp_eating_end");
		flySound.GetComponent<playSound>().playLooped();
	}

	public void stopAttacking(){
		attacking = false;
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
		//Knock wasp away from player
		xDiff = transform.position.x - playerLocation.x;
		yDiff = transform.position.y - playerLocation.y;
		travelDirection = new Vector2(xDiff,yDiff);
		rigidbody2D.AddForce(travelDirection.normalized * speed * 35);
	}
	
	public void kill(){
		flySound.GetComponent<playSound>().stop();
		dead=true;
		giveScore();
		anim.SetTrigger("wasp_death");
		shadowAnim.SetTrigger ("wasp_death");
		transform.position = new Vector3(transform.position.x,transform.position.y-0.3f,-0.5f);
		shadow.transform.position = new Vector3(shadow.transform.position.x,shadow.transform.position.y+0.2f,shadow.transform.position.z);
		shadow.transform.localScale = new Vector3(shadow.transform.localScale.x+0.15f,shadow.transform.localScale.y+0.15f,shadow.transform.localScale.z);
		destroyWasp();
		_GM.GetComponent<Setup>().numWasps--;
	}

	public void destroyWasp(){
		Destroy(transform.Find("passthrough").gameObject);
		Destroy(this.gameObject,Random.Range(destroyDelay-1,destroyDelay+1));
	}

}