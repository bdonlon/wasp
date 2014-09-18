using UnityEngine;
using System.Collections;

public class crosshair : MonoBehaviour {

	public Quaternion q;
	public float angle;

	public GameObject localMouse;
	public GameObject swatter;

	// Update is called once per frame
	void Update () {

		Vector3 mousePosition = localMouse.transform.position;
		Vector3 playerPosition = transform.parent.position;





		//*****probably wrong place for this Key grabbing code
		if(Input.GetKeyDown(KeyCode.UpArrow))
		{
			//StartCoroutine(swatter.GetComponent<swatter_script>().swingKey("up"));
			swatter.GetComponent<swatter_script>().swingKey ("up");
			mousePosition = new Vector3(playerPosition.x-0.86f,playerPosition.y+0.86f,0);
		}
		if(Input.GetKeyUp (KeyCode.UpArrow)){
			swatter.GetComponent<swatter_script>().keyUp();
		}

		if(Input.GetKeyDown(KeyCode.DownArrow))
		{
			swatter.GetComponent<swatter_script>().swingKey ("down");
			mousePosition = new Vector3(playerPosition.x+0.86f,playerPosition.y-0.86f,0);
		}
		if(Input.GetKeyUp (KeyCode.DownArrow)){
			swatter.GetComponent<swatter_script>().keyUp();
		}

		if(Input.GetKeyDown(KeyCode.LeftArrow))
		{
			swatter.GetComponent<swatter_script>().swingKey ("left");
			mousePosition = new Vector3(playerPosition.x-0.86f,playerPosition.y-0.86f,0);
		}
		if(Input.GetKeyUp (KeyCode.LeftArrow)){
			swatter.GetComponent<swatter_script>().keyUp();
		}

		if(Input.GetKeyDown(KeyCode.RightArrow))
		{
			swatter.GetComponent<swatter_script>().swingKey ("right");
			mousePosition = new Vector3(playerPosition.x+0.86f,playerPosition.y+0.86f,0);
		}
		if(Input.GetKeyUp (KeyCode.RightArrow)){
			swatter.GetComponent<swatter_script>().keyUp();
		}
		//*******************************************************






		float vx = playerPosition.x - mousePosition.x;	//x vector between origin (static) and mouse position
		float vy = playerPosition.y - mousePosition.y;	//y vector between origin (static) and mouse position

		float mag = Mathf.Sqrt(vx*vx + vy*vy);	//length between origin and mouse position

		if(playerPosition!=mousePosition){	//guards against divide by 0
			vx = vx/mag;	//normalise
			vy = vy/mag;	//normalise
		}
			
		float distance = 1.5f;		//distance to travel along vector from player position
			
		float px = (mousePosition.x + (vx * (mag - distance)));		// x value displacement from center of origin or player
		float py = (mousePosition.y + (vy * (mag - distance)));		// y value displacement from center of origin or player

			
		Vector3 newPosition = new Vector3(px, py, -3);		//add x and y displacements to player position to get crosshair position
			
		transform.position = newPosition;

		
		//Rotate the crosshair
		Vector3 vectorToTarget = playerPosition - transform.position;
		angle = (Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg) + 90;
		q = Quaternion.AngleAxis(angle, Vector3.forward);
		transform.rotation = Quaternion.RotateTowards(transform.rotation, q, Time.deltaTime * 10000);
	}
}
