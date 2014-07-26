using UnityEngine;
using System.Collections;

public class GUIscript_score : MonoBehaviour {

	public GameObject player;
	public string scoreString;
	public int scoreInt;

	// Update is called once per frame
	void Update () {
		scoreInt = player.GetComponent<playerMovement>().score;
		scoreString = "Kills: "+scoreInt.ToString();
		guiText.text = scoreString;
	}
}
