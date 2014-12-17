using UnityEngine;
using System.Collections;

public class script_health : MonoBehaviour {

	public float hp;
	public float maxHp;
	public float healthBarWidth;
	public int healthBarHeight;
	public GameObject healthBar;
	public GameObject hb;
	public GameObject player;
	public float xOffset;
	public float yOffset;

	// Use this for initialization
	void Start (){
		healthBarWidth=1;
		hb = Instantiate(healthBar,transform.position,transform.rotation) as GameObject;
		hb.SetActive(false);
		hb.transform.parent = transform;
		player = GameObject.Find("player");
	}
	
	// Update is called once per frame
	void Update () {
		hp=player.GetComponent<playerMovement>().health;
		hb.transform.position=Camera.main.WorldToViewportPoint(transform.position);
		hb.transform.localScale=Vector3.zero;
		float healthPercent = hp/maxHp*100;
		if(healthPercent<0){
			healthPercent=0;
		}
		if(healthPercent>100){
			healthPercent=100;
		}
		healthBarWidth = healthPercent*0.5f;
		hb.transform.Find("red").guiTexture.pixelInset=new Rect(xOffset,yOffset,100*0.5f,5);
		hb.transform.Find("green").guiTexture.pixelInset=new Rect(xOffset,yOffset,healthBarWidth,5);
	}
}
