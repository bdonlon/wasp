using UnityEngine;
using System.Collections;

public class Setup : MonoBehaviour {

	public Camera mainCam;
	public BoxCollider2D borderTop,borderBottom,borderLeft,borderRight;
	public GameObject spawnerTop,spawnerBottom,spawnerLeft,spawnerRight;
	private Vector3 spawnerPosition = new Vector3(0,0,0);
	public GameObject player;
	public GameObject picnic_food;
	public GameObject grass;
	public GameObject storm;
	public GameObject graphic_waveInfo;
	public Animator picnicAnim;
	public Animator spouseAnim;
	public Animator picnicBasketAnim;

	public int numWasps;
	public int maxWasps;

	private int maxWaves;
	private int currentWave;
	public int waspsSpawnedThisWave;
	
	public bool spawnPhase;
	public bool pauseGame;

	public float width,height;

	public bool foodAvailable = false;
	private bool victoryCondition,failureCondition=false;

	private bool endless;
	public int endlessSwitch;
	public int endlessHealth;

	private float specialAlpha;

						//waveData rows = {maxWasps, healthFromFood}
	public int[,] waveData = new int[4,2] { {5, 10},		//wave 1
											{10, 20},		//wave 2
											{20, 30},		//wave 3
											{30, 40} };		//wave 4

	//public int[,] waveData = new int[1,2] { {5, 10}};		//1 wave for testing

	// Use this for initialization
	void Start () {
		endless=ApplicationModel.endless;
		endlessSwitch=0;
		pauseGame=false;
		Time.timeScale = 1.0f;
		AudioListener.pause = false;

		spawnPhase=false;
		currentWave=0;
		numWasps=0;
		maxWasps=waveData[currentWave,0];
		waspsSpawnedThisWave=0;
		maxWaves = waveData.GetLength(0);

		Screen.showCursor = false;

		width = Screen.width;
		height = Screen.height;

		borderTop.size = new Vector2 (mainCam.ScreenToWorldPoint (new Vector3 (width * 1.5f, 0, 0)).x ,1f);
		borderTop.center = new Vector2 (0f, mainCam.ScreenToWorldPoint (new Vector3 (0, height, 0)).y + 0.5f);

		spawnerPosition = new Vector3(0f,mainCam.ScreenToWorldPoint (new Vector3 (0, height, 0)).y + 2f, 0f);
		spawnerTop.transform.position = spawnerPosition;


		
		borderBottom.size   = new Vector2(mainCam.ScreenToWorldPoint(new Vector3 (width * 1.5f,0f,0f)).x, 1f);
		borderBottom.center = new Vector2(0f,mainCam.ScreenToWorldPoint(new Vector3 (0f,0f, 0f)).y-0.5f);

		spawnerPosition = new Vector3(0f,mainCam.ScreenToWorldPoint(new Vector3 (0f,0f, 0f)).y-2.0f, 0f);
		spawnerBottom.transform.position = spawnerPosition;



		borderLeft.size   = new Vector2(1f, mainCam.ScreenToWorldPoint(new Vector3 (0f, height * 1.5f,0f)).y);
		borderLeft.center = new Vector2(mainCam.ScreenToWorldPoint(new Vector3 (0f,0f, 0f)).x-0.5f,0f);

		spawnerPosition = new Vector3(mainCam.ScreenToWorldPoint(new Vector3 (0f,0f, 0f)).x-2.0f,0f,0f);
		spawnerLeft.transform.position = spawnerPosition;



		borderRight.size   = new Vector2(1f, mainCam.ScreenToWorldPoint(new Vector3 (0f, height * 1.5f,0f)).y);
		borderRight.center = new Vector2(mainCam.ScreenToWorldPoint(new Vector3 (width, 0f,0f)).x+0.5f,0f);

		spawnerPosition = new Vector3(mainCam.ScreenToWorldPoint(new Vector3 (width, 0f,0f)).x+2.0f,0f,0f);
		spawnerRight.transform.position = spawnerPosition;

		Physics2D.IgnoreLayerCollision(12, 08, true); //Disable collisions between wasps (layer 12) and borders (layer 8)
		//Physics2D.IgnoreLayerCollision(14, 14, true); //Doesn't work //Disable collisions for objects in layer 14 (wasps, when they are not tracking the player, so that they don't block eachother around picnic)
		Physics2D.IgnoreLayerCollision(12, 12, true); //Disable collisions between wasps
		Physics2D.IgnoreLayerCollision(10, 09, true); //Disable collisions between player and picnic
	
		StartCoroutine(setupPhase());
		endlessHealth=10;

		specialAlpha = -254;
	}

	public IEnumerator setupPhase(){

		if(foodAvailable){	//won't trigger on game start, but will between wasp phases
			for(int i=5; i>0; i--)
			{
				if(foodAvailable){
					yield return new WaitForSeconds(1.0f);	//wait 1 second then check again (up to max 5 seconds)
				}else{
					i=0;	//kill the loop
				}
			}
			graphic_waveInfo.GetComponent<script_GUI_wave_text>().setWaveInfo(currentWave,maxWaves);	//Render current wave info
		}
		foodAvailable=false;



		//animate rustlling in hamper
		spouseAnim.SetTrigger("spouse_basket_start");
		picnicBasketAnim.SetTrigger ("picnic_basket_open");

		yield return new WaitForSeconds(3.0f);

		//place new picnic food on platter
		setPlatter();
		spouseAnim.SetTrigger("spouse_basket_end");
		picnicBasketAnim.SetTrigger ("picnic_basket_close");
		spawnPhase=true;
	}

