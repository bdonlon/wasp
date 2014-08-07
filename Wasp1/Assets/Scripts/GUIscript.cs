using UnityEngine;
using System.Collections;

public class GUIscript : MonoBehaviour {

	public GameObject player;
	public string healthString;
	public int healthInt;

	// Update is called once per frame
	void Update () {
		healthInt = player.GetComponent<playerMovement>().health;
		healthString = "Health: "+healthInt.ToString();
		guiText.text = healthString;
	}
}
