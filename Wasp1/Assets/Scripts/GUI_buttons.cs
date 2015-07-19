using UnityEngine;
using System.Collections;

public class GUI_buttons : MonoBehaviour {

	//public GameObject player;
	public GameObject GM;
	public GameObject pauseMenu;
	public GameObject restartMenu;
	public GameObject victoryMenu;
	public GameObject failureMessage;
	Animator cursorAnimator;
	//public GameObject killSound;
	//public GameObject hitSound;
	public GameObject cursor;
	public int cursorIndex;

	int menuVisible = 20;
	int menuHidden =-20;

	void Start(){
		cursorAnimator = cursor.GetComponent<Animator>();
	}

	void Update(){
		if(GM.GetComponent<Setup>().getFailureCondition())
		{
			restartMenu.transform.position = new Vector3(restartMenu.transform.position.x, restartMenu.transform.position.y, menuVisible);
			failureMessage.transform.position = new Vector3(failureMessage.transform.position.x, failureMessage.transform.position.y, menuVisible);
		}else if(GM.GetComponent<Setup>().getVictoryCondition()){
			victoryMenu.transform.position = new Vector3(victoryMenu.transform.position.x, victoryMenu.transform.position.y, menuVisible);
			restartMenu.transform.position = new Vector3(restartMenu.transform.position.x, restartMenu.transform.position.y, menuVisible);
		}else{
			restartMenu.transform.position = new Vector3(restartMenu.transform.position.x, restartMenu.transform.position.y, menuHidden);
			victoryMenu.transform.position = new Vector3(victoryMenu.transform.position.x, victoryMenu.transform.position.y, menuHidden);
			failureMessage.transform.position = new Vector3(failureMessage.transform.position.x, failureMessage.transform.position.y, menuHidden);
		}

		if (GM.GetComponent<Setup>().pauseGame) {
			//pauseMenu.SetActive(true);
			pauseMenu.transform.position = new Vector3(pauseMenu.transform.position.x, pauseMenu.transform.position.y, menuVisible);
			 
			if((Input.GetKeyDown(KeyCode.Return)) || (Input.GetButtonDown("360_A")) || pauseMenu.GetComponent<script_MenuButtons>().touchOrClickButtonPress)
			{	
				pauseMenu.GetComponent<script_MenuButtons>().touchOrClickButtonPress=false;
				runMenuOption();
			}
		
		}else{
			pauseMenu.GetComponent<script_MenuButtons>().cursorIndex=0;
			pauseMenu.transform.position = new Vector3(pauseMenu.transform.position.x, pauseMenu.transform.position.y, menuHidden);
			cursorAnimator.SetTrigger("wasp_reset");
			//pauseMenu.SetActive(false);
		}
	}

	public void runMenuOption(){
//		cursorAnimator.SetTrigger("wasp_death");
//		killSound.GetComponent<playSound>().play();
		//yield return new WaitForSeconds(0.001f);
		cursorIndex=pauseMenu.GetComponent<script_MenuButtons>().cursorIndex;
		if(cursorIndex==0)
		{
			GM.GetComponent<Setup>().unPause();
		}
		else if(cursorIndex==1)
		{
			GM.GetComponent<Setup>().pauseGame=false;
			Time.timeScale = 1.0f;
			AudioListener.pause = false;
			Application.LoadLevel("menu");
		}
		else if(cursorIndex==2)
		{
			Debug.Log ("Quitting");
			Application.Quit();
		}
	}

//		if (GM.GetComponent<Setup>().pauseGame) {
//			if (GUI.Button (new Rect (((Screen.width)/2)-100,((Screen.height)/2)-100,200,20), "Resume Game")) {
//				GM.GetComponent<Setup>().unPause();
//			}
//			if (GUI.Button (new Rect (((Screen.width)/2)-100,((Screen.height)/2)-50,200,20), "Exit to Main Menu")) {
//				GM.GetComponent<Setup>().pauseGame=false;
//				Time.timeScale = 1.0f;
//				AudioListener.pause = false;
//				Application.LoadLevel("menu");
//			}
//			if (GUI.Button (new Rect (((Screen.width)/2)-100,((Screen.height)/2)-10,200,20), "Exit to Desktop")) {
//				Application.Quit();
//			}
//		}


		//GUI.Box(new Rect(10,10,100,90), "Loader Menu");
	//}
}
