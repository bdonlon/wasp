using UnityEngine;
using System.Collections;

public class script_cursor : MonoBehaviour {
	
	public Sprite sprite1;
	public Sprite sprite2;

	public SpriteRenderer spriteRenderer;

	void Start()
	{
		spriteRenderer = this.GetComponent<SpriteRenderer>();
		spriteRenderer.sprite = sprite2;
	} 

	void Update()
	{
		spriteRenderer.sprite = sprite2;
	}
}
