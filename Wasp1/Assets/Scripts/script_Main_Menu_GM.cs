using UnityEngine;
using System.Collections;

public class script_Main_Menu_GM : MonoBehaviour {

	public Camera cam1;
	public Camera cam2;
	public GameObject[] cameras;
	public GameObject[] cursors;
	public ButtonBankMatrix[] ButtonBank;
	public int currentCamera;
	public int cursorIndex;
	public GameObject killSound;
	public GameObject hitSound;
	public bool LSUp,LSDown,DUp,DDown,padUp,padDown=false;
	public bool padCurrentUp, padCurrentDown, padPreviousUp, padPreviousDown;
	public float LSY,DY;
	public float cursorXposition;
	public float cursorXoffset;

	[System.Serializable]
	public class ButtonBankMatrix
	{
		public GameObject[] buttons;
	}

	void Start () {
		cursorIndex=0;
		currentCamera=0;
		changeScreen(currentCamera);
	}

	void Update () {
		if(Input.GetKeyDown(KeyCode.Return) || Input.GetButtonDown("360_A"))
		{	
			cursors[currentCamera].gameObject.GetComponent<Animator>().SetTrigger("wasp_death");
			killSound.GetComponent<playSound>().play();
			StartCoroutine(runMenuOption());
		}
//
//		padPreviousUp = padCurrentUp;
//		padPreviousDown = padCurrentDown;
//		padUp = false;
//		padDown = false;
//		
//		LSY=Input.GetAxis ("360_LeftStickY");
//		DY=Input.GetAxis ("360_DY");
//		
//		if(LSY<-0.1)							{	LSUp=true;			}
//		if(LSY>-0.1)							{	LSUp=false;			}
//		if(DY>0)								{	DUp=true;			}
//		if(DY<0.1)								{	DUp=false;			}
//		if(!LSUp	||	!DUp)					{	padCurrentUp=false;		}
//		if(LSUp		||	DUp)					{	padCurrentUp=true;			}
//		
//		if(LSY>0.1)								{	LSDown=true;		}
//		if(LSY<0.1)								{	LSDown=false;		}
//		if(DY<0)								{	DDown=true;			}
//		if(DY>-0.1)								{	DDown=false;		}
//		if(!LSDown	||	!DDown)					{	padCurrentDown=false;		}
//		if(LSDown	||	DDown)					{	padCurrentDown=true;		}
//		
//		if(padCurrentDown && !padPreviousDown){	//to prevent multiple down commands being registered for held input
//			padDown = true;
//		}
//		if(padCurrentUp && !padPreviousUp){	//to prevent multiple up commands being registered for held input
//			padUp = true;
//		}
//		
//		if((Input.GetKeyDown(KeyCode.UpArrow) || padUp) && cursorIndex > 0)
//		{	
//			hitSound.GetComponent<playSound>().play();
//			cursorIndex--;
//		}
//		if((Input.GetKeyDown(KeyCode.DownArrow) || padDown) && cursorIndex< buttonBanks.Length -1)
//		{
//			hitSound.GetComponent<playSound>().play();
//			cursorIndex++;
//		}
//
//		//cursorPosition = new Vector3(cursorXposition, menuOptions[cursorIndex].transform.position.y, menuOptions[cursorIndex].transform.position.z);
//		
//		cursorPosition = new Vector3(cursorXposition, buttonBanks[currentCamera][cursorIndex].transform.position.y, buttonBanks[currentCamera][cursorIndex].transform.position.z);
//		cursor.transform.position = cursorPosition;
	}

	public IEnumerator runMenuOption(){
		yield return new WaitForSeconds(0.001f);
		//cursorIndex=buttons.GetComponent<script_MenuButtons>().cursorIndex;

		switch (currentCamera)
		{
		case 0:
			if(cursorIndex==0)
			{
				changeScreen(1);
			}
			else if(cursorIndex==1)
			{
				Application.LoadLevel("credits");
			}
			else if(cursorIndex==2)
			{
				Application.Quit();
			}
			break;
		case 1:
			if(cursorIndex==0)
			{
				Application.LoadLevel("wasp1");
			}
			else if(cursorIndex==1)
			{
				Application.LoadLevel("wasp1");
			}
			else if(cursorIndex==2)
			{
				changeScreen(0);
			}
			break;
		}
	}

	public void changeScreen(int cam){
		for(int i=0; i<cameras.Length; i++)
		{
			if(i==cam){
				cameras[i].camera.enabled=true;
				cursors[i].gameObject.active=true;
			}else{
				cameras[i].camera.enabled=false;
				cursors[i].gameObject.active=false;
			}
		}
	}
}
