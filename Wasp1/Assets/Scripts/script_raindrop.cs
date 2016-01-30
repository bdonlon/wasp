using UnityEngine;
using System.Collections;

public class script_raindrop : MonoBehaviour {
	
	Animator anim;
	Rigidbody2D rb;
	public Vector2 splashCoordY;
	public Vector3 spawnLocation;
	public SpriteRenderer spriteRenderer;
	public GameObject storm;
	public float jitterX,jitterY;

	// Use this for initialization
	void Start () {
	}

	void OnEnable(){
		jitterX=10f;
		jitterY=8f;

		anim = GetComponent<Animator>();
		storm = GameObject.Find("Storm");
		transform.parent = GameObject.Find("Storm").transform;
		spawnLocation.x = storm.transform.position.x + Random.Range(-jitterX,jitterX);
		spawnLocation.y = storm.transform.position.y + Random.Range(-jitterY,jitterY);
		transform.position = spawnLocation;
		rb = GetComponent<Rigidbody2D>();
		spriteRenderer.sortingOrder = (int)Camera.main.WorldToScreenPoint (splashCoordY).y * -1;
		rb.isKinematic=false;
		splashCoordY = new Vector2(0,storm.transform.position.y - Random.Range(100,200)*0.1f);
	}
	
	// Update is called once per frame
	void Update () {
		if(transform.position.y < (splashCoordY).y){
			transform.parent = GameObject.Find("cloneRain").transform;
			int splash = Random.Range (1,3);	//(inclusive,exclusive)
			switch(splash)
			{
			case 1:
				anim.SetTrigger("splash_01");
				break;
			case 2:
				anim.SetTrigger("splash_02");
				break;
			}
			rb.isKinematic=true;
			StartCoroutine(Destroy());

		}
	}

	IEnumerator Destroy()
	{
		yield return new WaitForSeconds(0.05f);
		anim.SetTrigger("reset");
		gameObject.SetActive(false);
	}
}
