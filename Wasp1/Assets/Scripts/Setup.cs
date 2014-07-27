using UnityEngine;
using System.Collections;

public class Setup : MonoBehaviour {

	//private Camera mainCam = new Camera();
	//public Camera MainCamera = GameObject.Find("MainCamera").camera;

	public Camera mainCam;
	public BoxCollider2D borderTop,borderBottom,borderLeft,borderRight;
	public GameObject spawnerTop,spawnerBottom,spawnerLeft,spawnerRight;
	private Vector3 spawnerPosition = new Vector3(0,0,0);
	public GameObject player;

	public int numWasps;
	public int maxWasps;

	public int maxWaves;
	public int currentWave;
	public int waspsSpawnedThisWave;
	
	public bool spawnPhase = false;

	public float width,height;

	public bool victoryCondition,failureCondition = false;

	// Use this for initialization
	void Start () {

		spawnPhase = true;
		numWasps=1;
		maxWasps=5;
		waspsSpawnedThisWave=0;
		currentWave=1;
		maxWaves = 4;

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
	}

	void Update(){

		if(victoryCondition){

		}else if(failureCondition){

		}else{
			if(waspsSpawnedThisWave>=maxWasps){
				spawnPhase = false;
				if(numWasps == 0){
					maxWasps = maxWasps*2;
					currentWave++;
					waspsSpawnedThisWave=0;
					if(currentWave==maxWaves && spawnPhase==false){
						victoryCondition = true;
					}else{
						spawnPhase = true;
					}
				}
			}
		}
	}
}