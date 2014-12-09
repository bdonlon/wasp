using UnityEngine;
using System.Collections;

public class script_MenuButtons : MonoBehaviour {
	public GameObject GM;
	public GameObject[] menuOptions;
	public GameObject cursor;
	public GameObject killSound;
	public GameObject hitSound;
	public Vector3 cursorPosition;
	public int cursorIndex;
	public float cursorXposition;
	public float cursorXoffset;

	AudioSource cursorAudioSource;
	Animator cursorAnimator;

	void Start(){
		Screen.showCursor = false;

		cursorAnimator = cursor.GetComponent<Animator>();
		cursorAudioSource = cursor.GetComponent<AudioSource>();
		cursorAudioSource.ignoreListenerPause = true;

		cursorIndex=0;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		cursor.transform.localScale = theScale;

		cursorXposition = menuOptions[cursorIndex].transform.position.x-cursorXoffset;

		cursorPosition = new Vector3(cursorXposition, menuOptions[cursorIndex].transform.position.y, menuOptions[cursorIndex].transform.position.z);
		cursor.transform.position = cursorPosition;
	}

	void Update(){
		if(GM.GetComponent<Setup>().pauseGame)
		{
			if(Input.GetKeyDown(KeyCode.UpArrow) && cursorIndex > 0)
			{	
				//AudioListener.pause = false;
				hitSound.GetComponent<playSound>().play();
				cursorIndex--;
			}
			if(Input.GetKeyDown(KeyCode.DownArrow) && cursorIndex<menuOptions.Length -1)
			{
				hitSound.GetComponent<playSound>().play();
				cursorIndex++;
			}

			cursorPosition = new Vector3(cursorXposition, menuOptions[cursorIndex].transform.position.y, menuOptions[cursorIndex].transform.position.z);
			cursor.transform.position = cursorPosition;

			if(Input.GetKeyDown(KeyCode.Return))
			{	
				cursorAnimator.SetTrigger("wasp_death");
				killSound.GetComponent<playSound>().play();
				//StartCoroutine(runMenuOption());
			}
		}
	}

//	public IEnumerator runMenuOption(){
//
//
//		yield return new WaitForSeconds(0.2f);
//
//		if(cursorIndex==0)
//		{
//			Application.LoadLevel("wasp1");
//		}
//		else if(cursorIndex==1)
//		{
//			Application.LoadLevel("credits");
//		}
//		else if(cursorIndex==2)
//		{
//			Application.Quit();
//		}
//	}



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
