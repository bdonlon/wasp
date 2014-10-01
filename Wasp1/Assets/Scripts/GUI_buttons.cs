using UnityEngine;
using System.Collections;

public class GUI_buttons : MonoBehaviour {

	//public GameObject player;
	public GameObject GM;

	void OnGUI () {
		if (GM.GetComponent<Setup>().victoryCondition) {
			if (GUI.Button (new Rect (((Screen.width)/2)-100,((Screen.height)/2)-10,200,20), "VICTORY!")) {
				Application.LoadLevel(0);
			}
		}

		if (GM.GetComponent<Setup>().failureCondition) {
			if (GUI.Button (new Rect (((Screen.width)/2)-100,((Screen.height)/2)-10,200,20), "Restart")) {
				Application.LoadLevel(0);
			}
		}

		if (GM.GetComponent<Setup>().pauseGame) {
			if (GUI.Button (new Rect (((Screen.width)/2)-100,((Screen.height)/2)-100,200,20), "Resume Game")) {
				GM.GetComponent<Setup>().unPause();
			}
			if (GUI.Button (new Rect (((Screen.width)/2)-100,((Screen.height)/2)-50,200,20), "Exit to Main Menu")) {
				GM.GetComponent<Setup>().pauseGame=false;
				Application.LoadLevel("menu");
			}
			if (GUI.Button (new Rect (((Screen.width)/2)-100,((Screen.height)/2)-10,200,20), "Exit to Desktop")) {
				Application.Quit();
			}
		}


		//GUI.Box(new Rect(10,10,100,90), "Loader Menu");
	}
}
