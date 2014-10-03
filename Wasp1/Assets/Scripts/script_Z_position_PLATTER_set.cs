using UnityEngine;
using System.Collections;

public class script_Z_position_PLATTER_set : MonoBehaviour {

	public SpriteRenderer spriteRenderer;
	public SpriteRenderer foodSpriteRenderer;
	public int order;
	
	// Use this for initialization
	void Start () {
		spriteRenderer.sortingOrder = (int)Camera.main.WorldToScreenPoint (spriteRenderer.bounds.min).y * -1;
		order = spriteRenderer.sortingOrder;
		foodSpriteRenderer.sortingOrder = spriteRenderer.sortingOrder;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void setSortingOrder(){

	}
}
