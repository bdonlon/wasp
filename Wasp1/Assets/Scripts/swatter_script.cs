using UnityEngine;
using System.Collections;

public class swatter_script : MonoBehaviour {

	public GameObject crosshair;
	public bool idle;

	public float wobble;
	public int timer;
	public float defaultX;
	public float defaultY;
	public float defaultZRot;
	public Vector3 rotatePoint;
	public GameObject player;
	public bool setStartSwingPositionOnce;
	public bool setQuatOnce = true;
	public bool interrupt=false;
	public Quaternion quat;

	private float swingDuration = 0.15f;
	private float swingSpeed = 10;
	
	private float swingTimer = 0f;
	private bool swinging = false;
	private bool swungOnce=false;

	private Vector3 startRot;

	private Vector3 startSwingPosition;

	// Use this for initialization
	void Start () {
		wobble = 0.2f;
		timer = 0;
		idle = true;
		setStartSwingPositionOnce = true;

		defaultX = Mathf.Abs(transform.parent.position.x - transform.position.x);
		defaultY = Mathf.Abs(transform.parent.position.y - transform.position.y);
		defaultZRot = gameObject.transform.rotation.eulerAngles.z;

		startRot = transform.eulerAngles;
	}
	
	// Update is called once per frame
	void Update () {

		//weapon idle wobble
		if(idle){
			if(timer>20){
				wobble = wobble*-1;
				timer=0;
			}

			//transform.Rotate(0,0,wobble);
			//transform.RotateAround (Vector3.zero, Vector3.up, wobble);
			Vector3 rotatePoint = new Vector3(transform.parent.position.x+defaultX-0.45f,transform.parent.position.y+defaultY-0.2f,0);
			transform.RotateAround(rotatePoint, Vector3.back, wobble);
			timer++;
		}else if (swinging){
			gameObject.GetComponent<BoxCollider2D>().enabled = true;
			swingTimer += Time.deltaTime;
			if(setStartSwingPositionOnce){
				setStartSwingPositionOnce=false;
				transform.position = startSwingPosition;	//moves to crosshair position
				if(setQuatOnce){
					setQuatOnce=false;
					quat = Quaternion.AngleAxis(crosshair.GetComponent<crosshair>().angle+90, Vector3.forward);
				}
				transform.rotation = Quaternion.RotateTowards(transform.rotation, quat, Time.deltaTime * 10000);
			}
			rotatePoint = new Vector3(player.transform.position.x,player.transform.position.y,0);
			transform.RotateAround(rotatePoint, Vector3.back, swingSpeed);

			if (swingTimer > swingDuration){
				swungOnce=true;
			}

			if(swingTimer>swingDuration){
				setStartSwingPositionOnce = true;
				swingTimer = 0f;
			}
			
			if (swungOnce && interrupt) {
				//reset swatter to idle position
				swingTimer = 0f;
				swinging = false;
				setStartSwingPositionOnce = true;
				swungOnce=false;
				
				transform.position = new Vector3(transform.parent.position.x+defaultX,transform.parent.position.y+defaultY,transform.position.z);
				gameObject.GetComponent<BoxCollider2D>().enabled = false;
				transform.rotation = Quaternion.Euler(0,0,defaultZRot);
				idle=true;
			}
		}
	}

	public void keyUp(){
		interrupt=true;
	}

	public void swingKey(string Key){

		if(Key.Equals("up")){
			interrupt=false;
			idle=false;
			swinging=true;
			startSwingPosition = new Vector3(player.transform.position.x-0.86f,player.transform.position.y+0.86f,0);
			setQuatOnce=true;
			swungOnce=false;
			//setStartSwingPositionOnce = true;

		}
		if(Key.Equals("down")){
			interrupt=false;
			idle=false;
			swinging=true;
			startSwingPosition = new Vector3(player.transform.position.x+0.86f,player.transform.position.y-0.86f,0);
			setQuatOnce=true;
			swungOnce=false;
			//setStartSwingPositionOnce = true;

		}
		if(Key.Equals("left")){
			interrupt=false;
			idle=false;
			swinging=true;
			startSwingPosition = new Vector3(player.transform.position.x-0.86f,player.transform.position.y-0.86f,0);
			setQuatOnce=true;
			swungOnce=false;
			//setStartSwingPositionOnce = true;
		}
		if(Key.Equals("right")){
			interrupt=false;
			idle=false;
			swinging=true;
			startSwingPosition = new Vector3(player.transform.position.x+0.86f,player.transform.position.y+0.86f,0);
			setQuatOnce=true;
			swungOnce=false;
			//setStartSwingPositionOnce = true;
		}
	}

	public IEnumerator swingMouse(){
		idle=false;
		swinging=true;
		yield return new WaitForSeconds(0.2f);

		gameObject.GetComponent<BoxCollider2D>().enabled = false;
		transform.rotation = Quaternion.Euler(0,0,defaultZRot);
		transform.position = new Vector3(transform.parent.position.x+defaultX,transform.parent.position.y+defaultY,transform.position.z);

		swinging=false;
		idle=true;
	}
}
