using UnityEngine;
using System.Collections;


public class ApplicationModel : MonoBehaviour
{
	static public bool endless = false;
	static public bool equalityMode = false;
	static public bool touchScreen = false;
	static public bool autoSwing = false;
}

public class script_Main_Menu_GM : MonoBehaviour {

	public Camera cam1;
	public Camera cam2;
	public GameObject[] cameras;
	public GameObject[] cursors;
	public float[] cursorXoffset;
	public ButtonBankMatrix[] ButtonBank;
	public int currentCamera;
	public int cursorIndex;
	public GameObject killSound;
	public GameObject hitSound;
	public bool LSUp,LSDown,DUp,DDown,padUp,padDown=false;
	public bool padCurrentUp, padCurrentDown, padPreviousUp, padPreviousDown, backButton;
	public bool touchOrClickButtonPress;
	public float LSY,DY;
	public float cursorXposition;
	public Vector3 cursorPosition;
	public Vector3 creditsForce = new Vector3(0,1,0);
	private Vector3 creditsInitialPosition;
	private Vector3 rainbowStartPosition;
	public GameObject creditsGraphic;
	public GameObject gamepadText;
	public Sprite[] gamepadDetected;
	public GameObject controllerGraphic;
	public GameObject box_checked;
	public GameObject box_unchecked;
	public GameObject rainbow;
	SpriteRenderer gamepadDetectedRenderer;
	private int timer;
	private float scaleRate;

	[System.Serializable]
	public class ButtonBankMatrix
	{
		public GameObject[] buttons;
	}

	void Start () {
		if (Application.platform == RuntimePlatform.Android){ApplicationModel.touchScreen=true;}	//Check if using a touchscreen
		touchOrClickButtonPress=false;
		backButton=false;
		scaleRate = 0.004f;
		timer = 0;
		cursorIndex=0;
		currentCamera=0;
		changeScreen(currentCamera);
		creditsInitialPosition = creditsGraphic.transform.position;

		gamepadDetectedRenderer=gamepadText.GetComponent<SpriteRenderer>();

		if(ApplicationModel.equalityMode){	// For when player exits from game to menu, having previously set equality mode on/off
			box_checked.gameObject.SetActive(true);
			box_unchecked.gameObject.SetActive(false);
		}else if(!ApplicationModel.equalityMode){
			box_checked.gameObject.SetActive(false);
			box_unchecked.gameObject.SetActive(true);
		}

		rainbowStartPosition = rainbow.transform.position;
	}

	void Update () {

		//Mouse clicking button...
		if(Input.GetMouseButtonDown(0)){
			Vector2 pos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
			RaycastHit2D hitInfo = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(pos), Vector2.zero);
			touchOrClickButtonPress = clickedOrTouched(hitInfo);
		}

