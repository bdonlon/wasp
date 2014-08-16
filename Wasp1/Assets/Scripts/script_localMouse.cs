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

	float deltaX;
	float deltaY;

	void Start(){
		playerPosition = new Vector2(player.transform.position.x,player.transform.position.y);
		worldMousePreviousPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		worldMouseCurrentPosition = worldMousePreviousPosition;
		localMousePreviousPosition= new Vector2(0,0);
		localMouseCurrentPosition=new Vector2(0,0);
		transform.position = player.transform.position;



//		localMousePreviousPosition = transform.parent.position;
//		worldMousePreviousPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
	}

	// Update is called once per frame
	void Update () {

		worldMousePreviousPosition = worldMouseCurrentPosition;
		playerPosition = new Vector2 (player.transform.position.x, player.transform.position.y);
	
		worldMouseCurrentPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

		deltaX = worldMouseCurrentPosition.x - worldMousePreviousPosition.x;
		deltaY = worldMouseCurrentPosition.y - worldMousePreviousPosition.y;

		if(deltaX!=0 || deltaY!=0)
		{
			localMouseNewPosition = new Vector2(playerPosition.x+localMouseCurrentPosition.x+deltaX, playerPosition.y+localMouseCurrentPosition.y+deltaY);

			if(distanceBetween(localMouseNewPosition, playerPosition)<1.5){
				transform.position = new Vector3 (localMouseNewPosition.x,localMouseNewPosition.y,-5);
				localMousePreviousPosition = new Vector2(localMouseCurrentPosition.x+deltaX, localMouseCurrentPosition.y+deltaY);
				localMouseCurrentPosition = new Vector2(localMouseCurrentPosition.x+deltaX, localMouseCurrentPosition.y+deltaY);
			}
		}
	}

	public float distanceBetween(Vector2 pos1, Vector2 pos2){
		
		float vx = pos1.x - pos2.x;	//x vector between origin (static) and mouse position
		float vy = pos1.y - pos2.y;	//y vector between origin (static) and mouse position
		
		float mag = Mathf.Sqrt(vx*vx + vy*vy);	//length between origin and mouse position

		return mag;
	}
}
