using UnityEngine;
using System.Collections;

public class playerMovement : MonoBehaviour {
		
	private float speed = 10;
	private Vector3 velocity;
	public int health = 200;
	public int score = 0;
	public GameObject _GM;

	public bool up,down,left,right = false;
	public bool upPrev,downPrev,leftPrev,rightPrev = false;

	public GameObject hitSound;
	public GameObject deathSound;

	public SpriteRenderer spriteRenderer;
	public SpriteRenderer swatterSpriteRenderer;

	public bool dead = false;
	public bool moving;
	public bool movingPrevious;

	Animator anim;

	void Start(){
		moving = false;
		movingPrevious=false;
		anim = GetComponent<Animator>();
	}

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

	public int getScore(){
		return score;
	}

	public void flip(){
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	public void setAnimationTrigger(){

		if(left&&right){
			//moving=false;
		}else{

			if(left){	anim.SetTrigger("running_left");	}

			if(right){	anim.SetTrigger("running_right");	}
		}

		if(!up && !down && !left && !right){	anim.SetTrigger("stop_running");	}

		if(movingPrevious && !moving){	anim.SetTrigger("stop_running");	}

	}

//	public void storePreviousDirection(){
//		upPrev = up;
//		downPrev = down;
//		leftPrev = left;
//		rightPrev = right;
//	}
	
	void Update () {
		movingPrevious=moving;
		
		spriteRenderer.sortingOrder = (int)Camera.main.WorldToScreenPoint (spriteRenderer.bounds.min).y * -1;
		swatterSpriteRenderer.sortingOrder = spriteRenderer.sortingOrder;
		
		if(!dead){
			// not dead, therefore capture keystrokes and move
			
			if(Input.GetKey(KeyCode.W) || Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKeyUp(KeyCode.S) || Input.GetKey(KeyCode.D) || Input.GetKeyUp(KeyCode.D))
			{
				//storePreviousDirection ();
				moving=true;
				
				if(
					(Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A)) ||
					(Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D)) ||
					(Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A)) ||
					(Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D)))
				{
					speed=8;
				}else{
					speed=10;
				}
				if(Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.S)){
					moving=false;
					if (Input.GetKey(KeyCode.A)){
						left=true;
						moving=true;
						//Debug.Log("W&S&A");
						velocity.x=0;
						velocity.y=0;
						velocity.x=speed;
						rigidbody2D.velocity=velocity;
					}
					if (Input.GetKey(KeyCode.D)){
						right=true;
						moving=true;
						//Debug.Log("W&S&D");
						velocity.x=0;
						velocity.y=0;
						velocity.x=speed*-1;
						rigidbody2D.velocity=velocity;
					}
				}
				else{
					if(Input.GetKey(KeyCode.W)){
						up=true;
						velocity.y = speed;
						rigidbody2D.velocity=velocity;
					}
					if(Input.GetKeyUp(KeyCode.W)){
						up=false;
						velocity.y = 0;
						rigidbody2D.AddForce(Vector2.up * speed * 10);
					}
					
					if (Input.GetKey(KeyCode.S)){
						down=true;
						velocity.y=speed*-1;
						rigidbody2D.velocity=velocity;
					}
					if(Input.GetKeyUp(KeyCode.S)){
						down=false;
						velocity.y = 0;
						rigidbody2D.AddForce(-Vector2.up * speed * 10);
					}
				}
				
				if(Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.A)){
					moving=false;
					left=true;
					right=true;
					if (Input.GetKey(KeyCode.W)){
						up=true;
						moving=true;
						//Debug.Log("A&D&W");
						velocity.x=0;
						velocity.y=0;
						velocity.y=speed;
						rigidbody2D.velocity=velocity;
					}
					if (Input.GetKey(KeyCode.S)){
						down=true;
						moving=true;
						//Debug.Log("A&D&W");
						velocity.x=0;
						velocity.y=0;
						velocity.y=speed*-1;
						rigidbody2D.velocity=velocity;
					}
				}
				else{
					if (Input.GetKey(KeyCode.D)){
						right=true;
						velocity.x=speed;
						rigidbody2D.velocity=velocity;
					}
					if(Input.GetKeyUp(KeyCode.D)){
						right=false;
						velocity.x=0;
						rigidbody2D.AddForce(Vector2.right * speed * 10);
					}
					
					if (Input.GetKey(KeyCode.A)){
						left=true;
						velocity.x=speed*-1;
						rigidbody2D.velocity=velocity;
					}
					if(Input.GetKeyUp(KeyCode.A)){
						left=false;
						velocity.x=0;
						rigidbody2D.AddForce(-Vector2.right * speed * 10);
					}
				}
				setAnimationTrigger();
			}else{
				moving=false;
				if(movingPrevious && !moving){
					setAnimationTrigger();
				}
			}

		}else{
			//player dead, no movement
		}
	}
}