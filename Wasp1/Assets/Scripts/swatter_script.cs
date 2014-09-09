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

	private float swingDuration = 0.2f;
	private float swingSpeed = 0.22f;
	
	private float swingTimer = 0f;
	private bool swinging = false;

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
				Quaternion quat = Quaternion.AngleAxis(crosshair.GetComponent<crosshair>().angle+90, Vector3.forward);
				transform.rotation = Quaternion.RotateTowards(transform.rotation, quat, Time.deltaTime * 10000);
			}
			rotatePoint = new Vector3(player.transform.position.x,player.transform.position.y,0);
			transform.RotateAround(rotatePoint, Vector3.back, 10);
			if (swingTimer > swingDuration) {
				//reset swatter to idle position
				swingTimer = 0f;
				swinging = false;
				setStartSwingPositionOnce = true;
				transform.rotation = Quaternion.Euler(0,0,defaultZRot);
				transform.position = new Vector3(transform.parent.position.x+defaultX,transform.parent.position.y+defaultY,transform.position.z);
				gameObject.GetComponent<BoxCollider2D>().enabled = false;
			}
		}
	}

	public void swingKey(string Key){

		if(Key.Equals("up")){
			idle=false;
			swinging=true;
			startSwingPosition = new Vector3(player.transform.position.x+1,player.transform.position.y+1,0);

		}
		if(Key.Equals("down")){
			idle=false;
			swinging=true;

		}
		if(Key.Equals("left")){
			idle=false;
			swinging=true;

		}
		if(Key.Equals("right")){
			idle=false;
			swinging=true;

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
