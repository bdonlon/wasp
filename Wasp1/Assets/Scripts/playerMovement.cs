using UnityEngine;
using System.Collections;

public class playerMovement : MonoBehaviour {
		
	private float speed = 10;
	//private bool movingUp,movingDown,movingLeft,movingRight = false;
	private Vector3 velocity;
	public int health = 200;
	public int score = 0;
	public GameObject _GM;

	public bool dead = false;

	public void injure(int damage){
		if(!dead){	//may still recieve injure instructions after death
			health = health-damage;
			if(health<=0){
				kill();
				_GM.GetComponent<Setup>().failureCondition=true;
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
	
	void Update () {

		if(!dead){
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


				if (Input.GetKey(KeyCode.A)){
					//Debug.Log("W&S&A");
					velocity.x=0;
					velocity.y=0;
					velocity.x=speed;
					rigidbody2D.velocity=velocity;
				}
				if (Input.GetKey(KeyCode.D)){
					//Debug.Log("W&S&D");
					velocity.x=0;
					velocity.y=0;
					velocity.x=speed*-1;
					rigidbody2D.velocity=velocity;
				}
			}
			else{
				if(Input.GetKey(KeyCode.W)){
					velocity.y = speed;
					rigidbody2D.velocity=velocity;
				}
				if(Input.GetKeyUp(KeyCode.W)){
					velocity.y = 0;
					rigidbody2D.AddForce(Vector2.up * speed * 10);
				}
				
				if (Input.GetKey(KeyCode.S)){
					velocity.y=speed*-1;
					rigidbody2D.velocity=velocity;
				}
				if(Input.GetKeyUp(KeyCode.S)){
					velocity.y = 0;
					rigidbody2D.AddForce(-Vector2.up * speed * 10);
				}
			}

			if(Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.A)){
				//Debug.Log("A&D");
				if (Input.GetKey(KeyCode.W)){
					//Debug.Log("A&D&W");
					velocity.x=0;
					velocity.y=0;
					velocity.y=speed;
					rigidbody2D.velocity=velocity;
				}
				if (Input.GetKey(KeyCode.S)){
					//Debug.Log("A&D&W");
					velocity.x=0;
					velocity.y=0;
					velocity.y=speed*-1;
					rigidbody2D.velocity=velocity;
				}
			}
			else{
				if (Input.GetKey(KeyCode.D)){
					velocity.x=speed;
					rigidbody2D.velocity=velocity;
				}
				if(Input.GetKeyUp(KeyCode.D)){
					velocity.x=0;
					rigidbody2D.AddForce(Vector2.right * speed * 10);
				}
				
				if (Input.GetKey(KeyCode.A)){
					velocity.x=speed*-1;
					rigidbody2D.velocity=velocity;
				}
				if(Input.GetKeyUp(KeyCode.A)){
					velocity.x=0;
					rigidbody2D.AddForce(-Vector2.right * speed * 10);
				}
			}
		}else{
			//player dead, no movement
		}
	}
}