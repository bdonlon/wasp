using UnityEngine;
using System.Collections;


public class ApplicationModel : MonoBehaviour
{
	static public bool endless = false;
}

public class script_Main_Menu_GM : MonoBehaviour {

	public Camera cam1;
	public Camera cam2;
	public GameObject[] cameras;
	public GameObject[] cursors;
	public float[] cursorXoffset;
	public ButtonBankMatrix[] ButtonBank;
	public int currentCamera;
	public int cursorIndex;
	public GameObject killSound;
	public GameObject hitSound;
	public bool LSUp,LSDown,DUp,DDown,padUp,padDown=false;
	public bool padCurrentUp, padCurrentDown, padPreviousUp, padPreviousDown;
	public float LSY,DY;
	public float cursorXposition;
	public Vector3 cursorPosition;

	[System.Serializable]
	public class ButtonBankMatrix
	{
		public GameObject[] buttons;
	}

	void Start () {
		//Flip cursor orientation (I know, I should just fix the prefab...)
//		Vector3 theScale = transform.localScale;
//		theScale.x *= -1;
//		cursors[0].transform.localScale = theScale;
//		cursors[1].transform.localScale = theScale;


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

		padPreviousUp = padCurrentUp;
		padPreviousDown = padCurrentDown;
		padUp = false;
		padDown = false;
		
		LSY=Input.GetAxis ("360_LeftStickY");
		DY=Input.GetAxis ("360_DY");
		
		if(LSY<-0.1)							{	LSUp=true;				}
		if(LSY>-0.1)							{	LSUp=false;				}
		if(DY>0)								{	DUp=true;				}
		if(DY<0.1)								{	DUp=false;				}
		if(!LSUp	||	!DUp)					{	padCurrentUp=false;		}
		if(LSUp		||	DUp)					{	padCurrentUp=true;		}
		
		if(LSY>0.1)								{	LSDown=true;			}
		if(LSY<0.1)								{	LSDown=false;			}
		if(DY<0)								{	DDown=true;				}
		if(DY>-0.1)								{	DDown=false;			}
		if(!LSDown	||	!DDown)					{	padCurrentDown=false;	}
		if(LSDown	||	DDown)					{	padCurrentDown=true;	}
		
		if(padCurrentDown && !padPreviousDown){	//to prevent multiple down commands being registered for held input
			padDown = true;
		}
		if(padCurrentUp && !padPreviousUp){	//to prevent multiple up commands being registered for held input
			padUp = true;
		}
		
		if((Input.GetKeyDown(KeyCode.UpArrow) || padUp) && cursorIndex > 0)
		{	
			hitSound.GetComponent<playSound>().play();
			cursorIndex--;
		}
		if((Input.GetKeyDown(KeyCode.DownArrow) || padDown) && cursorIndex < ButtonBank[currentCamera].buttons.GetLength(0) -1)
		{
			hitSound.GetComponent<playSound>().play();
			cursorIndex++;
		}

		cursorXposition = ButtonBank[currentCamera].buttons[0].transform.position.x-cursorXoffset[currentCamera];
		cursorPosition = new Vector3(cursorXposition, ButtonBank[currentCamera].buttons[cursorIndex].transform.position.y, ButtonBank[currentCamera].buttons[cursorIndex].transform.position.z);
		cursors[currentCamera].transform.position = cursorPosition;
	}

	public IEnumerator runMenuOption(){
		yield return new WaitForSeconds(0.1f);

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
				//Waves
				Application.LoadLevel("wasp1");
				ApplicationModel.endless = false;
			}
			else if(cursorIndex==1)
			{
				//Endless
				Application.LoadLevel("wasp1");
				ApplicationModel.endless = true;
			}
			else if(cursorIndex==2)
			{
				changeScreen(0);
			}
			break;
		}
	}

	public void changeScreen(int cam){
		cursorIndex=0;	//reset cursor position when changing menu screen
		currentCamera=cam;
		for(int i=0; i<cameras.Length; i++)
		{
			if(i==currentCamera){
				cameras[i].camera.enabled=true;
				cursors[i].gameObject.active=true;
			}else{
				cameras[i].camera.enabled=false;
				cursors[i].gameObject.active=false;
			}
		}
	}
}
