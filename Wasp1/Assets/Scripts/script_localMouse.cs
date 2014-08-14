using UnityEngine;
using System.Collections;

public class script_localMouse : MonoBehaviour {

	public Vector3 localMousePreviousPosition;
	public Vector3 localMouseCurrentPosition;
	public Vector3 localMouseNewPosition;

	public Vector3 worldMousePreviousPosition;
	public Vector3 worldMouseCurrentPosition;

	public GameObject player;

	float deltaX;
	float deltaY;

	void Start(){
		localMousePreviousPosition = player.transform.position;
		//transform.position = player.transform.position;
		worldMousePreviousPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);



//		localMousePreviousPosition = transform.parent.position;
//		worldMousePreviousPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
	}

	// Update is called once per frame
	void Update () {
	
		//x // this doesn't work. Need to look into calculating mouse movement around screen center which is static. translate mousemovemts around screen center to crosshair movement around player
		worldMouseCurrentPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

		deltaX = worldMouseCurrentPosition.x - worldMousePreviousPosition.x;
		deltaY = worldMouseCurrentPosition.y - worldMousePreviousPosition.y;

		if(deltaX!=0 || deltaY!=0)
		{

//			if(distanceBetween(new Vector3(localMouseCurrentPosition.x+deltaX,localMouseCurrentPosition.y,localMouseCurrentPosition.z), player.transform.position)<1.5){
//				localMouseCurrentPosition.x = transform.position.x+deltaX;
//			}
//			if(distanceBetween(new Vector3(localMouseCurrentPosition.x,localMouseCurrentPosition.y+deltaY,localMouseCurrentPosition.z), player.transform.position)<1.5){
//				localMouseCurrentPosition.x = transform.position.y+deltaY;
//			}
//			if((deltaX<0)&&(player.transform.position.x - localMouseCurrentPosition.x)<-5.1f){
//				localMouseCurrentPosition.x = transform.position.x+deltaX;
//			}
//			if((deltaY!=0)&&(Mathf.Abs(localMouseCurrentPosition.y + player.transform.position.y)<5.1f)){
//				localMouseCurrentPosition.y = transform.position.y+deltaY;
//			}





			localMouseCurrentPosition.x = transform.position.x+deltaX;
			localMouseCurrentPosition.y = transform.position.y+deltaY;
			transform.position = localMouseCurrentPosition;
		}
		
		localMousePreviousPosition = localMouseCurrentPosition;
		worldMousePreviousPosition = worldMouseCurrentPosition;
	}

	public float distanceBetween(Vector3 pos1, Vector3 pos2){
		
		float vx = pos1.x - pos2.x;	//x vector between origin (static) and mouse position
		float vy = pos1.y - pos2.y;	//y vector between origin (static) and mouse position
		
		float mag = Mathf.Sqrt(vx*vx + vy*vy);	//length between origin and mouse position

		//Debug.Log(mag);
		return mag;
	}
}
