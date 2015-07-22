using UnityEngine;
using System.Collections;

public class GUIscript_score : MonoBehaviour {

	public GameObject player;
	public string scoreString;
	public int scoreInt;
	
	//public double x;
	//public double y;

	// Update is called once per frame
	void Update () {
		scoreInt = player.GetComponent<playerMovement>().score;
		scoreString = "Kills: "+scoreInt.ToString();

		//Usefull code for debugging virtual joystick values when testing with android emulator
			//scoreString ="x:"+x.ToString()+" y:"+y.ToString();

		guiText.text = scoreString;
	}
}
