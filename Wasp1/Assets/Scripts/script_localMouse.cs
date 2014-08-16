using UnityEngine;
using System.Collections;

public class script_localMouse : MonoBehaviour {

	public Vector3 localMousePreviousPosition;
	public Vector3 localMouseCurrentPosition;

	public Vector2 playerPosition;
	public Vector2 localMouseNewPosition;

	public Vector3 worldMousePreviousPosition;
	public Vector3 worldMouseCurrentPosition;

	public GameObject player;
	public GameObject crosshair;

	public bool dragging;

	float deltaX;
	float deltaY;

	void Start(){
		playerPosition = new Vector2(player.transform.position.x,player.transform.position.y);
		worldMousePreviousPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		worldMouseCurrentPosition = worldMousePreviousPosition;
		localMousePreviousPosition= new Vector2(0,0);
		localMouseCurrentPosition=new Vector2(0,0);
		transform.position = player.transform.position;
		dragging = false;
	}
	
	void Update () {

		worldMousePreviousPosition = worldMouseCurrentPosition;
		playerPosition = new Vector2 (player.transform.position.x, player.transform.position.y);
	
		worldMouseCurrentPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

		deltaX = worldMouseCurrentPosition.x - worldMousePreviousPosition.x;
		deltaY = worldMouseCurrentPosition.y - worldMousePreviousPosition.y;

		if(deltaX!=0 || deltaY!=0)
		{
			dragging = true;
			localMouseNewPosition = new Vector2(playerPosition.x+localMouseCurrentPosition.x+deltaX, playerPosition.y+localMouseCurrentPosition.y+deltaY);

			transform.position = new Vector3 (localMouseNewPosition.x,localMouseNewPosition.y,-5);
			localMousePreviousPosition = new Vector2(localMouseCurrentPosition.x+deltaX, localMouseCurrentPosition.y+deltaY);
			localMouseCurrentPosition = new Vector2(localMouseCurrentPosition.x+deltaX, localMouseCurrentPosition.y+deltaY);
		}else{
			dragging = false;
		}

		if(!dragging && distanceBetween(new Vector2(playerPosition.x+localMouseCurrentPosition.x,playerPosition.y+localMouseCurrentPosition.y),playerPosition)>1.5f){
			reelIn();
		}
	}

	public void reelIn()
	{		
		deltaX = playerPosition.x - crosshair.transform.position.x;
		deltaY = playerPosition.y - crosshair.transform.position.y;

		localMouseNewPosition = new Vector2(playerPosition.x-deltaX, playerPosition.y-deltaY);

		transform.position = new Vector3 (localMouseNewPosition.x,localMouseNewPosition.y,-5);
		localMousePreviousPosition = new Vector2(0-deltaX, 0-deltaY);
		localMouseCurrentPosition = new Vector2(0-deltaX, 0-deltaY);
	}

	public float distanceBetween(Vector2 pos1, Vector2 pos2){

		float vx = pos1.x - pos2.x;	//x vector between origin (static) and mouse position
		float vy = pos1.y - pos2.y;	//y vector between origin (static) and mouse position
		
		float mag = Mathf.Sqrt(vx*vx + vy*vy);	//length between origin and mouse position
		return mag;
	}
}
