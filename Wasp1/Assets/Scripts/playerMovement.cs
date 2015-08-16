using UnityEngine;
using System.Collections;

public class playerMovement : MonoBehaviour {
		
	private float speed = 10;
	public Vector3 velocity;
	public int health;
	private int maxHealth;
	public int score = 0;
	public GameObject _GM;
	public GameObject swatter;
	public GameObject UI;

	public bool LSUp,keyUp,LSDown,keyDown,LSLeft,keyLeft,LSRight,keyRight,DUp,DDown,DLeft,DRight = false;
	public bool keykeyup,keykeydown,keykeyleft,keykeyright = false;
	public GameObject VirtualJoystick;
	public bool up,down,left,right = false;
	public bool diagonal;

	public GameObject hitSound;
	public GameObject deathSound;
	public GameObject eatSound;


	public SpriteRenderer spriteRenderer;
	public SpriteRenderer swatterSpriteRenderer;

	public bool dead = false;
	public bool moving;
	public bool movingPrevious;
	private bool hurting;

	public float timeStung;
	public float timeCurr;
	public float elapsedTime;

	public float LSY,LSX,VLSY,VLSX,DX,DY;

	Animator anim;

	void Start(){
		maxHealth=health;
		timeStung =  Time.time-100;
		hurting=false;
		moving = false;
		movingPrevious=false;
		anim = GetComponent<Animator>();
	}

	void Update () {
		drawHealthBar();
		movingPrevious=moving;
		spriteRenderer.sortingOrder = (int)Camera.main.WorldToScreenPoint (spriteRenderer.bounds.min).y * -1;
		swatterSpriteRenderer.sortingOrder = spriteRenderer.sortingOrder;

		if(!dead){
			// not dead, therefore capture keystrokes and move
			LSY=Input.GetAxis ("360_LeftStickY");
			LSX=Input.GetAxis ("360_LeftStickX");
			DY=Input.GetAxis ("360_DY");
			DX=Input.GetAxis ("360_DX");
			VLSX=VirtualJoystick.GetComponent<Joystick>().position.x;
			VLSY=(VirtualJoystick.GetComponent<Joystick>().position.y*-1);
			if((VLSY<0.0001) && (VLSY>-0.001)){
				VLSY=0;
			}
			if((VLSX<0.0001) && (VLSX>-0.001)){
				VLSX=0;
			}
			if((VLSY!=0) || (VLSX!=0)){
				Vector2 v = new Vector2(VirtualJoystick.GetComponent<Joystick>().position.x,VirtualJoystick.GetComponent<Joystick>().position.y*-1);
				v = Vector2.ClampMagnitude(v,1);
				LSX = v.x;
				LSY = v.y;
			}

			//Usefull code for debugging virtual joystick values when testing with android emulator
				//Debug.Log ("x:"+LSX+" y:"+LSY);
				//UI.GetComponent<GUIscript_score>().x=LSX;
				//UI.GetComponent<GUIscript_score>().y=LSY;
							
			if(Input.GetKeyDown(KeyCode.W))			{	keykeyup=true;		}
			if(Input.GetKeyUp(KeyCode.W))			{	keykeyup=false;		}
			if(LSY<-0.1)							{	LSUp=true;			}
			if(LSY>-0.1)							{	LSUp=false;			}
			if(DY>0)								{	DUp=true;			}
			if(DY<0.1)								{	DUp=false;			}
			if(!keykeyup	||	!LSUp	||	!DUp)	{	keyUp=false;		}
			if(keykeyup		||	LSUp	||	DUp)	{	keyUp=true;			}

			if(Input.GetKeyDown(KeyCode.S))			{	keykeydown=true;	}
			if(Input.GetKeyUp(KeyCode.S))			{	keykeydown=false;	}
			if(LSY>0.1)								{	LSDown=true;		}
			if(LSY<0.1)								{	LSDown=false;		}
			if(DY<0)								{	DDown=true;			}
			if(DY>-0.1)								{	DDown=false;		}
			if(!keykeydown	||	!LSDown	||	!DDown)	{	keyDown=false;		}
			if(keykeydown	||	LSDown	||	DDown)	{	keyDown=true;		}

			if(Input.GetKeyDown(KeyCode.A))			{	keykeyleft=true;	}
			if(Input.GetKeyUp(KeyCode.A))			{	keykeyleft=false;	}
			if(LSX<-0.1)							{	LSLeft=true;		}
			if(LSX>-0.1)							{	LSLeft=false;		}
			if(DX<0)								{	DLeft=true;			}
			if(DX>-0.1)								{	DLeft=false;		}
			if(!keykeyleft	||	!LSLeft	||	!DLeft)	{	keyLeft=false;		}
			if(keykeyleft	||	LSLeft	||	DLeft)	{	keyLeft=true;		}

			if(Input.GetKeyDown(KeyCode.D))			{	keykeyright=true;	}
			if(Input.GetKeyUp(KeyCode.D))			{	keykeyright=false;	}
			if(LSX>0.1)								{	LSRight=true;		}
			if(LSX<0.1)								{	LSRight=false;		}
			if(DX>0)								{	DRight=true;		}
			if(DX<0.1)								{	DRight=false;		}
			if(!keykeyright	||	!LSRight||	!DRight){	keyRight=false;		}
			if(keykeyright	||	LSRight	||	DRight)	{	keyRight=true;		}

			calculateMoveDirection();
			move();
			setAnimationTrigger();
		}else{
			//player dead, no movement
		}
	}

