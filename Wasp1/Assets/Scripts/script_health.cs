using UnityEngine;
using System.Collections;

public class script_health : MonoBehaviour {

	public float hp;
	public float maxHp;
	public float healthBarWidth;
	public int healthBarHeight;
	public GameObject healthBar;
	public GameObject hb;

	// Use this for initialization
	void Start (){
		healthBarWidth=1;
		hb = Instantiate(healthBar,transform.position,transform.rotation) as GameObject;
	}
	
	// Update is called once per frame
	void Update () {
		hb.transform.position=Camera.main.WorldToViewportPoint(transform.position);

//		hb.transform.position.x -= 0.05;
//		hb.transform.position.y -= 0.05;
		Debug.Log (transform.position);
		//hb.transform.position = new Vector3(transform.position.x,transform.position.y,transform.position.z);
		hb.transform.localScale=Vector3.zero;
		float healthPercent = hp/maxHp;
		if(healthPercent<0){
			healthPercent=0;
		}
		if(healthPercent>0){
			healthPercent=100;
		}
		healthBarWidth = healthPercent*0.5f;
		hb.guiTexture.pixelInset=new Rect(10,10,healthBarWidth,5);
	}
}
