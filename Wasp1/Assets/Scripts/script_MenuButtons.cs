using UnityEngine;
using System.Collections;

public class script_MenuButtons : MonoBehaviour {

	public GameObject[] menuOptions;
	public GameObject cursor;
	public Vector3 cursorPosition;
	public int cursorIndex;
	public float cursorXposition;

	Animator cursorAnimator;

	void Start(){
		cursorAnimator = cursor.GetComponent<Animator>();
		cursorIndex=0;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		cursor.transform.localScale = theScale;

		cursorXposition = menuOptions[cursorIndex].transform.position.x-1.5f;

		cursorPosition = new Vector3(cursorXposition, menuOptions[cursorIndex].transform.position.y, menuOptions[cursorIndex].transform.position.z);
		cursor.transform.position = cursorPosition;
	}

	void Update(){
		if(Input.GetKeyDown(KeyCode.UpArrow) && cursorIndex > 0)
		{	
			cursorIndex--;
		}
		if(Input.GetKeyDown(KeyCode.DownArrow) && cursorIndex<menuOptions.Length){
			cursorIndex++;
		}

		cursorPosition = new Vector3(cursorXposition, menuOptions[cursorIndex].transform.position.y, menuOptions[cursorIndex].transform.position.z);
		cursor.transform.position = cursorPosition;

		if(Input.GetKeyDown(KeyCode.Return))
		{	
			StartCoroutine(runMenuOption());
		}
	}

	public IEnumerator runMenuOption(){
		cursorAnimator.SetTrigger("wasp_death");
		yield return new WaitForSeconds(0.2f);
		Debug.Log (cursorIndex);
		if(cursorIndex==0)
		{
			Debug.Log ("play!");
			Application.LoadLevel("wasp1");
		}
		else if(cursorIndex==1)
		{
			Debug.Log ("TODO: Create credits page!");
			//Application.LoadLevel("credits");
		}
		else if(cursorIndex==2)
		{
			Debug.Log ("exit!");
			Application.Quit();
		}
	}



//	void OnGUI () {
//			if (GUI.Button (new Rect (((Screen.width)/2)-100,((Screen.height)/2)-50,200,20), "Start Game")) {
//				Application.LoadLevel("wasp1");
//			}
//
//			if (GUI.Button (new Rect (((Screen.width)/2)-100,((Screen.height)/2)-10,200,20), "Exit to Desktop")) {
//				Application.Quit();
//			}
//	}
}
