using UnityEngine;
using System.Collections;

public class script_localMouse : MonoBehaviour {

	public Vector3 localMousePreviousPosition;
	public Vector3 localMouseCurrentPosition;
	public Vector3 localMouseNewPosition;

	public Vector3 worldMousePreviousPosition;
	public Vector3 worldMouseCurrentPosition;

	float deltaX;
	float deltaY;

	void Start(){
		localMousePreviousPosition = transform.parent.position;
		worldMousePreviousPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
	}

	// Update is called once per frame
	void Update () {
	

		//x // this doesn't work. Need to look into calculating mouse movement around screen center which is static. translate mousemovemts around screen center to crosshair movement around player
		worldMouseCurrentPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

		deltaX = worldMouseCurrentPosition.x - worldMousePreviousPosition.x;
		deltaY = worldMouseCurrentPosition.y - worldMousePreviousPosition.y;

		if(deltaX!=0 || deltaY!=0)
		{
//				localMouseCurrentPosition.x = localMousePreviousPosition.x+deltaX;
//				localMouseCurrentPosition.y = localMousePreviousPosition.y+deltaY;

			localMouseCurrentPosition.x = transform.position.x+deltaX;
			localMouseCurrentPosition.y = transform.position.y+deltaY;

			transform.position = localMouseCurrentPosition;
		}

			

		localMousePreviousPosition = localMouseCurrentPosition;
		worldMousePreviousPosition = worldMouseCurrentPosition;
	}
}
