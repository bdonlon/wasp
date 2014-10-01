using UnityEngine;
using System.Collections;

public class swatter_script : MonoBehaviour {

	//public GameObject crosshair;
	public GameObject _GM;
	public bool idle;

	public float angle;
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

	public bool up,down,left,right = false;
	public bool preUp,preDown,preLeft,preRight = false;

	private float swingDuration = 0.15f;
	private float swingSpeed = 10;
	
	private float swingTimer = 0f;
	public bool swinging = false;
	public bool swungOnce=false;

	private Vector3 startRot;

	private Vector3 startSwingPosition;
	public Vector3 playerPosition;

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

		if(_GM.GetComponent<Setup>().pauseGame){
			//Game paused - Do nothing!
		}else{

			playerPosition = transform.parent.position;

			savePreviousInputState();

			//Keyboard handler
			if(Input.GetKeyDown(KeyCode.UpArrow))   {	up=true;		}
			if(Input.GetKeyUp  (KeyCode.UpArrow))   {	up=false;		}
			if(Input.GetKeyDown(KeyCode.DownArrow)) {	down=true;		}
			if(Input.GetKeyUp  (KeyCode.DownArrow)) {	down=false;		}
			if(Input.GetKeyDown(KeyCode.LeftArrow)) {	left=true;		}
			if(Input.GetKeyUp  (KeyCode.LeftArrow)) {	left=false;		}
			if(Input.GetKeyDown(KeyCode.RightArrow)){	right=true;		}
			if(Input.GetKeyUp  (KeyCode.RightArrow)){	right=false; 	}

			//decide which direction to swing (for changes midswing)
			analyseInput();

			//weapon swing checks
			checkSwinging();
			setStartSwingPosition();

			//weapon idle wobble
			if(idle){
				if(timer>20){
					wobble = wobble*-1;
					timer=0;
				}
				Vector3 rotatePoint = new Vector3(transform.parent.position.x+defaultX-0.45f,transform.parent.position.y+defaultY-0.2f,0);
				transform.RotateAround(rotatePoint, Vector3.back, wobble);
				timer++;
			}else if (swinging){
				gameObject.GetComponent<BoxCollider2D>().enabled = true;
				swingTimer += Time.deltaTime;

				if(setStartSwingPositionOnce){
					setStartSwingPositionOnce=false;
					transform.position = startSwingPosition;
				}

				Vector3 vectorToTarget = playerPosition - transform.position;
				angle = (Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg) + 90;
				quat = Quaternion.AngleAxis(angle+90, Vector3.forward);

				//rotate swatter to point away from player
				transform.rotation = Quaternion.RotateTowards(transform.rotation, quat, Time.deltaTime * 10000);

				rotatePoint = new Vector3(player.transform.position.x,player.transform.position.y,0);
				//rotate swatter as a swing
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
	}

	public void savePreviousInputState(){
		preUp=up;
		preDown=down;
		preLeft=left;
		preRight=right;
	}

	public void setStartSwingPosition(){
		float dx=0;
		float dy=0;
		if(up){
			dx=-0.86f;
			dy=0.86f;
		}
		if(down){
			dx=0.86f;
			dy=-0.86f;
		}
		if(left){
			dx=-0.86f;
			dy=-0.86f;
		}
		if(right){
			dx=0.86f;
			dy=0.86f;
		}

		startSwingPosition = new Vector3(player.transform.position.x+dx,player.transform.position.y+dy,0);
	}
	
	public void checkSwinging()
	{
		if(up||down||left||right)
		{
			swinging=true;
			idle=false;
			interrupt=false;
		}else{
			interrupt=true;
		}
	}

	public void analyseInput(){
		int test=0;
		if(up){test++;};
		if(down){test++;};
		if(left){test++;};
		if(right){test++;};

		if(test>1){	//multiple inputs
			interrupt=true;
			setStartSwingPositionOnce=true;	//allow in progress swing to break
			setLatest();	//set most recent key input as swing direction and negate previous in progress swing
		}
	}

	public void setLatest(){
		if(up&&preUp){
			up=false;
		}
		if(down&&preDown){
			down=false;
		}
		if(left&&preLeft){
			left=false;
		}
		if(right&&preRight){
			right=false;
		}
	}
}
