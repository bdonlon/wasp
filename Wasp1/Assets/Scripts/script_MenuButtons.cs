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
	public float LSY,DY;
	public bool LSUp,LSDown,DUp,DDown,padUp,padDown=false;
	public bool padCurrentUp, padCurrentDown, padPreviousUp, padPreviousDown;

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
		if(Application.loadedLevelName.Equals("menu") || GM.GetComponent<Setup>().pauseGame)	//In game menu needs to be paused (variable via GM object). Title menu has no GM object.
		{
			padPreviousUp = padCurrentUp;
			padPreviousDown = padCurrentDown;
			padUp = false;
			padDown = false;

			LSY=Input.GetAxis ("360_LeftStickY");
			DY=Input.GetAxis ("360_DY");

			if(LSY<-0.1)							{	LSUp=true;			}
			if(LSY>-0.1)							{	LSUp=false;			}
			if(DY>0)								{	DUp=true;			}
			if(DY<0.1)								{	DUp=false;			}
			if(!LSUp	||	!DUp)					{	padCurrentUp=false;		}
			if(LSUp		||	DUp)					{	padCurrentUp=true;			}

			if(LSY>0.1)								{	LSDown=true;		}
			if(LSY<0.1)								{	LSDown=false;		}
			if(DY<0)								{	DDown=true;			}
			if(DY>-0.1)								{	DDown=false;		}
			if(!LSDown	||	!DDown)					{	padCurrentDown=false;		}
			if(LSDown	||	DDown)					{	padCurrentDown=true;		}

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
			if((Input.GetKeyDown(KeyCode.DownArrow) || padDown) && cursorIndex<menuOptions.Length -1)
			{
				hitSound.GetComponent<playSound>().play();
				cursorIndex++;
			}

			cursorPosition = new Vector3(cursorXposition, menuOptions[cursorIndex].transform.position.y, menuOptions[cursorIndex].transform.position.z);
			cursor.transform.position = cursorPosition;

			if((Input.GetKeyDown(KeyCode.Return)) || (Input.GetButtonDown("360_A")))
			{	
				cursorAnimator.SetTrigger("wasp_death");
				killSound.GetComponent<playSound>().play();
			}
		}
	}
}
