using UnityEngine;
using System.Collections;

public class swatter_script : MonoBehaviour {

	public GameObject crosshair;
	public bool idle;
	public bool swinging;
	public float wobble;
	public int timer;
	public float defaultX;
	public float defaultY;
	public float defaultZRot;

	// Use this for initialization
	void Start () {
		wobble = 0.2f;
		timer = 0;
		idle = true;

		defaultX = Mathf.Abs(transform.parent.position.x - transform.position.x);
		defaultY = Mathf.Abs(transform.parent.position.y - transform.position.y);
		defaultZRot = gameObject.transform.rotation.eulerAngles.z;
	}
	
	// Update is called once per frame
	void Update () {

		//weapon idle wobble
		if(idle){
			if(timer>20){
				wobble = wobble*-1;
				timer=0;
			}

			//transform.Rotate(0,0,wobble);
			//transform.RotateAround (Vector3.zero, Vector3.up, wobble);
			Vector3 rotatePoint = new Vector3(transform.parent.position.x+defaultX-0.45f,transform.parent.position.y+defaultY-0.2f,0);
			transform.RotateAround(rotatePoint, Vector3.back, wobble);
			timer++;
		}else if (swinging){
			Debug.Log("swinging");
			gameObject.GetComponent<BoxCollider2D>().enabled = true;
			Vector3 newPosition = crosshair.transform.position;
			transform.position = newPosition;
			
			Quaternion quat = Quaternion.AngleAxis(crosshair.GetComponent<crosshair>().angle+90, Vector3.forward);
			transform.rotation = Quaternion.RotateTowards(transform.rotation, quat, Time.deltaTime * 10000);
		}
	}

//	public void disable(){
//		gameObject.active = false;
//	}
//
//	public void enable(){
//		gameObject.active = true;
//	}

	public IEnumerator swing(){
//		idle = false;
//
//		gameObject.GetComponent<BoxCollider2D>().enabled = true;
//
//		Vector3 newPosition = crosshair.transform.position;
//		transform.position = newPosition;
//
//		Quaternion quat = Quaternion.AngleAxis(crosshair.GetComponent<crosshair>().angle+90, Vector3.forward);
//		transform.rotation = Quaternion.RotateTowards(transform.rotation, quat, Time.deltaTime * 10000);
//
//		yield return new WaitForSeconds(0.2f);
//		gameObject.GetComponent<BoxCollider2D>().enabled = false;
//		transform.rotation = Quaternion.Euler(0,0,defaultZRot);
//		transform.position = new Vector3(transform.parent.position.x+defaultX,transform.parent.position.y+defaultY,transform.position.z);
//
//		idle = true;
		idle=false;
		swinging=true;
		yield return new WaitForSeconds(0.2f);

		gameObject.GetComponent<BoxCollider2D>().enabled = false;
		transform.rotation = Quaternion.Euler(0,0,defaultZRot);
		transform.position = new Vector3(transform.parent.position.x+defaultX,transform.parent.position.y+defaultY,transform.position.z);

		swinging=false;
		idle=true;
	}
}
