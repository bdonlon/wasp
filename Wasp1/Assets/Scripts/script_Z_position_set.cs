using UnityEngine;
using System.Collections;

public class script_Z_position_set : MonoBehaviour {

	public SpriteRenderer spriteRenderer;
	//public int order;
	
	// Use this for initialization
	void Start () {
		spriteRenderer.sortingOrder = (int)Camera.main.WorldToScreenPoint (spriteRenderer.bounds.min).y * -1;
		//order = spriteRenderer.sortingOrder;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
