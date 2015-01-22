using UnityEngine;
using System.Collections;

public class script_mainMenuButtons : MonoBehaviour {

	public GameObject buttons;
	int cursorIndex;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Return) || Input.GetButtonDown("360_A"))
		{	
			StartCoroutine(runMenuOption());
		}
	}

	public IEnumerator runMenuOption(){
		//		cursorAnimator.SetTrigger("wasp_death");
		//		killSound.GetComponent<playSound>().play();
		yield return new WaitForSeconds(0.001f);
		cursorIndex=buttons.GetComponent<script_MenuButtons>().cursorIndex;
		if(cursorIndex==0)
		{
			Application.LoadLevel("wasp1");
		}
		else if(cursorIndex==1)
		{
			Application.LoadLevel("credits");
		}
		else if(cursorIndex==2)
		{
			Application.Quit();
		}
	}
}