		//Finger touching button...
		for (var i = 0; i < Input.touchCount; ++i) {
			if (Input.GetTouch(i).phase == TouchPhase.Began) {
				RaycastHit2D hitInfo = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position), Vector2.zero);
				touchOrClickButtonPress = clickedOrTouched(hitInfo);
			}
		}

		animateControllerGraphic();

		if(currentCamera==2){
			creditsGraphic.rigidbody2D.AddForce(creditsForce);
		}

		if(Input.GetKeyDown(KeyCode.Return) || Input.GetButtonDown("360_A") || touchOrClickButtonPress)
		{	
			touchOrClickButtonPress=false;
			if(!(currentCamera==4 && cursorIndex==0)){
				cursors[currentCamera].gameObject.GetComponent<Animator>().SetTrigger("wasp_death");
			}
			killSound.GetComponent<playSound>().play();
			StartCoroutine(runMenuOption(currentCamera));
		}else if(currentCamera!=0 && (Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("360_B"))){	// 'Back' in menu
			backButton=true;
			cursors[currentCamera].gameObject.GetComponent<Animator>().SetTrigger("wasp_death");
			killSound.GetComponent<playSound>().play();
			StartCoroutine(runMenuOption(currentCamera));
		}

		padPreviousUp = padCurrentUp;
		padPreviousDown = padCurrentDown;
		padUp = false;
		padDown = false;
		
		LSY=Input.GetAxis ("360_LeftStickY");
		DY=Input.GetAxis ("360_DY");
		
		if(LSY<-0.1)							{	LSUp=true;				}
		if(LSY>-0.1)							{	LSUp=false;				}
		if(DY>0)								{	DUp=true;				}
		if(DY<0.1)								{	DUp=false;				}
		if(!LSUp	||	!DUp)					{	padCurrentUp=false;		}
		if(LSUp		||	DUp)					{	padCurrentUp=true;		}
		
		if(LSY>0.1)								{	LSDown=true;			}
		if(LSY<0.1)								{	LSDown=false;			}
		if(DY<0)								{	DDown=true;				}
		if(DY>-0.1)								{	DDown=false;			}
		if(!LSDown	||	!DDown)					{	padCurrentDown=false;	}
		if(LSDown	||	DDown)					{	padCurrentDown=true;	}
		
		if(padCurrentDown && !padPreviousDown){	//to prevent multiple down commands being registered for held input
			padDown = true;
		}
		if(padCurrentUp && !padPreviousUp){	//to prevent multiple up commands being registered for held input
			padUp = true;
		}
		
		if((Input.GetKeyDown(KeyCode.UpArrow) || padUp) && cursorIndex > 0)
		{	
			hitSound.GetComponent<playSound>().play();
			cursorIndex--;
		}
		if((Input.GetKeyDown(KeyCode.DownArrow) || padDown) && cursorIndex < ButtonBank[currentCamera].buttons.GetLength(0) -1)
		{
			hitSound.GetComponent<playSound>().play();
			cursorIndex++;
		}

		cursorXposition = ButtonBank[currentCamera].buttons[0].transform.position.x-cursorXoffset[currentCamera];
		cursorPosition = new Vector3(cursorXposition, ButtonBank[currentCamera].buttons[cursorIndex].transform.position.y, ButtonBank[currentCamera].buttons[cursorIndex].transform.position.z);
		cursors[currentCamera].transform.position = cursorPosition;
	}
	
	public IEnumerator runMenuOption(int cam){
		yield return new WaitForSeconds(0.1f);

		switch(cam)
		{
		case -1:	// Go 'back' from any screen
			changeScreen (0);
			break;
		case 0:
			if(cursorIndex==0)
			{
				if(ApplicationModel.touchScreen){	//If touchscreen, force easy mode
					//Easy Mode
					ApplicationModel.autoSwing = true;
					changeScreen(1);
				}else{								//else, allow choice
					changeScreen(3);	//Difficulty
				}
			}
			else if(cursorIndex==1)
			{
				changeScreen(4);	//Options
			}
			else if(cursorIndex==2)
			{
				changeScreen(2);	//Credits
			}
			else if(cursorIndex==3)
			{
				Application.Quit();	//Quit
			}
			break;
		case 1:	//Game mode/type
			if(backButton || cursorIndex==2)
			{
				backButton=false;
				if(ApplicationModel.touchScreen){	//If touchscreen, do not show difficulty screen
					changeScreen(0);
				}else{								//if not, go to difficulty screen
					changeScreen(3);
				}
			}
			else if(cursorIndex==0)
			{
				//Waves
				ApplicationModel.endless = false;
				Application.LoadLevel("wasp1");
			}
			else if(cursorIndex==1)
			{
				//Endless
				ApplicationModel.endless = true;
				Application.LoadLevel("wasp1");
			}
			break;
		case 2:
			if(cursorIndex==0)
			{
				changeScreen(0);
				creditsGraphic.rigidbody2D.velocity = Vector3.zero;
				creditsGraphic.transform.position=creditsInitialPosition;
			}
			break;
		case 3:	//Difficulty
			if(backButton || cursorIndex==2)
			{
				backButton=false;
				changeScreen(0);
			}
			else if(cursorIndex==0)
			{
				//Easy Mode
				ApplicationModel.autoSwing = true;
				changeScreen(1);
			}
			else if(cursorIndex==1)
			{
				//Hard Mode
				ApplicationModel.autoSwing = false;
				changeScreen(1);
			}
			break;
		case 4:
			if(backButton || cursorIndex==1)
			{
				backButton=false;
				changeScreen(0);
			}
			else if(cursorIndex==0)
			{
				if(ApplicationModel.equalityMode){
					box_checked.gameObject.SetActive(false);
					box_unchecked.gameObject.SetActive(true);
					ApplicationModel.equalityMode=false;
				}else if(!ApplicationModel.equalityMode){
					box_checked.gameObject.SetActive(true);
					box_unchecked.gameObject.SetActive(false);
					ApplicationModel.equalityMode=true;
					StartCoroutine(rollRainbow());
				}
				break;
			}
			break;
		}
	}

	public bool clickedOrTouched(RaycastHit2D hitInfo){
		bool hit = false;
		if(hitInfo)
		{
			//Debug.Log( hitInfo.transform.gameObject.name );
			if(hitInfo.transform.gameObject.name.Equals("button_start")){cursorIndex=0;}
			if(hitInfo.transform.gameObject.name.Equals("button_options")){cursorIndex=1;}
			if(hitInfo.transform.gameObject.name.Equals("button_credits")){cursorIndex=2;}
			if(hitInfo.transform.gameObject.name.Equals("button_exit")){cursorIndex=3;}
			
			if(hitInfo.transform.gameObject.name.Equals("button_easy")){cursorIndex=0;}
			if(hitInfo.transform.gameObject.name.Equals("button_hard")){cursorIndex=1;}
			if(hitInfo.transform.gameObject.name.Equals("button_difficulty_back")){cursorIndex=2;}
			
			if(hitInfo.transform.gameObject.name.Equals("button_set_waves")){cursorIndex=0;}
			if(hitInfo.transform.gameObject.name.Equals("button_endless")){cursorIndex=1;}
			if(hitInfo.transform.gameObject.name.Equals("button_mode_back")){cursorIndex=2;}
			
			if(hitInfo.transform.gameObject.name.Equals("box_checked")){cursorIndex=0;}
			if(hitInfo.transform.gameObject.name.Equals("box_unchecked")){cursorIndex=0;}
			if(hitInfo.transform.gameObject.name.Equals("button_options_back")){cursorIndex=1;}
			
			if(hitInfo.transform.gameObject.name.Equals("button_credits_back")){cursorIndex=0;}

			hit=true;
		}
		return hit;
	}

	public void changeScreen(int cam){
		cursorIndex=0;	//reset cursor position when changing menu screen
		currentCamera=cam;
		for(int i=0; i<cameras.Length; i++)
		{
			if(i==currentCamera){
				cameras[i].camera.enabled=true;
				cursors[i].gameObject.SetActive(true);
			}else{
				cameras[i].camera.enabled=false;
				cursors[i].gameObject.SetActive(false);
			}
		}
	}

	void animateControllerGraphic(){
		if(Input.GetJoystickNames().Length > 0)
		{
			if(timer>20){
				scaleRate = scaleRate*-1;
				timer=0;
			}
			controllerGraphic.transform.localScale = controllerGraphic.transform.localScale + (Vector3.one * scaleRate);
			timer++;
		}
		
		if(Input.GetJoystickNames().Length > 0){
			//Use controller graphic if controller is connected
			gamepadDetectedRenderer.sprite=gamepadDetected[0];
		}else{
			gamepadDetectedRenderer.sprite=gamepadDetected[1];
		}
	}

	IEnumerator rollRainbow(){
		rainbow.transform.position = rainbowStartPosition;
		for(int i=0; i<50; i++){
			Vector3 temp = new Vector3(0,1.0f,0);
			rainbow.transform.position += temp;
			yield return new WaitForSeconds(0.0f);
		}
		rainbow.transform.position = rainbowStartPosition;
	}
}
