using UnityEngine;
using System.Collections;

public class script_MenuButtons : MonoBehaviour {

	void OnGUI () {
			if (GUI.Button (new Rect (((Screen.width)/2)-100,((Screen.height)/2)-50,200,20), "Start Game")) {
				Application.LoadLevel("wasp1");
			}

			if (GUI.Button (new Rect (((Screen.width)/2)-100,((Screen.height)/2)-10,200,20), "Exit to Desktop")) {
				Application.Quit();
			}
	}
}
