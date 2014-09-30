using UnityEngine;
using System.Collections;

public class script_MenuButtons : MonoBehaviour {

	void OnGUI () {
			if (GUI.Button (new Rect (((Screen.width)/2)-600,((Screen.height)/2)-10,200,20), "BEGIN SEIGE")) {
				Application.LoadLevel("wasp1");
			}

			if (GUI.Button (new Rect (((Screen.width)/2)-100,((Screen.height)/2)-10,200,20), "OH NO TOO MANY STINGS")) {
				Application.Quit();
			}
	}
}
