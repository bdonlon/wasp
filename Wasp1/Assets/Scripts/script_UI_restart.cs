using UnityEngine;
using System.Collections;

public class script_UI_restart : MonoBehaviour {
	
	public Sprite[] sprites;
	SpriteRenderer rend1;

	// Use this for initialization
	void Start () {

		rend1=this.GetComponent<SpriteRenderer>();
		rend1.sprite=sprites[0];	//"R to restart" for keyboard

		if(Input.GetJoystickNames().Length > 0){
			//Use controller graphic if controller is connected
			rend1.sprite=sprites[1];		//"LB + RB to restart" for controller
		}

		if(ApplicationModel.touchScreen){
			rend1.sprite=sprites[2];		//"Restart" for touch
		}
	}
	
	// Update is called once per frame
	void Update () {
		//Finger touching button...
		for (var i = 0; i < Input.touchCount; ++i) {
			if (Input.GetTouch(i).phase == TouchPhase.Began) {
				RaycastHit2D hitInfo = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position), Vector2.zero);
				clickedOrTouched(hitInfo);
			}
		}
	}

	public void clickedOrTouched(RaycastHit2D hitInfo){
		//if(hitInfo)
		//{
			Debug.Log( hitInfo.transform.gameObject.name );
			if(hitInfo.transform.gameObject.name.Equals("restartText"))
			{
				Application.LoadLevel("wasp1");
			}
		//}
	}
}
