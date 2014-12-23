using UnityEngine;
using System.Collections;

public class picnic_health_script : MonoBehaviour {

	public int picnicHealthCurrent;
	public int picnicPercentage;
	public int picnicHealthTotal;
	public GameObject spouse;
	public GameObject food;
	public GameObject _GM;
	public GameObject hb;
	public GameObject rug;
	public GameObject mainCam;
	public GameObject AnchorLeft;
	public GameObject AnchorRight;
	public float healthBarWidth,healthBarSize;
	public SpriteRenderer SR;

	public float timeEaten;
	public float elapsedTime;

	// Use this for initialization
	void Start () {
		timeEaten =  Time.time-100;
		picnicHealthCurrent = 200;
		picnicHealthTotal = picnicHealthCurrent;
		healthBarSize = Camera.main.WorldToScreenPoint(AnchorRight.transform.position).x - Camera.main.WorldToScreenPoint(AnchorLeft.transform.position).x;
	}
	
	// Update is called once per frame
	void Update () {
		drawHealthBar();
		float healthPercent = getPercentage();
		healthBarWidth = healthPercent*healthBarSize/100;
		hb.transform.Find("red").guiTexture.pixelInset=new Rect(0,0,healthBarSize,5);
		hb.transform.Find("green").guiTexture.pixelInset=new Rect(0,0,healthBarWidth,5);
	}

	public void damagePicnicIntegrity(int damage){
		if((picnicHealthCurrent-damage)>=0){
			timeEaten =  Time.time;
			picnicHealthCurrent = picnicHealthCurrent - damage;

			if(picnicHealthCurrent <= 0){
				_GM.GetComponent<Setup>().failureCondition=true;
			}
		}
	}

	public float getPercentage(){
		float value = (100f/picnicHealthTotal)*picnicHealthCurrent;
		return value;
	}

	void drawHealthBar(){
		elapsedTime =  Time.time - timeEaten;
		if(elapsedTime<1.5f){
			hb.active=true;
		}else{
			hb.active=false;
		}
	}
}