	public void calculateMoveDirection(){
		if(keyUp)		{	up=true; 	}else{	up=false;		}
		if(keyDown)		{	down=true;	}else{	down=false;		}
		if(keyLeft)		{	left=true;	}else{	left=false;		}
		if(keyRight)	{	right=true;	}else{	right=false;	}
		if(up&&down){
			up = false;
			down = false;
		}
		if(left&&right){
			left=false;
			right=false;
		}

		if(up||down||left||right){	moving = true;	}else{	moving = false;	}

		diagonal=false;
		if((up&&left)||(up&&right)||(down&&left)||(down&&right)){	diagonal=true;	}
	}

	public void move()
	{
		velocity.x=0;
		velocity.y=0;

		speed=9;

		if(LSY==0 && LSX==0){	//skip if we are receiving L Stick input
			if(diagonal){
				speed=speed*0.68f;
			}

			if(up){
				velocity.y = speed;
			}
			if(down){
				velocity.y = speed*-1;
			}
			if(left){
				velocity.x = speed*-1;
			}
			if(right){
				velocity.x = speed;
			}
		}else{
			velocity.y = -LSY*speed;
			velocity.x = LSX*speed;
		}

		rigidbody2D.velocity=velocity;
	}

	public void setAnimationTrigger(){
		if(!_GM.GetComponent<Setup>().pauseGame && !hurting){
			if(left)				{	anim.SetTrigger("running_left");	}
			if(right)				{	anim.SetTrigger("running_right");	}
			if(up&&!left&&!right)	{	anim.SetTrigger("running_up");		}
			if(down&&!left&&!right)	{	anim.SetTrigger("running_down");	}

			if(!moving)				{	anim.SetTrigger("stop_running");	}
		}
	}

	public void injure(int damage){
		if(!dead && !hurting){	//may still recieve injure instructions after death		//hurting variable gives player a window of invulnerability (WaitForSeconds(0.4f)) after each sting
			timeStung =  Time.time;
			health = health-damage;
			StartCoroutine(injureAnimation());	//Play injured animation and start invulnerability timer/period
			hitSound.GetComponent<playSound>().play();
			if(health<=0){
				deathSound.GetComponent<playSound>().play();
				kill();
				_GM.GetComponent<Setup>().setFailureCondition(true);
				Screen.showCursor = true;
			}
		}
	}

	public IEnumerator heal(int healValue){
		timeStung =  Time.time;	//Will cause healthbar to render
		yield return new WaitForSeconds(0.3f);	//Delay will allow health increase to be apparent on the healthbar
		//if(!dead){}
		eatSound.GetComponent<playSound>().play();
		if((health+healValue)<maxHealth){
			health = health+healValue;
		}else{
			health = maxHealth;
		}


	}

	void drawHealthBar(){
		elapsedTime =  Time.time - timeStung;
		if(elapsedTime<1.5f){
			this.transform.Find("healthBar(Clone)").active=true;
		}else{
			this.transform.Find("healthBar(Clone)").active=false;
		}
	}

	public IEnumerator injureAnimation(){
		hurting=true;
		anim.SetBool("stop_running", false);
		anim.SetBool("running_left", false);
		anim.SetBool("running_right", false);
		anim.SetBool("running_up", false);
		anim.SetBool("running_down", false);
		anim.SetBool("player_hurt", false);
		anim.SetTrigger("player_hurt");
		yield return new WaitForSeconds(0.4f);
		hurting=false;
	}
	
	public void kill(){
		dead=true;

		anim.SetBool("stop_running", false);
		anim.SetBool("running_left", false);
		anim.SetBool("running_right", false);
		anim.SetBool("running_up", false);
		anim.SetBool("running_down", false);
		anim.SetBool("player_hurt", false);
		anim.SetTrigger("dead");

		StartCoroutine(swatter.GetComponent<swatter_script>().setDead());	//Disable swatter swings, and make it fall off the player

		Destroy(GetComponent<BoxCollider2D>());
	}
	
	public bool isDead(){
		return dead;
	}
	
	public int getScore(){
		return score;
	}

}