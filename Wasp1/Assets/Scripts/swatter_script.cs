using UnityEngine;
using System.Collections;

public class swatter_script : MonoBehaviour {

	//public GameObject crosshair;
	public GameObject _GM;
	public GameObject swatter;
	public GameObject trail;
	public float trailTime;
	public bool idle;

	public float angle;
	public float wobble;
	public int timer;
	public float defaultX;
	public float defaultY;
	public float defaultZRot;
	public Vector3 rotatePoint;
	public GameObject player;
	public GameObject swingSound;
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
	public bool dead;

	public bool unpauseBuffer=false;

	private Vector3 startRot;

	public Vector3 startSwingPosition;
	public Vector3 playerPosition;

	// Use this for initialization
	void Start () {
		wobble = 0.2f;
		timer = 0;
		idle = true;
		setStartSwingPositionOnce = true;
		dead=false;

		defaultX = Mathf.Abs(transform.parent.position.x - transform.position.x);
		defaultY = Mathf.Abs(transform.parent.position.y - transform.position.y);
		defaultZRot = gameObject.transform.rotation.eulerAngles.z;

		startRot = transform.eulerAngles;
		trail.renderer.sortingLayerName="Elevated objects";
		trail.renderer.sortingOrder=0;
		trailTime = 1.0f;

		//trail.renderer.sortingLayerName="Ground objects";
		//trail.renderer.sortingOrder=10;
	}

	public IEnumerator setDead(){
		dead=true;
		Destroy(trail);
		swatter.GetComponent<swatter_script>().rigidbody2D.isKinematic=false;

		if(down)	{yield return new WaitForSeconds(0.2f);}
		else if(up)	{yield return new WaitForSeconds(0.5f);}
		else 		{yield return new WaitForSeconds(0.4f);}
		swatter.GetComponent<swatter_script>().rigidbody2D.isKinematic=true;
	}

	// Update is called once per frame
	void Update () {

		if(_GM.GetComponent<Setup>().pauseGame){
			//Game paused - Do nothing!{
			unpauseBuffer=true;
		}else if (dead){
			//dead, no swinging or idle wobling
		}else{

			playerPosition = transform.parent.position;

			savePreviousInputState();

			if(unpauseBuffer && ((Input.GetButtonUp("360_A")) || (Input.GetButtonUp("360_B")) || 
			                     (Input.GetKeyDown(KeyCode.UpArrow)) || (Input.GetKeyDown(KeyCode.DownArrow)) || 
			                     (Input.GetKeyDown(KeyCode.LeftArrow)) || (Input.GetKeyDown(KeyCode.RightArrow)))){
				unpauseBuffer = false;
			}
			if(!unpauseBuffer){
				//Keyboard handler
				if((Input.GetKeyDown(KeyCode.UpArrow)) 		|| (Input.GetButtonDown("360_Y")))   	{	up=true;		}
				if((Input.GetKeyUp  (KeyCode.UpArrow)) 		|| (Input.GetButtonUp("360_Y"))) 		{	up=false;		}
				if((Input.GetKeyDown(KeyCode.DownArrow))	|| (Input.GetButtonDown("360_A"))) 		{	down=true;		}
				if((Input.GetKeyUp  (KeyCode.DownArrow))	|| (Input.GetButtonUp("360_A"))) 		{	down=false;		}
				if((Input.GetKeyDown(KeyCode.LeftArrow))	|| (Input.GetButtonDown("360_X"))) 		{	left=true;		}
				if((Input.GetKeyUp  (KeyCode.LeftArrow))	|| (Input.GetButtonUp("360_X"))) 		{	left=false;		}
				if((Input.GetKeyDown(KeyCode.RightArrow))	|| (Input.GetButtonDown("360_B")))		{	right=true;		}
				if((Input.GetKeyUp  (KeyCode.RightArrow))	|| (Input.GetButtonUp("360_B")))		{	right=false; 	}
			}

			//decide which direction to swing (for changes midswing)
			analyseInput();

			//weapon swing checks
			checkSwinging();
			setStartSwingPosition();

			//weapon idle wobble
			if(idle){
				//trail.GetComponent<TrailRenderer>().enabled=false;
				if(timer>20){
					wobble = wobble*-1;
					timer=0;
				}
				Vector3 rotatePoint = new Vector3(transform.parent.position.x+defaultX-0.45f,transform.parent.position.y+defaultY-0.2f,0);
				transform.RotateAround(rotatePoint, Vector3.back, wobble);
				timer++;
			}else if (swinging){
				trail.GetComponent<TrailRenderer>().enabled=true;

				gameObject.GetComponent<BoxCollider2D>().enabled = true;
				swingTimer += Time.deltaTime;

				if(setStartSwingPositionOnce){
					Destroy(trail);
					//trail.GetComponent<TrailRenderer>().enabled=false;
					trail = (GameObject)Instantiate(trail);

					trail.transform.parent = GameObject.Find("swatter").transform;
					trail.transform.position = GameObject.Find("swatter").transform.position;
					trail.GetComponent<TrailRenderer>().time = trailTime;

					//print (trail.transform.position + " " + GameObject.Find("swatter").transform.position);
					//StartCoroutine(resetTrail());
					swingSound.GetComponent<playSound>().play();	//start of swing, play sound.
					setStartSwingPositionOnce=false;
					transform.position = startSwingPosition;
				//	trail.GetComponent<TrailRenderer>().enabled=false;
				//	trail.GetComponent<TrailRenderer>().enabled=true;

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
					//trail.GetComponent<TrailRenderer>().enabled=false;
				}

				if(swingTimer>swingDuration){
					//trail.GetComponent<TrailRenderer>().enabled=false;
					setStartSwingPositionOnce = true;
					swingTimer = 0f;
				}
				
				if (swungOnce && interrupt) {
					//reset swatter to idle position
					trail.GetComponent<TrailRenderer>().enabled=false;
					swingSound.GetComponent<playSound>().stop();	//interrupting swing, so stop swing sound.
					swingTimer = 0f;
					swinging = false;
					setStartSwingPositionOnce = true;
					swungOnce=false;
					transform.position = new Vector3(transform.parent.position.x+defaultX,transform.parent.position.y+defaultY,-1);
					gameObject.GetComponent<BoxCollider2D>().enabled = false;
					transform.rotation = Quaternion.Euler(0,0,defaultZRot);
					idle=true;
				}
			}
		}
	}

//	IEnumerator resetTrail(){
//		trail.GetComponent<TrailRenderer>().time=-1;
//		yield return new WaitForSeconds(0.01f);
//		trail.GetComponent<TrailRenderer>().time=trailTime;
//	}

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
			//StartCoroutine(resetTrail());
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
