using UnityEngine;
using System.Collections;

public class credits_script : MonoBehaviour {

	public GameObject[] menuOptions;
	public GameObject cursor;
	public GameObject killSound;
	public GameObject creditsGraphic;
	public Vector3 cursorPosition;
	public int cursorIndex;
	public float cursorXposition;
	public Vector3 force = new Vector3(0,0,0);
	
	Animator cursorAnimator;

	// Use this for initialization
	void Start () {
		Screen.showCursor = false;

		cursorAnimator = cursor.GetComponent<Animator>();
		cursorIndex=0;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		cursor.transform.localScale = theScale;

		cursorXposition = menuOptions[cursorIndex].transform.position.x-1.5f;
		
		cursorPosition = new Vector3(cursorXposition, menuOptions[cursorIndex].transform.position.y, menuOptions[cursorIndex].transform.position.z);
		cursor.transform.position = cursorPosition;
	}
	
	// Update is called once per frame
	void Update(){


		creditsGraphic.rigidbody2D.AddForce(force);
		
		if(Input.GetKeyDown(KeyCode.Return))
		{	
			StartCoroutine(runMenuOption());
		}
	}

	public IEnumerator runMenuOption(){
		cursorAnimator.SetTrigger("wasp_death");
		killSound.GetComponent<playSound>().play();
		
		yield return new WaitForSeconds(0.2f);
		if(cursorIndex==0)
		{
			Application.LoadLevel("menu");
		}
//		else if(cursorIndex==1)
//		{
//			Debug.Log ("TODO: Create credits page!");
//			//Application.LoadLevel("credits");
//		}
//		else if(cursorIndex==2)
//		{
//			Debug.Log ("exit!");
//			Application.Quit();
//		}
	}
}