	public void setPlatter(){
		if(!endless){
			picnicAnim.SetTrigger("nextFood");
		}else{
			endlessSwitch=getSwitch(endlessSwitch);
			switch (endlessSwitch)
			{
			case 1:
				picnicAnim.SetTrigger("apple");
				break;
			case 2:
				picnicAnim.SetTrigger("sandwich");
				break;
			case 3:
				picnicAnim.SetTrigger("watermelon");
				break;
			case 4:
				picnicAnim.SetTrigger("cake");
				break;
			}
		}
	}

	public void setSpecialAlpha(float a){
		specialAlpha = a;
	}

	public float getSpecialAlpha(){
		return specialAlpha;
	}

	public int getSwitch(int lastSwitch){
		int random = Random.Range(1, maxWaves+1);
		while(random==lastSwitch){
			random = Random.Range(1, maxWaves+1);
		}
		return random;
	}

	public bool getEndless(){
		return endless;
	}

	public void foodConsumed(){	//To be called by (player or food?) script when food has been eaten
		picnicAnim.SetTrigger("eatFood");
		foodAvailable=false;
	}

	public void unPause()
	{	//To be triggered from pause menu button
		Screen.showCursor = false;
		pauseGame=false;
		Time.timeScale = 1.0f;
		AudioListener.pause = false;
	}

	public int getPlayerHealValue()
	{
		if(!endless){
			return waveData[currentWave-1,1];	//currentWave-1 because when player is able to access the food, the game logic has actually already moved into the next wave. so -1 to access the food health value for the previous wave, to which it belongs.
		}else{
			return endlessHealth;
		}
	}

	public void setFailureCondition(bool set){
		failureCondition=set;
		if(failureCondition){
			StartCoroutine(startStorm(1.0f));
		}
	}
	public bool getFailureCondition(){
		return failureCondition;
	}

	public void setVictoryCondition(bool set){
		victoryCondition=set;
		if(victoryCondition){
			StartCoroutine(startStorm(2.5f));	//Long-ish delay then storm starts when player wins :)
		}
	}

	public int getMaxWaves(){
		return maxWaves;
	}

	public int getCurrentWave(){
		return currentWave;
	}

	public bool getVictoryCondition(){
		return victoryCondition;
	}

	private IEnumerator startStorm(float delay){

		yield return new WaitForSeconds(delay);	//wait a few beats before starting the storm.
		storm.gameObject.active=true;
	}

	void Update(){

		if((Input.GetKeyDown(KeyCode.Escape)) || (Input.GetButtonDown("360_Start"))){
			pauseGame = !pauseGame;
			Screen.showCursor = true;
			if(pauseGame){
				Time.timeScale = 0.0f;
				AudioListener.pause = true;
			}else if(!pauseGame){
				Time.timeScale = 1.0f;
				AudioListener.pause = false;
			}
		}
		if(Input.GetButtonDown("360_B") && pauseGame){
			pauseGame = false;
			Time.timeScale = 1.0f;
			AudioListener.pause = false;
		}


		if(!endless){
			if(victoryCondition){
				if(Input.GetKeyDown(KeyCode.R) || ((Input.GetButton("360_L1"))&&(Input.GetButton("360_R1")))){
					Application.LoadLevel("wasp1");
				}
			}else if(failureCondition){
				spouseAnim.SetTrigger("spouse_cry_start");

				if(Input.GetKeyDown(KeyCode.R) || ((Input.GetButton("360_L1"))&&(Input.GetButton("360_R1")))){
					Application.LoadLevel("wasp1");
				}
			}else{
				if(waspsSpawnedThisWave>=maxWasps){
					spawnPhase = false;
					if(numWasps == 0){
						if((currentWave+1)==maxWaves){	//currentWave is indexed from 0 (eg 0-2), maxWaves is number of rows in waveData (eg 3)
							setVictoryCondition(true);
							foodAvailable=true;
						}else{
							currentWave++;
							maxWasps = waveData[currentWave,0];
							waspsSpawnedThisWave=0;
							foodAvailable=true;
							StartCoroutine(setupPhase());
						}
					}
				}
			}
		}else{
			if(failureCondition){
				spouseAnim.SetTrigger("spouse_cry_start");
				if(Input.GetKeyDown(KeyCode.R) || ((Input.GetButton("360_L1"))&&(Input.GetButton("360_R1")))){
					Application.LoadLevel("wasp1");
				}
			}else{
				if(waspsSpawnedThisWave>=maxWasps){
					spawnPhase = false;
					if(numWasps == 0)
					{
						currentWave++;
						maxWasps = (int)(maxWasps*1.5);	//50% more difficult each wave
						endlessHealth = (int)(endlessHealth*1.5);	//50% more health available each wave
						waspsSpawnedThisWave=0;
						foodAvailable=true;
						StartCoroutine(setupPhase());
					}
				}
			}
		}
	}
}